using System.Collections.Generic;
using Newtonsoft.Json;

namespace ArticleDigestWorkshop.GoogleApi.CustomSearch
{
    public class CustomSearchResponsePOCO: IResponsePOCO
    {
        [JsonProperty("items")]
        public List<CustomSearchResponseItem> Items { get; set; }
    }


    public class CustomSearchResponseItem
    {
        [JsonProperty("title")]
        public string Title { get; set; }


        [JsonProperty("link")]
        public string Link { get; set; }

        [JsonProperty("snippet")]
        public string Snippet { get; set; }
    }
}
