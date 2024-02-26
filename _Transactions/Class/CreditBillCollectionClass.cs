using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Windows.Forms;
using System.Drawing;
namespace CsHms
{
    class CreditBillCollectionClass
    {
        CommFuncs clsCom = new CommFuncs();
        Global mGlobal = new Global();
        string mstrTableName = "phm_creditbillcollection";

        public  DataTable loadPendingBillDetails(bool boolSundryBill,string strBillNo)
        {
            try
            {
                 
                string strSql = "";
                if (boolSundryBill == false)
                {
                    strSql = "select max(s_id) as BillId, max(s_in) as BillNo ,max(s_dt) as BillDate," +
                       " max(cu_code) as Code,max(cu_name) as CusName, (case when max(s_tc)='41' then sum(s_netamt)*-1 " +
                       " else sum(s_netamt) end) as BillAmt , " +
                       " PaidAmt=(select sum(ccb_paidamt)  from  phm_creditcollectionbilldetails  " +
                       " where s_id=ccb_billid and ccb_flg<>'*') from phm_sales,phm_cusmas  " +
                       " where s_cuscode=cu_code and  s_tc in('50','41')  and s_type='1'  and s_flg<>'*' and s_paid<>'Y'  " +
                       " and s_in='" + strBillNo + "'";
                }
                else
                {
                    strSql = "select max(s_id) as BillId, max(s_in) as BillNo ,max(s_dt) as BillDate," +
                    " max(s_patrefno) as Code,max(s_name) as CusName, (case when max(s_tc)='41' then sum(s_netamt)*-1 else sum(s_netamt) end) as BillAmt , " +
                    " PaidAmt=(select sum(ccb_paidamt)  from  phm_creditcollectionbilldetails  " +
                    " where s_id=ccb_billid and ccb_flg<>'*') from phm_sales " +
                    " where   s_tc in('50','41') and s_type='3'  and s_flg<>'*' and s_paid<>'Y'  " +
                    " and s_in='" + strBillNo + "'";
                }

                    strSql += " group by s_id having  (sum(s_netamt) - (select isnull(sum(ccb_paidamt), 0) from " +
                    " phm_creditcollectionbilldetails where s_id=ccb_billid and ccb_flg<>'*') )>0 order by s_id";

                   


                 return  mGlobal.LocalDBCon.ExecuteQuery(strSql);
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
            return null;

        }

        public bool loadPendingBillDetails(DateTime dateFrom, DateTime dateTo, string strCusCode,bool boolSundryBill,
            string strBillDeptCode,DataGridView dgvBill)
        {
            try
            {
                dgvBill.Rows.Clear();
                string strSql = "";
                if (boolSundryBill == false)
                    strSql = "select max(s_id) as BillId, max(s_in) as BillNo ,max(s_dt) as BillDate," +
                       " max(cu_code) as Code,max(cu_name) as CusName, (case when max(s_tc)='41' then sum(s_netamt)*-1 " +
                       " else sum(s_netamt) end) as BillAmt , " +
                       " PaidAmt=(select sum(ccb_paidamt)  from  phm_creditcollectionbilldetails  " +
                       " where s_id=ccb_billid and ccb_flg<>'*') from phm_sales,phm_cusmas  " +
                       " where s_cuscode=cu_code and  s_tc in('50','41')  and s_type='1'  and s_flg<>'*' and s_paid<>'Y'  " +
                       " and s_dt>= " + clsCom.FormatDBServDateWithTime(dateFrom.ToShortDateString(), "AM") +
                       " and s_dt<= " + clsCom.FormatDBServDateWithTime(dateTo.ToShortDateString(), "PM");
                else
                    strSql = "select max(s_id) as BillId, max(s_in) as BillNo ,max(s_dt) as BillDate," +
                    " max(s_patrefno) as Code,max(s_name) as CusName, (case when max(s_tc)='41' then sum(s_netamt)*-1 else sum(s_netamt) end) as BillAmt , " +
                    " PaidAmt=(select sum(ccb_paidamt)  from  phm_creditcollectionbilldetails  " +
                    " where s_id=ccb_billid and ccb_flg<>'*') from phm_sales " +
                    " where   s_tc in('50','41') and s_type='3'  and s_flg<>'*' and s_paid<>'Y'  " +
                    " and s_dt>= " + clsCom.FormatDBServDateWithTime(dateFrom.ToShortDateString(), "AM") +
                    " and s_dt<= " + clsCom.FormatDBServDateWithTime(dateTo.ToShortDateString(), "PM");
                 
                 if (strCusCode != "")
                 {
                     if (boolSundryBill == false)
                         strSql += " and s_cuscode ='" + strCusCode + "'";
                     else
                        strSql += " and s_patrefno='" + strCusCode + "'";
                 }
                 if (strBillDeptCode != "")
                     strSql += " and s_billingdeptptr='" + strBillDeptCode + "'";

                 strSql += " group by s_id having  (sum(s_netamt) - (select isnull(sum(ccb_paidamt), 0) from " +
                     " phm_creditcollectionbilldetails where s_id=ccb_billid and ccb_flg<>'*') )>0 order by s_id";

                DataTable dtData = mGlobal.LocalDBCon.ExecuteQuery(strSql);
                if (dtData.Rows.Count <= 0)
                {
                    return false ;
                }
                else
                {
                    int intRowCnt=0;
                    foreach (DataRow dr in dtData.Rows)
                    {
                        dgvBill.Rows.Add();
                       
                        dgvBill.Rows[dgvBill.Rows.Count - 1].Cells["billno"].Value = dr["BillNo"].ToString();
                        dgvBill.Rows[dgvBill.Rows.Count - 1].Cells["billdate"].Value = clsCom.FormatDate(
                            dr["BillDate"].ToString(), "dd/MM/yyyy").ToShortDateString();
                        dgvBill.Rows[dgvBill.Rows.Count - 1].Cells["Code"].Value = dr["Code"].ToString();
                        dgvBill.Rows[dgvBill.Rows.Count - 1].Cells["cusname"].Value = dr["CusName"].ToString();
                        dgvBill.Rows[dgvBill.Rows.Count - 1].Cells["billamt"].Value = clsCom.ConvertToNumber_Double(
                        dr["BillAmt"].ToString()).ToString("0.00");
                        dgvBill.Rows[dgvBill.Rows.Count - 1].Cells["totpaid"].Value = clsCom.ConvertToNumber_Double(
                        dr["PaidAmt"].ToString()).ToString("0.00");
                        dgvBill.Rows[dgvBill.Rows.Count - 1].Cells["billid"].Value = dr["BillId"].ToString();
                        if (clsCom.ConvertToNumber_Double(
                        dr["BillAmt"].ToString()) < 0)
                        {
                            dgvBill.Rows[intRowCnt].DefaultCellStyle.BackColor = Color.LightGray;
                        }
                        intRowCnt+= 1;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
            return true;

        }
        public String getVouNo_MaxNumber()
        {            
            Decimal decRet = 1;
            decRet = decRet + clsCom.ConvertToNumber_Dec(mGlobal.LocalDBCon.ExecuteQuery("select max(cc_vou) from " + mstrTableName).Rows[0][0]);
            return (decRet.ToString());
        }

        public bool savePaymentDetails(string strVouno,DateTime trnDate, int intBillMode, string strRemarks, 
            DataGridView dgvPaymentDetails,double dblDifSalesandReturn,double dblSelectedSalesAmt)
        {
             
            DataTable dtData = mGlobal.LocalDBCon.ExecuteQuery("select * from phm_creditbillcollection where 1=2");
            if (dblDifSalesandReturn == 0)//if sales amt and return amt is same
            {
                dtData.Rows.Add(dtData.NewRow());//for contra entry
                dtData.Rows.Add(dtData.NewRow());
            }
            else
            {
                foreach (DataGridViewRow dgvRow in dgvPaymentDetails.Rows)
                    dtData.Rows.Add(dtData.NewRow());
            }

            int intCnt = 0;
            foreach (DataRow dr in dtData.Rows)
            {
                dr["cc_vou"] = clsCom.ConvertToNumber_Dec(strVouno);
                dr["cc_dt"] = trnDate.Date;
                dr["cc_billmode"] = intBillMode==0?3:1;  //3= credit 1=sundry
                if (dblDifSalesandReturn == 0)
                {

                    dr["cc_paidamt"] = intCnt == 0 ? dblSelectedSalesAmt : dblSelectedSalesAmt * -1;
                    dr["cc_paymentmode"] = "0";
                    dr["cc_paymentmodedesc"] = "Cash";
                    dr["cc_instrumentno"] = "";
                    dr["cc_holdername"] = "";
                    dr["cc_bankname"] = "";
                    dr["cc_cardname"] = "";
                    dr["cc_issuedby"] = "";
                    dr["cc_expirydt"] = "";
                    dr["cc_paymentremarks"] = "";
                }
                else
                {
                    if (dblDifSalesandReturn > 0)
                        dr["cc_paidamt"] = clsCom.ConvertToNumber_Dec(dgvPaymentDetails.Rows[intCnt].Cells["amt"].Value);
                    else
                        dr["cc_paidamt"] = clsCom.ConvertToNumber_Dec(dgvPaymentDetails.Rows[intCnt].Cells["amt"].Value) * -1;

                    dr["cc_paymentmode"] = clsCom.ConvertToInt(dgvPaymentDetails.Rows[intCnt].Cells["paymode"].Value);
                    dr["cc_paymentmodedesc"] = dgvPaymentDetails.Rows[intCnt].Cells["instname"].Value.ToString();
                    dr["cc_instrumentno"] = clsCom.ConvertToString(dgvPaymentDetails.Rows[intCnt].Cells["instno"].Value);
                    dr["cc_holdername"] = clsCom.ConvertToString(dgvPaymentDetails.Rows[intCnt].Cells["hname"].Value);
                    dr["cc_bankname"] = clsCom.ConvertToString(dgvPaymentDetails.Rows[intCnt].Cells["bname"].Value);
                    dr["cc_cardname"] = clsCom.ConvertToString(dgvPaymentDetails.Rows[intCnt].Cells["cardname"].Value);
                    dr["cc_issuedby"] = clsCom.ConvertToString(dgvPaymentDetails.Rows[intCnt].Cells["bname"].Value);
                    dr["cc_expirydt"] = clsCom.ConvertToString(dgvPaymentDetails.Rows[intCnt].Cells["expdate"].Value);
                    if (dgvPaymentDetails.Rows[intCnt].Cells["chqdate"].Value != null)
                        dr["cc_chqdt"] = clsCom.ConvertToString(dgvPaymentDetails.Rows[intCnt].Cells["chqdate"].Value);
                    dr["cc_paymentremarks"] = clsCom.ConvertToString(dgvPaymentDetails.Rows[intCnt].Cells["remarks"].Value);
                }
                dr["cc_remarks"] = strRemarks;
                dr["cc_flg"] = "";
                dr["cc_user"] = mGlobal.CurrentUser;
                /*vipin dr["cc_userid"] = mGlobal.CurrentUserID;
                 dr["cc_time"] = mGlobal.ServerTime;
                 dr["cc_shiftno"] = mGlobal.CurrentShiftNo;
                 dr["cc_shiftdt"] = mGlobal.CurrentShiftDate;
                 dr["cc_finyearid"] = mGlobal.CurrentShiftNo;*/

                intCnt += 1;
            }
            if (mGlobal.LocalDBCon.UpdateDataTable("select * from phm_creditbillcollection where 1=2", dtData) > 0)
            {
                return true ;
            }
            else
                MessageBox.Show("Updation Failed. Please try again.");
            return false;
        }

        bool updateSalesPaidStatus(string strStatus ,string strSalesId)
        {
            try
            {
                if (mGlobal.LocalDBCon.ExecuteNonQuery("update phm_sales set s_paid='" + strStatus +
                      "' where s_id=" + strSalesId) > 0)
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
            return false;
        }

        public bool saveBillDetails(string strVouno, DateTime trnDate, int intBillMode, DataGridView dgvBillDetails,
            double dblDueAmt)
        {

            DataTable dtData = mGlobal.LocalDBCon.ExecuteQuery("select * from phm_creditcollectionbilldetails where 1=2");
            foreach (DataGridViewRow dgvRow in dgvBillDetails.Rows)
            {
                if ((bool)dgvRow.Cells["select"].Value == true &&
                    clsCom.ConvertToNumber_Dec(dgvRow.Cells["paidamt"].Value)!= 0)
                {
                    /********* Set data to Data Table */
                    dtData.Rows.Add(dtData.NewRow());
                    dtData.Rows[dtData.Rows.Count-1]["ccb_vou"] = clsCom.ConvertToNumber_Dec(strVouno);
                    dtData.Rows[dtData.Rows.Count - 1]["ccb_dt"] = trnDate.Date;
                    dtData.Rows[dtData.Rows.Count - 1]["ccb_billmode"] = intBillMode == 0 ? 3 : 1;  //3= credit 1=sundry
                    dtData.Rows[dtData.Rows.Count - 1]["ccb_billid"] = clsCom.ConvertToString(dgvRow.Cells["billid"].Value);
                    dtData.Rows[dtData.Rows.Count - 1]["ccb_billno"] = clsCom.ConvertToString(dgvRow.Cells["billno"].Value);
                    dtData.Rows[dtData.Rows.Count - 1]["ccb_cuscode"] = clsCom.ConvertToString(dgvRow.Cells["code"].Value);
                    dtData.Rows[dtData.Rows.Count - 1]["ccb_cusname"] = clsCom.ConvertToString(dgvRow.Cells["cusname"].Value);
                    if (clsCom.ConvertToNumber_Double(dgvRow.Cells["billamt"].Value)> 0)
                     dtData.Rows[dtData.Rows.Count - 1]["ccb_paidamt"] = clsCom.ConvertToNumber_Dec(
                         dgvRow.Cells["paidamt"].Value);
                    else
                         dtData.Rows[dtData.Rows.Count - 1]["ccb_paidamt"] = clsCom.ConvertToNumber_Dec(
                                 dgvRow.Cells["paidamt"].Value)*-1;

                    dtData.Rows[dtData.Rows.Count - 1]["ccb_flg"] = "";
                    /*********/
                    string strStatus = "";
                    if (Convert.ToDouble(dgvRow.Cells["balamt"].Value) == 0)
                        strStatus = "Y";
                    else
                        strStatus = "P";

                    if (updateSalesPaidStatus(strStatus, dgvRow.Cells["billid"].Value.ToString()) == false)
                    {
                        MessageBox.Show("Sales Paid Status Updation Failed");
                        return false;
                    }
                }
            }           
            if (mGlobal.LocalDBCon.UpdateDataTable("select * from phm_creditcollectionbilldetails where 1=2", dtData) > 0)
            {
                return true;
            }
            else
                MessageBox.Show("Updation Failed. Please try again.");
            return false;
        }

        public bool cancelVoucher(string strVoucher, string strRemarks)
        {
            try
            {
                if (MessageBox.Show("Are You Sure To Cancel This Vouhcer ", "Confirm", MessageBoxButtons.YesNo)
                       == DialogResult.Yes)
                {
                    string strUpdateQry = "";
                  /*vipin  string strUpdateQry = " update  phm_creditbillcollection set cc_flg='*',cc_canuser='" +
                        mGlobal.CurrentUser + "',cc_canuserid='" + mGlobal.CurrentUserID + "', cc_cantime=" + clsCom.FormatDBServDateWithTime(
                        DateTime.Now.ToString()) + ", " + " cc_canremarks='" + strRemarks + "' where  cc_vou=" + strVoucher;*/
                    if (mGlobal.LocalDBCon.ExecuteNonQuery(strUpdateQry) == -1)
                    {
                        MessageBox.Show("Cancellation Failed . Please Try Again.... ");
                        return false;
                    }
                    strUpdateQry = " update phm_creditcollectionbilldetails   set ccb_flg='*' " +
                        " where  ccb_vou=" + strVoucher;
                    if (mGlobal.LocalDBCon.ExecuteNonQuery(strUpdateQry) == -1)
                    {
                        MessageBox.Show("Cancellation Failed . Please Try Again.....");
                        return false;
                    }
                    MessageBox.Show("Cancellation Successfully Done...!");
                    return true;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        public DataTable checkVoucherForcancel (string strVouNo)
        {
            try
            {
                DataTable dtData = mGlobal.LocalDBCon.ExecuteQuery("select max(cc_dt) as VouDate," +
                    " sum(cc_paidamt) as Amt ,max(cc_flg) as cc_flg  from phm_creditbillcollection " +
                    " where cc_vou=" + strVouNo);
                if (dtData.Rows.Count <= 0)
                {
                    MessageBox.Show("Vouhcer Not Found ");
                    return null;
                }
                if (clsCom.ConvertToString(dtData.Rows[0]["cc_flg"]) == "*")
                {
                    MessageBox.Show("This Vouhcer Already Cancelled");
                    return null;
                }
                return dtData;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return  null ;
            }
            return null;
        }

        public bool updateSalesStatusBasedOnCcVouNo(string strVouno)
        {
            try
            {
                DataTable dtData = mGlobal.LocalDBCon.ExecuteQuery("select ccb_billid from phm_creditcollectionbilldetails " +
                    " where  ccb_vou=" + strVouno);
                if (dtData.Rows.Count <= 0)
                {
                    MessageBox.Show("Voucher Not Exist in Credit Collection Bill Details");
                    return false;

                }
                foreach (DataRow dr in dtData.Rows)
                {
                    this.updateSalesPaidStatus("P", dr["ccb_billid"].ToString());
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
            return false;
        }
    }
}
