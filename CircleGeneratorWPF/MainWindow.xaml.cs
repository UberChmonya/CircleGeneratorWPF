using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace CircleGeneratorWPF
{
    public partial class MainWindow : Window
    {

        private readonly Circle _circle;
        double aspect = 1.3;
        public MainWindow()
        {

            canvas = new Canvas();
            _circle = new Circle(40, 10, 10);
            InitializeComponent();
        }
    
        private void DrawCircle(int pixelSize)
        {
            canvas.Children.Clear();
            foreach (var point in _circle.Points)
            {
                canvas.Children.Add(point.Rect);
                Canvas.SetLeft(point.Rect, point.X * pixelSize - pixelSize / 2);
                Canvas.SetTop(point.Rect, point.Y * pixelSize - pixelSize / 2);
            }
        }
        protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo)
        {
            if (sizeInfo.WidthChanged) this.Width = sizeInfo.NewSize.Height * aspect;
            else this.Height = sizeInfo.NewSize.Width / aspect;
            
        }
        private void RadiusPreview(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsValid(((TextBox)sender).Text + e.Text);
        }
        private void RadiusChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            int radius = string.IsNullOrEmpty(textBox.Text) ? 1 : int.Parse(textBox.Text);
            _circle.changeRadius(radius);
            DrawCircle(1);
        }
        private static bool IsValid(string str)
        {
            int i;
            return int.TryParse(str, out i) && i >= 1 && i <= 300;
        }

    }
}