using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace CircleGeneratorWPF
{
    class Circle
    {
        public List<Point> Points;
        public int Radius;
        public int OffsetX;
        public int OffsetY;
        private int _size;
        public Circle(int radius, int offsetX, int offsetY)
        {
            _size = 2;
            Radius = radius;
            Points = new List<Point>();
            OffsetX = offsetX;
            OffsetY = offsetY;
            CreateList();
        }
        public void ChangeSize(int size = 2)
        {
            _size = size;
            foreach (var point in Points) point.ChangeSize(size);
        }
        private void CreateList()
        {
            for (int y = 0; y < Radius; ++y)
            {
                var x = (int)Math.Ceiling(Math.Sqrt((Radius * Radius) - y * y));
                Points.Add(new Point(x, y));
                Points.Add(new Point(y, x));
                Points.Add(new Point(-x, y));
                Points.Add(new Point(-y, x));
                Points.Add(new Point(x, -y));
                Points.Add(new Point(y, -x));
                Points.Add(new Point(-x, -y));
                Points.Add(new Point(-y, -x));
            }
            ParseAngle();
            ChangeSize(_size);
        }
        private void ParseAngle()
        {
            foreach(var point in Points) point.Angle = Math.Atan2(point.X, -point.Y);
            Points.Sort();
        }

        public void changeRadius(int radius)
        {
            Radius = radius;
            Points.Clear();
            CreateList();
        }
    }
}
