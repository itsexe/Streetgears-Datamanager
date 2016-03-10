using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SGunpacker
{
    public class SgdataIndex
    {
        public string Name;
        public string Hash;
        public int Size;
        public string DataId;
        public int Offset;
        public string Path;
    }


    static class StreetgearsDataCipher
    {
        private static readonly FileNameCipher Fhash = new FileNameCipher();

        private static readonly byte[] Xor = new byte[] { 
            0x77, 0xE8, 0x5E, 0xEC, 0xB7, 0x4E, 0xC1, 0x87, 0x4F, 0xE6, 0xF5, 0x3C, 0x1F, 0xB3, 0x15, 0x43,
            0x6A, 0x49, 0x30, 0xA6, 0xBF, 0x53, 0xA8, 0x35, 0x5B, 0xE5, 0x9E, 0x0E, 0x41, 0xEC, 0x22, 0xB8,
            0xD4, 0x80, 0xA4, 0x8C, 0xCE, 0x65, 0x13, 0x1D, 0x4B, 0x08, 0x5A, 0x6A, 0xBB, 0x6F, 0xAD, 0x25,
            0xB8, 0xDD, 0xCC, 0x77, 0x30, 0x74, 0xAC, 0x8C, 0x5A, 0x4A, 0x9A, 0x9B, 0x36, 0xBC, 0x53, 0x0A,
            0x3C, 0xF8, 0x96, 0x0B, 0x5D, 0xAA, 0x28, 0xA9, 0xB2, 0x82, 0x13, 0x6E, 0xF1, 0xC1, 0x93, 0xA9,
            0x9E, 0x5F, 0x20, 0xCF, 0xD4, 0xCC, 0x5B, 0x2E, 0x16, 0xF5, 0xC9, 0x4C, 0xB2, 0x1C, 0x57, 0xEE,
            0x14, 0xED, 0xF9, 0x72, 0x97, 0x22, 0x1B, 0x4A, 0xA4, 0x2E, 0xB8, 0x96, 0xEF, 0x4B, 0x3F, 0x8E,
            0xAB, 0x60, 0x5D, 0x7F, 0x2C, 0xB8, 0xAD, 0x43, 0xAD, 0x76, 0x8F, 0x5F, 0x92, 0xE6, 0x4E, 0xA7,
            0xD4, 0x47, 0x19, 0x6B, 0x69, 0x34, 0xB5, 0x0E, 0x62, 0x6D, 0xA4, 0x52, 0xB9, 0xE3, 0xE0, 0x64,
            0x43, 0x3D, 0xE3, 0x70, 0xF5, 0x90, 0xB3, 0xA2, 0x06, 0x42, 0x02, 0x98, 0x29, 0x50, 0x3F, 0xFD,
            0x97, 0x58, 0x68, 0x01, 0x8C, 0x1E, 0x0F, 0xEF, 0x8B, 0xB3, 0x41, 0x44, 0x96, 0x21, 0xA8, 0xDA,
            0x5E, 0x8B, 0x4A, 0x53, 0x1B, 0xFD, 0xF5, 0x21, 0x3F, 0xF7, 0xBA, 0x68, 0x47, 0xF9, 0x65, 0xDF,
            0x52, 0xCE, 0xE0, 0xDE, 0xEC, 0xEF, 0xCD, 0x77, 0xA2, 0x0E, 0xBC, 0x38, 0x2F, 0x64, 0x12, 0x8D,
            0xF0, 0x5C, 0xE0, 0x0B, 0x59, 0xD6, 0x2D, 0x99, 0xCD, 0xE7, 0x01, 0x15, 0xE0, 0x67, 0xF4, 0x32,
            0x35, 0xD4, 0x11, 0x21, 0xC3, 0xDE, 0x98, 0x65, 0xED, 0x54, 0x9D, 0x1C, 0xB9, 0xB0, 0xAA, 0xA9,
            0x0C, 0x8A, 0xB4, 0x66, 0x60, 0xE1, 0xFF, 0x2E, 0xC8, 0x00, 0x43, 0xA9, 0x67, 0x37, 0xDB, 0x9C,
            0x77, 0xE8, 0x5E, 0xEC, 0xB7, 0x4E, 0xC1, 0x87, 0x4F, 0xE6, 0xF5, 0x3C, 0x1F, 0xB3, 0x15, 0x43,
            0x6A, 0x49, 0x30, 0xA6, 0xBF, 0x53, 0xA8, 0x35, 0x5B, 0xE5, 0x9E, 0x0E, 0x41, 0xEC, 0x22, 0xB8,
            0xD4, 0x80, 0xA4, 0x8C, 0xCE, 0x65, 0x13, 0x1D, 0x4B, 0x08, 0x5A, 0x6A, 0xBB, 0x6F, 0xAD, 0x25,
            0xB8, 0xDD, 0xCC, 0x77, 0x30, 0x74, 0xAC, 0x8C, 0x5A, 0x4A, 0x9A, 0x9B, 0x36, 0xBC, 0x53, 0x0A,
            0x3C, 0xF8, 0x96, 0x0B, 0x5D, 0xAA, 0x28, 0xA9, 0xB2, 0x82, 0x13, 0x6E, 0xF1, 0xC1, 0x93, 0xA9,
            0x9E, 0x5F, 0x20, 0xCF, 0xD4, 0xCC, 0x5B, 0x2E, 0x16, 0xF5, 0xC9, 0x4C, 0xB2, 0x1C, 0x57, 0xEE,
            0x14, 0xED, 0xF9, 0x72, 0x97, 0x22, 0x1B, 0x4A, 0xA4, 0x2E, 0xB8, 0x96, 0xEF, 0x4B, 0x3F, 0x8E,
            0xAB, 0x60, 0x5D, 0x7F, 0x2C, 0xB8, 0xAD, 0x43, 0xAD, 0x76, 0x8F, 0x5F, 0x92, 0xE6, 0x4E, 0xA7,
            0xD4, 0x47, 0x19, 0x6B, 0x69, 0x34, 0xB5, 0x0E, 0x62, 0x6D, 0xA4, 0x52, 0xB9, 0xE3, 0xE0, 0x64,
            0x43, 0x3D, 0xE3, 0x70, 0xF5, 0x90, 0xB3, 0xA2, 0x06, 0x42, 0x02, 0x98, 0x29, 0x50, 0x3F, 0xFD,
            0x97, 0x58, 0x68, 0x01, 0x8C, 0x1E, 0x0F, 0xEF, 0x8B, 0xB3, 0x41, 0x44, 0x96, 0x21, 0xA8, 0xDA,
            0x5E, 0x8B, 0x4A, 0x53, 0x1B, 0xFD, 0xF5, 0x21, 0x3F, 0xF7, 0xBA, 0x68, 0x47, 0xF9, 0x65, 0xDF,
            0x52, 0xCE, 0xE0, 0xDE, 0xEC, 0xEF, 0xCD, 0x77, 0xA2, 0x0E, 0xBC, 0x38, 0x2F, 0x64, 0x12, 0x8D,
            0xF0, 0x5C, 0xE0, 0x0B, 0x59, 0xD6, 0x2D, 0x99, 0xCD, 0xE7, 0x01, 0x15, 0xE0, 0x67, 0xF4, 0x32,
            0x35, 0xD4, 0x11, 0x21, 0xC3, 0xDE, 0x98, 0x65, 0xED, 0x54, 0x9D, 0x1C, 0xB9, 0xB0, 0xAA, 0xA9,
            0x0C, 0x8A, 0xB4, 0x66, 0x60, 0xE1, 0xFF, 0x2E, 0xC8, 0x00, 0x43, 0xA9, 0x67, 0x37, 0xDB, 0x9C
        };
        public static readonly List<SgdataIndex> SgData = new List<SgdataIndex>();
        private static readonly List<SgdataIndex> SgPack = new List<SgdataIndex>();

        public static void Open(string res000File)
        {
            SgData.Clear();

            try
            {
                FileStream fs = new FileStream(res000File, FileMode.Open, FileAccess.Read);
                BinaryReader br = new BinaryReader(fs);

                uint fsl = (uint)fs.Length;
                uint cp = 0;
                byte xors = 0;

                while (cp < fsl)
                {   
                    SgdataIndex sgIndex = new SgdataIndex();
                    byte size = br.ReadByte();
                    cp++;

                    size ^= Xor[xors];
                    xors++;
                    byte[] hash = br.ReadBytes(size);

                    for (int i = 0; i < size; i++)
                    {
                        hash[i] ^= Xor[xors];
                        xors++;
                        cp++;
                    }

                    byte[] data = br.ReadBytes(8);

                    for (int i = 0; i < 8; i++)
                    {
                        data[i] ^= Xor[xors];
                        xors++;
                        cp++;
                    }

                    sgIndex.Name = Fhash.DecodeFileName(Sfm.BytesToString(hash));
                    sgIndex.Hash = Sfm.BytesToString(hash);
                    sgIndex.Size = BitConverter.ToInt32(data, 4);
                    sgIndex.DataId = GetFileNumber(sgIndex.Hash);
                    sgIndex.Offset = BitConverter.ToInt32(data, 0);
                    SgData.Add(sgIndex);

                }
                fs.Close();
                fs.Dispose();
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message, "Error", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
        }

        public static void BuildPackage(List<string> path, string savePath, System.Windows.Forms.ProgressBar pb){
            string fileName = savePath + "\\";
            SgPack.Clear();
            //==========================================
            //              Delete old files
            //==========================================

            string savepath = savePath + "\\";

            for (int i = 100; i < 200; i++)
            {
                if (File.Exists(savepath + "res." + i.ToString()))
                {
                    File.Delete(savepath + "res." + i.ToString());
                }
            }
            for (int i = 10; i < 100; i++)
            {
                if (File.Exists(savepath + "res.0" + i.ToString()))
                {
                    File.Delete(savepath + "res.0" + i.ToString());
                }
            }
            for (int i = 0; i < 10; i++)
            {
                if (File.Exists(savepath + "res.00" + i.ToString()))
                {
                    File.Delete(savepath + "res.00" + i.ToString());
                }
            }
            //==========================================
            //              Pack new files 
            //==========================================
            pb.Value = 0;
            pb.Maximum = path.Count();

            foreach (string pt in path)
            {
                SgdataIndex inx = new SgdataIndex();
                FileInfo fi = new FileInfo(pt);
                inx.DataId = GetFileNumber(Fhash.EncodeFileName(Path.GetFileName(pt)));
                inx.Hash = Fhash.EncodeFileName(Path.GetFileName(pt));
                inx.Name = Path.GetFileName(pt);
                inx.Size = (int)fi.Length;

                //Datei schreiben
                FileStream fstr = new FileStream(fileName + "res." + inx.DataId, FileMode.Append);
                BinaryWriter bwrite = new BinaryWriter(fstr);
                inx.Offset = (int)fstr.Position;

                byte[] bytes = File.ReadAllBytes(pt);

                string extension = Path.GetExtension(pt);
                if (extension != null && Encrypted(extension.Replace(".","")))
                {
                    // encrypt
                    byte xorss = 0;
                    for (int i = 0; i < bytes.Length; i++)
                    {
                        bytes[i] ^= Xor[xorss];
                        xorss++;
                    }
                    bwrite.Write(bytes);
                }
                else
                {
                    //save without encryption
                    bwrite.Write(bytes);
                    // bwrite.Write
                }

                SgPack.Add(inx);
                bwrite.Close();
                fstr.Close();
                pb.Value += 1;
                System.Windows.Forms.Application.DoEvents();
            }


            //==========================================
            //              build res.000 (index)
            //==========================================
            FileStream fs = new FileStream(fileName + "res.000", FileMode.CreateNew);
            BinaryWriter w = new BinaryWriter(fs);
            byte xors = 0;
            foreach (SgdataIndex pt in SgPack)
            {
                byte[] hashedfilename = Sfm.StringToBytes(pt.Hash);
                int hashlenght = hashedfilename.Length;
                int fileSize = (int)pt.Size;
                int offset = pt.Offset;
                byte[] data = new byte[8];
                byte[] off = BitConverter.GetBytes(offset);
                byte[] fz = BitConverter.GetBytes(fileSize);
                data[0] = off[0];
                data[1] = off[1];
                data[2] = off[2];
                data[3] = off[3]; 
                data[4] = fz[0];
                data[5] = fz[1];
                data[6] = fz[2];
                data[7] = fz[3];
                hashlenght ^= Xor[xors];
                xors++;

                for (int i = 0; i < hashedfilename.Length; i++)
                {
                    hashedfilename[i] ^= Xor[xors];
                    xors++;
                }

                for (int i = 0; i < 8; i++)
                {
                    data[i] ^= Xor[xors];
                    xors++;
                }
                w.Write((byte)hashlenght);
                w.Write(hashedfilename);
                w.Write(data);
                //Hashlänge(1 byte) -- hash(Hashlänge) -- data(8 Byte) {<------Offset----->}
                //                                                     {..........Size---->}
            }
          
            w.Close();
            fs.Close();
        }
        public static void DumpFile(string datapath, string dumppath, string name, int offset, string id, int size, bool createsubdir = true)
        {
            FileStream fs = new FileStream(datapath + "\\" + "res." + id, FileMode.Open, FileAccess.Read);
            fs.Seek(offset, SeekOrigin.Begin);

            string extension = Path.GetExtension(name);
            if (extension != null && !Encrypted(extension.Replace(".", "")))
            {
                BinaryReader br = new BinaryReader(fs);
                if (createsubdir)
                {
                    if (Directory.Exists(dumppath) == false)
                    {
                        Directory.CreateDirectory(dumppath);
                    }
                    File.WriteAllBytes(dumppath + "\\" + name, br.ReadBytes(size));
                }
                else
                {
                    File.WriteAllBytes(dumppath, br.ReadBytes(size));
                }
                fs.Close();
                fs.Dispose();
                br.Close();
                br.Dispose();
            }
            else
            {
                BinaryReader br = new BinaryReader(fs);
                byte[] data = br.ReadBytes(size);
                byte xors = 0;
                for (int i = 0; i < size; i++)
                {
                    data[i] ^= Xor[xors];
                    xors++;
                }
                if (createsubdir)
                {
                    if (Directory.Exists(dumppath) == false)
                    {
                        Directory.CreateDirectory(dumppath); 
                    }
                    File.WriteAllBytes(dumppath + "\\" + name, data);
                }
                else
                {
                    File.WriteAllBytes(dumppath , data);
                }
                fs.Close();
                fs.Dispose();
                br.Close();
                br.Dispose();

            }
        }


        private static bool Encrypted(string ext)
        {
            if (ext == "bmp") return true;
            if (ext == "item") return true;
            if (ext == "jpg") return true;
            if (ext == "jtv") return true;
            if (ext == "lua") return true;
            if (ext == "map") return true;
            if (ext == "naf") return true;
            if (ext == "npc") return true;
            if (ext == "nui") return true;
            if (ext == "nus") return true;
            if (ext == "nx3") return true;
            if (ext == "rsg") return true;
            if (ext == "spr") return true;
            if (ext == "trick") return true;
            if (ext == "ttf") return true;
            if (ext == "txt") return true;
            if (ext == "wav") return true;

            if (ext == "tga") return false;
            if (ext == "dds") return false;
            if (ext == "ffe") return false;
            if (ext == "fx") return false;
            return true;
        }


        private static string GetFileNumber(string pHash)
        {
            byte[] buff = System.Text.Encoding.ASCII.GetBytes(pHash.ToLower() + "0");
            int v3 = 0;
            int v30 = 0;
            int v31 = 0;
            int v29 = 0;
            string ret = "";
            if (buff[0] != 0)
            {
                while (buff.Length - 1 != v30)
                {
                    v3 = buff[v30];
                    v31 = v29 << 5;
                    v31 -= v29;

                    v31 += v3;
                    v30++;
                    v3 = buff[v30];
                    v29 = v31;
                }

                if (v31 < 0)
                    v29 = -v31;
            }
            int v32 = v29 % 200;
            v32 = v32 + 1;
            ret = v32.ToString();
            if (ret.Length == 1){
                return "00" + ret;
            }
            if (ret.Length == 2)
            {
                return "0" + ret;
            }
            return ret;
        }
    }
}