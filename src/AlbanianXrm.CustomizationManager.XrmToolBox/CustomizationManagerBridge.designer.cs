using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlbanianXrm.CustomizationManager
{
    internal partial class CustomizationManagerBridge
    {
        private CustomizationManagerControl customizationManagerControl;

        private void InitializeComponent()
        {
            this.customizationManagerControl = new AlbanianXrm.CustomizationManager.CustomizationManagerControl();
            this.SuspendLayout();
            // 
            // customizationManagerControl
            // 
            this.customizationManagerControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.customizationManagerControl.Location = new System.Drawing.Point(0, 0);
            this.customizationManagerControl.Name = "customizationManagerControl";
            this.customizationManagerControl.Size = new System.Drawing.Size(150, 150);
            this.customizationManagerControl.TabIndex = 0;
            // 
            // CustomizationManagerBridge
            // 
            this.Controls.Add(this.customizationManagerControl);
            this.Name = "CustomizationManagerBridge";
            this.ResumeLayout(false);

        }
    }
}
