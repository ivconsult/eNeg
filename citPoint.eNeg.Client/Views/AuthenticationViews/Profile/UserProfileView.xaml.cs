
#region → Usings   .
using System.ComponentModel.Composition;
using System.Windows.Controls;
using citPOINT.eNeg.Common;
using citPOINT.eNeg.ViewModel;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
#endregion

#region → History  .
/* Date         User        Change
 * 
 * 05.08.10    m.Wahab     • creation
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
    /// Update MyProfile Or any User Profile
    /// </summary>
    public partial class UserProfileView : UserControl, ICleanup
    {
        #region → Properties     .

        #region " Using MEF to import UpdateUserProfileViewModel "

        /// <summary>
        /// Sets the view model.
        /// </summary>
        /// <value>The view model.</value>
        public UpdateUserProfileViewModel ViewModel
        {
            get
            {
                return (DataContext as UpdateUserProfileViewModel);
            }
            set
            {
                DataContext = value;
            }
        }

        /// <summary>
        /// Gets or sets the view model.
        /// </summary>
        /// <value>The view model.</value>
        public ManageUserOrganizationViewModel ManageUserOrganizationViewModel
        {
            get
            {
                return this.ViewModel.ManageUserOrgViewModel;
            }
            set
            {
                this.ViewModel.ManageUserOrgViewModel = value;
                this.uxcntUserOrganizationManage.DataContext = value;
            }
        }

        #endregion

        #endregion

        #region → Constructors   .

        /// <summary>
        /// Initializes a new instance of the <see cref="UserProfileView"/> class.
        /// </summary>
        public UserProfileView(UpdateUserProfileViewModel updateUserProfileViewModel, ManageUserOrganizationViewModel manageUserOrganizationViewModel)
        {
            InitializeComponent();

            this.ViewModel = updateUserProfileViewModel;
            this.ManageUserOrganizationViewModel = manageUserOrganizationViewModel;
        }

        #endregion

        #region → Event Handlers .

        /// <summary>
        /// For Make Save button as Accept Key
        /// </summary>
        /// <param name="sender">Value of Sender</param>
        /// <param name="e">Value of KeyEventArgs</param>
        private void UserControl_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                uxtxt_LastName.Focus();
                uxtxt_FirstName.Focus();
                Dispatcher.BeginInvoke(() => { uxcmdSave.Command.Execute(uxcmdSave.CommandParameter); });
            }
        }

        #endregion

        #region → Methods        .

        #region → Private        .


        #endregion

        #region → Public         .

        /// <summary>
        /// ICleanup interface implementation
        /// </summary>
        public void Cleanup()
        {
            // call Cleanup on its ViewModel
            ((ICleanup)this.DataContext).Cleanup();

            this.uxcntUserOrganizationManage.Cleanup();

            // Cleanup itself
            Messenger.Default.Unregister(this);
        }

        #endregion

        #endregion
    }
}
