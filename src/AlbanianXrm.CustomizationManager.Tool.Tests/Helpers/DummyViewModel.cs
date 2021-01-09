using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace AlbanianXrm.CustomizationManager.Helpers
{
    class DummyViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public string _DummyString;

        public string DummyString
        {
            get
            {
                return _DummyString;
            }
            set
            {
                _DummyString = value;
                NotifyPropertyChanged();
            }
        }

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
