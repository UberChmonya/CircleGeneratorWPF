using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static System.Net.Mime.MediaTypeNames;

namespace CircleGeneratorWPF
{
    public partial class MainWindow : Window
    {
        private int _pixelSize;
        private Circle _circle;
        private Line _line;

        public MainWindow()
        {
            _circle = new Circle(40, 10, 10);
            _line = new Line();
            canvas = new Canvas();
            _pixelSize = 1;
            DrawInformationLayer();
        }

        private void DrawInformationLayer()
        {
            _line.StrokeThickness = 4;
            _line.Stroke = Brushes.Black;
            _line.X1 = 1000;
            _line.X2 = 1000;
            _line.Y1 = 0;
            _line.Y2 = 1000;

            canvas.Children.Add(_line);
        }
        
        private void Slider_Size(object sender, RoutedPropertyChangedEventArgs<double> slider) 
        {
            _pixelSize = (int)slider.NewValue;
            _circle.ChangeSize(_pixelSize);
            Draw_Circle();
        }
        private void Slider_Radius(object sender, RoutedPropertyChangedEventArgs<double> slider)
        {
            int radius = (int)slider.NewValue;
            _circle.changeRadius(radius);
            Draw_Circle();
        }
        private void DeleteAllButLine()
        {
            var childrens = canvas.Children;

            var removedChildren = childrens.Cast<object>().Where(child => !(child is Line)).ToList();

            foreach (var child in removedChildren)
            {
                canvas.Children.Remove(child as UIElement);
            }
        }
        private void Draw_Circle()
        {
            DeleteAllButLine();
            foreach (var point in _circle.Points)
            {
                canvas.Children.Add(point.Rect);
                Canvas.SetLeft(point.Rect, point.X * (_pixelSize) + 500 - _pixelSize / 2);
                Canvas.SetTop(point.Rect, point.Y * (_pixelSize) + 500 - _pixelSize / 2);
            }
        }
    }
}
