using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Windows.Forms ;
//using DocMan.Masters;

namespace DocMan.Trans
{
    class FileInOutEntryCls
    {
        CommFuncs mclsCFunc = new CommFuncs();
        Global mGlobal = new Global();
        String SQL;
        const String FORM_ID = "FILEMAS";// Unique id for this form
        const String TABLE_NAME = "filemovement";// Main Table name
        const String PRIMARY_KEY = "fme_id";// Primary key of the table
        FileCarrierMasCls mclsCarrier = new FileCarrierMasCls();
        FileMasCls mclsFileMas = new FileMasCls();
        FileLocationMasCls mclsLocMas = new FileLocationMasCls();
        string mstrFileCurrentLocation;
        string mstrFileCurrentLocationCode;
        double mdblQty;

        public void ClearAll()
        {
            SQL = "";
            mstrFileCurrentLocation = "";
            mstrFileCurrentLocationCode = "";
            mdblQty = 0;
        }
        public string FileCurrentLocation
        {
            set { mstrFileCurrentLocation = value; }
            get { return mstrFileCurrentLocation; }
        }
        public string FileCurrentLocationCode
        {
            set { mstrFileCurrentLocationCode = value; }
            get { return mstrFileCurrentLocationCode; }
        }

        public void getFileCurrentLocation(string strFilePtr)
        {

            SQL = @"select fme_tolocptr as 'LocationCode',fl_desc as 'LocationNm'
            from filemovement left join filelocationmas on(fl_code=fme_tolocptr) where fme_id in(
            select MAX(fme_id) from filemovement where fme_fileptr='" + strFilePtr + "')";
            DataTable dtData = mGlobal.LocalDBCon.ExecuteQuery(SQL);
            if (dtData != null)
            {
                if (dtData.Rows.Count > 0)
                {
                    mstrFileCurrentLocationCode = mclsCFunc.ConvertToString(dtData.Rows[0]["LocationCode"]);
                    mstrFileCurrentLocation = mclsCFunc.ConvertToString(dtData.Rows[0]["LocationNm"]);
                }
            }

        }

        public FileMasCls FileMaster
        {
            get { return mclsFileMas; }
        }
        public FileLocationMasCls LocationMaster
        {
            get { return mclsLocMas; }
        }
        public string searchDocument(string strDesc)
        {
            return mclsFileMas.searchFile(strDesc, true);
        }
        public string searchCarrier(string strDesc)
        {
            return mclsCarrier.searchCarrier(strDesc);
        }
        public string searchLocation(string strDesc)
        {
            return mclsLocMas.searchLocation(strDesc);
        }
        public string getLocationName(string strCode)
        {
            mclsLocMas.ClearAll();
            mclsLocMas.Code = strCode;
            mclsLocMas.getData();
            return mclsLocMas.Desc;
        }
        public string getCarrierName(string strCode)
        {
            mclsCarrier.ClearAll();
            mclsCarrier.Code = strCode;
            mclsCarrier.getData();
            return mclsCarrier.Name;
        }
        public void getFileDetails(string strCode)
        {
            mclsFileMas.ClearAll();
            mclsFileMas.Code = strCode;
            mclsFileMas.getData(true);
            this.ClearAll();
            mclsLocMas.ClearAll();
            mclsLocMas.Code = mclsFileMas.LocationPtr;
            mclsLocMas.getData();
            mstrFileCurrentLocationCode = mclsLocMas.Code;
            mstrFileCurrentLocation = mclsLocMas.Desc;
            getFileCurrentLocation(strCode);

        }
        public void fillCarrierInRow(DataGridView dgvList, int intRowIndex)
        {
            DataGridViewComboBoxCell CBCell = new DataGridViewComboBoxCell();
            CBCell = (DataGridViewComboBoxCell)dgvList.Rows[intRowIndex].Cells["colCarrier"];

            DataTable dtData = mclsCarrier.getDataList("  (fc_active is null or fc_active<>'N')");
            CBCell.DataSource = dtData;
            CBCell.DisplayMember = "fc_name";
            CBCell.ValueMember = "fc_code";

        }

        public bool saveFileMovement(DataGridView dgvList)
        {
            try
            {
                DataTable dtData = mGlobal.LocalDBCon.ExecuteQuery("select * from " + TABLE_NAME + " where 1=2");
                int intRowIndex = 0;
                foreach (DataGridViewRow dgvRow in dgvList.Rows)
                {
                    if (mclsCFunc.ConvertToString(dgvRow.Cells["colFileCode"].Value) != "")
                    {
                        dtData.Rows.Add();
                        dtData.Rows[intRowIndex]["fme_dt"] = mGlobal.RunningDate;
                        dtData.Rows[intRowIndex]["fme_fileptr"] = mclsCFunc.ConvertToString(dgvRow.Cells["colFileCode"].Value);
                        dtData.Rows[intRowIndex]["fme_frmlocptr"] = mclsCFunc.ConvertToString(dgvRow.Cells["colExDept"].Tag);
                        dtData.Rows[intRowIndex]["fme_tolocptr"] = mclsCFunc.ConvertToString(dgvRow.Cells["colLocationCode"].Value);
                        dtData.Rows[intRowIndex]["fme_carrierptr"] = mclsCFunc.ConvertToString(dgvRow.Cells["colCarrier"].Value);
                        dtData.Rows[intRowIndex]["fme_remarks"] = mclsCFunc.ConvertToString(dgvRow.Cells["colRemarks"].Value);
                        dtData.Rows[intRowIndex]["fme_user"] = mGlobal.CurrentUser;
                        dtData.Rows[intRowIndex]["fme_userid"] = mclsCFunc.ConvertToInt64(mGlobal.CurrentUserID);
                        dtData.Rows[intRowIndex]["fme_sysdt"] = DateTime.Now.Date;
                        dtData.Rows[intRowIndex]["fme_qty"] = mclsCFunc.ConvertToNumber_Double(dgvRow.Cells["colQty"].Value);
                        intRowIndex += 1;
                    }
                }
                if (mGlobal.LocalDBCon.UpdateDataTable("select * from " + TABLE_NAME + " where 1=2", dtData) > 0)
                    return true;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
            return false;

        }
        public void setSlNo(DataGridView dgvList)
        {
            int intSlno = 0;
            foreach (DataGridViewRow dgvRow in dgvList.Rows)
            {
                intSlno += 1;
                dgvRow.Cells["colslno"].Value = intSlno;
            }
        }
        public bool checkFileCodeAlreadyExist(DataGridView dgvList, int intRowIndex)
        {
            int intSlno = 0;
            foreach (DataGridViewRow dgvRow in dgvList.Rows)
            {
                if (intSlno == intRowIndex) continue;
                if (mclsCFunc.ConvertToString(dgvRow.Cells["colFileCode"].Value).ToUpper() ==
                    mclsCFunc.ConvertToString(dgvList.Rows[intRowIndex].Cells["colFileCode"].Value).ToUpper())
                {
                    MessageBox.Show("This File Already Exist in SL.No." +
                        mclsCFunc.ConvertToString(dgvRow.Cells["colslno"].Value));
                    return true;
                }
                intSlno += 1;
            }
            return false;
        }

        public bool checkValidFileMovement(DataGridView dgvList)
        {

            foreach (DataGridViewRow dgvRow in dgvList.Rows)
            {
                if (checkValidFileMovement(dgvList, dgvRow.Index) == false)
                    return false;
            }
            return true;
        }

        public bool checkValidFileMovementEntry(DataGridView dgvList)
        {
            if (dgvList.Rows.Count == 1)
            {
                if (mclsCFunc.ConvertToString(dgvList.Rows[0].Cells["colFileCode"].Value) == "")
                {
                    MessageBox.Show("File Required In SL.NO. 1");
                    dgvList.CurrentCell = dgvList.Rows[0].Cells["colFileCode"];
                    return false;
                }
            }
            foreach (DataGridViewRow dgvRow in dgvList.Rows)
            {
                if (mclsCFunc.ConvertToString(dgvRow.Cells["colFileCode"].Value) != "")
                {
                    if (mclsCFunc.ConvertToString(dgvRow.Cells["colLocationCode"].Value) == "")
                    {
                        MessageBox.Show("Transfer Location Required In SL.NO. " +
                            Convert.ToString(dgvRow.Index + 1));
                        dgvList.CurrentCell = dgvRow.Cells["colLocationCode"];
                        return false;
                    }
                    if (mclsCFunc.ConvertToString(dgvRow.Cells["colCarrier"].Value) == "")
                    {
                        MessageBox.Show("Carrier Required In SL.NO. " +
                            Convert.ToString(dgvRow.Index + 1));
                        dgvList.CurrentCell = dgvRow.Cells["colCarrier"];
                        return false;
                    }
                }
            }
            return true;
        }

        public bool checkValidFileMovement(DataGridView dgvList, int intRowIndex)
        {
            string strFileCode = mclsCFunc.ConvertToString(dgvList.Rows[intRowIndex].Cells["colFileCode"].Value);
            if (strFileCode.Trim() == "") return true;
            string strTfrLocationCode = mclsCFunc.ConvertToString(dgvList.Rows[intRowIndex].Cells["colLocationCode"].Value);
            if (strTfrLocationCode.Trim() == "") return true;
            string strExistLocationCode = mclsCFunc.ConvertToString(dgvList.Rows[intRowIndex].Cells["colExDept"].Tag);
            if (strExistLocationCode.Trim() == strTfrLocationCode.Trim())
            {
                MessageBox.Show("File Existing Location And Transfer Location Is Same In Sl.No." +
                  Convert.ToString(intRowIndex + 1));
                dgvList.CurrentCell = dgvList.Rows[intRowIndex].Cells["colLocationCode"];
                return false;
            }
            getFileDetails(strFileCode);
            if (FileMaster.Transferable == "N")
            {
                if (strExistLocationCode.ToUpper() != FileMaster.LocationPtr.ToUpper())
                {
                    if (strTfrLocationCode.ToUpper() != FileMaster.LocationPtr.ToUpper())
                    {
                        MessageBox.Show("The File Exist In  Sl.No." +
                          Convert.ToString(intRowIndex + 1) + " Can Only Transfer To " + LocationMaster.Desc);
                        dgvList.CurrentCell = dgvList.Rows[intRowIndex].Cells["colLocationCode"];
                        return false;
                    }
                }
            }
            if (FileMaster.Transfertooutside == "N")
            {
                LocationMaster.ClearAll();
                LocationMaster.Code = strTfrLocationCode;
                LocationMaster.getData();
                if (LocationMaster.Inhouse == "N")
                {
                    MessageBox.Show("The File Exist In  Sl.No." +
                         Convert.ToString(intRowIndex + 1) + " Can Only Transfer To  Inhouse Location/Department Only ");
                    dgvList.CurrentCell = dgvList.Rows[intRowIndex].Cells["colLocationCode"];
                    return false;
                }

            }
            return true;
        }

        public bool showFileLog(string strFileCode, DataGridView dgvShow)
        {
            if (strFileCode.Trim() == "") return false;
            dgvShow.DataSource = null;
            try
            {
                String strSql = @"select fme_dt as 'Date',fme_fileptr as 'File Code',FileNm  as 'File Name',
                        fl.fl_desc as 'From Location',f2.fl_desc as 'To Location',fc_name  as 'Carrier',
                        fme_remarks as 'Remakrs' from filemovement left join viewFileMas on(FileCode =fme_fileptr)
                        left join filelocationmas as fl on(fl.fl_code =fme_frmlocptr)
                        left join filelocationmas as f2 on(f2.fl_code =fme_tolocptr)
                        left join filecarriermas on(fc_code=fme_carrierptr)
                        where fme_fileptr='" + strFileCode + "' order by   fme_id desc";
                DataTable dtData = mGlobal.LocalDBCon.ExecuteQuery(strSql);
                dgvShow.DataSource = dtData;
                dgvShow.Refresh();
                if (dgvShow.Rows.Count > 0)
                    return true;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
            return false;
        }

        public void fillLocationAndCarrierInGridRow(DataGridView dgvList, string strLcode,
            string strLName, string strCCode, string strRemarks)
        {
            foreach (DataGridViewRow dgvRow in dgvList.Rows)
            {
                if (mclsCFunc.ConvertToString(dgvRow.Cells["colFileCode"].Value) != "")
                {
                    if (mclsCFunc.ConvertToString(dgvRow.Cells["colLocationCode"].Value) == "" && strLcode.Trim() != "")
                    {
                        dgvRow.Cells["colLocationCode"].Value = strLcode;
                        dgvRow.Cells["colLocationNm"].Value = strLName;
                    }
                    if (mclsCFunc.ConvertToString(dgvRow.Cells["colCarrier"].Value) == "" && strCCode.Trim() != "")
                    {
                        DataGridViewComboBoxCell dgvCmbCell = new DataGridViewComboBoxCell();
                        dgvCmbCell = (DataGridViewComboBoxCell)dgvRow.Cells["colCarrier"];
                        dgvCmbCell.Value = strCCode;
                    }
                    if (mclsCFunc.ConvertToString(dgvRow.Cells["colRemarks"].Value) == "" && strRemarks.Trim() != "")
                    {
                        dgvRow.Cells["colRemarks"].Value = strRemarks;
                    }
                }
            }
        }
    }
}