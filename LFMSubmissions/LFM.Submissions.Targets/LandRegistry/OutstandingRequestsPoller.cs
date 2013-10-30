using System.Collections.Generic;
using LFM.Submissions.AgentServices.OutstandingRequestsService;
using LFM.Submissions.AgentComms.LandRegistry;
using LFM.Submissions.InternalMessages.LandRegistry.Commands;

namespace LFM.Submissions.AgentServices.LandRegistry
{
    public class OutstandingRequestsPoller : IEdrsPoller<List<IOutstandingRequest>> // IOutstandingRequestsPoller
    {
        private ResponseOutstandingRequestsType _serviceResponse;

        public string MessageId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public List<IOutstandingRequest> Response {
            get
            {
                return _serviceResponse != null ? GetOutstandingRequests() : null;
            }
        }

        /// <summary>
        /// TODO: Design consideration -- need to find out exactly what a New response is.  
        /// If it is defined as items not already picked up from this (OutstandingRequests) service then we are in trouble if the responses get lost on 
        /// the way back to us then how do we recover????
        /// Hopefully New Responses are those which we have already requested from the individual poll services in which case we are OK.
        /// </summary>
        public bool Poll()
        {
            var request = new OutstandingRequestsService.RequestOutstandingRequestsType
            {
                ID = new Q1IdentifierType() { MessageID = new Q1TextType() { Value = MessageId } },
                Product = new Q1OutstandingRequestsProductType { ShowOnlyNewResponses = new IndicatorType { Value = true }, SpecificServiceSpecified = false }
            };

            // create an instance of the client
            var client = new OutstandingRequestsService.OutstandingRequestsV2_0ServiceClient();
            // create a Header Instance
            client.ChannelFactory.Endpoint.Behaviors.Add(new HMLRBGMessageEndpointBehavior(Username, Password));

            // submit the request
            _serviceResponse = client.getOutstandingRequests(request);

            return true;
        }

        private List<IOutstandingRequest> GetOutstandingRequests()
        {
            var outstandingRequests = new List<IOutstandingRequest>();

            foreach (var outstandingRequest in _serviceResponse.Results.OutstandingRequests)
            {
                var messageId = outstandingRequest.ID.MessageID;
                switch (outstandingRequest.ServiceType)
                {
                    case 87:
                        outstandingRequests.Add(new PollEdrs { ApplicationId = messageId });
                        break;
                    case 88:
                        outstandingRequests.Add(new PollEdrsAttachment { AttachmentId = messageId });
                        break;
                    case 89:
                        outstandingRequests.Add(new PollCorrespondence { MessageId = messageId });
                        break;
                    case 90:
                        outstandingRequests.Add(new PollEarlyCompletion { MessageId = messageId });
                        break;
                }
            }

            return outstandingRequests;
        }
    }
}
