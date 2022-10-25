using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace CircleGeneratorWPF
{
    class Point : IComparable<Point>
    {
        public int X;
        public int Y;
        public double Angle;
        private int Size;
        public Rectangle Rect ;
        public Point(int x, int y)
        {
            X = x;
            Y = y;
            Angle = 0.0;
            Size = 10;
            ChangeSize(Size);
        }

        public int CompareTo(Point other)
        {
            if(Angle < other.Angle)return -1;
            if (Angle == other.Angle) return 0;
            return 1;
        }
        public void ChangeSize(int size) {
            Size = size;
            Rect = new Rectangle
            {
                Width = Size,
                Height = Size,
                Fill = Brushes.Black
            };
        }
    }
}



