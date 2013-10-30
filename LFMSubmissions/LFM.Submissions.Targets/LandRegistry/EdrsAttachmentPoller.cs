using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LFM.Submissions.AgentComms.LandRegistry;
using LFM.Submissions.InternalMessages.LandRegistry.Messages;

namespace LFM.Submissions.AgentServices.LandRegistry
{
    public class EdrsAttachmentPoller : IEdrsPoller<IEdrsAttachmentResponseReceived>
    {
        private EdrsAttachmentPollService.AttachmentResponseV1_0Type _serviceResponse;

        public string MessageId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public IEdrsAttachmentResponseReceived Response
        {
            get
            {
                if (_serviceResponse == null)
                    throw new InvalidOperationException("No response from the webservice");
                return GetEdrsResponse();
            }
        }

        public bool Submit()
        {
            var request = new EdrsAttachmentPollService.PollRequestType
            {
                ID =
                    new EdrsAttachmentPollService.Q1IdentifierType
                    {
                        MessageID = new EdrsAttachmentPollService.MessageIDTextType { Value = MessageId }
                    }
            };

            // create an instance of the client
            var client = new EdrsAttachmentPollService.AttachmentV1_0PollRequestServiceClient();

            // create a Header Instance
            client.ChannelFactory.Endpoint.Behaviors.Add(new HMLRBGMessageEndpointBehavior(Username, Password));

            // submit the request
            _serviceResponse = client.getResponse(request);

            return true;
        }

        private IEdrsAttachmentResponseReceived GetEdrsResponse()
        {
            switch (_serviceResponse.GatewayResponse.TypeCode)
            {
                case EdrsAttachmentPollService.ProductResponseCodeContentType.Item10:
                    return new EdrsAttachmentAcknowledgementReceived
                    {
                    };

                case EdrsAttachmentPollService.ProductResponseCodeContentType.Item20:
                    return new EdrsAttachmentRejectionReceived
                    {
                        RejectionReason = _serviceResponse.GatewayResponse.Rejection.Reason
                    };

                case EdrsAttachmentPollService.ProductResponseCodeContentType.Item30:
                    return new EdrsAttachmentResultsReceived
                    {
                        Results = _serviceResponse.GatewayResponse.Results.MessageDetails
                    };

                default:
                    return new EdrsAttachmentOtherReceived { };
            }
        }
    }
}
