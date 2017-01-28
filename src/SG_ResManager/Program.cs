using Mono.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SG_ResManager
{
    public class Program
    {
        private enum operation
        {
            none,
            extract,
            view,
            create,
        }
        public static void Main(string[] args)
        {
            SG_ResManagerLib.Program resMgr = new SG_ResManagerLib.Program();

            int selection = 0;
            while (selection != 4)
            {
                Console.Clear();
                Console.WriteLine("Res Manager");
                Console.WriteLine("---------------------");
                Console.WriteLine("Please select an option:");
                Console.WriteLine("1. View contents of res files.");
                Console.WriteLine("2. Extract contents of res files.");
                Console.WriteLine("3. Create new res archive.");
                Console.WriteLine("4. Exit.");
                Console.Write("Selection: ");

                string sel = Console.ReadLine();

                if (int.TryParse(sel, out selection))
                {
                    switch (selection)
                    {
                        case 1:
                            Console.Write("Path to res.000: ");
                            string respath = Console.ReadLine();
                            if (System.IO.File.Exists(respath))
                            {
                                foreach (SG_ResManagerLib.SgFile f in resMgr.ReadContents(respath))
                                {
                                    Console.WriteLine(string.Format("res.{0}: {1} | {2} bytes", f.FileNumber.ToString().PadLeft(3, '0'), f.FileName, f.FileSize));
                                }
                            }
                            else
                            {
                                Console.WriteLine("File does not exist!");
                            }
                            break;
                        case 2:
                            Console.Write("Path to client folder: ");
                            string clientpath = Console.ReadLine();
                            if (System.IO.Directory.Exists(clientpath))
                            {
                                Console.Write("Path to output directory: ");
                                string outputdir = Console.ReadLine();
                                if (!System.IO.Directory.Exists(outputdir)) {
                                    Console.Write("Directory does not exist! Do you want to create it? (Y/n): ");
                                    string createDir = Console.ReadLine();
                                    if (createDir.ToLower() == "y" || string.IsNullOrEmpty(createDir))
                                    {
                                        System.IO.Directory.CreateDirectory(outputdir);
                                    }
                                    else
                                    {
                                        break;
                                    }
                                }
                                Console.WriteLine("Loading contents...");
                                List<SG_ResManagerLib.SgFile> fileList = resMgr.ReadContents(clientpath + @"\res.000");
                                Console.WriteLine("Saving files...");
                                foreach (SG_ResManagerLib.SgFile sgf in fileList)
                                {
                                    Console.WriteLine("Processig: " + sgf.FileName);
                                    if (!System.IO.Directory.Exists(outputdir + @"\" + System.IO.Path.GetExtension(sgf.FileName).Replace(".", "")))
                                    {
                                        System.IO.Directory.CreateDirectory(outputdir + @"\" + System.IO.Path.GetExtension(sgf.FileName).Replace(".", ""));
                                    }
                                    System.IO.File.WriteAllBytes(outputdir + @"\" + System.IO.Path.GetExtension(sgf.FileName).Replace(".", "") + @"\" + sgf.FileName, resMgr.GetFile(clientpath, sgf));
                                }
                                Console.WriteLine("Done!");
                            }
                            else
                            {
                                Console.WriteLine("Directory does not exist");
                            }

                            break;
                        case 3:
                            Console.Write("Path to folder containing files to archive: ");
                            string resourcePath = Console.ReadLine();
                            if (System.IO.Directory.Exists(resourcePath))
                            {
                                Console.Write("Output path: ");
                                string outputdir = Console.ReadLine();
                                if (!System.IO.Directory.Exists(outputdir))
                                {
                                    Console.Write("Directory does not exist! Do you want to create it? (Y/n): ");
                                    string createDir = Console.ReadLine();
                                    if (createDir.ToLower() == "y" || string.IsNullOrEmpty(createDir))
                                    {
                                        System.IO.Directory.CreateDirectory(outputdir);
                                    }
                                    else
                                    {
                                        break;
                                    }
                                }
                                List<string> filelist = new List<string>(System.IO.Directory.GetFiles(resourcePath, "*.*", System.IO.SearchOption.AllDirectories));
                                resMgr.CreateArchiveProgress += ResMgr_CreateArchiveProgress;
                                resMgr.CreateArchive(outputdir, filelist);
                            }
                            else
                            {
                                Console.WriteLine("Path does not exist!");
                            }

                                break;
                        case 4:
                            Console.WriteLine("Bye!");
                            break;
                        default:
                            Console.WriteLine(selection + " is not a valid option!");
                            break;
                    }

                }
                else
                {
                    Console.WriteLine(sel + " is not a valid option!");
                }
                if (selection != 4)
                {
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                }
            }
        }
        /// <summary>
        /// Reports the progress while archiving files
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void ResMgr_CreateArchiveProgress(object sender, SG_ResManagerLib.CreateArchiveProgressEventArgs e)
        {
            if (e.CreatingIndex)
            {
                Console.WriteLine("Generating res.000");
            }
            else
            {
                Console.WriteLine("Processing: " + e.CurrentFile);
            }
        }
    }
}
