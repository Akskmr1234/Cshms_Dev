using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Windows.Forms;

namespace LabInterface.CareData.LisWebAPI
{
    class HisPrintDispatchPdfCls
    {
        CommFuncs mclsCFunc = new CommFuncs();
        Global mGlobal = new Global();
        #region Proerties and it's Variables
        string mstrEncounterID;
        string mstrIsProvisionalReporte;
        string mstrTenantID;
        string mstTenantBranchID;

        public string EncounterID
        {
            set { mstrEncounterID = value; }
            get { return mstrEncounterID; }
        }
        public string IsProvisionalReport
        {
            set { mstrIsProvisionalReporte = value; }
            get { return mstrIsProvisionalReporte; }
        }
        public string TenantID
        {
            set { mstrTenantID = value; }
            get { return mstrTenantID; }
        }
        public string TenantBranchID
        {
            set { mstTenantBranchID = value; }
            get { return mstTenantBranchID; }
        }

        #endregion


        #region Methods
        public string BuildHeader_PrintDispatchPdf()
        {
            StringBuilder JsonString = new StringBuilder();
            JsonString.Append("[{");
            JsonString.Append("\"key\":" + "\"Content-Type\",");
            JsonString.Append("\"value\":" + "\"application/json\"");       
            JsonString.Append("}]");
            //return JsonString.ToString();

            JsonString.Append("[{");
            JsonString.Append("\"key\":" + "\"Authorization\",");
            JsonString.Append("\"value\":" + "\"Bearer  (get Token)\"");
            JsonString.Append("\"description\":enabled true");
            JsonString.Append("}]");
            return JsonString.ToString();
        }
        #endregion
    }
}
