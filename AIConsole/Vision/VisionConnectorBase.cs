using Microsoft.ProjectOxford.Vision;
using System;
using System.Collections.Generic;
using System.Text;

namespace AIConsole.Vision
{
    public class VisionConnectorBase
    {
        protected string UriPath { get { return "https://westcentralus.api.cognitive.microsoft.com"; } }

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
    }
}
