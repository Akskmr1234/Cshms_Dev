using System;
using System.Collections.Generic;
using System.Text;
using System.Data.Common;
using System.Windows.Forms;
using System.Data;

namespace CsHms.Akshay.Class
{
    class VoucherCls
    {
        Global mGlobal = new Global();
        CommFuncs mCommFunc = new CommFuncs();
        const String TABLE_NAME = "acvoucher";
        const String PRIMARY_KEY = "v_id";
        String mstrTableName = "";
        //DbConnection mdbConn = new DbConnection();
        String mstrKey = "";
        String mstrDbIndex = "";
        String mstrFirmPtr = "";
        String mstrTablePrefix = "";
        Int64 mintId;
        String mstrVtype;
        String mstrVno;
        Int64 mintVnon;
        DateTime mdateDt;
        DateTime mdateTm;
        String mstrCj;
        Int64 mintBigacptr;
        String mstrBigacdesc;
        Int64 mintBigopacptr;
        String mstrBigopacdesc;
        String mstrRefno;
        Decimal mdecTotamt;
        String mstrNarration;
        Int64 mintOthrefid;
        String mstrOthref;
        Int64 mintUserptr;
        Int32 mintFinyearptr;
        Int32 mintPcenterptr;
        Int32 mintBrptr;
        Int32 mintFirmptr;
        String mstrStatus;
        Int64 mintAuthuserptr;
        DateTime mdateAuthdt;
        String mstrAuthremarks;
        String mstrEditmode;
        String mFirmIds;
        Int32 mFirmptr;
        String SQL;

        public void ClearAll()
        {
            mintId = 0;
            mstrVtype = "";
            mstrVno = "";
            mintVnon = 0;
            mdateDt = DateTime.Now;
            mdateTm = DateTime.Now;
            mstrCj = "";
            mintBigacptr = 0;
            mstrBigacdesc = "";
            mintBigopacptr = 0;
            mstrBigopacdesc = "";
            mstrRefno = "";
            mdecTotamt = 0;
            mstrNarration = "";
            mintOthrefid = 0;
            mstrOthref = "";
            mintUserptr = 0;
            mintFinyearptr = 0;
            mintPcenterptr = 0;
            mintBrptr = 0;
            mintFirmptr = 0;
            mstrStatus = "";
            mintAuthuserptr = 0;
            mdateAuthdt = DateTime.Now;
            mstrAuthremarks = "";
            mstrEditmode = "";
            SQL = "";
        }
        public Int64 Id
        {
            set { mintId = value; }
            get { return mintId; }
        }
        public String Vtype
        {
            set { mstrVtype = value; }
            get { return mstrVtype; }
        }
        public String Vno
        {
            set { mstrVno = value; }
            get { return mstrVno; }
        }
        public Int64 Vnon
        {
            set { mintVnon = value; }
            get { return mintVnon; }
        }
        public DateTime Dt
        {
            set { mdateDt = value; }
            get { return mdateDt; }
        }
        public DateTime Tm
        {
            set { mdateTm = value; }
            get { return mdateTm; }
        }
        public String Cj
        {
            set { mstrCj = value; }
            get { return mstrCj; }
        }
        public Int64 Bigacptr
        {
            set { mintBigacptr = value; }
            get { return mintBigacptr; }
        }
        public String Bigacdesc
        {
            set { mstrBigacdesc = value; }
            get { return mstrBigacdesc; }
        }
        public Int64 Bigopacptr
        {
            set { mintBigopacptr = value; }
            get { return mintBigopacptr; }
        }
        public String Bigopacdesc
        {
            set { mstrBigopacdesc = value; }
            get { return mstrBigopacdesc; }
        }
        public String Refno
        {
            set { mstrRefno = value; }
            get { return mstrRefno; }
        }
        public Decimal Totamt
        {
            set { mdecTotamt = value; }
            get { return mdecTotamt; }
        }
        public String Narration
        {
            set { mstrNarration = value; }
            get { return mstrNarration; }
        }
        public Int64 Othrefid
        {
            set { mintOthrefid = value; }
            get { return mintOthrefid; }
        }
        public String Othref
        {
            set { mstrOthref = value; }
            get { return mstrOthref; }
        }
        public Int64 Userptr
        {
            set { mintUserptr = value; }
            get { return mintUserptr; }
        }
        public Int32 Finyearptr
        {
            set { mintFinyearptr = value; }
            get { return mintFinyearptr; }
        }
        public Int32 Pcenterptr
        {
            set { mintPcenterptr = value; }
            get { return mintPcenterptr; }
        }
        public Int32 Brptr
        {
            set { mintBrptr = value; }
            get { return mintBrptr; }
        }
        //public Int32 Firmptr
        //{
        // set { mintFirmptr = value; }
        // get { return mintFirmptr; }
        //}
        public String Status
        {
            set { mstrStatus = value; }
            get { return mstrStatus; }
        }
        public Int64 Authuserptr
        {
            set { mintAuthuserptr = value; }
            get { return mintAuthuserptr; }
        }
        public DateTime Authdt
        {
            set { mdateAuthdt = value; }
            get { return mdateAuthdt; }
        }
        public String Authremarks
        {
            set { mstrAuthremarks = value; }
            get { return mstrAuthremarks; }
        }
        public String Editmode
        {
            set { mstrEditmode = value; }
            get { return mstrEditmode; }
        }
        public Int32 Firmptr
        {
            set{mFirmptr=value;}
            get { return mFirmptr; }
        }
        public String FirmIds
        {
            set { mFirmIds=value;}
            get { return mFirmIds;}
        }
        public long Hdrid;
        public int Acptr;
        public int Opacptr;
        public string Bigopactype;
        public string Refdet;
        public string Remarks;
        public int Qty;
        public decimal Amt;
        public int Trn;
        public int Slno;
        public string Totrow;
        public string Pdstatus;
        public DateTime Pddt;
        public string Rconstatus;
        public DateTime Rcondt;


        public bool PostingToSalesAndCollectionAccounts(DateTime dtAson, string FirmCode)
        {
            try
            {
                decimal decCashAmt = 0;
                DeleteVoucher(dtAson, FirmCode);
                string SQL = "";

                #region Cash transaction
                //Post Cash Transaction
                SQL = "select * from udf_SalesandCollPostDetailed( '" + dtAson.Date.ToString("MM/dd/yyyy") + "','" + FirmCode + "') where IsCashTran='Y' ";
                DataTable dtData = mGlobal.LocalDBCon.ExecuteQuery_OnTran(SQL);
                if (dtData.Rows.Count > 0)
                {
                    this.Vtype = "RCTV";
                    this.Finyearptr = mCommFunc.ConvertToInt(dtData.Rows[0]["Finyrid"].ToString());
                    this.Brptr = mCommFunc.ConvertToInt(dtData.Rows[0]["Branch"].ToString());
                    this.Firmptr = mCommFunc.ConvertToInt(dtData.Rows[0]["Firm"].ToString());
                    this.Vno = GetNextVoucherNo(this.Vtype).ToString();
                    this.Vnon = mCommFunc.ConvertToInt64(this.Vno.ToString());
                    this.Dt = dtAson.Date;
                    this.Tm = DateTime.Now;
                    this.Cj = "C";
                    this.Refno = "";
                    this.Narration = "";
                    this.Othrefid = 0;
                    this.Othref = "AcPost " + Convert.ToString(this.Dt.Date.ToShortDateString());
                    this.Userptr = 0;
                    this.Pcenterptr = 1;
                    this.Status = "A";
                    this.Authuserptr = 0;
                    this.Authdt = Convert.ToDateTime("01-01-1900 00:00:00 AM"); //DateTime.Now;
                    this.Authremarks = "";
                    this.Editmode = "Y";

                    DeleteVoucher(this.Dt.Date, mCommFunc.ConvertToString(dtData.Rows[0]["Firm"].ToString()));

                    foreach (DataRow dr in dtData.Rows)
                    {
                        this.Vno = GetNextVoucherNo(this.Vtype).ToString();
                        this.Vnon = mCommFunc.ConvertToInt64(this.Vno.ToString());

                        this.Bigacptr = mCommFunc.ConvertToInt(dr["DebitAc"].ToString());
                        this.Bigacdesc = dr["DebitAcName"].ToString();
                        this.Bigopacptr = mCommFunc.ConvertToInt(dr["CreditAc"].ToString());
                        this.Bigopacdesc = dr["CreditAcName"].ToString();

                        this.Totamt = mCommFunc.ConvertToNumber_Dec(dr["Amount"].ToString());

                        insertData();

                        VoucherdCls mAcVoucherdCls = new VoucherdCls();
                        //mAcVoucherdCls.Init(mstrKey, mstrDbIndex, mstrFirmPtr, mstrTablePrefix);
                        mAcVoucherdCls.Id = 0;
                        mAcVoucherdCls.Hdrid = this.Id;
                        mAcVoucherdCls.Acptr = mCommFunc.ConvertToInt(dr["CreditAc"].ToString());
                        mAcVoucherdCls.Opacptr = mCommFunc.ConvertToInt(dr["DebitAc"].ToString());
                        mAcVoucherdCls.Bigopactype = "";
                        mAcVoucherdCls.Refdet = "";
                        mAcVoucherdCls.Remarks = dr["Remarks"].ToString();
                        mAcVoucherdCls.Qty = 0;
                        mAcVoucherdCls.Amt = this.Totamt;
                        mAcVoucherdCls.Trn = -1;
                        mAcVoucherdCls.Slno = 1;
                        mAcVoucherdCls.Totrow = "N";
                        mAcVoucherdCls.Othref = "AcPost " + Convert.ToString(this.Dt.Date.ToShortDateString());// "AcPost " + Convert.ToString(this.Dt.Date.ToShortDateString());
                        mAcVoucherdCls.Pdstatus = "";
                        mAcVoucherdCls.Pddt = Convert.ToDateTime("01-01-1900 00:00:00 AM");//DateTime.Now;
                        mAcVoucherdCls.Rconstatus = "";
                        mAcVoucherdCls.Rcondt = Convert.ToDateTime("01-01-1900 00:00:00 AM");//DateTime.Now;
                        mAcVoucherdCls.insertDetailData();


                        //AcVoucherdCls mAcVoucherdCls = new AcVoucherdCls();
                        mAcVoucherdCls.Id = 0;
                        mAcVoucherdCls.Hdrid = this.Id;
                        mAcVoucherdCls.Acptr = mCommFunc.ConvertToInt(dr["DebitAc"].ToString());
                        mAcVoucherdCls.Opacptr = mCommFunc.ConvertToInt(dr["CreditAc"].ToString());
                        mAcVoucherdCls.Bigopactype = "";
                        mAcVoucherdCls.Refdet = "";
                        mAcVoucherdCls.Remarks = mCommFunc.ConvertToString(dr["Remarks"].ToString());
                        mAcVoucherdCls.Qty = 0;
                        mAcVoucherdCls.Amt = this.Totamt;
                        mAcVoucherdCls.Trn = 1;
                        mAcVoucherdCls.Slno = 0;
                        mAcVoucherdCls.Totrow = "Y";
                        mAcVoucherdCls.Othref = "AcPost " + Convert.ToString(this.Dt.Date.ToShortDateString());
                        mAcVoucherdCls.Pdstatus = "";
                        mAcVoucherdCls.Pddt = Convert.ToDateTime("01-01-1900 00:00:00 AM");//DateTime.Now;
                        mAcVoucherdCls.Rconstatus = "";
                        mAcVoucherdCls.Rcondt = Convert.ToDateTime("01-01-1900 00:00:00 AM");//DateTime.Now;
                        mAcVoucherdCls.insertDetailData();
                    }
                }

                #endregion

                #region Non Cash transaction
                //Post Cash Transaction
                SQL = "select * from udf_SalesandCollPostDetailed( '" + dtAson.Date.ToString("MM/dd/yyyy") + "','" + FirmCode + "') where IsCashTran<>'Y' ";
                dtData = mGlobal.LocalDBCon.ExecuteQuery_OnTran(SQL);
                if (dtData.Rows.Count > 0)
                {
                    this.Vtype = "RCTV";
                    this.Finyearptr = mCommFunc.ConvertToInt(dtData.Rows[0]["Finyrid"].ToString());
                    this.Brptr = mCommFunc.ConvertToInt(dtData.Rows[0]["Branch"].ToString());
                    this.Firmptr = mCommFunc.ConvertToInt(dtData.Rows[0]["Firm"].ToString());
                    this.Vno = GetNextVoucherNo(this.Vtype).ToString();
                    this.Vnon = mCommFunc.ConvertToInt64(this.Vno.ToString());
                    this.Dt = dtAson.Date;
                    this.Tm = DateTime.Now;
                    this.Cj = "C";
                    this.Refno = "";
                    this.Narration = "";
                    this.Othrefid = 0;
                    this.Othref = "AcPost " + Convert.ToString(this.Dt.Date.ToShortDateString());
                    this.Userptr = 0;
                    this.Pcenterptr = 1;
                    this.Status = "A";
                    this.Authuserptr = 0;
                    this.Authdt = Convert.ToDateTime("01-01-1900 00:00:00 AM"); //DateTime.Now;
                    this.Authremarks = "";
                    this.Editmode = "Y";

                    foreach (DataRow dr in dtData.Rows)
                    {
                        this.Vno = GetNextVoucherNo(this.Vtype).ToString();
                        this.Vnon = mCommFunc.ConvertToInt64(this.Vno.ToString());
                        this.Bigacptr = mCommFunc.ConvertToInt(dr["DebitAc"].ToString());
                        this.Bigacdesc = dr["DebitAcName"].ToString();
                        this.Bigopacptr = mCommFunc.ConvertToInt(dr["CreditAc"].ToString());
                        this.Bigopacdesc = dr["CreditAcName"].ToString();

                        this.Totamt = mCommFunc.ConvertToNumber_Dec(dr["Amount"].ToString());

                        insertData();

                        VoucherdCls mAcVoucherdCls = new VoucherdCls();
                        //mAcVoucherdCls.Init(mstrKey, mstrDbIndex, mstrFirmPtr, mstrTablePrefix);
                        mAcVoucherdCls.Id = 0;
                        mAcVoucherdCls.Hdrid = this.Id;
                        mAcVoucherdCls.Acptr = mCommFunc.ConvertToInt(dr["CreditAc"].ToString());
                        mAcVoucherdCls.Opacptr = mCommFunc.ConvertToInt(dr["DebitAc"].ToString());
                        mAcVoucherdCls.Bigopactype = "";
                        mAcVoucherdCls.Refdet = "";
                        mAcVoucherdCls.Remarks = dr["Remarks"].ToString();
                        mAcVoucherdCls.Qty = 0;
                        mAcVoucherdCls.Amt = this.Totamt;
                        mAcVoucherdCls.Trn = -1;
                        mAcVoucherdCls.Slno = 1;
                        mAcVoucherdCls.Totrow = "N";
                        mAcVoucherdCls.Othref = "AcPost " + Convert.ToString(this.Dt.Date.ToShortDateString());// "AcPost " + Convert.ToString(this.Dt.Date.ToShortDateString());
                        mAcVoucherdCls.Pdstatus = "";
                        mAcVoucherdCls.Pddt = Convert.ToDateTime("01-01-1900 00:00:00 AM");//DateTime.Now;
                        mAcVoucherdCls.Rconstatus = "";
                        mAcVoucherdCls.Rcondt = Convert.ToDateTime("01-01-1900 00:00:00 AM");//DateTime.Now;
                        mAcVoucherdCls.insertDetailData();


                        //AcVoucherdCls mAcVoucherdCls = new AcVoucherdCls();
                        mAcVoucherdCls.Id = 0;
                        mAcVoucherdCls.Hdrid = this.Id;
                        mAcVoucherdCls.Acptr = mCommFunc.ConvertToInt(dr["DebitAc"].ToString());
                        mAcVoucherdCls.Opacptr = mCommFunc.ConvertToInt(dr["CreditAc"].ToString());
                        mAcVoucherdCls.Bigopactype = "";
                        mAcVoucherdCls.Refdet = "";
                        mAcVoucherdCls.Remarks = mCommFunc.ConvertToString(dr["Remarks"].ToString());
                        mAcVoucherdCls.Qty = 0;
                        mAcVoucherdCls.Amt = this.Totamt;
                        mAcVoucherdCls.Trn = 1;
                        mAcVoucherdCls.Slno = 0;
                        mAcVoucherdCls.Totrow = "Y";
                        mAcVoucherdCls.Othref = "AcPost " + Convert.ToString(this.Dt.Date.ToShortDateString());
                        mAcVoucherdCls.Pdstatus = "";
                        mAcVoucherdCls.Pddt = Convert.ToDateTime("01-01-1900 00:00:00 AM");//DateTime.Now;
                        mAcVoucherdCls.Rconstatus = "";
                        mAcVoucherdCls.Rcondt = Convert.ToDateTime("01-01-1900 00:00:00 AM");//DateTime.Now;
                        mAcVoucherdCls.insertDetailData();
                    }
                }

                #endregion

                return true;
                // ******************* 
               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
            return false;
        }
        public void DeleteVoucher(DateTime DtAson, string FirmCode)
        {
            try
            {
                string OthRef = "AcPost " + Convert.ToString(DtAson.Date.ToShortDateString());
                SQL = @"delete from AcVoucherd where  vd_hdrid in (select v_id from AcVoucher where v_dt='" + DtAson.ToString("MM/dd/yyyy") + "' and  v_firmptr=  '" +
                    FirmCode.Trim() + "' and v_finyearptr='" + Finyearptr + "' and v_othref='" + OthRef + "')";
                mGlobal.LocalDBCon.ExecuteNonQuery_OnTran(SQL);

                SQL = "delete from AcVoucher where v_dt='" + DtAson.ToString("MM/dd/yyyy") + "' and  v_firmptr='" + FirmCode.Trim() + "' and v_finyearptr= '" +
                    Finyearptr + "' and v_othref='" + OthRef + "' ";
                mGlobal.LocalDBCon.ExecuteNonQuery_OnTran(SQL);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
        public int GetNextVoucherNo(String VouType)
        {
            try
            {
                SQL = "select isnull(MAX(v_vnon),0) as Vno from AcVoucher where v_vtype='" + VouType + "' and  v_firmptr='" + Firmptr + "' and v_finyearptr= '" + Finyearptr + "'";
                DataTable dtData = mGlobal.LocalDBCon.ExecuteQuery_OnTran(SQL);
                if (mCommFunc.ConvertToInt(dtData.Rows.Count) > 0)
                {
                    return mCommFunc.ConvertToInt(dtData.Rows[0]["Vno"]) + 1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
            return 0;
        }
        public bool insertData()
        {
            try
            {
                SQL = @"insert into acvoucher(v_vtype,v_vno,v_vnon,v_dt,v_tm,v_cj,v_bigacptr,v_bigacdesc,v_bigopacptr,v_bigopacdesc
                        ,v_refno,v_totamt,v_narration,v_othrefid,v_othref,v_userptr,v_finyearptr,v_pcenterptr,v_brptr,v_firmptr
                        ,v_status,v_authuserptr,v_authdt,v_authremarks,v_editmode) values ('" + this.Vtype + "','" + this.Vno + "'," + this.Vnon + ",'" + this.Dt.ToString("yyyy-MM-dd") + "','" + this.Tm.ToString("yyyy-MM-dd") + "','" + this.Cj + "'," + this.Bigacptr + ",'" + this.Bigacdesc + "'," + this.Bigopacptr + ",'" + this.Bigopacdesc + "','" + this.Refno + "'," + this.Totamt + ",'" + this.Narration + "'," + this.Othrefid + ",'" + this.Othref + "'," + this.Userptr + "," + this.Finyearptr + "," + this.Pcenterptr + "," + this.Brptr + "," + this.Firmptr + ",'" + this.Status + "'," + this.Authuserptr + ",'" + this.Authdt.ToString("yyyy-MM-dd hh:mm:ss") + "','" + this.Authremarks + "','" + this.Editmode + "') SELECT @@IDENTITY AS ID";
                //if (mGlobal.LocalDBCon.ExecuteNonQuery(SQL) > 0)
                // return true;
                DataTable dtData = mGlobal.LocalDBCon.ExecuteQuery_OnTran(SQL);
                if (dtData.Rows.Count > 0)
                {
                    this.Id = Convert.ToInt32(dtData.Rows[0][0].ToString());
                    // delete previous
                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
            return false;
        }
        public bool insertDetailData()
        {
            try
            {
////                SQL = @"insert into acvoucher(v_vtype,v_vno,v_vnon,v_dt,v_tm,v_cj,v_bigacptr,v_bigacdesc,v_bigopacptr,v_bigopacdesc
////                        ,v_refno,v_totamt,v_narration,v_othrefid,v_othref,v_userptr,v_finyearptr,v_pcenterptr,v_brptr,v_firmptr
////                        ,v_status,v_authuserptr,v_authdt,v_authremarks,v_editmode) values ('" + this.Vtype + "','" + this.Vno + "'," + this.Vnon + ",'" + this.Dt.ToString("yyyy-MM-dd") + "','" + this.Tm.ToString("yyyy-MM-dd") + "','" + this.Cj + "'," + this.Bigacptr + ",'" + this.Bigacdesc + "'," + this.Bigopacptr + ",'" + this.Bigopacdesc + "','" + this.Refno + "'," + this.Totamt + ",'" + this.Narration + "'," + this.Othrefid + ",'" + this.Othref + "'," + this.Userptr + "," + this.Finyearptr + "," + this.Pcenterptr + "," + this.Brptr + "," + this.Firmptr + ",'" + this.Status + "'," + this.Authuserptr + ",'" + this.Authdt.ToString("yyyy-MM-dd hh:mm:ss") + "','" + this.Authremarks + "','" + this.Editmode + "') SELECT @@IDENTITY AS ID";
                SQL = @"insert into acvoucherd(vd_hdrid,vd_acptr,vd_opacptr,vd_bigopactype,vd_refdet,vd_remarks,vd_qty,vd_amt,vd_trn,vd_slno,vd_totrow,vd_othref,vd_pdstatus,vd_pddt,vd_rconstatus,vd_rcondt) values("+this.Hdrid+","+this.Bigacptr+","+this.Bigopacptr+",'','','"+this.Authremarks+"','','','"+this.Totamt+"','trn','slno','totrow','otherref','"+this.Dt+"','"+this.Status+"','"+this.Dt+"')";
                //if (mGlobal.LocalDBCon.ExecuteNonQuery(SQL) > 0)
                // return true;
                DataTable dtData = mGlobal.LocalDBCon.ExecuteQuery(SQL);
                if (dtData.Rows.Count > 0)
                {
                    this.Id = Convert.ToInt32(dtData.Rows[0][0].ToString());
                    // delete previous
                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
            return false;
        }
        public void Init(String _Key, String _DbIndex)
        {
            //Init(_Key, _DbIndex, "", "");
        }
        


  
    }

}
