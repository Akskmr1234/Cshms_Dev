using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Diagnostics;

namespace CsHms.Akshay
{
    public partial class PredictiveAI : Form
    {
        public PredictiveAI()
        {
            InitializeComponent();
            FillGrid();
        }
        Global mGlobal = new Global();
        CommFuncs mCommFunc = new CommFuncs();
        private string strMode="NPA";
        private string strbillid="1";

        private DataTable GetCoreScripts()
        {
            try
            {
                string strSql = "select script,other_details1,caption from core_scripts where mode='" + strMode + "'";
                DataTable dtCoreScripts = mGlobal.LocalDBCon.ExecuteQuery(strSql);
                if (dtCoreScripts != null && dtCoreScripts.Rows.Count > 0)
                {
                    return dtCoreScripts;
                }
                else
                    return null;
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message.ToString());

            }
            return null;
        }
        private void FillGrid()
        {
            try
            {
            DataTable dtCoreScripts = GetCoreScripts();
            DataTable dtLinkDetails = new DataTable();          
            dtLinkDetails.Columns.Add("Caption");
            dtLinkDetails.Columns.Add("Link");
            DataRow drLinkdetails = dtLinkDetails.NewRow();
            if (dtCoreScripts.Rows.Count > 0)
            {
                foreach (DataRow dr in dtCoreScripts.Rows)
                {
                    string strJsonLink = mCommFunc.ConvertToString(dr["other_details1"]);
                    string strScript = mCommFunc.ConvertToString(dr["script"]);
                    strScript=strScript.Replace("@billid;",strbillid);
                    string url = ExtractValue(strJsonLink, "base_url");
                    string strReplacedUrl = ReplaceUrl(strScript, url);
                    string caption = mCommFunc.ConvertToString(dr["caption"]);
                    drLinkdetails["Link"] = strReplacedUrl;
                    drLinkdetails["Caption"] = caption;
                    dtLinkDetails.Rows.Add(drLinkdetails); 
                }
                dataGridView1.DataSource = dtLinkDetails;
                DataGridViewButtonColumn ViewButtonColumn = new DataGridViewButtonColumn();
                ViewButtonColumn.Name = "View";
                ViewButtonColumn.Text = "View";
                ViewButtonColumn.UseColumnTextForButtonValue=true;
                int columnIndex = 2;
                if (dataGridView1.Columns["View"] == null)
                {
                    dataGridView1.Columns.Insert(columnIndex, ViewButtonColumn);
                }
                dataGridView1.Columns[1].Visible = false;
            }
            }
            catch(Exception ex)
            {MessageBox.Show(ex.Message.ToString());}
            
        }
        private string ReplaceUrl(string strQry, string strLink)
        {
            try
            {
                DataTable dtValues = mGlobal.LocalDBCon.ExecuteQuery(strQry);
                if (dtValues.Rows.Count > 0)
                {

                    strLink = strLink.Replace("@Gender;", mCommFunc.ConvertToString(dtValues.Rows[0]["Gender"]));
                    strLink= strLink.Replace("@Age;",mCommFunc.ConvertToString(dtValues.Rows[0]["Age"]));
                    strLink= strLink.Replace("@CtaValue;",mCommFunc.ConvertToString(dtValues.Rows[0]["ct"]));
                    strLink= strLink.Replace("@vfoValue;",mCommFunc.ConvertToString(dtValues.Rows[0]["vfo"]));
                    strLink= strLink.Replace("@hbaValue;",mCommFunc.ConvertToString(dtValues.Rows[0]["hba1c"]));
                    strLink= strLink.Replace("@bpValue;",mCommFunc.ConvertToString(dtValues.Rows[0]["bp"]));
                    return strLink;
                }
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message.ToString()); }
            return null;
        }
        static string ExtractValue(string json, string key)
    {
        // Create a regular expression pattern to match the key and its value
        string pattern = "\""+key+"\":\"(.*?)\"";

        // Match the pattern in the JSON string
        Match match = Regex.Match(json, pattern);

        // If a match is found, return the captured group (the value)
        if (match.Success)
        {
            return match.Groups[1].Value;
        }
        else
        {
            // Return null if no match is found
            return null;
        }
    }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataTable dtLinks = (DataTable)(dataGridView1.DataSource);
            string url = dtLinks.Rows[e.RowIndex]["Link"].ToString();
            Process.Start(url);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

      
    }
}