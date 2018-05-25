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

        public TextAnalysticsResponsePOCO Analyze(TextAnalysticsRequestPOCO request)
        {
            return (TextAnalysticsResponsePOCO)Newtonsoft.Json.JsonConvert.DeserializeObject(base.CallApi(_config, request), typeof(TextAnalysticsResponsePOCO));
        }
    }
}
