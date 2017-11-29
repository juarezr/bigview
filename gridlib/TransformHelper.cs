using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Printing;

namespace GridPrintPreviewLib
{
    /// <summary>
    /// Helper for Transfrom and Convert
    /// </summary>
    public class TransformHelper
    {
        /// <summary>
        /// Transform size using matrix
        /// </summary>
        /// <param name="matrix">Matrix for transform</param>
        /// <param name="size">Size to transform</param>
        /// <returns>Size transformed</returns>
        public static SizeF Transform(Matrix matrix, SizeF size)
        {
            PointF[] pts = new PointF[]{new PointF(size.Width, size.Height)};
            matrix.TransformPoints(pts);
            return new SizeF(pts[0].X,pts[0].Y); 
        }

        /// <summary>
        /// Transform point using matrix
        /// </summary>
        /// <param name="matrix">Matrix for transform</param>
        /// <param name="pt">Point to transform</param>
        /// <returns>Point transformed</returns>
        public static PointF Transform(Matrix matrix, PointF pt)
        {
            PointF[] pts = new PointF[] { pt };
            matrix.TransformPoints(pts);
            return pts[0];
        }

        /// <summary>
        /// Transform float x,y using matrix
        /// </summary>
        /// <param name="matrix">Matrix for transform</param>
        /// <param name="x">float x to transform</param>
        /// <param name="y">float y to transform</param>
        /// <returns>PointF (x,y) transformed</returns>
        public static PointF Transform(Matrix matrix, float x, float y)
        {
            PointF[] pts = new PointF[] { new PointF(x, y) };
            matrix.TransformPoints(pts);
            return pts[0];
        }

        /// <summary>
        /// Convert margins from unit to unit
        /// </summary>
        /// <param name="m">Source margins</param>
        /// <param name="source">Source unit</param>
        /// <param name="dest">Dest unit</param>
        /// <returns>Margins converted</returns>
        public static Margins Convert(Margins m, PrinterUnit source, PrinterUnit dest)
        {
            return PrinterUnitConvert.Convert(m, source, dest);
        }

        /// <summary>
        /// Convert rectangle from unit to unit
        /// </summary>
        /// <param name="r">Rectangle to convert</param>
        /// <param name="source">Source unit</param>
        /// <param name="dest">Dest unit</param>
        /// <returns>Rectangle converted</returns>
        public static Rectangle Convert(Rectangle r, PrinterUnit source, PrinterUnit dest)
        {
            return PrinterUnitConvert.Convert(r, source, dest);
        }

        /// <summary>
        /// Convert rectangle from unit to unit
        /// </summary>
        /// <param name="r">Rectangle to convert</param>
        /// <param name="source">Source unit</param>
        /// <param name="dest">Dest unit</param>
        /// <returns>Rectangle converted</returns>
        public static RectangleF Convert(RectangleF r, PrinterUnit source, PrinterUnit dest)
        {
            float l = (float)PrinterUnitConvert.Convert(r.Left, source, dest);
            float t = (float)PrinterUnitConvert.Convert(r.Top, source, dest);
            float w = (float)PrinterUnitConvert.Convert(r.Width, source, dest);
            float h = (float)PrinterUnitConvert.Convert(r.Height, source, dest);
            return new RectangleF(l, t, w, h);
        }

        /// <summary>
        /// Convert float from unit to unit
        /// </summary>
        /// <param name="x">Float to convert</param>
        /// <param name="source">Source unit</param>
        /// <param name="dest">Dest unit</param>
        /// <returns>Float converted</returns>
        public static float Convert(float x, PrinterUnit source, PrinterUnit dest)
        {
            return (float)PrinterUnitConvert.Convert(x, source, dest);
        }

        /// <summary>
        /// Convert paper size from unit to unit
        /// </summary>
        /// <param name="paperSize">Paper size to convert</param>
        /// <param name="source">Source unit</param>
        /// <param name="dest">Dest unit</param>
        /// <returns>Paper size converted</returns>
        public static PaperSize Convert(PaperSize paperSize, PrinterUnit source, PrinterUnit dest)
        {
            int w = PrinterUnitConvert.Convert(paperSize.Width, source, dest);
            int h = PrinterUnitConvert.Convert(paperSize.Height, source, dest);
            PaperSize ret = new PaperSize(paperSize.PaperName, w, h);
            ret.RawKind = paperSize.RawKind;
            return ret;
        }
    }
}
