using System;
using Microsoft.ProjectOxford.Vision.Contract;
using System.Threading.Tasks;
using System.IO;

namespace AIConsole.Vision
{
    public class OCRConnector : VisionConnectorBase, IVisionConnector<OcrResults>
    {
        public OCRConnector() : base("/vision/v1.0/ocr") { }

        public async Task<OcrResults> AnalizeImage(string imgLocalFileAbsolutePath)
        {
            //If the user uploaded an image, read it, and send it to the Vision API
            if (File.Exists(imgLocalFileAbsolutePath))
            {
                using (Stream imageFileStream = File.OpenRead(imgLocalFileAbsolutePath))
                {
                    try
                    {
                        //return await this.visionClient.RecognizeTextAsync(imageFileStream);
                        return await this.visionClient.RecognizeTextAsync("https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQwhxtGtb1gvPiXbBE5OsJ6xe2eDSeOZCZUTgZbI-U2yP-bhQl0_g");
                    }
                    catch (Exception e)
                    {
                        return null; //on error, reset analysis result to null
                    }
                }
            }
            //Else, if the user did not upload an image, determine if the message contains a url, and send it to the Vision API
            else
            {
                return null; //on error, reset analysis result to null
            }
        }
    }
}
