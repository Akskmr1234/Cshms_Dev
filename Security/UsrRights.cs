using System;
using System.Collections.Generic;
using System.Text;

    class UsrRights1
    {

        bool _View;
        bool _Add;
        bool _Edit;
        bool _Delete;
        String _MessageText = "";
        public void FillRights(String FormID)
        {
            _View = true;
            _Add = true;
            _Edit = true;
            _Delete = true;
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
            _MessageText="";
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