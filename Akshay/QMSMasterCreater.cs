using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using System.IO;
using System.Xml;
using System.Web.UI.WebControls;
namespace CsHms.Akshay
{
    public partial class MasterCreater : Form
    {
        public MasterCreater()
        {
            InitializeComponent();
            UpdateNavButtonStatus();
            GetMFirms();
            FillGridview("select ROW_NUMBER() OVER (ORDER BY id) AS Slno,id as Id,code as Code,description as Description,active as Active from token_firms");
            strBackupRequired=System.Configuration.ConfigurationSettings.AppSettings.Get("BackupRequired");
            if (strBackupRequired == "Y")
                DisableFields();          
        }
        Global mGlobal = new Global();
        CommFuncs mCommFunc = new CommFuncs();
        string mFirmstbl = "token_firms";
        string mAddresstbl = "token_address";
        string mBranchestbl = "token_branches";
        string mDeptbl = "token_departments";
        string mCountertbl = "token_counters";
        string strBackupRequired = "";
        private void DisableFields()
        {
            btnCreate.Text = "Backup";
            tabCtrl.Enabled = false;
            btnClear.Enabled = false;
            dgvData.Enabled = false;
            btnNext.Enabled = false;   
        }
        

        //Gridview and master queries
        #region Query Variables
        string mstrFirmqry = "select ROW_NUMBER() OVER (ORDER BY id) AS SlNo,id as Id,code as Code,description as Description,active as Active from token_firms";
        string mstrBranchqry = "select ROW_NUMBER() OVER (ORDER BY id) AS SlNo,id as Id,code as Code,description as Description,active from token_branches";
        string mstrDepartmentqry = "select ROW_NUMBER() OVER (ORDER BY Department.id) AS SlNo,Department.id as Id,Department.code as Code,Department.description as Description,Department.active as Active,Branch.description as Branch from token_departments Department left join token_branches Branch on Department.branch_id=Branch.id";
        string mstrCounterqry = "select ROW_NUMBER() OVER (ORDER BY Counter.id) AS SlNo,Counter.id as Id,Counter.code as Code,Counter.display_text as Description,Counter.active as Active,Branch.description as Branch from token_counters Counter left join token_branches Branch on Counter.branch_id=Branch.id";
        string mstrServiceqry = "select ROW_NUMBER() OVER (ORDER BY Service.id) AS SlNo,Service.id as Id,Service.code as Code,Service.option_text as Description,Service.active as Active,Branch.description as Branch from token_services Service left join token_branches Branch on TRY_CAST(Service.branch_ids as int)=Branch.id";
        string mstrDeviceqry = "select ROW_NUMBER() OVER (ORDER BY Device.id) AS SlNo,Device.id as Id,Device.code as Code,Device.description as Description,Device.active as Active,Branch.description as Branch from token_devices Device left join token_branches Branch on Device.branch_id=Branch.id";
        string mstrBillqry = "select ROW_NUMBER() OVER (ORDER BY Bill.id) AS SlNo,Bill.id as Id,Bill.code as Description,Bill.numbers as Numbers,Bill.locked as Locked,Branch.description as Branch from token_bill_numbers Bill left join token_branches Branch on Bill.branch_id=Branch.id";
        string mstrKioskqry = "SELECT ROW_NUMBER() OVER (ORDER BY Kiosk.id) AS SlNo,Kiosk.id AS Id,Kiosk.code AS Code,Kiosk.description AS Description, Kiosk.active AS Active,Device.id as DeviceId,Branch.description as Branch FROM token_kiosk_settings kiosk LEFT JOIN token_devices Device ON Kiosk.id = Device.token_setting_id LEFT JOIN token_branches Branch on Device.branch_id=Branch.id WHERE type = 'KIOSK'";
        string mstrDisplayqry = "SELECT ROW_NUMBER() OVER (ORDER BY Display.id) AS SlNo,Display.id AS Id,Display.logo_path AS LogoPath,Display.welcome_note AS Welcomenote,Display.active AS Active,Device.id as DeviceId,Branch.description as Branch FROM token_display_settings Display LEFT JOIN token_devices Device ON Display.id = Device.token_setting_id LEFT JOIN token_branches Branch on Device.branch_id=Branch.id WHERE type = 'DISPLAY'";
        string mstrTemplateqry = "SELECT ROW_NUMBER() OVER (ORDER BY mt_templateid) AS SlNO,mt_templateid as Id,mt_code as Code,mt_desc as Description,mt_active as Active from msgtemplate";
        string mstrUserSettingsqry = "SELECT ROW_NUMBER() OVER (ORDER BY id) AS SlNo,id as Id,name as Name,email as Email,password as Password from core_users";
        string mstrSql = "";
        #endregion

        //Textbox validating events
        //#region Textbox validating
        //private void txtFirmCode_Validating(object sender, CancelEventArgs e)
        //{
        //    try
        //    {

        //        FillFirm();

        //    }
        //    catch (Exception ex)
        //    {
        //        writeErrorLog(ex, "txtFirmCode_Validating");
        //    }

        //}
        //private void txtBrchcode_Validating(object sender, CancelEventArgs e)
        //{
        //    try
        //    {
        //        FillBranch();
        //    }
        //    catch (Exception ex)
        //    { writeErrorLog(ex, "txtBrchcode_Validating"); }
        //}
        //private void txtDepcode_Validating(object sender, CancelEventArgs e)
        //{
        //    try
        //    { FillDepartment(); }
        //    catch (Exception ex)
        //    {
        //        writeErrorLog(ex, "txtDepcode_Validating");
        //    }
        //}
        //private void txtSrvccode_Validating(object sender, CancelEventArgs e)
        //{
        //    try
        //    { FillServices(); }
        //    catch (Exception ex)
        //    { writeErrorLog(ex, "txtSrvccode_Validating"); }
        //}
        //private void txtDevicecode_Validating(object sender, CancelEventArgs e)
        //{
        //    try
        //    { FillDevices(); }
        //    catch (Exception ex)
        //    { writeErrorLog(ex, "txtDevicecode_Validating"); }
        //}
        //private void txtCounterCode_Validating(object sender, CancelEventArgs e)
        //{
        //    try
        //    {
        //        FillCounter();

        //    }
        //    catch (Exception ex)
        //    {
        //        writeErrorLog(ex, "txtCounterCode_Validating");
        //    }
        //}
        //private void txtTempCode_Validating(object sender, CancelEventArgs e)
        //{
        //    try
        //    {
        //        FillTemplate();

        //    }
        //    catch (Exception ex)
        //    { writeErrorLog(ex, "txtTempCode_Validating"); }

        //}
        //private void txtBillCode_Validating(object sender, CancelEventArgs e)
        //{
        //    try
        //    {
        //        FillBill();
        //    }
        //    catch (Exception ex)
        //    { writeErrorLog(ex, "txtBillCode_Validating"); }
        //}
        //#endregion

        //Checking for already existing or not for updation
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
                {

                    return true;
                }
            }
            catch (Exception ex)
            {
                writeErrorLog(ex, "CheckAlreadyExist");
                return false;
            }
            return false;
        }
        
        //Tab changing event
        private void tabCtrl_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                AddressClearAll();
                btnCreate.Text = "Create";
                switch (tabCtrl.SelectedTab.Name)
                {
                    case "tbFirm":
                        tabCtrl.TabPages[tabCtrl.SelectedIndex].Controls.Add(pnlAddress);
                        HideMBranch();
                        HideMFirm();
                        FillGridview(mstrFirmqry);
                        if (txtFirmCode.Text.Trim() != "")
                            FillFirm();
                        break;

                    case "tbBranch":
                        GetFirms();
                        tabCtrl.TabPages[tabCtrl.SelectedIndex].Controls.Add(pnlAddress);
                        FillGridview(mstrBranchqry);
                        GetBranch();
                        GetDepartment();
                        GetCounter();
                        GetService();
                        HideMBranch();
                        DisplayMFirm();
                        if (txtBrchcode.Text.Trim() != "")
                            FillBranch();
                        break;

                    case "tbDepartment":

                        GetFirms();
                        GetBranch();
                        FillGridview(mstrDepartmentqry);
                        DisplayMFirm();
                        DisplayMBranch();
                        if (txtDepcode.Text.Trim() != "")
                            FillDepartment();
                        cbxDepBranch.SelectedItem = null;
                        cbxDepBranch.SelectedText = "--select--";
                        break;

                    case "tbCounter":
                        GetFirms();
                        GetBranch();
                        GetDepartment();
                        DisplayMFirm();
                        DisplayMBranch();
                        FillGridview(mstrCounterqry);
                        if (txtCounterCode.Text.Trim() != "")
                            FillCounter();
                        break;
                    case "tbService":
                        GetFirms();
                        GetBranch();
                        GetDepartment();
                        GetCounter();
                        GetTemplates();
                        DisplayMFirm();
                        DisplayMBranch();
                        cbxSrvcType.SelectedItem = "NEW";
                        FillGridview(mstrServiceqry);
                        
                        if (txtSrvccode.Text.ToString().Trim() != "")
                            FillServices();
                        break;

                    case "tbBill":
                        GetFirms();
                        GetBranch();
                        GetService();
                        FillGridview(mstrBillqry);
                        DisplayMFirm();
                        DisplayMBranch();
                        if (txtBillCode.Text.ToString().Trim() != "")
                            FillBill();
                        break;
                    case "tbDevice":
                        FillGridview(mstrDeviceqry);
                        GetTemplates();
                        GetBranch();
                        DisplayMBranch();
                        HideMFirm();
                        if (txtDevicecode.Text.ToString().Trim() != "")
                            FillDevices();
                        break;
                    case "tbTemplate":
                        FillGridview(mstrTemplateqry);
                        HideMBranch();
                        HideMFirm();
                        if (txtTempCode.Text.ToString() != "")
                            FillTemplate();
                        break;
                    case "tbKiosk":
                        FillGridview(mstrKioskqry);
                        GetDevice();
                        DisplayMBranch();
                        HideMFirm();
                        if (txtKioskCode.Text.ToString().Trim() != "")
                            FillKiosk();
                        break;
                    case "tbDisplay":
                        FillGridview(mstrDisplayqry);
                        GetDevice();
                        GetDepartment();
                        DisplayMBranch();
                        HideMFirm();
                        if (txtDisplayLogopath.Text.ToString().Trim() != "")
                            FillDisplay();
                        break;
                    case "tbSettings":
                        GetcbxSetDepartment();
                        GetcbxSetNxtdepartment();
                        GetcbxSetEnddepartment();
                        GetcbxSetNxtcounter();
                        GetcbxSetEndcounter();
                        GetcbxSetCounter();
                        GetService();
                        HideMBranch();
                        HideMFirm();
                        GetSettingsSrvcFirms();
                        GetSettingsCounterFirms();
                        switch (tabSettings.SelectedTab.Name)
                        {
                            case "tbNextdepartment":
                                FillGridview(mstrDepartmentqry);                                
                                GetSettingsDepFirms();
                               
                                break;
                            case "tbNextcounter":
                                FillGridview(mstrCounterqry);
                                GetSettingsCounterFirms();
                                break;
                            case "tbSettingservice":
                                FillGridview(mstrServiceqry);
                                GetSettingsSrvcFirms();
                                break;
                            default:
                                break;
                        }
                        break;
                    case "tbUsersettings":
                        GetFirms();
                        FillGridview(mstrUserSettingsqry);
                        HideMFirm();
                        HideMBranch();
                        break;


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

        //Filter comboboxes
        #region Filter Combobox
        private void cbxDepFirm_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            { FiltercbxDepBranch(); }
            catch (Exception ex) { writeErrorLog(ex, "cbxDepFirm_SelectionChangeCommitted"); }
        }
        private void FiltercbxDepBranch()
        {
            try
            {
                if (cbxDepFirm.SelectedValue.ToString() != "")
                {
                    mstrSql = @"select id,description from token_branches where firm_id='" + cbxDepFirm.SelectedValue.ToString() + "'";
                    DataTable dtData = mGlobal.LocalDBCon.ExecuteQuery(mstrSql);
                    if (dtData.Rows.Count > 0)
                    {
                        dtData.Rows.Add(-1, "--select--");
                        cbxDepBranch.DataSource = dtData;
                        cbxDepBranch.DisplayMember = "description";
                        cbxDepBranch.ValueMember = "id";
                        cbxDepBranch.Enabled = true;
                        cbxDepBranch.SelectedValue = "-1";
                    }
                    else
                    {

                        
                        cbxDepBranch.Enabled = false;
                        dtData.Rows.Add(-2, "Not Found");
                        cbxDepBranch.SelectedValue = "-2";
                    }
                }


            }
            catch (Exception ex)
            { writeErrorLog(ex, "FiltercbxDepBranch"); }
        }
        private void cbxCounterFirm_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {

                cbxCounterDep.SelectedValue = "-1";
                cbxCounterDep.Enabled = false;
                FiltercbxCounterBranch();
            }
            catch (Exception ex)
            { writeErrorLog(ex, "cbxCounterFirm_SelectionChangeCommitted"); }
        }
        private void FiltercbxCounterBranch()
        {
            try
            {
                if (cbxCounterFirm.SelectedValue.ToString() != "")
                {
                    mstrSql = @"select id,description from token_branches where firm_id='" + cbxCounterFirm.SelectedValue.ToString() + "'";
                    DataTable dtData = mGlobal.LocalDBCon.ExecuteQuery(mstrSql);
                    if (dtData.Rows.Count > 0)
                    {
                        dtData.Rows.Add(-1, "--select--");
                        cbxCounterBranch.DataSource = dtData;
                        cbxCounterBranch.ValueMember = "id";
                        cbxCounterBranch.DisplayMember = "description";
                        cbxCounterBranch.Enabled = true;
                        cbxCounterBranch.SelectedValue = "-1";
                    }
                    else
                    {                       
                        cbxCounterBranch.Enabled = false;
                        dtData.Rows.Add(-1, "--select--");
                        cbxCounterBranch.SelectedValue = "-1";                        
                        
                    }
                }
            }
            catch (Exception ex) { writeErrorLog(ex, "FiltercbxCounterBranch"); }

        }
        private void cbxCounterBranch_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                FiltercbxCounterDep();
            }
            catch (Exception ex)
            { writeErrorLog(ex, "cbxCounterBranch_SelectionChangeCommitted"); }
        }
        private void FiltercbxCounterDep()
        {
            try
            {
                if (cbxCounterBranch.SelectedValue.ToString() != "")
                {
                    mstrSql = @"select id,description from token_departments where branch_id='" + cbxCounterBranch.SelectedValue.ToString() + "'";
                    DataTable dtData = mGlobal.LocalDBCon.ExecuteQuery(mstrSql);
                    if (dtData.Rows.Count > 0)
                    {
                        dtData.Rows.Add(-1, "--select--");
                        cbxCounterDep.DataSource = dtData;
                        cbxCounterDep.ValueMember = "id";
                        cbxCounterDep.DisplayMember = "description";
                        cbxCounterDep.Enabled = true;
                        cbxCounterDep.SelectedValue = "-1";
                    }
                    else
                    {
                        cbxCounterDep.Enabled = false;
                        dtData.Rows.Add(-1, "--select--");
                        cbxCounterDep.SelectedValue = "-1";
                    }
                }
            }
            catch (Exception ex)
            { writeErrorLog(ex, "FiltercbxCounterDep"); }
        }
        private void cbxSrvcFirm_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                cbxSrvcBranch.SelectedValue = "-1";
                
                cbxSrvcDefDept.Enabled = false;
                cbxSrvcDefCounter.Enabled = false;                
                FiltercbxSrvcBranch();
            }
            catch (Exception ex)
            { writeErrorLog(ex, "cbxSrvcFirm_SelectionChangeCommitted"); }
        }
        private void FiltercbxSrvcBranch()
        {
            try
            {
                if (cbxSrvcFirm.SelectedValue.ToString() != "")
                {
                    mstrSql = "select id,description from token_branches where firm_id='" + cbxSrvcFirm.SelectedValue.ToString() + "'";
                    DataTable dtData = mGlobal.LocalDBCon.ExecuteQuery(mstrSql);
                    if (dtData.Rows.Count > 0)
                    {
                        dtData.Rows.Add(-1, "--select--");
                        cbxSrvcBranch.DataSource = dtData;
                        cbxSrvcBranch.DisplayMember = "description";
                        cbxSrvcBranch.ValueMember = "id";
                        cbxSrvcBranch.Enabled = true;
                        cbxSrvcBranch.SelectedValue = "-1";
                    }
                    else
                    {
                        cbxSrvcBranch.SelectedValue = "-1";
                        cbxSrvcBranch.Enabled = false;
                        dtData.Rows.Add(-2, "Not Found");
                        cbxSrvcBranch.SelectedValue = "-2";
                    }
                }
            }
            catch (Exception ex)
            { writeErrorLog(ex, "FiltercbxSrvcBranch"); }
        }
        private void cbxSrvcDefdept_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                FiltercbxSrvcDefcounter();
            }
            catch (Exception ex)
            { writeErrorLog(ex, "cbxSrvcDefdept_SelectionChangeCommitted"); }
        }
        private void FiltercbxSrvcDefcounter()
        {
            try
            {
                if (cbxSrvcDefDept.SelectedValue.ToString() != "")
                {
                    mstrSql = "select id,display_text from token_counters where department_id='" + cbxSrvcDefDept.SelectedValue.ToString() + "'";
                    DataTable dtData = mGlobal.LocalDBCon.ExecuteQuery(mstrSql);
                    if (dtData.Rows.Count > 0)
                    {
                        dtData.Rows.Add(-1, "--select--");
                        cbxSrvcDefCounter.DataSource = dtData;
                        cbxSrvcDefCounter.DisplayMember = "display_text";
                        cbxSrvcDefCounter.ValueMember = "id";
                        cbxSrvcDefCounter.Enabled = true;
                        cbxSrvcDefCounter.SelectedValue = "-1";
                    }
                    else
                    {                        
                        cbxSrvcDefCounter.Enabled = false;
                    }
                }
            }
            catch (Exception ex)
            { writeErrorLog(ex, "FiltercbxSrvcDefcounter"); }
        }
       
        private void cbxSrvcBranch_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                FiltercbxSrvcDefdept();                
                cbxSrvcDefDept.SelectedValue = "-1";                
                cbxSrvcDefCounter.SelectedValue = "-1";                
                cbxSrvcDefCounter.Enabled = false;
               
            }
            catch (Exception ex)
            { writeErrorLog(ex, "cbxSrvcBranch_SelectionChangeCommitted"); }
        }
        private void FiltercbxSrvcDefdept()
        {
            try
            {
                if (cbxSrvcBranch.SelectedValue.ToString() != "")
                {
                    mstrSql = "select id,description from token_departments where branch_id='" + cbxSrvcBranch.SelectedValue.ToString() + "'";
                    DataTable dtData = mGlobal.LocalDBCon.ExecuteQuery(mstrSql);
                    
                    if (dtData.Rows.Count > 0)
                    {
                        dtData.Rows.Add(-1, "--select--");
                     
                        cbxSrvcDefDept.DataSource = dtData;
                        cbxSrvcDefDept.DisplayMember = "description";
                        cbxSrvcDefDept.ValueMember = "id";
                        cbxSrvcDefDept.Enabled = true;
                        cbxSrvcDefDept.SelectedValue = "-1";

                                          
                       
                    }
                    else
                    {

                        cbxSrvcDefDept.SelectedValue = "-1";
                        cbxSrvcDefDept.Enabled = false;
                      
                        
                    }
                }
            }
            catch (Exception ex)
            { writeErrorLog(ex, "FiltercbxSrvcDefdept"); }
        }        
        private void cbxBillFirm_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                cbxBillService.SelectedValue = "-1";
                cbxBillService.Enabled = false;
                FiltercbxBillBranch();

            }
            catch (Exception ex)
            { writeErrorLog(ex, "cbxBillFirm_SelectionChangeCommitted"); }
        }
        private void FiltercbxBillBranch()
        {
            try
            {
                if (cbxBillFirm.SelectedValue.ToString().Trim() != "")
                {
                    mstrSql = "select id,description from token_branches where firm_id='" + cbxBillFirm.SelectedValue.ToString() + "'";
                    DataTable dtData = mGlobal.LocalDBCon.ExecuteQuery(mstrSql);
                    if (dtData.Rows.Count > 0)
                    {
                        dtData.Rows.Add(-1, "--select--");
                        cbxBillBranch.DataSource = dtData;
                        cbxBillBranch.DisplayMember = "description";
                        cbxBillBranch.ValueMember = "id";
                        cbxBillBranch.Enabled = true;
                        cbxBillBranch.SelectedValue = "-1";
                    }
                    else
                    {

                        cbxBillBranch.SelectedValue = "-1";
                        cbxBillBranch.Enabled = false;
                        dtData.Rows.Add(-2, "Not Found");
                        cbxBillBranch.SelectedValue = "-2";
                    }
                }

            }
            catch (Exception ex)
            { writeErrorLog(ex, "FiltercbxBillBranch"); }
        }
        private void cbxBillBranch_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                FiltercbxBillService();
            }
            catch (Exception ex)
            { writeErrorLog(ex, "cbxBillBranch_SelectionChangeCommitted"); }
        }
        private void FiltercbxBillService()
        {
            try
            {
                if (cbxBillBranch.SelectedValue.ToString().Trim() != "")
                {
                    mstrSql = "select id,code+'_'+option_text as code  from token_services where branch_ids='" + "\"" + cbxBillBranch.SelectedValue.ToString() + "\"" + "'";
                    DataTable dtData = mGlobal.LocalDBCon.ExecuteQuery(mstrSql);
                    if (dtData.Rows.Count > 0)
                    {
                        dtData.Rows.Add(-1, "--select--");
                        cbxBillService.DataSource = dtData;
                        cbxBillService.DisplayMember = "code";
                        cbxBillService.ValueMember = "id";
                        cbxBillService.Enabled = true;
                        cbxBillService.SelectedValue = "-1";
                    }
                    else
                    {

                        dtData.Rows.Add(-2, "Not Found");                        
                        cbxBillService.DataSource = dtData;
                        cbxBillService.DisplayMember = "code";
                        cbxBillService.ValueMember = "id";
                        cbxBillService.Enabled = false;
                        cbxBillService.SelectedValue = "-2";
                    }
                }

            }
            catch (Exception ex)
            { writeErrorLog(ex, "FiltercbxBillService"); }
        }
        #endregion       

        //Filling Kiosk and Display 
        private void cbxKioskDevices_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            { FillKiosk(); }
            catch (Exception ex) { writeErrorLog(ex, "cbxKioskDevices_SelectionChangeCommitted"); }
        }
        private void cbxDisplayDevices_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                GetDisplayDepartment();
                FillDisplay();
           
        }
            catch (Exception ex) { writeErrorLog(ex, "cbxDisplayDevices_SelectionChangeCommitted"); }
        }

        //Grid double click even for view and update data 
        private void dgvData_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                btnCreate.Tag = "";
                switch (tabCtrl.SelectedTab.Name)
                {
                    case "tbBranch":
                        txtBrchcode.Tag = mCommFunc.ConvertToString(dgvData.CurrentRow.Cells["Id"].Value);
                        txtBrchcode.Text = mCommFunc.ConvertToString(dgvData.CurrentRow.Cells["Code"].Value);
                        if (txtBrchcode.Text.Trim() != "")
                            FillBranch();
                        break;
                    case "tbFirm":
                        txtFirmCode.Tag = mCommFunc.ConvertToString(dgvData.CurrentRow.Cells["Id"].Value);
                        txtFirmCode.Text = mCommFunc.ConvertToString(dgvData.CurrentRow.Cells["Code"].Value);
                        if (txtFirmCode.Text.Trim() != "")
                            FillFirm();
                        break;
                    case "tbDepartment":
                        txtDepcode.Tag = mCommFunc.ConvertToString(dgvData.CurrentRow.Cells["Id"].Value);
                        txtDepcode.Text = mCommFunc.ConvertToString(dgvData.CurrentRow.Cells["Code"].Value);
                        if (txtDepcode.Text.ToString().Trim() != "")
                        {
                            GetBranch();
                            cbxDepBranch.Enabled = false;
                            FillDepartment();
                        }
                        break;
                    case "tbCounter":
                        txtCounterCode.Tag = mCommFunc.ConvertToString(dgvData.CurrentRow.Cells["Id"].Value);
                        txtCounterCode.Text = mCommFunc.ConvertToString(dgvData.CurrentRow.Cells["Code"].Value);
                        if (txtCounterCode.Text.ToString() != "")
                        {
                            GetBranch();
                            GetDepartment();
                            cbxCounterBranch.Enabled = false;
                            cbxCounterDep.Enabled = false;
                            FillCounter();
                        }
                        break;
                    case "tbService":
                        txtSrvccode.Tag = mCommFunc.ConvertToString(dgvData.CurrentRow.Cells["Id"].Value);
                        txtSrvccode.Text = mCommFunc.ConvertToString(dgvData.CurrentRow.Cells["Code"].Value);
                        if (txtSrvccode.Text.ToString() != "")
                        {
                            GetBranch();
                            GetDepartment();
                            GetCounter();                           
                            FillServices();
                            cbxSrvcBranch.Enabled = false;
                            cbxSrvcDefDept.Enabled = false;
                            cbxSrvcDefCounter.Enabled = false;
                           
                        }
                        break;
                    case "tbBill":
                        txtBillCode.Text = mCommFunc.ConvertToString(dgvData.CurrentRow.Cells["Description"].Value);
                        txtBillCode.Tag = mCommFunc.ConvertToString(dgvData.CurrentRow.Cells["Id"].Value);
                        if (txtBillCode.Text.ToString() != "")
                        {
                            GetBranch();
                            GetService();
                            cbxBillBranch.Enabled = false;
                            cbxBillService.Enabled = false;
                            FillBill();
                        }

                        break;
                    case "tbTemplate":
                        txtTempCode.Tag = mCommFunc.ConvertToString(dgvData.CurrentRow.Cells["Id"].Value);
                        txtTempCode.Text = mCommFunc.ConvertToString(dgvData.CurrentRow.Cells["Code"].Value);
                        if (txtTempCode.Text.ToString() != "")
                        {
                            FillTemplate();
                        }
                        break;
                    case "tbDevice":
                        txtDevicecode.Tag = mCommFunc.ConvertToString(dgvData.CurrentRow.Cells["Id"].Value);
                        txtDevicecode.Text = mCommFunc.ConvertToString(dgvData.CurrentRow.Cells["Code"].Value);
                        if (txtDevicecode.Text.ToString() != "")
                        {
                            FillDevices();
                        }                        
                        break;
                    case "tbKiosk":
                        cbxKioskDevices.SelectedValue = dgvData.CurrentRow.Cells["DeviceId"].Value.ToString();
                        if (cbxKioskDevices.SelectedValue != null)
                        {
                            FillKiosk();
                        }
                        else
                            KioskClearall(true);

                        break;
                    case "tbDisplay":
                        
                        cbxDisplayDevices.SelectedValue = mCommFunc.ConvertToString(dgvData.CurrentRow.Cells["DeviceId"].Value);
                        if (cbxDisplayDevices.SelectedValue != null)
                        {
                            GetDisplayDepartment();
                            FillDisplay(); }
                        else
                            DisplayClearall(true);
                        break;
                    case "tbSettings":

                        //switch (tabSettings.SelectedTab.Name)
                        //{
                        //    case "tbNextdepartment":
                               
                        //        cbxSetDepartment.SelectedValue = mCommFunc.ConvertToString(dgvData.CurrentRow.Cells["Id"].Value);
                        //        FilterNextDepartment();
                        //        FiltercbxSetDepBranch();
                        //        FiltercbxSetDepartments();                                
                               
                        //        break;
                        //    case "tbNextcounter":
                        //        cbxSetCounter.SelectedValue = mCommFunc.ConvertToString(dgvData.CurrentRow.Cells["Id"].Value);
                        //        FillNextCounter();
                        //        break;
                        //    case "tbSettingservice":
                        //        cbxSetSrvc.SelectedValue = mCommFunc.ConvertToString(dgvData.CurrentRow.Cells["Id"].Value);
                        //        FillSrvcEndCouterDep();
                        //        break;
                        //    default:
                        //        break;
                        //}
                        
                        break;
                    case "tbUsersettings":
                        txtUserName.Tag = mCommFunc.ConvertToString(dgvData.CurrentRow.Cells["Id"].Value);
                        if (txtUserName.Tag != null && txtUserName.Tag.ToString() != "")
                            GetDepartment();
                        GetCounter();
                            //FilterUserDepartments();                        
                            FillUserSettings();
                       
                        break;
                    default:
                        AddressClearAll();
                        break;
                }
            }
            catch (Exception ex)
            { writeErrorLog(ex, "dgvData_CellDoubleClick"); }

        }
        private void FilterNextDepartment()
        {
            try
            {   
                string strSql="";
                if (cbxSetDepBranch.SelectedValue.ToString() != "")
                    strSql = "select id,description from token_departments where branch_id='" + cbxSetDepBranch.SelectedValue.ToString() + "' and id!='"+mCommFunc.ConvertToString(cbxSetDepartment.SelectedValue)+"'";
                else
                    strSql = "select id,description from token_departments";
                DataTable dtDepartments = mGlobal.LocalDBCon.ExecuteQuery(strSql);
                if (dtDepartments.Rows.Count > 0)
                {
                    dtDepartments.Rows.Add("-1", "--select--");

                    cbxSetNxtdepartment.DataSource = dtDepartments;
                    cbxSetNxtdepartment.DisplayMember = "description";
                    cbxSetNxtdepartment.ValueMember = "id";
                    cbxSetNxtdepartment.SelectedValue = "-1";
                    cbxSetNxtdepartment.Enabled = true;
                    
                }
                else
                {
                    cbxSetNxtdepartment.Enabled = false;

                }
                 strSql = "select next_department_id,firm_id,branch_id from token_departments where id='"+cbxSetDepartment.SelectedValue.ToString()+"'";
                DataTable dtNxtdep = mGlobal.LocalDBCon.ExecuteQuery(strSql);
                if (dtNxtdep.Rows[0]["next_department_id"].ToString()!="")
                {
                    btnCreate.Text = "Update";
                    cbxSetNxtdepartment.SelectedValue = dtNxtdep.Rows[0]["next_department_id"].ToString();
                    cbxSetDepFirm.SelectedValue = dtNxtdep.Rows[0]["firm_id"].ToString();
                    cbxSetDepBranch.SelectedValue = dtNxtdep.Rows[0]["branch_id"].ToString();
                    cbxSetDepFirm.Enabled=true;
                    cbxSetDepBranch.Enabled = true;
                    cbxSetNxtdepartment.Enabled = true;
                    cbxSetDepartment.Enabled = true;

                }
                else
                {
                    btnCreate.Text = "Create";
                    cbxSetNxtdepartment.SelectedValue = "-1";
                    cbxSetDepFirm.SelectedValue = dtNxtdep.Rows[0]["firm_id"].ToString();
                    cbxSetDepBranch.SelectedValue = dtNxtdep.Rows[0]["branch_id"].ToString();
                    cbxSetDepFirm.Enabled = true;
                    cbxSetDepBranch.Enabled = true;
                    cbxSetNxtdepartment.Enabled = true;
                    cbxSetDepartment.Enabled = true;
                }
            }
            catch (Exception ex)
            { writeErrorLog(ex, "FilterNextDepartment"); }

        }
        private void FillNextCounter()
        {
            try
            {
                string strSql = "";

                if (cbxSetCounterBranch.SelectedValue.ToString() != "")
                    strSql = "select id,display_text from token_counters where branch_id='" + cbxSetCounterBranch.SelectedValue.ToString() + "' and id!='" + mCommFunc.ConvertToString(cbxSetCounter.SelectedValue)+ "'";
                else
                    strSql = "select id,display_text from token_counters";
                DataTable dtDepartments = mGlobal.LocalDBCon.ExecuteQuery(strSql);
                if (dtDepartments.Rows.Count > 0)
                {
                    dtDepartments.Rows.Add("-1", "--select--");            

                    cbxSetNxtcounter.DataSource = dtDepartments;
                    cbxSetNxtcounter.DisplayMember = "display_text";
                    cbxSetNxtcounter.ValueMember = "id";
                    cbxSetNxtcounter.SelectedValue = "-1";
                    cbxSetNxtcounter.Enabled = true;
                }
                else
                {
                    cbxSetNxtcounter.Enabled = false;

                }




                 strSql = "select next_counter_id from token_counters where id='" + cbxSetCounter.SelectedValue.ToString() + "'";
                DataTable dtNxtCounter = mGlobal.LocalDBCon.ExecuteQuery(strSql);
                if (dtNxtCounter.Rows[0]["next_counter_id"].ToString() != "")
                {
                    btnCreate.Text = "Update";
                    cbxSetNxtcounter.SelectedValue = dtNxtCounter.Rows[0]["next_counter_id"].ToString();
                    cbxSetNxtcounter.Enabled = true;
                }
                else
                {
                    btnCreate.Text = "Create";
                    cbxSetNxtcounter.SelectedValue = "-1";
                }
            }
            catch (Exception ex)
            { writeErrorLog(ex, "FillNextCounter"); }

        }
        private void FillSrvcEndCouterDep()
        {
            try
            {
                cbxSetEndcounter.Enabled = true;
                cbxSetEnddepartment.Enabled = true;
                string strSql = "select ending_counter_id,ending_department_id from token_services where id='" +cbxSetSrvc.SelectedValue.ToString() + "'";
                DataTable dtEndcounterdep = mGlobal.LocalDBCon.ExecuteQuery(strSql);
                if (dtEndcounterdep.Rows[0]["ending_counter_id"].ToString() != "" || dtEndcounterdep.Rows[0]["ending_department_id"].ToString() != "")
                {
                    btnCreate.Text = "Update";                    
                    cbxSetEndcounter.SelectedValue = dtEndcounterdep.Rows[0]["ending_counter_id"].ToString();
                    cbxSetEnddepartment.SelectedValue = dtEndcounterdep.Rows[0]["ending_department_id"].ToString();
                }
                else
                {
                    btnCreate.Text = "Create";
                    cbxSetEndcounter.SelectedValue="-1";
                    cbxSetEnddepartment.SelectedValue = "-1";
                }
            }
            catch (Exception ex)
            { writeErrorLog(ex, "FillSrvcEndCouterDep"); }

        }
        private void FillUserSettings()
        {
            try
            {
                string strSql = @"SELECT * FROM core_users where id='" + mCommFunc.ConvertToString(txtUserName.Tag) + "'";
                DataTable dtUsersettings = mGlobal.LocalDBCon.ExecuteQuery(strSql);
                if (dtUsersettings != null && dtUsersettings.Rows.Count > 0)
                {
                    btnCreate.Text = "Update";
                    txtUserName.Text = mCommFunc.ConvertToString(dtUsersettings.Rows[0]["name"]);
                    txtUserEmail.Text = mCommFunc.ConvertToString(dtUsersettings.Rows[0]["email"]);
                    txtUserPassword.Text = mCommFunc.ConvertToString(dtUsersettings.Rows[0]["password"]);
                    cbxUserGroups.SelectedItem = mCommFunc.ConvertToString(dtUsersettings.Rows[0]["user_group"]);
                    cbxUserFirms.SelectedValue = dtUsersettings.Rows[0]["firm_id"];
                    FilterUserBranches();
                    cbxUserBranches.SelectedValue = dtUsersettings.Rows[0]["branch_id"];


                    string jsonString = mCommFunc.ConvertToString(dtUsersettings.Rows[0]["other_details"]);



                    // Find the index of "department_ids" and "counter_ids" in the JSON string
                    int departmentIdsIndex = jsonString.IndexOf("\"department_ids\":") + "\"department_ids\":".Length;
                    int counterIdsIndex = jsonString.IndexOf("\"counter_ids\":") + "\"counter_ids\":".Length;

                    // Extract the values between the brackets for department_ids
                    int departmentIdsStartIndex = jsonString.IndexOf('[', departmentIdsIndex) + 1;
                    int departmentIdsEndIndex = jsonString.IndexOf(']', departmentIdsStartIndex);
                    string departmentIds = jsonString.Substring(departmentIdsStartIndex, departmentIdsEndIndex - departmentIdsStartIndex);

                    // Extract the values between the brackets for counter_ids
                    int counterIdsStartIndex = jsonString.IndexOf('[', counterIdsIndex) + 1;
                    int counterIdsEndIndex = jsonString.IndexOf(']', counterIdsStartIndex);
                    string counterIds = jsonString.Substring(counterIdsStartIndex, counterIdsEndIndex - counterIdsStartIndex);                   
                    FilterUserDepartments();                    
                    string[] departmentIdsArray = departmentIds.Split(',');
                    string[] counterIdsArray = counterIds.Split(',');

                    // Parse each string into an integer and check the item
                    foreach (string idString in departmentIdsArray)
                    {
                        int departmentId;
                        if (int.TryParse(idString, out departmentId))
                        {
                            CheckItemByValue(departmentId, chklstbxUserDepartments);
                        }
                        else
                        {
                            // Handle the case where the conversion fails, e.g., log an error or show a message.
                        }
                    }

                    
                    FilterUserCounters(departmentIds);
                    chklstbxUserCounters.Enabled = false;
                    // Parse each string into an integer and check the item
                    foreach (string idString in counterIdsArray)
                    {
                        int departmentId;
                        if (int.TryParse(idString, out departmentId))
                        {
                            CheckItemByValue(departmentId, chklstbxUserCounters);
                        }
                        else
                        {
                            // Handle the case where the conversion fails, e.g., log an error or show a message.
                        }
                    }
                }
                else
                    btnCreate.Text = "Update";
            }
            catch (Exception ex)
            {
                writeErrorLog(ex, "FillUserSettings");
            }
        }


        //Create json data
        #region JSON window
        private void btnDepotherdet_Click(object sender, EventArgs e)
        {
            try
            {
                using (JSONMaker jsonfrm = new JSONMaker())
                {
                    jsonfrm.ShowDialog();
                    if(!string.IsNullOrEmpty(jsonfrm.STR_JSON))
                    txtDepotherdet.Text = jsonfrm.STR_JSON;
                }
            }
            catch (Exception ex)
            { writeErrorLog(ex, "btnDepotherdet_Click"); }
        }
        private void btnCounterOtherdet_Click(object sender, EventArgs e)
        {
            try
            {
                using (JSONMaker jsonfrm = new JSONMaker())
                {
                    jsonfrm.ShowDialog();
                    if(!string.IsNullOrEmpty(jsonfrm.STR_JSON))
                    txtCounterOtherdet.Text = jsonfrm.STR_JSON;
                }
            }
            catch (Exception ex)
            { writeErrorLog(ex, "btnCounterOtherdet_Click"); }
        }
        private void btnServiceOtherdet_Click(object sender, EventArgs e)
        {
            try
            {
                using (JSONMaker jsonfrm = new JSONMaker())
                {
                    jsonfrm.ShowDialog();
                    if(!string.IsNullOrEmpty(jsonfrm.STR_JSON))
                    txtSrvcotherdetails.Text = jsonfrm.STR_JSON;
                }
            }
            catch (Exception ex)
            { writeErrorLog(ex, "btnServiceOtherdet_Click"); }
        }
        private void btnDeviceOtherdet_Click(object sender, EventArgs e)
        {
            try
            {
                using (JSONMaker jsonfrm = new JSONMaker())
                {
                    jsonfrm.ShowDialog();
                    if(!string.IsNullOrEmpty(jsonfrm.STR_JSON))
                    txtDeviceotherdetails.Text = jsonfrm.STR_JSON;
                }
            }
            catch (Exception ex)
            {
                writeErrorLog(ex, "btnDeviceOtherdet_Click");
            }
        }
        private void btnKioskLayoutdet_Click(object sender, EventArgs e)
        {
            try
            {
                using (JSONMaker jsonfrm = new JSONMaker())
                {
                    jsonfrm.Gettype("Layout");
                    jsonfrm.ShowDialog();                    
                    if(!string.IsNullOrEmpty(jsonfrm.STR_JSON))
                    txtKioskLayoutdet.Text = jsonfrm.STR_JSON;
                }
            }
            catch (Exception ex)
            {
                writeErrorLog(ex, "btnKioskLayoutdet_Click");
            }
        }
        private void btnDisplayLayoutdet_Click(object sender, EventArgs e)
        {
            try
            {
                using (JSONMaker jsonfrm = new JSONMaker())
                {
                    jsonfrm.Gettype("Layout");
                    jsonfrm.ShowDialog();
                    if(!string.IsNullOrEmpty(jsonfrm.STR_JSON))
                    txtDisplayLayoutdetails.Text = jsonfrm.STR_JSON;
                }
            }
            catch (Exception ex)
            { writeErrorLog(ex, "btnDisplayLayoutdet_Click"); }
        }
        private void btnTemplateOtherdet_Click(object sender, EventArgs e)
        {
            try
            {
                using (JSONMaker jsonfrm = new JSONMaker())
                {
                    jsonfrm.ShowDialog();
                    if(!string.IsNullOrEmpty(jsonfrm.STR_JSON))
                    txtTempOtherdet.Text = jsonfrm.STR_JSON;
                }
            }
            catch (Exception ex)
            { writeErrorLog(ex, "btnDisplayLayoutdet_Click"); }
        }
        #endregion

        //Filling Data grid view with corresponding queries
        private void FillGridview(string strSql)
        {
            try
            {
                DataTable dtGriddata = mGlobal.LocalDBCon.ExecuteQuery(strSql);

                // Check if the selected tab is neither "tbFirm" nor "tbBranch"
                if (tabCtrl.SelectedTab.Name != "tbFirm" && tabCtrl.SelectedTab.Name != "tbBranch" && tabCtrl.SelectedTab.Name != "tbSettings" && tabCtrl.SelectedTab.Name!="tbUsersettings")
                {
                    // Add buttons only if the button column is not already present
                    if (!dgvData.Columns.Contains("btnCopy"))
                    {
                        DataGridViewButtonColumn btn = new DataGridViewButtonColumn();
                        dgvData.Columns.Insert(0, btn); // Insert button column at the first position
                        btn.HeaderText = "Action";
                        btn.Text = "Copy";
                        btn.Name = "btnCopy";
                        btn.UseColumnTextForButtonValue = true;
                    }
                }
                else
                {
                    // Remove button column if present
                    if (dgvData.Columns.Contains("btnCopy"))
                    {
                        dgvData.Columns.Remove("btnCopy");
                    }
                }

                dgvData.DataSource = dtGriddata;
                if (tabCtrl.SelectedTab.Name.ToString() != "tbDevice")
                    dgvData.Columns["Id"].Visible = false;
                else
                    dgvData.Columns["Id"].Visible = true;
                //dgvData.Columns["DeviceId"].Visible = false;
                //if (tabCtrl.SelectedTab.Name.ToString()!="tbDisplay")
                //dgvData.Columns["Description"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
            catch (Exception ex)
            {
                writeErrorLog(ex, "FillGridview");
            }
        }


        //Exit button
        private void btnExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        //Clear button
        private void btnClear_Click(object sender, EventArgs e)
        {
            btnCreate.Text = "Create";
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

                case "tbDepartment":
                    DepClearAll(true);
                    break;
                case "tbCounter":
                    CounterClearall(true);
                    break;
                case "tbService":
                    ServiceClearall(true);
                    break;
                case "tbBill":
                    BillClearall(true);
                    break;
                case "tbTemplate":
                    TemplateClearall(true);
                    break;
                case "tbDevice":
                    DeviceClearall(true);
                    break;
                case "tbKiosk":
                    KioskClearall(true);
                    break;
                case "tbDisplay":
                    DisplayClearall(true);
                    break;
                case "tbSettings":
                    switch (tabSettings.SelectedTab.Name)
                    {
                        case "tbNextdepartment":
                            SettingsNextDepClearall();
                            break;
                        case "tbNextcounter":
                            SettingsNextCounterClearall();
                            break;
                        case "tbSettingservice":
                            SettingsSrvcClearall();
                            break;
                        default:
                            break;
                    }
                    break;
                case "tbUsersettings":
                    UserClearAll();
                    break;

                default:
                    AddressClearAll();
                    break;
            }
        }

        //Create and update button
        private void btnCreate_Click(object sender, EventArgs e)
        {
            try
            {
                switch (tabCtrl.SelectedTab.Name)
                {
                    case "tbFirm":
                        if (btnCreate.Text == "Backup")
                        {
                            BackupDb();
                        }
                        else
                        {
                            if (ValidateFirm() == true)
                                FirmCreate();
                        }
                        break;

                    case "tbBranch":

                        if (ValidateBranch() == true)
                            BranchCreate();
                        break;
                    case "tbDepartment":

                        if (ValidateDepartment() == true)
                            DepartmentCreate();
                        break;
                    case "tbCounter":
                        if (ValidateCounter() == true)
                            CounterCreate();
                        break;
                    case "tbService":
                        if (ValidateService() == true)
                            ServiceCreate();
                        break;
                    case "tbBill":
                        if (ValidateBillNumber() == true)
                            BillCreate();
                        break;
                    case "tbTemplate":
                        if (ValidateTemplate() == true)
                            TemplateCreate();
                        break;
                    case "tbDevice":
                        if (ValidateDevice() == true)
                            DeviceCreate();
                        break;
                    case "tbKiosk":
                        if (ValidateKiosk() == true)
                            KioskCreate();
                        break;
                    case "tbDisplay":
                        if (ValidateDisplay() == true)
                            DisplayCreate();
                        break;
                    case "tbSettings":
                        switch (tabSettings.SelectedTab.Name)
                        {
                            case "tbNextdepartment":
                                if (ValidateSettingsDepartment())
                                NextDepCreate();
                                break;
                            case "tbNextcounter":
                                if (ValidateSettingsCounters())
                               NextCounterCreate();
                                break;
                            case "tbSettingservice":
                                if (ValidateSettingsService())
                              SettingsServiceCreate();
                                break;
                            default:
                                break;
                        }
                        break;
                    case "tbUsersettings":
                        if (ValidateUserSettings())
                        UserCreate();
                        break;
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
        private void BackupDb()
        {
            try
            {
                string strDbname=System.Configuration.ConfigurationSettings.AppSettings.Get("DbName");
                string strDbpath=System.Configuration.ConfigurationSettings.AppSettings.Get("BackupPath");
                string strSql = "EXEC	[dbo].[usp_backup_database]		@DBNAME = '" + strDbname + "',@PATH ='" + strDbpath + "'";
                int res=mGlobal.LocalDBCon.ExecuteNonQuery(strSql);
                //if (res > 0)
                //{
                    MessageBox.Show("Backup Completed");
                    tabCtrl.Enabled=true;
                    btnClear.Enabled = true;
                    btnCreate.Text = "Create";
                    dgvData.Enabled = true;
                    btnNext.Enabled = true;
                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        //insertion and updation of forms
        #region Insertion and updation 
        private void FirmCreate()
        {
            try
            {
                string strAddid = "";
                string strchkactive = chkFrmactive.Checked ? "Y" : "N";
                string tagValue = (txtFirmCode.Tag != null) ? txtFirmCode.Tag.ToString() : "";
                string strSql = "SELECT * FROM " + mFirmstbl + " WHERE id='" + tagValue + "'";

                if (!CheckAlreadyExist(strSql))
                {
                     strSql = @"INSERT into " + mAddresstbl + " (description,address_1,address_2,place,zipcode,phone_1,phone_2,mobile_1,mobile_2,email_1,email_2,website,registration_number_1,registration_number_2,active) OUTPUT INSERTED.adr_id values ('" + txtFrmname.Text.ToString().Trim() + "','" + txtAddress1.Text.ToString().Trim() + "','" + txtAddress2.Text.ToString().Trim() + "','" + txtPlace.Text.ToString().Trim() + "','" + txtZip.Text.ToString().Trim() + "','" + txtPhone1.Text.ToString().Trim() + "','" + txtPhone2.Text.ToString().Trim() + "','" + txtMob1.Text.ToString().Trim() + "','" + txtMob2.Text.ToString().Trim() + "','" + txtEmail1.Text.ToString().Trim() + "','" + txtEmail2.Text.ToString().Trim() + "','" + txtWebsite.Text.ToString().Trim() + "','" + txtRegno1.Text.ToString().Trim() + "','" + txtRegno2.Text.ToString().Trim() + "','Y')";
                    strAddid = AddressCreate(strSql);
                    if (strAddid != "")
                    {
                       
                            strSql = @"INSERT into " + mFirmstbl + "(code,description,short_name,address_id,remarks,serial_number,active) values('" + txtFirmCode.Text.ToString().Trim() + "','" + txtFrmname.Text.ToString().Trim() + "','" + txtFrmshrtname.Text.ToString().Trim() + "','" + strAddid + "','" + txtRemarks.Text.ToString().Trim() + "','"+txtFrmSlno.Text.ToString()+"','" + strchkactive + "')";
                            int res = mGlobal.LocalDBCon.ExecuteNonQuery(strSql);
                            if (res <= 0)
                            {
                                MessageBox.Show("Error Occured On " + mFirmstbl + "");
                            }
                            else
                            {
                                MessageBox.Show("Submitted");
                                tabCtrl.SelectedIndex++;
                                UpdateNavButtonStatus();
                                cbxBrchFirm.SelectedIndex = cbxBrchFirm.Items.Count - 1;
                                GetFirms();                                
                                FirmClearAll(true);
                                AddressClearAll();
                            }
                        
                    }
                }
                else
                {
                     strSql = @"UPDATE " + mAddresstbl + "  SET description = '" + txtFrmname.Text.ToString().Trim() + "'," +
                                      "address_1 = '" + txtAddress1.Text.ToString().Trim() + "'," +
                                      "address_2 = '" + txtAddress2.Text.ToString().Trim() + "'," +
                                      "place = '" + txtPlace.Text.ToString().Trim() + "'," +
                                      "zipcode = '" + txtZip.Text.ToString().Trim() + "'," +
                                      "phone_1 = '" + txtPhone1.Text.ToString().Trim() + "'," +
                                      "phone_2 = '" + txtPhone2.Text.ToString().Trim() + "'," +
                                      "mobile_1 = '" + txtMob1.Text.ToString().Trim() + "'," +
                                      "mobile_2 = '" + txtMob2.Text.ToString().Trim() + "'," +
                                      "email_1 = '" + txtEmail1.Text.ToString().Trim() + "'," +
                                      "email_2 = '" + txtEmail2.Text.ToString().Trim() + "'," +
                                      "website = '" + txtWebsite.Text.ToString().Trim() + "'," +
                                      "registration_number_1 = '" + txtRegno1.Text.ToString().Trim() + "'," +
                                      "registration_number_2 = '" + txtRegno2.Text.ToString().Trim() + "'," +                                      
                                      "active='" + strchkactive + "'" +
                                      "WHERE adr_id = '" + txtAddress1.Tag.ToString().Trim() + "'";
                    int res = mGlobal.LocalDBCon.ExecuteNonQuery(strSql);
                    if (res > 0)
                    {
                        res = 0;
                        strSql = @"UPDATE " + mFirmstbl + " SET description='" + txtFrmname.Text.ToString().Trim() + "',short_name='" + txtFrmshrtname.Text.ToString().Trim() + "',active='" + strchkactive + "',remarks='" + txtRemarks.Text.ToString() + "',serial_number='"+txtFrmSlno.Text.ToString()+"',code='"+txtFirmCode.Text.ToString()+"' WHERE id='" + txtFirmCode.Tag.ToString().Trim() + "'";
                        res = mGlobal.LocalDBCon.ExecuteNonQuery(strSql);
                        if (res <= 0)
                        {
                            MessageBox.Show("Error Occurred While Updating " + mFirmstbl + "");
                        }
                        else
                        {
                            MessageBox.Show("Updated");


                            FirmClearAll(true);
                            AddressClearAll();
                            FillGridview(mstrFirmqry);
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                writeErrorLog(ex, "FirmCreate");
            }

        }
        private void BranchCreate()
        {
            try
            {
                string strAddid = "";



                string strChkBrchActive = chkBrchActive.Checked ? "Y" : "N";
                bool isExist = false;
                if (txtBrchcode.Tag != null && cbxBrchFirm.SelectedValue.ToString()!="-1" )
                     isExist=CheckAlreadyExist("select * from " + mBranchestbl + " where id='" + txtBrchcode.Tag.ToString().Trim() + "' and firm_id='"+cbxBrchFirm.SelectedValue.ToString()+"'");
                 if (!isExist)
                {
                    string strSql = @"INSERT into " + mAddresstbl + " (description,address_1,address_2,place,zipcode,phone_1,phone_2,mobile_1,mobile_2,email_1,email_2,website,registration_number_1,registration_number_2,remarks,active) OUTPUT INSERTED.adr_id values ('" + txtBrchname.Text.ToString().Trim() + "','" + txtAddress1.Text.ToString().Trim() + "','" + txtAddress2.Text.ToString().Trim() + "','" + txtPlace.Text.ToString().Trim() + "','" + txtZip.Text.ToString().Trim() + "','" + txtPhone1.Text.ToString().Trim() + "','" + txtPhone2.Text.ToString().Trim() + "','" + txtMob1.Text.ToString().Trim() + "','" + txtMob2.Text.ToString().Trim() + "','" + txtEmail1.Text.ToString().Trim() + "','" + txtEmail2.Text.ToString().Trim() + "','" + txtWebsite.Text.ToString().Trim() + "','" + txtRegno1.Text.ToString().Trim() + "','" + txtRegno2.Text.ToString().Trim() + "','" + txtRemarks.Text.ToString().Trim() + "','Y')";
                    strAddid = AddressCreate(strSql);
                    if (strAddid != "")
                    {
                        strSql = @"INSERT into " + mBranchestbl + " (code,description,address_id,firm_id,serial_number,active) values ('" + txtBrchcode.Text.ToString() + "','" + txtBrchname.Text.ToString() + "','" + strAddid + "','" + cbxBrchFirm.SelectedValue.ToString() + "','" +txtBrchSlNo.Text.ToString()+"','"+ strChkBrchActive + "')";
                        int res = mGlobal.LocalDBCon.ExecuteNonQuery(strSql);
                        if (res <= 0)
                        {
                            MessageBox.Show("Error Occurred On " + mBranchestbl + "");
                        }
                        else
                        {
                            MessageBox.Show("Submitted");
                            BrchClearAll(true);
                            AddressClearAll();
                            FillGridview(mstrBranchqry);
                        }
                    }


                }
                else
                {
                    string strSql = @"UPDATE " + mAddresstbl + " SET description = '" + txtBrchname.Text.ToString().Trim() + "'," +
                                     "address_1 = '" + txtAddress1.Text.ToString().Trim() + "'," +
                                     "address_2 = '" + txtAddress2.Text.ToString().Trim() + "'," +
                                     "place = '" + txtPlace.Text.ToString().Trim() + "'," +
                                     "zipcode = '" + txtZip.Text.ToString().Trim() + "'," +
                                     "phone_1 = '" + txtPhone1.Text.ToString().Trim() + "'," +
                                     "phone_2 = '" + txtPhone2.Text.ToString().Trim() + "'," +
                                     "mobile_1 = '" + txtMob1.Text.ToString().Trim() + "'," +
                                     "mobile_2 = '" + txtMob2.Text.ToString().Trim() + "'," +
                                     "email_1 = '" + txtEmail1.Text.ToString().Trim() + "'," +
                                     "email_2 = '" + txtEmail2.Text.ToString().Trim() + "'," +
                                     "website = '" + txtWebsite.Text.ToString().Trim() + "'," +
                                     "registration_number_1 = '" + txtRegno1.Text.ToString().Trim() + "'," +
                                     "registration_number_2 = '" + txtRegno2.Text.ToString().Trim() + "'," +
                                     "remarks='" + txtRemarks.Text.ToString().Trim() + "'," +                                     
                                     "active='" + strChkBrchActive + "'" +
                                     "WHERE adr_id = '" + txtAddress1.Tag.ToString().Trim() + "'";
                    int res = mGlobal.LocalDBCon.ExecuteNonQuery(strSql);
                    if (res > 0)
                    {
                        res = 0;
                        strSql = @"UPDATE " + mBranchestbl + " SET description='" + txtBrchname.Text.ToString() + "', address_id='" + txtAddress1.Tag.ToString().Trim() + "', firm_id='" + cbxBrchFirm.SelectedValue.ToString() + "',active='" + strChkBrchActive + "',serial_number='"+txtBrchSlNo.Text.ToString()+"',code='"+txtBrchcode.Text.ToString()+"' WHERE id='" + txtBrchcode.Tag.ToString().Trim() + "'";
                        res = mGlobal.LocalDBCon.ExecuteNonQuery(strSql);
                        if (res <= 0)
                        {
                            MessageBox.Show("Error Occurred While Updating " + mBranchestbl + "");
                        }
                        else
                        {

                            MessageBox.Show("Updated");
                            BrchClearAll(true);
                            AddressClearAll();
                            FillGridview(mstrBranchqry);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                writeErrorLog(ex, "BranchCreate");
            }
        }
        private void DepartmentCreate()
        {
            try
            {
                string strDepActive = "";
                if (chkdepactive.Checked == true)
                    strDepActive = "Y";
                else
                    strDepActive = "N";
                bool isExist = false;
                if (txtDepcode.Tag != null && cbxDepFirm.SelectedValue.ToString() != "-1" && cbxDepBranch.SelectedValue.ToString()!="-1")
                 isExist= CheckAlreadyExist("select * from token_departments where id='" + txtDepcode.Tag.ToString().Trim() + "' and firm_id='"+cbxDepFirm.SelectedValue.ToString()+"' and branch_id='"+cbxDepBranch.SelectedValue.ToString()+"' and code='"+mCommFunc.ConvertToString(txtDepcode.Text)+"'" );
                if (!isExist)
                {
                    string strSql = @"insert into token_departments (code, description, active, other_details, branch_id, firm_id, serial_number) values ('" + txtDepcode.Text.ToString().Trim() + "','" + txtDepdesc.Text.ToString().Trim() + "','" + strDepActive + "',case when isjson('" + txtDepotherdet.Text.ToString() + "') = 1 then '" + txtDepotherdet.Text.ToString() + "' else NULL end,'" + mCommFunc.ConvertToInt(cbxDepBranch.SelectedValue) + "','" + mCommFunc.ConvertToInt(cbxDepFirm.SelectedValue) + "','" + txtDepslno.Text.ToString() + "')";
                    int res = mGlobal.LocalDBCon.ExecuteNonQuery(strSql);
                    if (res > 0)
                    {
                        MessageBox.Show("Success");
                        DepClearAll(true);
                        FillGridview(mstrDepartmentqry);
                    }
                    else
                        MessageBox.Show("Error");
                }
                else
                {
                    if(btnCreate.Text=="Create")
                    {
                        DialogResult drMbx = MessageBox.Show("Already exist do you want to update?", "Confirmation", MessageBoxButtons.YesNo);
                        if (drMbx == DialogResult.No)
                            return;
                    }

                    string strSql = @"update token_departments set description='" + txtDepdesc.Text.ToString().Trim() + "',active='" + strDepActive + "',other_details=case when isjson('" + txtDepotherdet.Text.ToString() + "') = 1 then '" + txtDepotherdet.Text.ToString() + "' else NULL end,branch_id='" + mCommFunc.ConvertToInt(cbxDepBranch.SelectedValue) + "',firm_id='" + mCommFunc.ConvertToInt(cbxDepFirm.SelectedValue) + "',serial_number='" + txtDepslno.Text.ToString() + "',code='" + txtDepcode.Text.ToString() + "' where id='" + txtDepcode.Tag.ToString() + "'";

                    int res = mGlobal.LocalDBCon.ExecuteNonQuery(strSql);
                    if (res > 0)
                    {
                        MessageBox.Show("Updated");
                        DepClearAll(true);
                        FillGridview(mstrDepartmentqry);
                    }
                    else
                        MessageBox.Show("Error");
                }
            }
            catch (Exception ex)
            { writeErrorLog(ex, "DepartmentCreate"); }
        }
        private void CounterCreate()
        {
            try
            {
                string strchkCounteractive = chkCounterActive.Checked ? "Y" : "N";
                bool isExist=false;
                if (txtCounterCode.Tag != null &&
    cbxCounterFirm.SelectedValue != null && cbxCounterFirm.SelectedValue.ToString() != "-1" &&
    cbxCounterBranch.SelectedValue != null && cbxCounterBranch.SelectedValue.ToString() != "-1" &&
    cbxCounterDep.SelectedValue != null && cbxCounterDep.SelectedValue.ToString() != "-1")
                 isExist= CheckAlreadyExist("select * from token_counters where id='" + txtCounterCode.Tag.ToString() + "' and firm_id='"+cbxCounterFirm.SelectedValue.ToString()+"' and branch_id='"+cbxCounterBranch.SelectedValue.ToString()+"' and department_id='"+cbxCounterDep.SelectedValue.ToString()+"' and code='"+mCommFunc.ConvertToString(txtCounterCode.Text)+"'");
                if ( !isExist )
                {
                    string strSql = @"insert into token_counters (code,display_text,other_details,remarks,serial_number,department_id,branch_id,firm_id,active) values('" + txtCounterCode.Text.ToString() + "','" + txtCounterDistext.Text.ToString() + "','" + txtCounterOtherdet.Text.ToString() + "','" + txtCounterRemarks.Text.ToString() + "','" + txtCounterSlno.Text.ToString() + "','" + cbxCounterDep.SelectedValue.ToString() + "','" + cbxCounterBranch.SelectedValue.ToString() + "','" +  cbxCounterFirm.SelectedValue.ToString() + "','" + strchkCounteractive + "')";
                    int res = mGlobal.LocalDBCon.ExecuteNonQuery(strSql);
                    if (res > 0)
                    {
                        MessageBox.Show("Success");
                        CounterClearall(true);
                        FillGridview(mstrCounterqry);
                    }
                    else
                        MessageBox.Show("Error");
                }
                else
                {
                    if (btnCreate.Text == "Create")
                    {
                        DialogResult drMbx = MessageBox.Show("Already exist do you want to update?", "Confirmation", MessageBoxButtons.YesNo);
                        if (drMbx == DialogResult.No)
                            return;
                    }

                    string strSql = @"update token_counters set display_text='" + txtCounterDistext.Text.ToString() + "',other_details='" + txtCounterOtherdet.Text.ToString() + "',remarks='" + txtCounterRemarks.Text.ToString() + "',serial_number='" + txtCounterSlno.Text.ToString() + "',department_id='" + cbxCounterDep.SelectedValue.ToString() + "',branch_id='" + cbxCounterBranch.SelectedValue.ToString() + "',firm_id='" + cbxCounterFirm.SelectedValue.ToString() + "',active='" + strchkCounteractive + "',code='"+txtCounterCode.Text.ToString()+"' where id='" + txtCounterCode.Tag.ToString() + "'";
                    int res = mGlobal.LocalDBCon.ExecuteNonQuery(strSql);
                    if (res > 0)
                    {
                        MessageBox.Show("Updated");
                        CounterClearall(true);
                        FillGridview(mstrCounterqry);
                    }
                    else
                        MessageBox.Show("Error");
                }
            }
            catch (Exception ex)
            { writeErrorLog(ex, "CounterCreate"); }
        }
        private void ServiceCreate()
        {
            try
            {
                string strSrvcchk = chkSrvcactive.Checked ? "Y" : "N";
                bool isExist=false;
                if (txtSrvccode.Tag != null && txtSrvccode.Tag.ToString() != null &&
     cbxSrvcFirm.SelectedValue != null && cbxSrvcFirm.SelectedValue.ToString() != "-1" &&
     cbxSrvcBranch.SelectedValue != null && cbxSrvcBranch.SelectedValue.ToString() != "-1")
                {
                    isExist = CheckAlreadyExist("select * from token_services where id='" + txtSrvccode.Tag.ToString() +
                                                 "' and firm_id='" + cbxSrvcFirm.SelectedValue.ToString() +
                                                 "' and branch_ids='" + "\"" + cbxSrvcBranch.SelectedValue.ToString() + "\"" + "' and code='"+mCommFunc.ConvertToString(txtSrvccode.Text)+"'");
                }

                if (!isExist )
                {
                   // Assuming cbxSrvcBranch.SelectedValue is not null, you might want to check for null before calling ToString()

string strSql = @"insert into token_services (code,option_text,other_details,action,type,default_department_id,active,firm_id,token_serving_interval,token_start_time,default_counter_id,print_template_id,branch_ids,token_kiosk_settings_id,serial_number)
values ('" + txtSrvccode.Text + "','" + txtSrvcOptiontext.Text + "','" + txtSrvcotherdetails.Text + "','PRINT','" + cbxSrvcType.SelectedItem + "','" + cbxSrvcDefDept.SelectedValue + "','" + strSrvcchk + "','" + cbxSrvcFirm.SelectedValue + "','" + numSrvcServeinterval.Text + "','" + dtSrvcTknstarttime.Text + "','" + cbxSrvcDefCounter.SelectedValue + "','" + cbxSrvcTemplates.SelectedValue + "','" + "\""+cbxSrvcBranch.SelectedValue.ToString()+ "\""+ "','1','" + txtSrvcSlno.Text + "')";


                    int res = mGlobal.LocalDBCon.ExecuteNonQuery(strSql);
                    if (res > 0)
                    {
                        MessageBox.Show("Submitted");
                        ServiceClearall(true);
                        FillGridview(mstrServiceqry);
                    }
                    else
                        MessageBox.Show("Error");

                }
                else
                {
                    if (btnCreate.Text == "Create")
                    {
                        DialogResult drMbx = MessageBox.Show("Already exist do you want to update?", "Confirmation", MessageBoxButtons.YesNo);
                        if (drMbx == DialogResult.No)
                            return;
                    }
                    string strSql = @"update token_services set option_text='" + txtSrvcOptiontext.Text.ToString() + "',other_details='" + txtSrvcotherdetails.Text.ToString() + "',type='" + cbxSrvcType.SelectedItem.ToString() + "',default_department_id='" + cbxSrvcDefDept.SelectedValue.ToString() + "',active='" + strSrvcchk + "',firm_id='" + cbxSrvcFirm.SelectedValue.ToString() + "',token_serving_interval='" + numSrvcServeinterval.Text.ToString() + "',token_start_time='" + dtSrvcTknstarttime.Text.ToString() + "',default_counter_id='" + cbxSrvcDefCounter.SelectedValue.ToString() + "',print_template_id='" + cbxSrvcTemplates.SelectedValue.ToString() + "',branch_ids='"+"\""+cbxSrvcBranch.SelectedValue.ToString()+"\""+"',serial_number='"+txtSrvcSlno.Text.ToString()+"',code='"+txtSrvccode.Text.ToString()+"' where id='" + txtSrvccode.Tag.ToString() + "'";
                    int res = mGlobal.LocalDBCon.ExecuteNonQuery(strSql);
                    if (res > 0)
                    {
                        MessageBox.Show("Updated");
                        ServiceClearall(true);
                        FillGridview(mstrServiceqry);
                    }
                    else
                        MessageBox.Show("Error");
                }
            }
            catch (Exception ex)
            { writeErrorLog(ex, "ServiceCreate"); }
        }
        private void BillCreate()
        {
            try
            {
                string strBillLocked = "";
                if (chkBillLocked.Checked == true)
                    strBillLocked = "Y";
                else
                    strBillLocked = "N";
                bool isExist = false;
                if (txtBillCode.Tag != null && txtBillCode.Tag.ToString() != ""
     && cbxBillFirm.SelectedValue != null && cbxBillFirm.SelectedValue.ToString() != "-1"
     && cbxBillBranch.SelectedValue != null && cbxBillBranch.SelectedValue.ToString() != "-1"
     && cbxBillService.SelectedValue != null && cbxBillService.SelectedValue.ToString() != "-1")
                    isExist = CheckAlreadyExist("select * from token_bill_numbers where id='" + txtBillCode.Tag.ToString().Trim() + "' and firm_id='" + cbxBillFirm.SelectedValue.ToString() + "' and branch_id='" + cbxBillBranch.SelectedValue.ToString() + "' and service_id='" + cbxBillService.SelectedValue.ToString() + "' and code='"+mCommFunc.ConvertToString(txtBillCode.Text)+"'");
                if (!isExist)
                {
                    string strSql = @"insert into token_bill_numbers (code,numbers,prefix,postfix,locked,service_id,firm_id,branch_id) values ('" + txtBillCode.Text.ToString() + "','" + txtBillNumbers.Text.ToString() + "','" + txtBillPrefix.Text.ToString() + "','" + txtBillPostfix.Text.ToString() + "','" + strBillLocked + "','" + cbxBillService.SelectedValue.ToString() + "','" + cbxBillFirm.SelectedValue.ToString() + "','" + cbxBillBranch.SelectedValue.ToString() + "')";
                    int res = mGlobal.LocalDBCon.ExecuteNonQuery(strSql);
                    if (res > 0)
                    {
                        MessageBox.Show("Success");
                        BillClearall(true);
                        FillGridview(mstrBillqry);
                    }
                    else
                        MessageBox.Show("Error");
                }
                else
                {
                    if (btnCreate.Text == "Create")
                    {
                        DialogResult drMbx = MessageBox.Show("Already exist do you want to update?", "Confirmation", MessageBoxButtons.YesNo);
                        if (drMbx == DialogResult.No)
                            return;
                    }
                    string strSql = @"update token_bill_numbers set numbers='" + txtBillNumbers.Text.ToString() + "',prefix='" + txtBillPrefix.Text.ToString() + "',locked='" + strBillLocked + "',service_id='" + cbxBillService.SelectedValue.ToString() + "',firm_id='" + cbxBillFirm.SelectedValue.ToString() + "',branch_id='" + cbxBillBranch.SelectedValue.ToString() + "',postfix='" + txtBillPostfix.Text.ToString() + "',code='" + txtBillCode.Text.ToString() + "' where id='" + txtBillCode.Tag.ToString() + "'";
                    int res = mGlobal.LocalDBCon.ExecuteNonQuery(strSql);
                    if (res > 0)
                    {
                        MessageBox.Show("Updated");
                        BillClearall(true);
                        FillGridview(mstrBillqry);
                    }
                    else
                        MessageBox.Show("Error updating");
                }
            }
            catch (Exception ex)
            { writeErrorLog(ex, "BillCreate"); }
        }
        private void TemplateCreate()
        {
            try
            {
                string strchkActive=chkTempActive.Checked?"Y":"N";
                bool isExist = false;
                if (txtTempCode.Tag !=null&& txtTempCode.Tag.ToString() != "")
                 isExist=CheckAlreadyExist("select * from msgtemplate where mt_templateid='" + txtTempCode.Tag.ToString() + "' and mt_code='"+mCommFunc.ConvertToString(txtTempCode.Text)+"'");
                if (!isExist )
                {
                    string strSql = "INSERT into msgtemplate (mt_code,mt_desc,mt_message,mt_slno,mt_active,mt_other_details) values('" + txtTempCode.Text.ToString() + "','" + txtTempDescription.Text.ToString() + "','" + txtTempMessage.Text.ToString() + "','" + txtTempSlno.Text.ToString() + "','" + strchkActive + "','" + txtTempOtherdet.Text.ToString() + "')";
                    int res = mGlobal.LocalDBCon.ExecuteNonQuery(strSql);
                    if (res > 0)
                    {
                        MessageBox.Show("Success");
                        FillGridview(mstrTemplateqry);
                        TemplateClearall(true);
                    }
                    else
                        MessageBox.Show("error");
                }
                else
                {
                  
                    string strSql = "update msgtemplate set mt_desc='"+txtTempDescription.Text.ToString()+"',mt_message='"+txtTempMessage.Text.ToString()+"',mt_slno='"+txtTempSlno.Text.ToString()+"',mt_active='"+strchkActive+"',mt_other_details='"+txtTempOtherdet.Text.ToString()+"' where mt_templateid='"+txtTempCode.Tag.ToString()+"'";
                    int res = mGlobal.LocalDBCon.ExecuteNonQuery(strSql);
                    if (res > 0)
                    {
                        MessageBox.Show("Updated");
                        FillGridview(mstrTemplateqry);
                        TemplateClearall(true);
                    }
                    else
                        MessageBox.Show("error");
                }
 
            }
            catch (Exception ex)
            { writeErrorLog(ex, "TemplateCreate"); }
        }        
        private void DeviceCreate()
        {
            try
            {
                string strDevicechk = chkDeviceactive.Checked ? "Y" : "N";
                bool isExist = false;
                if (txtDevicecode.Tag !=null && cbxDeviceBranch.SelectedValue.ToString()!="-1")
                isExist=CheckAlreadyExist("select * from token_devices where id='" + txtDevicecode.Tag.ToString() + "' and branch_id='"+cbxDeviceBranch.SelectedValue.ToString()+"' and code='"+mCommFunc.ConvertToString(txtDevicecode.Text)+"'");
                if (!isExist)
                {

                    string strSql = @"insert into token_devices (code,description,welcome_note,caption,type,active,other_details,serial_number,branch_id,print_template_id) output inserted.id values('" + txtDevicecode.Text.ToString() + "','" + txtDevicedescription.Text.ToString() + "','Welcome to','" + txtDevicecaption.Text.ToString() + "','" + cbxDevicetype.SelectedItem.ToString() + "','" + strDevicechk + "','" + txtDeviceotherdetails.Text.ToString() + "','" + txtDeviceSlno.Text.ToString() + "','" + cbxDeviceBranch.SelectedValue.ToString() + "','" + cbxDeviceTemplates.SelectedValue.ToString() + "')";
                   
                    DataTable dtres = mGlobal.LocalDBCon.ExecuteQuery(strSql);
                    string strId = dtres.Rows[0][0].ToString();
                    if (strId!="")
                    {
                        MessageBox.Show("Device Id is '"+strId+"'","Success");
                        DeviceClearall(true);
                        FillGridview(mstrDeviceqry);
                    }
                    else
                        MessageBox.Show("Error");
                }
                else
                {
                    if (btnCreate.Text == "Create")
                    {
                        DialogResult drMbx = MessageBox.Show("Already exist do you want to update?", "Confirmation", MessageBoxButtons.YesNo);
                        if (drMbx == DialogResult.No)
                            return;
                    }
                    string strSql = @"UPDATE token_devices SET 
                  description = '" + txtDevicedescription.Text + @"',
                  caption = '" + txtDevicecaption.Text + @"',
                 type = '" + cbxDevicetype.SelectedItem + @"',
                  active = '" + strDevicechk + @"',
                  other_details = '" + txtDeviceotherdetails.Text + @"',
                  serial_number = '" + txtDeviceSlno.Text + @"',
                  branch_id = '" + cbxDeviceBranch.SelectedValue + @"',
                  print_template_id = '" + cbxDeviceTemplates.SelectedValue + @"', 
                  code='"+txtDevicecode.Text+@"'
                  WHERE id = '" + txtDevicecode.Tag + "'";

                    int res = mGlobal.LocalDBCon.ExecuteNonQuery(strSql);

                    if (res > 0)
                    {
                        MessageBox.Show("Updated");

                        DeviceClearall(true);
                        FillGridview(mstrDeviceqry);
                    }
                    else
                        MessageBox.Show("Error updating");

                }
            }
            catch (Exception ex)
            { writeErrorLog(ex, "DeviceCreate"); }
        }                
        private void KioskCreate()
        {
            try
            {
                string strChkActive = chkKioskActive.Checked ? "Y" : "N";
                bool isExist = false;
                if (txtKioskCode.Tag!=null)
                isExist=CheckAlreadyExist("select * from token_kiosk_settings where id='" + txtKioskCode.Tag.ToString() + "' and code='"+mCommFunc.ConvertToString(txtKioskCode.Text)+"'");
                if (!isExist)
                {
                    string strSql = @"insert into token_kiosk_settings (code,description,maximum_option_per_page,other_details,serial_number,remarks,active,layout_details) output inserted.id values('" + txtKioskCode.Text.ToString() + "','" + txtKioskDescription.Text.ToString() + "','" + txtKioskMaxpage.Text.ToString() + "','" + txtKioskOtherdet.Text.ToString() + "','" + txtKioskSlno.Text.ToString() + "','" + txtKioskRemarks.Text.ToString() + "','" + strChkActive + "','" + txtKioskLayoutdet.Text.ToString() + "')";
                    DataTable dtresid = mGlobal.LocalDBCon.ExecuteQuery(strSql);
                    string id = dtresid.Rows[0]["id"].ToString();
                    strSql = @"update token_devices set token_setting_id='" + id + "' where id='" + cbxKioskDevices.SelectedValue.ToString() + "'";
                    int res2 = mGlobal.LocalDBCon.ExecuteNonQuery(strSql);
                    if (res2 > 0)
                    {
                        MessageBox.Show("Submitted");
                        KioskClearall(true);
                        FillGridview(mstrKioskqry);
                    }
                    else
                        MessageBox.Show("Error");
                }
                else
                {
                    if (btnCreate.Text == "Create")
                    {
                        DialogResult drMbx = MessageBox.Show("Already exist do you want to update?", "Confirmation", MessageBoxButtons.YesNo);
                        if (drMbx == DialogResult.No)
                            return;
                    }
                    string strSql = @"update token_kiosk_settings set description='" + txtKioskDescription.Text.ToString() + "',maximum_option_per_page='" + txtKioskMaxpage.Text.ToString() + "',other_details='" + txtKioskOtherdet.Text.ToString() + "',serial_number='" + txtKioskSlno.Text.ToString() + "',remarks='" + txtKioskRemarks.Text.ToString() + "',active='" + strChkActive + "',layout_details='" + txtKioskLayoutdet.Text.ToString() + "' where code='" + txtKioskCode.Text.ToString() + "'";
                    int res = mGlobal.LocalDBCon.ExecuteNonQuery(strSql);
                    strSql = @"update token_devices set token_setting_id='" + txtKioskCode.Tag.ToString() + "' where id='" + cbxKioskDevices.SelectedValue.ToString() + "'";
                    int res2 = mGlobal.LocalDBCon.ExecuteNonQuery(strSql);
                    if (res > 0 && res2 > 0)
                    {
                        MessageBox.Show("updated");
                        KioskClearall(true);
                        FillGridview(mstrKioskqry);
                    }
                    else
                        MessageBox.Show("Error updating");
                }
            }
            catch (Exception ex)
            { writeErrorLog(ex, "KioskCreate"); }
        }        
        private void DisplayCreate()
        {
            try
            {
                string strChkActive = chkDisplayActive.Checked ? "Y" : "N";
                string selectedValues = GetSelectedValues(chkdlbxDisplayDepartment);
                bool isExist = false;
                if (txtDisplayLogopath.Tag!=null)
                    isExist=CheckAlreadyExist("select * from token_display_settings where id='" + txtDisplayLogopath.Tag.ToString() + "'");
                if (!isExist)
                {
                    string strSql = "insert into token_display_settings (logo_path,welcome_note,maximum_option_per_page,other_details,serial_number,active,token_department_ids,layout_details,remarks) output inserted.id values('" + txtDisplayLogopath.Text.ToString() + "','" + txtDisplayWelcomenote.Text.ToString() + "','" + txtDisplayMaxpages.Text.ToString() + "','" + txtDisplayOtherdetails.Text.ToString() + "','" + txtDisplaySlno.Text.ToString() + "','" + strChkActive + "','" + selectedValues + "','" + txtDisplayLayoutdetails.Text.ToString() + "','" + txtDisplayRemarks.Text.ToString() + "')";
                    DataTable dtresid = mGlobal.LocalDBCon.ExecuteQuery(strSql);
                    string strid = dtresid.Rows[0]["id"].ToString();
                    strSql = "update token_devices set token_setting_id='" + strid + "' where id='" + cbxDisplayDevices.SelectedValue.ToString() + "'";
                    int res2 = mGlobal.LocalDBCon.ExecuteNonQuery(strSql);
                    if (res2 > 0)
                    {
                        MessageBox.Show("Submitted");
                        DisplayClearall(true);
                        FillGridview(mstrDisplayqry);
                    }
                    else
                        MessageBox.Show("Error");
                }
                else
                {
                    if (btnCreate.Text == "Create")
                    {
                        DialogResult drMbx = MessageBox.Show("Already exist do you want to update?", "Confirmation", MessageBoxButtons.YesNo);
                        if (drMbx == DialogResult.No)
                            return;
                    }
                    string strSql = "update token_display_settings set welcome_note='" + txtDisplayWelcomenote.Text.ToString() + "',maximum_option_per_page='" + txtDisplayMaxpages.Text.ToString() + "',other_details='" + txtDisplayOtherdetails.Text.ToString() + "',serial_number='" + txtDisplaySlno.Text.ToString() + "',active='" + strChkActive + "',token_department_ids='" + selectedValues + "',layout_details='" + txtDisplayLayoutdetails.Text.ToString() + "',remarks='" + txtDisplayRemarks.Text.ToString() + "' where id='" + txtDisplayLogopath.Tag.ToString() + "'";
                    int res = mGlobal.LocalDBCon.ExecuteNonQuery(strSql);
                    strSql = "update token_devices set token_setting_id='" + txtDisplayLogopath.Tag.ToString() + "' where id='" + cbxDisplayDevices.SelectedValue.ToString() + "'";
                    int res2 = mGlobal.LocalDBCon.ExecuteNonQuery(strSql);
                    if (res > 0 && res2 > 0)
                    {
                        MessageBox.Show("updated");
                        DisplayClearall(true);
                        FillGridview(mstrDisplayqry);
                    }
                    else
                        MessageBox.Show("Error updating");
                }
            }
            catch (Exception ex)
            { writeErrorLog(ex, "DisplayCreate"); }
        }
        private void NextDepCreate()
        {
            try
            {
              
                    string strSql = "update token_departments set next_department_id='" + cbxSetNxtdepartment.SelectedValue.ToString() + "' where id='" + cbxSetDepartment.SelectedValue.ToString() + "'";
                    int res = mGlobal.LocalDBCon.ExecuteNonQuery(strSql);
                    if (res > 0)
                    {
                        MessageBox.Show("Success");
                        SettingsNextDepClearall();
                    }
                    else
                    {
                        MessageBox.Show("Error Occured");
                    }
 
                
            }
            catch (Exception ex)
            { writeErrorLog(ex, "NextDepCreate"); }
        }
        private void NextCounterCreate()
        {
            try
            {

                string strSql = "update token_counters set next_counter_id='" +cbxSetNxtcounter.SelectedValue.ToString() + "' where id='" +cbxSetCounter.SelectedValue.ToString() + "'";
                int res = mGlobal.LocalDBCon.ExecuteNonQuery(strSql);
                if (res > 0)
                {
                    MessageBox.Show("Success");
                    SettingsNextCounterClearall();
                }
                else
                {
                    MessageBox.Show("Error Occured");
                }


            }
            catch (Exception ex)
            { writeErrorLog(ex, "NextCounterCreate"); }
        }

        private void SettingsServiceCreate()
        {
            try
            {

                string strSql = "update token_services set ending_counter_id='" + cbxSetEndcounter.SelectedValue.ToString() + "',ending_department_id='"+cbxSetEnddepartment.SelectedValue.ToString()+"' where id='" +cbxSetSrvc.SelectedValue.ToString() + "'";
                int res = mGlobal.LocalDBCon.ExecuteNonQuery(strSql);
                if (res > 0)
                {
                    MessageBox.Show("Success");
                    SettingsSrvcClearall();
                }
                else
                {
                    MessageBox.Show("Error Occured");
                }


            }
            catch (Exception ex)
            { writeErrorLog(ex, "SettingsServiceCreate"); }
        }
        //Create address for firm and branch
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
            catch (Exception ex)
            {
                writeErrorLog(ex, "AddressCreate");
                return null;
            }
            return null;
        }
        private void UserCreate()
        {
            try
            {
                bool isExist = false;
                string strSql = "";
                string strSelectedDepartment = GetSelectedValues(chklstbxUserDepartments);
                string strSelectedCounters = GetSelectedValues(chklstbxUserCounters);
                string strOtherDetails = "{\"token\":{\"department_ids\":[" + strSelectedDepartment + "],\"counter_ids\":[" + strSelectedCounters + "]}}";
                string strRoleid = mCommFunc.ConvertToString(cbxUserGroups.SelectedItem) == "GEN" ? "2" : "1";
                if (txtUserName.Tag != null && txtUserName.Text.ToString() != "")
                {
                    strSql = @"Select * from core_users where id='" + mCommFunc.ConvertToString(txtUserName.Tag) + "'";
                    isExist = CheckAlreadyExist(strSql);
                }
                if (!isExist)
                {
                    
                    mGlobal.LocalDBCon.BeginTrans();
                    strSql = @"INSERT INTO core_users (name,email,password,other_details,user_group,branch_id,firm_id,staff_id,admin,active) output inserted.id values('" + mCommFunc.ConvertToString(txtUserName.Text) + "','" + mCommFunc.ConvertToString(txtUserEmail.Text) + "','" + mCommFunc.ConvertToString(txtUserPassword.Text) + "','" + strOtherDetails + "','" + mCommFunc.ConvertToString(cbxUserGroups.SelectedItem) + "','" + mCommFunc.ConvertToString(cbxUserBranches.SelectedValue) + "','" + mCommFunc.ConvertToString(cbxUserFirms.SelectedValue) + "','1','N','1')";
                    DataTable dtResponse = mGlobal.LocalDBCon.ExecuteQuery_OnTran(strSql);
                    if (dtResponse.Rows.Count > 0)
                    {
                        
                        string strSqlUserroles = "insert into core_user_roles (user_id,role_id) values ('" + mCommFunc.ConvertToString(dtResponse.Rows[0]["id"]) + "','" + strRoleid + "')";
                        int res = mGlobal.LocalDBCon.ExecuteNonQuery_OnTran(strSqlUserroles);
                        if (res > 0)
                        {
                            MessageBox.Show("Success");
                            mGlobal.LocalDBCon.CommitTrans();
                            UserClearAll();
                        }
                        else
                        {
                            mGlobal.LocalDBCon.RollbackTrans();
                        }
                    }
                    else
                    {
                        mGlobal.LocalDBCon.RollbackTrans();
                    }
                }
                else
                {
                    //mGlobal.LocalDBCon.RollbackTrans();
                    mGlobal.LocalDBCon.BeginTrans();
                    strSql = @"update core_users set name='" + mCommFunc.ConvertToString(txtUserName.Text) + "',email='" + mCommFunc.ConvertToString(txtUserEmail.Text) + "',password='" + mCommFunc.ConvertToString(txtUserPassword.Text) + "',other_details='" + strOtherDetails + "',user_group='" + mCommFunc.ConvertToString(cbxUserGroups.SelectedItem) + "',branch_id='" + mCommFunc.ConvertToString(cbxUserBranches.SelectedValue) + "',firm_id='"+mCommFunc.ConvertToString(cbxUserFirms.SelectedValue)+"' where id='"+mCommFunc.ConvertToString(txtUserName.Tag)+"'";
                    int res = mGlobal.LocalDBCon.ExecuteNonQuery_OnTran(strSql);
                    if (res > 0)
                    {
                        strSql = @"update core_user_roles set role_id='"+strRoleid+"' where user_id='"+mCommFunc.ConvertToString(txtUserName.Tag)+"'";
                        int res1=mGlobal.LocalDBCon.ExecuteNonQuery_OnTran(strSql);
                        if(res1>0)
                        {
                        MessageBox.Show("Updated");
                            mGlobal.LocalDBCon.CommitTrans();
                        }
                        else
                        {
                            mGlobal.LocalDBCon.RollbackTrans();
                        }
                        UserClearAll();
                    }
                    else
                    {
                        mGlobal.LocalDBCon.RollbackTrans();
                    }
                }
            }
            catch (Exception ex)
            {
                writeErrorLog(ex, "UserCreate");
            mGlobal.LocalDBCon.RollbackTrans();
        }
        }
        #endregion

        //Feeding comboboxes and checked list box with corresponding data
        #region Feed comboboxes
        private void GetFirms()
        {
            try
            {
                mstrSql = @"select id, description,code from " + mFirmstbl + "";
                DataTable dtFirms = mGlobal.LocalDBCon.ExecuteQuery(mstrSql);

                if (dtFirms.Rows.Count > 0)
                {
                    dtFirms.Rows.Add(-1, "--select--");
                    cbxBrchFirm.DataSource = dtFirms;
                    cbxBrchFirm.DisplayMember = "description";
                    cbxBrchFirm.ValueMember = "id";
                    cbxBrchFirm.SelectedValue = "-1";

                    cbxDepFirm.DataSource = dtFirms;
                    cbxDepFirm.DisplayMember = "description";
                    cbxDepFirm.ValueMember = "id";
                    cbxDepFirm.SelectedValue = "-1";

                    cbxSrvcFirm.DataSource = dtFirms;
                    cbxSrvcFirm.DisplayMember = "description";
                    cbxSrvcFirm.ValueMember = "id";
                    cbxSrvcFirm.SelectedValue = "-1";

                    cbxCounterFirm.DataSource = dtFirms;
                    cbxCounterFirm.DisplayMember = "description";
                    cbxCounterFirm.ValueMember = "id";
                    cbxCounterFirm.SelectedValue = "-1";

                    cbxBillFirm.DataSource = dtFirms;
                    cbxBillFirm.DisplayMember = "description";
                    cbxBillFirm.ValueMember = "id";
                    cbxBillFirm.SelectedValue = "-1";


                    cbxUserFirms.DataSource = dtFirms;
                    cbxUserFirms.DisplayMember = "description";
                    cbxUserFirms.ValueMember = "id";
                    cbxUserFirms.SelectedValue = "-1";

                   
                }

            }
            catch (Exception ex)
            {
                writeErrorLog(ex, "FillAddress");
            }
        }
        private void GetMFirms()
        {
            try
            {
                HideMBranch();
                HideMFirm();
                GetMBranch();
                mstrSql = @"select id, description,code from " + mFirmstbl + "";
                DataTable dtFirms = mGlobal.LocalDBCon.ExecuteQuery(mstrSql);

                if (dtFirms.Rows.Count > 0)
                {
                    dtFirms.Rows.Add(-1, "Show all");
                    cbxMFirms.DataSource = dtFirms;
                    cbxMFirms.DisplayMember = "description";
                    cbxMFirms.ValueMember = "id";
                    cbxMFirms.SelectedValue = "-1";

                }

            }
            catch (Exception ex)
            {
                writeErrorLog(ex, "FillAddress");
            }
        }
        private void GetSettingsDepFirms()
        {
            try
            {
                mstrSql = @"select id, description,code from " + mFirmstbl + "";
                DataTable dtFirms = mGlobal.LocalDBCon.ExecuteQuery(mstrSql);

                if (dtFirms.Rows.Count > 0)
                {
                    dtFirms.Rows.Add(-1, "--select--");
                    cbxSetDepFirm.DataSource = dtFirms;
                    cbxSetDepFirm.ValueMember = "id";
                    cbxSetDepFirm.DisplayMember = "description";
                    cbxSetDepFirm.SelectedValue = "-1";
                }

            }
            catch (Exception ex)
            {
                writeErrorLog(ex, "GetSettingsDepFirms");
            }
        }
        private void GetSettingsCounterFirms()
        {
            try
            {
                mstrSql = @"select id, description,code from " + mFirmstbl + "";
                DataTable dtFirms = mGlobal.LocalDBCon.ExecuteQuery(mstrSql);

                if (dtFirms.Rows.Count > 0)
                {
                    dtFirms.Rows.Add(-1, "--select--");
                    cbxSetCounterFirm.DataSource = dtFirms;
                    cbxSetCounterFirm.ValueMember = "id";
                    cbxSetCounterFirm.DisplayMember = "description";
                    cbxSetCounterFirm.SelectedValue = "-1";
                }

            }
            catch (Exception ex)
            {
                writeErrorLog(ex, "GetSettingsCounterFirms");
            }
        }
        private void GetSettingsSrvcFirms()
        {
            try
            {
                mstrSql = @"select id, description,code from " + mFirmstbl + "";
                DataTable dtFirms = mGlobal.LocalDBCon.ExecuteQuery(mstrSql);

                if (dtFirms.Rows.Count > 0)
                {
                    dtFirms.Rows.Add(-1, "--select--");
                    cbxSetSrvcFirm.DataSource = dtFirms;
                    cbxSetSrvcFirm.ValueMember = "id";
                    cbxSetSrvcFirm.DisplayMember = "description";
                    cbxSetSrvcFirm.SelectedValue = "-1";
                }

            }
            catch (Exception ex)
            {
                writeErrorLog(ex, "GetSettingsCounterFirms");
            }
        }
        private void GetBranch()
        {
            try
            {
                mstrSql = @"select id,description from token_branches";
                DataTable dtBranch = mGlobal.LocalDBCon.ExecuteQuery(mstrSql);
                if (dtBranch.Rows.Count > 0)
                {
                    dtBranch.Rows.Add(-1, "--select--");
                    cbxDepBranch.DataSource = dtBranch;
                    cbxCounterBranch.DataSource = dtBranch;
                    cbxSrvcBranch.DataSource = dtBranch;
                    cbxBillBranch.DataSource = dtBranch;
                    cbxDeviceBranch.DataSource = dtBranch;

                    cbxDepBranch.DisplayMember = "description";
                    cbxCounterBranch.DisplayMember = "description";
                    cbxSrvcBranch.DisplayMember = "description";
                    cbxBillBranch.DisplayMember = "description";
                    cbxDeviceBranch.DisplayMember = "description";

                    cbxDepBranch.ValueMember = "id";
                    cbxCounterBranch.ValueMember = "id";
                    cbxSrvcBranch.ValueMember = "id";
                    cbxBillBranch.ValueMember = "id";
                    cbxDeviceBranch.ValueMember = "id";

                    cbxDepBranch.SelectedValue = "-1";
                    cbxCounterBranch.SelectedValue = "-1";
                    cbxSrvcBranch.SelectedValue = "-1";
                    cbxBillBranch.SelectedValue = "-1";
                    cbxDeviceBranch.SelectedValue = "-1";

                    cbxUserBranches.DataSource = dtBranch;
                    cbxUserBranches.DisplayMember = "description";
                    cbxUserBranches.ValueMember = "id";
                    cbxUserBranches.SelectedValue = "-1";

                    GetMBranch();

                }
            }
            catch (Exception ex) { writeErrorLog(ex, "GetBranch"); }
        }
     
        private void GetMBranch()
        {
            try
            {
                mstrSql = @"select id,description from token_branches";
                DataTable dtBranch = mGlobal.LocalDBCon.ExecuteQuery(mstrSql);
                if (dtBranch.Rows.Count > 0)
                {
                    dtBranch.Rows.Add(-1, "Show all");
                    cbxMBranches.DataSource = dtBranch;
                    cbxMBranches.DisplayMember = "description";
                    cbxMBranches.ValueMember = "id";
                    cbxMBranches.SelectedValue = "-1";

                }
            }
            catch (Exception ex) { writeErrorLog(ex, "GetBranch"); }
        }
      
       
        private void GetDepartment()
        {
            try
            {
                mstrSql = @"select id,description from token_departments";
                DataTable dtDepartment = mGlobal.LocalDBCon.ExecuteQuery(mstrSql);
                if (dtDepartment.Rows.Count > 0)
                {
                    dtDepartment.Rows.Add(-1, "--select--");
                    cbxCounterDep.DataSource = dtDepartment;                  
                    cbxCounterDep.DisplayMember = "description";    
                    
                    cbxCounterDep.ValueMember = "id";                    
                    
                    cbxCounterDep.SelectedValue = "-1";

                    cbxSrvcDefDept.DataSource = dtDepartment;
                    cbxSrvcDefDept.DisplayMember = "description";
                    cbxSrvcDefDept.ValueMember = "id";

                    cbxSetDepartment.DataSource = dtDepartment;
                    cbxSetDepartment.DisplayMember = "description";
                    cbxSetDepartment.ValueMember = "id";
                    cbxSetDepartment.SelectedValue = "-1";

                     cbxSetNxtdepartment.DataSource = dtDepartment;
                    cbxSetNxtdepartment.DisplayMember = "description";
                    cbxSetNxtdepartment.ValueMember = "id";
                    cbxSetNxtdepartment.SelectedValue = "-1";


                    chklstbxUserDepartments.DataSource = dtDepartment;
                    chklstbxUserDepartments.DisplayMember = "description";
                    chklstbxUserDepartments.ValueMember = "id";



                  


                }
            }
            catch (Exception ex) { writeErrorLog(ex, "GetDepartment"); }
        }
        private void GetcbxSetDepartment()
        {
            try
            {
                if (cbxSetDepBranch.SelectedValue.ToString() == "-1")
                    mstrSql = @"select id,description from token_departments";
                else
                    mstrSql = @"select id,description from token_departments where branch_id='"+cbxSetDepBranch.SelectedValue.ToString()+"'";
                DataTable dtDepartment = mGlobal.LocalDBCon.ExecuteQuery(mstrSql);
                if (dtDepartment.Rows.Count > 0)
                {
                    dtDepartment.Rows.Add(-1, "--select--");
                   

                    cbxSetDepartment.DataSource = dtDepartment;
                    cbxSetDepartment.DisplayMember = "description";
                    cbxSetDepartment.ValueMember = "id";
                    cbxSetDepartment.SelectedValue = "-1";                   


                }
            }
            catch (Exception ex) { writeErrorLog(ex, "GetcbxSetDepartment"); }
        }
        private void GetcbxSetNxtdepartment()
        {
            try
            {
                if (cbxSetDepBranch.SelectedValue.ToString() == "-1")
                    mstrSql = @"select id,description from token_departments";
                else
                    mstrSql = @"select id,description from token_departments where branch_id='" + cbxSetDepBranch.SelectedValue.ToString() + "'";
                DataTable dtDepartment = mGlobal.LocalDBCon.ExecuteQuery(mstrSql);
                if (dtDepartment.Rows.Count > 0)
                {
                    dtDepartment.Rows.Add(-1, "--select--");


                    cbxSetNxtdepartment.DataSource = dtDepartment;
                    cbxSetNxtdepartment.DisplayMember = "description";
                    cbxSetNxtdepartment.ValueMember = "id";
                    cbxSetNxtdepartment.SelectedValue = "-1";

                    

                }
            }
            catch (Exception ex) { writeErrorLog(ex, "GetcbxSetNxtdepartment"); }
        }
        private void GetcbxSetEnddepartment()
        {
            try
            {
                mstrSql = @"select id,description from token_departments";
                DataTable dtDepartment = mGlobal.LocalDBCon.ExecuteQuery(mstrSql);
                if (dtDepartment.Rows.Count > 0)
                {
                    dtDepartment.Rows.Add(-1, "--select--");


                    cbxSetEnddepartment.DataSource = dtDepartment;
                    cbxSetEnddepartment.DisplayMember = "description";
                    cbxSetEnddepartment.ValueMember = "id";
                    cbxSetEnddepartment.SelectedValue = "-1";



                }
            }
            catch (Exception ex) { writeErrorLog(ex, "GetcbxSetEnddepartment"); }
        }
        private void GetEndDepartment()
        {
            try
            {
                mstrSql = @"select id,description from token_departments";
                DataTable dtDepartment = mGlobal.LocalDBCon.ExecuteQuery(mstrSql);
                if (dtDepartment.Rows.Count > 0)
                {
                    dtDepartment.Rows.Add(-1, "--select--");              



                }
            }
            catch (Exception ex) { writeErrorLog(ex, "GetDepartment"); }
        }
        private void GetDisplayDepartment()
        {
            try
            {
                mstrSql = @"select dep.id as id,dep.description from token_departments dep left join token_devices td on dep.branch_id=td.branch_id where td.id='" + cbxDisplayDevices.SelectedValue.ToString() + "'";
                DataTable dtDepartment = mGlobal.LocalDBCon.ExecuteQuery(mstrSql);
                if (dtDepartment.Rows.Count > 0)
                {

                    chkdlbxDisplayDepartment.DataSource = dtDepartment;
                    chkdlbxDisplayDepartment.DisplayMember = "description";
                    chkdlbxDisplayDepartment.ValueMember = "id";


                }
                else
                {
                    dtDepartment.Clear();

                    // Reset the DataSource and rebind the CheckedListBox
                    chkdlbxDisplayDepartment.DataSource = null;
                    chkdlbxDisplayDepartment.Items.Clear(); 
                    

                }
            }
            catch (Exception ex) { writeErrorLog(ex, "GetDepartment"); }
        }
        private void GetDevice()
        {
            try
            {
                string strSql = "select id,description from token_devices where type='KIOSK'";
                DataTable dtKioskDevices = mGlobal.LocalDBCon.ExecuteQuery(strSql);
                if (dtKioskDevices.Rows.Count > 0)
                {
                    dtKioskDevices.Rows.Add(-1, "--select--");
                    cbxKioskDevices.DataSource = dtKioskDevices;
                    cbxKioskDevices.DisplayMember = "description";
                    cbxKioskDevices.ValueMember = "id";
                    cbxKioskDevices.SelectedValue = "-1";
                }
                strSql = "select id,description from token_devices where type='DISPLAY'";
                DataTable dtDisplayDevices = mGlobal.LocalDBCon.ExecuteQuery(strSql);
                if (dtDisplayDevices.Rows.Count > 0)
                {
                    dtDisplayDevices.Rows.Add(-1, "--select--");
                    cbxDisplayDevices.DataSource = dtDisplayDevices;
                    cbxDisplayDevices.DisplayMember = "description";
                    cbxDisplayDevices.ValueMember = "id";
                    cbxDisplayDevices.SelectedValue = "-1";
                }
            }
            catch (Exception ex)
            { writeErrorLog(ex, "GetDevice"); }
        }
        private void GetCounter()
        {
            try
            {
                mstrSql = @"select id,display_text from token_counters";
                DataTable dtCounter = mGlobal.LocalDBCon.ExecuteQuery(mstrSql);
                if (dtCounter.Rows.Count > 0)
                {
                    dtCounter.Rows.Add(-1, "--select--");
                    cbxSrvcDefCounter.DataSource = dtCounter;
                    cbxSrvcDefCounter.DisplayMember = "display_text";
                    cbxSrvcDefCounter.ValueMember = "id";
                    cbxSrvcDefCounter.SelectedValue = "-1";

                    chklstbxUserCounters.DataSource = dtCounter;
                    chklstbxUserCounters.DisplayMember = "display_text";
                    chklstbxUserCounters.ValueMember = "id";

                }
            }
            catch (Exception ex) { writeErrorLog(ex, "GetCounter"); }
        }
        private void GetcbxSetCounter()
        {
            try
            {
                mstrSql = @"select id,display_text from token_counters";
                DataTable dtCounter = mGlobal.LocalDBCon.ExecuteQuery(mstrSql);
                if (dtCounter.Rows.Count > 0)
                {
                    dtCounter.Rows.Add(-1, "--select--");
                    cbxSetCounter.DataSource = dtCounter;
                    cbxSetCounter.DisplayMember = "display_text";
                    cbxSetCounter.ValueMember = "id";
                    cbxSetCounter.SelectedValue = "-1";


                }
            }
            catch (Exception ex) { writeErrorLog(ex, "GetcbxSetCounter"); }
        }
        private void GetcbxSetEndcounter()
        {
            try
            {
                mstrSql = @"select id,display_text from token_counters";
                DataTable dtCounter = mGlobal.LocalDBCon.ExecuteQuery(mstrSql);
                if (dtCounter.Rows.Count > 0)
                {
                    dtCounter.Rows.Add(-1, "--select--");
                    cbxSetEndcounter.DataSource = dtCounter;
                    cbxSetEndcounter.DisplayMember = "display_text";
                    cbxSetEndcounter.ValueMember = "id";
                    cbxSetEndcounter.SelectedValue = "-1";


                }
            }
            catch (Exception ex) { writeErrorLog(ex, "GetcbxSetEndcounter"); }
        }
        private void GetcbxSetNxtcounter()
        {
            try
            {
                mstrSql = @"select id,display_text from token_counters";
                DataTable dtCounter = mGlobal.LocalDBCon.ExecuteQuery(mstrSql);
                if (dtCounter.Rows.Count > 0)
                {
                    dtCounter.Rows.Add(-1, "--select--");
                    cbxSetNxtcounter.DataSource = dtCounter;
                    cbxSetNxtcounter.DisplayMember = "display_text";
                    cbxSetNxtcounter.ValueMember = "id";
                    cbxSetNxtcounter.SelectedValue = "-1";


                }
            }
            catch (Exception ex) { writeErrorLog(ex, "GetcbxSetNxtcounter"); }
        }
        private void GetService()
        {
            try
            {
                mstrSql = @"select id,code+'_'+option_text as code  from token_services";
                DataTable dtService = mGlobal.LocalDBCon.ExecuteQuery(mstrSql);
                if (dtService.Rows.Count > 0)
                {
                    dtService.Rows.Add(-1, "--select--");
                    cbxBillService.DataSource = dtService;
                    cbxBillService.DisplayMember = "code";
                    cbxBillService.ValueMember = "id";
                    cbxBillService.SelectedValue = "-1";


                    cbxSetSrvc.DataSource = dtService;
                    cbxSetSrvc.DisplayMember = "code";
                    cbxSetSrvc.ValueMember = "id";
                    cbxSetSrvc.SelectedValue = "-1";

                }
            }
            catch (Exception ex) { writeErrorLog(ex, "GetService"); }
        }
        private void GetTemplates()
        {
            try
            {
                mstrSql = "select mt_templateid,mt_desc from msgtemplate";
                DataTable dtTemplates = new DataTable();

                dtTemplates = mGlobal.LocalDBCon.ExecuteQuery(mstrSql);
                if (dtTemplates.Rows.Count > 0)
                {
                    dtTemplates.Rows.Add("-1", "--select--");
                    cbxSrvcTemplates.DataSource = dtTemplates;
                    cbxSrvcTemplates.DisplayMember = "mt_desc";
                    cbxSrvcTemplates.ValueMember = "mt_templateid";
                    cbxSrvcTemplates.SelectedValue = "-1";

                    cbxDeviceTemplates.DataSource = dtTemplates;
                    cbxDeviceTemplates.DisplayMember = "mt_desc";
                    cbxDeviceTemplates.ValueMember = "mt_templateid";
                    cbxDeviceTemplates.SelectedValue = "-1";
                }
            }
            catch (Exception ex)
            { writeErrorLog(ex, "GetTemplates"); }
        }
        #endregion

        //Filling validated fields
        #region Fill Fields
        private void FillFirm()
        {
            try
            {
                mstrSql = @"select * from " + mFirmstbl + " left join " + mAddresstbl + " on address_id=adr_id where id='" + txtFirmCode.Tag.ToString().Trim() + "'";
                DataTable dtFirmData = mGlobal.LocalDBCon.ExecuteQuery(mstrSql);
                if (dtFirmData.Rows.Count > 0)
                {
                    btnCreate.Text = "Update";
                    txtFrmname.Text = dtFirmData.Rows[0]["description"].ToString();
                    txtFrmshrtname.Text = dtFirmData.Rows[0]["short_name"].ToString();
                    txtRemarks.Text = dtFirmData.Rows[0]["remarks"].ToString();
                    txtFrmSlno.Text = dtFirmData.Rows[0]["serial_number"].ToString();
                    if (dtFirmData.Rows[0]["active"].ToString() == "Y")
                        chkFrmactive.Checked = true;
                    else
                        chkFrmactive.Checked = false;
                    FillAddress(dtFirmData);
                    txtFirmCode.Enabled = false;

                }
                else
                {
                    AddressClearAll();
                    FirmClearAll(false);
                    txtFirmCode.Enabled = true;
                }
            }
            catch (Exception ex)
            { writeErrorLog(ex, "FillFirm"); }
        }        
        private void FillBranch()
        {
            try
            {
                mstrSql = @"select * from " + mBranchestbl + " left join " + mAddresstbl + " on address_id=adr_id where id='" + txtBrchcode.Tag.ToString().Trim() + "'";
                DataTable dtBranchdata = mGlobal.LocalDBCon.ExecuteQuery(mstrSql);
                if (dtBranchdata.Rows.Count > 0)
                {
                    btnCreate.Text = "Update";
                    FillAddress(dtBranchdata);
                    txtRemarks.Text = dtBranchdata.Rows[0]["remarks"].ToString();
                    txtBrchname.Text = dtBranchdata.Rows[0]["description"].ToString();
                    txtBrchSlNo.Text = dtBranchdata.Rows[0]["serial_number"].ToString();
                    chkBrchActive.Checked = dtBranchdata.Rows[0]["active"].ToString() == "Y" ? true : false;
                    txtAddress1.Tag = dtBranchdata.Rows[0]["address_id"].ToString();                    
                    cbxBrchFirm.SelectedValue = dtBranchdata.Rows[0]["firm_id"].ToString();
                    txtBrchcode.Enabled = false;

                }
                else
                {
                    BrchClearAll(false);
                    AddressClearAll();
                    txtBrchcode.Enabled = true;
                }
            }
            catch (Exception ex)
            { writeErrorLog(ex, "txtBrchcode_Validating"); }
        }
        private void FillDepartment()
        {
            try
            {
                mstrSql = @"select * from " + mDeptbl + " where id='" + txtDepcode.Tag.ToString().Trim() + "'";
                DataTable dtDepartment = mGlobal.LocalDBCon.ExecuteQuery(mstrSql);
                if (dtDepartment.Rows.Count > 0)
                {
                    btnCreate.Text = "Update";

                    txtDepdesc.Text = dtDepartment.Rows[0]["description"].ToString();
                    txtDepotherdet.Text = dtDepartment.Rows[0]["other_details"].ToString();
                    txtDepslno.Text = dtDepartment.Rows[0]["serial_number"].ToString();
                    cbxDepBranch.SelectedValue = dtDepartment.Rows[0]["branch_id"].ToString();
                    cbxDepFirm.SelectedValue = dtDepartment.Rows[0]["firm_id"].ToString();
                    if (dtDepartment.Rows[0]["active"].ToString() == "Y")
                        chkdepactive.Checked = true;
                    else
                        chkdepactive.Checked = false;
                    txtDepcode.Enabled = false;
                }
                else
                {
                    txtDepcode.Enabled = true;
                    DepClearAll(false);
                }
            }
            catch (Exception ex)
            { writeErrorLog(ex, "FillDepartment"); }
        }
        private void FillCounter()
        {
            try
            {
                mstrSql = @"select * from " + mCountertbl + " where id='" + txtCounterCode.Tag.ToString().Trim() + "'";
                DataTable dtCounter = mGlobal.LocalDBCon.ExecuteQuery(mstrSql);
                if (dtCounter.Rows.Count > 0)
                {
                    txtCounterDistext.Text = dtCounter.Rows[0]["display_text"].ToString();
                    txtCounterSlno.Text = dtCounter.Rows[0]["serial_number"].ToString();
                    cbxCounterBranch.SelectedValue = dtCounter.Rows[0]["branch_id"].ToString();
                    cbxCounterDep.SelectedValue = dtCounter.Rows[0]["department_id"].ToString();
                    cbxCounterFirm.SelectedValue = dtCounter.Rows[0]["firm_id"].ToString();
                    txtCounterOtherdet.Text = dtCounter.Rows[0]["other_details"].ToString();
                    txtCounterRemarks.Text = dtCounter.Rows[0]["remarks"].ToString();
                    if (dtCounter.Rows[0]["active"].ToString() == "Y")
                        chkCounterActive.Checked = true;
                    else
                        chkCounterActive.Checked = false;
                    btnCreate.Text = "Update";
                    txtCounterCode.Enabled = false;
                }
                else
                {
                    txtCounterCode.Enabled = true;
                    CounterClearall(false);
                }
            }
            catch (Exception ex)
            { writeErrorLog(ex, "FillCounter"); }

        }
        private void FillServices()
        {
            try
            {
                mstrSql = @"select * from token_services where id='" + txtSrvccode.Tag.ToString() + "'";
                DataTable dtServices = mGlobal.LocalDBCon.ExecuteQuery(mstrSql);                
                if (dtServices.Rows.Count > 0)
                {
                    cbxSrvcDefDept.SelectedValue = dtServices.Rows[0]["default_department_id"].ToString();
                    cbxSrvcDefCounter.SelectedValue = dtServices.Rows[0]["default_counter_id"].ToString();
                   
                    btnCreate.Text = "Update";
                    txtSrvcOptiontext.Text = dtServices.Rows[0]["option_text"].ToString();
                   
                    
                    cbxSrvcFirm.SelectedValue = dtServices.Rows[0]["firm_id"].ToString();
                    chkSrvcactive.Checked = (dtServices.Rows[0]["active"].ToString() == "Y");
                    numSrvcServeinterval.Text = dtServices.Rows[0]["token_serving_interval"].ToString();
                    dtSrvcTknstarttime.Text = dtServices.Rows[0]["token_start_time"].ToString();
                    cbxSrvcType.SelectedItem = dtServices.Rows[0]["type"].ToString();
                    cbxSrvcTemplates.SelectedValue = dtServices.Rows[0]["print_template_id"].ToString();                   
                    cbxSrvcBranch.SelectedValue = dtServices.Rows[0]["branch_ids"].ToString().Trim('"');
                    txtSrvcotherdetails.Text = dtServices.Rows[0]["other_details"].ToString();
                    txtSrvcSlno.Text = dtServices.Rows[0]["serial_number"].ToString();
                    txtSrvccode.Enabled = false;
                }
                else
                {
                    txtSrvccode.Enabled = true;
                    ServiceClearall(false);
                }
            }
            catch (Exception ex)
            { writeErrorLog(ex, "FillServices"); }
        }
        private void FillBill()
        {
            try
            {
                mstrSql = @"select * from token_bill_numbers where id='" + txtBillCode.Tag.ToString() + "'";
                DataTable dtBill = mGlobal.LocalDBCon.ExecuteQuery(mstrSql);
                if (dtBill.Rows.Count > 0)
                {
                    btnCreate.Text = "Update";
                    txtBillNumbers.Text = dtBill.Rows[0]["numbers"].ToString();
                    txtBillPrefix.Text = dtBill.Rows[0]["prefix"].ToString();
                    cbxBillFirm.SelectedValue = dtBill.Rows[0]["firm_id"].ToString();
                    cbxBillBranch.SelectedValue = dtBill.Rows[0]["branch_id"].ToString();
                    cbxBillService.SelectedValue = dtBill.Rows[0]["service_id"].ToString();
                    txtBillPostfix.Text = dtBill.Rows[0]["postfix"].ToString();
                    if (dtBill.Rows[0]["locked"].ToString() == "Y")
                        chkBillLocked.Checked = true;
                    else
                        chkBillLocked.Checked = false;
                    txtBillCode.Enabled = false;

                }
                else
                {

                    txtBillCode.Enabled = true;
                    BillClearall(false);

                }
            }
            catch (Exception ex)
            {
                writeErrorLog(ex, "FillBill");
            }
        }
        private void FillTemplate()
        {
            try
            {
                string strSql = "select * from msgtemplate where mt_code='"+txtTempCode.Text.ToString()+"'";
                DataTable dtTemplate = mGlobal.LocalDBCon.ExecuteQuery(strSql);
                if (dtTemplate.Rows.Count > 0)
                {
                    txtTempDescription.Text = dtTemplate.Rows[0]["mt_desc"].ToString();
                    txtTempMessage.Text = dtTemplate.Rows[0]["mt_message"].ToString();
                    txtTempSlno.Text = dtTemplate.Rows[0]["mt_slno"].ToString();
                    txtTempOtherdet.Text = dtTemplate.Rows[0]["mt_other_details"].ToString();
                    chkTempActive.Checked= dtTemplate.Rows[0]["mt_active"].ToString() == "Y" ? true : false;
                    txtTempCode.Enabled = false;
                    btnCreate.Text = "Update";
                }
                else
                {
                    txtTempCode.Enabled = true;
                    btnCreate.Text = "Create";
                }
            }
            catch (Exception ex)
            {
                writeErrorLog(ex, "FillTemplate");
            }
        }
        private void FillDevices()
        {
            try
            {

                mstrSql = @"select * from token_devices where id='" + txtDevicecode.Tag.ToString() + "'";
                DataTable dtDevices = mGlobal.LocalDBCon.ExecuteQuery(mstrSql);
                if (dtDevices.Rows.Count > 0)
                {
                    btnCreate.Text = "Update";
                    txtDeviceid.Text = dtDevices.Rows[0]["id"].ToString();
                    txtDevicedescription.Text = dtDevices.Rows[0]["description"].ToString();
                    txtDeviceotherdetails.Text = dtDevices.Rows[0]["other_details"].ToString();
                    txtDevicecaption.Text = dtDevices.Rows[0]["caption"].ToString();
                    txtDeviceSlno.Text = dtDevices.Rows[0]["serial_number"].ToString();
                    cbxDevicetype.SelectedItem = dtDevices.Rows[0]["type"].ToString();
                    cbxDeviceTemplates.SelectedValue = dtDevices.Rows[0]["print_template_id"].ToString();
                    cbxDeviceBranch.SelectedValue = dtDevices.Rows[0]["branch_id"].ToString();
                    if (dtDevices.Rows[0]["active"].ToString() == "Y")
                        chkDeviceactive.Checked = true;
                    else
                        chkDeviceactive.Checked = false;
                    txtDevicecode.Enabled = false;
                }
                else
                {
                    txtDevicecode.Enabled = true;
                    DeviceClearall(false);

                }
            }
            catch (Exception ex)
            { writeErrorLog(ex, "FillDevices"); }
        }
        private void FillKiosk()
        {
            try
            {
                string strSql = @"select * from token_kiosk_settings join token_devices ON token_kiosk_settings.id = token_devices.token_setting_id where type='KIOSK' and token_devices.id='" + cbxKioskDevices.SelectedValue.ToString() + "'";
                DataTable dtKiosk = mGlobal.LocalDBCon.ExecuteQuery(strSql);
                if (dtKiosk.Rows.Count > 0)
                {
                    txtKioskDescription.Text = dtKiosk.Rows[0]["description"].ToString();
                    txtKioskMaxpage.Text = dtKiosk.Rows[0]["maximum_option_per_page"].ToString();
                    txtKioskOtherdet.Text = dtKiosk.Rows[0]["other_details"].ToString();
                    txtKioskSlno.Text = dtKiosk.Rows[0]["serial_number"].ToString();
                    txtKioskRemarks.Text = dtKiosk.Rows[0]["remarks"].ToString();
                    txtKioskLayoutdet.Text = dtKiosk.Rows[0]["layout_details"].ToString();
                    txtKioskCode.Text = dtKiosk.Rows[0]["code"].ToString();
                    txtKioskCode.Tag = dtKiosk.Rows[0]["id"].ToString();
                    chkKioskActive.Checked = dtKiosk.Rows[0]["active"].ToString() == "Y" ? true : false;
                    btnCreate.Text = "Update";
                }
                else
                {

                    btnCreate.Text = "Create";
                    //KioskClearall(true);
                }

            }
            catch (Exception ex) { writeErrorLog(ex, "FillKiosk"); }
        }
        private void FillDisplay()
        {
            try
            {
                string strSql = @"SELECT * FROM token_display_settings JOIN token_devices ON token_display_settings.id = token_devices.token_setting_id WHERE token_devices.id = '" + cbxDisplayDevices.SelectedValue.ToString() + "'";
                DataTable dtDisplay = mGlobal.LocalDBCon.ExecuteQuery(strSql);
                if (dtDisplay.Rows.Count > 0)
                {
                    txtDisplayLogopath.Text = dtDisplay.Rows[0]["logo_path"].ToString();
                    txtDisplayLogopath.Tag = dtDisplay.Rows[0]["id"].ToString();
                    txtDisplayWelcomenote.Text = dtDisplay.Rows[0]["welcome_note"].ToString();
                    txtDisplayMaxpages.Text = dtDisplay.Rows[0]["maximum_option_per_page"].ToString();
                    txtDisplayOtherdetails.Text = dtDisplay.Rows[0]["other_details"].ToString();
                    txtDisplaySlno.Text = dtDisplay.Rows[0]["serial_number"].ToString();
                    chkDisplayActive.Checked = dtDisplay.Rows[0]["active"].ToString() == "Y" ? true : false;
                    txtDisplayRemarks.Text = dtDisplay.Rows[0]["remarks"].ToString();
                    txtDisplayLayoutdetails.Text = dtDisplay.Rows[0]["layout_details"].ToString();
                    btnCreate.Text = "Update";
                    // Assuming dtDisplay is your DataTable
                    string departmentIdsString = dtDisplay.Rows[0]["token_department_ids"].ToString();

            // Split the comma-separated string into an array of strings
            string[] departmentIdsArray = departmentIdsString.Split(',');

            // Parse each string into an integer and check the item
            foreach (string idString in departmentIdsArray)
            {
                int departmentId;
                if (int.TryParse(idString, out  departmentId))
                {
                    CheckItemByValue(departmentId, chkdlbxDisplayDepartment);
                }
                else
                {
                    // Handle the case where the conversion fails, e.g., log an error or show a message.
                }
            }
                   



                }
                else
                {

                    btnCreate.Text = "Create";
                    //DisplayClearall(true);
                }

            }
            catch (Exception ex) { writeErrorLog(ex, "FillDisplay"); }
        }
        
        private void FillAddress(DataTable dtAddress)
        {
            try
            {
                txtAddress1.Tag = dtAddress.Rows[0]["adr_id"].ToString();
                txtAddress1.Text = dtAddress.Rows[0]["address_1"].ToString();
                txtAddress2.Text = dtAddress.Rows[0]["address_2"].ToString();
                txtPlace.Text = dtAddress.Rows[0]["place"].ToString();
                txtZip.Text = dtAddress.Rows[0]["zipcode"].ToString();
                txtPhone1.Text = dtAddress.Rows[0]["phone_1"].ToString();
                txtPhone2.Text = dtAddress.Rows[0]["phone_2"].ToString();
                txtMob1.Text = dtAddress.Rows[0]["mobile_1"].ToString();
                txtMob2.Text = dtAddress.Rows[0]["mobile_2"].ToString();
                txtEmail1.Text = dtAddress.Rows[0]["email_1"].ToString();
                txtEmail2.Text = dtAddress.Rows[0]["email_2"].ToString();
                txtRegno1.Text = dtAddress.Rows[0]["registration_number_1"].ToString();
                txtRegno2.Text = dtAddress.Rows[0]["registration_number_2"].ToString();
                txtWebsite.Text = dtAddress.Rows[0]["website"].ToString();
            }
            catch (Exception ex)
            { writeErrorLog(ex, "FillAddress"); }
        }        
        
        
        
        #endregion

        //Validating required fields
        #region Form Validation
        private bool ValidateFirm()
        {
            try
            {
                if (string.IsNullOrEmpty(txtFirmCode.Text))
                {
                    ShowValidationMessage("Fill out firm code", txtFirmCode);
                    return false;
                }
                else if (string.IsNullOrEmpty(txtFrmname.Text))
                {
                    ShowValidationMessage("Fill out firm name", txtFrmname);
                    return false;
                }
                else if (string.IsNullOrEmpty(txtAddress1.Text))
                {
                    ShowValidationMessage("Fill out Address1", txtAddress1);
                    return false;
                }
                else
                    return true;

            }


            catch (Exception ex)
            {
                return false;
                writeErrorLog(ex, "ValidateFirm");
            }
        }
        private bool ValidateBranch()
        {
            try
            {
                  if (cbxBrchFirm.SelectedValue.ToString() == "-1")
                {
                    ShowValidationMessage("Select firm", cbxBrchFirm);
                    cbxBrchFirm.Focus();
                    return false;
                }
                else if (string.IsNullOrEmpty(txtBrchcode.Text))
                {
                    ShowValidationMessage("Fill out branch code", txtBrchcode);
                    txtBrchcode.Focus();
                    return false;
                }
                else if (string.IsNullOrEmpty(txtBrchname.Text))
                {
                    ShowValidationMessage("Fill out branch name", txtBrchname);
                    txtBrchname.Focus();
                    return false;
                }               
                else if (string.IsNullOrEmpty(txtAddress1.Text))
                {
                    ShowValidationMessage("Fill out address1", txtAddress1);
                    txtAddress1.Focus();
                    return false;
                }
                else
                    return true;
            }
            catch (Exception ex)
            {
                writeErrorLog(ex, "ValidateBranch");
                return false;
            }
        }
        private bool ValidateDepartment()
        {
            try
            {
                if (string.IsNullOrEmpty(txtDepcode.Text))
                {
                    ShowValidationMessage("Fill out department code", txtDepcode);
                    txtDepcode.Focus();
                    return false;
                }
                else if (cbxDepFirm.SelectedValue.ToString() == "-1")
                {
                    ShowValidationMessage("Select firm", cbxDepFirm);
                    cbxDepFirm.Focus();
                    return false;
                }
                else if (cbxDepBranch.SelectedValue.ToString() == "-1")
                {
                    ShowValidationMessage("Select branch", cbxDepBranch);
                    cbxDepBranch.Focus();
                    return false;
                }
                else
                {
                    return true;
                }

            }
            catch (Exception ex)
            {
                writeErrorLog(ex, "ValidateDepartment");
                return false;
            }
        }
        private bool ValidateCounter()
        {
            try
            {
                if (string.IsNullOrEmpty(txtCounterCode.Text))
                {
                    ShowValidationMessage("Fill counter code", txtCounterCode);
                    return false;
                }
                else if (cbxCounterFirm.SelectedValue.ToString() == "-1")
                {
                    ShowValidationMessage("Select Firm", cbxCounterFirm);
                    return false;
                }
                else if (cbxCounterBranch.SelectedValue.ToString() == "-1")
                {
                    ShowValidationMessage("Select branch", cbxCounterBranch);
                    return false;
                }
                else if (cbxCounterDep.SelectedValue.ToString() == "-1")
                {
                    ShowValidationMessage("Select department", cbxCounterDep);
                    return false;
                }
                else
                { return true; }

            }
            catch (Exception ex)
            {
                writeErrorLog(ex, "ValidateCounter");
                return false;
            }
        }
        private bool ValidateService()
        {
            try
            {
                if (string.IsNullOrEmpty(txtSrvccode.Text))
                {
                    ShowValidationMessage("Fill service code", txtSrvccode);
                    return false;
                }
                else if (cbxSrvcFirm.SelectedValue.ToString() == "-1")
                {
                    ShowValidationMessage("Select firm", cbxSrvcFirm);
                    return false;
                }
                else if (cbxSrvcBranch.SelectedValue.ToString() == "-1")
                {
                    ShowValidationMessage("Select Branch", cbxSrvcBranch);
                    return false;
                }
                else if (cbxSrvcDefDept.SelectedValue.ToString() == "-1")
                {
                    ShowValidationMessage("Select Department", cbxSrvcDefDept);
                    return false;
                }
                else if (cbxSrvcDefCounter.SelectedValue.ToString() == "-1")
                {
                    ShowValidationMessage("Select Counter", cbxSrvcDefCounter);
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                writeErrorLog(ex, "ValidateService");
                return false;
            }
        }
        private bool ValidateBillNumber()
        {
            try
            {
                if (string.IsNullOrEmpty(txtBillCode.Text))
                {
                    ShowValidationMessage("Fill bill code", txtBillCode);
                    return false;
                }
                else if (cbxBillFirm.SelectedValue.ToString() == "-1")
                {
                    ShowValidationMessage("Select firm", cbxBillFirm);
                    return false;
                }
                else if (cbxBillBranch.SelectedValue.ToString() == "-1")
                {
                    ShowValidationMessage("Select branch", cbxBillBranch);
                    return false;
                }
                else if (cbxBillService.SelectedValue.ToString() == "-1")
                {
                    ShowValidationMessage("Select service", cbxBillService);
                    return false;
                }
                else
                { return true; }
            }
            catch (Exception ex)
            {
                writeErrorLog(ex, "ValidateBillNumber");
                return false;
            }
        }
        private bool ValidateDevice()
        {
            try
            {
                if (string.IsNullOrEmpty(txtDevicecode.Text))
                {
                    ShowValidationMessage("Fill device code", txtDevicecode);
                    return false;

                }
                else if (cbxDeviceBranch.SelectedValue.ToString() == "-1")
                {
                    ShowValidationMessage("Select branch", cbxDeviceBranch);
                    return false;
                }
                else
                    return true;

            }
            catch (Exception ex)
            {
                writeErrorLog(ex, "ValidateDevice");
                return false;
            }
        }
        private bool ValidateKiosk()
        {
            try
            {
                if (cbxKioskDevices.SelectedValue.ToString() == "-1")
                {
                    ShowValidationMessage("Select device", cbxKioskDevices);
                    return false;
                }
                else if (string.IsNullOrEmpty(txtKioskCode.Text))
                {
                    ShowValidationMessage("Fill kiosk code", txtKioskCode);
                    return false;
                }
                else
                    return true;
            }
            catch (Exception ex)
            {
                writeErrorLog(ex, "ValidateKiosk");
                return false;
            }
        }
        private bool ValidateDisplay()
        {
            try
            {
                if (cbxDisplayDevices.SelectedValue.ToString() == "-1")
                {
                    ShowValidationMessage("Select device", cbxDisplayDevices);
                    return false;
                }
                else
                    return true;
            }
            catch (Exception ex)
            {
                writeErrorLog(ex, "ValidateDisplay");
                return false;
            }
        }
        private bool ValidateTemplate()
        {
            try
            {
                if (string.IsNullOrEmpty(txtTempCode.Text))
                {
                    ShowValidationMessage("Fill Template code", txtTempCode);
                    return false;
                }
                else if (string.IsNullOrEmpty(txtTempDescription.Text))
                {
                    ShowValidationMessage("Fill template description", txtTempDescription);
                    return false;
                }
                else
                    return true;
            }
            catch (Exception ex)
            {
                writeErrorLog(ex, "ValidateTemplate");
                return false;
            }
        }
        private bool ValidateSettingsDepartment()
        {
            try
            {
                if (cbxSetDepartment.SelectedValue.ToString()=="-1")
                {
                    ShowValidationMessage("Select Department", cbxSetDepartment);
                    return false;
                }
                else if (cbxSetNxtdepartment.SelectedValue.ToString() == "-1")
                {
                    ShowValidationMessage("Select end department", cbxSetEnddepartment);
                    return false;
                }
                else
                    return true;
            }
            catch (Exception ex)
            {
                writeErrorLog(ex, "ValidateSettingsDepartment");
                return false;
            }
        }
        private bool ValidateSettingsCounters()
        {
            try
            {
                if (cbxSetCounter.SelectedValue.ToString()== "-1")
                {
                    ShowValidationMessage("Select Counter", cbxSetDepartment);
                    return false;
                }
                else if (cbxSetNxtcounter.SelectedValue.ToString() == "-1")
                {
                    ShowValidationMessage("Select end counter", cbxSetEnddepartment);
                    return false;
                }
                else
                    return true;
            }
            catch (Exception ex)
            {
                writeErrorLog(ex, "ValidateSettingsCounters");
                return false;
            }
        }
        private bool ValidateSettingsService()
        {
            try
            {
                if (cbxSetSrvc.SelectedValue.ToString() == "-1")
                {
                    ShowValidationMessage("Select Service", cbxSetDepartment);
                    return false;
                }
                else if (cbxSetEndcounter.SelectedValue.ToString() == "-1")
                {
                    ShowValidationMessage("Select end  counter", cbxSetEnddepartment);
                    return false;
                }
                else if (cbxSetEnddepartment.SelectedValue.ToString() == "-1")
                {
                    ShowValidationMessage("Select end  department", cbxSetEnddepartment);
                    return false;
                }
                else
                    return true;
            }
            catch (Exception ex)
            {
                writeErrorLog(ex, "ValidateSettingsCounters");
                return false;
            }
        }
        private bool ValidateUserSettings()
        {
            try
            {
                if (string.IsNullOrEmpty(txtUserName.Text))
                {
                    ShowValidationMessage("Fill Username", txtUserName);
                    return false;
                }
                else if (string.IsNullOrEmpty(txtUserEmail.Text))
                {
                    ShowValidationMessage("Fill Email address", txtUserEmail);
                    return false;
                }
                else if (string.IsNullOrEmpty(txtUserPassword.Text))
                {
                    ShowValidationMessage("Fill Password", txtUserPassword);
                    return false;
                }
                else if (mCommFunc.ConvertToString(cbxUserFirms.SelectedValue) == "-1")
                {
                    ShowValidationMessage("Select Firm", cbxUserFirms);
                    return false;
                }
                else if (mCommFunc.ConvertToString(cbxUserBranches.SelectedValue) == "-1")
                {
                    ShowValidationMessage("Select Branch", cbxUserBranches);
                    return false;
                }
                else
                    return true;
            }
            catch (Exception ex)
            { 
             writeErrorLog(ex, "ValidateUserSettings");
            return false;
            }
        }
        #endregion

        //clearing all fields
        #region Clear Fields
    private void FirmClearAll(bool Clear)
        {
            if (Clear == true)
                txtFirmCode.Text = "";
            txtFirmCode.Tag = "";
            txtFrmSlno.Text = "";
            txtFirmCode.Enabled = true;
            txtAddress1.Text = "";
            txtFrmname.Text = "";
            txtPlace.Text = "";
            txtFrmshrtname.Text = "";
            btnCreate.Text = "Create";
            btnCreate.Tag = "";
            txtAddress1.Tag = "";
            chkFrmactive.Checked = false;


        }
        private void BrchClearAll(bool ClearAll)
        {
            if (ClearAll == true)
                txtBrchcode.Text = "";
            txtBrchcode.Tag = "";
            txtBrchSlNo.Text = "";
            txtBrchcode.Enabled = true;
            txtBrchname.Text = "";
            btnCreate.Text = "Create";
            btnCreate.Tag = "";
            cbxBrchFirm.SelectedValue = "-1";
            chkBrchActive.Checked = false;
            
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
        private void DepClearAll(bool Clear)
        {
            if (Clear == true)
                txtDepcode.Text = "";
            txtDepcode.Tag = "";
            txtDepcode.Enabled = true;
            txtDepdesc.Text = "";
            txtDepslno.Text = "";
            cbxDepBranch.SelectedValue = "-1";
            cbxDepFirm.SelectedValue = "-1";
            txtDepotherdet.Text = "";
            chkdepactive.Checked = false;
            btnCreate.Text = "Create";
            btnCreate.Tag = "";


        }
        private void ServiceClearall(bool Clear)
        {
            if (Clear == true)
                txtSrvccode.Text = "";
            txtSrvccode.Tag = "";
            txtSrvccode.Enabled = true;
            txtSrvcOptiontext.Text = "";
            cbxSrvcType.SelectedItem = "--select--";
            cbxSrvcFirm.SelectedValue = "-1";
            cbxSrvcDefDept.SelectedValue = "-1";
            cbxSrvcBranch.SelectedValue = "-1";
            cbxSrvcDefCounter.SelectedValue = "-1";
            cbxSrvcTemplates.SelectedValue = "-1";
            txtSrvcotherdetails.Text = "";
            numSrvcServeinterval.Text = "15";
            txtSrvcSlno.Text = "";
            dtSrvcTknstarttime.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 9, 0, 0);
            chkSrvcactive.Checked = false;
            cbxSrvcTemplates.SelectedValue = "-1";
            btnCreate.Text = "Create";
            btnCreate.Tag = "";
        }
        private void DeviceClearall(bool Clear)
        {
            if (Clear == true)
                txtDevicecode.Text = "";
            txtDevicecode.Tag = "";
            txtDeviceid.Text = "";
            txtDevicecode.Enabled = true;
            txtDevicedescription.Text = "";
            txtDeviceotherdetails.Text = "";
            txtDeviceSlno.Text = "";
            cbxDeviceBranch.SelectedValue = "-1";
            cbxDeviceTemplates.SelectedValue = "-1";
            chkDeviceactive.Checked = false;
            cbxDevicetype.SelectedItem = "--select--";
            txtDevicecaption.Text = "";
            btnCreate.Text = "Create";
            btnCreate.Tag = "";

        }        
        private void CounterClearall(bool Clear)
        {
            if (Clear == true)
                txtCounterCode.Text = "";
            txtCounterCode.Tag = "";
            txtCounterCode.Enabled = true;
            txtCounterDistext.Text = "";
            txtCounterOtherdet.Text = "";
            txtCounterRemarks.Text = "";
            cbxCounterBranch.SelectedValue = "-1";
            cbxCounterDep.SelectedValue = "-1";
            cbxCounterFirm.SelectedValue = "-1";
            chkCounterActive.Checked = false;
            txtCounterSlno.Text = "";
            btnCreate.Text = "Create";
            btnCreate.Tag = "";
        }        
        private void BillClearall(bool Clear)
        {
            if (Clear == true)
                txtBillCode.Text = "";
            txtBillCode.Tag = "";
            txtBillCode.Enabled = true;
            txtBillNumbers.Text = "";
            txtBillPrefix.Text = "";
            txtBillPostfix.Text = "";
            cbxBillBranch.SelectedValue = "-1";
            cbxBillFirm.SelectedValue = "-1";
            cbxBillService.SelectedValue = "-1";
            cbxBillBranch.Enabled = false;
            cbxBillService.Enabled = false;
            chkBillLocked.Checked = false;
            btnCreate.Text = "Create";
            btnCreate.Tag = "";
        }        
        private void KioskClearall(bool Clear)
        {
            if (Clear == true)
                txtKioskCode.Text = "";
            txtKioskCode.Tag = "";
            txtKioskDescription.Text = "";
            txtKioskLayoutdet.Text = "";
            txtKioskMaxpage.Text = "";
            txtKioskRemarks.Text = "";
            txtKioskSlno.Text = "";
            txtKioskOtherdet.Text = "";
            txtKioskCode.Tag = "";
            chkKioskActive.Checked = false;
            btnCreate.Text = "Create";
            btnCreate.Tag = "";
        }       
        private void DisplayClearall(bool Clear)
        {
            if (Clear == true)
                txtDisplayLogopath.Text = "";
            txtDisplayMaxpages.Text = "";
            txtDisplayOtherdetails.Text = "";
            txtDisplayRemarks.Text = "";
            txtDisplaySlno.Text = "";
            txtDisplayLayoutdetails.Text = "";
            txtDisplayWelcomenote.Text = "";
            chkDisplayActive.Checked = false;
            txtDisplayLogopath.Tag = "";
            for (int i = 0; i < chkdlbxDisplayDepartment.Items.Count; i++)
            {
                chkdlbxDisplayDepartment.SetItemChecked(i, false);
            }
            btnCreate.Text = "Create";
        }       
        private void TemplateClearall(bool Clear)
        {
            if (Clear == true)
                txtTempCode.Text = "";
            txtTempCode.Tag = "";
            txtTempCode.Enabled = true;
            txtTempDescription.Text = "";
            txtTempMessage.Text = "";
            txtTempOtherdet.Text = "";
            txtTempSlno.Text = "";
            chkTempActive.Checked = false;
            btnCreate.Text = "Create";
            btnCreate.Tag = "";
        }
        private void SettingsNextDepClearall()
        {
            cbxSetDepartment.SelectedValue = "-1";
            cbxSetNxtdepartment.SelectedValue = "-1";
            cbxSetDepFirm.SelectedValue = "-1";
            cbxSetDepBranch.SelectedValue = "-1";

            cbxSetDepBranch.Enabled = false;
            cbxSetNxtdepartment.Enabled = false;
            cbxSetDepartment.Enabled = false;
            

        }
        private void SettingsNextCounterClearall()
        {
            cbxSetCounter.SelectedValue = "-1";
            cbxSetNxtcounter.SelectedValue = "-1";
            cbxSetCounterFirm.SelectedValue = "-1";
            cbxSetCounterBranch.SelectedValue = "-1";

            cbxSetCounterBranch.Enabled = false;
            cbxSetCounter.Enabled = false;
            cbxSetNxtcounter.Enabled = false;
            
        }
        private void SettingsSrvcClearall()
        {
            cbxSetSrvc.SelectedValue = "-1";
            cbxSetEndcounter.SelectedValue = "-1";
            cbxSetEnddepartment.SelectedValue = "-1";
            cbxSetSrvcFirm.SelectedValue = "-1";
            cbxSetSrvcBranch.SelectedValue = "-1";

            cbxSetEndcounter.Enabled =false;
            cbxSetEnddepartment.Enabled = false;            
            cbxSetSrvcBranch.Enabled = false;
            cbxSetSrvc.Enabled = false;
        
        }
        private void UserClearAll()
        {
            txtUserName.Text = "";
            txtUserEmail.Text = "";
            txtUserPassword.Text = "";
            txtUserName.Tag = "";
            cbxUserGroups.SelectedItem = "GEN";
            cbxUserFirms.SelectedValue = "-1";
            cbxUserBranches.DataSource = null;
            chklstbxUserDepartments.DataSource = null;
            chklstbxUserCounters.DataSource = null;
            cbxUserBranches.Enabled = false;
            chklstbxUserCounters.Enabled = false;
            chklstbxUserDepartments.Enabled = false;

        }
    #endregion

        //Next and previous buttons
        #region Nav buttons
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
        #endregion
        
        //Updating navbutton status
        private void UpdateNavButtonStatus()
        {
            btnNext.Enabled = tabCtrl.SelectedIndex < tabCtrl.TabCount - 1;
            btnPrevious.Enabled = tabCtrl.SelectedIndex > 0;
        }

        //validation message
        private void ShowValidationMessage(string message, Control control)
        {
            MessageBox.Show(message);
            control.Focus();
        }

        //Error Logging
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
        private string GetSelectedValues(CheckedListBox checkedListBox)
        {
            // Create a comma-separated string for selected values
            string selectedValues = "";

            foreach (DataRowView drv in checkedListBox.CheckedItems)
            {
                // Assuming "Name" is the field you want to include in the result
                selectedValues += drv["id"].ToString();

                // Add a comma if it's not the last selected item
                if (checkedListBox.CheckedItems.IndexOf(drv) < checkedListBox.CheckedItems.Count - 1)
                {
                    selectedValues += ",";
                }
            }

            return selectedValues;
        }
        private void UpdateCheckedItems(CheckedListBox checkedListBox, string valuesToUpdate)
        {
            // Split the string into individual values
            string[] valuesArray = valuesToUpdate.Split(',');

            // Iterate through the items and check the corresponding ones
            foreach (string value in valuesArray)
            {
                // Convert the value to the appropriate type (e.g., int)
                int intValue;
                if (int.TryParse(value, out intValue))
                {
                    // Check the item if it exists in the CheckedListBox
                    int index = checkedListBox.FindStringExact(intValue.ToString());
                    if (index != System.Windows.Forms.ListBox.NoMatches)
                    {
                        checkedListBox.SetItemChecked(index, true);
                    }
                }
            }
        }
        /// <summary>
        /// Input accepts number only
        /// </summary>
        /// <param name="valueToCheck"></param>
        #region Number validation
        private void CheckItemByValue(int valueToCheck ,CheckedListBox Chklstbx)
        {
            // Loop through the items in the CheckedListBox
            for (int i = 0; i < Chklstbx.Items.Count; i++)
            {
                // Get the DataRowView from the CheckedListBox item
                DataRowView currentItem = (DataRowView)Chklstbx.Items[i];

                // Assuming your values are integers, change the column name accordingly
                int currentItemValue = Convert.ToInt32(currentItem[0]);

                // Check the item if the values match
                if (currentItemValue == valueToCheck)
                {
                    Chklstbx.SetItemChecked(i, true);
                    break; // Exit the loop once the item is found
                }
            }
        }

        private void dgvData_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            try
            {
                if (e.ColumnIndex == dgvData.Columns["btnCopy"].Index && e.RowIndex >= 0)
                {
                    switch (tabCtrl.SelectedTab.Name)
                    {                        
                        case "tbDepartment":
                            txtDepcode.Text = mCommFunc.ConvertToString(dgvData.CurrentRow.Cells["Code"].Value);
                            txtDepcode.Tag = mCommFunc.ConvertToString(dgvData.CurrentRow.Cells["Id"].Value);
                            if (txtDepcode.Text.ToString().Trim() != "")
                            {
                                GetBranch();
                                cbxDepBranch.Enabled = false;
                                FillDepartment();
                                btnCreate.Text = "Create";
                                txtDepcode.Enabled = true;
                                cbxDepFirm.SelectedValue = "-1";
                                cbxDepBranch.SelectedValue = "-1";
                                
                            }
                            break;
                        case "tbCounter":
                            txtCounterCode.Text = mCommFunc.ConvertToString(dgvData.CurrentRow.Cells["Code"].Value);
                            txtCounterCode.Tag = mCommFunc.ConvertToString(dgvData.CurrentRow.Cells["Id"].Value);
                            if (txtCounterCode.Text.ToString() != "")
                            {
                                GetBranch();
                                GetDepartment();
                                cbxCounterBranch.Enabled = false;
                                cbxCounterDep.Enabled = false;
                                FillCounter();
                                btnCreate.Text = "Create";
                                txtCounterCode.Enabled = true;
                                cbxCounterFirm.SelectedValue = "-1";
                                cbxCounterBranch.SelectedValue = "-1";
                                cbxCounterDep.SelectedValue = "-1";
                                
                            }
                            break;
                        case "tbService":
                            txtSrvccode.Text = mCommFunc.ConvertToString(dgvData.CurrentRow.Cells["Code"].Value);
                            txtSrvccode.Tag = mCommFunc.ConvertToString(dgvData.CurrentRow.Cells["Id"].Value);
                            if (txtSrvccode.Text.ToString() != "")
                            {
                                GetBranch();
                                GetDepartment();
                                GetCounter();

                                FillServices();
                                cbxSrvcBranch.Enabled = false;
                                cbxSrvcDefDept.Enabled = false;
                                cbxSrvcDefCounter.Enabled = false;                                
                                btnCreate.Text = "Create";
                                txtSrvccode.Enabled = true;
                                cbxSrvcFirm.SelectedValue = "-1";
                                cbxSrvcBranch.SelectedValue = "-1";
                                cbxSrvcDefDept.SelectedValue = "-1";
                                cbxSrvcDefCounter.SelectedValue = "-1";
                                
                            }
                            break;
                        case "tbBill":
                            txtBillCode.Text = mCommFunc.ConvertToString(dgvData.CurrentRow.Cells["Description"].Value);
                            txtBillCode.Tag = mCommFunc.ConvertToString(dgvData.CurrentRow.Cells["Id"].Value);
                            if (txtBillCode.Text.ToString() != "")
                            {
                                GetBranch();
                                GetService();
                                cbxBillBranch.Enabled = false;
                                cbxBillService.Enabled = false;
                                FillBill();
                                btnCreate.Text = "Create";
                                txtBillCode.Enabled = true;
                                cbxBillFirm.SelectedValue = "-1";
                                cbxBillBranch.SelectedValue = "-1";
                                cbxBillService.SelectedValue = "-1";
                                
                            }
                            break;
                        case "tbTemplate":
                            txtTempCode.Text = mCommFunc.ConvertToString(dgvData.CurrentRow.Cells["Code"].Value);                            
                            if (txtTempCode.Text.ToString() != "")
                            {
                                FillTemplate();
                                btnCreate.Text = "Create";
                                txtTempCode.Tag = "";
                                txtTempCode.Enabled = true;
                                
                            }
                            break;
                        case "tbDevice":
                            txtDevicecode.Text = mCommFunc.ConvertToString(dgvData.CurrentRow.Cells["code"].Value);
                            txtDevicecode.Tag = mCommFunc.ConvertToString(dgvData.CurrentRow.Cells["Id"].Value);
                            if (txtDevicecode.Text.ToString() != "")
                            {
                                FillDevices();
                                btnCreate.Text = "Create";
                                txtDevicecode.Enabled = true;
                                cbxDeviceBranch.SelectedValue = "-1";
                                
                            }
                            break;
                        case "tbKiosk":
                            cbxKioskDevices.SelectedValue = dgvData.CurrentRow.Cells["DeviceId"].Value.ToString();
                            if (cbxKioskDevices.SelectedValue != null)
                            {
                                FillKiosk();
                                btnCreate.Text = "Create";
                                txtKioskCode.Tag = "";
                                
                                
                            }
                            else
                                KioskClearall(true);

                            break;
                        case "tbDisplay":
                            cbxDisplayDevices.SelectedValue = mCommFunc.ConvertToString(dgvData.CurrentRow.Cells["DeviceId"].Value);
                            if (cbxDisplayDevices.SelectedValue != null)
                            {
                                GetDisplayDepartment();
                                FillDisplay();
                                btnCreate.Text = "Create";
                                txtDisplayLogopath.Tag = "";
                            
                        }
                            else
                                DisplayClearall(true);
                            break;                       
                        default:
                            AddressClearAll();
                            break;
                    }
                }
            }
            catch (Exception ex)
            { writeErrorLog(ex, "dgvData_CellDoubleClick"); }

        }

        private void NumericOnly_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; // Suppress the key press if it's not a digit or a control key
            }
        }

        private void txtBrchSlNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            NumericOnly_KeyPress(sender, e);
        }

        private void txtDepslno_KeyPress(object sender, KeyPressEventArgs e)
        {
            NumericOnly_KeyPress(sender, e);
        }

        private void txtCounterSlno_KeyPress(object sender, KeyPressEventArgs e)
        {
            NumericOnly_KeyPress(sender, e);
        }

        private void txtSrvcSlno_KeyPress(object sender, KeyPressEventArgs e)
        {
            NumericOnly_KeyPress(sender, e);
        }

        private void txtBillNumbers_KeyPress(object sender, KeyPressEventArgs e)
        {
            NumericOnly_KeyPress(sender, e);
        }

        private void txtTempSlno_KeyPress(object sender, KeyPressEventArgs e)
        {
            NumericOnly_KeyPress(sender, e);
        }

        private void txtDeviceSlno_KeyPress(object sender, KeyPressEventArgs e)
        {
            NumericOnly_KeyPress(sender, e);
        }

        private void txtKioskSlno_KeyPress(object sender, KeyPressEventArgs e)
        {
            NumericOnly_KeyPress(sender, e);
        }

        private void txtDisplaySlno_KeyPress(object sender, KeyPressEventArgs e)
        {
            NumericOnly_KeyPress(sender, e);
        }

        private void txtFrmSlno_KeyPress(object sender, KeyPressEventArgs e)
        {
            NumericOnly_KeyPress(sender, e);
        }
        #endregion

        private void cbxMFirms_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                string strSql="";
                if(cbxMFirms.SelectedValue.ToString()!="-1")
                strSql = "select id,description from token_branches where firm_id='"+cbxMFirms.SelectedValue.ToString()+"'";
                else
                strSql = "select id,description from token_branches ";

                DataTable dtBranches = mGlobal.LocalDBCon.ExecuteQuery(strSql);
                cbxMBranches.DataSource = dtBranches;
                cbxMBranches.DisplayMember = "description";
                cbxMBranches.ValueMember = "id";
                FilterFirmGridView();
            }
            catch (Exception ex) { writeErrorLog(ex, "cbxMFirms_SelectionChangeCommitted"); }
        }

        private void cbxMBranches_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                FilterBranchGridView();
            }
            catch (Exception ex)
            { writeErrorLog(ex, "cbxMBranches_SelectionChangeCommitted"); }

        }
        private void FilterFirmGridView()
        {
            try
            {
                switch (tabCtrl.SelectedTab.Name)
                {
                    
                    case "tbBranch":
                        if (cbxMFirms.SelectedValue.ToString() != "-1")
                            FillGridview(mstrBranchqry + " where firm_id='" + cbxMFirms.SelectedValue.ToString() + "'");
                        else
                            FillGridview(mstrBranchqry);
                        break;
                    case "tbDepartment":
                        if(cbxMFirms.SelectedValue.ToString()!="-1")
                        FillGridview(mstrDepartmentqry + " where Department.firm_id='"+cbxMFirms.SelectedValue.ToString()+"'");
                    else
                        FillGridview(mstrDepartmentqry);
                        break;
                    case "tbCounter":
                        if (cbxMFirms.SelectedValue.ToString() != "-1")
                        FillGridview(mstrCounterqry + " where Counter.firm_id='" + cbxMFirms.SelectedValue.ToString() + "'");
                    else
                        FillGridview(mstrCounterqry);
                        break;
                    case "tbService":
                        if (cbxMFirms.SelectedValue.ToString() != "-1")
                        FillGridview(mstrServiceqry + " where Service.firm_id='" + cbxMFirms.SelectedValue.ToString() + "'");
                    else
                        FillGridview(mstrServiceqry);
                        break;
                    case "tbBill":
                        if (cbxMFirms.SelectedValue.ToString() != "-1")
                        FillGridview(mstrBillqry + " where Bill.firm_id='" + cbxMFirms.SelectedValue.ToString() + "'");
                    else
                        FillGridview(mstrBillqry);
                        break;
                    
                    default:
                        
                        break;
                }
            }
            catch (Exception ex) { writeErrorLog(ex,"FilterGridview"); }
        }
         private void FilterBranchGridView()
        {
            try
            {
                switch (tabCtrl.SelectedTab.Name)
                {
                    case "tbDepartment":
                        if(cbxMFirms.SelectedValue.ToString()=="-1")
                        FillGridview(mstrDepartmentqry + " where Department.branch_id='"+cbxMBranches.SelectedValue.ToString()+"'");
                        else
                        FillGridview(mstrDepartmentqry + " where Department.branch_id='" + cbxMBranches.SelectedValue.ToString() + "' and Department.firm_id='"+cbxMFirms.SelectedValue.ToString()+"'");
                        break;
                    case "tbCounter":
                        if (cbxMFirms.SelectedValue.ToString() == "-1")
                            FillGridview(mstrCounterqry + " where Counter.branch_id='" + cbxMBranches.SelectedValue.ToString() + "'");
                        else
                            FillGridview(mstrCounterqry + " where Counter.branch_id='" + cbxMBranches.SelectedValue.ToString() + "' and Counter.firm_id='" + cbxMFirms.SelectedValue.ToString() + "'");
                        break;
                    case "tbService":
                        if (cbxMFirms.SelectedValue.ToString() == "-1")
                            FillGridview(mstrServiceqry + " where Service.branch_ids='" +"\""+ cbxMBranches.SelectedValue.ToString() +"\""+ "'");
                        else
                            FillGridview(mstrServiceqry + " where Service.branch_ids='" + "\"" + cbxMBranches.SelectedValue.ToString() + "\"" + "' and Service.firm_id='" + cbxMFirms.SelectedValue.ToString() + "'");
                        break;
                    case "tbBill":
                        if (cbxMFirms.SelectedValue.ToString() == "-1")
                            FillGridview(mstrBillqry + " where Bill.branch_id='" + cbxMBranches.SelectedValue.ToString() + "'");
                        else
                            FillGridview(mstrBillqry + " where Bill.branch_id='" + cbxMBranches.SelectedValue.ToString() + "' and Bill.firm_id='" + cbxMFirms.SelectedValue.ToString() + "'");
                        break;
                    case "tbDevice":
                        if (cbxMBranches.SelectedValue.ToString() != "-1")
                            FillGridview(mstrDeviceqry + " where Device.branch_id='" + cbxMBranches.SelectedValue.ToString() + "'");
                        else
                            FillGridview(mstrDeviceqry);
                        break;   
                    case "tbDisplay":
                        if (cbxMBranches.SelectedValue.ToString() != "-1")
                            FillGridview(mstrDisplayqry + " and Device.branch_id='" + cbxMBranches.SelectedValue.ToString() + "'");
                        else
                            FillGridview(mstrDisplayqry);
                        break;
                    case "tbKiosk":
                        if (cbxMBranches.SelectedValue.ToString() != "-1")
                            FillGridview(mstrKioskqry + " and Device.branch_id='" + cbxMBranches.SelectedValue.ToString() + "'");
                        else
                            FillGridview(mstrKioskqry);
                        break; 
                    default:
                        
                        break;
                }
            }
            catch (Exception ex) { writeErrorLog(ex,"FilterGridview"); }
        }
        private void HideMFirm()
        {
            try
            {
                lblMfirm.Visible = false;
                cbxMFirms.Visible = false;
            }
            catch (Exception ex) { writeErrorLog(ex, "HideMfirm"); }
        }
        private void HideMBranch()
        {
            try
            {
                lblMBranch.Visible = false;
                cbxMBranches.Visible = false;
            }
            catch (Exception ex) { writeErrorLog(ex, "HideMBranch"); }
        }
        private void DisplayMFirm()
        {
            try
            {
                lblMfirm.Visible = true;
                cbxMFirms.Visible = true;
                cbxMFirms.SelectedValue = "-1";
            }
            catch (Exception ex) { writeErrorLog(ex, "DisplayMFirm"); }
        }
        private void DisplayMBranch()
        {
            try
            {
                lblMBranch.Visible = true;
                cbxMBranches.Visible = true;
                cbxMBranches.SelectedValue = "-1";
            }
            catch (Exception ex) { writeErrorLog(ex, "DisplayMFirm"); }
        }

        private void tabSettings_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                switch (tabSettings.SelectedTab.Name)
                {
                    case "tbNextdepartment":
                        FillGridview(mstrDepartmentqry);
                        dgvData.Columns[0].Visible = false;
                        btnCreate.Text = "Create";
                        //GetSettingsDepFirms();
                        break;
                    case "tbNextcounter":
                        FillGridview(mstrCounterqry);
                        dgvData.Columns[0].Visible = false;
                        btnCreate.Text = "Create";
                        //GetSettingsCounterFirms();
                        break;
                    case "tbSettingservice":
                        FillGridview(mstrServiceqry);
                        dgvData.Columns[0].Visible = false;
                        btnCreate.Text = "Create";
                        //GetSettingsSrvcFirms();
                        break;
                    default:
                        break;
                }
                
            }
            catch (Exception ex)
            { writeErrorLog(ex, "tabSettings_SelectedIndexChanged"); }
        }

        private void cbxSetDepartment_SelectionChangeCommitted(object sender, EventArgs e)
        {
            FilterNextDepartment();
        }

        private void cbxSetCounter_SelectionChangeCommitted(object sender, EventArgs e)
        {
            FillNextCounter();
        }

        private void cbxSetSrvc_SelectionChangeCommitted(object sender, EventArgs e)
        {
            FillSrvcEndCouterDep();
        }

        private void cbxSetDepFirm_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                FiltercbxSetDepBranch();
            }
            catch (Exception ex) { writeErrorLog(ex, "cbxSetDepFirm_SelectionChangeCommitted"); }

        }
        private void FiltercbxSetDepBranch()
        {
            cbxSetDepartment.SelectedValue = "-1";
            cbxSetDepartment.Enabled = false;
            cbxSetNxtdepartment.SelectedValue = "-1";
            cbxSetNxtdepartment.Enabled = false;
            string strSql="";
            if(cbxSetDepFirm.SelectedValue.ToString()!="-1")
             strSql= "select id,description from token_branches where firm_id='" + cbxSetDepFirm.SelectedValue.ToString() + "'";
           
            DataTable dtbranches = mGlobal.LocalDBCon.ExecuteQuery(strSql);
            if (dtbranches.Rows.Count > 0)
            {
                dtbranches.Rows.Add("-1", "--select--");
                cbxSetDepBranch.DataSource = dtbranches;
                cbxSetDepBranch.DisplayMember = "description";
                cbxSetDepBranch.ValueMember = "id";
                cbxSetDepBranch.SelectedValue = "-1";
                cbxSetDepBranch.Enabled = true;

            }
            else
            {
                cbxSetDepBranch.Enabled = false;
            }
        }

        private void cbxSetDepBranch_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                FiltercbxSetDepartments();
            }
            catch (Exception ex) { writeErrorLog(ex, "cbxSetDepBranch_SelectionChangeCommitted"); }

        }
        private void FiltercbxSetDepartments()
        {
            try
            {    string strSql="";
            if (cbxSetDepBranch.SelectedValue.ToString() != "-1")
                strSql = "select id,description from token_departments where branch_id='" + cbxSetDepBranch.SelectedValue.ToString() + "'";
            else
                strSql = "select id,description from token_departments";

                DataTable dtDepartments = mGlobal.LocalDBCon.ExecuteQuery(strSql);
                if (dtDepartments.Rows.Count > 0)
                {
                    dtDepartments.Rows.Add("-1", "--select--");


                    cbxSetDepartment.DataSource = dtDepartments;
                    cbxSetDepartment.DisplayMember = "description";
                    cbxSetDepartment.ValueMember = "id";
                    cbxSetDepartment.SelectedValue = "-1";
                    cbxSetDepartment.Enabled = true;


                }
                else
                {
                    cbxSetDepBranch.Enabled = false;

                }
            }
            catch (Exception ex)
            { writeErrorLog(ex, "FiltercbxSetDepartments"); }
        }

        private void cbxSetCounterFirm_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                
                cbxSetCounter.SelectedValue = "-1";
                cbxSetCounter.Enabled = false;
                cbxSetNxtcounter.SelectedValue = "-1";
                cbxSetNxtcounter.Enabled=false;
                string strSql = "select id,description from token_branches where firm_id='" + cbxSetCounterFirm.SelectedValue.ToString() + "'";
                DataTable dtBranches = mGlobal.LocalDBCon.ExecuteQuery(strSql);
                if (dtBranches.Rows.Count > 0)
                {
                    dtBranches.Rows.Add("-1", "--select--");
                    cbxSetCounterBranch.DataSource = dtBranches;
                    cbxSetCounterBranch.DisplayMember = "description";
                    cbxSetCounterBranch.ValueMember = "id";
                    cbxSetCounterBranch.SelectedValue = "-1";
                    cbxSetCounterBranch.Enabled = true;

                }
                else 
                {
                    cbxSetCounterBranch.Enabled = false;
 
                }

            }
            catch (Exception ex)
            { writeErrorLog(ex, "cbxSetCounterFirm_SelectionChangeCommitted"); }
        }

        private void cbxSetCounterBranch_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {

                string strSql = "select id,display_text from token_counters where branch_id='" + cbxSetCounterBranch.SelectedValue.ToString() + "'";
                DataTable dtCounters = mGlobal.LocalDBCon.ExecuteQuery(strSql);
                if (dtCounters.Rows.Count > 0)
                {
                    dtCounters.Rows.Add("-1", "--select--");

                    cbxSetCounter.DataSource = dtCounters;
                    cbxSetCounter.DisplayMember = "display_text";
                    cbxSetCounter.ValueMember = "id";
                    cbxSetCounter.SelectedValue = "-1";
                    cbxSetCounter.Enabled = true;

                }
                else
                {
                    cbxSetCounter.Enabled = false;

                }

            }
            catch (Exception ex)
            { writeErrorLog(ex, "cbxSetCounterFirm_SelectionChangeCommitted"); }
        }

        private void cbxSetSrvcFirm_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                cbxSetSrvc.SelectedValue = "-1";
                cbxSetSrvc.Enabled = false;
                cbxSetEndcounter.SelectedValue = "-1";
                cbxSetEndcounter.Enabled = false;
                cbxSetEnddepartment.SelectedValue = "-1";
                cbxSetEnddepartment.Enabled = false;
                string strSql = "select id,description from token_branches where firm_id='" + cbxSetSrvcFirm.SelectedValue.ToString() + "'";
                DataTable dtBranches = mGlobal.LocalDBCon.ExecuteQuery(strSql);
                if (dtBranches.Rows.Count > 0)
                {
                    dtBranches.Rows.Add("-1", "--select--");
                    cbxSetSrvcBranch.DataSource = dtBranches;
                    cbxSetSrvcBranch.DisplayMember = "description";
                    cbxSetSrvcBranch.ValueMember = "id";
                    cbxSetSrvcBranch.SelectedValue = "-1";
                    cbxSetSrvcBranch.Enabled = true;

                }
                else
                {
                    cbxSetSrvcBranch.Enabled = false;

                }
            }
            catch (Exception ex) { writeErrorLog(ex, "cbxSetSrvcFirm_SelectionChangeCommitted"); }
        }

        private void cbxSetSrvcBranch_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                
                cbxSetEndcounter.SelectedValue = "-1";
                cbxSetEndcounter.Enabled = false;
                cbxSetEnddepartment.SelectedValue = "-1";
                cbxSetEnddepartment.Enabled = false;
                btnCreate.Text = "Create";
                string strSql = "select id,code+'_'+option_text as code  from token_services where branch_ids='" + "\"" + cbxSetSrvcBranch.SelectedValue.ToString() + "\"" + "'";
                DataTable dtServices = mGlobal.LocalDBCon.ExecuteQuery(strSql);
                if (dtServices.Rows.Count > 0)
                {
                    dtServices.Rows.Add("-1", "--select--");

                    cbxSetSrvc.DataSource = dtServices;
                    cbxSetSrvc.DisplayMember = "code";
                    cbxSetSrvc.ValueMember = "id";
                    cbxSetSrvc.SelectedValue = "-1";
                    cbxSetSrvc.Enabled = true;

                }
                else
                {
                    cbxSetSrvc.Enabled = false;

                }
                FiltercbxSetEnddepartment();
                FiltercbxSetEndcounter();
                
            }
            catch (Exception ex) { writeErrorLog(ex, "cbxSetSrvcBranch_SelectionChangeCommitted"); }
        }
        private void FiltercbxSetEndcounter()
        {
            try
            {
                string strSql = "select id,code+'_'+display_text as code from token_counters where branch_id='" + mCommFunc.ConvertToString(cbxSetSrvcBranch.SelectedValue) + "'";
                DataTable dtCounters = mGlobal.LocalDBCon.ExecuteQuery(strSql);
                if (dtCounters.Rows.Count > 0 && dtCounters != null)
                {
                    cbxSetEndcounter.Enabled = true;
                    dtCounters.Rows.Add("-1", "--select--");
                    cbxSetEndcounter.DataSource = dtCounters;
                    cbxSetEndcounter.DisplayMember = "code";
                    cbxSetEndcounter.ValueMember = "id";
                    cbxSetEndcounter.SelectedValue = "-1";

                }
                else
                {
                    cbxSetEndcounter.Enabled = false;
                }
            }
            catch (Exception ex)
            { writeErrorLog(ex, "FiltercbxSetEndcounter"); }
        }
        private void FiltercbxSetEnddepartment()
        {
            try
            {
                string strSql = "select id,code+'_'+description as code from token_departments where branch_id='" + mCommFunc.ConvertToString(cbxSetSrvcBranch.SelectedValue) + "'";
                DataTable dtDepartments = mGlobal.LocalDBCon.ExecuteQuery(strSql);
                if (dtDepartments.Rows.Count > 0 && dtDepartments != null)
                {
                    cbxSetEnddepartment.Enabled = true;
                    dtDepartments.Rows.Add("-1", "--select--");
                    cbxSetEnddepartment.DataSource = dtDepartments;
                    cbxSetEnddepartment.DisplayMember = "code";
                    cbxSetEnddepartment.ValueMember = "id";
                    cbxSetEnddepartment.SelectedValue = "-1";
                }
                else
                {                    
                    cbxSetEnddepartment.Enabled = false; 
                }
            }
            catch (Exception ex)
            { writeErrorLog(ex, "FiltercbxSetEnddepartment"); }
        }

        private void cbxUserBranches_SelectionChangeCommitted(object sender, EventArgs e)
        {
            FilterUserDepartments();
            
        }
        private void FilterUserDepartments()
        {
            try
            {
                string strSql = "select id,description from token_departments where branch_id='"+mCommFunc.ConvertToString(cbxUserBranches.SelectedValue)+"'";
                DataTable dtDepartments = mGlobal.LocalDBCon.ExecuteQuery(strSql);
                if (dtDepartments != null && dtDepartments.Rows.Count > 0)
                {
                    
                    chklstbxUserDepartments.DataSource = dtDepartments;
                    chklstbxUserDepartments.DisplayMember = "description";
                    chklstbxUserDepartments.ValueMember = "id";
                    chklstbxUserDepartments.Enabled = true;

                }


            }
            catch (Exception ex)
            { }
        }
         

       
        private void FilterUserCounters(string strDepids)
        {
            try
            {
                string strSql = "";
                if (!string.IsNullOrEmpty(strDepids) && strDepids!=" ")
                {
                    strSql = @"select id,display_text from token_counters where department_id in (" + strDepids + ")";
                    DataTable dtCounters = mGlobal.LocalDBCon.ExecuteQuery(strSql);
                    if (dtCounters != null && dtCounters.Rows.Count > 0)
                    {
                        chklstbxUserCounters.DataSource = dtCounters;
                        chklstbxUserCounters.ValueMember = "id";
                        chklstbxUserCounters.DisplayMember = "display_text";
                        chklstbxUserCounters.Enabled = true;
                    }
                    else
                    {
                        chklstbxUserCounters.Enabled = false;
                        chklstbxUserCounters.DataSource = null;
                    }
                }

                else
                    chklstbxUserCounters.DataSource = null;
                    //chklstbxUserCounters.Items.Clear();

               
            }
            catch (Exception ex)
            {
                writeErrorLog(ex, "FilterUserCounters");
            }
        }
        private void chklstbxUserDepartments_Validating(object sender, CancelEventArgs e)
        {
            string strDepids = GetSelectedValues(chklstbxUserDepartments);

            FilterUserCounters(strDepids);
        }

        private void cbxUserFirms_SelectionChangeCommitted(object sender, EventArgs e)
        {
            FilterUserBranches();
        }
        private void FilterUserBranches()
        {
            try
            {
                String strSql = @"Select id,description from token_branches where firm_id='"+mCommFunc.ConvertToString(cbxUserFirms.SelectedValue)+"'";
                DataTable dtFirms = mGlobal.LocalDBCon.ExecuteQuery(strSql);
                if (dtFirms != null && dtFirms.Rows.Count > 0)
                {
                    cbxUserBranches.Enabled = true;
                    cbxUserBranches.DataSource = dtFirms;
                    cbxUserBranches.ValueMember = "id";
                    cbxUserBranches.DisplayMember = "description";

                }
                else
                {
                    cbxUserBranches.Enabled = false;
                    cbxUserBranches.DataSource = null;
                }
            }
            catch (Exception ex)
            { writeErrorLog(ex, "FilterUserFIrms"); }
        }
    }
}