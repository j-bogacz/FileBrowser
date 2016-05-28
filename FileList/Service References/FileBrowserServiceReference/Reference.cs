﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FileBrowser.FileList.FileBrowserServiceReference {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="FileInfo", Namespace="http://schemas.datacontract.org/2004/07/FileServer")]
    [System.SerializableAttribute()]
    public partial class FileInfo : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.DateTime CreatedField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private byte[] IconField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private bool IsFolderField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private bool IsParentField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.DateTime ModifiedField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string NameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string TypeField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.DateTime Created {
            get {
                return this.CreatedField;
            }
            set {
                if ((this.CreatedField.Equals(value) != true)) {
                    this.CreatedField = value;
                    this.RaisePropertyChanged("Created");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public byte[] Icon {
            get {
                return this.IconField;
            }
            set {
                if ((object.ReferenceEquals(this.IconField, value) != true)) {
                    this.IconField = value;
                    this.RaisePropertyChanged("Icon");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public bool IsFolder {
            get {
                return this.IsFolderField;
            }
            set {
                if ((this.IsFolderField.Equals(value) != true)) {
                    this.IsFolderField = value;
                    this.RaisePropertyChanged("IsFolder");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public bool IsParent {
            get {
                return this.IsParentField;
            }
            set {
                if ((this.IsParentField.Equals(value) != true)) {
                    this.IsParentField = value;
                    this.RaisePropertyChanged("IsParent");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.DateTime Modified {
            get {
                return this.ModifiedField;
            }
            set {
                if ((this.ModifiedField.Equals(value) != true)) {
                    this.ModifiedField = value;
                    this.RaisePropertyChanged("Modified");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Name {
            get {
                return this.NameField;
            }
            set {
                if ((object.ReferenceEquals(this.NameField, value) != true)) {
                    this.NameField = value;
                    this.RaisePropertyChanged("Name");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Type {
            get {
                return this.TypeField;
            }
            set {
                if ((object.ReferenceEquals(this.TypeField, value) != true)) {
                    this.TypeField = value;
                    this.RaisePropertyChanged("Type");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="FileBrowserServiceReference.IFileBrowserService", CallbackContract=typeof(FileBrowser.FileList.FileBrowserServiceReference.IFileBrowserServiceCallback))]
    public interface IFileBrowserService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IFileBrowserService/RequestFileList", ReplyAction="http://tempuri.org/IFileBrowserService/RequestFileListResponse")]
        void RequestFileList(string path);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IFileBrowserService/RequestFileList", ReplyAction="http://tempuri.org/IFileBrowserService/RequestFileListResponse")]
        System.Threading.Tasks.Task RequestFileListAsync(string path);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IFileBrowserServiceCallback {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IFileBrowserService/FileListChanged", ReplyAction="http://tempuri.org/IFileBrowserService/FileListChangedResponse")]
        void FileListChanged(FileBrowser.FileList.FileBrowserServiceReference.FileInfo[] fileList);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IFileBrowserServiceChannel : FileBrowser.FileList.FileBrowserServiceReference.IFileBrowserService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class FileBrowserServiceClient : System.ServiceModel.DuplexClientBase<FileBrowser.FileList.FileBrowserServiceReference.IFileBrowserService>, FileBrowser.FileList.FileBrowserServiceReference.IFileBrowserService {
        
        public FileBrowserServiceClient(System.ServiceModel.InstanceContext callbackInstance) : 
                base(callbackInstance) {
        }
        
        public FileBrowserServiceClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName) : 
                base(callbackInstance, endpointConfigurationName) {
        }
        
        public FileBrowserServiceClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName, string remoteAddress) : 
                base(callbackInstance, endpointConfigurationName, remoteAddress) {
        }
        
        public FileBrowserServiceClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(callbackInstance, endpointConfigurationName, remoteAddress) {
        }
        
        public FileBrowserServiceClient(System.ServiceModel.InstanceContext callbackInstance, System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(callbackInstance, binding, remoteAddress) {
        }
        
        public void RequestFileList(string path) {
            base.Channel.RequestFileList(path);
        }
        
        public System.Threading.Tasks.Task RequestFileListAsync(string path) {
            return base.Channel.RequestFileListAsync(path);
        }
    }
}
