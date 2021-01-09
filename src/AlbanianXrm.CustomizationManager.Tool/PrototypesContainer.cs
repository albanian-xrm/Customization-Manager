using AlbanianXrm.CustomizationManager.Interfaces;
using AlbanianXrm.CustomizationManager.Tool;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using WeifenLuo.WinFormsUI.Docking;

namespace AlbanianXrm.CustomizationManager
{
    public partial class PrototypesContainer : DockContent, INotifyPropertyChanged
    {
        private IMessageBroker _MessageBroker;
        private IOrganizationService _OrganizationService = null;

        public event PropertyChangedEventHandler PropertyChanged;

        public PrototypesContainer()
        {
            InitializeComponent();
        }

        public void InitializeBindings(ToolViewModel toolViewModel)
        {
            if (toolViewModel == null) throw new ArgumentNullException(nameof(toolViewModel));
            this.Bind(
                t => t.MessageBroker,
                toolViewModel,
                s => s.MessageBroker);
            this.Bind(
                t => t.OrganizationService,
                toolViewModel,
                s => s.OrganizationService);
        }

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

        private IWorkerHostWrapper _WorkerHost;
        public IWorkerHostWrapper WorkerHost
        {
            get
            {
                return this._WorkerHost;
            }
            set
            {
                this._WorkerHost = value;
                this.RaisePropertyChanged();
            }
        }


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

        internal void BtnWhoAmI_Click(object sender, EventArgs e)
        {
            var organizationService = OrganizationService;
            if (organizationService == null)
            {
                MessageBroker.Show(Resources.MISSING_ORGANIZATION_SERVICE);
                return;
            }
            var whoAmIResponse = organizationService.Execute(new WhoAmIRequest()) as WhoAmIResponse;
            MessageBroker.Show(string.Format(Resources.HELLO_USERID, whoAmIResponse.UserId));
        }

        internal void BtnEntities_Click(object sender, EventArgs e)
        {
            var organizationService = OrganizationService;
            if (organizationService == null)
            {
                MessageBroker.Show(Resources.MISSING_ORGANIZATION_SERVICE);
                return;
            }
            var retrieveAllEntitiesResponse = organizationService.Execute(new RetrieveAllEntitiesRequest()) as RetrieveAllEntitiesResponse;
            List<string> unmanagedEntities = new List<string>();
            foreach (var entityMetadata in retrieveAllEntitiesResponse.EntityMetadata)
            {
                if (!(entityMetadata.IsManaged.HasValue && entityMetadata.IsManaged == true) && entityMetadata.IsCustomizable.Value)
                {
                    unmanagedEntities.Add(entityMetadata.SchemaName);
                }
            }
            MessageBroker.Show(string.Format(Resources.UNMANAGED_ENTITIES, unmanagedEntities.Count) + "\r\n" + string.Join("\r\n", unmanagedEntities));
        }

        internal void BtnWebresources_Click(object sender, EventArgs e)
        {
            var organizationService = OrganizationService;
            if (organizationService == null)
            {
                MessageBroker.Show(Resources.MISSING_ORGANIZATION_SERVICE);
                return;
            }

            var webResourcesQuery = new QueryExpression("webresource");


            var retrieveAllEntitiesResponse = organizationService.RetrieveMultiple(webResourcesQuery);
            List<string> unmanagedEntities = new List<string>();
            foreach (var entityMetadata in retrieveAllEntitiesResponse.Entities)
            {

            }
            MessageBroker.Show(string.Format(Resources.UNMANAGED_ENTITIES, unmanagedEntities.Count) + "\r\n" + string.Join("\r\n", unmanagedEntities));
        }

        private void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
