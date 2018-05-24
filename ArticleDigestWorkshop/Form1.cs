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
                _btnAnalyze.Visible = true;
            }
            else
            {
                _lblResult.Text = "No records.";
                _btnAnalyze.Visible = false;
            }
        }

        private void _btnAnalyze_Click(object sender, EventArgs e)
        {
            TextAnalysticsConnector textAnalyze = new TextAnalysticsConnector();
            textAnalyze.Analyze(ArticleList[0].Title, ArticleList[0].Body);
        }
    }
}
