using Rayffer.PersonalPortfolio.Automator.Library.v1._0.Interfaces;
using System.IO;

namespace Rayffer.PersonalPortfolio.Automator.Library.v1._0.Action.DataTransformers
{
    public class ObserverMemoryStreamPlainDataTransformer : IObserverActionDataTransformer<MemoryStream, MemoryStream>
    {
        public MemoryStream TransformData(MemoryStream data)
        {
            return data;
        }
    }
}