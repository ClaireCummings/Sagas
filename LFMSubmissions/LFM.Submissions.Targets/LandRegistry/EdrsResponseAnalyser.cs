using LFM.Submissions.InternalMessages.LandRegistry.Messages;

namespace LFM.Submissions.AgentServices.LandRegistry
{
    public class EdrsResponseAnalyser : IEdrsResponseAnalyser
    {
        public IEdrsResponseReceived GetEdrsResponse(EdrsSubmissionService.ResponseApplicationToChangeRegisterV1_0Type response)
        {
            switch (response.GatewayResponse.TypeCode)
            {
                case EdrsSubmissionService.ProductResponseCodeContentType.Item10:
                    return new EdrsAcknowledgementReceived
                    {
                    };

                case EdrsSubmissionService.ProductResponseCodeContentType.Item20:
                    return new EdrsRejectionReceived
                    {
                        RejectionReason = response.GatewayResponse.Rejection.RejectionResponse.Reason
                    };

                case EdrsSubmissionService.ProductResponseCodeContentType.Item30:
                    return new EdrsResultsReceived
                    {
                        Results = response.GatewayResponse.Results.MessageDetails
                    };

                default:
                    return new EdrsOtherReceived
                    {};
            }
        }

        public IEdrsResponseReceived GetEdrsResponse(EdrsPollRequestService.ResponseApplicationToChangeRegisterV1_0Type response)
        {
            switch (response.GatewayResponse.TypeCode)
            {
                case EdrsPollRequestService.ProductResponseCodeContentType.Item10:
                    return new EdrsAcknowledgementReceived
                    {
                    };

                case EdrsPollRequestService.ProductResponseCodeContentType.Item20:
                    return new EdrsRejectionReceived
                    {
                        RejectionReason = response.GatewayResponse.Rejection.RejectionResponse.Reason
                    };

                case EdrsPollRequestService.ProductResponseCodeContentType.Item30:
                    return new EdrsResultsReceived
                    {
                        Results = response.GatewayResponse.Results.MessageDetails
                    };

                default:
                    return new EdrsOtherReceived { };
            }
        }

        public IEdrsAttachmentResponseReceived GetEdrsResponse(EdrsAttachmentService.AttachmentResponseV1_0Type response)
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
                    return new EdrsAttachmentOtherReceived { };
            }
        }

        public IEdrsAttachmentResponseReceived GetEdrsResponse(EdrsAttachmentPollService.AttachmentResponseV1_0Type response)
        {
            switch (response.GatewayResponse.TypeCode)
            {
                case EdrsAttachmentPollService.ProductResponseCodeContentType.Item10:
                    return new EdrsAttachmentAcknowledgementReceived
                    {
                    };

                case EdrsAttachmentPollService.ProductResponseCodeContentType.Item20:
                    return new EdrsAttachmentRejectionReceived
                    {
                        RejectionReason = response.GatewayResponse.Rejection.Reason
                    };

                case EdrsAttachmentPollService.ProductResponseCodeContentType.Item30:
                    return new EdrsAttachmentResultsReceived
                    {
                        Results = response.GatewayResponse.Results.MessageDetails
                    };

                default:
                    return new EdrsAttachmentOtherReceived { };
            }
        }
    }
}
