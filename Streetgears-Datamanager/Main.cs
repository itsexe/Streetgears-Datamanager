using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using SGunpacker.Properties;

namespace SGunpacker
{
    public partial class Main : Form
    {
        private readonly BackgroundWorker _backgroundWorkerDumpFiles = new BackgroundWorker();
        private readonly BackgroundWorker _backgroundWorkerLoadFiles = new BackgroundWorker();


        readonly List<Sgfile> _sgf = new List<Sgfile>();
        string _dataDir;

        public enum Action
        {
            DumpByFile,
            DumpByExtention,
            DumpWithoutSubfolders,
        }

        public Main()
        {
            InitializeComponent();
            _backgroundWorkerDumpFiles.WorkerReportsProgress = true;
            _backgroundWorkerDumpFiles.DoWork += bw_dumpfiles;
            _backgroundWorkerDumpFiles.ProgressChanged += bw_ProgressChanged;
            _backgroundWorkerLoadFiles.DoWork += bw_LoadFiles;
        }
        /// <summary>
        /// Dumps files (Backgroundworker)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bw_dumpfiles(object sender, DoWorkEventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            Action act = (Action) e.Argument;
            Int32 count = 0;
            DialogResult result = DialogResult.OK;
            Invoke((MethodInvoker)delegate
            {
                result = fbd.ShowDialog();
            });
            if (result != DialogResult.OK) return;

            switch (act)
            {
                case Action.DumpByExtention:
                    foreach (SgdataIndex sgIndex in StreetgearsDataCipher.SgData)
                    {
                        var extension = System.IO.Path.GetExtension(sgIndex.Name);
                        if (extension != null)
                            StreetgearsDataCipher.DumpFile(_dataDir, fbd.SelectedPath + "\\" + extension.Replace(".", ""), sgIndex.Name, sgIndex.Offset, sgIndex.DataId, sgIndex.Size);
                        count++;
                        _backgroundWorkerDumpFiles.ReportProgress((count / _sgf.Count) * 100);
                    }
                    break;
                case Action.DumpByFile:
                    foreach (Sgfile sg in _sgf)
                    {
                        StreetgearsDataCipher.DumpFile(_dataDir, fbd.SelectedPath + "\\" + sg.Dataid.ToString() + "\\" + sg.Name, sg.Name, sg.Offset, sg.Dataid, sg.Size, false);
                        count++;
                        _backgroundWorkerDumpFiles.ReportProgress((count / _sgf.Count) * 100);
                    }
                    break;

                case Action.DumpWithoutSubfolders:
                    foreach (Sgfile sg in _sgf)
                    {
                        StreetgearsDataCipher.DumpFile(_dataDir, fbd.SelectedPath + "\\" + sg.Name, sg.Name, sg.Offset, sg.Dataid, sg.Size, false);
                        count++;
                        _backgroundWorkerDumpFiles.ReportProgress((count / _sgf.Count) * 100);
                    }
                    break;
            }
        }

        private void bw_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            Invoke((MethodInvoker)delegate
            {
                progressBarMain.Maximum = 100;
                progressBarMain.Value = e.ProgressPercentage;
            });
        }
        /// <summary>
        /// Loads res files (backgroundworker)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bw_LoadFiles(object sender, DoWorkEventArgs e)
        {
            Invoke((MethodInvoker) delegate
            {
                btn_Open.Enabled = false;
                progressBarMain.Style = ProgressBarStyle.Marquee;
            });
            DataDump.Read_Type(_dataDir, _sgf);
            Invoke((MethodInvoker)delegate
            {
                sgfileBindingSource.DataSource = _sgf;
                sgfileBindingSource.ResetBindings(false);
                progressBarMain.Style = ProgressBarStyle.Continuous;
                btn_Open.Enabled = true;
                file_count.Text = Resources.Files + _sgf.Count.ToString();
            });

        }
        /// <summary>
        /// Loads res files
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void list_btn_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (_backgroundWorkerLoadFiles.IsBusy) return;
            if (fbd.ShowDialog() != DialogResult.OK) return;
            _dataDir = fbd.SelectedPath;
            _backgroundWorkerLoadFiles.RunWorkerAsync();
        }
        /// <summary>
        /// Dumps by Filetype (.txt, .dds...)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dumpByFileType_Click(object sender, EventArgs e)
        {
            if (_backgroundWorkerDumpFiles.IsBusy == false)
            {
                _backgroundWorkerDumpFiles.RunWorkerAsync(Action.DumpByExtention);
            }
        }
        /// <summary>
        /// Dumps by Resindex (000-200)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonDumpByRes_Click(object sender, EventArgs e)
        {
            if (_backgroundWorkerDumpFiles.IsBusy == false)
            {
                _backgroundWorkerDumpFiles.RunWorkerAsync(Action.DumpByFile);
            }
        }
        /// <summary>
        /// Dumps everything without sorting the files
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dumpWithoutSubfolders_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(Resources.dumpWithoutSubfoldersWarningtext, Resources.DumpWithoutSubfolders_Warning, MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                if (_backgroundWorkerDumpFiles.IsBusy == false)
                {
                    _backgroundWorkerDumpFiles.RunWorkerAsync(Action.DumpWithoutSubfolders);
                }
            }
        }
        /// <summary>
        /// Creates new Resfiles
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonCreateArchive_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog sfd = new FolderBrowserDialog {Description = Resources.SavepathNewFiles };
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                FolderBrowserDialog ofd = new FolderBrowserDialog();
                ofd.Description = Resources.PleaseSelectSavePath; 
                if (ofd.ShowDialog() == DialogResult.OK){      
                    List<string> filelist = new List<string>(System.IO.Directory.GetFiles(ofd.SelectedPath,"*.*",System.IO.SearchOption.AllDirectories));
                    StreetgearsDataCipher.BuildPackage(filelist, sfd.SelectedPath, progressBarMain);
                    MessageBox.Show(Resources.FileCreated);
                }
            }
        }


    }
}
