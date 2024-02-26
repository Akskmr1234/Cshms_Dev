using System;
using System.Data;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace CsHms.Masters
{
    class TaxCls
    {
        CommFuncs mclsCFunc = new CommFuncs();
        Global mGlobal = new Global();
        String mstrCode;
        String mstrDesc;
        String mstrMode;
        Decimal mdecTaxper;
        String mstrAst1desc;
        Decimal mdecAst1per;
        String mstrAst2desc;
        Decimal mdecAst2per;
        Decimal mdecNettax;
        Int32 mintSlno;
        String mstrActive;
        String mstrRemarks;
        String SQL;
        public void ClearAll()
        {
            mstrCode = "";
            mstrDesc = "";
            mstrMode = "";
            mdecTaxper = 0;
            mstrAst1desc = "";
            mdecAst1per = 0;
            mstrAst2desc = "";
            mdecAst2per = 0;
            mdecNettax = 0;
            mintSlno = 0;
            mstrActive = "";
            mstrRemarks = "";
            SQL = "";
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
        public String Mode
        {
            set { mstrMode = value; }
            get { return mstrMode; }
        }
        public Decimal Taxper
        {
            set { mdecTaxper = value; }
            get { return mdecTaxper; }
        }
        public String Ast1desc
        {
            set { mstrAst1desc = value; }
            get { return mstrAst1desc; }
        }
        public Decimal Ast1per
        {
            set { mdecAst1per = value; }
            get { return mdecAst1per; }
        }
        public String Ast2desc
        {
            set { mstrAst2desc = value; }
            get { return mstrAst2desc; }
        }
        public Decimal Ast2per
        {
            set { mdecAst2per = value; }
            get { return mdecAst2per; }
        }
        public Decimal Nettax
        {
            set { mdecNettax = value; }
            get { return mdecNettax; }
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
        public bool insertData()
        {
            try
            {
                SQL = "insert into tax(tx_code,tx_desc,tx_mode,tx_taxper,tx_ast1desc,tx_ast1per,tx_ast2desc,tx_ast2per,tx_nettax,tx_slno,tx_active,tx_remarks) values ('" + this.Code + "','" + this.Desc + "','" + this.Mode + "'," + this.Taxper + ",'" + this.Ast1desc + "'," + this.Ast1per + ",'" + this.Ast2desc + "'," + this.Ast2per + "," + this.Nettax + "," + this.Slno + ",'" + this.Active + "','" + this.Remarks + "')";
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
                SQL = "update   tax set tx_code='" + this.Code + "',tx_desc='" + this.Desc + "',tx_mode='" + this.Mode + "',tx_taxper=" + this.Taxper + ",tx_ast1desc='" + this.Ast1desc + "',tx_ast1per=" + this.Ast1per + ",tx_ast2desc='" + this.Ast2desc + "',tx_ast2per=" + this.Ast2per + ",tx_nettax=" + this.Nettax + ",tx_slno=" + this.Slno + ",tx_active='" + this.Active + "',tx_remarks='" + this.Remarks + "' where tx_code='" + this.Code + "'";
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
                SQL = "delete  from   tax  where tx_code='" + this.Code + "'";
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
                SQL = " select tx_code,tx_desc,tx_mode,tx_taxper,tx_ast1desc,tx_ast1per,tx_ast2desc,tx_ast2per,tx_nettax,tx_slno,tx_active,tx_remarks from tax where  tx_code='" + this.Code + "'";
                DataTable dtData = mGlobal.LocalDBCon.ExecuteQuery(SQL);
                if (mclsCFunc.ConvertToInt(dtData.Rows.Count) > 0)
                {
                    this.Code = mclsCFunc.ConvertToString(dtData.Rows[0]["tx_code"]);
                    this.Desc = mclsCFunc.ConvertToString(dtData.Rows[0]["tx_desc"]);
                    this.Mode = mclsCFunc.ConvertToString(dtData.Rows[0]["tx_mode"]);
                    this.Taxper = mclsCFunc.ConvertToNumber_Dec(dtData.Rows[0]["tx_taxper"]);
                    this.Ast1desc = mclsCFunc.ConvertToString(dtData.Rows[0]["tx_ast1desc"]);
                    this.Ast1per = mclsCFunc.ConvertToNumber_Dec(dtData.Rows[0]["tx_ast1per"]);
                    this.Ast2desc = mclsCFunc.ConvertToString(dtData.Rows[0]["tx_ast2desc"]);
                    this.Ast2per = mclsCFunc.ConvertToNumber_Dec(dtData.Rows[0]["tx_ast2per"]);
                    this.Nettax = mclsCFunc.ConvertToNumber_Dec(dtData.Rows[0]["tx_nettax"]);
                    this.Slno = mclsCFunc.ConvertToInt(dtData.Rows[0]["tx_slno"]);
                    this.Active = mclsCFunc.ConvertToString(dtData.Rows[0]["tx_active"]);
                    this.Remarks = mclsCFunc.ConvertToString(dtData.Rows[0]["tx_remarks"]);
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
                SQL = " select tx_code,tx_desc,tx_mode,tx_taxper,tx_ast1desc,tx_ast1per,tx_ast2desc,tx_ast2per,tx_nettax,tx_slno,tx_active,tx_remarks from tax";
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