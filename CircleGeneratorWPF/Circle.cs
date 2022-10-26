using System;
using System.Collections.Generic;
using System.Linq;

namespace CircleGeneratorWPF
{
    public class Circle
    {
        public List<Point> Points { get; private set; }
        public int Radius { get; private set; }
        
        public int OffsetX;
        public int OffsetY;
        
        public int PixelSize { get; private set; }

        public Circle(int offsetX, int offsetY)
        {
            PixelSize = 2;
            Radius = 10;
            
            OffsetX = offsetX;
            OffsetY = offsetY;
            
            Points = new List<Point>();
            
            CreateList();
        }
        
        public void ChangeSize(int pixelSize)
        {
            PixelSize = pixelSize;
            
            foreach (var point in Points)
            {
                point.ChangeSize(PixelSize);
            }
        }
        
        private void CreateList()
        {
            for (var y = 0; y < Radius; ++y)
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
            
            SortAngle();
            
            ChangeSize(PixelSize);
        }
        
        private void SortAngle()
        {
            foreach (var point in Points)
            {
                point.Angle = Math.Atan2(point.X, -point.Y);
            }
            
            Points.Sort();
            Points = Points.Distinct().ToList();
        }

        public void ChangeRadius(int radius)
        {
            Radius = radius;
            
            Points.Clear();
            CreateList();
        }
    }
}