namespace bigview
{
    partial class FormPreview
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormPreview));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.toolbarContainer = new System.Windows.Forms.ToolStripContainer();
            this.allTabs = new System.Windows.Forms.TabControl();
            this.tabTable = new System.Windows.Forms.TabPage();
            this.gridView = new System.Windows.Forms.DataGridView();
            this.tabRow = new System.Windows.Forms.TabPage();
            this.gridRow = new System.Windows.Forms.DataGridView();
            this.gridRowFieldName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gridRowFieldValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabPrinted = new System.Windows.Forms.TabPage();
            this.printPreview = new System.Windows.Forms.PrintPreviewControl();
            this.toolbarMain = new System.Windows.Forms.ToolStrip();
            this.fileButton = new System.Windows.Forms.ToolStripSplitButton();
            this.openMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.printMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openMenuItemSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.viewAsButton = new System.Windows.Forms.ToolStripDropDownButton();
            this.viewAsTableMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewAsRowMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewAsPrintedMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolbarSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnPrint = new System.Windows.Forms.ToolStripButton();
            this.btnPageSetup = new System.Windows.Forms.ToolStripButton();
            this.toolbarSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnZoom = new System.Windows.Forms.ToolStripDropDownButton();
            this.zoomToFullPage = new System.Windows.Forms.ToolStripMenuItem();
            this.zoomToPageWidth = new System.Windows.Forms.ToolStripMenuItem();
            this.zoomToTwoPages = new System.Windows.Forms.ToolStripMenuItem();
            this.zoomMenuItemSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.zoomTo500 = new System.Windows.Forms.ToolStripMenuItem();
            this.zoomTo200 = new System.Windows.Forms.ToolStripMenuItem();
            this.zoomTo150 = new System.Windows.Forms.ToolStripMenuItem();
            this.zoomTo100 = new System.Windows.Forms.ToolStripMenuItem();
            this.zoomTo75 = new System.Windows.Forms.ToolStripMenuItem();
            this.zoomTo50 = new System.Windows.Forms.ToolStripMenuItem();
            this.zoomTo25 = new System.Windows.Forms.ToolStripMenuItem();
            this.zoomTo10 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolbarSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.btnFirst = new System.Windows.Forms.ToolStripButton();
            this.btnPrev = new System.Windows.Forms.ToolStripButton();
            this.txtPosition = new System.Windows.Forms.ToolStripTextBox();
            this.lblCount = new System.Windows.Forms.ToolStripLabel();
            this.btnNext = new System.Windows.Forms.ToolStripButton();
            this.btnLast = new System.Windows.Forms.ToolStripButton();
            this.statusBar = new System.Windows.Forms.StatusStrip();
            this.statusInfo = new System.Windows.Forms.ToolStripStatusLabel();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.printDialog = new System.Windows.Forms.PrintDialog();
            this.pageSetupDialog = new System.Windows.Forms.PageSetupDialog();
            this.toolbarContainer.ContentPanel.SuspendLayout();
            this.toolbarContainer.TopToolStripPanel.SuspendLayout();
            this.toolbarContainer.SuspendLayout();
            this.allTabs.SuspendLayout();
            this.tabTable.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridView)).BeginInit();
            this.tabRow.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridRow)).BeginInit();
            this.tabPrinted.SuspendLayout();
            this.toolbarMain.SuspendLayout();
            this.statusBar.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolbarContainer
            // 
            // 
            // toolbarContainer.BottomToolStripPanel
            // 
            this.toolbarContainer.BottomToolStripPanel.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            // 
            // toolbarContainer.ContentPanel
            // 
            this.toolbarContainer.ContentPanel.Controls.Add(this.allTabs);
            resources.ApplyResources(this.toolbarContainer.ContentPanel, "toolbarContainer.ContentPanel");
            resources.ApplyResources(this.toolbarContainer, "toolbarContainer");
            // 
            // toolbarContainer.LeftToolStripPanel
            // 
            this.toolbarContainer.LeftToolStripPanel.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.toolbarContainer.Name = "toolbarContainer";
            // 
            // toolbarContainer.RightToolStripPanel
            // 
            this.toolbarContainer.RightToolStripPanel.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            // 
            // toolbarContainer.TopToolStripPanel
            // 
            this.toolbarContainer.TopToolStripPanel.Controls.Add(this.toolbarMain);
            this.toolbarContainer.TopToolStripPanel.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            // 
            // allTabs
            // 
            resources.ApplyResources(this.allTabs, "allTabs");
            this.allTabs.Controls.Add(this.tabTable);
            this.allTabs.Controls.Add(this.tabRow);
            this.allTabs.Controls.Add(this.tabPrinted);
            this.allTabs.Name = "allTabs";
            this.allTabs.SelectedIndex = 0;
            this.allTabs.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.allTabs.TabStop = false;
            this.allTabs.SelectedIndexChanged += new System.EventHandler(this.allTabs_SelectedIndexChanged);
            // 
            // tabTable
            // 
            this.tabTable.Controls.Add(this.gridView);
            resources.ApplyResources(this.tabTable, "tabTable");
            this.tabTable.Name = "tabTable";
            this.tabTable.UseVisualStyleBackColor = true;
            // 
            // gridView
            // 
            this.gridView.AllowDrop = true;
            this.gridView.AllowUserToAddRows = false;
            this.gridView.AllowUserToDeleteRows = false;
            this.gridView.AllowUserToOrderColumns = true;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.gridView.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.gridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.gridView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.gridView.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText;
            this.gridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            resources.ApplyResources(this.gridView, "gridView");
            this.gridView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.gridView.Name = "gridView";
            this.gridView.ReadOnly = true;
            this.gridView.ShowCellToolTips = false;
            this.gridView.ShowEditingIcon = false;
            this.gridView.VirtualMode = true;
            this.gridView.CellValueNeeded += new System.Windows.Forms.DataGridViewCellValueEventHandler(this.gridView_CellValueNeeded);
            // 
            // tabRow
            // 
            this.tabRow.Controls.Add(this.gridRow);
            resources.ApplyResources(this.tabRow, "tabRow");
            this.tabRow.Name = "tabRow";
            this.tabRow.UseVisualStyleBackColor = true;
            // 
            // gridRow
            // 
            this.gridRow.AllowDrop = true;
            this.gridRow.AllowUserToAddRows = false;
            this.gridRow.AllowUserToDeleteRows = false;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.gridRow.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle2;
            this.gridRow.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.gridRow.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.gridRow.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
            this.gridRow.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridRow.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.gridRowFieldName,
            this.gridRowFieldValue});
            resources.ApplyResources(this.gridRow, "gridRow");
            this.gridRow.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.gridRow.Name = "gridRow";
            this.gridRow.ReadOnly = true;
            this.gridRow.ShowCellToolTips = false;
            this.gridRow.ShowEditingIcon = false;
            // 
            // gridRowFieldName
            // 
            this.gridRowFieldName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.Silver;
            this.gridRowFieldName.DefaultCellStyle = dataGridViewCellStyle3;
            this.gridRowFieldName.Frozen = true;
            resources.ApplyResources(this.gridRowFieldName, "gridRowFieldName");
            this.gridRowFieldName.Name = "gridRowFieldName";
            this.gridRowFieldName.ReadOnly = true;
            // 
            // gridRowFieldValue
            // 
            this.gridRowFieldValue.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            resources.ApplyResources(this.gridRowFieldValue, "gridRowFieldValue");
            this.gridRowFieldValue.Name = "gridRowFieldValue";
            this.gridRowFieldValue.ReadOnly = true;
            // 
            // tabPrinted
            // 
            this.tabPrinted.Controls.Add(this.printPreview);
            resources.ApplyResources(this.tabPrinted, "tabPrinted");
            this.tabPrinted.Name = "tabPrinted";
            this.tabPrinted.UseVisualStyleBackColor = true;
            // 
            // printPreview
            // 
            resources.ApplyResources(this.printPreview, "printPreview");
            this.printPreview.Name = "printPreview";
            this.printPreview.StartPageChanged += new System.EventHandler(this.printPreview_StartPageChanged);
            // 
            // toolbarMain
            // 
            resources.ApplyResources(this.toolbarMain, "toolbarMain");
            this.toolbarMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileButton,
            this.viewAsButton,
            this.toolbarSeparator1,
            this.btnPrint,
            this.btnPageSetup,
            this.toolbarSeparator2,
            this.btnZoom,
            this.toolbarSeparator3,
            this.btnFirst,
            this.btnPrev,
            this.txtPosition,
            this.lblCount,
            this.btnNext,
            this.btnLast});
            this.toolbarMain.Name = "toolbarMain";
            this.toolbarMain.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            // 
            // fileButton
            // 
            this.fileButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.fileButton.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openMenuItem,
            this.printMenuItem,
            this.openMenuItemSeparator1});
            resources.ApplyResources(this.fileButton, "fileButton");
            this.fileButton.Margin = new System.Windows.Forms.Padding(8, 1, 0, 2);
            this.fileButton.Name = "fileButton";
            this.fileButton.ButtonClick += new System.EventHandler(this.openMenuItem_Click);
            // 
            // openMenuItem
            // 
            this.openMenuItem.Name = "openMenuItem";
            resources.ApplyResources(this.openMenuItem, "openMenuItem");
            this.openMenuItem.Tag = "view,print";
            this.openMenuItem.Click += new System.EventHandler(this.openMenuItem_Click);
            // 
            // printMenuItem
            // 
            this.printMenuItem.Name = "printMenuItem";
            resources.ApplyResources(this.printMenuItem, "printMenuItem");
            this.printMenuItem.Tag = "print";
            this.printMenuItem.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // openMenuItemSeparator1
            // 
            this.openMenuItemSeparator1.Name = "openMenuItemSeparator1";
            resources.ApplyResources(this.openMenuItemSeparator1, "openMenuItemSeparator1");
            // 
            // viewAsButton
            // 
            this.viewAsButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.viewAsButton.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.viewAsTableMenuItem,
            this.viewAsRowMenuItem,
            this.viewAsPrintedMenuItem});
            resources.ApplyResources(this.viewAsButton, "viewAsButton");
            this.viewAsButton.Margin = new System.Windows.Forms.Padding(8, 1, 0, 2);
            this.viewAsButton.Name = "viewAsButton";
            // 
            // viewAsTableMenuItem
            // 
            this.viewAsTableMenuItem.Checked = true;
            this.viewAsTableMenuItem.CheckOnClick = true;
            this.viewAsTableMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            resources.ApplyResources(this.viewAsTableMenuItem, "viewAsTableMenuItem");
            this.viewAsTableMenuItem.Name = "viewAsTableMenuItem";
            this.viewAsTableMenuItem.Click += new System.EventHandler(this.viewAsTableMenuItem_Click);
            // 
            // viewAsRowMenuItem
            // 
            this.viewAsRowMenuItem.CheckOnClick = true;
            resources.ApplyResources(this.viewAsRowMenuItem, "viewAsRowMenuItem");
            this.viewAsRowMenuItem.Name = "viewAsRowMenuItem";
            this.viewAsRowMenuItem.Click += new System.EventHandler(this.viewAsRowMenuItem_Click);
            // 
            // viewAsPrintedMenuItem
            // 
            this.viewAsPrintedMenuItem.CheckOnClick = true;
            resources.ApplyResources(this.viewAsPrintedMenuItem, "viewAsPrintedMenuItem");
            this.viewAsPrintedMenuItem.Name = "viewAsPrintedMenuItem";
            this.viewAsPrintedMenuItem.Click += new System.EventHandler(this.viewAsPrintedMenuItem_Click);
            // 
            // toolbarSeparator1
            // 
            this.toolbarSeparator1.Margin = new System.Windows.Forms.Padding(8, 0, 8, 0);
            this.toolbarSeparator1.Name = "toolbarSeparator1";
            resources.ApplyResources(this.toolbarSeparator1, "toolbarSeparator1");
            this.toolbarSeparator1.Tag = "print";
            // 
            // btnPrint
            // 
            this.btnPrint.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.btnPrint, "btnPrint");
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Tag = "print";
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // btnPageSetup
            // 
            this.btnPageSetup.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.btnPageSetup, "btnPageSetup");
            this.btnPageSetup.Name = "btnPageSetup";
            this.btnPageSetup.Tag = "print";
            this.btnPageSetup.Click += new System.EventHandler(this.btnPageSetup_Click);
            // 
            // toolbarSeparator2
            // 
            this.toolbarSeparator2.Margin = new System.Windows.Forms.Padding(8, 0, 8, 0);
            this.toolbarSeparator2.Name = "toolbarSeparator2";
            resources.ApplyResources(this.toolbarSeparator2, "toolbarSeparator2");
            this.toolbarSeparator2.Tag = "print";
            // 
            // btnZoom
            // 
            this.btnZoom.AutoToolTip = false;
            this.btnZoom.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.zoomToFullPage,
            this.zoomToPageWidth,
            this.zoomToTwoPages,
            this.zoomMenuItemSeparator1,
            this.zoomTo500,
            this.zoomTo200,
            this.zoomTo150,
            this.zoomTo100,
            this.zoomTo75,
            this.zoomTo50,
            this.zoomTo25,
            this.zoomTo10});
            resources.ApplyResources(this.btnZoom, "btnZoom");
            this.btnZoom.Name = "btnZoom";
            this.btnZoom.Tag = "print";
            // 
            // zoomToFullPage
            // 
            resources.ApplyResources(this.zoomToFullPage, "zoomToFullPage");
            this.zoomToFullPage.Name = "zoomToFullPage";
            this.zoomToFullPage.Click += new System.EventHandler(this.btnZoom_DropDownItemClicked);
            // 
            // zoomToPageWidth
            // 
            resources.ApplyResources(this.zoomToPageWidth, "zoomToPageWidth");
            this.zoomToPageWidth.Name = "zoomToPageWidth";
            this.zoomToPageWidth.Click += new System.EventHandler(this.btnZoom_DropDownItemClicked);
            // 
            // zoomToTwoPages
            // 
            resources.ApplyResources(this.zoomToTwoPages, "zoomToTwoPages");
            this.zoomToTwoPages.Name = "zoomToTwoPages";
            this.zoomToTwoPages.Click += new System.EventHandler(this.btnZoom_DropDownItemClicked);
            // 
            // zoomMenuItemSeparator1
            // 
            this.zoomMenuItemSeparator1.Name = "zoomMenuItemSeparator1";
            resources.ApplyResources(this.zoomMenuItemSeparator1, "zoomMenuItemSeparator1");
            this.zoomMenuItemSeparator1.Click += new System.EventHandler(this.btnZoom_DropDownItemClicked);
            // 
            // zoomTo500
            // 
            this.zoomTo500.Name = "zoomTo500";
            resources.ApplyResources(this.zoomTo500, "zoomTo500");
            this.zoomTo500.Click += new System.EventHandler(this.btnZoom_DropDownItemClicked);
            // 
            // zoomTo200
            // 
            this.zoomTo200.Name = "zoomTo200";
            resources.ApplyResources(this.zoomTo200, "zoomTo200");
            this.zoomTo200.Click += new System.EventHandler(this.btnZoom_DropDownItemClicked);
            // 
            // zoomTo150
            // 
            this.zoomTo150.Name = "zoomTo150";
            resources.ApplyResources(this.zoomTo150, "zoomTo150");
            this.zoomTo150.Click += new System.EventHandler(this.btnZoom_DropDownItemClicked);
            // 
            // zoomTo100
            // 
            this.zoomTo100.Name = "zoomTo100";
            resources.ApplyResources(this.zoomTo100, "zoomTo100");
            this.zoomTo100.Click += new System.EventHandler(this.btnZoom_DropDownItemClicked);
            // 
            // zoomTo75
            // 
            this.zoomTo75.Name = "zoomTo75";
            resources.ApplyResources(this.zoomTo75, "zoomTo75");
            this.zoomTo75.Click += new System.EventHandler(this.btnZoom_DropDownItemClicked);
            // 
            // zoomTo50
            // 
            this.zoomTo50.Name = "zoomTo50";
            resources.ApplyResources(this.zoomTo50, "zoomTo50");
            this.zoomTo50.Click += new System.EventHandler(this.btnZoom_DropDownItemClicked);
            // 
            // zoomTo25
            // 
            this.zoomTo25.Name = "zoomTo25";
            resources.ApplyResources(this.zoomTo25, "zoomTo25");
            this.zoomTo25.Click += new System.EventHandler(this.btnZoom_DropDownItemClicked);
            // 
            // zoomTo10
            // 
            this.zoomTo10.Name = "zoomTo10";
            resources.ApplyResources(this.zoomTo10, "zoomTo10");
            this.zoomTo10.Click += new System.EventHandler(this.btnZoom_DropDownItemClicked);
            // 
            // toolbarSeparator3
            // 
            this.toolbarSeparator3.Margin = new System.Windows.Forms.Padding(8, 0, 8, 0);
            this.toolbarSeparator3.Name = "toolbarSeparator3";
            resources.ApplyResources(this.toolbarSeparator3, "toolbarSeparator3");
            this.toolbarSeparator3.Tag = "print";
            // 
            // btnFirst
            // 
            this.btnFirst.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.btnFirst, "btnFirst");
            this.btnFirst.Name = "btnFirst";
            this.btnFirst.Tag = "print";
            this.btnFirst.Click += new System.EventHandler(this.btnFirst_Click);
            // 
            // btnPrev
            // 
            this.btnPrev.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.btnPrev, "btnPrev");
            this.btnPrev.Name = "btnPrev";
            this.btnPrev.Tag = "print";
            this.btnPrev.Click += new System.EventHandler(this.btnPrev_Click);
            // 
            // txtPosition
            // 
            resources.ApplyResources(this.txtPosition, "txtPosition");
            this.txtPosition.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtPosition.Name = "txtPosition";
            this.txtPosition.Tag = "print";
            this.txtPosition.Validating += new System.ComponentModel.CancelEventHandler(this.txtPosition_Validating);
            this.txtPosition.Click += new System.EventHandler(this.txtPosition_Enter);
            // 
            // lblCount
            // 
            this.lblCount.Name = "lblCount";
            resources.ApplyResources(this.lblCount, "lblCount");
            this.lblCount.Tag = "print";
            // 
            // btnNext
            // 
            this.btnNext.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.btnNext, "btnNext");
            this.btnNext.Name = "btnNext";
            this.btnNext.Tag = "print";
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // btnLast
            // 
            this.btnLast.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.btnLast, "btnLast");
            this.btnLast.Name = "btnLast";
            this.btnLast.Tag = "print";
            this.btnLast.Click += new System.EventHandler(this.btnLast_Click);
            // 
            // statusBar
            // 
            this.statusBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusInfo});
            resources.ApplyResources(this.statusBar, "statusBar");
            this.statusBar.Name = "statusBar";
            // 
            // statusInfo
            // 
            this.statusInfo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.statusInfo.Name = "statusInfo";
            resources.ApplyResources(this.statusInfo, "statusInfo");
            // 
            // openFileDialog
            // 
            this.openFileDialog.DefaultExt = "parquet";
            resources.ApplyResources(this.openFileDialog, "openFileDialog");
            this.openFileDialog.ShowReadOnly = true;
            // 
            // printDialog
            // 
            this.printDialog.AllowSelection = true;
            this.printDialog.AllowSomePages = true;
            this.printDialog.UseEXDialog = true;
            // 
            // FormPreview
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.Controls.Add(this.toolbarContainer);
            this.Controls.Add(this.statusBar);
            this.Name = "FormPreview";
            this.toolbarContainer.ContentPanel.ResumeLayout(false);
            this.toolbarContainer.TopToolStripPanel.ResumeLayout(false);
            this.toolbarContainer.TopToolStripPanel.PerformLayout();
            this.toolbarContainer.ResumeLayout(false);
            this.toolbarContainer.PerformLayout();
            this.allTabs.ResumeLayout(false);
            this.tabTable.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridView)).EndInit();
            this.tabRow.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridRow)).EndInit();
            this.tabPrinted.ResumeLayout(false);
            this.toolbarMain.ResumeLayout(false);
            this.toolbarMain.PerformLayout();
            this.statusBar.ResumeLayout(false);
            this.statusBar.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.StatusStrip statusBar;
        private System.Windows.Forms.ToolStripStatusLabel statusInfo;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.TabControl allTabs;
        private System.Windows.Forms.TabPage tabTable;
        private System.Windows.Forms.DataGridView gridView;
        private System.Windows.Forms.TabPage tabRow;
        private System.Windows.Forms.TabPage tabPrinted;
        private System.Windows.Forms.PrintPreviewControl printPreview;
        private System.Windows.Forms.PrintDialog printDialog;
        private System.Windows.Forms.PageSetupDialog pageSetupDialog;
        private System.Windows.Forms.DataGridView gridRow;
        private System.Windows.Forms.DataGridViewTextBoxColumn gridRowFieldName;
        private System.Windows.Forms.DataGridViewTextBoxColumn gridRowFieldValue;
        private System.Windows.Forms.ToolStrip toolbarMain;
        private System.Windows.Forms.ToolStripButton btnPrint;
        private System.Windows.Forms.ToolStripButton btnPageSetup;
        private System.Windows.Forms.ToolStripSeparator toolbarSeparator1;
        private System.Windows.Forms.ToolStripButton btnFirst;
        private System.Windows.Forms.ToolStripButton btnPrev;
        private System.Windows.Forms.ToolStripTextBox txtPosition;
        private System.Windows.Forms.ToolStripLabel lblCount;
        private System.Windows.Forms.ToolStripButton btnNext;
        private System.Windows.Forms.ToolStripButton btnLast;
        private System.Windows.Forms.ToolStripSplitButton fileButton;
        private System.Windows.Forms.ToolStripMenuItem openMenuItem;
        private System.Windows.Forms.ToolStripMenuItem printMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolbarSeparator2;
        private System.Windows.Forms.ToolStripSeparator toolbarSeparator3;
        private System.Windows.Forms.ToolStripContainer toolbarContainer;
        private System.Windows.Forms.ToolStripDropDownButton viewAsButton;
        private System.Windows.Forms.ToolStripMenuItem viewAsTableMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewAsRowMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewAsPrintedMenuItem;
        private System.Windows.Forms.ToolStripDropDownButton btnZoom;
        private System.Windows.Forms.ToolStripMenuItem zoomToFullPage;
        private System.Windows.Forms.ToolStripMenuItem zoomToPageWidth;
        private System.Windows.Forms.ToolStripMenuItem zoomToTwoPages;
        private System.Windows.Forms.ToolStripSeparator zoomMenuItemSeparator1;
        private System.Windows.Forms.ToolStripMenuItem zoomTo500;
        private System.Windows.Forms.ToolStripMenuItem zoomTo200;
        private System.Windows.Forms.ToolStripMenuItem zoomTo150;
        private System.Windows.Forms.ToolStripMenuItem zoomTo100;
        private System.Windows.Forms.ToolStripMenuItem zoomTo75;
        private System.Windows.Forms.ToolStripMenuItem zoomTo50;
        private System.Windows.Forms.ToolStripMenuItem zoomTo25;
        private System.Windows.Forms.ToolStripMenuItem zoomTo10;
        private System.Windows.Forms.ToolStripSeparator openMenuItemSeparator1;
    }
}

