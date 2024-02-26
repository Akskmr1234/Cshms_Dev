using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.ComponentModel;

public static class AuditLog
{
    #region Login && Logout Log
    public static void Login() { Login(true); }
    public static void LogOut() { Login(false); }
    private static void Login(bool _Login)
    {
        try
        {
            Global mGlobal = new Global();// Global data     
            CommFuncs mCommFuncs = new CommFuncs();// Common Function library						    
            String strSql = "";
            if (_Login)
            {
                strSql = "insert into loginlog(llog_user,llog_userid,llog_logindttm,llog_active,llog_remarks) values(";
                strSql += "'" + mGlobal.CurrentUser + "',";
                strSql += "'" + mGlobal.CurrentUserID + "',";
                strSql += mCommFuncs.FormatDBServDateWithTime(mGlobal.ServerDate + " " + mGlobal.ServerTime) + ",";
                strSql += "'Y',''";
                strSql += ")";
                mGlobal.LocalDBConLog.BeginTrans();
                mGlobal.LocalDBConLog.ExecuteNonQuery_OnTran(strSql);
                try
                {
                    strSql = "SELECT @@identity";
                    mGlobal.CurrentLoginID = mCommFuncs.ConvertToInt64(mGlobal.LocalDBConLog.ExecuteQuery_OnTran(strSql).Rows[0][0]);                    
                }
                catch { }
                mGlobal.LocalDBConLog.CommitTrans();
            }
            else
            {
                strSql = "update loginlog set llog_logoutdttm=" + mCommFuncs.FormatDBServDateWithTime(mGlobal.ServerDate + " " + mGlobal.ServerTime) + ",llog_active='N' ";
                strSql += " where llog_id=" + mGlobal.CurrentLoginID;
                mGlobal.LocalDBConLog.ExecuteNonQuery(strSql);
            }
        }
        catch { }
    }
    #endregion
    #region Master Log
    public static void MasterLogAsync(String _Action, String _Module, String _PrimaryKey, String _Remarks, DataTable _dataTable, bool UseDBConnBatchTrans)
    {
        try
        {
            MasterLogDelegate worker = new MasterLogDelegate(MasterLog);
            AsyncOperation async = AsyncOperationManager.CreateOperation(null);            
            worker.BeginInvoke(_Action, _Module, _PrimaryKey, _Remarks, _dataTable, UseDBConnBatchTrans, null, null);
        }
        catch { }
    }
    private delegate void MasterLogDelegate(String _Action, String _Module, String _PrimaryKey, String _Remarks, DataTable _dataTable, bool UseDBConnBatchTrans);
    public static void MasterLog(String _Action,String _Module,String _PrimaryKey,String _Remarks, DataTable _dataTable,bool UseDBConnBatchTrans)
    {
        try
        {
            Global mGlobal = new Global();// Global data     
            CommFuncs mCommFuncs = new CommFuncs();// Common Function library						    
            String strSql = "";
            for (int intRow = 0; intRow < _dataTable.Rows.Count; intRow++)
            {
                strSql = @"insert into masterlog(
                        mlog_dttm
                       ,mlog_loginid
                       ,mlog_userid
                       ,mlog_user
                       ,mlog_action
                       ,mlog_module
                       ,mlog_table
                       ,mlog_pkfield
                       ,mlog_pkvalue
                       ,mlog_data
                       ,mlog_remarks)
                    VALUES (";
                strSql += "" + mCommFuncs.FormatDBServDateWithTime(mGlobal.ServerDate_OnTran(UseDBConnBatchTrans) + " " + mGlobal.ServerTime_OnTran(UseDBConnBatchTrans)) + ",";
                strSql += mGlobal.CurrentLoginID + ",";
                strSql += "'" + mGlobal.CurrentUserID + "',";
                strSql += "'" + mGlobal.CurrentUser + "',";
                strSql += "'" + _Action + "',";
                strSql += "'" + _Module + "',";
                strSql += "'" + _dataTable.TableName + "',";
                strSql += "'" + _PrimaryKey + "',";
                strSql += "'" + _dataTable.Rows[intRow][_PrimaryKey].ToString() + "',";
                strSql += "'" + mCommFuncs.DataRowToText(_dataTable.Rows[intRow], false) + "',";
                strSql += "'" + _Remarks + "')";
                try
                {
                    if (UseDBConnBatchTrans)
                        mGlobal.LocalDBConLog.ExecuteNonQuery_OnTran(strSql);
                    else
                        mGlobal.LocalDBConLog.ExecuteNonQuery(strSql);
                }
                catch(Exception ex) { }
            }
        }
        catch(Exception ex) { }
    }

    public static void MasterLogIndividual(String _TableName, String _Action, DataTable _dataTable)
    {
        try
        {
            Global mGlobal = new Global();// Global data                 
            String strSql = "select * from " + _TableName + " where 1=2";
            DataTable dtSave = mGlobal.LocalDBConLog.ExecuteQuery(strSql);
            DataRow drNewRow = null;

            for (int intRow = 0; intRow < _dataTable.Rows.Count; intRow++)
            {
                drNewRow = dtSave.NewRow();
                drNewRow["piml_dttm"] = mGlobal.ServerDate + " " + mGlobal.ServerTime;
                drNewRow["piml_user"] = mGlobal.CurrentUser;
                drNewRow["piml_userid"] = mGlobal.CurrentUserID;
                drNewRow["piml_action"] = _Action;
                for (int intCol = 0; intCol < _dataTable.Columns.Count; intCol++)
                {
                    drNewRow[_dataTable.Columns[intCol].ColumnName] = _dataTable.Rows[intRow][intCol];
                }
                dtSave.Rows.Add(drNewRow);
            }
            dtSave.AcceptChanges();
            dtSave.TableName = _TableName;
            mGlobal.LocalDBConLog.InsertDataTableWithSql(dtSave, "piml_id", "");
            mGlobal.LocalDBConLog.UpdateDataTable(strSql, dtSave);
        }
        catch(Exception ex) { }
    }
    #endregion
}