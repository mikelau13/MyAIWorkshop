using Microsoft.ProjectOxford.Vision;
using Microsoft.ProjectOxford.Vision.Contract;
using System.Threading.Tasks;

namespace AIConsole.Vision
{
    public interface IVisionConnector<T>
    {
        Task<T> AnalizeImage(string localFileAbsolutePath);

        VisualFeature[] getVisualFeatures();

        VisionServiceClient getVisionClient();
    }
}
