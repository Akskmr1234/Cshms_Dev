using System;
using System.Collections.Generic;
using System.Text;
using System.Data.Common;
using System.Windows.Forms;
using System.Data;

namespace CsHms.Akshay.Class
{
    class VoucherdCls
    {
        Global mGlobal = new Global();
        CommFuncs mCommFunc = new CommFuncs();
        const String TABLE_NAME = "acvoucher";
        const String PRIMARY_KEY = "v_id";
        String mstrTableName = "";   
        String SQL;
        int intId;
        long lngHdrid;
        int intAcptr;
        int intOpacptr;
        string strBigopactype;
        string strRefdet;
        string strRemarks;
        int intQty;
        decimal decAmt;
        int intTrn;
        int intSlno;
        string strTotrow;
        string strOthref;
        string strPdstatus;
        DateTime dtPddt;
        string strRconstatus;
        DateTime dtRcondt;
        public int Id
        {
            set { intId = value; }
            get { return intId; }
        }
        public long Hdrid
        {
            set { lngHdrid=value; }
            get { return lngHdrid; }
        }
        public int Acptr
        {
            set { intAcptr = value; }
            get { return intAcptr; }
        }
        public int Opacptr
        {
            set { intOpacptr = value; }
            get { return intOpacptr; }
        }
        public string Bigopactype
        {
            set { strBigopactype = value; }
            get { return strBigopactype; }
        }
        public string Refdet
        {
            set { strRefdet = value; }
            get { return strRefdet; }
        }
        public string Remarks
        {
            set { strRemarks = value; }
            get { return strRemarks; }
        }
        public int Qty
        {
            set { intQty = value; }
            get { return intQty; }
        }
        public decimal Amt
        {
            set { decAmt = value; }
            get { return Amt; }
        }
        public int Trn
        {
            set { intTrn = value; }
            get { return Trn; }
        }
        public int Slno
        {
            set { intSlno = value; }
            get { return intSlno; }
        }
        public string Totrow
        {
            set { strTotrow = value; }
            get { return strTotrow; }
        }
        public string Othref
        {
            set {strOthref=value; }
            get { return strOthref; }
        }
        public string Pdstatus
        {
            set { strPdstatus = value; }
            get { return strPdstatus; }
        }
        public DateTime Pddt
        {
            set { dtPddt = value; }
            get { return dtPddt; }
        }
        public string Rconstatus
        {
            set { strRconstatus = value; }
            get { return strRconstatus; }
        }
        public DateTime Rcondt
        {
            set { dtRcondt = value; }
            get { return dtRcondt; }
        }      
        public bool insertDetailData()
        {
            try
            {
                ////                SQL = @"insert into acvoucher(v_vtype,v_vno,v_vnon,v_dt,v_tm,v_cj,v_bigacptr,v_bigacdesc,v_bigopacptr,v_bigopacdesc
                ////                        ,v_refno,v_totamt,v_narration,v_othrefid,v_othref,v_userptr,v_finyearptr,v_pcenterptr,v_brptr,v_firmptr
                ////                        ,v_status,v_authuserptr,v_authdt,v_authremarks,v_editmode) values ('" + this.Vtype + "','" + this.Vno + "'," + this.Vnon + ",'" + this.Dt.ToString("yyyy-MM-dd") + "','" + this.Tm.ToString("yyyy-MM-dd") + "','" + this.Cj + "'," + this.Bigacptr + ",'" + this.Bigacdesc + "'," + this.Bigopacptr + ",'" + this.Bigopacdesc + "','" + this.Refno + "'," + this.Totamt + ",'" + this.Narration + "'," + this.Othrefid + ",'" + this.Othref + "'," + this.Userptr + "," + this.Finyearptr + "," + this.Pcenterptr + "," + this.Brptr + "," + this.Firmptr + ",'" + this.Status + "'," + this.Authuserptr + ",'" + this.Authdt.ToString("yyyy-MM-dd hh:mm:ss") + "','" + this.Authremarks + "','" + this.Editmode + "') SELECT @@IDENTITY AS ID";
                SQL = @"insert into acvoucherd(vd_hdrid,vd_acptr,vd_opacptr,vd_bigopactype,vd_refdet,vd_remarks,vd_qty,vd_amt,vd_trn,vd_slno,vd_totrow,vd_othref,vd_pdstatus,vd_pddt,vd_rconstatus,vd_rcondt) values(" + this.lngHdrid + "," + this.intAcptr + "," + this.intOpacptr + ",'" + this.strBigopactype + "','" + this.strRefdet + "','" + this.strRemarks + "','" + this.intQty + "','" + this.decAmt + "','" + this.intTrn + "','" + this.intSlno + "','" + this.strTotrow + "','" + this.strOthref + "','" + this.strPdstatus + "','" + this.dtPddt + "','" + this.strRconstatus + "','"+this.dtRcondt+"')";
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
        public void Init(String _Key, String _DbIndex)
        {
            //Init(_Key, _DbIndex, "", "");
        }
        

    }
}
