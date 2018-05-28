using ArticleDigestWorkshop.BingApi.CustomSearch;
using System.IO;
using System.Net;


namespace ArticleDigestWorkshop.BingApi
{
    public class BingApiConnectorBase
    {
        protected string CallApi(CustomSearch.CustomSearchConfig config, CustomSearchRequestPOCO body)
        {
            string parameters = "customConfig=" + config.CustomConfigId
                + "&q=" + body.Query
                ;

            WebRequest engageRequest = WebRequest.Create(config.BaseUrl + "?" + parameters);
            engageRequest.Headers.Add("Ocp-Apim-Subscription-Key", config.Key);
            engageRequest.Method = "GET";

            string result;

            using (WebResponse engageResponse = engageRequest.GetResponse())
            {
                if (engageResponse == null) { return "engageResponse"; }

                using (StreamReader engageStream = new StreamReader(engageResponse.GetResponseStream()))
                {
                    result = engageStream.ReadToEnd().Trim();

                    engageStream.Close();
                }

                engageResponse.Close();
            }

            return result;
        }
    }
}
