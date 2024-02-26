namespace CsHms
{
    partial class Search
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Search));
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.btnSearch = new System.Windows.Forms.Button();
			this.cboCriteria = new System.Windows.Forms.ComboBox();
			this.label3 = new System.Windows.Forms.Label();
			this.txtSearchText = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.cboSearchby = new System.Windows.Forms.ComboBox();
			this.label1 = new System.Windows.Forms.Label();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.btnCancel = new System.Windows.Forms.Button();
			this.btnSelect = new System.Windows.Forms.Button();
			this.dgvDet = new System.Windows.Forms.DataGridView();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dgvDet)).BeginInit();
			this.SuspendLayout();
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.btnSearch);
			this.groupBox1.Controls.Add(this.cboCriteria);
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Controls.Add(this.txtSearchText);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.cboSearchby);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Location = new System.Drawing.Point(3, -4);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(497, 84);
			this.groupBox1.TabIndex = 0;
			this.groupBox1.TabStop = false;
			// 
			// btnSearch
			// 
			this.btnSearch.Location = new System.Drawing.Point(414, 55);
			this.btnSearch.Name = "btnSearch";
			this.btnSearch.Size = new System.Drawing.Size(75, 23);
			this.btnSearch.TabIndex = 6;
			this.btnSearch.TabStop = false;
			this.btnSearch.Text = "Searc&h";
			this.btnSearch.UseVisualStyleBackColor = true;
			this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
			// 
			// cboCriteria
			// 
			this.cboCriteria.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboCriteria.FormattingEnabled = true;
			this.cboCriteria.Items.AddRange(new object[] {
            "Start with",
            "Any where",
            "Equal to",
            "Grater than",
            "Less than"});
			this.cboCriteria.Location = new System.Drawing.Point(304, 11);
			this.cboCriteria.Name = "cboCriteria";
			this.cboCriteria.Size = new System.Drawing.Size(185, 21);
			this.cboCriteria.TabIndex = 3;
			this.cboCriteria.TabStop = false;
			this.cboCriteria.SelectedIndexChanged += new System.EventHandler(this.cboCriteria_SelectedIndexChanged);
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(266, 14);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(39, 13);
			this.label3.TabIndex = 2;
			this.label3.Text = "&Criteria";
			// 
			// txtSearchText
			// 
			this.txtSearchText.Location = new System.Drawing.Point(75, 34);
			this.txtSearchText.Name = "txtSearchText";
			this.txtSearchText.Size = new System.Drawing.Size(414, 20);
			this.txtSearchText.TabIndex = 5;
			this.txtSearchText.TextChanged += new System.EventHandler(this.txtSearchText_TextChanged);
			this.txtSearchText.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSearchText_KeyDown);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(6, 35);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(65, 13);
			this.label2.TabIndex = 4;
			this.label2.Text = "Search &Text";
			// 
			// cboSearchby
			// 
			this.cboSearchby.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboSearchby.FormattingEnabled = true;
			this.cboSearchby.Location = new System.Drawing.Point(75, 11);
			this.cboSearchby.Name = "cboSearchby";
			this.cboSearchby.Size = new System.Drawing.Size(185, 21);
			this.cboSearchby.TabIndex = 1;
			this.cboSearchby.TabStop = false;
			this.cboSearchby.SelectedIndexChanged += new System.EventHandler(this.cboSearchby_SelectedIndexChanged);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(6, 13);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(56, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "Search &By";
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.btnCancel);
			this.groupBox2.Controls.Add(this.btnSelect);
			this.groupBox2.Controls.Add(this.dgvDet);
			this.groupBox2.Location = new System.Drawing.Point(3, 79);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(497, 416);
			this.groupBox2.TabIndex = 7;
			this.groupBox2.TabStop = false;
			// 
			// btnCancel
			// 
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Location = new System.Drawing.Point(416, 389);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(75, 23);
			this.btnCancel.TabIndex = 9;
			this.btnCancel.Text = "Cance&l";
			this.btnCancel.UseVisualStyleBackColor = true;
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// btnSelect
			// 
			this.btnSelect.Location = new System.Drawing.Point(339, 389);
			this.btnSelect.Name = "btnSelect";
			this.btnSelect.Size = new System.Drawing.Size(75, 23);
			this.btnSelect.TabIndex = 8;
			this.btnSelect.Text = "&Select";
			this.btnSelect.UseVisualStyleBackColor = true;
			this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click);
			// 
			// dgvDet
			// 
			this.dgvDet.AllowUserToAddRows = false;
			this.dgvDet.AllowUserToDeleteRows = false;
			this.dgvDet.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dgvDet.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
			this.dgvDet.Location = new System.Drawing.Point(4, 11);
			this.dgvDet.MultiSelect = false;
			this.dgvDet.Name = "dgvDet";
			this.dgvDet.ReadOnly = true;
			this.dgvDet.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.dgvDet.ShowCellErrors = false;
			this.dgvDet.ShowCellToolTips = false;
			this.dgvDet.ShowEditingIcon = false;
			this.dgvDet.ShowRowErrors = false;
			this.dgvDet.Size = new System.Drawing.Size(487, 377);
			this.dgvDet.StandardTab = true;
			this.dgvDet.TabIndex = 0;
			this.dgvDet.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.dgvDet_MouseDoubleClick);
			// 
			// Search
			// 
			this.AcceptButton = this.btnSelect;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.btnCancel;
			this.ClientSize = new System.Drawing.Size(503, 498);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.groupBox2);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.KeyPreview = true;
			this.MaximizeBox = false;
			this.MaximumSize = new System.Drawing.Size(511, 532);
			this.MinimizeBox = false;
			this.MinimumSize = new System.Drawing.Size(511, 532);
			this.Name = "Search";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Search";
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Search_KeyDown);
			this.Load += new System.EventHandler(this.Search_Load);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.groupBox2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.dgvDet)).EndInit();
			this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cboSearchby;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtSearchText;
        private System.Windows.Forms.ComboBox cboCriteria;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSelect;
        private System.Windows.Forms.DataGridView dgvDet;
    }
}