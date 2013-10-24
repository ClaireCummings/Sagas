using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LFM.Submissions.GovGateway.OutstandingRequestsService;
using LFM.Submissions.InternalMessages.LandRegistry.Commands;
using LFM.Submissions.InternalMessages.LandRegistry.Messages;
using NServiceBus;

namespace LFM.Submissions.GovGateway.LandRegistry
{
    public static class OutstandingRequestsResponseAnalyser
    {
        public static bool WasRejected(ResponseOutstandingRequestsType response)
        {
            return response.TypeCode.Value == ProductResponseCodeContentType.Item20;
        }

        public static List<IOutstandingRequest> GetOutstandingRequests(ResponseOutstandingRequestsType response)
        {
            var outstandingRequests = new List<IOutstandingRequest>();

            foreach (var outstandingRequest in response.Results.OutstandingRequests)
            {
                var messageId = outstandingRequest.ID.MessageID;
                switch (outstandingRequest.ServiceType)
                {
                    case 87:
                       outstandingRequests.Add(new PollEdrs {ApplicationId = messageId});
                        break;
                    case 88:
                       outstandingRequests.Add(new PollEdrsAttachment {AttachmentId = messageId});
                        break;
                    case 89:
                        outstandingRequests.Add(new PollCorrespondence {MessageId = messageId});
                        break;
                    case 90:
                        outstandingRequests.Add(new PollEarlyCompletion {MessageId = messageId});
                        break;
                }
            }

            return outstandingRequests;
        }
    }
}
