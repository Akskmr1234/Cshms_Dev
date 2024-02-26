using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DocMan.Trans
{
    public partial class FileInOutEntry : Form
    {
        CommFuncs mclsCFunc = new CommFuncs();
        Global mGlobal = new Global();
        FileInOutEntryCls mclsEntry = new FileInOutEntryCls();
        string FileMode = "Lin";//Gen-General,Lin
        public FileInOutEntry()
        {
            InitializeComponent();
            clearAll();
            
        }
        void clearAll()
        {
           lblRack.Text = "";

           if (FileMode == "Gen")
           {
               dgvList.Columns[3].Visible = false;               
           }
           if (FileMode == "Lin")
           {
                dgvList.Columns[3].Visible = true ;               
           }
           dgvList.Rows.Clear();
            
            txtCarrierCode.Text="";
            lblToLocationName.Text="";
            lblCarrierName.Text="";
            txtToLocationCode.Text ="";
            txtRemarks.Text ="";
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtToLocationCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(mGlobal.SearchKey))// Checking Search key Pressed
            {
                txtToLocationCode.Text = mclsEntry.searchLocation(txtToLocationCode.Text);
            }
        }


        private void txtToLocationCode_Validating(object sender, CancelEventArgs e)
        {
            txtToLocationCode.Text = txtToLocationCode.Text.Trim().ToUpper().Replace("'", "");
            lblToLocationName.Text = "";
            if (txtToLocationCode.Text == "") return;
            lblToLocationName.Text = mclsEntry.getLocationName(txtToLocationCode.Text);
            if (lblToLocationName.Text.Trim() == "")
            {
                MessageBox.Show("Not Valid Location Code");
                e.Cancel = true;
            }
        }

        private void txtCarrierCode_Validating(object sender, CancelEventArgs e)
        {
            txtCarrierCode.Text = txtCarrierCode.Text.Trim().ToUpper().Replace("'", "");
            lblCarrierName.Text = "";
            if (txtCarrierCode.Text == "") return;
            lblCarrierName.Text = mclsEntry.getCarrierName(txtCarrierCode.Text);
            if (lblCarrierName.Text.Trim() == "")
            {
                MessageBox.Show("Not Valid Carrier Code");
                e.Cancel = true;
            }
        }

        private void txtCarrierCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(mGlobal.SearchKey))// Checking Search key Pressed
            {
                txtCarrierCode.Text = mclsEntry.searchCarrier(txtCarrierCode.Text);
            }
        }
        void setValueToCell(int intRowIndex, string strColName, string strValue)
        {

            dgvList.EditMode = DataGridViewEditMode.EditProgrammatically;
            dgvList.BeginEdit(true);
            dgvList.Rows[intRowIndex].Cells[strColName].Value = strValue;
            dgvList.EndEdit();
            dgvList.EditMode = DataGridViewEditMode.EditOnEnter;


        }
        void setValueToCellTag(int intRowIndex, string strColName, string strValue)
        {

            dgvList.EditMode = DataGridViewEditMode.EditProgrammatically;
            dgvList.BeginEdit(true);
            dgvList.Rows[intRowIndex].Cells[strColName].Tag = strValue;
            dgvList.EndEdit();
            dgvList.EditMode = DataGridViewEditMode.EditOnEnter;


        }
        void EditingControl_KeyPress(object sender, KeyPressEventArgs e)
        {
            string strValue = "";
            if (e.KeyChar == Convert.ToChar(mGlobal.SearchKey))
            {
                try
                {
                    if (dgvList.Columns[dgvList.CurrentCell.ColumnIndex].Name == "colFileCode")
                    {

                        strValue = mclsEntry.searchDocument(mclsCFunc.ConvertToString(
                            dgvList.CurrentCell.EditedFormattedValue));
                        setValueToCell(dgvList.CurrentCell.RowIndex, "colFileCode", strValue);

                    }
                    else if (dgvList.Columns[dgvList.CurrentCell.ColumnIndex].Name == "colLocationCode")
                    {

                        strValue = mclsEntry.searchLocation(mclsCFunc.ConvertToString(
                            dgvList.CurrentCell.EditedFormattedValue));
                        setValueToCell(dgvList.CurrentCell.RowIndex, "colLocationCode", strValue);

                    }
                }
                catch (Exception ex) { MessageBox.Show(ex.Message); }
            }
           
        }
        private void dgvList_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            dgvList.EditingControl.KeyPress -= new KeyPressEventHandler(EditingControl_KeyPress);
            dgvList.EditingControl.KeyPress += new KeyPressEventHandler(EditingControl_KeyPress);
        }


        private void dgvList_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            string strValue = "";

            if (FileMode == "Gen")
            {
                setValueToCell(e.RowIndex, "colQty", "1");
            }
            
            if (dgvList.Columns[e.ColumnIndex].Name == "colFileCode")
            {
                lblRack.Text = "";
                setValueToCell(e.RowIndex, "colDocumentNm", "");
                setValueToCellTag(e.RowIndex, "colExDept", "");
                if (mclsCFunc.ConvertToString(dgvList.Rows[e.RowIndex].Cells[e.ColumnIndex].Value) == "") return;
                if (mclsEntry.checkFileCodeAlreadyExist(dgvList, e.RowIndex) == true)
                {
                    e.Cancel = true;
                    return;
                }
                if (mclsCFunc.ConvertToString(dgvList.CurrentCell.EditedFormattedValue) != "")
                {
                    mclsEntry.getFileDetails(mclsCFunc.ConvertToString(dgvList.CurrentCell.EditedFormattedValue));
                    strValue=mclsEntry.FileMaster.Desc;
                    if (strValue == "")
                    {
                        e.Cancel = true;
                        return;
                    }
                    lblRack.Text =mclsEntry.FileMaster.FileRack;
                    setValueToCell(e.RowIndex, "colDocumentNm", strValue);
                    setValueToCell(e.RowIndex, "colExDept", mclsEntry.FileCurrentLocation);
                    setValueToCellTag(e.RowIndex, "colExDept", mclsEntry.FileCurrentLocationCode);
                    mclsEntry.FileMaster.ClearAll();
                }
            }
            else if (dgvList.Columns[e.ColumnIndex].Name == "colLocationCode")
            {
                setValueToCell(e.RowIndex, "colLocationNm", "");
                if (mclsCFunc.ConvertToString(dgvList.CurrentCell.EditedFormattedValue) != "")
                {

                    strValue = mclsEntry.getLocationName(
                        mclsCFunc.ConvertToString(dgvList.CurrentCell.EditedFormattedValue));
                    if (strValue == "")
                    {
                        e.Cancel = true;
                        return;
                    }
                    setValueToCell(e.RowIndex, "colLocationNm", strValue);
                }


            }
            
            
        }

        private void dgvList_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            mclsEntry.fillCarrierInRow(dgvList,e.RowIndex );
            mclsEntry.setSlNo(dgvList);
        }

        private void dgvList_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            SendKeys.Send("{F4}");
        }

        bool isValidFillData()
        {
            if ((txtToLocationCode.Text.Trim() + txtCarrierCode.Text.Trim() + txtRemarks.Text.Trim())=="")
            {
                MessageBox.Show("Location or Carrier or Remarks Required");
                txtToLocationCode.Focus();
                return false ;
            }            
            return true;
        }
        void clearFillData()
        {
            txtRemarks.Text = "";
            txtToLocationCode.Text = "";
            lblToLocationName.Text = "";
            txtCarrierCode.Text = "";
            lblCarrierName.Text = "";
        }
        private void btnFill_Click(object sender, EventArgs e)
        {
            if (isValidFillData() == false) return;
            mclsEntry.fillLocationAndCarrierInGridRow(dgvList, txtToLocationCode.Text, 
                lblToLocationName.Text , txtCarrierCode.Text, txtRemarks.Text);
            clearFillData();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (mclsEntry.checkValidFileMovementEntry(dgvList) == false) return;
            if (mclsEntry.checkValidFileMovement(dgvList) == false) return;
            if (mclsEntry.saveFileMovement(dgvList) == true)
            {
                MessageBox.Show("File Movement Successfully Saved");
            }
            else
            {
                MessageBox.Show("File Movement Saving Process Failed. Please Try Again.");
                return;
            }
            clearAll();
            dgvList.Focus();
        }

        private void dgvList_RowValidating(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (mclsEntry.checkValidFileMovement(dgvList, e.RowIndex) == false)
            {
                e.Cancel = true;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            clearAll();
            dgvList.Focus();
        }

        private void dgvLog_Leave(object sender, EventArgs e)
        {
            gbShowDet.Visible = false ;
        }

        private void dgvList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
                if (mclsEntry.showFileLog(mclsCFunc.ConvertToString(
                    dgvList.Rows[dgvList.CurrentRow.Index].Cells["colFileCode"].Value),dgvLog)==true)
                {
                    gbShowDet.Visible = true;
                    dgvLog.Focus();
                }
             
        }
        

         
    }
}