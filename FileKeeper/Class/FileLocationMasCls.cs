using System;
using System.Data;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using CsHms.Common;
class FileLocationMasCls
{
    CommFuncs mclsCFunc = new CommFuncs();
    Global mGlobal = new Global();
    String mstrCode;
    String mstrDesc;
    String mstrInhouse;
    String mstrRemarks;
    String mstrActive;
    String SQL;
    const String FORM_ID = "FLOCMAS";// Unique id for this form
    const String TABLE_NAME = "filelocationmas";// Main Table name
    const String PRIMARY_KEY = "fl_code";// Primary key of the table
    public void ClearAll()
    {
        mstrCode="";
        mstrDesc="";
        mstrInhouse="";
        mstrRemarks="";
        mstrActive="";
        SQL="";
    }
    public String Code
    {
     set { mstrCode = value; }
     get { return mstrCode; }
    }
    public String Desc
    {
     set { mstrDesc = value; }
     get { return mstrDesc; }
    }
    public String Inhouse
    {
     set { mstrInhouse = value; }
     get { return mstrInhouse; }
    }
    public String Remarks
    {
     set { mstrRemarks = value; }
     get { return mstrRemarks; }
    }
    public String Active
    {
     set { mstrActive = value; }
     get { return mstrActive; }
    }

     public bool insertData()
     {
         try
         {
            SQL ="insert into " +TABLE_NAME +" ( " +PRIMARY_KEY +" ,fl_desc,fl_inhouse,fl_remarks,fl_active) values ('"+this.Code+"','"+this.Desc+"','"+this.Inhouse+"','"+this.Remarks+"','"+this.Active+"')";
            if (mGlobal.LocalDBCon.ExecuteNonQuery(SQL) > 0)
            {
                DataTable dtData = getDataList("  " +PRIMARY_KEY +"  ='" + this.Code + "'");
                AuditLog.MasterLog("Add", FORM_ID, PRIMARY_KEY, "", dtData, false);
                return true;
            }
        }
        catch (Exception ex)
        {
          MessageBox.Show(ex.Message.ToString());
        }
        return false ;
   }
    public bool updateData()
    {
        try
        {
            SQL ="update   " +TABLE_NAME +"  set  " +PRIMARY_KEY +" ='"+this.Code+"',fl_desc='"+this.Desc+"',fl_inhouse='"+this.Inhouse+"',fl_remarks='"+this.Remarks+"',fl_active='"+this.Active+"' where  " +PRIMARY_KEY +" ='"+this.Code+"'";
            if (mGlobal.LocalDBCon.ExecuteNonQuery(SQL) > 0)
            {
                DataTable dtData = getDataList( PRIMARY_KEY +"  ='" + this.Code + "'");
                AuditLog.MasterLog("Edit", FORM_ID, PRIMARY_KEY, "", dtData, false);
                return true;
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message.ToString());
        }
     return false;
   }
    public bool deleteData()
    {
        try
        {
            SQL ="delete  from   " +TABLE_NAME +"   where  " +PRIMARY_KEY +" ='"+this.Code+"'";
            if (mGlobal.LocalDBCon.ExecuteNonQuery(SQL) > 0)
            return true;
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message.ToString());
        }
        return false;
    }
public bool getData()
{
    try
    {
        SQL=" select  " +PRIMARY_KEY +" ,fl_desc,fl_inhouse,fl_remarks,fl_active from " +TABLE_NAME +"  where " +PRIMARY_KEY +" ='"+this.Code+"'";
        DataTable dtData = mGlobal.LocalDBCon.ExecuteQuery(SQL);
        if (mclsCFunc.ConvertToInt(dtData.Rows.Count) > 0)
        {
        this.Code=mclsCFunc.ConvertToString(dtData.Rows[0][PRIMARY_KEY]);
        this.Desc=mclsCFunc.ConvertToString(dtData.Rows[0]["fl_desc"]);
        this.Inhouse=mclsCFunc.ConvertToString(dtData.Rows[0]["fl_inhouse"]);
        this.Remarks=mclsCFunc.ConvertToString(dtData.Rows[0]["fl_remarks"]);
        this.Active=mclsCFunc.ConvertToString(dtData.Rows[0]["fl_active"]);
        return true;
        }
    }
    catch (Exception ex)
    {
        MessageBox.Show(ex.Message.ToString());
    }
    return false;
}
    public DataTable getDataList()
    {
        try
        {
            DataTable dtData =getDataList("");
            if (mclsCFunc.ConvertToInt(dtData.Rows.Count) > 0)
            return dtData;
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message.ToString());
        }
        return null;
    }
    public string searchLocation(string strDesc)
    {
        try
        {
            // Search Settings 
            Search Searchfrm = new Search();
            Searchfrm.DefSearchFldIndex = 1;
            Searchfrm.DefSearchText = strDesc;
            Searchfrm.Query = "select " + PRIMARY_KEY + ",fl_desc from " + TABLE_NAME;
            Searchfrm.FilterCond = "";
            Searchfrm.ReturnFldIndex = 0;
            Searchfrm.ColumnHeader = "Code|Name";
            Searchfrm.ColumnWidth = "130|330";
            Searchfrm.ShowDialog();
            if (!Searchfrm.Cancelled) // whether not clicked on cancel button in search screen
                return Searchfrm.ReturnValue;
           
            // Search value returns
        }
        catch (Exception ex) { MessageBox.Show(ex.Message); }
        return "";

    }
    public DataTable getDataList(string strConditionSql)
    {
        try
        {
            SQL=" select  " +PRIMARY_KEY +" ,fl_desc,fl_inhouse,fl_remarks,fl_active from " +TABLE_NAME +" " ; 
            if (strConditionSql.Trim().Length > 0)
            SQL+=  " where " + strConditionSql;
            DataTable dtData = mGlobal.LocalDBCon.ExecuteQuery(SQL);
            if (mclsCFunc.ConvertToInt(dtData.Rows.Count) > 0)
            return dtData;
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message.ToString());
        }
        return null;
    }

}
