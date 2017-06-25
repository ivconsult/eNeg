
#region → Usings   .
using System.Windows.Controls;
using citPOINT.eNeg.Common;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;

#endregion

#region → History  .

/* Date         User            Change
 * 
 * 01.11.10     Yousra Reda     creation
 */

# endregion

#region → ToDos    .

/*
 * Date         set by User     Description
 * 
 * 
*/

# endregion
namespace citPOINT.eNeg.Client
{
    /// <summary>
    /// Manage Users View
    /// </summary>
    public partial class ManageUsers : UserControl, ICleanup
    {

        #region → Fields         .

        UsersOverview mUsersOverviewView;
        MasterMyProfileView masterMyProfileView;

        #endregion Fields

        #region → Properties     .
       
        /// <summary>
        /// Gets the users overview.
        /// </summary>
        /// <value>The users overview.</value>
        public UsersOverview UsersOverviewView
        {
            get
            {
                if (mUsersOverviewView == null)
                {
                    mUsersOverviewView = new UsersOverview();
                }
                return mUsersOverviewView;
            }
        }

        #endregion Properties

        #region → Constructors   .
      
        /// <summary>
        /// Initializes a new instance of the <see cref="ManageUsers"/> class.
        /// </summary>
        public ManageUsers()
        {
            InitializeComponent();

            uxManageUsersContent.Content = UsersOverviewView;

            eNegMessanger.FlippMessage.Register(this, OnFlipMessage);
        }
        
        #endregion

        #region → Methods        .

        #region → Private        .
        
        /// <summary>
        /// Called when [flip message].
        /// </summary>
        /// <param name="PageName">Name of the page.</param>
        private void OnFlipMessage(string PageName)
        {
            if (PageName == ViewTypes.UsersOverview)
            {
                this.CleanupInnerView();

                //Re-check users roles
                this.UsersOverviewView.ViewModel.RefreshDataSource();

                uxManageUsersContent.Content = UsersOverviewView;
            }
            else if (PageName == ViewTypes.UserDetails)
            {

                this.CleanupInnerView();

                masterMyProfileView = new MasterMyProfileView();

                uxManageUsersContent.Content = masterMyProfileView;
            }
            else if (PageName == ViewTypes.ClosePopupView)
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
        /// Cleanups the inner view.
        /// </summary>
        private void CleanupInnerView()
        {
            if (masterMyProfileView != null)
            {
                masterMyProfileView.Cleanup();
                masterMyProfileView = null;
            }
        }

        #endregion

        #region → Public         .
        
        /// <summary>
        /// ICleanup interface implementation
        /// </summary>
        public void Cleanup()
        {
            // call Cleanup on its ViewModel
            this.UsersOverviewView.Cleanup();

            // Cleanup itself
            Messenger.Default.Unregister(this);

            this.CleanupInnerView();
        }

        #endregion

        #endregion
    }
}
