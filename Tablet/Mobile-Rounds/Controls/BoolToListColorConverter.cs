using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;

namespace Mobile_Rounds.Controls
{
    /// <summary>
    /// Provides a way to convert between boolean values and Visibitlity.
    /// </summary>
    public class BoolToListColorConverter : IValueConverter
    {
        /// <inheritdoc/>
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            bool boolVal = false;
            if (value is bool)
            {
                boolVal = (bool)value;
            }

            return boolVal ? new SolidColorBrush(Colors.Gold) : new SolidColorBrush(Colors.Transparent);
        }

        /// <inheritdoc/>
        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            SolidColorBrush val = new SolidColorBrush(Colors.Black);
            if (value is SolidColorBrush)
            {
                val = (SolidColorBrush)value;
            }

            return val.Color == Colors.LightYellow ? false : true;
        }
    }
}
