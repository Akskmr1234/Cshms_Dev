namespace CsHms.Akshay
{
    partial class ModalityTechnicianEntry
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
            this.txtAccessionno = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.dgvData = new System.Windows.Forms.DataGridView();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.pnlBillDetails = new System.Windows.Forms.Panel();
            this.txtGender = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtAge = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtOpNo = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).BeginInit();
            this.pnlBillDetails.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtAccessionno
            // 
            this.txtAccessionno.Location = new System.Drawing.Point(151, 26);
            this.txtAccessionno.Name = "txtAccessionno";
            this.txtAccessionno.Size = new System.Drawing.Size(158, 26);
            this.txtAccessionno.TabIndex = 5;
            this.txtAccessionno.Validating += new System.ComponentModel.CancelEventHandler(this.txtAccessionno_Validating);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(35, 29);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(110, 20);
            this.label3.TabIndex = 4;
            this.label3.Text = "Accession No.";
            // 
            // dgvData
            // 
            this.dgvData.AllowUserToAddRows = false;
            this.dgvData.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvData.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvData.Location = new System.Drawing.Point(1, 166);
            this.dgvData.Name = "dgvData";
            this.dgvData.RowHeadersVisible = false;
            this.dgvData.RowTemplate.Height = 28;
            this.dgvData.Size = new System.Drawing.Size(666, 278);
            this.dgvData.TabIndex = 10;
            // 
            // btnExit
            // 
            this.btnExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExit.Location = new System.Drawing.Point(545, 467);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(86, 32);
            this.btnExit.TabIndex = 11;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnClear
            // 
            this.btnClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClear.Location = new System.Drawing.Point(425, 467);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(86, 32);
            this.btnClear.TabIndex = 12;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Location = new System.Drawing.Point(300, 467);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(86, 32);
            this.btnSave.TabIndex = 13;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            // 
            // pnlBillDetails
            // 
            this.pnlBillDetails.Controls.Add(this.txtGender);
            this.pnlBillDetails.Controls.Add(this.label4);
            this.pnlBillDetails.Controls.Add(this.txtAge);
            this.pnlBillDetails.Controls.Add(this.label5);
            this.pnlBillDetails.Controls.Add(this.txtOpNo);
            this.pnlBillDetails.Controls.Add(this.label2);
            this.pnlBillDetails.Controls.Add(this.txtName);
            this.pnlBillDetails.Controls.Add(this.label1);
            this.pnlBillDetails.Enabled = false;
            this.pnlBillDetails.Location = new System.Drawing.Point(12, 62);
            this.pnlBillDetails.Name = "pnlBillDetails";
            this.pnlBillDetails.Size = new System.Drawing.Size(644, 98);
            this.pnlBillDetails.TabIndex = 14;
            // 
            // txtGender
            // 
            this.txtGender.BackColor = System.Drawing.Color.White;
            this.txtGender.Location = new System.Drawing.Point(414, 58);
            this.txtGender.Name = "txtGender";
            this.txtGender.ReadOnly = true;
            this.txtGender.Size = new System.Drawing.Size(207, 26);
            this.txtGender.TabIndex = 17;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(346, 61);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(63, 20);
            this.label4.TabIndex = 16;
            this.label4.Text = "Gender";
            // 
            // txtAge
            // 
            this.txtAge.BackColor = System.Drawing.Color.White;
            this.txtAge.Location = new System.Drawing.Point(91, 58);
            this.txtAge.Name = "txtAge";
            this.txtAge.ReadOnly = true;
            this.txtAge.Size = new System.Drawing.Size(207, 26);
            this.txtAge.TabIndex = 15;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(24, 58);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(38, 20);
            this.label5.TabIndex = 14;
            this.label5.Text = "Age";
            // 
            // txtOpNo
            // 
            this.txtOpNo.BackColor = System.Drawing.Color.White;
            this.txtOpNo.Location = new System.Drawing.Point(414, 15);
            this.txtOpNo.Name = "txtOpNo";
            this.txtOpNo.ReadOnly = true;
            this.txtOpNo.Size = new System.Drawing.Size(207, 26);
            this.txtOpNo.TabIndex = 13;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(346, 18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(54, 20);
            this.label2.TabIndex = 12;
            this.label2.Text = "OpNo.";
            // 
            // txtName
            // 
            this.txtName.BackColor = System.Drawing.Color.White;
            this.txtName.ForeColor = System.Drawing.Color.Black;
            this.txtName.Location = new System.Drawing.Point(91, 15);
            this.txtName.Name = "txtName";
            this.txtName.ReadOnly = true;
            this.txtName.Size = new System.Drawing.Size(207, 26);
            this.txtName.TabIndex = 11;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(24, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 20);
            this.label1.TabIndex = 10;
            this.label1.Text = "Name";
            // 
            // ModalityTechnicianEntry
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(668, 518);
            this.Controls.Add(this.pnlBillDetails);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.dgvData);
            this.Controls.Add(this.txtAccessionno);
            this.Controls.Add(this.label3);
            this.Name = "ModalityTechnicianEntry";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ModalityTechnicianEntry";            
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).EndInit();
            this.pnlBillDetails.ResumeLayout(false);
            this.pnlBillDetails.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtAccessionno;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridView dgvData;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Panel pnlBillDetails;
        private System.Windows.Forms.TextBox txtGender;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtAge;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtOpNo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label label1;
    }
}