namespace CsHms.Akshay
{
    partial class OpMerge
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
            this.labelhead1 = new System.Windows.Forms.Label();
            this.lblhead2 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnClearall = new System.Windows.Forms.Button();
            this.btnMerge = new System.Windows.Forms.Button();
            this.txtA_PatientNm = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtA_Address = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtA_Category = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtA_Gender = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtA_Age = new System.Windows.Forms.TextBox();
            this.txtA_Doctor = new System.Windows.Forms.TextBox();
            this.txtA_ipno = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblAemail = new System.Windows.Forms.Label();
            this.txtA_email = new System.Windows.Forms.TextBox();
            this.txtA_phone = new System.Windows.Forms.TextBox();
            this.lblAphone = new System.Windows.Forms.Label();
            this.lblmoreinfo1 = new System.Windows.Forms.Label();
            this.dgvActualdata = new System.Windows.Forms.DataGridView();
            this.txtA_opno = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lblDemail = new System.Windows.Forms.Label();
            this.txtD_email = new System.Windows.Forms.TextBox();
            this.txtD_phone = new System.Windows.Forms.TextBox();
            this.lblDphone = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.dgvDupdata = new System.Windows.Forms.DataGridView();
            this.txtD_opno = new System.Windows.Forms.TextBox();
            this.label24 = new System.Windows.Forms.Label();
            this.txtD_ipno = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.txtD_Doctor = new System.Windows.Forms.TextBox();
            this.txtD_Age = new System.Windows.Forms.TextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.txtD_Gender = new System.Windows.Forms.TextBox();
            this.label20 = new System.Windows.Forms.Label();
            this.txtD_Category = new System.Windows.Forms.TextBox();
            this.label21 = new System.Windows.Forms.Label();
            this.txtD_Address = new System.Windows.Forms.TextBox();
            this.label22 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.txtD_PatientNm = new System.Windows.Forms.TextBox();
            this.panel2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvActualdata)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDupdata)).BeginInit();
            this.SuspendLayout();
            // 
            // labelhead1
            // 
            this.labelhead1.AutoSize = true;
            this.labelhead1.Font = new System.Drawing.Font("Franklin Gothic Medium Cond", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelhead1.Location = new System.Drawing.Point(5, 9);
            this.labelhead1.Name = "labelhead1";
            this.labelhead1.Size = new System.Drawing.Size(92, 37);
            this.labelhead1.TabIndex = 45;
            this.labelhead1.Text = "Actual";
            // 
            // lblhead2
            // 
            this.lblhead2.AutoSize = true;
            this.lblhead2.Font = new System.Drawing.Font("Franklin Gothic Medium Cond", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblhead2.Location = new System.Drawing.Point(650, 9);
            this.lblhead2.Name = "lblhead2";
            this.lblhead2.Size = new System.Drawing.Size(130, 37);
            this.lblhead2.TabIndex = 48;
            this.lblhead2.Text = "Duplicate";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnClose);
            this.panel2.Controls.Add(this.btnClearall);
            this.panel2.Controls.Add(this.btnMerge);
            this.panel2.Location = new System.Drawing.Point(410, 589);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(459, 53);
            this.panel2.TabIndex = 49;
            // 
            // btnClose
            // 
            this.btnClose.ForeColor = System.Drawing.Color.Black;
            this.btnClose.Location = new System.Drawing.Point(162, 0);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(129, 50);
            this.btnClose.TabIndex = 2;
            this.btnClose.Text = "Exit";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnClearall
            // 
            this.btnClearall.ForeColor = System.Drawing.Color.Black;
            this.btnClearall.Location = new System.Drawing.Point(16, 0);
            this.btnClearall.Name = "btnClearall";
            this.btnClearall.Size = new System.Drawing.Size(129, 50);
            this.btnClearall.TabIndex = 1;
            this.btnClearall.Text = "Clear All";
            this.btnClearall.UseVisualStyleBackColor = true;
            this.btnClearall.Click += new System.EventHandler(this.btnClearall_Click);
            // 
            // btnMerge
            // 
            this.btnMerge.ForeColor = System.Drawing.Color.Black;
            this.btnMerge.Location = new System.Drawing.Point(308, 0);
            this.btnMerge.Name = "btnMerge";
            this.btnMerge.Size = new System.Drawing.Size(129, 50);
            this.btnMerge.TabIndex = 0;
            this.btnMerge.Text = "Merge";
            this.btnMerge.UseVisualStyleBackColor = true;
            this.btnMerge.Click += new System.EventHandler(this.btnMerge_Click);
            // 
            // txtA_PatientNm
            // 
            this.txtA_PatientNm.BackColor = System.Drawing.Color.White;
            this.txtA_PatientNm.Enabled = false;
            this.txtA_PatientNm.Location = new System.Drawing.Point(89, 54);
            this.txtA_PatientNm.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtA_PatientNm.Name = "txtA_PatientNm";
            this.txtA_PatientNm.Size = new System.Drawing.Size(497, 26);
            this.txtA_PatientNm.TabIndex = 2;
            this.txtA_PatientNm.TabStop = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Franklin Gothic Book", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(335, 20);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(50, 21);
            this.label3.TabIndex = 5;
            this.label3.Text = "IP No.";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Franklin Gothic Book", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(9, 57);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(50, 21);
            this.label5.TabIndex = 9;
            this.label5.Text = "Name";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Franklin Gothic Book", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(9, 90);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(64, 21);
            this.label6.TabIndex = 11;
            this.label6.Text = "Address";
            // 
            // txtA_Address
            // 
            this.txtA_Address.BackColor = System.Drawing.Color.White;
            this.txtA_Address.Enabled = false;
            this.txtA_Address.Location = new System.Drawing.Point(89, 86);
            this.txtA_Address.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtA_Address.Name = "txtA_Address";
            this.txtA_Address.Size = new System.Drawing.Size(497, 26);
            this.txtA_Address.TabIndex = 12;
            this.txtA_Address.TabStop = false;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Franklin Gothic Book", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(9, 154);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(68, 21);
            this.label8.TabIndex = 15;
            this.label8.Text = "Category";
            // 
            // txtA_Category
            // 
            this.txtA_Category.BackColor = System.Drawing.Color.White;
            this.txtA_Category.Enabled = false;
            this.txtA_Category.Location = new System.Drawing.Point(89, 154);
            this.txtA_Category.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtA_Category.Name = "txtA_Category";
            this.txtA_Category.Size = new System.Drawing.Size(188, 26);
            this.txtA_Category.TabIndex = 16;
            this.txtA_Category.TabStop = false;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Franklin Gothic Book", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(9, 120);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(59, 21);
            this.label7.TabIndex = 17;
            this.label7.Text = "Gender";
            // 
            // txtA_Gender
            // 
            this.txtA_Gender.BackColor = System.Drawing.Color.White;
            this.txtA_Gender.Enabled = false;
            this.txtA_Gender.Location = new System.Drawing.Point(89, 121);
            this.txtA_Gender.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtA_Gender.Name = "txtA_Gender";
            this.txtA_Gender.Size = new System.Drawing.Size(188, 26);
            this.txtA_Gender.TabIndex = 18;
            this.txtA_Gender.TabStop = false;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Franklin Gothic Book", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(355, 123);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(35, 21);
            this.label9.TabIndex = 19;
            this.label9.Text = "Age";
            // 
            // txtA_Age
            // 
            this.txtA_Age.BackColor = System.Drawing.Color.White;
            this.txtA_Age.Enabled = false;
            this.txtA_Age.Location = new System.Drawing.Point(398, 121);
            this.txtA_Age.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtA_Age.Name = "txtA_Age";
            this.txtA_Age.Size = new System.Drawing.Size(188, 26);
            this.txtA_Age.TabIndex = 20;
            this.txtA_Age.TabStop = false;
            // 
            // txtA_Doctor
            // 
            this.txtA_Doctor.BackColor = System.Drawing.Color.White;
            this.txtA_Doctor.Enabled = false;
            this.txtA_Doctor.Location = new System.Drawing.Point(398, 154);
            this.txtA_Doctor.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtA_Doctor.Name = "txtA_Doctor";
            this.txtA_Doctor.Size = new System.Drawing.Size(188, 26);
            this.txtA_Doctor.TabIndex = 21;
            this.txtA_Doctor.TabStop = false;
            // 
            // txtA_ipno
            // 
            this.txtA_ipno.BackColor = System.Drawing.Color.White;
            this.txtA_ipno.Enabled = false;
            this.txtA_ipno.Location = new System.Drawing.Point(398, 20);
            this.txtA_ipno.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtA_ipno.Name = "txtA_ipno";
            this.txtA_ipno.Size = new System.Drawing.Size(188, 26);
            this.txtA_ipno.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblAemail);
            this.groupBox1.Controls.Add(this.txtA_email);
            this.groupBox1.Controls.Add(this.txtA_phone);
            this.groupBox1.Controls.Add(this.lblAphone);
            this.groupBox1.Controls.Add(this.lblmoreinfo1);
            this.groupBox1.Controls.Add(this.dgvActualdata);
            this.groupBox1.Controls.Add(this.txtA_opno);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtA_ipno);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.txtA_Doctor);
            this.groupBox1.Controls.Add(this.txtA_Age);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.txtA_Gender);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.txtA_Category);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.txtA_Address);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtA_PatientNm);
            this.groupBox1.Location = new System.Drawing.Point(12, 46);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox1.Size = new System.Drawing.Size(617, 530);
            this.groupBox1.TabIndex = 43;
            this.groupBox1.TabStop = false;
            // 
            // lblAemail
            // 
            this.lblAemail.AutoSize = true;
            this.lblAemail.Font = new System.Drawing.Font("Franklin Gothic Book", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAemail.Location = new System.Drawing.Point(337, 192);
            this.lblAemail.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblAemail.Name = "lblAemail";
            this.lblAemail.Size = new System.Drawing.Size(48, 21);
            this.lblAemail.TabIndex = 50;
            this.lblAemail.Text = "Email";
            // 
            // txtA_email
            // 
            this.txtA_email.BackColor = System.Drawing.Color.White;
            this.txtA_email.Enabled = false;
            this.txtA_email.Location = new System.Drawing.Point(398, 190);
            this.txtA_email.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtA_email.Name = "txtA_email";
            this.txtA_email.Size = new System.Drawing.Size(188, 26);
            this.txtA_email.TabIndex = 49;
            this.txtA_email.TabStop = false;
            // 
            // txtA_phone
            // 
            this.txtA_phone.BackColor = System.Drawing.Color.White;
            this.txtA_phone.Enabled = false;
            this.txtA_phone.Location = new System.Drawing.Point(89, 190);
            this.txtA_phone.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtA_phone.Name = "txtA_phone";
            this.txtA_phone.Size = new System.Drawing.Size(188, 26);
            this.txtA_phone.TabIndex = 48;
            this.txtA_phone.TabStop = false;
            // 
            // lblAphone
            // 
            this.lblAphone.AutoSize = true;
            this.lblAphone.Font = new System.Drawing.Font("Franklin Gothic Book", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAphone.Location = new System.Drawing.Point(9, 190);
            this.lblAphone.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblAphone.Name = "lblAphone";
            this.lblAphone.Size = new System.Drawing.Size(53, 21);
            this.lblAphone.TabIndex = 47;
            this.lblAphone.Text = "Phone";
            // 
            // lblmoreinfo1
            // 
            this.lblmoreinfo1.AutoSize = true;
            this.lblmoreinfo1.Font = new System.Drawing.Font("Franklin Gothic Medium Cond", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblmoreinfo1.Location = new System.Drawing.Point(5, 240);
            this.lblmoreinfo1.Name = "lblmoreinfo1";
            this.lblmoreinfo1.Size = new System.Drawing.Size(97, 30);
            this.lblmoreinfo1.TabIndex = 46;
            this.lblmoreinfo1.Text = "More Info";
            // 
            // dgvActualdata
            // 
            this.dgvActualdata.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvActualdata.Location = new System.Drawing.Point(0, 273);
            this.dgvActualdata.Name = "dgvActualdata";
            this.dgvActualdata.RowTemplate.Height = 28;
            this.dgvActualdata.Size = new System.Drawing.Size(617, 257);
            this.dgvActualdata.TabIndex = 41;
            // 
            // txtA_opno
            // 
            this.txtA_opno.BackColor = System.Drawing.Color.White;
            this.txtA_opno.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtA_opno.Location = new System.Drawing.Point(90, 21);
            this.txtA_opno.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtA_opno.Name = "txtA_opno";
            this.txtA_opno.Size = new System.Drawing.Size(188, 26);
            this.txtA_opno.TabIndex = 40;
            this.txtA_opno.TabStop = false;
            this.txtA_opno.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtA_opno_KeyDown);
            this.txtA_opno.Validating += new System.ComponentModel.CancelEventHandler(this.txtA_opno_Validating);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Franklin Gothic Book", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(10, 23);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(58, 21);
            this.label4.TabIndex = 39;
            this.label4.Text = "OP NO.";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Franklin Gothic Book", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(337, 156);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(53, 21);
            this.label10.TabIndex = 22;
            this.label10.Text = "Doctor";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lblDemail);
            this.groupBox2.Controls.Add(this.txtD_email);
            this.groupBox2.Controls.Add(this.txtD_phone);
            this.groupBox2.Controls.Add(this.lblDphone);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.dgvDupdata);
            this.groupBox2.Controls.Add(this.txtD_opno);
            this.groupBox2.Controls.Add(this.label24);
            this.groupBox2.Controls.Add(this.txtD_ipno);
            this.groupBox2.Controls.Add(this.label18);
            this.groupBox2.Controls.Add(this.txtD_Doctor);
            this.groupBox2.Controls.Add(this.txtD_Age);
            this.groupBox2.Controls.Add(this.label19);
            this.groupBox2.Controls.Add(this.txtD_Gender);
            this.groupBox2.Controls.Add(this.label20);
            this.groupBox2.Controls.Add(this.txtD_Category);
            this.groupBox2.Controls.Add(this.label21);
            this.groupBox2.Controls.Add(this.txtD_Address);
            this.groupBox2.Controls.Add(this.label22);
            this.groupBox2.Controls.Add(this.label23);
            this.groupBox2.Controls.Add(this.label25);
            this.groupBox2.Controls.Add(this.txtD_PatientNm);
            this.groupBox2.Location = new System.Drawing.Point(657, 47);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox2.Size = new System.Drawing.Size(617, 530);
            this.groupBox2.TabIndex = 50;
            this.groupBox2.TabStop = false;
            // 
            // lblDemail
            // 
            this.lblDemail.AutoSize = true;
            this.lblDemail.Font = new System.Drawing.Font("Franklin Gothic Book", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDemail.Location = new System.Drawing.Point(337, 195);
            this.lblDemail.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDemail.Name = "lblDemail";
            this.lblDemail.Size = new System.Drawing.Size(48, 21);
            this.lblDemail.TabIndex = 56;
            this.lblDemail.Text = "Email";
            // 
            // txtD_email
            // 
            this.txtD_email.BackColor = System.Drawing.Color.White;
            this.txtD_email.Enabled = false;
            this.txtD_email.Location = new System.Drawing.Point(398, 193);
            this.txtD_email.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtD_email.Name = "txtD_email";
            this.txtD_email.Size = new System.Drawing.Size(188, 26);
            this.txtD_email.TabIndex = 55;
            this.txtD_email.TabStop = false;
            // 
            // txtD_phone
            // 
            this.txtD_phone.BackColor = System.Drawing.Color.White;
            this.txtD_phone.Enabled = false;
            this.txtD_phone.Location = new System.Drawing.Point(89, 193);
            this.txtD_phone.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtD_phone.Name = "txtD_phone";
            this.txtD_phone.Size = new System.Drawing.Size(188, 26);
            this.txtD_phone.TabIndex = 54;
            this.txtD_phone.TabStop = false;
            // 
            // lblDphone
            // 
            this.lblDphone.AutoSize = true;
            this.lblDphone.Font = new System.Drawing.Font("Franklin Gothic Book", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDphone.Location = new System.Drawing.Point(9, 193);
            this.lblDphone.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDphone.Name = "lblDphone";
            this.lblDphone.Size = new System.Drawing.Size(53, 21);
            this.lblDphone.TabIndex = 53;
            this.lblDphone.Text = "Phone";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Franklin Gothic Medium Cond", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(7, 240);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(97, 30);
            this.label2.TabIndex = 52;
            this.label2.Text = "More Info";
            // 
            // dgvDupdata
            // 
            this.dgvDupdata.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDupdata.Location = new System.Drawing.Point(0, 273);
            this.dgvDupdata.Name = "dgvDupdata";
            this.dgvDupdata.RowTemplate.Height = 28;
            this.dgvDupdata.Size = new System.Drawing.Size(617, 257);
            this.dgvDupdata.TabIndex = 42;
            // 
            // txtD_opno
            // 
            this.txtD_opno.BackColor = System.Drawing.Color.White;
            this.txtD_opno.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtD_opno.Location = new System.Drawing.Point(89, 23);
            this.txtD_opno.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtD_opno.Name = "txtD_opno";
            this.txtD_opno.Size = new System.Drawing.Size(188, 26);
            this.txtD_opno.TabIndex = 51;
            this.txtD_opno.TabStop = false;
            this.txtD_opno.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtD_opno_KeyDown);
            this.txtD_opno.Validating += new System.ComponentModel.CancelEventHandler(this.txtD_opno_Validating);
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Font = new System.Drawing.Font("Franklin Gothic Book", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label24.Location = new System.Drawing.Point(10, 26);
            this.label24.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(58, 21);
            this.label24.TabIndex = 50;
            this.label24.Text = "OP NO.";
            // 
            // txtD_ipno
            // 
            this.txtD_ipno.BackColor = System.Drawing.Color.White;
            this.txtD_ipno.Enabled = false;
            this.txtD_ipno.Location = new System.Drawing.Point(397, 23);
            this.txtD_ipno.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtD_ipno.Name = "txtD_ipno";
            this.txtD_ipno.Size = new System.Drawing.Size(188, 26);
            this.txtD_ipno.TabIndex = 0;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Franklin Gothic Book", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.Location = new System.Drawing.Point(337, 159);
            this.label18.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(53, 21);
            this.label18.TabIndex = 22;
            this.label18.Text = "Doctor";
            // 
            // txtD_Doctor
            // 
            this.txtD_Doctor.BackColor = System.Drawing.Color.White;
            this.txtD_Doctor.Enabled = false;
            this.txtD_Doctor.Location = new System.Drawing.Point(398, 157);
            this.txtD_Doctor.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtD_Doctor.Name = "txtD_Doctor";
            this.txtD_Doctor.Size = new System.Drawing.Size(188, 26);
            this.txtD_Doctor.TabIndex = 21;
            this.txtD_Doctor.TabStop = false;
            // 
            // txtD_Age
            // 
            this.txtD_Age.BackColor = System.Drawing.Color.White;
            this.txtD_Age.Enabled = false;
            this.txtD_Age.Location = new System.Drawing.Point(398, 124);
            this.txtD_Age.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtD_Age.Name = "txtD_Age";
            this.txtD_Age.Size = new System.Drawing.Size(188, 26);
            this.txtD_Age.TabIndex = 20;
            this.txtD_Age.TabStop = false;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("Franklin Gothic Book", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.Location = new System.Drawing.Point(355, 125);
            this.label19.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(35, 21);
            this.label19.TabIndex = 19;
            this.label19.Text = "Age";
            // 
            // txtD_Gender
            // 
            this.txtD_Gender.BackColor = System.Drawing.Color.White;
            this.txtD_Gender.Enabled = false;
            this.txtD_Gender.Location = new System.Drawing.Point(89, 124);
            this.txtD_Gender.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtD_Gender.Name = "txtD_Gender";
            this.txtD_Gender.Size = new System.Drawing.Size(188, 26);
            this.txtD_Gender.TabIndex = 18;
            this.txtD_Gender.TabStop = false;
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Font = new System.Drawing.Font("Franklin Gothic Book", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label20.Location = new System.Drawing.Point(9, 123);
            this.label20.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(59, 21);
            this.label20.TabIndex = 17;
            this.label20.Text = "Gender";
            // 
            // txtD_Category
            // 
            this.txtD_Category.BackColor = System.Drawing.Color.White;
            this.txtD_Category.Enabled = false;
            this.txtD_Category.Location = new System.Drawing.Point(89, 157);
            this.txtD_Category.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtD_Category.Name = "txtD_Category";
            this.txtD_Category.Size = new System.Drawing.Size(188, 26);
            this.txtD_Category.TabIndex = 16;
            this.txtD_Category.TabStop = false;
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Font = new System.Drawing.Font("Franklin Gothic Book", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label21.Location = new System.Drawing.Point(9, 157);
            this.label21.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(68, 21);
            this.label21.TabIndex = 15;
            this.label21.Text = "Category";
            // 
            // txtD_Address
            // 
            this.txtD_Address.BackColor = System.Drawing.Color.White;
            this.txtD_Address.Enabled = false;
            this.txtD_Address.Location = new System.Drawing.Point(89, 89);
            this.txtD_Address.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtD_Address.Name = "txtD_Address";
            this.txtD_Address.Size = new System.Drawing.Size(497, 26);
            this.txtD_Address.TabIndex = 12;
            this.txtD_Address.TabStop = false;
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Font = new System.Drawing.Font("Franklin Gothic Book", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label22.Location = new System.Drawing.Point(9, 93);
            this.label22.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(64, 21);
            this.label22.TabIndex = 11;
            this.label22.Text = "Address";
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Font = new System.Drawing.Font("Franklin Gothic Book", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label23.Location = new System.Drawing.Point(9, 60);
            this.label23.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(50, 21);
            this.label23.TabIndex = 9;
            this.label23.Text = "Name";
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Font = new System.Drawing.Font("Franklin Gothic Book", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label25.Location = new System.Drawing.Point(340, 28);
            this.label25.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(50, 21);
            this.label25.TabIndex = 5;
            this.label25.Text = "IP No.";
            // 
            // txtD_PatientNm
            // 
            this.txtD_PatientNm.BackColor = System.Drawing.Color.White;
            this.txtD_PatientNm.Enabled = false;
            this.txtD_PatientNm.Location = new System.Drawing.Point(89, 57);
            this.txtD_PatientNm.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtD_PatientNm.Name = "txtD_PatientNm";
            this.txtD_PatientNm.Size = new System.Drawing.Size(497, 26);
            this.txtD_PatientNm.TabIndex = 2;
            this.txtD_PatientNm.TabStop = false;
            // 
            // OpMerge
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1293, 659);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.lblhead2);
            this.Controls.Add(this.labelhead1);
            this.Controls.Add(this.groupBox1);
            this.Name = "OpMerge";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "OpMerge";
            this.panel2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvActualdata)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDupdata)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelhead1;
        private System.Windows.Forms.Label lblhead2;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnMerge;
        private System.Windows.Forms.TextBox txtA_PatientNm;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtA_Address;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtA_Category;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtA_Gender;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtA_Age;
        private System.Windows.Forms.TextBox txtA_Doctor;
        private System.Windows.Forms.TextBox txtA_ipno;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtD_ipno;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.TextBox txtD_Doctor;
        private System.Windows.Forms.TextBox txtD_Age;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.TextBox txtD_Gender;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.TextBox txtD_Category;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.TextBox txtD_Address;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.TextBox txtD_PatientNm;
        private System.Windows.Forms.TextBox txtA_opno;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtD_opno;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Button btnClearall;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.DataGridView dgvActualdata;
        private System.Windows.Forms.DataGridView dgvDupdata;
        private System.Windows.Forms.Label lblmoreinfo1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblAemail;
        private System.Windows.Forms.TextBox txtA_email;
        private System.Windows.Forms.TextBox txtA_phone;
        private System.Windows.Forms.Label lblAphone;
        private System.Windows.Forms.Label lblDemail;
        private System.Windows.Forms.TextBox txtD_email;
        private System.Windows.Forms.TextBox txtD_phone;
        private System.Windows.Forms.Label lblDphone;
    }
}