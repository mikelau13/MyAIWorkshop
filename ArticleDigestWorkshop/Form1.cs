using System;
using System.Collections.Generic;
using System.Windows.Forms;
using ArticleDigestWorkshop.CognitiveServices.TextAnalysticsApi;
using ArticleDigestWorkshop.CognitiveServices;
using ArticleDigestWorkshop.GoogleApi;
using System.Text.RegularExpressions;
using ArticleDigestWorkshop.GoogleApi.CustomSearch;

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

            List<GoogleApi.AnalyzeResult> results = new List<GoogleApi.AnalyzeResult>();

            for (int i = 0; i < ArticleList.Count; i++)
            {
                TextAnalysticsRequestPOCO txtAnaRequest = new TextAnalysticsRequestPOCO();
                txtAnaRequest.Documents.Add(new TextAnalysticsRequestDocument { Language = "en", Id = "1", Text = Regex.Replace(ArticleList[i].Title, "<.*?>", String.Empty) });
                txtAnaRequest.Documents.Add(new TextAnalysticsRequestDocument { Language = "en", Id = "2", Text = Regex.Replace(Regex.Replace(ArticleList[i].Body, "<.*?>", String.Empty), "&.*?;", String.Empty) });

                TextAnalysticsResponsePOCO txtAnaResponse = textAnalyze.Analyze(txtAnaRequest);

                CognitiveServices.AnalyzeResult textAnalysticsResult = new CognitiveServices.AnalyzeResult
                {
                    RequestObject = txtAnaRequest,
                    ResponseObject = txtAnaResponse
                };

                // ----------- Google Custom Search

                CustomSearchConnector googleCustomSearch = new CustomSearchConnector();

                string searchQ = googleCustomSearch.SearchQueryFromKeyPhrase(((TextAnalysticsResponsePOCO)textAnalysticsResult.ResponseObject).Documents);
                CustomSearchRequestPOCO csRequest = new CustomSearchRequestPOCO();
                csRequest.Query = searchQ;
                csRequest.Num = 10;
                csRequest.SiteSearch = "www.therecord.com"; // let's just hardcoding for now for testing purpose

                CustomSearchResponsePOCO customSearchResponse = googleCustomSearch.DoCustomSearch(csRequest);

                //------------

                results.Add(new GoogleApi.AnalyzeResult
                {
                    CognitiveResult = textAnalysticsResult,
                    ResponseObject = customSearchResponse
                });
            }

            SaveAnalyzeResult(results);
        }


        private void SaveAnalyzeResult(List<GoogleApi.AnalyzeResult> resultToSave)
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
