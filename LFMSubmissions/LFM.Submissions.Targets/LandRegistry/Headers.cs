using System;
using System.Xml;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using System.ServiceModel.Channels;
using System.ServiceModel;
using System.Text;

namespace LFM.Submissions.Targets.LandRegistry
{
    public class HMLRBGMessageInspector : IClientMessageInspector
    {
        #region Member Variables
        private string m_Username = "";
        private string m_Password = "";

        #endregion

        #region Constructor

        public HMLRBGMessageInspector(string username, string password)
        {
            m_Username = username;
            m_Password = password;
        }

        #endregion

        #region IClientMessageInspector Methods

        public void AfterReceiveReply(ref Message reply, object correlationState)
        {
        }

        public object BeforeSendRequest(ref Message request, IClientChannel channel)
        {
            // Create the WsseSecurityHeader with the current user credetials.
            MessageHeader wsseHeader = CreateWsseSecurityHeader();
            request.Headers.Add(wsseHeader);
            // Create the i18nHeader with the locale settings.
            MessageHeader i18nHeader = CreateI18nHeader();
            request.Headers.Add(i18nHeader);
            request.Headers.Action = null;
            return null;
        }

        #endregion

        #region Private Methods

        private MessageHeader CreateWsseSecurityHeader()
        {
            XmlDocument doc = new XmlDocument();
            StringBuilder xml = new StringBuilder();
            xml.Append("<UsernameToken xmlns=\"http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd\">");
            xml.Append("<Username>");
            xml.Append(m_Username);
            xml.Append("</Username>");
            xml.Append("<Password>");
            xml.Append(m_Password);
            xml.Append("</Password>");
            xml.Append("</UsernameToken>");
            doc.LoadXml(xml.ToString());
            return MessageHeader.CreateHeader("Security", "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd", doc.DocumentElement);
        }

        private MessageHeader CreateI18nHeader()
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml("<locale xmlns=\"http://www.w3.org/2005/09/ws-i18n\">en</locale>");
            return MessageHeader.CreateHeader("international", "http://www.w3.org/2005/09/ws-i18n", doc.DocumentElement);
        }

        #endregion
    }

    public class HMLRBGMessageEndpointBehavior : Attribute, IEndpointBehavior, IOperationBehavior, IContractBehavior
    {
        #region Member Variables

        private string m_Username = "";
        private string m_Password = "";

        #endregion

        #region Constructor

        public HMLRBGMessageEndpointBehavior(string username, string password)
        {
            m_Username = username;
            m_Password = password;
        }

        #endregion

        #region IEndpointBehavior Methods

        void IEndpointBehavior.AddBindingParameters(ServiceEndpoint endpoint, BindingParameterCollection bindingParameters)
        {
        }

        void IEndpointBehavior.ApplyClientBehavior(ServiceEndpoint endpoint, System.ServiceModel.Dispatcher.ClientRuntime clientRuntime)
        {
            clientRuntime.MessageInspectors.Add(new HMLRBGMessageInspector(m_Username, m_Password));
        }
        void IEndpointBehavior.ApplyDispatchBehavior(ServiceEndpoint endpoint, System.ServiceModel.Dispatcher.EndpointDispatcher endpointDispatcher)
        {
        }
        void IEndpointBehavior.Validate(ServiceEndpoint endpoint)
        {
        }

        #endregion

        #region IOperationBehavior Members

        void IOperationBehavior.AddBindingParameters(OperationDescription operationDescription, BindingParameterCollection bindingParameters)
        {
        }

        void IOperationBehavior.ApplyClientBehavior(OperationDescription operationDescription, ClientOperation clientOperation)
        {
            clientOperation.Parent.MessageInspectors.Add(new HMLRBGMessageInspector(m_Username, m_Password));
        }

        void IOperationBehavior.ApplyDispatchBehavior(OperationDescription operationDescription, DispatchOperation dispatchOperation)
        {
            //throw new NotImplementedException();
        }

        void IOperationBehavior.Validate(OperationDescription operationDescription)
        {
            // throw new NotImplementedException();
        }

        #endregion

        #region IContractBehavior Members

        void IContractBehavior.AddBindingParameters(ContractDescription contractDescription, ServiceEndpoint endpoint, BindingParameterCollection bindingParameters)
        {
        }

        void IContractBehavior.ApplyClientBehavior(ContractDescription contractDescription, ServiceEndpoint endpoint, ClientRuntime clientRuntime)
        {
            clientRuntime.MessageInspectors.Add(new HMLRBGMessageInspector(m_Username, m_Password));
        }

        void IContractBehavior.ApplyDispatchBehavior(ContractDescription contractDescription, ServiceEndpoint endpoint, DispatchRuntime dispatchRuntime)
        {
            //throw new NotImplementedException();
        }
        void IContractBehavior.Validate(ContractDescription contractDescription, ServiceEndpoint endpoint)
        {
            //throw new NotImplementedException();
        }

        #endregion
    }

    public class AcceptAllCertificatePolicy : ICertificatePolicy
    {
        public AcceptAllCertificatePolicy()
        {
        }

        public bool CheckValidationResult(ServicePoint sPoint, X509Certificate cert, WebRequest wRequest, int certProb)
        {
            // *** Always accept
            return true;
        }
    }
}
