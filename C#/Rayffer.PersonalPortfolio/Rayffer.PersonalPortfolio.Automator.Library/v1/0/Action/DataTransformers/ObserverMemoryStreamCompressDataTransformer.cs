using Rayffer.PersonalPortfolio.Automator.Library.v1._0.Interfaces;
using System.IO;
using System.IO.Compression;

namespace Rayffer.PersonalPortfolio.Automator.Library.v1._0.Action.DataTransformers
{
    public class ObserverMemoryStreamCompressDataTransformer : IObserverActionDataTransformer<MemoryStream, MemoryStream>
    {
        public MemoryStream TransformData(MemoryStream data)
        {
            MemoryStream compressedStream = new MemoryStream();
            using (var compressor = new DeflateStream(compressedStream, CompressionMode.Compress))
            {
                data.WriteTo(compressor);
            }
            data.Dispose();
            return compressedStream;
        }
    }
}