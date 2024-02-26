using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Data;
using System.Windows.Forms;


namespace CsHms.Akshay.Class
{
    class CouponCreatorCls
    {
        CommFuncs mclsCFunc = new CommFuncs();
        Global mGlobal = new Global();
        Int32 mintId;
        String mstrMode;
        String mstrDescription;
        String mstrBaseratetypeptr;
        DateTime mdateFromdt;
        DateTime mdateFromdttm;
        DateTime mdateTodt;
        DateTime mdateTodttm;
        String mstrBillmoduletypes;
        String mstrItemtype;
        String mstrValuetype;
        Decimal mdecValue;
        Int32 mintCouponnos;
        String mstrCouponprefix;
        String mstrCouponpostfix;
        Int32 mintCouponsnoofdigits;
        Int32 mintSlno;
        String mstrActive;
        String mstrRemarks;
        DateTime mdateDt;
        String mstrCaption;
        String mstrItemptr;
        String mstrGender;
        String mstrOtherdet1;
        String mstrOtherdet2;
        String mstrOtherdet3;
        String mstrGrp;
        String mstrApplicableitemcodes;
        String mstrCouponCode ;
        String mstrCouponRemarks ;
        String mstrCouponAvailablenos ;
        String SQL;
        String mstrmasptr;
        public void ClearAll()
        {
            mintId = 0;
            mstrMode = "";
            mstrDescription = "";
            mstrBaseratetypeptr = "";
            mdateFromdt = DateTime.Now;
            mdateFromdttm = DateTime.Now;
            mdateTodt = DateTime.Now;
            mdateTodttm = DateTime.Now;
            mstrBillmoduletypes = "";
            mstrItemtype = "";
            mstrValuetype = "";
            mdecValue = 0;
            mintCouponnos = 0;
            mstrCouponprefix = "";
            mstrCouponpostfix = "";
            mintCouponsnoofdigits = 0;
            mintSlno = 0;
            mstrActive = "";
            mstrRemarks = "";
            mdateDt = DateTime.Now;
            mstrCaption = "";
            mstrItemptr = "";
            mstrGender = "";
            mstrOtherdet1 = "";
            mstrOtherdet2 = "";
            mstrOtherdet3 = "";
            mstrGrp = "";
            mstrApplicableitemcodes = "";
            mstrCouponCode = "";
            mstrCouponRemarks = "";
            mstrCouponAvailablenos = "";
            SQL = "";
            mstrmasptr = "";
        }
        public Int32 Id
        {
            set { mintId = value; }
            get { return mintId; }
        }
        public String Mode
        {
            set { mstrMode = value; }
            get { return mstrMode; }
        }
        public String Description
        {
            set { mstrDescription = value; }
            get { return mstrDescription; }
        }
        public String Baseratetypeptr
        {
            set { mstrBaseratetypeptr = value; }
            get { return mstrBaseratetypeptr; }
        }
        public DateTime Fromdt
        {
            set { mdateFromdt = value; }
            get { return mdateFromdt; }
        }
        public DateTime Fromdttm
        {
            set { mdateFromdttm = value; }
            get { return mdateFromdttm; }
        }
        public DateTime Todt
        {
            set { mdateTodt = value; }
            get { return mdateTodt; }
        }
        public DateTime Todttm
        {
            set { mdateTodttm = value; }
            get { return mdateTodttm; }
        }
        public String Billmoduletypes
        {
            set { mstrBillmoduletypes = value; }
            get { return mstrBillmoduletypes; }
        }
        public String Itemtype
        {
            set { mstrItemtype = value; }
            get { return mstrItemtype; }
        }
        public String Valuetype
        {
            set { mstrValuetype = value; }
            get { return mstrValuetype; }
        }
        public Decimal Value
        {
            set { mdecValue = value; }
            get { return mdecValue; }
        }
        public Int32 Couponnos
        {
            set { mintCouponnos = value; }
            get { return mintCouponnos; }
        }
        public String Couponprefix
        {
            set { mstrCouponprefix = value; }
            get { return mstrCouponprefix; }
        }
        public String Couponpostfix
        {
            set { mstrCouponpostfix = value; }
            get { return mstrCouponpostfix; }
        }
        public Int32 Couponsnoofdigits
        {
            set { mintCouponsnoofdigits = value; }
            get { return mintCouponsnoofdigits; }
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
        public DateTime Dt
        {
            set { mdateDt = value; }
            get { return mdateDt; }
        }
        public String Caption
        {
            set { mstrCaption = value; }
            get { return mstrCaption; }
        }
        public String Itemptr
        {
            set { mstrItemptr = value; }
            get { return mstrItemptr; }
        }
        public String Gender
        {
            set { mstrGender = value; }
            get { return mstrGender; }
        }
        public String Otherdet1
        {
            set { mstrOtherdet1 = value; }
            get { return mstrOtherdet1; }
        }
        public String Otherdet2
        {
            set { mstrOtherdet2 = value; }
            get { return mstrOtherdet2; }
        }
        public String Otherdet3
        {
            set { mstrOtherdet3 = value; }
            get { return mstrOtherdet3; }
        }
        public String Grp
        {
            set { mstrGrp = value; }
            get { return mstrGrp; }
        }
        public String Applicableitemcodes
        {
            set { mstrApplicableitemcodes = value; }
            get { return mstrApplicableitemcodes; }
        }
        public String CouponCode
        {
            set { mstrCouponCode = value; }
            get { return mstrCouponCode; }
        }
        public String CouponRemarks
        {
            set { mstrCouponRemarks = value; }
            get { return mstrCouponRemarks; }
        }
        public String CouponAvailablenos
        {
            set { mstrCouponAvailablenos = value; }
            get { return mstrCouponAvailablenos; }

        }

        public DataTable dtGetItems()
        {
            try
            {
                string strSql = "select itm_desc,itm_code from item where itm_package='OP' and itm_active='Y'";
                DataTable dtdata= mGlobal.LocalDBCon.ExecuteQuery(strSql);
                if (dtdata.Rows.Count > 0)
                    return dtdata;
                else
                    return null;
                
                
                
            }
            catch(Exception ex)
            { 
                writeErrorLog(ex, "GetItems");
            return null;
        }
 
        }
        public string CreateCouponMaster()
        {
            try
            {
                string strSql = "INSERT INTO campgndiscountmas (cmgdcm_mode, cmgdcm_baseratetypeptr, cmgdcm_fromdt, cmgdcm_fromdttm, cmgdcm_todt, cmgdcm_todttm, cmgdcm_billmoduletypes, cmgdcm_itemtype, cmgdcm_valuetype, cmgdcm_value, cmgdcm_couponnos, cmgdcm_couponprefix, cmgdcm_couponpostfix, cmgdcm_couponsnoofdigits, cmgdcm_slno, cmgdcm_active, cmgdcm_remarks, cngd_dt, cmgdcm_applicableItemcodes,cmgdcm_description) OUTPUT inserted.cmgdcm_id VALUES ('"
     + mclsCFunc.ConvertToString(mstrMode) + "', '"
     + mclsCFunc.ConvertToString(mstrBaseratetypeptr) + "', '"
     + mclsCFunc.ConvertToString(mdateFromdt.ToString("yyyy-MM-dd")) + "', '"
     + mclsCFunc.ConvertToString(mdateFromdttm.ToString("yyyy-MM-dd HH:mm:ss")) + "', '"
     + mclsCFunc.ConvertToString(mdateTodt.ToString("yyyy-MM-dd")) + "', '"
     + mclsCFunc.ConvertToString(mdateTodttm.ToString("yyyy-MM-dd HH:mm:ss")) + "', '"
     + mclsCFunc.ConvertToString(mstrBillmoduletypes) + "', '"
     + mclsCFunc.ConvertToString(mstrItemtype) + "', '"
     + mclsCFunc.ConvertToString(mstrValuetype) + "', '"
     + mclsCFunc.ConvertToString(mdecValue) + "', '"
     + mclsCFunc.ConvertToString(mintCouponnos) + "', '"
     + mclsCFunc.ConvertToString(mstrCouponprefix) + "', '"
     + mclsCFunc.ConvertToString(mstrCouponpostfix) + "', '"
     + mclsCFunc.ConvertToString(mintCouponsnoofdigits) + "','1', '"
     + mstrActive + "', '"
     + mclsCFunc.ConvertToString(mstrRemarks) + "', '"
     + mclsCFunc.ConvertToString(mdateDt.ToString("yyyy-MM-dd HH:mm:ss")) + "', '"
     + mclsCFunc.ConvertToString(mstrApplicableitemcodes) + "','"
     + mclsCFunc.ConvertToString(mstrDescription)+ "')";

                DataTable dtDismas = mGlobal.LocalDBCon.ExecuteQuery_OnTran(strSql);
                if (dtDismas.Rows.Count > 0)
                {
                    mstrmasptr = mclsCFunc.ConvertToString(dtDismas.Rows[0]["cmgdcm_id"]);
                    if (mstrmasptr != "")
                    {
                        return mstrmasptr;
                    
                    }
                    else
                    {
                        return null;
                        mGlobal.LocalDBCon.RollbackTrans();
                    }

                }
                return null;
            }
            catch(Exception ex)
            { writeErrorLog(ex, "CreateCouponMaster");
            mGlobal.LocalDBCon.RollbackTrans();
            return null;
        }
        }
        public int CreateCoupons()
        {
            try
            {
                string strSql = "insert into campgndiscountcoupons (cmgdcpn_campgndiscountmasptr,cmgdcpn_couponnumber,cmgdcpn_usedstatus,cmgdcpn_module,cmgdcpn_moduletranid,cmgdcpn_remarks,cmgdcpn_mobile,cmgdcpn_availablenos,cmgdcpn_usednos,cmgdcpn_active) values('" + mstrmasptr + "','" + mstrCouponCode + "','N',NULL,'0','" + mstrCouponRemarks + "','1','" + mstrCouponAvailablenos + "','0','Y')";
                int res = mGlobal.LocalDBCon.ExecuteNonQuery_OnTran(strSql);
                if (res > 0)
                    return res;
                else
                {
                    return 0;
                    mGlobal.LocalDBCon.RollbackTrans();
                }
                return 0;
            }
            catch (Exception ex)
            {
                writeErrorLog(ex, "CreateCoupons");
                mGlobal.LocalDBCon.RollbackTrans();
                return 0;
            }
        }
        public void UpdateCouponMaster()
        {
            try
            {
                string strSql = "update campgndiscountmas set cmgdcm_fromdt='" + Fromdt.ToString("yyyy-MM-dd") + "', cmgdcm_fromdttm='" + Fromdttm.ToString("yyyy-MM-ddTHH:mm:ss") + "', cmgdcm_todt='" + Todt.ToString("yyyy-MM-dd") + "', cmgdcm_todttm='" + Todttm.ToString("yyyy-MM-ddTHH:mm:ss") + "' where cmgdcm_id='" + Id + "'";

                int res = mGlobal.LocalDBCon.ExecuteNonQuery(strSql);
                if (res > 0)
                {
                    MessageBox.Show("Updated");
                }
            }
            catch (Exception ex)
            {
                writeErrorLog(ex, "UpdateCouponMaster");
            }
        }
        public static void writeErrorLog(Exception exception, string strErrorSource)
        {
            // Retrieve the path to the error log file from the application configuration.
            string strlogfile = String.Empty;
            strlogfile = System.Configuration.ConfigurationSettings.AppSettings["ErrorLog_path"].ToString();

            try
            {
                // Check if the log file exists; if not, create it.
                if (!File.Exists(strlogfile))
                {
                    File.CreateText(strlogfile);
                }
                // Construct the error log entry.
                string strErrorText = "TMR_Error_Log=> An Error occurred: " + strErrorSource + " ON:" + DateTime.Now;
                strErrorText += Environment.NewLine + exception.Message + Environment.NewLine + exception.StackTrace;
                // Append the error log entry to the log file.
                using (System.IO.FileStream aFile = new System.IO.FileStream(strlogfile, System.IO.FileMode.Append, System.IO.FileAccess.Write))
                using (System.IO.StreamWriter sw = new System.IO.StreamWriter(aFile))
                {
                    sw.WriteLine("**********************************************************************");
                    sw.WriteLine(strErrorText);
                    sw.WriteLine("**********************************************************************");
                }
            }
            catch (Exception ex)
            {
                // If an error occurs during logging, re-throw the exception.
                writeErrorLog(ex, "writeErrorLog");
                throw;
            }
        }
        public bool CheckAlreadyExist(string couponCode)
        {
            try
            {
                string strSql = "SELECT * FROM campgndiscountcoupons WHERE cmgdcpn_couponnumber='" + couponCode + "'";

                DataTable dtData = mGlobal.LocalDBCon.ExecuteQuery_OnTran(strSql);

                if (dtData.Rows.Count > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                writeErrorLog(ex, "CheckAlreadyExist");
                return false;
            }
        }
        public bool CheckAlreadyExist(string couponCode, List<string> existingCodes)
        {
            try
            {
                if (existingCodes.Contains(couponCode))
                {
                    //MessageBox.Show("Coupon code " + couponCode + " already exists. Generating  new one.");
                    return true;
                }
                string strSql = "SELECT * FROM campgndiscountcoupons WHERE cmgdcpn_couponnumber='" + couponCode + "'";

                DataTable dtData = mGlobal.LocalDBCon.ExecuteQuery(strSql);

                if (dtData.Rows.Count > 0)
                    return true;
                else
                    return false;
               
            }
            catch (Exception ex)
            {
                writeErrorLog(ex, "CheckAlreadyExist");
                return false;
            }

        }

    }
}
