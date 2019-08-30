using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rayffer.PersonalPortfolio.UnityFactory.Types
{
    public enum ServiceExampleTypes
    {
        NotDefined = 0,
        UnityInjectionConstructor,
        UnityDefaultResolution,
        ConstructorDefinedInjections,
        PropertyDefinedInjections
    }
}
