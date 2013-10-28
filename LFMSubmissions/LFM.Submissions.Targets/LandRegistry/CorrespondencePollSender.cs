using LFM.Submissions.AgentServices.CorrespondenceService;

namespace LFM.Submissions.AgentServices.LandRegistry
{
    public class CorrespondencePollSender
    {
        public string MessageId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public CorrespondenceService.CorrespondenceV1_0Type Poll()
        {
            var request = new CorrespondenceService.PollRequestType{
                ID = new CorrespondenceService.Q1IdentifierType() { MessageID = new MessageIDTextType() { Value = MessageId } },
            };

            // create an instance of the client
            var client = new CorrespondenceService.CorrespondenceV1_0PollRequestServiceClient();
            // create a Header Instance
            client.ChannelFactory.Endpoint.Behaviors.Add(new HMLRBGMessageEndpointBehavior(Username, Password));

            // submit the request
            var response = client.getResponse(request);

            return response;
        }
    }
}
