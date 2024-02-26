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
    public partial class MasterCreater : Form
    {
        public MasterCreater()
        {
            InitializeComponent();
            GetFirms();
            UpdateNavButtonStatus();
            GetBranches();
        }
        Global mGlobal = new Global();
        CommFuncs mCommFunc = new CommFuncs();

       

        private void btnCreate_Click(object sender, EventArgs e)
        {
            try
            {
                switch (tabCtrl.SelectedTab.Name)
                { 
                    case "tbFirm":
                       
                        if (txtFirmCode.Text.Trim() != "")
                            FirmSubmit();
                        break;

                    case "tbBranch":
                        
                        if (txtBrchcode.Text.Trim() != "")
                            BranchCreate();
                        break;

                   
                    // Add more cases as needed

                    default:
                        AddressClearAll();
                        break;
                }
               

            }
            catch (Exception ex)
            {
                writeErrorLog(ex, "btnCreate_Click");
               
            }
             
            
        }
        private void FirmSubmit()
        {
            try
            {
                string strAddid = "";
                string strchkactive = "";
                if (chkFrmactive.Checked == true)
                    strchkactive = "Y";
                else
                    strchkactive = "N";
                if (CheckAlreadyExist("select * from firmmas   where f_code='" + txtFirmCode.Text.ToString().Trim() + "'") == false)
                {
                    string strSql = @"INSERT into rstAddress (adr_desc,adr_address1,adr_address2,adr_place,adr_zip,adr_phone1,adr_phone2,adr_mobile1,adr_mobile2,adr_emailid1,adr_emailid2,adr_website,adr_regno1,adr_regno2,adr_active) OUTPUT INSERTED.adr_id
                    values ('" + txtFrmname.Text.ToString().Trim() + "','" + txtAddress1.Text.ToString().Trim() + "','" + txtAddress2.Text.ToString().Trim() + "','" + txtPlace.Text.ToString().Trim() + "','" + txtZip.Text.ToString().Trim() + "','" + txtPhone1.Text.ToString().Trim() + "','" + txtPhone2.Text.ToString().Trim() + "','" + txtMob1.Text.ToString().Trim() + "','" + txtMob2.Text.ToString().Trim() + "','" + txtEmail1.Text.ToString().Trim() + "','" + txtEmail2.Text.ToString().Trim() + "','" + txtWebsite.Text.ToString().Trim() + "','" + txtRegno1.Text.ToString().Trim() + "','" + txtRegno2.Text.ToString().Trim() + "','Y')";
                    strAddid = AddressCreate(strSql);
                    if (strAddid != "")
                    {
                        if (CheckAlreadyExist("select * from firmmas where f_code='" + txtFirmCode.Text.ToString().Trim() + "'") == false)
                        {
                            strSql = @"INSERT into firmmas(f_code,f_desc,f_shortname,f_group,f_addressid,f_remarks,f_active,f_otherdetails1) values('" + txtFirmCode.Text.ToString().Trim() + "','" + txtFrmname.Text.ToString().Trim() + "','" + txtFrmshrtname.Text.ToString().Trim() + "',NULL,'" + strAddid + "','" + txtRemarks.Text.ToString().Trim() + "','" + strchkactive + "',NULL)";
                            int res = mGlobal.LocalDBCon.ExecuteNonQuery(strSql);
                            if (res <= 0)
                            {
                                MessageBox.Show("Error Occured On Firmmas");
                            }
                            else
                            {

                                MessageBox.Show("Submitted");
                                tabCtrl.SelectedTab = tbBranch;
                                UpdateNavButtonStatus();
                                GetFirms();
                                cbxFirm.SelectedValue = txtFirmCode.Text;
                                FirmClearAll(true);
                                AddressClearAll();
                            }
                        }

                    }
                }
                else
                {
                    string strSql = @"UPDATE rstAddress 
                  SET adr_desc = '" + txtFrmname.Text.ToString().Trim() + "'," +
                                      "adr_address1 = '" + txtAddress1.Text.ToString().Trim() + "'," +
                                      "adr_address2 = '" + txtAddress2.Text.ToString().Trim() + "'," +
                                      "adr_place = '" + txtPlace.Text.ToString().Trim() + "'," +
                                      "adr_zip = '" + txtZip.Text.ToString().Trim() + "'," +
                                      "adr_phone1 = '" + txtPhone1.Text.ToString().Trim() + "'," +
                                      "adr_phone2 = '" + txtPhone2.Text.ToString().Trim() + "'," +
                                      "adr_mobile1 = '" + txtMob1.Text.ToString().Trim() + "'," +
                                      "adr_mobile2 = '" + txtMob2.Text.ToString().Trim() + "'," +
                                      "adr_emailid1 = '" + txtEmail1.Text.ToString().Trim() + "'," +
                                      "adr_emailid2 = '" + txtEmail2.Text.ToString().Trim() + "'," +
                                      "adr_website = '" + txtWebsite.Text.ToString().Trim() + "'," +
                                      "adr_regno1 = '" + txtRegno1.Text.ToString().Trim() + "'," +
                                      "adr_regno2 = '" + txtRegno2.Text.ToString().Trim() + "'," +
                                      "adr_active='Y'" +
                                      "WHERE adr_id = '" + txtAddress1.Tag.ToString().Trim() + "'";
                    int res = mGlobal.LocalDBCon.ExecuteNonQuery(strSql);
                    if (res > 0)
                    {
                        res = 0;
                        strSql = @"UPDATE firmmas SET f_desc='" + txtFrmname.Text.ToString().Trim() + "',f_shortname='" + txtFrmshrtname.Text.ToString().Trim() + "',f_active='" + strchkactive + "' WHERE f_code='" + txtFirmCode.Text.ToString().Trim() + "'";
                        res = mGlobal.LocalDBCon.ExecuteNonQuery(strSql);
                        if (res <= 0)
                        {
                            MessageBox.Show("Error Occurred While Updating Firmmas");
                        }
                        else
                        {

                            MessageBox.Show("Updated");

                            GetFirms();
                            cbxFirm.SelectedValue = txtFirmCode.Text;
                            FirmClearAll(true);
                            AddressClearAll();
                        }
                    }
                }
            }
            catch(Exception ex){}
 
        }
        private string AddressCreate(string strSql)
        {
            try
            {
                DataTable dtResid = mGlobal.LocalDBCon.ExecuteQuery(strSql);
                if (dtResid.Rows.Count > 0)
                {
                    string strAddid = dtResid.Rows[0]["adr_id"].ToString();
                    return strAddid;
                }
                else 
                    return null;
            }
            catch(Exception ex)
            {
                writeErrorLog(ex, "AddressCreate");
                return null;
            }
            return null;
        }

        private void txtFirmCode_Validating(object sender, CancelEventArgs e)
        {
            try
            {

                FillFirm();

            }
            catch (Exception ex)
            {
                writeErrorLog(ex, "txtFirmCode_Validating");
            }

        }
        private void FillFirm()
        {
            try
            {
                string strSql = @"select * from firmmas left join rstAddress on f_addressid=adr_id where f_code='" + txtFirmCode.Text.ToString().Trim() + "'";
                DataTable dtFirmData = mGlobal.LocalDBCon.ExecuteQuery(strSql);
                if (dtFirmData.Rows.Count > 0)
                {
                    btnCreate.Text = "Update";
                    txtFrmname.Text = dtFirmData.Rows[0]["f_desc"].ToString();
                    txtFrmshrtname.Text = dtFirmData.Rows[0]["f_shortname"].ToString();
                    txtRemarks.Text = dtFirmData.Rows[0]["f_remarks"].ToString();
                    FillAddress(dtFirmData);

                }
                else
                {
                    AddressClearAll();
                    FirmClearAll(false);
                }
            }
            catch(Exception ex)
            { writeErrorLog(ex, "FillFirm"); }
        }
        private void FillAddress(DataTable dtAddress)
        {
            try
            {
                txtAddress1.Tag = dtAddress.Rows[0]["adr_id"].ToString();
                txtAddress1.Text = dtAddress.Rows[0]["adr_address1"].ToString();
                txtAddress2.Text = dtAddress.Rows[0]["adr_address2"].ToString();
                txtPlace.Text = dtAddress.Rows[0]["adr_place"].ToString();
                txtZip.Text = dtAddress.Rows[0]["adr_zip"].ToString();
                txtPhone1.Text = dtAddress.Rows[0]["adr_phone1"].ToString();
                txtPhone2.Text = dtAddress.Rows[0]["adr_phone2"].ToString();
                txtMob1.Text = dtAddress.Rows[0]["adr_mobile1"].ToString();
                txtMob2.Text = dtAddress.Rows[0]["adr_mobile2"].ToString();
                txtEmail1.Text = dtAddress.Rows[0]["adr_emailid1"].ToString();
                txtEmail2.Text = dtAddress.Rows[0]["adr_emailid2"].ToString();
                txtRegno1.Text = dtAddress.Rows[0]["adr_regno1"].ToString();
                txtRegno2.Text = dtAddress.Rows[0]["adr_regno2"].ToString();
                txtWebsite.Text = dtAddress.Rows[0]["adr_website"].ToString();
            }
            catch (Exception ex)
            { writeErrorLog(ex, "FillAddress"); }
        }
        private void GetFirms()
        {
            try
            {
                string strSql = @"select f_desc,f_code from firmmas";
                DataTable dtFirms = mGlobal.LocalDBCon.ExecuteQuery(strSql);
                if (dtFirms.Rows.Count > 0)
                {
                    cbxFirm.DataSource = dtFirms;
                    cbxFirm.DisplayMember = "f_desc";
                    cbxFirm.ValueMember = "f_code";
                    cbxDepFirm.DataSource = dtFirms;
                    cbxDepFirm.DisplayMember = "f_desc";
                    cbxDepFirm.ValueMember = "f_code";
                }
            }
            catch (Exception ex)
            {
                writeErrorLog(ex, "FillAddress");
            }
        }
        private void GetBranches()
        {
            try
            {
                string strSql = @"select br_code,br_desc from branchmas";
                DataTable dtBranches = mGlobal.LocalDBCon.ExecuteQuery(strSql);
                if (dtBranches.Rows.Count > 0)
                {
                    cbxDepBranch.DataSource = dtBranches;                   
                    cbxDepBranch.DisplayMember = "br_desc";
                    cbxDepBranch.ValueMember = "br_code";
                }
            }
            catch (Exception ex)
            {
                writeErrorLog(ex, "FillAddress");
            }
        }

        private void btnFirmClear_Click(object sender, EventArgs e)
        {
            switch (tabCtrl.SelectedTab.Name)
            {
                case "tbBranch":

                    BrchClearAll(true);
                    AddressClearAll();
                    break;

                case "tbFirm":
                    FirmClearAll(true);
                    AddressClearAll();
                    break;

                // Add more cases as needed

                default:
                    AddressClearAll();
                    break;
            }
        }
        private void FirmClearAll(bool Clear)
        {
            if(Clear==true)
            txtFirmCode.Text = "";
            txtAddress1.Text = "";
            txtFrmname.Text = "";
            txtPlace.Text = "";
            txtFrmshrtname.Text = "";
            btnCreate.Text = "Create";
            txtAddress1.Tag = "";
        }
        private void BrchClearAll(bool Clear)
        {
            if (Clear == true)
            txtBrchcode.Text = "";
            txtBrchname.Text = "";
            txtBrchaddress.Text = "";
            btnCreate.Text = "Create";
            cbxFirm.SelectedValue = "-1";
           
        }
        private void AddressClearAll()
        {
            txtAddress1.Text = "";
            txtAddress2.Text = "";
            txtPlace.Text = "";
            txtZip.Text = "";
            txtPhone1.Text = "";
            txtPlace.Text = "";
            txtZip.Text = "";
            txtPhone1.Text = "";
            txtPhone2.Text = "";
            txtMob1.Text = "";
            txtMob2.Text = "";
            txtEmail1.Text = "";
            txtEmail2.Text = "";
            txtRegno1.Text = "";
            txtRegno2.Text = "";
            txtWebsite.Text = "";
            txtRemarks.Text = "";
        }
 
      
        private void BranchCreate()
        {  try
            {
                string strAddid = "";
                string strSql = @"INSERT into rstAddress (adr_desc,adr_address1,adr_address2,adr_place,adr_zip,adr_phone1,adr_phone2,adr_mobile1,adr_mobile2,adr_emailid1,adr_emailid2,adr_website,adr_regno1,adr_regno2,adr_remarks,adr_active) OUTPUT INSERTED.adr_id
                    values ('" + txtBrchname.Text.ToString().Trim() + "','" + txtAddress1.Text.ToString().Trim() + "','" + txtAddress2.Text.ToString().Trim() + "','" + txtPlace.Text.ToString().Trim() + "','" + txtZip.Text.ToString().Trim() + "','" + txtPhone1.Text.ToString().Trim() + "','" + txtPhone2.Text.ToString().Trim() + "','" + txtMob1.Text.ToString().Trim() + "','" + txtMob2.Text.ToString().Trim() + "','" + txtEmail1.Text.ToString().Trim() + "','" + txtEmail2.Text.ToString().Trim() + "','" + txtWebsite.Text.ToString().Trim() + "','" + txtRegno1.Text.ToString().Trim() + "','" + txtRegno2.Text.ToString().Trim() + "','"+txtRemarks.Text.ToString().Trim()+"','Y')";
                strAddid = AddressCreate(strSql);
               
               if (CheckAlreadyExist("select * from branchmas where br_code='" + txtBrchcode.Text.ToString().Trim() + "'") == false)
               {
                        if (strAddid != "")
                        {
                        strSql = @"INSERT into branchmas (br_code,br_desc,br_addptr,br_firmptr,br_active) values ('" + txtBrchcode.Text.ToString() + "','" + txtBrchname.Text.ToString() + "','" + strAddid + "','" + cbxFirm.SelectedValue.ToString() + "','Y')";
                        int res = mGlobal.LocalDBCon.ExecuteNonQuery(strSql);
                        if (res <= 0)
                        {
                            MessageBox.Show("Error Occurred On Branchmas");
                        }
                        else
                        {
                            MessageBox.Show("Submitted");
                            BrchClearAll(true);
                            AddressClearAll();
                        }
                        }
                   

                }
                else
                {
                     strSql = @"UPDATE rstAddress 
                  SET adr_desc = '" + txtBrchname.Text.ToString().Trim() + "'," +
                                      "adr_address1 = '" + txtAddress1.Text.ToString().Trim() + "'," +
                                      "adr_address2 = '" + txtAddress2.Text.ToString().Trim() + "'," +
                                      "adr_place = '" + txtPlace.Text.ToString().Trim() + "'," +
                                      "adr_zip = '" + txtZip.Text.ToString().Trim() + "'," +
                                      "adr_phone1 = '" + txtPhone1.Text.ToString().Trim() + "'," +
                                      "adr_phone2 = '" + txtPhone2.Text.ToString().Trim() + "'," +
                                      "adr_mobile1 = '" + txtMob1.Text.ToString().Trim() + "'," +
                                      "adr_mobile2 = '" + txtMob2.Text.ToString().Trim() + "'," +
                                      "adr_emailid1 = '" + txtEmail1.Text.ToString().Trim() + "'," +
                                      "adr_emailid2 = '" + txtEmail2.Text.ToString().Trim() + "'," +
                                      "adr_website = '" + txtWebsite.Text.ToString().Trim() + "'," +
                                      "adr_regno1 = '" + txtRegno1.Text.ToString().Trim() + "'," +
                                      "adr_regno2 = '" + txtRegno2.Text.ToString().Trim() + "'," +
                                      "adr_remarks='"+txtRemarks.Text.ToString().Trim()+"',"+
                                      "adr_active='Y'" +
                                      "WHERE adr_id = '" + txtAddress1.Tag.ToString().Trim() + "'";
                    int res = mGlobal.LocalDBCon.ExecuteNonQuery(strSql);
                    if (res > 0)
                    {
                        res = 0;
                        strSql = @"UPDATE branchmas SET br_desc='" + txtBrchname.Text.ToString() + "', br_addptr='" + strAddid + "', br_firmptr='" + cbxFirm.SelectedValue.ToString() + "' WHERE br_code='" + txtBrchcode.Text.ToString().Trim() + "'";
                         res = mGlobal.LocalDBCon.ExecuteNonQuery(strSql);
                        if (res <= 0)
                        {
                            MessageBox.Show("Error Occurred While Updating branchmas");
                        }
                        else
                        {

                            MessageBox.Show("Updated");
                          
                            BrchClearAll(true);
                            AddressClearAll();
                        }
                    }
                }
            }
            catch(Exception ex)
        {
            }
        }
        private bool CheckAlreadyExist(string strSql)
        {
            try
            {
                DataTable dtData = mGlobal.LocalDBCon.ExecuteQuery(strSql);
                if (dtData.Rows.Count <= 0)
                {
                    return false;
                }
                else
                    return true;
            }
            catch (Exception ex)
            {
                writeErrorLog(ex, "CheckAlreadyExist");
            return false;
            }
            return false;
        }

        private void txtBrchcode_Validating(object sender, CancelEventArgs e)
        {
            try
            {

                FillBranch();
            }
            catch (Exception ex)
            { writeErrorLog(ex, "txtBrchcode_Validating"); }
        }
        private void FillBranch()
        {
            try
            {
                string strSql = @"select * from branchmas left join rstAddress on br_addptr=adr_id where br_code='" + txtBrchcode.Text.ToString().Trim() + "'";
                DataTable dtBranchdata = mGlobal.LocalDBCon.ExecuteQuery(strSql);
                if (dtBranchdata.Rows.Count > 0)
                {
                    btnCreate.Text = "Update";
                    FillAddress(dtBranchdata);
                    txtRemarks.Text = dtBranchdata.Rows[0]["adr_remarks"].ToString();
                    txtBrchname.Text = dtBranchdata.Rows[0]["br_desc"].ToString();
                    cbxFirm.SelectedValue = dtBranchdata.Rows[0]["br_firmptr"].ToString();

                }
                else
                {
                    BrchClearAll(false);
                    AddressClearAll();
                }
            }
            catch (Exception ex)
            { writeErrorLog(ex, "txtBrchcode_Validating"); }
        }
       

        private void tabCtrl_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                AddressClearAll();
                switch (tabCtrl.SelectedTab.Name)
                {
                    case "tbBranch":
                        cbxFirm.SelectedValue = "-1";
                        tabCtrl.TabPages[tabCtrl.SelectedIndex].Controls.Add(pnlAddress);
                        tabCtrl.TabPages[tabCtrl.SelectedIndex].Controls.Add(pnlBtns);
                        if (txtBrchcode.Text.Trim() != "")
                            FillBranch();
                        break;

                    case "tbFirm":
                        tabCtrl.TabPages[tabCtrl.SelectedIndex].Controls.Add(pnlAddress);
                        tabCtrl.TabPages[tabCtrl.SelectedIndex].Controls.Add(pnlBtns);
                        if (txtFirmCode.Text.Trim() != "")
                            FillFirm();
                        break;
                    case "tbDepartment":
                        
                        tabCtrl.TabPages[tabCtrl.SelectedIndex].Controls.Add(pnlBtns);
                       
                        break;

                    // Add more cases as needed

                    default:
                        AddressClearAll();
                        break;
                }
                UpdateNavButtonStatus();
            }
            catch (Exception ex)
            {
                writeErrorLog(ex, "tabCtrl_SelectedIndexChanged");
            }
        }


        private void btnFrmexit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void txtZip_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;

            if (!char.IsNumber(ch) && !char.IsLetter(ch) && ch != 8 && ch != 46)  //8 is Backspace key; 46 is Delete key. This statement accepts dot key. 
            //if (!char.IsLetterOrDigit(ch) && !char.IsLetter(ch) && ch != 8 && ch != 46)   //This statement accepts dot key. 
            {
                e.Handled = true;
                MessageBox.Show("Only accept digital character or letter.");
            }
        }

        private void btnBrchexit_Click(object sender, EventArgs e)
        {
            Close();
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

        private void UpdateNavButtonStatus()
        {
            btnNext.Enabled = tabCtrl.SelectedIndex < tabCtrl.TabCount - 1;
            btnPrevious.Enabled = tabCtrl.SelectedIndex > 0;
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            tabCtrl.SelectedIndex++;
            UpdateNavButtonStatus();
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            tabCtrl.SelectedIndex--;
            UpdateNavButtonStatus();
        }

        

       


      

      
    }
}