using System;
using System.Security.Cryptography.X509Certificates;
using LFM.Submissions.AgentComms.LandRegistry;
using LFM.Submissions.InternalMessages.LandRegistry.Messages;

namespace LFM.Submissions.AgentServices.LandRegistry
{
    public class EdrsSender : IEdrsSender
    {
        public string ApplicationId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Payload { get; set; }
        public IEdrsResponseReceived Response
        {
            get
            {
                if (_pollServiceResponse != null)
                    return GetEdrsResponse(_pollServiceResponse);

                if (_serviceResponse == null)
                    throw new InvalidOperationException("No response from the web service");
                
                return GetEdrsResponse(_serviceResponse);
            }
        }

        private EdrsSubmissionService.ResponseApplicationToChangeRegisterV1_0Type _serviceResponse;
        private EdrsPollRequestService.ResponseApplicationToChangeRegisterV1_0Type _pollServiceResponse;

        public bool Submit()
        {
            try
            {
                EdrsSubmissionService.RequestApplicationToChangeRegisterV1_0Type request;

                request =
                    ObjectSerializer
                        .XmlDeserializeFromString<EdrsSubmissionService.RequestApplicationToChangeRegisterV1_0Type>(
                            Payload);

                request.MessageId = ApplicationId;

                // create an instance of the client
                var client = new EdrsSubmissionService.EDocumentRegistrationV1_0ServiceClient();

                // create a Header Instance
                client.ChannelFactory.Endpoint.Behaviors.Add(new HMLRBGMessageEndpointBehavior(Username, Password));

                // submit the request
                _serviceResponse = client.eDocumentRegistration(request);
            }
            catch (DeserializationException ex)
            {
                throw new InvalidPayloadException("Payload is invalid",ex);
            }
            return true;
        }

        public bool Poll()
        {
            var request = new EdrsPollRequestService.PollRequestType
            {
                ID = new EdrsPollRequestService.Q1IdentifierType { MessageID = new EdrsPollRequestService.MessageIDTextType { Value = ApplicationId } }
            };

            // create an instance of the client
            var client = new EdrsPollRequestService.EDocumentRegistrationV1_0PollRequestServiceClient();

            // overwrite endpointBehavior attributes
            client.ChannelFactory.Credentials.ClientCertificate.SetCertificate(StoreLocation.CurrentUser, StoreName.My, X509FindType.FindBySerialNumber, "47 ce 29 6f");

            // create a Header Instance
            client.ChannelFactory.Endpoint.Behaviors.Add(new HMLRBGMessageEndpointBehavior(Username, Password));

            // submit the request
            _pollServiceResponse = client.getResponse(request);

            return true;
        }

        private IEdrsResponseReceived GetEdrsResponse(EdrsSubmissionService.ResponseApplicationToChangeRegisterV1_0Type response)
        {
            switch (response.GatewayResponse.TypeCode)
            {
                case EdrsSubmissionService.ProductResponseCodeContentType.Item10:
                    return new EdrsAcknowledgementReceived
                    {
                    };

                case EdrsSubmissionService.ProductResponseCodeContentType.Item20:
                    return new EdrsRejectionReceived
                    {
                        RejectionReason = response.GatewayResponse.Rejection.RejectionResponse.Reason
                    };

                case EdrsSubmissionService.ProductResponseCodeContentType.Item30:
                    return new EdrsResultsReceived
                    {
                        Results = response.GatewayResponse.Results.MessageDetails
                    };

                default:
                    return new EdrsOtherReceived { };
            }
        }

        private IEdrsResponseReceived GetEdrsResponse(EdrsPollRequestService.ResponseApplicationToChangeRegisterV1_0Type response)
        {
            switch (response.GatewayResponse.TypeCode)
            {
                case EdrsPollRequestService.ProductResponseCodeContentType.Item10:
                    return new EdrsAcknowledgementReceived
                    {
                    };

                case EdrsPollRequestService.ProductResponseCodeContentType.Item20:
                    return new EdrsRejectionReceived
                    {
                        RejectionReason = response.GatewayResponse.Rejection.RejectionResponse.Reason
                    };

                case EdrsPollRequestService.ProductResponseCodeContentType.Item30:
                    return new EdrsResultsReceived
                    {
                        Results = response.GatewayResponse.Results.MessageDetails
                    };

                default:
                    return new EdrsOtherReceived { };
            }
        }
    }
}
