using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace CsHms
{
    class TableHdr
    {        
        Global mGlobal = new Global();// Global data     
        CommFuncs mCommFuncs = new CommFuncs();// Common Function library

        private DataTable mdtMain = new DataTable();
        private String mstrSqlMain = "";

        private Int32 _LastIdentityNo = -1;

        public bool HdrInit(String _Table, String _Field1Name, String _Field1Value, bool _isNew)
        {
            if (_Field1Value.Trim() == "")
                _Field1Value = "0";
            String strSql = "select * from " + _Table + " where " + _Field1Name + "=" + _Field1Value.Trim();
            mstrSqlMain = strSql;
            mdtMain = mGlobal.LocalDBCon.ExecuteQuery(strSql);
            mdtMain.TableName = _Table;
            if (_isNew)
            {
                DataRow drNewRow = mdtMain.NewRow();
                mdtMain.Rows.Add(drNewRow);
                mstrSqlMain = "select * from " + _Table + " where 1=2";
            }
            else
            {
                if (mdtMain.Rows.Count <= 0) return false;
            }
            return true;
        }
        public object getData(String _FieldName)
        {
            //if (mdtMain == null) return null;
            Object objRet = null;

            if (mdtMain.Columns.Contains(_FieldName))
                objRet = mdtMain.Rows[0][_FieldName];
            return objRet;
        }
        public String getGRN_MaxNumber()
        {
            DataTable dtData = mGlobal.LocalDBCon.ExecuteQuery("select max(puh_grn) from purchasehdr");
            Decimal decRet = 1;
            decRet += Decimal.Parse(mCommFuncs.ConvertToNumberObj(dtData.Rows[0][0].ToString()).ToString());
            return (decRet.ToString());
        }
        public String getBillNo_MaxNumber()
        {
            DataTable dtData = mGlobal.LocalDBCon.ExecuteQuery("select max(slh_no) from saleshdr");
            Decimal decRet = 1;
            decRet += Decimal.Parse(mCommFuncs.ConvertToNumberObj(dtData.Rows[0][0].ToString()).ToString());
            return (decRet.ToString());
        }        
        public void setData(String _FieldName, Object _Data)
        {
            //if (mdtMain == null) return;
            if (mdtMain.Columns.Contains(_FieldName))
                mdtMain.Rows[0][_FieldName] = _Data;
        }
        public bool SaveData(String _FromMode)
        {
            _LastIdentityNo = -1;
            if (_FromMode == "P")
            {
                mdtMain.Rows[0]["puh_grn"] = getGRN_MaxNumber();
                int intCnt = mGlobal.LocalDBCon.UpdateDataTable(mstrSqlMain, mdtMain);
                if (intCnt > 0)
                {
                    _LastIdentityNo = Int32.Parse(mGlobal.LocalDBCon.ExecuteQuery("select @@identity from " + mdtMain.TableName).Rows[0][0].ToString());
                    return true;
                }
            }
            else if (_FromMode == "S")
            {
                mdtMain.Rows[0]["slh_no"] = getBillNo_MaxNumber();
                mdtMain.Rows[0]["slh_billno"] = mdtMain.Rows[0]["slh_no"];
                int intCnt = mGlobal.LocalDBCon.UpdateDataTable(mstrSqlMain, mdtMain);
                if (intCnt > 0)
                {
                    _LastIdentityNo = Int32.Parse(mGlobal.LocalDBCon.ExecuteQuery("select @@identity from " + mdtMain.TableName).Rows[0][0].ToString());
                    return true;
                }
            }
            return false;
        }
        public void DeleteData(String _FromMode, String _Table, String _Field1Name, String _Field1Value)
        {
            mGlobal.LocalDBCon.ExecuteNonQuery("delete from " + _Table + " where " + _Field1Name + "=" + _Field1Value);
        }
        public Int32 LastIdentityNo
        {
            get { return _LastIdentityNo; }
            //set { _LastIdentityNo = value; }
        }
    }
}
