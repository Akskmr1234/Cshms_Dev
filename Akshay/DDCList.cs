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


namespace CsHms.Akshay
{
    public partial class DDCList : Form
    {
        public DDCList()
        {
            InitializeComponent();
            ListType();
        }
        Global mGlobal = new Global();
        CommFuncs mCommFunc = new CommFuncs();
        private void ListType()
        {
            try
            {
                DataTable dtTypes = new DataTable();
                dtTypes.Columns.Add("ListValue");
                dtTypes.Columns.Add("ListType");
                dtTypes.Rows.Add("DDC", "DDC List");
                dtTypes.Rows.Add("PNAME", "Change Product Name");
                dtTypes.Rows.Add("", "");
                cbxType.DataSource = dtTypes;
                cbxType.DisplayMember = "ListType";
                cbxType.ValueMember = "ListValue";

            }
            catch (Exception ex)
            { writeErrorLog(ex, ""); }
        }
        private void btnFile_Click(object sender, EventArgs e)
        {
            
            Waitfrm WaitForm = new Waitfrm();
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Excel Files|*.xls;*.xlsx";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string filePath = openFileDialog.FileName;
                    DataTable dataTable = ReadExcel(filePath);
                    if (dataTable != null && dataTable.Rows.Count > 0)
                    {
                        dgvDdcList.DataSource = dataTable;
                
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
                string query = "SELECT * FROM [Table Export$]  ";

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
            Close();
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
                DataTable dtDgvData = ((DataTable)dgvDdcList.DataSource);
                progressBar.Minimum = 0;
                progressBar.Maximum = dtDgvData.Rows.Count;               
                mGlobal.LocalDBCon.BeginTrans();
                StringBuilder batchQuery = new StringBuilder();

                for (int i = 0; i < dtDgvData.Rows.Count; i++)
                {
                    string strstatus = mCommFunc.ConvertToString(dtDgvData.Rows[i]["STATUS"]);
                    string strStrength = mCommFunc.ConvertToString(dtDgvData.Rows[i]["INGREDIENT_STRENGTH"]);                    
                    string strPackage = mCommFunc.ConvertToString(dtDgvData.Rows[i]["DOSAGE_FORM_PACKAGE"]);
                    strStrength = strStrength.Replace("'", "~");
                    strPackage = strPackage.Replace("'", "~");


                    DateTime discontinuedDate;
                    SetProgressBar(i);
                    
                    
                    if (strstatus.StartsWith("Discontinued on") && DateTime.TryParse(strstatus.Substring(16), out discontinuedDate))
                    {
                        if (!CheckALreadyExist(mCommFunc.ConvertToString(dtDgvData.Rows[i]["DDC_CODE"])))
                        {
                            writeDDCcode(mCommFunc.ConvertToString(dtDgvData.Rows[i]["DDC_CODE"]));
                        }
                        else
                        {
                            strSql = @"update phm_product_temp set pm_discontinuedon='" + discontinuedDate.ToString("yyyy.MM.dd") + "',pm_packing='"+strPackage+"',pm_strength='"+strStrength+"' where pm_cod='" + mCommFunc.ConvertToString(dtDgvData.Rows[i]["DDC_CODE"]) + "'";
                        }
                    }
                    else if (strstatus.Contains("(to be discontinued on"))
                    {
                        DateTime graceDate = ExtractDate(strstatus);

                        if (!CheckALreadyExist(mCommFunc.ConvertToString(dtDgvData.Rows[i]["DDC_CODE"])))
                        {
                            writeDDCcode(mCommFunc.ConvertToString(dtDgvData.Rows[i]["DDC_CODE"]));
                        }
                        else
                        {
                            strSql = @"update phm_product_temp set pm_discontinuedon='" + graceDate.ToString("yyyy.MM.dd") + "',pm_strength='"+strStrength+"',pm_packing='"+strPackage+"' where pm_cod='" + mCommFunc.ConvertToString(dtDgvData.Rows[i]["DDC_CODE"]) + "'";
                        }
                    }
                    else if (strstatus.Contains("Active"))
                    {
                        if (!CheckALreadyExist(mCommFunc.ConvertToString(dtDgvData.Rows[i]["DDC_CODE"])))
                        {
                            writeDDCcode(mCommFunc.ConvertToString(dtDgvData.Rows[i]["DDC_CODE"]));
                        }
                        else
                        {
                            strSql = @"update phm_product_temp set pm_active='Y',pm_strength='"+strStrength+"',pm_packing='"+strPackage+"'  where pm_cod='" + mCommFunc.ConvertToString(dtDgvData.Rows[i]["DDC_CODE"]) + "'";
                        }
                    }

                    // Append the current query to the batch query
                    batchQuery.AppendLine(strSql);

                    // If the batch size is reached or it's the last iteration, execute the batch
                    if ((i + 1) % BatchSize == 0 || (dtDgvData.Rows.Count - 1) == i)
                    {
                        int batchRes = mGlobal.LocalDBCon.ExecuteNonQuery_OnTran(batchQuery.ToString());

                        // Reset the batch query for the next set of queries
                        batchQuery = new StringBuilder();

                        // Handle batch execution result
                        if (batchRes < 1)
                        {
                            MessageBox.Show("Error in batch execution");
                            mGlobal.LocalDBCon.RollbackTrans();
                            return;
                        }
                    }

                    if ((dtDgvData.Rows.Count) - 1 == i)
                    {

                        strSql = @"update phm_product_temp set pm_active= case when cast(pm_discontinuedon as date)<=cast(getdate() as date) then 'N' else 'Y' end where pm_discontinuedon is not null";
                        int finalBatchRes = mGlobal.LocalDBCon.ExecuteNonQuery_OnTran(strSql);

                        if (finalBatchRes < 1)
                        {
                            MessageBox.Show("Error in final batch execution");
                            mGlobal.LocalDBCon.RollbackTrans();
                        }
                        else
                        {
                            mGlobal.LocalDBCon.CommitTrans();
                            MessageBox.Show("Success");
                        }
                    }
                }


                //strSql = @"update phm_product_temp set pm_active= case when pm_discontinuedon<getdate() then 'N' else 'Y' end where pm_discontinuedon is not null";



            }
            catch (Exception ex)
            {
                writeErrorLog(ex, "SubmitData");
                MessageBox.Show(ex.Message.ToString());
                mGlobal.LocalDBCon.RollbackTrans();
            }
            finally
            { progressBar.Value = 0; }
        }

        static DateTime ExtractDate(string input)
        {
            // Find the position of the date substring
            int startIndex = input.IndexOf("on ") + 3;
            int endIndex = input.IndexOf(")", startIndex);

            // Extract the date substring
            string dateSubstring = input.Substring(startIndex, endIndex - startIndex);

            // Parse the date string into a DateTime object
            DateTime date;
            if (DateTime.TryParseExact(dateSubstring, "yyyy.MM.dd", null, System.Globalization.DateTimeStyles.None, out date))
            {
                return date;
            }
            else
            {
                // Handle parsing error
                throw new ArgumentException("Invalid date format in the input string");
            }
        }
        private bool CheckALreadyExist(string strpmcode)
        {
            try
            {
                string strSql=@"select * from phm_product_temp where pm_cod='"+strpmcode+"'";
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
            {
                progressBar.Value = intValue;
                
               
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message.ToString()); }
        }
        static List<Dictionary<string, string>> DeserializeJson(string jsonString)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            return serializer.Deserialize<List<Dictionary<string, string>>>(jsonString);
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
        public static void writeDDCcode(string strDdccode)
        {
            // Retrieve the path to the error log file from the application configuration.
            string strlogfile = String.Empty;
            strlogfile = System.Configuration.ConfigurationSettings.AppSettings["DDC_CODE_path"].ToString();

            try
            {
                // Check if the log file exists; if not, create it.
                if (!File.Exists(strlogfile))
                {
                    File.CreateText(strlogfile);
                }
                // Construct the error log entry.
                string strErrorText = "CODE: " + strDdccode + " ON:" + DateTime.Now;
                
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
