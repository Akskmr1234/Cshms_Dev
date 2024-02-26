using System;
using System.Data;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace CsHms._Masters.Class
{
    #region Properies And Methods
    public class ShareholderCls
    {
        CommFuncs mclsCFunc = new CommFuncs();
        Global mGlobal = new Global();
        Int64 mintId;
        String mstrOpno;
        String mstrNo;
        String mstrTitle;
        String mstrName;
        Decimal mdecAge;
        String mstrAgeType;
        String mstrDispage;
        DateTime? mdateDob;
        String mstrGender;
        String mstrType;
        DateTime mdateRegdt;
        Decimal mdecSharevalue;
        Decimal mdecSharenos;
        Decimal mdecAppvalue;
        String mstrStatus;
        DateTime mdateEntrydttm;
        DateTime mdateSeravaildt;
        String mstrIslinkedop;
        String SQL;
        public void ClearAll()
        {
            mintId = 0;
            mstrOpno = "";
            mstrNo = "";
            mstrTitle = "";
            mstrName = "";
            mdecAge = 0;
            mstrDispage = "";
            mdateDob = null;
            mstrGender = "";
            mstrType = "";
            mdateRegdt = DateTime.Now;
            mdecSharevalue = 0;
            mdecSharenos = 0;
            mdecAppvalue = 0;
            mstrStatus = "";
            mdateEntrydttm = DateTime.Now;
            mdateSeravaildt = DateTime.Now;
            mstrIslinkedop = "";
            SQL = "";
        }
        public Int64 Id
        {
            set { mintId = value; }
            get { return mintId; }
        }
        public String Opno
        {
            set { mstrOpno = value; }
            get { return mstrOpno; }
        }
        public String No
        {
            set { mstrNo = value; }
            get { return mstrNo; }
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
        public Decimal Age
        {
            set { mdecAge = value; }
            get { return mdecAge; }
        }
        public String AgeType
        {
            set { mstrAgeType  = value; }
            get { return mstrAgeType; }
        }
        public String Dispage
        {
            set { mstrDispage = value; }
            get { return mstrDispage; }
        }
        public DateTime ? Dob
        {
            set { mdateDob = value; }
            //get { return mdateDob.GetValueOrDefault(); }
            get { return mdateDob; }
        }
        public String Gender
        {
            set { mstrGender = value; }
            get { return mstrGender; }
        }
        public String Type
        {
            set { mstrType = value; }
            get { return mstrType; }
        }
        public DateTime Regdt
        {
            set { mdateRegdt = value; }
            get { return mdateRegdt; }
        }
        public Decimal Sharevalue
        {
            set { mdecSharevalue = value; }
            get { return mdecSharevalue; }
        }
        public Decimal Sharenos
        {
            set { mdecSharenos = value; }
            get { return mdecSharenos; }
        }
        public Decimal Appvalue
        {
            set { mdecAppvalue = value; }
            get { return mdecAppvalue; }
        }
        public String Status
        {
            set { mstrStatus = value; }
            get { return mstrStatus; }
        }
        public DateTime Entrydttm
        {
            set { mdateEntrydttm = value; }
            get { return mdateEntrydttm; }
        }
        public DateTime Seravaildt
        {
            set { mdateSeravaildt = value; }
            get { return mdateSeravaildt; }
        }
        public String Islinkedop
        {
            set { mstrIslinkedop = value; }
            get { return mstrIslinkedop; }
        }
       
        
        public bool insertData()
        {
            try
            {
                if (this.Dob.HasValue)
                {
                    SQL = "insert into shareholder(shldr_opno,shldr_no,shldr_title,shldr_name,shldr_age,shldr_agetype,shldr_dispage,shldr_dob,shldr_gender,shldr_type,shldr_regdt,shldr_sharevalue,shldr_sharenos,shldr_appvalue,shldr_status,shldr_entrydttm,shldr_seravaildt,shldr_islinkedop) values ('" + this.Opno + "','" + this.No + "','" + this.Title + "','" + this.Name + "'," + this.Age + ",'" + this.AgeType + "','" + this.Dispage + "'," + mclsCFunc.FormatDBDate(this.Dob.ToString()) + ",'" + this.Gender + "','" + this.Type + "'," + mclsCFunc.FormatDBDate(this.Regdt.ToString()) + "," + this.Sharevalue + "," + this.Sharenos + "," + this.Appvalue + ",'" + this.Status + "'," + mclsCFunc.FormatDBDate(this.Entrydttm.ToString()) + "," + mclsCFunc.FormatDBDate(this.Seravaildt.ToString()) + ",'" + this.Islinkedop + "')";
                }
                else
                {
                    SQL = "insert into shareholder(shldr_opno,shldr_no,shldr_title,shldr_name,shldr_age,shldr_agetype,shldr_dispage,shldr_gender,shldr_type,shldr_regdt,shldr_sharevalue,shldr_sharenos,shldr_appvalue,shldr_status,shldr_entrydttm,shldr_seravaildt,shldr_islinkedop) values ('" + this.Opno + "','" + this.No + "','" + this.Title + "','" + this.Name + "'," + this.Age + ",'" + this.AgeType + "','" + this.Dispage + "','" + this.Gender + "','" + this.Type + "'," + mclsCFunc.FormatDBDate(this.Regdt.ToString()) + "," + this.Sharevalue + "," + this.Sharenos + "," + this.Appvalue + ",'" + this.Status + "'," + mclsCFunc.FormatDBDate(this.Entrydttm.ToString()) + "," + mclsCFunc.FormatDBDate(this.Seravaildt.ToString()) + ",'" + this.Islinkedop + "')";
                }
                if (mGlobal.LocalDBCon.ExecuteNonQuery(SQL) > 0)
                {
                    mintId = Int32.Parse(mGlobal.LocalDBCon.ExecuteQuery("select @@identity from shareholder").Rows[0][0].ToString());
                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
            return false;
        }
        public bool updateData()
        {
            try
            {
                if (this.Dob.HasValue)
                {
                    SQL = "update   shareholder set shldr_opno='" + this.Opno + "',shldr_no='" + this.No + "',shldr_title='" + this.Title + "',shldr_name='" + this.Name + "',shldr_age=" + this.Age + ",shldr_agetype='" + this.AgeType + "',shldr_dispage='" + this.Dispage + "',shldr_dob=" + mclsCFunc.FormatDBDate(this.Dob.ToString()) + ",shldr_gender='" + this.Gender + "',shldr_type='" + this.Type + "',shldr_regdt=" + mclsCFunc.FormatDBDate(this.Regdt.ToString()) + ",shldr_sharevalue=" + this.Sharevalue + ",shldr_sharenos=" + this.Sharenos + ",shldr_appvalue=" + this.Appvalue + ",shldr_status='" + this.Status + "',shldr_entrydttm=" + mclsCFunc.FormatDBDate(this.Entrydttm.ToString()) + ",shldr_seravaildt=" + mclsCFunc.FormatDBDate(this.Seravaildt.ToString()) + ",shldr_islinkedop='" + this.Islinkedop + "' where shldr_id=" + this.Id + "";
                }
                else
                {
                    SQL = "update   shareholder set shldr_opno='" + this.Opno + "',shldr_no='" + this.No + "',shldr_title='" + this.Title + "',shldr_name='" + this.Name + "',shldr_age=" + this.Age + ",shldr_agetype='" + this.AgeType + "',shldr_dispage='" + this.Dispage + "',shldr_dob=null,shldr_gender='" + this.Gender + "',shldr_type='" + this.Type + "',shldr_regdt=" + mclsCFunc.FormatDBDate(this.Regdt.ToString()) + ",shldr_sharevalue=" + this.Sharevalue + ",shldr_sharenos=" + this.Sharenos + ",shldr_appvalue=" + this.Appvalue + ",shldr_status='" + this.Status + "',shldr_entrydttm=" + mclsCFunc.FormatDBDate(this.Entrydttm.ToString()) + ",shldr_seravaildt=" + mclsCFunc.FormatDBDate(this.Seravaildt.ToString()) + ",shldr_islinkedop='" + this.Islinkedop + "' where shldr_id=" + this.Id + "";
                }
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
                SQL = "delete  from   shareholder  where shldr_id=" + this.Id + "";
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
                SQL = " select shldr_id,shldr_opno,shldr_no,shldr_title,shldr_name,shldr_age,shldr_agetype,shldr_dispage,shldr_dob,shldr_gender,shldr_type,shldr_regdt,shldr_sharevalue,shldr_sharenos,shldr_appvalue,shldr_status,shldr_entrydttm,shldr_seravaildt,shldr_islinkedop from shareholder where  shldr_id=" + this.Id + "";
                DataTable dtData = mGlobal.LocalDBCon.ExecuteQuery(SQL);
                if (mclsCFunc.ConvertToInt(dtData.Rows.Count) > 0)
                {
                    this.Id = mclsCFunc.ConvertToInt64(dtData.Rows[0]["shldr_id"]);
                    this.Opno = mclsCFunc.ConvertToString(dtData.Rows[0]["shldr_opno"]);
                    this.No = mclsCFunc.ConvertToString(dtData.Rows[0]["shldr_no"]);
                    this.Title = mclsCFunc.ConvertToString(dtData.Rows[0]["shldr_title"]);
                    this.Name = mclsCFunc.ConvertToString(dtData.Rows[0]["shldr_name"]);
                    this.Age = mclsCFunc.ConvertToNumber_Dec(dtData.Rows[0]["shldr_age"]);
                    this.AgeType = mclsCFunc.ConvertToString(dtData.Rows[0]["shldr_agetype"]);
                    this.Dispage = mclsCFunc.ConvertToString(dtData.Rows[0]["shldr_dispage"]);
                    if (dtData.Rows[0]["shldr_dob"].ToString() == "")
                    {
                        this.Dob = null;
                    }
                    else
                    {
                        this.Dob = DateTime.Parse(dtData.Rows[0]["shldr_dob"].ToString());
                    }                   
                    this.Gender = mclsCFunc.ConvertToString(dtData.Rows[0]["shldr_gender"]);
                    this.Type = mclsCFunc.ConvertToString(dtData.Rows[0]["shldr_type"]);
                    this.Regdt = mclsCFunc.FormatDate(dtData.Rows[0]["shldr_regdt"].ToString(), "dd/MM/yyyy");
                    this.Sharevalue = mclsCFunc.ConvertToNumber_Dec(dtData.Rows[0]["shldr_sharevalue"]);
                    this.Sharenos = mclsCFunc.ConvertToNumber_Dec(dtData.Rows[0]["shldr_sharenos"]);
                    this.Appvalue = mclsCFunc.ConvertToNumber_Dec(dtData.Rows[0]["shldr_appvalue"]);
                    this.Status = mclsCFunc.ConvertToString(dtData.Rows[0]["shldr_status"]);
                    this.Entrydttm = mclsCFunc.FormatDate(dtData.Rows[0]["shldr_entrydttm"].ToString(), "dd/MM/yyyy");
                    this.Seravaildt = mclsCFunc.FormatDate(dtData.Rows[0]["shldr_seravaildt"].ToString(), "dd/MM/yyyy");
                    this.Islinkedop = mclsCFunc.ConvertToString(dtData.Rows[0]["shldr_islinkedop"]);
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
                DataTable dtData = getDataList("");
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
                SQL = " select shldr_id,shldr_opno,shldr_no,shldr_title,shldr_name,shldr_age,shldr_agetype,shldr_dispage,shldr_dob,shldr_gender,shldr_type,shldr_regdt,shldr_sharevalue,shldr_sharenos,shldr_appvalue,shldr_status,shldr_entrydttm,shldr_seravaildt,shldr_islinkedop from shareholder";
                if (strConditionSql.Trim().Length > 0)
                    SQL += " where " + strConditionSql;
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
        //public bool shareholderfmly()
        //{

        //    DataTable dtData = new DataTable();
        //    dtData = mGlobal.LocalDBCon.ExecuteQuery("select shldr_opno as OPNo,shldr_name as Name,shldr_no as ShareNo,shldr_age as  Age, shldr_regdt as Date,shldr_sharevalue  as ShareValue,shldr_sharenos as NoOfShare,shldr_appvalue as AppValue ,shldr_entrydttm as RegisterDate,shldr_seravaildt as ServiceValidDate from shareholder");
          
               
        //}

        //void fillDataToControl
        //{
        //txtId.Text= this.Id
        //txtOpno.Text= this.Opno
        //txtNo.Text= this.No
        //txtTitle.Text= this.Title
        //txtName.Text= this.Name
        //txtAge.Text= this.Age
        //txtDispage.Text= this.Dispage
        //txtDob.Text= this.Dob
        //txtGender.Text= this.Gender
        //txtType.Text= this.Type
        //txtRegdt.Text= this.Regdt
        //txtSharevalue.Text= this.Sharevalue
        //txtSharenos.Text= this.Sharenos
        //txtAppvalue.Text= this.Appvalue
        //txtStatus.Text= this.Status
        //txtEntrydttm.Text= this.Entrydttm
        //txtSeravaildt.Text= this.Seravaildt
        //txtIslinkedop.Text= this.Islinkedop
        //}
        // public string NewCode()
        //{ 
        // return mclsCFunc.NewCode(mstrTableNm, mstrPrimaryKey, '');
        //} 
    }



    #endregion




}
