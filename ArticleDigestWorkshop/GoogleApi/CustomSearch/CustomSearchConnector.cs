using ArticleDigestWorkshop.CognitiveServices.TextAnalysticsApi;
using System.Collections.Generic;

namespace ArticleDigestWorkshop.GoogleApi.CustomSearch
{
    public class CustomSearchConnector:GoogleApiConnectorBase
    {
        private CustomSearchConfig _config = CustomSearchConfig.GetInstance;


        public string SearchQueryFromKeyPhrase(List<TextAnalysticsResponseDocument> keyPhrase)
        {
            int totalSearchPhrase = 0;
            string result = string.Empty;

            for (int i = 0; i < keyPhrase.Count; i++)
            {
                TextAnalysticsResponseDocument each = keyPhrase[i];
                string delimiter = (each.Id == "1" ? "AND" : "OR");

                for (int j = 0; j < each.KeyPhrases.Count; j++)
                {
                    string eachKeyPhr = each.KeyPhrases[j];

                    result = (!string.IsNullOrWhiteSpace(result) ? result + "+" + delimiter  + "+" : result) 
                        + @"%22" + eachKeyPhr.Replace("+", "") + @"%22";

                    totalSearchPhrase++;

                    if (totalSearchPhrase >= 5) return result;
                }
            }

            return result;
        }


        public CustomSearchResponsePOCO DoCustomSearch(CustomSearchRequestPOCO request)
        {
            return (CustomSearchResponsePOCO)Newtonsoft.Json.JsonConvert.DeserializeObject(base.CallApi(_config, request), typeof(CustomSearchResponsePOCO));
        }
    }
}
