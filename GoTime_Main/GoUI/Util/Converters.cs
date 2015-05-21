using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Data;
using System.Windows.Controls;
using GoUI.Controls;

namespace GoUI.Util
{
    #region GridBoardRowVisibilityConverter

    /// <summary>
    /// Converter that converts a GridBoardType value to a Visibility value
    /// </summary>
    public class GridBoardTypeVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (targetType == typeof(Visibility) &&
                parameter is GridBoardControl.GridBoardType &&
                value is GridBoardControl.GridBoardType)
            {
                // Return visible if the parameter BoardType matches or "comes before" the type specified by the value.
                // ie: a 13x13 GridType "comes before" a 19x19 type
                if ((GridBoardControl.GridBoardType)parameter <= (GridBoardControl.GridBoardType)value)
                {
                    return Visibility.Visible;
                }
            }

            return Visibility.Collapsed;
        }
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }

    #endregion End of GridBoardRowVisibilityConverter
}
