using System;
using System.Data;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
namespace CsHms._Masters.Class
{
  public class gtgrouptreatlistCls
  {
    CommFuncs mclsCFunc = new CommFuncs();
    Global mGlobal = new Global();
    Int64 mintId;
    String mstrType;
    String mstrRefno;
    DateTime mdateDt;
    DateTime mdateDttm;
    String mstrTitle;
    String mstrName;
    String mstrGender;
    DateTime mdateDob;
    Decimal mdecAge;
    String mstrAgetype;
    String mstrRel;
    String mstrOccu;
    String mstrIdprooftypeptr;
    String mstrIdproofNo;
    String mstrPhone;
    String mstrMobile;
    String mstrOpno;
    DateTime mdateOpupdt;
    String mstrIsheader;
    String mstrActive;
    Int32 mintSlno;
    String mstrUser;
    Int32 mintUserid;
    String mstrRemarks;
    String SQL; 
    public void ClearAll()
    {
        mintId=0;
        mstrType="";
        mstrRefno="";
        mdateDt=DateTime.Now;
        mdateDttm=DateTime.Now;
        mstrTitle="";
        mstrName="";
        mstrGender="";
        mdateDob=DateTime.Now;
        mdecAge=0;
        mstrAgetype="";
        mstrRel="";
        mstrOccu="";
        mstrIdprooftypeptr="";
        mstrIdproofNo="";
        mstrPhone="";
        mstrMobile="";
        mstrOpno="";
        mdateOpupdt=DateTime.Now;
        mstrIsheader="";
        mstrActive="";
        mintSlno=0;
        mstrUser="";
        mintUserid=0;
        mstrRemarks="";
        SQL="";
    }
    public Int64 Id
    {
     set { mintId = value; }
     get { return mintId; }
    }
    public String Type
    {
     set { mstrType = value; }
     get { return mstrType; }
    }
    public String Refno
    {
     set { mstrRefno = value; }
     get { return mstrRefno; }
    }
    public DateTime Dt
    {
     set { mdateDt = value; }
     get { return mdateDt; }
    }
    public DateTime Dttm
    {
     set { mdateDttm = value; }
     get { return mdateDttm; }
    }
    public String Title
    {
     set { mstrTitle = value; }
     get { return mstrTitle; }
    }
    public String Name
    {
     set { mstrName = value; }
     get { return mstrName; }
    }
    public String Gender
    {
     set { mstrGender = value; }
     get { return mstrGender; }
    }
    public DateTime Dob
    {
     set { mdateDob = value; }
     get { return mdateDob; }
    }
    public Decimal Age
    {
     set { mdecAge = value; }
     get { return mdecAge; }
    }
    public String Agetype
    {
     set { mstrAgetype = value; }
     get { return mstrAgetype; }
    }
    public String Rel
    {
     set { mstrRel = value; }
     get { return mstrRel; }
    }
    public String Occu
    {
     set { mstrOccu = value; }
     get { return mstrOccu; }
    }
    public String Idprooftypeptr
    {
     set { mstrIdprooftypeptr = value; }
     get { return mstrIdprooftypeptr; }
    }
    public String IdproofNo
    {
     set { mstrIdproofNo = value; }
     get { return mstrIdproofNo; }
    }
    public String Phone
    {
     set { mstrPhone = value; }
     get { return mstrPhone; }
    }
    public String Mobile
    {
     set { mstrMobile = value; }
     get { return mstrMobile; }
    }
    public String Opno
    {
     set { mstrOpno = value; }
     get { return mstrOpno; }
    }
    public DateTime Opupdt
    {
     set { mdateOpupdt = value; }
     get { return mdateOpupdt; }
    }
    public String Isheader
    {
     set { mstrIsheader = value; }
     get { return mstrIsheader; }
    }
    public String Active
    {
     set { mstrActive = value; }
     get { return mstrActive; }
    }
    public Int32 Slno
    {
     set { mintSlno = value; }
     get { return mintSlno; }
    }
    public String User
    {
     set { mstrUser = value; }
     get { return mstrUser; }
    }
    public Int32 Userid
    {
     set { mintUserid = value; }
     get { return mintUserid; }
    }
    public String Remarks
    {
     set { mstrRemarks = value; }
     get { return mstrRemarks; }
    }
    //void setDataToProperties
    //{
    //Id=mclsCFunc.ConvertToInt64( txtId.Text)
    //Type=mclsCFunc.ConvertToString( txtType.Text)
    //Refno=mclsCFunc.ConvertToString( txtRefno.Text)
    //Dt=mclsCFunc.FormatDate( txtDt.Text,"dd/MM/yyyy")
    //Dttm=mclsCFunc.FormatDate( txtDttm.Text,"dd/MM/yyyy")
    //Title=mclsCFunc.ConvertToString( txtTitle.Text)
    //Name=mclsCFunc.ConvertToString( txtName.Text)
    //Gender=mclsCFunc.ConvertToString( txtGender.Text)
    //Dob=mclsCFunc.FormatDate( txtDob.Text,"dd/MM/yyyy")
    //Age=mclsCFunc.ConvertToNumber_Dec( txtAge.Text)
    //Agetype=mclsCFunc.ConvertToString( txtAgetype.Text)
    //Rel=mclsCFunc.ConvertToString( txtRel.Text)
    //Occu=mclsCFunc.ConvertToString( txtOccu.Text)
    //Idprooftypeptr=mclsCFunc.ConvertToString( txtIdprooftypeptr.Text)
    //IdproofNo=mclsCFunc.ConvertToString( txtIdproofNo.Text)
    //Phone=mclsCFunc.ConvertToString( txtPhone.Text)
    //Mobile=mclsCFunc.ConvertToString( txtMobile.Text)
    //Opno=mclsCFunc.ConvertToString( txtOpno.Text)
    //Opupdt=mclsCFunc.FormatDate( txtOpupdt.Text,"dd/MM/yyyy")
    //Isheader=mclsCFunc.ConvertToString( txtIsheader.Text)
    //Active=mclsCFunc.ConvertToString( txtActive.Text)
    //Slno=mclsCFunc.ConvertToInt( txtSlno.Text)
    //User=mclsCFunc.ConvertToString( txtUser.Text)
    //Userid=mclsCFunc.ConvertToInt( txtUserid.Text)
    //Remarks=mclsCFunc.ConvertToString( txtRemarks.Text)
    //}
public bool insertData()
{
 try
 {
    SQL ="insert into gtgrouptreatlist(gtl_id,gtl_type,gtl_refno,gtl_dt,gtl_dttm,gtl_title,gtl_name,gtl_gender,gtl_dob,gtl_age,gtl_agetype,gtl_rel,gtl_occu,gtl_idprooftypeptr,gtl_idproofNo,gtl_phone,gtl_mobile,gtl_opno,gtl_opupdt,gtl_isheader,gtl_active,gtl_slno,gtl_user,gtl_userid,gtl_remarks) values ("+this.Id+",'"+this.Type+"','"+this.Refno+"',"+mclsCFunc.FormatDBDate(this.Dt.ToString())+","+mclsCFunc.FormatDBDate(this.Dttm.ToString())+",'"+this.Title+"','"+this.Name+"','"+this.Gender+"',"+mclsCFunc.FormatDBDate(this.Dob.ToString())+","+this.Age+",'"+this.Agetype+"','"+this.Rel+"','"+this.Occu+"','"+this.Idprooftypeptr+"','"+this.IdproofNo+"','"+this.Phone+"','"+this.Mobile+"','"+this.Opno+"',"+mclsCFunc.FormatDBDate(this.Opupdt.ToString())+",'"+this.Isheader+"','"+this.Active+"',"+this.Slno+",'"+this.User+"',"+this.Userid+",'"+this.Remarks+"')";
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
        SQL ="update   gtgrouptreatlist set gtl_id="+this.Id+",gtl_type='"+this.Type+"',gtl_refno='"+this.Refno+"',gtl_dt="+mclsCFunc.FormatDBDate(this.Dt.ToString())+",gtl_dttm="+mclsCFunc.FormatDBDate(this.Dttm.ToString())+",gtl_title='"+this.Title+"',gtl_name='"+this.Name+"',gtl_gender='"+this.Gender+"',gtl_dob="+mclsCFunc.FormatDBDate(this.Dob.ToString())+",gtl_age="+this.Age+",gtl_agetype='"+this.Agetype+"',gtl_rel='"+this.Rel+"',gtl_occu='"+this.Occu+"',gtl_idprooftypeptr='"+this.Idprooftypeptr+"',gtl_idproofNo='"+this.IdproofNo+"',gtl_phone='"+this.Phone+"',gtl_mobile='"+this.Mobile+"',gtl_opno='"+this.Opno+"',gtl_opupdt="+mclsCFunc.FormatDBDate(this.Opupdt.ToString())+",gtl_isheader='"+this.Isheader+"',gtl_active='"+this.Active+"',gtl_slno="+this.Slno+",gtl_user='"+this.User+"',gtl_userid="+this.Userid+",gtl_remarks='"+this.Remarks+"' where gtl_id="+this.Id+"";
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
        SQL ="delete  from   gtgrouptreatlist  where gtl_id="+this.Id+"";
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
        SQL=" select gtl_id,gtl_type,gtl_refno,gtl_dt,gtl_dttm,gtl_title,gtl_name,gtl_gender,gtl_dob,gtl_age,gtl_agetype,gtl_rel,gtl_occu,gtl_idprooftypeptr,gtl_idproofNo,gtl_phone,gtl_mobile,gtl_opno,gtl_opupdt,gtl_isheader,gtl_active,gtl_slno,gtl_user,gtl_userid,gtl_remarks from gtgrouptreatlist where  gtl_id="+this.Id+"";
        DataTable dtData = mGlobal.LocalDBCon.ExecuteQuery(SQL);
        if (mclsCFunc.ConvertToInt(dtData.Rows.Count) > 0)
         {
            this.Id=mclsCFunc.ConvertToInt64(dtData.Rows[0]["gtl_id"]);
            this.Type=mclsCFunc.ConvertToString(dtData.Rows[0]["gtl_type"]);
            this.Refno=mclsCFunc.ConvertToString(dtData.Rows[0]["gtl_refno"]);
            this.Dt=mclsCFunc.FormatDate(dtData.Rows[0]["gtl_dt"].ToString(),"dd/MM/yyyy");
            this.Dttm=mclsCFunc.FormatDate(dtData.Rows[0]["gtl_dttm"].ToString(),"dd/MM/yyyy");
            this.Title=mclsCFunc.ConvertToString(dtData.Rows[0]["gtl_title"]);
            this.Name=mclsCFunc.ConvertToString(dtData.Rows[0]["gtl_name"]);
            this.Gender=mclsCFunc.ConvertToString(dtData.Rows[0]["gtl_gender"]);
            this.Dob=mclsCFunc.FormatDate(dtData.Rows[0]["gtl_dob"].ToString(),"dd/MM/yyyy");
            this.Age=mclsCFunc.ConvertToNumber_Dec(dtData.Rows[0]["gtl_age"]);
            this.Agetype=mclsCFunc.ConvertToString(dtData.Rows[0]["gtl_agetype"]);
            this.Rel=mclsCFunc.ConvertToString(dtData.Rows[0]["gtl_rel"]);
            this.Occu=mclsCFunc.ConvertToString(dtData.Rows[0]["gtl_occu"]);
            this.Idprooftypeptr=mclsCFunc.ConvertToString(dtData.Rows[0]["gtl_idprooftypeptr"]);
            this.IdproofNo=mclsCFunc.ConvertToString(dtData.Rows[0]["gtl_idproofNo"]);
            this.Phone=mclsCFunc.ConvertToString(dtData.Rows[0]["gtl_phone"]);
            this.Mobile=mclsCFunc.ConvertToString(dtData.Rows[0]["gtl_mobile"]);
            this.Opno=mclsCFunc.ConvertToString(dtData.Rows[0]["gtl_opno"]);
            this.Opupdt=mclsCFunc.FormatDate(dtData.Rows[0]["gtl_opupdt"].ToString(),"dd/MM/yyyy");
            this.Isheader=mclsCFunc.ConvertToString(dtData.Rows[0]["gtl_isheader"]);
            this.Active=mclsCFunc.ConvertToString(dtData.Rows[0]["gtl_active"]);
            this.Slno=mclsCFunc.ConvertToInt(dtData.Rows[0]["gtl_slno"]);
            this.User=mclsCFunc.ConvertToString(dtData.Rows[0]["gtl_user"]);
            this.Userid=mclsCFunc.ConvertToInt(dtData.Rows[0]["gtl_userid"]);
            this.Remarks=mclsCFunc.ConvertToString(dtData.Rows[0]["gtl_remarks"]);
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
        SQL=" select gtl_id,gtl_type,gtl_refno,gtl_dt,gtl_dttm,gtl_title,gtl_name,gtl_gender,gtl_dob,gtl_age,gtl_agetype,gtl_rel,gtl_occu,gtl_idprooftypeptr,gtl_idproofNo,gtl_phone,gtl_mobile,gtl_opno,gtl_opupdt,gtl_isheader,gtl_active,gtl_slno,gtl_user,gtl_userid,gtl_remarks from gtgrouptreatlist" ; 
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
//txtId.Text= this.Id
//txtType.Text= this.Type
//txtRefno.Text= this.Refno
//txtDt.Text= this.Dt
//txtDttm.Text= this.Dttm
//txtTitle.Text= this.Title
//txtName.Text= this.Name
//txtGender.Text= this.Gender
//txtDob.Text= this.Dob
//txtAge.Text= this.Age
//txtAgetype.Text= this.Agetype
//txtRel.Text= this.Rel
//txtOccu.Text= this.Occu
//txtIdprooftypeptr.Text= this.Idprooftypeptr
//txtIdproofNo.Text= this.IdproofNo
//txtPhone.Text= this.Phone
//txtMobile.Text= this.Mobile
//txtOpno.Text= this.Opno
//txtOpupdt.Text= this.Opupdt
//txtIsheader.Text= this.Isheader
//txtActive.Text= this.Active
//txtSlno.Text= this.Slno
//txtUser.Text= this.User
//txtUserid.Text= this.Userid
//txtRemarks.Text= this.Remarks
//}
 // public string NewCode()
 //{ 
 // return mclsCFunc.NewCode(mstrTableNm, mstrPrimaryKey, '');
 //} 
}
}