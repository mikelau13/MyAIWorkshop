using System.Collections.Generic;
using Newtonsoft.Json;

namespace ArticleDigestWorkshop.CognitiveServices.TextAnalysticsApi
{
    public class TextAnalysticsRequestPOCO: IRequestPOCO
    {
        public TextAnalysticsRequestPOCO()
        {
            Documents = new List<TextAnalysticsRequestDocument>();
        }

        [JsonProperty("documents")]
        public List<TextAnalysticsRequestDocument> Documents { get; set; }
    }


    public class TextAnalysticsRequestDocument
    {
        [JsonProperty("language")]
        public string Language { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }
    }
}
