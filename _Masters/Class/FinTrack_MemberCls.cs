using System;
using System.Data;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace CsHms._Masters.Class
{
    public class FinTrack_MemberCls
    {
        CommFuncs mclsCFunc = new CommFuncs();
        Global mGlobal = new Global();
        String SQL;

        public DataTable LoadMemberType(string strConditionSql)
        {
            try
            {
                SQL = "select distinct MemType,MtName  from View_MemberDetails_Fintrack  ";
                if (strConditionSql.Trim().Length > 0)
                    SQL += " where " + strConditionSql;
                SQL += " order by MtName";
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

        public DataTable getDataList(string strConditionSql)
        {
            try
            {
                SQL = " select * from View_MemberDetails_Fintrack ";
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
    }
}
