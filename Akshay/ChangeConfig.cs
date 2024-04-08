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
    public partial class ChangeConfig : Form
    {
        public ChangeConfig()
        {
            InitializeComponent();
            GetDbs();
            
            
        }
        Global mGlobal = new Global();
        CommFuncs mCommFunc = new CommFuncs();
        private void btnBillNo_Click(object sender, EventArgs e)
        {
            Form frm = new CsHms.Akshay.BillUpdate();
            frm.Show();
        }
        private void GetDbs()
        {
            try
            {
                string strSql = @"SELECT name FROM sys.databases";
                DataTable dtDbs = mGlobal.LocalDBCon.ExecuteQuery(strSql);
                if (dtDbs != null && dtDbs.Rows.Count > 0)
                {
                    cbxDatabase.DataSource = dtDbs;
                    cbxDatabase.DisplayMember = "name";
                    cbxDatabase.ValueMember = "name";
                }
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message.ToString()); }
        }

        private void cbxDatabase_SelectionChangeCommitted(object sender, EventArgs e)
        {
            string strConstring = System.Configuration.ConfigurationSettings.AppSettings.Get("SqlConString");
            string strUpdatedConstring = ReplaceInitialCatalog(strConstring,mCommFunc.ConvertToString(cbxDatabase.SelectedValue));
            SqlConnection Conn = null;
            DbConnSql dbRemovecon = new DbConnSql(Conn);
            DbConnSql dbConString = new DbConnSql(strUpdatedConstring);
        }
        static string ReplaceInitialCatalog(string connectionString, string newInitialCatalog)
        {
            // Split the connection string by semicolon into key-value pairs
            string[] parts = connectionString.Split(';');

            // Find and replace the value of Initial Catalog
            for (int i = 0; i < parts.Length; i++)
            {
                if (parts[i].Trim().StartsWith("Initial Catalog", StringComparison.OrdinalIgnoreCase))
                {
                    // Replace the value part
                    parts[i] = "Initial Catalog=" + newInitialCatalog;
                    break; // Once replaced, no need to continue the loop
                }
            }

            // Reconstruct the connection string
            string modifiedConnectionString = string.Join(";", parts);
            return modifiedConnectionString;
        }

        private void btnFinancialYear_Click(object sender, EventArgs e)
        {
            UpdateFinancialYear();
        }
        private void UpdateFinancialYear()
        {
            try
            {
                string strDate = DateTime.Now.ToString("ddMMMMyyyy_HH_mm_ss");
                string strSql = "select * into financialyear_Backup" + strDate + " from financialyear";
                mGlobal.LocalDBCon.ExecuteQuery(strSql);
                DateTime dtnow=DateTime.Now;
                string PrevYear = mCommFunc.ConvertToString(Convert.ToInt32(dtnow.Year) - 1);
                string CurrentYear = mCommFunc.ConvertToString(dtnow.Year).Substring(2);
                 strSql = "update financialyear set fy_desc='" + PrevYear + "-" + CurrentYear + "' where fy_id='1'";
                int res = 0;
                if (MessageBox.Show("Are You sure want to update", "Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    res = mGlobal.LocalDBCon.ExecuteNonQuery(strSql);
                else
                    return;
                if (res > 0)
                    MessageBox.Show("Updated");
                else
                    MessageBox.Show("Error Occured");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

    }
}