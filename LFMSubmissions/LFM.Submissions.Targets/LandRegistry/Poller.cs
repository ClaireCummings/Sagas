using System;
using LFM.Submissions.AgentComms.LandRegistry;
using LFM.Submissions.AgentServices.EarlyCompletionService;
using LFM.Submissions.Contract.LandRegistry;

namespace LFM.Submissions.AgentServices.LandRegistry
{
    public class Poller : IEdrsPoller<EarlyCompletionReceived>
    {
        private EarlyCompletionService.ResponseEarlyCompletionV1_0Type _serviceResponse;

        public string MessageId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public virtual EarlyCompletionReceived Response
        {
            get
            {
                if (_serviceResponse == null)
                    throw new InvalidOperationException("No response from the webservice");

                return new EarlyCompletionReceived
                    {
                        ApplicationMessageId = _serviceResponse.GatewayResponse.EarlyCompletion.ApplicationMessageId,
                        ExternalReference = _serviceResponse.GatewayResponse.EarlyCompletion.ExternalReference,
                        ABR = _serviceResponse.GatewayResponse.EarlyCompletion.ABR,
                        Filename = _serviceResponse.GatewayResponse.EarlyCompletion.DespatchDocument.filename,
                        FileFormat = _serviceResponse.GatewayResponse.EarlyCompletion.DespatchDocument.format,
                        DespatchDocument = _serviceResponse.GatewayResponse.EarlyCompletion.DespatchDocument.Value
                    };
            }
        }

        public virtual bool Poll()
        {
            var request = new EarlyCompletionService.PollRequestType
            {
                ID = new EarlyCompletionService.Q1IdentifierType() { MessageID = new MessageIDTextType() { Value = MessageId } },
            };

            // create an instance of the client
            var client = new EarlyCompletionService.EarlyCompletionV1_0PollRequestServiceClient();
            // create a Header Instance
            client.ChannelFactory.Endpoint.Behaviors.Add(new HMLRBGMessageEndpointBehavior(Username, Password));

            // submit the request
            _serviceResponse = client.getResponse(request);

            return true;
        }
    }
}
