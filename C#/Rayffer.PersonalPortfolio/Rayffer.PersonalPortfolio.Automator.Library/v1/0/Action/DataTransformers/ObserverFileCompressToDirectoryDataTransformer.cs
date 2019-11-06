using Rayffer.PersonalPortfolio.Automator.Library.v1._0.Interfaces;
using System.IO;
using System.IO.Compression;

namespace Rayffer.PersonalPortfolio.Automator.Library.v1._0.Action.DataTransformers
{
    public class ObserverFileCompressToDirectoryDataTransformer : IObserverActionDataTransformer<string, string>
    {
        public string TransformData(string data)
        {
            using (FileStream fs = new FileStream($@".\Temp\{System.IO.Path.GetFileNameWithoutExtension(data)}.zip", FileMode.Create))
            using (ZipArchive arch = new ZipArchive(fs, ZipArchiveMode.Create))
            {
                arch.CreateEntryFromFile(data, System.IO.Path.GetFileName(data));
            }

            return $@".\Temp\{System.IO.Path.GetFileNameWithoutExtension(data)}.zip";
        }
    }
}