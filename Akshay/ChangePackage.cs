using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace CsHms.Akshay
{
    public partial class ChangePackage : Form
    {
        public ChangePackage()
        {
            InitializeComponent();            
            FillGender();
        }
        Global mGlobal = new Global();
        CommFuncs mCommFunc = new CommFuncs();
        private void txtOpno_Validating(object sender, CancelEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtOpno.Text))
            {
                string strSql = @"select da_packagetypevalue,da_gender,da_billid from opbill join doctorappointment on opb_id=da_billid where opb_bno='"+txtOpno.Text+"'";
                DataTable dtData = mGlobal.LocalDBCon.ExecuteQuery(strSql);
                if (dtData != null && dtData.Rows.Count > 0)
                {
                    
                    cbxPackage.SelectedValue = dtData.Rows[0]["da_packagetypevalue"].ToString();
                    cbxGender.SelectedValue = dtData.Rows[0]["da_gender"].ToString();
                    cbxGender.Tag = dtData.Rows[0]["da_gender"].ToString();
                    txtOpno.Tag = dtData.Rows[0]["da_billid"].ToString();

                    pnlPackage.Enabled = true;
                }
                else
                pnlPackage.Enabled = false;
            }
            else
                pnlPackage.Enabled = false;
            
        }
        private void FillGender()
        {
            DataTable dtGender = new DataTable();
            dtGender.Columns.Add("ge_code",typeof(System.String));
            dtGender.Columns.Add("ge_gender",typeof(System.String));
            dtGender.Rows.Add("M", "Male");
            dtGender.Rows.Add("F", "Female");
            dtGender.Rows.Add("PB", "Others");
            cbxGender.DataSource = dtGender;
            cbxGender.ValueMember = "ge_code";
            cbxGender.DisplayMember = "ge_gender";
        }
        private void GetItems( string strGender)
        {
            try
            {
                string strSql = @"select itm_code,itm_desc from item where itm_groupptr='GEN' and itm_package='OP' and itm_active='Y' and itm_gender='" + strGender + "'";
                DataTable dtItems = mGlobal.LocalDBCon.ExecuteQuery(strSql);
                if (dtItems != null && dtItems.Rows.Count > 0)
                {
                    cbxPackage.DataSource = dtItems;
                    cbxPackage.DisplayMember = "itm_desc";
                    cbxPackage.ValueMember = "itm_code";
                    cbxPackage.Enabled = true;
                }
                else
                {
                    cbxPackage.Enabled = false;
 
                }
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message.ToString()); }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if(mCommFunc.ConvertToString(cbxGender.SelectedValue)=="M" && mCommFunc.ConvertToString(cbxGender.Tag)!=mCommFunc.ConvertToString(cbxGender.SelectedValue))
            { }
            else if (mCommFunc.ConvertToString(cbxGender.SelectedValue) == "F" && mCommFunc.ConvertToString(cbxGender.Tag) != mCommFunc.ConvertToString(cbxGender.SelectedValue))
            { }
            else
            { }

           
            
        }

        private void MaletoFemale()
        {
            try
            { 
            }
            catch (Exception ex)
            { writeErrorLog(ex, "MaletoFemale"); }
        }
        private void FemaletoMale()
        {
            try
            {
                string strSql = "";
                string strCurrentDate = DateTime.Now.ToString();
                StringBuilder queries = new StringBuilder();
                //strSql = @"update doctorappointment set da_packagetypevalue='" + cbxPackage.SelectedValue + "',da_gender='M' where da_newopno='" + txtOpno.Text + "'";
                //queries.Append(strSql);
                strSql = @"delete opbilld where opbd_itemptr in ('COLPO001','XRBBS001') and  opbd_hdrid='"+mCommFunc.ConvertToString(txtOpno.Tag)+"'";
                queries.Append(strSql);                
                strSql = @"delete modalitypatientstatustran where mpst_itemptr  in ('COLPO001','XRBBS001') and mpst_refid='" + mCommFunc.ConvertToString(txtOpno.Tag) + "'";
                queries.Append(strSql);

                strSql = "select opbd_itemptr as Items from opbilld where opbd_hdrid='" + mCommFunc.ConvertToString(txtOpno.Tag) + "'";
                DataTable dtBillItems = mGlobal.LocalDBCon.ExecuteQuery(strSql);
                if (dtBillItems != null && dtBillItems.Rows.Count > 0)
                {
                    for (int i = 0; i < dtBillItems.Rows.Count; i++)
                    {
                        strSql = "select pck_subitemptr as Items from packagedet where pck_itemptr='" + cbxPackage.SelectedValue + "'";
                        DataTable dtPckItems = mGlobal.LocalDBCon.ExecuteQuery(strSql);
                        for (int j = 0; j < dtBillItems.Rows.Count; j++)
                        {
                            string stritemptr=(mCommFunc.ConvertToString(dtBillItems.Rows[i]["opbd_itemptr"]));

                            if (dtPckItems.Rows.Contains(stritemptr))
                            {
                                List<string> Itemlist = new List<string>();
                                Itemlist.Add(stritemptr);

                            }
 
                        }

 
                    }
                }

                strSql = @"insert into opbilld (opbd_hdrid,opbd_itemptr,opbd_itemdesc,opbd_srvddt,opbd_taxinclusive,opbd_qty) values ('" + mCommFunc.ConvertToString(txtOpno.Tag) + "','4','PSA','" + strCurrentDate + "','Y','1')";
                queries.Append(strSql);

            }
            catch (Exception ex)
            {
                writeErrorLog(ex, "FemaletoMale");
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            pnlPackage.Enabled = false;
            txtOpno.Text = "";
        }
        private void ClearAll()
        {
            pnlPackage.Enabled = false;
            txtOpno.Text = "";
            cbxGender.Enabled = false;
            cbxGender.Tag = "";
            cbxPackage.Enabled = false;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
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

        private void cbxGender_SelectionChangeCommitted(object sender, EventArgs e)
        {
            GetItems(mCommFunc.ConvertToString(cbxGender.SelectedValue));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            StringBuilder queries = new StringBuilder();
            string strCurrentDate = DateTime.Now.ToString();

            // Assuming mGlobal.LocalDBCon.ExecuteQuery(string) returns a DataTable
            string strSql = "SELECT opbd_itemptr AS Items FROM opbilld WHERE opbd_hdrid='" + mCommFunc.ConvertToString(txtOpno.Tag) + "'";
            DataTable dtBillItems = mGlobal.LocalDBCon.ExecuteQuery(strSql);

            // Initialize a list to hold items not found in dtPckItems
            List<string> nonMatchingItems = new List<string>();

            if (dtBillItems != null && dtBillItems.Rows.Count > 0)
            {
                // Fetch the package items only once
                strSql = "SELECT pck_subitemptr AS Items FROM packagedet WHERE pck_itemptr='" + mCommFunc.ConvertToString(cbxPackage.SelectedValue) + "'";
                DataTable dtPckItems = mGlobal.LocalDBCon.ExecuteQuery(strSql);

                // Since HashSet is not available, use Dictionary or List for compatibility
                List<string> pckItemsList = new List<string>();
                foreach (DataRow row in dtPckItems.Rows)
                {
                    string item = mCommFunc.ConvertToString(row["Items"]);
                    if (!pckItemsList.Contains(item))
                    {
                        pckItemsList.Add(item);
                    }
                }

                // Iterate through dtBillItems and find items not in dtPckItems
                foreach (DataRow billItemRow in dtBillItems.Rows)
                {
                    string billItemPtr = mCommFunc.ConvertToString(billItemRow["Items"]);
                    if (!pckItemsList.Contains(billItemPtr))
                    {
                        // This item is not found in dtPckItems, add it to the list
                        nonMatchingItems.Add(billItemPtr);
                    }
                }
                for (int i=0;i<nonMatchingItems.Count;i++)
                {
                    strSql = @"select * from item left join packagedet on itm_code=pck_subitemptr where itm_code='" + mCommFunc.ConvertToString(nonMatchingItems[i]) + "' ";
                    DataTable dtPackage = mGlobal.LocalDBCon.ExecuteQuery(strSql);
                    if (dtPackage != null && dtPackage.Rows.Count > 0)
                    {
                        strSql = @"insert into opbilld (opbd_hdrid,opbd_itemptr,opbd_itemdesc,opbd_srvddt,opbd_taxinclusive,opbd_qty) values ('" + mCommFunc.ConvertToString(txtOpno.Tag) + "','" + mCommFunc.ConvertToString(dtPackage.Rows[0]["pck_itemptr"]) + "','" + mCommFunc.ConvertToString(dtPackage.Rows[0]["itm_desc"]) + "','" + strCurrentDate + "','Y','1')";
                        queries.Append(strSql);
                    }
                }
            }

            // nonMatchingItems now contains all items from dtBillItems not present in dtPckItems

        }
    }
}