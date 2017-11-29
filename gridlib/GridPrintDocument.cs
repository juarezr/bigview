using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Printing;
using System.Windows.Forms;

namespace GridPrintPreviewLib
{
    /// <summary>
    /// Class to select an area inside a grid.
    /// </summary>
    public class GridSelectedArea
    {
        /// <summary>
        /// Private attribute
        /// </summary>
        private int m_StartCol = 0;
        private int m_EndCol = 0;
        private int m_StartRow = 0;
        private int m_EndRow = 0;

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="startCol">Start column</param>
        /// <param name="startRow">Start row</param>
        /// <param name="endCol">End column</param>
        /// <param name="endRow">End row</param>
        public GridSelectedArea(int startCol, int startRow, int endCol, int endRow)
        {
            m_StartCol = startCol;
            m_StartRow = startRow;
            m_EndCol = endCol;
            m_EndRow = endRow;
        }

        /// <summary>
        /// Get start column
        /// </summary>
        public int StartCol
        {
            get
            {
                return m_StartCol;
            }
        }

        /// <summary>
        /// Get start row
        /// </summary>
        public int StartRow
        {
            get
            {
                return m_StartRow;
            }
        }

        /// <summary>
        /// Get end colun
        /// </summary>
        public int EndCol
        {
            get
            {
                return m_EndCol;
            }
        }

        /// <summary>
        /// Get end row
        /// </summary>
        public int EndRow
        {
            get
            {
                return m_EndRow;
            }
        }
    }

    /// <summary>
    /// Class to print a datagrid
    /// </summary>
    public class GridPrintDocument : PrintDocument
    {
        protected DataGridView m_AttachedGrid = null;
        private GridSelectedArea m_SelArea = null;
        private int m_RowIndex = 0;
        private int m_PrevPageRowIndex = 0;
        private int m_ColIndex = 0;
        private int m_PrevPageColIndex = 0;
        private bool m_IsPrinting = false;
        private Font m_Font = null;
        private bool m_FirstPage = false;
        private bool m_PrintHeader = false;
        private bool m_ShowMargin = false;
        private float m_ScaleX = 1f;
        private float m_ScaleY = 1f;
        private float m_MaxWidth = 0;
        private float m_MaxHeight = 0;
        private RectangleF m_PageSize = RectangleF.Empty;
        private bool m_DrawCellBox = false;
        private int m_PageCount = 0;

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="source">DataGridView to be print</param>
        /// <param name="docFont">Font used for print (null = use DataGridView display font)</param>
        /// <param name="flagHeader">Flag for print column header (true)</param>
        public GridPrintDocument(DataGridView source, Font docFont, bool flagHeader)
        {
            m_AttachedGrid = source;
            m_Font = docFont;
            m_PrintHeader = flagHeader;
            m_SelArea = new GridSelectedArea(0, 0, source.ColumnCount - 1, source.RowCount - 1);
        }

        /// <summary>
        /// Sleect area in the DataGridView for the print operation (default is the all grid)
        /// </summary>
        public GridSelectedArea SelectedArea
        {
            get
            {
                return m_SelArea;
            }
            set
            {
                m_SelArea = value;
            }
        }

        /// <summary>
        /// Flag to print box on every single cell (true)
        /// </summary>
        public bool DrawCellBox
        {
            get
            {
                return m_DrawCellBox;
            }
            set
            {
                m_DrawCellBox = value;
            }
        }

        /// <summary>
        /// Get/Set scale factor (for both X and Y axis)
        /// </summary>
        public float ScaleFactor
        {
            get
            {
                return Math.Min(m_ScaleX, m_ScaleY);
            }
            set
            {
                m_ScaleX = value;
                m_ScaleY = value;
            }
        }

        /// <summary>
        /// Get/Set scale factor for X axis
        /// </summary>
        public float ScaleFactorX
        {
            get
            {
                return m_ScaleX;
            }
            set
            {
                m_ScaleX = value;
            }
        }

        /// <summary>
        /// Get/Set scale factor for Y axis
        /// </summary>
        public float ScaleFactorY
        {
            get
            {
                return m_ScaleY;
            }
            set
            {
                m_ScaleY = value;
            }
        }

        /// <summary>
        /// Flag for show margin in preview - not in print (true)
        /// </summary>
        public bool ShowMargin
        {
            get
            {
                return m_ShowMargin;
            }
            set
            {
                m_ShowMargin = value;
            }
        }

        /// <summary>
        /// Get if status is in printing
        /// </summary>
        public bool IsPrinting
        {
            get
            {
                return m_IsPrinting;
            }
        }

        /// <summary>
        /// Get the total number of pages
        /// </summary>
        public int PageCount
        {
            get
            {
                return m_PageCount;
            }
        }

        /// <summary>
        /// Override OnBeginPrint
        /// </summary>
        /// <param name="e"></param>
        protected override void OnBeginPrint(PrintEventArgs e)
        {
            base.OnBeginPrint(e);
            m_IsPrinting = true;
            m_RowIndex = m_SelArea.StartRow;
            m_PrevPageRowIndex = m_SelArea.StartRow;
            m_ColIndex = m_SelArea.StartCol;
            m_PrevPageColIndex = m_SelArea.StartCol;
            m_FirstPage = true;
            m_PageCount = 0;
        }

        /// <summary>
        /// Override OnEndPrint
        /// </summary>
        /// <param name="e"></param>
        protected override void OnEndPrint(PrintEventArgs e)
        {
            base.OnEndPrint(e);
            m_IsPrinting = false;
        }

        /// <summary>
        /// Calc best X and Y scale factor for fit print in n horizontal pages (xPage) and n vertical pages (yPage)
        /// </summary>
        /// <param name="xPage">Number of horizontal pages</param>
        /// <param name="yPage">Number of vertical pages</param>
        /// <param name="scaleX">X scale factor in out</param>
        /// <param name="scaleY">Y scale factor in out</param>
        public void CalcScaleForFit(int xPage, int yPage, ref float scaleX, ref float scaleY)
        {
            scaleX = (m_PageSize.Width * xPage) / m_MaxWidth;
            scaleY = (m_PageSize.Height * yPage) / m_MaxHeight;
        }

        /// <summary>
        /// Calc best scale factor to fit print in one page
        /// </summary>
        /// <returns>Scale factor</returns>
        public float CalcScaleForFit()
        {
            float scaleX = 0;
            float scaleY = 0;
            CalcScaleForFit(1, 1, ref scaleX, ref scaleY);
            return Math.Min(scaleX, scaleY);
        }


        /// <summary>
        /// Measure cell (string)
        /// </summary>
        /// <param name="g">Graphics</param>
        /// <param name="cell">Cell to measure</param>
        /// <param name="s">String inside the cell</param>
        /// <param name="font">Used font for the measure</param>
        /// <returns>Size of the string</returns>
        protected virtual SizeF onMeasureCell(Graphics g, DataGridViewCell cell, string s, Font font)
        {
            SizeF cellSize = cell.Size;
            return g.MeasureString(s, font, cellSize);
        }

        /// <summary>
        /// Prepare for draw a cell.
        /// </summary>
        /// <param name="g">Graphics</param>
        /// <param name="cell">Cell to draw</param>
        /// <param name="s">String to draw</param>
        /// <param name="layoutRect">Rectangle for the layout</param>
        /// <param name="font">Used font</param>
        protected virtual void onPrepareDrawCell(Graphics g, DataGridViewCell cell, string s, RectangleF layoutRect, Font font)
        {
            // Set string format
            StringFormat format = new StringFormat();
            switch (cell.Style.Alignment)
            {
                case DataGridViewContentAlignment.BottomCenter:
                    format.Alignment = StringAlignment.Center;
                    format.LineAlignment = StringAlignment.Far;
                    break;
                case DataGridViewContentAlignment.MiddleCenter:
                    format.Alignment = StringAlignment.Center;
                    format.LineAlignment = StringAlignment.Center;
                    break;
                case DataGridViewContentAlignment.TopCenter:
                    format.Alignment = StringAlignment.Center;
                    format.LineAlignment = StringAlignment.Near;
                    break;
                case DataGridViewContentAlignment.BottomLeft:
                    format.Alignment = StringAlignment.Near;
                    format.LineAlignment = StringAlignment.Far;
                    break;
                case DataGridViewContentAlignment.MiddleLeft:
                    format.Alignment = StringAlignment.Near;
                    format.LineAlignment = StringAlignment.Center;
                    break;
                case DataGridViewContentAlignment.TopLeft:
                    format.Alignment = StringAlignment.Near;
                    format.LineAlignment = StringAlignment.Near;
                    break;
                case DataGridViewContentAlignment.BottomRight:
                    format.Alignment = StringAlignment.Far;
                    format.LineAlignment = StringAlignment.Far;
                    break;
                case DataGridViewContentAlignment.MiddleRight:
                    format.Alignment = StringAlignment.Far;
                    format.LineAlignment = StringAlignment.Center;
                    break;
                case DataGridViewContentAlignment.TopRight:
                    format.Alignment = StringAlignment.Far;
                    format.LineAlignment = StringAlignment.Near;
                    break;
            }
            // Prepare fore color
            SolidBrush brush = null;
            bool disposeBrush = false;
            if (cell.Style.ForeColor.ToArgb() == Color.FromArgb(0, 0, 0, 0).ToArgb())
            {
                brush = (SolidBrush)Brushes.Black;
            }
            else
            {
                if (cell.Style.ForeColor.IsNamedColor)
                {
                    brush = (SolidBrush)BrushHelper.GetBrush(cell.Style.ForeColor);
                }
                else
                {
                    brush = new SolidBrush(cell.Style.ForeColor);
                    disposeBrush = true;
                }
            }
            // Prepare back color
            SolidBrush brushBack = null;
            bool disposeBrushBack = false;
            if (cell.Style.BackColor.ToArgb() == Color.FromArgb(0, 0, 0, 0).ToArgb())
            {
                brushBack = (SolidBrush)Brushes.White;
            }
            else
            {
                if (cell.Style.BackColor.IsNamedColor)
                {
                    brushBack = (SolidBrush)BrushHelper.GetBrush(cell.Style.BackColor);
                }
                else
                {
                    brushBack = new SolidBrush(cell.Style.BackColor);
                    disposeBrushBack = true;
                }
            }
            // Prepare font
            Font f = (cell.Style.Font != null) ? cell.Style.Font : font;
            // Prepare box
            DataGridViewAdvancedBorderStyle borderStyle = new DataGridViewAdvancedBorderStyle();
            if (m_DrawCellBox)
            {
                borderStyle.All = DataGridViewAdvancedCellBorderStyle.Single;
            }
            // Draw
            onDrawCell(g, s, layoutRect, format, f, brush, brushBack, borderStyle);
            // Check to dispose brush
            if (disposeBrush)
            {
                brush.Dispose();
            }
            if (disposeBrushBack)
            {
                brushBack.Dispose();
            }
        }

        /// <summary>
        /// Draw cell
        /// </summary>
        /// <param name="g">Graphics</param>
        /// <param name="s">String to draw</param>
        /// <param name="layoutRect">Rectangle for the layout</param>
        /// <param name="format">String format</param>
        /// <param name="font">Used font</param>
        /// <param name="brush">Fore brush</param>
        /// <param name="brushBack">Background brush</param>
        /// <param name="borderStyle">Border style</param>
        protected virtual void onDrawCell(Graphics g, string s, RectangleF layoutRect, StringFormat format, Font font, Brush brush, Brush brushBack, DataGridViewAdvancedBorderStyle borderStyle)
        {
            // Draw
            g.FillRectangle(brushBack, layoutRect);
            g.DrawString(s, font, brush, layoutRect, format);
            // Draw box
            drawBox(g, layoutRect, borderStyle);
        }

        /// <summary>
        /// Measure column header
        /// </summary>
        /// <param name="g">Graphics</param>
        /// <param name="font">Used font</param>
        /// <returns>Height of the header</returns>
        protected virtual float onMeasureColumnHeaderHeight(Graphics g, Font font)
        {
            float maxH = 0;

            for (int i = m_SelArea.StartCol; i <= m_SelArea.EndCol; i++)
            {
                string s = m_AttachedGrid.Columns[i].HeaderText;
                float w = m_AttachedGrid.Columns[i].Width;
                SizeF sizeF = g.MeasureString(s, font);
                if (sizeF.Height > maxH)
                {
                    maxH = sizeF.Height;
                }
            }
            return maxH;
        }

        /// <summary>
        /// Prepare to draw column headers
        /// </summary>
        /// <param name="g">Graphics</param>
        /// <param name="x">Draw header starting at X posision</param>
        /// <param name="y">Draw header starting at Y position</param>
        /// <param name="colStart">Start column to draw</param>
        /// <param name="colEnd">End column to draw</param>
        /// <param name="font">Used font</param>
        protected virtual void onPrepareColumnHeader(Graphics g, float x, float y, int colStart, int colEnd, Font font)
        {
            // Set format for header label
            StringFormat f = new StringFormat();
            f.Alignment = StringAlignment.Center;
            f.LineAlignment = StringAlignment.Center;
            float maxY = 0;

            // Draw headers
            for (int i = colStart; i <= colEnd; i++)
            {
                string s = m_AttachedGrid.Columns[i].HeaderText;
                float w = m_AttachedGrid.Columns[i].Width;
                SizeF sizeF = g.MeasureString(s, font);
                RectangleF layoutRect = new RectangleF(x, y, w, sizeF.Height);
                DataGridViewAdvancedBorderStyle borderStyle = new DataGridViewAdvancedBorderStyle();
                if (m_DrawCellBox)
                {
                    borderStyle.All = DataGridViewAdvancedCellBorderStyle.Single;
                }
                // Draw single header label and border
                onDrawColumnHeader(g, s, layoutRect, f, font, borderStyle);
                if (sizeF.Height > maxY)
                {
                    maxY = sizeF.Height;
                }
                x += w;
            }
        }

        /// <summary>
        /// Draw column header
        /// </summary>
        /// <param name="g">Graphics</param>
        /// <param name="s">Column header label</param>
        /// <param name="layoutRect">Rectangle for the layout</param>
        /// <param name="format">Format for the string</param>
        /// <param name="font">Used font</param>
        /// <param name="borderStyle">Style for border</param>
        protected virtual void onDrawColumnHeader(Graphics g, string s, RectangleF layoutRect, StringFormat format, Font font, DataGridViewAdvancedBorderStyle borderStyle)
        {
            g.DrawString(s, font, Brushes.Black, layoutRect, format);
            // Draw box
            drawBox(g, layoutRect, borderStyle);
        }

        /// <summary>
        /// Draw line box
        /// </summary>
        /// <param name="g">Graphics</param>
        /// <param name="layoutRect">Layout rectangle</param>
        /// <param name="borderStyle">Border style</param>
        private void drawBox(Graphics g, RectangleF layoutRect, DataGridViewAdvancedBorderStyle borderStyle)
        {
            if (borderStyle.All == DataGridViewAdvancedCellBorderStyle.Single)
            {
                g.DrawRectangle(Pens.Black, layoutRect.Left, layoutRect.Top, layoutRect.Width, layoutRect.Height);
            }
            else
            {
                if (borderStyle.Left == DataGridViewAdvancedCellBorderStyle.Single)
                {
                    g.DrawLine(Pens.Black, layoutRect.Left, layoutRect.Top, layoutRect.Left, layoutRect.Bottom);
                }
                if (borderStyle.Top == DataGridViewAdvancedCellBorderStyle.Single)
                {
                    g.DrawLine(Pens.Black, layoutRect.Left, layoutRect.Top, layoutRect.Right, layoutRect.Top);
                }
                if (borderStyle.Right == DataGridViewAdvancedCellBorderStyle.Single)
                {
                    g.DrawLine(Pens.Black, layoutRect.Right, layoutRect.Top, layoutRect.Right, layoutRect.Bottom);
                }
                if (borderStyle.Bottom == DataGridViewAdvancedCellBorderStyle.Single)
                {
                    g.DrawLine(Pens.Black, layoutRect.Left, layoutRect.Bottom, layoutRect.Right, layoutRect.Bottom);
                }
            }
        }

        /// <summary>
        /// OnPrintPage
        /// </summary>
        /// <param name="e">Print page arguments</param>
        protected override void OnPrintPage(PrintPageEventArgs e)
        {
            base.OnPrintPage(e);

            bool skip = false;
            do
            {
                bool hasMoreXPage = false;
                bool hasMoreYPage = false;
                int maxCol = 0;

                skip = false;
                m_PageCount++;

                if (this.DefaultPageSettings.PrinterSettings.FromPage != 0 && m_PageCount < this.DefaultPageSettings.PrinterSettings.FromPage)
                {
                    skip = true;
                }

                // Draw margin on preview if required
                if (m_ShowMargin && this.PrintController.IsPreview)
                {
                    Rectangle r = new Rectangle(e.MarginBounds.Left * 10, e.MarginBounds.Top * 10, e.MarginBounds.Width * 10, e.MarginBounds.Height * 10);
                    Rectangle rect = TransformHelper.Convert(r, PrinterUnit.ThousandthsOfAnInch, PrinterUnit.Display);
                    e.Graphics.DrawRectangle(Pens.Gray, rect);
                }

                m_PageSize = e.MarginBounds;

                // Set scale factor
                Matrix myMatrix = new Matrix();
                myMatrix.Scale(m_ScaleX, m_ScaleY, MatrixOrder.Append);
                e.Graphics.Transform = myMatrix;

                Matrix myMatrixInv = new Matrix();
                myMatrixInv.Scale(1 / m_ScaleX, 1 / m_ScaleY, MatrixOrder.Append);

                // Start to draw page
                e.HasMorePages = false;
                using (Graphics gr = m_AttachedGrid.CreateGraphics())
                {
                    PointF scaleMarginRB = TransformHelper.Transform(myMatrixInv, e.MarginBounds.Right, e.MarginBounds.Bottom);
                    PointF scaleMarginLT = TransformHelper.Transform(myMatrixInv, e.MarginBounds.Left, e.MarginBounds.Top);
                    float y = scaleMarginLT.Y;
                    float x = scaleMarginLT.X;
                    SizeF sizeF = SizeF.Empty;
                    SizeF cellSize = SizeF.Empty;
                    float maxX = 0;

                    // Check if draw header is required and measure header height
                    if (m_FirstPage && m_PrintHeader)
                    {
                        y += onMeasureColumnHeaderHeight(e.Graphics, m_Font);
                    }

                    // Save start page
                    m_PrevPageRowIndex = m_RowIndex;
                    // Start to check new page
                    while (m_RowIndex <= m_SelArea.EndRow)
                    {
                        // Start row for first col
                        m_ColIndex = m_PrevPageColIndex;
                        while (m_ColIndex <= m_SelArea.EndCol)
                        {
                            // max col for current page reach
                            if (hasMoreXPage && m_ColIndex >= maxCol)
                            {
                                break;
                            }
                            // Call cell size
                            DataGridViewCell cell = m_AttachedGrid[m_ColIndex, m_RowIndex];
                            string s = cell.FormattedValue.ToString();
                            cellSize = cell.Size;
                            sizeF = onMeasureCell(e.Graphics, cell, s, m_Font);
                            // Check Y : use sizeF.Height insteand of cellSize.Height if you want to measure string and not cell
                            //if (y + sizeF.Height > scaleMarginRB.Y)
                            if (y + cellSize.Height > scaleMarginRB.Y)
                            {
                                hasMoreYPage = true;
                                break;
                            }
                            // Check X : use sizeF.Width insteand of cellSize.Width if you want to measure string and not cell
                            //if (x + sizeF.Width > scaleMarginRB.X)
                            if (x + cellSize.Width > scaleMarginRB.X)
                            {
                                hasMoreXPage = true;
                                maxCol = m_ColIndex;
                                break;
                            }
                            // Print cell
                            RectangleF layoutRect = new RectangleF(new PointF(x, y), cellSize);
                            onPrepareDrawCell(e.Graphics, cell, s, layoutRect, m_Font);
                            // Move to next cell in X
                            x += cellSize.Width;
                            m_ColIndex++;
                        }
                        // If more page in Y change page
                        if (hasMoreYPage)
                        {
                            break;
                        }
                        maxX = x;
                        y += cellSize.Height;
                        x = scaleMarginLT.X;
                        m_ColIndex = m_PrevPageColIndex;
                        m_RowIndex++;
                    }
                    // Draw column header (called at end of page but draw at begin on page
                    if (m_FirstPage)
                    {
                        m_MaxWidth += (maxX - scaleMarginLT.X);
                        if (m_PrintHeader)
                        {
                            int maxc = (maxCol == 0) ? m_SelArea.EndCol : maxCol - 1;
                            onPrepareColumnHeader(e.Graphics, scaleMarginLT.X, scaleMarginLT.Y, m_PrevPageColIndex, maxc, m_Font);
                        }
                        if (!hasMoreXPage)
                        {
                            m_FirstPage = false;
                        }
                    }
                    // If more X page restore start row as this page and col from next col
                    if (hasMoreXPage)
                    {
                        m_RowIndex = m_PrevPageRowIndex;
                        m_PrevPageColIndex = maxCol;
                    }
                    else
                    {
                        m_MaxHeight += (y - scaleMarginLT.Y);
                        m_PrevPageColIndex = m_SelArea.StartCol;
                    }
                }
                // Set flag HasMorePages if there're more pages
                e.HasMorePages = hasMoreXPage || hasMoreYPage;
                if (this.DefaultPageSettings.PrinterSettings.ToPage != 0 && (m_PageCount + 1) > this.DefaultPageSettings.PrinterSettings.ToPage)
                {
                    e.HasMorePages = false;
                }
            } while (skip && e.HasMorePages);
        }
    }
}
