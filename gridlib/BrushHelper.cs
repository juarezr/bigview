using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace GridPrintPreviewLib
{
    /// <summary>
    /// Helper for Brush
    /// </summary>
    class BrushHelper
    {
        /// <summary>
        /// Get the system brush from named color
        /// </summary>
        /// <param name="c">Color (name color)</param>
        /// <returns>System brush (to not dispose it)</returns>
        public static Brush GetBrush(Color c)
        {
            Brush b = null;
            switch (c.Name)
            {
                case "AliceBlue":
                    b = Brushes.AliceBlue;
                    break;
                case "AntiqueWhite":
                    b = Brushes.AntiqueWhite;
                    break;
                case "Aqua":
                    b = Brushes.Aqua;
                    break;
                case "Aquamarine":
                    b = Brushes.Aquamarine;
                    break;
                case "Azure":
                    b = Brushes.Azure;
                    break;
                case "Beige":
                    b = Brushes.Beige;
                    break;
                case "Bisque":
                    b = Brushes.Bisque;
                    break;
                case "Black":
                    b = Brushes.Black;
                    break;
                case "BlanchedAlmond":
                    b = Brushes.BlanchedAlmond;
                    break;
                case "Blue":
                    b = Brushes.Blue;
                    break;
                case "BlueViolet":
                    b = Brushes.BlueViolet;
                    break;
                case "Brown":
                    b = Brushes.Brown;
                    break;
                case "BurlyWood":
                    b = Brushes.BurlyWood;
                    break;
                case "CadetBlue":
                    b = Brushes.CadetBlue;
                    break;
                case "Chartreuse":
                    b = Brushes.Chartreuse;
                    break;
                case "Chocolate":
                    b = Brushes.Chocolate;
                    break;
                case "Coral":
                    b = Brushes.Coral;
                    break;
                case "CornflowerBlue":
                    b = Brushes.CornflowerBlue;
                    break;
                case "Cornsilk":
                    b = Brushes.Cornsilk;
                    break;
                case "Crimson":
                    b = Brushes.Crimson;
                    break;
                case "Cyan":
                    b = Brushes.Cyan;
                    break;
                case "DarkBlue":
                    b = Brushes.DarkBlue;
                    break;
                case "DarkCyan":
                    b = Brushes.DarkCyan;
                    break;
                case "DarkGoldenrod":
                    b = Brushes.DarkGoldenrod;
                    break;
                case "DarkGray":
                    b = Brushes.DarkGray;
                    break;
                case "DarkGreen":
                    b = Brushes.DarkGreen;
                    break;
                case "DarkKhaki":
                    b = Brushes.DarkKhaki;
                    break;
                case "DarkMagenta":
                    b = Brushes.DarkMagenta;
                    break;
                case "DarkOliveGreen":
                    b = Brushes.DarkOliveGreen;
                    break;
                case "DarkOrange":
                    b = Brushes.DarkOrange;
                    break;
                case "DarkOrchid":
                    b = Brushes.DarkOrchid;
                    break;
                case "DarkRed":
                    b = Brushes.DarkRed;
                    break;
                case "DarkSalmon":
                    b = Brushes.DarkSalmon;
                    break;
                case "DarkSeaGreen":
                    b = Brushes.DarkSeaGreen;
                    break;
                case "DarkSlateBlue":
                    b = Brushes.DarkSlateBlue;
                    break;
                case "DarkSlateGray":
                    b = Brushes.DarkSlateGray;
                    break;
                case "DarkTurquoise":
                    b = Brushes.DarkTurquoise;
                    break;
                case "DarkViolet":
                    b = Brushes.DarkViolet;
                    break;
                case "DeepPink":
                    b = Brushes.DeepPink;
                    break;
                case "DeepSkyBlue":
                    b = Brushes.DeepSkyBlue;
                    break;
                case "DimGray":
                    b = Brushes.DimGray;
                    break;
                case "DodgerBlue":
                    b = Brushes.DodgerBlue;
                    break;
                case "Firebrick":
                    b = Brushes.Firebrick;
                    break;
                case "FloralWhite":
                    b = Brushes.FloralWhite;
                    break;
                case "ForestGreen":
                    b = Brushes.ForestGreen;
                    break;
                case "Fuchsia":
                    b = Brushes.Fuchsia;
                    break;
                case "Gainsboro":
                    b = Brushes.Gainsboro;
                    break;
                case "GhostWhite":
                    b = Brushes.GhostWhite;
                    break;
                case "Gold":
                    b = Brushes.Gold;
                    break;
                case "Goldenrod":
                    b = Brushes.Goldenrod;
                    break;
                case "Gray":
                    b = Brushes.Gray;
                    break;
                case "Green":
                    b = Brushes.Green;
                    break;
                case "GreenYellow":
                    b = Brushes.GreenYellow;
                    break;
                case "Honeydew":
                    b = Brushes.Honeydew;
                    break;
                case "HotPink":
                    b = Brushes.HotPink;
                    break;
                case "IndianRed":
                    b = Brushes.IndianRed;
                    break;
                case "Indigo":
                    b = Brushes.Indigo;
                    break;
                case "Ivory":
                    b = Brushes.Ivory;
                    break;
                case "Khaki":
                    b = Brushes.Khaki;
                    break;
                case "Lavender":
                    b = Brushes.Lavender;
                    break;
                case "LavenderBlush":
                    b = Brushes.LavenderBlush;
                    break;
                case "LawnGreen":
                    b = Brushes.LawnGreen;
                    break;
                case "LemonChiffon":
                    b = Brushes.LemonChiffon;
                    break;
                case "LightBlue":
                    b = Brushes.LightBlue;
                    break;
                case "LightCoral":
                    b = Brushes.LightCoral;
                    break;
                case "LightCyan":
                    b = Brushes.LightCyan;
                    break;
                case "LightGoldenrodYellow":
                    b = Brushes.LightGoldenrodYellow;
                    break;
                case "LightGray":
                    b = Brushes.LightGray;
                    break;
                case "LightGreen":
                    b = Brushes.LightGreen;
                    break;
                case "LightPink":
                    b = Brushes.LightPink;
                    break;
                case "LightSalmon":
                    b = Brushes.LightSalmon;
                    break;
                case "LightSeaGreen":
                    b = Brushes.LightSeaGreen;
                    break;
                case "LightSkyBlue":
                    b = Brushes.LightSkyBlue;
                    break;
                case "LightSlateGray":
                    b = Brushes.LightSlateGray;
                    break;
                case "LightSteelBlue":
                    b = Brushes.LightSteelBlue;
                    break;
                case "LightYellow":
                    b = Brushes.LightYellow;
                    break;
                case "Lime":
                    b = Brushes.Lime;
                    break;
                case "LimeGreen":
                    b = Brushes.LimeGreen;
                    break;
                case "Linen":
                    b = Brushes.Linen;
                    break;
                case "Magenta":
                    b = Brushes.Magenta;
                    break;
                case "Maroon":
                    b = Brushes.Maroon;
                    break;
                case "MediumAquamarine":
                    b = Brushes.MediumAquamarine;
                    break;
                case "MediumBlue":
                    b = Brushes.MediumBlue;
                    break;
                case "MediumOrchid":
                    b = Brushes.MediumOrchid;
                    break;
                case "MediumPurple":
                    b = Brushes.MediumPurple;
                    break;
                case "MediumSeaGreen":
                    b = Brushes.MediumSeaGreen;
                    break;
                case "MediumSlateBlue":
                    b = Brushes.MediumSlateBlue;
                    break;
                case "MediumSpringGreen":
                    b = Brushes.MediumSpringGreen;
                    break;
                case "MediumTurquoise":
                    b = Brushes.MediumTurquoise;
                    break;
                case "MediumVioletRed":
                    b = Brushes.MediumVioletRed;
                    break;
                case "MidnightBlue":
                    b = Brushes.MidnightBlue;
                    break;
                case "MintCream":
                    b = Brushes.MintCream;
                    break;
                case "MistyRose":
                    b = Brushes.MistyRose;
                    break;
                case "Moccasin":
                    b = Brushes.Moccasin;
                    break;
                case "NavajoWhite":
                    b = Brushes.NavajoWhite;
                    break;
                case "Navy":
                    b = Brushes.Navy;
                    break;
                case "OldLace":
                    b = Brushes.OldLace;
                    break;
                case "Olive":
                    b = Brushes.Olive;
                    break;
                case "OliveDrab":
                    b = Brushes.OliveDrab;
                    break;
                case "Orange":
                    b = Brushes.Orange;
                    break;
                case "OrangeRed":
                    b = Brushes.OrangeRed;
                    break;
                case "Orchid":
                    b = Brushes.Orchid;
                    break;
                case "PaleGoldenrod":
                    b = Brushes.PaleGoldenrod;
                    break;
                case "PaleGreen":
                    b = Brushes.PaleGreen;
                    break;
                case "PaleTurquoise":
                    b = Brushes.PaleTurquoise;
                    break;
                case "PaleVioletRed":
                    b = Brushes.PaleVioletRed;
                    break;
                case "PapayaWhip":
                    b = Brushes.PapayaWhip;
                    break;
                case "PeachPuff":
                    b = Brushes.PeachPuff;
                    break;
                case "Peru":
                    b = Brushes.Peru;
                    break;
                case "Pink":
                    b = Brushes.Pink;
                    break;
                case "Plum":
                    b = Brushes.Plum;
                    break;
                case "PowderBlue":
                    b = Brushes.PowderBlue;
                    break;
                case "Purple":
                    b = Brushes.Purple;
                    break;
                case "Red":
                    b = Brushes.Red;
                    break;
                case "RosyBrown":
                    b = Brushes.RosyBrown;
                    break;
                case "RoyalBlue":
                    b = Brushes.RoyalBlue;
                    break;
                case "SaddleBrown":
                    b = Brushes.SaddleBrown;
                    break;
                case "Salmon":
                    b = Brushes.Salmon;
                    break;
                case "SandyBrown":
                    b = Brushes.SandyBrown;
                    break;
                case "SeaGreen":
                    b = Brushes.SeaGreen;
                    break;
                case "SeaShell":
                    b = Brushes.SeaShell;
                    break;
                case "Sienna":
                    b = Brushes.Sienna;
                    break;
                case "Silver":
                    b = Brushes.Silver;
                    break;
                case "SkyBlue":
                    b = Brushes.SkyBlue;
                    break;
                case "SlateBlue":
                    b = Brushes.SlateBlue;
                    break;
                case "SlateGray":
                    b = Brushes.SlateGray;
                    break;
                case "Snow":
                    b = Brushes.Snow;
                    break;
                case "SpringGreen":
                    b = Brushes.SpringGreen;
                    break;
                case "SteelBlue":
                    b = Brushes.SteelBlue;
                    break;
                case "Tan":
                    b = Brushes.Tan;
                    break;
                case "Teal":
                    b = Brushes.Teal;
                    break;
                case "Thistle":
                    b = Brushes.Thistle;
                    break;
                case "Tomato":
                    b = Brushes.Tomato;
                    break;
                case "Transparent":
                    b = Brushes.Transparent;
                    break;
                case "Turquoise":
                    b = Brushes.Turquoise;
                    break;
                case "Violet":
                    b = Brushes.Violet;
                    break;
                case "Wheat":
                    b = Brushes.Wheat;
                    break;
                case "White":
                    b = Brushes.White;
                    break;
                case "WhiteSmoke":
                    b = Brushes.WhiteSmoke;
                    break;
                case "Yellow":
                    b = Brushes.Yellow;
                    break;
                case "YellowGreen":
                    b = Brushes.YellowGreen;
                    break;
            }
            return b;
        }

        /// <summary>
        /// Get the system brush from color name
        /// </summary>
        /// <param name="name">Color name</param>
        /// <returns>System brush (to not dispose it)</returns>
        public static Brush GetBrush(string name)
        {
            Color c = Color.FromName(name);
            return GetBrush(c);
        }
    }
}
