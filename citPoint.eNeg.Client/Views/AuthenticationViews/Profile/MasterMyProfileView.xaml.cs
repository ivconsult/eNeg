
#region → Usings   .
using System.ComponentModel.Composition;
using System.Windows.Controls;
using citPOINT.eNeg.Common;
using citPOINT.eNeg.ViewModel;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using System;
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
    public partial class MasterMyProfileView : UserControl, ICleanup
    {
        #region → Fields         .

        private UserProfileView mUserProfileView;
        private ChangeEmailAddressView mChangeEmailAddressView;
        private ChangePasswordView mChangePasswordView;

        #endregion

        #region → Properties     .

        #region " Using MEF to import UpdateUserProfileViewModel "

        /// <summary>
        /// Gets the user profile view.
        /// </summary>
        /// <value>The user profile view.</value>
        private UserProfileView UserProfileView
        {
            get
            {
                if (mUserProfileView == null)
                {
                    mUserProfileView = new UserProfileView(this.ViewModel, this.ManageUserOrganizationViewModel);
                }

                return mUserProfileView;
            }
        }

        /// <summary>
        /// Gets the change email address view.
        /// </summary>
        /// <value>The change email address view.</value>
        private ChangeEmailAddressView ChangeEmailAddressView
        {
            get
            {
                if (mChangeEmailAddressView == null)
                {
                    mChangeEmailAddressView = new ChangeEmailAddressView();
                    mChangeEmailAddressView.DataContext = this.ViewModel;
                }

                return mChangeEmailAddressView;
            }
        }

        /// <summary>
        /// Gets the change password view.
        /// </summary>
        /// <value>The change password view.</value>
        private ChangePasswordView ChangePasswordView
        {
            get
            {
                if (mChangePasswordView == null)
                {
                    mChangePasswordView = new ChangePasswordView();
                    mChangePasswordView.DataContext = this.ViewModel;
                }

                return mChangePasswordView;
            }
        }

        /// <summary>
        /// Sets the view model.
        /// </summary>
        /// <value>The view model.</value>
        [Import(ViewModelTypes.UpdateUserProfileViewModel)]
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
        [Import(ViewModelTypes.ManageUserOrganizationViewModel)]
        public ManageUserOrganizationViewModel ManageUserOrganizationViewModel
        {
            get
            {
                return this.ViewModel.ManageUserOrgViewModel;
            }
            set
            {
                this.ViewModel.ManageUserOrgViewModel = value;
            }
        }

        #endregion

        #endregion

        #region → Constructors   .

        /// <summary>
        /// Initializes a new instance of the <see cref="MasterMyProfileView"/> class.
        /// </summary>
        public MasterMyProfileView()
        {
            InitializeComponent();

            #region → Use MEF To load the View Model                       .
            if (!ViewModelBase.IsInDesignModeStatic)
            {
                CompositionInitializer.SatisfyImports(this);
            }
            #endregion

            #region → Registeration for needed messages in eNegMessenger   .

            eNegMessanger.SendCustomMessage.Register(this, OnUpdateMessage);
            eNegMessanger.FlippMessage.Register(this, onFlippMessage);

            #endregion

            this.uxcntMainContent.Content = this.UserProfileView;

            Dispatcher.BeginInvoke(() => SetViewSize());
        }


        #endregion

        #region → Event Handlers .


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
        /// Ons the flipp message.
        /// </summary>
        /// <param name="viewName">Name of the view.</param>
        private void onFlippMessage(string viewName)
        {
            if (viewName == ViewTypes.InnerViewChangeUserEmail)
            {
                this.uxcntMainContent.Content = this.ChangeEmailAddressView;
            }
            else if (viewName == ViewTypes.InnerViewChangeUserPassword)
            {
                this.uxcntMainContent.Content = this.ChangePasswordView;
            }
            else if (viewName == ViewTypes.InnerViewChangeUserContactsDetails)
            {
                this.uxcntMainContent.Content = this.UserProfileView;
            }
            else if (viewName == ViewTypes.ClosePopupView)
            {
                PopUpWindow parentPopupWindow = (GetPopUpWindow(this) as PopUpWindow);

                if (parentPopupWindow != null && parentPopupWindow.IsMostTopWindow)
                {
                    this.Cleanup();
                }
            }
        }

        /// <summary>
        /// Gets the pop up window.
        /// </summary>
        /// <param name="control">The control.</param>
        /// <returns></returns>
        private Control GetPopUpWindow(Control control)
        {
            if (control != null && control is Control)
            {
                if (control is PopUpWindow)
                {
                    return control;
                }
                else
                {
                    return GetPopUpWindow((control.Parent as Control));
                }
            }

            return null;
        }
        
        /// <summary>
        /// Sets the size of the view.
        /// </summary>
        private void SetViewSize()
        {
            this.uxcntMainContent.MaxWidth = this.ActualWidth;
        }

        #endregion

        #region → Public         .

        /// <summary>
        /// ICleanup interface implementation
        /// </summary>
        public void Cleanup()
        {
            // call Cleanup on its ViewModel
            ((ICleanup)this.DataContext).Cleanup();

            this.ManageUserOrganizationViewModel.Cleanup();

            if (mUserProfileView != null)
            {
                mUserProfileView.Cleanup();
            }

            if (mChangeEmailAddressView != null)
            {
                //mChangeEmailAddressView.Cleanup();
            }

            if (mChangePasswordView != null)
            {
                //mChangePasswordView.Cleanup();
            }

            // Cleanup itself
            Messenger.Default.Unregister(this);
        }

        #endregion

        #endregion
    }
}
