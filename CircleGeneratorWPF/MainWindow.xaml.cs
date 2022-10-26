using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace CircleGeneratorWPF
{
    public partial class MainWindow : Window
    {
        private readonly Circle _circle;
        private readonly double _aspect = 1.3;
        private Size SizeCanvas;
        
        public MainWindow()
        {
            InitializeComponent();

            canvas = new Canvas();
            _circle = new Circle(40, 10, 10);
        }

        private (int height, int width) GetCanvasSize()
        {
            var row = DgCircle.RowDefinitions.First(r => r.Name == "CanvasRow");
            var column = DgCircle.ColumnDefinitions.First(c => c.Name == "CanvasColumn");

            return ((int)row.ActualHeight, (int)column.ActualWidth);
        }
        private void DrawCircle(int pixelSize)
        {
            canvas.Children.Clear();
            
            foreach (var point in _circle.Points)
            {
                canvas.Children.Add(point.Rect);
                Canvas.SetLeft(point.Rect, point.X * pixelSize);
                Canvas.SetTop(point.Rect, point.Y * pixelSize);
            }
        }
        
        protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo)
        {
            if (sizeInfo.WidthChanged) Width = sizeInfo.NewSize.Height * _aspect;
            else Height = sizeInfo.NewSize.Width / _aspect;
            ChangePixelSize();
        }
        
        private void RadiusPreview(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsValid(((TextBox)sender).Text + e.Text);
        }
        
        private void RadiusChanged(object sender, TextChangedEventArgs e)
        {
            var textBox = (TextBox)sender;
            var radius = string.IsNullOrEmpty(textBox.Text) ? 1 : int.Parse(textBox.Text);
            
            _circle.changeRadius(radius);
            ChangePixelSize();
        }
        
        private void OnChangeCanvas(object sender, SizeChangedEventArgs e)
        {
            SizeCanvas = e.NewSize; // not work, metod dont called
            MessageBox.Show(SizeCanvas.ToString());
        }
        
        private void ChangePixelSize()
        {
            var size = GetCanvasSize();

            var pixelSize = size.height / (_circle.Radius * 2);

            DrawCircle(pixelSize);
        }
        
        private static bool IsValid(string str)
        {
            return int.TryParse(str, out var i) && i >= 1 && i <= 300;
        }
    }
}