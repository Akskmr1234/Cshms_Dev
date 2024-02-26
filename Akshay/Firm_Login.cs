using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace CsHms.Akshay
{
    public partial class Firm_Login : Form
    {
        public Firm_Login()
        {
            InitializeComponent();
        }


        CommFuncs mCommfunc = new CommFuncs();
        Global mGLobal =new Global();
        private void btnLogin_Click(object sender, EventArgs e)
        {
            Login();
        }
        private void Login()
        {
            try
            {
                string strSql = "select * from core_users where email='"+mCommfunc.ConvertToString(txtUname.Text)+"' and password='"+mCommfunc.ConvertToString(txtPassword.Text)+"'";
                DataTable dtLogin = mGLobal.LocalDBCon.ExecuteQuery(strSql);
                if (dtLogin.Rows.Count > 0)
                {
                    MessageBox.Show("Login Success");
                }
                else
                    MessageBox.Show("Login Failed");
            }
            catch (Exception ex) { }
 
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            Close();
        }
        public static void writeErrorLog(Exception exception, string strErrorSource)
        {
            // Retrieve the path to the error log file from the application configuration.
            string strlogfile = String.Empty;
            strlogfile = System.Configuration.ConfigurationSettings.AppSettings["ErrorLog_path"].ToString();

            try
            {
                // Check if the log file exists; if not, create it.
                if (!File.Exists(strlogfile))
                {
                    File.CreateText(strlogfile);
                }
                // Construct the error log entry.
                string strErrorText = "TMR_Error_Log=> An Error occurred: " + strErrorSource + " ON:" + DateTime.Now;
                strErrorText += Environment.NewLine + exception.Message + Environment.NewLine + exception.StackTrace;
                // Append the error log entry to the log file.
                using (System.IO.FileStream aFile = new System.IO.FileStream(strlogfile, System.IO.FileMode.Append, System.IO.FileAccess.Write))
                using (System.IO.StreamWriter sw = new System.IO.StreamWriter(aFile))
                {
                    sw.WriteLine("**********************************************************************");
                    sw.WriteLine(strErrorText);
                    sw.WriteLine("**********************************************************************");
                }
            }
            catch (Exception ex)
            {
                // If an error occurs during logging, re-throw the exception.
                writeErrorLog(ex, "writeErrorLog");
                throw;
            }
        }

       
    }
}