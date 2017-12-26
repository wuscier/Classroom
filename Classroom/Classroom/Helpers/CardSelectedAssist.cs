using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Classroom.Helpers
{
    public static class CardSelectedAssist
    {
        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsCardSelectedProperty = DependencyProperty.RegisterAttached(
            "IsCardSelected",
            typeof(bool),
            typeof(CardSelectedAssist),
            new PropertyMetadata(false));

        public static void SetIsCardSelected(DependencyObject element, bool value)
        {
            element.SetValue(IsCardSelectedProperty, value);
        }

        public static bool GetIsCardSelected(DependencyObject element)
        {
            return (bool)element.GetValue(IsCardSelectedProperty);
        }


    }
}
