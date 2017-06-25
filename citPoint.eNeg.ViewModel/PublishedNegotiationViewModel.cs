#region → Usings   .
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.ServiceModel.DomainServices.Client;
using System.Windows.Controls;
using citPOINT.eNeg.Common;
using citPOINT.eNeg.Data.Web;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using citPOINT.eNeg.Data;
using citPOINT.eNeg.Common.eNegApps;
#endregion

#region → History  .

/* Date         User         Change
 * 
 * 22.09.11     M.Wahab     • creation
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
    #region  Using MEF to export Published Negotiation  View Model
    /// <summary>
    /// This Class Do all Operations Related To Published Negotiation View Model.
    /// And Sending Mail to the Users.
    /// </summary>
    [Export(ViewModelTypes.PublishedNegotiationViewModel)]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    #endregion
    public class PublishedNegotiationViewModel : ViewModelBase
    {
        #region → Fields         .

        private IPublishedNegotiationModel mPublishedNegotiationModel;

        private IEnumerable<Channel> mChannelSource;
        private IEnumerable<Negotiation> mNegotiationSource;
        private IEnumerable<Conversation> mConversationSource;
        private IEnumerable<Message> mMessageSource;
        private List<IArchiveItem> mNegotiationArchiveSource;

        private List<eNegConstant.NegotiationStatusFilter> mNegotiationStatusFilterSource;
        private List<eNegConstant.NegotiationOwnerFilter> mNegotiationOwnerFilterSource;

        private eNegConstant.NegotiationStatusFilter mNegotiationStatusFilterID = eNegConstant.NegotiationStatusFilter.All;
        private eNegConstant.NegotiationOwnerFilter mNegotiationOwnerFilterID = eNegConstant.NegotiationOwnerFilter.All;

        private bool mIsBusy;
        private object mSelectedItem;

        private Conversation mCurrentConversation;
        private Negotiation mCurrentNegotiation;

        private RelayCommand<IArchiveItem> mNavigateToConversationDetailsCommand;
        private bool mIsPublishedNegotiationsSourceEmpty;


        #endregion

        #region → Properties     .

        /// <summary>
        /// Gets a value indicating whether this instance is published negotiations source empty.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is published negotiations source empty; otherwise, <c>false</c>.
        /// </value>
        public bool IsPublishedNegotiationsSourceEmpty
        {
            get
            {
                return mIsPublishedNegotiationsSourceEmpty;
            }
            set
            {
                mIsPublishedNegotiationsSourceEmpty = value;
                RaisePropertyChanged("IsPublishedNegotiationsSourceEmpty");
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

                if (value != null && (value as IArchiveItem) != null)
                {
                    IArchiveItem selectedArchvItem = (value as IArchiveItem);

                    switch (selectedArchvItem.ArchiveItemType)
                    {
                        case ArchiveItemTypes.Year:
                            this.CurrentConversation = null;
                            this.CurrentNegotiation = null;

                            eNegMessanger.ApplyChangesForAppsMessage.Send
                                (
                                     new ApplicationChange()
                                     {
                                         IsForUserChanges = false,
                                         ApplyChanged = true,
                                         CurrentConversation = this.CurrentConversation,
                                         CurrentNegotiation = this.CurrentNegotiation
                                     }
                                );

                            break;
                        case ArchiveItemTypes.Month:
                            {
                                this.CurrentConversation = null;
                                this.CurrentNegotiation = null;

                                eNegMessanger.ApplyChangesForAppsMessage.Send
                                    (
                                         new ApplicationChange()
                                         {
                                             IsForUserChanges = false,
                                             ApplyChanged = true,
                                             CurrentConversation = this.CurrentConversation,
                                             CurrentNegotiation = this.CurrentNegotiation
                                         }
                                    );

                                if (selectedArchvItem.IsLoadedOnDemand)
                                {
                                    this.GetPublishedNegotiationByArchiveAsync(
                                        (int)selectedArchvItem.Parent.Value,
                                        (int)selectedArchvItem.Value);
                                }

                                break;
                            }
                        case ArchiveItemTypes.Negotiation:

                            this.CurrentConversation = null;
                            this.CurrentNegotiation = ((this.SelectedItem as IArchiveItem).Value as Negotiation);

                            eNegMessanger.ApplyChangesForAppsMessage.Send
                                (
                                     new ApplicationChange()
                                     {
                                         IsForUserChanges = false,
                                         ApplyChanged = true,
                                         CurrentConversation = this.CurrentConversation,
                                         CurrentNegotiation = this.CurrentNegotiation
                                     }
                                );

                            eNegMessanger.FlippMessage.Send(ViewTypes.NegotiationDetailsForPublishedView);
                            break;
                        case ArchiveItemTypes.Conversation:
                            {
                                this.CurrentNegotiation = null;
                                this.CurrentConversation = ((this.SelectedItem as IArchiveItem).Value as Conversation);

                                NavigateToConversationDetailsCommand.Execute(selectedArchvItem);

                                break;
                            }
                        default:
                            break;
                    }
                }

                this.RaisePropertyChanged("SelectedItem");
            }
        }


        /// <summary>
        /// Gets or sets the current negotiation.
        /// </summary>
        /// <value>The current negotiation.</value>
        public Negotiation CurrentNegotiation
        {
            get { return mCurrentNegotiation; }
            set
            {
                if (mCurrentNegotiation != value)
                {
                    mCurrentNegotiation = value;

                    eNegMessanger.EditNegotiationMessage.Send(value);

                    this.ConversationVM.CurrentNegotiation = value;

                    this.RaisePropertyChanged("CurrentNegotiation");
                }
            }
        }

        /// <summary>
        /// Gets or sets the current conversation.
        /// </summary>
        /// <value>The current conversation.</value>
        public Conversation CurrentConversation
        {
            get { return mCurrentConversation; }
            set
            {
                if (mCurrentConversation != value)
                {
                    mCurrentConversation = value;
                    eNegMessanger.EditConversationMessage.Send(value);
                    this.MessageVM.CurrentConversation = value;

                    this.RaisePropertyChanged("CurrentConversation");
                }
            }
        }

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
        /// Gets or sets the negotiation status filter ID.
        /// </summary>
        /// <value>The negotiation status filter ID.</value>
        public eNegConstant.NegotiationStatusFilter NegotiationStatusFilterID
        {
            get
            {
                return mNegotiationStatusFilterID;
            }
            set
            {
                if (mNegotiationStatusFilterID != value)
                {
                    mNegotiationStatusFilterID = value;
                    this.ReLoadArchive();
                    this.RaisePropertyChanged("NegotiationStatusFilterID");
                }
            }
        }

        /// <summary>
        /// Gets or sets the negotiation owner filter ID.
        /// </summary>
        /// <value>The negotiation owner filter ID.</value>
        public eNegConstant.NegotiationOwnerFilter NegotiationOwnerFilterID
        {
            get
            {
                return mNegotiationOwnerFilterID;
            }
            set
            {
                if (mNegotiationOwnerFilterID != value)
                {
                    mNegotiationOwnerFilterID = value;

                    this.ReLoadArchive();

                    this.RaisePropertyChanged("NegotiationOwnerFilterID");
                }
            }
        }

        /// <summary>
        /// Gets the negotiation status filter source.
        /// </summary>
        /// <value>The negotiation status filter source.</value>
        public List<eNegConstant.NegotiationStatusFilter> NegotiationStatusFilterSource
        {
            get
            {
                if (mNegotiationStatusFilterSource == null)
                {
                    mNegotiationStatusFilterSource = new List<eNegConstant.NegotiationStatusFilter>();
                    mNegotiationStatusFilterSource.Add(eNegConstant.NegotiationStatusFilter.All);
                    mNegotiationStatusFilterSource.Add(eNegConstant.NegotiationStatusFilter.Ongoing);
                    mNegotiationStatusFilterSource.Add(eNegConstant.NegotiationStatusFilter.Closed);
                }

                return mNegotiationStatusFilterSource;
            }
        }

        /// <summary>
        /// Gets the negotiation status filter source.
        /// </summary>
        /// <value>The negotiation status filter source.</value>
        public List<eNegConstant.NegotiationOwnerFilter> NegotiationOwnerFilterSource
        {
            get
            {
                if (mNegotiationOwnerFilterSource == null)
                {
                    mNegotiationOwnerFilterSource = new List<eNegConstant.NegotiationOwnerFilter>();
                    mNegotiationOwnerFilterSource.Add(eNegConstant.NegotiationOwnerFilter.All);
                    mNegotiationOwnerFilterSource.Add(eNegConstant.NegotiationOwnerFilter.MyNegotiations);
                    mNegotiationOwnerFilterSource.Add(eNegConstant.NegotiationOwnerFilter.OthersNegotiatons);
                }

                return mNegotiationOwnerFilterSource;
            }
        }

        /// <summary>
        /// Gets or sets the channel source.
        /// </summary>
        /// <value>The channel source.</value>
        public IEnumerable<Channel> ChannelSource
        {
            get
            {
                return mChannelSource;
            }
            set
            {
                mChannelSource = value;
                this.RaisePropertyChanged("ChannelSource");
            }
        }

        /// <summary>
        /// Gets or sets the negotiation source.
        /// </summary>
        /// <value>The negotiation source.</value>
        public IEnumerable<Negotiation> NegotiationSource
        {
            get
            {
                return mNegotiationSource;
            }
            set
            {
                mNegotiationSource = value;
                this.RaisePropertyChanged("NegotiationSource");
            }
        }

        /// <summary>
        /// Gets or sets the conversation source.
        /// </summary>
        /// <value>The conversation source.</value>
        public IEnumerable<Conversation> ConversationSource
        {
            get
            {
                return mConversationSource;
            }
            set
            {
                mConversationSource = value;
                this.RaisePropertyChanged("ConversationSource");
            }
        }

        /// <summary>
        /// Gets or sets the message source.
        /// </summary>
        /// <value>The message source.</value>
        public IEnumerable<Message> MessageSource
        {
            get
            {
                return mMessageSource;
            }
            set
            {
                mMessageSource = value;
                this.RaisePropertyChanged("MessageSource");
            }
        }

        /// <summary>
        /// Gets or sets the negotiation archive source.
        /// </summary>
        /// <value>The negotiation archive source.</value>
        public List<IArchiveItem> NegotiationArchiveSource
        {
            get
            {
                return mNegotiationArchiveSource;
            }
            set
            {
                mNegotiationArchiveSource = value;

                this.RaisePropertyChanged("NegotiationArchiveSource");
            }
        }

        #endregion

        #region → Constructors   .

        /// <summary>
        /// Initializes a new instance of the <see cref="PublishedNegotiationViewModel"/> class.
        /// </summary>
        /// <param name="PublishedNegotiationModel">The manage organization model.</param>
        [ImportingConstructor]
        public PublishedNegotiationViewModel(IPublishedNegotiationModel PublishedNegotiationModel)
        {
            this.mPublishedNegotiationModel = PublishedNegotiationModel;

            this.MessageVM = new MessageViewModel();
            this.MessageVM.IsForPublishedNegotiation = true;

            this.ConversationVM = new ConversationViewModel();
            this.ConversationVM.IsForPublishedNegotiation = true;

            #region → Setup event handling   .

            this.mPublishedNegotiationModel.PropertyChanged += new PropertyChangedEventHandler(mPublishedNegotiationModel_PropertyChanged);
            this.mPublishedNegotiationModel.GetChannelComplete += new EventHandler<eNegEntityResultArgs<Channel>>(mPublishedNegotiationModel_GetChannelComplete);
            this.mPublishedNegotiationModel.GetPublishedNegotiationArchiveComplete += new EventHandler<eNegEntityResultArgs<NegotiationArchive>>(mPublishedNegotiationModel_GetPublishedNegotiationArchiveComplete);
            this.mPublishedNegotiationModel.GetPublishedNegotiationComplete += new EventHandler<eNegEntityResultArgs<Negotiation>>(mPublishedNegotiationModel_GetPublishedNegotiationComplete);
            this.mPublishedNegotiationModel.GetMessagesByConversationIDsComplete += new EventHandler<eNegEntityResultArgs<Message>>(mPublishedNegotiationModel_GetMessagesByConversationIDsComplete);
            this.mPublishedNegotiationModel.GetConversationByNegIDComplete += new EventHandler<eNegEntityResultArgs<Conversation>>(mPublishedNegotiationModel_GetConversationByNegIDComplete);

            #endregion


            eNegMessanger.LoadingQueueMessage.Register(this, OnLoadingQueue);

            #region → Loading Related Tables .

            this.GetChannelAsync();

            this.GetPublishedNegotiationArchiveAsync();

            #endregion
        }

        #endregion

        #region → Event Handlers .

        #region →  Loading Event Handlers .

        /// <summary>
        /// Call back of Get channel.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        private void mPublishedNegotiationModel_GetChannelComplete(object sender, eNegEntityResultArgs<Channel> e)
        {
            if (!e.HasError)
            {
                this.ChannelSource = e.Results;
            }
            else
            {
                // notify user if there is any error
                eNegMessanger.RaiseErrorMessage.Send(e.Error);
            }
        }

        /// <summary>
        /// Ms the published negotiation model_ get published negotiation archive complete.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        private void mPublishedNegotiationModel_GetPublishedNegotiationArchiveComplete(object sender, eNegEntityResultArgs<NegotiationArchive> e)
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

                    this.NegotiationArchiveSource = negArchiveSource;

                    this.IsPublishedNegotiationsSourceEmpty = this.NegotiationArchiveSource == null || this.NegotiationArchiveSource.Count == 0;

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
        /// Call back of Get published negotiation.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        private void mPublishedNegotiationModel_GetPublishedNegotiationComplete(object sender, eNegEntityResultArgs<Negotiation> e)
        {
            if (!e.HasError)
            {
                try
                {
                    this.NegotiationSource = e.Results;

                    this.ConversationSource = null;

                    #region → Loading Related (Conversation-Messages .

                    List<Guid> NegotiationIDs = new List<Guid>();

                    foreach (var negotiationItem in this.NegotiationSource)
                    {
                        NegotiationIDs.Add(negotiationItem.NegotiationID);


                        IArchiveItem archItem =
                        this.NegotiationArchiveSource
                                                 .Where(s => s.ArchiveItemType == ArchiveItemTypes.Year &&
                                                             s.Value.ToString() == negotiationItem.CreatedDate.Year.ToString())


                                                 .FirstOrDefault()
                                                 .Children.Where(d => d.ArchiveItemType == ArchiveItemTypes.Month &&
                                                                      d.Value.ToString() == negotiationItem.CreatedDate.Month.ToString())
                                                 .FirstOrDefault();


                        if (archItem != null)
                        {
                            (archItem.Children as List<IArchiveItem>).Add(negotiationItem);
                        }
                    }

                    //Getting Conversation
                    this.GetConversationByNegIDAsync(NegotiationIDs.ToArray<Guid>());

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
        /// Call Back of Get Conversations by negotiation Ids.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        private void mPublishedNegotiationModel_GetConversationByNegIDComplete(object sender, eNegEntityResultArgs<Conversation> e)
        {
            if (!e.HasError)
            {
                this.ConversationSource = e.Results;

                this.ConversationVM.RefreshConversationsSource();

                eNegMessanger.DoOperationMessage.Send(eNegMessanger.OperationType.NegotiationArchiveLoaded);
            }
            else
            {
                // notify user if there is any error
                eNegMessanger.RaiseErrorMessage.Send(e.Error);
            }
        }

        /// <summary>
        /// Ms the published negotiation model_ get messages by conversation I ds complete.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        private void mPublishedNegotiationModel_GetMessagesByConversationIDsComplete(object sender, eNegEntityResultArgs<Message> e)
        {
            if (!e.HasError)
            {
                this.MessageSource = new List<Message>(e.Results.OrderByDescending(s => s.MessageDate));

                this.MessageVM.RefreshMessagesSource();
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
        private void mPublishedNegotiationModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals("HasChanges") || e.PropertyName.Equals("IsBusy"))
            {
                this.IsBusy = mPublishedNegotiationModel.IsBusy;

                this.MessageVM.IsBusy = this.IsBusy;
                this.ConversationVM.IsBusy = this.IsBusy;
            }
        }

        #endregion

        #region → Commands       .

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
                            eNegMessanger.ApplyChangesForAppsMessage.Send
                                (
                                     new ApplicationChange()
                                     {
                                         IsForUserChanges = false,
                                         ApplyChanged = true,
                                         CurrentConversation = this.CurrentConversation,
                                         CurrentNegotiation = this.CurrentConversation.Negotiation
                                     }
                                );

                            if (currentItem.IsLoadedOnDemand)
                            {
                                this.GetMessageByConversationIDAsync(((this.SelectedItem as IArchiveItem).Value as Conversation).ConversationID);

                                currentItem.IsLoadedOnDemand = false;
                            }
                            else
                            {
                                this.MessageVM.RefreshMessagesSource();
                            }

                            eNegMessanger.FlippMessage.Send(ViewTypes.ConversationDetailsForPublishedView);


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

        #endregion

        #region → Methods        .

        #region → Private        .

        /// <summary>
        /// Res the load archive.
        /// </summary>
        private void ReLoadArchive()
        {
            //Speacial case for pubkish closed or open negotiation.
            if (ReloadArchive)
            {
                #region → clear the selected Node .

                this.CurrentConversation = null;
                this.CurrentNegotiation = null;

                eNegMessanger.ApplyChangesForAppsMessage.Send
                    (
                         new ApplicationChange()
                         {
                             IsForUserChanges = false,
                             ApplyChanged = false,
                             CurrentConversation = this.CurrentConversation,
                             CurrentNegotiation = this.CurrentNegotiation
                         }
                    );

                #endregion

                this.GetPublishedNegotiationArchiveAsync();
            }
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

        private bool ReloadArchive = true;

        /// <summary>
        /// Called when [loading queue].
        /// </summary>
        /// <param name="loadQueue">The load queue.</param>
        private void OnLoadingQueue(LoadingQueue loadQueue)
        {
            if (loadQueue != null &&
                loadQueue.CurrentNegotiation != null &&
                loadQueue.LoadType == LoadTypes.PublishedNegotiation)
            {
                this.ReloadArchive = false;
                this.NegotiationOwnerFilterID = eNegConstant.NegotiationOwnerFilter.All;
                
                this.NegotiationStatusFilterID = eNegConstant.NegotiationStatusFilter.All;

                this.ReloadArchive = true;

                this.ReLoadArchive();
            }
        }

        #endregion

        #region → Public         .

        /// <summary>
        /// Gets the channel async.
        /// </summary>
        public void GetChannelAsync()
        {
            this.mPublishedNegotiationModel.GetChannelAsync();
        }

        /// <summary>
        /// Gets the published negotiation archive async.
        /// </summary>
        /// <param name="negStatusFilter">The neg status filter.</param>
        /// <param name="negOwnerFilter">The neg owner filter.</param>
        public void GetPublishedNegotiationArchiveAsync()
        {
            this.mPublishedNegotiationModel.GetPublishedNegotiationArchiveAsync(this.NegotiationStatusFilterID, this.NegotiationOwnerFilterID);
        }

        /// <summary>
        /// Gets the published negotiation by archive.
        /// </summary>
        /// <param name="archiveYear">The archive year.</param>
        /// <param name="archiveMonth">The archive month.</param>
        /// <param name="NegStatusFilter">The neg status filter.</param>
        /// <param name="NegOwnerFilter">The neg owner filter.</param>
        public void GetPublishedNegotiationByArchiveAsync(int archiveYear, int archiveMonth)
        {
            this.mPublishedNegotiationModel.GetPublishedNegotiationByArchiveAsync(archiveYear, archiveMonth, this.NegotiationStatusFilterID, this.NegotiationOwnerFilterID);
        }

        /// <summary>
        /// Gets the message by conversation ID async.
        /// </summary>
        /// <param name="conversationID">The conversation ID.</param>
        public void GetMessageByConversationIDAsync(Guid conversationID)
        {
            mPublishedNegotiationModel.GetMessageByConversationIDAsync(conversationID);
        }

        /// <summary>
        /// Gets the conversation by neg ID async.
        /// </summary>
        /// <param name="NegotiationIDs">The negotiation I ds.</param>
        public void GetConversationByNegIDAsync(Guid[] NegotiationIDs)
        {
            mPublishedNegotiationModel.GetConversationByNegIDAsync(NegotiationIDs.ToArray<Guid>());
        }

        #region → ICleanup interface implementation .

        /// <summary>
        /// Cleanups this instance.
        /// </summary>
        public override void Cleanup()
        {
            // Unregister all events
            this.mPublishedNegotiationModel.PropertyChanged -= new PropertyChangedEventHandler(mPublishedNegotiationModel_PropertyChanged);
            this.mPublishedNegotiationModel.GetChannelComplete -= new EventHandler<eNegEntityResultArgs<Channel>>(mPublishedNegotiationModel_GetChannelComplete);
            this.mPublishedNegotiationModel.GetPublishedNegotiationArchiveComplete -= new EventHandler<eNegEntityResultArgs<NegotiationArchive>>(mPublishedNegotiationModel_GetPublishedNegotiationArchiveComplete);
            this.mPublishedNegotiationModel.GetPublishedNegotiationComplete -= new EventHandler<eNegEntityResultArgs<Negotiation>>(mPublishedNegotiationModel_GetPublishedNegotiationComplete);
            this.mPublishedNegotiationModel.GetMessagesByConversationIDsComplete -= new EventHandler<eNegEntityResultArgs<Message>>(mPublishedNegotiationModel_GetMessagesByConversationIDsComplete);
            this.mPublishedNegotiationModel.GetConversationByNegIDComplete -= new EventHandler<eNegEntityResultArgs<Conversation>>(mPublishedNegotiationModel_GetConversationByNegIDComplete);

            // unregister any messages for this ViewModel
            base.Cleanup();

            // unregister any messages for this ViewModel
            // Cleanup itself
            Messenger.Default.Unregister(this);

        }

        #endregion

        #endregion

        #endregion
    }
}