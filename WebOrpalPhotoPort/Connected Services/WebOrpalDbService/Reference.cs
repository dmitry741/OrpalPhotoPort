﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebOrpalPhotoPort.WebOrpalDbService {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="WebOrpalDbService.IWebDbService")]
    public interface IWebDbService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IWebDbService/GetUsers", ReplyAction="http://tempuri.org/IWebDbService/GetUsersResponse")]
        OrpalPhotoPort.Domain.DataContractMemebers.UserDataContract[] GetUsers();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IWebDbService/GetUsers", ReplyAction="http://tempuri.org/IWebDbService/GetUsersResponse")]
        System.Threading.Tasks.Task<OrpalPhotoPort.Domain.DataContractMemebers.UserDataContract[]> GetUsersAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IWebDbService/GetActiveUsers", ReplyAction="http://tempuri.org/IWebDbService/GetActiveUsersResponse")]
        OrpalPhotoPort.Domain.DataContractMemebers.UserDataContract[] GetActiveUsers();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IWebDbService/GetActiveUsers", ReplyAction="http://tempuri.org/IWebDbService/GetActiveUsersResponse")]
        System.Threading.Tasks.Task<OrpalPhotoPort.Domain.DataContractMemebers.UserDataContract[]> GetActiveUsersAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IWebDbService/AddUser", ReplyAction="http://tempuri.org/IWebDbService/AddUserResponse")]
        bool AddUser(OrpalPhotoPort.Domain.DataContractMemebers.UserDataContract udc);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IWebDbService/AddUser", ReplyAction="http://tempuri.org/IWebDbService/AddUserResponse")]
        System.Threading.Tasks.Task<bool> AddUserAsync(OrpalPhotoPort.Domain.DataContractMemebers.UserDataContract udc);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IWebDbService/EditUser", ReplyAction="http://tempuri.org/IWebDbService/EditUserResponse")]
        bool EditUser(OrpalPhotoPort.Domain.DataContractMemebers.UserDataContract udc);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IWebDbService/EditUser", ReplyAction="http://tempuri.org/IWebDbService/EditUserResponse")]
        System.Threading.Tasks.Task<bool> EditUserAsync(OrpalPhotoPort.Domain.DataContractMemebers.UserDataContract udc);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IWebDbService/RemoveUserAt", ReplyAction="http://tempuri.org/IWebDbService/RemoveUserAtResponse")]
        bool RemoveUserAt(int id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IWebDbService/RemoveUserAt", ReplyAction="http://tempuri.org/IWebDbService/RemoveUserAtResponse")]
        System.Threading.Tasks.Task<bool> RemoveUserAtAsync(int id);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IWebDbServiceChannel : WebOrpalPhotoPort.WebOrpalDbService.IWebDbService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class WebDbServiceClient : System.ServiceModel.ClientBase<WebOrpalPhotoPort.WebOrpalDbService.IWebDbService>, WebOrpalPhotoPort.WebOrpalDbService.IWebDbService {
        
        public WebDbServiceClient() {
        }
        
        public WebDbServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public WebDbServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public WebDbServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public WebDbServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public OrpalPhotoPort.Domain.DataContractMemebers.UserDataContract[] GetUsers() {
            return base.Channel.GetUsers();
        }
        
        public System.Threading.Tasks.Task<OrpalPhotoPort.Domain.DataContractMemebers.UserDataContract[]> GetUsersAsync() {
            return base.Channel.GetUsersAsync();
        }
        
        public OrpalPhotoPort.Domain.DataContractMemebers.UserDataContract[] GetActiveUsers() {
            return base.Channel.GetActiveUsers();
        }
        
        public System.Threading.Tasks.Task<OrpalPhotoPort.Domain.DataContractMemebers.UserDataContract[]> GetActiveUsersAsync() {
            return base.Channel.GetActiveUsersAsync();
        }
        
        public bool AddUser(OrpalPhotoPort.Domain.DataContractMemebers.UserDataContract udc) {
            return base.Channel.AddUser(udc);
        }
        
        public System.Threading.Tasks.Task<bool> AddUserAsync(OrpalPhotoPort.Domain.DataContractMemebers.UserDataContract udc) {
            return base.Channel.AddUserAsync(udc);
        }
        
        public bool EditUser(OrpalPhotoPort.Domain.DataContractMemebers.UserDataContract udc) {
            return base.Channel.EditUser(udc);
        }
        
        public System.Threading.Tasks.Task<bool> EditUserAsync(OrpalPhotoPort.Domain.DataContractMemebers.UserDataContract udc) {
            return base.Channel.EditUserAsync(udc);
        }
        
        public bool RemoveUserAt(int id) {
            return base.Channel.RemoveUserAt(id);
        }
        
        public System.Threading.Tasks.Task<bool> RemoveUserAtAsync(int id) {
            return base.Channel.RemoveUserAtAsync(id);
        }
    }
}
