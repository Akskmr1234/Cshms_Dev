using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Windows.Forms;
namespace CsHms
{
    class repackproductclass
    {
        Global mGlobal = new Global();// Global data     
        CommFuncs mCommFuncs = new CommFuncs();// Common Function library
        UsrRights1 mUsrRight = new UsrRights1();//User Rights
        TableHdr clsTblHdr = new TableHdr();
        ItemTrn clsItemTrn = new ItemTrn();
        DataGridView dgvItemTrn;
        string mstrStaffPtr;
        int mintEntryNo;
        DateTime mdtEntryDate;
        string mstrUserId;
        int mintTrnType;
        DataTable mdtTrnData;
        string mstrTableName="repackproduct";
        
        public repackproductclass()
        {
            mstrStaffPtr = "";
            mintEntryNo = 0;
            mdtEntryDate=DateTime.Now ;
            mstrUserId = "";
            mintTrnType = -1;             
        }
        public string StaffPtr
        {
            get { return mstrStaffPtr; }
            set { mstrStaffPtr = value; }
        }
        public string UserId
        {
            get { return mstrUserId; }
            set { mstrUserId = value; }
        }
        public int  EntryNo
        {
            get { return mintEntryNo; }
            set { mintEntryNo = value; }
        }
        public DateTime  EntryDate
        {
            get { return mdtEntryDate; }
            set { mdtEntryDate = value; }
        }
        void createTablestructure()
        {
            mdtTrnData = mGlobal.LocalDBCon.ExecuteQuery("select * from  " + mstrTableName + " where 1=2");
        }        
       public  bool insertRawMaterialData(DataGridView dgvData, int intTrnType,
           String strBalItemCode,double dblBalUnit,string strDmgItemPtr,double dblDmgUnit)
        {
            createTablestructure();
            foreach(DataGridViewRow dgvRow in dgvData.Rows)
            {
                DataRow drNew = mdtTrnData.NewRow();
                drNew["rp_entryno"] = this.EntryNo;
                drNew["rp_date"] = this.EntryDate;
                drNew["rp_sysdate"] = DateTime.Now;
                drNew["rp_staffptr"] = this.StaffPtr;
                drNew["rp_transtype"] = intTrnType;
                drNew["rp_user"] = this.UserId;
                drNew["rp_itemptr"] = dgvRow.Cells["Code"].Value.ToString();
               // double dblTemp = 0;
                drNew["rp_qty"] = dgvRow.Cells["quantity"].Value.ToString();
                if (dgvRow.Cells["Code"].Value.ToString() == strBalItemCode && dblBalUnit>0)
                    drNew["rp_balanceunit"] = dblBalUnit.ToString();
                else
                    drNew["rp_balanceunit"] = "0";
                if (dgvRow.Cells["Code"].Value.ToString() == strDmgItemPtr && dblDmgUnit > 0)
                    drNew["rp_damagedunit"] = dblDmgUnit.ToString();
                else
                    drNew["rp_damagedunit"] = "0";
               
                drNew["rp_rate"] = dgvRow.Cells["itemrate"].Value.ToString();
                drNew["rp_totunit"] = dgvRow.Cells["totunit"].Value.ToString();
                mdtTrnData.Rows.Add(drNew);
            }
            int intRnt= mGlobal.LocalDBCon.UpdateDataTable("select * from  " + mstrTableName + " where 1=2", mdtTrnData);
            return intRnt ==1 ? true : false;
        }
       public  bool insertProductData(DataGridView dgvData, int intTrnType)
        {
            createTablestructure();
            foreach (DataGridViewRow dgvRow in dgvData.Rows)
            {
                DataRow drNew = mdtTrnData.NewRow();
                drNew["rp_entryno"] = this.EntryNo;
                drNew["rp_date"] = this.EntryDate;
                drNew["rp_sysdate"] = DateTime.Now;
                drNew["rp_staffptr"] = this.StaffPtr;
                drNew["rp_transtype"] = intTrnType;
                drNew["rp_user"] = this.UserId;
                drNew["rp_itemptr"] = dgvRow.Cells["Codep"].Value.ToString();
                drNew["rp_qty"] = dgvRow.Cells["qtyp"].Value.ToString();
                drNew["rp_balanceunit"] = "0";               
                drNew["rp_damagedunit"] = "0";
                drNew["rp_rate"] = dgvRow.Cells["itemratep"].Value.ToString();
                drNew["rp_totunit"] = dgvRow.Cells["totunitp"].Value.ToString();
                mdtTrnData.Rows.Add(drNew);
            }
            int intRnt = mGlobal.LocalDBCon.UpdateDataTable("select * from  " + mstrTableName + " where 1=2", mdtTrnData);
            return intRnt == 1 ? true : false;
        }
       public void saveSalesHeaderDataandTransaction(string strDmgItemPtr,double dblDmgQtyUnit,
           string strBalItemPtr,double dblBalItemUnit,double dblBalItemBaseUnit)
       {
            string strBillno= clsTblHdr.getBillNo_MaxNumber();  
            clsTblHdr.HdrInit("saleshdr","slh_id", "0",true);
            // Fill Header Data
            clsTblHdr.setData("slh_dt", this.EntryDate.Date.ToShortDateString());
            clsTblHdr.setData("slh_tm", DateTime.Now.ToShortTimeString());
            clsTblHdr.setData("slh_sdt", mCommFuncs.FormatDBServDate(EntryDate.Date.ToShortDateString(), true));
            clsTblHdr.setData("slh_stm", DateTime.Now.ToShortTimeString());
            clsTblHdr.setData("slh_sltype",0);
            clsTblHdr.setData("slh_brptr", mGlobal.BranchCode);
            clsTblHdr.setData("slh_tobrptr",  mGlobal.BranchCode);
            clsTblHdr.setData("slh_custptr", "D");//direct
            clsTblHdr.setData("slh_salesmanptr", this.StaffPtr);            
            clsTblHdr.setData("slh_total", 0);
            clsTblHdr.setData("slh_add", 0);
            clsTblHdr.setData("slh_less",0);
            clsTblHdr.setData("slh_net", 0);
            clsTblHdr.setData("slh_remarks","Repack No .: " + this.EntryNo.ToString());
            clsTblHdr.setData("slh_user", this.UserId);
            clsItemTrn.TransactionType = ((int)(0+ 1)).ToString();
            clsItemTrn.TransactionDate =  this.EntryDate.Date.ToShortDateString();
            if (clsTblHdr.SaveData("S"))
            {
                saveTransactionData("S", clsTblHdr.LastIdentityNo , mdtTrnData,
                    "rp_itemptr", "rp_qty", "rp_rate", strDmgItemPtr,
                    dblDmgQtyUnit,strBalItemPtr,dblBalItemUnit,dblBalItemBaseUnit);
            }
       }
        public void savePurchaseHeaderDataandTransaction()
        {

            string strBillno = clsTblHdr.getGRN_MaxNumber();
            clsTblHdr.HdrInit("purchasehdr", "puh_id", "0", true);
            // Fill Header Data
            clsTblHdr.setData("puh_dt", this.EntryDate.Date.ToShortDateString());
            clsTblHdr.setData("puh_tm", DateTime.Now.ToShortTimeString());
            clsTblHdr.setData("puh_sdt", mCommFuncs.FormatDBServDate(EntryDate.Date.ToShortDateString(), true));
            clsTblHdr.setData("puh_stm", DateTime.Now.ToShortTimeString());
            clsTblHdr.setData("puh_putype", 0);
            clsTblHdr.setData("puh_brptr", mGlobal.BranchCode);
            clsTblHdr.setData("puh_frombrptr", mGlobal.BranchCode);
            clsTblHdr.setData("puh_billno",  this.EntryNo.ToString());
            clsTblHdr.setData("puh_billdt",  this.EntryDate.Date.ToShortDateString());
            clsTblHdr.setData("puh_sbilldt", mCommFuncs.FormatDBServDate(EntryDate.Date.ToShortDateString(), true));
            clsTblHdr.setData("puh_supptr", "D");//direct            
            clsTblHdr.setData("puh_total", 0);
            clsTblHdr.setData("puh_add", 0);
            clsTblHdr.setData("puh_less", 0);
            clsTblHdr.setData("puh_net", 0);
            clsTblHdr.setData("puh_remarks", "Repack No .: " + this.EntryNo.ToString());
            clsTblHdr.setData("puh_user", this.UserId);
            clsItemTrn.TransactionType = ((int)(0 + 1)).ToString();
            clsItemTrn.TransactionDate = this.EntryDate.Date.ToShortDateString();
            if (clsTblHdr.SaveData("P"))
            {
                saveTransactionData("P", clsTblHdr.LastIdentityNo, mdtTrnData, 
                    "rp_itemptr", "rp_qty", "rp_rate","",0,"",0,0);
            }
        }
        public String getTrnNo_MaxNumber()
        {
            DataTable dtData = mGlobal.LocalDBCon.ExecuteQuery("select max(rp_entryno) from " + mstrTableName);
            Decimal decRet = 1;
            decRet += Decimal.Parse(mCommFuncs.ConvertToNumberObj(dtData.Rows[0][0].ToString()).ToString());
            return (decRet.ToString());
        }

        void saveTransactionData(string strFormMode,int intHdrIdentityNo,
            DataTable dtTrnData,string strItemPtrColName,string strQtyColName,string strRateColName,
            string strDmgItemPtr,double dblDmgUnit,string strBalItemPtr,double dblBalItemUnit,double dblBalItemBaseUnit)
        {
            
           
            String strSql = "select * from itemtran where itn_trnmode='" + strFormMode + "' and itn_hdrid=" + intHdrIdentityNo;
            DataTable dtRet = mGlobal.LocalDBCon.ExecuteQuery(strSql);
            String strPrimaryKey = "itn_id";
            DataRow dr = null;
            int intIndex = -1;
            try
            {
                dtRet.Columns[strPrimaryKey].AutoIncrement = true;
                DataColumn[] dcPrimaryKey = { dtRet.Columns[strPrimaryKey] };
                dtRet.PrimaryKey = dcPrimaryKey;
                for (int intRow = 0; intRow < dtTrnData.Rows.Count; intRow++)
                {                    
                    dr = dtRet.NewRow();
                    dtRet.Rows.Add(dr);
                    intIndex = dtRet.Rows.IndexOf(dr);
                    dtRet.Rows[intIndex]["itn_hdrid"] = intHdrIdentityNo;
                    dtRet.Rows[intIndex]["itn_trndt"] = this.EntryDate.Date;
                    dtRet.Rows[intIndex]["itn_trntm"] = DateTime.Now.ToShortTimeString();
                    dtRet.Rows[intIndex]["itn_strndt"] = mCommFuncs.FormatDBServDate(this.EntryDate.Date.ToShortDateString() , true);
                    dtRet.Rows[intIndex]["itn_net"] = 0;
                    dtRet.Rows[intIndex]["itn_trnmode"] = strFormMode;
                    dtRet.Rows[intIndex]["itn_brptr"] = mGlobal.BranchCode;                    
                    dtRet.Rows[intIndex]["itn_freeqty"] = 0;
                    dtRet.Rows[intIndex]["itn_freevalue"] = 0;
                    dtRet.Rows[intIndex]["itn_prodptr"] = dtTrnData.Rows[intRow][strItemPtrColName].ToString();
                    dtRet.Rows[intIndex]["itn_qty"] = dtTrnData.Rows[intRow][strQtyColName].ToString();
                    dtRet.Rows[intIndex]["itn_rsalesrt"] = dtTrnData.Rows[intRow][strRateColName].ToString();
                    dtRet.Rows[intIndex]["itn_trnrt"] = 0;
                    if (strFormMode == "S" && dtTrnData.Rows[intRow][strItemPtrColName].ToString() == strDmgItemPtr && dblDmgUnit > 0)
                        dtRet.Rows[intIndex]["itn_dmgunit"] = dblDmgUnit;
                    else
                        dtRet.Rows[intIndex]["itn_dmgunit"] = 0;

                    if (strFormMode == "S" && dtTrnData.Rows[intRow][strItemPtrColName].ToString()
                        == strBalItemPtr && dblBalItemUnit > 0)
                    {
                        double dblTemp = dblBalItemUnit / dblBalItemBaseUnit;
                        dblTemp = Math.Round(dblTemp, 2);
                        dtRet.Rows[intIndex]["itn_qty"] =mCommFuncs.ConvertToNumber_Double(
                            dtTrnData.Rows[intRow][strQtyColName].ToString())-dblTemp;
                    }
                    switch (strFormMode)
                    {
                        case "P"://Purchase
                                dtRet.Rows[intIndex]["itn_trntype"] = 1;
                            break;
                        case "S"://Sales whole sale rate
                                dtRet.Rows[intIndex]["itn_trntype"] = 11;
                            break;
                        default:
                            break;
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
            mGlobal.LocalDBCon.UpdateDataTable("select * from itemtran where 1=2", dtRet);
        

        }
    }
}
