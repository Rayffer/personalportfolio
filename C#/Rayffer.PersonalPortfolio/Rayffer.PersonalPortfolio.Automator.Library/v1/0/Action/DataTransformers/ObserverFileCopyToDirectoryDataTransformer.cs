using Rayffer.PersonalPortfolio.Automator.Library.v1._0.Interfaces;

namespace Rayffer.PersonalPortfolio.Automator.Library.v1._0.Action.DataTransformers
{
    public class ObserverFileCopyToDirectoryDataTransformer : IObserverActionDataTransformer<string, string>
    {
        private string tempFolder = @".\Temp\";

        public string TransformData(string data)
        {
            if (System.IO.File.Exists(data))
            {
                string destinationPath = tempFolder + System.IO.Path.GetFileName(data);
                System.IO.File.Copy(data, destinationPath);
                return destinationPath;
            }
            return string.Empty;
        }
    }
}