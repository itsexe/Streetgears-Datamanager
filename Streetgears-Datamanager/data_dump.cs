using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using SGunpacker.Properties;

namespace SGunpacker
{
    static class DataDump
    {
        public static void Read_Type(string dataDir, List<Sgfile> sgf)
        {
            try
            {
                StreetgearsDataCipher.Open(dataDir + "\\res.000");                       
                for (int i = 0; i < StreetgearsDataCipher.SgData.Count; i++)
                {
                    Sgfile sg = new Sgfile(StreetgearsDataCipher.SgData[i].Name, StreetgearsDataCipher.SgData[i].Hash, StreetgearsDataCipher.SgData[i].Size, StreetgearsDataCipher.SgData[i].DataId, StreetgearsDataCipher.SgData[i].Offset);
                    sgf.Add(sg);
                }
            }
            catch (Exception e)
            {
               
                MessageBox.Show(Resources.resnotfound +e , Resources.error, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public static void Dump_All(string dataDir, string dumpDir, ProgressBar pBar)
        {
            pBar.Value = 0;
            pBar.Maximum = StreetgearsDataCipher.SgData.Count;
            foreach (SgdataIndex sgIndex in StreetgearsDataCipher.SgData)
            {
                string extension = Path.GetExtension(sgIndex.Name);
                if (extension != null)
                    StreetgearsDataCipher.DumpFile(dataDir, dumpDir + extension.Replace(".", ""), sgIndex.Name, sgIndex.Offset, sgIndex.DataId, sgIndex.Size);
                pBar.Value++;
            }
        }
    }
}
