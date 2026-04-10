using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Spanzuratoarea_SuhariucIulia.Helpers
{
    public class MistakeToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int mistakes = (int)value;
            int step = int.Parse(parameter.ToString()!);

            return mistakes >= step ? Visibility.Visible : Visibility.Hidden;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null!;
        }
    }
}