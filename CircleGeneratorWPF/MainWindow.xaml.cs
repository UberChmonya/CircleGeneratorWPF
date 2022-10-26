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

            var size = GetCanvasSize();

            MessageBox.Show($"{size.Item1} {size.Item2}");
        }

        private (int, int) GetCanvasSize()
        {
            var row = DgCircle.RowDefinitions.First(r => r.Name == "CanvasRow");
            var column = DgCircle.ColumnDefinitions.First(c => c.Name == "CanvasColumn");

            return ((int)row.Height.Value, (int)column.Width.Value);
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
            //TODO
            //adaptive size
            DrawCircle(1);
        }
        
        private static bool IsValid(string str)
        {
            return int.TryParse(str, out var i) && i >= 1 && i <= 300;
        }
    }
}