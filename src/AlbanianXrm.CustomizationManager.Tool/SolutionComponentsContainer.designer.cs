using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlbanianXrm.CustomizationManager
{
    public partial class SolutionComponentsContainer
    {
        [GeneratedCode("Form Designer", "0.0.0.0")]
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.lstSolutionItems = new System.Windows.Forms.ListBox();
            this.cmbFilteringSolution = new System.Windows.Forms.ComboBox();
            this.cmnFilteringSolution = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuRefreshSolutions = new System.Windows.Forms.ToolStripMenuItem();
            this.pnlContainer = new System.Windows.Forms.TableLayoutPanel();
            this.cmnFilteringSolution.SuspendLayout();
            this.pnlContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // lstSolutionItems
            // 
            this.lstSolutionItems.DisplayMember = "Name";
            this.lstSolutionItems.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstSolutionItems.FormattingEnabled = true;
            this.lstSolutionItems.Location = new System.Drawing.Point(3, 30);
            this.lstSolutionItems.Name = "lstSolutionItems";
            this.lstSolutionItems.Size = new System.Drawing.Size(278, 228);
            this.lstSolutionItems.TabIndex = 0;
            this.lstSolutionItems.ValueMember = "ComponentType";
            // 
            // cmbFilteringSolution
            // 
            this.cmbFilteringSolution.ContextMenuStrip = this.cmnFilteringSolution;
            this.cmbFilteringSolution.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbFilteringSolution.FormattingEnabled = true;
            this.cmbFilteringSolution.Location = new System.Drawing.Point(3, 3);
            this.cmbFilteringSolution.Name = "cmbFilteringSolution";
            this.cmbFilteringSolution.Size = new System.Drawing.Size(278, 21);
            this.cmbFilteringSolution.TabIndex = 2;
            this.cmbFilteringSolution.DropDown += new System.EventHandler(this.CmbFilteringSolution_DropDown);
            this.cmbFilteringSolution.SelectedValueChanged += new System.EventHandler(this.CmbFilteringSolution_SelectedValueChanged);
            // 
            // cmnFilteringSolution
            // 
            this.cmnFilteringSolution.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuRefreshSolutions});
            this.cmnFilteringSolution.Name = "cmnFilteringSolution";
            this.cmnFilteringSolution.Size = new System.Drawing.Size(166, 26);
            // 
            // mnuRefreshSolutions
            // 
            this.mnuRefreshSolutions.Name = "mnuRefreshSolutions";
            this.mnuRefreshSolutions.Size = new System.Drawing.Size(165, 22);
            this.mnuRefreshSolutions.Text = "Refresh Solutions";
            this.mnuRefreshSolutions.Click += new System.EventHandler(this.MnuRefreshSolutions_Click);
            // 
            // pnlContainer
            // 
            this.pnlContainer.ColumnCount = 1;
            this.pnlContainer.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.pnlContainer.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.pnlContainer.Controls.Add(this.cmbFilteringSolution, 0, 0);
            this.pnlContainer.Controls.Add(this.lstSolutionItems, 0, 1);
            this.pnlContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlContainer.Location = new System.Drawing.Point(0, 0);
            this.pnlContainer.Name = "pnlContainer";
            this.pnlContainer.RowCount = 2;
            this.pnlContainer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27F));
            this.pnlContainer.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.pnlContainer.Size = new System.Drawing.Size(284, 261);
            this.pnlContainer.TabIndex = 3;
            // 
            // SolutionComponentsContainer
            // 
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.pnlContainer);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "SolutionComponentsContainer";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.cmnFilteringSolution.ResumeLayout(false);
            this.pnlContainer.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.ListBox lstSolutionItems;
        private System.Windows.Forms.ComboBox cmbFilteringSolution;
        private System.Windows.Forms.TableLayoutPanel pnlContainer;
        private System.Windows.Forms.ContextMenuStrip cmnFilteringSolution;
        private System.ComponentModel.IContainer components;
        private System.Windows.Forms.ToolStripMenuItem mnuRefreshSolutions;
    }
}
