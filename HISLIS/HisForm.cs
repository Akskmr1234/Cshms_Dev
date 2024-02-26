using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Net;
using System.Collections.Specialized;
using System.Web.Script.Serialization;
using System.IO;

namespace LabInterface.CareData.LisWebAPI
{
    public partial class HisForm : Form
    {
        HisJsonConvertCls mHisJsonConvertCls = new HisJsonConvertCls();
        HisPrintDispatchPdfCls mHisPrintDispatchPdfCls = new HisPrintDispatchPdfCls();
        HisLisCommunication hisLisCommunicatn = new HisLisCommunication();
        //Global mGlobal = new Global();
        string strDeptPtr = "15";

        public HisForm()
        {
            InitializeComponent();
        }

        private void btnshow_Click(object sender, EventArgs e)
        {
            try
            {
                string strOrderBillRefNo = txtbillno.Text.Trim();
                try
                {
                    Global mGlobal = new Global();
                    CommFuncs mCommFuncs = new CommFuncs();
                    if (comboBox1.Text.Trim() == "OPB")
                    {
                        strOrderBillRefNo = mCommFuncs.ConvertToString(mGlobal.LocalDBCon.ExecuteScalar("select opb_id from opbill where opb_bno='" + txtbillno.Text.Trim() + "'"));
                    }
                    string result = hisLisCommunicatn.ProcessOrderAPI(comboBox1.Text.Trim(), strOrderBillRefNo, "15");//mGlobal.GetSettingsListFieldValue("LABDID"));
                    if (result == "")
                        MessageBox.Show("Lab Items not found or Token generation failed.");
                    else
                        MessageBox.Show(result);
                }
                catch { }
                //string strToken = hisLisCommunicatn.TokenGeneration(comboBox1.Text.Trim(), strOrderBillRefNo, strDeptPtr, true);
                //if (strToken.Length > 0)
                //{
                //    //pushing to lis is commented because matri is in live from 04/10/2019                                                            
                //    string result = hisLisCommunicatn.PushOrderDetails(strToken, comboBox1.Text.Trim(), strOrderBillRefNo, strDeptPtr);
                //    MessageBox.Show(result);
                //}
                //else
                //{
                //    string _message = "Token not generated or Selected Items not in Lab Group";
                //    hisLisCommunicatn.updateStatus(comboBox1.Text.Trim(), txtbillno.Text.Trim().ToString(), "FAL", _message);
                //}
            }
            catch (Exception ex)
            {
                hisLisCommunicatn.updateStatus(comboBox1.Text.Trim(), txtbillno.Text.Trim().ToString(), "FAL", ex.Message);
                MessageBox.Show(ex.Message);
            }
        }

        private void btnGetResults_Click(object sender, EventArgs e)
        {
            try
            {
                string strToken = hisLisCommunicatn.TokenGeneration(comboBox1.Text.Trim(), txtbillno.Text.Trim().ToString(), strDeptPtr, false);
                string result = hisLisCommunicatn.GetVisitandOrderDetails(strToken, txtbillno.Text.Trim().ToString());
                MessageBox.Show(result);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnFinalReport_Click(object sender, EventArgs e)
        {
            try
            {
                string strToken = hisLisCommunicatn.TokenGeneration(comboBox1.Text.Trim(), txtbillno.Text.Trim().ToString(), strDeptPtr, false);
                string result = hisLisCommunicatn.GetFinalReport(strToken, comboBox1.Text.Trim(), txtbillno.Text.Trim().ToString());
                MessageBox.Show(result);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void HisForm_Load(object sender, EventArgs e)
        {
            comboBox1.Items.Add("OPB");
            comboBox1.Items.Add("OPO");
            comboBox1.Items.Add("IPO");
            comboBox1.SelectedIndex = 0;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                string strOrderBillRefNo = txtbillno.Text.Trim();
                Global mGlobal = new Global();
                CommFuncs mCommFuncs = new CommFuncs();
                if (comboBox1.Text.Trim() == "OPB")
                {
                    strOrderBillRefNo = mCommFuncs.ConvertToString(mGlobal.LocalDBCon.ExecuteScalar("select opb_id from opbill where opb_bno='" + txtbillno.Text.Trim() + "'"));
                }
                string strToken = hisLisCommunicatn.TokenGeneration(comboBox1.Text.Trim(), txtbillno.Text.Trim().ToString(), strDeptPtr, false);
                string result = hisLisCommunicatn.CancelOrderDetails(strToken, comboBox1.Text.Trim(), strOrderBillRefNo, strDeptPtr);
                MessageBox.Show(result);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                string strOrderBillRefNo = txtbillno.Text.Trim();
                try
                {
                    Global mGlobal = new Global();
                    CommFuncs mCommFuncs = new CommFuncs();
                    if (comboBox1.Text.Trim() == "OPB")
                    {
                        strOrderBillRefNo = mCommFuncs.ConvertToString(mGlobal.LocalDBCon.ExecuteScalar("select opb_id from opbill where opb_bno='" + txtbillno.Text.Trim() + "'"));
                    }
                    string strToken = hisLisCommunicatn.TokenResult();
                    string result = hisLisCommunicatn.UpdatePatientInfo(comboBox1.Text.Trim(),strOrderBillRefNo, strToken);//mGlobal.GetSettingsListFieldValue("LABDID"));
                    if (result == "")
                        MessageBox.Show("No Updation");
                    else
                        MessageBox.Show(result);
                }
                catch { }               
            }
            catch (Exception ex)
            {
                hisLisCommunicatn.updateStatus(comboBox1.Text.Trim(), txtbillno.Text.Trim().ToString(), "FAL", ex.Message);
                MessageBox.Show(ex.Message);
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                WebClient client = new WebClient();
                ConvertJsonStringToDataTable ConvertJsonCls = new ConvertJsonStringToDataTable();
                client.Headers["Content-type"] = "application/json";
                client.Headers["User-Agent"] = "Mozilla/5.0 (Windows NT 6.1; rv:71.0) Gecko/20100101 Firefox/71.0";//PostmanRuntime/7.20.1
                //client.Headers.Add("loginType", "LIS");
                //client.Headers.Add("Authorization", "Bearer " + strAuthToken);
                client.Encoding = Encoding.UTF8;
                string url="http://beyporecharitabletrust.com/lab_report/Api/add_file.aspx?key=Git_Labreport_2019&username=T770&password=TBMD321622&file=LabReport771.pdf";
                string strJsonResult = client.UploadString(url, "");
                
            }
            catch (Exception ex) { }
        }
    }
}