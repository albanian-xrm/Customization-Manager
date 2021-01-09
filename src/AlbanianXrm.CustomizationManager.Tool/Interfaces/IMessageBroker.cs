using System.Windows.Forms;

namespace AlbanianXrm.CustomizationManager.Interfaces
{
    public interface IMessageBroker
    {
        DialogResult Show(string message);
    }
}
