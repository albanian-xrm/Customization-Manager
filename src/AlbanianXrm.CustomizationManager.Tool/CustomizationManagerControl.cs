using AlbanianXrm.CustomizationManager.Interfaces;
using Microsoft.Xrm.Sdk;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

[assembly: InternalsVisibleTo("AlbanianXrm.CustomizationManager.Tool.Tests")]
[assembly: InternalsVisibleTo("DynamicProxyGenAssembly2")]
namespace AlbanianXrm.CustomizationManager
{

    public partial class CustomizationManagerControl : UserControl, INotifyPropertyChanged
    {
        internal SolutionComponentsContainer solutionComponentsContainer;
        internal PrototypesContainer prototypesContainer;

        public CustomizationManagerControl()
        {
            InitializeComponent();
            solutionComponentsContainer = new SolutionComponentsContainer();
            solutionComponentsContainer.Show(dockPanel, DockState.DockLeft);
            prototypesContainer = new PrototypesContainer();
            prototypesContainer.Show(dockPanel, DockState.DockRight);
        }

        public void InitializeBindings(ToolViewModel toolViewModel)
        {
            if (toolViewModel == null) throw new ArgumentNullException(nameof(toolViewModel));
            this.Bind(
                t => t.AsyncWorkQueue,
                toolViewModel,
                s => s.AsyncWorkQueue);
            this.Bind(
                t => t.MessageBroker,
                toolViewModel,
                s => s.MessageBroker);
            this.Bind(
                t => t.OrganizationService,
                toolViewModel,
                s => s.OrganizationService);
            solutionComponentsContainer.InitializeBindings(toolViewModel);
            prototypesContainer.InitializeBindings(toolViewModel);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private IMessageBroker _MessageBroker;
        public IMessageBroker MessageBroker
        {
            get
            {
                return this._MessageBroker;
            }
            set
            {
                this._MessageBroker = value;
                this.RaisePropertyChanged();
            }
        }

        private AsyncWorkQueue _AsyncWorkQueue;
        public AsyncWorkQueue AsyncWorkQueue
        {
            get
            {
                return this._AsyncWorkQueue;
            }
            set
            {
                this._AsyncWorkQueue = value;
                this.RaisePropertyChanged();
            }
        }

        private IOrganizationService _OrganizationService = null;
        public IOrganizationService OrganizationService
        {
            get
            {
                return this._OrganizationService;
            }
            set
            {
                this._OrganizationService = value;
                this.RaisePropertyChanged();
            }
        }

        private void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
