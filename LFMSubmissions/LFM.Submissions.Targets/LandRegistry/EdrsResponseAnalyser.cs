using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LFM.Submissions.Targets.EdrsSubmissionService;

namespace LFM.Submissions.Targets.LandRegistry
{
    public class EdrsResponseAnalyser
    {
        private readonly string _responsePayload;
        private readonly ResponseApplicationToChangeRegisterV1_0Type _response;
        public EdrsResponse EdrsResponse { get; set; }

        public EdrsResponseAnalyser(string responsePayload)
        {
            _responsePayload = responsePayload;
            _response =
                ObjectSerializer.XmlDeserializeFromString<ResponseApplicationToChangeRegisterV1_0Type>(_responsePayload);
            GetEdrsResponse();
        }

        private void GetEdrsResponse()
        {
            switch (_response.GatewayResponse.TypeCode)
            {
                case EdrsSubmissionService.ProductResponseCodeContentType.Item10:
                    EdrsResponse = EdrsResponse.Acknowledgement;
                    break;
                case EdrsSubmissionService.ProductResponseCodeContentType.Item20:
                    EdrsResponse = EdrsResponse.Rejection;
                    break;
                case EdrsSubmissionService.ProductResponseCodeContentType.Item30:
                    EdrsResponse = EdrsResponse.Results;
                    break;
                default:
                    EdrsResponse = EdrsResponse.Other;
                    break;
            }
        }
    }
}
