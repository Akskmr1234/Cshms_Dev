namespace CsHms.Akshay
{
    partial class FirmSelection
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
            this.lstbxFirms = new System.Windows.Forms.ListBox();
            this.lstbxFyear = new System.Windows.Forms.ListBox();
            this.btnSelect = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lstbxFirms
            // 
            this.lstbxFirms.FormattingEnabled = true;
            this.lstbxFirms.ItemHeight = 20;
            this.lstbxFirms.Location = new System.Drawing.Point(12, 24);
            this.lstbxFirms.Name = "lstbxFirms";
            this.lstbxFirms.Size = new System.Drawing.Size(779, 344);
            this.lstbxFirms.TabIndex = 0;
            this.lstbxFirms.SelectedValueChanged += new System.EventHandler(this.lstbxFirms_SelectedValueChanged);
            // 
            // lstbxFyear
            // 
            this.lstbxFyear.FormattingEnabled = true;
            this.lstbxFyear.ItemHeight = 20;
            this.lstbxFyear.Location = new System.Drawing.Point(797, 24);
            this.lstbxFyear.Name = "lstbxFyear";
            this.lstbxFyear.Size = new System.Drawing.Size(189, 244);
            this.lstbxFyear.TabIndex = 1;
            // 
            // btnSelect
            // 
            this.btnSelect.Location = new System.Drawing.Point(797, 287);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new System.Drawing.Size(189, 36);
            this.btnSelect.TabIndex = 2;
            this.btnSelect.Text = "Select";
            this.btnSelect.UseVisualStyleBackColor = true;
            this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click);
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(797, 332);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(189, 36);
            this.btnExit.TabIndex = 3;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // FirmSelection
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(995, 380);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnSelect);
            this.Controls.Add(this.lstbxFyear);
            this.Controls.Add(this.lstbxFirms);
            this.Name = "FirmSelection";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FirmSelection";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox lstbxFirms;
        private System.Windows.Forms.ListBox lstbxFyear;
        private System.Windows.Forms.Button btnSelect;
        private System.Windows.Forms.Button btnExit;
    }
}