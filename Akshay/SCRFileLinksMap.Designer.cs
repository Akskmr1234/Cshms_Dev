namespace CsHms.Akshay
{
    partial class SCRFileLinksMap
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
            this.btnClear = new System.Windows.Forms.Button();
            this.btnmap = new System.Windows.Forms.Button();
            this.txtAccno = new System.Windows.Forms.TextBox();
            this.lblassno = new System.Windows.Forms.Label();
            this.btnExit = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(217, 80);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(75, 33);
            this.btnClear.TabIndex = 13;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnmap
            // 
            this.btnmap.Location = new System.Drawing.Point(310, 80);
            this.btnmap.Name = "btnmap";
            this.btnmap.Size = new System.Drawing.Size(75, 33);
            this.btnmap.TabIndex = 16;
            this.btnmap.Text = "Map";
            this.btnmap.UseVisualStyleBackColor = true;
            this.btnmap.Click += new System.EventHandler(this.btnmap_Click);
            // 
            // txtAccno
            // 
            this.txtAccno.Location = new System.Drawing.Point(217, 24);
            this.txtAccno.Name = "txtAccno";
            this.txtAccno.Size = new System.Drawing.Size(168, 26);
            this.txtAccno.TabIndex = 17;
            // 
            // lblassno
            // 
            this.lblassno.AutoSize = true;
            this.lblassno.Font = new System.Drawing.Font("Franklin Gothic Medium Cond", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblassno.Location = new System.Drawing.Point(12, 24);
            this.lblassno.Name = "lblassno";
            this.lblassno.Size = new System.Drawing.Size(173, 30);
            this.lblassno.TabIndex = 18;
            this.lblassno.Text = "Accession number";
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(125, 80);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(75, 33);
            this.btnExit.TabIndex = 19;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // scrfilelinksmap
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(398, 122);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.lblassno);
            this.Controls.Add(this.txtAccno);
            this.Controls.Add(this.btnmap);
            this.Controls.Add(this.btnClear);
            this.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.Name = "scrfilelinksmap";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "scrfilelinksmap";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnmap;
        private System.Windows.Forms.TextBox txtAccno;
        private System.Windows.Forms.Label lblassno;
        private System.Windows.Forms.Button btnExit;
    }
}