#region → Usings   .
using System.Windows.Controls;
using citPOINT.eNeg.Common;
using citPOINT.eNeg.ViewModel;
using System.Windows;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
#endregion

#region → History  .
/* Date         User        Change
 * 
 * 16.08.11    m.Wahab     • creation
 * 
 */
#endregion

#region → ToDos    .

/*
 * Date         set by User     Description
 * 
 * 
*/

#endregion

namespace citPOINT.eNeg.Client
{
    /// <summary>
    /// Manage User Organization View
    /// </summary>
    public partial class ManageUserOrganizationView : UserControl, ICleanup
    {
        #region → Constructors   .

        /// <summary>
        /// Initializes a new instance of the <see cref="ManageUserOrganizationView"/> class.
        /// </summary>
        public ManageUserOrganizationView()
        {
            InitializeComponent();
            eNegMessanger.NewPopUp.Register(this, OnAddNewPopUp);
        }

        #endregion

        #region → Event Handlers .

        /// <summary>
        /// Handles the Click event of the uxcmdLeave control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void uxcmdLeave_Click(object sender, RoutedEventArgs e)
        {
            (this.DataContext as ManageUserOrganizationViewModel).LeaveOrganizationCommand.Execute((sender as System.Windows.Controls.HyperlinkButton).Tag);

        }
        #endregion

        #region → Methods        .

        #region → Private        .

        /// <summary>
        /// Called when [add new pop up].
        /// </summary>
        /// <param name="PopUpName">Name of the pop up.</param>
        private void OnAddNewPopUp(string PopUpName)
        {
            if (eNegMessanger.NewPopUp.PopUpType == eNegMessanger.PopUpType.AddNewOrganization.ToString())
            {
                #region Show PopUp window to choose identify set of receivers and add it
                PopUpWindow addNewOrganization = new PopUpWindow(PopUpName);
                addNewOrganization.DataContext = this.DataContext;
                addNewOrganization.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
                addNewOrganization.VerticalAlignment = System.Windows.VerticalAlignment.Center;
                addNewOrganization.Content = new AddNewOrganization();
                addNewOrganization.CenterWindow();
                addNewOrganization.ShowDialog();
                #endregion
            }
        }

        #endregion

        #region → Public         .

        /// <summary>
        /// Cleanups this instance.
        /// </summary>
        public void Cleanup()
        {
            // call Cleanup on its ViewModel
            ((ICleanup)this.DataContext).Cleanup();

            // Cleanup itself
            Messenger.Default.Unregister(this);
        }

        #endregion

        #endregion
    }
}
