using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Data.SqlClient;

namespace CsHms.Akshay
{
    public partial class FirmSelection : Form
    {
        public FirmSelection()
        {
            InitializeComponent();
            GetFirm();
        }
        
        Global mGlobal = new Global();
        CommFuncs mCommfunc = new CommFuncs();
        string mFirmId = "";        
        string mFinId = "";

        private void btnExit_Click(object sender, EventArgs e)
        {
            Close();
        }


        private void GetFirm()
        {
            try
            {
                string strSql = "select fm_id,fm_desc from firmmas";
                DataTable dtFirm = mGlobal.LocalDBCon.ExecuteQuery(strSql);
                if (dtFirm.Rows.Count > 0)
                {
                    lstbxFirms.DataSource = dtFirm;
                    lstbxFirms.DisplayMember = "fm_desc";
                    lstbxFirms.ValueMember = "fm_id";
                }
            }
            catch (Exception ex)
            { writeErrorLog(ex, "GetFirm"); }
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

        private void btnSelect_Click(object sender, EventArgs e)
        {
            try
            {
                mFirmId = mCommfunc.ConvertToString(lstbxFirms.SelectedValue);
                mFinId = mCommfunc.ConvertToString(lstbxFyear.SelectedValue);
                string strSql = "select config_ConString from Config where config_FirmId='" + mCommfunc.ConvertToString(lstbxFirms.SelectedValue) + "' and config_Financialyearid='"+mCommfunc.ConvertToString(lstbxFyear.SelectedValue)+"'";
                DataTable dtConstring = mGlobal.LocalDBCon.ExecuteQuery(strSql);
                if (dtConstring.Rows.Count > 0)
                {
                    DialogResult dr = new DialogResult();
                    dr=MessageBox.Show("Do you want to save changes?", "Confirmation", MessageBoxButtons.YesNo);
                    if (dr == DialogResult.Yes)
                    {
                        string strConstring = mCommfunc.ConvertToString(dtConstring.Rows[0]["config_ConString"]);
                        SqlConnection Conn = null;
                        DbConnSql dbRemovecon = new DbConnSql(Conn);
                        DbConnSql dbConString = new DbConnSql(strConstring);
                        Firm_Login Login = new Firm_Login();
                        Login.Show();
                    }
                    
                    
                }
            }
            catch (Exception ex)
            { writeErrorLog(ex, "btnSelect_Click"); }
        }

        private void lstbxFirms_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            { 
                string strSql = "select fy_desc,fy_id from FinancialYear where fy_firmptr='" + mCommfunc.ConvertToString(lstbxFirms.SelectedValue) + "' order by fy_slno";
                DataTable dtFyear = mGlobal.LocalDBCon.ExecuteQuery(strSql);
                if (dtFyear.Rows.Count > 0)
                {
                    lstbxFyear.DataSource = dtFyear;
                    lstbxFyear.DisplayMember = "fy_desc";
                    lstbxFyear.ValueMember = "fy_id";
                }
                else
                {
                    lstbxFyear.DataSource = null;
                    lstbxFyear.Items.Clear(); 
                }
            }
            catch (Exception ex)
            { writeErrorLog(ex, "lstbxFirms_SelectedValueChanged"); }
        }
    }
}