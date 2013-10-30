﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18051
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace LFM.Submissions.AgentServices.EdrsAttachmentService {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="http://attachmentv1_0.ws.bg.lr.gov/", ConfigurationName="EdrsAttachmentService.AttachmentV1_0Service")]
    public interface AttachmentV1_0Service {
        
        // CODEGEN: Parameter 'return' requires additional schema information that cannot be captured using the parameter mode. The specific attribute is 'System.Xml.Serialization.XmlElementAttribute'.
        [System.ServiceModel.OperationContractAttribute(Action="", ReplyAction="*")]
        [System.ServiceModel.FaultContractAttribute(typeof(string), Action="", Name="SOAPEngineSystemException")]
        [System.ServiceModel.FaultContractAttribute(typeof(string), Action="", Name="SchemaException")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        [return: System.ServiceModel.MessageParameterAttribute(Name="return")]
        LFM.Submissions.AgentServices.EdrsAttachmentService.newAttachmentResponse newAttachment(LFM.Submissions.AgentServices.EdrsAttachmentService.newAttachmentRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="", ReplyAction="*")]
        System.Threading.Tasks.Task<LFM.Submissions.AgentServices.EdrsAttachmentService.newAttachmentResponse> newAttachmentAsync(LFM.Submissions.AgentServices.EdrsAttachmentService.newAttachmentRequest request);
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.18058")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.oscre.org/ns/eReg-Final/2012/RequestAttachmentV1_0")]
    public partial class AttachmentV1_0Type : object, System.ComponentModel.INotifyPropertyChanged {
        
        private string messageIdField;
        
        private string externalReferenceField;
        
        private string applicationMessageIdField;
        
        private string applicationServiceField;
        
        private object itemField;
        
        private CertifiedTypeContent certifiedCopyField;
        
        private AttachmentType attachmentField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=0)]
        public string MessageId {
            get {
                return this.messageIdField;
            }
            set {
                this.messageIdField = value;
                this.RaisePropertyChanged("MessageId");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=1)]
        public string ExternalReference {
            get {
                return this.externalReferenceField;
            }
            set {
                this.externalReferenceField = value;
                this.RaisePropertyChanged("ExternalReference");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=2)]
        public string ApplicationMessageId {
            get {
                return this.applicationMessageIdField;
            }
            set {
                this.applicationMessageIdField = value;
                this.RaisePropertyChanged("ApplicationMessageId");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType="integer", Order=3)]
        public string ApplicationService {
            get {
                return this.applicationServiceField;
            }
            set {
                this.applicationServiceField = value;
                this.RaisePropertyChanged("ApplicationService");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("ApplicationType", typeof(ApplicationTypeContent), Order=4)]
        [System.Xml.Serialization.XmlElementAttribute("MessageId", typeof(string), DataType="positiveInteger", Order=4)]
        [System.Xml.Serialization.XmlElementAttribute("DocumentName", typeof(DocumentNameContent), Order=4)]
        public object Item {
            get {
                return this.itemField;
            }
            set {
                this.itemField = value;
                this.RaisePropertyChanged("Item");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=5)]
        public CertifiedTypeContent CertifiedCopy {
            get {
                return this.certifiedCopyField;
            }
            set {
                this.certifiedCopyField = value;
                this.RaisePropertyChanged("CertifiedCopy");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=6)]
        public AttachmentType Attachment {
            get {
                return this.attachmentField;
            }
            set {
                this.attachmentField = value;
                this.RaisePropertyChanged("Attachment");
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
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.18058")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.oscre.org/ns/eReg-Final/2012/RequestAttachmentV1_0")]
    public enum ApplicationTypeContent {
        
        /// <remarks/>
        AN1,
        
        /// <remarks/>
        AP1,
        
        /// <remarks/>
        APT,
        
        /// <remarks/>
        AS1,
        
        /// <remarks/>
        AS2,
        
        /// <remarks/>
        C,
        
        /// <remarks/>
        CAG,
        
        /// <remarks/>
        CCD,
        
        /// <remarks/>
        CH2,
        
        /// <remarks/>
        CH3,
        
        /// <remarks/>
        CN,
        
        /// <remarks/>
        CNL,
        
        /// <remarks/>
        COA,
        
        /// <remarks/>
        DIS,
        
        /// <remarks/>
        DJP,
        
        /// <remarks/>
        DSP,
        
        /// <remarks/>
        EX1,
        
        /// <remarks/>
        EX3,
        
        /// <remarks/>
        HR1,
        
        /// <remarks/>
        NOE,
        
        /// <remarks/>
        NOL,
        
        /// <remarks/>
        PC,
        
        /// <remarks/>
        RC,
        
        /// <remarks/>
        RFN,
        
        /// <remarks/>
        ROCA,
        
        /// <remarks/>
        ROCC,
        
        /// <remarks/>
        ROCU,
        
        /// <remarks/>
        ROE,
        
        /// <remarks/>
        RX1,
        
        /// <remarks/>
        RX2,
        
        /// <remarks/>
        RX3,
        
        /// <remarks/>
        RX4,
        
        /// <remarks/>
        SBC,
        
        /// <remarks/>
        SEV,
        
        /// <remarks/>
        SL,
        
        /// <remarks/>
        TNV,
        
        /// <remarks/>
        TR1,
        
        /// <remarks/>
        TR2,
        
        /// <remarks/>
        TR4,
        
        /// <remarks/>
        TRM,
        
        /// <remarks/>
        TSC,
        
        /// <remarks/>
        UN1,
        
        /// <remarks/>
        UN2,
        
        /// <remarks/>
        UN3,
        
        /// <remarks/>
        UN4,
        
        /// <remarks/>
        UT1,
        
        /// <remarks/>
        VC,
        
        /// <remarks/>
        VLAN,
        
        /// <remarks/>
        VLAP,
        
        /// <remarks/>
        VLUN,
        
        /// <remarks/>
        VOCA,
        
        /// <remarks/>
        VOCU,
        
        /// <remarks/>
        VOE,
        
        /// <remarks/>
        VOEA,
        
        /// <remarks/>
        VOEU,
        
        /// <remarks/>
        WCT,
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.18058")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.oscre.org/ns/eReg-Final/2012/RequestAttachmentV1_0")]
    public enum DocumentNameContent {
        
        /// <remarks/>
        Abstract,
        
        /// <remarks/>
        Agreement,
        
        /// <remarks/>
        Assignment,
        
        /// <remarks/>
        Conveyance,
        
        /// <remarks/>
        Correspondence,
        
        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("Court Order")]
        CourtOrder,
        
        /// <remarks/>
        Deed,
        
        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("Form DI")]
        FormDI,
        
        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("Document List")]
        DocumentList,
        
        /// <remarks/>
        Evidence,
        
        /// <remarks/>
        EX1A,
        
        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("Identity Evidence")]
        IdentityEvidence,
        
        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("Identity Form")]
        IdentityForm,
        
        /// <remarks/>
        Indenture,
        
        /// <remarks/>
        Lease,
        
        /// <remarks/>
        Licence,
        
        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("LR Correspondence")]
        LRCorrespondence,
        
        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("Power of Attorney")]
        PowerofAttorney,
        
        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("Stamp Duty Land Tax")]
        StampDutyLandTax,
        
        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("Statement Of Truth")]
        StatementOfTruth,
        
        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("Statutory Declaration")]
        StatutoryDeclaration,
        
        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("Witness Statement")]
        WitnessStatement,
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.18058")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.oscre.org/ns/eReg-Final/2012/RequestAttachmentV1_0")]
    public enum CertifiedTypeContent {
        
        /// <remarks/>
        Original,
        
        /// <remarks/>
        Certified,
        
        /// <remarks/>
        Scanned,
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.18058")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.oscre.org/ns/eReg-Final/2012/RequestAttachmentV1_0")]
    public partial class AttachmentType : object, System.ComponentModel.INotifyPropertyChanged {
        
        private string filenameField;
        
        private string formatField;
        
        private byte[] valueField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(Form=System.Xml.Schema.XmlSchemaForm.Qualified)]
        public string filename {
            get {
                return this.filenameField;
            }
            set {
                this.filenameField = value;
                this.RaisePropertyChanged("filename");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(Form=System.Xml.Schema.XmlSchemaForm.Qualified)]
        public string format {
            get {
                return this.formatField;
            }
            set {
                this.formatField = value;
                this.RaisePropertyChanged("format");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlTextAttribute(DataType="base64Binary")]
        public byte[] Value {
            get {
                return this.valueField;
            }
            set {
                this.valueField = value;
                this.RaisePropertyChanged("Value");
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
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.18058")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.oscre.org/ns/eReg-Final/2012/ResponseAttachmentV1_0")]
    public partial class ResultsType : object, System.ComponentModel.INotifyPropertyChanged {
        
        private string externalReferenceField;
        
        private string messageDetailsField;
        
        private string attachmentIDField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=0)]
        public string ExternalReference {
            get {
                return this.externalReferenceField;
            }
            set {
                this.externalReferenceField = value;
                this.RaisePropertyChanged("ExternalReference");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=1)]
        public string MessageDetails {
            get {
                return this.messageDetailsField;
            }
            set {
                this.messageDetailsField = value;
                this.RaisePropertyChanged("MessageDetails");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType="integer", Order=2)]
        public string AttachmentID {
            get {
                return this.attachmentIDField;
            }
            set {
                this.attachmentIDField = value;
                this.RaisePropertyChanged("AttachmentID");
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
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.18058")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.oscre.org/ns/eReg-Final/2012/ResponseAttachmentV1_0")]
    public partial class ValidationErrorsType : object, System.ComponentModel.INotifyPropertyChanged {
        
        private string codeField;
        
        private string descriptionField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=0)]
        public string Code {
            get {
                return this.codeField;
            }
            set {
                this.codeField = value;
                this.RaisePropertyChanged("Code");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=1)]
        public string Description {
            get {
                return this.descriptionField;
            }
            set {
                this.descriptionField = value;
                this.RaisePropertyChanged("Description");
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
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.18058")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.oscre.org/ns/eReg-Final/2012/ResponseAttachmentV1_0")]
    public partial class RejectionType : object, System.ComponentModel.INotifyPropertyChanged {
        
        private string externalReferenceField;
        
        private string reasonField;
        
        private string codeField;
        
        private string otherDescriptionField;
        
        private ValidationErrorsType[] validationErrorsField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=0)]
        public string ExternalReference {
            get {
                return this.externalReferenceField;
            }
            set {
                this.externalReferenceField = value;
                this.RaisePropertyChanged("ExternalReference");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=1)]
        public string Reason {
            get {
                return this.reasonField;
            }
            set {
                this.reasonField = value;
                this.RaisePropertyChanged("Reason");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=2)]
        public string Code {
            get {
                return this.codeField;
            }
            set {
                this.codeField = value;
                this.RaisePropertyChanged("Code");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=3)]
        public string OtherDescription {
            get {
                return this.otherDescriptionField;
            }
            set {
                this.otherDescriptionField = value;
                this.RaisePropertyChanged("OtherDescription");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("ValidationErrors", Order=4)]
        public ValidationErrorsType[] ValidationErrors {
            get {
                return this.validationErrorsField;
            }
            set {
                this.validationErrorsField = value;
                this.RaisePropertyChanged("ValidationErrors");
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
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.18058")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.oscre.org/ns/eReg-Final/2012/ResponseAttachmentV1_0")]
    public partial class AcknowledgementType : object, System.ComponentModel.INotifyPropertyChanged {
        
        private string uniqueIDField;
        
        private string messageDescriptionField;
        
        private System.DateTime expectedResponseDateTimeField;
        
        private bool expectedResponseDateTimeFieldSpecified;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=0)]
        public string UniqueID {
            get {
                return this.uniqueIDField;
            }
            set {
                this.uniqueIDField = value;
                this.RaisePropertyChanged("UniqueID");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=1)]
        public string MessageDescription {
            get {
                return this.messageDescriptionField;
            }
            set {
                this.messageDescriptionField = value;
                this.RaisePropertyChanged("MessageDescription");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=2)]
        public System.DateTime ExpectedResponseDateTime {
            get {
                return this.expectedResponseDateTimeField;
            }
            set {
                this.expectedResponseDateTimeField = value;
                this.RaisePropertyChanged("ExpectedResponseDateTime");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool ExpectedResponseDateTimeSpecified {
            get {
                return this.expectedResponseDateTimeFieldSpecified;
            }
            set {
                this.expectedResponseDateTimeFieldSpecified = value;
                this.RaisePropertyChanged("ExpectedResponseDateTimeSpecified");
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
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.18058")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.oscre.org/ns/eReg-Final/2012/ResponseAttachmentV1_0")]
    public partial class GatewayResponseType : object, System.ComponentModel.INotifyPropertyChanged {
        
        private ProductResponseCodeContentType typeCodeField;
        
        private AcknowledgementType acknowledgementField;
        
        private RejectionType rejectionField;
        
        private ResultsType resultsField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=0)]
        public ProductResponseCodeContentType TypeCode {
            get {
                return this.typeCodeField;
            }
            set {
                this.typeCodeField = value;
                this.RaisePropertyChanged("TypeCode");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=1)]
        public AcknowledgementType Acknowledgement {
            get {
                return this.acknowledgementField;
            }
            set {
                this.acknowledgementField = value;
                this.RaisePropertyChanged("Acknowledgement");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=2)]
        public RejectionType Rejection {
            get {
                return this.rejectionField;
            }
            set {
                this.rejectionField = value;
                this.RaisePropertyChanged("Rejection");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=3)]
        public ResultsType Results {
            get {
                return this.resultsField;
            }
            set {
                this.resultsField = value;
                this.RaisePropertyChanged("Results");
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
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.18058")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.oscre.org/ns/eReg-Final/2012/ResponseAttachmentV1_0")]
    public enum ProductResponseCodeContentType {
        
        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("0")]
        Item0,
        
        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("10")]
        Item10,
        
        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("20")]
        Item20,
        
        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("30")]
        Item30,
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.18058")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.oscre.org/ns/eReg-Final/2012/ResponseAttachmentV1_0")]
    public partial class AttachmentResponseV1_0Type : object, System.ComponentModel.INotifyPropertyChanged {
        
        private GatewayResponseType gatewayResponseField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=0)]
        public GatewayResponseType GatewayResponse {
            get {
                return this.gatewayResponseField;
            }
            set {
                this.gatewayResponseField = value;
                this.RaisePropertyChanged("GatewayResponse");
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
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="newAttachment", WrapperNamespace="http://attachmentv1_0.ws.bg.lr.gov/", IsWrapped=true)]
    public partial class newAttachmentRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://attachmentv1_0.ws.bg.lr.gov/", Order=0)]
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public LFM.Submissions.AgentServices.EdrsAttachmentService.AttachmentV1_0Type arg0;
        
        public newAttachmentRequest() {
        }
        
        public newAttachmentRequest(LFM.Submissions.AgentServices.EdrsAttachmentService.AttachmentV1_0Type arg0) {
            this.arg0 = arg0;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="newAttachmentResponse", WrapperNamespace="http://attachmentv1_0.ws.bg.lr.gov/", IsWrapped=true)]
    public partial class newAttachmentResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://attachmentv1_0.ws.bg.lr.gov/", Order=0)]
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public LFM.Submissions.AgentServices.EdrsAttachmentService.AttachmentResponseV1_0Type @return;
        
        public newAttachmentResponse() {
        }
        
        public newAttachmentResponse(LFM.Submissions.AgentServices.EdrsAttachmentService.AttachmentResponseV1_0Type @return) {
            this.@return = @return;
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface AttachmentV1_0ServiceChannel : LFM.Submissions.AgentServices.EdrsAttachmentService.AttachmentV1_0Service, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class AttachmentV1_0ServiceClient : System.ServiceModel.ClientBase<LFM.Submissions.AgentServices.EdrsAttachmentService.AttachmentV1_0Service>, LFM.Submissions.AgentServices.EdrsAttachmentService.AttachmentV1_0Service {
        
        public AttachmentV1_0ServiceClient() {
        }
        
        public AttachmentV1_0ServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public AttachmentV1_0ServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public AttachmentV1_0ServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public AttachmentV1_0ServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        LFM.Submissions.AgentServices.EdrsAttachmentService.newAttachmentResponse LFM.Submissions.AgentServices.EdrsAttachmentService.AttachmentV1_0Service.newAttachment(LFM.Submissions.AgentServices.EdrsAttachmentService.newAttachmentRequest request) {
            return base.Channel.newAttachment(request);
        }
        
        public LFM.Submissions.AgentServices.EdrsAttachmentService.AttachmentResponseV1_0Type newAttachment(LFM.Submissions.AgentServices.EdrsAttachmentService.AttachmentV1_0Type arg0) {
            LFM.Submissions.AgentServices.EdrsAttachmentService.newAttachmentRequest inValue = new LFM.Submissions.AgentServices.EdrsAttachmentService.newAttachmentRequest();
            inValue.arg0 = arg0;
            LFM.Submissions.AgentServices.EdrsAttachmentService.newAttachmentResponse retVal = ((LFM.Submissions.AgentServices.EdrsAttachmentService.AttachmentV1_0Service)(this)).newAttachment(inValue);
            return retVal.@return;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<LFM.Submissions.AgentServices.EdrsAttachmentService.newAttachmentResponse> LFM.Submissions.AgentServices.EdrsAttachmentService.AttachmentV1_0Service.newAttachmentAsync(LFM.Submissions.AgentServices.EdrsAttachmentService.newAttachmentRequest request) {
            return base.Channel.newAttachmentAsync(request);
        }
        
        public System.Threading.Tasks.Task<LFM.Submissions.AgentServices.EdrsAttachmentService.newAttachmentResponse> newAttachmentAsync(LFM.Submissions.AgentServices.EdrsAttachmentService.AttachmentV1_0Type arg0) {
            LFM.Submissions.AgentServices.EdrsAttachmentService.newAttachmentRequest inValue = new LFM.Submissions.AgentServices.EdrsAttachmentService.newAttachmentRequest();
            inValue.arg0 = arg0;
            return ((LFM.Submissions.AgentServices.EdrsAttachmentService.AttachmentV1_0Service)(this)).newAttachmentAsync(inValue);
        }
    }
}
