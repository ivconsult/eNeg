
#region → Usings   .
using System;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.ServiceModel.DomainServices.Client;
using citPOINT.eNeg.Common;
using citPOINT.eNeg.Data.Web;
using System.Collections.Generic;
using System.ServiceModel;

#endregion

#region → History  .

/* Date         User            Change
 * 
 * 16.08.10     M.Wahab         • creation
 * 31.08.10     Dergham         • Update XML Documentation
 * 05.09.10     Degrham         • Add "AddNewMessage" method
 * 
 */

# endregion

#region → ToDos    .

/*
 * Date         set by User     Description
 * 
 * 
*/

# endregion

namespace citPOINT.eNeg.Common
{

    #region " Using MEF to export NegotiationViewModel "
    /// <summary>
    /// Mopdel Layer for handling any operation related to Negotiaon, Conversation, or Message  
    /// </summary>
    [Export(typeof(INegotiationModel))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    #endregion
    public class NegotiationModel : ModelBase, INegotiationModel
    {
        #region → Fields         .

        private eNegContext mNegContext;

        private Boolean mHasChanges = false;
        private Boolean mIsBusy = false;

        DateTime LastActionDate = DateTime.Now;
        bool SkipCheckTime = false;

        #endregion

        #region → Properties     .

        /// <summary>
        /// Context of Service eNegService
        /// </summary>
        public eNegContext Context
        {
            get
            {
                if (mNegContext == null)
                {

                    mNegContext = new eNegContext();
                    mNegContext.PropertyChanged += new PropertyChangedEventHandler(meNegContext_PropertyChanged);
                }

                return mNegContext;
            }
        }

        /// <summary>
        /// Gets all negotiation.
        /// </summary>
        /// <value>All negotiation.</value>
        public IEnumerable<Negotiation> AllNegotiation
        {
            get
            {
                return this.Context.Negotiations.ToArray();
            }
        }

        /// <summary>
        /// True if Domain context Has Changes ;otherwise false
        /// </summary>
        public Boolean HasChanges
        {
            get
            {

                return this.mHasChanges;
            }

            private set
            {
                if (this.mHasChanges != value)
                {
                    this.mHasChanges = value;
                    this.OnPropertyChanged("HasChanges");
                }
            }
        }

        /// <summary>
        /// True if either "IsLoading" or "IsSubmitting" is
        /// in progress; otherwise, false
        /// </summary>
        public Boolean IsBusy
        {
            get
            {
                return this.mIsBusy;
            }

            private set
            {
                if (this.mIsBusy != value)
                {
                    this.mIsBusy = value;
                    this.OnPropertyChanged("IsBusy");
                }
            }
        }

        #endregion Properties

        #region → Event Handlers .

        /// <summary>
        /// Executed when any property of Domain context is changed
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">PropertyChangedEventArgs</param>
        private void meNegContext_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "HasChanges":
                    this.HasChanges = mNegContext.HasChanges;
                    break;
                case "IsLoading":
                    this.IsBusy = mNegContext.IsLoading;
                    break;
                case "IsSubmitting":
                    this.IsBusy = mNegContext.IsSubmitting;
                    break;
            }
        }
        #endregion

        #region → Events         .

        /// <summary>
        /// Occurs when [generate PDF complete].
        /// </summary>
        public event Action<InvokeOperation<byte[]>> GeneratePDFComplete;

        /// <summary>
        /// Occurs when [get closed negotiation archive complete].
        /// </summary>
        public event EventHandler<eNegEntityResultArgs<NegotiationArchive>> GetClosedNegotiationArchiveComplete;

        /// <summary>
        /// Occurs when [get closed negotiation for archive complete].
        /// </summary>
        public event EventHandler<eNegEntityResultArgs<Negotiation>> GetClosedNegotiationForArchiveComplete;

        /// <summary>
        /// Occurs when [get conversation by neg ID complete].
        /// </summary>
        public event EventHandler<eNegEntityResultArgs<Conversation>> GetConversationByNegotiationIDComplete;

        /// <summary>
        /// Occurs when [get onging negotiation complete].
        /// </summary>
        public event EventHandler<eNegEntityResultArgs<Negotiation>> GetOngingNegotiationComplete;

        /// <summary>
        /// Occurs when [get messages by conversation Ids complete].
        /// </summary>
        public event EventHandler<eNegEntityResultArgs<Message>> GetMessagesByConversationIDsComplete;

        /// <summary>
        /// Occurs when [get negotiation orgaization complete].
        /// </summary>
        public event EventHandler<eNegEntityResultArgs<NegotiationOrganization>> GetNegotiationOrgaizationComplete;

        /// <summary>
        /// Occurs when [get organizations for user complete].
        /// </summary>
        public event EventHandler<eNegEntityResultArgs<Organization>> GetOrganizationsForUserComplete;

        /// <summary>
        /// Occurs when [get apps active for DM complete].
        /// </summary>
        public event EventHandler<eNegEntityResultArgs<UserApplicationStatu>> GetAppsActiveForDMComplete;

        /// <summary>
        /// Occurs when [check on negotiation repeat complete].
        /// </summary>
        public event Action<InvokeOperation<int?>> CheckOnNegotiationRepeatComplete;

        /// <summary>
        /// GetApplicationComplete
        /// </summary>
        public event EventHandler<eNegEntityResultArgs<Application>> GetApplicationComplete;

        /// <summary>
        /// GetAttachementComplete
        /// </summary>
        public event EventHandler<eNegEntityResultArgs<Attachement>> GetAttachementComplete;

        /// <summary>
        /// GetChannelComplete
        /// </summary>
        public event EventHandler<eNegEntityResultArgs<Channel>> GetChannelComplete;

        /// <summary>
        /// GetChannelTypeComplete
        /// </summary>
        public event EventHandler<eNegEntityResultArgs<ChannelType>> GetChannelTypeComplete;

        /// <summary>
        /// GetNegotiationApplicationStatusComplete
        /// </summary>
        public event EventHandler<eNegEntityResultArgs<NegotiationApplicationStatu>> GetNegotiationApplicationStatusComplete;

        /// <summary>
        /// GetNegotiationStatusComplete
        /// </summary>
        public event EventHandler<eNegEntityResultArgs<NegotiationStatu>> GetNegotiationStatusComplete;

        /// <summary>
        /// Occurs when [get negotiation page number complete].
        /// </summary>
        public event Action<InvokeOperation<int?>> GetNegotiationPageNumberComplete;

        /// <summary>
        /// SaveChangesComplete
        /// </summary>
        public event EventHandler<SubmitOperationEventArgs> SaveChangesComplete;

        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region → Methods        .

        #region → Private        .

        /// <summary>
        /// Private Method used to perform query on certain entity of eNeg Entities
        /// </summary>
        /// <typeparam name="T">Value Of T</typeparam>
        /// <param name="qry">Value Of qry</param>
        /// <param name="evt">Value Of evt</param>
        private void PerformQuery<T>(EntityQuery<T> qry, EventHandler<eNegEntityResultArgs<T>> evt) where T : Entity
        {

            Context.Load<T>(qry, LoadBehavior.RefreshCurrent, r =>
            {
                if (evt != null)
                {
                    try
                    {
                        if (r.HasError)
                        {
                            evt(this, new eNegEntityResultArgs<T>(r.Error));
                            r.MarkErrorAsHandled();
                        }
                        else
                        {
                            evt(this, new eNegEntityResultArgs<T>(r.Entities));
                        }
                    }
                    catch (Exception ex)
                    {
                        evt(this, new eNegEntityResultArgs<T>(ex));
                    }
                }
            }, null);
        }

        /// <summary>
        /// Gets the name of the item.
        /// </summary>
        /// <param name="PreName">pre Name e.g. Neg,Conv.</param>
        /// <returns></returns>
        private string GetItemName(string PreName)
        {
            TimeSpan span = DateTime.Now.Subtract(LastActionDate);

            if (span.TotalSeconds < 1 && (!SkipCheckTime))
            {
                System.Threading.Thread.Sleep(1000 - (int)span.TotalMilliseconds);
            }

            LastActionDate = DateTime.Now;

            return PreName + DateTime.Now.ToString(" (MM/dd/yyyy HH:mm:ss)");
        }

        #endregion

        #region → Protected      .

        #region "INotifyPropertyChanged Interface implementation"

        /// <summary>
        ///  called On Property Changed
        /// </summary>
        /// <param name="propertyName">property name</param>
        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion "INotifyPropertyChanged Interface implementation"

        #endregion

        #region → Public         .

        #region "INegotationModel Interface implementation"

        /// <summary>
        /// Generates the PDF conversations async.
        /// </summary>
        /// <param name="conversationIDs">The conversation I ds.</param>
        public void GeneratePDFConversationsAsync(Guid[] conversationIDs)
        {
            Context.GeneratePDFConversations(conversationIDs, GeneratePDFComplete, null);
        }

        /// <summary>
        /// Generates the PDF messages async.
        /// </summary>
        /// <param name="MessagesIDs">The messages I ds.</param>
        public void GeneratePDFMessagesAsync(Guid[] messagesIDs)
        {
            Context.GeneratePDFMessages(messagesIDs, GeneratePDFComplete, null);
        }

        /// <summary>
        /// Gets the closed negotiation archive async.
        /// </summary>
        public void GetClosedNegotiationsArchiveAsync()
        {
            var qry = Context.GetClosedNegotiationArchiveQuery(AppConfigurations.CurrentLoginUser.UserID);

            PerformQuery<NegotiationArchive>(qry, GetClosedNegotiationArchiveComplete);
        }

        /// <summary>
        /// Gets the onging negotiations async.
        /// </summary>
        public void GetOngingNegotiationsAsync()
        {
            // Generate the query with paging
            var qry = Context.GetNegotiationsQuery()
                             .Where(s => s.StatusID == eNegConstant.NegotiationStatus.Ongoing &&
                                         s.UserID == AppConfigurations.CurrentLoginUser.UserID &&
                                         s.Deleted == false)
                             .OrderBy(s => s.NegotiationName);

            // Execute the Ongoing Neg. query
            this.PerformQuery<Negotiation>(qry, GetOngingNegotiationComplete);
        }

        /// <summary>
        /// Gets the closed negotiation by archive async.
        /// </summary>
        /// <param name="archiveYear">The archive year.</param>
        /// <param name="archiveMonth">The archive month.</param>
        public void GetClosedNegotiationByArchiveAsync(int archiveYear, int archiveMonth)
        {
            var qry = Context.GetClosedNegoiationsForArchiveQuery(AppConfigurations.CurrentLoginUser.UserID, archiveYear, archiveMonth);

            this.PerformQuery<Negotiation>(qry, GetClosedNegotiationForArchiveComplete);
        }

        /// <summary>
        /// Gets the conversation by negotiation ID async.
        /// </summary>
        /// <param name="negotiationIDs">The negotiation I ds.</param>
        public void GetConversationByNegotiationIDAsync(Guid[] negotiationIDs)
        {
            var qry = this.Context
                          .GetConversationsByNegotiationIDQuery(negotiationIDs)
                          .OrderBy(s => s.ConversationName);

            PerformQuery<Conversation>(qry, GetConversationByNegotiationIDComplete);
        }

        /// <summary>
        /// Gets the message by conversation ID async.
        /// </summary>
        /// <param name="conversationID">The conversation ID.</param>
        public void GetMessageByConversationIDAsync(Guid conversationID)
        {
            PerformQuery<Message>(Context.GetMessagesQuery()
                             .Where(s => s.Deleted == false &&
                                         s.ConversationID == conversationID)
                             .OrderByDescending(s => s.MessageDate),
                             GetMessagesByConversationIDsComplete);
        }

        /// <summary>
        /// Gets the negotiation organization async.
        /// </summary>
        /// <param name="NegotiationIDs">The negotiation I ds.</param>
        public void GetNegotiationOrganizationAsync(Guid[] NegotiationIDs)
        {
            PerformQuery<NegotiationOrganization>(Context.GetNegotiationOrganizationsQuery(NegotiationIDs), GetNegotiationOrgaizationComplete);
        }

        /// <summary>
        /// Gets the organizations for user async.
        /// </summary>
        public void GetOrganizationsForUserAsync()
        {
            PerformQuery<Organization>(Context.GetOrganizationsForUserQuery(AppConfigurations.CurrentLoginUser.UserID), GetOrganizationsForUserComplete);
        }

        /// <summary>
        /// Gets the active apps for DM.
        /// </summary>
        public void GetAppsActiveForDM()
        {
            PerformQuery<UserApplicationStatu>(Context.GetAppsActiveForDMQuery(AppConfigurations.CurrentLoginUser.UserID), GetAppsActiveForDMComplete);
        }

        /// <summary>
        /// Gets application asynchronously
        /// </summary>
        public void GetApplicationAsync()
        {
            PerformQuery<Application>(Context.GetApplicationsQuery(), GetApplicationComplete);
        }

        /// <summary>
        /// Gets attachements asynchronously
        /// </summary>
        public void GetAttachementAsync()
        {
            PerformQuery<Attachement>(Context.GetAttachementsQuery(), GetAttachementComplete);
        }

        /// <summary>
        /// Gets channels asynchronously
        /// </summary>
        public void GetChannelAsync()
        {
            PerformQuery<Channel>(Context.GetChannelsQuery(), GetChannelComplete);
        }

        /// <summary>
        /// Gets channel types asynchronously
        /// </summary>
        public void GetChannelTypeAsync()
        {
            PerformQuery<ChannelType>(Context.GetChannelTypesQuery(), GetChannelTypeComplete);
        }

        /// <summary>
        /// Gets Negotiation Application Status asynchronously
        /// </summary>
        /// <param name="NegotiationIDs">array if negotations IDs</param>
        public void GetNegotiationApplicationStatusAsync(Guid[] NegotiationIDs)
        {
            PerformQuery<NegotiationApplicationStatu>(Context.GetNegotiationApplicationStatusByNegotiationIDQuery(NegotiationIDs), GetNegotiationApplicationStatusComplete);
        }

        /// <summary>
        /// Gets negotiation status asynchronously
        /// </summary>
        public void GetNegotiationStatusAsync()
        {
            PerformQuery<NegotiationStatu>(Context.GetNegotiationStatusQuery(), GetNegotiationStatusComplete);
        }

        /// <summary>
        /// Checks the on negotiation.
        /// </summary>
        /// <param name="negotiationID">The negotiation ID.</param>
        /// <param name="negotiationName">Name of the negotiation.</param>
        /// <param name="userID">The user ID.</param>
        public void CheckOnNegotiationRepeatAsync(Guid negotiationID, string negotiationName, Guid userID)
        {
            Context.CheckOnNegotiationRepeat(negotiationID, negotiationName, userID, CheckOnNegotiationRepeatComplete, null);
        }

        /// <summary>
        /// Adds the negotiation organization.
        /// </summary>
        /// <param name="Neg">The neg.</param>
        /// <param name="orga">The orga.</param>
        /// <param name="SetInContext">if set to <c>true</c> [set in context].</param>
        /// <returns>Entity for Negotiation organization</returns>
        public NegotiationOrganization AddNegotiationOrganization(Negotiation Neg, Organization orga, bool SetInContext)
        {
            NegotiationOrganization NegOrga = new NegotiationOrganization()
            {
                NegotiationOrganizationID = Guid.NewGuid(),
                Negotiation = Neg,
                Organization = orga,
                NegotiationID = Neg.NegotiationID,
                OrganizationID = orga.OrganizationID,
                Deleted = false,
                DeletedOn = DateTime.Now,
                DeletedBy = AppConfigurations.CurrentLoginUser.UserID
            };

            if (SetInContext)
            {
                this.Context.NegotiationOrganizations.Add(NegOrga);
            }
            return NegOrga;
        }

        /// <summary>
        /// Add new negotiation
        /// </summary>
        /// <param name="SetInContext">to set negotaion object in Context or not</param>
        /// <returns>Added Negotiation</returns>
        public Negotiation AddNewNegotiation(bool SetInContext)
        {

            #region Add New Negotiation ....................

            Negotiation Neg = new Negotiation()
            {
                CreatedDate = DateTime.Now,
                Deleted = false,
                DeletedDate = DateTime.Now,
                NegotiationID = Guid.NewGuid(),
                NegotiationName = GetItemName("Neg"),
                StatusID = eNegConstant.NegotiationStatus.Ongoing,
                UserID = AppConfigurations.CurrentLoginUser.UserID,
                IsNewNegotiation = true
            };

            if (SetInContext)
            {
                this.Context.Negotiations.Add(Neg);
            }

            #endregion Add New Negotiation

            #region Add New Conversation to this Negotiation
            SkipCheckTime = true;
            Conversation Conv = AddNewConversation(true);
            Conv.NegotiationID = Neg.NegotiationID;
            Neg.Conversations.Add(Conv);
            SkipCheckTime = false;
            #endregion Add New Conversation to this Negotiation

            #region Build Application to this Negotiation...

            if (SetInContext)
            {
                foreach (var item in this.Context.Applications)
                {
                    NegotiationApplicationStatu NegAppStatus = new NegotiationApplicationStatu()
                    {
                        Active = false,
                        Application = item,
                        NegotiationApplicationStatusID = Guid.NewGuid(),
                        ApplicationID = item.ApplicationID,
                        Negotiation = Neg,
                        UserID = AppConfigurations.CurrentLoginUser.UserID,
                        NegotiationID = Neg.NegotiationID
                    };

                    Neg.NegotiationApplicationStatus.Add(NegAppStatus);

                    this.Context.NegotiationApplicationStatus.Add(NegAppStatus);
                }
            }

            #endregion Build Application to this Negotiation

            return Neg;
        }

        /// <summary>
        /// Add new Conversation
        /// </summary>
        /// <param name="SetInContext">to set Conversation object in Context or not</param>
        /// <returns>Added Conversation</returns>
        public Conversation AddNewConversation(bool SetInContext)
        {
            Conversation Conv = new Conversation()
            {
                ConversationID = Guid.NewGuid(),
                Deleted = false,
                DeletedBy = AppConfigurations.CurrentLoginUser.UserID,
                DeletedOn = DateTime.Now,
                ConversationName = GetItemName("Conv")
            };

            if (SetInContext)
                this.Context.Conversations.Add(Conv);

            return Conv;
        }

        /// <summary>
        /// This method creates a new object of type message
        /// </summary>
        /// <param name="SetInContext">this flag determines whether to add this new message to context or not</param>
        /// <returns>New Message Object</returns>
        public Message AddNewMessage(bool SetInContext)
        {
            Message message = new Message()
            {
                MessageID = Guid.NewGuid(),
                MessageDate = DateTime.Now,
                //MessageSender = string.Empty,
                //MessageReceiver = string.Empty,
                ChannelID = eNegConstant.Channel.Email,
                Deleted = false,
                DeletedBy = AppConfigurations.CurrentLoginUser.UserID,
                DeletedOn = DateTime.Now,
                IsSent = false,
                Sent = false,
                Received = true,
                IsAppsMessage = false //Mean your message is from eNeg (Normal Message)
            };

            if (SetInContext)
                this.Context.Messages.Add(message);

            return message;
        }

        /// <summary>
        /// Remove Negotiation
        /// </summary>
        /// <param name="Neg">negotiation object to remove</param>
        public void RemoveNegotiation(Negotiation Neg)
        {
            if (this.Context.Negotiations.Contains(Neg))
            {
                Neg = this.Context.Negotiations.FirstOrDefault(s => s.NegotiationID == Neg.NegotiationID);

                if (Neg.Conversations.Count > 0)
                {
                    foreach (var item in Neg.Conversations)
                        RemoveConversation(item);
                }
                this.Context.Negotiations.Remove(Neg);
            }
        }

        /// <summary>
        /// Remove Conversation
        /// </summary>
        /// <param name="Conv">Conversation object to remove</param>
        public void RemoveConversation(Conversation Conv)
        {
            if (this.Context.Conversations.Contains(Conv))
            {
                Conv = this.Context.Conversations.FirstOrDefault(s => s.ConversationID == Conv.ConversationID);
                if (Conv.Messages.Count > 0)
                {
                    foreach (var item in Conv.Messages)
                        RemoveMessage(item);
                }

                this.Context.Conversations.Remove(Conv);
            }
        }

        /// <summary>
        /// RemoveMessage
        /// </summary>
        /// <param name="message">message object to remove</param>
        public void RemoveMessage(Message message)
        {
            if (this.Context.Messages.Contains(message))
            {
                message = this.Context.Messages.FirstOrDefault(s => s.MessageID == message.MessageID);
                this.Context.Messages.Remove(message);
            }
        }

        /// <summary>
        /// Save changes asynchronously
        /// </summary>
        public void SaveChangesAsync()
        {
            this.Context.SubmitChanges(s =>
            {
                if (SaveChangesComplete != null)
                {
                    try
                    {
                        Exception ex = null;
                        if (s.HasError)
                        {
                            ex = s.Error;
                            s.MarkErrorAsHandled();
                        }
                        SaveChangesComplete(this, new SubmitOperationEventArgs(s, ex));
                    }
                    catch (Exception ex)
                    {
                        SaveChangesComplete(this, new SubmitOperationEventArgs(ex));
                    }
                }
            }, null);
        }

        /// <summary>
        /// Reject any pending changes
        /// </summary>
        public void RejectChanges()
        {
            this.Context.RejectChanges();
        }
        #endregion

        #endregion  Public

        #endregion Methods
    }
}
