using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Data.SqlClient;

namespace CsHms.Akshay
{
    public partial class CouponCreator : Form
    {
        public CouponCreator()
        {
            InitializeComponent();
            GetItems();            
            GetItemtype();
            GetModeCount();
            cbxMasmodule.SelectedIndex = 0;
            cbxMasItemtype.SelectedIndex = 0;
            btnSave.Text = "Generate";
            dtMasFrmdt.Value = DateTime.Today;
            dtMasFrmdttime.Value = DateTime.Today;
            dtMasTodt.Value = DateTime.Now.AddDays(30);            
            DateTime futureDate = DateTime.Now.AddDays(30);            
            DateTime futureDateTimeEndOfDay = new DateTime(futureDate.Year, futureDate.Month, futureDate.Day, 23, 59, 59);
            dtMasTodttime.Value = futureDateTimeEndOfDay;

        }
        Global mGlobal=new Global();
        CommFuncs mCommFunc=new CommFuncs();        
        Akshay.Class.CouponCreatorCls mCouponCreateCls = new Akshay.Class.CouponCreatorCls();
        private bool CouponExist;
        string mstrmasptr = "";

        private void btnClear_Click(object sender, EventArgs e)
        {
            if (tbCouponCreator.SelectedTab.Name != "tbExtendvalidity")
                ClearAll();
            else
                ClearExtendItems();
        }
      
        private void ClearAll()
        {
            for (int i = 0; i < chklbkItems.Items.Count; i++)
            {
                chklbkItems.SetItemChecked(i, false);
            }
            txtMasCount.Text = "";            
            txtMasMode.Text = "";
            txtMasPostfix.Text = "";
            txtMasPrefix.Text = "";
            txtMasRemarks.Text = "";
            numMasValue.Text = "1";
            dtMasFrmdt.Text = "";
            dtMasFrmdttime.Text = "";
            dtMasTodt.Text = "";
            dtMasTodttime.Text = "23:59:59";
            radValTypeA.Checked = false;
            radValTypeP.Checked = false;
            txtMasDescription.Text = "";
            mstrmasptr = "";
            dgvCoupons.DataSource = null;
            numMasAvailCount.Value = 1;
            dtMasFrmdt.Value = DateTime.Now;
            dtMasTodt.Value = DateTime.Now.AddDays(30);
            GetModeCount();
            numMasDigits.Minimum = 10;
            numMasDigits.Value = 10;
            CouponExist = false;
            btnSave.Enabled = true;
           

        }
        private void ClearExtendItems()
        {
            txtExtDescription.Text = "";
            txtExtRemarks.Text = "";
            dtExtFrmdt.Text = "";
            dtExtFrmdttime.Text = "";
            dtExtTodt.Text = "";
            dtExtTodttime.Text = "23:59:59";
            txtExtMode.Text = "";
        }
        private void GetItems()
        {
            try
            {
                DataTable dtItems = mCouponCreateCls.dtGetItems();
                if (dtItems.Rows.Count > 0)
                {
                    chklbkItems.DataSource = dtItems;
                    chklbkItems.DisplayMember = "itm_desc";
                    chklbkItems.ValueMember = "itm_code";
                }

            }
            catch (Exception ex)
            { writeErrorLog(ex, "GetItems"); }
        }
        public static void writeErrorLog(Exception exception, string strErrorSource)
        {
            // Retrieve the path to the error log file from the application configuration.
            string strlogfile = String.Empty;
            strlogfile = System.Configuration.ConfigurationSettings.AppSettings["ErrorLog_path"].ToString();

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

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (mCommFunc.ConvertToString(tbCouponCreator.SelectedTab.Name) == "tbCouponMaster" && Validate())
                {
                    
                        if (DialogResult.Yes == MessageBox.Show("Selected date range is between \n" + mCommFunc.ConvertToString(dtMasFrmdt.Text) + " and " + mCommFunc.ConvertToString(dtMasTodt.Text) + "", "Do you want to proceed?", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                        {
                            GetDiscountCoupons();

                        }
                    
                }
                else if (mCommFunc.ConvertToString(tbCouponCreator.SelectedTab.Name) == "tbCoupons" &&Validate())
                {
                    
                        mGlobal.LocalDBCon.BeginTrans();

                        CreateCouponMaster();

                        if (mstrmasptr != "")
                        {
                            CreateCoupons();
                        }
                    
                    
                }
                else if (mCommFunc.ConvertToString(tbCouponCreator.SelectedTab.Name) == "tbExtendvalidity" && ValidateExtend())
                {
                    UpdateCouponMaster();
                }
            }
            catch (Exception ex)
            {
                // Handle the exception appropriately (log, display message, etc.)
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                // Rollback the transaction in case of an error
                //mGlobal.LocalDBCon.RollbackTrans();
            }
        }
        
        private void CreateCouponMaster()
        {
            try
            {

                string strValueType = "";
                string strchkActive="";
                string strCurrentdt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                string strSelectedItems = GetSelectedValues(chklbkItems);
                strValueType = radValTypeA.Checked ? "A" : "P";
                strchkActive = chkActive.Checked ? "Y" : "N";

                mCouponCreateCls.Mode = mCommFunc.ConvertToString(txtMasMode.Text);
                mCouponCreateCls.Baseratetypeptr = mCommFunc.ConvertToString(cbxMasRatetype.SelectedValue);
                mCouponCreateCls.Fromdt = dtMasFrmdt.Value;
                mCouponCreateCls.Fromdttm = dtMasFrmdttime.Value;
                mCouponCreateCls.Todt = dtMasTodt.Value;
                mCouponCreateCls.Todttm = dtMasTodttime.Value;
                mCouponCreateCls.Billmoduletypes = mCommFunc.ConvertToString(cbxMasmodule.SelectedItem);
                mCouponCreateCls.Itemtype = mCommFunc.ConvertToString(cbxMasItemtype.SelectedItem);
                mCouponCreateCls.Valuetype = strValueType;
                mCouponCreateCls.Value = numMasValue.Value;
                mCouponCreateCls.Couponnos = mCommFunc.ConvertToInt(txtMasCount.Text);
                mCouponCreateCls.Couponprefix = mCommFunc.ConvertToString(txtMasPrefix.Text);
                mCouponCreateCls.Couponpostfix = mCommFunc.ConvertToString(txtMasPostfix.Text);
                mCouponCreateCls.Couponsnoofdigits = mCommFunc.ConvertToInt(numMasDigits.Value);
                mCouponCreateCls.Active = strchkActive;
                mCouponCreateCls.Remarks = mCommFunc.ConvertToString(txtMasRemarks.Text);
                mCouponCreateCls.Dt =DateTime.Now;
                mCouponCreateCls.Applicableitemcodes = strSelectedItems;
                mCouponCreateCls.Description = txtMasDescription.Text;

                mstrmasptr = mCouponCreateCls.CreateCouponMaster();


            }
            catch(Exception ex)
            { 

                writeErrorLog(ex, "CreateCouponMaster");
                MessageBox.Show(ex.Message.ToString());
               mGlobal.LocalDBCon.RollbackTrans();

            }
        }
        private void UpdateCouponMaster()
        {
            try
            {
                mCouponCreateCls.Fromdt = dtExtFrmdt.Value;
                mCouponCreateCls.Fromdttm = dtExtFrmdttime.Value;
                mCouponCreateCls.Todt = dtExtTodt.Value;
                mCouponCreateCls.Todttm = dtExtTodttime.Value;
                mCouponCreateCls.Id = mCommFunc.ConvertToInt(txtExtCouponCode.Tag);
                mCouponCreateCls.UpdateCouponMaster();
                

            }
            catch (Exception ex)
            { writeErrorLog(ex, "UpdateCouponMaster"); }
        }


        private void CreateCoupons()
        {
            try
            {
                DataTable dtCoupon = (DataTable)dgvCoupons.DataSource;

                for (int i = 0; i < dtCoupon.Rows.Count; i++)
                {
                    string strCouponCode = mCommFunc.ConvertToString(dtCoupon.Rows[i]["Coupon Number"]);
                    if (mCouponCreateCls.CheckAlreadyExist(strCouponCode))
                    {
                        MessageBox.Show("Coupon:" + strCouponCode + " already exist generate a new one");
                         mGlobal.LocalDBCon.RollbackTrans();
                        return;
                    }
                   
                   

                }
                for (int i = 0; i < dtCoupon.Rows.Count; i++)
                {
                   
                    mCouponCreateCls.CouponCode= mCommFunc.ConvertToString(dtCoupon.Rows[i]["Coupon Number"]);                    
                    mCouponCreateCls.CouponRemarks = mCommFunc.ConvertToString(dtCoupon.Rows[i]["Remarks"]);                    
                    mCouponCreateCls.CouponAvailablenos = mCommFunc.ConvertToString(dtCoupon.Rows[i]["Available nos"]);
                    int res = mCouponCreateCls.CreateCoupons();                   
                    if (res < 0)
                    { 
                        mGlobal.LocalDBCon.RollbackTrans(); 
                    }
                        if (dtCoupon.Rows.Count-1  == i && res > 0)
                        {
                            MessageBox.Show("Coupon Created");
                            GetModeCount();
                            mGlobal.LocalDBCon.CommitTrans();
                            if(chkBranches.Checked==false)
                            ClearAll();
                        }
                       
                 }
                }
            
            catch (Exception ex)
            { writeErrorLog(ex, "CreateCoupons");
            mGlobal.LocalDBCon.RollbackTrans();
        }
        }
       
        private string GetSelectedValues(CheckedListBox checkedListBox)
        {
            // Create a comma-separated string for selected values
            string selectedValues = "";

            foreach (DataRowView drv in checkedListBox.CheckedItems)
            {
                // Assuming "Name" is the field you want to include in the result
                selectedValues += drv["itm_code"].ToString();

                // Add a comma if it's not the last selected item
                if (checkedListBox.CheckedItems.IndexOf(drv) < checkedListBox.CheckedItems.Count - 1)
                {
                    selectedValues += ",";
                }
            }

            return selectedValues;
        }


        private void txtMasValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; // Suppress the key press if it's not a digit or a control key
            }
           
        }



        private void txtMasCount_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; // Suppress the key press if it's not a digit or a control key
            }
        }

        private void txtMasDigits_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; // Suppress the key press if it's not a digit or a control key
            }
        }              
        private void GetDiscountCoupons()
        {
            try
            {
                //string strSql = "select * from campgndiscountmas where cmgdcm_id='" + mstrmasptr + "'";
                //DataTable dtDiscountmas = mGlobal.LocalDBCon.ExecuteQuery(strSql);
                DataRow drDismas;
                DataTable dtcoupons = new DataTable();
                dtcoupons.Columns.Add("Coupon Count", typeof(System.Int32));
                dtcoupons.Columns.Add("Remarks", typeof(System.String));
                dtcoupons.Columns.Add("Coupon Number", typeof(System.String));
                dtcoupons.Columns.Add("Available nos", typeof(System.String));
               
                    string strCouponRemark = mCommFunc.ConvertToString(txtMasRemarks.Text); ;
                    int intCouponnos = mCommFunc.ConvertToInt(txtMasCount.Text);
                    int intcouponDigits = mCommFunc.ConvertToInt(numMasDigits.Text);
                    string strPrefix = mCommFunc.ConvertToString(txtMasPrefix.Text);
                    string strPostfix = mCommFunc.ConvertToString(txtMasPostfix.Text);
                    int intCouponLength = intcouponDigits - (strPrefix.Length + strPostfix.Length);
                    
                         
                    for (int i = 0; i < intCouponnos; i++)
                    {
                        drDismas = dtcoupons.NewRow();
                        drDismas[0] = i+1;
                        drDismas[1] = strCouponRemark;
                        drDismas[3] =numMasAvailCount.Value;
                        dtcoupons.Rows.Add(drDismas);
                        dgvCoupons.DataSource = dtcoupons;

                    }
                    
                    GenerateCouponCodes(intCouponnos, intCouponLength, strPrefix,strPostfix);

                


            }
            catch (Exception ex) { writeErrorLog(ex, "GetDiscountCoupons"); }
        }

        private void dtMasFrmdt_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                DateTime originalDate = dtMasFrmdt.Value;
                string formattedDate = originalDate.ToString("dd-MM-yyyy 00:00:00");
                DateTime modifiedDate = DateTime.ParseExact(formattedDate, "dd-MM-yyyy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
                dtMasFrmdttime.Value = modifiedDate;
            }
            catch (Exception ex)
            { writeErrorLog(ex, "dtMasFrmdt_ValueChanged"); }
        }

        private void dtMasTodt_ValueChanged(object sender, EventArgs e)
        {
            try
            {                
                DateTime originalDate = dtMasTodt.Value;
                string formattedDate = originalDate.ToString("dd-MM-yyyy 23:59:59");
                DateTime modifiedDate = DateTime.ParseExact(formattedDate, "dd-MM-yyyy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
                dtMasTodttime.Value = modifiedDate;
            }
            catch (Exception ex)
            { writeErrorLog(ex, "dtMasTodt_ValueChanged"); }
        }

        private void GetItemtype()
        {
            try
            {
                string strSql = "select itmrt_code,itmrt_desc from itemratetype";
                DataTable dtItemtypes = mGlobal.LocalDBCon.ExecuteQuery(strSql);
                if (dtItemtypes.Rows.Count > 0)
                {
                    cbxMasRatetype.DataSource = dtItemtypes;
                    cbxMasRatetype.DisplayMember = "itmrt_desc";
                    cbxMasRatetype.ValueMember = "itmrt_code";
                    cbxMasRatetype.SelectedValue = "GEN";
                }
            }
            catch (Exception ex)
            { writeErrorLog(ex, "GetItemtype"); }
        }
        private void GetModeCount()
        {
            try
            {
                string strSql = @"select MAX(CAST(REPLACE(cmgdcm_mode,'CPN','')AS INT))+1 as Last_count from campgndiscountmas where cmgdcm_mode like 'CPN%' and  PATINDEX('%[^a-zA-Z!@#$%^&*()}~`{[\|]%', REPLACE(cmgdcm_mode,'CPN','')) > 0";
                DataTable dtModeCount = mGlobal.LocalDBCon.ExecuteQuery(strSql);
                if (dtModeCount.Rows.Count > 0)
                {
                    txtMasMode.Text = "CPN" + mCommFunc.ConvertToString(dtModeCount.Rows[0]["Last_count"]);
                }
            }
            catch (Exception ex)
            { writeErrorLog(ex, "GetModeCount"); }
        }

        

    private void GenerateCouponCodes(int count, int digitCount,string prefix,string postfix)
{
    List<string> couponCodes = new List<string>();   
    Random random = new Random();
    for (int i = 0; i < count; i++)
    {
        string couponCode;
        // Loop until a unique coupon code is generated
        do
        {
            // Generate a random number with the specified digit count
            int randomNumber = random.Next((int)Math.Pow(10, digitCount - 1), (int)Math.Pow(10, digitCount));

            // Combine the prefix, random number, and postfix to form the coupon code
            couponCode = "" + prefix + "" + randomNumber + "" + postfix + "";
            //if (mCouponCreateCls.CheckAlreadyExist(couponCode, couponCodes))
            //{
            //    MessageBox.Show("Coupon Already Existing. \n Please review the coupon length or coupon count settings and ensure they meet the requirements for successful generation.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    return;
            //}
        }
        while (mCouponCreateCls.CheckAlreadyExist(couponCode, couponCodes));
        {
            
            if (couponCodes.Contains(couponCode))
            {
                MessageBox.Show("Coupon code " + couponCode + " already exists. Generating  new one.");
                
            }
            else
                couponCodes.Add(couponCode);

        }
    }
    if (couponCodes.Count != count)
    {
        MessageBox.Show("Coupon Already Existing. \n Please review the coupon length or coupon count settings and ensure they meet the requirements for successful generation.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        return;
    }

    // Assuming "Coupon Number" is the name of the column in your DataGridView
    for (int i = 0; i < couponCodes.Count; i++)
    {
        dgvCoupons.Rows[i].Cells["Coupon Number"].Value = couponCodes[i];
    }
    tbCouponCreator.SelectedIndex++;
}



        private void btnExit_Click(object sender, EventArgs e)
        {
            Close();
        }
        private bool Validate()
        {
            try
            {
                string strExistCheck = "select * from campgndiscountmas where cmgdcm_mode='" + mCommFunc.ConvertToString(txtMasMode.Text) + "'";
                DataTable dtres = mGlobal.LocalDBCon.ExecuteQuery(strExistCheck);
                bool Exist = dtres.Rows.Count > 0 ? true : false;
                if (string.IsNullOrEmpty(txtMasDescription.Text))
                {
                    MessageBox.Show("Fill Description");
                    return false;
                }
                else if (radValTypeA.Checked == false && radValTypeP.Checked == false)
                {
                    MessageBox.Show("Select value type");
                    return false;
                }
                else if (string.IsNullOrEmpty(numMasValue.Text))
                {
                    MessageBox.Show("Fill value");
                    return false;
                }
                else if (string.IsNullOrEmpty(txtMasRemarks.Text))
                {
                    MessageBox.Show("Fill remarks");
                    return false;
                }
                else if (string.IsNullOrEmpty(txtMasCount.Text))
                {
                    MessageBox.Show("Fill count");
                    return false;
                }
                else if (chklbkItems.CheckedItems.Count == 0)
                {
                    MessageBox.Show("Select items");
                    return false;
                }
                else if (chkActive.Checked == false)
                {
                    MessageBox.Show("Check active");
                    return false;
                } 
                else if(Convert.ToDateTime(dtMasFrmdt.Value)>Convert.ToDateTime(dtMasTodt.Value))
                {
                    MessageBox.Show("From date is greater than to date");
                    return false;
                }

                else
                    return true;

            return false;
            }
                catch(Exception ex)
            {
                writeErrorLog(ex, "Validate");
                return false;

            }
        }

        private bool ValidateExtend()
        {
            try
            {
                if (Convert.ToDateTime(dtExtFrmdt.Value) > Convert.ToDateTime(dtExtTodt.Value))
                {
                    MessageBox.Show("From date is greater than to date");
                    return false;
                }
                else
                    return true;
            }
            catch (Exception ex) { writeErrorLog(ex,"ValidateExtend");
            
            }
            return false;
        }

        private void tbCouponCreator_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tbCouponCreator.SelectedTab.Name == "tbCouponMaster")
            {
                if (!CouponExist)
                {
                    btnSave.Text = "Generate";
                    btnSave.Enabled = true;
                   
                }
                else
                {
                    
                    btnSave.Enabled = false;
                    
                }
            }
            else if (tbCouponCreator.SelectedTab.Name == "tbCoupons")
            {
                if (!CouponExist)
                {
                    btnSave.Text = "Save";
                    btnSave.Enabled = true;
                }
                else
                {

                    btnSave.Enabled = false;
                }
            }
            else
            {
                btnSave.Text = "Update";
            }
        }

      
        private void FillCouponDetails()
        {
            try
            {
                string strSql = "select * from campgndiscountmas left join campgndiscountcoupons on cmgdcm_id=cmgdcpn_campgndiscountmasptr where cmgdcm_mode='"+mCommFunc.ConvertToString(txtMasMode.Text)+"'";
                DataTable dtCouponDet = mGlobal.LocalDBCon.ExecuteQuery(strSql);
                if (dtCouponDet.Rows.Count > 0)
                {
                    CouponExist = true;
                    btnSave.Enabled = false;
                    cbxMasRatetype.SelectedValue = mCommFunc.ConvertToString(dtCouponDet.Rows[0]["cmgdcm_baseratetypeptr"]);
                    txtMasDescription.Text = mCommFunc.ConvertToString(dtCouponDet.Rows[0]["cmgdcm_description"]);
                    dtMasFrmdt.Value = Convert.ToDateTime(dtCouponDet.Rows[0]["cmgdcm_fromdt"]);
                    dtMasFrmdttime.Value = Convert.ToDateTime(dtCouponDet.Rows[0]["cmgdcm_fromdttm"]);
                    dtMasTodt.Value = Convert.ToDateTime(dtCouponDet.Rows[0]["cmgdcm_todt"]);
                    dtMasTodttime.Value = Convert.ToDateTime(dtCouponDet.Rows[0]["cmgdcm_todttm"]);
                    cbxMasmodule.SelectedItem = mCommFunc.ConvertToString(dtCouponDet.Rows[0]["cmgdcm_billmoduletypes"]);
                    cbxMasItemtype.SelectedItem = mCommFunc.ConvertToString(dtCouponDet.Rows[0]["cmgdcm_itemtype"]);
                    if (mCommFunc.ConvertToString(dtCouponDet.Rows[0]["cmgdcm_valuetype"]) == "P")
                    { radValTypeP.Checked = true; }
                    else
                    { radValTypeP.Checked = true; }
                    numMasValue.Text = mCommFunc.ConvertToString(dtCouponDet.Rows[0]["cmgdcm_value"]);
                    txtMasCount.Text = mCommFunc.ConvertToString(dtCouponDet.Rows[0]["cmgdcm_couponnos"]);
                    txtMasPrefix.Text = mCommFunc.ConvertToString(dtCouponDet.Rows[0]["cmgdcm_couponprefix"]);
                    txtMasPostfix.Text = mCommFunc.ConvertToString(dtCouponDet.Rows[0]["cmgdcm_couponpostfix"]);
                    numMasDigits.Value = mCommFunc.ConvertToNumber_Dec(dtCouponDet.Rows[0]["cmgdcm_couponsnoofdigits"]);
                    txtMasRemarks.Text = mCommFunc.ConvertToString(dtCouponDet.Rows[0]["cmgdcm_remarks"]);
                    chkActive.Checked = mCommFunc.ConvertToString(dtCouponDet.Rows[0]["cmgdcm_active"]) == "Y" ? true : false;
                    string Itemcodes = mCommFunc.ConvertToString(dtCouponDet.Rows[0]["cmgdcm_applicableItemcodes"]);
                    // Split the comma-separated string into an array of strings
                    string[] ItemcodesArray = Itemcodes.Split(',');
                    // Parse each string into an integer and check the item
                    foreach (string idString in ItemcodesArray)
                    {
                        CheckItemByValue(idString);
                    }


                    DataRow drDismas;
                    DataTable dtcoupons = new DataTable();
                    dtcoupons.Columns.Add("Coupon Count", typeof(System.Int32));
                    dtcoupons.Columns.Add("Remarks", typeof(System.String));
                    dtcoupons.Columns.Add("Coupon Number", typeof(System.String));
                    dtcoupons.Columns.Add("Available nos", typeof(System.String));

                    string strCouponRemark = mCommFunc.ConvertToString(txtMasRemarks.Text); ;
                    int intCouponnos = mCommFunc.ConvertToInt(txtMasCount.Text);
                    int intcouponDigits = mCommFunc.ConvertToInt(numMasDigits.Text);
                    string strPrefix = mCommFunc.ConvertToString(txtMasPrefix.Text);
                    string strPostfix = mCommFunc.ConvertToString(txtMasPostfix.Text);
                    int intCouponLength = intcouponDigits - (strPrefix.Length + strPostfix.Length);


                    for (int i = 0; i < dtCouponDet.Rows.Count; i++)
                    {
                        //drDismas = dtcoupons.NewRow();
                        //drDismas[0] = i + 1;
                        //drDismas[1] = mCommFunc.ConvertToString(dtCouponDet.Rows[0]["cmgdcm_remarks"]);
                        //drDismas[2] = mCommFunc.ConvertToString(dtCouponDet.Rows[0]["cmgdcpn_couponnumber"]);
                        //drDismas[3] = mCommFunc.ConvertToString(dtCouponDet.Rows[0]["cmgdcpn_availablenos"]);
                        //dtcoupons.Rows.Add(drDismas);
                        //dgvCoupons.DataSource = dtcoupons;
                        if (dtCouponDet.Rows.Count >= i)
                        {
                            DataRow drCoupon = dtcoupons.NewRow();
                            dtcoupons.Rows.Add(drCoupon);
                        }
                        dtcoupons.Rows[i]["Coupon Count"] = mCommFunc.ConvertToString(i + 1);
                        dtcoupons.Rows[i]["Remarks"] = dtCouponDet.Rows[i]["cmgdcm_remarks"];
                        dtcoupons.Rows[i]["Coupon Number"] = dtCouponDet.Rows[i]["cmgdcpn_couponnumber"];
                        dtcoupons.Rows[i]["Available nos"] = dtCouponDet.Rows[i]["cmgdcpn_availablenos"];

                    }
                    dgvCoupons.DataSource = dtcoupons;
                    //GenerateCouponCodes(intCouponnos, intCouponLength, strPrefix, strPostfix);

                }
                else
                {
                    ClearAll();
                    CouponExist = false;
                    
                }
            }
            catch (Exception ex)
            {
                writeErrorLog(ex, "FillCouponDetails");
            }
        }
        private void CheckItemByValue(string valueToCheck)
        {
            // Loop through the items in the CheckedListBox
            for (int i = 0; i < chklbkItems.Items.Count; i++)
            {
                // Get the DataRowView from the CheckedListBox item
                DataRowView currentItem = (DataRowView)chklbkItems.Items[i];

                // Assuming your values are integers, change the column name accordingly
                string currentItemValue = mCommFunc.ConvertToString(currentItem[1]);

                // Check the item if the values match
                if (currentItemValue == valueToCheck)
                {
                    chklbkItems.SetItemChecked(i, true);
                    break; // Exit the loop once the item is found
                }
            }
        }

        private void txtMasMode_Validating(object sender, CancelEventArgs e)
        {
            FillCouponDetails();
        }     

        private void radValTypeP_CheckedChanged(object sender, EventArgs e)
        {
            numMasValue.Value = 1;
            if (radValTypeP.Checked == true)
            {
                numMasValue.Maximum = 100;
            }
        }

        private void radValTypeA_CheckedChanged(object sender, EventArgs e)
        {
            numMasValue.Value = 1;
            if (radValTypeA.Checked == true)
            {
                numMasValue.Maximum = 99999;
            }
        }

        private void txtMasValue_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            
            if (radValTypeP.Checked == true)
            {
                numMasValue.Maximum = 100;
            }
        }

        private void chkSingleuse_CheckStateChanged(object sender, EventArgs e)
        {
            if (chkSingleuse.Checked == true)
            {
                numMasAvailCount.Enabled = false;
                numMasAvailCount.Value = 1;
            }
            else if (chkSingleuse.Checked == false)
            {
                numMasAvailCount.Enabled = true;
                numMasAvailCount.Value = 1;
            }
        }

        private void numMasAvailCount_ValueChanged(object sender, EventArgs e)
        {
            if (txtMasCount.Text.ToString() != "")
                lblCount.Text = "Total coupons:" + mCommFunc.ConvertToString(Convert.ToInt32(txtMasCount.Text) * Convert.ToInt32(numMasAvailCount.Value));
            else
                lblCount.Visible = false;
            
        }

        private void txtMasCount_TextChanged(object sender, EventArgs e)
        {
            if (txtMasCount.Text.ToString() != "")
            {
                lblCount.Visible = true;
                lblCount.Text = "Total coupons:" + mCommFunc.ConvertToString(Convert.ToInt32(txtMasCount.Text.ToString()) * Convert.ToInt32(numMasAvailCount.Value));
            }
            else
                lblCount.Visible = false;

        if (mCommFunc.ConvertToInt(txtMasCount.Text) > 10)
        { numMasDigits.Minimum = 13; }
        else
        { numMasDigits.Minimum = 10; }
        UpdateNumMasDigitsMinimum();
        }

        private void numMasAvailCount_KeyPress(object sender, KeyPressEventArgs e)
        {
            UpdateNumMasDigitsMinimum();
        }

        private void txtMasPrefix_KeyPress(object sender, KeyPressEventArgs e)
        {
            UpdateNumMasDigitsMinimum();
        }

        private void txtMasPostfix_KeyPress(object sender, KeyPressEventArgs e)
        {
            UpdateNumMasDigitsMinimum();
        }

        private void txtMasPrefix_Validating(object sender, CancelEventArgs e)
        {
            UpdateNumMasDigitsMinimum();
        }
        private void UpdateNumMasDigitsMinimum()
        {
            int baseMinimum = 10;
            int countExtra = mCommFunc.ConvertToInt(txtMasCount.Text) > 9 ? 1 : 0;
            countExtra = mCommFunc.ConvertToInt(txtMasCount.Text) > 999 ? 2 : countExtra;
            countExtra = mCommFunc.ConvertToInt(txtMasCount.Text) > 9999 ? 3 : countExtra;
            int prefixExtra = Math.Max(0, mCommFunc.ConvertToString(txtMasPrefix.Text).Length - 4);
            int postfixExtra = Math.Max(0, mCommFunc.ConvertToString(txtMasPostfix.Text).Length - 4);
            numMasDigits.Minimum = baseMinimum + countExtra + prefixExtra + postfixExtra;
        }


        private void txtMasPostfix_Validating(object sender, CancelEventArgs e)
        {
            UpdateNumMasDigitsMinimum();
        }

        private void txtExtCouponCode_Validating(object sender, CancelEventArgs e)
        {
            FillCouponExtendedValidity();
        }
        private void FillCouponExtendedValidity()
        {
            try
            {
                string strSql = @"select cmgdcm_id,cmgdcm_mode,cmgdcm_description,cmgdcm_fromdt,cmgdcm_fromdttm,cmgdcm_todt,cmgdcm_todttm,cmgdcm_remarks from campgndiscountmas join campgndiscountcoupons on cmgdcm_id=cmgdcpn_campgndiscountmasptr where cmgdcpn_couponnumber='" + txtExtCouponCode.Text + "' ";
                DataTable dtExtenddata = mGlobal.LocalDBCon.ExecuteQuery(strSql);
                if (dtExtenddata != null && dtExtenddata.Rows.Count > 0)
                {
                    txtExtDescription.Text = mCommFunc.ConvertToString(dtExtenddata.Rows[0]["cmgdcm_description"]);
                    txtExtRemarks.Text = mCommFunc.ConvertToString(dtExtenddata.Rows[0]["cmgdcm_remarks"]);
                    dtExtFrmdt.Value = Convert.ToDateTime(dtExtenddata.Rows[0]["cmgdcm_fromdt"]);
                    dtExtFrmdttime.Value = Convert.ToDateTime(dtExtenddata.Rows[0]["cmgdcm_fromdttm"]);
                    dtExtTodt.Value = Convert.ToDateTime(dtExtenddata.Rows[0]["cmgdcm_todt"]);
                    dtExtTodttime.Value = Convert.ToDateTime(dtExtenddata.Rows[0]["cmgdcm_todttm"]);
                    txtExtCouponCode.Tag = mCommFunc.ConvertToString(dtExtenddata.Rows[0]["cmgdcm_id"]);
                    txtExtMode.Text = mCommFunc.ConvertToString(dtExtenddata.Rows[0]["cmgdcm_mode"]);
                }
                else
                {
                    MessageBox.Show("Not Found");
                    ClearExtendItems();
                }
            }
            catch (Exception ex)
            { writeErrorLog(ex, "FillCouponExtendedValidity"); }
        }

       

        private void dtExtFrmdt_ValueChanged(object sender, EventArgs e)
        {
            DateTime originalDate = dtExtFrmdt.Value;
            string formattedDate = originalDate.ToString("dd-MM-yyyy");
            DateTime modifiedDate = DateTime.ParseExact(formattedDate, "dd-MM-yyyy", System.Globalization.CultureInfo.InvariantCulture);
            dtExtFrmdttime.Value = modifiedDate;
        }

        private void dtExtTodt_ValueChanged(object sender, EventArgs e)
        {
            DateTime originalDate = dtExtTodt.Value;
            string formattedDate = originalDate.ToString("dd-MM-yyyy 23:59:59");
            DateTime modifiedDate = DateTime.ParseExact(formattedDate, "dd-MM-yyyy 23:59:59", System.Globalization.CultureInfo.InvariantCulture);
            dtExtTodttime.Value = dtExtTodt.Value;
        }

        private void chkBranches_CheckStateChanged(object sender, EventArgs e)
        {
            if (chkBranches.Checked == true)
            {
                cbxBranches.Visible = true;
            }
            else
                cbxBranches.Visible = false;
        }

        private void cbxBranches_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                string strConstring = System.Configuration.ConfigurationSettings.AppSettings.Get("SqlConString");

                SqlConnection Conn = null;
                DbConnSql dbRemovecon = new DbConnSql(Conn);
                //DbConnSql dbConString = new DbConnSql(strUpdatedConstring);
                if (mCommFunc.ConvertToString(cbxBranches.SelectedItem) == "bangalore")
                {
                    strConstring = System.Configuration.ConfigurationSettings.AppSettings.Get("bangalore");
                    DbConnSql dbConString = new DbConnSql(strConstring);

                }
                else if (mCommFunc.ConvertToString(cbxBranches.SelectedItem) == "hyderabad")
                {
                    strConstring = System.Configuration.ConfigurationSettings.AppSettings.Get("hyderabad");
                    DbConnSql dbConString = new DbConnSql(strConstring);
                }
                else if (mCommFunc.ConvertToString(cbxBranches.SelectedItem) == "gurgaon")
                {
                    strConstring = System.Configuration.ConfigurationSettings.AppSettings.Get("gurgaon");
                    DbConnSql dbConString = new DbConnSql(strConstring);
                }
                else if (mCommFunc.ConvertToString(cbxBranches.SelectedItem) == "mumbai")
                {
                    strConstring = System.Configuration.ConfigurationSettings.AppSettings.Get("mumbai");
                    DbConnSql dbConString = new DbConnSql(strConstring);
                }
                GetModeCount();
            }
            catch (Exception ex)
            { }
        }
    }
}