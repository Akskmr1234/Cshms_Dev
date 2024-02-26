using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Windows.Forms;

class GridDesgnr
{
    Global mGlobal = new Global();// Global data     
    CommFuncs mCommFuncs = new CommFuncs();// Common Function library

    String mstrDispSql = "", mstrUpdtSql = "";

    DataGridView dvgMain = new DataGridView();
    public GridDesgnr(String _DisplaySql,String _UpdateSql)
    {
        mstrDispSql = _DisplaySql;
        mstrUpdtSql = _UpdateSql;
    }

    public void FillData(ref DataGridView _dvgData)
    {
        dvgMain = _dvgData;
        try { dvgMain.Rows.Clear(); }
        catch { }
        try { dvgMain.DataSource = null; dvgMain.Refresh(); }
        catch { }

        try
        {
            DataTable dtTbl = mGlobal.LocalDBCon.ExecuteQuery(mstrDispSql);
            dvgMain.DataSource = dtTbl;
        }
        catch (Exception ex) { MessageBox.Show(ex.Message); }
    }
    
    public void SaveData(ref DataGridView _dvgData,bool _isDiffColName)
    {        
        // _isDiffColName - If different column names used for display and update sql(aliasis for display)
        try
        {
            DataTable dtData = (DataTable)_dvgData.DataSource;
            DataTable dtUpdt = mGlobal.LocalDBCon.ExecuteQuery(mstrUpdtSql);
            if (_isDiffColName)
            {
                for (int intRow = 0; intRow < dtUpdt.Rows.Count; intRow++)
                    for (int intCol = 0; intCol < dtUpdt.Columns.Count; intCol++)
                        dtUpdt.Rows[intRow][intCol] = dtData.Rows[intRow][intCol];                
            }
            else
                dtUpdt = dtData;
            if (mGlobal.LocalDBCon.UpdateDataTable(mstrUpdtSql, dtUpdt) > 0)
            {

                AuditLog.MasterLog("Edit", "GENSET", "gs_id", "", dtUpdt, false); 
            }
        }
        catch (Exception ex) { MessageBox.Show(ex.Message); }
    }

    private DataTable FlipDataTable(DataTable my_DataTbl)
    {
        DataTable table = new DataTable();
        for (int i = 0; i <= my_DataTbl.Rows.Count; i++)
        {
            table.Columns.Add();
        }
        table.Columns[0].ReadOnly = true;
        DataRow r = null;
        for (int k = 0; k < my_DataTbl.Columns.Count; k++)
        {
            r = table.NewRow();
            r[0] = my_DataTbl.Columns[k].ToString();
            for (int j = 1; j <= my_DataTbl.Rows.Count; j++)
                r[j] = my_DataTbl.Rows[j - 1][k];
            table.Rows.Add(r);
        }        
        return table;
    }    
}

