using AlbanianXrm.CustomizationManager.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace AlbanianXrm.CustomizationManager.Helpers
{
    class DummyWorkerHostWrapper : IWorkerHostWrapper
    {
        public int Working { get; set; }
        private Queue<BackgroundWorker> queueWorkers = new Queue<BackgroundWorker>();

        public void WorkAsync(IWorkAsyncWrapper info)
        {
            Working += 1;
            var backgroundWorker = new BackgroundWorker();
            queueWorkers.Enqueue(backgroundWorker);

            if (info.Work == null && info.PostWorkCallBack == null)
            {
                throw new Exception("No Work to be done");
            }

            backgroundWorker.DoWork += new DoWorkEventHandler((sender, args) =>
            {
                info.Work?.Invoke(backgroundWorker, args);
            });

            backgroundWorker.ProgressChanged += new ProgressChangedEventHandler((sender, args) =>
            {
                info.ProgressChanged?.Invoke(args);
            });

            backgroundWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler((sender, args) =>
            {
                info.PostWorkCallBack?.Invoke(args);
                queueWorkers.Dequeue();
                Working -= 1;
            });

            backgroundWorker.RunWorkerAsync(info.AsyncArgument);
        }
    }
}
