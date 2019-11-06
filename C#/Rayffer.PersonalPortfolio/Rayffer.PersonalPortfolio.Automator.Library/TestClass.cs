using Rayffer.PersonalPortfolio.Automator.Library.v1._0.DataValidators;
using Rayffer.PersonalPortfolio.Automator.Library.v1._0.DTOs;
using Rayffer.PersonalPortfolio.Automator.Library.v1._0.Factories.Checks;
using Rayffer.PersonalPortfolio.Automator.Library.v1._0.Interfaces;
using Rayffer.PersonalPortfolio.Automator.Library.v1._0.Providers.MemoryProviders;
using Rayffer.PersonalPortfolio.Automator.Library.v1._0.Types;
using System;
using Unity;

namespace Rayffer.PersonalPortfolio.Automator.Library
{
    public class TestClass
    {
        public TestClass()
        {
            UnityContainer unityContainer = new UnityContainer();
            unityContainer.RegisterType<IObserverMemoryProvider, NativeCacheMemoryProvider>();
            unityContainer.RegisterType<IDataValidator, DataValidator>();

            unityContainer.RegisterType<IDataValidator, ActionInformationValidator>("ActionDataValidator");

            unityContainer.RegisterType<ITracingFactory, Log4NetTracingFactory>();

            CheckFactory checkFactory = new CheckFactory(unityContainer, new Log4NetTracingFactory());

            checkFactory.GetObserverCheck(new CheckInformation()
            {
                CheckType = CheckTypes.None,
                Identifier = Guid.NewGuid(),
                HashType = HashingTypes.None
            });

            //UnityContainer mainContainer = new UnityContainer();

            //ConfigureUnity.Configure(mainContainer);

            ////ICheckFactory checkFactory = mainContainer.Resolve<ICheckFactory>();

            ////var p = checkFactory.GetObserverCheck(
            ////    new CheckInformation()
            ////    {
            ////        CheckType = Rayffer.PersonalPortfolio.Automator.Library.v1._0.Types.CheckTypes.CheckHash,
            ////        Identifier = Guid.NewGuid(),
            ////        FilePath = @"C:\Cajero\Prueba.txt",
            ////        HashType = Rayffer.PersonalPortfolio.Automator.Library.v1._0.Types.HashingTypes.Md5,
            ////    });
            ////p.PerformCheck();
            //IObserverService observerService = mainContainer.Resolve<IObserverService>();

            //ActionFactory triggerFactory = new ActionFactory(new UnityContainer());

            //var intervalChangeTrigger = triggerFactory.GetObserverAction(new v1._0.DTOs.ActionInformation()
            //{
            //    DataSenderType = v1._0.Types.ActionDataSenderTypes.SendToConsole,
            //    DataProvidersType = v1._0.Types.ActionDataProvidersTypes.File,
            //    DataTransformType = v1._0.Types.ActionDataTransformTypes.Compress,
            //    SourceFilePath = @"C:\test.txt"
            //});

            //intervalChangeTrigger.PerformAction();
            //Observer observerMoveFileByInterval = new Observer(
            //    intervalChangeTrigger,
            //    new List<IObserverCheck>() {
            //        new DummyCheck()
            //     },
            //    new List<IObserverAction>() {
            //        new ObserverActionBase(
            //            new SendToDirectory(@"C:\test.txt"),
            //            new ObserverFileDataProvider(@"C:\Lab\test.txt"),
            //            new ObserverStringPlainDataTransformer())
            //    });

            //intervalChangeTrigger = triggerFactory.GetObserverEventTrigger(new v1._0.DTOs.EventTriggerInformation()
            //{
            //    IntervalSeconds = 5,
            //    EventTriggerType = v1._0.Types.EventTriggerTypes.Interval
            //});

            //Observer observerWriteToConsoleByInterval = new Observer(
            //    intervalChangeTrigger,
            //    new List<IObserverCheck>() {
            //        new DummyCheck()
            //     },
            //    new List<IObserverAction>() {
            //        new ObserverActionBase(
            //            new SendToConsole(),
            //            new ObserverFileDataProvider(@"C:\Lab\test.txt"),
            //            new ObserverStringPlainDataTransformer())
            //    });

            //observerMoveFileByInterval.StartObserver();
            //observerWriteToConsoleByInterval.StartObserver();
        }
    }
}