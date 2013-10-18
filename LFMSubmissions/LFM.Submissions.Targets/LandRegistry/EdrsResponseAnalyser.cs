using LFM.Submissions.LandRegistry;

namespace LFM.Submissions.GovGateway.LandRegistry
{
    public static class EdrsResponseAnalyser
    {
        public static EdrsResponse GetEdrsResponse(EdrsSubmissionService.ResponseApplicationToChangeRegisterV1_0Type response)
        {
            switch (response.GatewayResponse.TypeCode)
            {
                case EdrsSubmissionService.ProductResponseCodeContentType.Item10:
                    return EdrsResponse.Acknowledgement;
                case EdrsSubmissionService.ProductResponseCodeContentType.Item20:
                    return EdrsResponse.Rejection;
                case EdrsSubmissionService.ProductResponseCodeContentType.Item30:
                    return EdrsResponse.Results;
                default:
                    return EdrsResponse.Other;
            }
        }

        public static EdrsResponse GetEdrsResponse(EdrsPollRequestService.ResponseApplicationToChangeRegisterV1_0Type response)
        {
            switch (response.GatewayResponse.TypeCode)
            {
                case EdrsPollRequestService.ProductResponseCodeContentType.Item10:
                    return EdrsResponse.Acknowledgement;
                case EdrsPollRequestService.ProductResponseCodeContentType.Item20:
                    return EdrsResponse.Rejection;
                case EdrsPollRequestService.ProductResponseCodeContentType.Item30:
                    return EdrsResponse.Results;
                default:
                    return EdrsResponse.Other;
            }
        }
    }
}
