using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace AlbanianXrm.CustomizationManager.Interfaces
{
    public interface IWorkAsyncWrapper
    {
        object AsyncArgument { get; set; }
        Control Host { get; set; }
        bool IsCancelable { get; set; }
        string Message { get; set; }
        int MessageHeight { get; set; }
        int MessageWidth { get; set; }
        Action<RunWorkerCompletedEventArgs> PostWorkCallBack { get; set; }
        Action<ProgressChangedEventArgs> ProgressChanged { get; set; }
        Action<BackgroundWorker, DoWorkEventArgs> Work { get; set; }
    }
}
