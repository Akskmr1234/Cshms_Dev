namespace CsHms.Akshay
{
    partial class ChangeConfig
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
            this.cbxDatabase = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnBillNo = new System.Windows.Forms.Button();
            this.btnFinancialYear = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // cbxDatabase
            // 
            this.cbxDatabase.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxDatabase.FormattingEnabled = true;
            this.cbxDatabase.Location = new System.Drawing.Point(151, 29);
            this.cbxDatabase.Name = "cbxDatabase";
            this.cbxDatabase.Size = new System.Drawing.Size(345, 28);
            this.cbxDatabase.TabIndex = 3;
            this.cbxDatabase.SelectionChangeCommitted += new System.EventHandler(this.cbxDatabase_SelectionChangeCommitted);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(41, 33);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(79, 20);
            this.label2.TabIndex = 2;
            this.label2.Text = "Database";
            // 
            // btnBillNo
            // 
            this.btnBillNo.Location = new System.Drawing.Point(37, 93);
            this.btnBillNo.Name = "btnBillNo";
            this.btnBillNo.Size = new System.Drawing.Size(125, 43);
            this.btnBillNo.TabIndex = 4;
            this.btnBillNo.Text = "BillNo";
            this.btnBillNo.UseVisualStyleBackColor = true;
            this.btnBillNo.Click += new System.EventHandler(this.btnBillNo_Click);
            // 
            // btnFinancialYear
            // 
            this.btnFinancialYear.Location = new System.Drawing.Point(209, 93);
            this.btnFinancialYear.Name = "btnFinancialYear";
            this.btnFinancialYear.Size = new System.Drawing.Size(125, 43);
            this.btnFinancialYear.TabIndex = 5;
            this.btnFinancialYear.Text = "Financial Year";
            this.btnFinancialYear.UseVisualStyleBackColor = true;
            this.btnFinancialYear.Click += new System.EventHandler(this.btnFinancialYear_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(381, 93);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(125, 43);
            this.button3.TabIndex = 6;
            this.button3.Text = "button3";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(37, 159);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(125, 43);
            this.button4.TabIndex = 7;
            this.button4.Text = "button4";
            this.button4.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(209, 159);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(125, 43);
            this.button1.TabIndex = 8;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(381, 159);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(125, 43);
            this.button5.TabIndex = 9;
            this.button5.Text = "button5";
            this.button5.UseVisualStyleBackColor = true;
            // 
            // ChangeConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(527, 227);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.btnFinancialYear);
            this.Controls.Add(this.btnBillNo);
            this.Controls.Add(this.cbxDatabase);
            this.Controls.Add(this.label2);
            this.Name = "ChangeConfig";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ChangeConfig";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbxDatabase;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnBillNo;
        private System.Windows.Forms.Button btnFinancialYear;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button5;
    }
}