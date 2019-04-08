using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

namespace TaskManagerControls
{
    internal class RightContentAdorner : Adorner
    {
        private readonly ContentPresenter contentPresenter;
        private Control Control => (Control)this.AdornedElement;

        public RightContentAdorner(UIElement adornedElement)
            : base(adornedElement)
        {
            VisualCollection visualCollection = new VisualCollection(this);
            contentPresenter = new ContentPresenter();
            visualCollection.Add(contentPresenter);
        }

        public RightContentAdorner(UIElement adornedElement, object rightContent)
            : this(adornedElement)
        {
            Content = rightContent;

            this.contentPresenter.Opacity = 0.5;
            this.contentPresenter.HorizontalAlignment = HorizontalAlignment.Right;
            this.contentPresenter.Margin = new Thickness(Control.Margin.Left + Control.Padding.Left,
                Control.Margin.Top + Control.Padding.Top, 0, 0);
        }

        protected override int VisualChildrenCount => 1;

        protected override Visual GetVisualChild(int index)
        {
            return this.contentPresenter;
        }

        protected override Size MeasureOverride(Size constraint)
        {
            this.contentPresenter.Measure(Control.RenderSize);
            return Control.RenderSize;
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            this.contentPresenter.Arrange(new Rect(finalSize));
            return finalSize;
        }

        public object Content
        {
            get { return contentPresenter.Content; }
            set { contentPresenter.Content = value; }
        }
    }
}