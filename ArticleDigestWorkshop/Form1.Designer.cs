namespace ArticleDigestWorkshop
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this._btnLoadArticles = new System.Windows.Forms.Button();
            this._lblResult = new System.Windows.Forms.Label();
            this._txtConnectStr = new System.Windows.Forms.TextBox();
            this._btnAnalyze = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // _btnLoadArticles
            // 
            this._btnLoadArticles.Location = new System.Drawing.Point(710, 13);
            this._btnLoadArticles.Name = "_btnLoadArticles";
            this._btnLoadArticles.Size = new System.Drawing.Size(80, 22);
            this._btnLoadArticles.TabIndex = 1;
            this._btnLoadArticles.Text = "Load Articles";
            this._btnLoadArticles.UseVisualStyleBackColor = true;
            this._btnLoadArticles.Click += new System.EventHandler(this._btnLoadArticles_Click);
            // 
            // _lblResult
            // 
            this._lblResult.AutoSize = true;
            this._lblResult.Location = new System.Drawing.Point(17, 47);
            this._lblResult.Name = "_lblResult";
            this._lblResult.Size = new System.Drawing.Size(35, 13);
            this._lblResult.TabIndex = 2;
            this._lblResult.Text = "label1";
            // 
            // _txtConnectStr
            // 
            this._txtConnectStr.Location = new System.Drawing.Point(20, 12);
            this._txtConnectStr.Name = "_txtConnectStr";
            this._txtConnectStr.Size = new System.Drawing.Size(686, 20);
            this._txtConnectStr.TabIndex = 3;
            this._txtConnectStr.Text = "Data Source=172.29.144.91;Initial Catalog=AR_Czuza;User Id=CZuza;Password=$Passwo" +
    "rd..;";
            // 
            // _btnAnalyze
            // 
            this._btnAnalyze.Location = new System.Drawing.Point(710, 41);
            this._btnAnalyze.Name = "_btnAnalyze";
            this._btnAnalyze.Size = new System.Drawing.Size(76, 24);
            this._btnAnalyze.TabIndex = 4;
            this._btnAnalyze.Text = "Analyze";
            this._btnAnalyze.UseVisualStyleBackColor = true;
            this._btnAnalyze.Visible = false;
            this._btnAnalyze.Click += new System.EventHandler(this._btnAnalyze_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this._btnAnalyze);
            this.Controls.Add(this._txtConnectStr);
            this.Controls.Add(this._lblResult);
            this.Controls.Add(this._btnLoadArticles);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button _btnLoadArticles;
        private System.Windows.Forms.Label _lblResult;
        private System.Windows.Forms.TextBox _txtConnectStr;
        private System.Windows.Forms.Button _btnAnalyze;
    }
}

