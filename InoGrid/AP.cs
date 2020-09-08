using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace InoGrid
{
    public static class AP
    {
        public static readonly DependencyProperty ColumnsProperty =
    DependencyProperty.RegisterAttached("Columns", typeof(int), typeof(AP), new PropertyMetadata(2));

        public static int GetColumns(UIElement element)
        {
            return (int)element.GetValue(ColumnsProperty);
        }

        public static void SetColumns(UIElement element, int value)
        {
            element.SetValue(ColumnsProperty, value);
        }






    }
}
