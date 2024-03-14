using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;
using System.IO;
using System.Web.Script.Serialization;
using System.Data.SqlClient;



namespace CsHms.Akshay
{
    public partial class CampgnMasterCreator : Form
    {
        public CampgnMasterCreator()
        {
            InitializeComponent();            
        }
        Global mGlobal = new Global();
        CommFuncs mCommFunc = new CommFuncs();
        string filePath = "";
        private void btnFile_Click(object sender, EventArgs e)
        {
            
            Waitfrm WaitForm = new Waitfrm();
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Excel Files|*.xls;*.xlsx;*.csv";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    filePath = openFileDialog.FileName;
                    DataTable dataTable = ReadExcel(filePath);
                    if (dataTable != null && dataTable.Rows.Count > 0)
                    {
                        dgvList.DataSource = dataTable;
                
                    }
                }
                catch (Exception ex)
                {
                    // Handle or log the exception
                    MessageBox.Show("An error occurred: " + ex.Message + "", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private DataTable ReadExcel(string filePath)
        {
            // Connection string for Excel
            string connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filePath + ";Extended Properties='Excel 12.0 Xml;HDR=YES;ReadOnly=1';";
            // Create connection
            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                // Open connection
                connection.Open();

                // SQL command to retrieve data (you may need to adjust the sheet name)
                DataTable sheets = connection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new object[] { null, null, null, "TABLE" });

                cbxSheets.DataSource = sheets;
                cbxSheets.DisplayMember = "TABLE_NAME";
                cbxSheets.ValueMember = "TABLE_NAME";
                string query = "SELECT * FROM [" + sheets.Rows[0]["TABLE_NAME"].ToString() + "]  ";

                // Create DataAdapter
                using (OleDbDataAdapter adapter = new OleDbDataAdapter(query, connection))
                {
                    // Create DataTable to hold the data
                    DataTable dataTable = new DataTable();
                    // Fill the DataTable with data from Excel
                    adapter.Fill(dataTable);
                    return dataTable;
                }
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SubmitData();
        }

      
        private const int BatchSize = 1000; // Set your desired batch size
        
        private void SubmitData()
        {
            try
            {
                string strSql = "";
                DataTable dtDgvData = ((DataTable)dgvList.DataSource);
                progressBar.Minimum = 0;
                progressBar.Maximum = dtDgvData.Rows.Count;
                //mGlobal.LocalDBCon.BeginTrans();
                StringBuilder batchQuery = new StringBuilder();
                if (cbxSheets.SelectedValue.ToString() == "Bengaluru$")
                {
                    string strConstring = System.Configuration.ConfigurationSettings.AppSettings.Get("Bengaluru");
                    SqlConnection Conn = null;
                    DbConnSql dbRemovecon = new DbConnSql(Conn);
                    DbConnSql dbConString = new DbConnSql(strConstring);
                }
                else if (cbxSheets.SelectedValue.ToString() == "Gurgaon$")
                {
                    string strConstring = System.Configuration.ConfigurationSettings.AppSettings.Get("Gurgaon");
                    SqlConnection Conn = null;
                    DbConnSql dbRemovecon = new DbConnSql(Conn);
                    DbConnSql dbConString = new DbConnSql(strConstring);
                }
                else if (cbxSheets.SelectedValue.ToString() == "Mumbai$")
                {
                    string strConstring = System.Configuration.ConfigurationSettings.AppSettings.Get("Mumbai");
                    SqlConnection Conn = null;
                    DbConnSql dbRemovecon = new DbConnSql(Conn);
                    DbConnSql dbConString = new DbConnSql(strConstring);
                }
                else if (cbxSheets.SelectedValue.ToString() == "Hyderabad$")
                {
                    string strConstring = System.Configuration.ConfigurationSettings.AppSettings.Get("Hyderabad");
                    SqlConnection Conn = null;
                    DbConnSql dbRemovecon = new DbConnSql(Conn);
                    DbConnSql dbConString = new DbConnSql(strConstring);
                }
                for (int i = 0; i < dtDgvData.Rows.Count; i++)
                {                  
                    //string strhdrid = mCommFunc.ConvertToString(dtDgvData.Rows[i]["NURA_ID"]);
                    string strRefno = mCommFunc.ConvertToString(dtDgvData.Rows[i]["NURA_ID"]);
                    strSql = @"select * from opreg where op_no='" + strRefno + "'";
                    DataTable dtOpregdet = mGlobal.LocalDBCon.ExecuteQuery(strSql);
                    string strMobile = "";
                    string strEmail = "";
                    string strName = "";
                    string strAddress = "";
                    if (dtOpregdet != null && dtOpregdet.Rows.Count > 0)
                    {
                        string strMobileNums = mCommFunc.ConvertToString(dtOpregdet.Rows[0]["op_mobile"]);
                        string[] mobileparts=strMobileNums.Split('/');
                        if (mobileparts.Length > 0)
                        {
                            strMobile = mobileparts[0].Replace("+91", "").Replace(" ", "").Replace("-","");
                        }
                        else
                        {
                            string strTrimMobile=mCommFunc.ConvertToString(dtOpregdet.Rows[0]["op_mobile"]);
                            strMobile = strTrimMobile.Replace("+91","").Replace(" ","").Replace("-","");
                            strEmail = mCommFunc.ConvertToString(dtOpregdet.Rows[0]["op_email"]);
                            strName = mCommFunc.ConvertToString(dtOpregdet.Rows[0]["op_title"]) + " " + mCommFunc.ConvertToString(dtOpregdet.Rows[0]["op_fname"]) + " " + mCommFunc.ConvertToString(dtOpregdet.Rows[0]["op_lname"]);
                            strAddress = mCommFunc.ConvertToString(dtOpregdet.Rows[0]["op_add1"]);
                        }

                    }
                    string strRfid = mCommFunc.ConvertToString(dtDgvData.Rows[i]["OrderNo"]);
                    string strLocation = mCommFunc.ConvertToString(dtDgvData.Rows[i]["location"]);
                    string filename = mCommFunc.ConvertToString(dtDgvData.Rows[i]["filename"]);
                    string strPersonalDet = "";
                    //string strotherDet = mCommFunc.ConvertToString(dtDgvData.Rows[i]["location"]);
                    string strotherDet="";
                    strSql="select prereg_id from prereg where prereg_opno='"+strRefno+"'";
                    DataTable dtPreregid=mGlobal.LocalDBCon.ExecuteQuery(strSql);
                    int index = filename.LastIndexOf(".html");
                    if (index > 0)
                    {
                        strPersonalDet = filename.Substring(0, index);
                    }



                    DateTime discontinuedDate;
                    SetProgressBar(i);


                    int res = 0;
                    if(dtPreregid!=null && dtPreregid.Rows.Count>0)
                    {
                     strotherDet="{\"prereg_id\":\""+mCommFunc.ConvertToString(dtPreregid.Rows[0]["prereg_id"])+"\"}";
                     strSql = @"INSERT INTO [dbo].[campgn_master_details] ([header_id],[reference_number],[mobile],[email],[other_personal_details],[other_details],[delivery_status],[delivery_id],[delivery_datetime],[name],[address],[mail_content]) values('','" + strRefno + "','" + strMobile + "','" + strEmail + "','" + strPersonalDet + "','" + strotherDet + "','','','','" + strName + "','" + strAddress + "','')";
                    }
                    else
                        strSql = @"INSERT INTO [dbo].[campgn_master_details] ([header_id],[reference_number],[mobile],[email],[other_personal_details],[other_details],[delivery_status],[delivery_id],[delivery_datetime],[name],[address],[mail_content]) values('','" + strRefno + "','" + strMobile + "','" + strEmail + "','" + strPersonalDet + "','','PENDING','','','" + strName + "','" + strAddress + "','')";
                   
                    
                    // Append the current query to the batch query
                   
                         res = mGlobal.LocalDBCon.ExecuteNonQuery(strSql);
                    

                    // If the batch size is reached or it's the last iteration, execute the batch
                    if ((dtDgvData.Rows.Count - 1) == i )
                    {
                        MessageBox.Show("Success");
                    }
                }
                //strSql = @"update phm_product_temp set pm_active= case when pm_discontinuedon<getdate() then 'N' else 'Y' end where pm_discontinuedon is not null";
                
            }
            catch (Exception ex)
            {
                writeErrorLog(ex, "SubmitData");
                MessageBox.Show(ex.Message.ToString());
                
                //mGlobal.LocalDBCon.RollbackTrans();
            }
            finally
            { progressBar.Value = 0; }
        }

        
        private bool CheckALreadyExist(string strrefno)
        {
            try
            {
                string strSql = @"select * from campgn_master_details where reference_number='" + strrefno + "'";
                DataTable dtres = mGlobal.LocalDBCon.ExecuteQuery_OnTran(strSql);
                if (dtres.Rows.Count>0)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            { 
            writeErrorLog(ex, "CheckALreadyExist");
            return false;
            }
        }
        private void SetProgressBar(int intValue)
        {
            try
            {progressBar.Value = intValue;}
            catch (Exception ex)
            { MessageBox.Show(ex.Message.ToString()); }
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
    
        private DataTable ReadExcelByValue(string filePath)
        {
            // Connection string for Excel
            string connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filePath + ";Extended Properties='Excel 12.0 Xml;HDR=YES;ReadOnly=1';";
            // Create connection
            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                // Open connection
                connection.Open();



                string query = "SELECT * FROM [" + cbxSheets.SelectedValue.ToString() + "]  ";
                // Create DataAdapter
                using (OleDbDataAdapter adapter = new OleDbDataAdapter(query, connection))
                {
                    // Create DataTable to hold the data
                    DataTable dataTable = new DataTable();
                    // Fill the DataTable with data from Excel
                    adapter.Fill(dataTable);
                    return dataTable;
                }

            }
        }

        private void cbxSheets_SelectionChangeCommitted(object sender, EventArgs e)
        {
           dgvList.DataSource=ReadExcelByValue(filePath);
        }
      
       
      
    }
}
