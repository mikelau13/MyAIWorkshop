using Microsoft.ProjectOxford.Vision;
using Microsoft.ProjectOxford.Vision.Contract;
using System.Threading.Tasks;

namespace AIConsole.Vision
{
    public interface IVisionConnector
    {
        string UriPath { get; }

        Task<AnalysisResult> AnalizeImage(string localFileAbsolutePath);

        VisualFeature[] getVisualFeatures();

        VisionServiceClient getVisionClient();
    }
}
