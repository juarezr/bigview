using OfficeOpenXml;
using System.IO;
using System.Windows.Forms;

namespace bigview
{
    public class ExcelExport
    {
        protected class CellArea
        {
            public int StartCol { get; set; }
            public int StartRow { get; set; }
            public int EndCol { get; set; }
            public int EndRow { get; set; }
        }

        public void exportTo(DataGridView grid, string filePath)
        {
            var area = getSelectionArea(grid);

            using (var package = new ExcelPackage())
            {
                for (int i = area.StartRow; i < area.EndRow; i++)
                {
                    for (int j = area.StartCol; j < area.EndCol; j++)
                    {

                    }
                }


                var finfo = new FileInfo(filePath);
                package.SaveAs(finfo);
            }
        }

        private static CellArea getSelectionArea(DataGridView grid)
        {
            var area = new CellArea();

            if (grid.SelectedCells.Count > 1)
            {
                foreach (DataGridViewCell cell in grid.SelectedCells)
                {
                    if (cell.ColumnIndex < area.StartCol)
                        area.StartCol = cell.ColumnIndex;
                    if (cell.ColumnIndex > area.EndCol)
                        area.EndCol = cell.ColumnIndex;
                    if (cell.RowIndex < area.StartRow)
                        area.StartRow = cell.RowIndex;
                    if (cell.RowIndex > area.EndRow)
                        area.EndRow = cell.RowIndex;
                }
            }
            else
            {
                area.StartCol = 0;
                area.EndCol = grid.ColumnCount;
                area.StartRow = 0;
                area.EndRow = grid.RowCount;
            }

            return area;
        }
    }

}
