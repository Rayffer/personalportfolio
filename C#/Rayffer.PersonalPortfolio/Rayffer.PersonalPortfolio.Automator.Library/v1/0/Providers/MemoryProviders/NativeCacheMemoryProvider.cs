using Rayffer.PersonalPortfolio.Automator.Library.v1._0.Interfaces;
using System;
using System.Collections.Generic;

namespace Rayffer.PersonalPortfolio.Automator.Library.v1._0.Providers.MemoryProviders
{
    public class NativeCacheMemoryProvider : IObserverMemoryProvider
    {
        public object MemoryObject { get; set; }
    }
}