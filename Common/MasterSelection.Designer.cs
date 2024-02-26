namespace CsHms.Ctrl 
{
    partial class MasterSelection
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.gbMain = new System.Windows.Forms.GroupBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.lbtnDeleteAll = new System.Windows.Forms.LinkLabel();
            this.lstData = new System.Windows.Forms.ListBox();
            this.lbtnSelectAll = new System.Windows.Forms.LinkLabel();
            this.scsMaster = new UserControls.SimpleComboSearch();
            this.gbMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbMain
            // 
            this.gbMain.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.gbMain.Controls.Add(this.btnAdd);
            this.gbMain.Controls.Add(this.lbtnDeleteAll);
            this.gbMain.Controls.Add(this.lstData);
            this.gbMain.Controls.Add(this.lbtnSelectAll);
            this.gbMain.Controls.Add(this.scsMaster);
            this.gbMain.Location = new System.Drawing.Point(0, -3);
            this.gbMain.Name = "gbMain";
            this.gbMain.Size = new System.Drawing.Size(335, 130);
            this.gbMain.TabIndex = 9;
            this.gbMain.TabStop = false;
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(311, 10);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(21, 21);
            this.btnAdd.TabIndex = 1;
            this.btnAdd.Text = "+";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // lbtnDeleteAll
            // 
            this.lbtnDeleteAll.AutoSize = true;
            this.lbtnDeleteAll.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbtnDeleteAll.LinkColor = System.Drawing.Color.Black;
            this.lbtnDeleteAll.Location = new System.Drawing.Point(281, 114);
            this.lbtnDeleteAll.Name = "lbtnDeleteAll";
            this.lbtnDeleteAll.Size = new System.Drawing.Size(52, 13);
            this.lbtnDeleteAll.TabIndex = 4;
            this.lbtnDeleteAll.TabStop = true;
            this.lbtnDeleteAll.Text = "Delete All";
            this.lbtnDeleteAll.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lbtnDeleteAll_LinkClicked);
            // 
            // lstData
            // 
            this.lstData.FormattingEnabled = true;
            this.lstData.Location = new System.Drawing.Point(2, 31);
            this.lstData.Name = "lstData";
            this.lstData.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lstData.Size = new System.Drawing.Size(331, 82);
            this.lstData.TabIndex = 2;
            this.lstData.TabStop = false;
            this.lstData.DoubleClick += new System.EventHandler(this.lstData_DoubleClick);
            // 
            // lbtnSelectAll
            // 
            this.lbtnSelectAll.AutoSize = true;
            this.lbtnSelectAll.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbtnSelectAll.LinkColor = System.Drawing.Color.Black;
            this.lbtnSelectAll.Location = new System.Drawing.Point(3, 114);
            this.lbtnSelectAll.Name = "lbtnSelectAll";
            this.lbtnSelectAll.Size = new System.Drawing.Size(45, 13);
            this.lbtnSelectAll.TabIndex = 3;
            this.lbtnSelectAll.TabStop = true;
            this.lbtnSelectAll.Text = "Load All";
            this.lbtnSelectAll.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lbtnSelectAll_LinkClicked);
            // 
            // scsMaster
            // 
            this.scsMaster.Location = new System.Drawing.Point(2, 10);
            this.scsMaster.Name = "scsMaster";
            this.scsMaster.Size = new System.Drawing.Size(310, 21);
            this.scsMaster.TabIndex = 0;
            // 
            // MasterSelection
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.Controls.Add(this.gbMain);
            this.Name = "MasterSelection";
            this.Size = new System.Drawing.Size(335, 127);
            this.Leave += new System.EventHandler(this.MasterSelection_Leave);
            this.gbMain.ResumeLayout(false);
            this.gbMain.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbMain;
        private UserControls.SimpleComboSearch scsMaster;
        private System.Windows.Forms.LinkLabel lbtnSelectAll;
        private System.Windows.Forms.ListBox lstData;
        private System.Windows.Forms.LinkLabel lbtnDeleteAll;
        private System.Windows.Forms.Button btnAdd;

    }
}
