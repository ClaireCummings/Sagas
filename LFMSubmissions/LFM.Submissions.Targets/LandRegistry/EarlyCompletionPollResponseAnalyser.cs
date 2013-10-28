using LFM.Submissions.Contract.LandRegistry;

namespace LFM.Submissions.AgentServices.LandRegistry
{
    public static class EarlyCompletionPollResponseAnalyser
    {
        public static EarlyCompletionReceived GetEarlyCompletionResponse(EarlyCompletionService.ResponseEarlyCompletionV1_0Type response)
        {
            if (response.GatewayResponse.TypeCode != EarlyCompletionService.ProductResponseCodeContentType.Item30)
                return null;
            
            return new EarlyCompletionReceived
            {
                ApplicationMessageId = response.GatewayResponse.EarlyCompletion.ApplicationMessageId,
                ExternalReference = response.GatewayResponse.EarlyCompletion.ExternalReference,
                ABR = response.GatewayResponse.EarlyCompletion.ABR,
                Filename = response.GatewayResponse.EarlyCompletion.DespatchDocument.filename,
                FileFormat = response.GatewayResponse.EarlyCompletion.DespatchDocument.format,
                DespatchDocument = response.GatewayResponse.EarlyCompletion.DespatchDocument.Value
            };
        }
    }
}
