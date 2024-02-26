using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace CsHms.Akshay
{
    public partial class ConfigSettings : Form
    {
        public ConfigSettings()
        {
            InitializeComponent();
            FillCbx();
        }
        Global mGlobal = new Global();
        private void FillCbx()
        {
            try
            {
                string strSql = @"select id,description from config";
                DataTable dtConfig = mGlobal.LocalDBCon.ExecuteQuery(strSql);
                if(dtConfig!=null && dtConfig.Rows.Count>0)
                {
                    cbxFirm.DataSource = dtConfig;
                    cbxFirm.DisplayMember = "description";
                    cbxFirm.ValueMember = "id";
                }
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message.ToString()); }
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {

            try
            {
                string strSql = "select database_connectionstring,logdb_connectionstring from config where id='" + cbxFirm.SelectedValue + "'";
                DataTable dtConstrings = mGlobal.LocalDBCon.ExecuteQuery(strSql);
                if (dtConstrings != null && dtConstrings.Rows.Count > 0)
                {
                    string strConstring = dtConstrings.Rows[0]["database_connectionstring"].ToString();
                    string strLogConstring = dtConstrings.Rows[0]["logdb_connectionstring"].ToString();
                    SqlConnection Conn = null;
                    DbConnSql dbRemovecon = new DbConnSql(Conn);
                    DbConnSql dbConString = new DbConnSql(strConstring);
                    DbConnSql_Log dbLogRemovecon = new DbConnSql_Log(Conn);
                    DbConnSql_Log dbLogConString = new DbConnSql_Log(strLogConstring);
                    this.Close();
                }
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message.ToString()); }
        }
    }
}