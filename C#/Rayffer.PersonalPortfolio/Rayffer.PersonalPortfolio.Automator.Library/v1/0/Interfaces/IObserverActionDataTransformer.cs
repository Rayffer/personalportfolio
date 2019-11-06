using System;
using System.Collections.Generic;
using System.Text;

namespace Rayffer.PersonalPortfolio.Automator.Library.v1._0.Interfaces
{
    public interface IObserverActionDataTransformer<SourceType, DestinationType>
    {
        DestinationType TransformData(SourceType data);
    }
}