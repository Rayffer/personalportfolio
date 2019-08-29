using Rayffer.PersonalPortfolio.Generators;
using Rayffer.PersonalPortfolio.QueueManagers;
using Rayffer.PersonalPortfolio.Sorters;
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
            Semaphore semaphore = new Semaphore(1, 1);
            BackgroundWorkerActionQueueManager actionQueueManager = new BackgroundWorkerActionQueueManager();

            actionQueueManager.EnqueueAction(() => semaphore.WaitOne());
            actionQueueManager.EnqueueAction(() => Console.WriteLine("First console append"));
            actionQueueManager.EnqueueAction(() => Thread.Sleep(2000));
            actionQueueManager.EnqueueAction(() => Console.WriteLine("Second console append"));
            actionQueueManager.EnqueueAction(() => semaphore.Release());

            Thread.Sleep(500);

            semaphore.WaitOne();

            Console.WriteLine("The backgroundworker has ended its tasks, press enter to exit");

            Console.ReadLine();
        }
    }
}