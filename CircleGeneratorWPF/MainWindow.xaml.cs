﻿using System;
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
        private Circle _test;
        private Line _line;

        public MainWindow()
        {
            _test = new Circle(40, 10, 10);
            _line = new Line();
            InformationLayer = new Canvas();
            
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
            InformationLayer.Children.Add(_line);
            _pixelSize = 1;
        }
        
        private void Slider_Size(object sender, RoutedPropertyChangedEventArgs<double> slider) 
        {
            _pixelSize = (int)slider.NewValue;
            _test.ChangeSize(_pixelSize);
            InformationLayer.Children.Clear();
            //InformationLayer.Children.Add(line);
            foreach (var point in _test.Points)
            {
                InformationLayer.Children.Add(point.Rect);
                Canvas.SetLeft(point.Rect, point.X * (_pixelSize) + 500 - _pixelSize / 2);
                Canvas.SetTop(point.Rect, point.Y * (_pixelSize) + 500 - _pixelSize / 2);
            }
        }

        private void Slider_Radius(object sender, RoutedPropertyChangedEventArgs<double> slider)
        {
            
        }
    }
}
