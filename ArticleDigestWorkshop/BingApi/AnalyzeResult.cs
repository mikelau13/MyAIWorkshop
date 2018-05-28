using ArticleDigestWorkshop.BingApi.CustomSearch;
using Newtonsoft.Json;

namespace ArticleDigestWorkshop.BingApi
{
    [JsonObject("BingCustomApi")]
    public class AnalyzeResult
    {
        [JsonProperty("CognitiveResult")]
        public CognitiveServices.AnalyzeResult CognitiveResult { get; set; }


        [JsonProperty("Response")]
        public CustomSearchResponsePOCO ResponseObject { get; set; }
    }
}
