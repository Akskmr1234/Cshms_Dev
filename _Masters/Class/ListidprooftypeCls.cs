using System;
using System.Data;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace CsHms._Masters.Class
{	
public class ListidprooftypeCls
{ 
    CommFuncs mclsCFunc = new CommFuncs();
    Global mGlobal = new Global();
    String mstrCode;
    String mstrDesc;
    Int32 mintSlno;
    String mstrActive;
    String mstrRemarks;
    String SQL; 
    public void ClearAll()
    {
        mstrCode="";
        mstrDesc="";
        mintSlno=0;
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
    public Int32 Slno
    {
     set { mintSlno = value; }
     get { return mintSlno; }
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
//void setDataToProperties
//{
//Code=mclsCFunc.ConvertToString( txtCode.Text)
//Desc=mclsCFunc.ConvertToString( txtDesc.Text)
//Slno=mclsCFunc.ConvertToInt( txtSlno.Text)
//Active=mclsCFunc.ConvertToString( txtActive.Text)
//Remarks=mclsCFunc.ConvertToString( txtRemarks.Text)
//}
     public bool insertData()
    {
        try
        {
            SQL ="insert into listidprooftype(lidt_code,lidt_desc,lidt_slno,lidt_active,lidt_remarks) values ('"+this.Code+"','"+this.Desc+"',"+this.Slno+",'"+this.Active+"','"+this.Remarks+"')";
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
            SQL ="update   listidprooftype set lidt_code='"+this.Code+"',lidt_desc='"+this.Desc+"',lidt_slno="+this.Slno+",lidt_active='"+this.Active+"',lidt_remarks='"+this.Remarks+"' where lidt_code='"+this.Code+"'";
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
            SQL ="delete  from   listidprooftype  where lidt_code='"+this.Code+"'";
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
                SQL=" select lidt_code,lidt_desc,lidt_slno,lidt_active,lidt_remarks from listidprooftype where  lidt_code='"+this.Code+"'";
                DataTable dtData = mGlobal.LocalDBCon.ExecuteQuery(SQL);
                 if (mclsCFunc.ConvertToInt(dtData.Rows.Count) > 0)
                 {
                    this.Code=mclsCFunc.ConvertToString(dtData.Rows[0]["lidt_code"]);
                    this.Desc=mclsCFunc.ConvertToString(dtData.Rows[0]["lidt_desc"]);
                    this.Slno=mclsCFunc.ConvertToInt(dtData.Rows[0]["lidt_slno"]);
                    this.Active=mclsCFunc.ConvertToString(dtData.Rows[0]["lidt_active"]);
                    this.Remarks=mclsCFunc.ConvertToString(dtData.Rows[0]["lidt_remarks"]);
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
        SQL=" select lidt_code,lidt_desc,lidt_slno,lidt_active,lidt_remarks from listidprooftype" ; 
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
//void fillDataToControl
//{
//txtCode.Text= this.Code
//txtDesc.Text= this.Desc
//txtSlno.Text= this.Slno
//txtActive.Text= this.Active
//txtRemarks.Text= this.Remarks
//}
 //public string NewCode()
 //{ 
 // return mclsCFunc.NewCode(mstrTableNm, mstrPrimaryKey, '');
 //} 
}

}