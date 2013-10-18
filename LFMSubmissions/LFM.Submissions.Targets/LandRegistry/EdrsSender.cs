using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using LFM.Submissions.Targets.EdrsPollRequestService;
using LFM.Submissions.Targets.EdrsSubmissionService;

namespace LFM.Submissions.Targets.LandRegistry
{
    public enum EdrsResponse
    {
        None,
        Acknowledgement,
        Results,
        Rejection,
        Other
    }

    public class EdrsSender : IEdrsSender
    {
        public string ApplicationId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Payload { get; set; }

        public string Submit()
        {
            RequestApplicationToChangeRegisterV1_0Type request;
            try
            {
                 request =
                    ObjectSerializer.XmlDeserializeFromString<RequestApplicationToChangeRegisterV1_0Type>(Payload);
            }
            catch 
            {
                return null;
            }
            
            request.MessageId = ApplicationId;

            // create an instance of the client
            var client = new EDocumentRegistrationV1_0ServiceClient();
            
            // create a Header Instance
            client.ChannelFactory.Endpoint.Behaviors.Add(new HMLRBGMessageEndpointBehavior(Username, Password));

            // submit the request
            var response = client.eDocumentRegistration(request);

            return response.XmlSerializeToString();

        }
    }
}
