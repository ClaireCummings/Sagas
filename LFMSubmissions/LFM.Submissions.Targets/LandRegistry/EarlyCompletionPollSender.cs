using LFM.Submissions.GovGateway.EarlyCompletionService;

namespace LFM.Submissions.GovGateway.LandRegistry
{
    public class EarlyCompletionPollSender
    {
        public string MessageId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public EarlyCompletionService.ResponseEarlyCompletionV1_0Type Poll()
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
            var response = client.getResponse(request);

            return response;
        }
    }
}
