using LFM.Submissions.InternalMessages.LandRegistry.Messages;

namespace LFM.Submissions.GovGateway.LandRegistry
{
    public static class EdrsResponseAnalyser
    {
        public static IEdrsResponseReceived GetEdrsResponse(EdrsSubmissionService.ResponseApplicationToChangeRegisterV1_0Type response)
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

        public static IEdrsResponseReceived GetEdrsResponse(EdrsPollRequestService.ResponseApplicationToChangeRegisterV1_0Type response)
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
    }
}
