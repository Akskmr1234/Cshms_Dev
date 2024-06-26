using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;

namespace CsHms.Akshay
{
    public partial class TableCreator : Form
    {
        public TableCreator()
        {
            InitializeComponent();
        }
        string StrFilepath = "";
        Global mGlobal = new Global();
        private void btnBrowse_Click(object sender, EventArgs e)
        {
            Waitfrm WaitForm = new Waitfrm();
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Excel Files|*.xls;*.xlsx";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string filePath = openFileDialog.FileName;
                    StrFilepath = filePath;
                    DataTable dataTable = ReadExcel(filePath);
                    if (dataTable != null && dataTable.Rows.Count > 0)
                    {
                        dgvData.DataSource = dataTable;

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
                string query = "SELECT * FROM [" + sheets.Rows[0]["TABLE_NAME"].ToString()+ "]  ";
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
        private DataTable ReadExcelByValue(string filePath)
        {
            // Connection string for Excel
            string connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filePath + ";Extended Properties='Excel 12.0 Xml;HDR=YES;ReadOnly=1';";
            // Create connection
            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                // Open connection
                connection.Open();

                
                
                string query = "SELECT * FROM [" + cbxSheets.SelectedValue + "]  ";
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

        private void btnSave_Click(object sender, EventArgs e)
        {
            CreateTable();
        }

        private void cbxSheets_SelectionChangeCommitted(object sender, EventArgs e)
        {
            dgvData.DataSource=ReadExcelByValue(StrFilepath);
        }

        private void CreateTable()
        {
            try
            {
                StringBuilder StrQueries =new StringBuilder();
                DataTable dtTableDetails = (DataTable)(dgvData.DataSource);
                string strQueryBuilder = "CREATE TABLE " + dtTableDetails.Rows[0]["TABLE_NAME"].ToString() + " (";
                StrQueries.Append(strQueryBuilder);
                for (int i = 0; i < dtTableDetails.Rows.Count; i++)
                {
                    string columnName = dtTableDetails.Rows[i]["COLUMN_NAME"].ToString();
                    string dataType = dtTableDetails.Rows[i]["DATA_TYPE"].ToString();
                    string charMaxLen = dtTableDetails.Rows[i]["CHARACTER_MAXIMUM_LENGTH"].ToString();
                    string columnDefault = dtTableDetails.Rows[i]["COLUMN_DEFAULT"].ToString();
                    string isNullable = dtTableDetails.Rows[i]["IS_NULLABLE"].ToString();
                    string Identity = dtTableDetails.Rows[i]["IDENTITY"].ToString();
                     strQueryBuilder = columnName + " " + dataType+" ";                    
                     if (!string.IsNullOrEmpty(charMaxLen) && charMaxLen !="NULL" && charMaxLen!="")
                    {
                        strQueryBuilder += " (" + charMaxLen + ") ";
                    }
                    if (!string.IsNullOrEmpty(Identity) && Identity == "YES" && Identity != "")
                    {
                        strQueryBuilder += "IDENTITY(1,1) ";
                    }
                    if (!string.IsNullOrEmpty(columnDefault) && columnDefault != "NULL" && columnDefault != "")
                    {
                        strQueryBuilder += " default " + columnDefault+",";
                    }
                    else if (isNullable=="YES")
                        strQueryBuilder +=  "null"+",";
                    else if (isNullable == "NO")
                        strQueryBuilder+="not null"+",";
                    StrQueries.Append(strQueryBuilder); 
                }
                strQueryBuilder = ")";
                StrQueries.Append(strQueryBuilder);
                mGlobal.LocalDBCon.ExecuteQuery(StrQueries.ToString());
                MessageBox.Show("Table Created");
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message.ToString()); }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            dgvData.DataSource = null;
            cbxSheets.DataSource = null;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}