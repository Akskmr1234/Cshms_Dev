using System;
using System.Data;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using CsHms.Common;
class FileMasCls
{
CommFuncs mclsCFunc = new CommFuncs();
Global mGlobal = new Global();
String mstrCode;
String mstrDesc;
String mstrCategoryptr;
String mstrLocationptr;
String mstrType;
String mstrFileRack;
String mstrTransferable;
String mstrTransfertooutside;
String mstrAuthdperson;
String mstrActive;
String mstrRemarks;
String SQL;
const String FORM_ID = "FILEMAS";// Unique id for this form
const String TABLE_NAME = "filemas";// Main Table name
const String PRIMARY_KEY = "fm_code";// Primary key of the table
public void ClearAll()
{
mstrCode="";
mstrDesc="";
mstrCategoryptr="";
mstrLocationptr="";
mstrType="";
mstrFileRack="";
mstrTransferable="";
mstrTransfertooutside="";
mstrAuthdperson="";
mstrActive="";
mstrRemarks="";
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
public String CategoryPtr
{
 set { mstrCategoryptr = value; }
 get { return mstrCategoryptr; }
}
public String LocationPtr
{
 set { mstrLocationptr = value; }
 get { return mstrLocationptr; }
}
public String TypePtr
{
 set { mstrType = value; }
 get { return mstrType; }
}
  public String FileRack
{
 set { mstrFileRack = value; }
 get { return mstrFileRack; }
}
public String Transferable
{
 set { mstrTransferable = value; }
 get { return mstrTransferable; }
}
public String Transfertooutside
{
 set { mstrTransfertooutside = value; }
 get { return mstrTransfertooutside; }
}
public String Authdperson
{
 set { mstrAuthdperson = value; }
 get { return mstrAuthdperson; }
}
public String Active
{
 set { mstrActive = value; }
 get { return mstrActive; }
}
public String Remarks
{
 set { mstrRemarks = value; }
 get { return mstrRemarks; }
}

    public string searchFile(string strDesc,bool useView)
    {
        try
        {
            // Search Settings 
            Search Searchfrm = new Search();
            Searchfrm.DefSearchFldIndex = 1;
            Searchfrm.DefSearchText = strDesc;
            if (useView == false)
                Searchfrm.Query = "select " + PRIMARY_KEY + ",fm_desc,fm_filerack from " + TABLE_NAME;
            else
                Searchfrm.Query = "select FileCode,FileNm,FileRack from viewFileMas";

            Searchfrm.FilterCond = "";
            Searchfrm.ReturnFldIndex = 0;
            Searchfrm.ColumnHeader = "Code|Name|Rack";
            Searchfrm.ColumnWidth = "130|330|100";
            Searchfrm.ShowDialog();
            if (!Searchfrm.Cancelled) // whether not clicked on cancel button in search screen
                return Searchfrm.ReturnValue;

            // Search value returns
        }
        catch (Exception ex) { MessageBox.Show(ex.Message); }
        return "";

    }
    
    public void fillLocation(UserControls.SimpleComboSearch scsList)
    {
        try
        {
            DataTable dtData = mGlobal.LocalDBCon.ExecuteQuery("select fl_code,fl_desc from   filelocationmas" +
               " where (fl_active<>'N' or fl_active is null)order by fl_desc");
            scsList.DataTabl = dtData;
            scsList.LoadData();
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message.ToString());
        }
    }
    public void fillGeneralList(UserControls.SimpleComboSearch scsList,string strMode)
    {
        // Auto Complete 
        DataTable dtData = mGlobal.LocalDBCon.ExecuteQuery("select genl_code,genl_desc from generallist " +
            " where (genl_active<>'N' or genl_active is null) and genl_mode='" + strMode + "' order by genl_desc");
        scsList.DataTabl = dtData;
        scsList.LoadData();
    }
    public bool checkFileNameAlreadyExist(string strCode,string strDesc)
    {
        try
        {
            DataTable dtData = mGlobal.LocalDBCon.ExecuteQuery("select * from filemas where  " + PRIMARY_KEY +" <>'" + strCode
                + "' and fm_desc='" + strDesc + "'");
            if (dtData.Rows.Count > 0)
            {
                MessageBox.Show("File name already use in File Code : " + dtData.Rows[0][0].ToString());
                return true;
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message.ToString());
        }
        return false;
          
         
    }

 public bool insertData()
{
 try
 {
SQL ="insert into filemas( " + PRIMARY_KEY +" ,fm_desc,fm_categoryptr,fm_locationptr,fm_typeptr,fm_filerack,fm_transferable,fm_transfertooutside,fm_authdperson,fm_active,fm_remarks) values ('"+this.Code+"','"+this.Desc+"','"+this.CategoryPtr+"','"+this.LocationPtr+"','"+this.TypePtr+"','"+this.FileRack+"','"+this.Transferable+"','"+this.Transfertooutside+"','"+this.Authdperson+"','"+this.Active+"','"+this.Remarks+"')";
if (mGlobal.LocalDBCon.ExecuteNonQuery(SQL) > 0)
{
    DataTable dtData = getDataList("  " + PRIMARY_KEY +"  ='" + this.Code + "'");
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
SQL ="update   filemas set  " + PRIMARY_KEY +" ='"+this.Code+"',fm_desc='"+this.Desc+"',fm_categoryptr='"+this.CategoryPtr+"',fm_locationptr='"+this.LocationPtr+"',fm_typeptr='"+this.TypePtr+"',fm_filerack='"+this.FileRack+"',fm_transferable='"+this.Transferable+"',fm_transfertooutside='"+this.Transfertooutside+"',fm_authdperson='"+this.Authdperson+"',fm_active='"+this.Active+"',fm_remarks='"+this.Remarks+"' where  " + PRIMARY_KEY +" ='"+this.Code+"'";
if (mGlobal.LocalDBCon.ExecuteNonQuery(SQL) > 0)
{
    DataTable dtData = getDataList("  " + PRIMARY_KEY +"  ='" + this.Code + "'");
    AuditLog.MasterLog("Edit", FORM_ID, PRIMARY_KEY, "", dtData, false);
    return true;
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
SQL ="delete  from   filemas  where  " + PRIMARY_KEY +" ='"+this.Code+"'";
if (mGlobal.LocalDBCon.ExecuteNonQuery(SQL) > 0)
return true;
}
catch (Exception ex)
{
MessageBox.Show(ex.Message.ToString());
}
 return false;
}
    public bool getData(bool useView)
    {
        try
        {
            if (useView == false)
            {
                SQL = @" select  fm_code as 'FileCode', fm_desc as 'FileNm',fm_categoryptr as 'FileCatCode',fm_locationptr as 'FileLocCode',
                fm_typeptr as 'FileTypeCode',fm_filerack as 'FileRack',fm_transferable as 'Transferable',
                fm_transfertooutside as 'TransferOutSide',fm_authdperson as 'AuthPerson',fm_active as 'Active',
                fm_remarks as 'Remarks'  from filemas where   " + PRIMARY_KEY + " ='" + this.Code + "'";
            }
            else
            {
                SQL = " select  * from viewFileMas where filecode ='" + this.Code + "'";
            }
            DataTable dtData = mGlobal.LocalDBCon.ExecuteQuery(SQL);
            if (mclsCFunc.ConvertToInt(dtData.Rows.Count) > 0)
            {
                this.Code = mclsCFunc.ConvertToString(dtData.Rows[0]["FileCode"]);
                this.Desc = mclsCFunc.ConvertToString(dtData.Rows[0]["FileNm"]);
                this.CategoryPtr = mclsCFunc.ConvertToString(dtData.Rows[0]["FileCatCode"]);
                this.LocationPtr = mclsCFunc.ConvertToString(dtData.Rows[0]["FileLocCode"]);
                this.TypePtr = mclsCFunc.ConvertToString(dtData.Rows[0]["FileTypeCode"]);
                this.FileRack = mclsCFunc.ConvertToString(dtData.Rows[0]["FileRack"]);
                this.Transferable = mclsCFunc.ConvertToString(dtData.Rows[0]["Transferable"]);
                this.Transfertooutside = mclsCFunc.ConvertToString(dtData.Rows[0]["TransferOutSide"]);
                this.Authdperson = mclsCFunc.ConvertToString(dtData.Rows[0]["AuthPerson"]);
                this.Active = mclsCFunc.ConvertToString(dtData.Rows[0]["Active"]);
                this.Remarks = mclsCFunc.ConvertToString(dtData.Rows[0]["Remarks"]);
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
SQL=" select  " + PRIMARY_KEY +" ,fm_desc,fm_categoryptr,fm_locationptr,fm_typeptr,fm_filerack,fm_transferable,fm_transfertooutside,fm_authdperson,fm_active,fm_remarks from filemas" ; 
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
