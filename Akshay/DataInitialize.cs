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
    public partial class DataInitialize : Form
    {
        Global mGLobal = new Global();
        CommFuncs mCommFunc = new CommFuncs();
        public DataInitialize()
        {
            InitializeComponent();
            GetScriptType();

        }
        private void GetScriptType()
        {
            try
            {
                string strSql = @"select submode,caption from core_scripts where mode='INIT' and active='Y'";
                DataTable dtScriptTypes = mGLobal.LocalDBCon.ExecuteQuery(strSql);
                if (dtScriptTypes.Rows.Count > 0)
                {
                    cbxScripts.DataSource = dtScriptTypes;
                    cbxScripts.ValueMember = "submode";
                    cbxScripts.DisplayMember = "caption";
                }
            }
            catch (Exception ex)
            { }
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

        private void btnExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnExcecute_Click(object sender, EventArgs e)
        {
            try
            {
                string strSql = @"select script from core_scripts where mode='INIT' and submode='"+mCommFunc.ConvertToString(cbxScripts.SelectedValue)+"'";
                DataTable dtScripts = mGLobal.LocalDBCon.ExecuteQuery(strSql);
                if (dtScripts.Rows.Count > 0)
                {
                    int res = 0;
                    mGLobal.LocalDBCon.BeginTrans();
                    for (int i = 0; i < dtScripts.Rows.Count; i++)
                    {
                        strSql = mCommFunc.ConvertToString(dtScripts.Rows[i]["script"]);
                        res = mGLobal.LocalDBCon.ExecuteNonQuery_OnTran(strSql);
                    }
                    if (res > 0)
                    {
                        MessageBox.Show("Excecution Completed");
                        mGLobal.LocalDBCon.CommitTrans();
                    }
                    else
                    {
                        MessageBox.Show("Error occured");
                        mGLobal.LocalDBCon.RollbackTrans();
                    }
                }
                else
                    MessageBox.Show("Data not found");
            }
            catch (Exception ex)
            {
                writeErrorLog(ex, "btnExcecute_Click");
                mGLobal.LocalDBCon.RollbackTrans();
            }
        }
    }
}