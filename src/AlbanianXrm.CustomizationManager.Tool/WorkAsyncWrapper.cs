using AlbanianXrm.CustomizationManager.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AlbanianXrm.CustomizationManager
{
    public class WorkAsyncWrapper : IWorkAsyncWrapper
    {
        public object AsyncArgument { get; set; }
        public Control Host { get; set; }
        public bool IsCancelable { get; set; }
        public string Message { get; set; }
        public int MessageHeight { get; set; }
        public int MessageWidth { get; set; }
        public Action<RunWorkerCompletedEventArgs> PostWorkCallBack { get; set; }
        public Action<ProgressChangedEventArgs> ProgressChanged { get; set; }
        public Action<BackgroundWorker, DoWorkEventArgs> Work { get; set; }
    }
}
