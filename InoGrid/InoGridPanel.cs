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
        #region Dependancy Property

        public int ChildMargin
        {
            get { return (int)GetValue(ChildMarginProperty); }
            set { SetValue(ChildMarginProperty, value); }
        }

        public static readonly DependencyProperty ChildMarginProperty =
            DependencyProperty.Register("ChildMargin", typeof(int), typeof(InoGridPanel), new PropertyMetadata(5));

        #endregion


        #region Properties

        public int Rows { get; set; }
        public double MaxRowHeight { get; set; }
        public double HorizontalMargin { get; set; }
        public double MaxColumnOneWidth { get; set; }
        public double MaxColumnTwoWidth { get; set; }
        public double AvailableColumnTwoWidth { get; set; }
        public double VerticalMargin { get; set; }
        public double PanelWidth { get; set; }
        public double PanelHeight { get; set; }

        #endregion


        #region Constructor

        public InoGridPanel() : base()
        {

        }

        #endregion


        #region Measure and Arrange Method

        protected override Size MeasureOverride(Size availableSize)
        {

            Size panelSize = new Size(0, 0);
            Rows = 0;
            MaxRowHeight = 0;
            HorizontalMargin = ChildMargin * 2;
            MaxColumnOneWidth = 0;
            MaxColumnTwoWidth = 0;
            VerticalMargin = ChildMargin * 2;
            PanelWidth = 0;
            PanelHeight = 0;
            int itemCount = 0;

            foreach (UIElement child in Children)
            {
                //for all even (including 0) itemcounts move to first column
                if (itemCount % 2 == 0)
                {
                    child.Measure(availableSize);
                    MaxColumnOneWidth = Math.Max(MaxColumnOneWidth, (child.DesiredSize.Width+ HorizontalMargin));
                    MaxRowHeight = Math.Max(MaxRowHeight, (child.DesiredSize.Height + VerticalMargin));
                    Rows++;
                }
                //for all odd itemcounts move to second column
                if (itemCount % 2 != 0)
                {
                    child.Measure(availableSize);
                    MaxColumnTwoWidth = Math.Max(MaxColumnTwoWidth, (child.DesiredSize.Width+ HorizontalMargin));
                    MaxRowHeight = Math.Max(MaxRowHeight, (child.DesiredSize.Height+ VerticalMargin));
                }
                itemCount++;
            }
            AvailableColumnTwoWidth = availableSize.Width - MaxColumnOneWidth;
            PanelHeight = MaxRowHeight * Rows;
            PanelWidth = MaxColumnOneWidth + MaxColumnTwoWidth;
            panelSize = new Size(PanelWidth, PanelHeight);

            return panelSize;
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            double offsetHeight = ChildMargin;
            double itemLocalOffsetHeight = 0;
            int itemCount = 0;

            foreach (UIElement child in Children)
            {
                itemLocalOffsetHeight = (MaxRowHeight - child.DesiredSize.Height)/2;
                //for all even (including 0) itemcounts move to first column
                if (itemCount % 2 == 0)
                {
                    double itemWidth = MaxColumnOneWidth - HorizontalMargin;
                    //double itemHeight = MaxRowHeight - VerticalMargin;
                    double itemHeight = child.DesiredSize.Height;
                    double itemOffsetWidth = ChildMargin;
                    double itemOffsetHeight = offsetHeight - ChildMargin + itemLocalOffsetHeight;

                    if (itemWidth < 0)
                    {
                        itemWidth = 0;
                    }
                    if (itemHeight < 0)
                    {
                        itemHeight = 0;
                    }
                    child.Arrange(new Rect(itemOffsetWidth, itemOffsetHeight, itemWidth, itemHeight));
                }
                //for all odd itemcounts move to second column
                if (itemCount % 2 != 0)
                {
                    double itemWidth = AvailableColumnTwoWidth - HorizontalMargin;
                    //double itemHeight = MaxRowHeight - VerticalMargin;
                    double itemHeight = child.DesiredSize.Height;
                    double itemOffsetWidth = MaxColumnOneWidth;
                    double itemOffsetHeight = offsetHeight - ChildMargin + itemLocalOffsetHeight;

                    if (itemWidth<0)
                    {
                        itemWidth = 0;
                    }
                    if (itemHeight < 0)
                    {
                        itemHeight = 0;
                    }
                    child.Arrange(new Rect(itemOffsetWidth, itemOffsetHeight, itemWidth, itemHeight));
                    offsetHeight = offsetHeight + MaxRowHeight;
                }
                itemCount++;
            }
            return finalSize;
        }

        #endregion
    }
}
