using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using CsHms;

namespace CsHms
{
    public partial class Search : Form
    {
        int vDefSearchFldIndex = 0;
		bool isFirst = true;
        public String DefSearchText = "";
        public String Query = "";
        public String FilterCond = "";
        public int ReturnFldIndex = 0;
        public String ColumnHeader = "";
        public String ColumnWidth = "";
        public bool Cancelled = true;
        public String ReturnValue;
        public DataGridViewRow ReturnRow;

        DbConnSql mdbAcc = new DbConnSql();

        public Search()
        {
            InitializeComponent();
        }
        private void setDataGrid()
        {
            try
            {
                char[] chrSep ={ '|' };
                String[] strColHead = ColumnHeader.Split(chrSep);
                String[] strColWidth = ColumnWidth.Split(chrSep);

                //dgvDet.ColumnCount = cboSearchby.Items.Count;
                for (int intLoop1 = 0; intLoop1 < dgvDet.ColumnCount; intLoop1++)
                {
                    dgvDet.Columns[intLoop1].HeaderText = strColHead[intLoop1].ToString();
                    dgvDet.Columns[intLoop1].Width = int.Parse(strColWidth[intLoop1].ToString());
                }
                dgvDet.RowHeadersWidth = 4;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void Search_Load(object sender, EventArgs e)
        {
            cboSearchby.Items.Clear();
            cboCriteria.SelectedIndex = 0;
            DataTable dt = mdbAcc.ExecuteQuery(Query + " where 1=2");
            if (dt.Columns.Count <= 0) return;

			//Setting Combo Text & value
			DataTable dtCombo = new DataTable();
			dtCombo.Columns.Add("vText");
			dtCombo.Columns.Add("vValue");
			DataRow dr = null;
			char[] chrSep ={ '|' };
			String[] strColHead = ColumnHeader.Split(chrSep);
            for (int intLoop1 = 0; intLoop1 < dt.Columns.Count; intLoop1++)
            {
				dr = dtCombo.NewRow();
				dr["vText"] = strColHead[intLoop1].ToString();
				dr["vValue"] = dt.Columns[intLoop1].Caption;
				dtCombo.Rows.Add(dr);
            }
			cboSearchby.DataSource = dtCombo;
			cboSearchby.DisplayMember = "vText";
			cboSearchby.ValueMember = "vValue";
			cboSearchby.Refresh();
            cboSearchby.SelectedIndex = vDefSearchFldIndex;
            txtSearchText.Text = DefSearchText;
            SearchData();
        }
        private void SearchData()
        {
			isFirst = false;
            String strSql = "";
            //dgvDet.row
            strSql = Query;
            if (cboCriteria.SelectedIndex == 0)// Start with
                strSql += " where " + cboSearchby.SelectedValue.ToString() + " like '" + txtSearchText.Text + "%'";
            else if (cboCriteria.SelectedIndex == 1)// Any where
				strSql += " where " + cboSearchby.SelectedValue.ToString() + " like '%" + txtSearchText.Text + "%'";
            else if (cboCriteria.SelectedIndex == 2)// Equal to
				strSql += " where " + cboSearchby.SelectedValue.ToString() + " = '" + txtSearchText.Text + "'";
            else if (cboCriteria.SelectedIndex == 3)// grater than
				strSql += " where " + cboSearchby.SelectedValue.ToString() + " > '" + txtSearchText.Text + "'";
            else if (cboCriteria.SelectedIndex == 4)// less than
				strSql += " where " + cboSearchby.SelectedValue.ToString() + " < '" + txtSearchText.Text + "'";
            if (FilterCond.Trim() != "")
                strSql += " and " + FilterCond;
			strSql += " order by " + cboSearchby.SelectedValue.ToString();
            
            DataTable dt = mdbAcc.ExecuteQuery(strSql);
            dgvDet.DataSource = dt;
            dgvDet.Refresh();
            setDataGrid();
            if (dgvDet.RowCount > 0)
                dgvDet.Rows[0].Selected = true;
        }

        private void txtSearchText_TextChanged(object sender, EventArgs e)
        {
            SearchData();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Cancelled = true;
            ReturnValue = null;
            ReturnValue = null;
            this.Close();
        }

        private void txtSearchText_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 40)
                SendKeys.Send("{TAB}");
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            if (dgvDet.RowCount <= 0) return;
            Cancelled = false;
            ReturnValue = dgvDet.CurrentRow.Cells[ReturnFldIndex].Value.ToString();
            ReturnRow = dgvDet.CurrentRow;
            this.Close();
        }
        public int DefSearchFldIndex
        {
            get { return vDefSearchFldIndex; }
            set { vDefSearchFldIndex = value; }
        }

        private void dgvDet_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            btnSelect_Click(sender, new EventArgs());
        }

        private void Search_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
                btnSelect_Click(sender, new EventArgs());
        }

		private void btnSearch_Click(object sender, EventArgs e)
		{
			SearchData();
		}

		private void cboSearchby_SelectedIndexChanged(object sender, EventArgs e)
		{
			if(!isFirst)
				SearchData();
		}

		private void cboCriteria_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (!isFirst)
				SearchData();
		}
    }
}