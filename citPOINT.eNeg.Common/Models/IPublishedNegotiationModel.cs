#region → Usings   .
using System;
using citPOINT.eNeg.Data.Web;
using citPOINT.eNeg.Common;
using System.ServiceModel.DomainServices.Client;
using System.ComponentModel;
#endregion

#region → History  .

/* Date         User            Change
 * 
 * 22.09.11     Yousra Reda     creation
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
    /// Interface for Published Negotiation Model
    /// </summary>
    public interface IPublishedNegotiationModel : INotifyPropertyChanged
    {
        #region → Properties     .

        /// <summary>
        /// Gets a value indicating whether this instance has changes.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance has changes; otherwise, <c>false</c>.
        /// </value>
        bool HasChanges { get; }

        /// <summary>
        /// Gets a value indicating whether this instance is busy.
        /// </summary>
        /// <value><c>true</c> if this instance is busy; otherwise, <c>false</c>.</value>
        bool IsBusy { get; }

        #endregion

        #region → Events         .

        /// <summary>
        /// Get Channel Complete
        /// </summary>
        event EventHandler<eNegEntityResultArgs<Channel>> GetChannelComplete;

        /// <summary>
        /// Occurs when [get published negotiation archive complete].
        /// </summary>
        event EventHandler<eNegEntityResultArgs<NegotiationArchive>> GetPublishedNegotiationArchiveComplete;

        /// <summary>
        /// Occurs when [get published negotiation complete].
        /// </summary>
        event EventHandler<eNegEntityResultArgs<Negotiation>> GetPublishedNegotiationComplete;

        /// <summary>
        /// Occurs when [get conversation by neg ID complete].
        /// </summary>
        event EventHandler<eNegEntityResultArgs<Conversation>> GetConversationByNegIDComplete;

        /// <summary>
        /// Occurs when [get messages by conversation Ids complete].
        /// </summary>
        event EventHandler<eNegEntityResultArgs<Message>> GetMessagesByConversationIDsComplete;
        
        #endregion

        #region → Methods        .

        /// <summary>
        /// Gets the channel async.
        /// </summary>
        void GetChannelAsync();
        
        /// <summary>
        /// Gets the published negotiation archive async.
        /// </summary>
        /// <param name="negStatusFilter">The neg status filter.</param>
        /// <param name="negOwnerFilter">The neg owner filter.</param>
        void GetPublishedNegotiationArchiveAsync(eNegConstant.NegotiationStatusFilter negStatusFilter, eNegConstant.NegotiationOwnerFilter negOwnerFilter);

        /// <summary>
        /// Gets the published negotiation by archive.
        /// </summary>
        /// <param name="archiveYear">The archive year.</param>
        /// <param name="archiveMonth">The archive month.</param>
        /// <param name="NegStatusFilter">The neg status filter.</param>
        /// <param name="NegOwnerFilter">The neg owner filter.</param>
        void GetPublishedNegotiationByArchiveAsync(int archiveYear, int archiveMonth, eNegConstant.NegotiationStatusFilter NegStatusFilter, eNegConstant.NegotiationOwnerFilter NegOwnerFilter);

        /// <summary>
        /// Gets the conversation by neg ID async.
        /// </summary>
        /// <param name="NegotiationIDs">The negotiation I ds.</param>
        void GetConversationByNegIDAsync(Guid[] NegotiationIDs);

        /// <summary>
        /// Gets the message by conversation ID async.
        /// </summary>
        /// <param name="conversationID">The conversation ID.</param>
        void GetMessageByConversationIDAsync(Guid conversationID);

        #endregion
    }
}
