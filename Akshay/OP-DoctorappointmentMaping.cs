using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CsHms.Akshay
{
    public partial class OP_DoctorappointmentMaping : Form
    {
        public OP_DoctorappointmentMaping()
        {
            InitializeComponent();
        }
        Global mGlobal = new Global();
        CommFuncs mCommFunc = new CommFuncs();
        DataTable dtOpdetails = new DataTable();
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtOpno_Validating(object sender, CancelEventArgs e)
        {
            GetData();
        }
        private void GetData()
        {
            try
            {
                string strSql = @"select * from opreg  where op_no='"+mCommFunc.ConvertToString(txtOpno.Text)+"'";
                 dtOpdetails = mGlobal.LocalDBCon.ExecuteQuery(strSql);
                if (dtOpdetails != null && dtOpdetails.Rows.Count > 0)
                {
                    lblEmail.Text = mCommFunc.ConvertToString(dtOpdetails.Rows[0]["op_email"]);
                    lblName.Text = mCommFunc.ConvertToString(dtOpdetails.Rows[0]["op_fname"]);
                    lblPhone.Text = mCommFunc.ConvertToString(dtOpdetails.Rows[0]["op_mobile"]);
                }
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message.ToString()); }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtOpno.Text = "";
            lblPhone.Text = "";
            lblName.Text = "";
            lblEmail.Text = "";
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SubmitData();
        }
        private void SubmitData()
        {
            try
            {
                string strSql = "";
                if (!AlreadyExistDoctorappointment(mCommFunc.ConvertToString(txtOpno.Text)))
                {
                    strSql = @"insert into doctorappointment (da_doctorptr,da_date,da_opno,da_lname,da_fname,da_add1,da_add2,da_place,da_phone,da_mobile,da_tokenno,da_aptime,da_remarks,da_userid,da_user,da_time,da_canflg,da_canuser,da_canuserid,da_cantime,da_title,da_visitstatus,da_visitid,da_mode,da_paystatus,da_referencetype,da_referenceid,da_bkstatus,da_bkstatususer,da_bkstatusdttm,da_packagetype,da_packagetypevalue,da_newopno,da_billvalue,da_couponvalue,da_couponno,da_delivarymode,da_delivarydttm,da_delivaryaddressid,da_zip,da_landmark,da_questfilled,da_consentfilled,Da_questmailsendstatus,da_consentmailsendstatus,da_fitkitmailsendstatus,da_fitkitstatus,da_billid,da_reportmailsendstatus,da_phyverified,da_rptuploaded,da_phyrptstatus,da_rptstatus,da_email,da_gender,da_dob,da_reportmailsenddttm,da_canremarks,da_sourcetype,da_referrerptr,da_firmptr,da_branchptr,da_samplestatus,da_corporateptr,da_corporateinfo,da_otherdetails1,da_updateduserid,da_updatedtime,da_consentscannedstatus) 
                        values('" + mCommFunc.ConvertToString(dtOpdetails.Rows[0]["op_doctorptr"]) + "','" + mCommFunc.ConvertToString(dtOpdetails.Rows[0]["op_dt"]) + "','" + mCommFunc.ConvertToString(dtOpdetails.Rows[0]["op_no"]) + "','" + mCommFunc.ConvertToString(dtOpdetails.Rows[0]["op_lname"]) + "','" + mCommFunc.ConvertToString(dtOpdetails.Rows[0]["op_fname"]) + "','" + mCommFunc.ConvertToString(dtOpdetails.Rows[0]["op_add1"]) + "','" + mCommFunc.ConvertToString(dtOpdetails.Rows[0]["op_add2"]) + "','" + mCommFunc.ConvertToString(dtOpdetails.Rows[0]["op_place"]) + "','" + mCommFunc.ConvertToString(dtOpdetails.Rows[0]["op_phone"]) + "','" + mCommFunc.ConvertToString(dtOpdetails.Rows[0]["op_mobile"]) + "','da_tokenno','" + mCommFunc.ConvertToString(dtOpdetails.Rows[0]["op_time"]) + "','" + mCommFunc.ConvertToString(dtOpdetails.Rows[0]["op_remarks"]) + "','" + mCommFunc.ConvertToString(dtOpdetails.Rows[0]["op_userid"]) + "','" + mCommFunc.ConvertToString(dtOpdetails.Rows[0]["op_user"]) + "','" + mCommFunc.ConvertToString(dtOpdetails.Rows[0]["op_time"]) + "','da_canflag','da_canuser','da_canuserid','da_cantime','da_title','davisitid','da_mode','da_paystatus','da_referencetype','da_referenceid','da_bkstatus','da_bkstatususer','da_bkstatusdttm','da_packagetype','da_packagetypevalue','da_newopno','da_billvalue','da_couponvalue','da_couponno','da_delivarymode','da_delivarydttm','da_delivaryaddressid','da_zip','da_landmark','da_questfilled','da_consentfilled','Da_questmailsendstatus','da_consentmailsendstatus','da_fitkitmailsendstatus','da_fitkitstatus','da_billid','da_reportmailsendstatus','da_phyverified','da_rptuploaded','da_phyrptstatus','da_rptstatus','da_email','da_gender','da_dob','da_reportmailsenddttm','da_canremarks','da_sourcetype','da_referrerptr','da_firmptr','da_branchptr','da_samplestatus','da_corporateptr','da_corporateinfo','da_otherdetails1','da_updateduserid','da_updatedtime','da_consentscannedstatus')";
                }
                else
                { }

                if (!AlreadyExistPrereg(mCommFunc.ConvertToString(txtOpno.Text)))
                {
                    strSql = @"insert into prereg (prereg_type,prereg_dt,prereg_tm,prereg_title,prereg_fname,prereg_lname,prereg_gender,prereg_ageday,prereg_agemonth,prereg_ageyear,prereg_dob,prereg_address1,prereg_address2,prereg_city,prereg_phone,prereg_mobile1,prereg_mobile2,prereg_email,prereg_referralno,prereg_username,prereg_password,prereg_opno,prereg_packagetypevalue,prereg_packagetype,prereg_active,updttm,prereg_user,prereg_zip,prereg_landmark,prereg_mob1counrtycode,prereg_mob2counrtycode,prereg_mob1countrycode,prereg_mob2countrycode,prereg_otherdetails1,prereg_firmid,prereg_branchid,prereg_finyearid,prereg_state,prereg_country,prereg_device_notification_id,prereg_batch_parentid,prereg_batch_mode)
values('prereg_type','prereg_dt','prereg_tm','prereg_title','prereg_fname','prereg_lname','prereg_gender','prereg_ageday','prereg_agemonth','prereg_ageyear','prereg_dob','prereg_address1','prereg_address2','prereg_city','prereg_phone','prereg_mobile1','prereg_mobile2','prereg_email','prereg_referralno','prereg_username','prereg_password','prereg_opno','prereg_packagetypevalue','prereg_packagetype','prereg_active','updttm',,prereg_user,prereg_zip,prereg_landmark,prereg_mob1counrtycode,prereg_mob2counrtycode,prereg_mob1countrycode,prereg_mob2countrycode,prereg_otherdetails1,prereg_firmid,prereg_branchid,prereg_finyearid,prereg_state,prereg_country,prereg_device_notification_id,prereg_batch_parentid,prereg_batch_mode)";
                }
                else
                { }
            }
            catch (Exception ex)
            { }
        }
        private bool AlreadyExistDoctorappointment(string strOpno)
        {
            try
            {

               string strSql=@"select * from doctorappointment where da_newopno='"+strOpno+"'";
               DataTable dtdoctorappointment = mGlobal.LocalDBCon.ExecuteQuery(strSql);
               if (dtdoctorappointment != null && dtdoctorappointment.Rows.Count > 0)
               {
                   return true;
               }
               else
                   return false;

 
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message.ToString());
            return false;
        }
        }
        private bool AlreadyExistPrereg(string strOpno)
        {
            try
            {
                string strSql = @"select * from prereg where prereg_opno='" + strOpno + "'";
                DataTable dtprereg = mGlobal.LocalDBCon.ExecuteQuery(strSql);
                if (dtprereg != null && dtprereg.Rows.Count > 0)
                {
                    return true;
                }
                else
                return false;


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
                return false;
            }
        }
    }
    }