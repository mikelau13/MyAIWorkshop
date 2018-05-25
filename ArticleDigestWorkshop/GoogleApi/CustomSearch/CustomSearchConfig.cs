using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArticleDigestWorkshop.GoogleApi.CustomSearch
{
    public class CustomSearchConfig
    {
        public string Key { get { return "AIzaSyDbQRgoJNFDio4wITXJz1mo6ORTloKaDPw"; } } // this is just a DEV key

        public string BaseUrl { get { return "https://www.googleapis.com/customsearch/v1"; } }

        public string CustomSearchEngineId { get { return "002863338977469305439:f-6kj2ouexo"; } }


        private static readonly Lazy<CustomSearchConfig> lazy = new Lazy<CustomSearchConfig>(() => new CustomSearchConfig());

        public static CustomSearchConfig GetInstance { get { return lazy.Value; } }
    }
}
