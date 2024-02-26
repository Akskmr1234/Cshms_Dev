using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace CsHms.Ctrl
{
    public partial class MasterSelection : UserControl
    {
        DataTable mDtMaster = new DataTable();
        DataTable mdtListData = new DataTable();
        public MasterSelection()
        {
            InitializeComponent();
            
        }

        bool mSelectAll = false;
        public bool SelectAll
        {
            set{
                mSelectAll=value;
                if (mSelectAll == true)
                    lbtnSelectAll_LinkClicked(null, null);
            }

        }
        bool mSelectAllData;
        public bool SelectAllData
        {
            set{mSelectAllData = value;}
            get { return mSelectAllData; }

        }

        public DataTable SelectedData
        {
            get
            {
                DataTable dtData = (DataTable)lstData.DataSource;
                if (dtData == null)
                    return null;
                if(dtData.Rows.Count<=0)
                    return null ;
                return dtData;
            }

        }


        public  void loadData(string strSql)
        {
            Global mGlobal = new Global();
            DataTable dtData = mGlobal.LocalDBCon.ExecuteQuery(strSql);
            mDtMaster = dtData.Copy();
            
            scsMaster.DataTabl = dtData;
            scsMaster.LoadData();
           
          
          
        }

        void setListDataTable(DataTable dtData)
        {
            lstData.DataSource = dtData;
            lstData.DisplayMember = dtData.Columns[1].ToString();
            lstData.ValueMember = dtData.Columns[0].ToString();
            lstData.Refresh();    

        }
        private void lbtnSelectAll_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
         
            lstData.DataSource = mDtMaster.Copy();
            lstData.DisplayMember = mDtMaster.Columns[1].ToString();
            lstData.ValueMember = mDtMaster.Columns[0].ToString();
            lstData.Refresh();
            mdtListData = mDtMaster.Copy();
            this.mSelectAllData = true;
             
        }



        private void lstData_DoubleClick(object sender, EventArgs e)
        {
            if (lstData.Items.Count > 0)
            {
                mdtListData.Rows.RemoveAt(lstData.SelectedIndex);
                setListDataTable(mdtListData);
            }
        }

      

        private void lbtnDeleteAll_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            lstData.DataSource = null;
            lstData.Items.Clear();
            mdtListData = null;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (scsMaster.SelectedValue == null)
                {
                    scsMaster.Focus(); return;
                }
                DataTable dtData = (DataTable)lstData.DataSource;
                if (dtData != null)
                {
                    DataRow[] dr = dtData.Select("Code ='" + scsMaster.SelectedValue + "'");
                    if (dr.Length == 0)
                    {
                        DataRow[] dr2 = mDtMaster.Select("Code ='" + scsMaster.SelectedValue + "'");
                        dtData.ImportRow(dr2[0]);
                        setListDataTable(dtData);
                    }
                    else
                    {
                        MessageBox.Show("Already Exist");

                    }
                }
                else
                {
                    DataRow[] dr1 = mDtMaster.Select("Code ='" + scsMaster.SelectedValue + "'");
                    dtData = mDtMaster.Clone();
                    dtData.ImportRow(dr1[0]);
                    setListDataTable(dtData);
                }
                mdtListData  = (DataTable)lstData.DataSource;
                scsMaster.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        

        private void MasterSelection_Leave(object sender, EventArgs e)
        {
            try
            {
                DataTable dtData = (DataTable)lstData.DataSource;
                if (dtData.Rows.Count == mDtMaster.Rows.Count)
                    this.mSelectAllData = true;
                else
                    this.mSelectAllData = false;

            }
            catch
            {
            }
        }

     
    }
}
