using System.CodeDom.Compiler;
using System.Windows.Forms;

namespace AlbanianXrm.CustomizationManager
{

    public partial class CustomizationManagerControl
    {      
        [GeneratedCode("Form Designer", "0.0.0.0")]
        private void InitializeComponent()
        {
            this.dockPanel = new WeifenLuo.WinFormsUI.Docking.DockPanel();
            this.SuspendLayout();
            // 
            // dockPanel
            // 
            this.dockPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dockPanel.DocumentStyle = WeifenLuo.WinFormsUI.Docking.DocumentStyle.DockingWindow;
            this.dockPanel.Location = new System.Drawing.Point(0, 0);
            this.dockPanel.Name = "dockPanel";
            this.dockPanel.Size = new System.Drawing.Size(735, 394);
            this.dockPanel.TabIndex = 3;
            // 
            // CustomizationManagerControl
            // 
            this.Controls.Add(this.dockPanel);
            this.Name = "CustomizationManagerControl";
            this.Size = new System.Drawing.Size(735, 394);
            this.ResumeLayout(false);

        }
        private WeifenLuo.WinFormsUI.Docking.DockPanel dockPanel;
    }
}
