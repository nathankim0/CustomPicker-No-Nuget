using System;
using Xamarin.Forms;

namespace CustomPickMe
{
    public class MyLayout : Layout<Xamarin.Forms.View>
    {
        protected override SizeRequest OnMeasure(double widthConstraint, double heightConstraint)
        {
            Size reqSize = new Size();
            Size minSize = new Size();

            foreach (Xamarin.Forms.View child in Children)
            {
                if (!child.IsVisible) continue;

                SizeRequest childSizeRequest = child.Measure(widthConstraint,Double.PositiveInfinity, MeasureFlags.IncludeMargins);

                reqSize.Width = Math.Max(reqSize.Width, childSizeRequest.Request.Width);
                reqSize.Height += childSizeRequest.Request.Height;
                minSize.Width = Math.Max(minSize.Width, childSizeRequest.Minimum.Width);
                minSize.Height += childSizeRequest.Minimum.Height;
            }
            return new SizeRequest(reqSize, minSize);
        }

        protected override void LayoutChildren(double x, double y, double width, double height)
        {
            foreach (Xamarin.Forms.View child in Children)
            {
                if (!child.IsVisible) continue; 
                SizeRequest childSizeRequest = child.Measure(width, Double.PositiveInfinity, MeasureFlags.IncludeMargins);
                
                double xChild = x;
                double yChild = y;
                double childWidth = childSizeRequest.Request.Width;
                double childHeight = childSizeRequest.Request.Height;
                
                

                switch (child.HorizontalOptions.Alignment)
                {
                    case LayoutAlignment.Start:
                        break;
                    case LayoutAlignment.Center:
                        xChild += (width - childWidth) / 2;
                        break;
                    case LayoutAlignment.End:
                        xChild += (width - childWidth);
                        break;
                    case LayoutAlignment.Fill:
                        childWidth = width;
                        break;    
                }
                child.Layout(new Rectangle(xChild, yChild, childWidth, childHeight));
                y += childHeight;
            }
        }
    }
}
