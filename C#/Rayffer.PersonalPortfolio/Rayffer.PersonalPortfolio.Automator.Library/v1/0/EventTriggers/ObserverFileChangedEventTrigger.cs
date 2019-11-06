using Rayffer.PersonalPortfolio.Automator.Library.v1._0.Interfaces;
using System.IO;

namespace Rayffer.PersonalPortfolio.Automator.Library.v1._0.EventTriggers
{
    public class ObserverFileChangedEventTrigger : ObserverEventTriggerBase
    {
        private FileSystemWatcher fileWatcher;

        public ObserverFileChangedEventTrigger(
            IDataValidator dataValidator,
            ITracingFactory tracingFactory,
            string directoryToWatch,
            string fileToWatch) : base(dataValidator, tracingFactory)
        {
            if (!dataValidator.ValidateData(directoryToWatch))
            {
                throw new DirectoryNotFoundException(dataValidator.GetErrorString());
            }
            if (!dataValidator.ValidateData(fileToWatch))
            {
                throw new FileNotFoundException(dataValidator.GetErrorString());
            }
            fileWatcher = new FileSystemWatcher(directoryToWatch, fileToWatch);
        }

        private void FileWatcher_Changed(object sender, FileSystemEventArgs e)
        {
            myITracing.Information("Event Fired Start");
            base.OnEvent();
            myITracing.Information("Event Fired End");
        }

        public override void SubscribeAndStartEventTrigger()
        {
            SubscribeEvent();
            fileWatcher.EnableRaisingEvents = true;
        }

        public override void StopAndUnsuscribeEventTrigger()
        {
            fileWatcher.EnableRaisingEvents = false;
            UnsubscribeEvent();
        }

        protected override void SubscribeEvent()
        {
            fileWatcher.Changed += FileWatcher_Changed;
        }

        protected override void UnsubscribeEvent()
        {
            fileWatcher.Changed -= FileWatcher_Changed;
        }
    }
}