using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace CircleGeneratorWPF
{
    class Circle
    {
        public List<Point> Points;
        private int _radius;
        public int OffsetX;
        public int OffsetY;
        public Circle(int radius, int offsetX, int offsetY)
        {
            _radius = radius;
            Points = new List<Point>();
            OffsetX = offsetX;
            OffsetY = offsetY;
            CreateList();
        }
        public void ChangeSize(int size)
        {
            foreach(var point in Points) point.ChangeSize(size);
        }
        private void CreateList()
        {
            for (int y = 0; y < _radius; ++y)
            {
                var x = (int)Math.Ceiling(Math.Sqrt((_radius * _radius) - y * y));
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
        }
        private void ParseAngle()
        {
            foreach(var point in Points) point.Angle = Math.Atan2(point.X, -point.Y);
            Points.Sort();
        }

        public void changeRadius(int radius)
        {
            _radius = radius;
            Points.Clear();
            CreateList();
        }

    }
}
