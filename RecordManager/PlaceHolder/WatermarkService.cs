using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;

namespace TaskManagerControls
{
    public static class WatermarkService
    {
        public static readonly DependencyProperty WatermarkProperty = DependencyProperty.RegisterAttached(
            "Watermark",
            typeof(object),
            typeof(WatermarkService),
            new FrameworkPropertyMetadata(null, OnWatermarkChanged));

        public static object GetWatermark(DependencyObject d)
        {
            return d.GetValue(WatermarkProperty);
        }

        public static void SetWatermark(DependencyObject d, object value)
        {
            d.SetValue(WatermarkProperty, value);
        }

        private static void OnWatermarkChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Control control = (Control)d;
            control.Loaded += Loaded;

            if(d is TextBox)
            {
                control.GotKeyboardFocus += GotKeyboardFocus;
                control.LostKeyboardFocus += Loaded;
                ((TextBox)control).TextChanged += GotKeyboardFocus;
            }
        }

        private static void GotKeyboardFocus(object sender, RoutedEventArgs e)
        {
            Control control = (Control)sender;
            if(ShouldShowWatermark(control))
                ShowWatermark(control);
            else
                RemoveWatermark(control);
        }

        private static void Loaded(object sender, RoutedEventArgs e)
        {
            Control control = (Control)sender;
            if(ShouldShowWatermark(control))
                ShowWatermark(control);
        }

        private static void RemoveWatermark(UIElement control)
        {
            AdornerLayer layer = AdornerLayer.GetAdornerLayer(control);

            if(layer == null)
                return;

            Adorner[] adorners = layer.GetAdorners(control);
            if(adorners == null)
                return;


            foreach(Adorner adorner in adorners)
            {
                if(adorner is WatermarkAdorner)
                {
                    adorner.Visibility = Visibility.Hidden;
                    layer.Remove(adorner);
                }
            }
        }

        private static void ShowWatermark(Control control)
        {
            AdornerLayer layer = AdornerLayer.GetAdornerLayer(control);

            if(layer != null)
                layer.Add(new WatermarkAdorner(control, GetWatermark(control)));
        }

        private static bool ShouldShowWatermark(Control control)
        {
            if(!(control is TextBoxBase textBoxBase))
                return false;

            string text = (textBoxBase as TextBox)?.Text;
            return String.IsNullOrWhiteSpace(text);
        }
    }
}