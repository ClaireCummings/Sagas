using LFM.Submissions.GovGateway.EdrsAttachmentPollService;
using LFM.Submissions.GovGateway.EdrsSubmissionService;

namespace LFM.Submissions.GovGateway.LandRegistry
{
    public class EdrsAttachmentSender
    {
        public string ApplicationId { get; set; }
        public string AttachmentId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Payload { get; set; }

        public EdrsAttachmentService.AttachmentResponseV1_0Type Submit()
        {
            EdrsAttachmentService.newAttachmentRequest request;
            try
            {
                request =
                   ObjectSerializer.XmlDeserializeFromString<EdrsAttachmentService.newAttachmentRequest>(Payload);
            }
            catch
            {
                return null;
            }

            request.arg0.ApplicationMessageId = ApplicationId;
            request.arg0.MessageId = AttachmentId;

            // create an instance of the client
            var client = new EdrsAttachmentService.AttachmentV1_0ServiceClient();

            // create a Header Instance
            client.ChannelFactory.Endpoint.Behaviors.Add(new HMLRBGMessageEndpointBehavior(Username, Password));

            // submit the request
            var response = client.newAttachment(request.arg0);

            return response;
        }

        public EdrsAttachmentPollService.AttachmentResponseV1_0Type Poll()
        {
            var request = new EdrsAttachmentPollService.PollRequestType();

            request.ID = new EdrsAttachmentPollService.Q1IdentifierType();
            request.ID.MessageID = new MessageIDTextType();
            request.ID.MessageID.Value = AttachmentId;

            // create an instance of the client
            var client = new EdrsAttachmentPollService.AttachmentV1_0PollRequestServiceClient();

            // create a Header Instance
            client.ChannelFactory.Endpoint.Behaviors.Add(new HMLRBGMessageEndpointBehavior(Username, Password));

            // submit the request
            var response = client.getResponse(request);

            return response;
        }
    }
}
