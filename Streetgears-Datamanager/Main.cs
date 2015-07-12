using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using SGunpacker.Properties;

namespace SGunpacker
{
    public partial class Main : Form
    {
        readonly List<Sgfile> _sgf = new List<Sgfile>();
        string _dataDir;
        Sgfile _selected;
        public Main()
        {
            InitializeComponent();
        }

        private void list_btn_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                _dataDir = fbd.SelectedPath;
                try
                {
                    DataDump.Read_Type(fbd.SelectedPath, _sgf);
                    sgfileBindingSource.DataSource = _sgf;
                    sgfileBindingSource.ResetBindings(false);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(Resources.ErrorLoadingItems + ex.ToString());
                }
            }
        }

        private void dump_a_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                DataDump.Dump_All(_dataDir, fbd.SelectedPath + "\\", progressBar3);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void dgv3_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try {
                _selected = _sgf[e.RowIndex];
            }
            catch (Exception ex)
            {

            }

        }

        private void buttonDumpSingleFile_Click(object sender, EventArgs e)
        {
            Sgfile selfile = _selected;
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.FileName = _selected.Name;
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                StreetgearsDataCipher.DumpFile(_dataDir, sfd.FileName, selfile.Name, selfile.Offset, selfile.Dataid, selfile.Size, false);
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog sfd = new FolderBrowserDialog();
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                string basepath = sfd.SelectedPath;
                progressBar3.Maximum = _sgf.Count();
                progressBar3.Value = 0;
                foreach (Sgfile sg in _sgf)
                {
                    if (!System.IO.Directory.Exists(basepath + "\\" + sg.Dataid.ToString()))
                    {
                        System.IO.Directory.CreateDirectory(basepath + "\\" + sg.Dataid.ToString());
                    }
                    StreetgearsDataCipher.DumpFile(_dataDir, basepath + "\\" + sg.Dataid.ToString() + "\\" + sg.Name, sg.Name, sg.Offset, sg.Dataid, sg.Size, false);
                    progressBar3.Value += 1;
                    Application.DoEvents();
                }
            }

        }

        private void buttonCreateArchive_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog sfd = new FolderBrowserDialog {Description = Resources.PleaseSelectSavePath};
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                FolderBrowserDialog ofd = new FolderBrowserDialog();
                ofd.Description = Resources.SavepathNewFiles;
                if(ofd.ShowDialog() == DialogResult.OK){      
                    List<string> filelist = new List<string>(System.IO.Directory.GetFiles(ofd.SelectedPath,"*",System.IO.SearchOption.AllDirectories));
                    StreetgearsDataCipher.BuildPackage(filelist, sfd.SelectedPath, progressBar3);
                    MessageBox.Show(Resources.FileCreated);
                }
            }
        }
    }
}
