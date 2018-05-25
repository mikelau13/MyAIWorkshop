using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArticleDigestWorkshop.GoogleApi.CustomSearch
{
    public class CustomSearchConnector:GoogleApiConnectorBase
    {
        private CustomSearchConfig _config = CustomSearchConfig.GetInstance;

        public CustomSearchResponsePOCO DoCustomSearch(CustomSearchRequestPOCO request)
        {
            return (CustomSearchResponsePOCO)Newtonsoft.Json.JsonConvert.DeserializeObject(base.CallApi(_config, request), typeof(CustomSearchResponsePOCO));
        }
    }
}
