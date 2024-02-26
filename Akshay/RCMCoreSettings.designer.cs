namespace CsHms.Akshay
{
    partial class RCMCoreSettings
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
            this.lblClinician = new System.Windows.Forms.Label();
            this.txtCode = new System.Windows.Forms.TextBox();
            this.txtDetails = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtRegno1 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtRegno2 = new System.Windows.Forms.TextBox();
            this.txtSno = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtRemarks = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.btnSubmit = new System.Windows.Forms.Button();
            this.chkActive = new System.Windows.Forms.CheckBox();
            this.dgvrcmcoresettings = new System.Windows.Forms.DataGridView();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.cbxType = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.pnlfooter = new System.Windows.Forms.Panel();
            this.label15 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvrcmcoresettings)).BeginInit();
            this.pnlfooter.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblClinician
            // 
            this.lblClinician.AutoSize = true;
            this.lblClinician.Font = new System.Drawing.Font("Franklin Gothic Book", 9F);
            this.lblClinician.Location = new System.Drawing.Point(362, 21);
            this.lblClinician.Name = "lblClinician";
            this.lblClinician.Size = new System.Drawing.Size(48, 23);
            this.lblClinician.TabIndex = 0;
            this.lblClinician.Text = "Code";
            // 
            // txtCode
            // 
            this.txtCode.Enabled = false;
            this.txtCode.Location = new System.Drawing.Point(419, 18);
            this.txtCode.MaxLength = 10;
            this.txtCode.Name = "txtCode";
            this.txtCode.Size = new System.Drawing.Size(196, 26);
            this.txtCode.TabIndex = 2;
            this.txtCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtClinician_KeyDown);
            this.txtCode.Validating += new System.ComponentModel.CancelEventHandler(this.txtClinician_Validating);
            // 
            // txtDetails
            // 
            this.txtDetails.Location = new System.Drawing.Point(116, 87);
            this.txtDetails.Multiline = true;
            this.txtDetails.Name = "txtDetails";
            this.txtDetails.ReadOnly = true;
            this.txtDetails.Size = new System.Drawing.Size(499, 55);
            this.txtDetails.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Franklin Gothic Book", 9F);
            this.label2.Location = new System.Drawing.Point(22, 87);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 23);
            this.label2.TabIndex = 5;
            this.label2.Text = "Details";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Franklin Gothic Book", 9F);
            this.label3.Location = new System.Drawing.Point(22, 151);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(69, 23);
            this.label3.TabIndex = 7;
            this.label3.Text = "Regno1";
            // 
            // txtRegno1
            // 
            this.txtRegno1.Location = new System.Drawing.Point(116, 151);
            this.txtRegno1.Name = "txtRegno1";
            this.txtRegno1.Size = new System.Drawing.Size(196, 26);
            this.txtRegno1.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Franklin Gothic Book", 9F);
            this.label4.Location = new System.Drawing.Point(341, 151);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(69, 23);
            this.label4.TabIndex = 9;
            this.label4.Text = "Regno2";
            // 
            // txtRegno2
            // 
            this.txtRegno2.Location = new System.Drawing.Point(419, 150);
            this.txtRegno2.Name = "txtRegno2";
            this.txtRegno2.Size = new System.Drawing.Size(196, 26);
            this.txtRegno2.TabIndex = 6;
            // 
            // txtSno
            // 
            this.txtSno.Location = new System.Drawing.Point(116, 221);
            this.txtSno.Name = "txtSno";
            this.txtSno.Size = new System.Drawing.Size(196, 26);
            this.txtSno.TabIndex = 9;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Franklin Gothic Book", 9F);
            this.label5.Location = new System.Drawing.Point(22, 223);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(77, 23);
            this.label5.TabIndex = 13;
            this.label5.Text = "Serial no";
            // 
            // txtUsername
            // 
            this.txtUsername.Location = new System.Drawing.Point(116, 185);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(196, 26);
            this.txtUsername.TabIndex = 7;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Franklin Gothic Book", 9F);
            this.label6.Location = new System.Drawing.Point(22, 184);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(88, 23);
            this.label6.TabIndex = 11;
            this.label6.Text = "Username";
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(419, 185);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(196, 26);
            this.txtPassword.TabIndex = 8;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Franklin Gothic Book", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(327, 185);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(83, 23);
            this.label7.TabIndex = 15;
            this.label7.Text = "Password";
            // 
            // txtRemarks
            // 
            this.txtRemarks.Location = new System.Drawing.Point(419, 220);
            this.txtRemarks.Name = "txtRemarks";
            this.txtRemarks.Size = new System.Drawing.Size(196, 26);
            this.txtRemarks.TabIndex = 10;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Franklin Gothic Book", 9F);
            this.label8.Location = new System.Drawing.Point(332, 222);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(78, 23);
            this.label8.TabIndex = 17;
            this.label8.Text = "Remarks";
            // 
            // btnSubmit
            // 
            this.btnSubmit.Location = new System.Drawing.Point(7, 7);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(115, 39);
            this.btnSubmit.TabIndex = 12;
            this.btnSubmit.Text = "Save";
            this.btnSubmit.UseVisualStyleBackColor = true;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // chkActive
            // 
            this.chkActive.AutoSize = true;
            this.chkActive.Location = new System.Drawing.Point(35, 275);
            this.chkActive.Name = "chkActive";
            this.chkActive.Size = new System.Drawing.Size(78, 24);
            this.chkActive.TabIndex = 11;
            this.chkActive.Text = "Active";
            this.chkActive.UseVisualStyleBackColor = true;
            // 
            // dgvrcmcoresettings
            // 
            this.dgvrcmcoresettings.AllowUserToAddRows = false;
            this.dgvrcmcoresettings.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvrcmcoresettings.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvrcmcoresettings.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgvrcmcoresettings.Location = new System.Drawing.Point(26, 314);
            this.dgvrcmcoresettings.Name = "dgvrcmcoresettings";
            this.dgvrcmcoresettings.RowHeadersVisible = false;
            this.dgvrcmcoresettings.RowTemplate.Height = 25;
            this.dgvrcmcoresettings.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvrcmcoresettings.Size = new System.Drawing.Size(589, 198);
            this.dgvrcmcoresettings.TabIndex = 21;
            this.dgvrcmcoresettings.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvrcmcoresettings_CellDoubleClick);
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(128, 7);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(115, 39);
            this.btnClear.TabIndex = 13;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(249, 7);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(115, 39);
            this.btnExit.TabIndex = 14;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // cbxType
            // 
            this.cbxType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxType.FormattingEnabled = true;
            this.cbxType.Items.AddRange(new object[] {
            "--select--",
            "CLINICIAN",
            "HOSPITAL"});
            this.cbxType.Location = new System.Drawing.Point(116, 18);
            this.cbxType.Name = "cbxType";
            this.cbxType.Size = new System.Drawing.Size(196, 28);
            this.cbxType.TabIndex = 1;
            this.cbxType.SelectedValueChanged += new System.EventHandler(this.cbxType_SelectedValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Franklin Gothic Book", 9F);
            this.label1.Location = new System.Drawing.Point(22, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 23);
            this.label1.TabIndex = 24;
            this.label1.Text = "Type";
            // 
            // pnlfooter
            // 
            this.pnlfooter.Controls.Add(this.label15);
            this.pnlfooter.Controls.Add(this.btnExit);
            this.pnlfooter.Controls.Add(this.btnClear);
            this.pnlfooter.Controls.Add(this.btnSubmit);
            this.pnlfooter.Location = new System.Drawing.Point(245, 253);
            this.pnlfooter.Margin = new System.Windows.Forms.Padding(3, 3, 30, 3);
            this.pnlfooter.Name = "pnlfooter";
            this.pnlfooter.Size = new System.Drawing.Size(370, 55);
            this.pnlfooter.TabIndex = 52;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(18, 153);
            this.label15.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(73, 20);
            this.label15.TabIndex = 55;
            this.label15.Text = "Remarks";
            this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Franklin Gothic Book", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.MediumBlue;
            this.label9.Location = new System.Drawing.Point(31, 512);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(209, 21);
            this.label9.TabIndex = 53;
            this.label9.Text = "*Double click columns to edit";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Franklin Gothic Book", 9F);
            this.label10.Location = new System.Drawing.Point(22, 53);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(56, 23);
            this.label10.TabIndex = 54;
            this.label10.Text = "Name";
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(116, 55);
            this.txtName.Name = "txtName";
            this.txtName.ReadOnly = true;
            this.txtName.Size = new System.Drawing.Size(499, 26);
            this.txtName.TabIndex = 3;
            // 
            // RCMCoreSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(640, 542);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.pnlfooter);
            this.Controls.Add(this.cbxType);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dgvrcmcoresettings);
            this.Controls.Add(this.chkActive);
            this.Controls.Add(this.txtRemarks);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtSno);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtUsername);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtRegno2);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtRegno1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtDetails);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtCode);
            this.Controls.Add(this.lblClinician);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "RCMCoreSettings";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "RCMCoreSettings";
            ((System.ComponentModel.ISupportInitialize)(this.dgvrcmcoresettings)).EndInit();
            this.pnlfooter.ResumeLayout(false);
            this.pnlfooter.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblClinician;
        private System.Windows.Forms.TextBox txtCode;
        private System.Windows.Forms.TextBox txtDetails;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtRegno1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtRegno2;
        private System.Windows.Forms.TextBox txtSno;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtRemarks;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button btnSubmit;
        private System.Windows.Forms.CheckBox chkActive;
        private System.Windows.Forms.DataGridView dgvrcmcoresettings;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.ComboBox cbxType;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel pnlfooter;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtName;
    }
}