
#region → Usings   .
using System;
using System.Windows;
using System.Windows.Data;
#endregion

#region → History  .

/* Date         User            change
 * 
 * 27.09.11     Yousra Reda     creation
 */

# endregion

#region → ToDos    .

/*
 * Date         set by User     Description
 * 
 * 
*/

# endregion

namespace citPOINT.eNeg.Common
{
    /// <summary>
    /// Represents the converter that applies the not operator to Boolean values and then convert to and from Visibility enumeration values. 
    /// </summary>
    public sealed class BooleanToVisibilityConverter : IValueConverter
    {
        #region → Methods        .

        #region Puplic

        #region Implement Interface IValueConverter
        /// <summary>
        /// Convert from bool to visibilty type
        /// </summary>
        /// <param name="value">bool value to convert</param>
        /// <param name="targetType">Value of target type</param>
        /// <param name="parameter">Parameters if found</param>
        /// <param name="culture">value of used Culture</param>
        /// <returns>Either visible or collapsed</returns>
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            bool flag = false;
            if (value is bool)
            {
                flag = (bool)value;
            }
            else if (value is bool?)
            {
                bool? nullable = (bool?)value;
                flag = nullable.HasValue ? nullable.Value : false;
            }
            return (flag ? Visibility.Visible : Visibility.Collapsed);
        }

        /// <summary>
        /// Convert from visibilty type to bool
        /// </summary>
        /// <param name="value">visibilty to convert</param>
        /// <param name="targetType">Value of target type</param>
        /// <param name="parameter">Parameters if found</param>
        /// <param name="culture">value of used Culture</param>
        /// <returns>bool</returns>
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return ((value is Visibility) && (((Visibility)value) == Visibility.Visible));
        }
        #endregion

        #endregion

        #endregion

    }

}
