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

        public void WorkAsync(IWorkAsyncWrapper info)
        {
            if (info == null) throw new ArgumentNullException(nameof(info));
            var workAsyncInfo = new WorkAsyncInfo();
            if (info.AsyncArgument != null)
            {
                workAsyncInfo.AsyncArgument = info.AsyncArgument;
            }
            if (info.Host != null)
            {
                workAsyncInfo.Host = info.Host;
            }
            if (info.IsCancelable)
            {
                workAsyncInfo.IsCancelable = info.IsCancelable;
            }
            if (info.Message != null)
            {
                workAsyncInfo.Message = info.Message;
            }          
            if (info.MessageHeight > 0)
            {
                workAsyncInfo.MessageHeight = info.MessageHeight;
            }
            if (info.MessageWidth > 0)
            {
                workAsyncInfo.MessageWidth = info.MessageWidth;
            }
            if (info.PostWorkCallBack != null)
            {
                workAsyncInfo.PostWorkCallBack = info.PostWorkCallBack;
            }
            if (info.ProgressChanged != null)
            {
                workAsyncInfo.ProgressChanged = info.ProgressChanged;
            }
            if (info.Work != null)
            {
                workAsyncInfo.Work = info.Work;
            }
            WorkAsync(workAsyncInfo);
        }
    }
}
