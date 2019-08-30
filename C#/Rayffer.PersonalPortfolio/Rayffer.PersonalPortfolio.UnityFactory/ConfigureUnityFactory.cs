using Rayffer.PersonalPortfolio.UnityFactory.Types;
using Unity;
using Unity.Injection;

namespace Rayffer.PersonalPortfolio.UnityFactory
{
    public static class ConfigureUnityFactory
    {
        public static void Configure(IUnityContainer unityContainer)
        {
            ConfigureDatabaseDependencies(unityContainer);

            ConfigureLoggingDependencies(unityContainer);

            ConfigureMappingDependencies(unityContainer);

            ConfigureServiceClientDependencies(unityContainer);

            ConfigureServiceExamples(unityContainer);
        }

        private static void ConfigureServiceExamples(IUnityContainer unityContainer)
        {
            unityContainer.RegisterType<
                Interfaces.IServiceExample,
                Classes.ServiceExamples.ServiceExampleUnityInjectionConstructor>(
                ServiceExampleTypes.UnityInjectionConstructor.ToString(),
                new InjectionConstructor(
                    new ResolvedParameter(typeof(Interfaces.IMappingDependency), MappingDependencyTypes.AutoMapper.ToString()),
                    new ResolvedParameter(typeof(Interfaces.IServiceClientDependency), ServiceClientDependencyTypes.WcfClient.ToString()),
                    new ResolvedParameter(typeof(Interfaces.ILoggingDependency), LoggingDependencyTypes.Serilog.ToString())));

            unityContainer.RegisterType<
                Interfaces.IServiceExample,
                Classes.ServiceExamples.ServiceExampleUnityDefaultResolution>(
                ServiceExampleTypes.UnityDefaultResolution.ToString());

            unityContainer.RegisterType<
                Interfaces.IServiceExample,
                Classes.ServiceExamples.ServiceExampleConstructorDefinedInjections>(
                ServiceExampleTypes.ConstructorDefinedInjections.ToString());

            unityContainer.RegisterType<
                Interfaces.IServiceExample,
                Classes.ServiceExamples.ServiceExamplePropertyDefinedInjections>(
                ServiceExampleTypes.PropertyDefinedInjections.ToString());
        }

        private static void ConfigureServiceClientDependencies(IUnityContainer unityContainer)
        {
            // Default resolution
            unityContainer.RegisterType<
                Interfaces.IServiceClientDependency,
                Classes.ServiceClientDependencies.RealServiceClientDependency>();

            unityContainer.RegisterType<
                Interfaces.IServiceClientDependency,
                Classes.ServiceClientDependencies.WcfClientServiceClientDepency>(
                ServiceClientDependencyTypes.WcfClient.ToString());
            unityContainer.RegisterType<
                Interfaces.IServiceClientDependency,
                Classes.ServiceClientDependencies.RealServiceClientDependency>(
                ServiceClientDependencyTypes.Real.ToString());
        }

        private static void ConfigureMappingDependencies(IUnityContainer unityContainer)
        {
            // Default resolution
            unityContainer.RegisterType<
                Interfaces.IMappingDependency,
                Classes.MappingDependencies.AutoMapperMappingDependency>();

            unityContainer.RegisterType<
                Interfaces.IMappingDependency,
                Classes.MappingDependencies.AutoMapperMappingDependency>(
                MappingDependencyTypes.AutoMapper.ToString());

            unityContainer.RegisterType<
                Interfaces.IMappingDependency,
                Classes.MappingDependencies.DapperMappingDependency>(
                MappingDependencyTypes.Dapper.ToString());

            unityContainer.RegisterType<
                Interfaces.IMappingDependency,
                Classes.MappingDependencies.LinqMappingDependency>(
                MappingDependencyTypes.Linq.ToString());
        }

        private static void ConfigureLoggingDependencies(IUnityContainer unityContainer)
        {
            // Default resolution
            unityContainer.RegisterType<
                Interfaces.ILoggingDependency,
                Classes.LoggingDependencies.Log4NetLoggingDependency>();

            unityContainer.RegisterType<
                Interfaces.ILoggingDependency,
                Classes.LoggingDependencies.Log4NetLoggingDependency>(
                LoggingDependencyTypes.Log4Net.ToString());

            unityContainer.RegisterType<
                Interfaces.ILoggingDependency,
                Classes.LoggingDependencies.SerilogLogigngDependency>(
                LoggingDependencyTypes.Serilog.ToString());
        }

        private static void ConfigureDatabaseDependencies(IUnityContainer unityContainer)
        {
            // Default resolution
            unityContainer.RegisterType<
                Interfaces.IDatabaseDependency,
                Classes.DatabaseDependencies.EntityFrameworkDatabaseDependency>();

            unityContainer.RegisterType<
                Interfaces.IDatabaseDependency,
                Classes.DatabaseDependencies.EntityFrameworkDatabaseDependency>(
                DatabaseDependencyTypes.EntityFramework.ToString());

            unityContainer.RegisterType<
                Interfaces.IDatabaseDependency,
                Classes.DatabaseDependencies.OracleDatabaseDependency>(
                DatabaseDependencyTypes.Oracle.ToString());

            unityContainer.RegisterType<
                Interfaces.IDatabaseDependency,
                Classes.DatabaseDependencies.SqlDatabaseDependency>(
                DatabaseDependencyTypes.Sql.ToString());
        }
    }
}