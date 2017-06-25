
#region → Usings   .
using System;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.ServiceModel.DomainServices.Client;
using citPOINT.eNeg.Common;
using citPOINT.eNeg.Data.Web;
using citPOINT.eNeg.Common.Helper;

#endregion

#region → History  .

/* Date         User            Change
 * 
 * 14.08.11     M.Wahab       •creation
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

    #region  Using MEF to export ManageUserOrganizationModel
    /// <summary>
    /// Model Layer for User Organizations managements.
    /// </summary>
    [Export(typeof(IManageUserOrganizationModel))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    #endregion
    public class ManageUserOrganizationModel : IManageUserOrganizationModel
    {
        #region → Fields         .
        private eNegContext mNegContext;
        private MailHelper mMailHelper;
        private Boolean mHasChanges = false;
        private Boolean mIsBusy = false;
        #endregion

        #region → Properties     .

        /// <summary>
        /// Property with getter only Used to send mail to user when he request login
        /// </summary>
        public MailHelper MailHelper
        {
            get
            {
                if (mMailHelper == null)
                {

                    mMailHelper = new MailHelper();
                    mMailHelper.MailSendComplete += new Action<InvokeOperation>(SendingMailCompleted);
                }

                return mMailHelper;
            }
        }

        /// <summary>
        /// Protocted property with a getter only to used to update user login and logout data in eNeg Database 
        /// </summary>
        public eNegContext Context
        {
            get
            {

                if (mNegContext == null)
                {
                    mNegContext = Repository.Context;
                    mNegContext.PropertyChanged += new PropertyChangedEventHandler(ctx_PropertyChanged);
                }

                return mNegContext;
            }
        }

        /// <summary>
        /// True if _ctx.HasChanges is true; otherwise, false
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

        #endregion

        #region → Event Handlers .

        /// <summary>
        /// Private Event Handler that called after any change in 
        /// HasChanges, IsLoading, IsSubmitting properties
        /// </summary>
        /// <param name="sender">Value of Sender</param>
        /// <param name="e">Value of e</param>
        private void ctx_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
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
        /// Occurs when [get organization type complete].
        /// </summary>
        public event EventHandler<eNegEntityResultArgs<OrganizationType>> GetOrganizationTypeComplete;

        /// <summary>
        /// Occurs when [get all Organization complete].
        /// </summary>
        public event EventHandler<eNegEntityResultArgs<Organization>> GetOrganizationComplete;

        /// <summary>
        /// Occurs when [get user organization for user complete].
        /// </summary>
        public event EventHandler<eNegEntityResultArgs<UserOrganization>> GetUserOrganizationForUserComplete;

        /// <summary>
        /// call back of Getting organizations owners'
        /// </summary>
        public event EventHandler<eNegEntityResultArgs<User>> GetOrganizationsOwnersComplete;

        /// <summary>
        /// Occurs when [can user leave organization complete].
        /// </summary>
        public event EventHandler<eNegEntityResultArgs<UserLeaveOrganizationResult>> CanUserLeaveOrganizationComplete;

        /// <summary>
        /// Event Handler For Method SendingMail
        /// </summary>
        public event Action<InvokeOperation> SendingMailCompleted;

        /// <summary>
        /// Event Handler For Method PropertyChanged
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

        #endregion Private

        #region → Protected      .

        #region INotifyPropertyChanged Interface implementation
        /// <summary>
        /// Handle changes in any Property
        /// </summary>
        /// <param name="propertyName">Property Name</param>
        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion INotifyPropertyChanged Interface implementation

        #endregion Protected

        #region → Public         .

        #region IManageUserOrganizationModel Interface implementation

        /// <summary>
        /// Gets the organization type async.
        /// </summary>
        public void GetOrganizationTypeAsync()
        {
            PerformQuery<OrganizationType>(Context.GetOrganizationTypesQuery(), GetOrganizationTypeComplete);
        }

        /// <summary>
        /// Gets the organizations async.
        /// </summary>
        public void GetOrganizationAsync()
        {
            PerformQuery<Organization>(Context.GetOrganizationsQuery(), GetOrganizationComplete);
        }

        /// <summary>
        /// Gets the user organizations status bridge table for user async.
        /// </summary>
        /// <param name="userID">The user ID.</param>
        public void GetUserOrganizationForUserAsync(Guid userID)
        {
            PerformQuery<UserOrganization>(Context.GetUserOrganizationsForUserQuery(userID), GetUserOrganizationForUserComplete);
        }

        /// <summary>
        /// Gets the organizations owners
        /// For sending mails for them.
        /// </summary>
        /// <param name="organizationIDs">The organization Ids.</param>
        public void GetOrganizationsOwners(Guid[] organizationIDs)
        {
            PerformQuery<User>(Context.GetOrganizationsOwnersQuery(organizationIDs), GetOrganizationsOwnersComplete);
        }

        /// <summary>
        /// public method that add Organization to the current context
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="SetInContext">Add in Current Context</param>
        /// <returns>New Instance of Organization</returns>
        public Organization AddNewOrganization(User user, bool SetInContext)
        {
            #region → Add New organization  .

            Organization organization = new Organization()
            {
                OrganizationID = Guid.NewGuid(),
                Deleted = false,
                DeletedBy = user.UserID,
                DeletedOn = DateTime.Now,
            };


            if (SetInContext)
                this.Context.Organizations.Add(organization);

            //Adding creator User as Owner.
            AddNewUserOrganization(user, organization, eNegConstant.OrganizationMemberStatus.Owner, SetInContext);

            #endregion

            return organization;
        }

        /// <summary>
        /// Adds the new user organization.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="organziation">The organziation.</param>
        /// <param name="memberStatus">The member status.[Member,Owner]</param>
        /// <param name="SetInContext">if set to <c>true</c> [set in context].</param>
        /// <returns>new Instance of User Organization</returns>
        public UserOrganization AddNewUserOrganization(User user, Organization organziation, byte memberStatus, bool SetInContext)
        {

            #region → Add New User organization.

            UserOrganization userOrganization = new UserOrganization()
            {
                UserOrganizationID = Guid.NewGuid(),
                UserID = user.UserID,
                User = user,
                Organization = organziation,
                OrganizationID = organziation.OrganizationID,
                MemberStatus = memberStatus,
                Deleted = false,
                DeletedBy = user.UserID,
                DeletedOn = DateTime.Now,
            };


            if (SetInContext)
                this.Context.UserOrganizations.Add(userOrganization);

            #endregion

            return userOrganization;
        }

              
        /// <summary>
        /// Determines whether this user can leave this Organization or not
        /// </summary>
        /// <param name="userID">The user ID.</param>
        /// <param name="organizationID">The organization ID.</param>
        public void CanUserLeaveOrganization(Guid userID, Guid organizationID)
        {
            PerformQuery<UserLeaveOrganizationResult>(Context.CanUserLeaveOrganizationQuery(userID, organizationID), CanUserLeaveOrganizationComplete);
        }

        /// <summary>
        /// public method that remove Organization from the current context
        /// </summary>
        /// <param name="organization">The organization.</param>
        public void RemoveOrganization(Organization organization)
        {
            while (organization.UserOrganizations.Count() > 0)
            {
                RemoveUserOrganization(organization.UserOrganizations.FirstOrDefault());
            }

            if (this.Context.Organizations.Contains(organization))
                this.Context.Organizations.Remove(organization);
        }

        /// <summary>
        /// Removes the user organization.
        /// </summary>
        /// <param name="userOrganization">The user organization.</param>
        public void RemoveUserOrganization(UserOrganization userOrganization)
        {
            if (this.Context.UserOrganizations.Contains(userOrganization))
                this.Context.UserOrganizations.Remove(userOrganization);
        }
        
        /// <summary>
        /// Sets the alternative owner for organization.
        /// </summary>
        /// <param name="alternativeOwnerID">The alternative owner ID.</param>
        /// <param name="organizationID">The organization ID.</param>
        public void SetAlternativeOwnerForOrganization(Guid alternativeOwnerID, Guid organizationID)
        {

            #region → Add New User organization.

            UserOrganization userOrganization = new UserOrganization()
            {
                UserOrganizationID = Guid.NewGuid(),
                UserID = alternativeOwnerID,
                OrganizationID = organizationID,
                MemberStatus = eNegConstant.OrganizationMemberStatus.Owner,
                Deleted = true,//falg Indicating that alternative one will be owner no new record.
                DeletedBy = alternativeOwnerID,
                DeletedOn = DateTime.Now,
            };


            this.Context.UserOrganizations.Add(userOrganization);

            #endregion
        }

        /// <summary>
        /// Reject all changes happen on current Context
        /// </summary>
        public void RejectChanges()
        {
            this.Context.RejectChanges();
        }

        #endregion

        #endregion

        #endregion Methods
    }
}