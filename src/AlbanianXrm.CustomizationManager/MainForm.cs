using System.Windows.Forms;

namespace AlbanianXrm.CustomizationManager
{
    public partial class MainForm : Form
    {
        private readonly ToolViewModel toolViewModel;

        public MainForm()
        {
            toolViewModel = new ToolViewModel
            {
                MessageBroker = new MessageBoxBroker()
            };
            InitializeComponent();
            customizationManagerControl.InitializeBindings(toolViewModel);
        }
    }
}
