using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace InoGrid
{
    public class InoGridPanel : Panel
    {
        public int Columns { get; set; }

        // Default public constructor 
        public InoGridPanel() : base()
        {
        }

        protected override Size MeasureOverride(Size availableSize)
        {
            Size size = ArrangeAndMeasure(availableSize, true);
            if (double.IsInfinity(availableSize.Height) || double.IsInfinity(availableSize.Width))
                return size;
            else
                return availableSize;
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            return ArrangeAndMeasure(finalSize, false);
        }

        Size ArrangeAndMeasure(Size finalSize, bool isMeasure)
        {
            //if columns not specified set it value 2.
            Columns = Columns == 0 ? 2 : Columns;
            Size controlCurrentSize = new Size(0, 0);
            double maxColumnOneWidth = 0.0;
            int itemCount = 0;

            foreach (UIElement child in InternalChildren)
            {
                //for all even itemcounts move to first column
                if (itemCount % 2 == 0)
                {    
                    controlCurrentSize.Width -= maxColumnOneWidth;
                    if (maxColumnOneWidth < child.DesiredSize.Width)
                        maxColumnOneWidth = child.DesiredSize.Width;
                }
                //for all odd itemcounts move to second column
                if (itemCount % 2 != 0)
                {
                    controlCurrentSize.Width += maxColumnOneWidth;           
                }

                //Setting the location of the Element in the Control
                if (isMeasure)
                    child.Measure(finalSize);
                else
                    child.Arrange(new Rect(new Point(controlCurrentSize.Width, controlCurrentSize.Height), child.DesiredSize));

                //increasing height of control for all even itemcounts
                if (itemCount % 2 != 0)
                {
                    controlCurrentSize.Height += child.DesiredSize.Height;
                }

                itemCount++;
            }
            return controlCurrentSize;
        }
    }
}


//protected override Size MeasureOverride(Size availableSize)
//{
//    Size mySize = new Size(0, 0);
//    foreach (UIElement child in Children)
//    {
//        child.Measure(availableSize);
//        mySize.Width += child.DesiredSize.Width;
//        mySize.Height = Math.Max(mySize.Height, child.DesiredSize.Height);
//    }

//    return mySize;
//}

//protected override Size ArrangeOverride(Size finalSize)
//{
//    double offset = 0.0;
//    foreach (UIElement child in Children)
//    {
//        child.Arrange(new Rect(offset, 0, child.DesiredSize.Width, finalSize.Height));
//        offset += child.DesiredSize.Width;
//    }
//    return finalSize;
//}