
#region → Usings   .

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using citPOINT.eNeg.Apps.Common.Interfaces;
using citPOINT.eNeg.Common.eNegApps;
using Telerik.Windows.Controls;

#endregion

#region → History  .

/* 
 * Date         User            Change
 * *********************************************
 * 13.03.2012   Mwahab         • creation
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

namespace citPOINT.eNeg.Common.Controls
{
    /// <summary>
    /// eNeg Tab Control.
    /// </summary>
    public class eNegTabControl : RadTabControl
    {
        #region → Fields         .

        private IeNegApplication DefaultAppView { get; set; }

        /// <summary>
        ///Using a DependencyProperty as the backing store for ApplicationSource.  
        ///This enables animation, styling, binding, etc... 
        /// </summary>
        public static readonly DependencyProperty ApplicationSourceProperty =
            DependencyProperty.Register("ApplicationSource", typeof(IEnumerable), typeof(eNegTabControl), new PropertyMetadata(new PropertyChangedCallback(OnChange)));


        #endregion

        #region → Properties     .

        /// <summary>
        /// Gets or sets the application source.
        /// </summary>
        /// <value>The application source.</value>
        public IEnumerable ApplicationSource
        {
            get { return (IEnumerable)GetValue(ApplicationSourceProperty); }
            set { SetValue(ApplicationSourceProperty, value); }
        }

        #endregion Properties

        #region → Constractor    .

        /// <summary>
        /// Initializes a new instance of the <see cref="eNegTabControl"/> class.
        /// </summary>
        public eNegTabControl()
            : base()
        {
            if (!DesignerProperties.IsInDesignTool)
            {

                this.DefaultAppView = new eNegApplication()
                {
                    ApplicationTitle = "",
                    RegionName = "DefaulAppRegion",
                    IsActive = true
                };

                this.onActivationChanged += new EventHandler<EventArgs>(eNegTabControl_onActivationChanged);

                this.Items.Add(new eNegTabItem(this.DefaultAppView, this.onActivationChanged));
            }
        }

        #endregion

        #region → Event Handlers .

        /// <summary>
        /// Called when [changes] of application Source.
        /// </summary>
        /// <param name="obj">The obj.</param>
        /// <param name="e">The <see cref="System.Windows.DependencyPropertyChangedEventArgs"/> instance containing the event data.</param>
        private static void OnChange(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            eNegTabControl tabControl = (obj as eNegTabControl);

            if (e.NewValue != null)
            {

                foreach (var IeNegApplicationItem in e.NewValue as IEnumerable<IeNegApplication>)
                {
                    eNegTabItem eNegTabItem = new eNegTabItem(IeNegApplicationItem, tabControl.onActivationChanged)
                    {
                        HorizontalAlignment = HorizontalAlignment.Stretch,
                        HorizontalContentAlignment = HorizontalAlignment.Stretch,

                        VerticalAlignment = VerticalAlignment.Stretch,
                        VerticalContentAlignment = VerticalAlignment.Stretch
                    };
                    
                    tabControl.Items.Add(eNegTabItem);
                }

                tabControl.onActivationChanged(null, null);
            }
        }

        /// <summary>
        /// Handles the onActivationChanged event of the eNegTabControl control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void eNegTabControl_onActivationChanged(object sender, EventArgs e)
        {
            this.DefaultAppView.IsActive = this.ApplicationSource == null || (this.ApplicationSource as IEnumerable<IeNegApplication>).Where(s => s.IsActive == true).Count() <= 0;

            SelectFirstActiveTab();
        }

        #endregion

        #region → Events         .

        /// <summary>
        /// Occurs when [on activation changed].
        /// </summary>
        public event EventHandler<EventArgs> onActivationChanged;

        #endregion

        #region → Methods        .

        #region → Private        .

        /// <summary>
        /// Selects the first active tab.
        /// </summary>
        private void SelectFirstActiveTab()
        {
            if (this.Items == null)
            {
                return;
            }

            var firstActiveTab = this.Items.Where(s => (s as eNegTabItem).Visibility == System.Windows.Visibility.Visible).FirstOrDefault();

            if (firstActiveTab != null)
            {
                this.SelectedItem = firstActiveTab;
            }
        }

        #endregion

        #endregion

    }
}
