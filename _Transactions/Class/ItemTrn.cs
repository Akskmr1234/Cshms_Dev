using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Windows.Forms;

namespace CsHms
{
    class ItemTrn
    {
        const int NUMFIELD_PIXEL_LEN = 83;
        const String TRAN_TABLE_NAME = "itemtran";
        const String TRAN_PRIMARY_KEY = "itn_id";
        Global mGlobal = new Global();// Global data     
        CommFuncs mCommFuncs = new CommFuncs();// Common Function library

        private DataTable mdtMain = new DataTable();
        private String mstrSqlMain = "";
        private DataTable mdtItem = new DataTable();
        private DataTable mdtPack = new DataTable();
        private Int32 _HdrLastIdentityNo = -1;
        private DataGridView mdvgGrid = null;

        private Decimal mdecTotQty = 0;
        private Decimal mdecTotAmt = 0;
        private String mstrTransactionDate = DateTime.Now.ToShortDateString();
        private String mstrTransactionType = "1";
        private bool mboolErrorOccurred = false;

        public void Init(String _Table, String _Field1Name, String _Field1Value, String _FromMode, ref DataGridView _dgvData)
        {
            // _FromMode - From which form (P-Purchase, S-Sales)

            if (_Field1Value.Trim() == "")
                _Field1Value = "0";
            mdecTotAmt = 0; mdecTotQty = 0;

            String strSql = "";
            strSql = "select ";
            strSql += "itn_id,itn_prodptr,itn_wrsale,itn_purrt,itn_landrt,itn_wsalesrt,itn_rsalesrt,itn_qty,itn_freeqty,";
            strSql += "itn_freevalue,itn_amt,itn_costrt";
            strSql += " from " + _Table + " where " + _Field1Name + "=" + _Field1Value.Trim();
            mstrSqlMain = strSql;
            mdtMain = mGlobal.LocalDBCon.ExecuteQuery(strSql);
            mdtMain.TableName = _Table;
            mdtItem = mGlobal.LocalDBCon.ExecuteQuery("select pd_code,pd_desc from prodmas order by pd_desc");
            setDataGrid(ref _dgvData, _FromMode);
        }
        public void FillProductDet(String _ProdCode, int _RowIndex, ref DataGridView _dgvData)
        {
            if (_ProdCode.Trim() == "") return;
            String strSql = "";
            strSql = "select pd_purrt,pd_landrt,pd_costrt,pd_wsalesrt,pd_rsalesrt  from prodmas ";
            strSql += " where pd_code='" + _ProdCode + "'";
            DataTable dtProd = mGlobal.LocalDBCon.ExecuteQuery(strSql);
            if (dtProd.Rows.Count > 0)
            {
                _dgvData.Rows[_RowIndex].Cells["itn_purrt"].Value = dtProd.Rows[0]["pd_purrt"];
                _dgvData.Rows[_RowIndex].Cells["itn_landrt"].Value = dtProd.Rows[0]["pd_landrt"];
                _dgvData.Rows[_RowIndex].Cells["itn_wsalesrt"].Value = dtProd.Rows[0]["pd_wsalesrt"];
                _dgvData.Rows[_RowIndex].Cells["itn_rsalesrt"].Value = dtProd.Rows[0]["pd_rsalesrt"];
                _dgvData.Rows[_RowIndex].Cells["itn_costrt"].Value = dtProd.Rows[0]["pd_costrt"];
            }
        }
        public void CalcRowAmt(String _Mode, String _RateMode, int _RowIndex, ref DataGridView _dgvData)
        {
            if (_dgvData.Rows[_RowIndex].Cells["itn_purrt"].Value.ToString() == "")
                _dgvData.Rows[_RowIndex].Cells["itn_purrt"].Value = 0;
            if (_dgvData.Rows[_RowIndex].Cells["itn_qty"].Value.ToString() == "")
                _dgvData.Rows[_RowIndex].Cells["itn_qty"].Value = 0;
            if (_dgvData.Rows[_RowIndex].Cells["itn_wsalesrt"].Value.ToString() == "")
                _dgvData.Rows[_RowIndex].Cells["itn_wsalesrt"].Value = 0;
            if (_dgvData.Rows[_RowIndex].Cells["itn_rsalesrt"].Value.ToString() == "")
                _dgvData.Rows[_RowIndex].Cells["itn_rsalesrt"].Value = 0;
            if (_dgvData.Rows[_RowIndex].Cells["itn_landrt"].Value.ToString() == "")
                _dgvData.Rows[_RowIndex].Cells["itn_landrt"].Value = 0;
            if (_dgvData.Rows[_RowIndex].Cells["itn_amt"].Value == null)
                _dgvData.Rows[_RowIndex].Cells["itn_amt"].Value = 0;
            if (_dgvData.Rows[_RowIndex].Cells["itn_qty"].Value == null)
                _dgvData.Rows[_RowIndex].Cells["itn_qty"].Value = 0;
            if (_dgvData.Rows[_RowIndex].Cells["itn_costrt"].Value == null)
                _dgvData.Rows[_RowIndex].Cells["itn_costrt"].Value = 0;

			if (_Mode == "P")//Purchase
				if (_RateMode == "T")
					_dgvData.Rows[_RowIndex].Cells["itn_amt"].Value =
						Decimal.Parse(_dgvData.Rows[_RowIndex].Cells["itn_costrt"].Value.ToString()) * Decimal.Parse(_dgvData.Rows[_RowIndex].Cells["itn_qty"].Value.ToString());
				else
					_dgvData.Rows[_RowIndex].Cells["itn_amt"].Value =
						Decimal.Parse(_dgvData.Rows[_RowIndex].Cells["itn_purrt"].Value.ToString()) * Decimal.Parse(_dgvData.Rows[_RowIndex].Cells["itn_qty"].Value.ToString());
				else
				{
					if (_RateMode == "SW")
						_dgvData.Rows[_RowIndex].Cells["itn_amt"].Value =
							Decimal.Parse(_dgvData.Rows[_RowIndex].Cells["itn_wsalesrt"].Value.ToString()) * Decimal.Parse(_dgvData.Rows[_RowIndex].Cells["itn_qty"].Value.ToString());
					else if (_RateMode == "SR")
						_dgvData.Rows[_RowIndex].Cells["itn_amt"].Value =
							Decimal.Parse(_dgvData.Rows[_RowIndex].Cells["itn_rsalesrt"].Value.ToString()) * Decimal.Parse(_dgvData.Rows[_RowIndex].Cells["itn_qty"].Value.ToString());
					else if (_RateMode == "ST")
						_dgvData.Rows[_RowIndex].Cells["itn_amt"].Value =
							Decimal.Parse(_dgvData.Rows[_RowIndex].Cells["itn_costrt"].Value.ToString()) * Decimal.Parse(_dgvData.Rows[_RowIndex].Cells["itn_qty"].Value.ToString());
				}
            mdecTotAmt = 0; mdecTotQty = 0;
            for (int intCnt = 0; intCnt < _dgvData.Rows.Count; intCnt++)
            {
                if (_dgvData.Rows[intCnt].Cells["itn_prodptr"].Value != null && _dgvData.Rows[intCnt].Cells["itn_prodptr"].Value.ToString() != "" && _dgvData.Rows[intCnt].Cells["itn_amt"].Value != null && _dgvData.Rows[intCnt].Cells["itn_qty"].Value != null)
                {
                    if (_dgvData.Rows[intCnt].Cells["itn_amt"].Value.ToString().Trim() != "")
                        mdecTotAmt += Decimal.Parse(_dgvData.Rows[intCnt].Cells["itn_amt"].Value.ToString());
                    if (_dgvData.Rows[intCnt].Cells["itn_qty"].Value.ToString().Trim() != "")
                        mdecTotQty += Decimal.Parse(_dgvData.Rows[intCnt].Cells["itn_qty"].Value.ToString());
                }
            }
			mdecTotAmt = Decimal.Round(mdecTotAmt, 3);
			mdecTotQty = Decimal.Round(mdecTotQty, 3);
        }
        public void ShowPacking(String _ItemCode)
        {
            String strSql = "select distinct pp_packptr as pk_code,pk_desc from prodpack,packmas where pk_code=pp_packptr and ";
            strSql += "pp_active<>'false' and pp_prodptr='" + _ItemCode + "'";
            try
            {
                mdtPack = mGlobal.LocalDBCon.ExecuteQuery(strSql);
                mdvgGrid.Refresh();
            }
            catch { }
        }
        private void setDataGrid(ref DataGridView dgvDetails, String _FromMode)
        {
            bool boolPurchase, boolSales;
            if (_FromMode == "P")
            {
                boolPurchase = true; boolSales = false;
            }
            else
            {
                boolPurchase = false; boolSales = true;
            }
            mdvgGrid = dgvDetails;

            #region  Declaring Columns
            //Declaring Columns
            System.Windows.Forms.DataGridViewTextBoxColumn slno = new System.Windows.Forms.DataGridViewTextBoxColumn();
            System.Windows.Forms.DataGridViewTextBoxColumn itn_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            System.Windows.Forms.DataGridViewComboBoxColumn itn_prodptr = new System.Windows.Forms.DataGridViewComboBoxColumn();
            System.Windows.Forms.DataGridViewTextBoxColumn itn_wrsale = new System.Windows.Forms.DataGridViewTextBoxColumn();
            System.Windows.Forms.DataGridViewTextBoxColumn itn_purrt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            System.Windows.Forms.DataGridViewTextBoxColumn itn_landrt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            System.Windows.Forms.DataGridViewTextBoxColumn itn_costrt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            System.Windows.Forms.DataGridViewTextBoxColumn itn_wsalesrt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            System.Windows.Forms.DataGridViewTextBoxColumn itn_rsalesrt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            System.Windows.Forms.DataGridViewTextBoxColumn itn_qty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            System.Windows.Forms.DataGridViewTextBoxColumn itn_freeqty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            System.Windows.Forms.DataGridViewTextBoxColumn itn_freevalue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            System.Windows.Forms.DataGridViewTextBoxColumn itn_amt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            #endregion

            #region  Declaring Celltypes and format sttings
            //Declaring Celltypes
            System.Windows.Forms.DataGridViewCellStyle dgvCellStyleBOOL = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dgvCellStyleTEXT = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dgvCellStyleNUM_INT = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dgvCellStyleNUM_18DEC2 = new System.Windows.Forms.DataGridViewCellStyle();

            //Cell format setting
            dgvCellStyleBOOL.NullValue = false;

            dgvCellStyleNUM_18DEC2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dgvCellStyleNUM_18DEC2.Format = "N3";
            dgvCellStyleNUM_18DEC2.NullValue = "0";
            #endregion

            #region  Columns Data/Bind other details
            #region   slno
            slno.HeaderText = "Sl. No."; slno.Name = "slno"; slno.DataPropertyName = "slno";
            slno.Width = 55; slno.Frozen = true;
            #endregion
            #region itn_id
            itn_id.HeaderText = "id"; itn_id.Name = "itn_id"; itn_id.DataPropertyName = "itn_id"; itn_id.Visible = false;
            itn_id.Frozen = true;
            #endregion
            #region itn_prodptr - Product code/Desc
            itn_prodptr.HeaderText = "Item"; itn_prodptr.Name = "itn_prodptr"; itn_prodptr.DataPropertyName = "itn_prodptr";
            itn_prodptr.Width = 250; itn_prodptr.Frozen = true;
            itn_prodptr.DataSource = mdtItem;
            itn_prodptr.DisplayMember = "pd_desc";
            itn_prodptr.ValueMember = "pd_code";
            itn_prodptr.AutoComplete = true;
            #endregion
            #region itn_wrsale
            itn_wrsale.HeaderText = "W/R Sale"; itn_wrsale.Name = "itn_wrsale"; itn_wrsale.DataPropertyName = "itn_wrsale";
            itn_wrsale.Visible = boolSales;
            itn_wrsale.MaxInputLength = 1;
            itn_wrsale.Width = 82;
            #endregion
            #region itn_purrt
            itn_purrt.HeaderText = "Pur. Rate";
            itn_purrt.Name = "itn_purrt";
            itn_purrt.DataPropertyName = "itn_purrt";
            itn_purrt.DefaultCellStyle = dgvCellStyleNUM_18DEC2;
            itn_purrt.MaxInputLength = 18;
            itn_purrt.ToolTipText = "Rate fixed by the company(Purchase Rate)";
            itn_purrt.Width = NUMFIELD_PIXEL_LEN;
            itn_purrt.Visible = boolPurchase;
            #endregion
            #region itn_landrt
            itn_landrt.HeaderText = "Land. Rate";
            itn_landrt.Name = "itn_landrt";
            itn_landrt.DataPropertyName = "itn_landrt";
            itn_landrt.DefaultCellStyle = dgvCellStyleNUM_18DEC2;
            itn_landrt.MaxInputLength = 18;
            itn_landrt.ToolTipText = "Landing Rate";
            itn_landrt.Width = NUMFIELD_PIXEL_LEN;
            itn_landrt.Visible = boolPurchase;
            #endregion
            #region itn_costrt
            itn_costrt.HeaderText = "Cost Rate";
            itn_costrt.Name = "itn_costrt";
            itn_costrt.DataPropertyName = "itn_costrt";
            itn_costrt.DefaultCellStyle = dgvCellStyleNUM_18DEC2;
            itn_costrt.MaxInputLength = 18;
            itn_costrt.ToolTipText = "Landing Rate";
            itn_costrt.Width = NUMFIELD_PIXEL_LEN;
            itn_costrt.Visible = boolPurchase;
            #endregion
            #region itn_wsalesrt
            itn_wsalesrt.HeaderText = "W.S. Rate";
            itn_wsalesrt.Name = "itn_wsalesrt";
            itn_wsalesrt.DataPropertyName = "itn_wsalesrt";
            itn_wsalesrt.DefaultCellStyle = dgvCellStyleNUM_18DEC2;
            itn_wsalesrt.MaxInputLength = 18;
            itn_wsalesrt.ToolTipText = "Whole Sale Rate";
            itn_wsalesrt.Width = NUMFIELD_PIXEL_LEN;
            itn_wsalesrt.Visible = boolSales;
            #endregion
            #region itn_rsalesrt
            itn_rsalesrt.HeaderText = "R.S. Rate";
            itn_rsalesrt.Name = "itn_rsalesrt";
            itn_rsalesrt.DataPropertyName = "itn_rsalesrt";
            itn_rsalesrt.DefaultCellStyle = dgvCellStyleNUM_18DEC2;
            itn_rsalesrt.MaxInputLength = 18;
            itn_rsalesrt.ToolTipText = "Whole Sale Rate";
            itn_rsalesrt.Width = NUMFIELD_PIXEL_LEN;
            itn_rsalesrt.Visible = boolSales;
            #endregion
            #region itn_qty
            itn_qty.HeaderText = "Quantity";
            itn_qty.Name = "itn_qty";
            itn_qty.DataPropertyName = "itn_qty";
            itn_qty.DefaultCellStyle = dgvCellStyleNUM_18DEC2;
            itn_qty.MaxInputLength = 18;
            itn_qty.ToolTipText = "Quantity to be purchased";
            itn_qty.Width = NUMFIELD_PIXEL_LEN;
            #endregion
            #region itn_freeqty
            itn_freeqty.HeaderText = "Free Qty";
            itn_freeqty.Name = "itn_freeqty";
            itn_freeqty.DataPropertyName = "itn_freeqty";
            itn_freeqty.DefaultCellStyle = dgvCellStyleNUM_18DEC2;
            itn_freeqty.MaxInputLength = 18;
            itn_freeqty.ToolTipText = "Free Quantity";
            itn_freeqty.Width = NUMFIELD_PIXEL_LEN;
            #endregion
            #region itn_freevalue
            itn_freevalue.HeaderText = "Free Value";
            itn_freevalue.Name = "itn_freevalue";
            itn_freevalue.DataPropertyName = "itn_freevalue";
            itn_freevalue.DefaultCellStyle = dgvCellStyleNUM_18DEC2;
            itn_freevalue.MaxInputLength = 18;
            itn_freevalue.ToolTipText = "Free Quantity Value";
            itn_freevalue.Width = NUMFIELD_PIXEL_LEN;
            itn_freevalue.ReadOnly = true; itn_freevalue.Visible = false;
            #endregion
            #region itn_amt
            itn_amt.HeaderText = "Amount";
            itn_amt.Name = "itn_amt";
            itn_amt.DataPropertyName = "itn_amt";
            itn_amt.DefaultCellStyle = dgvCellStyleNUM_18DEC2;
            itn_amt.MaxInputLength = 18;
            itn_amt.ToolTipText = "Amount(Rate * Quantity)";
            itn_amt.Width = NUMFIELD_PIXEL_LEN;
            itn_amt.ReadOnly = true;
            #endregion
            #endregion

            try
            {
                dgvDetails.Columns.Clear();
                dgvDetails.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
                slno,
                itn_id,
                itn_prodptr,
                itn_wrsale,
                itn_purrt,
                itn_landrt,
                itn_costrt,
                itn_wsalesrt,
                itn_rsalesrt,
                itn_qty,                                    
                itn_freeqty,
                itn_freevalue,
                itn_amt
                });
                dgvDetails.DataSource = mdtMain;
                dgvDetails.Refresh();
            }
            catch (Exception ex) { }
        }

        /*
         private void setDataGrid(ref DataGridView dgvDetails)
        {
            mdvgGrid = dgvDetails;            

            #region  Declaring Columns
            //Declaring Columns
            System.Windows.Forms.DataGridViewTextBoxColumn slno = new System.Windows.Forms.DataGridViewTextBoxColumn();
            System.Windows.Forms.DataGridViewTextBoxColumn itn_id = new System.Windows.Forms.DataGridViewTextBoxColumn();            
            System.Windows.Forms.DataGridViewComboBoxColumn itn_prodptr = new System.Windows.Forms.DataGridViewComboBoxColumn();
            
            System.Windows.Forms.DataGridViewComboBoxColumn itn_proddesc = new System.Windows.Forms.DataGridViewComboBoxColumn();
            System.Windows.Forms.DataGridViewComboBoxColumn itn_packptr = new System.Windows.Forms.DataGridViewComboBoxColumn();

            System.Windows.Forms.DataGridViewTextBoxColumn itn_packsf = new System.Windows.Forms.DataGridViewTextBoxColumn();
            System.Windows.Forms.DataGridViewTextBoxColumn itn_packqty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            System.Windows.Forms.DataGridViewTextBoxColumn itn_purtaxptr = new System.Windows.Forms.DataGridViewTextBoxColumn();
            System.Windows.Forms.DataGridViewTextBoxColumn itn_saltaxptr = new System.Windows.Forms.DataGridViewTextBoxColumn();
            System.Windows.Forms.DataGridViewTextBoxColumn itn_purrt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            System.Windows.Forms.DataGridViewTextBoxColumn itn_landrt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            System.Windows.Forms.DataGridViewTextBoxColumn itn_wsalesrt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            System.Windows.Forms.DataGridViewTextBoxColumn itn_rsalesrt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            System.Windows.Forms.DataGridViewTextBoxColumn itn_qty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            System.Windows.Forms.DataGridViewTextBoxColumn itn_amt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            System.Windows.Forms.DataGridViewTextBoxColumn itn_disper = new System.Windows.Forms.DataGridViewTextBoxColumn();
            System.Windows.Forms.DataGridViewTextBoxColumn itn_disamt = new System.Windows.Forms.DataGridViewTextBoxColumn();                                                            
            System.Windows.Forms.DataGridViewTextBoxColumn itn_tax = new System.Windows.Forms.DataGridViewTextBoxColumn();
            System.Windows.Forms.DataGridViewTextBoxColumn itn_ast = new System.Windows.Forms.DataGridViewTextBoxColumn();

            System.Windows.Forms.DataGridViewTextBoxColumn itn_isoffer = new System.Windows.Forms.DataGridViewTextBoxColumn();
            System.Windows.Forms.DataGridViewComboBoxColumn itn_freepackptr = new System.Windows.Forms.DataGridViewComboBoxColumn();
            System.Windows.Forms.DataGridViewTextBoxColumn itn_freeqty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            System.Windows.Forms.DataGridViewTextBoxColumn itn_add = new System.Windows.Forms.DataGridViewTextBoxColumn();
            System.Windows.Forms.DataGridViewTextBoxColumn itn_less = new System.Windows.Forms.DataGridViewTextBoxColumn();
            System.Windows.Forms.DataGridViewTextBoxColumn itn_rnd = new System.Windows.Forms.DataGridViewTextBoxColumn();
            System.Windows.Forms.DataGridViewTextBoxColumn itn_net = new System.Windows.Forms.DataGridViewTextBoxColumn();
            #endregion

            #region  Declaring Celltypes and format sttings
            //Declaring Celltypes
            System.Windows.Forms.DataGridViewCellStyle dgvCellStyleBOOL = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dgvCellStyleTEXT = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dgvCellStyleNUM_INT = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dgvCellStyleNUM_18DEC2 = new System.Windows.Forms.DataGridViewCellStyle();

            //Cell format setting
            dgvCellStyleBOOL.NullValue = false;

            dgvCellStyleNUM_18DEC2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dgvCellStyleNUM_18DEC2.Format = "N2";
            dgvCellStyleNUM_18DEC2.NullValue = "0";
            #endregion

         #region  Columns Data/Bind other details 
            #region   slno
            slno.HeaderText = "Sl.No."; slno.Name = "slno"; slno.DataPropertyName = "slno";
            slno.Width = 30; slno.Frozen = true;
            #endregion
            #region itn_id
            itn_id.HeaderText = "id"; itn_id.Name = "itn_id"; itn_id.DataPropertyName = "itn_id"; itn_id.Visible = false;
            itn_id.Frozen = true;            
            #endregion
            #region itn_prodptr - Product code/Desc           
            itn_prodptr.HeaderText = "Item"; itn_prodptr.Name = "itn_prodptr"; itn_prodptr.DataPropertyName = "itn_prodptr";
            itn_prodptr.Width = 200; itn_prodptr.Frozen = true;
            itn_prodptr.DataSource = mdtItem;
            itn_prodptr.DisplayMember = "pd_desc";
            itn_prodptr.ValueMember = "pd_code";
            itn_prodptr.AutoComplete = true;
            #endregion
            #region itn_packptr
            itn_packptr.HeaderText = "Packing"; itn_packptr.Name = "itn_packptr"; itn_packptr.DataPropertyName = "itn_packptr";
            itn_packptr.Width = 80; itn_packptr.Frozen = true;
            itn_packptr.DataSource = mdtPack;
            itn_packptr.DisplayMember = "pk_desc";
            itn_packptr.ValueMember = "pk_code";
            itn_packptr.AutoComplete = true;
            #endregion            
            #region itn_packsf (Packin Shoet form)
            itn_packsf.HeaderText = "PackSF"; itn_packsf.Name = "itn_packsf"; itn_packsf.DataPropertyName = "itn_packsf"; itn_packsf.Visible = false;
            #endregion
            #region itn_packqty 
            itn_packqty.HeaderText = "PackQty"; itn_packqty.Name = "itn_packqty"; itn_packqty.DataPropertyName = "itn_packqty"; itn_packqty.Visible = false;
            #endregion
            #region itn_purtaxptr
            itn_purtaxptr.HeaderText = "Pur. Tax"; itn_purtaxptr.Name = "itn_purtaxptr"; itn_purtaxptr.DataPropertyName = "itn_purtaxptr"; itn_purtaxptr.Visible = false;
            #endregion
            #region itn_saltaxptr
            itn_saltaxptr.HeaderText = "Sale. Tax"; itn_saltaxptr.Name = "itn_saltaxptr"; itn_saltaxptr.DataPropertyName = "itn_saltaxptr"; itn_saltaxptr.Visible = false;
            #endregion
            #region itn_purrt
            itn_purrt.HeaderText = "Pur. Rate";
            itn_purrt.Name = "itn_purrt";
            itn_purrt.DataPropertyName = "itn_purrt";
            itn_purrt.DefaultCellStyle = dgvCellStyleNUM_18DEC2;            
            itn_purrt.MaxInputLength = 18;            
            itn_purrt.ToolTipText = "Rate fixed by the company(Purchase Rate)";
            itn_purrt.Width = NUMFIELD_PIXEL_LEN;
            #endregion
            #region itn_landrt
            itn_landrt.HeaderText = "Land. Rate";
            itn_landrt.Name = "itn_landrt";
            itn_landrt.DataPropertyName = "itn_landrt";
            itn_landrt.DefaultCellStyle = dgvCellStyleNUM_18DEC2;
            itn_landrt.MaxInputLength = 18;
            itn_landrt.ToolTipText = "Landing Rate";
            itn_landrt.Width = NUMFIELD_PIXEL_LEN;
            #endregion
            #region itn_wsalesrt
            itn_wsalesrt.HeaderText = "W.S. Rate";
            itn_wsalesrt.Name = "itn_wsalesrt";
            itn_wsalesrt.DataPropertyName = "itn_wsalesrt";
            itn_wsalesrt.DefaultCellStyle = dgvCellStyleNUM_18DEC2;
            itn_wsalesrt.MaxInputLength = 18;
            itn_wsalesrt.ToolTipText = "Whole Sale Rate";
            itn_wsalesrt.Width = NUMFIELD_PIXEL_LEN;
            #endregion
            #region itn_rsalesrt
            itn_rsalesrt.HeaderText = "R.S. Rate";
            itn_rsalesrt.Name = "itn_rsalesrt";
            itn_rsalesrt.DataPropertyName = "itn_rsalesrt";
            itn_rsalesrt.DefaultCellStyle = dgvCellStyleNUM_18DEC2;
            itn_rsalesrt.MaxInputLength = 18;
            itn_rsalesrt.ToolTipText = "Whole Sale Rate";
            itn_rsalesrt.Width = NUMFIELD_PIXEL_LEN;
            #endregion
            #region itn_qty
            itn_qty.HeaderText = "Quantity";
            itn_qty.Name = "itn_qty";
            itn_qty.DataPropertyName = "itn_qty";
            itn_qty.DefaultCellStyle = dgvCellStyleNUM_18DEC2;
            itn_qty.MaxInputLength = 18;
            itn_qty.ToolTipText = "Quantity to be purchased";
            itn_qty.Width = NUMFIELD_PIXEL_LEN;
            #endregion
            #region itn_amt
            itn_amt.HeaderText = "Amount";
            itn_amt.Name = "itn_amt";
            itn_amt.DataPropertyName = "itn_amt";
            itn_amt.DefaultCellStyle = dgvCellStyleNUM_18DEC2;
            itn_amt.MaxInputLength = 18;
            itn_amt.ToolTipText = "Amount(Rate * Quantity)";
            itn_amt.Width = NUMFIELD_PIXEL_LEN;
            #endregion
            #region itn_disper
            itn_disper.HeaderText = "Dis. %";
            itn_disper.Name = "itn_disper";
            itn_disper.DataPropertyName = "itn_disper";
            itn_disper.DefaultCellStyle = dgvCellStyleNUM_18DEC2;
            itn_disper.MaxInputLength = 18;
            itn_disper.ToolTipText = "Discount Percentage";
            itn_disper.Width = NUMFIELD_PIXEL_LEN - 20;
            #endregion
            #region itn_disamt
            itn_disamt.HeaderText = "Dis. Amt.";
            itn_disamt.Name = "itn_disamt";
            itn_disamt.DataPropertyName = "itn_disamt";
            itn_disamt.DefaultCellStyle = dgvCellStyleNUM_18DEC2;
            itn_disamt.MaxInputLength = 18;
            itn_disamt.ToolTipText = "Discount Amount";
            itn_disamt.Width = NUMFIELD_PIXEL_LEN;
            #endregion
            #region itn_tax
            itn_tax.HeaderText = "Tax Amt.";
            itn_tax.Name = "itn_tax";
            itn_tax.DataPropertyName = "itn_tax";
            itn_tax.DefaultCellStyle = dgvCellStyleNUM_18DEC2;
            itn_tax.MaxInputLength = 18;
            itn_tax.ToolTipText = "Tax Amount";
            itn_tax.Width = NUMFIELD_PIXEL_LEN;
            #endregion
            #region itn_ast
            itn_ast.HeaderText = "AST Amt.";
            itn_ast.Name = "itn_ast";
            itn_ast.DataPropertyName = "itn_ast";
            itn_ast.DefaultCellStyle = dgvCellStyleNUM_18DEC2;
            itn_ast.MaxInputLength = 18;
            itn_ast.ToolTipText = "Additional Sales Tax Amount";
            itn_ast.Width = NUMFIELD_PIXEL_LEN;
            #endregion

            System.Windows.Forms.DataGridViewTextBoxColumn itn_isoffer = new System.Windows.Forms.DataGridViewTextBoxColumn();
            System.Windows.Forms.DataGridViewComboBoxColumn itn_freepackptr = new System.Windows.Forms.DataGridViewComboBoxColumn();
            System.Windows.Forms.DataGridViewTextBoxColumn itn_freeqty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            System.Windows.Forms.DataGridViewTextBoxColumn itn_add = new System.Windows.Forms.DataGridViewTextBoxColumn();
            System.Windows.Forms.DataGridViewTextBoxColumn itn_less = new System.Windows.Forms.DataGridViewTextBoxColumn();
            System.Windows.Forms.DataGridViewTextBoxColumn itn_rnd = new System.Windows.Forms.DataGridViewTextBoxColumn();
            System.Windows.Forms.DataGridViewTextBoxColumn itn_net = new System.Windows.Forms.DataGridViewTextBoxColumn();
         #endregion

            try
            {
                dgvDetails.Columns.Clear();
                dgvDetails.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
                slno,
                itn_id,
                itn_prodptr,
                itn_packptr,
                itn_purrt 
                });
                dgvDetails.DataSource = mdtMain;
                dgvDetails.Refresh();
            }
            catch (Exception ex) { }
        } 
        */
        public object getFieldData(String _FieldName)
        {
            Object objRet = null;
            if (mdtMain.Columns.Contains(_FieldName))
                objRet = mdtMain.Rows[0][_FieldName];
            return objRet;
        }
        public void setFieldData(String _FieldName, Object _Data)
        {
            if (mdtMain.Columns.Contains(_FieldName))
                mdtMain.Rows[0][_FieldName] = _Data;
        }
        public Decimal getProductStock(String _ProdPtr,String _BranchPtr)
        {
            Decimal decRet = mCommFuncs.ConvertToNumber_Dec((mGlobal.LocalDBCon.ExecuteScalar("exec ProductStock " + _ProdPtr + "," + _BranchPtr).ToString()));
            return (decRet);
        }

        public Decimal getClosingStockValue(DateTime asONdate)
        {
            try
            {
                string strSql = "";
                strSql = "SELECT sum(IIf(itn_trntype<=10,(itn_qty+itn_freeqty),(itn_qty+itn_freeqty)*-1)*pd_purrt) AS StockValue  " +
                         " FROM itemtran, prodmas WHERE (itn_canflg<>'Y' Or itn_canflg Is Null) " +
                         " and itn_prodptr=pd_code and  Trndt<=" + mCommFuncs.FormatDBDate(asONdate.ToShortDateString());
                decimal StockValue = mCommFuncs.ConvertToNumber_Dec(mGlobal.LocalDBCon.ExecuteScalar(strSql).ToString());
                return StockValue;
            }
            catch { }
            return 0;

        }
        public Decimal getClosingStockValue(DateTime asONdate,string strBrCodes)
        {
            try
            {
                string strSql = "";
                strSql = "SELECT sum(IIf(itn_trntype<=10,(itn_qty+itn_freeqty),(itn_qty+itn_freeqty)*-1)*pd_purrt) AS StockValue  " +
                         " FROM itemtran, prodmas WHERE (itn_canflg<>'Y' Or itn_canflg Is Null) " +
                         " and itn_brptr in( " + strBrCodes + ") and  itn_prodptr=pd_code and  Trndt<=" + mCommFuncs.FormatDBDate(asONdate.ToShortDateString());
                decimal StockValue = mCommFuncs.ConvertToNumber_Dec(mGlobal.LocalDBCon.ExecuteScalar(strSql).ToString());
                return StockValue;
            }
            catch { }
            return 0;

        }






        public bool CheckStock(ref DataGridView _dvgData)
        {
            Decimal decRet = 0;
            DataTable dt = (DataTable)_dvgData.DataSource;

            for (int intCnt = 0; intCnt < _dvgData.Rows.Count; intCnt++)
            {
                if (_dvgData.Rows[intCnt].Cells["itn_prodptr"].Value == null || _dvgData.Rows[intCnt].Cells["itn_prodptr"].Value.ToString() == "")
                    continue;
                decRet = mCommFuncs.ConvertToNumber_Dec(
                    dt.Compute("sum(itn_qty)", "itn_prodptr='" + _dvgData.Rows[intCnt].Cells["itn_prodptr"].Value.ToString() + "'"));
                decRet += mCommFuncs.ConvertToNumber_Dec(
                    dt.Compute("sum(itn_freeqty)", "itn_prodptr='" + _dvgData.Rows[intCnt].Cells["itn_prodptr"].Value.ToString() + "'"));
                if (decRet > getProductStock(_dvgData.Rows[intCnt].Cells["itn_prodptr"].Value.ToString(),mGlobal.BranchCode ))
                {
                    _dvgData.Rows[intCnt].Selected = true;
                    _dvgData.Rows[intCnt].Cells["itn_qty"].Selected = true;
                    _dvgData.Focus();
                    return false;
                }
            }
            return true;
        }
        private DataTable GetDataTable_ForSave(Int32 _HdrIdentityNo, String _FromMode, ref DataGridView _dgvData)
        {   // Read all needed data from datagridview & add additional data if needed(like totals)            
            DataTable dtGrid = null;
            String strSql = "select * from itemtran where itn_trnmode='" + _FromMode + "' and itn_hdrid=" + _HdrIdentityNo;
            DataTable dtRet = mGlobal.LocalDBCon.ExecuteQuery(strSql);
            String strPrimaryKey = "itn_id";
            DataRow dr = null;
            int intIndex = -1;

            try
            {
                dtRet.Columns[strPrimaryKey].AutoIncrement = true;
                DataColumn[] dcPrimaryKey = { dtRet.Columns[strPrimaryKey] };
                dtRet.PrimaryKey = dcPrimaryKey;
                dtGrid = (DataTable)_dgvData.DataSource;
                for (int intRow = 0; intRow < dtGrid.Rows.Count; intRow++)
                {
                    if (dtGrid.Rows[intRow]["itn_prodptr"].ToString().Trim() == "" || dtGrid.Rows[intRow]["itn_qty"].ToString().Trim() == "0") continue;
                    if (dtGrid.Rows[intRow][strPrimaryKey].ToString().Trim() != "0" && dtRet.Rows.Contains(dtGrid.Rows[intRow][strPrimaryKey]))
                    {
                        dr = dtRet.Rows.Find(dtGrid.Rows[intRow][strPrimaryKey]);
                        intIndex = dtRet.Rows.IndexOf(dr);
                    }
                    else
                    {
                        dr = dtRet.NewRow();
                        dtRet.Rows.Add(dr);
                        intIndex = dtRet.Rows.IndexOf(dr);
                    }
                    foreach (DataColumn dc in dtGrid.Columns)
                    {
                        if (dc.ColumnName.Trim().ToLower() != strPrimaryKey.ToLower())
                            dtRet.Rows[intIndex][dc.ColumnName] = dtGrid.Rows[intRow][dc.ColumnName];
                    }
                    dtRet.Rows[intIndex]["itn_hdrid"] = _HdrIdentityNo;
                    dtRet.Rows[intIndex]["itn_trndt"] = mstrTransactionDate;
                    dtRet.Rows[intIndex]["itn_trntm"] = DateTime.Now.ToShortTimeString();
                    dtRet.Rows[intIndex]["itn_strndt"] = mCommFuncs.FormatDBServDate(mstrTransactionDate, true);
                    dtRet.Rows[intIndex]["itn_net"] = dtRet.Rows[intIndex]["itn_amt"];
                    dtRet.Rows[intIndex]["itn_trnmode"] = _FromMode;
                    dtRet.Rows[intIndex]["itn_brptr"] = mGlobal.BranchCode;
                    dtRet.Rows[intIndex]["itn_canflg"] = "N";
                    
                    if (dtRet.Rows[intIndex]["itn_freeqty"].ToString().Trim() == "")
                    {
                        dtRet.Rows[intIndex]["itn_freeqty"] = 0;
                        dtRet.Rows[intIndex]["itn_freevalue"] = 0;
                    }
                    if (mCommFuncs.ConvertToNumber_Dec(dtRet.Rows[intIndex]["itn_qty"]) != 0)
                        dtRet.Rows[intIndex]["itn_trnrt"] = mCommFuncs.ConvertToNumber_Dec(dtRet.Rows[intIndex]["itn_amt"]) /
                            mCommFuncs.ConvertToNumber_Dec(dtRet.Rows[intIndex]["itn_qty"]);
                    else
                        dtRet.Rows[intIndex]["itn_trnrt"] = 0;

                    switch (_FromMode)
                    {
                        case "P"://Purchase
                            if (mstrTransactionType == "4")//Opening Stock
                                dtRet.Rows[intIndex]["itn_trntype"] = 0;
                            else if (mstrTransactionType == "3")//Stock Transfer(IN)
                                dtRet.Rows[intIndex]["itn_trntype"] = 6;
                            else
                                dtRet.Rows[intIndex]["itn_trntype"] = 1;

							if (mstrTransactionType == "3")//Stock Transfer(IN)
								dtRet.Rows[intIndex]["itn_freevalue"] =
									Decimal.Parse(mCommFuncs.ConvertToNumberObj(dtRet.Rows[intIndex]["itn_costrt"]).ToString())
									* Decimal.Parse(mCommFuncs.ConvertToNumberObj(dtRet.Rows[intIndex]["itn_freeqty"]).ToString());
							else
								dtRet.Rows[intIndex]["itn_freevalue"] =
									Decimal.Parse(mCommFuncs.ConvertToNumberObj(dtRet.Rows[intIndex]["itn_purrt"]).ToString())
									* Decimal.Parse(mCommFuncs.ConvertToNumberObj(dtRet.Rows[intIndex]["itn_freeqty"]).ToString());
                            break;
                        case "S"://Sales whole sale rate
                            if (mstrTransactionType == "4")//Others
                                dtRet.Rows[intIndex]["itn_trntype"] = 18;
                            if (mstrTransactionType == "3")//Stock Transfer(OUT)
                                dtRet.Rows[intIndex]["itn_trntype"] = 16;
                            else
                                dtRet.Rows[intIndex]["itn_trntype"] = 11;

                            dtRet.Rows[intIndex]["itn_freevalue"] =
                                Decimal.Parse(mCommFuncs.ConvertToNumberObj(dtRet.Rows[intIndex]["itn_costrt"]).ToString())
                                * Decimal.Parse(mCommFuncs.ConvertToNumberObj(dtRet.Rows[intIndex]["itn_freeqty"]).ToString());
                            break;
                        default:
                            break;
                    }
                }
            }
            catch (Exception ex) { mboolErrorOccurred = true; MessageBox.Show(ex.Message); }
            return dtRet;
        }

        public bool SaveData(Int32 _HdrIdentityNo, String _FromMode, ref DataGridView _dgvData)
        {
            mboolErrorOccurred = false;
            int intCnt = mGlobal.LocalDBCon.UpdateDataTable("select * from itemtran where 1=2", GetDataTable_ForSave(_HdrIdentityNo, _FromMode, ref _dgvData));
            if (intCnt > 0 && (!mboolErrorOccurred))
                return true;
            return false;
        }
        public void DeleteData_WithHdrID(Int32 _HdrIdentityNo,String _FromMode)
        {
            if (_FromMode == "P") _FromMode = "1";//Purchase
            else _FromMode = "11";//Sales
            mGlobal.LocalDBCon.ExecuteNonQuery("delete from itemtran where itn_trntype=" + _FromMode + " and itn_hdrid=" + _HdrIdentityNo);
        }
        public Int32 HdrLastIdentityNo
        {
            //get { return _HdrLastIdentityNo; }
            set { _HdrLastIdentityNo = value; }
        }
        // Column Index Starts.........>
        public int ColIndexProduct // Grid column index of product
        {
            get { return 2; }
        }
        public int ColIndexQty // Grid column index of quantity
        {
            get { return 8; }
        }
        public int ColIndexFreeQty // Grid column index of free quantity
        {
            get { return 9; }
        }
        public int ColIndexPurRate // Grid column index of purchase rate
        {
            get { return 4; }
        }
        public int ColIndexRateMode // Grid column index of Rate mode(W-Whole/R-Retal)
        {
            get { return 3; }
        }
        public int ColIndexWSalesRate // Grid column index of Rate 
        {
            get { return 6; }
        }
        public int ColIndexRSalesRate // Grid column index of Rate 
        {
            get { return 7; }
        }
        public int ColIndexLandRate // Grid column index of Rate 
        {
            get { return 5; }
        }
        public int ColIndexCostRate // Grid column index of Rate 
        {
            get { return 12; }
        }
        // Column Index Ends.........>|
        public Decimal TotalAmount
        {
            get { return mdecTotAmt; }
        }
        public Decimal TotalQuantity
        {
            get { return mdecTotQty; }
        }
        public String TransactionDate
        {
            set { mstrTransactionDate = value; }
        }
        public String TransactionType
        {
            set { mstrTransactionType = value; }
        }
    }
}
