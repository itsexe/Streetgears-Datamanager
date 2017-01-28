using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SG_ResManagerLib
{
    public class CreateArchiveProgressEventArgs : EventArgs
    {
        public bool CreatingIndex { get; }
        public string CurrentFile { get; }
        public int CurrentProgress { get; }
        public int MaxProgress { get; }
        public CreateArchiveProgressEventArgs(bool creatingIndex)
        {
            CreatingIndex = creatingIndex;
        }
        public CreateArchiveProgressEventArgs(string currentFile, int currentProgress, int maxProgress)
        {
            CurrentFile = currentFile;
            CurrentProgress = currentProgress;
            MaxProgress = maxProgress;
        }
    }
    public class Program
    {
        public event EventHandler<CreateArchiveProgressEventArgs> CreateArchiveProgress;
        /// <summary>
        /// Returns the contents of the res files. 
        /// </summary>
        /// <param name="indexFile">Path to res.000</param>
        public List<SgFile> ReadContents(string indexFile)
        {
            List<SgFile> retList = new List<SgFile>();

            using(FileStream fs = new FileStream(indexFile, FileMode.Open, FileAccess.Read))
            {
                using (BinaryReader br = new BinaryReader(fs))
                {
                    byte xors = 0;
                    while (br.BaseStream.Position < fs.Length)
                    {
                        byte SizeOfFileName = br.ReadByte();

                        //Decrypt size of file name
                        SizeOfFileName ^= FileHelper.Xor[xors]; 
                        xors++;

                        //Decrypt file name
                        byte[] EncodedFileName = br.ReadBytes(SizeOfFileName);
                        for (int i = 0; i < SizeOfFileName; i++)
                        {
                            EncodedFileName[i] ^= FileHelper.Xor[xors];
                            xors++;
                        }

                        //Decrypt file size 
                        byte[] FileSize = br.ReadBytes(8);
                        for (int i = 0; i < 8; i++)
                        {
                            FileSize[i] ^= FileHelper.Xor[xors];
                            xors++;
                        }

                        //Add file to list
                        retList.Add(new SgFile(FileHelper.DecodeFileName(BytesToString(EncodedFileName)), BytesToString(EncodedFileName), BitConverter.ToInt32(FileSize, 4), BitConverter.ToInt32(FileSize, 0)));
                    }
                }
            }
            return retList;
        }

        /// <summary>
        /// Returns a file as byte array
        /// </summary>
        /// <param name="resPath">Path to the folder which contains the res.xxx files</param>
        /// <param name="file">File that you want to extract</param>
        /// <returns></returns>
        public byte[] GetFile(string resPath, SgFile file)
        {
            using (FileStream fs = new FileStream(resPath + @"\res." + file.FileNumber.ToString().PadLeft(3, '0'), FileMode.Open, FileAccess.Read))
            {
                fs.Seek(file.Offset, SeekOrigin.Begin);
                using(BinaryReader br = new BinaryReader(fs))
                {
                    byte[] retBytes = br.ReadBytes(file.FileSize);
                    if (file.IsEncrypted())
                    {
                        byte xors = 0;
                        for (int i = 0; i < file.FileSize; i++)
                        {
                            retBytes[i] ^= FileHelper.Xor[xors];
                            xors++;
                        }
                    }
                    return retBytes;
                }
            }
        }

        /// <summary>
        /// Adds files to an res.xxx archive
        /// EXISTING RES.XXX FILES WILL BE REMOVED
        /// </summary>
        /// <param name="destPath">Destination path for the res.xxx files</param>
        /// <param name="files">Files that you want to archive</param>
        public void CreateArchive(string destPath, List<String> files)
        {
            if (!Directory.Exists(destPath))
                throw (new Exception("Destination directory does not exist!"));
            for (int i = 0; i < 300; i++)
            {

                File.Create(destPath + @"\res." + i.ToString().PadLeft(3, '0')).Dispose();
            }

            //Convert all paths to SgFile and write them to the res.xxx files
            List<SgFile> sgfiles = new List<SgFile>();
            foreach (string f in files)
            {
                SgFile currentFile = new SgFile();
                FileInfo fInfo = new FileInfo(f);
                currentFile.FileName = Path.GetFileName(f);
                currentFile.EncodedFileName = FileHelper.EncodeFileName(currentFile.FileName);
                currentFile.FileSize = (int)fInfo.Length;
                using (FileStream fs = new FileStream(destPath + @"\res." + currentFile.FileNumber.ToString().PadLeft(3, '0'), FileMode.Append, FileAccess.Write)) {
                    using (BinaryWriter bw = new BinaryWriter(fs))
                    {
                        currentFile.Offset = (int)fs.Position;
                        byte[] filebytes = File.ReadAllBytes(f);
                        if (currentFile.IsEncrypted())
                        {
                            byte xors = 0;
                            for (int i = 0; i < filebytes.Length; i++)
                            {
                                filebytes[i] ^= FileHelper.Xor[xors];
                                xors++;
                            }
                        }
                        bw.Write(filebytes);
                    }
                }
                sgfiles.Add(currentFile);
                CreateArchiveProgress(this, new CreateArchiveProgressEventArgs(currentFile.FileName, sgfiles.Count, files.Count));
            }


            //Create Index (res.000)
            CreateArchiveProgress(this, new CreateArchiveProgressEventArgs(true));
            using(FileStream fs = new FileStream(destPath + @"\res.000", FileMode.Open,FileAccess.Write))
            {
                using(BinaryWriter bw = new BinaryWriter(fs))
                {
                    byte xors = 0;
                    foreach (SgFile currentFile in sgfiles)
                    {
                    
                        //Write file name lenght
                        int fileNameLength = currentFile.EncodedFileName.Length;
                        fileNameLength ^= FileHelper.Xor[xors];
                        xors++;
                        bw.Write((byte)fileNameLength);

                        //Write file name
                        byte[] filename = Encoding.ASCII.GetBytes(FileHelper.EncodeFileName(currentFile.FileName));
                        for (int i = 0; i < filename.Length; i++)
                        {
                            filename[i] ^= FileHelper.Xor[xors];
                            xors++;
                        }
                        bw.Write(filename);

                        //Write offset and size
                        byte[] data = new byte[8];
                        byte[] off = BitConverter.GetBytes(currentFile.Offset);
                        byte[] fz = BitConverter.GetBytes(currentFile.FileSize);
                        data[0] = off[0];
                        data[1] = off[1];
                        data[2] = off[2];
                        data[3] = off[3];
                        data[4] = fz[0];
                        data[5] = fz[1];
                        data[6] = fz[2];
                        data[7] = fz[3];

                        for (int i = 0; i < 8; i++)
                        {
                            data[i] ^= FileHelper.Xor[xors];
                            xors++;
                        }
                        bw.Write(data);
                    }
                }
            }



        }

        private static string BytesToString(byte[] b)
        {
            int num = 0;
            for (int i = 0; i < b.Length && b[i] > 0; i++)
            {
                num++;
            }
            byte[] numArray = new byte[num];
            for (int i = 0; i < num && b[i] > 0; i++)
            {
                numArray[i] = b[i];
            }
            return System.Text.Encoding.ASCII.GetString(numArray);
        }

    }
}

