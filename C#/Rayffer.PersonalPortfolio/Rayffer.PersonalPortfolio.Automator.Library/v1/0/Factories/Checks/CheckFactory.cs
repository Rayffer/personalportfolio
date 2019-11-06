using Rayffer.PersonalPortfolio.Automator.Library.v1._0.Checks;
using Rayffer.PersonalPortfolio.Automator.Library.v1._0.Checks.Managers;
using Rayffer.PersonalPortfolio.Automator.Library.v1._0.DTOs;
using Rayffer.PersonalPortfolio.Automator.Library.v1._0.Interfaces;
using Rayffer.PersonalPortfolio.Automator.Library.v1._0.Providers.HashingProviders;
using Rayffer.PersonalPortfolio.Automator.Library.v1._0.Tracing;
using System;
using System.Collections.Generic;
using Unity;
using Unity.Injection;
using Unity.Resolution;

namespace Rayffer.PersonalPortfolio.Automator.Library.v1._0.Factories.Checks
{
    public class CheckFactory : ICheckFactory
    {
        private readonly ITracing myITracing;
        private readonly IUnityContainer unityContainer;

        public CheckFactory(IUnityContainer unityContainer,
            ITracingFactory tracingFactory)
        {
            myITracing = (tracingFactory != null) ? tracingFactory.GetTracing(this.GetType()) : new Log4NetTracing(this.GetType());
            this.unityContainer = unityContainer.CreateChildContainer();

            ConfigureChecks();
            ConfigureCheckManagers();
        }

        private void ConfigureCheckManagers()
        {
            unityContainer.RegisterType<IObserverCheckManager, ObserverCheckManager>(
                new InjectionConstructor(
                    new InjectionParameter<IList<IObserverCheck>>(new List<IObserverCheck>()),
                    typeof(ITracingFactory)
                    ));
        }

        private void ConfigureChecks()
        {
            unityContainer.RegisterType<IHashingProvider, Md5HashingProvider>(Types.HashingTypes.Md5.ToString());

            unityContainer.RegisterType<IHashingProvider, DummyHashingProvider>(Types.HashingTypes.None.ToString());

            unityContainer.RegisterType<IObserverCheck, ObserverDefaultCheck>(Types.CheckTypes.None.ToString());

            unityContainer.RegisterType<IObserverCheck, ObserverFileDateTimeChangedCheck>(Types.CheckTypes.FileDateTimeChanged.ToString(),
                new InjectionConstructor(
                    new InjectionParameter<Guid>(Guid.Empty),
                    new InjectionParameter<bool>(false),
                    typeof(IDataValidator),
                    typeof(ITracingFactory),
                    new InjectionParameter<string>(null)));

            unityContainer.RegisterType<IObserverCheck, ObserverFileHashCheck>(Types.CheckTypes.CheckHash.ToString(),
                new InjectionConstructor(
                    new InjectionParameter<Guid>(Guid.Empty),
                    new InjectionParameter<bool>(false),
                    typeof(IDataValidator),
                    typeof(ITracingFactory),
                    new InjectionParameter<string>(null),
                    typeof(IFileProvider),
                    typeof(IHashingProvider)));

            unityContainer.RegisterType<IObserverCheck, ObserverFileWithFileHashCheck>(Types.CheckTypes.CheckHashDiferentFile.ToString(),
               new InjectionConstructor(
                   new InjectionParameter<Guid>(Guid.Empty),
                   new InjectionParameter<bool>(false),
                   typeof(IDataValidator),
                   typeof(ITracingFactory),
                   new InjectionParameter<string>(null),
                   typeof(IFileProvider),
                   typeof(IHashingProvider)));

            unityContainer.RegisterType<IObserverCheck, ObserverNewFileInDirectoryCheck>(Types.CheckTypes.NewFilesInDirectory.ToString(),
                new InjectionConstructor(
                    new InjectionParameter<Guid>(Guid.Empty),
                    new InjectionParameter<bool>(false),
                    typeof(IDataValidator),
                    typeof(ITracingFactory),
                    new InjectionParameter<string>(null),
                    new InjectionParameter<string>(null)));

            unityContainer.RegisterType<IObserverCheck, ObserverDataBaseQueryResultChangedCheck>(Types.CheckTypes.SQLQueryResultChange.ToString(),
                new InjectionConstructor(
                    new InjectionParameter<Guid>(Guid.Empty),
                    new InjectionParameter<bool>(false),
                    typeof(IDataValidator),
                    typeof(ITracingFactory),
                    typeof(IDataBaseConnectionProvider),
                    new InjectionParameter<string>(null)));

            unityContainer.RegisterType<IObserverCheck, ObserverXmlFileValueChangedCheck>(Types.CheckTypes.XmlFileValueChanged.ToString(),
                new InjectionConstructor(
                    new InjectionParameter<Guid>(Guid.Empty),
                    new InjectionParameter<bool>(false),
                    typeof(IDataValidator),
                    typeof(ITracingFactory),
                    new InjectionParameter<string>(null),
                    new InjectionParameter<string>(null)));

            unityContainer.RegisterType<IObserverCheck, ObserverIniFileValueChangedCheck>(Types.CheckTypes.IniFileValueChanged.ToString(),
                new InjectionConstructor(
                    new InjectionParameter<Guid>(Guid.Empty),
                    new InjectionParameter<bool>(false),
                    typeof(IDataValidator),
                    typeof(ITracingFactory),
                    new InjectionParameter<string>(null),
                    new InjectionParameter<string>(null),
                    new InjectionParameter<string>(null)));
        }

        public IObserverCheck GetObserverCheck(CheckInformation checkInformation)
        {
            IObserverCheck observerCheck = unityContainer.Resolve<IObserverCheck>(checkInformation.CheckType.ToString(),
                new ResolverOverride[]
                {
                    new PropertyOverride("IsMandatory", checkInformation.IsMandatory),
                    new PropertyOverride("Priority", checkInformation.Priority),
                    new ParameterOverride("filePath", checkInformation.FilePath ?? string.Empty),
                    new ParameterOverride("fileToWatch", checkInformation.FileToWatch ?? string.Empty),
                    new ParameterOverride("keyToWatch", checkInformation.KeyToWatch ?? string.Empty),
                    new ParameterOverride("sectionToWatch", checkInformation.SectionToWatch ?? string.Empty),
                    new ParameterOverride("databaseQuery", checkInformation.DatabaseQuery?? string.Empty),
                    new ParameterOverride("fileFilter", checkInformation.FileFilter ?? string.Empty),
                    new ParameterOverride("checkIdentifier", checkInformation.Identifier),
                    new ParameterOverride("directoryToWatch", checkInformation.DirectoryToWatch ?? string.Empty),
                    new ParameterOverride("proceedWithoutPreviousResult", checkInformation.ProceedWithoutPreviousResult),
                    new ParameterOverride("hashProvider", unityContainer.Resolve<IHashingProvider>(checkInformation.HashType.ToString())),
                    new ParameterOverride("dataBaseConnectionProvider", unityContainer.Resolve<IDataBaseConnectionProvider>(
                        new ResolverOverride[]
                            {
                                new ParameterOverride("connectionString", checkInformation.ConnectionString ?? string.Empty )
                            }
                        ))
                });

            myITracing.Information($"Resolved a new instance of an observer check of type {observerCheck.GetType()}.");

            return observerCheck;
        }

        public IObserverCheckManager GetObserverChecksManager(IList<IObserverCheck> observerChecks)
        {
            return unityContainer.Resolve<IObserverCheckManager>(
                new ResolverOverride[]
                {
                    new ParameterOverride("checksToPerform", observerChecks)
                });
        }
    }
}