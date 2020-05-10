using Microsoft.Extensions.Logging;
using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace LogReader.Utils
{
    public class LevelColorSelector : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is LogLevel)
            {
                return value switch
                {
                    LogLevel.Information => new SolidColorBrush(Colors.Blue),
                    LogLevel.Error => new SolidColorBrush(Colors.Red),
                    LogLevel.Warning => new SolidColorBrush(Colors.Orange),
                    _ => Binding.DoNothing
                };
            }

            return Binding.DoNothing;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
