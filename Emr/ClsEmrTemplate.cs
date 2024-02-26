//using System;
//using System.Collections.Generic;
//using System.Text;
//using System.Data;
//using CsHms.Common;
//using System.Windows.Forms;
//namespace CsHms.Emr
//{
//    class ClsEmrTemplate
//    {
//        const String TABLE_NAME = "EmrTemplate";// Main Table name
//        const String PRIMARY_KEY = "etmp_id";// Primary key of the table
//        Global mGlobal = new Global();// Global data     
//        CommFuncs mCommFuncs = new CommFuncs();// Common Function library  
//        #region Proerties and  Variables
//        Int32 mintid = 0;
//        String mstrdesc = "";
//        String mstrdata = "";
//        String mstractive = "";
//        Int32 mintslno = 0;
//        public Int32 id
//        {
//            set { mintid = value; }
//            get { return mintid; }
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

//       public  void ClearAll()
//        {
//            mintid = 0;
//            mstrdesc = "";
//            mstrdata = "";
//            mstractive = "";
//            mintSlno = 0;           
//        }
//        public bool InsertData()
//        {         
//            try
//            {
//                string strSql = "insert into " + TABLE_NAME + " (etmp_desc,etmp_data,etmp_active,etmp_slno) values " +
//                    " ( '" + this.desc + "','" + this.data + "','" + this.active + "'," + this.Slno + ")";
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
//                string strSql = "update " + TABLE_NAME + "  set etmp_desc='" + this.desc + "',etmp_data='" + this.data + "',etmp_active='" + this.active + "',etmp_slno=" + this.Slno + ") where etmp_id="+ this.mintid  +" " ;                
//                int intAns = mGlobal.LocalDBCon.ExecuteNonQuery(strSql);
//                if (intAns == 1) return true;
//                else return false;
//            }
//            catch { }
//            return false;
//        }
//        public int  generateNewCode()
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
//                    this.desc = dtData.Rows[0]["etmp_desc"].ToString();
//                    this.data = dtData.Rows[0]["etmp_data"].ToString();
//                    this.active = Convert.ToDouble(dtData.Rows[0]["etmp_active"]);
//                    this.Slno = mCommFuncs.ConvertToInt(dtData.Rows[0]["etmp_slno"]);                    
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
