using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Data;
namespace CsHms 
{
   
    class stockadjustmentclass
    {
        Global mGlobal = new Global();
        CommFuncs mComm = new CommFuncs();
        public bool mboolErrorOccured = false;
       public  String newTrnNumber()
        {
            object objRtn = mGlobal.LocalDBCon.ExecuteScalar("select max(itn_hdrid) from  " +
                " itemtran where itn_trnmode='A'");
            if (objRtn.ToString() == "")
                return "1";
            else
            {
                return Convert.ToString(mComm.ConvertToNumber_Double(objRtn) + 1);
            }


        }
        public Decimal getProductStock(String _ProdPtr,String _BrPtr)
        {
            Decimal decRet = mComm.ConvertToNumber_Dec((mGlobal.LocalDBCon.ExecuteScalar(" exec ProductStock " + _ProdPtr + "," + _BrPtr).ToString()));
            return (decRet);
        }
        private DataTable GetDataTable_ForSave(Int32 _HdrIdentityNo,string strTransactionDate,
            ref DataGridView _dgvData)
        {   // Read all needed data from datagridview & add additional data if needed(like totals)            
            DataTable dtGrid = null;
            String strSql = "select * from itemtran where itn_trnmode='A' and itn_hdrid=" + _HdrIdentityNo;
            DataTable dtRet = mGlobal.LocalDBCon.ExecuteQuery(strSql);
            String strPrimaryKey = "itn_id";
            DataRow dr = null;
            int intIndex = -1;

            try
            {
                dtRet.Columns[strPrimaryKey].AutoIncrement = true;
                DataColumn[] dcPrimaryKey = { dtRet.Columns[strPrimaryKey] };
                dtRet.PrimaryKey = dcPrimaryKey;

                for (int intRow = 0; intRow < _dgvData.Rows.Count; intRow++)
                {
                    dtRet.Rows.Add(dtRet.NewRow());
                    intIndex = dtRet.Rows.Count-1;
                    dtRet.Rows[intIndex]["itn_hdrid"] = _HdrIdentityNo;
                    dtRet.Rows[intIndex]["itn_trndt"] = strTransactionDate;
                    dtRet.Rows[intIndex]["itn_trntm"] = DateTime.Now.ToShortTimeString();
                    dtRet.Rows[intIndex]["itn_prodptr"] = _dgvData.Rows[intRow].Cells["productcode"].Value.ToString();
                    dtRet.Rows[intIndex]["itn_wrsale"] = "";
                    dtRet.Rows[intIndex]["itn_purrt"] = _dgvData.Rows[intRow].Cells["purchaserate"].Value.ToString();
                    dtRet.Rows[intIndex]["itn_landrt"] = _dgvData.Rows[intRow].Cells["landingrate"].Value.ToString();
                    dtRet.Rows[intIndex]["itn_costrt"] = _dgvData.Rows[intRow].Cells["costrate"].Value.ToString();
                    dtRet.Rows[intIndex]["itn_wsalesrt"] = _dgvData.Rows[intRow].Cells["wholesalerate"].Value.ToString();
                    dtRet.Rows[intIndex]["itn_rsalesrt"] = _dgvData.Rows[intRow].Cells["retailrate"].Value.ToString();
                    dtRet.Rows[intIndex]["itn_ssalesrt"] = 0;
                    dtRet.Rows[intIndex]["itn_strndt"] = mComm.FormatDBServDate(strTransactionDate, true);
                    dtRet.Rows[intIndex]["itn_net"] = 0;
                    dtRet.Rows[intIndex]["itn_trnmode"] = "A";
                    dtRet.Rows[intIndex]["itn_brptr"] = mGlobal.BranchCode;
                    dtRet.Rows[intIndex]["itn_canflg"] = "N";
                    dtRet.Rows[intIndex]["itn_freeqty"] = 0;
                    dtRet.Rows[intIndex]["itn_freevalue"] = 0;
                    dtRet.Rows[intIndex]["itn_trnrt"] = mComm.ConvertToNumber_Dec(
                        _dgvData.Rows[intRow].Cells["amt"].Value.ToString()) /
                        mComm.ConvertToNumber_Dec(_dgvData.Rows[intRow].Cells["qty"].Value.ToString());

                    if (_dgvData.Rows[intRow].Cells["addorless"].Value.ToString() == "Add")
                    {
                        dtRet.Rows[intIndex]["itn_trntype"] = 7; // stock add                       
                    }
                    else
                    {
                        dtRet.Rows[intIndex]["itn_trntype"] = 17; //// stock Less                       
                    }
                    dtRet.Rows[intIndex]["itn_qty"] = _dgvData.Rows[intRow].Cells["qty"].Value.ToString();
                    dtRet.Rows[intIndex]["itn_amt"] = _dgvData.Rows[intRow].Cells["amt"].Value.ToString();
                    dtRet.Rows[intIndex]["itn_net"] = _dgvData.Rows[intRow].Cells["amt"].Value.ToString();

                     dtRet.Rows[intIndex]["itn_remarks"]=_dgvData.Rows[intRow].Cells["remarks"].Value.ToString();
 
                }
            }
            catch (Exception ex) { mboolErrorOccured = true; MessageBox.Show(ex.Message); }
            return dtRet;
        }
        public bool SaveData(Int32 _HdrIdentityNo, ref DataGridView _dgvData,string strTrnDate)
        {
            mboolErrorOccured = false;
            int intCnt = mGlobal.LocalDBCon.UpdateDataTable("select * from itemtran where 1=2", GetDataTable_ForSave(_HdrIdentityNo,strTrnDate ,ref _dgvData));
            if (intCnt > 0 && (!mboolErrorOccured))
                return true;
            return false;
        }
    }
    


}
