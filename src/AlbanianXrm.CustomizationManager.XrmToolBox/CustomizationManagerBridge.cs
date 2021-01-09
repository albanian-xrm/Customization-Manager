using AlbanianXrm.CustomizationManager.Interfaces;
using McTools.Xrm.Connection;
using Microsoft.Xrm.Sdk;
using System;
using XrmToolBox.Extensibility;
using XrmToolBox.Extensibility.Interfaces;

namespace AlbanianXrm.CustomizationManager
{
    internal partial class CustomizationManagerBridge : PluginControlBase, IGitHubPlugin, IWorkerHostWrapper
    {
        private readonly ToolViewModel toolViewModel;

        /// <summary>
        /// GitHub repository 
        /// </summary>
        public string RepositoryName => "Customization-Manager";

        /// <summary>
        /// GitHub UserName
        /// </summary>
        public string UserName => "albanian-xrm";

        /// <summary>
        /// Constructor
        /// </summary>
        public CustomizationManagerBridge()
        {

            toolViewModel = new ToolViewModel
            {
                MessageBroker = new MessageBoxBroker(),
                OrganizationService = Service
            };
            toolViewModel.AsyncWorkQueue = new AsyncWorkQueue(this, toolViewModel);
            InitializeComponent();
            customizationManagerControl.InitializeBindings(toolViewModel);
        }

        public override void UpdateConnection(IOrganizationService newService, ConnectionDetail detail, string actionName, object parameter)
        {
            base.UpdateConnection(newService, detail, actionName, parameter);
            toolViewModel.OrganizationService = newService;
        }

        public void WorkAsync(IWorkAsyncWrapper workAsyncWrapper)
        {
            if (workAsyncWrapper == null) throw new ArgumentNullException(nameof(workAsyncWrapper));
            var workAsyncInfo = new WorkAsyncInfo();
            if (workAsyncWrapper.AsyncArgument != null)
            {
                workAsyncInfo.AsyncArgument = workAsyncWrapper.AsyncArgument;
            }
            if (workAsyncWrapper.Host != null)
            {
                workAsyncInfo.Host = workAsyncWrapper.Host;
            }
            if (workAsyncWrapper.IsCancelable)
            {
                workAsyncInfo.IsCancelable = workAsyncWrapper.IsCancelable;
            }
            if (workAsyncWrapper.Message != null)
            {
                workAsyncInfo.Message = workAsyncWrapper.Message;
            }          
            if (workAsyncWrapper.MessageHeight > 0)
            {
                workAsyncInfo.MessageHeight = workAsyncWrapper.MessageHeight;
            }
            if (workAsyncWrapper.MessageWidth > 0)
            {
                workAsyncInfo.MessageWidth = workAsyncWrapper.MessageWidth;
            }
            if (workAsyncWrapper.PostWorkCallBack != null)
            {
                workAsyncInfo.PostWorkCallBack = workAsyncWrapper.PostWorkCallBack;
            }
            if (workAsyncWrapper.ProgressChanged != null)
            {
                workAsyncInfo.ProgressChanged = workAsyncWrapper.ProgressChanged;
            }
            if (workAsyncWrapper.Work != null)
            {
                workAsyncInfo.Work = workAsyncWrapper.Work;
            }
            WorkAsync(workAsyncInfo);
        }
    }
}
