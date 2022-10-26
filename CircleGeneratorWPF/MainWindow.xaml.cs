using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace CircleGeneratorWPF
{
    public partial class MainWindow : Window
    {
        private readonly Circle _circle;
        private const double Aspect = 1.3;

        public MainWindow()
        {
            InitializeComponent();
            
            _circle = new Circle(10, 10);
        }

        private (int height, int width) GetCanvasSize()
        {
            var row = GCircle.RowDefinitions.First(r => r.Name == "CanvasRow");
            var column = GCircle.ColumnDefinitions.First(c => c.Name == "CanvasColumn");

            return ((int)row.ActualHeight, (int)column.ActualWidth);
        }
<<<<<<< HEAD
        private void DrawCircle(int pixelSize)
=======
    
        private void DrawCircle()
>>>>>>> e2b9522 (Code refactoring.)
        {
            CircleCanvas.Children.Clear();
            
            foreach (var point in _circle.Points)
            {
                CircleCanvas.Children.Add(point.Rect);
                Canvas.SetLeft(point.Rect, point.X * _circle.PixelSize);
                Canvas.SetTop(point.Rect, point.Y * _circle.PixelSize);
            }
        }
        
        protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo)
        {
            if (sizeInfo.WidthChanged)
            {
                Width = sizeInfo.NewSize.Height * Aspect;
            }
            else
            {
                Height = sizeInfo.NewSize.Width / Aspect;
            }
            
            ChangePixelSize();
            DrawCircle();
        }
        
        private void RadiusPreview(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsValid(((TextBox)sender).Text + e.Text);
        }
        
        private void RadiusChanged(object sender, TextChangedEventArgs e)
        {
            var textBox = (TextBox)sender;
            var radius = string.IsNullOrEmpty(textBox.Text) ? 1 : int.Parse(textBox.Text);
            
            _circle.ChangeRadius(radius);
            ChangePixelSize();
            
            DrawCircle();
        }

        private void ChangePixelSize()
        {
            _circle.ChangeSize(GetCanvasSize().height / (_circle.Radius * 2));
        }
        
        private static bool IsValid(string str)
        {
            return int.TryParse(str, out var i) && i >= 1 && i <= 300;
        }
    }
}