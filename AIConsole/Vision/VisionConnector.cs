using System;
using Microsoft.ProjectOxford.Vision;
using Microsoft.ProjectOxford.Vision.Contract;
using System.Threading.Tasks;
using System.Web;
using System.IO;

namespace AIConsole.Vision
{
    public class VisionConnector: VisionConnectorBase, IVisionConnector
    {
        string IVisionConnector.UriPath { get { return "/vision/v1.0"; } }

        public VisionConnector()
        {
            visionClient = new VisionServiceClient("d3e951e5bc7b43df98069688885983d4", UriPath + UriPath);
        }


        /// <summary>
        /// Returns the contents of the specified file as a byte array.
        /// </summary>
        /// <param name="imageFilePath">The image file to read.</param>
        /// <returns>The byte array of the image data.</returns>
        private byte[] GetImageAsByteArray(string imageFilePath)
        {
            FileStream fileStream = new FileStream(imageFilePath, FileMode.Open, FileAccess.Read);
            BinaryReader binaryReader = new BinaryReader(fileStream);
            return binaryReader.ReadBytes((int)fileStream.Length);
        }


        public async Task<AnalysisResult> AnalizeImage(string imgLocalFileAbsolutePath)
        {
            //If the user uploaded an image, read it, and send it to the Vision API
            if (File.Exists(imgLocalFileAbsolutePath))
            {
                using (Stream imageFileStream = File.OpenRead(imgLocalFileAbsolutePath))
                {
                    try
                    {
                        return await this.visionClient.AnalyzeImageAsync(imageFileStream, visualFeatures);
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
