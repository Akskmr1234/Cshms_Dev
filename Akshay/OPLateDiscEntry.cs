using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using System.IO;
namespace CsHms.Akshay
{
    public partial class OPLateDiscEntry : Form
    {
        Global mGlobal = new Global();
        CommFuncs mCommFuncs = new CommFuncs();
        const String FORM_ID = "OPBLATEDISENTRY";
        UsrRights mUsrRight = new UsrRights();//User Rights
        bool mboolAdd = false, mboolEdit = false;//add/edit flags
        String TABLE_NAME = "opbill";// Main Table name
        String PRIMARY_KEY = "opb_bno";// Primary key of the table
        string mstrModule = "";
        string mstrFinYearID = "";
        

        public OPLateDiscEntry()
        {
            InitializeComponent();
            clearAll(true);
            //tlpMain.SetColumnSpan(txtName , 3);
            //tlpMain.SetColumnSpan(txtAdd1, 3);
            //tlpMain.SetColumnSpan(txtAdd2, 3);
            //tlpMain.SetColumnSpan(txtPlace , 3);
            //tlpMain.SetColumnSpan(txtRemarks , 5);
            //tlpMain.SetColumnSpan(tlpAction, 6);
            mUsrRight.FillRights(FORM_ID);
            getCorporates();
            getReferers();
            cbxCorp.SelectedValue = -1;
            cbxreferer.SelectedValue = -1;
            cbxMode.SelectedItem = "OP";
       
        }
        /// <summary>
        /// Fill Bill details for OP,IP,PHM
        /// </summary>
        /// <returns></returns>
        bool  fillBillDetails()
        {
            try
            {
                mstrModule = cbxMode.SelectedItem.ToString();
                string strSql = "";
                txtBillNo.Tag = "";
                if (mstrModule == "OP")
                {
                    strSql = @"select opb_id as BillID, opb_bno AS 'BillNo',convert(varchar,opb_dt,103) as  'BillDt',
                            btyp_desc as 'BillType' ,   opb_opno as 'OPNO',
                            opb_name as 'Name', opb_add1 as 'Add1',opb_add2 as 'Add2',opb_place as 'Place',
                            opb_age as 'Age',opb_gender as 'Gender',opb_amt as 'Amt', (opb_bdisamt -coalesce(opb_latebdisamt,0)) as 'BillDisAmt',(opb_disamt-coalesce(opb_latebdisamt,0) - opb_bdisamt) as 'ItemDisAmt', opb_bdisper as 'BillDisPer',opb_taxamt as 'TaxAmt',opb_paidamt as 'PaidAmt',opb_btype as 'BillTypeCode',
                            opb_rndamt  as 'RoundAmt',coalesce(opb_latebdisamt,0)  as 'BillLateDisAmt',
                            opb_netamt as 'NetAmt',opb_remarks as 'Remarks', opb_canflg as 'CanFlg',opb_referrerptr as 'Referrerptr',opb_corporateptr 'Corpptr'
                             from opbill left join billingtypes on(opb_btype=billingtypes.btyp_code and btyp_module= 'OP') where opb_bno='" + txtBillNo.Text + "'";
                }
                else if (mstrModule == "IP")
                {
                    strSql = @"select ipb_id as BillID, ipb_bno AS 'BillNo',convert(varchar,ipb_dt,103) as  'BillDt',
                    btyp_desc as 'BillType' ,   ipb_opno as 'OPNO',
                    ipb_name as 'Name', ipb_add1 as 'Add1',ipb_add2 as 'Add2',ipb_place as 'Place',
                    ipb_age as 'Age',ipb_gender as 'Gender',ipb_amt as 'Amt', (ipb_bdisamt -coalesce(ipb_latebdisamt,0))
                    as 'BillDisAmt',(ipb_disamt-coalesce(ipb_latebdisamt,0) - ipb_bdisamt) as 'ItemDisAmt', ipb_bdisper as 'BillDisPer',ipb_taxamt
                    as 'TaxAmt',ipb_paidamt as 'PaidAmt',ipb_btype as 'BillTypeCode',
                    ipb_rndamt  as 'RoundAmt',coalesce(ipb_latebdisamt,0)  as 'BillLateDisAmt',
                    ipb_netamt as 'NetAmt',ipb_remarks as 'Remarks', ipb_canflg as 'CanFlg',ipb_referrerptr as 'Referrerptr',ipb_corpptr as 'Corpptr'
                    from ipbill left join billingtypes on(ipb_btype=billingtypes.btyp_code and btyp_module= 'IP')  
                    where ipb_bno='" + txtBillNo.Text + "'";
                }
                else if (mstrModule == "PHM")
                {
                    strSql = @"select s_id as BillID, s_in AS 'BillNo',convert(varchar,s_dt,103) as  'BillDt',
                    btyp_desc as 'BillType' ,   s_patrefno as 'OPNO',
                    s_name as 'Name', op_add1 as 'Add1',op_add2 as 'Add2',op_place as 'Place',
                     convert(varchar,op_age ) as 'Age',op_gender as 'Gender',s_am1 as 'Amt', (s_odis -coalesce(s_latebdisamt,0))
                    as 'BillDisAmt',0 as 'ItemDisAmt',0 as 'BillDisPer',s_taxamt
                    as 'TaxAmt',s_paidamt as 'PaidAmt',s_type as 'BillTypeCode',
                    0  as 'RoundAmt',coalesce(s_latebdisamt,0)  as 'BillLateDisAmt',
                    s_netamt as 'NetAmt',s_remarks as 'Remarks',s_flg as 'CanFlg'
                    from phm_sales left join billingtypes on(s_type=billingtypes.btyp_code and btyp_module= 'PHM') 
                    left join opreg on(op_no=s_patrefno) 
                    where s_in='" + txtBillNo.Text + "' and s_tc='50' and s_type='2'";
                }
                DataTable dtData = mGlobal.LocalDBCon.ExecuteQuery(strSql);
                if (dtData == null)
                {
                    MessageBox.Show("Error occurred while loading data.");
                    return false;
                }
                if (dtData.Rows.Count <= 0)
                {
                    MessageBox.Show("Data not found.");
                    clearAll(true);
                    return false;
                }
                if (mCommFuncs.ConvertToString(dtData.Rows[0]["CanFlg"]) == "Y")
                {
                    MessageBox.Show("Bill already cancelled.");
                    return false;
                }

                if (mstrModule == "PHM")
                {
                    if (mCommFuncs.ConvertToString(dtData.Rows[0]["BillTypeCode"]).Trim() != "2")
                    {
                        DialogResult result = MessageBox.Show("This option only applies to cash bill. Do you want to continue?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                        if (result != DialogResult.Yes)
                        {
                      
                            return false;
                        }
                    }
                }
                else
                {
                    if (mCommFuncs.ConvertToString(dtData.Rows[0]["BillTypeCode"]).Trim() != "1")
                    {
                        DialogResult result = MessageBox.Show("This option only applies to cash bill. Do you want to continue?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                        if (result != DialogResult.Yes)
                        {
                            return false;
                        }
                    }
                }
                mboolEdit = true;
                txtBillNo.Tag = mCommFuncs.ConvertToString(dtData.Rows[0]["BillID"]);
                txtDate.Text = mCommFuncs.ConvertToString(dtData.Rows[0]["BillDt"]);
                txtBIllType.Text = mCommFuncs.ConvertToString(dtData.Rows[0]["BillType"]);
                txtBIllType.Tag = mCommFuncs.ConvertToString(dtData.Rows[0]["BillTypeCode"]);
                txtName.Text = mCommFuncs.ConvertToString(dtData.Rows[0]["Name"]);
                txtOpno.Text = mCommFuncs.ConvertToString(dtData.Rows[0]["OPNO"]);
                txtAdd1.Text = mCommFuncs.ConvertToString(dtData.Rows[0]["Add1"]) +","+ mCommFuncs.ConvertToString(dtData.Rows[0]["Add2"]);
                //txtPlace.Text = mCommFuncs.ConvertToString(dtData.Rows[0]["Place"]);
                txtGender.Text = mCommFuncs.ConvertToString(dtData.Rows[0]["Gender"]);
                txtAge.Text = mCommFuncs.ConvertToString(dtData.Rows[0]["Age"]);
                txtAmt.Text = mCommFuncs.ConvertToNumber_Dec(dtData.Rows[0]["Amt"]).ToString("0.00");
                txtTaxTot.Text = mCommFuncs.ConvertToNumber_Dec(dtData.Rows[0]["TaxAmt"]).ToString("0.00");
                txtRndAmt.Text = mCommFuncs.ConvertToNumber_Dec(dtData.Rows[0]["RoundAmt"]).ToString("0.00");
                txtItemDisAmt.Text = mCommFuncs.ConvertToNumber_Dec(dtData.Rows[0]["ItemDisAmt"]).ToString("0.00");
                txtBillDisPer.Text = mCommFuncs.ConvertToNumber_Dec(dtData.Rows[0]["BillDisPer"]).ToString("0.00");
                txtBillDisAmt.Text = mCommFuncs.ConvertToNumber_Dec(dtData.Rows[0]["BillDisAmt"]).ToString("0.00");
                txtLateDisAmt.Text = mCommFuncs.ConvertToNumber_Dec(dtData.Rows[0]["BillLateDisAmt"]).ToString("0.00");
                txtLateDisAmt.Tag = mCommFuncs.ConvertToNumber_Dec(dtData.Rows[0]["BillLateDisAmt"]);
                txtNetAmt.Text = mCommFuncs.ConvertToNumber_Dec(dtData.Rows[0]["NetAmt"]).ToString("0.00");
                txtPaidAmt.Text = mCommFuncs.ConvertToNumber_Dec(dtData.Rows[0]["PaidAmt"]).ToString("0.00");
                txtNetAmt.Tag = mCommFuncs.ConvertToNumber_Dec(dtData.Rows[0]["NetAmt"]);
                txtRndAmt.Tag = mCommFuncs.ConvertToNumber_Dec(dtData.Rows[0]["RoundAmt"]);
                txtRemarks.Text = mCommFuncs.ConvertToString(dtData.Rows[0]["Remarks"]);
                txtRemarks.Tag = mCommFuncs.ConvertToString(dtData.Rows[0]["Remarks"]);
                if (dtData.Rows[0]["Corpptr"].ToString() != "")
                {
                    cbxCorp.SelectedValue = dtData.Rows[0]["Corpptr"].ToString();
                    DataRowView selectedDataRowView = (DataRowView)cbxCorp.SelectedItem;                    
                    string strCorp = selectedDataRowView["cc_desc"].ToString();
                    cbxCorp.Tag = strCorp;
                }
                else
                    cbxCorp.SelectedValue = -1;
            if (dtData.Rows[0]["Referrerptr"].ToString() != "")
            {
                cbxreferer.SelectedValue = dtData.Rows[0]["Referrerptr"].ToString();
                DataRowView selectedDataRowView = (DataRowView)cbxreferer.SelectedItem;
                string strRefrer = selectedDataRowView["refr_desc"].ToString();
                cbxreferer.Tag = strRefrer;
            }
            else
                 cbxreferer.SelectedValue=-1;
                if (mCommFuncs.ConvertToNumber_Dec(dtData.Rows[0]["BillLateDisAmt"]) > 0)
                {
                    MessageBox.Show("Late discount already updated.");
                    //txtBillNo.Text = "";
                    return false;
                }
                txtLateDisAmt.Enabled = true;
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
            return false;
        }

        void clearAll(bool clearBillNo)
        {
            if(clearBillNo==true)
            txtBillNo.Text="";
            txtDate.Text="";
            txtBIllType.Text="";
            txtBIllType.Tag = "";
            txtName.Text="";
            txtOpno.Text="";
            txtAdd1.Text="";
            txtAge.Text="";                      
            txtGender.Text = "";
            txtAmt.Text="0.00";
            txtTaxTot.Text = "0.00";
            txtRndAmt.Text = "0.00";
            txtItemDisAmt.Text = "0.00";
            txtBillDisPer.Text = "0.00";
            txtBillDisAmt.Text = "0.00";
            txtLateDisAmt.Text = "0.00";
            txtPaidAmt.Text = "0.00";
            txtNetAmt.Text = "0.00";
            txtRemarks.Text = "";
            txtNetAmt.Tag = "";
            cbxCorp.SelectedValue = -1;
            cbxreferer.SelectedValue = -1;
            mboolEdit = false;
            btnSave.Enabled = true;
            txtLateDisAmt.Enabled = true;
            cbxreferer.Tag = "";
            cbxCorp.Tag = "";
            txtRemarks.Tag = "";
            txtLateDisAmt.Tag = "";
        }

        private void txtBillNo_Validating(object sender, CancelEventArgs e)
        {
            clearAll(false);
            txtBillNo.Text = txtBillNo.Text.Trim().ToUpper().Replace("'", "");
            
            if (txtBillNo.Text.Trim() == "") return;
            if (fillBillDetails() == false)
            {
                //btnSave.Enabled = false;
                //e.Cancel = true;
                //return;
            }
            CreditBalance();
        }
        /// <summary>
        /// Validation for bill number,discount amount,remarks
        /// </summary>
        /// <returns></returns>
        private bool isValidData()
        {
            if (txtBillNo.Text == "")
            {
                MessageBox.Show("Bill number required.");
                txtBillNo.Focus();
                return false ;
            }
            //if (mCommFuncs.ConvertToNumber_Dec(txtLateDisAmt.Text) <= 0)
            //{
            //    MessageBox.Show("Late discount amount required.");
            //    txtLateDisAmt.Focus();
            //    return false;
            //}
            if (txtRemarks.Text.Length <= 5)
            {
                MessageBox.Show("Valid remarks required.");
                txtRemarks.Focus();
                return false;
            }
           
            return true;
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            clearAll(true);
            txtBillNo.Focus();
        }
        /// <summary>
        /// Submit discount for ip,op,phm
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                string strSql = "";
                string strRemarks = "";
                if (mboolAdd == false && mboolEdit == false) return;
                if (CreditBalance() == false) return;  
                if (isValidData() == false) return;
                    

                DataTable dtData = new DataTable();
                if (mboolEdit)
                {
                    if (!mUsrRight.HaveRights("E"))
                    {
                        MessageBox.Show(mUsrRight.MessageText);
                        return;
                    }
                    if (mstrModule == "OP")
                    {
                        strSql = "select * from opbill where opb_id='" + txtBillNo.Tag.ToString() + "'";
                    }
                    else if (mstrModule == "IP")
                    {
                        strSql = "select * from ipbill where ipb_id='" + txtBillNo.Tag.ToString() + "'";
                    }
                    else if (mstrModule == "PHM")
                    {
                        strSql = "select * from phm_sales where s_in='" + txtBillNo.Text.ToString() + "' and s_tc='50' and s_type='2'";
                    }
                 
                    dtData = mGlobal.LocalDBCon.ExecuteQuery(strSql, true);
                    if (dtData.Rows.Count <= 0)
                    {
                        MessageBox.Show("Unable to save. Please try again.");
                        clearAll(true);
                        mGlobal.LocalDBCon.RollbackTrans(true);
                        return;
                    }
                }
                #region OP
                if (mstrModule == "OP")
                {
                    dtData.Rows[0]["opb_bdisper"] = (mCommFuncs.ConvertToNumber_Dec(txtLateDisAmt.Text) * 100) / mCommFuncs.ConvertToNumber_Dec(txtAmt.Text);
                    dtData.Rows[0]["opb_disamt"] = mCommFuncs.ConvertToString(txtLateDisAmt.Text);
                    dtData.Rows[0]["opb_bdisamt"] = mCommFuncs.ConvertToString(txtLateDisAmt.Text);
                    dtData.Rows[0]["opb_latebdisamt"] =  mCommFuncs.ConvertToNumber_Dec(txtLateDisAmt.Text);
                    dtData.Rows[0]["opb_rndamt"] = mCommFuncs.ConvertToNumber_Dec(txtRndAmt.Text);
                    dtData.Rows[0]["opb_netamt"] = mCommFuncs.ConvertToNumber_Dec(txtAmt.Text)-mCommFuncs.ConvertToNumber_Dec(txtLateDisAmt.Text);
                    dtData.Rows[0]["opb_corpamt"] = mCommFuncs.ConvertToNumber_Dec(txtAmt.Text) - mCommFuncs.ConvertToNumber_Dec(txtLateDisAmt.Text);
                    dtData.Rows[0]["opb_remarks"] = txtRemarks.Text;
                    if (cbxreferer.SelectedValue.ToString()!="")
                    dtData.Rows[0]["opb_referrerptr"] = mCommFuncs.ConvertToNumber_Dec(cbxreferer.SelectedValue);
                    if (mCommFuncs.ConvertToString(cbxCorp.SelectedValue)!=null)
                    dtData.Rows[0]["opb_corporateptr"] = mCommFuncs.ConvertToString(cbxCorp.SelectedValue);

                    if (txtBIllType.Tag.ToString() == "1")// cash bill 
                    {
                        dtData.Rows[0]["opb_paidamt"] = mCommFuncs.ConvertToNumber_Dec(dtData.Rows[0]["opb_paidamt"]) -
                              mCommFuncs.ConvertToNumber_Dec(txtLateDisAmt.Text);
                    }
                    // Putting Data to Datatable Ends----->
                    mGlobal.LocalDBCon.BeginTrans();
                    if (mGlobal.LocalDBCon.UpdateDataTable_OnTran(strSql, dtData) > 0)
                    {
                        // update bill discout portion in to details table
                        strSql = "select opbd_id,opbd_bdisamt,opbd_total,opbd_disper,opbd_net,opbd_selfpayable from opbilld where opbd_hdrid=" + dtData.Rows[0]["opb_id"].ToString();
                        DataTable dtBillDet = mGlobal.LocalDBCon.ExecuteQuery_OnTran(strSql);
                        decimal decBillDisPortion = 0;
                        decimal decBillDis = 0;
                        decimal decTotal = 0;
                        decimal decTotBillDisPortion = 0;                        
                        decBillDis = mCommFuncs.ConvertToNumber_Dec(dtData.Rows[0]["opb_bdisamt"]);
                        //decTotal = mCommFuncs.ConvertToNumber_Dec(dtBillDet.Rows[intIndexDet]["opb_total"]);
                        if (decBillDis > 0)
                        {
                            for (int intIndexDet = 0; intIndexDet < dtBillDet.Rows.Count; intIndexDet++)
                            {
                                //if (intIndexDet == dtBillDet.Rows.Count - 1)
                                //    dtBillDet.Rows[intIndexDet]["opbd_bdisamt"] = decBillDis - decTotBillDisPortion;
                                //else
                                //{ }
                                    if (mCommFuncs.ConvertToNumber_Dec(dtBillDet.Rows[intIndexDet]["opbd_total"])>0)
                                    {
                                    decTotal = mCommFuncs.ConvertToNumber_Dec(dtBillDet.Rows[intIndexDet]["opbd_total"]);
                                        dtBillDet.Rows[intIndexDet]["opbd_disper"] = (mCommFuncs.ConvertToNumber_Dec(txtLateDisAmt.Text) / decTotal) * 100;
                                        dtBillDet.Rows[intIndexDet]["opbd_bdisamt"] = decTotal * ((mCommFuncs.ConvertToNumber_Dec(txtLateDisAmt.Text) / decTotal) * 100) / 100;
                                        decimal decBilldisnet = mCommFuncs.ConvertToNumber_Dec(mCommFuncs.ConvertToNumber_Dec(dtBillDet.Rows[intIndexDet]["opbd_total"])) - mCommFuncs.ConvertToNumber_Dec(dtBillDet.Rows[intIndexDet]["opbd_bdisamt"]);
                                        dtBillDet.Rows[intIndexDet]["opbd_net"] = decBilldisnet;
                                        dtBillDet.Rows[intIndexDet]["opbd_selfpayable"] = decBilldisnet;
                                    }

                                    
                                    //decBillDisPortion = Decimal.Round((decBillDis / mCommFuncs.ConvertToNumber_Dec(dtData.Rows[0]["opb_amt"])) * decTotal, 2);                                  
                                  
                                
                                decTotBillDisPortion += decBillDisPortion;
                            }
                            if (mGlobal.LocalDBCon.UpdateDataTable_OnTran(strSql, dtBillDet) <= 0)
                            {
                                mGlobal.LocalDBCon.RollbackTrans(true);
                                MessageBox.Show("Updation Failed. Please try again.");
                            }
                        }
                        mGlobal.LocalDBCon.CommitTrans();
                        strRemarks += "Bill no:" + mCommFuncs.ConvertToString(txtBillNo.Text) + ";";
                        
                        DataRowView selectedCorpDataRowView = (DataRowView)cbxCorp.SelectedItem;
                        string StrSelectedCorp = selectedCorpDataRowView["cc_desc"].ToString();
                        DataRowView selectedRefererDataRowView = (DataRowView)cbxreferer.SelectedItem;
                        string StrSelectedReferer = selectedRefererDataRowView["refr_desc"].ToString();
                        if (mCommFuncs.ConvertToString(cbxreferer.Tag) != StrSelectedReferer)
                        {
                            strRemarks += "Referer changed from:" + mCommFuncs.ConvertToString(cbxreferer.Tag) + " to :" + StrSelectedReferer + ";";
                        }
                        if (mCommFuncs.ConvertToString(cbxCorp.Tag) != StrSelectedCorp)
                        {
                            strRemarks += "Corporate changed from:" + mCommFuncs.ConvertToString(cbxCorp.Tag) + " to :" + StrSelectedCorp + ";";
                        }
                        if (mCommFuncs.ConvertToString(txtRemarks.Tag) != mCommFuncs.ConvertToString(txtRemarks.Text))
                        {
                            strRemarks += "Remarks changed from:" + mCommFuncs.ConvertToString(txtRemarks.Tag) + " to :" + mCommFuncs.ConvertToString(txtRemarks.Text) + ";";
                        }
                        if(mCommFuncs.ConvertToNumber_Dec(txtLateDisAmt.Tag)!=mCommFuncs.ConvertToNumber_Dec(txtLateDisAmt.Text))
                        {
                            strRemarks += "Discount amount changed from:"+mCommFuncs.ConvertToNumber_Dec(txtLateDisAmt.Tag)+" to :" + mCommFuncs.ConvertToString(txtLateDisAmt.Text) + ";";
                        }
                        if (mboolAdd) { AuditLog.MasterLog("Add", FORM_ID, "opb_id", strRemarks, dtData, false); }
                        else if (mboolEdit) { AuditLog.MasterLog("Edit", FORM_ID, "opb_id", strRemarks, dtData, false); }
                        clearAll(true);
                        MessageBox.Show("Successfully updated.");
                        cbxMode.Focus();
                    }
                    else
                    {
                        mGlobal.LocalDBCon.RollbackTrans(true);
                        MessageBox.Show("Updation Failed. Please try again.");
                    }
                }
                #endregion
                #region IP
                else if (mstrModule == "IP")
                {
                    dtData.Rows[0]["ipb_disamt"] = (mCommFuncs.ConvertToNumber_Dec(txtBillDisAmt.Text) +
                         mCommFuncs.ConvertToNumber_Dec(txtLateDisAmt.Text) + mCommFuncs.ConvertToNumber_Dec(txtItemDisAmt.Text));
                    dtData.Rows[0]["ipb_bdisamt"] = (mCommFuncs.ConvertToNumber_Dec(txtBillDisAmt.Text) +
                         mCommFuncs.ConvertToNumber_Dec(txtLateDisAmt.Text));
                    dtData.Rows[0]["ipb_latebdisamt"] = mCommFuncs.ConvertToNumber_Dec(dtData.Rows[0]["ipb_latebdisamt"]) + mCommFuncs.ConvertToNumber_Dec(txtLateDisAmt.Text);
                    dtData.Rows[0]["ipb_rndamt"] = mCommFuncs.ConvertToNumber_Dec(txtRndAmt.Text);
                    dtData.Rows[0]["ipb_netamt"] = mCommFuncs.ConvertToNumber_Dec(txtNetAmt.Text);
                    dtData.Rows[0]["ipb_remarks"] = txtRemarks.Text;
                    dtData.Rows[0]["ipb_referrerptr"] = mCommFuncs.ConvertToNumber_Dec(cbxreferer.SelectedValue);
                    dtData.Rows[0]["ipb_corpptr"] = cbxCorp.SelectedValue.ToString();
                    if (txtBIllType.Tag.ToString() == "1")// cash bill 
                    {
                        dtData.Rows[0]["ipb_paidamt"] = mCommFuncs.ConvertToNumber_Dec(dtData.Rows[0]["ipb_paidamt"]) -
                              mCommFuncs.ConvertToNumber_Dec(txtLateDisAmt.Text);
                    }
                    // Putting Data to Datatable Ends----->
                    if (mGlobal.LocalDBCon.UpdateDataTable_OnTran(strSql, dtData) > 0)
                    {
                        // update bill discout portion in to details table
                        strSql = "select ipbd_id,ipbd_bdisamt,ipbd_total from ipbilld where ipbd_hdrid=" + dtData.Rows[0]["ipb_id"].ToString();
                        DataTable dtBillDet = mGlobal.LocalDBCon.ExecuteQuery_OnTran(strSql);
                        decimal decBillDisPortion = 0;
                        decimal decBillDis = 0;
                        decimal decTotal = 0;
                        decimal decTotBillDisPortion = 0;
                        decBillDis = mCommFuncs.ConvertToNumber_Dec(dtData.Rows[0]["ipb_bdisamt"]);
                        //decTotal = mCommFuncs.ConvertToNumber_Dec(dtBillDet.Rows[intIndexDet]["opb_total"]);
                        if (decBillDis > 0)
                        {
                            for (int intIndexDet = 0; intIndexDet < dtBillDet.Rows.Count; intIndexDet++)
                            {
                                if (intIndexDet == dtBillDet.Rows.Count - 1)
                                    dtBillDet.Rows[intIndexDet]["ipbd_bdisamt"] = decBillDis - decTotBillDisPortion;
                                else
                                {
                                    decTotal = mCommFuncs.ConvertToNumber_Dec(dtBillDet.Rows[intIndexDet]["ipbd_total"]);
                                    decBillDisPortion = 0;
                                    decBillDisPortion = Decimal.Round((decBillDis / mCommFuncs.ConvertToNumber_Dec(dtData.Rows[0]["ipb_amt"])) * decTotal, 2);

                                    dtBillDet.Rows[intIndexDet]["ipbd_bdisamt"] = decBillDisPortion;
                                }
                                decTotBillDisPortion += decBillDisPortion;
                            }
                            if (mGlobal.LocalDBCon.UpdateDataTable_OnTran(strSql, dtBillDet) <= 0)
                            {
                                mGlobal.LocalDBCon.RollbackTrans(true);
                                MessageBox.Show("Updation Failed. Please try again.");
                            }
                        }
                        mGlobal.LocalDBCon.CommitTrans();

                        if (mboolAdd) { AuditLog.MasterLog("Add", FORM_ID, "ipbd_id", "", dtData, false); }
                        else if (mboolEdit) { AuditLog.MasterLog("Edit", FORM_ID, "ipbd_id", "", dtData, false); }
                        clearAll(true);
                        MessageBox.Show("Late discount successfully updated.");
                        txtBillNo.Focus();
                    }
                    else
                    {
                        mGlobal.LocalDBCon.RollbackTrans(true);
                        MessageBox.Show("Updation Failed. Please try again.");
                    }
                }
                #endregion
                #region Pharmacy
                else if (mstrModule == "PHM")
                {
                    decimal decCurrentLateDisIntPer = 0;
                    decimal decTotDiscount = 0;
                    decimal decCurrentLateDiscount = 0;
                    decimal decTotLateCurrentDiscount = 0;

                    decCurrentLateDisIntPer = Math.Round((mCommFuncs.ConvertToNumber_Dec(txtLateDisAmt.Text) /
                       mCommFuncs.ConvertToNumber_Dec(txtNetAmt.Tag)) * 100, 2);

                    string strSqlDetail = "select * from phm_tran where in_tc='50' and in_typ='2' and in_no='" + txtBillNo.Text + "'";
                    DataTable dtDetails = mGlobal.LocalDBCon.ExecuteQuery_OnTran(strSqlDetail);
                    foreach (DataRow dr in dtDetails.Rows)
                    {
                        decCurrentLateDiscount = 0;
                        decCurrentLateDiscount = Math.Round((((mCommFuncs.ConvertToNumber_Dec(dr["in_mrp"]) *
                                (mCommFuncs.ConvertToNumber_Dec(dr["in_qty"]) / mCommFuncs.ConvertToNumber_Dec(dr["in_un"]))) -
                                mCommFuncs.ConvertToNumber_Dec(dr["in_dis"]))
                                * decCurrentLateDisIntPer) / 100, 2);

                        //if (mCommFuncs.ConvertToString(dr["in_salestaxinclusive"]) == "Y")
                        //{
                        //    decCurrentLateDiscount = Math.Round((((mCommFuncs.ConvertToNumber_Dec(dr["in_mrp"]) *
                        //        (mCommFuncs.ConvertToNumber_Dec(dr["in_qty"]) / mCommFuncs.ConvertToNumber_Dec(dr["in_un"]))) -
                        //        mCommFuncs.ConvertToNumber_Dec(dr["in_dis"]))
                        //        * decCurrentLateDisIntPer) / 100, 2);
                        //}
                        //else
                        //{
                        //    decCurrentLateDiscount = Math.Round(((((mCommFuncs.ConvertToNumber_Dec(dr["in_mrp"]) *
                        //        mCommFuncs.ConvertToNumber_Dec(dr["in_qty"])) +
                        //        mCommFuncs.ConvertToNumber_Dec(dr["in_tps"])) -
                        //         mCommFuncs.ConvertToNumber_Dec(dr["in_dis"]))
                        //        * decCurrentLateDisIntPer) / 100, 2);

                        //}

                        decTotLateCurrentDiscount += decCurrentLateDiscount;
                        dr["in_disper"] = mCommFuncs.ConvertToNumber_Dec(dr["in_disper"]) + decCurrentLateDisIntPer;
                        dr["in_dis"] = mCommFuncs.ConvertToNumber_Dec(dr["in_dis"]) + decCurrentLateDiscount;
                        dr["in_profit"] = mCommFuncs.ConvertToNumber_Dec(dr["in_profit"]) - decCurrentLateDiscount;
                    }
                    txtLateDisAmt.Text = decTotLateCurrentDiscount.ToString("#########0.#0");
                    //dtData.Rows[0]["s_odis"] = (mCommFuncs.ConvertToNumber_Dec(txtBillDisAmt.Text) +
                    //     mCommFuncs.ConvertToNumber_Dec(txtLateDisAmt.Text) + mCommFuncs.ConvertToNumber_Dec(txtItemDisAmt.Text));
                    dtData.Rows[0]["s_odis"] = (mCommFuncs.ConvertToNumber_Dec(txtBillDisAmt.Text) +
                         mCommFuncs.ConvertToNumber_Dec(txtLateDisAmt.Text));
                    dtData.Rows[0]["S_latebdisamt"] = mCommFuncs.ConvertToNumber_Dec(txtLateDisAmt.Text);
                    dtData.Rows[0]["s_nt"] = mCommFuncs.ConvertToNumber_Dec(dtData.Rows[0]["s_nt"]) + (mCommFuncs.ConvertToNumber_Dec(txtNetAmt.Text) - (mCommFuncs.ConvertToNumber_Dec(txtNetAmt.Tag) - mCommFuncs.ConvertToNumber_Dec(txtLateDisAmt.Text))); //mCommFuncs.ConvertToNumber_Dec(txtRndAmt.Text);
                    dtData.Rows[0]["S_netamt"] = mCommFuncs.ConvertToNumber_Dec(txtNetAmt.Text);
                    dtData.Rows[0]["S_remarks"] = txtRemarks.Text;
                    dtData.Rows[0]["s_profit"] =
                        mCommFuncs.ConvertToNumber_Dec(dtData.Rows[0]["s_profit"]) - mCommFuncs.ConvertToNumber_Dec(txtLateDisAmt.Text);

                    // Putting Data to Datatable Ends----->
                    if (mGlobal.LocalDBCon.UpdateDataTable_OnTran(strSql, dtData) > 0)
                    {
                        ////// update bill discout portion in to details table
                        ////strSql = "select s_id,s_odis,s_netamt from phm_sales where s_id=" + dtData.Rows[0]["s_id"].ToString();
                        ////DataTable dtBillDet = mGlobal.LocalDBCon.ExecuteQuery(strSql, true);
                        ////decimal decBillDisPortion = 0;
                        ////decimal decBillDis = 0;
                        ////decimal decTotal = 0;
                        ////decimal decTotBillDisPortion = 0;
                        ////decBillDis = mCommFuncs.ConvertToNumber_Dec(dtData.Rows[0]["s_odis"]);
                        //////decTotal = mCommFuncs.ConvertToNumber_Dec(dtBillDet.Rows[intIndexDet]["opb_total"]);
                        ////if (decBillDis > 0)
                        ////{

                        ////}

                        if (mGlobal.LocalDBCon.UpdateDataTable_OnTran(strSqlDetail, dtDetails) > 0)
                        {
                            mGlobal.LocalDBCon.CommitTrans();
                        }
                        else
                        {
                            mGlobal.LocalDBCon.RollbackTrans(true);
                            MessageBox.Show("Updation Failed. Please try again.");
                        }

                        if (mboolAdd) { AuditLog.MasterLog("Add", FORM_ID, PRIMARY_KEY, "", dtData, false); }
                        else if (mboolEdit) { AuditLog.MasterLog("Edit", FORM_ID, PRIMARY_KEY, "", dtData, false); }
                        clearAll(true);
                        MessageBox.Show("Late discount successfully updated.");
                        txtBillNo.Focus();
                    }
                    else
                    {
                        mGlobal.LocalDBCon.RollbackTrans(true);
                        MessageBox.Show("Updation Failed. Please try again.");
                    }
                }
                #endregion
            }
            catch (Exception ex)
            {
                mGlobal.LocalDBCon.RollbackTrans(true);
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// Validating discount amount 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtLateDisAmt_Validating(object sender, CancelEventArgs e)
        {
            txtNetAmt.Text = mCommFuncs.ConvertToNumber_Dec(txtNetAmt.Tag).ToString("0.00");
            txtRndAmt.Text = mCommFuncs.ConvertToNumber_Dec(txtRndAmt.Tag).ToString("0.00");
            if ( txtLateDisAmt.Text.Trim() == "") return;

            try
            {
                decimal decTemp = decimal.Parse(txtLateDisAmt.Text);
                if (decTemp < 0)
                {
                    MessageBox.Show("Not a valid discount amount");
                    e.Cancel = true;
                    return;
                }
                else if (decTemp == 0)
                    return;               

                txtLateDisAmt.Text = decTemp.ToString("0.00");
                txtNetAmt.Text =( mCommFuncs.ConvertToNumber_Dec(mCommFuncs.ConvertToNumber_Dec(txtNetAmt.Tag)+
                  mCommFuncs.ConvertToNumber_Dec(txtRndAmt.Tag)) - decTemp).ToString("0.00");
                decimal decTempDisAmt = 0;
                decimal decRndamt = 0;
                decimal decAmt =  mCommFuncs.ConvertToNumber_Dec(txtNetAmt.Text);               
                decAmt = mCommFuncs.ConvertToNumber_Dec(txtNetAmt.Text);
                decTempDisAmt = Math.Round(decAmt, MidpointRounding.AwayFromZero);
                decRndamt = decTempDisAmt - decAmt;
                decAmt = decTempDisAmt;
                txtNetAmt.Text = decAmt.ToString("0.00");
                txtRndAmt.Text = decRndamt.ToString("0.00");

                if (txtBIllType.Tag.ToString() == "0")
                {
                    if (mCommFuncs.ConvertToNumber_Dec(txtNetAmt.Text) < mCommFuncs.ConvertToNumber_Dec(txtPaidAmt.Text))
                    {
                        MessageBox.Show("Not a valid discount amount.Bill Net Amount less than paid amount.");
                        txtLateDisAmt.Focus();
                        e.Cancel = false;
                        return ;
                    }
                }
            }
            catch
            {
                MessageBox.Show("Not a valid  data");
                e.Cancel = true;
                return;
            }
        }

        private void OPLateDiscEntry_Load(object sender, EventArgs e)
        {
            txtBillNo.Focus();
        }
        /// <summary>
        /// Disable corporate and referer for pharmacy
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbxMode_SelectedValueChanged(object sender, EventArgs e)
        {
            txtBillNo.Enabled = true;
            if (cbxMode.SelectedItem == "PHM")
            {
                cbxreferer.Visible = false;
                cbxCorp.Visible = false;
                lblCorp.Visible = false;
                lblreferer.Visible = false;
                pnlfooter.Location = new Point(pnlfooter.Location.X, 99);
            }
            else if (cbxMode.SelectedItem == "IP")
            {
                cbxreferer.Visible = true;
                cbxCorp.Visible = true;
                lblCorp.Visible = true;
                lblreferer.Visible = true;
                pnlfooter.Location = new Point(pnlfooter.Location.X, 149);
            }
            else
            {
                cbxreferer.Visible = true;
                cbxCorp.Visible = true;
                lblCorp.Visible = true;
                lblreferer.Visible = true;
                pnlfooter.Location = new Point(pnlfooter.Location.X, 149);
                
            }
        }
        /// <summary>
        /// Get corporate into comboboxes
        /// </summary>
        private void getCorporates()
        {
            try
            {
                              
                string strsql = @"select cc_desc,cc_code from customer";
                DataTable dtCorporates = mGlobal.LocalDBCon.ExecuteQuery(strsql);
                if (dtCorporates == null)
                {
                    MessageBox.Show("Error Occured While Loading Data");
                }
                if (dtCorporates.Rows.Count <= 0)
                {
                    MessageBox.Show("Data Not Found");
                }
                cbxCorp.DataSource = dtCorporates;
                cbxCorp.DisplayMember = "cc_desc";
                cbxCorp.ValueMember = "cc_code";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
                writeErrorLog(ex, "getCorporates");
            }
        }
         /// <summary>
         /// Get Referers into comboboxes
         /// </summary>
         private void getReferers()
        {
            try
            {
                
                string strsql = @"select refr_code,refr_desc from referrer";
                DataTable dtRefererf = mGlobal.LocalDBCon.ExecuteQuery(strsql);
                if (dtRefererf == null)
                {
                    MessageBox.Show("Error Occured While Loading Data");
                }
                if (dtRefererf.Rows.Count <= 0)
                {
                    MessageBox.Show("Data Not Found");
                }
                cbxreferer.DataSource = dtRefererf;
                cbxreferer.DisplayMember = "refr_desc";
                cbxreferer.ValueMember = "refr_code";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
                writeErrorLog(ex, "getReferers");
            }
        }
        public static void writeErrorLog(Exception exception, string strErrorSource)
        {
            // Retrieve the path to the error log file from the application configuration.
            string strlogfile = String.Empty;
            strlogfile = ConfigurationSettings.AppSettings["ErrorLog_path"].ToString();

            try
            {
                // Check if the log file exists; if not, create it.
                if (!File.Exists(strlogfile))
                {
                    File.CreateText(strlogfile);
                }
                // Construct the error log entry.
                string strErrorText = "TMR_Error_Log=> An Error occurred: " + strErrorSource + " ON:" + DateTime.Now;
                strErrorText += Environment.NewLine + exception.Message + Environment.NewLine + exception.StackTrace;
                // Append the error log entry to the log file.
                using (System.IO.FileStream aFile = new System.IO.FileStream(strlogfile, System.IO.FileMode.Append, System.IO.FileAccess.Write))
                using (System.IO.StreamWriter sw = new System.IO.StreamWriter(aFile))
                {
                    sw.WriteLine("**********************************************************************");
                    sw.WriteLine(strErrorText);
                    sw.WriteLine("**********************************************************************");
                }
            }
            catch (Exception ex)
            {
                // If an error occurs during logging, re-throw the exception.
                writeErrorLog(ex, "writeErrorLog");
                throw;
            }
        }

        private void btnExit_Click_1(object sender, EventArgs e)
        {
            Close();
        }
        //checking of credit balance
        private bool CreditBalance()
        {
            try
            {
               
                decimal balance = mCommFuncs.ConvertToNumber_Dec(txtNetAmt.Text)-mCommFuncs.ConvertToNumber_Dec(txtPaidAmt.Text);
                if (mCommFuncs.ConvertToNumber_Dec(txtNetAmt.Text) == mCommFuncs.ConvertToNumber_Dec(txtPaidAmt.Text) && mCommFuncs.ConvertToNumber_Dec(txtPaidAmt.Text) > 0 && mCommFuncs.ConvertToNumber_Dec(txtNetAmt.Text)>0)
                {
                    MessageBox.Show("Bill  already paid ");
                    txtLateDisAmt.Enabled = false;
                    return true;
                }
                else if (balance <= mCommFuncs.ConvertToNumber_Dec(txtLateDisAmt.Text) && balance>0)
                {
                    MessageBox.Show("Enter Late discount less than '" + (mCommFuncs.ConvertToNumber_Dec(txtNetAmt.Text) - mCommFuncs.ConvertToNumber_Dec(txtPaidAmt.Text)).ToString() + "'\n Already Paid '" + txtPaidAmt.Text.ToString() + "'");
                    txtLateDisAmt.Enabled = true;
                    return false;
                }
                else
                {
                    txtLateDisAmt.Enabled = true;
                    return true;
                }

            }
            catch(Exception ex)
            {
               
                return false;
            }
            return false;
        }

        private void txtLateDisAmt_Validating_1(object sender, CancelEventArgs e)
        {
            if (CreditBalance() == false)
            {
               
                txtLateDisAmt.Text = "";

            }
        }

      
       
       
    }

}