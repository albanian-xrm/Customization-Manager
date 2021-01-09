using AlbanianXrm.CustomizationManager.Interfaces;
using AlbanianXrm.CustomizationManager.Models;
using AlbanianXrm.CustomizationManager.Tool;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using WeifenLuo.WinFormsUI.Docking;

namespace AlbanianXrm.CustomizationManager
{
    public partial class SolutionComponentsContainer : DockContent, INotifyPropertyChanged
    {
        private IMessageBroker _MessageBroker;
        private IOrganizationService _OrganizationService = null;
        private readonly BindingList<SolutionComponentItem> solutionComponentItems = new BindingList<SolutionComponentItem>() { RaiseListChangedEvents = true };

        public event PropertyChangedEventHandler PropertyChanged;
        private ToolViewModel toolViewModel;

        public SolutionComponentsContainer()
        {
            InitializeComponent();
            lstSolutionItems.DataSource = solutionComponentItems;
            solutionComponentItems.Add(new SolutionComponentItem() { ComponentType = OptionSets.ComponentType.Entity, ComponentsInSolution = 0 });
            solutionComponentItems.Add(new SolutionComponentItem() { ComponentType = OptionSets.ComponentType.WebResource, ComponentsInSolution = 0 });
        }

        public void InitializeBindings(ToolViewModel toolViewModel)
        {
            this.toolViewModel = toolViewModel ?? throw new ArgumentNullException(nameof(toolViewModel));
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
            cmbFilteringSolution.Bind(
                t => t.Enabled,
                toolViewModel,
                s => s.SolutionsFilter_Enabled);
            lstSolutionItems.Bind(
                t => t.Enabled,
                toolViewModel,
                s => s.AllowRequests);

            cmbFilteringSolution.DataSource = toolViewModel.Solutions;
            cmbFilteringSolution.DisplayMember = nameof(Solution.FriendlyName);
            cmbFilteringSolution.ValueMember = nameof(Solution.Id);
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

        internal void MnuRefreshSolutions_Click(object sender, EventArgs e)
        {
            toolViewModel.SolutionsFilter_Enabled = false;
            AsyncWorkQueue.Enqueue(new WorkAsyncWrapper()
            {
                Message = Resources.REFRESHING_SOLUTION_LIST,
                Work = RefreshSolutionList,
                PostWorkCallBack = RefreshSolutionList
            });
        }

        internal void CmbFilteringSolution_DropDown(object sender, EventArgs e)
        {
            if (cmbFilteringSolution.Items.Count != 0)
            {
                return;
            }
            MnuRefreshSolutions_Click(sender, e);
        }

        internal void CmbFilteringSolution_SelectedValueChanged(object sender, EventArgs e)
        {
            if (!(cmbFilteringSolution.SelectedItem is Solution selectedSolution))
            {
                return;
            }
            AsyncWorkQueue.Enqueue(new WorkAsyncWrapper()
            {
                AsyncArgument = selectedSolution,
                Message = string.Format(Resources.REFRESHING_SOLUTION_COMPONENTS, selectedSolution.UniqueName),
                Work = RefreshSolutionComponentList,
                PostWorkCallBack = RefreshSolutionComponentList
            }); ;
        }

        internal void RefreshSolutionList(BackgroundWorker worker, DoWorkEventArgs args)
        {
            var service = OrganizationService;
            var query = new QueryExpression(Solution.EntityLogicalName)
            {
                ColumnSet = new ColumnSet(Solution.Fields.FriendlyName, Solution.Fields.UniqueName, Solution.Fields.SolutionId)
            };
            args.Result = service.RetrieveAll(query);
        }

        internal void RefreshSolutionList(RunWorkerCompletedEventArgs args)
        {
            var response = args.Result as List<Entity>;
            toolViewModel.Solutions.Clear();
            foreach (var solution in response)
            {
                toolViewModel.Solutions.Add(solution.ToEntity<Solution>());
            }
            toolViewModel.SolutionsFilter_Enabled = true;
            if (!(cmbFilteringSolution.SelectedItem is Solution selectedSolution))
            {
                selectedSolution = new Solution() { UniqueName = "Default" };
            }
            selectedSolution = toolViewModel.Solutions.FirstOrDefault(s => s.UniqueName == selectedSolution.UniqueName);
            if (selectedSolution == null)
            {
                selectedSolution = toolViewModel.Solutions.FirstOrDefault(s => s.UniqueName == "Default");
            }
            cmbFilteringSolution.SelectedItem = selectedSolution;
        }

        internal void RefreshSolutionComponentList(BackgroundWorker worker, DoWorkEventArgs args)
        {
            var service = OrganizationService;
            var solutionId = (args.Argument as Solution).Id;
            var query = new QueryExpression(SolutionComponent.EntityLogicalName)
            {
                ColumnSet = new ColumnSet(SolutionComponent.Fields.ComponentType, SolutionComponent.Fields.SolutionId)
            };
            query.Criteria.AddCondition(SolutionComponent.Fields.SolutionId, ConditionOperator.Equal, solutionId);
            args.Result = service.RetrieveAll(query);
        }

        internal void RefreshSolutionComponentList(RunWorkerCompletedEventArgs args)
        {
            var response = args.Result as List<Entity>;
            toolViewModel.SolutionComponents.Clear();
            foreach (var solutionComponent in response)
            {
                toolViewModel.SolutionComponents.Add(solutionComponent.ToEntity<SolutionComponent>());
            }
            toolViewModel.SolutionsFilter_Enabled = true;
            foreach (var solutionComponentItem in solutionComponentItems)
            {
                solutionComponentItem.ComponentsInSolution = toolViewModel.SolutionComponents.Count(s => s.ComponentType?.Value == (int)solutionComponentItem.ComponentType);
            }
        }

        private void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
