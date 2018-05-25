using System.Collections.Generic;
using Newtonsoft.Json;

namespace ArticleDigestWorkshop.CognitiveServices.TextAnalysticsApi
{
    public class TextAnalysticsResponsePOCO: IResponsePOCO
    {
        [JsonProperty("documents")]
        public List<TextAnalysticsResponseDocument> Documents { get; set; }

        [JsonProperty("errors")]
        public List<TextAnalysticsResponseError> Errors { get; set; }
    }


    public class TextAnalysticsResponseDocument
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("keyPhrases")]
        public List<string> KeyPhrases { get; set; }
    }


    public class TextAnalysticsResponseError
    {
        [JsonProperty("id")]
        public string Id { get; set; }


        [JsonProperty("message")]
        public string Message { get; set; }
    }
}
