using System;
using System.Collections.Concurrent;
using System.ComponentModel;
using System.Threading;

namespace Rayffer.PersonalPortfolio.QueueManagers
{
    /// <summary>
    /// This is a class that performs task asynchronously in a fire and forget fashion, the ActionQueue is processed as a FIFO queue.
    /// The user does not know when the action will complete, only that it will if no exceptions are thrown.
    /// </summary>
    public class BackgroundWorkerActionQueueManager : IDisposable
    {
        public bool IsBusy { get; private set; }

        private readonly BlockingCollection<Action> actionQueue;
        private readonly BackgroundWorker backgroundWorker;
        private CancellationTokenSource cancellationTokenSource;
        public RunWorkerCompletedEventHandler runWorkerCompletedEventHandler;

        public BackgroundWorkerActionQueueManager()
        {
            backgroundWorker = new BackgroundWorker();
            actionQueue = new BlockingCollection<Action>();
            backgroundWorker.WorkerSupportsCancellation = true;
            backgroundWorker.DoWork += BackgroundWorker_ManageQueue;
        }

        /// <summary>
        /// This constructor provides the option to be notified with an event when an action has finished due to an exception
        /// </summary>
        /// <param name="runWorkerCompletedEventHandler">
        /// The method that will handle the completion of the background worker work or exception that has been thrown
        /// </param>
        public BackgroundWorkerActionQueueManager(RunWorkerCompletedEventHandler runWorkerCompletedEventHandler) : base()
        {
            this.runWorkerCompletedEventHandler = runWorkerCompletedEventHandler;
            backgroundWorker.RunWorkerCompleted += runWorkerCompletedEventHandler;
        }

        /// <summary>
        /// This methods enqueues an action to be performed by the backgroundworker in the class's queue, the action can be anything that the user
        /// can come up with, mind that the user has to control the exceptions that are thrown.
        /// </summary>
        /// <param name="actionToEnqueue">
        /// The action to enqueue
        /// </param>
        public void EnqueueAction(Action actionToEnqueue)
        {
            actionQueue.Add(actionToEnqueue);
            if (!backgroundWorker.IsBusy)
                backgroundWorker.RunWorkerAsync();
        }

        /// <summary>
        /// This method manages the queue, taking actions from the queue and performing them. It won't process the next action (if any) until the current
        /// one has finished. Once it finishes an action, it will inmediately take another one if the queue still holds items or wait for the following
        /// action to be enqueued.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BackgroundWorker_ManageQueue(object sender, DoWorkEventArgs e)
        {
            while (!backgroundWorker.CancellationPending)
            {
                Action actionToPerform = null;
                try
                {
                    IsBusy = false;
                    cancellationTokenSource = new CancellationTokenSource();
                    actionToPerform = actionQueue.Take(cancellationTokenSource.Token);
                    IsBusy = true;
                }
                catch (OperationCanceledException ex)
                {
                    // This exception control intends to capture only when the cancellationToken has been requested to cancel
                    break;
                }
                if (actionToPerform != null)
                {
                    actionToPerform.Invoke();
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
                    if (runWorkerCompletedEventHandler != null)
                    {
                        backgroundWorker.RunWorkerCompleted -= runWorkerCompletedEventHandler;
                        runWorkerCompletedEventHandler = null;
                    }
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