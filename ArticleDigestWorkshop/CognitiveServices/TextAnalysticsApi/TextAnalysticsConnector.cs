using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArticleDigestWorkshop.CognitiveServices.TextAnalysticsApi
{
    public class TextAnalysticsConnector: MSCSConnectorBase
    {
        private TextAnalysticsApiConfig _config = TextAnalysticsApiConfig.GetInstance;

        public string Analyze(string title, string articleContent)
        {
            TextAnalysticsRequestPOCO requestO = new TextAnalysticsRequestPOCO();
            requestO.Documents.Add(new TextAnalysticsDocument { Language="en", Id="1", Text= title });
            requestO.Documents.Add(new TextAnalysticsDocument { Language = "en", Id = "2", Text = articleContent });

            return (string)Newtonsoft.Json.JsonConvert.DeserializeObject(base.CallApi(_config, requestO), typeof(string));
        }
    }
}
