
#region → Usings   .

using System;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Practices.Prism.Regions;
using Telerik.Windows.Controls;
using citPOINT.eNeg.Apps.Common.Interfaces;


#endregion

#region → History  .

/* 
 * Date         User            Change
 * *********************************************
 * 18.03.2012   Mwahab         • creation
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
    /// eNeg Tab Item.
    /// </summary>
    public class eNegTabItem : RadTabItem
    {

        #region → Fields         .

        private IeNegApplication eNegApp;

        #endregion

        #region → Constractor    .

        /// <summary>
        /// Initializes a new instance of the <see cref="eNegTabItem"/> class.
        /// </summary>
        /// <param name="eNegApp">The e neg app.</param>
        /// <param name="onActivationChanged">The on activation changed.</param>
        public eNegTabItem(IeNegApplication eNegApp, EventHandler<EventArgs> onActivationChanged)
        {
            //Set Control Name
            this.Name = "Name" + DateTime.Now.Ticks;

            //to not duplicate the control name.
            Thread.Sleep(5);

            //Header of the tab control e.g. [Pref App.]
            this.Header = eNegApp.ApplicationTitle;

            this.Content = new ContentControl()
            {
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalContentAlignment = HorizontalAlignment.Stretch,

                VerticalAlignment = VerticalAlignment.Stretch,
                VerticalContentAlignment = VerticalAlignment.Stretch
            };


            //Set Region Name [Prism region]
            (this.Content as ContentControl).SetValue(RegionManager.RegionNameProperty, eNegApp.RegionName);

            this.eNegApp = eNegApp;

            //Handling activation changes on Parent control.
            this.onActivationChanged = onActivationChanged;

            this.HandleActivationState();

            eNegApp.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(eNegApp_PropertyChanged);
        }

        #endregion

        #region → Event Handlers .

        /// <summary>
        /// Handles the PropertyChanged event of the IeNegApplication control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.ComponentModel.PropertyChangedEventArgs"/> instance containing the event data.</param>
        private void eNegApp_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (this.eNegApp != null && e.PropertyName == "IsActive")
            {
                HandleActivationState();
            }
        }

        #endregion

        #region → Events         .

        /// <summary>
        /// on Activation Changed.
        /// </summary>
        private EventHandler<EventArgs> onActivationChanged;

        #endregion

        #region → Methods        .

        #region → Private        .

        /// <summary>
        /// Handles the state of the activation.
        /// </summary>
        private void HandleActivationState()
        {
            this.Visibility = eNegApp.IsActive ? Visibility.Visible : Visibility.Collapsed;

            if (this.onActivationChanged != null)
            {
                this.onActivationChanged(this, null);
            }
        }

        #endregion

        #endregion

    }
}
