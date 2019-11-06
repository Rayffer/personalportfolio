using Rayffer.PersonalPortfolio.Automator.Library.v1._0.Interfaces;
using System;
using System.IO;

namespace Rayffer.PersonalPortfolio.Automator.Library.v1._0.EventTriggers
{
    public class ObserverNewFileInDirectoryEventTrigger : ObserverEventTriggerBase
    {
        private readonly FileSystemWatcher fileWatcher;

        public ObserverNewFileInDirectoryEventTrigger(
            IDataValidator dataValidator,
            ITracingFactory tracingFactory,
            string directoryToWatch,
            string fileFilter = "") : base(dataValidator, tracingFactory)
        {
            if (!dataValidator.ValidateData(directoryToWatch))
            {
                throw new DirectoryNotFoundException(dataValidator.GetErrorString());
            }
            if (string.IsNullOrEmpty(fileFilter))
            {
                fileWatcher = new FileSystemWatcher(directoryToWatch);
            }
            else
            {
                if (!dataValidator.ValidateData(fileFilter))
                {
                    throw new ArgumentException(dataValidator.GetErrorString());
                }
                fileWatcher = new FileSystemWatcher(directoryToWatch, fileFilter);
            }
        }

        private void FileWatcher_Created(object sender, FileSystemEventArgs e)
        {
            myITracing.Information("Event Fired Start");
            base.OnEvent();
            myITracing.Information("Event Fired Ended");
        }

        public override void SubscribeAndStartEventTrigger()
        {
            fileWatcher.Created += FileWatcher_Created;
            fileWatcher.EnableRaisingEvents = true;
        }

        public override void StopAndUnsuscribeEventTrigger()
        {
            fileWatcher.EnableRaisingEvents = false;
            fileWatcher.Created -= FileWatcher_Created;
        }

        protected override void UnsubscribeEvent()
        {
            fileWatcher.Created -= FileWatcher_Created;
        }

        protected override void SubscribeEvent()
        {
            fileWatcher.Created += FileWatcher_Created;
        }
    }
}