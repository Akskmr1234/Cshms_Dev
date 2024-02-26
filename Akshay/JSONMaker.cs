using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;



namespace CsHms.Akshay
{
    public partial class JSONMaker : Form
    {
        public JSONMaker()
        {
            InitializeComponent();
            GetJSON();
            string jsonString = @"
                [
                    {""caption"":""Video Urls"",""column_name"":""videoURLs"",""value_type"":""Text"",""default_value"":[""http://commondatastorage.googleapis.com/gtv-videos-bucket/sample/ForBiggerBlazes.mp4"", ""http://commondatastorage.googleapis.com/gtv-videos-bucket/sample/ForBiggerEscapes.mp4"",""http://commondatastorage.googleapis.com/gtv-videos-bucket/sample/ForBiggerFun.mp4""],""value_editable"":""Y"",""remarks"":""Video urls""},
                    {""caption"":""Playback"",""column_name"":""playback"",""value_type"":""Text"",""default_value"":""HDMI"",""value_editable"":""N"",""remarks"":""Playback""},
                    {""caption"":""Port"",""column_name"":""port"",""value_type"":""Text"",""default_value"":""1"",""value_editable"":""N"",""remarks"":""Port""},
                    {""caption"":""Greeting Message"",""column_name"":""greetingMessage"",""value_type"":""Text"",""default_value"":""Hello and welcome"",""value_editable"":""N"",""remarks"":""Greeting and welcome""},
                    {""caption"":""Voice Name"",""column_name"":""voiceName"",""value_type"":""Text"",""default_value"":""en-in-x-enc-network"",""value_editable"":""N"",""remarks"":""Voice name""},
                    {""caption"":""Token call settings"",""column_name"":""tokenCallSettings"",""value_type"":""Text"",""default_value"":[
                        {""pitch"": ""1"",""speechRate"": ""0.7"",""text"": ""Token number:""},
                        {""delay"": 100},
                        {""text"": ""{tokenNo}"",""placeholders"": [{""name"": ""tokenNo"",""tokenPath"": ""$.token_number""}]},
                        {""delay"": 100},
                        {""pitch"": ""1"",""speechRate"": ""0.8"",""text"": ""{counterNo}"",""placeholders"": [{""name"": ""counterNo"",""tokenPath"": ""$.counter.display_text""}]}
                    ],""value_editable"":""N"",""remarks"":""Voice name""}
                ]
            ";

            // Call the recursive method to populate the TreeView
            PopulateTreeView(tviewJson, jsonString, "Root");
        }
        public void Gettype(string Type)
        {
            if (Type == "Layout")
            {
                dgvJson.Visible = false;
                tviewJson.Visible = true;
            }

        }
        Global mGlobal = new Global();
        public String STR_JSON;
        private void GetJSON()
        {
            try
            {
                string strSql = @"select * from template_settings where mode='QMS' and submode='KIOSK'";
                DataTable dtJson = mGlobal.LocalDBCon.ExecuteQuery(strSql);
                string strJson = dtJson.Rows[0]["value"].ToString();
                ConvertJsonStringToDataTable jsontodt = new ConvertJsonStringToDataTable();
                DataTable dtdgvdata = jsontodt.JsonStringToDataTableOtherDetails(strJson);
                dgvJson.DataSource = dtdgvdata;
                dgvJson.Columns[1].Visible = false;
                dgvJson.Columns[2].Visible = false;
                dgvJson.Columns[4].Visible = false;
                dgvJson.Columns[5].Visible = false;
                dgvJson.Columns[0].HeaderText = "Caption";
                dgvJson.Columns[3].HeaderText = "Value";
            }
            catch (Exception ex)
            { }
        }
        private void MakeJson()
        {
            try
            {
                DataTable dtdgv = (DataTable)dgvJson.DataSource;
                Akshay.Class.JsonConvertCls dttojson = new CsHms.Akshay.Class.JsonConvertCls();
                string strJson = dttojson.ConvertDataTableToJson(dtdgv);
                STR_JSON = strJson;
            }
            catch (Exception ex)
            { }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            MakeJson();
            this.Close();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            dgvJson.CurrentRow.Cells["default_value"].Value = txtValue.Text;

        }

        private void dgvJson_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dgvJson.CurrentRow.Cells["value_editable"].Value.ToString() == "Y")
                {
                    txtKey.Text = dgvJson.CurrentRow.Cells["column_name"].Value.ToString();
                    txtValue.Text = dgvJson.CurrentRow.Cells["default_value"].Value.ToString();
                }
                else
                    MessageBox.Show("Not editable");
            }
            catch (Exception ex) { MessageBox.Show(ex.Message.ToString()); }
        }

        private void PopulateTreeView(TreeView treeView, string jsonString, string nodeName)
        {
            TreeNode parentNode = treeView.Nodes.Add(nodeName);

            // Manually parse the JSON structure
            ParseJsonAndPopulateTree(jsonString, parentNode.Nodes);
        }

        private void ParseJsonAndPopulateTree(string jsonString, TreeNodeCollection nodes)
        {
            // Split the JSON string into individual tokens
            string[] tokens = jsonString.Split(new char[] { '{', '}', '[', ']', ',', ':', '"', '\n', '\r', '\t', ' ' }, StringSplitOptions.RemoveEmptyEntries);

            // Populate the TreeView based on the tokens
            for (int i = 0; i < tokens.Length; i++)
            {
                string token = tokens[i];

                if (token == "caption")
                {
                    i++; // Move to the next token (value)
                    nodes.Add(tokens[i]);
                }
                //else if (token == "column_name" || token == "value_type" || token == "value_editable" || token == "remarks")
                //{
                //    i++; // Move to the next token (value)
                //    nodes[nodes.Count - 1].Nodes.Add(tokens[i]);
                //}
                else if (token == "default_value")
                {
                    i++; // Move to the next token (value)

                    // Manually handle the array structure
                    if (tokens[i] == "[")
                    {
                        i++;
                        while (i < tokens.Length && tokens[i] != "]")
                        {
                            nodes[nodes.Count - 1].Nodes.Add(tokens[i]);
                            i++;
                        }
                    }
                    else
                    {
                        nodes[nodes.Count - 1].Nodes.Add(tokens[i]);
                    }
                }
            }
        }
    }
}