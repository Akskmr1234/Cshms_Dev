using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
namespace CsHms
{

    public partial class FileCarrier : Form
    {
        FileCarrierMasCls mclsCarrier = new FileCarrierMasCls();
        const String FORM_ID = "FLCARMAS";// Unique id for this form
        const String TABLE_NAME = "filecarriermas";// Main Table name
        const String PRIMARY_KEY = "fc_code";// Primary key of the table
        Global mGlobal = new Global();// Global data
        CommFuncs mCommFuncs = new CommFuncs();
        UsrRights mUsrRight = new UsrRights();//User Rights
        bool mboolAdd = false, mboolEdit = false;//add/edit flags

        public FileCarrier()
        {
            InitializeComponent();
            mUsrRight.FillRights(FORM_ID);
            mCommFuncs.AutoCompleet("fc_name", TABLE_NAME, txtDesc);
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

            txtDesc.Text = "";
            txtRemarks.Text = "";
        }
        void fillDataToControl()
        {

            txtCode.Text = mclsCarrier.Code;
            txtDesc.Text = mclsCarrier.Name ;

            chkActive.Checked = mclsCarrier.Active == "Y" ? true : false;
            txtRemarks.Text = mclsCarrier.Remarks;
        }
        void setDataToProperties()
        {
            mclsCarrier.Code = txtCode.Text;
            mclsCarrier.Name = txtDesc.Text;
            mclsCarrier.Active = chkActive.Checked == true ? "Y" : "N";

            mclsCarrier.Remarks = txtRemarks.Text;
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

            mclsCarrier.ClearAll();
            mclsCarrier.Code = txtCode.Text;
            if (mclsCarrier.deleteData() == false)
                MessageBox.Show("Unable to delete. It may be shared data.");
            else
            {
                MessageBox.Show("Data successfully deleted.");
                ClearData(true);
            }
        }

        private void txtCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(mGlobal.SearchKey))// Checking Search key Pressed
            {
                txtCode.Text = mclsCarrier.searchCarrier(txtCode.Text);
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
                MessageBox.Show("Invalid Name.");
                txtDesc.Focus();
                return false;
            }
            return boolRetVal;
        }

        private void txtCode_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                //                
                mboolAdd = false; mboolEdit = false;
                txtCode.Text = txtCode.Text.Trim().ToUpper().Replace("'", "");
                if (txtCode.Text == "") return;
                ClearData(false);
                mclsCarrier.ClearAll();
                mclsCarrier.Code = txtCode.Text;
                if (mclsCarrier.getData() == true)
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
            mclsCarrier.ClearAll();
            setDataToProperties();
            if (mboolAdd)
            {
                if (!mUsrRight.HaveRights("A"))
                {
                    MessageBox.Show(mUsrRight.MessageText);
                    return;
                }
                if (mclsCarrier.insertData() == true)
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
                if (mclsCarrier.updateData() == true)
                    MessageBox.Show("Data successfully updated.");
                else
                    MessageBox.Show("Unable to update. Please try again.");

            }
            ClearData(true);
            txtCode.Focus();
        }

        private void FileCarrier_Load(object sender, EventArgs e)
        {
            ClearData(true);
        }
    }
}