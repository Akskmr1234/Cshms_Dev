using System;
using System.Data;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
namespace CsHms._Masters.Class
{
    #region Properies And Methods

    public class ShareHolderFamilyCls
    {
        CommFuncs mclsCFunc = new CommFuncs();
        Global mGlobal = new Global();
        Int32 mintId;
        Int32 mintHdrId;
        String mstrOpno;
        String mstrName;
        Decimal mdecAge;
        String mstrAgetype;
        String mstrDispage;
        DateTime? mdateDob;
        String mstrGender;
        String mstrRelation;
        DateTime mdateRegdt;
        DateTime mdateSeravaildt;
        String mstrIslinkedop;
        String SQL;
        public void ClearAll()
        {
            mintId = 0;
            mintHdrId = 0;
            mstrOpno = "";
            mstrName = "";
            mdecAge = 0;
            mstrAgetype = "";
            mstrDispage = "";
           // mdateDob = DateTime.Now;
            mdateDob = null;
            mstrGender = "";
            mstrRelation = "";
            mdateRegdt = DateTime.Now;
            mdateSeravaildt = DateTime.Now;
            mstrIslinkedop = "";
            SQL = "";
        }
        public Int32 Id
        {
            set { mintId = value; }
            get { return mintId; }
        }
        public Int32 HdrId
        {
            set { mintHdrId = value; }
            get { return mintHdrId; }
        }
        public String Opno
        {
            set { mstrOpno = value; }
            get { return mstrOpno; }
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
        public String Agetype
        {
            set { mstrAgetype = value; }
            get { return mstrAgetype; }
        }
        public String Dispage
        {
            set { mstrDispage = value; }
            get { return mstrDispage; }
        }
        public DateTime? Dob
        {
            set { mdateDob = value; }
            get { return mdateDob; }
        }
        public String Gender
        {
            set { mstrGender = value; }
            get { return mstrGender; }
        }
        public String Relation
        {
            set { mstrRelation = value; }
            get { return mstrRelation; }
        }
        public DateTime Regdt
        {
            set { mdateRegdt = value; }
            get { return mdateRegdt; }
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
        //void setDataToProperties
        //{
        //Id=mclsCFunc.ConvertToInt( txtId.Text)
        //Opno=mclsCFunc.ConvertToString( txtOpno.Text)
        //Name=mclsCFunc.ConvertToString( txtName.Text)
        //Age=mclsCFunc.ConvertToNumber_Dec( txtAge.Text)
        //Agetype=mclsCFunc.ConvertToString( txtAgetype.Text)
        //Dispage=mclsCFunc.ConvertToString( txtDispage.Text)
        //Dob=mclsCFunc.FormatDate( txtDob.Text,"dd/MM/yyyy")
        //Gender=mclsCFunc.ConvertToString( txtGender.Text)
        //Relation=mclsCFunc.ConvertToString( txtRelation.Text)
        //Regdt=mclsCFunc.FormatDate( txtRegdt.Text,"dd/MM/yyyy")
        //Islinkedop=mclsCFunc.ConvertToString( txtIslinkedop.Text)
        //}
        public bool insertData()
        {
            try
            {
                //SQL = "insert into ShareHolderFamily(shldrfly_hdrid,shldrfly_opno,shldrfly_name,shldrfly_age,shldrfly_agetype,shldrfly_dispage,shldrfly_dob,shldrfly_gender,shldrfly_relation,shldrfly_regdt,shldrfly_seravaildt,shldrfly_islinkedop) values ("+ this.HdrId  +",'" + this.Opno + "','" + this.Name + "'," + this.Age + ",'" + this.Agetype + "','" + this.Dispage + "'," + mclsCFunc.FormatDBDate(this.Dob.ToString()) + ",'" + this.Gender + "','" + this.Relation + "'," + mclsCFunc.FormatDBDate(this.Regdt.ToString()) + "," + mclsCFunc.FormatDBDate(this.Seravaildt.ToString()) + ",'" + this.Islinkedop + "')";
                if (this.Dob.HasValue)
                {
                    SQL = "insert into ShareHolderFamily(shldrfly_hdrid,shldrfly_opno,shldrfly_name,shldrfly_age,shldrfly_agetype,shldrfly_dispage,shldrfly_dob,shldrfly_gender,shldrfly_relation,shldrfly_regdt,shldrfly_seravaildt,shldrfly_islinkedop) values (" + this.HdrId + ",'" + this.Opno + "','" + this.Name + "'," + this.Age + ",'" + this.Agetype + "','" + this.Dispage + "'," + mclsCFunc.FormatDBDate(this.Dob.ToString()) + ",'" + this.Gender + "','" + this.Relation + "'," + mclsCFunc.FormatDBDate(this.Regdt.ToString()) + "," + mclsCFunc.FormatDBDate(this.Seravaildt.ToString()) + ",'" + this.Islinkedop + "')";
                }
                else
                {
                    SQL = "insert into ShareHolderFamily(shldrfly_hdrid,shldrfly_opno,shldrfly_name,shldrfly_age,shldrfly_agetype,shldrfly_dispage,shldrfly_dob,shldrfly_gender,shldrfly_relation,shldrfly_regdt,shldrfly_seravaildt,shldrfly_islinkedop) values (" + this.HdrId + ",'" + this.Opno + "','" + this.Name + "'," + this.Age + ",'" + this.Agetype + "','" + this.Dispage + "',null,'" + this.Gender + "','" + this.Relation + "'," + mclsCFunc.FormatDBDate(this.Regdt.ToString()) + "," + mclsCFunc.FormatDBDate(this.Seravaildt.ToString()) + ",'" + this.Islinkedop + "')";
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
        public bool updateData()
        {
            try
            {
                SQL = "update   ShareHolderFamily set shldrfly_hdrid=" + this.HdrId + ",shldrfly_opno='" + this.Opno + "',shldrfly_name='" + this.Name + "',shldrfly_age=" + this.Age + ",shldrfly_agetype='" + this.Agetype + "',shldrfly_dispage='" + this.Dispage + "',shldrfly_dob=" + mclsCFunc.FormatDBDate(this.Dob.ToString()) + ",shldrfly_gender='" + this.Gender + "',shldrfly_relation='" + this.Relation + "',shldrfly_regdt=" + mclsCFunc.FormatDBDate(this.Regdt.ToString()) + ",shldrfly_islinkedop='" + this.Islinkedop + "',shldrfly_seravaildt=" + mclsCFunc.FormatDBDate(this.Seravaildt.ToString()) + " where shldrfly_id=" + this.Id + "";
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
                SQL = "delete  from   ShareHolderFamily  where shldrfly_hdrid=" + this.HdrId  + "";
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
                SQL = " select shldrfly_id,shldrfly_hdrid,shldrfly_opno,shldrfly_name,shldrfly_age,shldrfly_agetype,shldrfly_dispage,shldrfly_dob,shldrfly_gender,shldrfly_relation,shldrfly_regdt,shldrfly_seravaildt,shldrfly_islinkedop from ShareHolderFamily where  shldrfly_id=" + this.Id + "";
                DataTable dtData = mGlobal.LocalDBCon.ExecuteQuery(SQL);
                if (mclsCFunc.ConvertToInt(dtData.Rows.Count) > 0)
                {
                    this.Id = mclsCFunc.ConvertToInt(dtData.Rows[0]["shldrfly_id"]);
                    this.HdrId = mclsCFunc.ConvertToInt(dtData.Rows[0]["shldrfly_hdrid"]);
                    this.Opno = mclsCFunc.ConvertToString(dtData.Rows[0]["shldrfly_opno"]);
                    this.Name = mclsCFunc.ConvertToString(dtData.Rows[0]["shldrfly_name"]);
                    this.Age = mclsCFunc.ConvertToNumber_Dec(dtData.Rows[0]["shldrfly_age"]);
                    this.Agetype = mclsCFunc.ConvertToString(dtData.Rows[0]["shldrfly_agetype"]);
                    this.Dispage = mclsCFunc.ConvertToString(dtData.Rows[0]["shldrfly_dispage"]);
                    this.Dob = mclsCFunc.FormatDate(dtData.Rows[0]["shldrfly_dob"].ToString(), "dd/MM/yyyy");
                    this.Gender = mclsCFunc.ConvertToString(dtData.Rows[0]["shldrfly_gender"]);
                    this.Relation = mclsCFunc.ConvertToString(dtData.Rows[0]["shldrfly_relation"]);
                    this.Regdt = mclsCFunc.FormatDate(dtData.Rows[0]["shldrfly_regdt"].ToString(), "dd/MM/yyyy");
                    this.Seravaildt = mclsCFunc.FormatDate(dtData.Rows[0]["shldrfly_seravaildt"].ToString(), "dd/MM/yyyy");
                    this.Islinkedop = mclsCFunc.ConvertToString(dtData.Rows[0]["shldrfly_islinkedop"]);
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
                SQL = " select shldrfly_id,shldrfly_hdrid,shldrfly_opno,shldrfly_name,shldrfly_age,shldrfly_agetype,shldrfly_dispage,shldrfly_dob,shldrfly_gender,shldrfly_relation,shldrfly_regdt,shldrfly_islinkedop from ShareHolderFamily";
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

        public DataTable LoadToGrid(string strConditionSql)
        {
            try
            {
                SQL = " select shldrfly_hdrid,shldrfly_opno as opno,shldrfly_name as opname,shldrfly_age as age,shldrfly_agetype as agetype,shldrfly_dispage as displayage,cast(shldrfly_dob as date) as dob,shldrfly_gender as gender,shldrfly_relation as relation,cast(shldrfly_regdt as date) as regdate,cast(shldrfly_seravaildt as  date) as seravdate,shldrfly_islinkedop as linkedtoop from ShareHolderFamily";
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
        //void fillDataToControl
        //{
        //txtId.Text= this.Id
        //txtOpno.Text= this.Opno
        //txtName.Text= this.Name
        //txtAge.Text= this.Age
        //txtAgetype.Text= this.Agetype
        //txtDispage.Text= this.Dispage
        //txtDob.Text= this.Dob
        //txtGender.Text= this.Gender
        //txtRelation.Text= this.Relation
        //txtRegdt.Text= this.Regdt
        //txtIslinkedop.Text= this.Islinkedop
        //}
        // public string NewCode()
        //{ 
        // return mclsCFunc.NewCode(mstrTableNm, mstrPrimaryKey, '');
        //} 
    }



    #endregion
}
