using System;
using System.Windows.Media;
using System.Windows.Shapes;

namespace CircleGeneratorWPF
{
    public class Point : IComparable<Point>
    {
        public Rectangle Rect;
        
        public int X { get; }
        public int Y { get; }
        public double Angle { get; set; }
        
        private int _size;

        public Point(int x, int y)
        {
            X = x;
            Y = y;
            Angle = 0.0;

            ChangeSize(10);
        }

        public int CompareTo(Point other)
        {
            if (Angle < other.Angle)
            {
                return -1;
            }
            
            return Angle > other.Angle ? 1 : 0;
        }
        public void ChangeSize(int size) 
        {
            _size = size;
            
            Rect = new Rectangle
            {
                Width = _size,
                Height = _size,
                Fill = Brushes.Black
            };
        }
    }
}