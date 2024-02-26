using System;
using System.Collections.Generic;
using System.Text;
using System.Data.OleDb;
using System.Data;
using CsHms;

class Global
{
    //private static DBConnAcc vLocalDBCon = new DBConnAcc();
    private static DbConnSql vLocalDBCon = new DbConnSql();
    private static DbConnSql_Log vLocalDBConLog = new DbConnSql_Log(System.Configuration.ConfigurationSettings.AppSettings.Get("LogSqlConString"));
    private static object vMisDCon;
    private static String vServerIP = "127.0.0.1";//"208.109.181.108";//"127.0.0.1";
    private static bool vIsConnected = false;
    private static String vDBType = "S";//Client A=Access,S=Sql Server
    private static String vServerDBType = "S";//
    private static int vSearchKey = 70;
   
    private static int vNewNoKey = 27;
    private static String vCurrentUser = "";
    private static String vCurrentUserID = "";
    private static String vCurrentUserGroup = "A";
    private static Boolean vIsCurrentUserAdmin = false;
    private static String vFirmId = "1";
    private static String vFirmName = "";
    private static String vFirmAddress = "";
    
    private static bool mvrboolMSendPending;
    private static CsHms.MDIMain vMDIMainObj = null;
    private static DateTime vStartDate = DateTime.Now;
    private static String vRunningDateMode = "S"; //S-Server date, D-from database field 
    private static Boolean vCredentialsCheckOnEachCall = false;
    private static DateTime vRunningDate = DateTime.Now;
    private static int vCurrentShiftNo = 1;
    private static DateTime vCurrentShiftDate = DateTime.Now;
    private static int vFinYearID = 8;
    private static Int64 vCurrentLoginID = -1;
    private static int vLimitRows = 50;
    private static int vRegValidDays = 0;
    private static Decimal vRegFees = 0;
    private DataTable mdtSettingsList;
    public static string TablePrefix = "";
    public String ServerDateTime = DateTime.Now.ToString();
    public   Global()
    {            
       // vBranchCode = "";
       // mdtSettingsList = LocalDBCon.ExecuteQuery("select * from settingslist");       
    } 
    public CsHms.MDIMain MDIMainObj
    {
        get { return vMDIMainObj; }
        set { vMDIMainObj = value; }
    }
    public DbConnSql LocalDBCon
    {
        get { return vLocalDBCon; }
        set { vLocalDBCon = value;}
    }
    public DbConnSql_Log LocalDBConLog
    {
        get { return vLocalDBConLog; }
        set { vLocalDBConLog = value; }
    }
    public bool MSendPending
    {
        get { return mvrboolMSendPending; }
        set { mvrboolMSendPending = value; }
    }

    bool vIsHO;
    public bool IsHO
    {
        get { return vIsHO; }
        set { vIsHO = value; }
    }


    public String LoginDoctorCode
    {
        get { return ""; }
        
    }
    public Object MisDCon
    {
        get { return vMisDCon; }
        set { vMisDCon = value; }
    }
    public String ServerIP
    {
        get { return vServerIP; }
        set { vServerIP = value; }
    }
    public bool IsConnected
    {
        get { return vIsConnected; }
        set { vIsConnected = value; }
    }
    public String DBType
    {
        get { return vDBType; }
        set { vDBType = value; }
    }
    public String ServerDBType
    {
        get { return vServerDBType; }
        set { vServerDBType = value; }
    }
    public String ConnectionError
    {
        get { return "Unable to connect server. Please check the internet connection."; }
    }
    public int SearchKey
    {
        get { return vSearchKey; }
        set { vSearchKey = value; }
    }
    public int NewNumberKey
    {
        get { return vNewNoKey; }
        set { vNewNoKey = value; }
    }
    public DateTime StartDate
    {
        get { return vStartDate; }
        set { vStartDate = value; }
    }
    public String RunningDateMode
    {
        get { return vRunningDateMode; }
        set { vRunningDateMode = value; }
    }
    public Boolean CredentialsCheckOnEachCall
    {
        get { return vCredentialsCheckOnEachCall; }
        set { vCredentialsCheckOnEachCall = value; }
    }
    public int FinYearID
    {
        get { return vFinYearID; }
        set { vFinYearID = value; }
    }
    public Int64 CurrentLoginID
    {
        get { return vCurrentLoginID; }
        set { vCurrentLoginID = value; }
    }
    public DateTime RunningDate
    {
        get { return vRunningDate; }
        set { vRunningDate = value; }
    }
    public int CurrentShiftNo
    {
        get { return vCurrentShiftNo; }
        set { vCurrentShiftNo = value; }
    }
    public DateTime CurrentShiftDate
    {
        get { return vCurrentShiftDate; }
        set { vCurrentShiftDate = value; }
    }
    public String CurrentUser
    {
        get { return  "vipin"; }
        set { vCurrentUser = value; }
    }
    public String CurrentUserID
    {
        get { return "2"; }
        set { vCurrentUserID = value; }
    }
    public String CurrentUserGroup
    {
        get { return vCurrentUserGroup; }
        set { vCurrentUserGroup = value; }
    }
    public Boolean IsCurrentUserAdmin
    {
        get { return vIsCurrentUserAdmin; }
        set { vIsCurrentUserAdmin = value; }

    }
    public String FirmId
    {
        get { return vFirmId; }
        set { vFirmId = value; }
    }
    public String FirmName
    {
        get { return vFirmName; }
        set { vFirmName = value; }
    }

    string vBranchCode;
    public String BranchCode
    {
        get { return vBranchCode; }
        set { vBranchCode = value; }
    }

   
    public String FirmAddress
    {
        get { return vFirmAddress; }
        set { vFirmAddress = value; }
    }
    public String PurchaseHeadPtr
    {
        get { return "PUR"; }
    }
    public String PurAddHeadPtr
    {
        get { return "EXD"; }
    }
    public String PurLessHeadPtr
    {
        get { return "PURDIS"; }
    }
    public String PurAdvHeadPtr //Purchase advance head
    {
        get { return "PURADV"; }
    }
    public String SalesHeadPtr
    {
        get { return "SAL"; }
    }
    public String RichTextBoxKey
    {
        get { return "DFK9343NKK23@42LLJHJ234LL234@"; }
    }
    public DataTable GetSettingsListDatatable(String _Mode)
    {
        CommFuncs CommFuncs = new CommFuncs();
        return CommFuncs.ConvertRowCollToDataTable(mdtSettingsList.Select("settl_mode='" + _Mode + "' "), mdtSettingsList);
    }
    public String PharmacyEXE
    {
        get
        {
            return System.Configuration.ConfigurationSettings.AppSettings.Get("StartUpPath") + "/Pharmacy/CsPharma.exe";
            //return System.Windows.Forms.Application.StartupPath + "/Pharmacy/CsPharma.exe";
        }
    }
    public String ServerDate
    {
        get
        {
            return ServerDate_OnTran(false);
        }
    }
    public String ServerDate_OnTran(bool _OnTran)
    {
        switch (ServerDBType)
        {
            case "S":
                if (_OnTran)
                    return LocalDBCon.ExecuteQuery_OnTran("select CONVERT(VARCHAR(10),GETDATE(),103)").Rows[0][0].ToString();
                else
                    return LocalDBCon.ExecuteQuery("select CONVERT(VARCHAR(10),GETDATE(),103)").Rows[0][0].ToString();
                break;
        }
        return DateTime.Now.ToShortDateString();
    }
    public String ServerTime
    {
        get
        {
            return ServerTime_OnTran(false);
        }
    }
    public String ServerTime_OnTran(bool _OnTran)
    {
        switch (ServerDBType)
        {
            case "S":
                if (_OnTran)
                    return LocalDBCon.ExecuteQuery_OnTran("select CONVERT(VARCHAR(8),GETDATE(),108)").Rows[0][0].ToString();
                else
                    return LocalDBCon.ExecuteQuery("select CONVERT(VARCHAR(8),GETDATE(),108)").Rows[0][0].ToString();
                break;
        }
        return DateTime.Now.ToShortDateString();
    }

    public String BankGroupCode
    {
        get
        {
            return LocalDBCon.ExecuteQuery("select ACG_CODE from acgroupmas where ltrim(rtrim(acg_desc))='BANK'").Rows[0][0].ToString();
        }
    }


    public void RefreshCredentials() { RefreshCredentials(false); }
    public void RefreshCredentials(bool vForce)
    {
        if (!vForce && !vCredentialsCheckOnEachCall) return;
        try
        {
            CommFuncs CommFuncs = new CommFuncs();
            DataTable dtData = new DataTable();
            dtData = LocalDBCon.ExecuteQuery("select gs_curfinyearid,gs_rundt,gs_curshiftno,gs_curshiftdt from gensett");
            if (dtData.Rows.Count <= 0) return;
            try
            {
                if (vRunningDateMode == "D")
                    vRunningDate = DateTime.Parse(dtData.Rows[0]["gs_rundt"].ToString());
                else if (vRunningDateMode == "S")
                    vRunningDate = DateTime.Parse(ServerDate);
                else
                    vRunningDate = DateTime.Parse(DateTime.Now.ToShortDateString());
            }
            catch { }
            vFinYearID = CommFuncs.ConvertToInt(dtData.Rows[0]["gs_curfinyearid"].ToString());
            vCurrentShiftNo = CommFuncs.ConvertToInt(dtData.Rows[0]["gs_curshiftno"].ToString());
            try { vCurrentShiftDate = DateTime.Parse(dtData.Rows[0]["gs_curshiftdt"].ToString()); }
            catch { }
        }
        catch { }
    }
    public int LimitRows
    {
        get { return vLimitRows; }
        set { vLimitRows = value; }
    }
    public int RegValidDays
    {
        get { return vRegValidDays; }
        set { vRegValidDays = value; }
    }

    public int FinYearId
    {
        get { return vFinYearID ; }
        set { vFinYearID = value; }
    }


    public Decimal RegFees
    {
        get { return vRegFees; }
        set { vRegFees = value; }
    }
}
