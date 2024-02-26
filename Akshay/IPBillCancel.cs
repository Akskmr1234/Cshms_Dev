using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using System.IO;

namespace CsHms.IP
{
    public partial class IPBillCancel : Form
    {
        Global mGlobal = new Global();
        CommFuncs mCommFuncs = new CommFuncs();
        const String FORM_ID = "IPBILLCAN";// Unique id for this form         
        UsrRights mUsrRight = new UsrRights();//User Rights
        String mstrPHMCollectionMode = "PHM";
        String mstrIPCollectionMode = "PHM";
        public IPBillCancel()
        {
            InitializeComponent();

        }
        /// <summary>
        //set datas into text box
        /// </summary>
        private bool getHeader(string strIPno)
        {
            try
            {
                // SQL query to retrieve bill header information based on the provided IP number.
                String sqlstr = @" SELECT ipb_id as 'BillId', s_in as 'PharmacyBillNo',
             btyp_caption as 'BIllType',op_gender as 'Gender',ipb_btype as 'BillTypeCode',ipb_bno as 'BillNO' ,convert(varchar, ipb_dt,103) as 'BillDate'
            ,ipb_tm as 'BillTime',ipb_opno as 'OPno' ,ipb_ipno as 'IPno',ipb_name as 'PatientName'
            ,ipb_add1 as 'Add1',ipb_add2 as 'Add2',ipb_place as 'Place',ipb_gender as 'Gender'
            ,ipb_age as 'Age' ,ipb_patcatptr as 'PatCatCode',ptc_desc as 'PatientCategory',do_name as 'Doctor',ipb_amt as 'BillAmt'
            ,ipb_taxmainamt as 'TaxMainAmt',ipb_taxcessamt as 'CessAmt',ipb_taxamt as 'TaxAmt'
            ,ipb_bdisper as 'BillDisPer',ipb_bdisamt as 'BillDisAmt',ipb_disamt as 'DisAmt'
            ,ipb_rndamt  as 'RoundedAmt',ipb_netamt as'NetAmt' ,ipb_advamt as 'AdvAmt',ipb_corpamt as 'CorpAmt'
            ,cc_desc as 'CorporateDesc',ipb_corpamt as 'CorporateAmt',ipb_patamt as 'PatientPaidAmt'
            ,ipb_patpayableamt as 'PatientPayableAmt' ,itm_desc  as 'PackageDesc',ipb_remarks as 'Remarks'
            ,ipb_paidflg as 'PaidFlg',ipb_paidamt as 'PaidAmt',ipb_refundflg as 'RefundFlg'
            ,ipb_refundamt as 'RefundAmt',ipb_canflg as Cnaflg,CONVERT(varchar, ia_dt,103)as AdmitDt,
            CONVERT(varchar, ia_dischargeddt,103)as DisDt ,ipb_netamt as 'NetAmt' 
            FROM  ipbill   left join patientcategory on(ptc_code=ipb_patcatptr)
            left join customer on(cc_code=ipb_corpptr)  left join item on(itm_code=ipb_packageptr)
            left join billingtypes on(btyp_code =ipb_btype and btyp_module ='IP')
            left join ipadmit on(ia_ipno=ipb_ipno)
            left join opreg on(ia_opno=op_no)
            left join phm_sales on(s_ipno=ia_ipno)
            left join doctor on(do_code=ipb_doctorptr) where ipb_ipno='" + strIPno + "'";
                //get the datas into a datatable
                DataTable dtData = mGlobal.LocalDBCon.ExecuteQuery(sqlstr);
                if (dtData == null)
                {
                    MessageBox.Show("Error occurred while loading data");
                    return false;
                }
                if (dtData.Rows.Count <= 0)
                {
                    MessageBox.Show("Data not found");

                    return false;
                }
                //checking the bill is cancelled or not with Cnaflg column
                if (mCommFuncs.ConvertToString(dtData.Rows[0]["Cnaflg"]) == "Y")
                {
                    MessageBox.Show("Cancelled bill");
                    return false;
                }
                // Populate UI fields with retrieved data.

                txtBillNo.Tag = mCommFuncs.ConvertToString(dtData.Rows[0]["BillId"]);
                txtBillNo.Text = mCommFuncs.ConvertToString(dtData.Rows[0]["BillNO"]);
                // txtPhmBillNo.Text = mCommFuncs.ConvertToString(dtData.Rows[0]["PharmacyBillNo"]);
                txtBillType.Text = mCommFuncs.ConvertToString(dtData.Rows[0]["BIllType"]);
                txtBillType.Tag = mCommFuncs.ConvertToString(dtData.Rows[0]["BillTypeCode"]);
                txtBillDt.Text = mCommFuncs.ConvertToString(dtData.Rows[0]["BillDate"]) + " " +
                mCommFuncs.FormatDateS(mCommFuncs.ConvertToString(dtData.Rows[0]["BillTime"]), "hh:mm tt");
                txtOpNO.Text = mCommFuncs.ConvertToString(dtData.Rows[0]["OPno"]);
                txtIPNO.Text = mCommFuncs.ConvertToString(dtData.Rows[0]["IPno"]);
                txtPatientNm.Text = mCommFuncs.ConvertToString(dtData.Rows[0]["PatientName"]);
                txtOpNO.Text = mCommFuncs.ConvertToString(dtData.Rows[0]["OPno"]);
                txtAddress.Text = mCommFuncs.ConvertToString(dtData.Rows[0]["Add1"]).Trim() + "," +
                mCommFuncs.ConvertToString(dtData.Rows[0]["Add2"]).Trim() + "," +
                mCommFuncs.ConvertToString(dtData.Rows[0]["Place"]).Trim();
                txtAddress.Text = txtAddress.Text.Replace(",,", "");
                txtGender.Text = mCommFuncs.ConvertToString(dtData.Rows[0]["Gender"]);
                txtAge.Text = mCommFuncs.ConvertToString(dtData.Rows[0]["Age"]);
                txtCategory.Text = mCommFuncs.ConvertToString(dtData.Rows[0]["PatientCategory"]);
                txtCategory.Tag = mCommFuncs.ConvertToString(dtData.Rows[0]["PatCatCode"]);
                txtDoctor.Text = mCommFuncs.ConvertToString(dtData.Rows[0]["Doctor"]);
                txtCorportate.Text = mCommFuncs.ConvertToString(dtData.Rows[0]["CorporateDesc"]);
                txtPackage.Text = mCommFuncs.ConvertToString(dtData.Rows[0]["PackageDesc"]);
                txtCorpAmt.Text = mCommFuncs.ConvertToString(dtData.Rows[0]["CorpAmt"]);
                txtAdmitDt.Text = mCommFuncs.ConvertToString(dtData.Rows[0]["AdmitDt"]);
                txtDisDt.Text = mCommFuncs.ConvertToString(dtData.Rows[0]["DisDt"]);
                txtPaidAmt.Text = mCommFuncs.ConvertToNumber_Dec(dtData.Rows[0]["PatientPaidAmt"]).ToString("0.00");
                txtBillNetAmt.Text = mCommFuncs.ConvertToNumber_Dec(dtData.Rows[0]["NetAmt"]).ToString("0.00");
                btnOpenBill.Enabled = true;
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
                writeErrorLog(ex, "getHeader");
            }


            return false;
        }

        private void clearData(bool _clearIPNO)
        {
            if (_clearIPNO == true)
            {
                txtIPNO.Text = "";
            }
            txtBillNo.Text = "";
            txtPhmRctNo.Text = "";
            txtPhmBillNo.Text = "";
            txtIPRctNo.Text = "";
            txtBillDt.Text = "";
            txtCategory.Text = "";
            txtPatientNm.Text = "";
            txtAddress.Text = "";
            txtOpNO.Text = "";
            txtAge.Text = "";
            txtGender.Text = "";
            txtDoctor.Text = "";
            txtCorpAmt.Text = "0.00";
            txtCorportate.Text = "";
            txtPackage.Text = "";
            txtCategory.Text = "";
            txtBillType.Text = "";
            txtBillType.Tag = "";
            txtBillNo.Tag = "";
            txtDisDt.Text = "";
            txtBillNetAmt.Text = "";
            txtAdmitDt.Text = "";
            txtPaidAmt.Text = "0.00";

        }
        /// <summary>
        /// save billing information
        /// </summary>
        private bool saveBIllLog()
        {
            try
            {
                string strColRctno = "";
                string strColID = "";
                // Attempt to retrieve receipt details; if unsuccessful, display an error message.
                if (getRctDetails(out strColID, out strColRctno) == false)
                {
                    MessageBox.Show("Error occurred while loading receipt details. Please contact system administrator.");
                    return false;
                }
                // Create a DataTable and populate it with billing information.
                DataTable dtData = mGlobal.LocalDBCon.ExecuteQuery_OnTran("select * from ipbeo where 1=2");
                dtData.Rows.Add(dtData.NewRow());
                dtData.Rows[0]["ipbeo_opno"] = txtOpNO.Text;
                dtData.Rows[0]["ipbeo_ipno"] = txtIPNO.Text;
                dtData.Rows[0]["ipbeo_ipbtranid"] = txtBillNo.Tag.ToString();
                dtData.Rows[0]["ipbeo_ipbno"] = txtBillNo.Text;
                dtData.Rows[0]["ipbeo_crcollids"] = strColID;
                dtData.Rows[0]["ipbeo_crcollrctnos"] = strColRctno;
                dtData.Rows[0]["ipbeo_status"] = "0";
                // Attempt to update the database with the DataTable.
                int intRtn = mGlobal.LocalDBCon.UpdateDataTable_OnTran("select * from ipbeo where 1=2", dtData);
                // If the update is successful, return true.
                if (intRtn > 0) return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
                writeErrorLog(ex, "saveBIllLog");
            }
            // If any errors occur, return false.
            return false;
        }
        /// <summary>
/// Get rct details from coodetails
/// </summary>
/// <param name="strRctid"></param>
/// <param name="strRctNo"></param>
/// <returns></returns>
        private bool getRctDetails(out string strRctid, out string strRctNo)
        {
            string strTempRctno = "";
            string strTempID = "";
            try
            {
                // Construct the SQL query to retrieve receipt details.
                string strSql = "  select cold_id,cold_rctno from colldetails where cold_mode='IP'  and cold_tranno='" + txtBillNo.Tag + "' and (cold_canflg is null or cold_canflg<>'Y')";
                // Execute the query and retrieve data into a DataTable.
                DataTable dtData = mGlobal.LocalDBCon.ExecuteQuery(strSql, true);
                // Iterate through the DataTable and concatenate receipt IDs and numbers into strings.
                foreach (DataRow dr in dtData.Rows)
                {
                    strTempID += dr["cold_id"].ToString() + ",";
                    strTempRctno += dr["cold_rctno"].ToString() + ",";
                }
                // Remove the trailing comma if there are values in the strings.
                if (strTempID.Length > 0)
                {
                    strTempID = strTempID.Substring(0, strTempID.Length - 1);
                    strTempRctno = strTempRctno.Substring(0, strTempRctno.Length - 1);

                }
                // Store the retrieved receipt details in the provided variables.
                strRctid = strTempID;
                strRctNo = strTempRctno;
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
                writeErrorLog(ex, "getRctDetails");
            }
            // If any errors occur, set the output variables and return false.
            strRctid = strTempID;
            strRctNo = strTempRctno;
            return false;
        }
        /// <summary>
        /// Validation
        /// </summary>
        /// <returns></returns>
        private bool isValidData()
        {
            if (txtIPNO.Text.Trim() == "")
            {
                MessageBox.Show("IP Number required");
                txtIPNO.Focus();
                return false;
            }
            return true;
        }       
        /// <summary>
        /// Updating ipadmit table
        /// </summary>
        /// <returns></returns>
        bool updateIPAdmit()
        {
            try
            {
                //int intRnt = mGlobal.LocalDBCon.ExecuteNonQuery("update ipadmit set ia_discharged='N',ia_billed ='N',ia_tmpsaved='N'   where ia_ipno='" + txtIPNO.Text + "'", true);
                int intRnt = mGlobal.LocalDBCon.ExecuteNonQuery_OnTran("update A set ia_billed='N',ia_discharged='N',ia_dischargeddt=null   from ipadmit A  where ia_ipno='" + txtIPNO.Text + "'");

                if (intRnt > 0)
                    return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
            return false;
        }
        /// <summary>
        /// Insertion of ipbill_tm header
        /// </summary>
        /// <returns></returns>
        bool InsertIPBillTmHeader()
        {
            try
            {
                String SQL;
                SQL = "insert into  ipbill_tm([ipb_mode],[ipb_btype],[ipb_bno],[ipb_dt],[ipb_tm],[ipb_opno],[ipb_opid],[ipb_ipno],[ipb_name],[ipb_add1],[ipb_add2],[ipb_place],[ipb_gender],[ipb_age],[ipb_patcatptr],[ipb_doctorptr],[ipb_referrerptr],[ipb_amt],[ipb_taxmainamt],[ipb_taxcessamt],[ipb_taxamt],[ipb_bdisper],[ipb_bdisamt],[ipb_disamt],[ipb_rndamt],[ipb_netamt],[ipb_advamt],[ipb_corpptr],[ipb_corpamt],[ipb_patamt],[ipb_patpayableamt],[ipb_packageptr],[ipb_user],[ipb_userid],[ipb_shiftno],[ipb_shiftdt]" +
                      ",[ipb_canflg],[ipb_canuser],[ipb_canuserid],[ipb_cantime],[ipb_canremarks],[ipb_canshiftno],[ipb_canshiftdt],[ipb_finyearid],[ipb_remarks],[ipb_paidflg],[ipb_paidamt],[ipb_refundflg],[ipb_refundamt])" +
                      "SELECT [ipb_mode],[ipb_btype],[ipb_ipno],[ipb_dt],[ipb_tm],[ipb_opno],[ipb_opid],[ipb_ipno],[ipb_name],[ipb_add1],[ipb_add2],[ipb_place],[ipb_gender],[ipb_age],[ipb_patcatptr],[ipb_doctorptr],[ipb_referrerptr],[ipb_amt],[ipb_taxmainamt],[ipb_taxcessamt],[ipb_taxamt],[ipb_bdisper],[ipb_bdisamt],[ipb_disamt],[ipb_rndamt],[ipb_netamt],[ipb_advamt],[ipb_corpptr],[ipb_corpamt],[ipb_patamt],[ipb_patpayableamt],[ipb_packageptr],[ipb_user],[ipb_userid],[ipb_shiftno],[ipb_shiftdt]" +
                       ",[ipb_canflg],[ipb_canuser],[ipb_canuserid],[ipb_cantime],[ipb_canremarks],[ipb_canshiftno],[ipb_canshiftdt],[ipb_finyearid],[ipb_remarks],[ipb_paidflg],[ipb_paidamt],[ipb_refundflg],[ipb_refundamt] FROM ipbill where ipb_ipno ='" + txtIPNO.Text.ToString() + "';  ";

                if (mGlobal.LocalDBCon.ExecuteNonQuery_OnTran(SQL) > 0)

                    return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
            return false;
        }
        /// <summary>
        /// Insertion of ipbilld_tm footer
        /// </summary>
        /// <returns></returns>
        bool InsertIPBillTmFooter()
        {
            try
            {
                String SQL;
                SQL = "declare @Hid int;" +
                 "set @Hid=@@identity;" +
                 "insert into ipbilld_tm([ipbd_hdrid],[ipbd_itemptr],[ipbd_itemdesc],[ipbd_packagemode],[ipbd_srvddt],[ipbd_srvddoctorptr],[ipbd_taxptr],[ipbd_taxinclusive]" +
                 ",[ipbd_taxremarks],[ipbd_rate],[ipbd_qty],[ipbd_total],[ipbd_disper],[ipbd_disamt],[ipbd_bdisamt],[ipbd_taxmainamt],[ipbd_taxcess1amt],[ipbd_taxcess2amt]" +
                 ",[ipbd_taxamt],[ipbd_packageamt],[ipbd_net],[ipbd_pckaplymode],[ipbd_pckrate],[ipbd_pckqty],[ipbd_pcktotal],[ipbd_corppayable],[ipbd_selfpayable],[ipbd_roomptr]" +
                 ",[ipbd_canflg],[ipbd_pckbefrate],[ipbd_pckbefqty],[ipbd_otherdet])" +
                 "SELECT @Hid,[ipbd_itemptr],[ipbd_itemdesc],[ipbd_packagemode],[ipbd_srvddt],[ipbd_srvddoctorptr],[ipbd_taxptr],[ipbd_taxinclusive]" +
                 ",[ipbd_taxremarks],[ipbd_rate],[ipbd_qty],[ipbd_total],[ipbd_disper],[ipbd_disamt],[ipbd_bdisamt],[ipbd_taxmainamt],[ipbd_taxcess1amt],[ipbd_taxcess2amt]" +
                 ",[ipbd_taxamt],[ipbd_packageamt],[ipbd_net],[ipbd_pckaplymode],[ipbd_pckrate],[ipbd_pckqty],[ipbd_pcktotal],[ipbd_corppayable],[ipbd_selfpayable],[ipbd_roomptr]" +
                 ",[ipbd_canflg],[ipbd_pckbefrate],[ipbd_pckbefqty],[ipbd_otherdet]  FROM ipbilld where [ipbd_hdrid] in(select ipb_id from ipbill where ipb_ipno ='" + txtIPNO.Text.ToString() + "');  ";
                if (mGlobal.LocalDBCon.ExecuteNonQuery_OnTran(SQL) > 0)
                    return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
                writeErrorLog(ex, "InsertIPBillTmFooter");
            }
            return false;
        }
        /// <summary>
        /// Deleteing ipbillheader
        /// </summary>
        /// <returns></returns>
        bool deleteIPBillHeader()
        {
            try
            {
                int intRnt = mGlobal.LocalDBCon.ExecuteNonQuery_OnTran("delete from ipbilld where ipbd_hdrid in (select ipb_id from ipbill where ipb_ipno ='" + txtIPNO.Text.ToString() + "')");
                if (intRnt > 0)
                    return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
            return false;
        }
        /// <summary>
        /// deleting ipbill footer
        /// </summary>
        /// <returns></returns>
        bool deleteIPBillFooter()
        {
            try
            {
                int intRnt = mGlobal.LocalDBCon.ExecuteNonQuery_OnTran("delete from  ipbill where ipb_ipno ='" + txtIPNO.Text.ToString() + "'");
                if (intRnt > 0)
                    return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
            return false;
        }

        #region Events
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            clearData(true);
            txtIPNO.Focus();
        }
        private bool checkIPCollection(string strCollectionMode)
        {
            string strSql = "  select distinct(cold_rctno)as cold_rctno from colldetails where cold_mode='" + strCollectionMode + "'  and cold_refno='" + txtBillNo.Text + "' and (cold_canflg is null or cold_canflg<>'Y')";
            // Execute the query and retrieve data into a DataTable.
            DataTable dtData = mGlobal.LocalDBCon.ExecuteQuery(strSql, true);
            // Display the panel (pnlPhmRct) if dtData have value init
            if (dtData != null && dtData.Rows.Count > 0)
            {
                foreach (DataRow dtRow in dtData.Rows)
                {
                    if (txtIPRctNo.Text.Length > 0)
                    { txtIPRctNo.Text = mCommFuncs.ConvertToString(dtRow["cold_rctno"]); }
                    else
                    {
                        txtIPRctNo.Text = mCommFuncs.ConvertToString(dtRow["cold_rctno"]);
                    }
                }
                pnlPhmRct.Visible = true;
                return true;
            }
            // Hide the panel (pnlPhmRct) if dtData dont have a value init
            else
            {
                pnlPhmRct.Visible = false;

            }

            return false;

        }
        private bool checkPHMCollection(string strCollectionMode)
        {
            bool boolSuccess = false;
            string strSql = "  select distinct(cold_rctno)as cold_rctno ,cold_refno from colldetails where cold_mode='" + strCollectionMode + "'  and cold_tranno in(select s_id from phm_sales where s_ipno='" + txtIPNO.Text + "' and (cold_canflg is null or cold_canflg<>'Y'))";
            DataTable dtData = mGlobal.LocalDBCon.ExecuteQuery(strSql, true);
            // Check if there is data returned from the query.
            if (dtData != null && dtData.Rows.Count > 0)
            {
                // Loop through each row in the DataTable and concatenate cold_rctno values to txtPhmRctNo.
                foreach (DataRow dtRow in dtData.Rows)
                {

                    if (txtPhmRctNo.Text.Length > 0)
                    {
                        if (!txtPhmRctNo.Text.Contains(mCommFuncs.ConvertToString(dtRow["cold_rctno"])))
                            txtPhmRctNo.Text = txtPhmRctNo.Text + "," + mCommFuncs.ConvertToString(dtRow["cold_rctno"]);
                    }
                    else
                    {
                        txtPhmRctNo.Text = mCommFuncs.ConvertToString(dtRow["cold_rctno"]);
                    }
                    if (txtPhmBillNo.Text.Length > 0)
                    {
                        if (!txtPhmBillNo.Text.Contains(mCommFuncs.ConvertToString(dtRow["cold_refno"])))
                            txtPhmBillNo.Text = txtPhmBillNo.Text + "," + mCommFuncs.ConvertToString(dtRow["cold_refno"]);
                    }
                    else
                    {
                        txtPhmBillNo.Text = mCommFuncs.ConvertToString(dtRow["cold_refno"]);
                    }
                }
                // Make the panel pnlPhmRct visible.
                pnlPhmRct.Visible = true;
                // Return true indicating successful operation.
                boolSuccess = true;
            }
            else
            {
                // If no data is returned, hide the panel pnlPhmRct.
                pnlPhmRct.Visible = false;
                boolSuccess = false;
                MessageBox.Show("Pharmacy Collection Not Found", "Medicant");
                chkPhmColl.Checked = false;
            }
            // Return false indicating unsuccessful operation.
            return boolSuccess;

        }
        private void btnOpenBill_Click(object sender, EventArgs e)
        {

            if (MessageBox.Show("Are you sure to Open this bill.?", "Cancellation", MessageBoxButtons.YesNo) == DialogResult.No) return;

            if (isValidData() == false) return;//checking the ipnumber entered or not


            if (chkPhmColl.Checked == true)
            {
                if (checkPHMCollection(mstrPHMCollectionMode))
                {
                    MessageBox.Show("Can't Open this bill, Please Cancel Pharmacy Credit Collection first.");
                    return;
                }
                if (txtIPRctNo.Text.Length > 0)
                {
                    MessageBox.Show("Can't Open this bill, Please Cancel IP Credit Collection first.");

                    return;
                }

                checkIPCollection(mstrIPCollectionMode);
            }
            btnOpenBill.Enabled = false;
            mGlobal.LocalDBCon.BeginTrans();

            if (saveBIllLog() == false)
            {
                MessageBox.Show("Error occurred while saving data. Please contact system administrator.");
                mGlobal.LocalDBCon.RollbackTrans(true);
                return;
            }
            //if (updateOPRegister() == false)
            //{
            //    MessageBox.Show("OP register updation failed. Please contact system administrator.");
            //    mGlobal.LocalDBCon.RollbackTrans(true);
            //    return;
            //}
            if (updateIPAdmit() == false)
            {
                MessageBox.Show("IP Admit updation failed. Please contact system administrator.");
                mGlobal.LocalDBCon.RollbackTrans(true);
                return;
            }
            if (InsertIPBillTmHeader() == false)
            {
                MessageBox.Show("IP billTemp insert process failed. Please contact system administrator.");
                mGlobal.LocalDBCon.RollbackTrans(true);
                return;
            }

            //to do later
            if (InsertIPBillTmFooter() == false)
            {
                MessageBox.Show("IP billTemp insert process failed. Please contact system administrator.");
                mGlobal.LocalDBCon.RollbackTrans(true);
                return;
            }
            if (deleteIPBillHeader() == false)
            {
                MessageBox.Show("IP bill delete process failed. Please contact system administrator.");
                mGlobal.LocalDBCon.RollbackTrans(true);
                return;
            }
            if (deleteIPBillFooter() == false)
            {
                MessageBox.Show("IP bill delete process failed. Please contact system administrator.");
                mGlobal.LocalDBCon.RollbackTrans(true);
                return;
            }

            mGlobal.LocalDBCon.CommitTrans();
            MessageBox.Show("Bill Successfully Opened");
            btnCancel_Click(null, null);
        }
        private void txtIPNO_Validating(object sender, CancelEventArgs e)
        {
            clearData(false);
            chkPhmColl.Checked = false;
            if (txtIPNO.Text.Trim() == "") return;

            if (getHeader(txtIPNO.Text) == false)
            {
                e.Cancel = true;
                return;
            }
        }
        private void txtIPNO_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == Convert.ToChar(mGlobal.SearchKey))// Checking Search key Pressed
                {
                    // Search Settings 
                    Search Searchfrm = new Search();
                    Searchfrm.DefSearchFldIndex = 2;
                    Searchfrm.DefSearchText = txtBillNo.Text;
                    Searchfrm.Query = @"select ipb_ipno, ipb_opno ,ipb_name ,
                                      ipb_add1 +ipb_add2+ipb_place,ipb_bno,ptc_desc from ipbill  
                                      left join patientcategory on(ptc_code=ipb_patcatptr)";
                    Searchfrm.FilterCond = "(ipb_canflg is null or ipb_canflg<>'Y') and ipb_mode='IS'";
                    Searchfrm.ReturnFldIndex = 0;
                    Searchfrm.ColumnHeader = "IP No|0P No |Name|Address|Bill No|Category";
                    Searchfrm.ColumnWidth = "100|100|150|150|100|100";
                    Searchfrm.ShowDialog();
                    // Search value returns
                    if (!Searchfrm.Cancelled) // whether not clicked on cancel button in search screen
                        txtIPNO.Text = Searchfrm.ReturnValue;
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); writeErrorLog(ex, "txtIPNO_KeyPress"); }
        }
        private void chkPhmColl_CheckedChanged(object sender, EventArgs e)
        {
            pnlPhmRct.Visible = chkPhmColl.Checked;
            if (chkPhmColl.Checked && txtIPNO.Text != "")
            {
                checkPHMCollection(mstrPHMCollectionMode);
                checkIPCollection(mstrIPCollectionMode);
            }
            else
            {
                txtIPRctNo.Text = "";
                txtPhmBillNo.Text = "";
                txtPhmRctNo.Text = "";
            }
        }
        # endregion
        //writing errorlog
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
    }
}
 
