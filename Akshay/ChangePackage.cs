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
            GetItems();
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
                    GetItems(dtData.Rows[0]["da_gender"].ToString());
                    pnlPackage.Enabled = true;
                }
                else
                {
                    pnlPackage.Enabled = false;
                    MessageBox.Show("Bill not avalable");
                }
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
        private void GetItems()
        {
            try
            {
                string strSql = @"select itm_code,itm_desc from item where itm_groupptr='GEN' and itm_package='OP' and itm_active='Y' and itm_gender='"+mCommFunc.ConvertToString(cbxGender.SelectedValue)+"'";
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
            //if(mCommFunc.ConvertToString(cbxGender.SelectedValue)=="M" && mCommFunc.ConvertToString(cbxGender.Tag)!=mCommFunc.ConvertToString(cbxGender.SelectedValue))
            //{ FemaletoMale(); }
            //else if (mCommFunc.ConvertToString(cbxGender.SelectedValue) == "F" && mCommFunc.ConvertToString(cbxGender.Tag) != mCommFunc.ConvertToString(cbxGender.SelectedValue))
            //{ MaletoFemale(); }
            //else
            //{ }

            FemaletoMale(); 
            
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
                string strCurrentDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
                StringBuilder queries = new StringBuilder();
                if (mCommFunc.ConvertToString(cbxGender.SelectedValue) == "M" && mCommFunc.ConvertToString(cbxGender.Tag) != mCommFunc.ConvertToString(cbxGender.SelectedValue))
                {
                    strSql = @"update doctorappointment set da_packagetypevalue='" + cbxPackage.SelectedValue + "',da_gender='M' where da_billid='" + txtOpno.Tag + "'";
                    queries.Append(strSql);
                    strSql = @"update opbill set opb_gender='Male' where opb_id='" + mCommFunc.ConvertToString(txtOpno.Tag) + "'";
                    queries.Append(strSql);
                    strSql = @"update opbilld set opbd_itemptr='16',opbd_itemdesc='Nura Men Health Screening' where opbd_itemptr='17' and opbd_hdrid='"+txtOpno.Tag+"'";
                    queries.Append(strSql);
                }
                else if (mCommFunc.ConvertToString(cbxGender.SelectedValue) == "F" && mCommFunc.ConvertToString(cbxGender.Tag) != mCommFunc.ConvertToString(cbxGender.SelectedValue))
                {
                    strSql = @"update doctorappointment set da_packagetypevalue='" + cbxPackage.SelectedValue + "',da_gender='F' where da_billid='" + txtOpno.Tag + "'";
                    queries.Append(strSql);
                    strSql = @"update opbill set opb_gender='Female' where opb_id='" + mCommFunc.ConvertToString(txtOpno.Tag) + "'";
                    queries.Append(strSql);
                    strSql = @"update opbilld set opbd_itemptr='17',opbd_itemdesc='Nura Women Health Screening' where opbd_itemptr='17' and opbd_hdrid='"+txtOpno.Tag+"'";
                    queries.Append(strSql);
                }
                else if (mCommFunc.ConvertToString(cbxGender.SelectedValue) == "F")
                {
                    strSql = @"update doctorappointment set da_packagetypevalue='" + cbxPackage.SelectedValue + "',da_gender='F' where da_billid='" + txtOpno.Tag + "'";
                    queries.Append(strSql);
                    strSql = @"update opbill set opb_gender='Female' where opb_id='" + mCommFunc.ConvertToString(txtOpno.Tag) + "'";
                    queries.Append(strSql);
                    strSql = @"update opbilld set opbd_itemptr='17',opbd_itemdesc='Nura Women Health Screening' where opbd_itemptr='17' and opbd_hdrid='" + txtOpno.Tag + "'";
                    queries.Append(strSql);
                }
                else if (mCommFunc.ConvertToString(cbxGender.SelectedValue) == "M")
                {
                    strSql = @"update doctorappointment set da_packagetypevalue='" + cbxPackage.SelectedValue + "',da_gender='M' where da_billid='" + txtOpno.Tag + "'";
                    queries.Append(strSql);
                    strSql = @"update opbill set opb_gender='Male' where opb_id='" + mCommFunc.ConvertToString(txtOpno.Tag) + "'";
                    queries.Append(strSql);
                    strSql = @"update opbilld set opbd_itemptr='16',opbd_itemdesc='Nura Men Health Screening' where opbd_itemptr='17' and opbd_hdrid='" + txtOpno.Tag + "'";
                    queries.Append(strSql);
                }
               
                //strSql = @"delete opbilld where opbd_itemptr in ('COLPO001','XRBBS001') and  opbd_hdrid='"+mCommFunc.ConvertToString(txtOpno.Tag)+"'";
                //queries.Append(strSql);                
                //strSql = @"delete modalitypatientstatustran where mpst_itemptr  in ('COLPO001','XRBBS001') and mpst_refid='" + mCommFunc.ConvertToString(txtOpno.Tag) + "'";
                //queries.Append(strSql);
             
                strSql = @"SELECT opbd_itemptr AS Items FROM opbilld WHERE opbd_hdrid='" + mCommFunc.ConvertToString(txtOpno.Tag) + "'";
            DataTable dtBillItems = mGlobal.LocalDBCon.ExecuteQuery(strSql);

            // Initialize a list to hold items not found in dtPckItems
            List<string> nonMatchingItems = new List<string>();
            List<string> ItemsList = new List<string>();
            List<string> PckItemsLIst = new List<string>();
            if (dtBillItems != null && dtBillItems.Rows.Count > 0)
            {
                // Fetch the package items only once
                strSql = @"SELECT pck_subitemptr AS Items FROM packagedet WHERE pck_itemptr='" + mCommFunc.ConvertToString(cbxPackage.SelectedValue) + "'";
                DataTable dtPckItems = mGlobal.LocalDBCon.ExecuteQuery(strSql);

                // Since HashSet is not available, use Dictionary or List for compatibility
              
                foreach (DataRow row in dtBillItems.Rows)
                {
                    string item = mCommFunc.ConvertToString(row["Items"]);
                    if (!ItemsList.Contains(item))
                    {
                        ItemsList.Add(item);
                    }
                }

                // Iterate through dtBillItems and find items not in dtPckItems
                foreach (DataRow nonbillItemRow in dtPckItems.Rows)
                {
                    string billItemPtr = mCommFunc.ConvertToString(nonbillItemRow["Items"]);
                    PckItemsLIst.Add(billItemPtr);
                    if (!ItemsList.Contains(billItemPtr))
                    {
                        // This item is not found in dtPckItems, add it to the list
                        nonMatchingItems.Add(billItemPtr);
                    }
                }
                for (int i = 0; i < ItemsList.Count; i++)
                {
                    if (!PckItemsLIst.Contains(ItemsList[i]))
                    {
                        strSql = @"delete from opbilld where opbd_itemptr='" + mCommFunc.ConvertToString(ItemsList[i]) + "' and (opbd_packagemode<>'PCK' or opbd_packagemode is null) and opbd_hdrid='" + mCommFunc.ConvertToString(txtOpno.Tag) + "'";
                        mGlobal.LocalDBCon.ExecuteQuery(strSql);
                    }
                }
                for (int i = 0; i < nonMatchingItems.Count; i++)
                {
                    strSql = @"select * from item left join packagedet on itm_code=pck_subitemptr where itm_code='" + mCommFunc.ConvertToString(nonMatchingItems[i]) + "' ";
                    DataTable dtPackage = mGlobal.LocalDBCon.ExecuteQuery(strSql);
                    strSql = @"select * from opbilld where opbd_itemptr='" + mCommFunc.ConvertToString(dtPackage.Rows[0]["itm_code"]) + "' and opbd_hdrid='"+mCommFunc.ConvertToString(txtOpno.Tag)+"'";
                    if (dtPackage != null && dtPackage.Rows.Count > 0 && !CheckAlreadyExist(strSql))
                    {
                        strSql = @"insert into opbilld (opbd_hdrid,opbd_itemptr,opbd_itemdesc,opbd_srvddt,opbd_taxinclusive,opbd_qty,opbd_rate,opbd_total,opbd_disper,opbd_disamt,opbd_bdisamt,opbd_taxmainamt,opbd_taxcess1amt,opbd_taxamt,opbd_net,opbd_corppayable,opbd_selfpayable) values ('" + mCommFunc.ConvertToString(txtOpno.Tag) + "','" + mCommFunc.ConvertToString(dtPackage.Rows[0]["itm_code"]) + "','" + mCommFunc.ConvertToString(dtPackage.Rows[0]["itm_desc"]) + "','" + strCurrentDate + "','Y','1',0,0,0,0,0,0,0,0,0,0,0)";
                        mGlobal.LocalDBCon.ExecuteQuery(strSql);
                    }
                }
            
            }
                List<string> ModalityItemLIst=new List<string>();
                strSql=@" select mpst_itemptr from modalitypatientstatustran where mpst_refid='"+mCommFunc.ConvertToString(mCommFunc.ConvertToString(txtOpno.Tag))+"'";
                DataTable dtModalityitems = mGlobal.LocalDBCon.ExecuteQuery(strSql);
                if (dtModalityitems != null && dtModalityitems.Rows.Count > 0)
                {
                    foreach (DataRow modalityitems in dtModalityitems.Rows)
                    {
                        string modalityitem = mCommFunc.ConvertToString(modalityitems["mpst_itemptr"]);
                        if (!ModalityItemLIst.Contains(modalityitem))
                        {
                            ModalityItemLIst.Add(modalityitem);
                        }
                    }
 
                }
                for (int i = 0; i < ModalityItemLIst.Count; i++)
                {
                    if (!PckItemsLIst.Contains(ModalityItemLIst[i]))
                    {
                        strSql = @"delete from modalitypatientstatustran where mpst_refid='" + mCommFunc.ConvertToString(txtOpno.Tag) + "' and mpst_itemptr='" + mCommFunc.ConvertToString(ModalityItemLIst[i]) + "'";
                        mGlobal.LocalDBCon.ExecuteQuery(strSql);
                    }
                }

                    strSql = @"SELECT * FROM opbilld RIGHT JOIN item ON opbilld.opbd_itemptr = item.itm_code WHERE itm_groupptr NOT IN ('LAB', 'GEN')  AND itm_package NOT IN ('OP','IP')  AND itm_active = 'Y'  AND opbd_hdrid = '" + mCommFunc.ConvertToString(txtOpno.Tag) + "'";
            DataTable dtBilldItems = mGlobal.LocalDBCon.ExecuteQuery(strSql);
            if (dtBilldItems != null && dtBilldItems.Rows.Count > 0)
            {
                for (int i = 0; i < dtBilldItems.Rows.Count; i++)
                {
                    strSql = @"select * from modalitypatientstatustran where mpst_itemptr='" + mCommFunc.ConvertToString(dtBilldItems.Rows[i]["opbd_itemptr"]) + "' and mpst_refid='"+mCommFunc.ConvertToString(txtOpno.Tag)+"'";
                    if (!CheckAlreadyExist(strSql))
                    {
                        strSql = @"update billnos set blno_no=blno_no+1 where blno_code='MRDAN'";
                        mGlobal.LocalDBCon.ExecuteQuery(strSql);
                        strSql = @"select blno_no from billnos where blno_code='MRDAN'";
                        DataTable dtBillnos = mGlobal.LocalDBCon.ExecuteQuery(strSql);
                        DataTable dtModality = mGlobal.LocalDBCon.ExecuteQuery(@"select mgig_modalitygrouppptr from opbilld left join item on opbd_itemptr=itm_code left join modalitygroupitemgroupmap on itm_groupptr=mgig_modalitygrouppptr where opbd_id='" + mCommFunc.ConvertToString(dtBilldItems.Rows[i]["opbd_id"]) + "'");
                        string strModalityptr = dtModality.Rows[0][0].ToString();
                        strSql = @"insert into modalitypatientstatustran (mpst_modmodeptr,mpst_module,mpst_refid,mpst_refno,mpst_detrefid,mpst_accessionno,mpst_modalityptr,mpst_itemptr,mpst_statusptr,mpst_processstarttime,mpst_user,mpst_ormstatus) values ('RAD','OPB','" + mCommFunc.ConvertToString(dtBilldItems.Rows[i]["opbd_hdrid"]) + "','" + mCommFunc.ConvertToString(dtBilldItems.Rows[i]["opbd_hdrid"]) + "','" + mCommFunc.ConvertToString(dtBilldItems.Rows[i]["opbd_id"]) + "','1MUA" + mCommFunc.ConvertToString(dtBillnos.Rows[0]["blno_no"]) + "','" + mCommFunc.ConvertToString(dtModality.Rows[0]["mgig_modalitygrouppptr"]) + "','" + mCommFunc.ConvertToString(dtBilldItems.Rows[i]["opbd_itemptr"]) + "','ARR','" + DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss") + "','admin','Y')";
                        queries.Append(strSql);
                    }
                }
            }
            int res = mGlobal.LocalDBCon.ExecuteNonQuery(queries.ToString());
            if (res > 0)
                MessageBox.Show("Success");
            else
                MessageBox.Show("Error");

            }
            catch (Exception ex)
            {
                writeErrorLog(ex, "FemaletoMale");
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private bool CheckAlreadyExist(string strSql)
        {
            try
            {
                DataTable dtExist = mGlobal.LocalDBCon.ExecuteQuery(strSql);
                if (dtExist!=null && dtExist.Rows.Count>0)
                    return true;
                else
                    return false;
 
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

       
    }
}