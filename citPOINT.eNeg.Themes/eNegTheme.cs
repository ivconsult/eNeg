
#region → Usings   .
using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Telerik.Windows.Controls;

#endregion

#region → History  .

/* Date         User            Change
 * 
 * 22.04.12     Yousra Reda     creation
 */

# endregion

#region → ToDos    .

/*
 * Date         set by User     Description
 * 
 * 
*/

# endregion

namespace citPOINT.eNeg.Themes
{
    /// <summary>
    /// Custom theme for eNeg Project
    /// </summary>
    [ThemeLocation(ThemeLocation.BuiltIn)]
    public class eNegTheme : SummerTheme
    {
        #region → Constructors   .

        /// <summary>
        /// Initializes a new instance of the <see cref="eNegTheme"/> class.
        /// </summary>
        public eNegTheme()
        {
            this.Source = new Uri("/citPOINT.eNeg.Themes;component/Themes/Generic.xaml", UriKind.RelativeOrAbsolute);
        }
        #endregion

    }
}
