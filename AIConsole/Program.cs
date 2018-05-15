using AIConsole.Vision;
using Microsoft.ProjectOxford.Vision.Contract;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
// preleased version
//using Microsoft.Azure.CognitiveServices.Language.TextAnalytics;
//using Microsoft.Azure.CognitiveServices.Language.TextAnalytics.Models;

namespace AIConsole
{
    static class Program
    {
        // **********************************************
        // *** Update or verify the following values. ***
        // **********************************************

        // Replace the subscriptionKey string value with your valid subscription key.
        const string subscriptionKey = "d3e951e5bc7b43df98069688885983d4";

        // Replace or verify the region.
        //
        // You must use the same region in your REST API call as you used to obtain your subscription keys.
        // For example, if you obtained your subscription keys from the westus region, replace 
        // "westcentralus" in the URI below with "westus".
        //
        // NOTE: Free trial subscription keys are generated in the westcentralus region, so if you are using
        // a free trial subscription key, you should not need to change this region.
        const string uriBase = "https://westcentralus.api.cognitive.microsoft.com/vision/v1.0/analyze";


        static void Main()
        {
            // Get the path and filename to process from the user.
            Console.WriteLine("Analyze an image:");
            Console.Write("Enter the path to an image you wish to analzye: ");
            string imageFilePath = Console.ReadLine();

            // Execute the REST API call.
            MakeAnalysisRequest(imageFilePath);
            MakeThumbNailRequest(imageFilePath);
            MakeOCRRequest(imageFilePath);


            Console.WriteLine("\nPlease wait a moment for the results to appear. Then, press Enter to exit...\n");
            Console.ReadLine();
        }


        /// <summary>
        /// Gets the analysis of the specified image file by using the Computer Vision REST API.
        /// </summary>
        /// <param name="imageFilePath">The image file.</param>
        static async void MakeAnalysisRequest(string imageFilePath)
        {
            HttpClient client = new HttpClient();

            // Request headers.
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", subscriptionKey);

            // Request parameters. A third optional parameter is "details".
            string requestParameters = "visualFeatures=Categories,Description,Color&language=en";
            //string requestParameters = "model=landmarks"; // landmarks, celebrities, etc

            // Assemble the URI for the REST API Call.
            string uri = uriBase + "?" + requestParameters;

            //HttpResponseMessage response;

            VisionConnector connector = new VisionConnector();
            AnalysisResult analysisResult = await connector.AnalizeImage(imageFilePath);

            if (analysisResult != null)
            {
                string imageCaption = analysisResult.Description.Captions[0].Text;

                Console.WriteLine("This is ");
                Console.WriteLine(imageCaption);
            }
            Console.WriteLine("\n");
            /*
            // Request body. Posts a locally stored JPEG image.
            byte[] byteData = GetImageAsByteArray(imageFilePath);

            using (ByteArrayContent content = new ByteArrayContent(byteData))
            {
                // This example uses content type "application/octet-stream".
                // The other content types you can use are "application/json" and "multipart/form-data".
                content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");

                // Execute the REST API call.
                response = await client.PostAsync(uri, content);

                // Get the JSON response.
                string contentString = await response.Content.ReadAsStringAsync();

                // Display the JSON response.
                Console.WriteLine("\nResponse:\n");
                Console.WriteLine(JsonPrettyPrint(contentString));
            }*/
        }


        /// <summary>
        /// Gets a thumbnail image from the specified image file by using the Computer Vision REST API.
        /// </summary>
        /// <param name="imageFilePath">The image file to use to create the thumbnail image.</param>
        static async void MakeThumbNailRequest(string imageFilePath)
        {
            HttpClient client = new HttpClient();

            // Request headers.
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", subscriptionKey);

            // Request parameters.
            string requestParameters = "width=200&height=150&smartCropping=true";

            // Assemble the URI for the REST API Call.
            string uri = "https://westcentralus.api.cognitive.microsoft.com/vision/v1.0/generateThumbnail" + "?" + requestParameters;

            HttpResponseMessage response;

            // Request body. Posts a locally stored JPEG image.
            byte[] byteData = GetImageAsByteArray(imageFilePath);

            using (ByteArrayContent content = new ByteArrayContent(byteData))
            {
                // This example uses content type "application/octet-stream".
                // The other content types you can use are "application/json" and "multipart/form-data".
                content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");

                // Execute the REST API call.
                response = await client.PostAsync(uri, content);

                if (response.IsSuccessStatusCode)
                {
                    // Display the response data.
                    Console.WriteLine("\nThumbnail Response:\n");
                    Console.WriteLine(response);

                    // Get the image data.
                    byte[] thumbnailImageData = await response.Content.ReadAsByteArrayAsync();
                }
                else
                {
                    // Display the JSON error data.
                    Console.WriteLine("\nError:\n");
                    Console.WriteLine(JsonHelper.JsonPrettyPrint(await response.Content.ReadAsStringAsync()));
                }
            }
        }


        /// <summary>
        /// Gets the text visible in the specified image file by using the Computer Vision REST API.
        /// </summary>
        /// <param name="imageFilePath">The image file.</param>
        static async void MakeOCRRequest2(string imageFilePath)
        {
            // Request parameters. A third optional parameter is "details".
            string requestParameters = "language=unk&detectOrientation=true";

            //HttpResponseMessage response;

            IVisionConnector<OcrResults> connector = new OCRConnector();
            OcrResults analysisResult = await connector.AnalizeImage(imageFilePath);

            if (analysisResult != null)
            {
                string imageCaption = analysisResult.Orientation;

                Console.WriteLine("OCR result: ");
                Console.WriteLine(imageCaption);
            }
            else
            {
                Console.WriteLine("OCR FAILED");
            }
            Console.WriteLine("\n");
        }


        static async void MakeOCRRequest(string imageFilePath)
        {
            HttpClient client = new HttpClient();

            // Request headers.
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", subscriptionKey);

            // Request parameters.
            string requestParameters = "language=unk&detectOrientation=true";

            // Assemble the URI for the REST API Call.
            string uri = "https://westcentralus.api.cognitive.microsoft.com/vision/v1.0/ocr" + "?" + requestParameters;

            HttpResponseMessage response;

            // Request body. Posts a locally stored JPEG image.
            byte[] byteData = GetImageAsByteArray(imageFilePath);

            using (ByteArrayContent content = new ByteArrayContent(byteData))
            {
                // This example uses content type "application/octet-stream".
                // The other content types you can use are "application/json" and "multipart/form-data".
                content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");

                // Execute the REST API call.
                response = await client.PostAsync(uri, content);

                // Get the JSON response.
                string contentString = await response.Content.ReadAsStringAsync();

                // Display the JSON response.
                Console.WriteLine("\nResponse:\n");
                Console.WriteLine(JsonHelper.JsonPrettyPrint(contentString));
            }
        }


        static async void MakeTextAnalyticsRequest()
        {
            // key 1 773bcc2319d142efbc1058a951453184
            // key 2 a8191a3957ea4104a5ca14aaeb8b611a
        }

        /*
        /// <summary>
        /// pre-released
        /// </summary>
        static async void MakeTextAnalyticsRequest()
        {
            // example codes from: https://docs.microsoft.com/en-us/azure/cognitive-services/text-analytics/quickstarts/csharp
            // Create a client.
            ITextAnalyticsAPI client = new TextAnalyticsAPI();
            client.AzureRegion = AzureRegions.Westus;
            client.SubscriptionKey = "ENTER KEY HERE";

            Console.OutputEncoding = System.Text.Encoding.UTF8;

            // Extracting language
            Console.WriteLine("===== LANGUAGE EXTRACTION ======");

            LanguageBatchResult result = client.DetectLanguage(
                    new BatchInput(
                        new List<Input>()
                        {
                          new Input("1", "This is a document written in English."),
                          new Input("2", "Este es un document escrito en Español."),
                          new Input("3", "这是一个用中文写的文件")
                        }));

            // Printing language results.
            foreach (var document in result.Documents)
            {
                Console.WriteLine("Document ID: {0} , Language: {1}", document.Id, document.DetectedLanguages[0].Name);
            }

            // Getting key-phrases
            Console.WriteLine("\n\n===== KEY-PHRASE EXTRACTION ======");

            KeyPhraseBatchResult result2 = client.KeyPhrases(
                    new MultiLanguageBatchInput(
                        new List<MultiLanguageInput>()
                        {
                          new MultiLanguageInput("ja", "1", "猫は幸せ"),
                          new MultiLanguageInput("de", "2", "Fahrt nach Stuttgart und dann zum Hotel zu Fu."),
                          new MultiLanguageInput("en", "3", "My cat is stiff as a rock."),
                          new MultiLanguageInput("es", "4", "A mi me encanta el fútbol!")
                        }));


            // Printing keyphrases
            foreach (var document in result2.Documents)
            {
                Console.WriteLine("Document ID: {0} ", document.Id);

                Console.WriteLine("\t Key phrases:");

                foreach (string keyphrase in document.KeyPhrases)
                {
                    Console.WriteLine("\t\t" + keyphrase);
                }
            }

            // Extracting sentiment
            Console.WriteLine("\n\n===== SENTIMENT ANALYSIS ======");

            SentimentBatchResult result3 = client.Sentiment(
                    new MultiLanguageBatchInput(
                        new List<MultiLanguageInput>()
                        {
                          new MultiLanguageInput("en", "0", "I had the best day of my life."),
                          new MultiLanguageInput("en", "1", "This was a waste of my time. The speaker put me to sleep."),
                          new MultiLanguageInput("es", "2", "No tengo dinero ni nada que dar..."),
                          new MultiLanguageInput("it", "3", "L'hotel veneziano era meraviglioso. È un bellissimo pezzo di architettura."),
                        }));


            // Printing sentiment results
            foreach (var document in result3.Documents)
            {
                Console.WriteLine("Document ID: {0} , Sentiment Score: {1:0.00}", document.Id, document.Score);
            }
        }*/


        /// <summary>
        /// Returns the contents of the specified file as a byte array.
        /// </summary>
        /// <param name="imageFilePath">The image file to read.</param>
        /// <returns>The byte array of the image data.</returns>
        static byte[] GetImageAsByteArray(string imageFilePath)
        {
            FileStream fileStream = new FileStream(imageFilePath, FileMode.Open, FileAccess.Read);
            BinaryReader binaryReader = new BinaryReader(fileStream);
            return binaryReader.ReadBytes((int)fileStream.Length);
        }
    }

    static class Extensions
    {
        public static void ForEach<T>(this IEnumerable<T> ie, Action<T> action)
        {
            foreach (var i in ie)
            {
                action(i);
            }
        }
    }
}