using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace Mobile_Rounds.Controls
{
    public class BooleanToVisibilityConverter : IValueConverter
    {
        /// <inheritdoc/>
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            bool boolVal = false;
            if (value is bool)
            {
                boolVal = (bool)value;
            }

            return boolVal ? Visibility.Visible : Visibility.Collapsed;
        }

        /// <inheritdoc/>
        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            Visibility val = Visibility.Visible;
            if (value is Visibility)
            {
                val = (Visibility)value;
            }

            return val == Visibility.Visible ? true : false;
        }
    }
}
