using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.IO;
using System.Data.SqlClient;
using System.Configuration;

namespace CsHms.Akshay
{
    public partial class OpMerge : Form
    {
        CommFuncs mCommFuncs = new CommFuncs();
        Global mGlobal = new Global();
        public OpMerge()
        {
            InitializeComponent();          
            
        }
        
        
        private void txtA_opno_Validating(object sender, CancelEventArgs e)
        {
            if (txtA_opno.Text != "")
            {               
                if (getActualData(txtA_opno.Text) == false)
                {
                    e.Cancel = true;
                    return;
                }
                
            }

        }
        private void txtD_opno_Validating(object sender, CancelEventArgs e)
        {
            if (txtD_opno.Text != "")
            {           
                
                    if (getDuplicateData(txtD_opno.Text) == false)
                    {
                        e.Cancel = true;
                        return;
                    }
              
                 
            }
        }
        /// <summary>
        /// Get data into datatable
        /// </summary>
        /// <param name="Opno"></param>
        /// <returns></returns>
        private DataTable GetData(string Opno)
        {
            try
            {
                String sqlstr = @"SELECT opb_name as 'PatientName', opb_ipno as 'IPno', opb_add1 as 'Add1', opb_add2 as 'Add2', opb_place as 'Place', opb_gender as 'Gender'
                        , opb_age as 'Age', ptc_desc as 'PatientCategory', opb_patcatptr as 'PatCatCode', do_name as 'Doctor', opb_mobile as 'MobileNO', opb_email as 'Email', opb_canflg as 'Cnaflg'
                        FROM opbill
                        LEFT JOIN patientcategory ON (ptc_code = opb_patcatptr)
                        LEFT JOIN billingtypes ON (btyp_code = opb_btype AND btyp_module = 'IP')
                        LEFT JOIN ipadmit ON (ia_ipno = opb_ipno)
                        LEFT JOIN opreg ON (ia_opno = op_no)
                        LEFT JOIN phm_sales ON (s_ipno = ia_ipno)
                        LEFT JOIN doctor ON (do_code = opb_doctorptr) WHERE opb_opno='" + Opno + "'";

                // Get the datas into a datatable
                DataTable dtdata = mGlobal.LocalDBCon.ExecuteQuery(sqlstr);
                return dtdata;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
                writeErrorLog(ex, "GetData");
                return null; // or return an empty DataTable if that makes sense for your application
            }
        }
        /// <summary>
        /// Set values into actual data
        /// </summary>
        /// <param name="Opno"></param>
        /// <returns></returns>
        private bool getActualData(string Opno)
        {
            try
            {
                // SQL query to retrieve bill header information based on the provided IP number.
                DataTable dtactualdata = GetData(Opno);
              
                if (dtactualdata.Rows.Count <= 0)
                {
                    MessageBox.Show("Data not found");
                    txtA_opno.Text = "";
                    ClearAllActual();
                    return false;
                }
                //checking the bill is cancelled or not with Cnaflg column
                if (mCommFuncs.ConvertToString(dtactualdata.Rows[0]["Cnaflg"]) == "Y")
                {
                    MessageBox.Show("Cancelled bill");
                    return false;
                }

                // Populate UI fields with retrieved data.
                txtA_ipno.Text = mCommFuncs.ConvertToString(dtactualdata.Rows[0]["IPno"]);
                txtA_ipno.Tag = mCommFuncs.ConvertToString(dtactualdata.Rows[0]["IPno"]);
                txtA_PatientNm.Text = mCommFuncs.ConvertToString(dtactualdata.Rows[0]["PatientName"]);
                txtA_Address.Text = mCommFuncs.ConvertToString(dtactualdata.Rows[0]["Add1"]).Trim() + "," +
                    mCommFuncs.ConvertToString(dtactualdata.Rows[0]["Add2"]).Trim() + "," +
                    mCommFuncs.ConvertToString(dtactualdata.Rows[0]["Place"]).Trim();

                txtA_Gender.Text = mCommFuncs.ConvertToString(dtactualdata.Rows[0]["Gender"]);
                txtA_Age.Text = mCommFuncs.ConvertToString(dtactualdata.Rows[0]["Age"]);
                txtA_Category.Text = mCommFuncs.ConvertToString(dtactualdata.Rows[0]["PatientCategory"]);
                txtA_Category.Tag = mCommFuncs.ConvertToString(dtactualdata.Rows[0]["PatCatCode"]);
                txtA_Doctor.Text = mCommFuncs.ConvertToString(dtactualdata.Rows[0]["Doctor"]);
                txtA_phone.Text = mCommFuncs.ConvertToString(dtactualdata.Rows[0]["MobileNo"]);
                txtA_email.Text = mCommFuncs.ConvertToString(dtactualdata.Rows[0]["Email"]);

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
                writeErrorLog(ex, "getActualData");
            }

            return false;
        }

/// <summary>
/// set values into duplicate data
/// </summary>
/// <param name="OPno"></param>
/// <returns></returns>
        bool getDuplicateData(string OPno)
        {
            try
            {
                DataTable dtdupdata = GetData(OPno);
              
                if (dtdupdata.Rows.Count <= 0)
                {
                    MessageBox.Show("Data not found");
                    txtD_opno.Text = "";
                    ClearAllDup();
                    return false;
                }
                //checking the bill is cancelled or not with Cnaflg column
                if (mCommFuncs.ConvertToString(dtdupdata.Rows[0]["Cnaflg"]) == "Y")
                {
                    MessageBox.Show("Cancelled bill");
                    return false;
                }
                if (txtA_opno.Text == txtD_opno.Text)
                {
                    MessageBox.Show("Enter a different OP Number");
                    txtD_opno.Text = "";
                    return false;
                }
                // Populate UI fields with retrieved data.               
                //txtPhmBillNo.Text = mCommFuncs.ConvertToString(dtData.Rows[0]["PharmacyBillNo"]);
                //txtD_BillType.Text = mCommFuncs.ConvertToString(dtData.Rows[0]["BIllType"]);
                //txtD_BillType.Tag = mCommFuncs.ConvertToString(dtData.Rows[0]["BillTypeCode"]);
                txtD_ipno.Text = mCommFuncs.ConvertToString(dtdupdata.Rows[0]["IPno"]);
                txtD_ipno.Tag = mCommFuncs.ConvertToString(dtdupdata.Rows[0]["IPno"]);
                txtD_PatientNm.Text = mCommFuncs.ConvertToString(dtdupdata.Rows[0]["PatientName"]);
                txtD_Address.Text = mCommFuncs.ConvertToString(dtdupdata.Rows[0]["Add1"]).Trim() + "," +
                mCommFuncs.ConvertToString(dtdupdata.Rows[0]["Add2"]).Trim() + "," +
                mCommFuncs.ConvertToString(dtdupdata.Rows[0]["Place"]).Trim();
                txtD_Gender.Text = mCommFuncs.ConvertToString(dtdupdata.Rows[0]["Gender"]);
                txtD_Age.Text = mCommFuncs.ConvertToString(dtdupdata.Rows[0]["Age"]);
                txtD_Category.Text = mCommFuncs.ConvertToString(dtdupdata.Rows[0]["PatientCategory"]);
                txtD_Category.Tag = mCommFuncs.ConvertToString(dtdupdata.Rows[0]["PatCatCode"]);
                txtD_Doctor.Text = mCommFuncs.ConvertToString(dtdupdata.Rows[0]["Doctor"]);
                txtD_phone.Text = mCommFuncs.ConvertToString(dtdupdata.Rows[0]["MobileNo"]);
                txtD_email.Text = mCommFuncs.ConvertToString(dtdupdata.Rows[0]["Email"]);
               
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
                writeErrorLog(ex, "getDuplicateData");
            }

            return false;
        }

        private void btnClearall_Click(object sender, EventArgs e)
        {
            ClearAllActual();
            ClearAllDup();
        }

        void ClearAllActual()
        {
           
                txtA_Address.Text = "";                
                txtA_ipno.Text = "";
                txtA_opno.Text = "";
                txtA_PatientNm.Text = "";
                txtA_Gender.Text = "";
                txtA_Age.Text = "";
                txtA_Category.Text = "";
                txtA_Doctor.Text = "";
                txtA_email.Text = "";
                txtA_phone.Text = "";
               
            
        }
        void ClearAllDup()
        {
            
                txtD_Address.Text = "";
                txtD_ipno.Text = "";
                txtD_opno.Text = "";
                txtD_PatientNm.Text = "";
                txtD_Gender.Text = "";
                txtD_Age.Text = "";
                txtD_Category.Text = "";
                txtD_Doctor.Text = "";
                txtD_phone.Text = "";
                txtD_email.Text = "";
            
        }
        /// <summary>
        /// Op search window
        /// </summary>
        /// <param name="_txt"></param>
        private void ShowOpSearch(ref TextBox _txt)
        {
            // Search Settings 
            Search Searchfrm = new Search();

            Searchfrm.DefSearchFldIndex = 1;
            Searchfrm.DefSearchText = _txt.Text;
            Searchfrm.Query = "select opb_opno,opb_name,opb_add1,opb_add2,opb_place,opb_gender,opb_age from opbill";
            Searchfrm.FilterCond = "";
            //Searchfrm.LimitRows = 100;
            Searchfrm.ReturnFldIndex = 0;
            Searchfrm.ColumnHeader = "Op No|Name|Address1 |Address2|Place|Gender|Age ";
            Searchfrm.ColumnWidth = "130|150|150|150|150|100|100";
            Searchfrm.ShowDialog();
            // Search value returns
            if (!Searchfrm.Cancelled) // whether not clicked on cancel button in search screen
                _txt.Text = Searchfrm.ReturnValue;
        }        

        private void btnClose_Click(object sender, EventArgs e)
        {
            ClearAllActual();
            ClearAllDup();
            Close();
        }


        string Mode = "OPM";
        string Type = "QRY";
        /// <summary>
        /// Merging of op
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnMerge_Click(object sender, EventArgs e)
        {
            try
            {
                string Mergemessage = "";
                DialogResult dr=new DialogResult();

                if (mCommFuncs.ConvertToString(txtA_email.Text) != mCommFuncs.ConvertToString(txtD_email.Text))
                {
                    Mergemessage = " Email";
                }

                if (  mCommFuncs.ConvertToString(txtA_phone.Text) != mCommFuncs.ConvertToString(txtD_phone.Text))
                {
                    if(Mergemessage!="")
                    Mergemessage += ",Phone";
                    else
                    Mergemessage = "Phone";
                }
                if (Mergemessage != "")
                      Mergemessage+="does not match in actual and duplicate,";
                dr = MessageBox.Show(Mergemessage.ToString() + "\n This action can't undone, Do you want to continue?", "Confirm", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
               // dr = MessageBox.Show("The Results Cant Be Changed Do You Want To Continue", "Confirm", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                
                    try
                    {
                        CoreScripts script = new CoreScripts();

                        DataTable dtqueries = script.getQueries(Mode, Type);
                        mGlobal.LocalDBCon.BeginTrans();
                        int res = 0;

                        foreach (DataRow dtRow in dtqueries.Rows)
                        {
                            string strqueries = dtRow["script"].ToString();
                            string strTableName = dtRow["other_details1"].ToString();

                            BackupTable(strTableName);

                            // Update the query dynamically
                            string actualOpNO = txtA_opno.Text;
                            string duplicateOpNO = txtD_opno.Text;
                            strqueries = strqueries.Replace("~ActualOpNO~;", actualOpNO).Replace("~DuplicateOpNO~;", duplicateOpNO);

                            res = mGlobal.LocalDBCon.ExecuteNonQuery(strqueries);

                            if (res <= 0)
                            {
                                MessageBox.Show("Error Occurred");
                                mGlobal.LocalDBCon.RollbackTrans(true);
                                return; // exit the method if an error occurs
                            }
                        }

                        if (res > 0)
                        {
                            mGlobal.LocalDBCon.CommitTrans();
                            MessageBox.Show("Merge Completed");
                        }
                        else
                        {
                            MessageBox.Show("Error Occurred while executing queries. Merge aborted.");
                        }
                    }
                    catch (Exception ex)
                    {
                        mGlobal.LocalDBCon.RollbackTrans(true);
                        writeErrorLog(ex, "btnMerge_Click");
                        MessageBox.Show(ex.Message.ToString());
                    }
                }
            
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
                mGlobal.LocalDBCon.RollbackTrans(true);
                writeErrorLog(ex, "btnMerge_Click");
                // Handle the outer exception, log it, or perform additional actions here
            }
        }

        private bool ShowConfirmation(string message)
        {
            DialogResult result = MessageBox.Show(message, "Confirm", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
            return result == DialogResult.Yes;
        }
        /// <summary>
        /// Backup table function
        /// </summary>
        /// <param name="strTablename"></param>
        /// <returns></returns>
        private bool BackupTable(string strTablename)
        {
            try
            {

                string currentDateTime = DateTime.Now.ToString("ddMMMyyyy")+txtD_opno.Text.ToString();

                int res = mGlobal.LocalDBCon.ExecuteNonQuery(@"select * into " + strTablename.ToString() + "_Backup_" + currentDateTime + " from " + strTablename.ToString());
                if (res <= 0)
                {
                    MessageBox.Show("backup '" + strTablename + "' failed");
                    return false;
                }
                else
                    return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
                writeErrorLog(ex, "BackupTable");

            }
            return false;
        }

        //Events for invoking search window



        private void txtD_opno_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyValue == mGlobal.SearchKey && e.Modifiers == Keys.Control)
            { ShowOpSearch(ref txtD_opno); }
        }
        private void txtA_opno_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyValue == mGlobal.SearchKey && e.Modifiers == Keys.Control)
            { ShowOpSearch(ref txtA_opno); }
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