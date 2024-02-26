namespace CsHms.Akshay
{
    partial class DDCList
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.dgvDdcList = new System.Windows.Forms.DataGridView();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.btnFile = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.cbxType = new System.Windows.Forms.ComboBox();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDdcList)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvDdcList
            // 
            this.dgvDdcList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDdcList.Location = new System.Drawing.Point(-1, 63);
            this.dgvDdcList.Name = "dgvDdcList";
            this.dgvDdcList.RowTemplate.Height = 28;
            this.dgvDdcList.Size = new System.Drawing.Size(748, 383);
            this.dgvDdcList.TabIndex = 0;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // btnFile
            // 
            this.btnFile.Location = new System.Drawing.Point(562, 12);
            this.btnFile.Name = "btnFile";
            this.btnFile.Size = new System.Drawing.Size(172, 45);
            this.btnFile.TabIndex = 1;
            this.btnFile.Text = "Open File";
            this.btnFile.UseVisualStyleBackColor = true;
            this.btnFile.Click += new System.EventHandler(this.btnFile_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(448, 455);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(132, 34);
            this.btnSave.TabIndex = 2;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(602, 455);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(132, 34);
            this.btnExit.TabIndex = 3;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // cbxType
            // 
            this.cbxType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxType.DropDownWidth = 300;
            this.cbxType.FormattingEnabled = true;
            this.cbxType.Location = new System.Drawing.Point(12, 21);
            this.cbxType.Name = "cbxType";
            this.cbxType.Size = new System.Drawing.Size(172, 28);
            this.cbxType.TabIndex = 4;
            
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(204, 21);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(337, 28);
            this.progressBar.TabIndex = 5;
            // 
            // DDCList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(746, 498);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.cbxType);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnFile);
            this.Controls.Add(this.dgvDdcList);
            this.Name = "DDCList";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "DDCList";
            ((System.ComponentModel.ISupportInitialize)(this.dgvDdcList)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvDdcList;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button btnFile;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.ComboBox cbxType;
        private System.Windows.Forms.ProgressBar progressBar;

    }
}