using LFM.Submissions.Contract.LandRegistry;

namespace LFM.Submissions.GovGateway.LandRegistry
{
    public static class CorrespondencePollResponseAnalyser
    {
        public static CorrespondenceReceived GetCorrespondencePollResponse(CorrespondenceService.CorrespondenceV1_0Type response)
        {
            if (response.GatewayResponse.TypeCode != CorrespondenceService.ProductResponseCodeContentType.Item31)
                return null;

            return new CorrespondenceReceived
                {
                    ApplicationMessageId = response.GatewayResponse.ApplicationMessageId,
                    ExternalReference = response.GatewayResponse.ExternalReference,
                    ABR = response.GatewayResponse.ABR,
                    Filename = response.GatewayResponse.Correspondence.filename,
                    FileFormat = response.GatewayResponse.Correspondence.format,
                    FileData = response.GatewayResponse.Correspondence.Value
                };
        }
    }
}
