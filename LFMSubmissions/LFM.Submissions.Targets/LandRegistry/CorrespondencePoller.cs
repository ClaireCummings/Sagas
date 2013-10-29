using System;
using LFM.Submissions.AgentServices.CorrespondenceService;
using LFM.Submissions.AgentComms.LandRegistry;
using LFM.Submissions.Contract.LandRegistry;

namespace LFM.Submissions.AgentServices.LandRegistry
{
    public class CorrespondencePoller : IEdrsPoller<CorrespondenceReceived>
    {
        private CorrespondenceV1_0Type _serviceResponse;

        public string MessageId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public CorrespondenceReceived Response 
        {
            get 
            { 
                if(_serviceResponse == null)
                    throw new InvalidOperationException("No resonse from the webservice");
                return GetCorrespondencePollResponse();
            }
        }

        public bool Poll()
        {
            var request = new CorrespondenceService.PollRequestType{
                ID = new CorrespondenceService.Q1IdentifierType() { MessageID = new MessageIDTextType() { Value = MessageId } },
            };

            // create an instance of the client
            var client = new CorrespondenceService.CorrespondenceV1_0PollRequestServiceClient();
            // create a Header Instance
            client.ChannelFactory.Endpoint.Behaviors.Add(new HMLRBGMessageEndpointBehavior(Username, Password));

            // submit the request
            _serviceResponse = client.getResponse(request);

            return true;
        }

        public CorrespondenceReceived GetCorrespondencePollResponse()
        {
            if (_serviceResponse.GatewayResponse.TypeCode != CorrespondenceService.ProductResponseCodeContentType.Item31)
                return null;

            return new CorrespondenceReceived
            {
                ApplicationMessageId = _serviceResponse.GatewayResponse.ApplicationMessageId,
                ExternalReference = _serviceResponse.GatewayResponse.ExternalReference,
                ABR = _serviceResponse.GatewayResponse.ABR,
                Filename = _serviceResponse.GatewayResponse.Correspondence.filename,
                FileFormat = _serviceResponse.GatewayResponse.Correspondence.format,
                FileData = _serviceResponse.GatewayResponse.Correspondence.Value
            };
        }
    }
}
