namespace CsHms
{
    partial class FileMaster
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtFileCode = new System.Windows.Forms.TextBox();
            this.grpbButton = new System.Windows.Forms.GroupBox();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.panDetail = new System.Windows.Forms.Panel();
            this.chkActive = new System.Windows.Forms.CheckBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtAuthPerson = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtRack = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.scsCategory = new UserControls.SimpleComboSearch();
            this.label5 = new System.Windows.Forms.Label();
            this.scsType = new UserControls.SimpleComboSearch();
            this.label4 = new System.Windows.Forms.Label();
            this.scsLocation = new UserControls.SimpleComboSearch();
            this.label2 = new System.Windows.Forms.Label();
            this.txtDesc = new System.Windows.Forms.TextBox();
            this.chkTrnOut = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.chkTransfer = new System.Windows.Forms.CheckBox();
            this.txtRemarks = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.grpbButton.SuspendLayout();
            this.panDetail.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtFileCode);
            this.groupBox1.Controls.Add(this.grpbButton);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.panDetail);
            this.groupBox1.Location = new System.Drawing.Point(3, -3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(433, 276);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // txtFileCode
            // 
            this.txtFileCode.Location = new System.Drawing.Point(121, 13);
            this.txtFileCode.MaxLength = 20;
            this.txtFileCode.Name = "txtFileCode";
            this.txtFileCode.Size = new System.Drawing.Size(120, 20);
            this.txtFileCode.TabIndex = 0;
            this.txtFileCode.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtFileCode_KeyPress);
            this.txtFileCode.Validating += new System.ComponentModel.CancelEventHandler(this.txtFileCode_Validating);
            // 
            // grpbButton
            // 
            this.grpbButton.Controls.Add(this.btnExit);
            this.grpbButton.Controls.Add(this.btnDelete);
            this.grpbButton.Controls.Add(this.btnCancel);
            this.grpbButton.Controls.Add(this.btnSave);
            this.grpbButton.Location = new System.Drawing.Point(27, 220);
            this.grpbButton.Name = "grpbButton";
            this.grpbButton.Size = new System.Drawing.Size(378, 46);
            this.grpbButton.TabIndex = 2;
            this.grpbButton.TabStop = false;
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(286, 11);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(85, 29);
            this.btnExit.TabIndex = 3;
            this.btnExit.Text = "E&xit";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Enabled = false;
            this.btnDelete.Location = new System.Drawing.Point(193, 11);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(85, 29);
            this.btnDelete.TabIndex = 2;
            this.btnDelete.Text = "&Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(100, 11);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(85, 29);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.Enabled = false;
            this.btnSave.Location = new System.Drawing.Point(7, 11);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(85, 29);
            this.btnSave.TabIndex = 0;
            this.btnSave.Text = "&Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(32, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Code";
            // 
            // panDetail
            // 
            this.panDetail.Controls.Add(this.chkActive);
            this.panDetail.Controls.Add(this.label8);
            this.panDetail.Controls.Add(this.txtAuthPerson);
            this.panDetail.Controls.Add(this.label7);
            this.panDetail.Controls.Add(this.txtRack);
            this.panDetail.Controls.Add(this.label6);
            this.panDetail.Controls.Add(this.scsCategory);
            this.panDetail.Controls.Add(this.label5);
            this.panDetail.Controls.Add(this.scsType);
            this.panDetail.Controls.Add(this.label4);
            this.panDetail.Controls.Add(this.scsLocation);
            this.panDetail.Controls.Add(this.label2);
            this.panDetail.Controls.Add(this.txtDesc);
            this.panDetail.Controls.Add(this.chkTrnOut);
            this.panDetail.Controls.Add(this.label3);
            this.panDetail.Controls.Add(this.chkTransfer);
            this.panDetail.Controls.Add(this.txtRemarks);
            this.panDetail.Location = new System.Drawing.Point(6, 32);
            this.panDetail.Name = "panDetail";
            this.panDetail.Size = new System.Drawing.Size(422, 182);
            this.panDetail.TabIndex = 1;
            // 
            // chkActive
            // 
            this.chkActive.AutoSize = true;
            this.chkActive.Location = new System.Drawing.Point(364, 117);
            this.chkActive.Name = "chkActive";
            this.chkActive.Size = new System.Drawing.Size(56, 17);
            this.chkActive.TabIndex = 8;
            this.chkActive.Text = "Active";
            this.chkActive.UseVisualStyleBackColor = true;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 97);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(93, 13);
            this.label8.TabIndex = 14;
            this.label8.Text = "Authorised Person";
            // 
            // txtAuthPerson
            // 
            this.txtAuthPerson.Location = new System.Drawing.Point(115, 93);
            this.txtAuthPerson.MaxLength = 100;
            this.txtAuthPerson.Name = "txtAuthPerson";
            this.txtAuthPerson.Size = new System.Drawing.Size(300, 20);
            this.txtAuthPerson.TabIndex = 5;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 75);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(33, 13);
            this.label7.TabIndex = 12;
            this.label7.Text = "Rack";
            // 
            // txtRack
            // 
            this.txtRack.Location = new System.Drawing.Point(115, 71);
            this.txtRack.MaxLength = 100;
            this.txtRack.Name = "txtRack";
            this.txtRack.Size = new System.Drawing.Size(300, 20);
            this.txtRack.TabIndex = 4;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(243, 52);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(49, 13);
            this.label6.TabIndex = 10;
            this.label6.Text = "Category";
            // 
            // scsCategory
            // 
            this.scsCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
            this.scsCategory.Location = new System.Drawing.Point(295, 48);
            this.scsCategory.Name = "scsCategory";
            this.scsCategory.Size = new System.Drawing.Size(120, 21);
            this.scsCategory.TabIndex = 3;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 52);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(31, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "Type";
            // 
            // scsType
            // 
            this.scsType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
            this.scsType.Location = new System.Drawing.Point(115, 48);
            this.scsType.Name = "scsType";
            this.scsType.Size = new System.Drawing.Size(120, 21);
            this.scsType.TabIndex = 2;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 29);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(108, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Location/Department";
            // 
            // scsLocation
            // 
            this.scsLocation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
            this.scsLocation.Location = new System.Drawing.Point(115, 25);
            this.scsLocation.Name = "scsLocation";
            this.scsLocation.Size = new System.Drawing.Size(300, 21);
            this.scsLocation.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 7);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Description";
            // 
            // txtDesc
            // 
            this.txtDesc.Location = new System.Drawing.Point(115, 3);
            this.txtDesc.MaxLength = 100;
            this.txtDesc.Name = "txtDesc";
            this.txtDesc.Size = new System.Drawing.Size(300, 20);
            this.txtDesc.TabIndex = 0;
            // 
            // chkTrnOut
            // 
            this.chkTrnOut.AutoSize = true;
            this.chkTrnOut.Location = new System.Drawing.Point(255, 117);
            this.chkTrnOut.Name = "chkTrnOut";
            this.chkTrnOut.Size = new System.Drawing.Size(113, 17);
            this.chkTrnOut.TabIndex = 7;
            this.chkTrnOut.Text = "Forward to outside";
            this.chkTrnOut.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 149);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(49, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Remarks";
            // 
            // chkTransfer
            // 
            this.chkTransfer.AutoSize = true;
            this.chkTransfer.Location = new System.Drawing.Point(115, 117);
            this.chkTransfer.Name = "chkTransfer";
            this.chkTransfer.Size = new System.Drawing.Size(143, 17);
            this.chkTransfer.TabIndex = 6;
            this.chkTransfer.Text = "Forward to other location";
            this.chkTransfer.UseVisualStyleBackColor = true;
            // 
            // txtRemarks
            // 
            this.txtRemarks.Location = new System.Drawing.Point(115, 136);
            this.txtRemarks.MaxLength = 250;
            this.txtRemarks.Multiline = true;
            this.txtRemarks.Name = "txtRemarks";
            this.txtRemarks.Size = new System.Drawing.Size(300, 39);
            this.txtRemarks.TabIndex = 9;
            // 
            // FileMaster
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(439, 277);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "FileMaster";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "File Master";
            this.Load += new System.EventHandler(this.FileMaster_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.grpbButton.ResumeLayout(false);
            this.panDetail.ResumeLayout(false);
            this.panDetail.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtFileCode;
        private System.Windows.Forms.GroupBox grpbButton;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panDetail;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtDesc;
        private System.Windows.Forms.CheckBox chkTrnOut;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox chkTransfer;
        private System.Windows.Forms.TextBox txtRemarks;
        private UserControls.SimpleComboSearch scsLocation;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
        private UserControls.SimpleComboSearch scsCategory;
        private System.Windows.Forms.Label label5;
        private UserControls.SimpleComboSearch scsType;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtRack;
        private System.Windows.Forms.CheckBox chkActive;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtAuthPerson;
    }
}