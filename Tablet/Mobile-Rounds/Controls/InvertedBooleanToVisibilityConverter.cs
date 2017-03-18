using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace Mobile_Rounds.Controls
{
    /// <summary>
    /// Provides a way to convert between boolean values and Visibitlity.
    /// </summary>
    public class InvertedBooleanToVisibilityConverter : IValueConverter
    {
        /// <inheritdoc/>
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            bool boolVal = true;
            if (value is bool)
            {
                boolVal = !(bool)value;
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

            return val == Visibility.Visible ? false : true;
        }
    }
}
