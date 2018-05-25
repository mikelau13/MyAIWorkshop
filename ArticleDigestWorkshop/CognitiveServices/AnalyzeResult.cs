using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArticleDigestWorkshop.CognitiveServices
{
    public class AnalyzeResult
    {
        [JsonProperty("Request")]
        public IRequestPOCO RequestObject { get; set; }

        [JsonProperty("Response")]
        public IResponsePOCO ResponseObject { get; set; }
    }
}
