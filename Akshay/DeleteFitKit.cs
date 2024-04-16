using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CsHms.Akshay
{
    public partial class DeleteFitKit : Form
    {
        public DeleteFitKit()
        {
            InitializeComponent();
        }
        CommFuncs mCommFunc = new CommFuncs();
        Global mGlobal = new Global();

        private void txtBillno_Validating(object sender, CancelEventArgs e)
        {
            string strSql = "select opb_name,opb_opno,opb_id from opbill where opb_bno='" + mCommFunc.ConvertToString(txtBillno.Text) + "'";
            DataTable dtBillDet = mGlobal.LocalDBCon.ExecuteQuery(strSql);
            if (dtBillDet != null && dtBillDet.Rows.Count > 0)
            {
                panel1.Visible = true;
                txtName.Text = mCommFunc.ConvertToString(dtBillDet.Rows[0]["opb_name"]);
                txtOpno.Text = mCommFunc.ConvertToString(dtBillDet.Rows[0]["opb_opno"]);
                txtOpno.Tag = mCommFunc.ConvertToString(dtBillDet.Rows[0]["opb_id"]);

            }
            else
                panel1.Visible = false;
        }

        private void DeleteLabResult()
        {
            try
            {
                //Get the current date and time to set to Backup table
                string currentDateTime = DateTime.Now.ToString("ddMMMyyyyHHmmss") +"_"+ txtBillno.Text.ToString();
                //Backing up the labtresult 
                mGlobal.LocalDBCon.ExecuteQuery(@"select * into labtresult_Backup_" + currentDateTime + " from labtresult where lbtrs_refid='" + mCommFunc.ConvertToString(txtOpno.Tag) + "' and lbtrs_itemptr='5'");
                string strSql = "select lbtrs_id from labtresult where lbtrs_refid='" + mCommFunc.ConvertToString(txtOpno.Tag) + "' and lbtrs_itemptr='5'";
                DataTable dtLabtid = mGlobal.LocalDBCon.ExecuteQuery(strSql);
                if (dtLabtid != null && dtLabtid.Rows.Count > 0)
                {      // Backup the row labtresultd corresponding to the lbtrs_id              
                    mGlobal.LocalDBCon.ExecuteQuery(@"select * into labtresultd_Backup_" + currentDateTime + " from labtresultd  where lbtrsd_hdrid='" + mCommFunc.ConvertToString(dtLabtid.Rows[0]["lbtrs_id"]) + "' and lbtrsd_itemptr='5'");
                    strSql = "delete from labtresult where lbtrs_refid='" + mCommFunc.ConvertToString(txtBillno.Tag) + "' and lbtrs_itemptr='5'";
                    strSql += "; delete from labtresultd where lbtrsd_hdrid='" + mCommFunc.ConvertToString(dtLabtid.Rows[0]["lbtrs_id"]) + "' and lbtrsd_itemptr='5'";                    
                    int res = mGlobal.LocalDBCon.ExecuteNonQuery(strSql);
                    if (res > 0)
                    {
                        MessageBox.Show("Deleted");
                        ClearAll();
                    }
                    else
                    {
                        MessageBox.Show("Error occured");
                    }
                }
                else
                { MessageBox.Show("Labresult Not Found"); }

            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message.ToString()); }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearAll();
        }
        private void ClearAll()
        {
            panel1.Visible = false;
            txtBillno.Text = "";
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            DeleteLabResult();
        }
     

       
    }
}