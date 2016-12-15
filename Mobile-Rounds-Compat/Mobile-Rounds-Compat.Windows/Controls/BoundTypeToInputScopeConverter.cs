using Mobile_Rounds.ViewModels.Shared.ReadingType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;

namespace Mobile_Rounds.Controls
{
    /// <summary>
    /// Provides a way to convert between bound type and keyboard type.
    /// </summary>
    public class BoundTypeToInputScopeConverter : IValueConverter
    {
        /// <inheritdoc/>
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            BoundType val = BoundType.EqualTo;
            if (value is BoundType)
            {
                val = (BoundType)value;
            }

            InputScope scope = new InputScope();
            InputScopeName scopeName = new InputScopeName(InputScopeNameValue.Default);

            if (val == BoundType.Between || val == BoundType.GreaterThan || val == BoundType.LessThan)
            {
                scopeName = new InputScopeName(InputScopeNameValue.NumberFullWidth);
            }

            scope.Names.Add(scopeName);

            return scope;
        }

        /// <inheritdoc/>
        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            // this should never be converted back.
            throw new NotSupportedException("Converting from keyboard type not supported.");
        }
    }
}
