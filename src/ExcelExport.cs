using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Threading;
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

        public static void ExportTo(DataGridView grid, string filePath)
        {
            var area = getSelectionArea(grid);

            int wpage = 0;
            int wrow = 1;
            using (var excel = new ExcelPackage())
            {
                ExcelWorksheet wsheet = null;

                for (int i = area.StartRow; i < area.EndRow; i++)
                {
                    var row = grid.Rows[i];

                    if (i > 1048576 || i == area.StartRow)
                    {
                        wpage += 1;
                        wsheet = excel.Workbook.Worksheets.Add("page" + wpage);
                        wrow = 1;

                        int hcol = 1;
                        for (int j = area.StartCol; j < area.EndCol; j++)
                        {
                            int ci = row.Cells[j].ColumnIndex;
                            var col = grid.Columns[ci];
                            string header = col.HeaderText;

                            var cell = wsheet.Cells[wrow, hcol];
                            SetCellValue(cell, header);

                            hcol += 1;
                        }

                        wrow += 1;
                    }


                    int wcol = 1;
                    for (int j = area.StartCol; j < area.EndCol; j++)
                    {
                        object value = row.Cells[j].Value;

                        var cell = wsheet.Cells[wrow, wcol];
                        SetCellValue(cell, value);

                        wcol += 1;
                    }

                    wrow += 1;
                }


                var finfo = new FileInfo(filePath);
                excel.SaveAs(finfo);
            }
        }

        private static void SetCellValue(ExcelRange cell, object value)
        {
            if (value is DateTime)
            {
                var valueRaw = GetValueAsDateTime(value);
                cell.Value = valueRaw;
                string dateTimeFormat = GetDateTimeFormat(Thread.CurrentThread.CurrentCulture);
                cell.Style.Numberformat.Format = dateTimeFormat;
                //cell.Style.Numberformat.Format = "dd/mm/yyyy hh:mm";
            }
            else if (value is TimeSpan)
            {
                var valueRaw = GetValueAsTimeSpan(value);
                cell.Value = valueRaw;
                cell.Style.Numberformat.Format = "[h]:mm:ss";
            }
            else if (value.IsNumeric())
            {
                cell.Value = value;
                string fmt = Thread.CurrentThread.CurrentCulture.NumberFormat.ToString();
                //TODO Check number format
                //cell.Style.Numberformat.Format = "#,##0.00";
                cell.Style.Numberformat.Format = fmt;
                cell.Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
            }
            else
            {
                if (value != null)
                {
                    var valtext = value.ToString();
                    if (!string.IsNullOrWhiteSpace(valtext))
                        cell.Value = valtext;
                }
            }

            //if (column.WrapText)
            //    cell.Style.WrapText = true;
        }

        private static object GetValueAsTimeSpan(object value)
        {
            var tvalue = (TimeSpan)value;
            var tz = TimeSpan.FromSeconds(0);
            if (tvalue == tz)
                return null;
            return tvalue;
        }

        private static object GetValueAsDateTime(object value)
        {
            var dvalue = (DateTime)value;
            if (!dvalue.IsValid())
                return null;

            return dvalue;
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

        #region Excel DateTime Formats

        public static string GetDateTimeFormat(CultureInfo culture)
        {
            if (_formats.ContainsKey(CultureInfo.CurrentCulture.Name))
                return _formats[CultureInfo.CurrentCulture.Name];
            else
                return _formats["pt-BR"];
        }

        internal static Dictionary<string, string> _formats = new Dictionary<string, string>{
            {"ar-DJ", "d/m/yyyy h:mm"},
            {"ar-EG", "dd/mm/yyyy hh:mm"},
            {"ar-ER", "d/m/yyyy h:mm"},
            {"ar-IQ", "dd/mm/yyyy hh:mm"},
            {"ar-IL", "d/m/yyyy h:mm"},
            {"ar-JO", "dd/mm/yyyy hh:mm"},
            {"ar-KW", "dd/mm/yyyy hh:mm"},
            {"ar-LB", "dd/mm/yyyy hh:mm"},
            {"ar-LY", "dd/mm/yyyy hh:mm"},
            {"ar-MR", "d/m/yyyy h:mm"},
            {"ar-MA", "dd-mm-yyyy h:mm"},
            {"ar-OM", "dd/mm/yyyy hh:mm"},
            {"ar-PS", "d/m/yyyy h:mm"},
            {"ar-QA", "dd/mm/yyyy hh:mm"},
            {"ar-SA", "dd/mm/yy hh:mm"},
            {"ar-SO", "d/m/yyyy h:mm"},
            {"ar-SS", "d/m/yyyy h:mm"},
            {"ar-SD", "d/m/yyyy h:mm"},
            {"ar-SY", "dd/mm/yyyy hh:mm"},
            {"ar-TN", "dd-mm-yyyy h:mm"},
            {"ar-AE", "dd/mm/yyyy hh:mm"},
            {"ar-001", "d/m/yyyy h:mm"},
            {"ar-YE", "dd/mm/yyyy hh:mm"},
            {"hy-AM", "dd.mm.yyyy hh:mm"},
            {"as-IN", "dd-mm-yyyy h:mm"},
            {"ast-ES", "d/m/yyyy hh:mm"},
            {"asa-TZ", "dd/mm/yyyy h:mm"},
            {"az-Cyrl-AZ", "dd.mm.yyyy hh:mm"},
            {"az-Latn-AZ", "dd.mm.yyyy hh:mm"},
            {"ksf-CM", "d/m/yyyy hh:mm"},
            {"bm-Latn-ML", "d/m/yyyy hh:mm"},
            {"bn-BD", "dd-mm-yy hh.mm"},
            {"bn-IN", "dd-mm-yy hh.mm"},
            {"bas-CM", "d/m/yyyy hh:mm"},
            {"ba-RU", "dd.ММ.ГГ ч:мм"},
            {"eu-ES", "yyyy/mm/dd hh:mm"},
            {"be-BY", "dd.mm.yy hh:mm"},
            {"bem-ZM", "dd/mm/yyyy h:mm"},
            {"bez-TZ", "dd/mm/yyyy h:mm"},
            {"byn-ER", "dd/mm/yyyy h:mm"},
            {"brx-IN", "m/d/yyyy h:mm"},
            {"bs-Cyrl-BA", "d.m.yyyy h:mm"},
            {"bs-Latn-BA", "dd.mm.yyyy hh:mm"},
            {"br-FR", "yyyy-mm-jj hh:mm"},
            {"bg-BG", "d.m.yyyy h:mm"},
            {"my-MM", "dd-mm-yyyy hh:mm"},
            {"ca-AD", "d/m/yyyy h:mm"},
            {"ca-ES", "d/m/yyyy h:mm"},
            {"ca-FR", "d/m/yyyy h:mm"},
            {"ca-IT", "d/m/yyyy h:mm"},
            {"tzm-Arab-MA", "d/m/yyyy hh:mm"},
            {"tzm-Latn-DZ", "dd-mm-yyyy h:mm"},
            {"tzm-Latn-MA", "dd/mm/yyyy h:mm"},
            {"tzm-Tfng-MA", "dd-mm-yyyy h:mm"},
            {"ku-Arab-IQ", "yyyy/mm/dd hh:mm"},
            {"chr-Cher-US", "m/d/yyyy h:mm"},
            {"cgg-UG", "dd/mm/yyyy h:mm"},
            {"zh-CN", "yyyy/m/d h:mm"},
            {"zh-Hans-HK", "d/m/yyyy h:mm"},
            {"zh-Hans-MO", "d/m/yyyy h:mm"},
            {"zh-SG", "d/m/yyyy h:mm"},
            {"zh-HK", "d/m/yyyy h:mm"},
            {"zh-MO", "d/m/yyyy h:mm"},
            {"zh-TW", "yyyy/m/d hh:mm"},
            {"swc-CD", "d/m/yyyy hh:mm"},
            {"kw-GB", "dd/mm/yyyy hh:mm"},
            {"co-FR", "dd/mm/yyyy h:mm"},
            {"hr-BA", "dd.mm.yyyy hh:mm"},
            {"hr-HR", "d.m.yyyy h:mm"},
            {"cs-CZ", "dd.mm.rrrr h:mm"},
            {"da-DK", "dd-mm-yyyy tt:mm"},
            {"da-GL", "dd/mm/yyyy hh.mm"},
            {"prs-AF", "yyyy/m/d h:mm"},
            {"dv-MV", "dd/mm/yy hh:mm"},
            {"dua-CM", "d/m/yyyy hh:mm"},
            {"nl-AW", "dd-mm-yyyy hh:mm"},
            {"nl-BE", "d/mm/yyyy u:mm"},
            {"nl-BQ", "dd-mm-yyyy hh:mm"},
            {"nl-CW", "dd-mm-yyyy hh:mm"},
            {"nl-NL", "d-m-yyyy uu:mm"},
            {"nl-SX", "dd-mm-yyyy hh:mm"},
            {"nl-SR", "dd-mm-yyyy hh:mm"},
            {"dz-BT", "yyyy-mm-dd h:mm"},
            {"bin-NG", "d/m/yyyy h:mm"},
            {"ebu-KE", "dd/mm/yyyy h:mm"},
            {"en-AS", "m/d/yyyy h:mm"},
            {"en-AI", "dd/mm/yyyy h:mm"},
            {"en-AG", "dd/mm/yyyy h:mm"},
            {"en-AU", "d/mm/yyyy h:mm"},
            {"en-BS", "dd/mm/yyyy h:mm"},
            {"en-BB", "dd/mm/yyyy h:mm"},
            {"en-BE", "dd/mm/yyyy h:mm"},
            {"en-BZ", "dd/mm/yyyy hh:mm"},
            {"en-BM", "dd/mm/yyyy h:mm"},
            {"en-BW", "dd/mm/yyyy h:mm"},
            {"en-IO", "dd/mm/yyyy h:mm"},
            {"en-VG", "dd/mm/yyyy h:mm"},
            {"en-CM", "dd/mm/yyyy h:mm"},
            {"en-CA", "yyyy-mm-dd h:mm"},
            {"en-029", "dd/mm/yyyy hh:mm"},
            {"en-KY", "dd/mm/yyyy h:mm"},
            {"en-CX", "dd/mm/yyyy h:mm"},
            {"en-CC", "dd/mm/yyyy h:mm"},
            {"en-CK", "dd/mm/yyyy h:mm"},
            {"en-DM", "dd/mm/yyyy h:mm"},
            {"en-ER", "dd/mm/yyyy h:mm"},
            {"en-150", "dd/mm/yyyy h:mm"},
            {"en-FK", "dd/mm/yyyy h:mm"},
            {"en-FJ", "dd/mm/yyyy h:mm"},
            {"en-GM", "dd/mm/yyyy h:mm"},
            {"en-GH", "dd/mm/yyyy h:mm"},
            {"en-GI", "dd/mm/yyyy h:mm"},
            {"en-GD", "dd/mm/yyyy h:mm"},
            {"en-GU", "m/d/yyyy h:mm"},
            {"en-GG", "dd/mm/yyyy h:mm"},
            {"en-GY", "dd/mm/yyyy h:mm"},
            {"en-HK", "d/m/yyyy h:mm"},
            {"en-IN", "dd-mm-yyyy hh:mm"},
            {"en-ID", "dd/mm/yyyy hh:mm"},
            {"en-IE", "dd/mm/yyyy hh:mm"},
            {"en-IM", "dd/mm/yyyy h:mm"},
            {"en-JM", "d/m/yyyy h:mm"},
            {"en-JE", "dd/mm/yyyy h:mm"},
            {"en-KE", "dd/mm/yyyy h:mm"},
            {"en-KI", "dd/mm/yyyy h:mm"},
            {"en-LS", "dd/mm/yyyy h:mm"},
            {"en-LR", "dd/mm/yyyy h:mm"},
            {"en-MO", "dd/mm/yyyy h:mm"},
            {"en-MG", "dd/mm/yyyy h:mm"},
            {"en-MW", "dd/mm/yyyy h:mm"},
            {"en-MY", "d/m/yyyy h:mm"},
            {"en-MT", "dd/mm/yyyy h:mm"},
            {"en-MH", "m/d/yyyy h:mm"},
            {"en-MU", "dd/mm/yyyy h:mm"},
            {"en-FM", "dd/mm/yyyy h:mm"},
            {"en-MS", "dd/mm/yyyy h:mm"},
            {"en-NA", "dd/mm/yyyy h:mm"},
            {"en-NR", "dd/mm/yyyy h:mm"},
            {"en-NZ", "d/mm/yyyy h:mm"},
            {"en-NG", "dd/mm/yyyy h:mm"},
            {"en-NU", "dd/mm/yyyy h:mm"},
            {"en-NF", "dd/mm/yyyy h:mm"},
            {"en-MP", "m/d/yyyy h:mm"},
            {"en-PK", "dd/mm/yyyy h:mm"},
            {"en-PW", "dd/mm/yyyy h:mm"},
            {"en-PG", "dd/mm/yyyy h:mm"},
            {"en-PH", "dd/mm/yyyy h:mm"},
            {"en-PN", "dd/mm/yyyy h:mm"},
            {"en-PR", "m/d/yyyy h:mm"},
            {"en-RW", "dd/mm/yyyy h:mm"},
            {"en-KN", "dd/mm/yyyy h:mm"},
            {"en-LC", "dd/mm/yyyy h:mm"},
            {"en-VC", "dd/mm/yyyy h:mm"},
            {"en-WS", "dd/mm/yyyy h:mm"},
            {"en-SC", "dd/mm/yyyy h:mm"},
            {"en-SL", "dd/mm/yyyy h:mm"},
            {"en-SG", "d/m/yyyy h:mm"},
            {"en-SX", "dd/mm/yyyy h:mm"},
            {"en-SB", "dd/mm/yyyy h:mm"},
            {"en-ZA", "yyyy/mm/dd h:mm"},
            {"en-SS", "dd/mm/yyyy h:mm"},
            {"en-SH", "dd/mm/yyyy h:mm"},
            {"en-SD", "dd/mm/yyyy h:mm"},
            {"en-SZ", "dd/mm/yyyy h:mm"},
            {"en-TZ", "dd/mm/yyyy h:mm"},
            {"en-TK", "dd/mm/yyyy h:mm"},
            {"en-TO", "dd/mm/yyyy h:mm"},
            {"en-TT", "dd/mm/yyyy h:mm"},
            {"en-TC", "dd/mm/yyyy h:mm"},
            {"en-TV", "dd/mm/yyyy h:mm"},
            {"en-UG", "dd/mm/yyyy h:mm"},
            {"en-GB", "dd/mm/yyyy hh:mm"},
            {"en-US", "m/d/yyyy h:mm"},
            {"en", "m/d/yyyy h:mm"},
            {"en-UM", "m/d/yyyy h:mm"},
            {"en-VI", "m/d/yyyy h:mm"},
            {"en-VU", "dd/mm/yyyy h:mm"},
            {"en-001", "dd/mm/yyyy h:mm"},
            {"en-ZM", "dd/mm/yyyy h:mm"},
            {"en-ZW", "d/m/yyyy h:mm"},
            {"eo-001", "yyyy-mm-dd hh:mm"},
            {"et-EE", "dd.mm.yyyy h:mm"},
            {"ee-GH", "m/d/yyyy h:mm"},
            {"ee-TG", "m/d/yyyy h:mm"},
            {"ewo-CM", "d/m/yyyy hh:mm"},
            {"fo-FO", "dd-mm-yyyy hh:mm"},
            {"fil-PH", "m/d/yyyy h:mm"},
            {"fi-FI", "d.m.yyyy t:mm"},
            {"fr-DZ", "dd/mm/yyyy hh:mm"},
            {"fr-BE", "jj-mm-aa hh:mm"},
            {"fr-BJ", "dd/mm/yyyy hh:mm"},
            {"fr-BF", "dd/mm/yyyy hh:mm"},
            {"fr-BI", "dd/mm/yyyy hh:mm"},
            {"fr-CM", "dd/mm/yyyy hh:mm"},
            {"fr-CA", "yyyy-mm-jj hh:mm"},
            {"fr-029", "dd/mm/yyyy hh:mm"},
            {"fr-CF", "dd/mm/yyyy hh:mm"},
            {"fr-TD", "dd/mm/yyyy hh:mm"},
            {"fr-KM", "dd/mm/yyyy hh:mm"},
            {"fr-CG", "dd/mm/yyyy hh:mm"},
            {"fr-CD", "dd/mm/yyyy hh:mm"},
            {"fr-CI", "dd/mm/yyyy hh:mm"},
            {"fr-DJ", "dd/mm/yyyy hh:mm"},
            {"fr-GQ", "dd/mm/yyyy hh:mm"},
            {"fr-FR", "dd/mm/yyyy hh:mm"},
            {"fr-GF", "dd/mm/yyyy hh:mm"},
            {"fr-PF", "dd/mm/yyyy hh:mm"},
            {"fr-GA", "dd/mm/yyyy hh:mm"},
            {"fr-GP", "dd/mm/yyyy hh:mm"},
            {"fr-GN", "dd/mm/yyyy hh:mm"},
            {"fr-HT", "dd/mm/yyyy hh:mm"},
            {"fr-LU", "dd/mm/yyyy hh:mm"},
            {"fr-MG", "dd/mm/yyyy hh:mm"},
            {"fr-ML", "dd/mm/yyyy hh:mm"},
            {"fr-MQ", "dd/mm/yyyy hh:mm"},
            {"fr-MR", "dd/mm/yyyy hh:mm"},
            {"fr-MU", "dd/mm/yyyy hh:mm"},
            {"fr-YT", "dd/mm/yyyy hh:mm"},
            {"fr-MC", "dd/mm/yyyy hh:mm"},
            {"fr-MA", "dd/mm/yyyy hh:mm"},
            {"fr-NC", "dd/mm/yyyy hh:mm"},
            {"fr-NE", "dd/mm/yyyy hh:mm"},
            {"fr-RE", "dd/mm/yyyy hh:mm"},
            {"fr-RW", "dd/mm/yyyy hh:mm"},
            {"fr-BL", "dd/mm/yyyy hh:mm"},
            {"fr-MF", "dd/mm/yyyy hh:mm"},
            {"fr-PM", "dd/mm/yyyy hh:mm"},
            {"fr-SN", "dd/mm/yyyy hh:mm"},
            {"fr-SC", "dd/mm/yyyy hh:mm"},
            {"fr-CH", "dd.mm.yyyy hh:mm"},
            {"fr-SY", "dd/mm/yyyy hh:mm"},
            {"fr-TG", "dd/mm/yyyy hh:mm"},
            {"fr-TN", "dd/mm/yyyy hh:mm"},
            {"fr-VU", "dd/mm/yyyy hh:mm"},
            {"fr-WF", "dd/mm/yyyy hh:mm"},
            {"fy-NL", "dd-mm-yyyy uu:mm"},
            {"fur-IT", "dd/mm/yyyy hh:mm"},
            {"ff-CM", "d/m/yyyy hh:mm"},
            {"ff-GN", "d/m/yyyy hh:mm"},
            {"ff-Latn-SN", "dd/mm/yyyy hh:mm"},
            {"ff-MR", "d/m/yyyy hh:mm"},
            {"ff-NG", "d/m/yyyy hh:mm"},
            {"gl-ES", "dd/mm/yyyy hh:mm"},
            {"lg-UG", "dd/mm/yyyy h:mm"},
            {"ka-GE", "dd.mm.yyyy hh:mm"},
            {"de-AT", "DD.MM.yyyy hh:mm"},
            {"de-BE", "DD.MM.yyyy hh:mm"},
            {"de-DE", "DD.MM.yyyy hh:mm"},
            {"de-LI", "dd.mm.yyyy hh:mm"},
            {"de-LU", "dd.mm.yyyy hh:mm"},
            {"de-CH", "DD.MM.yyyy hh:mm"},
            {"el-CY", "d/m/yyyy h:mm"},
            {"el-GR", "d/m/yyyy h:mm"},
            {"kl-GL", "dd-mm-yyyy hh:mm"},
            {"gn-PY", "dd/mm/yyyy hh:mm"},
            {"gu-IN", "dd-mm-yy hh:mm"},
            {"guz-KE", "dd/mm/yyyy h:mm"},
            {"ha-Latn-GH", "d/m/yyyy hh:mm"},
            {"ha-Latn-NE", "d/m/yyyy hh:mm"},
            {"ha-Latn-NG", "d/m/yyyy hh:mm"},
            {"haw-US", "d/m/yyyy h:mm"},
            {"he-IL", "dd/mm/yyyy hh:mm"},
            {"hi-IN", "dd-mm-yyyy hh:mm"},
            {"hu-HU", "yyyy.mm.dd ó:pp"},
            {"ibb-NG", "d/m/yyyy h:mm"},
            {"is-IS", "d.m.yyyy hh:mm"},
            {"ig-NG", "dd/mm/yyyy h:mm"},
            {"id-ID", "dd/mm/yyyy hh.mm"},
            {"ia-FR", "yyyy/mm/dd hh:mm"},
            {"ia-001", "yyyy/mm/dd hh:mm"},
            {"iu-Latn-CA", "d/mm/yyyy h:mm"},
            {"iu-Cans-CA", "d/m/yyyy h:mm"},
            {"ga-IE", "dd/mm/yyyy hh:mm"},
            {"xh-ZA", "yyyy-mm-dd hh:mm"},
            {"zu-ZA", "m/d/yyyy h:mm"},
            {"it-IT", "gg/mm/yyyy hh:mm"},
            {"it-SM", "dd/mm/yyyy hh:mm"},
            {"it-CH", "dd.mm.yyyy hh:mm"},
            {"ja-JP", "yyyy/mm/dd h:mm"},
            {"jv-Latn-ID", "dd/mm/yyyy hh.mm"},
            {"jv-Java-ID", "dd/mm/yyyy hh.mm"},
            {"dyo-SN", "d/m/yyyy hh:mm"},
            {"kea-CV", "d/m/yyyy hh:mm"},
            {"kab-DZ", "d/m/yyyy hh:mm"},
            {"kkj-CM", "dd/mm/yyyy hh:mm"},
            {"kln-KE", "dd/mm/yyyy h:mm"},
            {"kam-KE", "dd/mm/yyyy h:mm"},
            {"kn-IN", "dd-mm-yy hh:mm"},
            {"kr-NG", "d/m/yyyy h:mm"},
            {"ks-Deva-IN", "dd-mm-yyyy hh:mm"},
            {"ks-Arab-IN", "m/d/yyyy h:mm"},
            {"kk-KZ", "dd/ММ/yyyy чч:мм"},
            {"km-KH", "dd/mm/yy hh:mm"},
            {"quc-Latn-GT", "dd/mm/yyyy h:mm"},
            {"ki-KE", "dd/mm/yyyy h:mm"},
            {"rw-RW", "yyyy/mm/dd hh:mm"},
            {"sw-CD", "d/m/yyyy hh:mm"},
            {"sw-KE", "dd/mm/yyyy h:mm"},
            {"sw-TZ", "dd/mm/yyyy h:mm"},
            {"sw-UG", "dd/mm/yyyy h:mm"},
            {"kok-IN", "dd-mm-yyyy hh:mm"},
            {"ko-KR", "yyyy-mm-dd h:mm"},
            {"ses-ML", "d/m/yyyy hh:mm"},
            {"khq-ML", "d/m/yyyy hh:mm"},
            {"nmg-CM", "d/m/yyyy hh:mm"},
            {"ky-KG", "d-mm-yy hh:mm"},
            {"lkt-US", "m/d/yyyy h:mm"},
            {"lag-TZ", "dd/mm/yyyy h:mm"},
            {"lo-LA", "d/m/yyyy h:mm"},
            {"la-001", "dd/mm/yyyy hh:mm"},
            {"lv-LV", "dd.mm.yyyy hh:mm"},
            {"ln-AO", "d/m/yyyy hh:mm"},
            {"ln-CF", "d/m/yyyy hh:mm"},
            {"ln-CG", "d/m/yyyy hh:mm"},
            {"ln-CD", "d/m/yyyy hh:mm"},
            {"lt-LT", "yyyy-mm-dd hh:mm"},
            {"dsb-DE", "D.M.yyyy hh:mm"},
            {"lu-CD", "d/m/yyyy hh:mm"},
            {"luo-KE", "dd/mm/yyyy h:mm"},
            {"lb-LU", "dd.mm.yy hh:mm"},
            {"luy-KE", "dd/mm/yyyy h:mm"},
            {"mk-MK", "dd.m.yyyy hh:mm"},
            {"jmc-TZ", "dd/mm/yyyy h:mm"},
            {"mgh-MZ", "dd/mm/yyyy h:mm"},
            {"kde-TZ", "dd/mm/yyyy h:mm"},
            {"mg-MG", "d/m/yyyy hh:mm"},
            {"ml-IN", "dd-mm-yy hh.mm"},
            {"ms-BN", "d/mm/yyyy h:mm"},
            {"ms-MY", "d/mm/yyyy h:mm"},
            {"ms-SG", "d/mm/yyyy h:mm"},
            {"mt-MT", "dd/mm/yyyy hh:mm"},
            {"mni-IN", "dd/mm/yyyy hh:mm"},
            {"gv-IM", "dd/mm/yyyy hh:mm"},
            {"mi-NZ", "dd/mm/yyyy h:mm"},
            {"arn-CL", "dd-mm-yyyy h:mm"},
            {"mr-IN", "dd-mm-yyyy hh:mm"},
            {"mas-KE", "dd/mm/yyyy h:mm"},
            {"mas-TZ", "dd/mm/yyyy h:mm"},
            {"mer-KE", "dd/mm/yyyy h:mm"},
            {"mgo-CM", "yyyy-mm-dd hh:mm"},
            {"moh-CA", "m/d/yyyy h:mm"},
            {"mn-MN", "yyyy-mm-dd hh:mm"},
            {"mn-Mong-CN", "yyyy/m/d h:mm"},
            {"mn-Mong-MN", "yyyy/m/d h:mm"},
            {"mfe-MU", "d/m/yyyy hh:mm"},
            {"mua-CM", "d/m/yyyy hh:mm"},
            {"naq-NA", "dd/mm/yyyy h:mm"},
            {"ne-IN", "yyyy-mm-dd hh:mm"},
            {"ne-NP", "m/d/yyyy h:mm"},
            {"nnh-CM", "dd/mm/yyyy hh:mm"},
            {"jgo-CM", "yyyy-mm-dd hh:mm"},
            {"nqo-GN", "dd/mm/yyyy hh:mm"},
            {"nd-ZW", "dd/mm/yyyy h:mm"},
            {"nb-NO", "dd.mm.yyyy tt:mm"},
            {"nb-SJ", "dd.mm.yyyy tt:mm"},
            {"nn-NO", "dd.mm.yyyy tt:mm"},
            {"nus-SS", "d/mm/yyyy h:mm"},
            {"nyn-UG", "dd/mm/yyyy h:mm"},
            {"oc-FR", "dd/mm/yyyy hh.mm"},
            {"or-IN", "dd-mm-yy hh:mm"},
            {"om-ET", "dd/mm/yyyy h:mm"},
            {"om-KE", "dd/mm/yyyy h:mm"},
            {"os-GE", "dd.mm.yyyy hh:mm"},
            {"os-RU", "dd.ММ.yyyy hh:mm"},
            {"pap-029", "d-m-yyyy h:mm"},
            {"ps-AF", "yyyy/m/d h:mm"},
            {"fa-IR", "dd/mm/yyyy hh:mm"},
            {"pl-PL", "dd.mm.rrrr gg:mm"},
            {"pt-AO", "dd/mm/yyyy hh:mm"},
            {"pt-BR", "dd/mm/yyyy hh:mm"},
            {"pt-CV", "dd/mm/yyyy hh:mm"},
            {"pt-GW", "dd/mm/yyyy hh:mm"},
            {"pt-MO", "dd/mm/yyyy hh:mm"},
            {"pt-MZ", "dd/mm/yyyy hh:mm"},
            {"pt-PT", "dd/mm/yyyy hh:mm"},
            {"pt-ST", "dd/mm/yyyy hh:mm"},
            {"pt-TL", "dd/mm/yyyy hh:mm"},
            {"pa-IN", "dd-mm-yy hh:mm"},
            {"pa-Arab-PK", "dd-mm-yy h.mm"},
            {"quz-BO", "dd/mm/yyyy hh:mm"},
            {"quz-PE", "dd/mm/yyyy hh:mm"},
            {"quz-EC", "dd/mm/yyyy h:mm"},
            {"ksh-DE", "D.M.yyyy hh:mm"},
            {"ro-MD", "dd.mm.yyyy hh:mm"},
            {"ro-RO", "dd.mm.yyyy hh:mm"},
            {"rm-CH", "dd-mm-yyyy hh:mm"},
            {"rof-TZ", "dd/mm/yyyy h:mm"},
            {"rn-BI", "d/m/yyyy hh:mm"},
            {"ru-BY", "dd.mm.yyyy h:mm"},
            {"ru-KZ", "dd.ММ.yyyy ч:мм"},
            {"ru-KG", "dd.mm.yyyy h:mm"},
            {"ru-MD", "dd.mm.yyyy h:mm"},
            {"ru-RU", "dd.ММ.yyyy ч:мм"},
            {"ru-UA", "dd.mm.yyyy hh:mm"},
            {"rwk-TZ", "dd/mm/yyyy h:mm"},
            {"ssy-ER", "dd/mm/yyyy h:mm"},
            {"sah-RU", "dd.ММ.yyyy ч:мм"},
            {"saq-KE", "dd/mm/yyyy h:mm"},
            {"smn-FI", "p.k.yyyy t:mm"},
            {"smj-NO", "dd.mm.yyyy tt:mm"},
            {"smj-SE", "yyyy-MM-DD tt:mm"},
            {"se-FI", "d.m.yyyy t:mm"},
            {"se-NO", "yyyy-mm-dd tt:mm"},
            {"se-SE", "yyyy-MM-DD tt:mm"},
            {"sms-FI", "d.m.yyyy t:mm"},
            {"sma-NO", "dd.mm.yyyy tt:mm"},
            {"sma-SE", "yyyy-MM-DD tt:mm"},
            {"sg-CF", "d/m/yyyy hh:mm"},
            {"sbp-TZ", "dd/mm/yyyy h:mm"},
            {"sa-IN", "dd-mm-yyyy hh:mm"},
            {"gd-GB", "dd/mm/yyyy hh:mm"},
            {"seh-MZ", "d/m/yyyy hh:mm"},
            {"sr-Cyrl-BA", "d.m.yyyy h:mm"},
            {"sr-Cyrl-XK", "d.m.yyyy hh:mm"},
            {"sr-Cyrl-ME", "d.m.yyyy h:mm"},
            {"sr-Cyrl-RS", "dd.mm.yyyy h:mm"},
            {"sr-Latn-BA", "d.m.yyyy hh:mm"},
            {"sr-Latn-XK", "d.m.yyyy hh:mm"},
            {"sr-Latn-ME", "d.m.yyyy hh:mm"},
            {"sr-Latn-RS", "d.m.yyyy hh:mm"},
            {"st-LS", "yyyy-mm-dd hh:mm"},
            {"nso-ZA", "yyyy-mm-dd hh:mm"},
            {"st-ZA", "yyyy-mm-dd hh:mm"},
            {"tn-BW", "yyyy-mm-dd hh:mm"},
            {"tn-ZA", "yyyy-mm-dd hh:mm"},
            {"ksb-TZ", "dd/mm/yyyy h:mm"},
            {"sn-Latn-ZW", "dd/mm/yyyy h:mm"},
            {"sd-Deva-IN", "dd/mm/yyyy hh:mm"},
            {"sd-Arab-PK", "dd/mm/yyyy h:mm"},
            {"si-LK", "yyyy-mm-dd h.mm"},
            {"ss-ZA", "yyyy-mm-dd hh:mm"},
            {"ss-SZ", "yyyy-mm-dd hh:mm"},
            {"sk-SK", "dd.mm.yyyy h:mm"},
            {"sl-SI", "d.mm.yyyy hh:mm"},
            {"xog-UG", "dd/mm/yyyy h:mm"},
            {"so-DJ", "dd/mm/yyyy h:mm"},
            {"so-ET", "dd/mm/yyyy h:mm"},
            {"so-KE", "dd/mm/yyyy h:mm"},
            {"so-SO", "dd/mm/yyyy h:mm"},
            {"nr-ZA", "yyyy-mm-dd hh:mm"},
            {"es-AR", "d/m/yyyy h:mm"},
            {"es-BO", "d/m/yyyy h:mm"},
            {"es-CL", "dd-mm-yyyy h:mm"},
            {"es-CO", "d/mm/yyyy h:mm"},
            {"es-CR", "d/m/yyyy h:mm"},
            {"es-CU", "d/m/yyyy h:mm"},
            {"es-DO", "d/m/yyyy h:mm"},
            {"es-EC", "d/m/yyyy h:mm"},
            {"es-SV", "d/m/yyyy h:mm"},
            {"es-GQ", "d/m/yyyy h:mm"},
            {"es-GT", "d/mm/yyyy h:mm"},
            {"es-HN", "d/m/yyyy h:mm"},
            {"es-419", "d/m/yyyy hh:mm"},
            {"es-MX", "dd/mm/yyyy hh:mm"},
            {"es-NI", "d/m/yyyy h:mm"},
            {"es-PA", "mm/dd/yyyy h:mm"},
            {"es-PY", "d/m/yyyy h:mm"},
            {"es-PE", "d/mm/yyyy h:mm"},
            {"es-PH", "d/m/yyyy h:mm"},
            {"es-PR", "mm/dd/yyyy h:mm"},
            {"es-ES", "dd/mm/yyyy h:mm"},
            {"es-US", "m/d/yyyy h:mm"},
            {"es-UY", "d/m/yyyy h:mm"},
            {"es-VE", "d/m/yyyy h:mm"},
            {"zgh-Tfng-MA", "d/m/yyyy hh:mm"},
            {"sv-AX", "yyyy-mm-pp tt:mm"},
            {"sv-FI", "dd-mm-yyyy tt:mm"},
            {"sv-SE", "yyyy-MM-DD tt:mm"},
            {"syr-SY", "dd/mm/yyyy hh:mm"},
            {"shi-Latn-MA", "d/m/yyyy hh:mm"},
            {"shi-Tfng-MA", "d/m/yyyy hh:mm"},
            {"dav-KE", "dd/mm/yyyy h:mm"},
            {"tg-Cyrl-TJ", "dd.mm.yyyy hh:mm"},
            {"ta-IN", "dd-mm-yyyy hh:mm"},
            {"ta-MY", "d-m-yyyy h:mm"},
            {"ta-SG", "d-m-yyyy h:mm"},
            {"ta-LK", "d-m-yyyy h:mm"},
            {"twq-NE", "d/m/yyyy hh:mm"},
            {"tt-RU", "dd.ММ.yyyy чч:мм"},
            {"te-IN", "dd-mm-yy hh:mm"},
            {"teo-KE", "dd/mm/yyyy h:mm"},
            {"teo-UG", "dd/mm/yyyy h:mm"},
            {"th-TH", "d/m/yyyy h:mm"},
            {"bo-CN", "yyyy/m/d hh:mm"},
            {"bo-IN", "yyyy-mm-dd hh:mm"},
            {"tig-ER", "dd/mm/yyyy h:mm"},
            {"ti-ER", "dd/mm/yyyy h:mm"},
            {"ti-ET", "dd/mm/yyyy h:mm"},
            {"to-TO", "d/m/yyyy h:mm"},
            {"tr-CY", "d.mm.yyyy hh:mm"},
            {"tr-TR", "d.mm.yyyy ss:dd"},
            {"tk-TM", "dd.mm.yy hh:mm"},
            {"uk-UA", "dd.mm.yyyy h:mm"},
            {"hsb-DE", "D.M.yyyy h:mm"},
            {"ur-IN", "d/m/yy h:mm"},
            {"ur-PK", "dd/mm/yyyy h:mm"},
            {"ug-CN", "yyyy-m-d h:mm"},
            {"uz-Cyrl-UZ", "yyyy/mm/dd hh:mm"},
            {"uz-Latn-UZ", "yyyy/mm/dd hh:mm"},
            {"uz-Arab-AF", "dd/mm/yyyy h:mm"},
            {"vai-Latn-LR", "dd/mm/yyyy h:mm"},
            {"vai-Vaii-LR", "dd/mm/yyyy h:mm"},
            {"ca-ES-valencia", "d/m/yyyy h:mm"},
            {"ve-ZA", "yyyy-mm-dd hh:mm"},
            {"vi-VN", "dd/mm/yyyy h:mm"},
            {"vo-001", "yyyy-mm-dd hh:mm"},
            {"vun-TZ", "dd/mm/yyyy h:mm"},
            {"wae-CH", "yyyy-mm-dd hh:mm"},
            {"cy-GB", "dd/mm/yyyy hh:mm"},
            {"wal-ET", "dd/mm/yyyy h:mm"},
            {"wo-SN", "dd/mm/yyyy hh:mm"},
            {"ts-ZA", "yyyy-mm-dd hh:mm"},
            {"yav-CM", "d/m/yyyy hh:mm"},
            {"ii-CN", "yyyy/m/d h:mm"},
            {"yi-001", "dd/mm/yyyy hh:mm"},
            {"yo-BJ", "dd/mm/yyyy h:mm"},
            {"yo-NG", "dd/mm/yyyy h:mm"},
            {"dje-NE", "d/m/yyyy hh:mm"}
        };

        #endregion Excel DateTime Formats

    }

}
