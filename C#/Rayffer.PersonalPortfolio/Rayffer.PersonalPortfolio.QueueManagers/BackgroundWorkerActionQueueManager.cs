using System;
using System.Collections.Concurrent;
using System.ComponentModel;
using System.Threading;

namespace Rayffer.PersonalPortfolio.QueueManagers
{
    public class BackgroundWorkerActionQueueManager : IDisposable
    {
        private BlockingCollection<Action> actionQueue;
        private readonly BackgroundWorker backgroundWorker;
        private CancellationTokenSource cancellationTokenSource;

        public BackgroundWorkerActionQueueManager()
        {
            backgroundWorker = new BackgroundWorker();
            actionQueue = new BlockingCollection<Action>();
            cancellationTokenSource = new CancellationTokenSource();
            backgroundWorker.WorkerSupportsCancellation = true;
            backgroundWorker.DoWork += BackgroundWorker_ManageQueue;
            backgroundWorker.RunWorkerAsync();
        }

        public void EnqueueAction(Action actionToEnqueue)
        {
            actionQueue.Add(actionToEnqueue);
        }

        private void BackgroundWorker_ManageQueue(object sender, DoWorkEventArgs e)
        {
            while (!backgroundWorker.CancellationPending)
            {
                Action actionToPerform = null;
                try
                {
                    actionToPerform = actionQueue.Take(cancellationTokenSource.Token);
                }
                catch (OperationCanceledException ex)
                {
                    // This exception control intends to capture only when the cancellationToken has been requested to cancel
                    break;
                }
                if (actionToPerform != null)
                {
                    actionToPerform();
                    actionToPerform = null;
                }
            }
        }

        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    backgroundWorker.CancelAsync();
                    cancellationTokenSource.Cancel();
                    backgroundWorker.DoWork -= BackgroundWorker_ManageQueue;
                    cancellationTokenSource.Dispose();
                    backgroundWorker.Dispose();
                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }
    }
}