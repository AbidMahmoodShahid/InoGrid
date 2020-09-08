using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace InoGrid
{
    public class InoGridItem : ContentControl
    {
        public object Label
        {
            get { return base.GetValue(LabelProperty); }
            set { base.SetValue(LabelProperty, value); }
        }

        static InoGridItem()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(InoGridItem), new FrameworkPropertyMetadata(typeof(InoGridItem)));
        }

        public static readonly DependencyProperty LabelProperty = DependencyProperty.Register(nameof(Label), typeof(object), typeof(InoGridItem), new FrameworkPropertyMetadata());

    }
}
