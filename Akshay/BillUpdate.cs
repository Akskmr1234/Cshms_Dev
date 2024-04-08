using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CsHms.Akshay
{
    public partial class BillUpdate : Form
    {
        public BillUpdate()
        {
            InitializeComponent();
            GetBillNos();
        }
        Global mGloblal = new Global();
        CommFuncs mCommFunc = new CommFuncs();
        private void GetBillNos()
        {
            try
            {
                string strSql = @"select * from billnos";
                DataTable dtBillnos = mGloblal.LocalDBCon.ExecuteQuery(strSql);
                if (dtBillnos != null && dtBillnos.Rows.Count > 0)
                {
                    dgvBIllnos.DataSource = dtBillnos;
                }
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message.ToString()); }
        }
        private void SaveBillDetails()
        {
            try
            {
                DataTable dtBillDetail=(DataTable)(dgvBIllnos.DataSource);
                

                foreach(DataRow dr in dtBillDetail.Rows)
                {
                    string strSql = "";
                    if (CheckAlreadyExist(mCommFunc.ConvertToString(dr["blno_code"])))
                    {
                         strSql = "update billnos set blno_no='" + mCommFunc.ConvertToString(dr["blno_no"]) + "',blno_prefix='" + mCommFunc.ConvertToString(dr["blno_prefix"]) + "',blno_postfix='" + mCommFunc.ConvertToString(dr["blno_postfix"]) + "',blno_locked='"+mCommFunc.ConvertToString(dr["blno_locked"])+"' where blno_code='" + mCommFunc.ConvertToString(dr["blno_code"]) + "'";
                    }
                    else
                        strSql="insert into billnos (blno_code,blno_no,blno_prefix,blno_postfix,blno_locked) values ('" + mCommFunc.ConvertToString(dr["blno_code"]) + "','" + mCommFunc.ConvertToString(dr["blno_no"]) + "','" + mCommFunc.ConvertToString(dr["blno_prefix"]) + "','" + mCommFunc.ConvertToString(dr["blno_postfix"]) + "','"+mCommFunc.ConvertToString(dr["blno_locked"])+"')";
                    mGloblal.LocalDBCon.ExecuteQuery(strSql);

                }
                MessageBox.Show("Updated");
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message.ToString()); }
        }

        private bool CheckAlreadyExist(string strBlcode)
        {
            try
            {
                string strSql = @"select * from billnos where blno_code='"+strBlcode+"'";
                DataTable dtExist = mGloblal.LocalDBCon.ExecuteQuery(strSql);
                if (dtExist != null && dtExist.Rows.Count > 0)
                {
                    return true;
                }
                else
                    return false;
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message.ToString());
            return false;
        }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            BackupTable();
            SaveBillDetails();
        }

        private void BackupTable()
        {
            try
            {
                string strDate = DateTime.Now.ToString("ddMMMMyyyy_HH_mm_ss");
                string strSql = "select * into billnos_Backup"+strDate+" from billnos";
                mGloblal.LocalDBCon.ExecuteQuery(strSql);
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message.ToString()); }
        }
        private void btnClear_Click(object sender, EventArgs e)
        {
            dgvBIllnos.DataSource = null;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}