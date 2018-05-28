using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArticleDigestWorkshop.BingApi.CustomSearch
{
    public class CustomSearchConfig
    {
        public string Key { get { return "996d9cf1462a4c5b83a6f7687a6f5849"; } } // this is just a DEV key

        public string BaseUrl { get { return "https://api.cognitive.microsoft.com/bingcustomsearch/v7.0/search"; } }

        public string CustomConfigId { get { return "2729466359"; } }


        private static readonly Lazy<CustomSearchConfig> lazy = new Lazy<CustomSearchConfig>(() => new CustomSearchConfig());

        public static CustomSearchConfig GetInstance { get { return lazy.Value; } }
    }
}
