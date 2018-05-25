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


            //create the URI
            string uri = config.BaseUrl;

            //make the web request
            WebRequest engageRequest = WebRequest.Create(uri);
            engageRequest.ContentType = "application/json";
            engageRequest.Method = "POST";

            byte[] bytes = Encoding.ASCII.GetBytes(parameters);
            Stream requestStream = null;

            try
            {
                engageRequest.ContentLength = bytes.Length;
                requestStream = engageRequest.GetRequestStream();
                requestStream.Write(bytes, 0, bytes.Length);
            }
            catch (WebException ex)
            {
                //Handle WebException error here
                return "WebException";
            }
            finally
            {
                if (requestStream != null)
                {
                    requestStream.Close();
                }
            }

            string result;
            //If there's a web response, then read the stream...otherwise exit the flow
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
