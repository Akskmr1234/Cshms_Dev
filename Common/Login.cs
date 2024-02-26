using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CsHms.Common
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (txtUsername.Text.ToString() == "admin" && txtPassword.Text.ToString() == "atsadmin")
            {
                
                MDIMain mdiForm = new MDIMain();
                mdiForm.Show();
            }
            else
            {
                lblValidation.Text = "Invalid Username or Password";
            }
        }

      
    }
}