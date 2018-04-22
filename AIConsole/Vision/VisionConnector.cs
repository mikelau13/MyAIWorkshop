﻿using System;
using Microsoft.ProjectOxford.Vision;
using Microsoft.ProjectOxford.Vision.Contract;
using System.Threading.Tasks;
using System.Web;
using System.IO;

namespace AIConsole.Vision
{
    public class VisionConnector : VisionConnectorBase, IVisionConnector<AnalysisResult>
    {
        public VisionConnector() : base("/vision/v1.0") { }

        public async Task<AnalysisResult> AnalizeImage(string imgLocalFileAbsolutePath)
        {
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
            else
            {
                return null; //on error, reset analysis result to null
            }
        }




    }
}
