namespace LabInterface.CareData.LisWebAPI
{
    partial class HisForm
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
            this.txtbillno = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnshow = new System.Windows.Forms.Button();
            this.btnGetResults = new System.Windows.Forms.Button();
            this.btnFinalReport = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtbillno
            // 
            this.txtbillno.Location = new System.Drawing.Point(86, 40);
            this.txtbillno.Name = "txtbillno";
            this.txtbillno.Size = new System.Drawing.Size(100, 20);
            this.txtbillno.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(28, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(21, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "No";
            // 
            // btnshow
            // 
            this.btnshow.Location = new System.Drawing.Point(86, 66);
            this.btnshow.Name = "btnshow";
            this.btnshow.Size = new System.Drawing.Size(98, 23);
            this.btnshow.TabIndex = 2;
            this.btnshow.Text = "Push Order Details";
            this.btnshow.UseVisualStyleBackColor = true;
            this.btnshow.Click += new System.EventHandler(this.btnshow_Click);
            // 
            // btnGetResults
            // 
            this.btnGetResults.Location = new System.Drawing.Point(86, 95);
            this.btnGetResults.Name = "btnGetResults";
            this.btnGetResults.Size = new System.Drawing.Size(98, 23);
            this.btnGetResults.TabIndex = 3;
            this.btnGetResults.Text = "Get Visit Details";
            this.btnGetResults.UseVisualStyleBackColor = true;
            this.btnGetResults.Click += new System.EventHandler(this.btnGetResults_Click);
            // 
            // btnFinalReport
            // 
            this.btnFinalReport.Location = new System.Drawing.Point(86, 124);
            this.btnFinalReport.Name = "btnFinalReport";
            this.btnFinalReport.Size = new System.Drawing.Size(98, 23);
            this.btnFinalReport.TabIndex = 4;
            this.btnFinalReport.Text = "Final report";
            this.btnFinalReport.UseVisualStyleBackColor = true;
            this.btnFinalReport.Click += new System.EventHandler(this.btnFinalReport_Click);
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(86, 13);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 21);
            this.comboBox1.TabIndex = 5;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(86, 153);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(98, 23);
            this.btnCancel.TabIndex = 6;
            this.btnCancel.Text = "Bill Cancelation";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnUpdate
            // 
            this.btnUpdate.Location = new System.Drawing.Point(86, 182);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(98, 23);
            this.btnUpdate.TabIndex = 7;
            this.btnUpdate.Text = "Update Info";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(160, 66);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(98, 23);
            this.button1.TabIndex = 8;
            this.button1.Text = "testurl";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // HisForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.btnFinalReport);
            this.Controls.Add(this.btnGetResults);
            this.Controls.Add(this.btnshow);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtbillno);
            this.MaximizeBox = false;
            this.Name = "HisForm";
            this.Text = "HisForm";
            this.Load += new System.EventHandler(this.HisForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtbillno;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnshow;
        private System.Windows.Forms.Button btnGetResults;
        private System.Windows.Forms.Button btnFinalReport;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button button1;
    }
}