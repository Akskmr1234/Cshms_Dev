using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.IO;
using System.Web.Script.Serialization;

namespace CsHms.Akshay
{
    public partial class QRApp : Form
    {
        public QRApp()
        {
            InitializeComponent();
        }
        CommFuncs mCommFunc = new CommFuncs();
        Global mGlobal = new Global();
        private void btnShow_Click(object sender, EventArgs e)
        {
            try
            {
                // URL of the API you want to call
                string apiUrl = "https://dev.nura.in/dev/scr-report-tran/report-login";
                ServicePointManager.SecurityProtocol = (SecurityProtocolType)768 | (SecurityProtocolType)3072;

                // JSON body data
                string jsonBody = "{\"op_no\":\""+mCommFunc.ConvertToString(txtOpno.Text)+"\",\"bill_id\":"+mCommFunc.ConvertToString(txtBillid.Text)+"}";

                WebClient client = new WebClient();
                client.Headers[HttpRequestHeader.UserAgent] = "PostmanRuntime/7.28.3";
                client.Headers.Add("db_ptr", "nuraho");
                client.Headers["content-type"] = "application/json";
                client.Encoding = Encoding.UTF8;

                // Convert JSON body to byte array
                byte[] reqString = Encoding.UTF8.GetBytes(jsonBody);

                // Send the POST request
                byte[] resByte = client.UploadData(apiUrl, "POST", reqString);

                // Convert response byte array to string
                string resString = Encoding.UTF8.GetString(resByte);
                DataTable dtResponse = ParseJsonToDataTable(resString);
              
                dataGridView1.DataSource = dtResponse;

                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
        static DataTable ParseJsonToDataTable(string jsonResponse)
        {
            DataTable dataTable = new DataTable();
            // Add columns to the DataTable
            dataTable.Columns.Add("Key");
            dataTable.Columns.Add("Value");
            dataTable.Columns.Add("Description");
            dataTable.Columns.Add("BindValue");

            // Find the start and end index of the result array
            int startIndex = jsonResponse.IndexOf("[");
            int endIndex = jsonResponse.LastIndexOf("]");
            if (startIndex != -1 && endIndex != -1)
            {
                // Extract the result array substring
                string resultArrayString = jsonResponse.Substring(startIndex, endIndex - startIndex + 1);

                // Split the result array into individual objects
                string[] resultObjects = resultArrayString.Split(new string[] { "{", "}" }, StringSplitOptions.RemoveEmptyEntries);
             
                // Iterate through each result object
                foreach (string resultObject in resultObjects)
                {

                    if (resultObject != "[" && resultObject != "]" && resultObject!=",")
                    {
                        // Split the result object into key-value pairs
                        string[] keyValuePairs = resultObject.Split(new string[] { ":" }, 2, StringSplitOptions.RemoveEmptyEntries);
                        

                        // Add key-value pairs to the DataTable
                        DataRow row = dataTable.NewRow();
                        for (int i = 0; i < keyValuePairs.Length; i ++)
                        {
                            if (i == 0)
                            {
                                string key = keyValuePairs[i].Trim().Trim('"');
                                row["Key"] = key;
                                DataRow drRepmas = GetReportMas(key);
                                row["Description"] = drRepmas["Description"];
                                row["BindValue"] = drRepmas["BindValue"];
                            }
                            else
                            {
                                string value = keyValuePairs[i++].Trim('"');
                                row["Value"] = value;
                            }
                        }
                        dataTable.Rows.Add(row);
                    }
                }
            }

            return dataTable;
        }
        
        static DataRow GetReportMas(string strid)
        {
            try
            {
                string strSql = @"select srepmd_desc as Description,srepmd_bindvalue as BindValue from scrreportmasd where srepmd_id='" + strid + "'";
                Global mGlobal = new Global();
                DataTable dtReportmas = mGlobal.LocalDBCon.ExecuteQuery(strSql);
                if (dtReportmas != null && dtReportmas.Rows.Count > 0)
                {return dtReportmas.Rows[0];}
                else
                { return null; }
                
                
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message.ToString());
            return null;
        }
        }
    }
}

    
