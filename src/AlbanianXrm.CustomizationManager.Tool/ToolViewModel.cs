using AlbanianXrm.CustomizationManager.Interfaces;
using AlbanianXrm.CustomizationManager.Models;
using Microsoft.Xrm.Sdk;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace AlbanianXrm.CustomizationManager
{
    public class ToolViewModel : INotifyPropertyChanged
    {
        private bool _AllowRequests = true;
        private bool _SolutionsFilter_Enabled = true;
        private AsyncWorkQueue _AsyncWorkQueue;
        private IMessageBroker _MessageBroker;
        private IOrganizationService _OrganizationService;
        private readonly BindingList<Solution> _Solutions = new BindingList<Solution>() { RaiseListChangedEvents = true };
        private readonly BindingList<SolutionComponent> _SolutionComponents = new BindingList<SolutionComponent> { RaiseListChangedEvents = true };

        public event PropertyChangedEventHandler PropertyChanged;

        public bool AllowRequests
        {
            get
            {
                return _AllowRequests;
            }
            set
            {
                if (_AllowRequests == value)
                {
                    return;
                }
                _AllowRequests = value;
                if (_SolutionsFilter_Enabled)
                    RaisePropertyChanged(nameof(SolutionsFilter_Enabled));
                RaisePropertyChanged();
            }
        }

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

        public BindingList<Solution> Solutions
        {
            get
            {
                return _Solutions;
            }
        }

        public BindingList<SolutionComponent> SolutionComponents
        {
            get
            {
                return _SolutionComponents;
            }
        }

        public bool SolutionsFilter_Enabled
        {
            get
            {
                return _SolutionsFilter_Enabled && _AllowRequests;
            }
            set
            {
                if (_SolutionsFilter_Enabled == value)
                {
                    return;
                }
                _SolutionsFilter_Enabled = value;
                RaisePropertyChanged();
            }
        }

        private void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
