
#region → Usings   .
using System.ComponentModel.Composition;
using System.Windows.Controls;
using citPOINT.eNeg.Common;
using citPOINT.eNeg.ViewModel;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using System;
using citPOINT.eNeg.Data.Web;
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
    public partial class PublishedProfileDetailsView : UserControl, ICleanup
    {
        #region → Properties     .

        #region " Using MEF to import UpdateUserProfileViewModel "

        /// <summary>
        /// Sets the view model.
        /// </summary>
        /// <value>The view model.</value>
        [Import(ViewModelTypes.PublishedProfileDetailsViewModel)]
        public PublishedProfileDetailsViewModel ViewModel
        {
            get
            {
                return (DataContext as PublishedProfileDetailsViewModel);
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
        /// Initializes a new instance of the <see cref="PublishedProfileDetailsView"/> class.
        /// </summary>
        /// <param name="ProfileUserID">The profile user ID.</param>
        public PublishedProfileDetailsView(Guid ProfileUserID)
        {
            InitializeComponent();

            IntializeView();

            this.ViewModel.UserID = ProfileUserID;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PublishedProfileDetailsView"/> class.
        /// </summary>
        /// <param name="ProfileUser">The profile user.</param>
        public PublishedProfileDetailsView(User ProfileUser)
        {
            InitializeComponent();

            IntializeView();

            this.ViewModel.CurrentUser = ProfileUser;
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
                    uxSPSucessMessage.MessageType = Message.MessageType;
                    uxSPSucessMessage.Completed = Message.ShowMessageCompleted;
                    uxSPSucessMessage.Show();
                });
            }
        }

        /// <summary>
        /// Intializes the view.
        /// </summary>
        private void IntializeView()
        {
            #region → Use MEF To load the View Model                       .
           
            if (!ViewModelBase.IsInDesignModeStatic)
            {
                CompositionInitializer.SatisfyImports(this);
            }
           
            #endregion
                   

            #region → Registeration for needed messages in eNegMessenger   .

            eNegMessanger.SendCustomMessage.Register(this, OnUpdateMessage);
            #endregion
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

            // Cleanup itself
            Messenger.Default.Unregister(this);
        }

        #endregion

        #endregion
    }
}
