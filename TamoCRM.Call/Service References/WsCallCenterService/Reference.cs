﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18444
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TamoCRM.Call.WsCallCenterService {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="http://callcenter.topica.com", ConfigurationName="WsCallCenterService.CallCenter")]
    public interface CallCenter {
        
        // CODEGEN: Generating message contract since element name input from namespace http://callcenter.topica.com is not marked nillable
        [System.ServiceModel.OperationContractAttribute(Action="", ReplyAction="*")]
        TamoCRM.Call.WsCallCenterService.submitResponse submit(TamoCRM.Call.WsCallCenterService.submitRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="", ReplyAction="*")]
        System.Threading.Tasks.Task<TamoCRM.Call.WsCallCenterService.submitResponse> submitAsync(TamoCRM.Call.WsCallCenterService.submitRequest request);
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class submitRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="submit", Namespace="http://callcenter.topica.com", Order=0)]
        public TamoCRM.Call.WsCallCenterService.submitRequestBody Body;
        
        public submitRequest() {
        }
        
        public submitRequest(TamoCRM.Call.WsCallCenterService.submitRequestBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://callcenter.topica.com")]
    public partial class submitRequestBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string input;
        
        public submitRequestBody() {
        }
        
        public submitRequestBody(string input) {
            this.input = input;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class submitResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="submitResponse", Namespace="http://callcenter.topica.com", Order=0)]
        public TamoCRM.Call.WsCallCenterService.submitResponseBody Body;
        
        public submitResponse() {
        }
        
        public submitResponse(TamoCRM.Call.WsCallCenterService.submitResponseBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://callcenter.topica.com")]
    public partial class submitResponseBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string submitReturn;
        
        public submitResponseBody() {
        }
        
        public submitResponseBody(string submitReturn) {
            this.submitReturn = submitReturn;
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface CallCenterChannel : TamoCRM.Call.WsCallCenterService.CallCenter, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class CallCenterClient : System.ServiceModel.ClientBase<TamoCRM.Call.WsCallCenterService.CallCenter>, TamoCRM.Call.WsCallCenterService.CallCenter {
        
        public CallCenterClient() {
        }
        
        public CallCenterClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public CallCenterClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public CallCenterClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public CallCenterClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        TamoCRM.Call.WsCallCenterService.submitResponse TamoCRM.Call.WsCallCenterService.CallCenter.submit(TamoCRM.Call.WsCallCenterService.submitRequest request) {
            return base.Channel.submit(request);
        }
        
        public string submit(string input) {
            TamoCRM.Call.WsCallCenterService.submitRequest inValue = new TamoCRM.Call.WsCallCenterService.submitRequest();
            inValue.Body = new TamoCRM.Call.WsCallCenterService.submitRequestBody();
            inValue.Body.input = input;
            TamoCRM.Call.WsCallCenterService.submitResponse retVal = ((TamoCRM.Call.WsCallCenterService.CallCenter)(this)).submit(inValue);
            return retVal.Body.submitReturn;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<TamoCRM.Call.WsCallCenterService.submitResponse> TamoCRM.Call.WsCallCenterService.CallCenter.submitAsync(TamoCRM.Call.WsCallCenterService.submitRequest request) {
            return base.Channel.submitAsync(request);
        }
        
        public System.Threading.Tasks.Task<TamoCRM.Call.WsCallCenterService.submitResponse> submitAsync(string input) {
            TamoCRM.Call.WsCallCenterService.submitRequest inValue = new TamoCRM.Call.WsCallCenterService.submitRequest();
            inValue.Body = new TamoCRM.Call.WsCallCenterService.submitRequestBody();
            inValue.Body.input = input;
            return ((TamoCRM.Call.WsCallCenterService.CallCenter)(this)).submitAsync(inValue);
        }
    }
}
