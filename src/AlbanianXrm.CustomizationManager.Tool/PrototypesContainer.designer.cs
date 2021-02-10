using System.CodeDom.Compiler;

namespace AlbanianXrm.CustomizationManager
{


    public partial class PrototypesContainer
    {
        [GeneratedCode("Form Designer", "0.0.0.0")]
        private void InitializeComponent()
        {
            this.pnlContainer = new System.Windows.Forms.TableLayoutPanel();
            this.btnWebResources = new System.Windows.Forms.Button();
            this.btnWhoAmI = new System.Windows.Forms.Button();
            this.btnEntities = new System.Windows.Forms.Button();
            this.pnlContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlContainer
            // 
            this.pnlContainer.ColumnCount = 1;
            this.pnlContainer.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.pnlContainer.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.pnlContainer.Controls.Add(this.btnWebResources, 0, 2);
            this.pnlContainer.Controls.Add(this.btnWhoAmI, 0, 0);
            this.pnlContainer.Controls.Add(this.btnEntities, 0, 1);
            this.pnlContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlContainer.Location = new System.Drawing.Point(0, 0);
            this.pnlContainer.Name = "pnlContainer";
            this.pnlContainer.RowCount = 4;
            this.pnlContainer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34F));
            this.pnlContainer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34F));
            this.pnlContainer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34F));
            this.pnlContainer.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.pnlContainer.Size = new System.Drawing.Size(284, 261);
            this.pnlContainer.TabIndex = 0;
            // 
            // btnWebResources
            // 
            this.btnWebResources.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnWebResources.Location = new System.Drawing.Point(3, 71);
            this.btnWebResources.Name = "btnWebResources";
            this.btnWebResources.Size = new System.Drawing.Size(278, 28);
            this.btnWebResources.TabIndex = 5;
            this.btnWebResources.Text = "WebResources";
            this.btnWebResources.UseVisualStyleBackColor = true;
            this.btnWebResources.Click += new System.EventHandler(this.BtnWebresources_Click);
            // 
            // btnWhoAmI
            // 
            this.btnWhoAmI.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnWhoAmI.Location = new System.Drawing.Point(3, 3);
            this.btnWhoAmI.Name = "btnWhoAmI";
            this.btnWhoAmI.Size = new System.Drawing.Size(278, 28);
            this.btnWhoAmI.TabIndex = 3;
            this.btnWhoAmI.Text = "Who Am I?";
            this.btnWhoAmI.UseVisualStyleBackColor = true;
            this.btnWhoAmI.Click += new System.EventHandler(this.BtnWhoAmI_Click);
            // 
            // btnEntities
            // 
            this.btnEntities.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnEntities.Location = new System.Drawing.Point(3, 37);
            this.btnEntities.Name = "btnEntities";
            this.btnEntities.Size = new System.Drawing.Size(278, 28);
            this.btnEntities.TabIndex = 4;
            this.btnEntities.Text = "Entities";
            this.btnEntities.UseVisualStyleBackColor = true;
            this.btnEntities.Click += new System.EventHandler(this.BtnEntities_Click);
            // 
            // PrototypesContainer
            // 
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.pnlContainer);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "PrototypesContainer";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.pnlContainer.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.TableLayoutPanel pnlContainer;
        private System.Windows.Forms.Button btnWebResources;
        private System.Windows.Forms.Button btnWhoAmI;
        private System.Windows.Forms.Button btnEntities;
    }
}
