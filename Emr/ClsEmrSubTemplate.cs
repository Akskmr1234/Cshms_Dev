//using System;
//using System.Collections.Generic;
//using System.Text;
//using System.Data;
//using CsHms.Common;
//using System.Windows.Forms;
//namespace CsHms.Emr
//{
//    class ClsEmrSubTemplate
//    {

//        #region Proerties and  Variables
//        Int32 mintid = 0;
//        Int32 mintheaderid = 0;
//        String mstrdesc = "";
//        String mstrdata = "";
//        String mstractive = "";
//        Int32 mintslno = 0;
//        public Int32 id
//        {
//            set { mintid = value; }
//            get { return mintid; }
//        }
//        public Int32 headerid
//        {
//            set { mintheaderid = value; }
//            get { return mintheaderid; }
//        }
//        public String desc
//        {
//            set { mstrdesc = value; }
//            get { return mstrdesc; }
//        }
//        public String data
//        {
//            set { mstrdata = value; }
//            get { return mstrdata; }
//        }
//        public String active
//        {
//            set { mstractive = value; }
//            get { return mstractive; }
//        }
//        public Int32 slno
//        {
//            set { mintslno = value; }
//            get { return mintslno; }
//        }

//        public void ClearAll()
//        {
//            mintid = 0;
//            mintheaderid = 0;
//            mstrdesc = "";
//            mstrdata = "";
//            mstractive = "";
//            mintSlno = 0;
//        }
//        public bool InsertData()
//        {
//            try
//            {
//                string strSql = "insert into " + TABLE_NAME + " (estmp_headerid,estmp_desc,estmp_data,estmp_active,estmp_slno) values " +
//                    " ( "+ this.headerid  +",'" + this.desc + "','" + this.data + "','" + this.active + "'," + this.Slno + ")";
//                int intAns = mGlobal.LocalDBCon.ExecuteNonQuery(strSql);
//                if (intAns == 1) return true;
//                else return false;
//            }
//            catch { }
//            return false;
//        }

//        public bool UpdateData()
//        {
//            try
//            {
//                string strSql = "update " + TABLE_NAME + "  set estmp_headerid=" + this.headerid + ",estmp_desc='" + this.desc + "',estmp_data='" + this.data + "',estmp_active='" + this.active + "',estmp_slno=" + this.Slno + ") where estmp_id=" + this.mintid + " ";
//                int intAns = mGlobal.LocalDBCon.ExecuteNonQuery(strSql);
//                if (intAns == 1) return true;
//                else return false;
//            }
//            catch { }
//            return false;
//        }
//        public int generateNewCode()
//        {
//            try
//            {
//                return mCommFuncs.NewCode(TABLE_NAME, PRIMARY_KEY, "");
//            }
//            catch { }
//            return 0;
//        }

//        public bool getData()
//        {
//            try
//            {
//                DataTable dtData = mGlobal.LocalDBCon.ExecuteQuery("select * from " + TABLE_NAME +
//                    " where " + PRIMARY_KEY + " ='" + this.id + "'");
//                if (dtData.Rows.Count > 0)
//                {
//                    this.headerid = dtData.Rows[0]["estmp_headerid"].ToString();
//                    this.desc = dtData.Rows[0]["estmp_desc"].ToString();
//                    this.data = dtData.Rows[0]["estmp_data"].ToString();
//                    this.active = Convert.ToDouble(dtData.Rows[0]["estmp_active"]);
//                    this.Slno = mCommFuncs.ConvertToInt(dtData.Rows[0]["estmp_slno"]);
//                    return true;
//                }
//                else
//                    return false;
//            }
//            catch { }
//            return false;
//        }
//        #endregion

//    }
//}
