using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Windows.Forms;
using System.Text.RegularExpressions;
namespace LabInterface.CareData.LisWebAPI
{
    class HisBuildDatasetCls
    {
        CommFuncs mclsCFunc = new CommFuncs();
        Global mGlobal = new Global();
        DataSet dsHisData = new DataSet();
        #region Proerties and it's Variables
        string mstrUHID;
        string mstrTitle;
        string mstrFirstName;
        string mstrMiddleName;
        string mstrLastName;
        string mstrGender;
        string mstrAge;
        string mstrAgeType;
        string mstrDOB;      
        string mstrPhoneNo;
      
        string mstrEmail;
        string mstrAddress;
        string mstrArea;
        string mstrCity;
        string mstrState;
        string mstrCountry;
        string mstrPincode;
        string mstrVisitDTTM;
        string mstVisitID;
        string mstrLabID;
        string mstrVisitType;
        string mstrWardDetails;
        string mstRoomNo;
        string mstrBedID;
        string mstrRefferralType;
        string mstrRefferralName;
        string mstrRefferralCode;
        string mstrDoctorName;
        string mstrDoctorCode;
        string mstrTenantNo;
        string mstrTenantBranchNo;



        //Request Parameters
        public string UHID
        {
            set { mstrUHID = value; }
            get { return mstrUHID; }
        }
        
        public string Title
        {
            set { mstrTitle = value; }
            get { return mstrTitle; }
        }
        public string FirstName
        {
            set { mstrFirstName = value; }
            get { return mstrFirstName; }
        }
        public string MiddleName
        {
            set { mstrMiddleName  = value; }
            get { return mstrMiddleName; }
        }
        public string LastName
        {
            set { mstrLastName = value; }
            get { return mstrLastName; }
        }
        public string Gender
        {
            set { mstrGender  = value; }
            get { return mstrGender; }
        }
        public string Age
        {
            set { mstrAge = value; }
            get { return mstrAge; }
        }
        public string AgeType
        {
            set { mstrAgeType = value; }
            get { return mstrAgeType; }
        }
        public string DOB
        {
            set { mstrDOB = value; }
            get { return mstrDOB; }
        }
        
        public string PhoneNo
        {
            set { mstrPhoneNo = value; }
            get { return mstrPhoneNo; }
        }
        
        public string Email
        {
            set { mstrEmail = value; }
            get { return mstrEmail; }
        }
        public string Address
        {
            set { mstrAddress = value; }
            get { return mstrAddress; }
        }
        public string Area
        {
            set { mstrArea = value; }
            get { return mstrArea; }
        }
        public string City
        {
            set { mstrCity = value; }
            get { return mstrCity; }
        }
        public string State
        {
            set { mstrState = value; }
            get { return mstrState; }
        }
        public string Country
        {
            set { mstrCountry = value; }
            get { return mstrCountry; }
        }

        public string Pincode
        {
            set { mstrPincode = value; }
            get { return mstrPincode; }
        }
        public string VisitDTTM
        {
            set { mstrVisitDTTM = value; }
            get { return mstrVisitDTTM; }
        }

        public string VisitID
        {
            set { mstVisitID = value; }
            get { return mstVisitID; }
        }
        public string LabID
        {
            set { mstrLabID = value; }
            get { return mstrLabID; }
        }
        public string VisitType
        {
            set { mstrVisitType = value; }
            get { return mstrVisitType; }
        }
        public string WardDetails
        {
            set { mstrWardDetails = value; }
            get { return mstrWardDetails; }
        }

        public string RoomNo
        {
            set { mstRoomNo = value; }
            get { return mstRoomNo; }
        }
        public string BedID
        {
            set { mstrBedID = value; }
            get { return mstrBedID; }
        }

        public string RefferralType
        {
            set { mstrRefferralType = value; }
            get { return mstrRefferralType; }
        }
        public string RefferralName
        {
            set { mstrRefferralName= value; }
            get { return mstrRefferralName; }
        }
        public string RefferralCode
        {
            set { mstrRefferralCode = value; }
            get { return mstrRefferralCode; }
        }
        public string DoctorName
        {
            set { mstrDoctorName = value; }
            get { return mstrDoctorName; }
        }
        public string DoctorCode
        {
            set { mstrDoctorCode = value; }
            get { return mstrDoctorCode; }
        }

        public string TenantNo
        {
            set { mstrTenantNo = value; }
            get { return mstrTenantNo; }
        }
        public string TenantBranchNo
        {
            set { mstrTenantBranchNo = value; }
            get { return mstrTenantBranchNo; }
        }

        #endregion


        #region Methods

        public DataSet  BuildDataSetForHis_Request(string TrType, string _id,string _LabDeptID)
        {
            string strSql = "";
            dsHisData = new DataSet();
            if (TrType == "OPB")
            {                
               strSql = " SELECT (CASE WHEN op_no IS NULL THEN opb_bno ELSE op_no END) as UHID," +
                         " isnull(op_title,'') as Title,(case when op_fname is null then isnull(opb_name,'') else op_fname end)  as FirstName," +
                         " '' as MiddleName," +
                         " isnull(op_lname,'') as LastName,(case isnull(opb_gender,'') when 'Male' then 'M' when 'FeMale' then 'F' else '' end) as Gender,";
                strSql = strSql + "op_dob as DOB,left(isnull(opb_age,'00'),2) as Age," +
                                " ltrim(substring(isnull(opb_age,'00'),3,len(isnull(opb_age,'00')))) as AgeType ," +
                                " (CASE WHEN opb_mobile IS NULL THEN isnull(op_mobile,'') ELSE opb_mobile END) as PhoneNo,";
                strSql = strSql + "(CASE WHEN opb_email IS NULL THEN isnull(op_email,'') ELSE opb_email END) as Email,isnull(opb_add1,'' ) as Address,isnull(opb_place,'') as Area,isnull(opb_place,'') as City," +
                                  " isnull(op_state,'') as State,";
                strSql = strSql + "isnull(op_country,'') as Country,isnull(op_zip,'') as Pincode,opb_tm as VisitDTTM," +
                                  " opb_id as VisitID, ('OPB'+opb_bno) as LabID," +
                                  " 'OP' as VisitType,'' as WardDetails,'' as RoomNo,";

                strSql = strSql + "'' as BedID,'DR' as ReferralType,'' as RefferralCode,'' as RefferralName,opb_doctorptr as DoctorCode,do_name as DoctorName," +
                                  " '70' as TenantNo,'129' as TenantBranchNo,'' as OrderDetails";
                strSql = strSql + " FROM opbill  " +
                                " LEFT JOIN opreg on opb_opno =op_no " +
                                " LEFT JOIN doctor on opb_doctorptr=do_code   " +
                                " WHERE opb_id ='" + _id + "' ";
            }
            else if (TrType == "OPO")
            {
                strSql = "SELECT ipo_opno as UHID,isnull(op_title,'') as Title,isnull(op_fname,'')  as FirstName,'' as MiddleName," +
                         " isnull(op_lname,'') as LastName,case isnull(op_gender,'') when 'Male' then 'M' when 'FeMale' then 'F' else '' end as Gender,";
                strSql = strSql + "op_dob as DOB,CAST(isnull(op_age,'00') AS INT) as Age," +
                                " (case isnull(op_agetype,'') when 'Years' then 'Y' when 'Months' then 'M' when 'Days' then 'D' else '' end) as AgeType ," +
                                " isnull(op_mobile,'') as PhoneNo,";
                strSql = strSql + "isnull(op_email,'') as Email,isnull(op_add1,'' ) as Address,isnull(op_place,'') as Area,isnull(op_place,'') as City," +
                                " isnull(op_state,'') as State,";
                strSql = strSql + "isnull(op_country,'') as Country,isnull(op_zip,'') as Pincode,ipo_tm as VisitDTTM," +
                                " ipo_id as VisitID,('OPO'+ipo_orderno) as LabID," +
                                " 'OP' as VisitType,'' as WardDetails,'' as RoomNo,";

                strSql = strSql + "'' as BedID,'DR' as ReferralType,'' as RefferralCode,'' as RefferralName,ipo_doctorptr as DoctorCode," +
                                " do_name as DoctorName,'70' as TenantNo,'129' as TenantBranchNo,'' as OrderDetails";
                strSql = strSql + " FROM iporder  " +
                                " LEFT JOIN opreg on ipo_opno =op_no " +
                                " LEFT JOIN doctor on ipo_doctorptr=do_code " +
                                " WHERE ipo_orderno ='" + _id + "' and ipo_mode='OP'";
            }
            else if (TrType == "IPO")
            {
                strSql = "SELECT ipo_opno as UHID,isnull(op_title,'') as Title,isnull(op_fname,'')  as FirstName,'' as MiddleName," +
                        " isnull(op_lname,'') as LastName,case isnull(op_gender,'') when 'Male' then 'M' when 'FeMale' then 'F' else '' end as Gender,";
                strSql = strSql + " op_dob as DOB,(SELECT left(isnull(Age,'00'),2) as Age FROM udf_opreg(iporder.ipo_opno)) as Age," +
                                  " (case isnull(op_agetype,'') when 'Years' then 'Y' when 'Months' then 'M' when 'Days' then 'D' else '' end) as AgeType ," +
                                  " isnull(op_mobile,'') as PhoneNo,";
                strSql = strSql + " isnull(op_email,'') as Email,isnull(op_add1,'' ) as Address,isnull(op_place,'') as Area,isnull(op_place,'') as City," +
                                  " isnull(op_state,'') as State,";
                strSql = strSql + " isnull(op_country,'') as Country,isnull(op_zip,'') as Pincode,ipo_tm as VisitDTTM," +
                                  " ipo_id as VisitID,('IPO'+ipo_orderno) as LabID,'IP' as VisitType,";//ipo_ipno

                strSql = strSql + "(SELECT top 1 rm_floor+' '+ rm_block  " +
                                   " FROM iproomdet " +
                                   " LEFT JOIN room on ipr_roomptr =rm_code  " +
                                   " WHERE ipr_ipno= ipo_ipno  ORDER BY ipr_checkindt desc) as WardDetails," +
                                   " (SELECT top 1 rm_code  " +
                                   " FROM iproomdet " +
                                   " LEFT JOIN room ON ipr_roomptr =rm_code  " +
                                   " WHERE ipr_ipno= ipo_ipno " +
                                   " ORDER BY ipr_checkindt desc) as RoomNo,";
                strSql = strSql + " '' as BedID,'DR' as ReferralType,'' as RefferralCode,'' as RefferralName,ipo_doctorptr as DoctorCode," +
                                " do_name as DoctorName,'70' as TenantNo,'129' as TenantBranchNo,'' as OrderDetails";
                strSql = strSql + " FROM iporder  " +
                                  " LEFT JOIN opreg on ipo_opno =op_no " +
                                  " LEFT JOIN doctor on ipo_doctorptr=do_code " +
                                  " WHERE ipo_orderno ='" + _id + "' and ipo_mode='IP'";
            }
            DataTable dtData = mGlobal.LocalDBCon.ExecuteQuery(strSql);
            if (dtData.Rows.Count > 0)
            {
                dtData.Rows[0]["UHID"] = dtData.Rows[0]["UHID"].ToString() == "" ? "1" : dtData.Rows[0]["UHID"].ToString();
                dtData.Rows[0]["Title"] = dtData.Rows[0]["Title"].ToString() == "" ? "." : dtData.Rows[0]["Title"].ToString();
                dtData.Rows[0]["FirstName"] = dtData.Rows[0]["FirstName"].ToString() == "" ? "." : dtData.Rows[0]["FirstName"].ToString();
                dtData.Rows[0]["Age"] = (dtData.Rows[0]["Age"].ToString() == "00" || dtData.Rows[0]["Age"].ToString() == "" || dtData.Rows[0]["Age"].ToString() == "0") ? "1" : dtData.Rows[0]["Age"].ToString();
                dtData.Rows[0]["AgeType"] = dtData.Rows[0]["AgeType"].ToString() == "" ? "D" : dtData.Rows[0]["AgeType"].ToString();
                if (mclsCFunc.ConvertToString(dtData.Rows[0]["AgeType"]).Trim().Length > 1)
                    dtData.Rows[0]["AgeType"] = dtData.Rows[0]["AgeType"].ToString().Substring(0, 1);
                dtData.Rows[0]["Address"] = dtData.Rows[0]["Address"].ToString().Replace('"', ' ');
                //dtData.Rows[0]["Address"] = Regex.Replace(dtData.Rows[0]["Address"].ToString(), "\"[^\"]*\"", string.Empty);
                dtData.TableName="Main";
                dsHisData.Tables.Add(dtData);               
            }            

            dtData = checkLab(TrType, _id, _LabDeptID);
            if (dtData != null && dtData.Rows.Count > 0)
            {
                dtData.TableName = "SER";
                dsHisData.Tables.Add(dtData);
            }

            return dsHisData;
        }
        public DataTable checkLab(string TrType, string _id,String _LabDeptID)
        {
            string strSql = "";
            string strDeptID = "15";//mGlobal.GetSettingsListFieldValue("LABDID");
            if (TrType == "OPB")
            {
                strSql = "select opbd_id as OrderNo, opbd_itemdesc as OrderName,opbd_itemptr as OrderCode,'PRO' as OrderType," +
                            " 0 as PriceListNo,0 as ServiceNo,opbd_net as ServiceAmount from opbill join opbilld on opb_id =opbd_hdrid " +
                            " where  opb_id='" + _id + "' and opbd_itemptr in " +
                            " (select itm_code from item where itm_deptptr=" + _LabDeptID + ")" +
                            " AND opbd_itemptr NOT IN (select Ipod_itemptr " +
                            " from iporderdet,iporder where ipod_orderno=ipo_orderno " +
                            " and ipo_mode='OP' and ipo_opno=opb_opno AND isnull(ipod_billedhdrid,0)='" + _id + "'" +
                            " and (ipo_canflg is null or ipo_canflg<>'Y') and (ipod_isbilled='Y')) ";
            }
            else if (TrType == "OPO")
            {
                strSql = "select ipod_id as OrderNo, ipod_itemnm as OrderName,ipod_itemptr as OrderCode,'PRO' as OrderType," +
                    " 0 as PriceListNo,0 as ServiceNo,ipod_rate as ServiceAmount " +
                    " from iporder join iporderdet on ipo_orderno =ipod_orderno " +
                    " where  ipo_mode ='OP' and ipo_orderno='" + _id + "' and ipod_itemptr in " +
                            " (select itm_code from item where itm_deptptr=" + _LabDeptID + ")";
            }
            else if (TrType == "IPO")
            {
                strSql = "select ipod_id as OrderNo, ipod_itemnm as OrderName,ipod_itemptr as OrderCode,'PRO' as OrderType," +
                        " 0 as PriceListNo,0 as ServiceNo,ipod_rate as ServiceAmount " +
                        " from iporder join iporderdet on ipo_orderno =ipod_orderno " +
                        " where  ipo_mode ='IP' and ipo_orderno='" + _id + "' and ipod_itemptr in " +
                            " (select itm_code from item where itm_deptptr=" + _LabDeptID + ")";
            }
            DataTable dtServiceData = mGlobal.LocalDBCon.ExecuteQuery(strSql);
            if (dtServiceData.Rows.Count > 0)
            {
                return dtServiceData;
            }
            return null;

        }

        public DataSet BuildDataSetForHis_Cancel(string TrType, string _id, string _LabDeptID)
        {
            string strSql = "";
            dsHisData = new DataSet();
            if (TrType == "OPB")
            {
                strSql = " SELECT (CASE WHEN op_no IS NULL THEN ('OPB'+opb_bno) ELSE op_no END) as UHID," +
                         " opb_id as VisitID, ('OPB'+opb_bno) as LabID," +
                         " '70' as TenantNo,'129' as TenantBranchNo,'' as OrderDetails";
                strSql = strSql + " FROM opbill  " +
                                " LEFT JOIN opreg on opb_opno =op_no " +
                                " JOIN doctor on opb_doctorptr=do_code   " +
                                " WHERE opb_id ='" + _id + "' ";
            }
            else if (TrType == "OPO")
            {
                strSql = "SELECT ipo_opno as UHID, " +
                                " ipo_id as VisitID,('OPO'+ipo_orderno) as LabID," +
                                " '70' as TenantNo,'129' as TenantBranchNo,'' as OrderDetails";
                strSql = strSql + " FROM iporder  " +
                                " LEFT JOIN opreg on ipo_opno =op_no " +
                                " JOIN doctor on ipo_doctorptr=do_code " +
                                " WHERE ipo_orderno ='" + _id + "' and ipo_mode='OP'";
            }
            else if (TrType == "IPO")
            {
                strSql = "SELECT ipo_opno as UHID," +
                                  " ipo_id as VisitID,('IPO'+ipo_orderno) as LabID,'IP' as VisitType," +
                                "'70' as TenantNo,'129' as TenantBranchNo,'' as OrderDetails";
                strSql = strSql + " FROM iporder  " +
                                  " LEFT JOIN opreg on ipo_opno =op_no " +
                                  " JOIN doctor on ipo_doctorptr=do_code " +
                                  " WHERE ipo_orderno ='" + _id + "' and ipo_mode='IP'";
            }
            DataTable dtData = mGlobal.LocalDBCon.ExecuteQuery(strSql);
            if (dtData.Rows.Count > 0)
            {
                dtData.Rows[0]["UHID"] = dtData.Rows[0]["UHID"].ToString() == "" ? "1" : dtData.Rows[0]["UHID"].ToString();
                dtData.TableName = "Main";
                dsHisData.Tables.Add(dtData);
            }

            dtData = checkLab_Cancel(TrType, _id, _LabDeptID);
            if (dtData != null && dtData.Rows.Count > 0)
            {
                dtData.TableName = "SER";
                dsHisData.Tables.Add(dtData);
            }

            return dsHisData;
        }

        public DataTable checkLab_Cancel(string TrType, string _id, String _LabDeptID)
        {
            string strSql = "";
            string strDeptID = "15";//mGlobal.GetSettingsListFieldValue("LABDID");
            if (TrType == "OPB")
            {
                strSql = "select opbd_itemdesc as OrderName,opbd_itemptr as OrderCode,'PRO' as OrderType " +
                            " from opbill join opbilld on opb_id =opbd_hdrid " +
                            " where  opb_id='" + _id + "' and opbd_itemptr in " +
                            " (select itm_code from item where itm_deptptr=" + _LabDeptID + ")" +
                            " AND opbd_itemptr NOT IN (select Ipod_itemptr " +
                            " from iporderdet,iporder where ipod_orderno=ipo_orderno " +
                            " and ipo_mode='OP' and ipo_opno=opb_opno AND isnull(ipod_billedhdrid,0)='" + _id + "'" +
                            " and (ipo_canflg is null or ipo_canflg<>'Y') and (ipod_isbilled='Y')) ";
            }
            else if (TrType == "OPO")
            {
                strSql = "select  ipod_itemnm as OrderName,ipod_itemptr as OrderCode,'PRO' as OrderType " +
                    " from iporder join iporderdet on ipo_orderno =ipod_orderno " +
                    " where  ipo_mode ='OP' and ipo_orderno='" + _id + "' and ipod_itemptr in " +
                            " (select itm_code from item where itm_deptptr=" + _LabDeptID + ")";
            }
            else if (TrType == "IPO")
            {
                strSql = "select ipod_itemnm as OrderName,ipod_itemptr as OrderCode,'PRO' as OrderType " +
                        " from iporder join iporderdet on ipo_orderno =ipod_orderno " +
                        " where  ipo_mode ='IP' and ipo_orderno='" + _id + "' and ipod_itemptr in " +
                            " (select itm_code from item where itm_deptptr=" + _LabDeptID + ")";
            }
            DataTable dtServiceData = mGlobal.LocalDBCon.ExecuteQuery(strSql);
            if (dtServiceData.Rows.Count > 0)
            {
                return dtServiceData;
            }
            return null;

        }

        public DataSet BuildDataSetForHis_Update(string TrType, string _id)
        {
            string strSql = "";
            dsHisData = new DataSet();
            if (TrType == "OPB")
            {
                strSql = @" SELECT (CASE WHEN op_no IS NULL THEN ('OPB'+opb_bno) ELSE op_no END) as UHID, isnull(op_title,'') as Title,
                        (case when op_fname is null then isnull(opb_name,'') else op_fname end)  as FirstName, '' as MiddleName, 
                        isnull(op_lname,'') as LastName,
                        (case isnull(opb_gender,'') when 'Male' then 'M' when 'FeMale' then 'F' else '' end) as Gender,
                         op_dob as DOB,cast(isnull(opb_age,'00') as int) as Age, 
                          ltrim(substring(isnull(opb_age,'00'),3,len(isnull(opb_age,'00')))) as AgeType ,
                           (CASE WHEN opb_mobile IS NULL THEN isnull(op_mobile,'') ELSE opb_mobile END) as PhoneNo,
                            (CASE WHEN opb_email IS NULL THEN isnull(op_email,'') ELSE opb_email END) as Email,
                           isnull(opb_add1,'' ) as Address,
                         isnull(opb_place,'') as Area,isnull(opb_place,'') as City, isnull(op_state,'') as State,
                         isnull(op_country,'') as Country,isnull(op_zip,'') as Pincode, 
                         opb_id as VisitID,('OPB'+opb_bno) as LabID, '70' as TenantNo,'129' as TenantBranchNo 
                         FROM opbill   
                         LEFT JOIN opreg on opb_opno =op_no  
                         JOIN doctor on opb_doctorptr=do_code   
                         WHERE opb_id ='" + _id + "' ";
            }
            else if (TrType == "OPO")
            {
                strSql = "SELECT ipo_opno as UHID,isnull(op_title,'') as Title,isnull(op_fname,'')  as FirstName,'' as MiddleName," +
                         " isnull(op_lname,'') as LastName,(case isnull(op_gender,'') when 'Male' then 'M' when 'FeMale' then 'F' else '' end) as Gender,";
                strSql = strSql + "op_dob as DOB,CAST(isnull(op_age,'00') AS INT) as Age," +
                                " (case isnull(op_agetype,'') when 'Years' then 'Y' when 'Months' then 'M' when 'Days' then 'D' else '' end) as AgeType ," +
                                " isnull(op_mobile,'') as PhoneNo,";
                strSql = strSql + "isnull(op_email,'') as Email,isnull(op_add1,'' ) as Address,isnull(op_place,'') as Area,isnull(op_place,'') as City," +
                                " isnull(op_state,'') as State,";
                strSql = strSql + "isnull(op_country,'') as Country,isnull(op_zip,'') as Pincode," +
                                " ipo_id as VisitID,('OPO'+ipo_orderno) as LabID," +
                                " '70' as TenantNo,'129' as TenantBranchNo ";
                strSql = strSql + " FROM iporder  " +
                                " LEFT JOIN opreg on ipo_opno =op_no " +
                                " WHERE ipo_orderno ='" + _id + "' and ipo_mode='OP'";
            }
            else if (TrType == "IPO")
            {
                strSql = "SELECT ipo_opno as UHID,isnull(op_title,'') as Title,isnull(op_fname,'')  as FirstName,'' as MiddleName," +
                        " isnull(op_lname,'') as LastName,case isnull(op_gender,'') when 'Male' then 'M' when 'FeMale' then 'F' else '' end as Gender,";
                strSql = strSql + " op_dob as DOB,(SELECT left(isnull(Age,'00'),2) as Age FROM udf_opreg(iporder.ipo_opno)) as Age," +
                                  " (case isnull(op_agetype,'') when 'Years' then 'Y' when 'Months' then 'M' when 'Days' then 'D' else '' end) as AgeType ," +
                                  " isnull(op_mobile,'') as PhoneNo,";
                strSql = strSql + " isnull(op_email,'') as Email,isnull(op_add1,'' ) as Address,isnull(op_place,'') as Area,isnull(op_place,'') as City," +
                                  " isnull(op_state,'') as State,";
                strSql = strSql + " isnull(op_country,'') as Country,isnull(op_zip,'') as Pincode," +
                                  " ipo_id as VisitID,('IPO'+ipo_orderno) as LabID," +
                                  " '70' as TenantNo,'129' as TenantBranchNo";
                strSql = strSql + " FROM iporder  " +
                                  " LEFT JOIN opreg on ipo_opno =op_no " +
                                  " WHERE ipo_orderno ='" + _id + "' and ipo_mode='IP'";
            }
            DataTable dtData = mGlobal.LocalDBCon.ExecuteQuery(strSql);
            if (dtData.Rows.Count > 0)
            {
                dtData.Rows[0]["UHID"] = dtData.Rows[0]["UHID"].ToString() == "" ? "1" : dtData.Rows[0]["UHID"].ToString();
                dtData.Rows[0]["Title"] = dtData.Rows[0]["Title"].ToString() == "" ? "." : dtData.Rows[0]["Title"].ToString();
                dtData.Rows[0]["FirstName"] = dtData.Rows[0]["FirstName"].ToString() == "" ? "." : dtData.Rows[0]["FirstName"].ToString();
                dtData.Rows[0]["Age"] = (dtData.Rows[0]["Age"].ToString() == "00" || dtData.Rows[0]["Age"].ToString() == "" || dtData.Rows[0]["Age"].ToString() == "0") ? "1" : dtData.Rows[0]["Age"].ToString();
                dtData.Rows[0]["AgeType"] = dtData.Rows[0]["AgeType"].ToString() == "" ? "D" : dtData.Rows[0]["AgeType"].ToString();
                if (mclsCFunc.ConvertToString(dtData.Rows[0]["AgeType"]).Trim().Length > 1)
                    dtData.Rows[0]["AgeType"] = dtData.Rows[0]["AgeType"].ToString().Substring(0, 1);
                dtData.Rows[0]["Address"] = dtData.Rows[0]["Address"].ToString().Replace('"', ' ');
                dtData.TableName = "Main";
                dsHisData.Tables.Add(dtData);
            }

            return dsHisData;
        }

        public string BuildHeader_SendOrder()
        {           
            StringBuilder JsonString = new StringBuilder();
            JsonString.Append("[{");
            JsonString.Append("\"key\":" + "\"Content-Type\",");
            JsonString.Append("\"value\":" + "\"application/json\",");
            JsonString.Append("\"description\":" + "\"\",");
            JsonString.Append("\"enabled\":true");
            JsonString.Append("}]");

            JsonString.Append("[{");
            JsonString.Append("\"key\":" + "\"Authorization\",");
            JsonString.Append("\"value\":" + "\"Bearer (get Token)\",");
            JsonString.Append("\"description\":" + "\"\",");
            JsonString.Append("\"enabled\":true");
            JsonString.Append("}]");

            return JsonString.ToString();
        }
        public bool updateStatus(string _BillType, string _BillNo, string _Status, string _message)
        {
            try
            {
                if (_message.Trim().Length > 100)
                    _message = _message.Substring(0, 98);

                if (_BillType == "OPB")
                {
                    string strSql = "";
                    strSql = "update  opbill set opb_extnapistatus='" + _Status + "', opb_extnapimessage='" + _message.Replace("'", "").Trim() + "' where  opb_id='" + _BillNo + "'";
                    DataTable dtUpdateSus = mGlobal.LocalDBCon.ExecuteQuery(strSql);
                    return true;
                }
                else if (_BillType == "OPO" || _BillType == "IPO")
                {
                    string strSql = "";
                    strSql = "update  iporder set ipo_extnapistatus='" + _Status + "', ipo_extnapimessage='" + _message.Replace("'", "").Trim() + "' where  ipo_orderno='" + _BillNo + "'";
                    DataTable dtUpdateSus = mGlobal.LocalDBCon.ExecuteQuery(strSql);
                    return true;
                }
            }
            catch (Exception ex) { }
            return false;
        }
        #endregion
    }
}
