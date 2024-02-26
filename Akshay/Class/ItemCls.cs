using System;
using System.Data;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using CsHms;

    public class ItemCls
    {
        CommFuncs mclsCFunc = new CommFuncs();
        Global mGlobal = new Global();
        String mstrCode;
        String mstrDesc;
        String mstrShortnm;
        String mstrDeptptr;
        String mstrGroupptr;
        String mstrTaxptr;
        String mstrTaxinclusive;
        String mstrDrcutsmode;
        String mstrRefcutsmode;
        String mstrIsfixedrate;
        Decimal mdecCost;
        String mstrPackage;
        Int32 mintSlno;
        String mstrActive;
        String mstrRemarks;
        DateTime mdateDt;
        String SQL;

        public void ClearAll(bool _clearCode)
        {
            if (_clearCode)
                mstrCode = "";
            mstrDesc = "";
            mstrShortnm = "";
            mstrDeptptr = "";
            mstrGroupptr = "";
            mstrTaxptr = "";
            mstrTaxinclusive = "";
            mstrDrcutsmode = "";
            mstrRefcutsmode = "";
            mstrIsfixedrate = "";
            mdecCost = 0;
            mstrPackage = "";
            mintSlno = 0;
            mstrActive = "";
            mstrRemarks = "";
            mdateDt = DateTime.Now;
            SQL = "";
        }
        public Decimal getRate(string strCatPtr)
        {
            try
            {
                SQL = " select ir_id,ir_itemptr,ir_ratecatptr,ir_rate from itemrate where   ir_itemptr='" + this.Code + "' and ir_ratecatptr='" + strCatPtr + "'";
                DataTable dtData = mGlobal.LocalDBCon.ExecuteQuery(SQL);
                if (mclsCFunc.ConvertToInt(dtData.Rows.Count) > 0)
                {

                    return mclsCFunc.ConvertToNumber_Dec(dtData.Rows[0]["ir_rate"]);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
            return 0;
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
        public String Shortnm
        {
            set { mstrShortnm = value; }
            get { return mstrShortnm; }
        }
        public String Deptptr
        {
            set { mstrDeptptr = value; }
            get { return mstrDeptptr; }
        }
        public String Groupptr
        {
            set { mstrGroupptr = value; }
            get { return mstrGroupptr; }
        }
        public String Taxptr
        {
            set { mstrTaxptr = value; }
            get { return mstrTaxptr; }
        }
        public String Taxinclusive
        {
            set { mstrTaxinclusive = value; }
            get { return mstrTaxinclusive; }
        }
        public String Drcutsmode
        {
            set { mstrDrcutsmode = value; }
            get { return mstrDrcutsmode; }
        }
        public String Refcutsmode
        {
            set { mstrRefcutsmode = value; }
            get { return mstrRefcutsmode; }
        }
        public String Isfixedrate
        {
            set { mstrIsfixedrate = value; }
            get { return mstrIsfixedrate; }
        }
        public Decimal Cost
        {
            set { mdecCost = value; }
            get { return mdecCost; }
        }
        public String Package
        {
            set { mstrPackage = value; }
            get { return mstrPackage; }
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
        public bool insertData()
        {
            try
            {
                SQL = "insert into item(itm_code,itm_desc,itm_shortnm,itm_deptptr,itm_groupptr,itm_taxptr,itm_taxinclusive,itm_drcutsmode,itm_refcutsmode,itm_isfixedrate,itm_cost,itm_package,itm_slno,itm_active,itm_remarks,cngd_dt) values ('" + this.Code + "','" + this.Desc + "','" + this.Shortnm + "','" + this.Deptptr + "','" + this.Groupptr + "','" + this.Taxptr + "','" + this.Taxinclusive + "','" + this.Drcutsmode + "','" + this.Refcutsmode + "','" + this.Isfixedrate + "'," + this.Cost + ",'" + this.Package + "'," + this.Slno + ",'" + this.Active + "','" + this.Remarks + "'," + mclsCFunc.FormatDBDate(this.Dt.ToString()) + ")";
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
                SQL = "update   item set itm_code='" + this.Code + "',itm_desc='" + this.Desc + "',itm_shortnm='" + this.Shortnm + "',itm_deptptr='" + this.Deptptr + "',itm_groupptr='" + this.Groupptr + "',itm_taxptr='" + this.Taxptr + "',itm_taxinclusive='" + this.Taxinclusive + "',itm_drcutsmode='" + this.Drcutsmode + "',itm_refcutsmode='" + this.Refcutsmode + "',itm_isfixedrate='" + this.Isfixedrate + "',itm_cost=" + this.Cost + ",itm_package='" + this.Package + "',itm_slno=" + this.Slno + ",itm_active='" + this.Active + "',itm_remarks='" + this.Remarks + "',cngd_dt=" + mclsCFunc.FormatDBDate(this.Dt.ToString()) + " where itm_code='" + this.Code + "'";
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
                SQL = "delete  from   item  where itm_code='" + this.Code + "'";
                if (mGlobal.LocalDBCon.ExecuteNonQuery(SQL) > 0)
                    return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
            return false;
        }
        public bool getData(string strItemGrp)
        {
            try
            {
                ClearAll(false);
                SQL = @" select itm_code,itm_desc,itm_shortnm,itm_deptptr,itm_groupptr,itm_taxptr,itm_taxinclusive,itm_drcutsmode,itm_refcutsmode,
                itm_isfixedrate,itm_cost,itm_package,itm_slno,itm_active,itm_remarks,cngd_dt from item where  itm_code='" + this.Code + "'";
                if (strItemGrp.Trim().Length > 0)
                    SQL += " and  " + strItemGrp;
                DataTable dtData = mGlobal.LocalDBCon.ExecuteQuery(SQL);
                if (mclsCFunc.ConvertToInt(dtData.Rows.Count) > 0)
                {
                    this.Code = mclsCFunc.ConvertToString(dtData.Rows[0]["itm_code"]);
                    this.Desc = mclsCFunc.ConvertToString(dtData.Rows[0]["itm_desc"]);
                    this.Shortnm = mclsCFunc.ConvertToString(dtData.Rows[0]["itm_shortnm"]);
                    this.Deptptr = mclsCFunc.ConvertToString(dtData.Rows[0]["itm_deptptr"]);
                    this.Groupptr = mclsCFunc.ConvertToString(dtData.Rows[0]["itm_groupptr"]);
                    this.Taxptr = mclsCFunc.ConvertToString(dtData.Rows[0]["itm_taxptr"]);
                    this.Taxinclusive = mclsCFunc.ConvertToString(dtData.Rows[0]["itm_taxinclusive"]);
                    this.Drcutsmode = mclsCFunc.ConvertToString(dtData.Rows[0]["itm_drcutsmode"]);
                    this.Refcutsmode = mclsCFunc.ConvertToString(dtData.Rows[0]["itm_refcutsmode"]);
                    this.Isfixedrate = mclsCFunc.ConvertToString(dtData.Rows[0]["itm_isfixedrate"]);
                    this.Cost = mclsCFunc.ConvertToNumber_Dec(dtData.Rows[0]["itm_cost"]);
                    this.Package = mclsCFunc.ConvertToString(dtData.Rows[0]["itm_package"]);
                    this.Slno = mclsCFunc.ConvertToInt(dtData.Rows[0]["itm_slno"]);
                    this.Active = mclsCFunc.ConvertToString(dtData.Rows[0]["itm_active"]);
                    this.Remarks = mclsCFunc.ConvertToString(dtData.Rows[0]["itm_remarks"]);
                    this.Dt = mclsCFunc.FormatDate(dtData.Rows[0]["cngd_dt"].ToString(), "dd/MM/yyyy");
                    return true;
                }
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
                ClearAll(false);
                SQL = @" select itm_code,itm_desc,itm_shortnm,itm_deptptr,itm_groupptr,itm_taxptr,itm_taxinclusive,itm_drcutsmode,itm_refcutsmode,
                itm_isfixedrate,itm_cost,itm_package,itm_slno,itm_active,itm_remarks,cngd_dt from item where  itm_code='" + this.Code + "'";
                //if (strItemGrp.Trim().Length > 0)
                //    SQL += " and itm_groupptr in(" + strItemGrp + ")";
                DataTable dtData = mGlobal.LocalDBCon.ExecuteQuery(SQL);
                if (mclsCFunc.ConvertToInt(dtData.Rows.Count) > 0)
                {
                    this.Code = mclsCFunc.ConvertToString(dtData.Rows[0]["itm_code"]);
                    this.Desc = mclsCFunc.ConvertToString(dtData.Rows[0]["itm_desc"]);
                    this.Shortnm = mclsCFunc.ConvertToString(dtData.Rows[0]["itm_shortnm"]);
                    this.Deptptr = mclsCFunc.ConvertToString(dtData.Rows[0]["itm_deptptr"]);
                    this.Groupptr = mclsCFunc.ConvertToString(dtData.Rows[0]["itm_groupptr"]);
                    this.Taxptr = mclsCFunc.ConvertToString(dtData.Rows[0]["itm_taxptr"]);
                    this.Taxinclusive = mclsCFunc.ConvertToString(dtData.Rows[0]["itm_taxinclusive"]);
                    this.Drcutsmode = mclsCFunc.ConvertToString(dtData.Rows[0]["itm_drcutsmode"]);
                    this.Refcutsmode = mclsCFunc.ConvertToString(dtData.Rows[0]["itm_refcutsmode"]);
                    this.Isfixedrate = mclsCFunc.ConvertToString(dtData.Rows[0]["itm_isfixedrate"]);
                    this.Cost = mclsCFunc.ConvertToNumber_Dec(dtData.Rows[0]["itm_cost"]);
                    this.Package = mclsCFunc.ConvertToString(dtData.Rows[0]["itm_package"]);
                    this.Slno = mclsCFunc.ConvertToInt(dtData.Rows[0]["itm_slno"]);
                    this.Active = mclsCFunc.ConvertToString(dtData.Rows[0]["itm_active"]);
                    this.Remarks = mclsCFunc.ConvertToString(dtData.Rows[0]["itm_remarks"]);
                    this.Dt = mclsCFunc.FormatDate(dtData.Rows[0]["cngd_dt"].ToString(), "dd/MM/yyyy");
                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
            return false;
        }
        public bool getDataByItemGroup(string strItemGrp)
        {
            try
            {
                ClearAll(false);
                SQL = @" select itm_code,itm_desc,itm_shortnm,itm_deptptr,itm_groupptr,itm_taxptr,itm_taxinclusive,itm_drcutsmode,itm_refcutsmode,
                itm_isfixedrate,itm_cost,itm_package,itm_slno,itm_active,itm_remarks,cngd_dt from item where  itm_code='" + this.Code + "' and  itm_groupptr in(" + strItemGrp + ")";
                DataTable dtData = mGlobal.LocalDBCon.ExecuteQuery(SQL);
                if (mclsCFunc.ConvertToInt(dtData.Rows.Count) > 0)
                {
                    this.Code = mclsCFunc.ConvertToString(dtData.Rows[0]["itm_code"]);
                    this.Desc = mclsCFunc.ConvertToString(dtData.Rows[0]["itm_desc"]);
                    this.Shortnm = mclsCFunc.ConvertToString(dtData.Rows[0]["itm_shortnm"]);
                    this.Deptptr = mclsCFunc.ConvertToString(dtData.Rows[0]["itm_deptptr"]);
                    this.Groupptr = mclsCFunc.ConvertToString(dtData.Rows[0]["itm_groupptr"]);
                    this.Taxptr = mclsCFunc.ConvertToString(dtData.Rows[0]["itm_taxptr"]);
                    this.Taxinclusive = mclsCFunc.ConvertToString(dtData.Rows[0]["itm_taxinclusive"]);
                    this.Drcutsmode = mclsCFunc.ConvertToString(dtData.Rows[0]["itm_drcutsmode"]);
                    this.Refcutsmode = mclsCFunc.ConvertToString(dtData.Rows[0]["itm_refcutsmode"]);
                    this.Isfixedrate = mclsCFunc.ConvertToString(dtData.Rows[0]["itm_isfixedrate"]);
                    this.Cost = mclsCFunc.ConvertToNumber_Dec(dtData.Rows[0]["itm_cost"]);
                    this.Package = mclsCFunc.ConvertToString(dtData.Rows[0]["itm_package"]);
                    this.Slno = mclsCFunc.ConvertToInt(dtData.Rows[0]["itm_slno"]);
                    this.Active = mclsCFunc.ConvertToString(dtData.Rows[0]["itm_active"]);
                    this.Remarks = mclsCFunc.ConvertToString(dtData.Rows[0]["itm_remarks"]);
                    this.Dt = mclsCFunc.FormatDate(dtData.Rows[0]["cngd_dt"].ToString(), "dd/MM/yyyy");
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


        public string searchItem(string strDfltSearchValue, string strGrpPtr)
        {
            try
            {

                // Search Settings 
                Search Searchfrm = new Search();

                Searchfrm.DefSearchFldIndex = 1;
                Searchfrm.DefSearchText = strDfltSearchValue;
                Searchfrm.Query = "select itm_code,itm_desc  from item ";
                Searchfrm.FilterCond = "itm_active='Y'";
                if (strGrpPtr.Trim() != "")
                    Searchfrm.FilterCond += " and " + strGrpPtr;
                Searchfrm.ReturnFldIndex = 0;
                Searchfrm.ColumnHeader = "Code|Name";
                Searchfrm.ColumnWidth = "130|330";
                Searchfrm.ShowDialog();
                // Search value returns
                if (!Searchfrm.Cancelled) // whether not clicked on cancel button in search screen
                    return Searchfrm.ReturnValue;

            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
            return "";
        }

        public DataTable getDataList(string strConditionSql)
        {
            try
            {
                SQL = " select itm_code,itm_desc,itm_shortnm,itm_deptptr,itm_groupptr,itm_taxptr,itm_taxinclusive,itm_drcutsmode,itm_refcutsmode,itm_isfixedrate,itm_cost,itm_package,itm_slno,itm_active,itm_remarks,cngd_dt from item";
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
