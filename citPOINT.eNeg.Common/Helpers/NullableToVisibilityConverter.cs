
#region → Usings   .
using System;
using System.Windows;
using System.Windows.Data;
#endregion

#region → History  .

/* Date         User            change
 * 
 * 03.08.10     Yousra Reda     creation
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
    /// Represents the converter that converts Null to Collapsed and NotNull To Visibility enumeration values. 
    /// </summary>
    public sealed class NullableToVisibilityConverter : IValueConverter
    {
        #region → Methods        .

        #region → Puplic         .

        #region Implement Interface IValueConverter

        /// <summary>
        /// Convert from null to visibilty type
        /// </summary>
        /// <param name="value">null value to convert</param>
        /// <param name="targetType">Value of target type</param>
        /// <param name="parameter">Parameters if found</param>
        /// <param name="culture">value of used Culture</param>
        /// <returns>Either visible or collapsed</returns>
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            bool flag = false;

            if (value != null)
            {
                if (value.GetType().Equals(typeof(string)) && string.IsNullOrEmpty((string)value))
                {
                    flag = false;
                }
                else
                {
                    flag = true;
                }
            }

            return (flag ? Visibility.Visible : Visibility.Collapsed);
        }

        /// <summary>
        /// Convert from visibilty type to null
        /// </summary>
        /// <param name="value">visibilty to convert</param>
        /// <param name="targetType">Value of target type</param>
        /// <param name="parameter">Parameters if found</param>
        /// <param name="culture">value of used Culture</param>
        /// <returns>null or new object</returns>
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return ((value is Visibility) && (((Visibility)value) == Visibility.Visible)) ? null : new object();
        }
        #endregion

        #endregion

        #endregion

    }

}
