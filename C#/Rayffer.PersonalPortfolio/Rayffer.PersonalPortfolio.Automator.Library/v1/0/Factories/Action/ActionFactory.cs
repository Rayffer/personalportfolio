using Moq;
using Rayffer.PersonalPortfolio.Automator.Library.v1._0.Action;
using Rayffer.PersonalPortfolio.Automator.Library.v1._0.Action.DataProviders;
using Rayffer.PersonalPortfolio.Automator.Library.v1._0.Action.DataTransformers;
using Rayffer.PersonalPortfolio.Automator.Library.v1._0.Action.Senders;
using Rayffer.PersonalPortfolio.Automator.Library.v1._0.DTOs;
using Rayffer.PersonalPortfolio.Automator.Library.v1._0.Interfaces;
using Rayffer.PersonalPortfolio.Automator.Library.v1._0.Tracing;
using Rayffer.PersonalPortfolio.Automator.Library.v1._0.Types;
using System;
using System.Collections.Generic;
using System.IO;
using Unity;
using Unity.Injection;
using Unity.Resolution;

namespace Rayffer.PersonalPortfolio.Automator.Library.v1._0.Factories.Action
{
    public class ActionFactory : IActionFactory
    {
        internal readonly IUnityContainer unityContainer;

        private readonly ITracing myITracing;
        private readonly IObserverMapper observerMapper;
        private readonly IDataValidator dataValidator;

        private Dictionary<ActionDataSenderTypes, Type> dataSenderTypesDictionary = new Dictionary<ActionDataSenderTypes, Type>();
        private Dictionary<ActionDataProvidersTypes, Type> dataProvidersTypesDictionary = new Dictionary<ActionDataProvidersTypes, Type>();
        private Dictionary<ActionDataTransformTypes, List<Type>> dataTransformTypesDictionary = new Dictionary<ActionDataTransformTypes, List<Type>>();
        private Dictionary<List<Type>, Guid> actionManagerTypesDictionary = new Dictionary<List<Type>, Guid>();

        public ActionFactory(IUnityContainer unityContainer,
            IObserverMapper observerMapper,
            IDataValidator dataValidator,
            ITracingFactory tracingFactory)
        {
            myITracing = (tracingFactory != null) ? tracingFactory.GetTracing(this.GetType()) : new Log4NetTracing(this.GetType());
            this.unityContainer = unityContainer.CreateChildContainer();
            this.observerMapper = observerMapper;
            this.dataValidator = dataValidator;
            ConfigureUnityContainer();
        }

        private void ConfigureUnityContainer()
        {
            ConfigureActionSenders();
            ConfigureActionDataProviders();
            ConfigureActionDataTransformers();
        }

        private void ConfigureActionDataProviders()
        {
            unityContainer.RegisterType(
                typeof(IObserverActionDataToSendProvider<>).MakeGenericType(typeof(string)),
                typeof(ObserverFileDataProvider),
                ActionDataProvidersTypes.File.ToString(),
                new InjectionConstructor(
                    new InjectionParameter<string>(null)));

            dataProvidersTypesDictionary[ActionDataProvidersTypes.File] = typeof(string);

            unityContainer.RegisterType(
                typeof(IObserverActionDataToSendProvider<>).MakeGenericType(typeof(MemoryStream)),
                typeof(ObserverFileMemoryStreamDataProvider),
                ActionDataProvidersTypes.FileMemoryStream.ToString(),
                new InjectionConstructor(
                    new InjectionParameter<string>(null)));

            dataProvidersTypesDictionary[ActionDataProvidersTypes.FileMemoryStream] = typeof(MemoryStream);
        }

        private void ConfigureActionDataTransformers()
        {
            unityContainer.RegisterType(
                typeof(IObserverActionDataTransformer<,>).MakeGenericType(typeof(string), typeof(string)),
                typeof(ObserverFileCopyToDirectoryDataTransformer),
                ActionDataTransformTypes.CopyFileToDirectory.ToString());

            dataTransformTypesDictionary[ActionDataTransformTypes.CopyFileToDirectory] = new List<Type>() { typeof(string), typeof(string) };

            unityContainer.RegisterType(
                typeof(IObserverActionDataTransformer<,>).MakeGenericType(typeof(string), typeof(string)),
                typeof(ObserverFileCompressToDirectoryDataTransformer),
                ActionDataTransformTypes.CompressFileToDirectory.ToString());

            dataTransformTypesDictionary[ActionDataTransformTypes.CompressFileToDirectory] = new List<Type>() { typeof(string), typeof(string) };

            unityContainer.RegisterType(
                typeof(IObserverActionDataTransformer<,>).MakeGenericType(typeof(MemoryStream), typeof(MemoryStream)),
                typeof(ObserverMemoryStreamPlainDataTransformer),
                ActionDataTransformTypes.PlainMemoryStream.ToString());

            dataTransformTypesDictionary[ActionDataTransformTypes.PlainMemoryStream] = new List<Type>() { typeof(MemoryStream), typeof(MemoryStream) };

            unityContainer.RegisterType(
                typeof(IObserverActionDataTransformer<,>).MakeGenericType(typeof(MemoryStream), typeof(MemoryStream)),
                typeof(ObserverMemoryStreamCompressDataTransformer),
                ActionDataTransformTypes.CompressMemoryStream.ToString());

            dataTransformTypesDictionary[ActionDataTransformTypes.CompressMemoryStream] = new List<Type>() { typeof(MemoryStream), typeof(MemoryStream) };
        }

        private void ConfigureActionSenders()
        {
            unityContainer.RegisterInstance(Mock.Of<IAutoSyncUploaderService>());

            unityContainer.RegisterType(
               typeof(IObserverActionSender<>).MakeGenericType(typeof(string)),
               typeof(ObserverSendFileToUploaderAction),
               ActionDataSenderTypes.SendFileToUploader.ToString(),
               new InjectionConstructor(
                   typeof(IAutoSyncUploaderService),
                   typeof(UploadItem)));

            dataSenderTypesDictionary[ActionDataSenderTypes.SendFileToUploader] = typeof(string);

            unityContainer.RegisterType(
               typeof(IObserverActionSender<>).MakeGenericType(typeof(MemoryStream)),
               typeof(ObserverSendMemoryStreamToUploaderAction),
               ActionDataSenderTypes.SendMemoryStreamToUploader.ToString(),
               new InjectionConstructor(
                   typeof(IAutoSyncUploaderService),
                   typeof(UploadItem)));

            dataSenderTypesDictionary[ActionDataSenderTypes.SendMemoryStreamToUploader] = typeof(MemoryStream);
        }

        public IObserverActionManager GetObserverAction(ActionInformation actionInformation)
        {
            if (!dataValidator.ValidateData(actionInformation))
            {
                throw new ArgumentException(dataValidator.GetErrorString());
            }

            unityContainer.RegisterType(
                    typeof(IObserverActionManager),
                    typeof(ObserverActionManager<,>).MakeGenericType(
                        dataProvidersTypesDictionary[actionInformation.DataProvidersType],
                        dataSenderTypesDictionary[actionInformation.DataSenderType]),
                    new InjectionConstructor
                    (
                        typeof(IObserverActionSender<>).MakeGenericType(dataSenderTypesDictionary[actionInformation.DataSenderType]),
                        typeof(IObserverActionDataToSendProvider<>).MakeGenericType(dataProvidersTypesDictionary[actionInformation.DataProvidersType]),
                        typeof(IObserverActionDataTransformer<,>).MakeGenericType(dataTransformTypesDictionary[actionInformation.DataTransformType].ToArray()),
                        typeof(ITracingFactory)
                    )
                );

            UploadItem uploadItem = CreateActionSenderUploadItem(actionInformation);

            IObserverActionManager observerActionManager = unityContainer.Resolve<IObserverActionManager>(
                new ResolverOverride[]
                {
                    new ParameterOverride("actionSender",
                        unityContainer.Resolve(
                        typeof(IObserverActionSender<>).MakeGenericType(dataSenderTypesDictionary[actionInformation.DataSenderType]),
                        actionInformation.DataSenderType.ToString(),
                        new ResolverOverride[]
                        {
                            new ParameterOverride("uploadItem", uploadItem)
                        })),
                    new ParameterOverride("dataToSendProvider",
                        unityContainer.Resolve(typeof(IObserverActionDataToSendProvider<>).MakeGenericType(dataProvidersTypesDictionary[actionInformation.DataProvidersType]),
                        actionInformation.DataProvidersType.ToString(),
                        new ResolverOverride[]
                        {
                            new ParameterOverride("filePath", actionInformation.SourceFilePath ?? string.Empty),
                            new ParameterOverride("databasePath", actionInformation.DatabaseName ?? string.Empty),
                            new ParameterOverride("username", actionInformation.DatabaseUserName ?? string.Empty),
                            new ParameterOverride("password", actionInformation.DatabasePassword ?? string.Empty)
                            //, new ParameterOverride("uploaderServiceClient", unityContainer.Resolve<IAutoSyncUploaderService>())
                        })),
                    new ParameterOverride("dataTransformer",
                        unityContainer.Resolve(typeof(IObserverActionDataTransformer<,>).MakeGenericType(dataTransformTypesDictionary[actionInformation.DataTransformType].ToArray()),
                        actionInformation.DataTransformType.ToString()))
                });

            myITracing.Information($"Resolved a new instance of an {observerActionManager.GetType()},");
            myITracing.Information($"with a provider of type {actionInformation.DataProvidersType.ToString()},");
            myITracing.Information($"with a transformer of type {actionInformation.DataTransformType.ToString()},");
            myITracing.Information($"with a sender of type {actionInformation.DataSenderType.ToString()}.");

            return observerActionManager;
        }

        private UploadItem CreateActionSenderUploadItem(ActionInformation actionInformation)
        {
            return observerMapper.Map<UploadItem>(actionInformation);
        }
    }
}