using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using ArticleDigestWorkshop.CognitiveServices.TextAnalysticsApi;
using ArticleDigestWorkshop.CognitiveServices;
using System.Text.RegularExpressions;

namespace ArticleDigestWorkshop
{
    public partial class Form1 : Form
    {
        private List<ArticlePOCO> ArticleList = new List<ArticlePOCO>();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void _btnLoadArticles_Click(object sender, EventArgs e)
        {
            ArticleList = Sql.Asset.GetAsset(_txtConnectStr.Text.Trim());

            if (ArticleList.Count > 0)
            {
                _lblResult.Text = "Successfully read " + ArticleList.Count.ToString() + " lines from the excel file.";
                _pnlAnalyze.Visible = true;
            }
            else
            {
                _lblResult.Text = "No records.";
                _pnlAnalyze.Visible = false;
            }
        }

        private void _btnAnalyze_Click(object sender, EventArgs e)
        {
            TextAnalysticsConnector textAnalyze = new TextAnalysticsConnector();

            List<AnalyzeResult> results = new List<AnalyzeResult>();

            for (int i = 0; i < ArticleList.Count; i++)
            {
                TextAnalysticsRequestPOCO thisRequest = new TextAnalysticsRequestPOCO();
                thisRequest.Documents.Add(new TextAnalysticsRequestDocument { Language = "en", Id = "1", Text = Regex.Replace(ArticleList[i].Title, "<.*?>", String.Empty) });
                thisRequest.Documents.Add(new TextAnalysticsRequestDocument { Language = "en", Id = "2", Text = Regex.Replace(ArticleList[i].Body, "<.*?>", String.Empty) });

                results.Add(new AnalyzeResult {
                    RequestObject = thisRequest
                    , ResponseObject = textAnalyze.Analyze(thisRequest)
                });
            }

            SaveAnalyzeResult(results);
        }


        private void SaveAnalyzeResult(List<AnalyzeResult> resultToSave)
        {
            string requestBody = Newtonsoft.Json.JsonConvert.SerializeObject(resultToSave);

            if (System.IO.Directory.Exists(_txtSavePath.Text.Trim()))
            {
                string savePath = _txtSavePath.Text.Trim() + "/" + DateTime.Now.ToString("yyyyMMddhhmmssms") + ".json";

                if (!System.IO.File.Exists(savePath))
                {
                    System.IO.File.WriteAllText(savePath, requestBody);
                    _lblResult.Text = "Save succeed.";
                }
                else
                {
                    _lblResult.Text = "Failed to save the result.";
                }
            }
            else
            {
                _lblResult.Text = "Folder does not exist.";
            }
        }
    }
}
