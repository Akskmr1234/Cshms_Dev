using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
namespace CsHms
{

    public partial class FileLocaitonMaster : Form
    {
        FileLocationMasCls mclsFileLoc=new FileLocationMasCls();
        const String FORM_ID = "FLOCMAS";// Unique id for this form
        const String TABLE_NAME = "filelocationmas";// Main Table name
        const String PRIMARY_KEY = "fl_code";// Primary key of the table
        Global mGlobal = new Global();// Global data
        CommFuncs mCommFuncs = new CommFuncs();
        UsrRights mUsrRight = new UsrRights();//User Rights
        bool mboolAdd = false, mboolEdit = false;//add/edit flags
       
        public FileLocaitonMaster()
        {
            InitializeComponent();
            mUsrRight.FillRights(FORM_ID);
            mCommFuncs.AutoCompleet("fl_desc", TABLE_NAME, txtDesc);
            ClearData(true);
        }
 
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {

            ClearData(true);
            txtCode.Focus();
        }
        void ClearData(bool vAll)
        {

            String strTmp = txtCode.Text;
             
            if (!vAll) txtCode.Text = strTmp;
            else
            {
                txtCode.Text = mCommFuncs.NewCode(TABLE_NAME, PRIMARY_KEY, "");
                txtCode.Enabled = true;
                txtCode.Focus();
                panDetail.Enabled = false;
                btnSave.Enabled = false;
                btnDelete.Enabled = false;
            }
            chkActive.Checked = true;
            chkInHse.Checked = false;
            txtDesc.Text = "";
            txtRemarks.Text = "";
        }
        void fillDataToControl()
        {
             
            txtCode.Text =mclsFileLoc.Code;
            txtDesc.Text= mclsFileLoc.Desc;
            chkInHse.Checked = mclsFileLoc.Inhouse == "Y" ? true : false;
            chkActive.Checked = mclsFileLoc.Active=="Y"? true :false; 
            txtRemarks.Text= mclsFileLoc.Remarks;
        }
        void setDataToProperties()
        {
            mclsFileLoc.Code=  txtCode.Text ;
            mclsFileLoc.Desc=  txtDesc.Text ;
            mclsFileLoc.Active = chkActive.Checked == true ? "Y" : "N";
            mclsFileLoc.Inhouse= chkInHse.Checked==true ? "Y":"N";
            mclsFileLoc.Remarks= txtRemarks.Text;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (!mUsrRight.HaveRights("D"))
            {
                MessageBox.Show(mUsrRight.MessageText);
                return;
            }
            if (MessageBox.Show("Are you sure want to delete data - " + txtDesc.Text, "Delete - Warning", MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.No) return;

             mclsFileLoc.ClearAll();
            mclsFileLoc.Code=txtCode.Text;
            if (mclsFileLoc.deleteData() == false)
                MessageBox.Show("Unable to delete. It may be shared data.");
            else
            {
                MessageBox.Show("Data successfully deleted.");
                ClearData(true);
            }
        }

        private void txtFileCode_KeyPress(object sender, KeyPressEventArgs e)
        {             
            if (e.KeyChar == Convert.ToChar(mGlobal.SearchKey))// Checking Search key Pressed
            {
                txtCode.Text = mclsFileLoc.searchLocation(txtCode.Text);
            }
            else if (e.KeyChar == Convert.ToChar(mGlobal.NewNumberKey))// Checking new number key Pressed
            {
                txtCode.Text = mCommFuncs.NewCode(TABLE_NAME, PRIMARY_KEY, "");
                txtCode.Select();
            }             
        }
        private bool ValidateObjs()
        {
            bool boolRetVal = true;
            foreach (Control ctlObj in this.Controls)// Searhing all controls in the form
            {
                if ((ctlObj is Panel) || (ctlObj is GroupBox))
                {
                    foreach (Control ctlObj2 in ctlObj.Controls)// Searhing all controls in the Panel/Group
                    {
                        if (ctlObj2 is TextBox)  //Formating the text of each textbox
                            ctlObj2.Text = ctlObj2.Text.Trim().ToUpper().Replace("'", "");
                    }
                }
                else if (ctlObj is TextBox)  //Formating the text of each textbox
                    ctlObj.Text = ctlObj.Text.Trim().ToUpper().Replace("'", "");
            }

            if (txtCode.Text == "")
            {
                MessageBox.Show("Invalid Code.");
                txtCode.Focus();
                return false;
            }
            if (txtDesc.Text == "")
            {
                MessageBox.Show("Invalid Description.");
                txtDesc.Focus();
                return false;
            }
            return boolRetVal;
        }

        private void txtFileCode_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                //                
                mboolAdd = false; mboolEdit = false;
                txtCode.Text = txtCode.Text.Trim().ToUpper().Replace("'", "");
                if (txtCode.Text == "") return;
                ClearData(false);
                mclsFileLoc.ClearAll();
                mclsFileLoc.Code = txtCode.Text;
                if (mclsFileLoc.getData() == true)
                {
                    mboolEdit = true;
                    fillDataToControl();
                    btnDelete.Enabled = true;
                }
                else
                {
                    mboolAdd = true;
                    btnDelete.Enabled = false;
                }
                panDetail.Enabled = true;
                btnSave.Enabled = true;
                txtDesc.Focus();
                txtCode.Enabled = false;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
                if (mboolAdd == false && mboolEdit == false) return;
                if (!ValidateObjs()) return;
                mclsFileLoc.ClearAll();
                setDataToProperties();
                if (mboolAdd)
                {
                    if (!mUsrRight.HaveRights("A"))
                    {
                        MessageBox.Show(mUsrRight.MessageText);
                        return;
                    }
                    if (mclsFileLoc.insertData() == true)
                        MessageBox.Show("Data successfully saved.");
                    else
                        MessageBox.Show("Unable to save. Please try again.");

                }
                else if (mboolEdit)
                {
                    if (!mUsrRight.HaveRights("E"))
                    {
                        MessageBox.Show(mUsrRight.MessageText);
                        return;
                    }
                    if (mclsFileLoc.updateData() == true)
                        MessageBox.Show("Data successfully updated.");
                    else
                        MessageBox.Show("Unable to update. Please try again.");
                     
                }
                ClearData(true);
                txtCode.Focus();
        }

        private void FileLocaitonMaster_Load(object sender, EventArgs e)
        {
            ClearData(true);
        }
    }
}