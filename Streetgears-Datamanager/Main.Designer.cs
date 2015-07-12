namespace SGunpacker
{
    partial class Main
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.dgv3 = new System.Windows.Forms.DataGridView();
            this.sgfileBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.list_btn = new System.Windows.Forms.Button();
            this.progressBar3 = new System.Windows.Forms.ProgressBar();
            this.file_count = new System.Windows.Forms.Label();
            this.dump_a = new System.Windows.Forms.Button();
            this.FBD = new System.Windows.Forms.FolderBrowserDialog();
            this.buttonDumpSingleFile = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.buttonCreateArchive = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgv3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sgfileBindingSource)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgv3
            // 
            this.dgv3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgv3.AutoGenerateColumns = false;
            this.dgv3.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCellsExceptHeaders;
            this.dgv3.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv3.DataSource = this.sgfileBindingSource;
            this.dgv3.EnableHeadersVisualStyles = false;
            this.dgv3.Location = new System.Drawing.Point(12, 12);
            this.dgv3.MultiSelect = false;
            this.dgv3.Name = "dgv3";
            this.dgv3.Size = new System.Drawing.Size(1332, 443);
            this.dgv3.TabIndex = 7;
            this.dgv3.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv3_CellClick);
            // 
            // sgfileBindingSource
            // 
            this.sgfileBindingSource.DataSource = typeof(SGunpacker.Sgfile);
            // 
            // list_btn
            // 
            this.list_btn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.list_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.list_btn.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.list_btn.Location = new System.Drawing.Point(126, 19);
            this.list_btn.Name = "list_btn";
            this.list_btn.Size = new System.Drawing.Size(99, 24);
            this.list_btn.TabIndex = 18;
            this.list_btn.Text = "Load";
            this.list_btn.UseVisualStyleBackColor = true;
            this.list_btn.Click += new System.EventHandler(this.list_btn_Click);
            // 
            // progressBar3
            // 
            this.progressBar3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar3.Location = new System.Drawing.Point(15, 465);
            this.progressBar3.Name = "progressBar3";
            this.progressBar3.Size = new System.Drawing.Size(1332, 10);
            this.progressBar3.TabIndex = 20;
            // 
            // file_count
            // 
            this.file_count.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.file_count.AutoSize = true;
            this.file_count.Location = new System.Drawing.Point(12, 489);
            this.file_count.Name = "file_count";
            this.file_count.Size = new System.Drawing.Size(66, 13);
            this.file_count.TabIndex = 19;
            this.file_count.Text = "File Count: 0";
            // 
            // dump_a
            // 
            this.dump_a.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.dump_a.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.dump_a.Location = new System.Drawing.Point(156, 19);
            this.dump_a.Name = "dump_a";
            this.dump_a.Size = new System.Drawing.Size(140, 23);
            this.dump_a.TabIndex = 21;
            this.dump_a.Text = "Dump by Filetype";
            this.dump_a.UseVisualStyleBackColor = true;
            this.dump_a.Click += new System.EventHandler(this.dump_a_Click);
            // 
            // buttonDumpSingleFile
            // 
            this.buttonDumpSingleFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonDumpSingleFile.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonDumpSingleFile.Location = new System.Drawing.Point(10, 19);
            this.buttonDumpSingleFile.Name = "buttonDumpSingleFile";
            this.buttonDumpSingleFile.Size = new System.Drawing.Size(140, 23);
            this.buttonDumpSingleFile.TabIndex = 22;
            this.buttonDumpSingleFile.Text = "Dump Selected";
            this.buttonDumpSingleFile.UseVisualStyleBackColor = true;
            this.buttonDumpSingleFile.Click += new System.EventHandler(this.buttonDumpSingleFile_Click);
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Location = new System.Drawing.Point(302, 19);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(140, 23);
            this.button1.TabIndex = 24;
            this.button1.Text = "Dump by Res";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // buttonCreateArchive
            // 
            this.buttonCreateArchive.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCreateArchive.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonCreateArchive.Location = new System.Drawing.Point(12, 19);
            this.buttonCreateArchive.Name = "buttonCreateArchive";
            this.buttonCreateArchive.Size = new System.Drawing.Size(108, 24);
            this.buttonCreateArchive.TabIndex = 25;
            this.buttonCreateArchive.Text = "Create Resfiles";
            this.buttonCreateArchive.UseVisualStyleBackColor = true;
            this.buttonCreateArchive.Click += new System.EventHandler(this.buttonCreateArchive_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dump_a);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.buttonDumpSingleFile);
            this.groupBox1.Location = new System.Drawing.Point(659, 481);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(448, 51);
            this.groupBox1.TabIndex = 26;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Unpack";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.buttonCreateArchive);
            this.groupBox2.Controls.Add(this.list_btn);
            this.groupBox2.Location = new System.Drawing.Point(1113, 481);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(231, 51);
            this.groupBox2.TabIndex = 27;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Pack";
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1356, 537);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.progressBar3);
            this.Controls.Add(this.file_count);
            this.Controls.Add(this.dgv3);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Main";
            this.Text = "Streetgears Filemanager";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgv3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sgfileBindingSource)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgv3;
        private System.Windows.Forms.Button list_btn;
        private System.Windows.Forms.ProgressBar progressBar3;
        private System.Windows.Forms.Label file_count;
        private System.Windows.Forms.Button dump_a;
        private System.Windows.Forms.FolderBrowserDialog FBD;
        private System.Windows.Forms.BindingSource sgfileBindingSource;
        private System.Windows.Forms.Button buttonDumpSingleFile;
        private System.Windows.Forms.DataGridViewTextBoxColumn nameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn hashDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn sizeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataidDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn offsetDataGridViewTextBoxColumn;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button buttonCreateArchive;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
    }
}

