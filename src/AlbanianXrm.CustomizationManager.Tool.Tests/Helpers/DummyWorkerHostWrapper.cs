using AlbanianXrm.CustomizationManager.Interfaces;
using FakeItEasy;
using System.ComponentModel;

namespace AlbanianXrm.CustomizationManager.Helpers
{
    class DummyWorkerHostWrapper : IWorkerHostWrapper
    { 
        public BackgroundWorker BackgroundWorker { get; set; }

        public DoWorkEventArgs DoWorkEventArgs { get; set; }

        public RunWorkerCompletedEventArgs RunWorkerCompletedEventArgs { get; set; }

        public virtual BackgroundWorker CreateBackgroundWorker()
        {
            return A.Fake<BackgroundWorker>();
        }

        public virtual DoWorkEventArgs CreateWorkEventArgs(object asyncArgument)
        {
            return A.Fake<DoWorkEventArgs>((o) => o.WithArgumentsForConstructor(new object[] { asyncArgument }));
        }

        public virtual RunWorkerCompletedEventArgs CreateWorkCompletedEventArgs(DoWorkEventArgs doWorkEventArgs)
        {
            return A.Fake<RunWorkerCompletedEventArgs>();
        }

        public void WorkAsync(IWorkAsyncWrapper info)
        {
            BackgroundWorker = CreateBackgroundWorker();
            DoWorkEventArgs = CreateWorkEventArgs(info.AsyncArgument);
            info.Work?.Invoke(BackgroundWorker, DoWorkEventArgs);
            RunWorkerCompletedEventArgs = CreateWorkCompletedEventArgs(DoWorkEventArgs);
            info.PostWorkCallBack?.Invoke(RunWorkerCompletedEventArgs);
         }
    }
}
