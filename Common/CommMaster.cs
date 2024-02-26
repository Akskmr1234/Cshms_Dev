using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Windows.Forms;
namespace CsHms
{
    class CommMaster
    {
        CommFuncs mComFuc = new CommFuncs();
        Global mGlobal = new Global();       
        public int createAccountMaster(String strCode, String strName, String strGrpCode, Double dblDefAmt, 
                    int intSlno,DateTime dtmChange)
        {
            int intAns=-1;
            try
            {
                String sql = "insert into acmas(ac_code,ac_desc,ac_groupptr,ac_defamt,ac_slno,cngd_dt)values " +
                 "   ('" + strCode.Trim() + "','" + strName.Trim() + "','" + strGrpCode.Trim() + "', " + dblDefAmt + " , " +
                    intSlno + ", " + mComFuc.FormatDBDate(dtmChange.ToShortDateString()) + ")";
                intAns= mGlobal.LocalDBCon.ExecuteNonQuery(sql);
            }
                
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);                
            }
            return intAns; 
        }

        public String getCustomerGenralAccountCode(string strCuscode)
        {
            object  objRtnValue = "";            
            objRtnValue = mGlobal.LocalDBCon.ExecuteScalar("Select cu_acptr from cusmas where cu_code='" + strCuscode.Trim() + "'");            
            return objRtnValue.ToString();
        }
        public void  setCustomerGenralAccountCode(string strCuscode,string strGeneralAcCode)
        {
            try
            {
                mGlobal.LocalDBCon.ExecuteNonQuery("update cusmas set  cu_acptr='" + strGeneralAcCode.Trim()+"' where cu_code='" + strCuscode.Trim() + "'");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(),"Common Master");
            }             
        }
        public void setBranchGenralAccountCode(string strBrcode, string strGeneralAcCode)
        {
            try
            {
                mGlobal.LocalDBCon.ExecuteNonQuery("update branchmas set  br_acptr='" + strGeneralAcCode.Trim() + "' where br_code='" + strBrcode.Trim() + "'");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Common Master");
            }
        }
        public void setSupplierGenralAccountCode(string strSupcode, string strGeneralAcCode)
        {
            try
            {
                mGlobal.LocalDBCon.ExecuteNonQuery("update supmas set  su_acptr='" + strGeneralAcCode.Trim() + "' where su_code='" + strSupcode.Trim() + "'");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Common Master");
            }
        }
            

    }

    
}
