using AlbanianXrm.CustomizationManager.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace AlbanianXrm.CustomizationManager
{
    public class AsyncWorkQueue
    {
        private readonly Queue<Job> queue;
        private readonly ToolViewModel toolViewModel;
        private readonly IWorkerHostWrapper solutionPackagerControl;

        public AsyncWorkQueue(IWorkerHostWrapper solutionPackagerControl, ToolViewModel toolViewModel)
        {
            this.queue = new Queue<Job>();
            this.solutionPackagerControl = solutionPackagerControl;
            this.toolViewModel = toolViewModel;
        }

        public void Enqueue(IWorkAsyncWrapper work)
        {
            var job = new Job(this, work);
            if (!queue.Any())
            {
                toolViewModel.AllowRequests = false;
                solutionPackagerControl.WorkAsync(job.Work);
            }
            queue.Enqueue(job);
        }

        private void WorkAsyncEnded()
        {
            queue.Dequeue();
            if (queue.Any())
            {
                solutionPackagerControl.WorkAsync(queue.Peek().Work);
            }
            else
            {
                toolViewModel.AllowRequests = true;
            }
        }

        private class Job
        {
            public IWorkAsyncWrapper Work { get; private set; }
            private readonly Action<RunWorkerCompletedEventArgs> postWorkCallBack;
            private readonly AsyncWorkQueue queue;

            public Job(AsyncWorkQueue queue, IWorkAsyncWrapper work)
            {
                this.queue = queue ?? throw new ArgumentNullException(nameof(queue));
                this.Work = work ?? throw new ArgumentNullException(nameof(work));
                this.postWorkCallBack = work.PostWorkCallBack;
                this.Work.PostWorkCallBack = PostWorkCallBack;
            }

            private void PostWorkCallBack(RunWorkerCompletedEventArgs args)
            {
                try
                {
                    postWorkCallBack?.Invoke(args);
                }
                finally
                {
                    this.queue.WorkAsyncEnded();
                }
            }
        }
    }
}
