
#region → Usings   .
using System.Windows.Controls;
using citPOINT.eNeg.Common;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;

#endregion

#region → History  .

/* Date         User        Change
 * 
 * 25.09.11     M.Wahab     • creation
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
    /// Public Users View
    /// </summary>
    public partial class PublicUsers : UserControl, ICleanup
    {

        #region → Fields         .

        PublicUsersOverview mPublicUsersOverviewView;

        #endregion Fields

        #region → Properties     .

        /// <summary>
        /// Gets the users overview.
        /// </summary>
        /// <value>The users overview.</value>
        public PublicUsersOverview PublicUsersOverviewView
        {
            get
            {
                if (mPublicUsersOverviewView == null)
                {
                    mPublicUsersOverviewView = new PublicUsersOverview();
                }
                return mPublicUsersOverviewView;
            }
        }

        #endregion Properties

        #region → Constructors   .
        /// <summary>
        /// Initializes a new instance of the <see cref="PublicUsers"/> class.
        /// </summary>
        public PublicUsers()
        {
            InitializeComponent();

            uxPublicUsersContent.Content = PublicUsersOverviewView;

            eNegMessanger.FlippMessage.Register(this, OnFlipMessage);

            Dispatcher.BeginInvoke(() => SetViewSize());

        }

        /// <summary>
        /// Sets the size of the view.
        /// </summary>
        private void SetViewSize()
        {
            this.uxPublicUsersContent.MaxWidth = this.ActualWidth;
        }

        #endregion Constructors

        #region → Methods        .

        #region → Public         .

        /// <summary>
        /// Called when [flip message].
        /// </summary>
        /// <param name="PageName">Name of the page.</param>
        public void OnFlipMessage(string PageName)
        {
            if (PageName == ViewTypes.PublishedProfiles)
            {
                if (uxPublicUsersContent.Content != null &&
                    uxPublicUsersContent.Content.GetType().Equals(typeof(PublishedProfileDetailsView)))
                {
                    (uxPublicUsersContent.Content as PublishedProfileDetailsView).Cleanup();
                }

                uxPublicUsersContent.Content = PublicUsersOverviewView;
            }
            else
            {
                uxPublicUsersContent.Content = new PublishedProfileDetailsView(this.PublicUsersOverviewView.ViewModel.CurrentUser);
            }
        }

        /// <summary>
        /// ICleanup interface implementation
        /// </summary>
        public void Cleanup()
        {
            // call Cleanup on its ViewModel
            this.PublicUsersOverviewView.Cleanup();

            // Cleanup itself
            Messenger.Default.Unregister(this);
        }

        #endregion

        #endregion
    }
}
