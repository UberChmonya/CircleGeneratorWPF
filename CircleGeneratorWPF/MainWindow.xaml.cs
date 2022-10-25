using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
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
        double aspect = 1.3;
        public MainWindow()
        {

            canvas = new Canvas();
            _circle = new Circle(40, 10, 10);
            _pixelSize = 1;
            InitializeComponent();
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

        
        private void DrawCircle()
        {
            canvas.Children.Clear();
            foreach (var point in _circle.Points)
            {
                canvas.Children.Add(point.Rect);
                Canvas.SetLeft(point.Rect, point.X * _pixelSize - _pixelSize / 2);
                Canvas.SetTop(point.Rect, point.Y * _pixelSize - _pixelSize / 2);
            }
        }
        protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo)
        {
            if (sizeInfo.WidthChanged) this.Width = sizeInfo.NewSize.Height * aspect;
            else this.Height = sizeInfo.NewSize.Width / aspect;

        }
    }

}