using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CsHms.Akshay
{
    public partial class LateCouponApply : Form
    {
        public LateCouponApply()
        {
            InitializeComponent();
        }
        CommFuncs mCommFunc = new CommFuncs();
        Global mGLobal = new Global();
        DataTable dtCouponDetails = new DataTable();
        DataTable dtBillDetails = new DataTable();
        private void btnSave_Click(object sender, EventArgs e)
        {
            SubmitData();

        }
        private void SubmitData()
        {
            try
            {
                if (CouponValid() == true)
                {
                    StringBuilder strQueries = new StringBuilder();
                    
                    string strQry = @"insert into campgndiscountcouponstran (cmgdcpnt_dttm,cmgdcpnt_campgndiscountptr,cmgdcpnt_couponnumber,cmgdcpnt_module,cmgdcpnt_moduletranid) values ('" + Convert.ToDateTime(dtBillDetails.Rows[0]["opb_tm"]).ToString("yyyy-MM-dd HH:mm:ss") + "','" + mCommFunc.ConvertToString(dtCouponDetails.Rows[0]["cmgdcm_id"]) + "','" + mCommFunc.ConvertToString(dtCouponDetails.Rows[0]["cmgdcpn_couponnumber"]) + "','" + mCommFunc.ConvertToString(dtCouponDetails.Rows[0]["cmgdcpn_module"]) + "','" + mCommFunc.ConvertToString(dtCouponDetails.Rows[0]["cmgdcpn_moduletranid"]) + "')";
                    strQueries.Append(strQry);
                    strQry = @"update opbill set opb_remarks='DiscountCoupon:" + mCommFunc.ConvertToString(dtCouponDetails.Rows[0]["cmgdcpn_couponnumber"]) + ":" + mCommFunc.ConvertToString(dtCouponDetails.Rows[0]["cmgdcpn_id"]) + "' where opb_bno='"+mCommFunc.ConvertToString(txtBillNumber.Text)+"'";
                    strQueries.Append(strQry);
                    strQry = @"update campgndiscountcoupons set cmgdcpn_usedstatus=case when cmgdcpn_availablenos=cmgdcpn_usednos+1 then 'U' else 'P' end,cmgdcpn_useddt='" + Convert.ToDateTime(dtBillDetails.Rows[0]["opb_dt"]).ToString("yyyy-MM-dd HH:mm:ss")+ "',cmgdcpn_useddttm='" + Convert.ToDateTime(dtBillDetails.Rows[0]["opb_tm"]).ToString("yyyy-MM-dd HH:mm:ss")+ "',cmgdcpn_remarks='opbill;" + mCommFunc.ConvertToString(dtBillDetails.Rows[0]["opb_bno"]) + "',cmgdcpn_usednos=cmgdcpn_usednos+1 where cmgdcpn_couponnumber='" + mCommFunc.ConvertToString(txtCouponCode.Text) + "'";
                    strQueries.Append(strQry);
                    int res = mGLobal.LocalDBCon.ExecuteNonQuery(strQueries.ToString());
                    if (res > 0)
                        MessageBox.Show("Coupon Applied");
                    else
                        MessageBox.Show("Error Occured");

                }
                else
                {
                    MessageBox.Show("Coupon is not valid");
                }
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message.ToString()); }
        }
        private bool CouponValid()
        {
            try
            {
                string strQry = @"select * from campgndiscountmas left join campgndiscountcoupons on cmgdcm_id=cmgdcpn_campgndiscountmasptr where cmgdcpn_couponnumber='"+mCommFunc.ConvertToString(txtCouponCode.Text)+"'";
                DataTable dtCouponDetails = mGLobal.LocalDBCon.ExecuteQuery(strQry);
                if (dtCouponDetails != null && dtCouponDetails.Rows.Count > 0)
                {
                    DateTime dtnow = DateTime.Now;
                    DateTime dtToDate=Convert.ToDateTime(dtCouponDetails.Rows[0]["cmgdcm_todttm"]) ;
                    if (dtToDate.Date < dtnow.Date || mCommFunc.ConvertToInt(dtCouponDetails.Rows[0]["cmgdcpn_availablenos"]) == mCommFunc.ConvertToInt(dtCouponDetails.Rows[0]["cmgdcpn_usednos"]) || mCommFunc.ConvertToString(dtCouponDetails.Rows[0]["cmgdcpn_active"])=="N")
                    { return false; }
                    else
                    { return true; }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
                return false;
            }
            return false;
        }
        private void btnClear_Click(object sender, EventArgs e)
        {
            pnlCouponDetail.Visible = false;
            txtCouponCode.Text = "";
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtCouponCode_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                string strSql = @"select * from campgndiscountmas left join campgndiscountcoupons on cmgdcm_id=cmgdcpn_campgndiscountmasptr where cmgdcpn_couponnumber='"+mCommFunc.ConvertToString(txtCouponCode.Text)+"'";
                dtCouponDetails = mGLobal.LocalDBCon.ExecuteQuery(strSql);
                if (dtCouponDetails != null && dtCouponDetails.Rows.Count > 0)
                {
                    pnlCouponDetail.Visible = true;
                    lblMode.Text = lblMode.Text + mCommFunc.ConvertToString(dtCouponDetails.Rows[0]["cmgdcm_mode"]);
                    lblRemark.Text = lblRemark.Text + mCommFunc.ConvertToString(dtCouponDetails.Rows[0]["cmgdcm_remarks"]);
                    lblFromDate.Text = lblFromDate.Text + mCommFunc.ConvertToString(dtCouponDetails.Rows[0]["cmgdcm_fromdttm"]);
                    lblToDate.Text = lblToDate.Text + mCommFunc.ConvertToString(dtCouponDetails.Rows[0]["cmgdcm_todttm"]);
                }
                else
                {
                    pnlCouponDetail.Visible = false;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void GetBillDetails()
        {
            try
            {
                string strSql = @"select * from opbill where opb_bno='" + mCommFunc.ConvertToString(txtBillNumber.Text)+ "'";
                dtBillDetails = mGLobal.LocalDBCon.ExecuteQuery(strSql);
                if (dtBillDetails != null && dtBillDetails.Rows.Count > 0)
                {

                }
                
            }
            catch (Exception ex)
            { }
        }

        private void txtBillNumber_Validating(object sender, CancelEventArgs e)
        {
            GetBillDetails();
        }
             
    }
}