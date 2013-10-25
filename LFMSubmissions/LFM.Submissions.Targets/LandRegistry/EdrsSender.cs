using System.Security.Cryptography.X509Certificates;

namespace LFM.Submissions.GovGateway.LandRegistry
{
    public class EdrsSender : IEdrsSender
    {
        public string ApplicationId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Payload { get; set; }

        public EdrsSubmissionService.ResponseApplicationToChangeRegisterV1_0Type Submit()
        {
            EdrsSubmissionService.RequestApplicationToChangeRegisterV1_0Type request;

            request =
                ObjectSerializer.XmlDeserializeFromString<EdrsSubmissionService.RequestApplicationToChangeRegisterV1_0Type>(Payload);
            
            request.MessageId = ApplicationId;

            // create an instance of the client
            var client = new EdrsSubmissionService.EDocumentRegistrationV1_0ServiceClient();
            
            // create a Header Instance
            client.ChannelFactory.Endpoint.Behaviors.Add(new HMLRBGMessageEndpointBehavior(Username, Password));

            // submit the request
            var response = client.eDocumentRegistration(request);

            return response;

        }

        public EdrsPollRequestService.ResponseApplicationToChangeRegisterV1_0Type Poll()
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
            var response = client.getResponse(request);

            return response;
        }
    }
}
