using GridPrintPreviewLib;
using System;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;
using VirtualDataTableLib;

namespace bigview
{
    public partial class FormPreview : Form
    {
        public FormPreview()
        {
            InitializeComponent();

            allTabs.SelectedTab = tabTable;
            displayAllowedPageControls();
        }

        public FormPreview(String filename) : this()
        {
            LoadFileOnGrid(filename);
        }

        private void openMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                LoadFileOnGrid(openFileDialog.FileName);
                closeMenuItem.Enabled = false;
            }
        }

        private void closeMenuItem_Click(object sender, EventArgs e)
        {
            CloseLoadedFile();
            closeMenuItem.Enabled = false;
        }

        private void gridView_DragOver(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy;
        }

        private void gridView_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            foreach (string file in files)
            {
                Console.WriteLine(file);
                if (File.Exists(file))
                {
                    LoadFileOnGrid(file);
                    break;
                }
            }

        }

        private void viewAsRowMenuItem_Click(object sender, EventArgs e)
        {
            gridRow.Rows.Clear();

            var row = gridView.CurrentRow;

            foreach (DataGridViewColumn col in gridView.Columns)
            {
                string fieldName = col.Name;
                object rowValue = row.Cells[fieldName].Value;
                string fieldValue = rowValue == null ? string.Empty : rowValue.ToString();

                gridRow.Rows.Add(fieldName, fieldValue);
            }

            gridRow.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.DisplayedCells);

            selectTab(tabRow, sender);
        }

        private void viewAsTableMenuItem_Click(object sender, EventArgs e)
        {
            selectTab(tabTable, sender);
        }

        private void selectTab(TabPage tab, object sender)
        {
            allTabs.SelectedTab = tab;

            // Set the current clicked item to item
            var item = sender as ToolStripMenuItem;
            var owner = item.OwnerItem as ToolStripDropDownItem;
            // Loop through all items in the subMenu and uncheck them but do check the clicked item
            foreach (ToolStripMenuItem tempItemp in owner.DropDownItems)
            {
                if (tempItemp == item)
                    tempItemp.Checked = true;
                else
                    tempItemp.Checked = false;
            }

            viewAsButton.Image = item.Image;
        }

        #region load

        private DataTableCache memoryCache;

        private void CloseLoadedFile()
        {
            if (memoryCache != null)
            {
                memoryCache.Dispose();
                memoryCache = null;

                gridView.Columns.Clear();
                lastRowCount = null;
                lastRowsLoaded = 0;
            }
            this.Text = "No file opened";
        }

        private void LoadFileOnGrid(string file)
        {
            CloseLoadedFile();

            int rowsPerPage = gridView.ClientRectangle.Height / gridView.RowTemplate.Height;

            try
            {
                memoryCache = DataTableCache.GetCacheFor(file, rowsPerPage);
                this.Text = Path.GetFileName(file);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error opening file '{file}': {ex.Message}\r\n At: {ex.StackTrace}");
            }

            try
            {
                var fields = memoryCache.GetFields();

                var newcolumns = new DataGridViewColumn[fields.Count];

                for (int i = 0; i < fields.Count; i++)
                {
                    var field = fields[i];
                    var col = new DataGridViewTextBoxColumn();
                    col.DataPropertyName = field.ColumnName;
                    col.HeaderText = string.IsNullOrWhiteSpace(field.Caption)
                        ? field.ColumnName : field.Caption;

                    newcolumns[i] = col;
                }

                this.SuspendLayout();
                try
                {
                    gridView.Columns.AddRange(newcolumns);

                    UpdateGridRowCount(0);
                }
                finally
                {
                    this.ResumeLayout();
                }
            }
            catch (Exception ex)
            {
                CloseLoadedFile();
                MessageBox.Show($"Error reading records from file '{file}': {ex.Message}\r\n At: {ex.StackTrace}");
            }

            try
            {
                // Adjust the column widths based on the displayed values.
                gridView.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.DisplayedCells);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error displaying records from file '{file}': {ex.Message}\r\n At: {ex.StackTrace}");
            }


            docToPrint = null;

            selectTab(tabTable, viewAsTableMenuItem);
            viewAsButton.Visible = true;
        }

        private int? lastRowCount = null;
        private int lastRowsLoaded = 0;

        private void UpdateGridRowCount(int rowIndex)
        {
            lastRowCount = memoryCache.GetTotalRowCount();
            lastRowsLoaded = memoryCache.GetMaxLoadedRowIndex();

            UpdateStatusText(rowIndex);

            int gridRows = lastRowCount ?? lastRowsLoaded;

            if (gridView.RowCount < gridRows)
            {
                gridView.RowCount = gridRows; // triggers gridView_CellValueNeeded
            }
        }

        private void gridView_CellValueNeeded(object sender, DataGridViewCellValueEventArgs e)
        {
            object cellValue = null;
            if (memoryCache != null)
                cellValue = memoryCache.RetrieveElement(e.RowIndex, e.ColumnIndex);

            e.Value = cellValue == null ? string.Empty : cellValue.ToString();

            if (memoryCache != null)
                UpdateGridRowCount(e.RowIndex);
        }

        private void gridView_RowEnter(object sender, DataGridViewCellEventArgs e)
        {

            UpdateStatusText(e.RowIndex);
        }

        private void UpdateStatusText(int rowIndex)
        {
            int rowNum = rowIndex + 1;

            statusInfo.Text = lastRowCount != null && lastRowsLoaded != lastRowCount.Value
                ? $"row {rowNum} ({lastRowsLoaded} found {lastRowCount} total)"
                : lastRowCount != null
                ? $"row {rowNum} ({lastRowCount} total)"
                : lastRowsLoaded > 0
                ? $"row {rowNum} ({lastRowsLoaded} found)"
                : string.Empty;
        }

        #endregion load


        #region Print

        private GridPrintDocument docToPrint = null;

        private void viewAsPrintedMenuItem_Click(object sender, EventArgs e)
        {
            bool isTable = allTabs.SelectedTab == tabTable;
            var grid = isTable ? gridView : gridRow;

            createNewDocumentWith(grid, isTable);

            printPreview.Document = getPrintDoc();
            selectTab(tabPrinted, sender);
        }

        private void createNewDocumentWith(DataGridView grid, bool landscape)
        {
            docToPrint = new GridPrintDocument(grid, grid.Font, true);

            if (grid.SelectedCells.Count > 1)
            {
                int minc, minr, maxc, maxr;
                minc = minr = int.MaxValue;
                maxc = maxr = 0;
                foreach (DataGridViewCell cell in grid.SelectedCells)
                {
                    if (cell.ColumnIndex < minc)
                        minc = cell.ColumnIndex;
                    if (cell.ColumnIndex > maxc)
                        maxc = cell.ColumnIndex;
                    if (cell.RowIndex < minr)
                        minr = cell.RowIndex;
                    if (cell.RowIndex > maxr)
                        maxr = cell.RowIndex;
                }

                var area = new GridSelectedArea(minc, minr, maxc, maxr);
                docToPrint.SelectedArea = area;
            }

            docToPrint.DocumentName = this.Text;
            docToPrint.DefaultPageSettings.Landscape = landscape;
            docToPrint.ShowMargin = true;
            docToPrint.DrawCellBox = true;

        }

        private GridPrintDocument getPrintDoc()
        {
            return docToPrint;
        }

        //--------------------------------------------------------------------
        #region ** main commands

        void btnPrint_Click(object sender, EventArgs e)
        {
            var doc = getPrintDoc();
            printDialog.Document = doc;

            var ps = printDialog.PrinterSettings;
            ps.MinimumPage = ps.FromPage = 1;
            ps.MaximumPage = ps.ToPage = doc.PageCount;

            if (printDialog.ShowDialog() == DialogResult.OK)
                doc.Print();
        }
        void btnPageSetup_Click(object sender, EventArgs e)
        {
            var doc = getPrintDoc();
            pageSetupDialog.Document = doc;
            if (pageSetupDialog.ShowDialog(this) == DialogResult.OK)
            {
                doc.DefaultPageSettings = pageSetupDialog.PageSettings;
                doc.PrinterSettings = pageSetupDialog.PrinterSettings;
                printPreview.Update();
            }
        }

        #endregion

        //--------------------------------------------------------------------
        #region ** zoom

        void btnZoom_DropDownItemClicked(object sender, EventArgs e)
        {
            float scaleX = 0;
            float scaleY = 0;

            var clickedItem = sender as ToolStripItem;
            string menuText = clickedItem.Text;
            var doc = getPrintDoc();

            if (menuText.EndsWith("%", StringComparison.InvariantCulture))
            {
                double zoom = int.Parse(menuText.Replace("%", "").Trim()) / 100.0;
                printPreview.Zoom = zoom;
            }
            else if (clickedItem == zoomToFullPage)
            {
                printPreview.Zoom = doc.CalcScaleForFit();
            }
            else if (clickedItem == zoomToPageWidth)
            {
                doc.CalcScaleForFit(1, 1, ref scaleX, ref scaleY);
                printPreview.Zoom = scaleX;
            }
            else if (clickedItem == zoomToTwoPages)
            {
                printPreview.Columns = 2;
                doc.CalcScaleForFit(2, 1, ref scaleX, ref scaleY);
                printPreview.Zoom = scaleX;
                return;
            }

            printPreview.Columns = 1;
        }


        #endregion

        //--------------------------------------------------------------------
        #region ** page navigation

        void btnFirst_Click(object sender, EventArgs e)
        {
            printPreview.StartPage = 0;
        }
        void btnPrev_Click(object sender, EventArgs e)
        {
            if (printPreview.StartPage > 0)
                printPreview.StartPage -= 1;
        }
        void btnNext_Click(object sender, EventArgs e)
        {
            var doc = getPrintDoc();
            if (doc.PageCount > printPreview.StartPage)
                printPreview.StartPage += 1;
        }
        void btnLast_Click(object sender, EventArgs e)
        {
            var doc = getPrintDoc();
            printPreview.StartPage = doc.PageCount;
        }
        void txtPosition_Enter(object sender, EventArgs e)
        {
            txtPosition.SelectAll();
        }
        void txtPosition_Validating(object sender, CancelEventArgs e)
        {
            CommitPageNumber();
        }
        void txtPosition_KeyPress(object sender, KeyPressEventArgs e)
        {
            var c = e.KeyChar;
            if (c == (char)13)
            {
                CommitPageNumber();
                e.Handled = true;
            }
            else if (c > ' ' && !char.IsDigit(c))
            {
                e.Handled = true;
            }
        }
        void CommitPageNumber()
        {
            int page;
            if (int.TryParse(txtPosition.Text, out page))
            {
                var doc = getPrintDoc();
                if (doc.PageCount > page)
                    printPreview.StartPage = page;
            }
        }
        void printPreview_StartPageChanged(object sender, EventArgs e)
        {
            var page = printPreview.StartPage + 1;
            txtPosition.Text = page.ToString();
        }
        private void printPreview_PageCountChanged(object sender, EventArgs e)
        {
            Application.DoEvents();

            var doc = getPrintDoc();
            lblCount.Text = string.Format("of {0}", doc.PageCount);
        }

        #endregion

        #endregion Print

        private void allTabs_SelectedIndexChanged(object sender, EventArgs e)
        {
            displayAllowedPageControls();
        }

        private void displayAllowedPageControls()
        {
            string tag = allTabs.SelectedTab == tabPrinted ? "print" : "view";

            foreach (ToolStripItem button in toolbarMain.Items)
                checkItemEnabled(button, tag);

            foreach (Control child in toolbarMain.Controls)
                checkItemEnabled(child, tag);
        }

        private void checkItemEnabled(ToolStripItem item, string tag)
        {
            if (item.Tag != null)
            {
                string itag = item.Tag.ToString();
                bool ok = itag.Contains(tag);
                item.Visible = ok;
                item.Enabled = ok;
            }

            if (item is ToolStripDropDownItem)
            {
                var dropItem = (ToolStripDropDownItem)item;
                if (dropItem.HasDropDownItems)
                {
                    foreach (ToolStripItem child in dropItem.DropDownItems)
                    {
                        checkItemEnabled(child, tag);
                    }
                }
            }
        }

        private void checkItemEnabled(Control item, string tag)
        {
            if (item.Tag != null)
            {
                string itag = item.Tag.ToString();
                bool ok = itag.Contains(tag);
                item.Visible = ok;
                item.Enabled = ok;
            }

            foreach (Control child in item.Controls)
            {
                checkItemEnabled(child, tag);
            }
        }

    }
}
