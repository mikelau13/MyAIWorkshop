using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArticleDigestWorkshop.CognitiveServices.TextAnalysticsApi
{
    public class TextAnalysticsApiConfig
    {
        public string Key { get { return "773bcc2319d142efbc1058a951453184"; } }

        public string BaseUrl { get { return "https://eastus.api.cognitive.microsoft.com/text/analytics/v2.0"; } }

        public string ApiName { get { return "keyPhrases"; } }

        private static readonly Lazy<TextAnalysticsApiConfig> lazy = new Lazy<TextAnalysticsApiConfig>(() => new TextAnalysticsApiConfig());

        public static TextAnalysticsApiConfig GetInstance { get { return lazy.Value; } }
    }
}
