namespace CsHms.Akshay
{
    partial class DataInitialize
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
            this.btnExcecute = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.cbxScripts = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // btnExcecute
            // 
            this.btnExcecute.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExcecute.Location = new System.Drawing.Point(29, 77);
            this.btnExcecute.Name = "btnExcecute";
            this.btnExcecute.Size = new System.Drawing.Size(377, 40);
            this.btnExcecute.TabIndex = 0;
            this.btnExcecute.Text = "Excecute";
            this.btnExcecute.UseVisualStyleBackColor = true;
            this.btnExcecute.Click += new System.EventHandler(this.btnExcecute_Click);
            // 
            // btnExit
            // 
            this.btnExit.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExit.Location = new System.Drawing.Point(29, 132);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(377, 37);
            this.btnExit.TabIndex = 1;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // cbxScripts
            // 
            this.cbxScripts.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxScripts.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxScripts.FormattingEnabled = true;
            this.cbxScripts.Location = new System.Drawing.Point(29, 28);
            this.cbxScripts.Name = "cbxScripts";
            this.cbxScripts.Size = new System.Drawing.Size(377, 33);
            this.cbxScripts.TabIndex = 2;
            // 
            // DataInitialize
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(434, 194);
            this.Controls.Add(this.cbxScripts);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnExcecute);
            this.Name = "DataInitialize";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "DataInitialize";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnExcecute;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.ComboBox cbxScripts;
    }
}