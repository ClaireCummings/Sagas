using System;
using LFM.Submissions.AgentComms.LandRegistry;
using LFM.Submissions.InternalMessages.LandRegistry.Messages;

namespace LFM.Submissions.AgentServices.LandRegistry
{
    public class EdrsAttachmentSender : IEdrsAttachmentSender
    {
        public string ApplicationId { get; set; }
        public string AttachmentId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Payload { get; set; }

        public IEdrsAttachmentResponseReceived Response
        {
            get
            {
                

                if (_serviceResponse == null)
                    throw new InvalidOperationException("No response from web service");
                return GetEdrsResponse(_serviceResponse);
            }
        }

        private EdrsAttachmentService.AttachmentResponseV1_0Type _serviceResponse;
        private EdrsAttachmentPollService.AttachmentResponseV1_0Type _pollServiceResponse;

        public bool Submit()
        {
            EdrsAttachmentService.newAttachmentRequest request;
            try
            {
                request =
                    ObjectSerializer.XmlDeserializeFromString<EdrsAttachmentService.newAttachmentRequest>(Payload);
            }
            catch
            {
                return false;
            }

            request.arg0.ApplicationMessageId = ApplicationId;
            request.arg0.MessageId = AttachmentId;

            // create an instance of the client
            var client = new EdrsAttachmentService.AttachmentV1_0ServiceClient();

            // create a Header Instance
            client.ChannelFactory.Endpoint.Behaviors.Add(new HMLRBGMessageEndpointBehavior(Username, Password));

            // submit the request
            _serviceResponse = client.newAttachment(request.arg0);

            return true;
        }

        

        private IEdrsAttachmentResponseReceived GetEdrsResponse(
            EdrsAttachmentService.AttachmentResponseV1_0Type response)
        {
            switch (response.GatewayResponse.TypeCode)
            {
                case EdrsAttachmentService.ProductResponseCodeContentType.Item10:
                    return new EdrsAttachmentAcknowledgementReceived
                        {
                        };

                case EdrsAttachmentService.ProductResponseCodeContentType.Item20:
                    return new EdrsAttachmentRejectionReceived
                        {
                            RejectionReason = response.GatewayResponse.Rejection.Reason
                        };

                case EdrsAttachmentService.ProductResponseCodeContentType.Item30:
                    return new EdrsAttachmentResultsReceived
                        {
                            Results = response.GatewayResponse.Results.MessageDetails
                        };

                default:
                    return new EdrsAttachmentOtherReceived {};
            }
        }

       
    }
}
