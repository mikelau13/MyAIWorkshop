using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ArticleDigestWorkshop
{
    public class ArticlePOCO
    {
        public string Title        { get; set; }
        public string SubTitle        { get; set; }
        public string Abstract        { get; set; }
        public string Body        { get; set; }
        public string JsonSent        { get; set; }
        public string JsonReturn        { get; set; }
        public string SearchString        { get; set; }
        public string GoogleUrl        { get; set; }


        public static ArticlePOCO FromCsv(string csvLine)
        {
            //string[] values = csvLine.Split(',');
            string pattern = "\"{1}[\\W|,]*\"{1}";
            string[] values = Regex.Split(csvLine, pattern).Select(x => x.Replace("\"", "")).ToArray();

            ArticlePOCO result = new ArticlePOCO();

            if (values.Length >= 3)
            {
                result.Title = (values[0]);
                result.SubTitle = (values[1]);
                result.Abstract = (values[2]);
                result.Body = (values[3]);
                //result.JsonSent = (values[4]);
                //result.JsonReturn = (values[5]);
                //result.SearchString = (values[6]);
                //result.GoogleUrl = (values[7]);
            }
            else
            {

            }

            return result;
        }


        public static ArticlePOCO FromCsv(string[] values)
        {
            ArticlePOCO result = new ArticlePOCO();

            if (values.Length >= 4)
            {
                result.Title = (values[0]);
                result.SubTitle = (values[1]);
                result.Abstract = (values[2]);
                result.Body = (values[3]);
                //result.JsonSent = (values[4]);
                //result.JsonReturn = (values[5]);
                //result.SearchString = (values[6]);
                //result.GoogleUrl = (values[7]);
            }
            else
            {

            }

            return result;
        }
    }
}
