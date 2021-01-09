using AlbanianXrm.CustomizationManager.Interfaces;
using System.Windows.Forms;

namespace AlbanianXrm.CustomizationManager
{
    class MessageBoxBroker : IMessageBroker
    {
        public DialogResult Show(string message)
        {
            return MessageBox.Show(message);
        }
    }
}
