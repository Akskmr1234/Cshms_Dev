using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Globalization;

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
                string strSql = @"select * from opreg left join opbill on op_no=opb_opno where op_no='" + mCommFunc.ConvertToString(txtOpno.Text) + "'";
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
                string strDateTimeNow = DateTime.Now.ToString("yyyy-MM-dd");
                strSql = "select top(1)da_tokenno+1 as da_tokenno,SUBSTRING(da_aptime,1,8) as da_aptime from doctorappointment where da_date='" + strDateTimeNow + "' order by da_tokenno desc";
                DataTable dtdadata = mGlobal.LocalDBCon.ExecuteQuery(strSql);
                string strTokenNo =  mCommFunc.ConvertToString(dtdadata.Rows[0]["da_tokenno"]);
                string strAptime = mCommFunc.ConvertToString(dtdadata.Rows[0]["da_aptime"]);
                DateTime time = DateTime.ParseExact(strAptime, "h:mm tt", CultureInfo.InvariantCulture);
                string strAddAptime = time.AddMinutes(15).ToString("h:mm tt");
                string strPreregusername = "";
                DateTime dtopdate = Convert.ToDateTime(dtOpdetails.Rows[0]["op_dt"]);
                string strOpDate = dtopdate.ToString("yyyy-MM-dd HH:mm:ss");
                DateTime dtoptime = Convert.ToDateTime(dtOpdetails.Rows[0]["op_time"]);
                string strOptime = dtoptime.ToString("yyyy-MM-dd HH:mm:ss");
                string strGenderptr = mCommFunc.ConvertToString(dtOpdetails.Rows[0]["op_gender"]) == "Male" ? "16" : "17";
                //DateTime strDob = mCommFunc.ChangeDateFormatTo_DD_MM_YYYY(dtOpdetails.Rows[0]["op_dob"]);
                DateTime dtdob = Convert.ToDateTime(dtOpdetails.Rows[0]["op_dob"]);
                DateTime dtnow = DateTime.Now;

                // Calculate full years
                int years = dtnow.Year - dtdob.Year;
                if (dtnow.Month < dtdob.Month || (dtnow.Month == dtdob.Month && dtnow.Day < dtdob.Day))
                {
                    years--;
                }

                // Calculate months
                int months = 0;
                if (dtnow.Month >= dtdob.Month)
                {
                    months = dtnow.Month - dtdob.Month;
                    if (dtnow.Day < dtdob.Day)
                    {
                        months--;
                    }
                }
                else
                {
                    months = 12 - (dtdob.Month - dtnow.Month);
                    if (dtnow.Day < dtdob.Day)
                    {
                        months--;
                    }
                }

                // Adjust month if it became negative
                if (months < 0)
                {
                    months += 12;
                    years--;
                }

                // Calculate days
                int days = dtnow.Day - dtdob.Day;
                if (days < 0)
                {
                    // Find the last day of the previous month
                    DateTime lastDayOfPrevMonth = new DateTime(dtnow.Year, dtnow.Month, 1).AddDays(-1);
                    days += lastDayOfPrevMonth.Day;
                }

                string strDob = dtdob.ToString("yyyy-MM-dd HH:mm:ss");
                string strAgeYears = years.ToString();
                string strAgeMonths = months.ToString();
                string strAgeDays = days.ToString();

                string strPreregid = "";
                string strFirmptr = "1";
                string strBranchptr = "";

                strSql = "select * from firmmas";
                DataTable dtFirmmas = mGlobal.LocalDBCon.ExecuteQuery(strSql);
                if (dtFirmmas != null && dtFirmmas.Rows.Count > 0)
                {
                    if (mCommFunc.ConvertToString(dtFirmmas.Rows[0]["f_desc"]) == "Nura Mumbai")
                        strBranchptr = "3";
                    else if (mCommFunc.ConvertToString(dtFirmmas.Rows[0]["f_desc"]) == "Nura Delhi")
                        strBranchptr = "2";
                    else if (mCommFunc.ConvertToString(dtFirmmas.Rows[0]["f_desc"]) == "Nura Hyderabad")
                        strBranchptr = "4";
                    else if (mCommFunc.ConvertToString(dtFirmmas.Rows[0]["f_desc"]) == "Nura Bangalore")
                        strBranchptr = "1";

                }
                strSql = @"select * from prereg where prereg_username='" + mCommFunc.ConvertToString(dtOpdetails.Rows[0]["op_email"]) + "'";
                DataTable dtUsercheck = mGlobal.LocalDBCon.ExecuteQuery(strSql);
                if (dtUsercheck.Rows.Count <= 0)
                {
                    strPreregusername = mCommFunc.ConvertToString(dtOpdetails.Rows[0]["op_email"]);

                }
                else
                {
                    strSql = @"select * from prereg where prereg_username='" + mCommFunc.ConvertToString(dtOpdetails.Rows[0]["op_mobile"]) + "'";
                    dtUsercheck = mGlobal.LocalDBCon.ExecuteQuery(strSql);
                    if (dtUsercheck.Rows.Count <= 0)
                    {
                        strPreregusername = mCommFunc.ConvertToString(dtOpdetails.Rows[0]["op_mobile"]);
                    }
                    else
                    {
                        strPreregusername = mCommFunc.ConvertToString(dtOpdetails.Rows[0]["op_fname"]);
                    }
 
                }



                if (!AlreadyExistPrereg(mCommFunc.ConvertToString(txtOpno.Text)))
                {
                    strSql = @"insert into prereg (prereg_type,prereg_dt,prereg_tm,prereg_title,prereg_fname,prereg_lname,prereg_gender,prereg_ageday,prereg_agemonth,prereg_ageyear,prereg_dob,prereg_address1,prereg_address2,prereg_city,prereg_phone,prereg_mobile1,prereg_mobile2,prereg_email,prereg_referralno,prereg_username,prereg_password,prereg_opno,prereg_packagetypevalue,prereg_packagetype,prereg_active,updttm,prereg_user,prereg_zip,prereg_landmark,prereg_mob1counrtycode,prereg_mob2counrtycode,prereg_mob1countrycode,prereg_mob2countrycode,prereg_otherdetails1,prereg_firmid,prereg_branchid,prereg_finyearid,prereg_state,prereg_country,prereg_device_notification_id)  values('SGN','" + strOpDate + "','" + strOpDate + "','" + mCommFunc.ConvertToString(dtOpdetails.Rows[0]["op_title"]) + "','" + mCommFunc.ConvertToString(dtOpdetails.Rows[0]["op_fname"]) + "','" + mCommFunc.ConvertToString(dtOpdetails.Rows[0]["op_lname"]) + "','" + mCommFunc.ConvertToString(dtOpdetails.Rows[0]["op_gender"]) + "','" + strAgeDays + "','" + strAgeMonths + "','" + strAgeYears + "','" + strDob + "','" + mCommFunc.ConvertToString(dtOpdetails.Rows[0]["op_add1"]) + "','" + mCommFunc.ConvertToString(dtOpdetails.Rows[0]["op_add2"]) + "',NULL,'" + mCommFunc.ConvertToString(dtOpdetails.Rows[0]["op_phone"]) + "','" + mCommFunc.ConvertToString(dtOpdetails.Rows[0]["op_mobile"]) + "',NULL,'" + mCommFunc.ConvertToString(dtOpdetails.Rows[0]["op_email"]) + "',NULL,'" + strPreregusername + "','Nura@1234','" + mCommFunc.ConvertToString(dtOpdetails.Rows[0]["op_no"]) + "','" + strGenderptr + "','ITM','" + mCommFunc.ConvertToString(dtOpdetails.Rows[0]["op_active"]) + "','" + strOpDate + "','" + mCommFunc.ConvertToString(dtOpdetails.Rows[0]["op_user"]) + "','" + mCommFunc.ConvertToString(dtOpdetails.Rows[0]["op_zip"]) + "',NULL,NULL,NULL,NULL,NULL,'" + mCommFunc.ConvertToString(dtOpdetails.Rows[0]["op_otherdetails1"]) + "','" + strFirmptr + "','" + strBranchptr + "','" + mCommFunc.ConvertToString(dtOpdetails.Rows[0]["op_finyearid"]) + "','" + mCommFunc.ConvertToString(dtOpdetails.Rows[0]["op_state"]) + "','" + mCommFunc.ConvertToString(dtOpdetails.Rows[0]["op_country"]) + "',NULL) select SCOPE_IDENTITY() AS ID";
                    DataTable dtPreregid = mGlobal.LocalDBCon.ExecuteQuery(strSql);
                    if (dtPreregid != null && dtPreregid.Rows.Count > 0)
                        strPreregid = mCommFunc.ConvertToString(dtPreregid.Rows[0][0]); 
                    
                }
                else
                {
                    MessageBox.Show("Already Exist");
                    return;
                }

                if (!AlreadyExistDoctorappointment(mCommFunc.ConvertToString(txtOpno.Text)))
                {
                    strSql = "insert into doctorappointment (da_doctorptr,da_date,da_opno,da_lname,da_fname,da_add1,da_add2,da_place,da_phone,da_mobile,da_tokenno,da_aptime,da_remarks,da_userid,da_user,da_time,da_canflg,da_canuser,da_canuserid,da_cantime,da_title,da_visitstatus,da_visitid,da_mode,da_paystatus,da_referencetype,da_referenceid,da_bkstatus,da_bkstatususer,da_bkstatusdttm,da_packagetype,da_packagetypevalue,da_newopno,da_billvalue,da_couponvalue,da_couponno,da_delivarymode,da_delivarydttm,da_delivaryaddressid,da_zip,da_landmark,da_questfilled,da_consentfilled,Da_questmailsendstatus,da_consentmailsendstatus,da_fitkitmailsendstatus,da_fitkitstatus,da_billid,da_reportmailsendstatus,da_phyverified,da_rptuploaded,da_phyrptstatus,da_rptstatus,da_email,da_gender,da_dob,da_reportmailsenddttm,da_canremarks,da_sourcetype,da_referrerptr,da_firmptr,da_branchptr,da_samplestatus,da_corporateptr,da_corporateinfo,da_otherdetails1,da_updateduserid,da_updatedtime)  values('22','" + strOpDate + "','','" + mCommFunc.ConvertToString(dtOpdetails.Rows[0]["op_lname"]) + "','" + mCommFunc.ConvertToString(dtOpdetails.Rows[0]["op_fname"]) + "','" + mCommFunc.ConvertToString(dtOpdetails.Rows[0]["op_add1"]) + "','" + mCommFunc.ConvertToString(dtOpdetails.Rows[0]["op_add2"]) + "','" + mCommFunc.ConvertToString(dtOpdetails.Rows[0]["op_place"]) + "','" + mCommFunc.ConvertToString(dtOpdetails.Rows[0]["op_phone"]) + "','" + mCommFunc.ConvertToString(dtOpdetails.Rows[0]["op_mobile"]) + "','" + strTokenNo + "','" + strAddAptime + "','" + mCommFunc.ConvertToString(dtOpdetails.Rows[0]["op_remarks"]) + "','" + mCommFunc.ConvertToString(dtOpdetails.Rows[0]["op_userid"]) + "','" + mCommFunc.ConvertToString(dtOpdetails.Rows[0]["op_user"]) + "','" + strOptime + "',NULL,NULL,NULL,NULL,'" + mCommFunc.ConvertToString(dtOpdetails.Rows[0]["op_title"]) + "',NULL,'-1',NULL,'P','sgn','" + strPreregid + "','B','" + mCommFunc.ConvertToString(dtOpdetails.Rows[0]["op_email"]) + "','" + strOpDate + "','ITM','" + strGenderptr + "','" + mCommFunc.ConvertToString(dtOpdetails.Rows[0]["op_no"]) + "','" + mCommFunc.ConvertToLong(dtOpdetails.Rows[0]["opb_amt"]) + "',NULL,NULL,'DEF','" + strOpDate + "','" + strPreregid + "','" + mCommFunc.ConvertToString(dtOpdetails.Rows[0]["op_zip"]) + "',NULL,NULL,'',NULL,NULL,NULL,NULL,'" + mCommFunc.ConvertToString(dtOpdetails.Rows[0]["op_id"]) + "',NULL,NULL,NULL,NULL,NULL,'" + mCommFunc.ConvertToString(dtOpdetails.Rows[0]["op_email"]) + "','" + mCommFunc.ConvertToString(dtOpdetails.Rows[0]["op_gender"]) + "','" + strDob + "',NULL,NULL,NULL,NULL,'" + strFirmptr + "','" + strBranchptr + "',NULL,NULL,'" + mCommFunc.ConvertToString(dtOpdetails.Rows[0]["op_corporateinfo"]) + "',NULL,'1','" + strOpDate + "');";
                 }
                else
                {
                    MessageBox.Show("Already Exist");
                    return;
                }

                int res = mGlobal.LocalDBCon.ExecuteNonQuery(strSql);
                if (res > 0)
                {
                    MessageBox.Show("Success");
                }
                else
                    MessageBox.Show("Error Occured");
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message.ToString()); }
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