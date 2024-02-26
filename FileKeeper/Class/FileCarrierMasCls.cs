using System;
using System.Data;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using CsHms.Common;
class FileCarrierMasCls
{
    CommFuncs mclsCFunc = new CommFuncs();
    Global mGlobal = new Global();
    String mstrCode;
    String mstrName;
    String mstrRemarks;
    String mstrActive;
    String SQL; 
    const String FORM_ID = "FILEMAS";// Unique id for this form
    const String TABLE_NAME = "filecarriermas";// Main Table name
    const String PRIMARY_KEY = "fc_code";// Primary key of the table
    public void ClearAll()
    {
        mstrCode="";
        mstrName="";
        mstrRemarks="";
        mstrActive="";
        SQL="";
    }
    public String Code
    {
     set { mstrCode = value; }
     get { return mstrCode; }
    }
    public String Name
    {
     set { mstrName = value; }
     get { return mstrName; }
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
            SQL ="insert into " + TABLE_NAME + " (fc_code,fc_name,fc_remarks,fc_active) values ('"+this.Code+"','"+this.Name+"','"+this.Remarks+"','"+this.Active+"')";
            if (mGlobal.LocalDBCon.ExecuteNonQuery(SQL) > 0)
            return true;
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
            SQL ="update   " + TABLE_NAME + " set fc_code='"+this.Code+"',fc_name='"+this.Name+"',fc_remarks='"+this.Remarks+"',fc_active='"+this.Active+"' where fc_code='"+this.Code+"'";
            if (mGlobal.LocalDBCon.ExecuteNonQuery(SQL) > 0)
            return true;
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
            SQL ="delete  from    " + TABLE_NAME +  "  where " +PRIMARY_KEY + " ='"+this.Code+"'";
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
            SQL=" select fc_code,fc_name,fc_remarks,fc_active from " +TABLE_NAME +" where  fc_code='"+this.Code+"'";
            DataTable dtData = mGlobal.LocalDBCon.ExecuteQuery(SQL);
            if (mclsCFunc.ConvertToInt(dtData.Rows.Count) > 0)
            {
                this.Code=mclsCFunc.ConvertToString(dtData.Rows[0]["fc_code"]);
                this.Name=mclsCFunc.ConvertToString(dtData.Rows[0]["fc_name"]);
                this.Remarks=mclsCFunc.ConvertToString(dtData.Rows[0]["fc_remarks"]);
                this.Active=mclsCFunc.ConvertToString(dtData.Rows[0]["fc_active"]);
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
    public DataTable getDataList(string strConditionSql)
    {
        try
        {
            SQL=" select fc_code,fc_name,fc_remarks,fc_active from "+TABLE_NAME  ; 
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
    public string searchCarrier(string strDesc)
    {
        try
        {
            // Search Settings 
            Search Searchfrm = new Search();
            Searchfrm.DefSearchFldIndex = 1;
            Searchfrm.DefSearchText = strDesc;
            Searchfrm.Query = "select " + PRIMARY_KEY + ",fc_name from " + TABLE_NAME;
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
}
