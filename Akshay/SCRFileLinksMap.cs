using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using System.IO;

namespace CsHms.Akshay
{
    public partial class SCRFileLinksMap : Form
    {
        CommFuncs mCommFuncs = new CommFuncs();
        Global mGlobal = new Global();
        string mintAccessionno = "";
        DataTable dtscrfiledata = new DataTable();
        public SCRFileLinksMap()
        {
            InitializeComponent();

        }      
       /// <summary>
        /// Get datat to check the accessionno is exxist or not
       /// </summary>
       /// <param name="strqry"></param>
       /// <returns></returns>
        private DataTable getdata(string strqry)
        {
            try
            {
                DataTable dtdata=mGlobal.LocalDBCon.ExecuteQuery(strqry);
                if(dtdata.Rows.Count<=0)
                {
                MessageBox.Show("Data not found");
                }
                return dtdata;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
                writeErrorLog(ex, "getdata");
                return null;
            }
            
        }     
        /// <summary>
        /// Check the accessionno is already exist or not in scrfileslinks
        /// </summary>
        /// <param name="accessionno"></param>
        /// <returns></returns>
        private bool Check_updateorinsert(string accessionno)
        {
            try
            {
                string sqlStr = @"select * from scrfileslinks where sflink_refno='" + accessionno + "'";

                DataTable dtdata = mGlobal.LocalDBCon.ExecuteQuery(sqlStr);
                if (dtdata.Rows.Count <= 0)
                {
                    return false;

                }
                else
                    return true;
            }
            catch(Exception ex)
            {
                
                writeErrorLog(ex, "Check_updateorinsert");
            }
            return false;
        }
        /// <summary>
        /// Writing errorlog
        /// </summary>
        /// <param name="exception"></param>
        /// <param name="strErrorSource"></param>
        public static void writeErrorLog(Exception exception, string strErrorSource)
        {
            // Retrieve the path to the error log file from the application configuration.
            string strlogfile = String.Empty;
            strlogfile = ConfigurationSettings.AppSettings["ErrorLog_path"].ToString();

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
        /// <summary>
        /// Events
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExit_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void btnClear_Click(object sender, EventArgs e)
        {
            txtAccno.Text = "";
        }
        private void btnmap_Click(object sender, EventArgs e)
        {
            try
            {
                mintAccessionno = txtAccno.Text.ToString();
                string strSql = "select * from modalitypatientstatustran join scrfileslinksmas on mpst_itemptr=itemptr where mpst_accessionno='" + mintAccessionno + "'";
                DataTable dtdata = getdata(strSql);

                int r = 0;
                if (dtdata.Rows.Count > 0)
                {
                    string strFilepath = dtdata.Rows[0]["filepath"].ToString() + mintAccessionno + ".pdf";

                    string strAccessionno = dtdata.Rows[0]["mpst_accessionno"].ToString();
                    DateTime strDate = DateTime.Now;
                    string strFormattedDate = strDate.ToString("yyyy-MM-dd HH:mm:ss");

                    string strOpid = dtdata.Rows[0]["mpst_refid"].ToString();
                    string strOpdid = dtdata.Rows[0]["mpst_detrefid"].ToString();

                    if (Check_updateorinsert(mintAccessionno) == false)
                    {
                        strSql = "INSERT INTO scrfileslinks (sflink_dt, sflink_dttm, sflink_refid, sflink_refdetid, sflink_refno, sflink_type, sflink_filetype, sflink_filename, sflink_slno, sflink_otherrefdet1, sflink_Remarks, sflink_canflag) " +
                                 "VALUES ('" + strFormattedDate + "', '" + strFormattedDate + "', '" + strOpid + "', '" + strOpdid + "', '" + strAccessionno + "', 'RPT', 'FILE', '" + strFilepath + "', '1', '~SYS_AP~', NULL, NULL)";
                        r = mGlobal.LocalDBCon.ExecuteNonQuery(strSql);
                        if (r <= 0)
                            MessageBox.Show("Error");
                        else
                            MessageBox.Show("Inserted");
                    }
                    else
                    {

                        strSql = "UPDATE scrfileslinks SET sflink_filename='" + strFilepath + "' WHERE sflink_refno='" + mintAccessionno + "' and sflink_filetype='FILE'";
                        r = mGlobal.LocalDBCon.ExecuteNonQuery(strSql);
                        if (r <= 0)
                            MessageBox.Show("Error");
                        else
                            MessageBox.Show("Updated");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
                writeErrorLog(ex, "btnmap_Click");
            }
        }
    }
}