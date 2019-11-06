using System;

namespace Rayffer.PersonalPortfolio.Automator.Library.v1._0.Interfaces
{
    public interface ITracingFactory
    {
        ITracing GetTracing(Type OwnerClassType);
    }
}