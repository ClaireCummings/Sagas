using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using LFM.Submissions.AgentComms.LandRegistry;
using LFM.Submissions.InternalMessages.LandRegistry.Messages;

namespace LFM.Submissions.AgentServices.LandRegistry
{
    public class EdrsPoller : IEdrsPoller<IEdrsResponseReceived>
    {
        private EdrsPollRequestService.ResponseApplicationToChangeRegisterV1_0Type _serviceResponse;

        public string MessageId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public IEdrsResponseReceived Response 
        { 
            get
            {
                if (_serviceResponse == null)
                    throw new InvalidOperationException("No response from the web service");
                return GetEdrsResponse();
            } 
        }

        public bool Poll()
        {
            var request = new EdrsPollRequestService.PollRequestType
            {
                ID = new EdrsPollRequestService.Q1IdentifierType { MessageID = new EdrsPollRequestService.MessageIDTextType { Value = MessageId } }
            };

            // create an instance of the client
            var client = new EdrsPollRequestService.EDocumentRegistrationV1_0PollRequestServiceClient();

            // overwrite endpointBehavior attributes
            client.ChannelFactory.Credentials.ClientCertificate.SetCertificate(StoreLocation.CurrentUser, StoreName.My, X509FindType.FindBySerialNumber, "47 ce 29 6f");

            // create a Header Instance
            client.ChannelFactory.Endpoint.Behaviors.Add(new HMLRBGMessageEndpointBehavior(Username, Password));

            // submit the request
            _serviceResponse = client.getResponse(request);

            return true;
        }

        private IEdrsResponseReceived GetEdrsResponse()
        {
            switch (_serviceResponse.GatewayResponse.TypeCode)
            {
                case EdrsPollRequestService.ProductResponseCodeContentType.Item10:
                    return new EdrsAcknowledgementReceived
                    {
                    };

                case EdrsPollRequestService.ProductResponseCodeContentType.Item20:
                    return new EdrsRejectionReceived
                    {
                        RejectionReason = _serviceResponse.GatewayResponse.Rejection.RejectionResponse.Reason
                    };

                case EdrsPollRequestService.ProductResponseCodeContentType.Item30:
                    return new EdrsResultsReceived
                    {
                        Results = _serviceResponse.GatewayResponse.Results.MessageDetails
                    };

                default:
                    return new EdrsOtherReceived { };
            }
        }
    }
}
