using Rayffer.PersonalPortfolio.Generators;
using Rayffer.PersonalPortfolio.QueueManagers;
using Rayffer.PersonalPortfolio.Sorters;
using Rayffer.PersonalPortfolio.UnityFactory;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

namespace Rayffer.PersonalPortfolio.TestLaboratory
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            ServiceUnityFactory serviceUnityFactory = new ServiceUnityFactory();

            var constructorInjectionService = serviceUnityFactory.GetServiceExample(UnityFactory.Types.ServiceExampleTypes.ConstructorDefinedInjections);
            var propertyInjectionService = serviceUnityFactory.GetServiceExample(UnityFactory.Types.ServiceExampleTypes.PropertyDefinedInjections);
            var defaultInjectionService = serviceUnityFactory.GetServiceExample(UnityFactory.Types.ServiceExampleTypes.UnityDefaultResolution);
            var unityInjectionConstructorService = serviceUnityFactory.GetServiceExample(UnityFactory.Types.ServiceExampleTypes.UnityInjectionConstructor);
        }
    }
}