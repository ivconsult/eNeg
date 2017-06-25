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
    /// Filter cell in case of string column binding 
    /// </summary>
    public partial class TextColumnHeader
    {
        #region → Constractor    .

        /// <summary>
        /// Initializes a new instance of the <see cref="TextColumnHeader"/> class.
        /// </summary>
        public TextColumnHeader()
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

            if (newValue != null)
            {
                newValue.Value = string.Empty;
            }
        }

        /// <summary>
        /// Gets the applicable filter operators.
        /// </summary>
        /// <value>The applicable filter operators.</value>
        protected override IEnumerable<FilterOperator> ApplicableFilterOperators
        {
            get
            {
                return new FilterOperator[]
                {
                    FilterOperator.StartsWith,
                    //FilterOperator.EndsWith,
                    FilterOperator.Contains
                    //FilterOperator.DoesNotContain,
                    //FilterOperator.IsContainedIn
                };
            }
        }

        #endregion

        #endregion
    }
}
