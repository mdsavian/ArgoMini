﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ArgoMini.NfeIntegracaoSoap {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="http://www.portalfiscal.inf.br/nfe/wsdl/NFeIntegracao", ConfigurationName="NfeIntegracaoSoap.NFeIntegracaoSoap")]
    public interface NFeIntegracaoSoap {
        
        // CODEGEN: Generating message contract since element name nfeDadosMsgDownload from namespace http://www.portalfiscal.inf.br/nfe/wsdl/NFeIntegracao is not marked nillable
        [System.ServiceModel.OperationContractAttribute(Action="http://www.portalfiscal.inf.br/nfe/wsdl/NFeIntegracao/nfeIntegracaoContab", ReplyAction="*")]
        ArgoMini.NfeIntegracaoSoap.nfeIntegracaoContabResponse nfeIntegracaoContab(ArgoMini.NfeIntegracaoSoap.nfeIntegracaoContabRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://www.portalfiscal.inf.br/nfe/wsdl/NFeIntegracao/nfeIntegracaoContab", ReplyAction="*")]
        System.Threading.Tasks.Task<ArgoMini.NfeIntegracaoSoap.nfeIntegracaoContabResponse> nfeIntegracaoContabAsync(ArgoMini.NfeIntegracaoSoap.nfeIntegracaoContabRequest request);
        
        // CODEGEN: Generating message contract since element name nfeDadosMsgDownload from namespace http://www.portalfiscal.inf.br/nfe/wsdl/NFeIntegracao is not marked nillable
        [System.ServiceModel.OperationContractAttribute(Action="http://www.portalfiscal.inf.br/nfe/wsdl/NFeIntegracao/nfeIntegracaoSEAPA", ReplyAction="*")]
        ArgoMini.NfeIntegracaoSoap.nfeIntegracaoSEAPAResponse nfeIntegracaoSEAPA(ArgoMini.NfeIntegracaoSoap.nfeIntegracaoSEAPARequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://www.portalfiscal.inf.br/nfe/wsdl/NFeIntegracao/nfeIntegracaoSEAPA", ReplyAction="*")]
        System.Threading.Tasks.Task<ArgoMini.NfeIntegracaoSoap.nfeIntegracaoSEAPAResponse> nfeIntegracaoSEAPAAsync(ArgoMini.NfeIntegracaoSoap.nfeIntegracaoSEAPARequest request);
        
        // CODEGEN: Generating message contract since element name nfeDadosMsgDownload from namespace http://www.portalfiscal.inf.br/nfe/wsdl/NFeIntegracao is not marked nillable
        [System.ServiceModel.OperationContractAttribute(Action="http://www.portalfiscal.inf.br/nfe/wsdl/NFeIntegracao/nfeIntegracaoOrgao", ReplyAction="*")]
        ArgoMini.NfeIntegracaoSoap.nfeIntegracaoOrgaoResponse nfeIntegracaoOrgao(ArgoMini.NfeIntegracaoSoap.nfeIntegracaoOrgaoRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://www.portalfiscal.inf.br/nfe/wsdl/NFeIntegracao/nfeIntegracaoOrgao", ReplyAction="*")]
        System.Threading.Tasks.Task<ArgoMini.NfeIntegracaoSoap.nfeIntegracaoOrgaoResponse> nfeIntegracaoOrgaoAsync(ArgoMini.NfeIntegracaoSoap.nfeIntegracaoOrgaoRequest request);
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class nfeIntegracaoContabRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="nfeIntegracaoContab", Namespace="http://www.portalfiscal.inf.br/nfe/wsdl/NFeIntegracao", Order=0)]
        public ArgoMini.NfeIntegracaoSoap.nfeIntegracaoContabRequestBody Body;
        
        public nfeIntegracaoContabRequest() {
        }
        
        public nfeIntegracaoContabRequest(ArgoMini.NfeIntegracaoSoap.nfeIntegracaoContabRequestBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://www.portalfiscal.inf.br/nfe/wsdl/NFeIntegracao")]
    public partial class nfeIntegracaoContabRequestBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public System.Xml.Linq.XElement nfeDadosMsgDownload;
        
        public nfeIntegracaoContabRequestBody() {
        }
        
        public nfeIntegracaoContabRequestBody(System.Xml.Linq.XElement nfeDadosMsgDownload) {
            this.nfeDadosMsgDownload = nfeDadosMsgDownload;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class nfeIntegracaoContabResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="nfeIntegracaoContabResponse", Namespace="http://www.portalfiscal.inf.br/nfe/wsdl/NFeIntegracao", Order=0)]
        public ArgoMini.NfeIntegracaoSoap.nfeIntegracaoContabResponseBody Body;
        
        public nfeIntegracaoContabResponse() {
        }
        
        public nfeIntegracaoContabResponse(ArgoMini.NfeIntegracaoSoap.nfeIntegracaoContabResponseBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://www.portalfiscal.inf.br/nfe/wsdl/NFeIntegracao")]
    public partial class nfeIntegracaoContabResponseBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public System.Xml.Linq.XElement nfeIntegracaoContabResult;
        
        public nfeIntegracaoContabResponseBody() {
        }
        
        public nfeIntegracaoContabResponseBody(System.Xml.Linq.XElement nfeIntegracaoContabResult) {
            this.nfeIntegracaoContabResult = nfeIntegracaoContabResult;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class nfeIntegracaoSEAPARequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="nfeIntegracaoSEAPA", Namespace="http://www.portalfiscal.inf.br/nfe/wsdl/NFeIntegracao", Order=0)]
        public ArgoMini.NfeIntegracaoSoap.nfeIntegracaoSEAPARequestBody Body;
        
        public nfeIntegracaoSEAPARequest() {
        }
        
        public nfeIntegracaoSEAPARequest(ArgoMini.NfeIntegracaoSoap.nfeIntegracaoSEAPARequestBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://www.portalfiscal.inf.br/nfe/wsdl/NFeIntegracao")]
    public partial class nfeIntegracaoSEAPARequestBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public System.Xml.Linq.XElement nfeDadosMsgDownload;
        
        public nfeIntegracaoSEAPARequestBody() {
        }
        
        public nfeIntegracaoSEAPARequestBody(System.Xml.Linq.XElement nfeDadosMsgDownload) {
            this.nfeDadosMsgDownload = nfeDadosMsgDownload;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class nfeIntegracaoSEAPAResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="nfeIntegracaoSEAPAResponse", Namespace="http://www.portalfiscal.inf.br/nfe/wsdl/NFeIntegracao", Order=0)]
        public ArgoMini.NfeIntegracaoSoap.nfeIntegracaoSEAPAResponseBody Body;
        
        public nfeIntegracaoSEAPAResponse() {
        }
        
        public nfeIntegracaoSEAPAResponse(ArgoMini.NfeIntegracaoSoap.nfeIntegracaoSEAPAResponseBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://www.portalfiscal.inf.br/nfe/wsdl/NFeIntegracao")]
    public partial class nfeIntegracaoSEAPAResponseBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public System.Xml.Linq.XElement nfeIntegracaoSEAPAResult;
        
        public nfeIntegracaoSEAPAResponseBody() {
        }
        
        public nfeIntegracaoSEAPAResponseBody(System.Xml.Linq.XElement nfeIntegracaoSEAPAResult) {
            this.nfeIntegracaoSEAPAResult = nfeIntegracaoSEAPAResult;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class nfeIntegracaoOrgaoRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="nfeIntegracaoOrgao", Namespace="http://www.portalfiscal.inf.br/nfe/wsdl/NFeIntegracao", Order=0)]
        public ArgoMini.NfeIntegracaoSoap.nfeIntegracaoOrgaoRequestBody Body;
        
        public nfeIntegracaoOrgaoRequest() {
        }
        
        public nfeIntegracaoOrgaoRequest(ArgoMini.NfeIntegracaoSoap.nfeIntegracaoOrgaoRequestBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://www.portalfiscal.inf.br/nfe/wsdl/NFeIntegracao")]
    public partial class nfeIntegracaoOrgaoRequestBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public System.Xml.Linq.XElement nfeDadosMsgDownload;
        
        public nfeIntegracaoOrgaoRequestBody() {
        }
        
        public nfeIntegracaoOrgaoRequestBody(System.Xml.Linq.XElement nfeDadosMsgDownload) {
            this.nfeDadosMsgDownload = nfeDadosMsgDownload;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class nfeIntegracaoOrgaoResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="nfeIntegracaoOrgaoResponse", Namespace="http://www.portalfiscal.inf.br/nfe/wsdl/NFeIntegracao", Order=0)]
        public ArgoMini.NfeIntegracaoSoap.nfeIntegracaoOrgaoResponseBody Body;
        
        public nfeIntegracaoOrgaoResponse() {
        }
        
        public nfeIntegracaoOrgaoResponse(ArgoMini.NfeIntegracaoSoap.nfeIntegracaoOrgaoResponseBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://www.portalfiscal.inf.br/nfe/wsdl/NFeIntegracao")]
    public partial class nfeIntegracaoOrgaoResponseBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public System.Xml.Linq.XElement nfeIntegracaoOrgaoResult;
        
        public nfeIntegracaoOrgaoResponseBody() {
        }
        
        public nfeIntegracaoOrgaoResponseBody(System.Xml.Linq.XElement nfeIntegracaoOrgaoResult) {
            this.nfeIntegracaoOrgaoResult = nfeIntegracaoOrgaoResult;
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface NFeIntegracaoSoapChannel : ArgoMini.NfeIntegracaoSoap.NFeIntegracaoSoap, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class NFeIntegracaoSoapClient : System.ServiceModel.ClientBase<ArgoMini.NfeIntegracaoSoap.NFeIntegracaoSoap>, ArgoMini.NfeIntegracaoSoap.NFeIntegracaoSoap {
        
        public NFeIntegracaoSoapClient() {
        }
        
        public NFeIntegracaoSoapClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public NFeIntegracaoSoapClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public NFeIntegracaoSoapClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public NFeIntegracaoSoapClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        ArgoMini.NfeIntegracaoSoap.nfeIntegracaoContabResponse ArgoMini.NfeIntegracaoSoap.NFeIntegracaoSoap.nfeIntegracaoContab(ArgoMini.NfeIntegracaoSoap.nfeIntegracaoContabRequest request) {
            return base.Channel.nfeIntegracaoContab(request);
        }
        
        public System.Xml.Linq.XElement nfeIntegracaoContab(System.Xml.Linq.XElement nfeDadosMsgDownload) {
            ArgoMini.NfeIntegracaoSoap.nfeIntegracaoContabRequest inValue = new ArgoMini.NfeIntegracaoSoap.nfeIntegracaoContabRequest();
            inValue.Body = new ArgoMini.NfeIntegracaoSoap.nfeIntegracaoContabRequestBody();
            inValue.Body.nfeDadosMsgDownload = nfeDadosMsgDownload;
            ArgoMini.NfeIntegracaoSoap.nfeIntegracaoContabResponse retVal = ((ArgoMini.NfeIntegracaoSoap.NFeIntegracaoSoap)(this)).nfeIntegracaoContab(inValue);
            return retVal.Body.nfeIntegracaoContabResult;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<ArgoMini.NfeIntegracaoSoap.nfeIntegracaoContabResponse> ArgoMini.NfeIntegracaoSoap.NFeIntegracaoSoap.nfeIntegracaoContabAsync(ArgoMini.NfeIntegracaoSoap.nfeIntegracaoContabRequest request) {
            return base.Channel.nfeIntegracaoContabAsync(request);
        }
        
        public System.Threading.Tasks.Task<ArgoMini.NfeIntegracaoSoap.nfeIntegracaoContabResponse> nfeIntegracaoContabAsync(System.Xml.Linq.XElement nfeDadosMsgDownload) {
            ArgoMini.NfeIntegracaoSoap.nfeIntegracaoContabRequest inValue = new ArgoMini.NfeIntegracaoSoap.nfeIntegracaoContabRequest();
            inValue.Body = new ArgoMini.NfeIntegracaoSoap.nfeIntegracaoContabRequestBody();
            inValue.Body.nfeDadosMsgDownload = nfeDadosMsgDownload;
            return ((ArgoMini.NfeIntegracaoSoap.NFeIntegracaoSoap)(this)).nfeIntegracaoContabAsync(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        ArgoMini.NfeIntegracaoSoap.nfeIntegracaoSEAPAResponse ArgoMini.NfeIntegracaoSoap.NFeIntegracaoSoap.nfeIntegracaoSEAPA(ArgoMini.NfeIntegracaoSoap.nfeIntegracaoSEAPARequest request) {
            return base.Channel.nfeIntegracaoSEAPA(request);
        }
        
        public System.Xml.Linq.XElement nfeIntegracaoSEAPA(System.Xml.Linq.XElement nfeDadosMsgDownload) {
            ArgoMini.NfeIntegracaoSoap.nfeIntegracaoSEAPARequest inValue = new ArgoMini.NfeIntegracaoSoap.nfeIntegracaoSEAPARequest();
            inValue.Body = new ArgoMini.NfeIntegracaoSoap.nfeIntegracaoSEAPARequestBody();
            inValue.Body.nfeDadosMsgDownload = nfeDadosMsgDownload;
            ArgoMini.NfeIntegracaoSoap.nfeIntegracaoSEAPAResponse retVal = ((ArgoMini.NfeIntegracaoSoap.NFeIntegracaoSoap)(this)).nfeIntegracaoSEAPA(inValue);
            return retVal.Body.nfeIntegracaoSEAPAResult;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<ArgoMini.NfeIntegracaoSoap.nfeIntegracaoSEAPAResponse> ArgoMini.NfeIntegracaoSoap.NFeIntegracaoSoap.nfeIntegracaoSEAPAAsync(ArgoMini.NfeIntegracaoSoap.nfeIntegracaoSEAPARequest request) {
            return base.Channel.nfeIntegracaoSEAPAAsync(request);
        }
        
        public System.Threading.Tasks.Task<ArgoMini.NfeIntegracaoSoap.nfeIntegracaoSEAPAResponse> nfeIntegracaoSEAPAAsync(System.Xml.Linq.XElement nfeDadosMsgDownload) {
            ArgoMini.NfeIntegracaoSoap.nfeIntegracaoSEAPARequest inValue = new ArgoMini.NfeIntegracaoSoap.nfeIntegracaoSEAPARequest();
            inValue.Body = new ArgoMini.NfeIntegracaoSoap.nfeIntegracaoSEAPARequestBody();
            inValue.Body.nfeDadosMsgDownload = nfeDadosMsgDownload;
            return ((ArgoMini.NfeIntegracaoSoap.NFeIntegracaoSoap)(this)).nfeIntegracaoSEAPAAsync(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        ArgoMini.NfeIntegracaoSoap.nfeIntegracaoOrgaoResponse ArgoMini.NfeIntegracaoSoap.NFeIntegracaoSoap.nfeIntegracaoOrgao(ArgoMini.NfeIntegracaoSoap.nfeIntegracaoOrgaoRequest request) {
            return base.Channel.nfeIntegracaoOrgao(request);
        }
        
        public System.Xml.Linq.XElement nfeIntegracaoOrgao(System.Xml.Linq.XElement nfeDadosMsgDownload) {
            ArgoMini.NfeIntegracaoSoap.nfeIntegracaoOrgaoRequest inValue = new ArgoMini.NfeIntegracaoSoap.nfeIntegracaoOrgaoRequest();
            inValue.Body = new ArgoMini.NfeIntegracaoSoap.nfeIntegracaoOrgaoRequestBody();
            inValue.Body.nfeDadosMsgDownload = nfeDadosMsgDownload;
            ArgoMini.NfeIntegracaoSoap.nfeIntegracaoOrgaoResponse retVal = ((ArgoMini.NfeIntegracaoSoap.NFeIntegracaoSoap)(this)).nfeIntegracaoOrgao(inValue);
            return retVal.Body.nfeIntegracaoOrgaoResult;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<ArgoMini.NfeIntegracaoSoap.nfeIntegracaoOrgaoResponse> ArgoMini.NfeIntegracaoSoap.NFeIntegracaoSoap.nfeIntegracaoOrgaoAsync(ArgoMini.NfeIntegracaoSoap.nfeIntegracaoOrgaoRequest request) {
            return base.Channel.nfeIntegracaoOrgaoAsync(request);
        }
        
        public System.Threading.Tasks.Task<ArgoMini.NfeIntegracaoSoap.nfeIntegracaoOrgaoResponse> nfeIntegracaoOrgaoAsync(System.Xml.Linq.XElement nfeDadosMsgDownload) {
            ArgoMini.NfeIntegracaoSoap.nfeIntegracaoOrgaoRequest inValue = new ArgoMini.NfeIntegracaoSoap.nfeIntegracaoOrgaoRequest();
            inValue.Body = new ArgoMini.NfeIntegracaoSoap.nfeIntegracaoOrgaoRequestBody();
            inValue.Body.nfeDadosMsgDownload = nfeDadosMsgDownload;
            return ((ArgoMini.NfeIntegracaoSoap.NFeIntegracaoSoap)(this)).nfeIntegracaoOrgaoAsync(inValue);
        }
    }
}