using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
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
            
            _circle = new Circle();
        }

        private (int height, int width) GetCanvasSize()
        {
            var row = GCircle.RowDefinitions.First(r => r.Name == "CanvasRow");
            var column = GCircle.ColumnDefinitions.First(c => c.Name == "CanvasColumn");

            return ((int)row.ActualHeight, (int)column.ActualWidth);
        }

        private void DrawCircle()
        {
            CircleCanvas.Children.Clear();
            
            foreach (var point in _circle.Points)
            {
                CircleCanvas.Children.Add(point.Rect);
                Canvas.SetLeft(point.Rect, point.X * _circle.PixelSize - _circle.PixelSize/2);
                Canvas.SetTop(point.Rect, point.Y * _circle.PixelSize - _circle.PixelSize/2);
            }
        }

        private void ChangePixelSize()
        {
            _circle.ChangeSize(GetCanvasSize().height / ((_circle.Radius * 2) + 1));
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
            e.Handled = !IsValid(((TextBox)sender).Text + e.Text, 0, 300);
        }
        
        private void RadiusChanged(object sender, TextChangedEventArgs e)
        {
            var textBox = (TextBox)sender;
            var radius = string.IsNullOrEmpty(textBox.Text) ? 1 : int.Parse(textBox.Text);
            
            _circle.ChangeRadius(radius);
            ChangePixelSize();
            
            DrawCircle();
        }

        private static bool IsValid(string str, int minNum, int maxNum)
        {
            return int.TryParse(str, out var i) && i >= minNum && i <= maxNum;
        }

        private void SaveToClipboardButtonPressed(object sender, RoutedEventArgs e)
        {
            var outputString = "uint16_t circlePointX[] = { " + string.Join(" ", CreateOutputX()) + " };\n" +
                            "uint16_t circlePointY[] = { " + string.Join(" ", CreateOutputY()) + " };\n";

            CopyToClipboard(outputString);
        }

        private async void CopyToClipboard(string clipboardText)
        {
            Clipboard.SetText(clipboardText);

            SaveButton.Content = "Copied!";
            SaveButton.IsEnabled = false;
            
            await Task.Run(() => Thread.Sleep(3000));
            
            SaveButton.Content = "Copy to clipboard";
            SaveButton.IsEnabled = true;
        }

        private IEnumerable<string> CreateOutputX()
        {
            return _circle.Points.Select(point => (point.X + _circle.OffsetX).ToString());
        }
        
        private IEnumerable<string> CreateOutputY()
        {
            return _circle.Points.Select(point => (point.Y + _circle.OffsetY).ToString());
        }

        private void BeforeSettingOffsets(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsValid(((TextBox)sender).Text + e.Text, 0, 999);
        }

        private void OffsetXTextChanged(object sender, TextChangedEventArgs e)
        {
            _circle.OffsetX = int.TryParse(((TextBox)sender).Text, out var num) ? num : 0;
        }

        private void OffsetYTextChanged(object sender, TextChangedEventArgs e)
        {
            _circle.OffsetY = int.TryParse(((TextBox)sender).Text, out var num) ? num : 0;
        }
    }
}