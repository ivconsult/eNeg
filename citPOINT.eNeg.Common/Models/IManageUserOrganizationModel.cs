#region → Usings   .
using System;
using System.ComponentModel;
using System.ServiceModel.DomainServices.Client;
using citPOINT.eNeg.Data.Web;
using citPOINT.eNeg.Common;
#endregion

#region → History  .

/* Date         User            Change
 * 
 * 14.08.11     M.Wahab         • creation
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
    /// Interface for  Manage User Organizations Model
    /// </summary>
    public interface IManageUserOrganizationModel
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

        /// <summary>
        /// Gets the context.
        /// </summary>
        /// <value>The context.</value>
        eNegContext Context { get; }

        /// <summary>
        /// Gets the mail helper.
        /// </summary>
        /// <value>The mail helper.</value>
        MailHelper MailHelper { get; }

        #endregion Properties

        #region → Events         .

        /// <summary>
        /// Occurs when [get organization type complete].
        /// </summary>
        event EventHandler<eNegEntityResultArgs<OrganizationType>> GetOrganizationTypeComplete;

        /// <summary>
        /// Occurs when [get organization complete].
        /// </summary>
        event EventHandler<eNegEntityResultArgs<Organization>> GetOrganizationComplete;

        /// <summary>
        /// Occurs when [get user organization for user complete].
        /// </summary>
        event EventHandler<eNegEntityResultArgs<UserOrganization>> GetUserOrganizationForUserComplete;

        /// <summary>
        /// Occurs when [can user leave organization complete].
        /// </summary>
        event EventHandler<eNegEntityResultArgs<UserLeaveOrganizationResult>> CanUserLeaveOrganizationComplete;

        /// <summary>
        /// call back of Getting organizations owners'
        /// </summary>
        event EventHandler<eNegEntityResultArgs<User>> GetOrganizationsOwnersComplete;

        /// <summary>
        /// Occurs when [sending mail completed].
        /// </summary>
        event Action<InvokeOperation> SendingMailCompleted;

        /// <summary>
        /// Occurs when [property changed].
        /// </summary>
        event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region → Methods        .

        /// <summary>
        /// Gets the organization type async.
        /// </summary>
        void GetOrganizationTypeAsync();

        /// <summary>
        /// Gets the organization async.
        /// </summary>
        void GetOrganizationAsync();

        /// <summary>
        /// Gets the user organizations status bridge table for user async.
        /// </summary>
        /// <param name="userID">The user ID.</param>
        void GetUserOrganizationForUserAsync(Guid userID);

        /// <summary>
        /// Determines whether this user can leave this Organization or not
        /// </summary>
        /// <param name="userID">The user ID.</param>
        /// <param name="organizationID">The organization ID.</param>
        void CanUserLeaveOrganization(Guid userID, Guid organizationID);

        /// <summary>
        /// Gets the organizations owners
        /// For sending mails for them.
        /// </summary>
        /// <param name="organizationIDs">The organization Ids.</param>
        void GetOrganizationsOwners(Guid[] organizationIDs);

        /// <summary>
        /// Sets the alternative owner for organization.
        /// </summary>
        /// <param name="alternativeOwnerID">The alternative owner ID.</param>
        /// <param name="organizationID">The organization ID.</param>
        void SetAlternativeOwnerForOrganization(Guid alternativeOwnerID, Guid organizationID);


        /// <summary>
        /// public method that add Organization to the current context
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="SetInContext">Add in Current Context</param>
        /// <returns>New Instance of Organization</returns>
        Organization AddNewOrganization(User user, bool SetInContext);

        /// <summary>
        /// Adds the new user organization.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="organziation">The organziation.</param>
        /// <param name="memberStatus">The member status.[Member,Owner]</param>
        /// <param name="SetInContext">if set to <c>true</c> [set in context].</param>
        /// <returns>new Instance of User Organization</returns>
        UserOrganization AddNewUserOrganization(User user, Organization organziation, byte memberStatus, bool SetInContext);

        /// <summary>
        /// Removes Organization from the current context
        /// </summary>
        /// <param name="organization">The organization.</param>
        void RemoveOrganization(Organization organization);

        /// <summary>
        /// Removes the user organization.
        /// </summary>
        /// <param name="userOrganization">The user organization.</param>
        void RemoveUserOrganization(UserOrganization userOrganization);

        /// <summary>
        /// Rejects the changes.
        /// </summary>
        void RejectChanges();
        
        #endregion
    }
}
