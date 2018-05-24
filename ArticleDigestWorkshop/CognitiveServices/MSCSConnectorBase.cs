using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ArticleDigestWorkshop.CognitiveServices
{
    public abstract class MSCSConnectorBase
    {
        protected string CallApi(TextAnalysticsApi.TextAnalysticsApiConfig config, IRequestPOCO body)
        {
            //create the URI
            string uri = config.BaseUrl + "/" + config.ApiName;

            //make the web request
            WebRequest engageRequest = WebRequest.Create(uri);
            engageRequest.ContentType = "application/json";
            engageRequest.Method = "POST";
            engageRequest.Headers.Add("Ocp-Apim-Subscription-Key", config.Key);

            string requestBody = Newtonsoft.Json.JsonConvert.SerializeObject(body);

            byte[] bytes = Encoding.ASCII.GetBytes(requestBody);
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
