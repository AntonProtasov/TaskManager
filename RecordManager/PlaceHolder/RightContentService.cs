using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace TaskManagerControls
{
    public static class RightContentService
    {
        public static readonly DependencyProperty RightContentProperty = DependencyProperty.RegisterAttached(
            "RightContent",
            typeof(object),
            typeof(RightContentService),
            new FrameworkPropertyMetadata(null, OnRightContentChanged));

        public static object GetRightContent(DependencyObject d)
        {
            return d.GetValue(RightContentProperty);
        }

        public static void SetRightContent(DependencyObject d, object value)
        {
            d.SetValue(RightContentProperty, value);
        }

        private static void OnRightContentChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Control control = (Control)d;
            control.Loaded += Loaded;
        }

        private static void Loaded(object sender, RoutedEventArgs e)
        {
            Control control = (Control)sender;
            ShowRightContent(control);
        }

        private static void ShowRightContent(Control control)
        {
            AdornerLayer layer = AdornerLayer.GetAdornerLayer(control);

            if(layer != null)
                layer.Add(new RightContentAdorner(control, GetRightContent(control)));
        }
    }
}