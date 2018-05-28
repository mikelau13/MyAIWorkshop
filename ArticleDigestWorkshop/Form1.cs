using System;
using System.Collections.Generic;
using System.Windows.Forms;
using ArticleDigestWorkshop.CognitiveServices.TextAnalysticsApi;
using System.Text.RegularExpressions;
using ArticleDigestWorkshop.BingApi.CustomSearch;

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

            List<BingApi.AnalyzeResult> results = new List<BingApi.AnalyzeResult>();

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

                // ----------- Custom Search

                CustomSearchConnector customSearch = new CustomSearchConnector();

                string searchQ = customSearch.SearchQueryFromKeyPhrase(((TextAnalysticsResponsePOCO)textAnalysticsResult.ResponseObject).Documents);
                CustomSearchRequestPOCO csRequest = new CustomSearchRequestPOCO();
                csRequest.Query = searchQ;

                CustomSearchResponsePOCO customSearchResponse = customSearch.DoCustomSearch(csRequest);

                System.Threading.Thread.Sleep(1000);

                //------------

                results.Add(new BingApi.AnalyzeResult
                {
                    CognitiveResult = textAnalysticsResult,
                    ResponseObject = customSearchResponse
                });
            }

            SaveResult(results);
        }


        private void SaveResult(List<BingApi.AnalyzeResult> results)
        {
            string fileName = DateTime.Now.ToString("yyyyMMddhhmmssms");

            SaveAnalyzeResult(fileName, results);
            GenerateHtmlFromAnalyzeResult(fileName, results);
        }



        private void GenerateHtmlFromAnalyzeResult(string fileName, List<BingApi.AnalyzeResult> resultToGenerate)
        {
            string templateRow = "<tr><td valign='top' width='20%'>{0}</td><td width='40%'><textarea rows='10' cols='80'>{1}</textarea></td><td valign='top' width='40%'>{2}</td></tr>";
            string templateEachResult = "<a href='{0}'>{1}</a><br/>";

            string allRow = "";

            for (int i = 0; i < resultToGenerate.Count; i++)
            {
                BingApi.AnalyzeResult each = resultToGenerate[i];

                string originalTitle = ((TextAnalysticsRequestPOCO)each.CognitiveResult.RequestObject).Documents[0].Text;
                string originalBody = ((TextAnalysticsRequestPOCO)each.CognitiveResult.RequestObject).Documents[1].Text;

                string searchedEach = "";

                if (each.ResponseObject != null && each.ResponseObject.WebPages.Items != null)
                {
                    for (int j = 0; j < each.ResponseObject.WebPages.Items.Count; j++)
                    {
                        searchedEach += (String.Format(templateEachResult, each.ResponseObject.WebPages.Items[j].Link, each.ResponseObject.WebPages.Items[j].Title));
                    }
                }
                else
                {
                    searchedEach = "NULL";
                }
                allRow += String.Format(templateRow, originalTitle, originalBody, searchedEach);
            }

            string html = String.Format("<html><body><table border=1>{0}</table></body></html>", allRow);


            if (System.IO.Directory.Exists(_txtSavePath.Text.Trim()))
            {
                string savePath = _txtSavePath.Text.Trim() + "/" + fileName + ".html";

                if (!System.IO.File.Exists(savePath))
                {
                    System.IO.File.WriteAllText(savePath, html);
                    _lblResult.Text = "Html Save succeed.";
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


        private void SaveAnalyzeResult(string fileName, List<BingApi.AnalyzeResult> resultToSave)
        {
            string requestBody = Newtonsoft.Json.JsonConvert.SerializeObject(resultToSave);

            if (System.IO.Directory.Exists(_txtSavePath.Text.Trim()))
            {
                string savePath = _txtSavePath.Text.Trim() + "/" + fileName + ".json";

                if (!System.IO.File.Exists(savePath))
                {
                    System.IO.File.WriteAllText(savePath, requestBody);
                    _lblResult.Text = "Json Save succeed.";
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





        private void SaveResult (List<GoogleApi.AnalyzeResult> results)
        {
            string fileName = DateTime.Now.ToString("yyyyMMddhhmmssms");

            SaveAnalyzeResult(fileName, results);
            GenerateHtmlFromAnalyzeResult(fileName, results);
        }


        private void GenerateHtmlFromAnalyzeResult(string fileName, List<GoogleApi.AnalyzeResult> resultToGenerate)
        {
            string templateRow = "<tr><td valign='top' width='20%'>{0}</td><td width='40%'><textarea rows='10' cols='80'>{1}</textarea></td><td valign='top' width='40%'>{2}</td></tr>";
            string templateEachResult = "<a href='{0}'>{1}</a><br/>";

            string allRow = "";

            for (int i = 0; i < resultToGenerate.Count; i++)
            {
                GoogleApi.AnalyzeResult each = resultToGenerate[i];

                string originalTitle = ((TextAnalysticsRequestPOCO)each.CognitiveResult.RequestObject).Documents[0].Text;
                string originalBody = ((TextAnalysticsRequestPOCO)each.CognitiveResult.RequestObject).Documents[1].Text;

                string searchedEach = "";

                if (each.ResponseObject != null && each.ResponseObject.Items != null)
                {
                    for (int j = 0; j < each.ResponseObject.Items.Count; j++)
                    {
                        searchedEach += (String.Format(templateEachResult, each.ResponseObject.Items[j].Link, each.ResponseObject.Items[j].Title));
                    }
                }
                else
                {
                    searchedEach = "NULL";
                }
                allRow += String.Format(templateRow, originalTitle, originalBody, searchedEach);
            }

            string html = String.Format("<html><body><table border=1>{0}</table></body></html>", allRow);


            if (System.IO.Directory.Exists(_txtSavePath.Text.Trim()))
            {
                string savePath = _txtSavePath.Text.Trim() + "/" + fileName + ".html";

                if (!System.IO.File.Exists(savePath))
                {
                    System.IO.File.WriteAllText(savePath, html);
                    _lblResult.Text = "Html Save succeed.";
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


        private void SaveAnalyzeResult(string fileName, List<GoogleApi.AnalyzeResult> resultToSave)
        {
            string requestBody = Newtonsoft.Json.JsonConvert.SerializeObject(resultToSave);

            if (System.IO.Directory.Exists(_txtSavePath.Text.Trim()))
            {
                string savePath = _txtSavePath.Text.Trim() + "/" + fileName + ".json";

                if (!System.IO.File.Exists(savePath))
                {
                    System.IO.File.WriteAllText(savePath, requestBody);
                    _lblResult.Text = "Json Save succeed.";
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
