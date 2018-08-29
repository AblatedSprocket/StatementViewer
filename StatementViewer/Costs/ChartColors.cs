using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace StatementViewer.Costs
{
    internal static class ChartColors
    {
        //private static Color _red = Color.FromRgb(235, 63, 39);
        //private static Color _orange = Color.FromRgb(255, 164, 22);
        //private static Color _yellow = Color.FromRgb(235, 235, 74);
        //private static Color _green = Color.FromRgb(177, 235, 34);
        //private static Color _aqua = Color.FromRgb(72, 235, 156);
        //private static Color _blue = Color.FromRgb(28, 136, 235);
        //private static Color _purple = Color.FromRgb(158, 69, 235);
        //private static Color _pink = Color.FromRgb(235, 99, 174);

        private static Color _red = Color.FromRgb(255, 0, 0);
        private static Color _orange = Color.FromRgb(255, 128, 0);
        private static Color _yellow = Color.FromRgb(255, 255, 0);
        private static Color _green = Color.FromRgb(0, 255, 0);
        private static Color _skyBlue = Color.FromRgb(0, 255, 255);
        private static Color _blue = Color.FromRgb(0, 128, 255);
        private static Color _purple = Color.FromRgb(128, 0, 255);
        private static Color _grape = Color.FromRgb(128, 0, 128);
        private static Color _periwinkle = Color.FromRgb(128, 128, 255);
        private static Color _pink = Color.FromRgb(255, 128, 255);
        private static Color _peach = Color.FromRgb(255, 128, 128);
        private static Color _spring = Color.FromRgb(0, 255, 128);
        private static Color _bergundy = Color.FromRgb(128, 0, 0);

        public static SolidColorBrush Red { get { return new SolidColorBrush(_red); } }
        public static SolidColorBrush Orange { get { return new SolidColorBrush(_orange); } }
        public static SolidColorBrush Yellow { get { return new SolidColorBrush(_yellow); } }
        public static SolidColorBrush Green { get { return new SolidColorBrush(_green); } }
        public static SolidColorBrush SkyBlue { get { return new SolidColorBrush(_skyBlue); } }
        public static SolidColorBrush Blue { get { return new SolidColorBrush(_blue); } }
        public static SolidColorBrush Purple { get { return new SolidColorBrush(_purple); } }
        public static SolidColorBrush Grape { get { return new SolidColorBrush(_grape); } }
        public static SolidColorBrush Periwinkle { get { return new SolidColorBrush(_periwinkle); } }
        public static SolidColorBrush Pink { get { return new SolidColorBrush(_pink); } }
        public static SolidColorBrush Peach { get { return new SolidColorBrush(_peach); } }
        public static SolidColorBrush Spring { get { return new SolidColorBrush(_spring); } }
        public static SolidColorBrush Bergundy { get { return new SolidColorBrush(_bergundy); } }
        public static SolidColorBrush[] Colors = new SolidColorBrush[]
        {
           new SolidColorBrush(_red),
           new SolidColorBrush(_orange),
           new SolidColorBrush(_yellow),
           new SolidColorBrush(_green),
           new SolidColorBrush(_skyBlue),
           new SolidColorBrush(_blue),
           new SolidColorBrush(_purple),
           new SolidColorBrush(_grape),
           new SolidColorBrush(_periwinkle),
           new SolidColorBrush(_pink),
           new SolidColorBrush(_peach),
           new SolidColorBrush(_spring),
           new SolidColorBrush(_bergundy)
        };
    }
}
