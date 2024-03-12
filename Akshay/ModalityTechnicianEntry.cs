using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace CsHms.Akshay
{
    public partial class ModalityTechnicianEntry : Form
    {
        public ModalityTechnicianEntry()
        {
            InitializeComponent();
        }
        Global mGlobal = new Global();
        CommFuncs mCommFunc = new CommFuncs();
        private void txtAccessionno_Validating(object sender, CancelEventArgs e)
        {
            GetBillDetails();
        }

        private void GetBillDetails()
        {
            try
            {
                string strSql = @"select * from modalitypatientstatustran left join opbill on opb_id=mpst_refid where mpst_accessionno='"+mCommFunc.ConvertToString(txtAccessionno.Text)+"'";
                DataTable dtBilldetails = mGlobal.LocalDBCon.ExecuteQuery(strSql);
                if (dtBilldetails != null && dtBilldetails.Rows.Count > 0)
                {
                    pnlBillDetails.Enabled = true;
                    txtName.Text = mCommFunc.ConvertToString(dtBilldetails.Rows[0]["opb_name"]);
                    txtOpNo.Text = mCommFunc.ConvertToString(dtBilldetails.Rows[0]["opb_opno"]);
                    txtGender.Text = mCommFunc.ConvertToString(dtBilldetails.Rows[0]["opb_age"]);
                    txtAge.Text = mCommFunc.ConvertToString(dtBilldetails.Rows[0]["opb_gender"]);
                    txtAccessionno.Tag = mCommFunc.ConvertToString(dtBilldetails.Rows[0]["mpst_itemptr"]);
                    dgvData.DataSource = GetXmlDetails(mCommFunc.ConvertToString(dtBilldetails.Rows[0]["mpst_itemptr"]));
                }
                else
                {
                    pnlBillDetails.Enabled = false;
                    dgvData.DataSource = null;
                }
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message.ToString()); }
        }
        private DataTable GetXmlDetails(string strItemptr)
        {
            try
            {
                string strSql = @"select xmltm_xmltemplate from xmltemplatemas where xmltm_mapvalue='"+strItemptr+"'";
                DataTable dtxmldetails = mGlobal.LocalDBCon.ExecuteQuery(strSql);
                if (dtxmldetails != null && dtxmldetails.Rows.Count > 0)
                {
                    DataTable dtparsedxml = ParseXMLToDataTable(mCommFunc.ConvertToString(dtxmldetails.Rows[0]["xmltm_xmltemplate"]));
                    return dtparsedxml;
                }
            }
            catch (Exception ex)
            { }
            return null;
        }
        private DataTable ParseXMLToDataTable(string xmlString)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Code", typeof(string));
            dt.Columns.Add("Description", typeof(string));
            dt.Columns.Add("Value", typeof(string));

            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xmlString);

            foreach (XmlNode node in doc.SelectNodes("/root/data"))
            {
                DataRow dr = dt.NewRow();
                dr["Code"] = node.SelectSingleNode("code").InnerText;
                dr["Description"] = node.SelectSingleNode("desc").InnerText;
                dr["Value"] = node.SelectSingleNode("value").InnerText;

                dt.Rows.Add(dr);
            }

            return dt;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearAll();
        }
        
        private void ClearAll()
        {
            try
            {
                txtName.Text = "";
                txtOpNo.Text = "";
                txtGender.Text = "";
                txtAccessionno.Text = "";
                txtAge.Text = "";
                pnlBillDetails.Enabled = false;
                dgvData.DataSource = null;
            }
            catch (Exception ex)
            { }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

      
      
    }
}