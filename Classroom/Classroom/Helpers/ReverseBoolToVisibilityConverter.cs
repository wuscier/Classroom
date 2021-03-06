﻿using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Classroom.Helpers
{
    public class ReverseBoolToVisibilityConverter2 : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool bValue = (bool)value;
            return bValue ? Visibility.Collapsed : Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
