
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
using System.ComponentModel;
using Telerik.Windows.Controls;
using Telerik.Windows.Data;
using System.Collections.Generic;
using Telerik.Windows;
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
    /// Custom user control act a as a filter cell in the filter row of a telerik grid
    /// </summary>
    public class ColumnFilterCell : UserControl, INotifyPropertyChanged
    {
        #region → Fields         .

        private bool isFilterActive;

        private GridViewColumn column;

        /// <summary>
        /// filter descriptor field
        /// </summary>
        public static readonly DependencyProperty FilterDescriptorProperty =
            DependencyProperty.Register("FilterDescriptor",
                typeof(Telerik.Windows.Data.FilterDescriptor), typeof(ColumnFilterCell),
                new System.Windows.PropertyMetadata(null, OnFilterDescriptorChanged)
            );
        #endregion

        #region → Properties     .

        /// <summary>
        /// Gets or sets the column.
        /// </summary>
        /// <value>The column.</value>
        public GridViewColumn Column
        {
            get
            {
                return this.column;
            }
            set
            {
                if (value != this.column)
                {
                    this.column = value;
                    this.OnPropertyChanged("Column");
                }
            }
        }

        /// <summary>
        /// Gets or sets the filter descriptor.
        /// </summary>
        /// <value>The filter descriptor.</value>
        public FilterDescriptor FilterDescriptor
        {
            get
            {
                return (FilterDescriptor)this.GetValue(FilterDescriptorProperty);
            }
            set
            {
                this.SetValue(FilterDescriptorProperty, value);
            }
        }

        /// <summary>
        /// Gets the filter operators.
        /// </summary>
        /// <value>The filter operators.</value>
        public IEnumerable<RadMenuItem> FilterOperators
        {
            get
            {
                List<RadMenuItem> filterOperatorMenuItems = new List<RadMenuItem>();

                filterOperatorMenuItems.Add(this.CreateMenu("No Filter"));

                foreach (FilterOperator filterOperator in ApplicableFilterOperators)
                {
                    filterOperatorMenuItems.Add(CreateMenu(filterOperator));
                }

                return filterOperatorMenuItems;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is filter active.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is filter active; otherwise, <c>false</c>.
        /// </value>
        public bool IsFilterActive
        {
            get
            {
                return this.isFilterActive;
            }
            set
            {
                //if (this.isFilterActive != value)
                {
                    this.isFilterActive = value;
                    OnPropertyChanged("IsFilterActive");
                    OnPropertyChanged("FilterVisibility");
                }
            }
        }

        /// <summary>
        /// Gets the applicable filter operators.
        /// </summary>
        /// <value>The applicable filter operators.</value>
        protected virtual IEnumerable<FilterOperator> ApplicableFilterOperators
        {
            get
            {
                return new FilterOperator[]
                {
                    FilterOperator.IsLessThan,
		            //FilterOperator.IsLessThanOrEqualTo,
		            FilterOperator.IsEqualTo,
		            FilterOperator.IsNotEqualTo,
		            //FilterOperator.IsGreaterThanOrEqualTo,
		            FilterOperator.IsGreaterThan
                };
            }
        }

        /// <summary>
        /// Gets the filter visibility.
        /// </summary>
        /// <value>The filter visibility.</value>
        public Visibility FilterVisibility
        {
            get
            {
                if (this.IsFilterActive)
                {
                    return Visibility.Visible;
                }
                else
                {
                    this.FilterDescriptor.Value = null;
                    return Visibility.Collapsed;
                }
            }
        }

        #endregion

        #region → Constructor    .

        /// <summary>
        /// Initializes a new instance of the <see cref="ColumnFilterCell"/> class.
        /// </summary>
        public ColumnFilterCell()
        {
            this.DataContext = this;
            this.Content = new Border();
        }
        #endregion

        #region → Events         .

        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region → Methods        .

        #region → Protected      .

        /// <summary>
        /// Called when [filter descriptor changed].
        /// </summary>
        /// <param name="oldValue">The old value.</param>
        /// <param name="newValue">The new value.</param>
        protected virtual void OnFilterDescriptorChanged(FilterDescriptor oldValue, Telerik.Windows.Data.FilterDescriptor newValue)
        {
        }

        /// <summary>
        /// Called when [property changed].
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        protected void OnPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion

        #region → Private        .

        /// <summary>
        /// Called when [filter descriptor changed].
        /// </summary>
        /// <param name="d">The d.</param>
        /// <param name="e">The <see cref="System.Windows.DependencyPropertyChangedEventArgs"/> instance containing the event data.</param>
        private static void OnFilterDescriptorChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((ColumnFilterCell)d).OnFilterDescriptorChanged((FilterDescriptor)e.OldValue, (FilterDescriptor)e.NewValue);
        }

        /// <summary>
        /// Creates the menu.
        /// </summary>
        /// <param name="filterOperator">The filter operator.</param>
        /// <returns></returns>
        private RadMenuItem CreateMenu(FilterOperator filterOperator)
        {
            RadMenuItem menu = CreateMenu(filterOperator.ToString());
            menu.Tag = filterOperator;

            return menu;
        }

        /// <summary>
        /// Creates the menu.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        private RadMenuItem CreateMenu(string name)
        {
            RadMenuItem menu = new RadMenuItem();
            menu.Header = name;
            menu.Click += OnMenuItemClick;

            return menu;
        }
        
        /// <summary>
        /// Called when [menu item click].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="Telerik.Windows.RadRoutedEventArgs"/> instance containing the event data.</param>
        private void OnMenuItemClick(object sender, RadRoutedEventArgs e)
        {
            RadMenuItem clickedMenuItem = (RadMenuItem)sender;
            if (clickedMenuItem.Tag != null)
            {
                this.FilterDescriptor.Operator = (FilterOperator)clickedMenuItem.Tag;
                this.IsFilterActive = true;
            }
            else
            {
                this.IsFilterActive = false;
            }
        }

        #endregion

        #endregion

    }
}
