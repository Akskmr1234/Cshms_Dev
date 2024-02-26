using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CsHms.Akshay
{
    public partial class ChangeUserName : Form
    {
        public ChangeUserName()
        {
            InitializeComponent();
        }
        Global mGlobal = new Global();
        DataTable dtBilldet = new DataTable();
        CommFuncs mCommFunc = new CommFuncs();
        private void txtOpno_Validating(object sender, CancelEventArgs e)
        {
            GetOpbill();
        }
        private void GetOpbill()
        {
            try
            {
                string strQry = @"select * from opbill where opb_opno='"+txtOpno.Text+"'";
                dtBilldet = mGlobal.LocalDBCon.ExecuteQuery(strQry);
                if (dtBilldet != null && dtBilldet.Rows.Count > 0)
                {
                    btnSave.Enabled = true;
                }
                else
                {
                    MessageBox.Show("Invalid Op Number");
                    btnSave.Enabled = false;
                }
                
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message.ToString()); }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SubmitData();
        }
        private void SubmitData()
        {
            try
            {
                string strSql = @"select * from prereg where prereg_opno='"+txtOpno.Text+"'";
                DataTable dtPrereg = mGlobal.LocalDBCon.ExecuteQuery(strSql);
                StringBuilder strQueries = new StringBuilder();
                if (mCommFunc.ConvertToString(dtPrereg.Rows[0]["prereg_username"]) == mCommFunc.ConvertToString(dtPrereg.Rows[0]["prereg_email"]))
                {
                    strQueries.Append("update prereg set prereg_username='" + mCommFunc.ConvertToString(txtUsername.Text) + "',prereg_email='" + mCommFunc.ConvertToString(txtUsername.Text) + "' where prereg_opno='" + mCommFunc.ConvertToString(txtOpno.Text) + "'");
                }
                else
                {
                    strQueries.Append("update prereg set prereg_email='" + mCommFunc.ConvertToString(txtUsername.Text) + "' where prereg_opno='" + mCommFunc.ConvertToString(txtOpno.Text) + "'");
                }             
                
                strQueries.Append("update opreg set op_email='" + mCommFunc.ConvertToString(txtUsername.Text) + "' where op_no='" + mCommFunc.ConvertToString(txtOpno.Text) + "'");
                strQueries.Append("update opbill set opb_email='" + mCommFunc.ConvertToString(txtUsername.Text) + "' where opb_opno='" + mCommFunc.ConvertToString(txtOpno.Text) + "'");
                strQueries.Append("update doctorappointment set da_email='" + mCommFunc.ConvertToString(txtUsername.Text) + "' where da_newopno='" + mCommFunc.ConvertToString(txtOpno.Text) + "'");

                int res = mGlobal.LocalDBCon.ExecuteNonQuery(strQueries.ToString());
                if (res > 0)
                { MessageBox.Show("Updated"); }
                else 
                { MessageBox.Show("Error occured"); }
                
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message.ToString()); }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtOpno.Text = "";
            txtUsername.Text = "";            
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();            
        }

    }
}