using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Windows.Forms;
namespace CsHms 
{
    

    class stocktransferclass
    {
        Global mGlobal = new Global();
        CommFuncs mCommFunc = new CommFuncs();
        public bool  saveTransferDetails(decimal decTransferNo,DateTime TrnDt,string strFromBrPtr,
            string strToBrPtr,int intStatus)
        {
            try
            {
                string strSql = "select *  from stocktransfer where trn_trnno =" + decTransferNo;
                DataTable dtData = mGlobal.LocalDBCon.ExecuteQuery(strSql);
                if (dtData == null)
                    return false;
                dtData.Rows.Add(dtData.NewRow());
                dtData.Rows[0]["trn_trnno"] = decTransferNo;
                dtData.Rows[0]["trn_dt"] = TrnDt.Date;
                dtData.Rows[0]["trn_frombrptr"] = strFromBrPtr;
                dtData.Rows[0]["trn_tobrptr"] = strToBrPtr;
                dtData.Rows[0]["trn_status"] = intStatus;
                mGlobal.LocalDBCon.UpdateDataTable(strSql, dtData);
                return true;
            }
            catch { }
            return false;
        }
        public bool saveReceivedTransferDetails(decimal decTransferNo, decimal decRctTransferNo,DateTime TrnDt,
            int intStatus)
        {
            try
            {
                string strSql = "select *  from stocktransfer where trn_trnno =" + decTransferNo;
                DataTable dtData = mGlobal.LocalDBCon.ExecuteQuery(strSql);
                if (dtData == null)
                    return false;
                dtData.Rows[0]["trn_rctno"] = decRctTransferNo;
                dtData.Rows[0]["trn_rctdt"] = TrnDt;
                dtData.Rows[0]["trn_status"] = intStatus;
                mGlobal.LocalDBCon.UpdateDataTable(strSql, dtData);
                return true;
            }
            catch { }
            return false;
        }
        public  DataTable  loadTransferData(decimal decTrnNo)
        {
            try
            {
                string strSql = "select slh_brptr,itn_prodptr,itn_qty,itn_purrt,itn_landrt,itn_freeqty,itn_trnrt from itemtran,saleshdr where " +
                    " itn_hdrid=slh_id and itn_trntype=16 and slh_id=" + decTrnNo;
                DataTable dtData = mGlobal.LocalDBCon.ExecuteQuery(strSql);

                return dtData;
            }
            catch { }
            return null;
        }

        public bool setTransferData(DataGridView dgvData,DataTable dtData)
        {
            try
            {
                DataTable dtMaster =(DataTable) dgvData.DataSource;
                int intCnt = 0;
                foreach (DataRow dr in dtData.Rows)
                {
                    dtMaster.Rows.Add(dtMaster.NewRow());                    
                    dtMaster.Rows[intCnt]["itn_prodptr"]=   dr["itn_prodptr"].ToString();
                    dtMaster.Rows[intCnt]["itn_costrt"] = dr["itn_trnrt"].ToString();
                    dtMaster.Rows[intCnt]["itn_qty"] = dr["itn_qty"].ToString();                    
                    dtMaster.Rows[intCnt]["itn_freeqty"] = dr["itn_freeqty"].ToString();
                    intCnt +=  1;
                }
                dgvData.DataSource = dtMaster;
                dgvData.Refresh();
                intCnt=0;
                foreach (DataRow dr in dtData.Rows)
                {
                    dgvData.CurrentCell = dgvData.Rows[intCnt].Cells["itn_prodptr"];
                    dgvData.BeginEdit(true);
                    dgvData.Rows[intCnt].Cells["itn_prodptr"].Value= dgvData.Rows[intCnt].Cells["itn_prodptr"].Value;
                    dgvData.EndEdit();
                    dgvData.CurrentCell = dgvData.Rows[intCnt].Cells["itn_costrt"];
                    dgvData.BeginEdit(true);
                    dgvData.Rows[intCnt].Cells["itn_costrt"].Value = dr["itn_trnrt"].ToString();
                    dgvData.EndEdit();
                    dgvData.CurrentCell = dgvData.Rows[intCnt].Cells["itn_qty"];
                    dgvData.BeginEdit(true);
                    dgvData.Rows[intCnt].Cells["itn_qty"].Value = dgvData.Rows[intCnt].Cells["itn_qty"].Value;
                    dgvData.EndEdit();
                    intCnt += 1;
                }

                return true;
               
            }
            catch { }
            return false;
        }


    }
}
