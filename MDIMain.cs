using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Configuration;
using System.Resources;
using System.Threading;
using CsHms; 
using System.IO;

namespace CsHms
{
    public partial class MDIMain : Form
    {
        private int childFormNumber = 0;
        Global mGlobal = new Global();
        CommFuncs mclsCom = new CommFuncs();
        int intAns = 0;
        string mstrIPTmpFileName = "IPNameSlip.prn";
        string mstrOPTmpFileName = "OPNameSlip.prn";
        string mstrOutFileName = "TempSlip.prn";
        public MDIMain()
        {
            InitializeComponent();

        }

        private void ShowNewForm(object sender, EventArgs e)
        {
            // Create a new instance of the child form.
            Form childForm = new Form();
            // Make it a child of this MDI form before showing it.
            childForm.MdiParent = this;
            childForm.Text = "Window " + childFormNumber++;
            childForm.Show();
        }

        private void OpenFile(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            openFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
            if (openFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string FileName = openFileDialog.FileName;
                // TODO: Add code here to open the file.
            }
        }

        private void SaveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            saveFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
            if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string FileName = saveFileDialog.FileName;
                // TODO: Add code here to save the current contents of the form to a file.
            }
        }

        private void ExitToolsStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void CascadeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.Cascade);
        }

        private void TileVerticleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileVertical);
        }

        private void TileHorizontalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileHorizontal);
        }

        private void ArrangeIconsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.ArrangeIcons);
        }

        private void CloseAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form childForm in MdiChildren)
            {
                childForm.Close();
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }


        private void MDIMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }


        private void MDIMain_Load(object sender, EventArgs e)
        {
            tlblUser.Text = "User : " + mGlobal.CurrentUser + "       ";
            tslblClient.Text = " Client : " + mGlobal.FirmName + ", " + mGlobal.FirmAddress + "         ";


        }


        private void btnLtdis_Click(object sender, EventArgs e)
        {
            Form frm = new CsHms.Akshay.OPLateDiscEntry();
            frm.MdiParent = this;
            frm.Icon = this.Icon;
            frm.Show();
        }

        private void btnOPmerge_Click(object sender, EventArgs e)
        {
            Form frm = new CsHms.Akshay.OpMerge();
            frm.MdiParent = this;
            frm.Icon = this.Icon;
            frm.Show();
        }

        private void btnScrmap_Click(object sender, EventArgs e)
        {
            Form frm = new CsHms.Akshay.SCRFileLinksMap();
            frm.MdiParent = this;
            frm.Icon = this.Icon;
            frm.Show();
        }

        private void btnOpbillmodality_Click(object sender, EventArgs e)
        {
            Form frm = new CsHms.Akshay.OpBillModalityMap();
            frm.MdiParent = this;
            frm.Icon = this.Icon;
            frm.Show();
        }

        private void btnIPbillOpen_Click(object sender, EventArgs e)
        {
            Form frm = new CsHms.IP.IPBillCancel();
            frm.MdiParent = this;
            frm.Icon = this.Icon;
            frm.Show();
        }

        private void btnRcmCoreset_Click(object sender, EventArgs e)
        {
            Form frm = new CsHms.Akshay.RCMCoreSettings();
            frm.MdiParent = this;
            frm.Icon = this.Icon;
            frm.Show();
        }

        private void btnQmsmaster_Click(object sender, EventArgs e)
        {
            Form frm = new CsHms.Akshay.MasterCreater();
            frm.MdiParent = this;
            frm.Icon = this.Icon;
            frm.Show();
        }

        private void btnFirmselection_Click(object sender, EventArgs e)
        {
            Form frm = new CsHms.Akshay.FirmSelection();
            frm.MdiParent = this;
            frm.Icon = this.Icon;
            frm.Show();
        }

        private void btnCouponCreator_Click(object sender, EventArgs e)
        {
            Form frm = new CsHms.Akshay.CouponCreator();
            frm.MdiParent = this;
            frm.Icon = this.Icon;
            frm.Show();
        }

        private void btnVoucherDet_Click(object sender, EventArgs e)
        {
            Form frm = new CsHms.Akshay.VoucherDetails();
            frm.MdiParent = this;
            frm.Icon = this.Icon;
            frm.Show();
        }

        private void btnDdcList_Click(object sender, EventArgs e)
        {
            Form frm = new CsHms.Akshay.DDCList();
            frm.MdiParent = this;
            frm.Icon = this.Icon;
            frm.Show();
        }

        private void btnDataInitialize_Click(object sender, EventArgs e)
        {
            Form frm = new CsHms.Akshay.DataInitialize();
            frm.MdiParent = this;
            frm.Icon = this.Icon;
            frm.Show();
        }

        private void btnRemitance_Click(object sender, EventArgs e)
        {
            Form frm = new CsHms.Akshay.Remittance();
            frm.MdiParent = this;
            frm.Icon = this.Icon;
            frm.Show();
        }

        private void btnXmlVisualizer_Click(object sender, EventArgs e)
        {
            Form frm = new CsHms.Akshay.XMLVisualizer();
            frm.MdiParent = this;
            frm.Icon = this.Icon;
            frm.Show();
        }

        private void btnConfigsettings_Click(object sender, EventArgs e)
        {
            Form frm = new CsHms.Akshay.ConfigSettings();
            frm.MdiParent = this;
            frm.Icon = this.Icon;
            frm.Show();
        }

        private void btnChangePackage_Click(object sender, EventArgs e)
        {
            Form frm = new CsHms.Akshay.ChangePackage();
            frm.MdiParent = this;
            frm.Icon = this.Icon;
            frm.Show();
        }

        private void btnCouponapply_Click(object sender, EventArgs e)
        {
            Form frm = new CsHms.Akshay.LateCouponApply();
            frm.MdiParent = this;
            frm.Icon = this.Icon;
            frm.Show();
        }

        private void btnTableCreator_Click(object sender, EventArgs e)
        {
            Form frm = new CsHms.Akshay.TableCreator();
            frm.MdiParent = this;
            frm.Icon = this.Icon;
            frm.Show();
        }

        private void btnChangeUsername_Click(object sender, EventArgs e)
        {
            Form frm = new CsHms.Akshay.ChangeUserName();
            frm.MdiParent = this;
            frm.Icon = this.Icon;
            frm.Show();
        }

        private void btnOpDoctorappmap_Click(object sender, EventArgs e)
        {
            Form frm = new CsHms.Akshay.OP_DoctorappointmentMaping();
            frm.MdiParent = this;
            frm.Icon = this.Icon;
            frm.Show();
        }

        private void btnCampgnMasterCreator_Click(object sender, EventArgs e)
        {
            Form frm = new CsHms.Akshay.CampgnMasterCreator();
            frm.MdiParent = this;
            frm.Icon = this.Icon;
            frm.Show();
        }

        private void btnModalityTechnicianEntry_Click(object sender, EventArgs e)
        {
            Form frm = new CsHms.Akshay.ModalityTechnicianEntry();
            frm.MdiParent = this;
            frm.Icon = this.Icon;
            frm.Show();
        }

        private void btnBillUpdate_Click(object sender, EventArgs e)
        {
            Form frm = new CsHms.Akshay.BillUpdate();
            frm.MdiParent = this;
            frm.Icon = this.Icon;
            frm.Show();
        }

        private void btnQrApp_Click(object sender, EventArgs e)
        {
            Form frm = new CsHms.Akshay.QRApp();
            frm.MdiParent = this;
            frm.Icon = this.Icon;
            frm.Show();
        }

        private void btnChangeconfig_Click(object sender, EventArgs e)
        {
            Form frm = new CsHms.Akshay.ChangeConfig();
            frm.MdiParent = this;
            frm.Show();

        }

        

     

    }
}
 