﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:2.0.50727.8762
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// 
// This source code was auto-generated by Microsoft.VSDesigner, Version 2.0.50727.8762.
// 
#pragma warning disable 1591

namespace CsHms.SmartDataSync {
    using System.Diagnostics;
    using System.Web.Services;
    using System.ComponentModel;
    using System.Web.Services.Protocols;
    using System;
    using System.Xml.Serialization;
    using System.Data;
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.5420")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name="SmartDataSyncSoap", Namespace="http://tempuri.org/")]
    public partial class SmartDataSync : System.Web.Services.Protocols.SoapHttpClientProtocol {
        
        private System.Threading.SendOrPostCallback TestSiteOperationCompleted;
        
        private System.Threading.SendOrPostCallback getEmptyTranDataSetOperationCompleted;
        
        private System.Threading.SendOrPostCallback ResultDataSetOperationCompleted;
        
        private System.Threading.SendOrPostCallback UpdateDailyTransOperationCompleted;
        
        private bool useDefaultCredentialsSetExplicitly;
        
        /// <remarks/>
        public SmartDataSync() {
            this.Url = global::CsHms.Properties.Settings.Default.SmartBiz_SmartDataSync_SmartDataSync;
            if ((this.IsLocalFileSystemWebService(this.Url) == true)) {
                this.UseDefaultCredentials = true;
                this.useDefaultCredentialsSetExplicitly = false;
            }
            else {
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        public new string Url {
            get {
                return base.Url;
            }
            set {
                if ((((this.IsLocalFileSystemWebService(base.Url) == true) 
                            && (this.useDefaultCredentialsSetExplicitly == false)) 
                            && (this.IsLocalFileSystemWebService(value) == false))) {
                    base.UseDefaultCredentials = false;
                }
                base.Url = value;
            }
        }
        
        public new bool UseDefaultCredentials {
            get {
                return base.UseDefaultCredentials;
            }
            set {
                base.UseDefaultCredentials = value;
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        /// <remarks/>
        public event TestSiteCompletedEventHandler TestSiteCompleted;
        
        /// <remarks/>
        public event getEmptyTranDataSetCompletedEventHandler getEmptyTranDataSetCompleted;
        
        /// <remarks/>
        public event ResultDataSetCompletedEventHandler ResultDataSetCompleted;
        
        /// <remarks/>
        public event UpdateDailyTransCompletedEventHandler UpdateDailyTransCompleted;
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/TestSite", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public bool TestSite() {
            object[] results = this.Invoke("TestSite", new object[0]);
            return ((bool)(results[0]));
        }
        
        /// <remarks/>
        public void TestSiteAsync() {
            this.TestSiteAsync(null);
        }
        
        /// <remarks/>
        public void TestSiteAsync(object userState) {
            if ((this.TestSiteOperationCompleted == null)) {
                this.TestSiteOperationCompleted = new System.Threading.SendOrPostCallback(this.OnTestSiteOperationCompleted);
            }
            this.InvokeAsync("TestSite", new object[0], this.TestSiteOperationCompleted, userState);
        }
        
        private void OnTestSiteOperationCompleted(object arg) {
            if ((this.TestSiteCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.TestSiteCompleted(this, new TestSiteCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/getEmptyTranDataSet", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public System.Data.DataSet getEmptyTranDataSet() {
            object[] results = this.Invoke("getEmptyTranDataSet", new object[0]);
            return ((System.Data.DataSet)(results[0]));
        }
        
        /// <remarks/>
        public void getEmptyTranDataSetAsync() {
            this.getEmptyTranDataSetAsync(null);
        }
        
        /// <remarks/>
        public void getEmptyTranDataSetAsync(object userState) {
            if ((this.getEmptyTranDataSetOperationCompleted == null)) {
                this.getEmptyTranDataSetOperationCompleted = new System.Threading.SendOrPostCallback(this.OngetEmptyTranDataSetOperationCompleted);
            }
            this.InvokeAsync("getEmptyTranDataSet", new object[0], this.getEmptyTranDataSetOperationCompleted, userState);
        }
        
        private void OngetEmptyTranDataSetOperationCompleted(object arg) {
            if ((this.getEmptyTranDataSetCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.getEmptyTranDataSetCompleted(this, new getEmptyTranDataSetCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/ResultDataSet", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public System.Data.DataSet ResultDataSet() {
            object[] results = this.Invoke("ResultDataSet", new object[0]);
            return ((System.Data.DataSet)(results[0]));
        }
        
        /// <remarks/>
        public void ResultDataSetAsync() {
            this.ResultDataSetAsync(null);
        }
        
        /// <remarks/>
        public void ResultDataSetAsync(object userState) {
            if ((this.ResultDataSetOperationCompleted == null)) {
                this.ResultDataSetOperationCompleted = new System.Threading.SendOrPostCallback(this.OnResultDataSetOperationCompleted);
            }
            this.InvokeAsync("ResultDataSet", new object[0], this.ResultDataSetOperationCompleted, userState);
        }
        
        private void OnResultDataSetOperationCompleted(object arg) {
            if ((this.ResultDataSetCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.ResultDataSetCompleted(this, new ResultDataSetCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/UpdateDailyTrans", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public bool UpdateDailyTrans(string BranchCode, string TranDt, System.Data.DataSet ds) {
            object[] results = this.Invoke("UpdateDailyTrans", new object[] {
                        BranchCode,
                        TranDt,
                        ds});
            return ((bool)(results[0]));
        }
        
        /// <remarks/>
        public void UpdateDailyTransAsync(string BranchCode, string TranDt, System.Data.DataSet ds) {
            this.UpdateDailyTransAsync(BranchCode, TranDt, ds, null);
        }
        
        /// <remarks/>
        public void UpdateDailyTransAsync(string BranchCode, string TranDt, System.Data.DataSet ds, object userState) {
            if ((this.UpdateDailyTransOperationCompleted == null)) {
                this.UpdateDailyTransOperationCompleted = new System.Threading.SendOrPostCallback(this.OnUpdateDailyTransOperationCompleted);
            }
            this.InvokeAsync("UpdateDailyTrans", new object[] {
                        BranchCode,
                        TranDt,
                        ds}, this.UpdateDailyTransOperationCompleted, userState);
        }
        
        private void OnUpdateDailyTransOperationCompleted(object arg) {
            if ((this.UpdateDailyTransCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.UpdateDailyTransCompleted(this, new UpdateDailyTransCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        public new void CancelAsync(object userState) {
            base.CancelAsync(userState);
        }
        
        private bool IsLocalFileSystemWebService(string url) {
            if (((url == null) 
                        || (url == string.Empty))) {
                return false;
            }
            System.Uri wsUri = new System.Uri(url);
            if (((wsUri.Port >= 1024) 
                        && (string.Compare(wsUri.Host, "localHost", System.StringComparison.OrdinalIgnoreCase) == 0))) {
                return true;
            }
            return false;
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.5420")]
    public delegate void TestSiteCompletedEventHandler(object sender, TestSiteCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.5420")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class TestSiteCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal TestSiteCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public bool Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((bool)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.5420")]
    public delegate void getEmptyTranDataSetCompletedEventHandler(object sender, getEmptyTranDataSetCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.5420")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class getEmptyTranDataSetCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal getEmptyTranDataSetCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public System.Data.DataSet Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((System.Data.DataSet)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.5420")]
    public delegate void ResultDataSetCompletedEventHandler(object sender, ResultDataSetCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.5420")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class ResultDataSetCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal ResultDataSetCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public System.Data.DataSet Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((System.Data.DataSet)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.5420")]
    public delegate void UpdateDailyTransCompletedEventHandler(object sender, UpdateDailyTransCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.5420")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class UpdateDailyTransCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal UpdateDailyTransCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public bool Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((bool)(this.results[0]));
            }
        }
    }
}

#pragma warning restore 1591