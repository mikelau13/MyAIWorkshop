using System.Collections.Generic;
using Newtonsoft.Json;

namespace ArticleDigestWorkshop.BingApi.CustomSearch
{
    public class CustomSearchResponsePOCO : IResponsePOCO
    {
        [JsonProperty("webPages")]
        public CustomSearchResponseWebPage WebPages { get; set; }
    }


    public class CustomSearchResponseWebPage
    {
        [JsonProperty("value")]
        public List<CustomSearchResponseItem> Items { get; set; }
    }


    public class CustomSearchResponseItem
    {
        [JsonProperty("name")]
        public string Title { get; set; }


        [JsonProperty("url")]
        public string Link { get; set; }

        [JsonProperty("snippet")]
        public string Snippet { get; set; }
    }
}
