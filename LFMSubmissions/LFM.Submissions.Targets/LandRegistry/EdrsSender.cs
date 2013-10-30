using System;
using System.Security.Cryptography.X509Certificates;
using LFM.Submissions.AgentComms.LandRegistry;
using LFM.Submissions.InternalMessages.LandRegistry.Messages;

namespace LFM.Submissions.AgentServices.LandRegistry
{
    public class EdrsSender : IEdrsSender
    {
        private EdrsSubmissionService.ResponseApplicationToChangeRegisterV1_0Type _serviceResponse;
        
        public string MessageId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Payload { get; set; }
        
        public IEdrsResponseReceived Response
        {
            get
            {
                if (_serviceResponse == null)
                    throw new InvalidOperationException("No response from the web service");
                
                return GetEdrsResponse(_serviceResponse);
            }
        }
        
        public bool Submit()
        {
            try
            {
                EdrsSubmissionService.RequestApplicationToChangeRegisterV1_0Type request;

                request =
                    ObjectSerializer
                        .XmlDeserializeFromString<EdrsSubmissionService.RequestApplicationToChangeRegisterV1_0Type>(
                            Payload);

                request.MessageId = MessageId;

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
    }
}
