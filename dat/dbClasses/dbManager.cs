using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.OleDb;

    class dbManager
    {
        private DBConnAcc mdbAcc;
        private string mstrTable;
        private string mstrKeyFld;

        public dbManager()
        {
            mdbAcc = new DBConnAcc();
            ClearAll();
        }
        public dbManager(Object obj)
        {
            mdbAcc = new DBConnAcc(obj);
            ClearAll();
        }
        private void ClearAll()
        {
            mstrTable = "";
            mstrKeyFld = "";
        }
        /*public DataTable getData(String vFilterValue)
        {

        }
        public DataTable getData(String vFilterValue,String vCondition)
        {

        }*/
        public String TableName
        {
            get { return mstrTable; }
            set { mstrTable=value; }
        }
        public String KeyField
        {
            get { return mstrKeyFld; }
            set { mstrKeyFld = value; }
        }
    }
