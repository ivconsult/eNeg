#region → Usings   .
using citPOINT.eNeg.Common;
using citPOINT.eNeg.Data.Web;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.ServiceModel.DomainServices.Client;
using HashHelper = citPOINT.eNeg.Common.HashHelper;
using System.Windows.Browser;

#endregion

#region → History  .

/* Date         User            Change
 * 
 * 19.10.10     M.Wahab     • creation
 */

# endregion

#region → ToDos    .

/*
 * Date         set by User     Description
 * 
 * 
*/

# endregion

namespace citPOINT.eNeg.ViewModel
{
    #region  Using MEF to export Update User Profile ViewModel

    /// <summary>
    /// This Class Do all Operations Related To Update My Profile or Update any user Profile.
    /// And Sending Mail to the User in case he change his mail.
    /// </summary>
    [Export(ViewModelTypes.PublishedProfileDetailsViewModel)]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    #endregion
    public class PublishedProfileDetailsViewModel : ViewModelBase
    {

        #region → Fields         .

        private IPublishedProfileDetailsModel mPublishedProfileDetailsModel;

        private User mCurrentUser;
        private bool mIsBusy;
        private bool mIsForManageOrganization = false;
        private Guid mUserID;
        private string mPrefix;

        private IEnumerable<Country> mCountryEntries;
        private IEnumerable<UserProfileStatisticalsResult> mStatisticalSource;
        private RelayCommand mLinkClickedCommand;

        private decimal mNegotiationCount = 0;


        private decimal mConversationCount = 0;

        private decimal mActiveAppsAvg = 0;


        #endregion

        #region → Properties     .

        /// <summary>
        /// Indicating That The Current User Is In Progress
        /// Saving Data.
        /// </summary>
        public bool IsBusy
        {
            get
            {
                return mIsBusy;
            }
            set
            {
                mIsBusy = value;
                this.RaisePropertyChanged("IsBusy");
            }
        }



        /// <summary>
        /// Gets or sets the prefix.
        /// </summary>
        /// <value>The prefix.</value>
        public string Prefix
        {
            get
            {
                return mPrefix;
            }
            set
            {
                mPrefix = value;
                this.RaisePropertyChanged("Prefix");
            }
        }

        /// <summary>
        /// The Value Of the Current User
        /// (New User info.)
        /// </summary>
        public User CurrentUser
        {
            get { return mCurrentUser; }
            set
            {
                if (mCurrentUser != value)
                {
                    mCurrentUser = value;

                    if (value != null)
                    {
                        GetUserProfileStatisticalsAsyc(value.UserID);
                        Prefix = value.Gender.HasValue && !value.Gender.Value ? "Mr." : "Miss.";
                    }
                    this.RaisePropertyChanged("CurrentUser");
                }
            }
        }

        /// <summary>
        /// Gets or sets the user ID.
        /// Used in case if the user is not loaded
        /// </summary>
        /// <value>The user ID.</value>
        public Guid UserID
        {
            get
            {
                return mUserID;
            }
            set
            {
                mUserID = value;
                this.IsForManageOrganization = true;
                this.CanUserSeeMemberProfileAsync(value);
            }
        }


        /// <summary>
        /// Gets or sets a value indicating whether this instance is for manage organization.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is for manage organization; otherwise, <c>false</c>.
        /// </value>
        public bool IsForManageOrganization
        {
            get
            {
                return mIsForManageOrganization;
            }
            set
            {
                mIsForManageOrganization = value;
                this.RaisePropertyChanged("IsForManageOrganization");
                this.RaisePropertyChanged("IsForPublicProfile");
                this.RaisePropertyChanged("LinkLabel");

            }
        }


        /// <summary>
        /// Gets a value indicating whether this instance is for manage organization.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is for manage organization; otherwise, <c>false</c>.
        /// </value>
        public bool IsForPublicProfile
        {
            get
            {
                return !IsForManageOrganization;
            }
        }

        /// <summary>
        /// Gets the link label.
        /// </summary>
        /// <value>The link label.</value>
        public string LinkLabel
        {
            get
            {
                return IsForPublicProfile ? " << Back to overview" : " >> go to organization management";
            }
        }


        /// <summary>
        /// Gets or sets the negotiation count.
        /// </summary>
        /// <value>The negotiation count.</value>
        public decimal NegotiationCount
        {
            get
            {
                return mNegotiationCount;
            }
            set
            {
                mNegotiationCount = value;
                this.RaisePropertyChanged("NegotiationCount");
            }
        }

        /// <summary>
        /// Gets or sets the conversation count.
        /// </summary>
        /// <value>The conversation count.</value>
        public decimal ConversationCount
        {
            get
            {
                return mConversationCount;
            }
            set
            {
                mConversationCount = value;
                this.RaisePropertyChanged("ConversationCount");
            }
        }

        /// <summary>
        /// Gets or sets the active apps avg.
        /// </summary>
        /// <value>The active apps avg.</value>
        public decimal ActiveAppsAvg
        {
            get
            {
                return mActiveAppsAvg;
            }
            set
            {
                mActiveAppsAvg = value;
                this.RaisePropertyChanged("ActiveAppsAvg");
            }
        }

        #region → Lockup tables    .


        /// <summary>
        /// Value Of Lockup Table (Country)
        /// </summary>
        public IEnumerable<Country> CountryEntries
        {
            get { return mCountryEntries; }
            private set
            {
                if (value != mCountryEntries)
                {
                    mCountryEntries = value;
                    this.RaisePropertyChanged("CountryEntries");
                }
            }
        }


        /// <summary>
        /// Gets or sets the statistical source.
        /// Chart Source
        /// </summary>
        /// <value>The statistical source.</value>
        public IEnumerable<UserProfileStatisticalsResult> StatisticalSource
        {

            get { return mStatisticalSource; }
            private set
            {

                this.mStatisticalSource = value;
                this.FillStatisticals();
                this.RaisePropertyChanged("StatisticalSource");
            }
        }

        #endregion

        #endregion Properties

        #region → Constructors   .

        /// <summary>
        /// Initializes a new instance of the <see cref="PublishedProfileDetailsViewModel"/> class.
        /// </summary>
        /// <param name="PublishedProfileDetailsModel">The user register model.</param>
        [ImportingConstructor]
        public PublishedProfileDetailsViewModel(IPublishedProfileDetailsModel PublishedProfileDetailsModel)
        {
            mPublishedProfileDetailsModel = PublishedProfileDetailsModel;

            #region → Set up event handling  .

            mPublishedProfileDetailsModel.PropertyChanged += new PropertyChangedEventHandler(mPublishedProfileDetailsModel_PropertyChanged);
            mPublishedProfileDetailsModel.GetCountryComplete += new EventHandler<eNegEntityResultArgs<Country>>(mPublishedProfileDetailsModel_GetCountryComplete);
            mPublishedProfileDetailsModel.GetUserByIDComplete += new EventHandler<eNegEntityResultArgs<User>>(mPublishedProfileDetailsModel_GetUserByIDComplete);
            mPublishedProfileDetailsModel.GetUserProfileStatisticalsComplete += new EventHandler<eNegEntityResultArgs<UserProfileStatisticalsResult>>(mPublishedProfileDetailsModel_GetUserProfileStatisticalsComplete);
            mPublishedProfileDetailsModel.CanUserSeeMemberProfileComplete += new Action<InvokeOperation<bool>>(mPublishedProfileDetailsModel_CanUserSeeMemberProfileComplete);

            #endregion

            #region → Loading Related Tables .

            GetCountryAsync();

            #endregion
        }




        #endregion "Constructor"

        #region → Event Handlers .

        #region →  Loading Event Handlers .

        /// <summary>
        /// Complete events of GetCountry
        /// that return A value of all records of table (Country).
        /// </summary>
        /// <param name="sender">Value Of Sender</param>
        /// <param name="e">Value Of e</param>
        private void mPublishedProfileDetailsModel_GetCountryComplete(object sender, eNegEntityResultArgs<Country> e)
        {

            if (!e.HasError)
            {
                CountryEntries = e.Results.OrderBy(g => g.CountryName);

                // raise property changed for CurrentUser to reflect changes.
                this.RaisePropertyChanged("CurrentUser");
            }
            else
            {
                // notify user if there is any error
                eNegMessanger.RaiseErrorMessage.Send(e.Error);
            }
        }

        /// <summary>
        /// Complete handler of Get User By ID.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        private void mPublishedProfileDetailsModel_GetUserByIDComplete(object sender, eNegEntityResultArgs<User> e)
        {
            if (!e.HasError)
            {
                CurrentUser = e.Results.FirstOrDefault();

                // raise property changed for CurrentUser to reflect changes.
                this.RaisePropertyChanged("CurrentUser");
            }
            else
            {
                // notify user if there is any error
                eNegMessanger.RaiseErrorMessage.Send(e.Error);
            }
        }

        /// <summary>
        /// Get user profile statisticals complete.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        private void mPublishedProfileDetailsModel_GetUserProfileStatisticalsComplete(object sender, eNegEntityResultArgs<UserProfileStatisticalsResult> e)
        {
            if (!e.HasError)
            {
                StatisticalSource = e.Results;
            }
            else
            {
                // notify user if there is any error
                eNegMessanger.RaiseErrorMessage.Send(e.Error);
            }
        }

        /// <summary>
        /// Ms the published profile details model_ can user see member profile complete.
        /// </summary>
        /// <param name="e">The e.</param>
        private void mPublishedProfileDetailsModel_CanUserSeeMemberProfileComplete(InvokeOperation<bool> e)
        {
            if (!e.HasError)
            {
                if (e.Value == false)
                {
                    ShowMessage();
                }
                else
                {
                    this.GetUserByID(this.UserID);
                }
            }
            else
            {
                // notify user if there is any error
                eNegMessanger.RaiseErrorMessage.Send(e.Error);
            }
        }

        #endregion

        /// <summary>
        /// Indicating That thier is any changes in the Current Data.
        /// </summary>
        /// <param name="sender">Value Of Sender</param>
        /// <param name="e">Value Of e</param>
        private void mPublishedProfileDetailsModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals("HasChanges") || e.PropertyName.Equals("IsBusy"))
            {

            }
        }

        #endregion

        #region → Commands       .


        /// <summary>
        /// Gets the link clicked command.
        /// </summary>
        /// <value>The link clicked command.</value>
        public RelayCommand LinkClickedCommand
        {
            get
            {
                if (mLinkClickedCommand == null)
                {
                    mLinkClickedCommand = new RelayCommand(() =>
                    {
                        try
                        {
                            if (IsForManageOrganization)
                            {
                                eNegMessanger.ChangeScreenMessage.Send(ViewTypes.OrganizationManagement);
                            }
                            else
                            {
                                eNegMessanger.FlippMessage.Send(ViewTypes.PublishedProfiles);
                            }

                        }
                        catch (Exception ex)
                        {
                            // notify user if there is any error
                            eNegMessanger.RaiseErrorMessage.Send(ex);
                        }
                    }
                    , () => true);
                }
                return mLinkClickedCommand;
            }
        }

        #endregion

        #region → Methods        .

        #region → Private        .

        /// <summary>
        /// Shows the message After save or After Sending The Mail.
        /// </summary>
        private void ShowMessage()
        {
            #region ► Show Message ◄

            eNegMessage message = new eNegMessage(Resources.NotAuthorizedProfile);
            message.MessageType = ImageType.Error;

            message.ShowMessageCompleted += (s, args) =>
            {
                HtmlPage.Window.Navigate(new Uri(AppConfigurations.HostBaseAddress + "Default.aspx", UriKind.Absolute));
            };

            eNegMessanger.SendCustomMessage.Send(message);

            #endregion
        }

        /// <summary>
        /// Fills the statisticals.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        private decimal FillStatisticals(string name)
        {
            if (this.StatisticalSource != null)
            {
                var statisticalItem = this.StatisticalSource.Where(ss => ss.StatisticalName == name).FirstOrDefault();
                if (statisticalItem != null && statisticalItem.StatisticalValue.HasValue)
                    return statisticalItem.StatisticalValue.Value;
            }
            return 0;
        }

        /// <summary>
        /// Fills the statisticals.
        /// </summary>
        private void FillStatisticals()
        {
            this.NegotiationCount = FillStatisticals("Negotiation");
            this.ConversationCount = FillStatisticals("Conversation");
            this.ActiveAppsAvg = FillStatisticals("Apps.Avg");
        }

        #endregion

        #region → Public         .

        /// <summary>
        /// Gets the user by ID.
        /// </summary>
        /// <param name="userID">The user ID.</param>
        public void GetUserByID(Guid userID)
        {
            mPublishedProfileDetailsModel.GetUserByID(userID);
        }

        /// <summary>
        /// Loading table (Country).
        /// </summary>
        public void GetCountryAsync()
        {
            mPublishedProfileDetailsModel.GetCountryAsync();
        }

        /// <summary>
        /// Gets the user profile statisticals asyc.
        /// </summary>
        /// <param name="userID">The user ID.</param>
        public void GetUserProfileStatisticalsAsyc(Guid userID)
        {
            this.mPublishedProfileDetailsModel.GetUserProfileStatisticalsAsyc(userID);
        }

        /// <summary>
        /// Determines whether this instance can user see member profile or not.
        /// </summary>
        /// <param name="userID">The user ID.</param>
        public void CanUserSeeMemberProfileAsync(Guid userID)
        {
            this.mPublishedProfileDetailsModel.CanUserSeeMemberProfileAsync(userID);
        }

        #region "ICleanup interface implementation"
        /// <summary>
        /// Cleanups this instance.
        /// </summary>
        public override void Cleanup()
        {
            // Unregister all events

            mPublishedProfileDetailsModel.PropertyChanged -= new PropertyChangedEventHandler(mPublishedProfileDetailsModel_PropertyChanged);
            mPublishedProfileDetailsModel.GetCountryComplete -= new EventHandler<eNegEntityResultArgs<Country>>(mPublishedProfileDetailsModel_GetCountryComplete);
            mPublishedProfileDetailsModel.GetUserByIDComplete -= new EventHandler<eNegEntityResultArgs<User>>(mPublishedProfileDetailsModel_GetUserByIDComplete);

            // unregister any messages for this ViewModel
            base.Cleanup();

            // unregister any messages for this ViewModel
            // Cleanup itself
            Messenger.Default.Unregister(this);

        }

        #endregion "ICleanup interface implementation"

        #endregion

        #endregion
    }
}


