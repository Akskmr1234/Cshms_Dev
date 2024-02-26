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
    public partial class RCMCoreSettings : Form
    {
        public RCMCoreSettings()
        {
            InitializeComponent();
            Get_rcmcoresettings();
            cbxType.SelectedItem = "CLINICIAN";
        }
        Global mGlobal = new Global();
        CommFuncs mCommfunc = new CommFuncs();
        string mFirmptr = "1";
        string mBranchptr = "1";
        private void txtClinician_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyValue == mGlobal.SearchKey && e.Modifiers == Keys.Control)
                { ShowOpSearch(ref txtCode); }
            }
            catch (Exception ex)
            { writeErrorLog(ex, "txtClinician_KeyDown"); }
        }
        private void ShowOpSearch(ref TextBox _txt)
        {
            // Search Settings 
            Search Searchfrm = new Search();

            Searchfrm.DefSearchFldIndex = 1;
            Searchfrm.DefSearchText = _txt.Text;
            Searchfrm.Query = "select do_code,do_name,do_regno1,do_regno2 from doctor";
            Searchfrm.FilterCond = "";
            //Searchfrm.LimitRows = 100;
            Searchfrm.ReturnFldIndex = 0;
            Searchfrm.ColumnHeader = "Doctor Code|Name|Regno 1 |Regno 2 ";
            Searchfrm.ColumnWidth = "100|100|100|100";
            Searchfrm.ShowDialog();
            // Search value returns
            if (!Searchfrm.Cancelled) // whether not clicked on cancel button in search screen
                _txt.Text = Searchfrm.ReturnValue;
        }
        
        private void txtClinician_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                if (cbxType.SelectedItem == "CLINICIAN")
                    Validate_clinician();
            }
            catch (Exception ex)
            { writeErrorLog(ex, "txtClinician_Validating"); }
        }

        /// <summary>
        /// Assign values into textboxes for clinician
        /// </summary>
        private void Validate_clinician()
        {
            try
            {
                string strDocode = txtCode.Text.ToString();
                string strSql = @"SELECT * FROM doctor left join rcmcoresettings on do_code=type_value where do_code='" + strDocode + "'";
                DataTable dtdata = mGlobal.LocalDBCon.ExecuteQuery(strSql);
                if (dtdata.Rows.Count > 0)
                {
                    txtName.Text = dtdata.Rows[0]["do_name"].ToString();                    
                    txtDetails.Text = "Qualification    :" + dtdata.Rows[0]["do_qualfication"].ToString();
                    txtDetails.Text += Environment.NewLine + "Designation     :" + dtdata.Rows[0]["do_desg"].ToString();
                    txtRegno1.Text = string.IsNullOrEmpty(dtdata.Rows[0]["do_regno1"].ToString()) ? dtdata.Rows[0]["regno1"].ToString() : dtdata.Rows[0]["do_regno1"].ToString();
                    txtRegno2.Text = string.IsNullOrEmpty(dtdata.Rows[0]["do_regno2"].ToString()) ? dtdata.Rows[0]["regno2"].ToString() : dtdata.Rows[0]["do_regno2"].ToString();
                    txtUsername.Text = dtdata.Rows[0]["user_name"].ToString();
                    txtPassword.Text = dtdata.Rows[0]["password"].ToString();
                    txtSno.Text = dtdata.Rows[0]["do_slno"].ToString();
                    if (dtdata.Rows[0]["do_active"].ToString() == "Y")
                        chkActive.Checked = true;
                    else
                        chkActive.Checked = false;

                }
                else
                {
                    MessageBox.Show("Data Not Found \n\nPlease Create Doctor First");
                    ClearAll();
                    txtCode.Enabled = true;

                }
            }
            catch (Exception ex)
            {
                writeErrorLog(ex, "Validate_clinician");
            }
        }
        /// <summary>
        /// Assign values into textboxes for hospital
        /// </summary>
        private void Validate_hospital()
        {
            try
            {
                string strDocode = txtCode.Text.ToString();
                string strSql = @"select * from firmmas where f_code='" + strDocode + "'";
                DataTable dtdata = mGlobal.LocalDBCon.ExecuteQuery(strSql);
                if (dtdata.Rows.Count > 0)
                {
                    txtName.Text = dtdata.Rows[0]["f_shortname"].ToString();
                    txtDetails.Text = " Name              :" + dtdata.Rows[0]["f_shortname"].ToString();
                    txtDetails.Text += Environment.NewLine + " Description    :" + dtdata.Rows[0]["f_desc"].ToString();
                    txtRegno1.Text = dtdata.Rows[0]["do_regno1"].ToString();
                    txtRegno2.Text = dtdata.Rows[0]["do_regno2"].ToString();
                }
            }
            catch (Exception ex)
            {
                writeErrorLog(ex, "Validate_hospital");
            }
        }


        /// <summary>
        /// insertion and updateion for 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                string strChkvalue = "";
                if(chkActive.Checked==true)
                strChkvalue="Y";
                else
                    strChkvalue="N";
                if (CheckAlreadyExist() == false)
                {
                    string strSql = @"INSERT INTO rcmcoresettings (type,type_value,url,user_name,password,active,remarks,other_details1,firm_ptr,branch_ptr,description,serial_number,regno1,regno2)
VALUES ('" + cbxType.SelectedItem.ToString() + "', '" + txtCode.Text.ToString() + "', NULL,'" + txtUsername.Text.ToString() + "','" + txtPassword.Text.ToString() + "','" + strChkvalue + "','" + txtRemarks.Text.ToString() + "',NULL,'" + mFirmptr + "','" + mBranchptr + "','"+txtName.Text.ToString()+"','"+txtSno.Text.ToString()+"','"+txtRegno1.Text.ToString()+"','"+txtRegno2.Text.ToString()+"')";

                    int res = mGlobal.LocalDBCon.ExecuteNonQuery(strSql);
            
                    if (res <= 0)
                    {
                        MessageBox.Show("Error Occured");

                    }
                    else
                    {
                        MessageBox.Show("Success");
                        ClearAll();
                        Get_rcmcoresettings();

                    }
                }
                else
                {
                    string strSql = @"update rcmcoresettings set type='" + cbxType.SelectedItem.ToString() + "',type_value='" + txtCode.Text.ToString() + "',user_name='" + txtUsername.Text.ToString() + "',password='" + txtPassword.Text.ToString() + "',active='" + strChkvalue + "',remarks='"+txtRemarks.Text.ToString()+"',firm_ptr='"+mFirmptr+"',branch_ptr='"+mBranchptr+"',description='"+txtName.Text.ToString()+"',serial_number='"+txtSno.Text.ToString()+"',regno1='"+txtRegno1.Text.ToString()+"',regno2='"+txtRegno2.Text.ToString()+"' where  id='"+txtCode.Tag.ToString()+"'";
                    int res = mGlobal.LocalDBCon.ExecuteNonQuery(strSql);
                    
                    if (res <= 0)
                    {
                        MessageBox.Show("Error Occured");

                    }
                    else
                    {
                        MessageBox.Show("Updated");
                        ClearAll();
                        Get_rcmcoresettings();
                    }
                }
            }
            catch(Exception ex)
            {
                writeErrorLog(ex, "btnSubmit_Click");
            }
        }
        /// <summary>
        /// Get values from rcmcoresettings
        /// </summary>
        private void Get_rcmcoresettings()
        {
            try
            {
                string strSql = @"select id,ROW_NUMBER() OVER (ORDER BY serial_number) AS sl_no, type as Type,type_value as Code,do_name as Name,regno1 as Regno1,regno2 as Regno2,user_name as UserName,password as Password,do_qualfication as Qualification,do_desg as Designation,serial_number as Serialno,active as Active,remarks as Remarks from rcmcoresettings left join doctor on type_value=do_code order by Serialno";
                DataTable dtrcmcoresettings = mGlobal.LocalDBCon.ExecuteQuery(strSql);
                dgvrcmcoresettings.DataSource = dtrcmcoresettings;
                dgvrcmcoresettings.Columns["id"].Visible = false;
                dgvrcmcoresettings.Columns["UserName"].Visible = false;
                dgvrcmcoresettings.Columns["Password"].Visible = false;
                dgvrcmcoresettings.Columns["Code"].Visible = false;

           
            }
            catch (Exception ex)
            {
                writeErrorLog(ex, "Get_rcmcoresettings");
            }
         


        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearAll();
        }
        private void ClearAll()
        {
            txtName.Text = "";
            txtCode.Text = "";
            txtDetails.Text = "";
            txtPassword.Text = "";
            txtRegno1.Text = "";
            txtRegno2.Text = "";
            txtRemarks.Text = "";
            txtSno.Text = "";
            txtUsername.Text = "";
            chkActive.Checked = false;
            txtCode.Tag = "";
            
        }

       /// <summary>
       /// Checking its already existing or not
       /// </summary>
       /// <returns></returns>
        private bool CheckAlreadyExist()
        {
            try
            {
               string strSql = @"select * from rcmcoresettings where id='"+txtCode.Tag.ToString()+"'";
                DataTable dtRes = mGlobal.LocalDBCon.ExecuteQuery(strSql);
                if (dtRes.Rows.Count<=0)
                    return false;
                else
                    return true;
            }
            catch (Exception ex)
            {
                writeErrorLog(ex, "CheckAlreadyExist");
            }
            return false;
        }

        private void cbxType_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                ClearAll();
                if (cbxType.SelectedItem.ToString() == "CLINICIAN")
                    txtCode.Enabled = true;
                if (cbxType.SelectedItem.ToString() == "HOSPITAL")
                {
                    MessageBox.Show("Not Available For Hospital");

                }
            }
            catch (Exception ex)
            { writeErrorLog(ex, "cbxType_SelectedValueChanged"); }
        }
        /// <summary>
        /// Fill textboxes when double clicked gridview
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvrcmcoresettings_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                ClearAll();
                if (dgvrcmcoresettings.SelectedCells.Count > 0)
                {
                    cbxType.SelectedItem = Convert.ToString(dgvrcmcoresettings.CurrentRow.Cells["Type"].Value);
                    txtCode.Text = Convert.ToString(dgvrcmcoresettings.CurrentRow.Cells["Code"].Value);
                    if (Convert.ToString(dgvrcmcoresettings.CurrentRow.Cells["Active"].Value) == "Y")
                        chkActive.Checked = true;
                    else
                        chkActive.Checked = false;
                    txtRemarks.Text = Convert.ToString(dgvrcmcoresettings.CurrentRow.Cells["remarks"].Value);
                    txtCode.Tag = Convert.ToString(dgvrcmcoresettings.CurrentRow.Cells["id"].Value);
                    txtUsername.Text = mCommfunc.ConvertToString(dgvrcmcoresettings.CurrentRow.Cells["UserName"].Value);
                    txtPassword.Text = mCommfunc.ConvertToString(dgvrcmcoresettings.CurrentRow.Cells["Password"].Value);
                    txtName.Text = mCommfunc.ConvertToString(dgvrcmcoresettings.CurrentRow.Cells["Name"].Value);
                    txtRegno1.Text = mCommfunc.ConvertToString(dgvrcmcoresettings.CurrentRow.Cells["Regno1"].Value);
                    txtRegno2.Text = mCommfunc.ConvertToString(dgvrcmcoresettings.CurrentRow.Cells["Regno2"].Value);
                    txtSno.Text = mCommfunc.ConvertToString(dgvrcmcoresettings.CurrentRow.Cells["Serialno"].Value);
                    txtDetails.Text = "Qualification    :" + mCommfunc.ConvertToString(dgvrcmcoresettings.CurrentRow.Cells["Qualification"].Value);
                    txtDetails.Text += Environment.NewLine + "Designation   :" + mCommfunc.ConvertToString(dgvrcmcoresettings.CurrentRow.Cells["Designation"].Value);




                }
                if (cbxType.SelectedItem.ToString() == "HOSPITAL")
                    ClearAll();
            }
            catch (Exception ex)
            {
                writeErrorLog(ex, "dgvrcmcoresettings_CellDoubleClick");
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

     

       
    }
}