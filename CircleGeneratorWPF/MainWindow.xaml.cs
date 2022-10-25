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
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public int PixelSize;
        Circle test;
        Line line;
        private void Window_ContentRendered(object sender, EventArgs e)
        {
            DrawInformationLayer();   
        }
        private void DrawInformationLayer()
        {
            line = new Line();
            line.StrokeThickness = 4;
            line.Stroke = Brushes.Black;
            line.X1 = 1000;
            line.X2 = 1000;
            line.Y1 = 0;
            line.Y2 = 1000;
            InformationLayer.Children.Add(line);
            PixelSize = 1;
            test = new Circle(40, 10, 10);
        }
        private void Slider_Size(object sender, RoutedPropertyChangedEventArgs<double> slider) 
        {
            PixelSize = (int)slider.NewValue;
            test.ChangeSize(PixelSize);
            InformationLayer.Children.Clear();
            InformationLayer.Children.Add(line);
            foreach (var point in test.Points)
            {
                InformationLayer.Children.Add(point.Rect);
                Canvas.SetLeft(point.Rect, point.X * (PixelSize) + 500 - PixelSize / 2);
                Canvas.SetTop(point.Rect, point.Y * (PixelSize) + 500 - PixelSize / 2);
            }
        }
    }
}
