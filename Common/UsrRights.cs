using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
class UsrRights 
{
    Global mGlobal = new Global();// Global data     
    CommFuncs mCommFuncs = new CommFuncs();// Common Function library						

    bool _View = false;
    bool _Add = false;
    bool _Edit = false;
    bool _Delete = false;
    bool _Discount = false;
    String _BillTypes_Phm = "";
    String _BillDepartments_Phm = "";
    String _Stores_Phm = "";
    String _BillTypes_Op = "";
    String _BillTypes_Ip = "";
    String _MessageText = "";

    public void FillRights(String FormID)
    {
        _Add =  true  ;
                _Edit = true  ;
                _Delete = true;


        String strSql = "select * from usergrp where ug_code='" + mGlobal.CurrentUserGroup + "'";
        DataTable dtData = mGlobal.LocalDBCon.ExecuteQuery(strSql);
        if (dtData.Rows.Count > 0)
        {
            // General Rights
            if (dtData.Rows[0]["ug_general"].ToString().Trim().Length == 4)
            {
                _View = true;
                _Add = dtData.Rows[0]["ug_general"].ToString().Substring(0, 1) == "Y" ? true : false;
                _Edit = dtData.Rows[0]["ug_general"].ToString().Substring(1, 1) == "Y" ? true : false;
                _Delete = dtData.Rows[0]["ug_general"].ToString().Substring(2, 1) == "Y" ? true : false;
                _Discount = dtData.Rows[0]["ug_general"].ToString().Substring(3, 1) == "Y" ? true : false;
            }

            // Pharmacy
            _BillTypes_Phm = dtData.Rows[0]["ug_phmbtypes"].ToString();
            _BillDepartments_Phm = dtData.Rows[0]["ug_phmbdepts"].ToString();
            _Stores_Phm = dtData.Rows[0]["ug_phmstores"].ToString();

            // OP / IP
            _BillTypes_Op = dtData.Rows[0]["ug_opbtypes"].ToString();
            _BillTypes_Ip = dtData.Rows[0]["ug_ipbtypes"].ToString();
        }
    }


    public bool OpReservedTokenAccess
    {
        get { return true ; }
    }
    public bool View
    {
        get { return _View; }
    }
    public bool Add
    {
        get { return _Add; }
    }
    public bool Edit
    {
        get { return _Edit; }
    }
    public bool Delete
    {
        get { return _Delete; }
    }
    public bool Discount
    {
        get { return _Discount; }
    }
    public String BillTypes_Phm
    {
        get { return _BillTypes_Phm; }
    }
    public String BillDepartments_Phm
    {
        get { return _BillDepartments_Phm; }
    }
    public String BillDepartments_Phm_Sql
    {
        get { return "'" + _BillDepartments_Phm.Trim().Replace(",", "', '") + "'"; }
    }
    public String Stores_Phm
    {
        get { return _Stores_Phm; }
    }
    public String Stores_Phm_Sql
    {
        get { return "'" + _Stores_Phm.Trim().Replace(",", "', '") + "'"; }
    }
    public String BillTypes_Op
    {
        get { return _BillTypes_Op; }
    }
    public String BillTypes_Ip
    {
        get { return _BillTypes_Ip; }
    }
    public String MessageText
    {
        get { return _MessageText; }
    }

    public bool HaveRights(String vAction)
    {
        //*************************************************//
        //** vAction  -> V-View, A-Add, E-Edit, D-Delete **//
        //*************************************************//
        bool boolRetValue = false;
        _Add = true;
        _Edit = true;
        _Delete = true;

        vAction = vAction.Trim().ToUpper();
        _MessageText = "";
        if (vAction == "D")
        {
            if (_Delete)
                boolRetValue = true;
            else
                _MessageText = "You have no right to delete this data. Contact your Administrator.";
        }
        else if (vAction == "A")
        {
            if (_Add)
                boolRetValue = true;
            else
                _MessageText = "You have no right to add this data. Contact your Administrator.";
        }
        else if (vAction == "E")
        {
            if (_Edit)
                boolRetValue = true;
            else
                _MessageText = "You have no right to edit this data. Contact your Administrator.";
        }
        else if (vAction == "V")
        {
            if (_View)
                boolRetValue = true;
            else
                _MessageText = "You have no right to view this form. Contact your Administrator.";
        }
        return boolRetValue;
    }
    public bool HaveRights(bool vView, bool vAdd, bool vEdit, bool vDelete)
    {
        _MessageText = "";
        if (vDelete)
        {
            if (_Delete)
                return true;
            else
            {
                _MessageText = "You have no right to delete this data. Contact your Administrator.";
                return false;
            }
        }
        if (vAdd)
        {
            if (_Add)
                return true;
            else
            {
                _MessageText = "You have no right to add this data. Contact your Administrator.";
                return false;
            }
        }
        if (vEdit)
        {
            if (_Edit)
                return true;
            else
            {
                _MessageText = "You have no right to edit this data. Contact your Administrator.";
                return false;
            }
        }
        if (vView)
        {
            if (_View)
                return true;
            else
            {
                _MessageText = "You have no right to view this form. Contact your Administrator.";
                return false;
            }
        }
        return false;
    }
}
