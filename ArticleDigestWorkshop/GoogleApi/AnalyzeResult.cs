using ArticleDigestWorkshop.GoogleApi.CustomSearch;
using Newtonsoft.Json;

namespace ArticleDigestWorkshop.GoogleApi
{
    [JsonObject("GoogleCustomApi")]
    public class AnalyzeResult
    {
        [JsonProperty("CognitiveResult")]
        public CognitiveServices.AnalyzeResult CognitiveResult { get; set; }


        [JsonProperty("Response")]
        public CustomSearchResponsePOCO ResponseObject { get; set; }
    }
}
