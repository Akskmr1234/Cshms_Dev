using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web.Script.Serialization;
using System.Net.Configuration;

namespace LabInterface.CareData.LisWebAPI
{
    public class HisLisCommunication
    {
        HisBuildDatasetCls mHisBuildDatasetCls = new HisBuildDatasetCls();
        CommFuncs mclsCFunc = new CommFuncs();

        public string TokenGeneration(string _BillType, string _OrderIDorNo, string _LabDeptID, bool _CheckDataNeeded)
        {
            //DataTable dtdata = mHisBuildDatasetCls.checkLab(_BillType, _OrderIDorNo, _LabDeptID);
            DataTable dtdata = null;
            if (_CheckDataNeeded)
            {
                 dtdata = mHisBuildDatasetCls.checkLab(_BillType, _OrderIDorNo, _LabDeptID);
            }

            if ((dtdata != null && _CheckDataNeeded) || !_CheckDataNeeded)
            {
                //string strApiUrl = "http://52.230.9.139/IntegrationNew/TranscareApp/token";
                //string strApiUrl = "http://www.carelabtrakdi.com/AppIntegration/token";
                //string strRequestBody = "grant_type=password&UserName=HisIntegration&Password=Integration@321";

                //WebClient client = new WebClient();
                //client.Headers["Content-type"] = "application/json";
                //client.Headers.Add("loginType", "LIS");
                //client.Encoding = Encoding.UTF8;

                //string resultJson = client.UploadString(strApiUrl, strRequestBody);
                //resultJson = resultJson.Substring(17);
                //resultJson = resultJson.Substring(0, resultJson.Length - 43);
                //return resultJson;
                string strresult = TokenResult();
                return strresult;
            }
            return "";
        }
        public string TokenResult()
        {
            string strApiUrl = "http://www.carelabtrakdi.com/AppIntegration/token";
            //string strApiUrl = "http://52.230.9.139/IntegrationNew/TranscareApp/token";

            string strRequestBody = "grant_type=password&UserName=HisIntegration&Password=Integration@321";
            //string strRequestBody = "grant_type=password&UserName=CareUser&Password=Care@123";

            WebClient client = new WebClient();
            client.Headers["Content-type"] = "application/json";
            client.Headers.Add("loginType", "LIS");
            client.Encoding = Encoding.UTF8;

            string resultJson = client.UploadString(strApiUrl,strRequestBody);
            resultJson = resultJson.Substring(17);
            resultJson = resultJson.Substring(0, resultJson.Length - 43);
            return resultJson;
        }

        public string ProcessOrderAPI(string _BillType, string _OrderIDorNo, string _LabDeptID)
        {
            string strToken = TokenGeneration(_BillType, _OrderIDorNo, _LabDeptID, true);
            if (strToken.Length <= 0)
            {
                try
                {
                    mHisBuildDatasetCls.updateStatus(_BillType, _OrderIDorNo, "FAL", "No Lab Items or Token generation Failed"); // Failed Token Generation
                }
                catch { }
                return "";
            }
            return PushOrderDetails(strToken, _BillType.Trim(), _OrderIDorNo.Trim().ToString(),_LabDeptID);
        }

        public string PushOrderDetails(string strAuthToken, string strBillType, string strBillNo, string _LabDeptID)
        {
            //string strApiUrl = "http://52.230.9.139/IntegrationNew/TranscareApp/api/CareDataIntegration/HISLISIntegration";
            string strApiUrl = "http://www.carelabtrakdi.com/AppIntegration/API/CareDataIntegration/HISLISIntegration";
            string strJsonResult = "";
            #region Local Data Population

            DataSet dsdata = mHisBuildDatasetCls.BuildDataSetForHis_Request(strBillType, strBillNo,_LabDeptID);

            #endregion

            if (dsdata.Tables.Count > 0)
            {
                try
                {
                    string strRequestBody = DataSetToJsonObj(dsdata);

                    #region WebClient calling

                    WebClient client = new WebClient();
                    ConvertJsonStringToDataTable ConvertJsonCls = new ConvertJsonStringToDataTable();
                    client.Headers["Content-type"] = "application/json";
                    client.Headers.Add("loginType", "LIS");
                    client.Headers.Add("Authorization", "Bearer " + strAuthToken);
                    client.Encoding = Encoding.UTF8;

                    strJsonResult = client.UploadString(strApiUrl, strRequestBody);
                    DataTable DtJson = ConvertJsonCls.JsonStringToDataTable(strJsonResult);
                    string strStatus = mclsCFunc.ConvertToString(DtJson.Rows[0]["Status"].ToString());
                    string strMessage = mclsCFunc.ConvertToString(DtJson.Rows[0]["Message"].ToString());
                    if (strStatus == "1") // 0 - Failed, 1 - Success
                        mHisBuildDatasetCls.updateStatus(strBillType, strBillNo, "SUS", strMessage); // API Status - Success
                    else
                        mHisBuildDatasetCls.updateStatus(strBillType, strBillNo, "FAL", strMessage); // API Status - failed
                    #endregion
                }
                catch (Exception ex)
                {
                    mHisBuildDatasetCls.updateStatus(strBillType, strBillNo, "FAL", ex.Message);
                }
            }
            return strJsonResult;
        }

        public string CancelOrderDetails(string strAuthToken, string strBillType, string strBillNo, string _LabDeptID)
        {
            //string strApiUrl = "http://52.230.9.139/IntegrationNew/TranscareApp/api/CareDataIntegration/HISLISCancelTest";
            string strApiUrl = "http://www.carelabtrakdi.com/AppIntegration/API/CareDataIntegration/HISLISCancelTest";
            string strJsonResult = "";
            #region Local Data Population

            DataSet dsdata = mHisBuildDatasetCls.BuildDataSetForHis_Cancel(strBillType, strBillNo, _LabDeptID);

            #endregion

            if (dsdata.Tables.Count > 0)
            {
                try
                {
                    string strRequestBody = DataSetToJsonObj(dsdata);

                    #region WebClient calling

                    WebClient client = new WebClient();
                    ConvertJsonStringToDataTable ConvertJsonCls = new ConvertJsonStringToDataTable();
                    client.Headers["Content-type"] = "application/json";
                    client.Headers.Add("loginType", "LIS");
                    client.Headers.Add("Authorization", "Bearer " + strAuthToken);
                    client.Encoding = Encoding.UTF8;

                    strJsonResult = client.UploadString(strApiUrl, strRequestBody);
                    DataTable DtJson = ConvertJsonCls.JsonStringToDataTable(strJsonResult);
                    string strStatus = mclsCFunc.ConvertToString(DtJson.Rows[0]["Status"].ToString());
                    string strMessage = mclsCFunc.ConvertToString(DtJson.Rows[0]["Message"].ToString());
                    if (strStatus == "1") // 0 - Failed, 1 - Success
                        mHisBuildDatasetCls.updateStatus(strBillType, strBillNo, "SUS", strMessage); // API Status - Success
                    else
                        mHisBuildDatasetCls.updateStatus(strBillType, strBillNo, "FAL", strMessage); // API Status - failed
                    #endregion
                }
                catch (Exception ex)
                {
                    mHisBuildDatasetCls.updateStatus(strBillType, strBillNo, "FAL", ex.Message);
                }
            }
            return strJsonResult;
        }

        public string UpdatePatientInfo( string strBillType, string strOp_id, string strAuthToken)
        {
            //string strApiUrl = "http://52.230.9.139/IntegrationNew/TranscareApp/api/CareDataIntegration/HISLISEditPatientInfo";
            string strApiUrl = "http://www.carelabtrakdi.com/AppIntegration/API/CareDataIntegration/HISLISEditPatientInfo";
            string strJsonResult = "";
            #region Local Data Population

            DataSet dsdata = mHisBuildDatasetCls.BuildDataSetForHis_Update(strBillType,strOp_id);

            #endregion

            if (dsdata.Tables.Count > 0)
            {
                try
                {
                    string strRequestBody = DataSetToJsonObj(dsdata);

                    #region WebClient calling

                    WebClient client = new WebClient();
                    ConvertJsonStringToDataTable ConvertJsonCls = new ConvertJsonStringToDataTable();
                    client.Headers["Content-type"] = "application/json";
                    client.Headers.Add("loginType", "LIS");
                    client.Headers.Add("Authorization", "Bearer " + strAuthToken);
                    client.Encoding = Encoding.UTF8;

                    strJsonResult = client.UploadString(strApiUrl, strRequestBody);
                    #endregion
                }
                catch (Exception ex)
                {
                }
            }
            return strJsonResult;
        }


        public string GetVisitandOrderDetails(string strAuthToken, string strBillNo)
        {
            //string strApiUrl = "http://52.230.9.139/IntegrationNew/TranscareApp/api/CareDataIntegration/HISLISOrderStatus";
            string strApiUrl = "http://www.carelabtrakdi.com/AppIntegration/API/CareDataIntegration/HISLISOrderStatus";
            string strJsonResult = "";

            string strRequestBody = GetVisitRequestBody(strBillNo);

            #region WebClient calling

            WebClient client = new WebClient();
            client.Headers["Content-type"] = "application/json";
            client.Headers.Add("loginType", "LIS");
            client.Headers.Add("Authorization", "Bearer " + strAuthToken);
            client.Encoding = Encoding.UTF8;

            strJsonResult = client.UploadString(strApiUrl, strRequestBody);

            #endregion

            return strJsonResult;

        }

        public string GetFinalReport(string strAuthToken, string strBillType, string strBillNo)
        {
            //string strApiUrl = "http://52.230.9.139/IntegrationNew/TranscareApp/api/CareDataIntegration/HISLISReportPDF";
            string strApiUrl = "http://www.carelabtrakdi.com/AppIntegration/API/CareDataIntegration/HISLISReportPDF";
            string strJsonResult = "";
            
            string strRequestBody = GetFinalReportRequestBody(strBillType, strBillNo);

            #region WebClient calling

            WebClient client = new WebClient();
            client.Headers["Content-type"] = "application/json";
            client.Headers.Add("loginType", "LIS");
            client.Headers.Add("Authorization", "Bearer " + strAuthToken);
            client.Encoding = Encoding.UTF8;

            strJsonResult = client.UploadString(strApiUrl, strRequestBody);

            #endregion
            return strJsonResult;

        }

        private string GetVisitRequestBody(string strBillNo)
        {
            StringBuilder JsonString = new StringBuilder();
            JsonString.Append("{");
            JsonString.Append("\"" + "UHID" + "\":" + "\"\",");
            JsonString.Append("\"" + "VisitID" + "\":" + "\"" + strBillNo + "\",");
            JsonString.Append("\"" + "LabID" + "\":" + "\"\",");
            JsonString.Append("\"" + "TenantNo" + "\":" + "\"70\",");
            JsonString.Append("\"" + "TenantBranchNo" + "\":" + "\"129\"");
            JsonString.Append("}");
            return JsonString.ToString();
        }

        private string GetFinalReportRequestBody(string strBillType, string strBillNo)
        {
            StringBuilder JsonString = new StringBuilder();
            JsonString.Append("{");
            JsonString.Append("\"" + "SubjectEncounterNo" + "\":" + "\"\",");
            JsonString.Append("\"" + "LabID" + "\":" + "\"" + strBillType + strBillNo + "\",");
            JsonString.Append("\"" + "TenantNo" + "\":" + "\"70\",");
            JsonString.Append("\"" + "TenantBranchNo" + "\":" + "\"129\"");
            JsonString.Append("}");
            return JsonString.ToString();
        }

        public bool updateStatus(string _BillType, string _BillNo, string _Status, string _message)
        {
            return mHisBuildDatasetCls.updateStatus(_BillType, _BillNo, _Status, _message);
        }

        private string DataSetToJsonObj(DataSet ds)
        {
            //DataSet ds = new DataSet();
            //ds.Merge(dt);
            StringBuilder JsonString = new StringBuilder();
            if (ds != null && ds.Tables["Main"].Rows.Count > 0)
            {
                //JsonString.Append("[");
                for (int i = 0; i < ds.Tables["Main"].Rows.Count; i++)
                {
                    JsonString.Append("{");
                    for (int j = 0; j < ds.Tables["Main"].Columns.Count; j++)
                    {


                        if (ds.Tables["Main"].Columns[j].ToString() != "OrderDetails")
                        {
                            if (j < ds.Tables["Main"].Columns.Count - 1)
                            {
                                JsonString.Append("\"" + ds.Tables["Main"].Columns[j].ColumnName.ToString() + "\":" + "\"" + ds.Tables["Main"].Rows[i][j].ToString() + "\",");
                            }
                            else if (j == ds.Tables["Main"].Columns.Count - 1)
                            {
                                JsonString.Append("\"" + ds.Tables["Main"].Columns[j].ColumnName.ToString() + "\":" + "\"" + ds.Tables["Main"].Rows[i][j].ToString() + "\"");
                            }
                        }
                        else if (ds.Tables["Main"].Columns[j].ToString() == "OrderDetails")
                        {
                            JsonString.Append("\"" + "OrderDetails" + "\":" + DataTableToJsonObj(ds.Tables["SER"]));
                            //JsonString.Append(",");
                        }
                        //else if (ds.Tables["Main"].Columns[j].ToString() == "PaymentDetails")
                        //{
                        //    JsonString.Append("\"" + "PaymentDetails" + "\":" + DataTableToJsonObj(ds.Tables["PAY"]));
                        //}

                    }
                    if (i == ds.Tables["Main"].Rows.Count - 1)
                    {
                        JsonString.Append("}");
                    }
                    else
                    {
                        JsonString.Append("},");
                    }
                }
                // JsonString.Append("]");
                return JsonString.ToString();
            }
            else
            {
                return null;
            }
        }

        private string DataTableToJsonObj(DataTable dt)
         {
             DataSet ds = new DataSet();
             ds.Merge(dt);
             StringBuilder JsonString = new StringBuilder();
             if (ds != null && ds.Tables[0].Rows.Count > 0)
             {
                 JsonString.Append("[");
                 for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                 {
                     JsonString.Append("{");
                     for (int j = 0; j < ds.Tables[0].Columns.Count; j++)
                     {
                         if (j < ds.Tables[0].Columns.Count - 1)
                         {
                             JsonString.Append("\"" + ds.Tables[0].Columns[j].ColumnName.ToString() + "\":" + "\"" + ds.Tables[0].Rows[i][j].ToString() + "\",");
                         }
                         else if (j == ds.Tables[0].Columns.Count - 1)
                         {
                             JsonString.Append("\"" + ds.Tables[0].Columns[j].ColumnName.ToString() + "\":" + "\"" + ds.Tables[0].Rows[i][j].ToString() + "\"");
                         }
                     }
                     if (i == ds.Tables[0].Rows.Count - 1)
                     {
                         JsonString.Append("}");
                     }
                     else
                     {
                         JsonString.Append("},");
                     }
                 }
                 JsonString.Append("]");
                 return JsonString.ToString();
             }
             else
             {
                 return null;
             }
         }



    }
}

