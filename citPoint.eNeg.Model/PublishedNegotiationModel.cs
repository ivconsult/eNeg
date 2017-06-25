
#region → Usings   .
using System;
using System.ComponentModel;
using System.Linq;
using System.ServiceModel.DomainServices.Client;
using citPOINT.eNeg.Common;
using citPOINT.eNeg.Data.Web;
using System.ComponentModel.Composition;
using System.Linq.Expressions;
using System.Collections.Generic;
using citPOINT.eNeg.Common.Helper;
#endregion

#region → History  .

/* Date         User            Change
 * 
 * 21.09.11     Yousra Reda     creation
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
    /// <summary>
    /// Model Layer for the UC of publish Negotiation to certain organization
    /// </summary>
    #region  Using MEF to export to PublishedNegotiationModel
    [Export(typeof(IPublishedNegotiationModel))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    #endregion
    public class PublishedNegotiationModel : IPublishedNegotiationModel
    {
        #region → Fields         .

        private eNegContext mNegContext;

        private bool mIsBusy = false;

        private bool mHasChanges = false;

        #endregion Fields

        #region → Properties     .

        /// <summary>
        /// Gets the context.
        /// </summary>
        /// <value>The context.</value>
        private eNegContext Context
        {
            get
            {
                if (mNegContext == null)
                {
                    mNegContext = Repository.Context;

                    mNegContext.PropertyChanged += new PropertyChangedEventHandler(mNegContext_PropertyChanged);
                }
                return mNegContext;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is busy.
        /// </summary>
        /// <value><c>true</c> if this instance is busy; otherwise, <c>false</c>.</value>
        public bool IsBusy
        {
            get
            {
                return mIsBusy;
            }
            private set
            {
                if (mIsBusy != value)
                {
                    mIsBusy = value;
                    OnPropertyChnaged("IsBusy");
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance has changes.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance has changes; otherwise, <c>false</c>.
        /// </value>
        public bool HasChanges
        {
            get
            {
                return mHasChanges;
            }
            private set
            {
                if (mHasChanges != value)
                {
                    mHasChanges = value;
                    OnPropertyChnaged("HasChanges");
                }
            }
        }

        #endregion Properties

        #region → Event Handlers .

        /// <summary>
        /// Handles the PropertyChanged event of the mNegContext control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.ComponentModel.PropertyChangedEventArgs"/> instance containing the event data.</param>
        private void mNegContext_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "HasChanges":
                    this.HasChanges = mNegContext.HasChanges;
                    break;
                case "IsBusy":
                    this.IsBusy = mNegContext.IsLoading;
                    break;
                case "IsSubmitting":
                    this.IsBusy = mNegContext.IsSubmitting;
                    break;
            }
        }

        #endregion Event Handlers

        #region → Events         .

        /// <summary>
        /// Get Channel Complete
        /// </summary>
        public event EventHandler<eNegEntityResultArgs<Channel>> GetChannelComplete;

        /// <summary>
        /// Occurs when [get published negotiation complete].
        /// </summary>
        public event EventHandler<eNegEntityResultArgs<Negotiation>> GetPublishedNegotiationComplete;

        /// <summary>
        /// Occurs when [get published negotiation archive complete].
        /// </summary>
        public event EventHandler<eNegEntityResultArgs<NegotiationArchive>> GetPublishedNegotiationArchiveComplete;

        /// <summary>
        /// Occurs when [get conversation by neg ID complete].
        /// </summary>
        public event EventHandler<eNegEntityResultArgs<Conversation>> GetConversationByNegIDComplete;

        /// <summary>
        /// Occurs when [get messages by negotiation I ds complete].
        /// </summary>
        public event EventHandler<eNegEntityResultArgs<Message>> GetMessagesByConversationIDsComplete;

        /// <summary>
        /// Occurs when [property changed].
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

        #endregion

        #region → Protected      .

        /// <summary>
        /// Called when [property chnaged].
        /// </summary>
        /// <param name="PropertyName">Name of the property.</param>
        protected virtual void OnPropertyChnaged(string PropertyName)
        {
            if (PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(PropertyName));
            }
        }

        #endregion

        #region → Public         .

        /// <summary>
        /// Gets channels asynchronously
        /// </summary>
        public void GetChannelAsync()
        {
            PerformQuery<Channel>(this.Context
                                      .GetChannelsQuery()
                                      .OrderBy(s => s.ChannelName),
                                  GetChannelComplete);
        }

        /// <summary>
        /// Gets the published negotiation count async.
        /// </summary>
        /// <param name="NegStatusFilter">The neg status filter.</param>
        /// <param name="NegOwnerFilter">The neg owner filter.</param>
        public void GetPublishedNegotiationArchiveAsync(eNegConstant.NegotiationStatusFilter NegStatusFilter, eNegConstant.NegotiationOwnerFilter NegOwnerFilter)
        {
            this.Context.NegotiationArchives.Clear();
            this.Context.Messages.Clear();
            this.Context.Conversations.Clear();
            this.Context.Negotiations.Clear();

            PerformQuery<NegotiationArchive>(this.Context
                                                 .GetPublishedNegotiationArchiveQuery((byte)NegStatusFilter,
                                                 (byte)NegOwnerFilter,
                                                  AppConfigurations.CurrentLoginUser.UserID), GetPublishedNegotiationArchiveComplete);
        }

        /// <summary>
        /// Gets the published negotiation by archive.
        /// </summary>
        /// <param name="archiveYear">The archive year.</param>
        /// <param name="archiveMonth">The archive month.</param>
        /// <param name="NegStatusFilter">The neg status filter.</param>
        /// <param name="NegOwnerFilter">The neg owner filter.</param>
        public void GetPublishedNegotiationByArchiveAsync(int archiveYear, int archiveMonth,
             eNegConstant.NegotiationStatusFilter NegStatusFilter,
             eNegConstant.NegotiationOwnerFilter NegOwnerFilter)
        {

            // Generate the query with paging
            var qry = Context.GetPublishedNegoiationsQuery((byte)NegStatusFilter, (byte)NegOwnerFilter,
                        AppConfigurations.CurrentLoginUser.UserID, archiveYear, archiveMonth)
                        .OrderBy(s => s.NegotiationName);


            PerformQuery<Negotiation>(qry, GetPublishedNegotiationComplete);
        }

        /// <summary>
        /// Get Conversation By negotation ID asynchronously
        /// </summary>
        /// <param name="NegotiationIDs">array if negotations IDs</param>
        public void GetConversationByNegIDAsync(Guid[] NegotiationIDs)
        {
            PerformQuery<Conversation>(this.Context
                                           .GetConversationsByNegotiationIDQuery(NegotiationIDs)
                                           .OrderBy(s => s.DeletedOn),
                                           GetConversationByNegIDComplete);
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

        #endregion

        #endregion


    }
}
