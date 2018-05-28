using ArticleDigestWorkshop.GoogleApi.CustomSearch;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ArticleDigestWorkshop.GoogleApi
{
    public class GoogleApiConnectorBase
    {
        protected string CallApi(CustomSearch.CustomSearchConfig config, CustomSearchRequestPOCO body)
        {
            string parameters = "key=" + config.Key
                + "&cx=" + config.CustomSearchEngineId
                + "&q=" + body.Query
                + (body.Num > 0 ? "&num=" + body.Num.ToString() : "")
                + (!string.IsNullOrWhiteSpace(body.SiteSearch) ? "&siteSearch=" + body.SiteSearch : "")
                ;

            WebRequest engageRequest = WebRequest.Create(config.BaseUrl + "?" + parameters);
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
