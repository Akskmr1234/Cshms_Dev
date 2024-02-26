using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CsHms.Akshay
{
    public partial class OpBillModalityMap : Form
    {
        Global mGlobal = new Global();
        CommFuncs mCommfunc = new CommFuncs();
        DataTable dtopbillddata = new DataTable();
        public OpBillModalityMap()
        {
            InitializeComponent();
        }

        
        //Checking already existing or not
        private bool CheckAlreadyExist(string strdetid)
        {
            try
            {
                string strsql = @"select * from modalitypatientstatustran  where mpst_detrefid='" + strdetid + "'";
                DataTable dtdata = mGlobal.LocalDBCon.ExecuteQuery_OnTran(strsql);
                if (dtdata.Rows.Count <= 0)
                {
                    return false;
                }
                else
                    return true;
            }
            catch (Exception ex)
            { 
            }
            return false;
        }
        private void btnMap_Click(object sender, EventArgs e)
        {
            try
            {
                mGlobal.LocalDBCon.BeginTrans();
                for (int i = 0; i < dtopbillddata.Rows.Count; i++)
                {
                    
                    string currentDateTime = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss.fff");
                    string strOpbid = mCommfunc.ConvertToString(dtopbillddata.Rows[i]["opbd_id"]);
                    string strRefid = dtopbillddata.Rows[i]["opbd_hdrid"].ToString();
                    int intRefno = mCommfunc.ConvertToInt(dtopbillddata.Rows[i]["opbd_id"]);
                    string strItemptr = dtopbillddata.Rows[i]["opbd_itemptr"].ToString();
                    //Get the last accession number
                    //DataTable dtAccessiondata = mGlobal.LocalDBCon.ExecuteQuery_OnTran(@"SELECT MAX(CAST(SUBSTRING(mpst_accessionno, 5, 7) AS INT)) AS highest_number FROM modalitypatientstatustran WHERE mpst_accessionno LIKE '1MUA%'");
                    DataTable dtAccessiondata = mGlobal.LocalDBCon.ExecuteQuery_OnTran(@"select blno_no from billnos where blno_code='MRDAN'");
                    int intAccessionno = mCommfunc.ConvertToInt(dtAccessiondata.Rows[0][0]) + 1;// To be changed
                    //Get modalityptr with op bill id
                    DataTable dtModality = mGlobal.LocalDBCon.ExecuteQuery_OnTran(@"select mgig_modalitygrouppptr from opbilld left join item on opbd_itemptr=itm_code left join modalitygroupitemgroupmap on itm_groupptr=mgig_modalitygrouppptr where opbd_id='" + strOpbid + "'");
                    string strModalityptr = dtModality.Rows[0][0].ToString();
                    //Checking already exxisting or not   
                    
                    if (CheckAlreadyExist(intRefno.ToString()) == false)
                    {

                        string strqry = @"INSERT INTO modalitypatientstatustran (mpst_modmodeptr,mpst_module,mpst_refid,mpst_refno,mpst_detrefid,mpst_accessionno,mpst_modalityptr,mpst_itemptr,mpst_statusptr,mpst_processstarttime,mpst_processendttime,mpst_processtimetaken,mpst_modalitystarttime,mpst_modalityendtime,mpst_modalitytimetaken,mpst_technicianstaffptr,mpst_technicianremarks,mpst_remarks,mpst_user,mpst_entrytime,mpst_patmedreportsid,mpst_ormstatus,mpst_ormstatusdttm,mpst_otherdet1,mpst_otherdet2) 
                                     VALUES ('RAD', 'OPB', '" + strRefid + "', '" + strRefid + "','" + intRefno + "','" + "1MUA" + intAccessionno + "','" + strModalityptr + "','" + strItemptr + "','ARR','" + currentDateTime + "',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'admin',NULL,NULL,'Y',NULL,NULL,NULL)";

                        int res = mGlobal.LocalDBCon.ExecuteNonQuery_OnTran(strqry);

                        if (res <= 0)
                        {
                            MessageBox.Show("Error Occurred");
                            mGlobal.LocalDBCon.RollbackTrans();
                        }
                        else
                        {
                            mGlobal.LocalDBCon.ExecuteNonQuery_OnTran(@"update billnos set blno_no='" + intAccessionno + "' where  blno_code='MRDAN'");
                            if (dtopbillddata.Rows.Count - 1 == i)
                            MessageBox.Show("Success");
                        }
                    }
                    else
                    {
                        string updateQuery = @"UPDATE modalitypatientstatustran 
                                   SET mpst_modmodeptr = 'RAD',
                                       mpst_module = 'OPB',
                                       mpst_refid = '" + strRefid + @"',
                                       mpst_refno = '" + strRefid + @"',
                                       mpst_detrefid = '" + intRefno + @"',                                      
                                       mpst_modalityptr = '" + strModalityptr + @"',
                                       mpst_itemptr = '" + strItemptr + @"',
                                       mpst_statusptr = 'ARR',
                                       mpst_processstarttime = '" + currentDateTime + @"'
                                   WHERE   mpst_detrefid='" + intRefno + "'";


                        int updateRes = mGlobal.LocalDBCon.ExecuteNonQuery_OnTran(updateQuery);

                        if (updateRes <= 0)
                        {
                            MessageBox.Show("Error Occurred while updating");
                            mGlobal.LocalDBCon.RollbackTrans();
                        }
                        else
                        {
                            if (dtopbillddata.Rows.Count - 1 == i)
                            {
                                MessageBox.Show("Success");
                               
                            }
                        }
                    }
                }
                mGlobal.LocalDBCon.CommitTrans();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
                mGlobal.LocalDBCon.RollbackTrans();
            }
        }   
        private void txtOpbno_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                if (txtOpbNo.Text != "")
                {

                    string strqry = @"select opbd_id,opbd_itemptr,opbd_hdrid,opbd_itemptr,opbd_itemdesc from opbill left join opbilld on opb_id=opbd_hdrid left join item on itm_code=opbd_itemptr where opb_bno='" + txtOpbNo.Text.ToString() + "' and item.itm_groupptr in ('CT','BMD','ES','EYE','MA','COL','OBI','ORL')";
                    
                    dtopbillddata = mGlobal.LocalDBCon.ExecuteQuery(strqry);
                    if (dtopbillddata.Rows.Count > 0)
                    {
                        dgvData.DataSource = dtopbillddata;
                    }
                    else
                        dgvData.DataSource = null;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}