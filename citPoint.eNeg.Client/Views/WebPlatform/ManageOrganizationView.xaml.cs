#region → Usings   .
using System.Windows.Controls;
using citPOINT.eNeg.Common;
using citPOINT.eNeg.ViewModel;
using System.Windows;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using System.ComponentModel.Composition;
using citPOINT.eNeg.Data.Web;
using System;
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
    /// Manage Organization View
    /// </summary>
    public partial class ManageOrganizationView : UserControl, ICleanup
    {

        #region → Properties     .

        #region " Using MEF to import LoginFormViewModel "

        /// <summary>
        /// Sets the view model.
        /// </summary>
        /// <value>The view model.</value>
        [Import(ViewModelTypes.ManageOrganizationViewModel)]
        public ManageOrganizationViewModel ViewModel
        {
            get
            {
                return DataContext as ManageOrganizationViewModel;
            }
            set
            {
                DataContext = value;
            }
        }
        #endregion

        #endregion
        
        #region → Constructors   .

        /// <summary>
        /// Initializes a new instance of the <see cref="ManageOrganizationView"/> class.
        /// </summary>
        public ManageOrganizationView()
        {
            InitializeComponent();

            #region → Use MEF To load the View Model                       .

            if (!ViewModelBase.IsInDesignModeStatic)
            {
                // Use MEF To load the View Model
                CompositionInitializer.SatisfyImports(this);
            }
            #endregion 

            #region → Initialize the UserControl Width & Height            .

            ViewsAdjustSize ViewsAdjustSize = new ViewsAdjustSize(this);

            #endregion Intialize PageSize

            #region → Registeration for needed messages in eNegMessenger   .

            eNegMessanger.SendCustomMessage.Register(this, OnUpdateMessage);

            eNegMessanger.EditUserOrganizationMessage.Register(this, OnEditUserOrganizationMessage);
            #endregion
        }

        #endregion

        #region → Event Handlers .

        /// <summary>
        /// Handles the Click event of the uxcmdRemove control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void uxcmdRemove_Click(object sender, RoutedEventArgs e)
        {
            this.ViewModel.RemoveUserFromOrganizationCommand.Execute((sender as HyperlinkButton).Tag);
        }

        #endregion

        #region → Methods        .

        #region → Private        .

        /// <summary>
        /// Called when [update message].
        /// </summary>
        /// <param name="Message">The message.</param>
        private void OnUpdateMessage(eNegMessage Message)
        {
            if (Message.ReceiverApplicationID == Guid.Empty)
            {
                Dispatcher.BeginInvoke(() =>
                {
                    uxSPSucessMessage.MessageText = Message.Message;
                    uxSPSucessMessage.Completed = Message.ShowMessageCompleted;
                    uxSPSucessMessage.Show();
                });
            }
        }

        /// <summary>
        /// Called when [edit user organization message].
        /// </summary>
        /// <param name="userOrganization">The user organization.</param>
        private void OnEditUserOrganizationMessage(UserOrganization userOrganization)
        {
            this.uxgrdOrganizations.SetIsSelected(userOrganization, true);
        }
        #endregion

        #region → Public         .

        /// <summary>
        /// Cleanups this instance.
        /// </summary>
        public void Cleanup()
        {
            // call Cleanup on its ViewModel
            this.ViewModel.Cleanup();

            // Cleanup itself
            Messenger.Default.Unregister(this);
        }

        #endregion        

        #endregion
    }
}
