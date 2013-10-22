using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LFM.Submissions.GovGateway.OutstandingRequestsService;

namespace LFM.Submissions.GovGateway.LandRegistry
{
    public class OutstandingRequestsSender
    {
        public string RequestId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        
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
