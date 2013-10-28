using LFM.Submissions.AgentServices.OutstandingRequestsService;

namespace LFM.Submissions.AgentServices.LandRegistry
{
    public class OutstandingRequestsSender
    {
        public string RequestId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        
        /// <summary>
        /// TODO: Design consideration -- need to find out exactly what a New response is.  
        /// If it is defined as items not already picked up from this (OutstandingRequests) service then we are in trouble if the responses get lost on 
        /// the way back to us then how do we recover????
        /// Hopefully New Responses are those which we have already requested from the individual poll services in which case we are OK.
        /// </summary>
        /// <returns></returns>
        public ResponseOutstandingRequestsType Query()
        {
            var request = new OutstandingRequestsService.RequestOutstandingRequestsType
                {
                    ID = new Q1IdentifierType() {MessageID = new Q1TextType() {Value = RequestId}},
                    Product = new Q1OutstandingRequestsProductType {ShowOnlyNewResponses = new IndicatorType {Value = true}, SpecificServiceSpecified = false}
                };

            // create an instance of the client
            var client = new OutstandingRequestsService.OutstandingRequestsV2_0ServiceClient();
            // create a Header Instance
            client.ChannelFactory.Endpoint.Behaviors.Add(new HMLRBGMessageEndpointBehavior(Username, Password));

            // submit the request
            var response = client.getOutstandingRequests(request);

            return response;
        }
    }
}
