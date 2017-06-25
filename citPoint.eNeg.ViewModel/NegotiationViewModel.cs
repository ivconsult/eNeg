
#region → Usings   .
using citPOINT.eNeg.Common;
using citPOINT.eNeg.Data.Web;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.ServiceModel.DomainServices.Client;
using System.Windows;
using System.Windows.Controls;
using ValidationResult = System.ComponentModel.DataAnnotations.ValidationResult;
using System.IO.IsolatedStorage;
using Telerik.Windows;
using Telerik.Windows.Controls;
using System.Windows.Automation;
using citPOINT.eNeg.Common.eNegApps;
using citPOINT.eNeg.Data;
using System.IO;
#endregion

#region → History  .

/* Date         User            Change
 * 
 * 03.08.10     M.Wahab         • creation
 * 31.08.10     Dergham         • Update XML Documentation
 * 05.09.10     Dergham         • Add mCurrentMessage fields and property
 * 06.09.10     M.Wahab         • Update XML Documentation And Region Accourding To New Code Convention
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

    #region → Using  MEF to export NegotiationViewModel
    /// <summary>
    /// This class is used to carry all operation related to Negotiation ,Conversation
    /// And Messages,And Application Activation Dectivation
    /// </summary>
    [Export(ViewModelTypes.NegotiationViewModel)]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    #endregion
    public partial class NegotiationViewModel : GalaSoft.MvvmLight.ViewModelBase
    {

        /// <summary>
        /// Wrapper class that used as a helper in binding When Adding Multiple Receivers in Message
        /// </summary>
        public class MultiNegotiator : Entity
        {
            /// <summary>
            /// Gets or sets the new negotiator.
            /// </summary>
            /// <value>The new negotiator.</value>
            public string NewNegotiator { get; set; }

            /// <summary>
            /// Gets or sets the old negotiators.
            /// </summary>
            /// <value>The old negotiators.</value>
            public IEnumerable<string> OldNegotiators { get; set; }
        }

        #region → Fields         .

        private const int CLICK_TIME = 3000000;
        private long mLastTicks = 0;
        public INegotiationModel mNegotiationModel;

        private bool sortNegotiations = false;

        private bool isLastActionPopup = false;

        private object mSelectedChannel;
        private List<IArchiveItem> mClosedNegotiationArchiveSource;

        #region Relay Commands
        private RelayCommand<string> mNavigationCommand = null;
        private RelayCommand mSubmitChangeCommand = null;
        private RelayCommand mCancelChangeCommand = null;
        private RelayCommand mDeleteItemCommand = null;
        private RelayCommand mAddNewNegotiationCommand = null;
        private RelayCommand mAddNewConversationCommand = null;
        private RelayCommand mCloseReOpenAllSelectedNegotiationCommand = null;
        private RelayCommand mCloseReOpenNegotiationCommand = null;
        private RelayCommand mDeleteSelectedItemsCommand = null;
        private RelayCommand mChangeApplicationStatusCommand = null;
        private RelayCommand mSubmitMessageChangesCommand = null;
        private RelayCommand mSubmitRenameChangesCommand = null;
        private RelayCommand mAddMoreReceiversCommand = null;
        private RelayCommand mAddReceiversToMessageCommand = null;
        private RelayCommand mSetLastRecentNegotiatorCommand = null;
        private RelayCommand mFlipMessageAreaCommand = null;
        private RelayCommand<RadRoutedEventArgs> mTreeCheckChangesCommand = null;
        private RelayCommand<IArchiveItem> mNavigateToConversationDetailsCommand = null;
        private RelayCommand mPublishNegotiationCommand = null;
        private RelayCommand mFinishPublishNegotiationCommand = null;
        private RelayCommand mCancelPublishNegotiationCommand = null;
        private RelayCommand mRaiseFinishPublishCommand = null;
        private RelayCommand<string> mNavigationByViewNameCommand;
        private RelayCommand<string> mCancelRenameChangesCommand;
        private RelayCommand mSavePDFCommand;
        private RelayCommand mStartNewNegotiationCommand;
        private RelayCommand mStartNewConversationCommand;
        #endregion Relay Commands

        private Negotiation mcurrentNegotiation;
        private Conversation mcurrentConversation;
        private Message mCurrentMessage;
        private IList<Negotiation> mOnGoingNegotiation;
        private IList<Negotiation> mClosedNegotiation;
        private bool mEnableMessageTextBox;
        private IEnumerable<Channel> mChannels;
        private List<string> mOldMessageSubjects;
        private IEnumerable<NegotiationApplicationStatu> mNegotiationApplicationStatus;
        private IList<NegotiationApplicationStatu> mNegotiationApplicationStatusDefault;
        private IEnumerable<citPOINT.eNeg.Data.Web.Application> mApplicationEntries;
        private IEnumerable<UserApplicationStatu> mDMApplication;
        private List<MultiNegotiator> mMultiNegotiatorSource;

        private Message internalMessage = null;
        private object mSelectedItem;
        private bool mIsExportInProgress;

        /// <summary>
        /// To Clear current Negotiation and Current Conversatuion After save
        /// </summary>
        private bool mClearCurrentSelectedItems = false;
        private bool mIsBusy;

        private bool mIsMessageHasMultiReceivers = false;
        private bool mIsMessageMultiReceiversEmpty = true;
        private bool mIsAddingConversation = false;

        private List<Organization> mUserOrganizations;

        #endregion fields

        #region → Properties     .


        /// <summary>
        /// Gets a value indicating whether this instance is publish neg item visible.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is publish neg item visible; otherwise, <c>false</c>.
        /// </value>
        public bool IsPublishNegItemVisible
        {
            get
            {
                return CurrentNegotiation != null && !AppConfigurations.IsAddon;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is mail channel used.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is mail channel used; otherwise, <c>false</c>.
        /// </value>
        public bool IsMailChannelUsed
        {
            get
            {
                return (SelectedChannel == null ||
                    (SelectedChannel as Channel).ChannelID == eNegConstant.Channel.Email) ? true : false;
            }
        }

        /// <summary>
        /// Gets a value indicating whether [show add message menu].
        /// </summary>
        /// <value><c>true</c> if [show add message menu]; otherwise, <c>false</c>.</value>
        public bool ShowAddMessageMenu
        {
            get
            {
                return this.SelectedItem != null &&
                    this.SelectedItem.GetType().Equals(typeof(Conversation)) &&
                    !AppConfigurations.IsAddon;
            }
        }

        /// <summary>
        /// Gets a value indicating whether [show published menu].
        /// </summary>
        /// <value><c>true</c> if [show published menu]; otherwise, <c>false</c>.</value>
        public bool ShowPublishedMenu
        {
            get
            {
                return this.SelectedItem != null &&
                    this.SelectedItem.GetType().Equals(typeof(Negotiation)) &&
                    !AppConfigurations.IsAddon;
            }
        }


        /// <summary>
        /// Gets or sets a value indicating whether this instance is export in progress.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is export in progress; otherwise, <c>false</c>.
        /// </value>
        public bool IsExportInProgress
        {
            get
            {
                return mIsExportInProgress;
            }
            set
            {
                mIsExportInProgress = value;
                RaisePropertyChanged("IsExportInProgress");
            }
        }

        /// <summary>
        /// Gets or sets the PDF array of bytes.
        /// </summary>
        /// <value>The PDF array of bytes.</value>
        public byte[] PDFArrayOfBytes { get; set; }

        /// <summary>
        /// Gets or sets the closed negotiation archive source.
        /// </summary>
        /// <value>The closed negotiation archive source.</value>
        public List<IArchiveItem> ClosedNegotiationArchiveSource
        {
            get
            {
                return mClosedNegotiationArchiveSource;
            }
            set
            {
                mClosedNegotiationArchiveSource = value;

                this.RaisePropertyChanged("ClosedNegotiationArchiveSource");
            }
        }

        /// <summary>
        /// Gets or sets the message VM.
        /// </summary>
        /// <value>The message VM.</value>
        public MessageViewModel MessageVM { get; set; }

        /// <summary>
        /// Gets or sets the conversation VM.
        /// </summary>
        /// <value>The conversation VM.</value>
        public ConversationViewModel ConversationVM { get; set; }

        /// <summary>
        /// Gets or sets the selected channel.
        /// </summary>
        /// <value>The selected channel.</value>
        public object SelectedChannel
        {
            get
            {
                return mSelectedChannel;
            }
            set
            {
                if (mSelectedChannel != null &&
                    value != null &&
                    CurrentMessage != null &&
                    mSelectedChannel != value &&
                    (((Channel)mSelectedChannel).ChannelID == eNegConstant.Channel.Email ||
                    ((Channel)value).ChannelID == eNegConstant.Channel.Email))
                {
                    if (CurrentMessage != null)
                    {
                        CurrentMessage.MessageReceiver = string.Empty;
                        MultiNegotiatorSource.Clear();
                        mMultiNegotiatorSource.Add(this.DefaultNegotiator);
                        RefreshReceiversCount();
                    }
                }

                mSelectedChannel = value;
                RaisePropertyChanged("SelectedChannel");
                RaisePropertyChanged("IsMailChannelUsed");
            }
        }

        /// <summary>
        /// Gets or sets the selected item.
        /// in the closed or open tree
        /// </summary>
        /// <value>The selected item.</value>
        public object SelectedItem
        {
            get
            {
                return mSelectedItem;
            }

            set
            {
                mSelectedItem = value;

                this.RaisePropertyChanged("ShowAddMessageMenu");
                this.RaisePropertyChanged("ShowPublishedMenu");


                this.SetSelectedItem(value);

                this.RaisePropertyChanged("SelectedItem");
            }
        }

        /// <summary>
        /// Gets the default negotiator.
        /// </summary>
        /// <value>The default negotiator.</value>
        public MultiNegotiator DefaultNegotiator
        {
            get
            {
                IEnumerable<string> TempNegotiators = null;
                if (this.CurrentConversation != null)
                    TempNegotiators = this.CurrentConversation.Negotiation.Negotiators;
                return new MultiNegotiator()
                {
                    OldNegotiators = TempNegotiators,
                    NewNegotiator = string.Empty
                };
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is messsage submit.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is messsage submit; otherwise, <c>false</c>.
        /// </value>
        public bool IsMesssageSubmit { get; set; }

        /// <summary>
        /// Gets or sets the Application
        /// </summary>
        /// <value>The DM application.</value>
        public IEnumerable<UserApplicationStatu> DMApplication
        {
            get { return mDMApplication; }
            set
            {
                if (mDMApplication != value)
                {
                    mDMApplication = value;
                    this.RaisePropertyChanged("DMApplication");
                }
            }
        }

        /// <summary>
        /// Gets or sets the negotiation model.
        /// </summary>
        /// <value>The negotiation model.</value>
        public INegotiationModel NegotiationModel
        {
            get { return mNegotiationModel; }
            set { mNegotiationModel = value; }
        }

        /// <summary>
        /// Gets or sets the last added negotiation ID.
        /// </summary>
        /// <value>The last added negotiation ID.</value>
        public Guid? LastAddedNegotiationID { get; set; }

        /// <summary>
        /// Gets or sets the last added converstaion ID.
        /// </summary>
        /// <value>The last added converstaion ID.</value>
        public Guid? LastAddedConverstaionID { get; set; }

        /// <summary>
        /// Gets or sets Enable Message TextBox
        /// </summary>
        /// <value>
        /// 	<c>true</c> if [enable message text box]; otherwise, <c>false</c>.
        /// </value>
        public bool EnableMessageTextBox
        {
            get { return mEnableMessageTextBox; }
            set
            {
                mEnableMessageTextBox = value;
                this.RaisePropertyChanged("EnableMessageTextBox");
            }
        }

        /// <summary>
        /// Used To Indicate that the Model Is Busy Loading Data Or Submitting Data
        /// </summary>
        /// <value><c>true</c> if this instance is busy; otherwise, <c>false</c>.</value>
        public bool IsBusy
        {
            get
            {
                return mIsBusy || this.mNegotiationModel.IsBusy;
            }
            set
            {
                mIsBusy = value;
                this.RaisePropertyChanged("IsBusy");
            }
        }

        /// <summary>
        /// Gets or sets the current negotation
        /// </summary>
        /// <value>The current negotiation.</value>
        public Negotiation CurrentNegotiation
        {
            get { return mcurrentNegotiation; }
            set
            {
                if (mcurrentNegotiation != value)
                {
                    mcurrentNegotiation = value;

                    this.ConversationVM.CurrentNegotiation = value;
                    this.ConversationVM.RefreshConversationsSource();

                    this.RaisePropertyChanged("CurrentNegotiation");
                    this.RaisePropertyChanged("IsPublishNegItemVisible");
                    SetNegotiationAppStatus();
                }
            }
        }

        /// <summary>
        /// Gets or sets the current conversation
        /// </summary>
        /// <value>The current conversation.</value>
        public Conversation CurrentConversation
        {
            get { return mcurrentConversation; }
            set
            {
                if (mcurrentConversation != value)
                {
                    mcurrentConversation = value;

                    this.MessageVM.CurrentConversation = value;

                    this.RaisePropertyChanged("CurrentConversation");

                    EnableMessageTextBox = (mcurrentConversation != null && mcurrentConversation.Negotiation != null && mcurrentConversation.Negotiation.StatusID == eNegConstant.NegotiationStatus.Ongoing);

                    if (mcurrentConversation != null)
                    {
                        BuildSubjectListForConversation();

                        if (mcurrentConversation.Negotiation != null)
                        {
                            mcurrentConversation.Negotiation.BuildNegotiators();

                            #region → For Show App Setting In case of Conversation  .

                            if (!mIsAddingConversation)
                            {
                                // this to raise chanes in Reports (Apps)
                                mcurrentConversation.Negotiation.ActiveConversation = mcurrentConversation;
                            }

                            SetNegotiationAppStatus();
                            #endregion
                        }
                    }

                    #region when conversation is changed change message

                    AddNewMessage();
                    mMultiNegotiatorSource = null;

                    #endregion
                }

                if (mcurrentNegotiation != null && mcurrentConversation == null)
                {
                    mcurrentNegotiation.ActiveConversation = null;
                }

            }
        }

        /// <summary>
        /// Gets or sets the Current message
        /// </summary>
        /// <value>The current message.</value>
        public Message CurrentMessage
        {
            get { return mCurrentMessage; }
            set
            {
                if (mCurrentMessage != value)
                {
                    mCurrentMessage = value;
                    this.RaisePropertyChanged("CurrentMessage");
                }
            }
        }

        /// <summary>
        /// Gets the is any negotiation selected.
        /// </summary>
        /// <value>The is any negotiation selected.</value>
        private bool IsAnyNegotiationSelected
        {
            get
            {
                bool Result = (this.mOnGoingNegotiation != null &&
                            this.mOnGoingNegotiation.Count(s => s.IsNegSelected == true) > 0) ||

                            (this.mClosedNegotiation != null &&
                            this.mClosedNegotiation.Count(s => s.IsNegSelected == true) > 0);

                return Result;
            }
        }

        /// <summary>
        /// Observable collection of Ongoing Negs
        /// </summary>
        /// <value>The on going negotiation source.</value>
        public ObservableCollection<Negotiation> OnGoingNegotiationSource { get; set; }

        /// <summary>
        /// Observable collection of Closed Negs
        /// </summary>
        /// <value>The closed negotiation source.</value>
        public ObservableCollection<Negotiation> ClosedNegotiationSource { get; set; }

        /// <summary>
        /// Gets all negotiations.
        /// </summary>
        /// <value>All negotiations.</value>
        public IEnumerable<Negotiation> AllNegotiations
        {
            get
            {
                return mNegotiationModel.AllNegotiation;
            }
        }

        /// <summary>
        /// Observable collection of Negotiation Application Status
        /// </summary>
        public ObservableCollection<NegotiationApplicationStatu> NegotiationApplicationStatusSource { get; private set; }

        /// <summary>
        /// Gets or sets the channels of a conversation
        /// </summary>
        /// <value>The channels.</value>
        public IEnumerable<Channel> Channels
        {
            get { return mChannels; }
            private set
            {
                if (value != mChannels)
                {
                    mChannels = value;
                    this.RaisePropertyChanged("Channels");
                }
            }
        }

        /// <summary>
        /// Gets or sets the message subjects.
        /// </summary>
        /// <value>The message subjects.</value>
        public List<string> OldMessageSubjects
        {
            get { return mOldMessageSubjects; }
            private set
            {
                if (value != mOldMessageSubjects)
                {
                    mOldMessageSubjects = value;
                    this.RaisePropertyChanged("OldMessageSubjects");
                }
            }
        }

        /// <summary>
        /// Gets or sets the Negotiation Application Status
        /// </summary>
        /// <value>The negotiation application status.</value>
        public IEnumerable<NegotiationApplicationStatu> NegotiationApplicationStatus
        {
            get { return mNegotiationApplicationStatus; }
            set
            {
                if (mNegotiationApplicationStatus != value)
                {
                    mNegotiationApplicationStatus = value;
                    NegotiationApplicationStatusSource = new ObservableCollection<NegotiationApplicationStatu>(mNegotiationApplicationStatus.OrderBy(s => s.Application.ApplicationTitle));
                    this.RaisePropertyChanged("NegotiationApplicationStatusSource");
                    this.RaisePropertyChanged("NegotiationApplicationStatus");
                }
            }
        }

        /// <summary>
        /// Gets or sets the Application
        /// </summary>
        /// <value>The application entries.</value>
        public IEnumerable<citPOINT.eNeg.Data.Web.Application> ApplicationEntries
        {
            get { return mApplicationEntries; }
            set
            {
                if (mApplicationEntries != value)
                {
                    mApplicationEntries = value;
                    this.RaisePropertyChanged("ApplicationEntries");
                }
            }
        }

        /// <summary>
        /// Gets a value indicating whether this instance is message has multi receiver.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is message has multi receiver; otherwise, <c>false</c>.
        /// </value>
        public bool IsMessageHasMultiReceivers
        {
            get
            {
                return mIsMessageHasMultiReceivers;
            }
            set
            {
                mIsMessageHasMultiReceivers = value;
                RaisePropertyChanged("IsMessageHasMultiReceivers");
            }
        }

        /// <summary>
        /// Gets a value indicating whether this instance is message multi receivers empty.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is message multi receivers empty; otherwise, <c>false</c>.
        /// </value>
        public bool IsMessageMultiReceiversEmpty
        {
            get
            {
                return mIsMessageMultiReceiversEmpty;
            }
            set
            {
                mIsMessageMultiReceiversEmpty = value;
                RaisePropertyChanged("IsMessageMultiReceiversEmpty");
            }
        }

        /// <summary>
        /// Gets or sets the multi negotiator source.
        /// </summary>
        /// <value>The multi negotiator source.</value>
        public List<MultiNegotiator> MultiNegotiatorSource
        {
            get
            {
                if (mMultiNegotiatorSource == null)
                {
                    mMultiNegotiatorSource = new List<MultiNegotiator>();
                    IsMessageHasMultiReceivers = false;
                    IsMessageMultiReceiversEmpty = true;
                    if (!string.IsNullOrEmpty(CurrentMessage.MessageReceiver))
                    {
                        mMultiNegotiatorSource.Add(new MultiNegotiator()
                            {
                                OldNegotiators = CurrentConversation.Negotiation.Negotiators,
                                NewNegotiator = CurrentMessage.MessageReceiver
                            });
                    }
                    mMultiNegotiatorSource.Add(this.DefaultNegotiator);
                }
                return mMultiNegotiatorSource;
            }
            set
            {
                mMultiNegotiatorSource = value;
                this.RaisePropertyChanged("MultiNegotiatorSource");
            }
        }

        /// <summary>
        /// Gets or sets the user organizations.
        /// </summary>
        /// <value>The user organizations.</value>
        public List<Organization> UserOrganizations
        {
            get
            {
                return mUserOrganizations;
            }
            set
            {
                mUserOrganizations = value;
                this.RaisePropertyChanged("UserOrganizations");
            }
        }

        #endregion Properties

        #region → Constructors   .

        /// <summary>
        /// NegotiationViewModel
        /// </summary>
        /// <param name="mNegotiationModel">INegotiationModel</param>
        [ImportingConstructor]
        public NegotiationViewModel(INegotiationModel mNegotiationModel)
        {
            this.mNegotiationModel = mNegotiationModel;
            this.MessageVM = new MessageViewModel();
            this.ConversationVM = new ConversationViewModel();

            if (mNegotiationModel.HasChanges)
                mNegotiationModel.RejectChanges();

            #region → Set up event handling           .

            this.mNegotiationModel.GeneratePDFComplete += new Action<InvokeOperation<byte[]>>(mNegotiationModel_GeneratePDFComplete);
            this.mNegotiationModel.GetClosedNegotiationArchiveComplete += new EventHandler<eNegEntityResultArgs<NegotiationArchive>>(mNegotiationModel_GetClosedNegotiationArchiveComplete);
            this.mNegotiationModel.GetClosedNegotiationForArchiveComplete += new EventHandler<eNegEntityResultArgs<Negotiation>>(mNegotiationModel_GetClosedNegotiationForArchiveComplete);
            this.mNegotiationModel.GetOngingNegotiationComplete += new EventHandler<eNegEntityResultArgs<Negotiation>>(mNegotiationModel_GetOngingNegotiationComplete);
            this.mNegotiationModel.GetConversationByNegotiationIDComplete += new EventHandler<eNegEntityResultArgs<Conversation>>(mNegotiationModel_GetConversationByNegotiationIDComplete);
            this.mNegotiationModel.GetMessagesByConversationIDsComplete += new EventHandler<eNegEntityResultArgs<Message>>(mNegotiationModel_GetMessagesByConversationIDsComplete);

            this.mNegotiationModel.SaveChangesComplete += new EventHandler<SubmitOperationEventArgs>(mNegotiationModel_SaveChangesComplete);
            this.mNegotiationModel.GetAppsActiveForDMComplete += new EventHandler<eNegEntityResultArgs<UserApplicationStatu>>(mNegotiationModel_GetAppsActiveForDMComplete);
            this.mNegotiationModel.CheckOnNegotiationRepeatComplete += new Action<InvokeOperation<int?>>(mnegotiationModel_CheckOnNegotiationRepeatComplete);
            this.mNegotiationModel.GetChannelComplete += new EventHandler<eNegEntityResultArgs<Channel>>(mNegotiationModel_GetChannelComplete);
            this.mNegotiationModel.GetNegotiationApplicationStatusComplete += new EventHandler<eNegEntityResultArgs<NegotiationApplicationStatu>>(mNegotiationModel_GetNegotiationApplicationStatusComplete);
            this.mNegotiationModel.GetApplicationComplete += new EventHandler<eNegEntityResultArgs<Data.Web.Application>>(mNegotiationModel_GetApplicationComplete);
            this.mNegotiationModel.GetNegotiationPageNumberComplete += new Action<InvokeOperation<int?>>(mnegotiationModel_GetNegotiationPageNumberComplete);
            this.mNegotiationModel.PropertyChanged += new PropertyChangedEventHandler(mNegotiationModel_PropertyChanged);
            this.mNegotiationModel.GetNegotiationOrgaizationComplete += new EventHandler<eNegEntityResultArgs<NegotiationOrganization>>(mNegotiationModel_GetNegotiationOrgaizationComplete);
            this.mNegotiationModel.GetOrganizationsForUserComplete += new EventHandler<eNegEntityResultArgs<Organization>>(mNegotiationModel_GetOrganizationsForUserComplete);

            #region Special Event handling To Add New Node In case of addon after loaing all data

            this.mNegotiationModel.GetClosedNegotiationArchiveComplete += new EventHandler<eNegEntityResultArgs<NegotiationArchive>>(AddNewNegotiationNodeForAddon);
            this.mNegotiationModel.GetClosedNegotiationForArchiveComplete += new EventHandler<eNegEntityResultArgs<Negotiation>>(AddNewNegotiationNodeForAddon);
            this.mNegotiationModel.GetOngingNegotiationComplete += new EventHandler<eNegEntityResultArgs<Negotiation>>(AddNewNegotiationNodeForAddon);
            this.mNegotiationModel.GetConversationByNegotiationIDComplete += new EventHandler<eNegEntityResultArgs<Conversation>>(AddNewNegotiationNodeForAddon);
            this.mNegotiationModel.GetMessagesByConversationIDsComplete += new EventHandler<eNegEntityResultArgs<Message>>(AddNewNegotiationNodeForAddon);
            this.mNegotiationModel.GetChannelComplete += new EventHandler<eNegEntityResultArgs<Channel>>(AddNewNegotiationNodeForAddon);
            this.mNegotiationModel.GetNegotiationApplicationStatusComplete += new EventHandler<eNegEntityResultArgs<NegotiationApplicationStatu>>(AddNewNegotiationNodeForAddon);
            this.mNegotiationModel.GetNegotiationOrgaizationComplete += new EventHandler<eNegEntityResultArgs<NegotiationOrganization>>(AddNewNegotiationNodeForAddon);
            this.mNegotiationModel.GetOrganizationsForUserComplete += new EventHandler<eNegEntityResultArgs<Organization>>(AddNewNegotiationNodeForAddon);
            this.mNegotiationModel.GetApplicationComplete += new EventHandler<eNegEntityResultArgs<Data.Web.Application>>(AddNewNegotiationNodeForAddon);
            this.mNegotiationModel.PropertyChanged += new PropertyChangedEventHandler(AddNewNegotiationNodeForAddon);

            #endregion Special Event handling To Add New Node In case of addon after loaing all data

            #endregion

            #region → Intializing Data Source         .

            this.mClosedNegotiation = new List<Negotiation>();
            this.mOnGoingNegotiation = new List<Negotiation>();
            this.UserOrganizations = new List<Organization>();
            this.ClosedNegotiationArchiveSource = new List<IArchiveItem>();

            RefreshData(true);
            RefreshData(false);

            #endregion

            #region → Loading Related Tables          .
            GetOrganizationsForUserAsync();
            GetChannelsAsync();
            GetApplicationAsync();

            #region → Getting Negotiations According to Needs

            //if (AppConfigurations.NegotiationIDParameter == null)
            //{
            this.GetOngingNegotiationsAsync();
            this.GetClosedNegotiationsArchiveAsync();
            // }
            //else
            //{
            //    //Hack:
            //    //if (AppConfigurations.IsStatusParamterIsOpen)
            //    //{
            //    //    //in case of user want certain negotiation so firstly we get the propitiate page number
            //    //    this.mNegotiationModel.GetNegotiationPageNumberAsync(AppConfigurations.NegotiationIDParameter.Value, eNegConstant.NegotiationStatus.Ongoing, AppConfigurations.CurrentLoginUser.UserID, 5);
            //    //    GetClosedNegotiationByPageNumberAsync(1);
            //    //}
            //    //else
            //    //{
            //    //    //in case of user want certain negotiation so firstly we get the propitiate page number
            //    //    this.mNegotiationModel.GetNegotiationPageNumberAsync(AppConfigurations.NegotiationIDParameter.Value, eNegConstant.NegotiationStatus.Closed, AppConfigurations.CurrentLoginUser.UserID, 5);
            //    //    GetOnGoingNegotiationByPageNumberAsync(1);
            //    //}
            //}

            #endregion


            #endregion

            #region → Register messages               .

            eNegMessanger.EditUserOrganizationMessage.Register(this, OnEditUserOrganizationMessage);

            // register for SubmitChangesMessage
            eNegMessanger.SubmitChangesMessage.Register(this, OnSubmitChangesMessage);
            // register for CancelChangesMessage
            eNegMessanger.CancelChangesMessage.Register(this, OnCancelChangesMessage);

            eNegMessanger.EditNegotiationMessage.Register(this, OnEditNegotiationMessage);

            eNegMessanger.EditConversationMessage.Register(this, OnEditConversationMessage);

            eNegMessanger.EditCertainMessage.Register(this, OnEditCertainMessage);

            eNegMessanger.DoOperationMessage.Register(this, OnLoadCompleted);

            eNegMessanger.FlippMessage.Register(this, OnFlipCompleted);

            eNegMessanger.LoadingQueueMessage.Register(this, OnLoadingQueue);

            eNegMessanger.ChangeScreenMessage.Register(this, OnChangeScreen);
            #endregion
        }

        #endregion Constructor

        #region → Events         .

        /// <summary>
        /// Occurs when [after save completed].
        /// </summary>
        public event EventHandler DataSaved;

        /// <summary>
        /// Occurs when [on deleting].
        /// </summary>
        public event EventHandler OnDeleting;

        #endregion Events

        #region → Event Handlers .

        /// <summary>
        /// Ms the negotiation model_ generate PDF complete.
        /// </summary>
        /// <param name="obj">The obj.</param>
        private void mNegotiationModel_GeneratePDFComplete(InvokeOperation<byte[]> obj)
        {
            if (!obj.HasError)
            {
                if (obj.Value != null)
                {
                    PDFArrayOfBytes = obj.Value;
                    IsExportInProgress = false;
                    //eNegMessanger.NewPopUp.Send("Save", eNegMessanger.PopUpType.SavePDF);
                }
            }
            else
            {
                // notify user if there is any error
                eNegMessanger.RaiseErrorMessage.Send(obj.Error);
            }
        }

        /// <summary>
        /// Call back of Get messages by conversation Ids.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        private void mNegotiationModel_GetMessagesByConversationIDsComplete(object sender, eNegEntityResultArgs<Message> e)
        {
            if (!e.HasError)
            {

                MessageVM.RefreshMessagesSource();

                BuildSubjectListForConversation();

                if (mcurrentConversation != null && mcurrentConversation.Negotiation != null)
                {
                    mcurrentConversation.Negotiation.BuildNegotiators();
                }

                this.AddNewMessage();

                //#region Refresh Source Object
                //RefreshData(true, true);
                //RefreshData(false, true);
                //#endregion
            }
            else
            {
                // notify user if there is any error
                eNegMessanger.RaiseErrorMessage.Send(e.Error);
            }
        }

        /// <summary>
        /// Cal back of Get conversation by negotiation ID.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        private void mNegotiationModel_GetConversationByNegotiationIDComplete(object sender, eNegEntityResultArgs<Conversation> e)
        {
            if (!e.HasError)
            {
                #region Refresh Source Object
                RefreshData(true, true);
                RefreshData(false, true);
                #endregion

                this.ConversationVM.RefreshConversationsSource();
                eNegMessanger.DoOperationMessage.Send(eNegMessanger.OperationType.ClosedNegotiationArchiveLoaded);
            }
            else
            {
                // notify user if there is any error
                eNegMessanger.RaiseErrorMessage.Send(e.Error);
            }
        }

        /// <summary>
        ///  handles the complete event of GetNegotiationApplicationStatus
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">eNegEntityResultArgs e</param>
        private void mNegotiationModel_GetNegotiationApplicationStatusComplete(object sender, eNegEntityResultArgs<NegotiationApplicationStatu> e)
        {
            if (!e.HasError)
            {

                #region Refresh Source Object

                RefreshData(true);
                RefreshData(false);


                #endregion
            }
            else
            {
                // notify user if there is any error
                eNegMessanger.RaiseErrorMessage.Send(e.Error);
            }
        }

        /// <summary>
        /// Call back of Get onging negotiation.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        private void mNegotiationModel_GetOngingNegotiationComplete(object sender, eNegEntityResultArgs<Negotiation> e)
        {
            if (!e.HasError)
            {
                #region → Set and Refresh Source Object .

                this.mOnGoingNegotiation = e.Results.AsEnumerable<Negotiation>().ToList();

                RefreshData(true);

                this.CurrentNegotiation = null;

                this.CurrentConversation = null;

                this.SetNegotiationAppStatus();

                #endregion

                #region → Loading Related               .

                Guid[] negotiationIDs = this.mOnGoingNegotiation.Select(s => s.NegotiationID).ToArray();

                //Getting Application settings
                GetNegotiationApplicationStatusAsync(negotiationIDs);
                //Getting Conversation
                GetConversationByNegotiationIDAsync(negotiationIDs);

                ////Getting Messages
                //mNegotiationModel.GetMessageByNegotiationsIDAsync(negotiationIDs));

                GetNegotiationOrganizationAsync(negotiationIDs);
                #endregion

            }
            else
            {
                // notify user if there is any error
                eNegMessanger.RaiseErrorMessage.Send(e.Error);
            }
        }

        /// <summary>
        /// Call back of  Get closed negotiation for archive.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        private void mNegotiationModel_GetClosedNegotiationForArchiveComplete(object sender, eNegEntityResultArgs<Negotiation> e)
        {
            if (!e.HasError)
            {
                try
                {
                    #region → Set and Refresh Source Object          .

                    this.mClosedNegotiation = e.Results.AsEnumerable<Negotiation>().ToList();
                    RefreshData(false);

                    this.CurrentNegotiation = null;
                    this.CurrentConversation = null;

                    #endregion

                    #region → Loading Related (Conversation-Messages .

                    List<Guid> NegotiationIDs = new List<Guid>();

                    foreach (var negotiationItem in this.mClosedNegotiation)
                    {
                        NegotiationIDs.Add(negotiationItem.NegotiationID);

                        IArchiveItem archItem =
                                  this.ClosedNegotiationArchiveSource
                                      .Where(s => s.ArchiveItemType == ArchiveItemTypes.Year &&
                                                 s.Value.ToString() == negotiationItem.CreatedDate.Year.ToString())


                                     .FirstOrDefault()
                                     .Children.Where(d => d.ArchiveItemType == ArchiveItemTypes.Month &&
                                                          d.Value.ToString() == negotiationItem.CreatedDate.Month.ToString())
                                     .FirstOrDefault();


                        if (archItem != null)
                        {
                            negotiationItem.Parent = archItem;
                            (archItem.Children as List<IArchiveItem>).Add(negotiationItem);
                        }
                    }

                    #endregion

                    #region → Get negotiation Details                .

                    //Getting Application settings
                    GetNegotiationApplicationStatusAsync(NegotiationIDs.ToArray<Guid>());

                    //Getting Conversation
                    GetConversationByNegotiationIDAsync(NegotiationIDs.ToArray<Guid>());

                    ////Getting Messages
                    //mNegotiationModel.GetMessageByNegotiationsIDAsync(NegotiationIDs.ToArray<Guid>());

                    GetNegotiationOrganizationAsync(NegotiationIDs.ToArray<Guid>());

                    #endregion

                }
                catch (Exception ex)
                {
                    // notify user if there is any error
                    eNegMessanger.RaiseErrorMessage.Send(ex);
                }
            }
            else
            {
                // notify user if there is any error
                eNegMessanger.RaiseErrorMessage.Send(e.Error);
            }
        }

        /// <summary>
        /// Call back of Get closed negotiation archive.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        private void mNegotiationModel_GetClosedNegotiationArchiveComplete(object sender, eNegEntityResultArgs<NegotiationArchive> e)
        {
            if (!e.HasError)
            {
                try
                {
                    List<IArchiveItem> negArchiveSource = new List<IArchiveItem>();

                    ArchiveItem lastArchvYearItem = null;

                    foreach (var archvItem in e.Results)
                    {
                        #region → Add Year Item  .

                        if (lastArchvYearItem == null ||
                            lastArchvYearItem.Value.ToString() != archvItem.ArchiveYear.ToString())
                        {
                            lastArchvYearItem = new ArchiveItem()
                            {
                                ArchiveItemType = ArchiveItemTypes.Year,
                                Value = archvItem.ArchiveYear,
                                Name = archvItem.ArchiveYear.ToString(),
                            };

                            lastArchvYearItem.Children = (new List<IArchiveItem>());

                            negArchiveSource.Add(lastArchvYearItem);
                        }

                        #endregion

                        #region → Add Month Item .

                        (lastArchvYearItem.Children as List<IArchiveItem>).Add(new ArchiveItem()
                        {
                            ArchiveItemType = ArchiveItemTypes.Month,
                            IsLoadedOnDemand = true,
                            Parent = lastArchvYearItem,
                            Value = archvItem.ArchiveMonth,
                            Name = GetMothName(archvItem.ArchiveMonth.Value),
                        });

                        #endregion
                    }

                    this.ClosedNegotiationArchiveSource = negArchiveSource;
                }
                catch (Exception ex)
                {
                    // notify user if there is any error
                    eNegMessanger.RaiseErrorMessage.Send(ex);
                }
            }
            else
            {
                // notify user if there is any error
                eNegMessanger.RaiseErrorMessage.Send(e.Error);
            }
        }

        /// <summary>
        /// Call back of Get organizations for user.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        private void mNegotiationModel_GetOrganizationsForUserComplete(object sender, eNegEntityResultArgs<Organization> e)
        {
            if (!e.HasError)
            {
                this.UserOrganizations = e.Results.OrderBy(s => s.OrganizationName).ToList();
            }
            else
            {
                eNegMessanger.RaiseErrorMessage.Send(e.Error);
            }
        }

        /// <summary>
        /// Call back of Get negotiation orgaization.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        private void mNegotiationModel_GetNegotiationOrgaizationComplete(object sender, eNegEntityResultArgs<NegotiationOrganization> e)
        {
            if (e.HasError)
            {
                eNegMessanger.RaiseErrorMessage.Send(e.Error);
            }
        }

        /// <summary>
        /// Ms the negotiation model_ get apps active for DM complete.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        private void mNegotiationModel_GetAppsActiveForDMComplete(object sender, eNegEntityResultArgs<UserApplicationStatu> e)
        {
            if (!e.HasError)
            {
                DMApplication = e.Results.Where(s => s.IsDMActive == true);
                RaisePropertyChanged("DMApplication");
            }
            else
            {
                eNegMessanger.RaiseErrorMessage.Send(e.Error);
            }
        }

        /// <summary>
        /// Mnegotiations the model_ check on negotiation repeat complete.
        /// </summary>
        /// <param name="obj">The obj.</param>
        private void mnegotiationModel_CheckOnNegotiationRepeatComplete(InvokeOperation<int?> obj)
        {
            if (!obj.HasError)
            {
                this.CurrentNegotiation.ValidationErrors.Clear();

                if (obj.Value != null && obj.Value.Value == 0)
                {
                    this.isLastActionPopup = false;

                    #region → Complete Adding Negotiations .

                    if (this.CurrentNegotiation != null && this.CurrentNegotiation.EntityState == EntityState.New)
                    {
                        this.mOnGoingNegotiation.Add(this.CurrentNegotiation);

                        sortNegotiations = true;
                    }

                    #endregion

                    SelectedItem = CurrentNegotiation;

                    eNegMessanger.SubmitChangesMessage.Send();

                    eNegMessanger.FlippMessage.Send(ViewTypes.ClosePopupView);
                }
                else
                {
                    this.CurrentNegotiation.ValidationErrors.Add(new ValidationResult(Resources.NegotiationRepeat, new string[] { "NegotiationName" }));
                }
            }
            else
            {
                eNegMessanger.RaiseErrorMessage.Send(obj.Error);
            }
        }

        /// <summary>
        /// Get Data of (Applications like PrefApp)
        /// </summary>
        /// <param name="sender">value of Sender</param>
        /// <param name="e">eNegEntityResultArgs</param>
        private void mNegotiationModel_GetApplicationComplete(object sender, eNegEntityResultArgs<Data.Web.Application> e)
        {
            if (!e.HasError)
            {
                ApplicationEntries = e.Results.OrderBy(g => g.ApplicationTitle);
                this.RaisePropertyChanged("ApplicationEntries");

                #region "Build Default Application In case one not choose any Negotiation"

                mNegotiationApplicationStatusDefault = new List<NegotiationApplicationStatu>();
                foreach (var item in ApplicationEntries)
                {
                    mNegotiationApplicationStatusDefault.Add(new NegotiationApplicationStatu()
                    {
                        Active = false,
                        Application = item,
                        NegotiationApplicationStatusID = Guid.NewGuid(),
                        ApplicationID = item.ApplicationID
                    });
                }

                SetNegotiationAppStatus();

                #endregion "Build Default Apllication In case one not choose any Negotiation"

                mNegotiationModel.GetAppsActiveForDM();
            }
            else
            {
                // notify user if there is any error
                eNegMessanger.RaiseErrorMessage.Send(e.Error);
            }
        }

        /// <summary>
        /// SaveChangesComplete event handler
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">SubmitOperationEventArgs</param>
        private void mNegotiationModel_SaveChangesComplete(object sender, SubmitOperationEventArgs e)
        {
            if (!e.HasError)
            {
                //Sorting new sets of negotiations after rename.
                if (sortNegotiations)
                {
                    var TempCurrentNeg = SelectedItem;

                    RefreshData(true);
                    sortNegotiations = false;

                    SelectedItem = TempCurrentNeg;

                }

                if (sortConversation)
                {
                    sortConversation = false;
                    if (CurrentConversation != null)
                        CurrentConversation.Negotiation.RefreshConversations();
                }

                if (mIsAddingConversation && this.CurrentConversation != null)
                {
                    //// this to raise chanes in Reports (Apps)
                    this.CurrentConversation.Negotiation.ActiveConversation = CurrentConversation;
                    mIsAddingConversation = false;
                }

                #region → If this submit was for Message insertion, then we will detect the Apps that will load for Data Matching

                if (IsMesssageSubmit)
                {
                    #region → Add Last Added Message Subject in OldMessagesSubjects DataSource            .
                    if (CurrentMessage != null)
                    {
                        if (!OldMessageSubjects.Contains(CurrentMessage.MessageSubject))
                        {
                            OldMessageSubjects.Add(CurrentMessage.MessageSubject);
                            OldMessageSubjects.Sort();
                            OldMessageSubjects = new List<string>(OldMessageSubjects);
                        }

                        //to Referesh the Message Source for messages Tab.
                        this.MessageVM.RefreshMessagesSource();
                    }
                    #endregion

                    #region → Filter Apps that will make Data Matching Automaticlly after message save    .
                    if (DMApplication.Count() > 0)
                    {
                        if (CurrentMessage != null)
                        {
                            Guid NegID = CurrentMessage.Conversation.Negotiation.NegotiationID;
                            Guid ConvID = CurrentMessage.Conversation.ConversationID;
                            Guid MsgID = CurrentMessage.MessageID;

                            IEnumerable<UserApplicationStatu> AppsActiveAndDM = DMApplication.Where
                                (s => s.Application.NegotiationApplicationStatus.Where
                                    (o => o.Active == true && o.NegotiationID == NegID).FirstOrDefault() != null);


                            foreach (var App in AppsActiveAndDM)
                            {
                                string url = ApplicationEntries.Where(s => s.ApplicationID == App.ApplicationID).First().ApplicationBaseAddress
                                    + "?NegID=" + NegID
                                    + "&ConvID=" + ConvID
                                    + "&MsgID=" + MsgID +
                                    "&ActionType=" + eNegConstant.ActionParameterType.DataMatching;

                                //Send Message to open PopUp window with App Data Matching
                                if (AppConfigurations.IsRunningOutOfBrowser)
                                {
                                    eNegMessanger.FlippMessage.Send(url);
                                }
                                // Open Browser loaded with App Data Matching
                                else
                                {
                                    eNegNavigation.NavigateToUrl(url, true);
                                }

                            }
                        }
                    }
                    #endregion

                    eNegMessage message = new eNegMessage(Resources.SaveMessageSucess);
                    eNegMessanger.SendCustomMessage.Send(message);
                }
                #endregion


                #region To Clear Current Negotiation and Current Conversatuion After save

                if (this.mClearCurrentSelectedItems)
                {
                    this.CurrentNegotiation = null;
                    this.CurrentConversation = null;
                    this.mClearCurrentSelectedItems = false;
                }

                #endregion To Clear Current Negotiation and Current Conversation After save

                //Notify the View That the Delete Operation Begin
                //To Save theResult Expanded Negotiations.
                PerformEvent(DataSaved);

                RaiseCanExecuteChanged();

                AddNewMessage();

                SetNegotiationAppStatus();

                BuildNegotiators(this.mOnGoingNegotiation);
                BuildNegotiators(this.mClosedNegotiation);

                //incase of close negotiation or publish it.
                if (LoadQueue != null)
                {
                    eNegMessanger.LoadingQueueMessage.Send(LoadQueue);

                    LoadQueue = null;
                }

            }
            else
            {
                // notify user if there is any error
                eNegMessanger.RaiseErrorMessage.Send(e.Error);
            }
        }

        /// <summary>
        /// PropertyChanged event handler
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">PropertyChangedEventArgs</param>
        private void mNegotiationModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals("HasChanges") || e.PropertyName.Equals("IsBusy"))
            {
                this.MessageVM.IsBusy = this.mNegotiationModel.IsBusy;
                this.ConversationVM.IsBusy = this.mNegotiationModel.IsBusy;
                this.RaiseCanExecuteChanged();
            }
        }

        /// <summary>
        /// Get Channel Complete 
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">eNegEntityResultArgs</param>
        private void mNegotiationModel_GetChannelComplete(object sender, eNegEntityResultArgs<Channel> e)
        {

            if (!e.HasError)
            {
                Channels = e.Results.Where(s => s.ChannelTypeID != eNegConstant.ChannelType.AppsChannels).OrderBy(g => g.ChannelName);

                this.RaisePropertyChanged("Channels");
            }
            else
            {
                // notify user if there is any error
                eNegMessanger.RaiseErrorMessage.Send(e.Error);
            }
        }

        /// <summary>
        /// Mnegotiations the model_ get negotiation page number complete.
        /// </summary>
        /// <param name="obj">The obj.</param>
        private void mnegotiationModel_GetNegotiationPageNumberComplete(InvokeOperation<int?> obj)
        {
            //If no errors and Page Numer more than zero as may be this item not exists (as Old Link for Deleted Item)
            if (!obj.HasError && obj.Value != null && obj.Value.Value > 0)
            {
                //Hack:Mohamed in case you need to navigate from addon to 
                //if (AppConfigurations.IsStatusParamterIsOpen)
                //    this.GetOnGoingNegotiationByPageNumberAsync(obj.Value.Value);
                //else
                //    this.GetClosedNegotiationByPageNumberAsync(obj.Value.Value);


            }
            else
            {
                if (obj.HasError)
                {
                    eNegMessanger.RaiseErrorMessage.Send(obj.Error);
                }

                //Hack:Mohamed in case you need to navigate from addon to 

                //if (AppConfigurations.IsStatusParamterIsOpen)
                //    this.GetOnGoingNegotiationByPageNumberAsync(1);
                //else
                //    this.GetClosedNegotiationByPageNumberAsync(1);


                //Reset Variables
                AppConfigurations.ConversationIDParameter = null;
                AppConfigurations.NegotiationIDParameter = null;
                AppConfigurations.ActionTypeParameter = null;
                AppConfigurations.StatusParameter = null;

            }
        }

        /// <summary>
        /// Used as a call back to all loading Events
        /// so if the Current Context is Not busy so
        /// add new Negotiation Node (Addon)
        /// </summary>
        /// <param name="sender">value of Sender</param>
        private void AddNewNegotiationNodeForAddon(object sender)
        {
            AddNewNegotiationNodeForAddon(sender, null);
        }

        /// <summary>
        /// Used as a call back to all loading Events
        /// so if the Current Context is Not busy so
        /// Adding New Negotiation In case Of WebPlatfrom to Addon (Navigation)
        /// </summary>
        /// <param name="sender">value of Sender</param>
        /// <param name="e">value of e</param>
        private void AddNewNegotiationNodeForAddon(object sender, object e)
        {
            Deployment.Current.Dispatcher.BeginInvoke(() =>
            {

                //in case the the Current Context is not busy (Loading)
                if (!mNegotiationModel.IsBusy)
                {
                    //this in case of Complete event of PropertyChanged
                    if (e != null && e.GetType().Equals(typeof(PropertyChangedEventArgs)))
                    {
                        //i need that to run in case of Empty data
                        //if (this.OnGoingNegotiationSource.Count() == 0)
                        //{
                        eNegMessanger.DoOperationMessage.Send(eNegMessanger.OperationType.DataLoadCompleted);
                        //}
                    }
                    else//in case we return from loading data
                    {
                        eNegMessanger.DoOperationMessage.Send(eNegMessanger.OperationType.DataLoadCompleted);
                    }
                }
            });
        }

        #endregion Event Handlers

        #region → Commands       .

        /// <summary>
        /// Gets the save PDF command.
        /// </summary>
        /// <value>The save PDF command.</value>
        public RelayCommand SavePDFCommand
        {
            get
            {
                if (mSavePDFCommand == null)
                {
                    mSavePDFCommand = new RelayCommand(() =>
                    {
                        try
                        {
                            SaveFileDialog dialog = new SaveFileDialog();

                            dialog.DefaultExt = ".pdf";

                            dialog.Filter = string.Format("{1} File (*.{0}) | *.{0}", dialog.DefaultExt, dialog.DefaultExt);

                            if (!(bool)dialog.ShowDialog())
                                return;

                            Stream fileStream = dialog.OpenFile();

                            fileStream.Write(PDFArrayOfBytes, 0, PDFArrayOfBytes.Length);
                            fileStream.Close();

                            eNegMessanger.FlippMessage.Send(ViewTypes.ClosePopupView);
                            eNegMessanger.SendCustomMessage.Send(new eNegMessage(Resources.ExportDone, ImageType.Success));
                        }
                        catch (Exception ex)
                        {
                            // notify user if there is any error
                            eNegMessanger.RaiseErrorMessage.Send(ex);
                        }
                    }
                    , () => !this.IsBusy);
                }
                return mSavePDFCommand;
            }
        }

        /// <summary>
        /// Gets the tree check changes command.
        /// in case of check or uncheck checkbox
        /// </summary>
        /// <value>The tree check changes command.</value>
        public RelayCommand<RadRoutedEventArgs> TreeCheckChangesCommand
        {
            get
            {
                if (mTreeCheckChangesCommand == null)
                {
                    mTreeCheckChangesCommand = new RelayCommand<RadRoutedEventArgs>((e) =>
                    {
                        try
                        {

                            RadTreeViewItem source = ((RadTreeViewItem)(e.Source));

                            object checkedItem = source.Item;

                            bool isChecked = source.CheckState == ToggleState.On;

                            if (checkedItem is Conversation)
                            {
                                (checkedItem as Conversation).IsConvSelected = isChecked;
                            }
                            else
                            {
                                (checkedItem as Negotiation).IsNegSelected = isChecked;
                            }

                            this.RaiseCanExecuteChanged();
                        }
                        catch (Exception ex)
                        {
                            eNegMessanger.RaiseErrorMessage.Send(ex);
                        }
                    }, (e) => !mNegotiationModel.IsBusy);
                }
                return mTreeCheckChangesCommand;
            }
        }

        /// <summary>
        /// Gets the navigate to conversation details command.
        /// </summary>
        /// <value>The navigate to conversation details command.</value>
        public RelayCommand<IArchiveItem> NavigateToConversationDetailsCommand
        {
            get
            {
                if (mNavigateToConversationDetailsCommand == null)
                {
                    mNavigateToConversationDetailsCommand = new RelayCommand<IArchiveItem>((currentItem) =>
                    {
                        try
                        {

                            if (currentItem.IsLoadedOnDemand)
                            {
                                this.GetMessageByConversationIDAsync(((this.SelectedItem as IArchiveItem).Value as Conversation).ConversationID);

                                currentItem.IsLoadedOnDemand = false;
                            }
                            else
                            {
                                this.MessageVM.RefreshMessagesSource();
                            }

                            eNegMessanger.FlippMessage.Send(ViewTypes.ConversationDetailsView);

                        }
                        catch (Exception ex)
                        {
                            eNegMessanger.RaiseErrorMessage.Send(ex);
                        }

                    }, (currentItem) => true);
                }
                return mNavigateToConversationDetailsCommand;
            }
        }

        /// <summary>
        /// Gets the flip message area command.
        /// </summary>
        /// <value>The flip message area command.</value>
        public RelayCommand FlipMessageAreaCommand
        {
            get
            {
                if (mFlipMessageAreaCommand == null)
                {
                    mFlipMessageAreaCommand = new RelayCommand(() =>
                    {
                        try
                        {
                            eNegMessanger.FlippMessage.Send();

                            if (CurrentMessage != null)
                            {
                                #region Loading Last Used Channel in Last Message
                                SelectedChannel = Channels.
                                    Where(s => s.ChannelID == CurrentMessage.ChannelID).FirstOrDefault();
                                #endregion

                                #region Intiailize all sender/Reciever controls and properties related to current message

                                if (CurrentConversation.Messages.Count > 0)
                                {
                                    if (CurrentMessage.Sent)
                                    {
                                        Message msg = CurrentConversation.Messages.Where(d => d.IsSent == false).OrderByDescending(s => s.DeletedOn).FirstOrDefault();
                                        CurrentMessage.MessageSender = msg != null ? msg.MessageSender : string.Empty;
                                    }
                                    else
                                    {
                                        Message msg = CurrentConversation.Messages.Where(d => d.IsSent == true).OrderByDescending(s => s.DeletedOn).FirstOrDefault();
                                        CurrentMessage.MessageReceiver = msg != null ? msg.MessageReceiver : string.Empty;
                                    }
                                }

                                CurrentMessage.ValidationErrors.Clear();
                                #endregion
                            }
                        }
                        catch (Exception ex)
                        {
                            eNegMessanger.RaiseErrorMessage.Send(ex);
                        }
                    });
                }
                return mFlipMessageAreaCommand;
            }
        }

        /// <summary>
        /// Gets the get last recent negotiator command.
        /// </summary>
        /// <value>The get last recent negotiator command.</value>
        public RelayCommand SetLastRecentNegotiatorCommand
        {
            get
            {
                if (mSetLastRecentNegotiatorCommand == null)
                {
                    mSetLastRecentNegotiatorCommand = new RelayCommand(() =>
                        {
                            try
                            {
                                if (CurrentMessage != null &&
                                    CurrentConversation.Messages.Count > 0)
                                {
                                    if (CurrentMessage.Sent)
                                    {
                                        Message msg = CurrentConversation.Messages.Where(d => !d.IsSent && !d.IsAppsMessage).OrderByDescending(s => s.DeletedOn).FirstOrDefault();
                                        CurrentMessage.MessageSender = msg != null ? msg.MessageSender : string.Empty;
                                    }
                                    else
                                    {
                                        Message msg = CurrentConversation.Messages.Where(d => d.IsSent && !d.IsAppsMessage).OrderByDescending(s => s.DeletedOn).FirstOrDefault();
                                        CurrentMessage.MessageReceiver = msg != null ? msg.MessageReceiver : string.Empty;
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                eNegMessanger.RaiseErrorMessage.Send(ex);
                            }
                        });
                }
                return mSetLastRecentNegotiatorCommand;
            }
        }

        /// <summary>
        /// Gets the add multi receiver command.
        /// </summary>
        /// <value>The add multi receiver command.</value>
        public RelayCommand AddReceiversToMessageCommand
        {
            get
            {
                if (mAddReceiversToMessageCommand == null)
                {
                    mAddReceiversToMessageCommand = new RelayCommand(() =>
                    {
                        try
                        {
                            if (!mNegotiationModel.IsBusy)
                            {
                                RefreshReceiversCount();
                                if (MultiNegotiatorSource.FirstOrDefault() != null &&
                                    MultiNegotiatorSource.First().NewNegotiator != string.Empty)
                                {
                                    CurrentMessage.MessageReceiver = MultiNegotiatorSource.First().NewNegotiator;
                                }
                                eNegMessanger.FlippMessage.Send(ViewTypes.ClosePopupView);
                            }
                        }
                        catch (Exception ex)
                        {
                            // notify user if there is any error
                            eNegMessanger.RaiseErrorMessage.Send(ex);
                        }
                    });
                }
                return mAddReceiversToMessageCommand;
            }
        }

        /// <summary>
        /// Gets the navigation command.
        /// </summary>
        /// <value>The navigation command.</value>
        public RelayCommand<string> NavigationCommand
        {
            get
            {
                if (mNavigationCommand == null)
                {
                    mNavigationCommand = new RelayCommand<string>((menuText) =>
                    {
                        try
                        {
                            if (!this.IsBusy)
                            {
                                if (menuText != null || (DateTime.Now.Ticks - mLastTicks) < CLICK_TIME)
                                {

                                    string url = AppConfigurations.HostBaseAddress;

                                    if (string.IsNullOrEmpty(menuText))
                                    {

                                        #region → Negotiation   .

                                        if (this.CurrentNegotiation != null)
                                        {
                                            url += "Default.Aspx?NegotiationID=" + this.CurrentNegotiation.NegotiationID.ToString() +
                                                "&ActionType=" + eNegConstant.ActionParameterType.Display;

                                            if (CurrentNegotiation.StatusID == eNegConstant.NegotiationStatus.Closed)
                                                url += "&Status=Closed";
                                        }

                                        #endregion

                                        #region → Conversation  .

                                        else if (this.CurrentConversation != null)
                                        {
                                            url += "Default.Aspx?NegotiationID=" + this.CurrentConversation.Negotiation.NegotiationID.ToString() +
                                                "&ConversationID=" + this.CurrentConversation.ConversationID.ToString() +
                                                "&ActionType=" + eNegConstant.ActionParameterType.ConversationDetails;

                                            if (CurrentConversation.Negotiation.StatusID == eNegConstant.NegotiationStatus.Closed)
                                                url += "&Status=Closed";
                                        }

                                        #endregion

                                        #region → Messages      .

                                        else if (this.internalMessage != null)
                                        {
                                            url += "Default.Aspx?NegotiationID=" + this.internalMessage.Conversation.Negotiation.NegotiationID.ToString() +
                                                 "&ConversationID=" + this.internalMessage.ConversationID.ToString() +
                                                 "&MessageID=" + this.internalMessage.MessageID.ToString() +
                                                 "&ActionType=" + eNegConstant.ActionParameterType.ConversationDetails;

                                            if (this.internalMessage.Conversation.Negotiation.StatusID == eNegConstant.NegotiationStatus.Closed)
                                                url += "&Status=Closed";
                                        }

                                        #endregion
                                    }
                                    else
                                    {
                                        #region → Negotiation   .

                                        if (this.CurrentNegotiation != null)
                                        {
                                            if (menuText == "Open in Add-on")
                                                url += "Addon.aspx?NegotiationID=" + this.CurrentNegotiation.NegotiationID.ToString() + "&ActionType=" + eNegConstant.ActionParameterType.Display;

                                        }
                                        #endregion

                                        #region → Conversation  .

                                        else if (this.CurrentConversation != null)
                                        {

                                            if (menuText == "Add New Message")
                                            {
                                                url += "Addon.aspx?NegotiationID=" + this.CurrentConversation.NegotiationID.ToString() + "&ConversationID=" + this.CurrentConversation.ConversationID.ToString() + "&ActionType=" + eNegConstant.ActionParameterType.AddNewMessage;

                                            }
                                            else if (menuText == "Open in Add-on")
                                            {
                                                url += "Addon.aspx?NegotiationID=" + this.CurrentConversation.NegotiationID.ToString() + "&ConversationID=" + this.CurrentConversation.ConversationID.ToString() + "&ActionType=" + eNegConstant.ActionParameterType.Display;

                                            }
                                        }

                                        #endregion
                                    }

                                    if (url != AppConfigurations.HostBaseAddress)
                                    {
                                        eNegNavigation.NavigateToUrl(url);
                                    }
                                }
                                mLastTicks = DateTime.Now.Ticks;
                            }
                        }
                        catch (Exception ex)
                        {
                            // notify user if there is any error
                            eNegMessanger.RaiseErrorMessage.Send(ex);
                        }
                    }
                    , (s) => true);
                }
                return mNavigationCommand;
            }
        }

        /// <summary>
        /// Gets the navigation by view name command.
        /// </summary>
        /// <value>The navigation by view name command.</value>
        public RelayCommand<string> NavigationByViewNameCommand
        {
            get
            {
                if (mNavigationByViewNameCommand == null)
                {
                    mNavigationByViewNameCommand = new RelayCommand<string>((viewName) =>
                    {
                        try
                        {
                            if (!this.IsBusy && !string.IsNullOrEmpty(viewName))
                            {
                                if (viewName == ViewTypes.RenameNegotiationConversationPopup ||
                                    viewName == ViewTypes.ApplicationSettingsPopup)
                                {
                                    if (viewName == ViewTypes.RenameNegotiationConversationPopup)
                                    {
                                        sortConversation = true;
                                    }
                                    isLastActionPopup = true;
                                }

                                this.RaiseCanExecuteChanged();
                                eNegMessanger.FlippMessage.Send(viewName);
                            }
                        }
                        catch (Exception ex)
                        {
                            // notify user if there is any error
                            eNegMessanger.RaiseErrorMessage.Send(ex);
                        }
                    }
                    , (s) => true);
                }
                return mNavigationByViewNameCommand;
            }
        }

        /// <summary>
        /// User Save changes via Calling SubmitChangesMessage so It call
        /// OnSubmitChangesMessage Method. but this time for Negotiation and conversation only
        /// </summary>
        /// <value>The submit negotiation changes command.</value>
        public RelayCommand SubmitRenameChangesCommand
        {
            get
            {
                if (mSubmitRenameChangesCommand == null)
                {
                    mSubmitRenameChangesCommand = new RelayCommand(() =>
                    {
                        try
                        {
                            if (!mNegotiationModel.IsBusy && SelectedItem != null)
                            {
                                if (SelectedItem is Negotiation)
                                {
                                    Negotiation neg = (SelectedItem as Negotiation);

                                    if (IsNegotiationValid(neg))
                                    {
                                        return;
                                    }
                                }
                                else if (SelectedItem is Conversation)
                                {
                                    if (IsConversationValid((SelectedItem as Conversation)))
                                    {
                                        eNegMessanger.SubmitChangesMessage.Send(eNegMessanger.OperationType.ConversationSubmit);

                                        this.isLastActionPopup = false;

                                        eNegMessanger.FlippMessage.Send(ViewTypes.ClosePopupView);
                                    }
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            // notify user if there is any error
                            eNegMessanger.RaiseErrorMessage.Send(ex);
                        }
                    }
                    , () => !this.mNegotiationModel.IsBusy);
                }
                return mSubmitRenameChangesCommand;
            }
        }

        /// <summary>
        /// Gets the cancel pop up changes command.
        /// </summary>
        /// <value>The cancel pop up changes command.</value>
        public RelayCommand<string> CancelPopUpChangesCommand
        {
            get
            {
                if (mCancelRenameChangesCommand == null)
                {
                    mCancelRenameChangesCommand = new RelayCommand<string>((viewName) =>
                    {
                        try
                        {
                            if (!string.IsNullOrEmpty(viewName))
                            {
                                this.isLastActionPopup = false;

                                if (viewName == ViewTypes.ApplicationSettingsPopup ||
                                    viewName == ViewTypes.RenameNegotiationConversationPopup ||
                                    viewName == ViewTypes.AddNewNegotiationView ||
                                    viewName == ViewTypes.AddNewConversationView)
                                {
                                    if (viewName == ViewTypes.AddNewConversationView || viewName == ViewTypes.AddNewNegotiationView)
                                    {
                                        var currentNode = SelectedItem;

                                        SelectedItem = null;

                                        SelectedItem = currentNode;

                                    }

                                    this.RejectChanges(false);

                                    eNegMessanger.FlippMessage.Send(ViewTypes.ClosePopupView);
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            // notify user if there is any error
                            eNegMessanger.RaiseErrorMessage.Send(ex);
                        }
                    }
                    , (s) => true);
                }
                return mCancelRenameChangesCommand;
            }
        }

        /// <summary>
        /// User Save changes via Calling SubmitChangesMessage so It call
        /// OnSubmitChangesMessage Method.
        /// </summary>
        public RelayCommand SubmitChangeCommand
        {
            get
            {
                if (mSubmitChangeCommand == null)
                {
                    mSubmitChangeCommand = new RelayCommand(() =>
                    {
                        try
                        {
                            if (!mNegotiationModel.IsBusy)
                            {
                                eNegMessanger.SubmitChangesMessage.Send();
                            }
                        }
                        catch (Exception ex)
                        {
                            // notify user if there is any error
                            eNegMessanger.RaiseErrorMessage.Send(ex);
                        }
                    }
                    , () => this.mNegotiationModel.HasChanges && !this.mNegotiationModel.IsBusy);
                }
                return mSubmitChangeCommand;
            }
        }

        /// <summary>
        /// User Cancelling changes via Calling CancelChangesMessage so It call
        /// OnCancelChangesMessage Method.
        /// </summary>
        public RelayCommand CancelChangeCommand
        {
            get
            {
                if (mCancelChangeCommand == null)
                {
                    mCancelChangeCommand = new RelayCommand(() =>
                    {
                        try
                        {
                            if (!mNegotiationModel.IsBusy)
                            {
                                // ask to confirm canceling this new User first
                                DialogMessage dialogMessage = new DialogMessage(
                                    this,
                                    Resources.CancelCurrentItemMessageBoxText,
                                    result =>
                                    {
                                        if (result == MessageBoxResult.OK)
                                        {
                                            // if confirmed, cancel this User
                                            eNegMessanger.CancelChangesMessage.Send();
                                        }
                                    })
                                {
                                    Button = MessageBoxButton.OKCancel,
                                    Caption = Resources.ConfirmMessageBoxCaption
                                };

                                eNegMessanger.ConfirmMessage.Send(dialogMessage);
                            }
                        }
                        catch (Exception ex)
                        {
                            // notify user if there is any error
                            eNegMessanger.RaiseErrorMessage.Send(ex);
                        }
                    }
                    , () => this.mNegotiationModel.HasChanges && !this.mNegotiationModel.IsBusy);
                }
                return mCancelChangeCommand;
            }
        }

        /// <summary>
        /// Used to Delete the Current Negotiation Or The Current Conversation
        /// Used Incase of Right Click Delete this Item (Addon-Web Plat Form)
        /// </summary>
        public RelayCommand DeleteItemCommand
        {
            get
            {
                if (mDeleteItemCommand == null)
                {
                    mDeleteItemCommand = new RelayCommand(() =>
                    {
                        try
                        {
                            if (!mNegotiationModel.IsBusy)
                            {
                                Action<MessageBoxResult> callBackResult = null;

                                #region "Confirmation Message"

                                // ask to confirm canceling this new issue first
                                DialogMessage dialogMessage = new DialogMessage(
                                    this,
                                    Resources.DeleteCurrentItemMessageBoxText,
                                    result => callBackResult(result))
                                {
                                    Button = MessageBoxButton.OKCancel,
                                    Caption = Resources.ConfirmMessageBoxCaption
                                };

                                eNegMessanger.ConfirmMessage.Send(dialogMessage);

                                #endregion "Confirmation Message"


                                callBackResult = (result) =>
                                {
                                    if (result == MessageBoxResult.Cancel)
                                        return;

                                    //Notify the View That the Delete Operation Begin
                                    //To Save theResult Expanded Negotiations.
                                    PerformEvent(OnDeleting);

                                    #region "Incase OF Deleteing Negotiation"

                                    if (this.CurrentNegotiation != null)
                                    {
                                        #region "Incase if the negotiation Is Open (Ongoing)"

                                        if (this.CurrentNegotiation.StatusID == eNegConstant.NegotiationStatus.Ongoing)
                                        {
                                            this.mNegotiationModel.RemoveNegotiation(this.CurrentNegotiation);
                                            this.mOnGoingNegotiation.Remove(this.CurrentNegotiation);

                                            RefreshData(true);
                                        }
                                        #endregion "Incase if the negotiation Is Open (Ongoing)"

                                        #region "Incase if the negotiation Is Closed"

                                        else if (this.CurrentNegotiation.StatusID == eNegConstant.NegotiationStatus.Closed)
                                        {
                                            //IArchiveItem monthNode = this.CurrentNegotiation.Parent;

                                            this.mNegotiationModel.RemoveNegotiation(this.CurrentNegotiation);

                                            this.mClosedNegotiation.Remove(this.CurrentNegotiation);

                                            DeleteTreeItem(this.CurrentNegotiation);

                                            //monthNode.Children.Remove(this.CurrentNegotiation);

                                            RefreshData(false);
                                        }
                                        #endregion "Incase if the negotiation Is Closed"
                                    }
                                    #endregion "Incase of Deleteing Negotiation"

                                    #region "Incase Of Deleting Conversation"

                                    if (this.CurrentConversation != null && this.CurrentConversation.Negotiation != null)
                                    {
                                        Conversation conversation = this.CurrentConversation;

                                        CurrentConversation.Negotiation.RefreshConversations();

                                        #region → Incase if the Conversation Is in Open Negotiation   .

                                        if (this.CurrentConversation.Negotiation.StatusID == eNegConstant.NegotiationStatus.Ongoing)
                                        {
                                            this.mNegotiationModel
                                                .RemoveConversation(conversation);

                                            this.mOnGoingNegotiation
                                                .FirstOrDefault(s => s.NegotiationID == conversation.NegotiationID)
                                                .Conversations
                                                .Remove(conversation);
                                        }

                                        #endregion

                                        #region → Incase if the Conversation Is in Closed Negotiation .

                                        else if (this.CurrentConversation.Negotiation.StatusID == eNegConstant.NegotiationStatus.Closed)
                                        {

                                            IArchiveItem conversationarchItem = (SelectedItem as IArchiveItem);


                                            this.mClosedNegotiation
                                                .FirstOrDefault(s => s.NegotiationID == conversation.NegotiationID)
                                                .Conversations
                                                .Remove(conversation);

                                            this.mNegotiationModel
                                                .RemoveConversation(conversation);

                                            //Remove from Archives
                                            if (conversationarchItem != null &&
                                                conversationarchItem.Parent != null)
                                            {
                                                conversationarchItem.Parent.Children.Remove(conversationarchItem.Parent);
                                                conversationarchItem.Parent.RefreshChildren();
                                            }

                                            RefreshData(false);
                                        }
                                        #endregion
                                    }
                                    #endregion

                                    //if ((this.CurrentNegotiation != null || (this.CurrentConversation != null)))
                                    {
                                        //to Clear current Negotiation and Current Conversation after save
                                        this.mClearCurrentSelectedItems = true;

                                        #region UnSelectAllItems
                                        foreach (var item in this.AllNegotiations)
                                        {
                                            if (item.IsNegSelected)
                                                item.IsNegSelected = false;
                                        }

                                        foreach (var item in mNegotiationModel.Context.Conversations)
                                        {
                                            if (item.IsConvSelected)
                                                item.IsConvSelected = false;
                                        }

                                        RefreshData(true);
                                        RefreshData(false);

                                        RaiseCanExecuteChanged();
                                        #endregion UnSelectAllItems

                                        eNegMessanger.SubmitChangesMessage.Send();
                                    }
                                };
                            }
                        }
                        catch (Exception ex)
                        {
                            // notify user if there is any error
                            eNegMessanger.RaiseErrorMessage.Send(ex);
                        }
                    }
                    , () => (this.CurrentNegotiation != null || this.CurrentConversation != null) && !this.mNegotiationModel.IsBusy);
                }
                return mDeleteItemCommand;
            }
        }

        /// <summary>
        /// Gets the start new negotiation command.
        /// </summary>
        /// <value>The start new negotiation command.</value>
        public RelayCommand StartNewNegotiationCommand
        {
            get
            {
                if (mStartNewNegotiationCommand == null)
                {
                    mStartNewNegotiationCommand = new RelayCommand(() =>
                    {
                        try
                        {
                            if (!this.mNegotiationModel.IsBusy)
                            {
                                Negotiation Neg;
                                this.mClearCurrentSelectedItems = false;
                                this.mNegotiationModel.RejectChanges();

                                //this.CurrentConversation = null;
                                //this.CurrentMessage = null;
                                //this.CurrentNegotiation = null;

                                Neg = this.AddNewNegotiation(true);

                                this.CurrentNegotiation = Neg;

                                this.CurrentConversation = this.CurrentNegotiation.Conversations.First();

                                eNegMessanger.NewPopUp.Send("New Negotiation", eNegMessanger.PopUpType.NewNegotiation);
                            }
                        }
                        catch (Exception ex)
                        {
                            // notify user if there is any error
                            eNegMessanger.RaiseErrorMessage.Send(ex);
                        }
                    }
                    , () => !this.mNegotiationModel.IsBusy);
                }
                return mStartNewNegotiationCommand;
            }
        }

        /// <summary>
        /// Useed to Add New Negotiation Node To The Leaf of the Tree
        /// Used Incase of Click Start New Neg
        /// </summary>
        public RelayCommand AddNewNegotiationCommand
        {
            get
            {
                if (mAddNewNegotiationCommand == null)
                {
                    mAddNewNegotiationCommand = new RelayCommand(() =>
                    {
                        try
                        {
                            if (!this.mNegotiationModel.IsBusy)
                            {
                                #region → Add temp cookie in Iso Storage in case of being not presistent in last login

                                try
                                {
                                    IsolatedStorageSettings appSettings = IsolatedStorageSettings.ApplicationSettings;
                                    if (!appSettings.Contains("UserName"))
                                    {
                                        if (!appSettings.Contains("TempUserName"))
                                        {
                                            appSettings.Add("TempUserName", AppConfigurations.CurrentLoginUser.EmailAddress);
                                        }
                                        else
                                        {
                                            appSettings["TempUserName"] = AppConfigurations.CurrentLoginUser.EmailAddress;
                                        }
                                    }
                                    if (!AppConfigurations.IsAddon)
                                    {
                                        if (!appSettings.Contains("AddNegotiation"))
                                        {
                                            appSettings.Add("AddNegotiation", true);
                                        }
                                        else
                                        {
                                            appSettings["AddNegotiation"] = true;
                                        }
                                    }
                                    appSettings.Save();
                                }
                                catch (Exception) { }
                                #endregion

                                if (AppConfigurations.IsAddon)
                                {
                                    if (!this.IsNegotiationValid(CurrentNegotiation))
                                    {
                                        return;
                                    }
                                }
                                else
                                {
                                    eNegNavigation.NavigateToUrl(AppConfigurations.HostBaseAddress + "Addon.aspx");
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            // notify user if there is any error
                            eNegMessanger.RaiseErrorMessage.Send(ex);
                        }
                    }
                    , () => !this.mNegotiationModel.IsBusy);
                }
                return mAddNewNegotiationCommand;
            }
        }

        /// <summary>
        /// Gets the start new conversation command.
        /// </summary>
        /// <value>The start new conversation command.</value>
        public RelayCommand StartNewConversationCommand
        {
            get
            {
                if (mStartNewConversationCommand == null)
                {
                    mStartNewConversationCommand = new RelayCommand(() =>
                    {
                        try
                        {
                            if (!mNegotiationModel.IsBusy && this.CurrentNegotiation != null)
                            {
                                Negotiation Neg = this.mOnGoingNegotiation.FirstOrDefault(s => s.NegotiationID == this.CurrentNegotiation.NegotiationID);

                                if (Neg != null)
                                {
                                    mIsAddingConversation = true;

                                    Conversation Conv;

                                    mClearCurrentSelectedItems = false;

                                    this.mNegotiationModel.RejectChanges();

                                    //this.CurrentConversation = null;
                                    //this.CurrentMessage = null;
                                    //this.CurrentNegotiation = null;

                                    Conv = this.AddNewConversation(true);
                                    Conv.IsNewConversation = true;
                                    Conv.NegotiationID = Neg.NegotiationID;
                                    Conv.Negotiation = Neg;

                                    Neg.Conversations.Add(Conv);

                                    this.CurrentConversation = Conv;
                                    eNegMessanger.NewPopUp.Send("New Conversation", eNegMessanger.PopUpType.NewConversation);
                                }
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
                return mStartNewConversationCommand;
            }
        }

        /// <summary>
        /// Useed to Add New Conversation Node To The Selected Negotiation
        /// Used Incase of Click Right Mose on Negotiation Start New Conversation Button (Addon)
        /// </summary>
        public RelayCommand AddNewConversationCommand
        {
            get
            {
                if (mAddNewConversationCommand == null)
                {
                    mAddNewConversationCommand = new RelayCommand(() =>
                    {
                        try
                        {
                            if (!IsConversationValid(CurrentConversation))
                            {
                                return;
                            }
                            else
                            {
                                CurrentNegotiation.RefreshConversations();

                                ConversationVM.CurrentNegotiation = CurrentNegotiation;
                                SelectedItem = CurrentConversation;

                                eNegMessanger.SubmitChangesMessage.Send();
                                eNegMessanger.FlippMessage.Send(ViewTypes.ClosePopupView);
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
                return mAddNewConversationCommand;
            }
        }

        /// <summary>
        /// Used to Change The Status Of The Selected Negotiation
        /// from Onging to Closed And Vice Versa.
        /// Used in Both (WebPlatform)
        /// Selected by Check Box
        /// ◙ Open   Negotiation 1
        /// ◙ Closed Negotiation 1.
        /// </summary>
        public RelayCommand CloseReOpenAllSelectedNegotiationCommand
        {
            get
            {
                if (mCloseReOpenAllSelectedNegotiationCommand == null)
                {
                    mCloseReOpenAllSelectedNegotiationCommand = new RelayCommand(() =>
                    {
                        try
                        {
                            if (!this.mNegotiationModel.IsBusy)
                            {

                                #region → Save the Selected Guid Of Selected Negotiations.

                                List<Guid> TmpListOgNegotiations = new List<Guid>();
                                foreach (var OngingItem in this.mOnGoingNegotiation.Where(s => s.IsNegSelected))
                                {
                                    TmpListOgNegotiations.Add(OngingItem.NegotiationID);
                                }

                                foreach (var ClosedItem in this.mClosedNegotiation.Where(s => s.IsNegSelected))
                                {
                                    TmpListOgNegotiations.Add(ClosedItem.NegotiationID);
                                }

                                #endregion

                                //Reject any other Changes
                                this.mNegotiationModel.RejectChanges();

                                foreach (var tmpNegotiationID in TmpListOgNegotiations)
                                {
                                    Negotiation neg = this.AllNegotiations.FirstOrDefault(s => s.NegotiationID == tmpNegotiationID);

                                    if (neg != null)
                                    {

                                        #region → Convert Negotiations from Open to Closed .

                                        if (neg.StatusID == eNegConstant.NegotiationStatus.Ongoing)
                                        {

                                            neg.StatusID = eNegConstant.NegotiationStatus.Closed;

                                            //First Delete Item From Onging And Add It To Closed
                                            this.mOnGoingNegotiation.Remove(neg);
                                            this.mClosedNegotiation.Add(neg);
                                        }
                                        #endregion

                                        #region → Convert Negotiations from Closed to Open .

                                        else
                                            if (neg.StatusID == eNegConstant.NegotiationStatus.Closed)
                                            {
                                                neg.StatusID = eNegConstant.NegotiationStatus.Ongoing;

                                                //First Delete Item From Onging And Add It To Closed
                                                this.mClosedNegotiation.Remove(neg);
                                                this.mOnGoingNegotiation.Add(neg);
                                            }

                                        #endregion

                                    }
                                }

                                if (TmpListOgNegotiations.Count > 0)
                                {
                                    this.CurrentConversation = null;

                                    //to Clear current Negotiation and Current Conversatuion after save
                                    this.mClearCurrentSelectedItems = true;

                                    RefreshData(true);
                                    RefreshData(false);


                                    eNegMessanger.SubmitChangesMessage.Send();
                                }
                            }

                        }
                        catch (Exception ex)
                        {
                            // notify user if there is any error
                            eNegMessanger.RaiseErrorMessage.Send(ex);
                        }
                    }
                    , () => IsAnyNegotiationSelected && !this.mNegotiationModel.IsBusy);
                }
                return mCloseReOpenAllSelectedNegotiationCommand;
            }
        }

        private LoadingQueue LoadQueue = null;
        private bool sortConversation;

        /// <summary>
        /// Used to Change The Status Of The Selected Negotiation
        /// from Onging to Closed And Vice Versa.
        /// Used in Both (Addon-WebPlatform)
        /// for drag and drop. and Close reopen Button in Addon.
        /// </summary>
        public RelayCommand CloseReOpenSelectedNegotiationCommand
        {
            get
            {
                if (mCloseReOpenNegotiationCommand == null)
                {
                    mCloseReOpenNegotiationCommand = new RelayCommand(() =>
                    {
                        try
                        {
                            if (this.CurrentNegotiation != null && !this.mNegotiationModel.IsBusy)
                            {
                                //Reject any other Changes
                                this.mNegotiationModel.RejectChanges();

                                Negotiation neg = this.AllNegotiations.FirstOrDefault(s => s.NegotiationID == this.CurrentNegotiation.NegotiationID);

                                this.CurrentConversation = null;
                                if (neg != null)
                                {
                                    if (this.CurrentNegotiation.StatusID == eNegConstant.NegotiationStatus.Ongoing)
                                    {
                                        this.CurrentNegotiation.StatusID = eNegConstant.NegotiationStatus.Closed;
                                        neg.StatusID = eNegConstant.NegotiationStatus.Closed;

                                        //First Delete Item From Onging And Add It To Closed
                                        this.mOnGoingNegotiation.Remove(this.CurrentNegotiation);
                                        this.mClosedNegotiation.Add(this.CurrentNegotiation);

                                        //For realoading archieve.
                                        LoadQueue = new LoadingQueue();

                                        LoadQueue.CurrentNegotiation = this.CurrentNegotiation;

                                        LoadQueue.LoadType = LoadTypes.ClosedNegotiation;
                                    }
                                    else if (this.CurrentNegotiation.StatusID == eNegConstant.NegotiationStatus.Closed)
                                    {
                                        this.CurrentNegotiation.StatusID = eNegConstant.NegotiationStatus.Ongoing;

                                        neg.StatusID = eNegConstant.NegotiationStatus.Ongoing;

                                        Negotiation tempNeg = this.CurrentNegotiation;

                                        //First Delete Item From Onging And Add It To Closed
                                        this.mClosedNegotiation.Remove(this.CurrentNegotiation);

                                        this.DeleteTreeItem(this.CurrentNegotiation);

                                        this.CurrentNegotiation = tempNeg;

                                        this.mOnGoingNegotiation.Add(this.CurrentNegotiation);
                                    }

                                    //to Clear current Negotiation and Current Conversatuion after save
                                    this.mClearCurrentSelectedItems = true;

                                    foreach (var item in this.AllNegotiations)
                                    {
                                        if (item.IsNegSelected)
                                            item.IsNegSelected = false;
                                    }

                                    foreach (var item in mNegotiationModel.Context.Conversations)
                                    {
                                        if (item.IsConvSelected)
                                            item.IsConvSelected = false;
                                    }

                                    RefreshData(true);

                                    RefreshData(false);

                                    eNegMessanger.SubmitChangesMessage.Send();
                                }
                            }

                        }
                        catch (Exception ex)
                        {
                            // notify user if there is any error
                            eNegMessanger.RaiseErrorMessage.Send(ex);
                        }
                    }
                    , () => this.CurrentNegotiation != null && !this.mNegotiationModel.IsBusy);
                }
                return mCloseReOpenNegotiationCommand;
            }
        }

        /// <summary>
        /// Used to Delete All The Selected Negotiation and Conversation By Check Box.
        /// Used in (WebPlatform) Only
        /// </summary>
        public RelayCommand DeleteSelectedItemsCommand
        {
            get
            {
                if (mDeleteSelectedItemsCommand == null)
                {
                    mDeleteSelectedItemsCommand = new RelayCommand(() =>
                    {

                        try
                        {
                            #region "Confirmation Message"

                            Action<MessageBoxResult> callBackResult = null;

                            // ask to confirm canceling this new issue first
                            DialogMessage dialogMessage = new DialogMessage(
                                this,
                                Resources.DeleteCurrentItemMessageBoxText,
                                result => callBackResult(result))
                            {
                                Button = MessageBoxButton.OKCancel,
                                Caption = Resources.ConfirmMessageBoxCaption
                            };

                            eNegMessanger.ConfirmMessage.Send(dialogMessage);

                            #endregion "Confirmation Message"

                            callBackResult = (result) =>
                                {
                                    if (result == MessageBoxResult.Cancel)
                                        return;

                                    bool IsAnyNodeSelectedOngiong = false;
                                    bool IsAnyNodeSelectedClosed = false;

                                    if (!mNegotiationModel.IsBusy)
                                    {
                                        //Notify the View That the Delete Operation Begin
                                        //To Save theResult Expanded Negotiations.
                                        PerformEvent(OnDeleting);

                                        //mNegotiationModel.RejectChanges();

                                        IsAnyNodeSelectedOngiong = DeleteSelected(true);
                                        IsAnyNodeSelectedClosed = DeleteSelected(false);

                                        if (IsAnyNodeSelectedOngiong)
                                            RefreshData(true);

                                        if (IsAnyNodeSelectedClosed)
                                            RefreshData(false);

                                        if (IsAnyNodeSelectedClosed || IsAnyNodeSelectedOngiong)
                                        {
                                            //to Clear current Negotiation and Current Conversatuion after save
                                            this.mClearCurrentSelectedItems = true;

                                            eNegMessanger.SubmitChangesMessage.Send();
                                        }
                                    }
                                };
                        }
                        catch (Exception ex)
                        {
                            // notify user if there is any error
                            eNegMessanger.RaiseErrorMessage.Send(ex);
                        }
                    }
                    , () => DisableEnableDeleteButton && !this.mNegotiationModel.IsBusy);
                }
                return mDeleteSelectedItemsCommand;
            }
        }

        /// <summary>
        /// Enable Disable Delete Button
        /// </summary>
        private bool DisableEnableDeleteButton
        {
            get
            {
                if (IsAnyNegotiationSelected)
                    return true;
                else
                {
                    IEnumerable<Conversation> conversation = this.mNegotiationModel.Context.Conversations;

                    if (conversation.Count(s => s.IsConvSelected == true) > 0)
                        return true;

                }
                return false;
            }
        }

        /// <summary>
        /// User Save Messages changes via Calling SubmitChangesMessage so It call
        /// OnSubmitChangesMessage Method.
        /// </summary>
        public RelayCommand SubmitMessageChangesCommand
        {
            get
            {
                if (mSubmitMessageChangesCommand == null)
                {
                    mSubmitMessageChangesCommand = new RelayCommand(() =>
                    {
                        try
                        {
                            if (!mNegotiationModel.IsBusy && this.CurrentMessage != null)
                            {

                                #region Validation On Current Message

                                bool IsAllValid = true;

                                // this should trigger validation even if the MessageContent is not changed and is null
                                if (string.IsNullOrWhiteSpace(this.CurrentMessage.MessageContent))
                                {
                                    this.CurrentMessage.MessageContent = string.Empty;
                                }

                                if (string.IsNullOrWhiteSpace(this.CurrentMessage.MessageSender))
                                    this.CurrentMessage.MessageSender = string.Empty;

                                if (string.IsNullOrWhiteSpace(this.CurrentMessage.MessageReceiver))
                                    this.CurrentMessage.MessageReceiver = string.Empty;


                                if (this.CurrentMessage.ChannelID == Guid.Empty)
                                {
                                    this.CurrentMessage.ValidationErrors.Add(new ValidationResult(Resources.ValidationErrorRequiredField, new string[] { "ChannelID" }));
                                    IsAllValid = false;
                                    return;
                                }
                                if (string.IsNullOrEmpty(this.CurrentMessage.MessageDate.ToString()))
                                {
                                    this.CurrentMessage.ValidationErrors.Add(new ValidationResult(Resources.ValidationErrorRequiredField, new string[] { "MessageDate" }));
                                    IsAllValid = false;
                                    return;
                                }

                                if (!(this.CurrentMessage.TryValidateObject()
                                    && this.CurrentMessage.TryValidateProperty("MessageContent")
                                    && this.CurrentMessage.TryValidateProperty("MessageSender")
                                    && this.CurrentMessage.TryValidateProperty("MessageReceiver")
                                    && this.CurrentMessage.TryValidateProperty("ChannelID")
                                    && CurrentMessage.ValidationErrors.Count == 0))
                                {
                                    IsAllValid = false;
                                }


                                #endregion Validation On Current Message

                                if (IsAllValid)
                                {
                                    if (string.IsNullOrEmpty(this.CurrentMessage.MessageSubject) ||
                                        string.IsNullOrWhiteSpace(this.CurrentMessage.MessageSubject))
                                    {
                                        this.CurrentMessage.MessageSubject = string.Empty;
                                        this.CurrentMessage.MessageSubject =
                                            string.Concat(this.CurrentMessage.MessageDate.ToString(), " ",
                                            Channels.Where(s => s.ChannelID == CurrentMessage.ChannelID).First().ChannelName);
                                    }
                                    this.mNegotiationModel.RejectChanges();
                                    this.mNegotiationModel.Context.Messages.Add(this.CurrentMessage);

                                    #region → Check on Additional Receievers List To create the same message for them
                                    if (this.CurrentMessage.Sent && MultiNegotiatorSource != null)
                                    {
                                        List<MultiNegotiator> SetOfReceivers = new List<MultiNegotiator>();

                                        foreach (var negotiator in MultiNegotiatorSource)
                                        {
                                            if (!string.IsNullOrEmpty(negotiator.NewNegotiator) &&
                                                SetOfReceivers.Where(s => s.NewNegotiator == negotiator.NewNegotiator).FirstOrDefault() == null)
                                            {
                                                SetOfReceivers.Add(negotiator);
                                            }
                                        }

                                        if (SetOfReceivers.Count() > 1)
                                        {
                                            foreach (var receiver in SetOfReceivers.Skip(1))
                                            {
                                                Conversation ConversationWithReceiver = CurrentConversation.Negotiation.Conversations.Where(s =>
                                                    s.Messages.Where(r => r.MessageReceiver == receiver.NewNegotiator || r.MessageSender == receiver.NewNegotiator)
                                                    .FirstOrDefault() != null).FirstOrDefault();

                                                if (ConversationWithReceiver == null)
                                                {
                                                    ConversationWithReceiver = AddNewConversation(true);
                                                    ConversationWithReceiver.ConversationName = CurrentMessage.Conversation.ConversationName + " With " + receiver.NewNegotiator;
                                                    ConversationWithReceiver.Negotiation = CurrentMessage.Conversation.Negotiation;
                                                    ConversationWithReceiver.NegotiationID = CurrentMessage.Conversation.Negotiation.NegotiationID;
                                                }

                                                Message ClonedMessage = CloneMessage(CurrentMessage);
                                                ClonedMessage.MessageReceiver = receiver.NewNegotiator;

                                                if (!ConversationWithReceiver.Messages.Contains(ClonedMessage))
                                                {
                                                    ConversationWithReceiver.Messages.Add(ClonedMessage);
                                                }
                                            }
                                        }
                                    }
                                    #endregion

                                    MultiNegotiatorSource = null;

                                    eNegMessanger.SubmitChangesMessage.Send(eNegMessanger.OperationType.MessageSubmit);
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            // notify user if there is any error
                            eNegMessanger.RaiseErrorMessage.Send(ex);
                        }
                    }
                    , () => !mNegotiationModel.IsBusy && this.CurrentMessage != null);
                }
                return mSubmitMessageChangesCommand;
            }
        }

        /// <summary>
        /// used to Activae/Decativate Application status (Webplatform and Addon).
        /// </summary>
        public RelayCommand ChangeApplicationStatusCommand
        {
            get
            {
                if (mChangeApplicationStatusCommand == null)
                {
                    mChangeApplicationStatusCommand = new RelayCommand(() =>
                    {
                        try
                        {
                            if (this.CurrentNegotiation != null)
                            {
                                this.isLastActionPopup = false;

                                eNegMessanger.SubmitChangesMessage.Send(eNegMessanger.OperationType.NegotiationApplicationStatus);

                                //Send message to close pop up
                                eNegMessanger.FlippMessage.Send(ViewTypes.ClosePopupView);
                            }
                        }
                        catch (Exception ex)
                        {
                            // notify user if there is any error
                            eNegMessanger.RaiseErrorMessage.Send(ex);
                        }
                    }
                    , () => (this.CurrentNegotiation != null && !this.mNegotiationModel.IsBusy));
                }
                return mChangeApplicationStatusCommand;
            }
        }

        /// <summary>
        /// Used to open the popup window for adding more receivers to the Additional Receivers list
        /// </summary>
        /// <value>The add more receivers command.</value>
        public RelayCommand AddMoreReceiversCommand
        {
            get
            {
                if (mAddMoreReceiversCommand == null)
                {
                    mAddMoreReceiversCommand = new RelayCommand(() =>
                    {
                        try
                        {
                            if (CurrentMessage != null &&
                                !string.IsNullOrEmpty(CurrentMessage.MessageReceiver) &&
                                MultiNegotiatorSource != null &&
                                MultiNegotiatorSource.First().NewNegotiator != CurrentMessage.MessageReceiver)
                            {
                                mMultiNegotiatorSource.Insert(0, new MultiNegotiator()
                                {
                                    OldNegotiators = CurrentConversation.Negotiation.Negotiators,
                                    NewNegotiator = CurrentMessage.MessageReceiver
                                });
                                MultiNegotiatorSource = new List<MultiNegotiator>(mMultiNegotiatorSource);
                            }

                            eNegMessanger.NewPopUp.Send("Add More Receivers", eNegMessanger.PopUpType.AddMoreReceivers);
                        }
                        catch (Exception ex)
                        {
                            // notify user if there is any error
                            eNegMessanger.RaiseErrorMessage.Send(ex);
                        }
                    }
                    , () => this.mNegotiationModel != null && !this.mNegotiationModel.IsBusy);
                }
                return mAddMoreReceiversCommand;
            }
        }

        /// <summary>
        /// Gets the publish negotiation command.
        /// </summary>
        /// <value>The publish negotiation command.</value>
        public RelayCommand PublishNegotiationCommand
        {
            get
            {
                if (mPublishNegotiationCommand == null)
                {
                    mPublishNegotiationCommand = new RelayCommand(() =>
                    {
                        try
                        {
                            if (this.CurrentNegotiation != null)
                            {

                                #region → In case Use without any Organization              .

                                if (this.UserOrganizations.Count() == 0)
                                {
                                    eNegMessanger.SendCustomMessage.Send(
                                        new eNegMessage(
                                             Resources.UserHaveNotOrganization,
                                             ImageType.Warning));
                                    //this.RemoveCheckedSelection();
                                    return;
                                }

                                #endregion

                                #region → Sure If negotiation published to all Organization .

                                foreach (var OrganizationItem in this.UserOrganizations)
                                {
                                    //If already published to that organization or not.

                                    OrganizationItem.CanItemPublish = this.CurrentNegotiation
                                                                          .NegotiationOrganizations
                                                                          .Where(d => d.OrganizationID == OrganizationItem.OrganizationID)
                                                                          .Count() == 0;

                                    OrganizationItem.IsSelected = true;
                                }

                                //Check if thier still unpublished negotiation to organization
                                if (this.UserOrganizations.Where(s => s.CanItemPublish).Count() <= 0)
                                {
                                    eNegMessanger.SendCustomMessage.Send(
                                                                           new eNegMessage(
                                                                                Resources.NegotaionAlreadyPublished,
                                                                                ImageType.Info));
                                    //this.RemoveCheckedSelection();

                                    return;

                                }

                                #endregion

                                eNegMessanger.NewPopUp.Send("Confirmation", eNegMessanger.PopUpType.SelectOrganizationToPublish);

                            }
                        }
                        catch (Exception ex)
                        {
                            // notify user if there is any error
                            eNegMessanger.RaiseErrorMessage.Send(ex);
                        }
                    }
                    , () => this.mNegotiationModel != null &&
                           !this.mNegotiationModel.IsBusy &&
                            this.CurrentNegotiation != null);
                }
                return mPublishNegotiationCommand;
            }
        }

        /// <summary>
        /// Gets the finish publish negotiation command.
        /// </summary>
        /// <value>The finish publish negotiation command.</value>
        public RelayCommand FinishPublishNegotiationCommand
        {
            get
            {
                if (mFinishPublishNegotiationCommand == null)
                {
                    mFinishPublishNegotiationCommand = new RelayCommand(() =>
                    {
                        try
                        {
                            if (this.CurrentNegotiation != null)
                            {
                                foreach (var OrganizationItem in this.UserOrganizations.Where(s => s.CanItemPublish && s.IsSelected))
                                {

                                    //Check if negotiaion was published before to that organization
                                    if (this.CurrentNegotiation.NegotiationOrganizations
                                                        .Where(o => o.OrganizationID == OrganizationItem.OrganizationID)
                                                        .Count() <= 0)
                                    {
                                        this.mNegotiationModel.AddNegotiationOrganization(this.CurrentNegotiation, OrganizationItem, true);
                                    }

                                }

                                //For realoading archieve.
                                LoadQueue = new LoadingQueue();

                                LoadQueue.CurrentNegotiation = this.CurrentNegotiation;

                                LoadQueue.LoadType = LoadTypes.PublishedNegotiation;


                                //this.RemoveCheckedSelection();

                                //Send messge to submit changes.
                                eNegMessanger.SubmitChangesMessage.Send();

                                //Send message to close pop up
                                eNegMessanger.FlippMessage.Send(ViewTypes.ClosePopupView);
                            }
                        }
                        catch (Exception ex)
                        {
                            // notify user if there is any error
                            eNegMessanger.RaiseErrorMessage.Send(ex);
                        }
                    }
                    , () => this.mNegotiationModel != null &&
                           !this.mNegotiationModel.IsBusy &&
                           this.UserOrganizations.Where(s => s.CanItemPublish && s.IsSelected).Count() > 0);
                }
                return mFinishPublishNegotiationCommand;
            }
        }

        /// <summary>
        /// Gets the cancel publish negotiation command.
        /// </summary>
        /// <value>The cancel publish negotiation command.</value>
        public RelayCommand CancelPublishNegotiationCommand
        {
            get
            {
                if (mCancelPublishNegotiationCommand == null)
                {
                    mCancelPublishNegotiationCommand = new RelayCommand(() =>
                    {
                        try
                        {
                            if (!mNegotiationModel.IsBusy)
                            {
                                eNegMessanger.FlippMessage.Send(ViewTypes.ClosePopupView);
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
                return mCancelPublishNegotiationCommand;
            }
        }

        /// <summary>
        /// Gets the raise finish publish command.
        /// </summary>
        /// <value>The raise finish publish command.</value>
        public RelayCommand RaiseFinishPublishCommand
        {
            get
            {
                if (mRaiseFinishPublishCommand == null)
                {
                    mRaiseFinishPublishCommand = new RelayCommand(() =>
                    {
                        try
                        {
                            if (!mNegotiationModel.IsBusy)
                            {
                                Deployment.Current.Dispatcher.BeginInvoke(() =>
                                FinishPublishNegotiationCommand.RaiseCanExecuteChanged());
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
                return mRaiseFinishPublishCommand;
            }
        }

        #endregion Command

        #region → Methods        .

        #region → Private        .

        /// <summary>
        /// update in Negotiation and conversation according to the type of obj
        /// </summary>
        /// <param name="selectedObj">Object to send as message parameter (neg or conversation)</param>
        private void SetSelectedItem(object selectedObj)
        {
            if (selectedObj != null)
            {
                eNegMessanger.DoOperationMessage.Send(eNegMessanger.OperationType.ResetSelectAll);

                IArchiveItem selectNode = (selectedObj as IArchiveItem);

                switch (selectNode.ArchiveItemType)
                {
                    case ArchiveItemTypes.Year:
                        {
                            eNegMessanger.EditNegotiationMessage.Send(null);
                            eNegMessanger.EditConversationMessage.Send(null);
                            eNegMessanger.EditCertainMessage.Send(null);
                            break;
                        }
                    case ArchiveItemTypes.Month:
                        {
                            if (selectNode.IsLoadedOnDemand)
                            {
                                this.GetClosedNegotiationByArchiveAsync(
                                    (int)selectNode.Parent.Value,
                                    (int)selectNode.Value);
                                selectNode.IsLoadedOnDemand = false;
                            }

                            eNegMessanger.EditNegotiationMessage.Send(null);
                            eNegMessanger.EditConversationMessage.Send(null);
                            eNegMessanger.EditCertainMessage.Send(null);

                            break;
                        }
                    case ArchiveItemTypes.Negotiation:
                        {
                            eNegMessanger.EditNegotiationMessage.Send((Negotiation)selectNode.Value);
                            eNegMessanger.EditConversationMessage.Send(null);
                            eNegMessanger.EditCertainMessage.Send(null);

                            eNegMessanger.FlippMessage.Send(ViewTypes.NegoitationDetailsView);
                            break;
                        }
                    case ArchiveItemTypes.Conversation:
                        {
                            eNegMessanger.EditConversationMessage.Send((Conversation)selectNode.Value);
                            eNegMessanger.EditNegotiationMessage.Send(null);
                            eNegMessanger.EditCertainMessage.Send(null);

                            NavigateToConversationDetailsCommand.Execute(selectNode);

                            break;
                        }
                    default:
                        break;
                }

                //if (selectedObj is Message)
                //{
                //    eNegMessanger.EditNegotiationMessage.Send(null);
                //    eNegMessanger.EditConversationMessage.Send(null);
                //    eNegMessanger.EditCertainMessage.Send((Message)selectedObj);
                //}
            }
            else
            {
                eNegMessanger.EditNegotiationMessage.Send(null);
                eNegMessanger.EditConversationMessage.Send(null);
                eNegMessanger.EditCertainMessage.Send(null);
            }

            eNegMessanger.ApplyChangesForAppsMessage.Send(
                new ApplicationChange()
            {
                IsForUserChanges = false,
                ApplyChanged = true,
                CurrentConversation = this.CurrentConversation,
                CurrentNegotiation = this.CurrentNegotiation
            });

            this.RaiseCanExecuteChanged();
        }

        /// <summary>
        /// Removes the checked selection.
        /// </summary>
        private void RemoveCheckedSelection()
        {

            foreach (var negotiationItem in AllNegotiations.Where(s => s.IsNegSelected))
            {
                negotiationItem.IsNegSelected = false;
                foreach (var conversationItem in negotiationItem.Conversations.Where(s => s.IsConvSelected))
                {
                    conversationItem.IsConvSelected = false;
                }
            }

            this.RefreshData(true, false);
            this.RefreshData(false, false);
            this.RaiseCanExecuteChanged();
        }

        /// <summary>
        /// To change the status Enable property for buttons
        /// </summary>
        private void RaiseCanExecuteChanged()
        {
            IsBusy = this.mNegotiationModel.IsBusy;
            SubmitRenameChangesCommand.RaiseCanExecuteChanged();
            SubmitChangeCommand.RaiseCanExecuteChanged();
            CancelChangeCommand.RaiseCanExecuteChanged();
            DeleteItemCommand.RaiseCanExecuteChanged();
            DeleteSelectedItemsCommand.RaiseCanExecuteChanged();
            AddNewNegotiationCommand.RaiseCanExecuteChanged();
            CloseReOpenSelectedNegotiationCommand.RaiseCanExecuteChanged();
            CloseReOpenAllSelectedNegotiationCommand.RaiseCanExecuteChanged();
            ChangeApplicationStatusCommand.RaiseCanExecuteChanged();
            SubmitMessageChangesCommand.RaiseCanExecuteChanged();
            NavigationCommand.RaiseCanExecuteChanged();
            SetLastRecentNegotiatorCommand.RaiseCanExecuteChanged();
            FlipMessageAreaCommand.RaiseCanExecuteChanged();
            TreeCheckChangesCommand.RaiseCanExecuteChanged();
            NavigateToConversationDetailsCommand.RaiseCanExecuteChanged();
            PublishNegotiationCommand.RaiseCanExecuteChanged();
            FinishPublishNegotiationCommand.RaiseCanExecuteChanged();
            StartNewConversationCommand.RaiseCanExecuteChanged();
            StartNewNegotiationCommand.RaiseCanExecuteChanged();
            AddNewConversationCommand.RaiseCanExecuteChanged();
            AddNewNegotiationCommand.RaiseCanExecuteChanged();
        }

        /// <summary>
        /// Called when [edit user organization message].
        /// </summary>
        /// <param name="userOrganization">The user organization.</param>
        private void OnEditUserOrganizationMessage(UserOrganization userOrganization)
        {
            this.GetOrganizationsForUserAsync();
        }

        /// <summary>
        /// Executed when SubmitChangesMessage is recieved
        /// </summary>
        /// <param name="OpType">if set to <c>true</c> [is negotiation sumbit].</param>
        private void OnSubmitChangesMessage(eNegMessanger.OperationType OpType)
        {
            //In case that this submit is for message Added from Addon, I put a flag indicate that this operation is for Message Submit 
            // to can check on this flag in the callback of SaveChangesAsync() to open Apps that required DataMatching In Addon
            if (OpType == eNegMessanger.OperationType.MessageSubmit)
            {
                IsMesssageSubmit = true;
            }
            else
            {
                IsMesssageSubmit = false;
            }

            //In case of Applicaton activated deactived.
            if (OpType == eNegMessanger.OperationType.NegotiationApplicationStatus)
            {
                eNegMessanger.ApplyChangesForAppsMessage
                             .Send(
                                   new ApplicationChange()
                                   {
                                       ApplyChanged = true,
                                       IsForUserChanges = false,
                                       CurrentConversation = this.CurrentConversation,
                                       CurrentNegotiation = this.CurrentNegotiation
                                   });
            }


            if ((this.mNegotiationModel.HasChanges))
            {
                mNegotiationModel.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Executed when EditNegotiationMessage is recieved
        /// </summary>
        /// <param name="CurrentNegotiation">CurrentNegotiation</param>
        private void OnEditNegotiationMessage(Negotiation CurrentNegotiation)
        {
            this.CurrentNegotiation = CurrentNegotiation;
            RaiseCanExecuteChanged();
        }

        /// <summary>
        ///Executed when EditConversationMessage is recieved
        /// </summary>
        /// <param name="CurrentConversation">CurrentConversation</param>
        private void OnEditConversationMessage(Conversation CurrentConversation)
        {
            this.CurrentConversation = CurrentConversation;

            RaiseCanExecuteChanged();
        }

        /// <summary>
        /// Called when [edit certain message].
        /// </summary>
        /// <param name="currentMessage">The current message.</param>
        private void OnEditCertainMessage(Message currentMessage)
        {
            //EnableMessageTextBox = false;
            internalMessage = currentMessage;
        }

        /// <summary>
        ///Executed when CancelChangesMessage is recieved
        /// </summary>
        /// <param name="ignore">ignore</param>
        private void OnCancelChangesMessage(Boolean ignore)
        {
            RejectChanges(true);
        }

        /// <summary>
        /// Called when [load completed].
        /// </summary>
        /// <param name="Types">The types.</param>
        private void OnLoadCompleted(eNegMessanger.OperationType Types)
        {
            #region → Export Conversations       .
            if (Types == eNegMessanger.OperationType.ExportPDFConversations)
            {
                IsExportInProgress = true;

                List<Guid> ConvIDs = new List<Guid>();

                foreach (var conv in CurrentNegotiation.Conversations.Where(s => s.IsConvSelected))
                {
                    ConvIDs.Add(conv.ConversationID);
                }
                GeneratePDFConversationsAsync(ConvIDs.ToArray());
            }

            #endregion

            #region → Export Messages            .
            else if (Types == eNegMessanger.OperationType.ExportPDFMessages)
            {
                IsExportInProgress = true;

                List<Guid> MsgIDs = new List<Guid>();

                foreach (var Msg in CurrentConversation.Messages.Where(s => s.IsChecked))
                {
                    MsgIDs.Add(Msg.MessageID);
                }
                GeneratePDFMessagesAsync(MsgIDs.ToArray());
            }

            #endregion

            #region → Data Load Completed        .
            else if (Types == eNegMessanger.OperationType.DataLoadCompleted)
            {
                if (AppConfigurations.IsStartNewNegotiation)
                {
                    AppConfigurations.IsStartNewNegotiation = false;

                    Deployment.Current.Dispatcher.BeginInvoke(() =>
                        {
                            StartNewNegotiationCommand.Execute(null);
                        });
                    #region Case adding new negotiation from web plateform
                    IsolatedStorageSettings appSettings = IsolatedStorageSettings.ApplicationSettings;

                    if (appSettings.Contains("TempUserName"))
                    {
                        appSettings.Remove("TempUserName");
                    }
                    if (appSettings.Contains("AddNegotiation"))
                    {
                        appSettings.Remove("AddNegotiation");
                    }
                    appSettings.Save();

                    #endregion
                }

                if (AppConfigurations.NegotiationIDParameter != null)
                {
                    //Search in open Negotiations
                    Negotiation Neg = this.mOnGoingNegotiation.Where(s => s.NegotiationID == AppConfigurations.NegotiationIDParameter).FirstOrDefault();

                    //Search also in closed one
                    if (Neg == null)
                        Neg = this.mClosedNegotiation.Where(s => s.NegotiationID == AppConfigurations.NegotiationIDParameter).FirstOrDefault();


                    if (Neg != null)
                    {
                        if (!string.IsNullOrEmpty(AppConfigurations.ActionTypeParameter))
                        {
                            if (AppConfigurations.ActionTypeParameter.ToLower() != eNegConstant.ActionParameterType.ConversationDetails.ToLower() && AppConfigurations.IsAddon)
                            {
                                if (AppConfigurations.ConversationIDParameter != null)
                                {

                                    this.CurrentConversation = Neg.Conversations.Where(s => s.ConversationID == AppConfigurations.ConversationIDParameter.Value).FirstOrDefault();
                                    if (this.CurrentConversation != null)
                                    {

                                        this.CurrentConversation.IsNewConversation = AppConfigurations.ActionTypeParameter.ToLower() == eNegConstant.ActionParameterType.Edit.ToLower();
                                        this.CurrentConversation.IsConvSelected = AppConfigurations.ActionTypeParameter.ToLower() == eNegConstant.ActionParameterType.Display.ToLower() || AppConfigurations.ActionTypeParameter.ToLower() == eNegConstant.ActionParameterType.AddNewMessage.ToLower();

                                        eNegMessanger.EditConversationMessage.Send(this.CurrentConversation);
                                        this.CurrentConversation.IsNewConversation = false;
                                        this.CurrentConversation.IsConvSelected = false;

                                        if (AppConfigurations.ActionTypeParameter.ToLower() == eNegConstant.ActionParameterType.AddNewMessage.ToLower())
                                        {
                                            eNegMessanger.FlippMessage.Send(ViewTypes.MessagesInAddon);
                                        }
                                    }
                                    else
                                    {
                                        AppConfigurations.ConversationIDParameter = null;
                                    }
                                }
                                if (AppConfigurations.ConversationIDParameter == null)
                                {
                                    Neg.IsNewNegotiation = AppConfigurations.ActionTypeParameter.ToLower() == eNegConstant.ActionParameterType.Edit.ToLower();
                                    Neg.IsNegSelected = AppConfigurations.ActionTypeParameter.ToLower() == eNegConstant.ActionParameterType.Display.ToLower();
                                }
                            }
                            else
                            {
                                if (!AppConfigurations.IsAddon && AppConfigurations.ConversationIDParameter != null && Neg.Conversations.Count(s => s.ConversationID == AppConfigurations.ConversationIDParameter.Value) > 0)
                                {
                                    this.CurrentConversation = Neg.Conversations.Where(s => s.ConversationID == AppConfigurations.ConversationIDParameter.Value).FirstOrDefault();
                                    this.CurrentConversation.IsNewConversation = true;

                                    eNegMessanger.EditConversationMessage.Send(this.CurrentConversation);

                                    this.CurrentConversation.IsNewConversation = false;

                                    eNegMessanger.FlippMessage.Send(ViewTypes.ConversationDetailsView);
                                }
                            }
                            if (AppConfigurations.ActionTypeParameter.ToLower() != eNegConstant.ActionParameterType.ConversationDetails.ToLower() && AppConfigurations.IsAddon && AppConfigurations.ConversationIDParameter == null)
                            {
                                eNegMessanger.EditNegotiationMessage.Send(Neg);

                                Neg.IsNewNegotiation = false;
                                Neg.IsNegSelected = false;
                            }
                            else if (AppConfigurations.ActionTypeParameter.ToLower() == eNegConstant.ActionParameterType.Display.ToLower() && !AppConfigurations.IsAddon && AppConfigurations.ConversationIDParameter == null)
                            {
                                Neg.IsNegSelected = true;
                                eNegMessanger.EditNegotiationMessage.Send(Neg);

                                Neg.IsNegSelected = false;
                            }
                            //Select application tab.
                            else if ((AppConfigurations.ActionTypeParameter.ToLower() == eNegConstant.ActionParameterType.AppSettings.ToLower() ||
                                      AppConfigurations.ActionTypeParameter.ToLower() == eNegConstant.ActionParameterType.Report.ToLower()) &&
                                      AppConfigurations.ApplicationIDParameter.HasValue)
                            {
                                Conversation conversation = null;

                                //incase if selected node is conversation
                                if (AppConfigurations.ConversationIDParameter.HasValue)
                                {
                                    conversation = Neg.Conversations
                                              .Where(s => s.ConversationID == AppConfigurations.ConversationIDParameter.Value)
                                              .FirstOrDefault();
                                }

                                if (conversation != null)
                                {
                                    eNegMessanger.EditConversationMessage
                                                 .Send(conversation);
                                }
                                else
                                {
                                    Neg.IsNegSelected = true;

                                    eNegMessanger.EditNegotiationMessage
                                                 .Send(Neg);

                                    Neg.IsNegSelected = false;
                                }

                                eNegMessanger.AtivateApplicationMessage
                                             .Send(AppConfigurations.ApplicationIDParameter.Value);
                            }
                        }

                        //Reset Variables
                        AppConfigurations.ConversationIDParameter = null;
                        AppConfigurations.NegotiationIDParameter = null;
                        AppConfigurations.ActionTypeParameter = null;
                        AppConfigurations.StatusParameter = null;
                        AppConfigurations.ApplicationIDParameter = null;

                    }
                }
            }
            #endregion
        }

        /// <summary>
        /// Called when [flip completed].
        /// </summary>
        /// <param name="PageName">Name of the page.</param>
        private void OnFlipCompleted(string PageName)
        {
            if (PageName == ViewTypes.AddMultiReceivers)
            {
                if (this.MultiNegotiatorSource != null &&
                    this.MultiNegotiatorSource.Last().NewNegotiator != string.Empty)
                {
                    this.MultiNegotiatorSource.Add(this.DefaultNegotiator);

                    MultiNegotiatorSource = new List<MultiNegotiator>(MultiNegotiatorSource);
                }
            }
            else if (PageName.Contains(ViewTypes.RemoveMultiReceivers))
            {
                string CurrentReceiverMail = PageName.Substring(ViewTypes.RemoveMultiReceivers.Length + 1);

                var CurrentReceiverItem = MultiNegotiatorSource.Where(s => s.NewNegotiator == CurrentReceiverMail).FirstOrDefault();

                if (CurrentReceiverItem != null)
                    MultiNegotiatorSource.Remove(CurrentReceiverItem);
                MultiNegotiatorSource = new List<MultiNegotiator>(MultiNegotiatorSource);
            }
            else if (PageName == ViewTypes.ClosePopupView)
            {
                //In case if user closed popup of Rename by close button of window.
                //And also in case of Application activation changed
                if (this.isLastActionPopup)
                {
                    CancelPopUpChangesCommand.Execute(null);
                }
            }
        }

        /// <summary>
        /// Used to Delete All The Selected Negotiation and Conversation By IsSelected property.
        /// Helper function for DeleteSelectedItemsCommand
        /// </summary>
        private bool DeleteSelected(bool IsOnging)
        {
            try
            {
                IList<Negotiation> NegotioationItems;

                bool IsAnyNodeSelected = false;

                if (IsOnging)
                    NegotioationItems = this.mOnGoingNegotiation;
                else
                    NegotioationItems = this.mClosedNegotiation;

                #region "Incase OF Deleteing Negotiation"
                IList<Negotiation> NegItems = new List<Negotiation>();

                foreach (Negotiation item in NegotioationItems)
                {

                    if (item.IsNegSelected)
                    {
                        IsAnyNodeSelected = true;
                        this.mNegotiationModel.RemoveNegotiation(item);
                        NegItems.Add(item);
                    }
                    else
                    {
                        foreach (Conversation Subitem in item.Conversations)
                        {
                            if (Subitem.IsConvSelected)
                            {
                                IsAnyNodeSelected = true;
                                this.mNegotiationModel.RemoveConversation(Subitem);
                                NegotioationItems.FirstOrDefault(s => s.NegotiationID == Subitem.NegotiationID).Conversations.Remove(Subitem);
                            }
                        }
                    }
                }

                while (NegItems.Count() > 0)
                {
                    NegotioationItems.Remove(NegItems[0]);
                    NegItems.Remove(NegItems[0]);
                }


                #endregion

                return IsAnyNodeSelected;
            }
            catch (Exception ex)
            {
                eNegMessanger.RaiseErrorMessage.Send(ex);

            }
            return false;
        }

        /// <summary>
        /// Deletes the tree item.
        /// </summary>
        /// <param name="treeNode">The tree node.</param>
        private void DeleteTreeItem(IArchiveItem treeNode)
        {
            if (treeNode.Parent != null)
            {
                treeNode.Parent.Children.Remove(treeNode);
                treeNode.Parent.RefreshChildren();
            }
            else if (treeNode.ArchiveItemType == ArchiveItemTypes.Year)
            {
                ClosedNegotiationArchiveSource.Remove(treeNode);
                //this.RaisePropertyChanged("ClosedNegotiationArchiveSource");
                ClosedNegotiationArchiveSource = new List<IArchiveItem>(ClosedNegotiationArchiveSource);
                return;
            }
            else
            {
                return;
            }


            if (treeNode.Parent.Children.Count == 0)
            {
                DeleteTreeItem(treeNode.Parent);
            }
        }

        /// <summary>
        /// Creates new message Object and relate it to Conversation
        /// </summary>
        private void AddNewMessage()
        {
            if (mcurrentConversation != null)
            {
                var msg = AddNewMessage(false);

                msg.ConversationID = mcurrentConversation.ConversationID;

                Message templateMessage = CurrentConversation.Messages
                                                             .Where(ss => !ss.IsAppsMessage)
                                                             .OrderByDescending(s => s.DeletedOn)
                                                             .FirstOrDefault();
                if (templateMessage != null)
                {
                    msg.ChannelID = templateMessage.ChannelID;

                    msg.IsSent = !templateMessage.IsSent;

                    msg.MessageSender = templateMessage.MessageSender;
                    msg.MessageReceiver = templateMessage.MessageReceiver;
                    msg.MessageSubject = templateMessage.MessageSubject;


                    SetLastRecentNegotiatorCommand.Execute(null);
                }

                this.CurrentMessage = msg;

            }
            else
            {
                this.CurrentMessage = null;
            }

            SubmitMessageChangesCommand.RaiseCanExecuteChanged();

            eNegMessanger.FlippMessage.Send(false);
        }

        /// <summary>
        /// To Set the Negotiation Application By The Current Negotiation App.
        /// </summary>
        private void SetNegotiationAppStatus()
        {
            if (mcurrentNegotiation != null && mcurrentNegotiation.StatusID == eNegConstant.NegotiationStatus.Ongoing)
            {
                NegotiationApplicationStatus = mcurrentNegotiation.NegotiationApplicationStatus;
            }
            else if (mcurrentConversation != null && mcurrentConversation.Negotiation != null && mcurrentConversation.Negotiation.StatusID == eNegConstant.NegotiationStatus.Ongoing)
            {
                NegotiationApplicationStatus = mcurrentConversation.Negotiation.NegotiationApplicationStatus;
            }
            else
            {
                NegotiationApplicationStatus = mNegotiationApplicationStatusDefault;
            }

            eNegMessanger.ChangeCurrentNegotionAppsStatus.Send(NegotiationApplicationStatus);

        }

        /// <summary>
        /// Performs the event.
        /// </summary>
        /// <param name="evt">The evt.</param>
        private void PerformEvent(EventHandler evt)
        {
            if (evt != null)
                evt(null, null);
        }

        /// <summary>
        /// Clones the message.
        /// </summary>
        /// <param name="OriginalMessage">The original message.</param>
        /// <returns>Copy of the original message with new guid</returns>
        private Message CloneMessage(Message OriginalMessage)
        {
            Message ClonedMessage = new Message()
            {
                MessageID = Guid.NewGuid(),
                MessageDate = OriginalMessage.MessageDate,
                MessageSender = OriginalMessage.MessageSender,
                MessageReceiver = OriginalMessage.MessageReceiver,
                MessageSubject = OriginalMessage.MessageSubject,
                MessageContent = OriginalMessage.MessageContent,
                ChannelID = OriginalMessage.ChannelID,
                Deleted = OriginalMessage.Deleted,
                DeletedBy = OriginalMessage.DeletedBy,
                DeletedOn = OriginalMessage.DeletedOn,
                IsSent = OriginalMessage.IsSent,
                Sent = OriginalMessage.Sent,
                Received = OriginalMessage.Received
            };
            return ClonedMessage;
        }

        /// <summary>
        /// Refreshes the receivers count.
        /// </summary>
        private void RefreshReceiversCount()
        {
            IsMessageMultiReceiversEmpty = MultiNegotiatorSource.All(s => s.NewNegotiator == string.Empty || s.NewNegotiator == null);
            IsMessageHasMultiReceivers = !IsMessageMultiReceiversEmpty;
        }

        /// <summary>
        /// Gets the name of the moth.
        /// </summary>
        /// <param name="month">The month.</param>
        /// <returns></returns>
        private string GetMothName(int month)
        {
            return (new DateTime(2000, month, 1)).ToString("MMMM");
        }

        /// <summary>
        /// Refreshes the observable collection of either 
        /// </summary>
        /// <param name="IsForOnging">The desired collection is ongoing or closed</param>
        /// <param name="BuildNegotiator">Value of BuildNegotiator is true so build Negotiator Value
        /// Which is Distinct of Sender And Reciever Value</param>
        private void RefreshData(bool IsForOnging, bool BuildNegotiator = false)
        {
            #region Ongoing

            if (IsForOnging)
            {
                if (BuildNegotiator)
                    BuildNegotiators(this.mOnGoingNegotiation);

                OnGoingNegotiationSource = new ObservableCollection<Negotiation>(this.mOnGoingNegotiation.OrderBy(s => s.NegotiationName));

                this.RaisePropertyChanged("OnGoingNegotiationSource");
            }
            #endregion Ongoing

            #region Closed

            else
            {
                if (BuildNegotiator)
                    BuildNegotiators(this.mClosedNegotiation);
                ClosedNegotiationSource = new ObservableCollection<Negotiation>(this.mClosedNegotiation);
                this.RaisePropertyChanged("ClosedNegotiationSource");
            }

            #endregion Closed
        }

        #region "Build Negotiators"

        /// <summary>
        /// Build Negotiators display template 
        /// </summary>
        /// <param name="Negs">List of negotations objects</param>
        private void BuildNegotiators(IList<Negotiation> Negs)
        {
            foreach (var item in Negs)
            {
                //Build Negotiatiors for Whole Negotiation
                item.BuildNegotiators();

                foreach (var subitem in item.Conversations)
                {
                    //Build (Negotiators) and (Date of last action) for whole conversation 
                    subitem.BuildFields();
                }

                //Set Date Of Last Action
                item.SetDateOfLastAction();

            }
        }

        #endregion

        /// <summary>
        /// Builds the subject list for conversation.
        /// </summary>
        private void BuildSubjectListForConversation()
        {
            if (mcurrentConversation != null)
            {
                OldMessageSubjects = new List<string>();

                if (mcurrentConversation.Messages.Count > 0)
                {
                    foreach (var msg in mcurrentConversation.Messages.Where(s => !s.IsAppsMessage).OrderByDescending(s => s.DeletedOn))
                    {
                        if (!OldMessageSubjects.Contains(msg.MessageSubject))
                        {
                            OldMessageSubjects.Add(msg.MessageSubject);
                        }
                    }
                    OldMessageSubjects.Sort();
                    OldMessageSubjects = new List<string>(OldMessageSubjects);
                }
            }
        }

        /// <summary>
        /// Called when [loading queue].
        /// </summary>
        /// <param name="loadQueue">The load queue.</param>
        private void OnLoadingQueue(LoadingQueue loadQueue)
        {
            if (loadQueue != null &&
                loadQueue.CurrentNegotiation != null &&
                loadQueue.LoadType == LoadTypes.ClosedNegotiation)
            {
                IArchiveItem archiveYearItem = null;
                IArchiveItem archiveMonthItem = null;

                int year = loadQueue.CurrentNegotiation.CreatedDate.Year;
                int month = loadQueue.CurrentNegotiation.CreatedDate.Month;

                #region → Add Year Item  .

                archiveYearItem = this.ClosedNegotiationArchiveSource
                                      .Where(s => s.ArchiveItemType == ArchiveItemTypes.Year &&
                                                  (int)s.Value == year)
                                      .FirstOrDefault();

                if (archiveYearItem != null)
                {
                    archiveMonthItem = archiveYearItem.Children
                                                      .Where(s => s.ArchiveItemType == ArchiveItemTypes.Month &&
                                                                  (int)s.Value == month)
                                                      .FirstOrDefault();
                }
                else
                {
                    archiveYearItem = new ArchiveItem()
                    {
                        ArchiveItemType = ArchiveItemTypes.Year,
                        IsLoadedOnDemand = false,
                        Name = year.ToString(),
                        Value = year
                    };

                    this.ClosedNegotiationArchiveSource.Add(archiveYearItem);

                    //For refresh tree. In 90 % or more cases 
                    //this year will be exist so no need for this branch of if.
                    this.ClosedNegotiationArchiveSource = new List<IArchiveItem>(this.ClosedNegotiationArchiveSource);
                }

                #endregion

                #region → Add Month Item .

                if (archiveMonthItem == null)
                {
                    archiveMonthItem = new ArchiveItem()
                    {
                        ArchiveItemType = ArchiveItemTypes.Month,
                        IsLoadedOnDemand = true,
                        Name = GetMothName(month),
                        Value = month,
                        Parent = archiveYearItem
                    };

                    archiveYearItem.Children.Add(archiveMonthItem);
                    archiveYearItem.RefreshChildren();
                }

                #endregion

                //Load archive.
                this.GetClosedNegotiationByArchiveAsync(year, month);

                archiveMonthItem.IsLoadedOnDemand = false;

                loadQueue = null;
            }
        }

        /// <summary>
        /// Called when [change screen].
        /// </summary>
        /// <param name="ScreenName">Name of the screen.</param>
        private void OnChangeScreen(string ScreenName)
        {
            if (ScreenName == ViewTypes.AddonView)
            {
                if (CurrentNegotiation == null && CurrentConversation == null)
                {
                    eNegNavigation.NavigateToUrl(AppConfigurations.HostBaseAddress + "Addon.aspx");
                }
                else if (CurrentConversation == null && CurrentNegotiation != null)
                {
                    eNegNavigation.NavigateToUrl(AppConfigurations.HostBaseAddress + "Addon.aspx?NegotiationID=" + CurrentNegotiation.NegotiationID + "&ActionType=Display");
                }
                else if (CurrentConversation != null)
                {
                    eNegNavigation.NavigateToUrl(AppConfigurations.HostBaseAddress + "Addon.aspx?NegotiationID=" + CurrentConversation.Negotiation.NegotiationID + "&ConversationID=" + CurrentConversation.ConversationID + "&ActionType=Display");
                }
            }
            else if (ScreenName == ViewTypes.MainPlatformView)
            {
                if (CurrentNegotiation == null && CurrentConversation == null)
                {
                    eNegNavigation.NavigateToUrl(AppConfigurations.HostBaseAddress + "Default.aspx");
                }
                else if (CurrentConversation == null && CurrentNegotiation != null)
                {
                    eNegNavigation.NavigateToUrl(AppConfigurations.HostBaseAddress + "Default.aspx?NegotiationID=" + CurrentNegotiation.NegotiationID + "&ActionType=Display");
                }
                else if (CurrentConversation != null)
                {
                    eNegNavigation.NavigateToUrl(AppConfigurations.HostBaseAddress + "Default.aspx?NegotiationID=" + CurrentConversation.Negotiation.NegotiationID + "&ConversationID=" + CurrentConversation.ConversationID + "&ActionType=Display");
                }
            }
        }
        #endregion Private

        #region → Public         .

        /// <summary>
        /// Generates the PDF conversations async.
        /// </summary>
        /// <param name="conversationIDs">The conversation I ds.</param>
        public void GeneratePDFConversationsAsync(Guid[] conversationIDs)
        {
            mNegotiationModel.GeneratePDFConversationsAsync(conversationIDs);
        }

        /// <summary>
        /// Generates the PDF messages async.
        /// </summary>
        /// <param name="messsagesIDs">The messsages I ds.</param>
        public void GeneratePDFMessagesAsync(Guid[] messsagesIDs)
        {
            mNegotiationModel.GeneratePDFMessagesAsync(messsagesIDs);
        }

        /// <summary>
        /// Determines whether [is conversation valid] [the specified currrent conv].
        /// </summary>
        /// <param name="conversation">The currrent conv.</param>
        /// <returns>
        /// 	<c>true</c> if [is conversation valid] [the specified currrent conv]; otherwise, <c>false</c>.
        /// </returns>
        public bool IsConversationValid(Conversation conversation)
        {
            if (conversation != null)
            {
                conversation.ValidationErrors.Clear();

                // this should trigger validation even if the NegotionName is not changed and is null
                if (string.IsNullOrWhiteSpace(conversation.ConversationName))
                    conversation.ConversationName = string.Empty;

                if (!(conversation.TryValidateObject() &&
                     conversation.TryValidateProperty("ConversationName")))
                {
                    return false;
                }

                #region → Check Repating .

                Negotiation convNegotiation = conversation.Negotiation;

                if (convNegotiation != null && convNegotiation.Conversations.Count > 0)
                {

                    bool isRepeated = convNegotiation.Conversations
                                                     .Where(s => s.ConversationName.Equals(conversation.ConversationName, StringComparison.InvariantCultureIgnoreCase) &&
                                                                 !s.ConversationID.Equals(conversation.ConversationID))
                                                     .FirstOrDefault() != null;
                    if (isRepeated)
                    {
                        conversation.ValidationErrors.Add(new ValidationResult(Resources.ConversationRepeat, new string[] { "ConversationName" }));
                        return false;
                    }
                }

                #endregion
            }

            return true;
        }

        /// <summary>
        /// Determines whether [is negotiation valid] [the specified negotiation].
        /// </summary>
        /// <param name="negotiation">The negotiation.</param>
        /// <returns>
        /// 	<c>true</c> if [is negotiation valid] [the specified negotiation]; otherwise, <c>false</c>.
        /// </returns>
        public bool IsNegotiationValid(Negotiation negotiation)
        {
            if (negotiation != null)
            {
                // this should trigger validation even if the NegotionName is not changed and is null
                if (string.IsNullOrWhiteSpace(negotiation.NegotiationName))
                    negotiation.NegotiationName = string.Empty;

                if (!(negotiation.TryValidateObject()
                    && negotiation.TryValidateProperty("NegotiationName")))
                {
                    return false;
                }

                //if (negotiation.IsNewNegotiation)
                //    negotiation.IsNewNegotiation = false;

                Negotiation orginalNeg = negotiation.GetOriginal() as Negotiation;

                //When To sort in case if name changed only
                sortNegotiations = (orginalNeg != null && orginalNeg.NegotiationName != negotiation.NegotiationName);

                if (sortNegotiations || negotiation.EntityState == EntityState.New/*in case of add new*/)
                {
                    this.CheckOnNegotiationRepeatAsync(negotiation.NegotiationID, negotiation.NegotiationName, AppConfigurations.CurrentLoginUser.UserID);
                }
                else //Mean nothing to do as no changes in name.
                {
                    //Send message to close pop up
                    CancelPopUpChangesCommand.Execute(ViewTypes.RenameNegotiationConversationPopup);
                }
            }

            return true;
        }

        /// <summary>
        /// Loading Channels 
        /// </summary>
        public void GetChannelsAsync()
        {
            mNegotiationModel.GetChannelAsync();
        }

        /// <summary>
        /// Gets the closed negotiations archive async.
        /// </summary>
        public void GetClosedNegotiationsArchiveAsync()
        {
            this.mNegotiationModel.GetClosedNegotiationsArchiveAsync();
        }

        /// <summary>
        /// Gets the onging negotiations async.
        /// </summary>
        public void GetOngingNegotiationsAsync()
        {
            this.mNegotiationModel.GetOngingNegotiationsAsync();
        }

        /// <summary>
        /// Gets the message by conversation ID async.
        /// </summary>
        /// <param name="conversationID">The conversation ID.</param>
        public void GetMessageByConversationIDAsync(Guid conversationID)
        {
            this.mNegotiationModel.GetMessageByConversationIDAsync(conversationID);
        }

        /// <summary>
        /// Gets the closed negotiation by archive async.
        /// </summary>
        /// <param name="archiveYear">The archive year.</param>
        /// <param name="archiveMonth">The archive month.</param>
        public void GetClosedNegotiationByArchiveAsync(int archiveYear, int archiveMonth)
        {
            this.mNegotiationModel.GetClosedNegotiationByArchiveAsync(archiveYear, archiveMonth);
        }

        /// <summary>
        /// Gets the organizations for user async.
        /// </summary>
        public void GetOrganizationsForUserAsync()
        {
            this.mNegotiationModel.GetOrganizationsForUserAsync();
        }

        /// <summary>
        /// Add New Negotiation
        /// </summary>
        /// <param name="SetInContext">Set new neg object In Context or not</param>
        /// <returns>Negotiation</returns>
        public Negotiation AddNewNegotiation(bool SetInContext)
        {
            return mNegotiationModel.AddNewNegotiation(SetInContext);
        }

        /// <summary>
        /// Add New Conversation
        /// </summary>
        /// <param name="SetInContext">set new conversation In Context or not</param>
        /// <returns>Conversation</returns>
        public Conversation AddNewConversation(bool SetInContext)
        {
            return mNegotiationModel.AddNewConversation(SetInContext);
        }

        /// <summary>
        /// Add New Message
        /// </summary>
        /// <param name="SetInContext">set new message In Context or not</param>
        /// <returns>message</returns>
        public Message AddNewMessage(bool SetInContext)
        {
            return mNegotiationModel.AddNewMessage(SetInContext);
        }

        /// <summary>
        /// Get Data of (Apllications like PrefApp)
        /// </summary>
        public void GetApplicationAsync()
        {
            mNegotiationModel.GetApplicationAsync();
        }

        /// <summary>
        /// Gets Negotiation Application Status asynchronously
        /// </summary>
        /// <param name="NegotiationIDs">Array of Negotiation IDs</param>
        public void GetNegotiationApplicationStatusAsync(Guid[] NegotiationIDs)
        {
            this.mNegotiationModel.GetNegotiationApplicationStatusAsync(NegotiationIDs);
        }

        /// <summary>
        /// Get Conversation By negotation ID asynchronously
        /// </summary>
        /// <param name="negotiationIDs">array of negotations IDs</param>
        public void GetConversationByNegotiationIDAsync(Guid[] negotiationIDs)
        {
            this.mNegotiationModel.GetConversationByNegotiationIDAsync(negotiationIDs);
        }

        /// <summary>
        /// Gets the negotiation organization async.
        /// </summary>
        /// <param name="NegotiationIDs">The negotiation I ds.</param>
        public void GetNegotiationOrganizationAsync(Guid[] NegotiationIDs)
        {
            this.mNegotiationModel.GetNegotiationOrganizationAsync(NegotiationIDs);
        }


        /// <summary>
        /// Checks the on negotiation async.
        /// </summary>
        /// <param name="negotiationID">The negotiation ID.</param>
        /// <param name="negotiationName">Name of the negotiation.</param>
        /// <param name="userID">The user ID.</param>
        public void CheckOnNegotiationRepeatAsync(Guid negotiationID, string negotiationName, Guid userID)
        {
            this.mNegotiationModel.CheckOnNegotiationRepeatAsync(negotiationID, negotiationName, userID);
        }

        /// <summary>
        /// Rejects the changes.
        /// </summary>
        public void RejectChanges(bool IsForOnging)
        {
            mNegotiationModel.RejectChanges();
            if (IsForOnging)
            {
                RefreshData(true, true);
                RefreshData(false, true);
            }
        }

        #region "ICleanup interface implementation"

        /// <summary>
        /// Cleanup negotiation model
        /// </summary>
        public override void Cleanup()
        {
            // unregister all events
            #region → Set up event handling           .

            this.mNegotiationModel.GetClosedNegotiationArchiveComplete -= new EventHandler<eNegEntityResultArgs<NegotiationArchive>>(mNegotiationModel_GetClosedNegotiationArchiveComplete);
            this.mNegotiationModel.GetClosedNegotiationForArchiveComplete -= new EventHandler<eNegEntityResultArgs<Negotiation>>(mNegotiationModel_GetClosedNegotiationForArchiveComplete);
            this.mNegotiationModel.GetOngingNegotiationComplete -= new EventHandler<eNegEntityResultArgs<Negotiation>>(mNegotiationModel_GetOngingNegotiationComplete);
            this.mNegotiationModel.GetConversationByNegotiationIDComplete -= new EventHandler<eNegEntityResultArgs<Conversation>>(mNegotiationModel_GetConversationByNegotiationIDComplete);
            this.mNegotiationModel.GetMessagesByConversationIDsComplete -= new EventHandler<eNegEntityResultArgs<Message>>(mNegotiationModel_GetMessagesByConversationIDsComplete);

            this.mNegotiationModel.SaveChangesComplete -= new EventHandler<SubmitOperationEventArgs>(mNegotiationModel_SaveChangesComplete);
            this.mNegotiationModel.GetAppsActiveForDMComplete -= new EventHandler<eNegEntityResultArgs<UserApplicationStatu>>(mNegotiationModel_GetAppsActiveForDMComplete);
            this.mNegotiationModel.CheckOnNegotiationRepeatComplete -= new Action<InvokeOperation<int?>>(mnegotiationModel_CheckOnNegotiationRepeatComplete);
            this.mNegotiationModel.GetChannelComplete -= new EventHandler<eNegEntityResultArgs<Channel>>(mNegotiationModel_GetChannelComplete);
            this.mNegotiationModel.GetNegotiationApplicationStatusComplete -= new EventHandler<eNegEntityResultArgs<NegotiationApplicationStatu>>(mNegotiationModel_GetNegotiationApplicationStatusComplete);
            this.mNegotiationModel.GetApplicationComplete -= new EventHandler<eNegEntityResultArgs<Data.Web.Application>>(mNegotiationModel_GetApplicationComplete);
            this.mNegotiationModel.GetNegotiationPageNumberComplete -= new Action<InvokeOperation<int?>>(mnegotiationModel_GetNegotiationPageNumberComplete);
            this.mNegotiationModel.PropertyChanged -= new PropertyChangedEventHandler(mNegotiationModel_PropertyChanged);
            this.mNegotiationModel.GetNegotiationOrgaizationComplete -= new EventHandler<eNegEntityResultArgs<NegotiationOrganization>>(mNegotiationModel_GetNegotiationOrgaizationComplete);
            this.mNegotiationModel.GetOrganizationsForUserComplete -= new EventHandler<eNegEntityResultArgs<Organization>>(mNegotiationModel_GetOrganizationsForUserComplete);

            #region Special Event handling To Add New Node In case of addon after loaing all data

            this.mNegotiationModel.GetClosedNegotiationArchiveComplete -= new EventHandler<eNegEntityResultArgs<NegotiationArchive>>(AddNewNegotiationNodeForAddon);
            this.mNegotiationModel.GetClosedNegotiationForArchiveComplete -= new EventHandler<eNegEntityResultArgs<Negotiation>>(AddNewNegotiationNodeForAddon);
            this.mNegotiationModel.GetOngingNegotiationComplete -= new EventHandler<eNegEntityResultArgs<Negotiation>>(AddNewNegotiationNodeForAddon);
            this.mNegotiationModel.GetConversationByNegotiationIDComplete -= new EventHandler<eNegEntityResultArgs<Conversation>>(AddNewNegotiationNodeForAddon);
            this.mNegotiationModel.GetMessagesByConversationIDsComplete -= new EventHandler<eNegEntityResultArgs<Message>>(AddNewNegotiationNodeForAddon);
            this.mNegotiationModel.GetChannelComplete -= new EventHandler<eNegEntityResultArgs<Channel>>(AddNewNegotiationNodeForAddon);
            this.mNegotiationModel.GetNegotiationApplicationStatusComplete -= new EventHandler<eNegEntityResultArgs<NegotiationApplicationStatu>>(AddNewNegotiationNodeForAddon);
            this.mNegotiationModel.GetApplicationComplete -= new EventHandler<eNegEntityResultArgs<Data.Web.Application>>(AddNewNegotiationNodeForAddon);
            this.mNegotiationModel.PropertyChanged -= new PropertyChangedEventHandler(AddNewNegotiationNodeForAddon);

            #endregion Special Event handling To Add New Node In case of addon after loaing all data

            #endregion

            // unregister any messages for this ViewModel
            // Cleanup itself
            Messenger.Default.Unregister(this);

            base.Cleanup();
        }

        #endregion "ICleanup interface implementation"

        #endregion Public

        #endregion Methods

    }
}

