#region → Usings   .
using System;
using citPOINT.eNeg.Data.Web;
using citPOINT.eNeg.Common;
using System.ComponentModel;
using System.ServiceModel.DomainServices.Client;

#endregion

#region → History  .

/* Date         User            Change
 * 
 * 04.09.11     yousra reda     •creation
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
    /// Interface for Managing Organization Model
    /// </summary>
    public interface IManageOrganizationModel
    {
        #region → Properties     .

        /// <summary>
        /// Gets the context.
        /// </summary>
        /// <value>The context.</value>
        eNegContext Context { get; }

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

        /// <summary>
        /// Gets the mail helper.
        /// </summary>
        /// <value>The mail helper.</value>
        MailHelper MailHelper { get; }

        #endregion

        #region → Events         .

        /// <summary>
        /// Occurs when [get organization by ID complete].
        /// </summary>
        event EventHandler<eNegEntityResultArgs<Organization>> GetOrganizationByIDComplete;
        
        /// <summary>
        /// Occurs when [get organization members complete].
        /// </summary>
        event EventHandler<eNegEntityResultArgs<User>> GetOrganizationMembersComplete;

        /// <summary>
        /// Occurs when [get organization members status complete].
        /// </summary>
        event EventHandler<eNegEntityResultArgs<UserOrganization>> GetOrganizationMembersStatusComplete;

        /// <summary>
        /// Occurs when [sending mail completed].
        /// </summary>
        event Action<InvokeOperation> SendingMailCompleted;

        /// <summary>
        /// Occurs when [save changes complete].
        /// </summary>
        event EventHandler<SubmitOperationEventArgs> SaveChangesComplete;

        /// <summary>
        /// Occurs when [property changed].
        /// </summary>
        event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region → Methods        .
        /// <summary>
        /// Gets the organization by ID.
        /// </summary>
        void GetOrganizationByID();
        
            /// <summary>void GetOrganizationByID(Guid organizationID)
        /// Gets the organization members async.
        /// </summary>
        void GetOrganizationMembersAsync();

        /// <summary>
        /// Gets the organization members status async.
        /// </summary>
        void GetOrganizationMembersStatusAsync();

        /// <summary>
        /// Removes the user organization.
        /// </summary>
        /// <param name="userOrganization">The user organization.</param>
        void RemoveUserOrganization(UserOrganization userOrganization);
        
        /// <summary>
        /// Saves the changes async.
        /// </summary>
        void SaveChangesAsync();

        /// <summary>
        /// Rejects the changes.
        /// </summary>
        void RejectChanges();

        #endregion
    }
}
