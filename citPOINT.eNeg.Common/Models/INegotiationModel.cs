
#region → Usings   .

using System;
using citPOINT.eNeg.Data.Web;
using System.ComponentModel;
using System.ServiceModel.DomainServices.Client;
using System.Collections.Generic;

#endregion

#region → History  .

/* Date         User
 * 
 * 16.08.10     Mohamed Abdulwahab
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
    /// Interface for Class NegotiationModel
    /// </summary>
    public interface INegotiationModel : INotifyPropertyChanged
    {

        #region → Properties     .

        /// <summary>
        /// Gets if data context has changes or not
        /// </summary>
        bool HasChanges { get; }

        /// <summary>
        /// Gets if data context is busy
        /// </summary>
        bool IsBusy { get; }

        /// <summary>
        /// Context of Service eNegService
        /// </summary>
        eNegContext Context { get; }

        /// <summary>
        /// Gets all negotiation.
        /// </summary>
        /// <value>All negotiation.</value>
        IEnumerable<Negotiation> AllNegotiation { get; }

        #endregion

        #region → Events         .

        /// <summary>
        /// Occurs when [generate PDF complete].
        /// </summary>
        event Action<InvokeOperation<byte[]>> GeneratePDFComplete;

        /// <summary>
        /// Occurs when [get closed negotiation archive complete].
        /// </summary>
        event EventHandler<eNegEntityResultArgs<NegotiationArchive>> GetClosedNegotiationArchiveComplete;

        /// <summary>
        /// Occurs when [get closed negotiation for archive complete].
        /// </summary>
        event EventHandler<eNegEntityResultArgs<Negotiation>> GetClosedNegotiationForArchiveComplete;

        /// <summary>
        /// Occurs when [get conversation by negotiation ID complete].
        /// </summary>
        event EventHandler<eNegEntityResultArgs<Conversation>> GetConversationByNegotiationIDComplete;

        /// <summary>
        /// Occurs when [get onging negotiation complete].
        /// </summary>
        event EventHandler<eNegEntityResultArgs<Negotiation>> GetOngingNegotiationComplete;
        
        /// <summary>
        /// Occurs when [get messages by conversation Ids complete].
        /// </summary>
        event EventHandler<eNegEntityResultArgs<Message>> GetMessagesByConversationIDsComplete;

        /// <summary>
        /// Occurs when [get negotiation orgaization complete].
        /// </summary>
        event EventHandler<eNegEntityResultArgs<NegotiationOrganization>> GetNegotiationOrgaizationComplete;

        /// <summary>
        /// Occurs when [get organizations for user complete].
        /// </summary>
        event EventHandler<eNegEntityResultArgs<Organization>> GetOrganizationsForUserComplete;

        /// <summary>
        /// Occurs when [get apps active for DM complete].
        /// </summary>
        event EventHandler<eNegEntityResultArgs<UserApplicationStatu>> GetAppsActiveForDMComplete;

        /// <summary>
        /// Occurs when [check on negotiation repeat complete].
        /// </summary>
        event Action<InvokeOperation<int?>> CheckOnNegotiationRepeatComplete;

        /// <summary>
        /// GetApplicationComplete
        /// </summary>
        event EventHandler<eNegEntityResultArgs<Application>> GetApplicationComplete;

        /// <summary>
        /// GetAttachementComplete
        /// </summary>
        event EventHandler<eNegEntityResultArgs<Attachement>> GetAttachementComplete;

        /// <summary>
        /// GetChannelComplete
        /// </summary>
        event EventHandler<eNegEntityResultArgs<Channel>> GetChannelComplete;

        /// <summary>
        /// GetChannelTypeComplete
        /// </summary>
        event EventHandler<eNegEntityResultArgs<ChannelType>> GetChannelTypeComplete;

        /// <summary>
        /// GetNegotiationApplicationStatusComplete
        /// </summary>
        event EventHandler<eNegEntityResultArgs<NegotiationApplicationStatu>> GetNegotiationApplicationStatusComplete;

        /// <summary>
        /// GetNegotiationStatusComplete
        /// </summary>
        event EventHandler<eNegEntityResultArgs<NegotiationStatu>> GetNegotiationStatusComplete;

        /// <summary>
        /// Occurs when [get negotiation page number complete].
        /// </summary>
        event Action<InvokeOperation<int?>> GetNegotiationPageNumberComplete;

        /// <summary>
        /// SaveChangesComplete
        /// </summary>
        event EventHandler<SubmitOperationEventArgs> SaveChangesComplete;

        #endregion

        #region → Methods        .

        /// <summary>
        /// Generates the PDF conversations async.
        /// </summary>
        /// <param name="conversationIDs">The conversation I ds.</param>
        void GeneratePDFConversationsAsync(Guid[] conversationIDs);

        /// <summary>
        /// Generates the PDF messages async.
        /// </summary>
        /// <param name="messagesIDs">The messages I ds.</param>
        void GeneratePDFMessagesAsync(Guid[] messagesIDs);

        /// <summary>
        /// Gets the closed negotiation archive async.
        /// </summary>
        void GetClosedNegotiationsArchiveAsync();

        /// <summary>
        /// Gets the closed negotiation by archive async.
        /// </summary>
        /// <param name="archiveYear">The archive year.</param>
        /// <param name="archiveMonth">The archive month.</param>
        void GetClosedNegotiationByArchiveAsync(int archiveYear, int archiveMonth);

        /// <summary>
        /// Gets the onging negotiations async.
        /// </summary>
        void GetOngingNegotiationsAsync();

        /// <summary>
        /// Gets the conversation by negotiation ID async.
        /// </summary>
        /// <param name="negotiationIDs">The negotiation I ds.</param>
        void GetConversationByNegotiationIDAsync(Guid[] negotiationIDs);

        /// <summary>
        /// Gets the message by conversation ID async.
        /// </summary>
        /// <param name="conversationID">The conversation ID.</param>
        void GetMessageByConversationIDAsync(Guid conversationID);

        /// <summary>
        /// Determines if the user is authorized or not for a right
        /// </summary>
        /// <param name="RightName">right name</param>
        /// <returns>is One is authorized to do functionality or not(Bool)</returns>
        bool IsAuthorized(RightsName RightName);

        /// <summary>
        /// Gets application asynchronously
        /// </summary>
        void GetApplicationAsync();

        /// <summary>
        /// Gets attachements asynchronously
        /// </summary>
        void GetAttachementAsync();

        /// <summary>
        /// Gets channels asynchronously
        /// </summary>
        void GetChannelAsync();

        /// <summary>
        /// Gets channel types asynchronously
        /// </summary>
        void GetChannelTypeAsync();
        
        /// <summary>
        /// Gets Negotiation Application Status asynchronously
        /// </summary>
        /// <param name="NegotiationIDs">Array OF Negotiation IDs</param>
        void GetNegotiationApplicationStatusAsync(Guid[] NegotiationIDs);

        /// <summary>
        /// Gets negotiation status asynchronously
        /// </summary>
        void GetNegotiationStatusAsync();

        /// <summary>
        /// Add new negotiation
        /// </summary>
        /// <param name="SetInContext">to set negotaion object in Context or not</param>
        /// <returns>Added Negotiation</returns>
        Negotiation AddNewNegotiation(bool SetInContext);

        /// <summary>
        /// Add new Conversation
        /// </summary>
        /// <param name="SetInContext">to set Conversation object in Context or not</param>
        /// <param name="Neg">The neg.</param>
        Conversation AddNewConversation(bool SetInContext);

        /// <summary>
        /// This method creates a new object of type message
        /// </summary>
        /// <param name="SetInContext">this flag determines whether to add this new message to context or not</param>
        /// <returns>New Message Object</returns>
        Message AddNewMessage(bool SetInContext);

        /// <summary>
        /// Remove Negotiation
        /// </summary>
        /// <param name="Neg">negotiation object to remove</param>
        void RemoveNegotiation(Negotiation Neg);

        /// <summary>
        /// Remove Conversation
        /// </summary>
        /// <param name="Conv">Conversation object to remove</param>
        void RemoveConversation(Conversation Conv);

        /// <summary>
        /// RemoveMessage
        /// </summary>
        /// <param name="message">message object to remove</param>
        void RemoveMessage(Message message);

        /// <summary>
        /// Save changes asynchronously
        /// </summary>
        void SaveChangesAsync();

        /// <summary>
        /// Reject any pending changes
        /// </summary>
        void RejectChanges();

        /// <summary>
        /// Checks the on negotiation.
        /// </summary>
        /// <param name="negotiationID">The negotiation ID.</param>
        /// <param name="negotiationName">Name of the negotiation.</param>
        /// <param name="userID">The user ID.</param>
        void CheckOnNegotiationRepeatAsync(Guid negotiationID, string negotiationName, Guid userID);

        /// <summary>
        /// Gets the active apps for DM.
        /// </summary>
        void GetAppsActiveForDM();

        /// <summary>
        /// Gets the organizations for user async.
        /// </summary>
        void GetOrganizationsForUserAsync();

        /// <summary>
        /// Gets the negotiation organization async.
        /// </summary>
        /// <param name="NegotiationIDs">The negotiation I ds.</param>
        void GetNegotiationOrganizationAsync(Guid[] NegotiationIDs);

        /// <summary>
        /// Adds the negotiation organization.
        /// </summary>
        /// <param name="Neg">The neg.</param>
        /// <param name="orga">The orga.</param>
        /// <param name="SetInContext">if set to <c>true</c> [set in context].</param>
        /// <returns>Entity for Negotiation organization</returns>
        NegotiationOrganization AddNegotiationOrganization(Negotiation Neg, Organization orga, bool SetInContext);

        #endregion

    }
}
