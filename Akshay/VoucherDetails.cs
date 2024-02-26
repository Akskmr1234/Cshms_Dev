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
    public partial class VoucherDetails : Form
    {
        Global mGLobal = new Global();
        CommFuncs mCommFunc = new CommFuncs();
        public VoucherDetails()
        {
            InitializeComponent();
            GetFirms();
        }
        private void GetFirms()
        {
            try
            {
                string strSql = "select f_code,f_desc from firmmas";
                DataTable dtFirms = mGLobal.LocalDBCon.ExecuteQuery(strSql);
                if (dtFirms.Rows.Count > 0)
                {
                    cbxFirm.DataSource = dtFirms;
                    cbxFirm.DisplayMember = "f_desc";
                    cbxFirm.ValueMember = "f_code";
                }
            }
            catch(Exception ex)
            { writeErrorLog(ex, "GetFirms"); }
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

        private void btnGetData_Click(object sender, EventArgs e)
        {
            try
            {
                string strSql = "select * from udf_SalesandCollPostDetailed('" + mCommFunc.ConvertToString(dtDate.Value.ToString("yyyy-MM-dd")) + "','" + mCommFunc.ConvertToString(cbxFirm.SelectedValue) + "')";
                DataTable dtData = mGLobal.LocalDBCon.ExecuteQuery(strSql);
                if (dtData.Rows.Count > 0)
                {
                    dgvVoucherData.DataSource = dtData;
                }
                else
                {
                    MessageBox.Show("Data not found");
                }
            }
            catch (Exception ex)
            { writeErrorLog(ex, "btnGetData_Click"); }
        }
        private void ClearData()
        {
            cbxFirm.SelectedValue = "-1";
            dtDate.Value = DateTime.Now;
            dgvVoucherData.DataSource = null;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearData();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                Akshay.Class.VoucherCls objvoucher = new Akshay.Class.VoucherCls();
                mGLobal.LocalDBCon.BeginTrans();
                bool res = objvoucher.PostingToSalesAndCollectionAccounts(dtDate.Value, cbxFirm.SelectedValue.ToString());

                if (res == true)
                {
                    MessageBox.Show("Success");
                    mGLobal.LocalDBCon.CommitTrans();
                    ClearData();
                }
                else
                    MessageBox.Show("Error");
            }
            catch (Exception ex)
            { writeErrorLog(ex, "btnSave_Click"); }
        }
    }
}