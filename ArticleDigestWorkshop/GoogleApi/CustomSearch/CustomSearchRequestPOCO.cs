using System.Collections.Generic;

namespace ArticleDigestWorkshop.GoogleApi.CustomSearch
{
    public class CustomSearchRequestPOCO: IRequestPOCO
    {
        public string Query { get; set; }

        public int Num { get; set; }

        public string SiteSearch { get; set; }
    }
}
