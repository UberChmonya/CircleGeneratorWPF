using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace CircleGeneratorWPF
{
    public partial class MainWindow : Window
    {
        private int _pixelSize;
        private readonly Circle _circle;
        private readonly Line _line;

        public MainWindow()
        {
            canvas = new Canvas();
            _circle = new Circle(40, 10, 10);
            _line = new Line();
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

            DrawCircle();
        }
        private void Slider_Radius(object sender, RoutedPropertyChangedEventArgs<double> slider)
        {
            var radius = (int)slider.NewValue;
            _circle.changeRadius(radius);

            DrawCircle();
        }
        private void DeleteAllButLine()
        {
            var removedItems = new List<UIElement>();

            foreach (var child in canvas.Children.OfType<Rectangle>())
            {
                removedItems.Add(child);
            }

            foreach (var item in removedItems)
            {
                canvas.Children.Remove(item);
            }
        }
        private void DrawCircle()
        {
            DeleteAllButLine();

            foreach (var point in _circle.Points)
            {
                canvas.Children.Add(point.Rect);
                Canvas.SetLeft(point.Rect, point.X * _pixelSize - _pixelSize / 2);
                Canvas.SetTop(point.Rect, point.Y * _pixelSize - _pixelSize / 2);
            }
        }
    }
}