using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace AlbanianXrm.CustomizationManager.Models
{
    internal class SolutionComponentItem : INotifyPropertyChanged
    {
        private int _ComponentsInSolution;

        public event PropertyChangedEventHandler PropertyChanged;

        public string Name { get { return $"{ComponentType} ({ComponentsInSolution})"; } }

        public OptionSets.ComponentType ComponentType { get; set; }

        public int ComponentsInSolution
        {
            get
            {
                return _ComponentsInSolution;
            }
            set
            {
                if (_ComponentsInSolution == value)
                {
                    return;
                }
                _ComponentsInSolution = value;
                RaisePropertyChanged();
                RaisePropertyChanged(nameof(Name));
            }
        }

        private void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
