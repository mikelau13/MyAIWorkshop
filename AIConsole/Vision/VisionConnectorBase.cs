using Microsoft.ProjectOxford.Vision;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AIConsole.Vision
{
    public class VisionConnectorBase
    {
        public VisionConnectorBase(string uriPath)
        {
            visionClient = new VisionServiceClient("d3e951e5bc7b43df98069688885983d4", UriBase + uriPath);
        }

        protected string UriBase { get { return "https://westcentralus.api.cognitive.microsoft.com"; } }

        protected VisionServiceClient visionClient;

        protected VisualFeature[] visualFeatures = new VisualFeature[] {
                                        VisualFeature.Adult, //recognize adult content
                                        VisualFeature.Categories, //recognize image features
                                        VisualFeature.Description //generate image caption
                                        };

        public virtual VisualFeature[] getVisualFeatures()
        {
            return visualFeatures;
        }

        public virtual VisionServiceClient getVisionClient()
        {
            return visionClient;
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
    }
}
