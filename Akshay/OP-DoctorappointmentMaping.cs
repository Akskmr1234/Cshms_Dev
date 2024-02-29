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
                DataTable dtOpdetails = mGlobal.LocalDBCon.ExecuteQuery(strSql);
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
                    strSql = @"";
                }
                else
                { }

                if (!AlreadyExistPrereg(mCommFunc.ConvertToString(txtOpno.Text)))
                { }
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