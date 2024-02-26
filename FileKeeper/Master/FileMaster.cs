using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CsHms
{
    public partial class FileMaster : Form
    {

        FileMasCls mclsFile= new FileMasCls();
        const String FORM_ID = "FILEMAS";// Unique id for this form
        const String TABLE_NAME = "filemas";// Main Table name
        const String PRIMARY_KEY = "fm_code";// Primary key of the table
        Global mGlobal = new Global();// Global data
        CommFuncs mCommFuncs = new CommFuncs();
        UsrRights mUsrRight = new UsrRights();//User Rights
        bool mboolAdd = false, mboolEdit = false;//add/edit flags

        public FileMaster()
        {
            InitializeComponent();
            mUsrRight.FillRights(FORM_ID);
            
            ClearData(true);
        }
        void init()
        {
            mCommFuncs.AutoCompleet("fm_desc", TABLE_NAME, txtDesc);
            mCommFuncs.AutoCompleet("fm_filerack", TABLE_NAME, txtRack);
            mCommFuncs.AutoCompleet("fm_authdperson", TABLE_NAME, txtAuthPerson);
            mclsFile.fillLocation(scsLocation);
            mclsFile.fillGeneralList(scsType, "FTYPE");
            mclsFile.fillGeneralList(scsCategory, "FCAT");
        }
 
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {

            ClearData(true);
            txtFileCode.Focus();
        }
        void ClearData(bool vAll)
        {

            init();
            String strTmp = txtFileCode.Text;             
            if (!vAll) txtFileCode.Text = strTmp;
            else
            {
                txtFileCode.Text = mCommFuncs.NewCode(TABLE_NAME, PRIMARY_KEY, "");
                txtFileCode.Enabled = true;
                txtFileCode.Focus();
                panDetail.Enabled = false;
                btnSave.Enabled = false;
                btnDelete.Enabled = false;
            }
            chkActive.Checked = true;
            chkTransfer.Checked = false;
            chkTrnOut.Checked = false;            
            txtDesc.Text = "";
            txtRack.Text = "";
            txtAuthPerson.Text = "";
            txtRemarks.Text = "";
        }
        void fillDataToControl()
        {             
            txtFileCode.Text =mclsFile.Code;
            txtDesc.Text= mclsFile.Desc;
            scsLocation.SelectItemCode(mclsFile.LocationPtr);
            scsType.SelectItemCode(mclsFile.TypePtr);
            scsCategory.SelectItemCode(mclsFile.CategoryPtr);
            txtRack.Text = mclsFile.FileRack;
            txtAuthPerson.Text = mclsFile.Authdperson;
            chkTransfer.Checked = mclsFile.Transferable == "Y" ? true : false;
            chkActive.Checked = mclsFile.Active=="Y"? true :false;
            chkTrnOut.Checked = mclsFile.Transfertooutside == "Y" ? true : false; 
            txtRemarks.Text= mclsFile.Remarks;
        }
        void setDataToProperties()
        {
            mclsFile.Code=  txtFileCode.Text ;
            mclsFile.Desc=  txtDesc.Text ;
            mclsFile.LocationPtr = scsLocation.SelectedValue;
            mclsFile.TypePtr = scsType.SelectedValue;
            mclsFile.FileRack = txtRack.Text;
            mclsFile.Authdperson = txtAuthPerson.Text;
            mclsFile.CategoryPtr = scsCategory.SelectedValue;
            mclsFile.Active = chkActive.Checked == true ? "Y" : "N";
            mclsFile.Transferable = chkTransfer.Checked == true ? "Y" : "N";
            mclsFile.Transfertooutside = chkTrnOut.Checked==true ? "Y":"N";
            mclsFile.Remarks= txtRemarks.Text;
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

             mclsFile.ClearAll();
            mclsFile.Code=txtFileCode.Text;
            if (mclsFile.deleteData() == false)
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
                txtFileCode.Text = mclsFile.searchFile(txtFileCode.Text,false);
            }
            else if (e.KeyChar == Convert.ToChar(mGlobal.NewNumberKey))// Checking new number key Pressed
            {
                txtFileCode.Text = mCommFuncs.NewCode(TABLE_NAME, PRIMARY_KEY, "");
                txtFileCode.Select();
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

            if (txtFileCode.Text == "")
            {
                MessageBox.Show("Invalid Code.");
                txtFileCode.Focus();
                return false;
            }
            if (txtDesc.Text == "")
            {
                MessageBox.Show("Invalid Description.");
                txtDesc.Focus();
                return false;
            }
            if (scsLocation.SelectedValue == "")
            {
                MessageBox.Show("Invalid Location.");
                scsType.Focus();
                return false;
            }
            if (scsType.SelectedValue == "")
            {
                MessageBox.Show("Invalid File Type.");
                scsType.Focus();
                return false;
            }
            if (scsCategory.SelectedValue == "")
            {
                MessageBox.Show("Invalid File Category.");
                scsCategory.Focus();
                return false;
            }
            if (txtRack.Text == "")
            {
                MessageBox.Show("Invalid Rack.");
                txtRack.Focus();
                return false;
            }
            if (txtAuthPerson.Text == "")
            {
                MessageBox.Show("Invalid Authorized Person.");
                txtAuthPerson.Focus();
                return false;
            }
            if (mclsFile.checkFileNameAlreadyExist(txtFileCode.Text, txtDesc.Text) == true)
            {
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
                txtFileCode.Text = txtFileCode.Text.Trim().ToUpper().Replace("'", "");
                if (txtFileCode.Text == "") return;
                ClearData(false);
                mclsFile.ClearAll();
                mclsFile.Code = txtFileCode.Text;
                if (mclsFile.getData(false) == true)
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
                txtFileCode.Enabled = false;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
                if (mboolAdd == false && mboolEdit == false) return;
                if (!ValidateObjs()) return;
                mclsFile.ClearAll();
                setDataToProperties();
                if (mboolAdd)
                {
                    if (!mUsrRight.HaveRights("A"))
                    {
                        MessageBox.Show(mUsrRight.MessageText);
                        return;
                    }
                    if (mclsFile.insertData() == true)
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
                    if (mclsFile.updateData() == true)
                        MessageBox.Show("Data successfully updated.");
                    else
                        MessageBox.Show("Unable to update. Please try again.");
                     
                }
                ClearData(true);
                txtFileCode.Focus();
        }

        private void FileMaster_Load(object sender, EventArgs e)
        {
            ClearData(true);
        }
    }
}