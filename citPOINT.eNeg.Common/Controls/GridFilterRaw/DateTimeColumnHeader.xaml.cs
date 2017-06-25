
#region → Usings   .
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Telerik.Windows.Data;
#endregion

#region → History  .

/* 
 * Date         User            Change
 * *********************************************
 * 24.03.12   Yousra Reda         • creation
 * **********************************************
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
    /// Filter cell in case of date time column binding 
    /// </summary>
    public partial class DateTimeColumnHeader
    {
        #region → Constractor    .

        /// <summary>
        /// Initializes a new instance of the <see cref="DateTimeColumnHeader"/> class.
        /// </summary>
        public DateTimeColumnHeader()
        {
            InitializeComponent();
        }
        #endregion

        #region → Methods        .

        #region → Protected      .

        /// <summary>
        /// Called when [filter descriptor changed].
        /// </summary>
        /// <param name="oldValue">The old value.</param>
        /// <param name="newValue">The new value.</param>
        protected override void OnFilterDescriptorChanged(FilterDescriptor oldValue, FilterDescriptor newValue)
        {
            base.OnFilterDescriptorChanged(oldValue, newValue);

            //if (newValue != null)
            //{
            //    newValue.Value = new DateTime();
            //}
        }

        #endregion


        #endregion
    }
}
