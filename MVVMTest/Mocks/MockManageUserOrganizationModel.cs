using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using citPOINT.eNeg.Common;
using System.ServiceModel.DomainServices.Client;
using citPOINT.eNeg.Data.Web;
using System.ComponentModel;

namespace citPOINT.eNeg.MVVM.UnitTest
{
    /// <summary>
    /// Mock of Manage User Organization Model
    /// </summary>
    class MockManageUserOrganizationModel : IManageUserOrganizationModel
    {
        #region → Fields         .
        private List<Organization> mOrganizations;
        private List<OrganizationType> mOrganizationTypes;
        private List<UserOrganization> mUserOrganizations;
        #endregion

        #region → Properties     .

        /// <summary>
        /// Gets the mail helper.
        /// </summary>
        /// <value>The mail helper.</value>
        public MailHelper MailHelper
        {
            get { return new MailHelper(); }
        }
        
        /// <summary>
        /// Gets a value indicating whether this instance has changes.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance has changes; otherwise, <c>false</c>.
        /// </value>
        public bool HasChanges
        {
            get { return false; }
        }

        /// <summary>
        /// Gets a value indicating whether this instance is busy.
        /// </summary>
        /// <value><c>true</c> if this instance is busy; otherwise, <c>false</c>.</value>
        public bool IsBusy
        {
            get { return false; }
        }

        /// <summary>
        /// Gets the context.
        /// </summary>
        /// <value>The context.</value>
        public eNegContext Context
        {
            get { return null; }
        }

        #region <2> Organizations

        /// <summary>
        /// Gets the organizations.
        /// </summary>
        /// <value>The organizations.</value>
        public List<Organization> Organizations
        {
            get
            {
                if (mOrganizations == null)
                {
                    mOrganizations = new List<Organization>()
                       {
                           new Organization ()
                            { 
                                OrganizationID = Guid.NewGuid(),
                                OrganizationName = "citPoint",
                                OrganizationTypeID = 1,
                                Deleted = false,
                                DeletedBy = Guid.NewGuid(),
                                DeletedOn = DateTime.Now
                            },
                            new Organization ()
                            { 
                                OrganizationID = Guid.NewGuid(),
                                OrganizationName = "Google",
                                OrganizationTypeID = 2,
                                Deleted = false,
                                DeletedBy = Guid.NewGuid(),
                                DeletedOn = DateTime.Now
                            }
                       };
                }
                return mOrganizations;
            }
        }

        #endregion Organizations

        #region <2> OrganizationTypes

        /// <summary>
        /// Gets the organization types.
        /// </summary>
        /// <value>The organization types.</value>
        public List<OrganizationType> OrganizationTypes
        {
            get
            {
                if (mOrganizationTypes == null)
                {
                    mOrganizationTypes = new List<OrganizationType>()
                       {
                           new OrganizationType ()
                            { 
                                OrganizationTypeID = 1,
                                OrganizationTypeName = "LP - KG"
                            },

                            new OrganizationType ()
                            { 
                                OrganizationTypeID = 2,
                                OrganizationTypeName = "PLC – AG"
                            }
                       };
                }
                return mOrganizationTypes;
            }
        }

        #endregion OrganizationTypes

        #region <2> UserOrganizations

        /// <summary>
        /// Gets the user organizations.
        /// </summary>
        /// <value>The user organizations.</value>
        public List<UserOrganization> UserOrganizations
        {
            get
            {
                if (mUserOrganizations == null)
                {
                    mUserOrganizations = new List<UserOrganization>()
                       {
                           new UserOrganization ()
                            { 
                                UserOrganizationID = Guid.NewGuid(),
                                UserID = new Guid("0E90E65D-5597-47DB-9E81-DD53FF4A9F7F"),
                                OrganizationID = Guid.NewGuid(),
                                MemberStatus = 0,
                                Deleted = false,
                                DeletedBy = Guid.NewGuid(),
                                DeletedOn = DateTime.Now
                            },

                            new UserOrganization ()
                            { 
                                UserOrganizationID = Guid.NewGuid(),
                                UserID = new Guid("0D2C957D-BB95-46D5-A31A-F073EE9570D4"),
                                OrganizationID = Guid.NewGuid(),
                                MemberStatus = 3,
                                Deleted = false,
                                DeletedBy = Guid.NewGuid(),
                                DeletedOn = DateTime.Now
                            }
                       };
                }
                return mUserOrganizations;
            }
        }
        #endregion UserOrganizations

        #endregion

        #region → Events         .
        /// <summary>
        /// Occurs when [get organization type complete].
        /// </summary>
        public event EventHandler<eNegEntityResultArgs<OrganizationType>> GetOrganizationTypeComplete;

        /// <summary>
        /// Occurs when [get organization complete].
        /// </summary>
        public event EventHandler<eNegEntityResultArgs<Organization>> GetOrganizationComplete;

        /// <summary>
        /// Occurs when [get user organization for user complete].
        /// </summary>
        public event EventHandler<eNegEntityResultArgs<UserOrganization>> GetUserOrganizationForUserComplete;

        /// <summary>
        /// Occurs when [can user leave organization complete].
        /// </summary>
        public event EventHandler<eNegEntityResultArgs<UserLeaveOrganizationResult>> CanUserLeaveOrganizationComplete;

        /// <summary>
        /// call back of Getting organizations owners'
        /// </summary>
        public event EventHandler<eNegEntityResultArgs<User>> GetOrganizationsOwnersComplete;

        /// <summary>
        /// Occurs when [sending mail completed].
        /// </summary>
        public event Action<InvokeOperation> SendingMailCompleted;

        /// <summary>
        /// Occurs when [property changed].
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region → Methods        .

        /// <summary>
        /// Gets the organization type async.
        /// </summary>
        public void GetOrganizationTypeAsync()
        {
            if (GetOrganizationTypeComplete != null)
            {
                GetOrganizationTypeComplete(this, new eNegEntityResultArgs<OrganizationType>(OrganizationTypes));
            }
        }

        /// <summary>
        /// Gets the organization async.
        /// </summary>
        public void GetOrganizationAsync()
        {
            if (GetOrganizationComplete != null)
            {
                GetOrganizationComplete(this, new eNegEntityResultArgs<Organization>(Organizations));
            }
        }

        /// <summary>
        /// Gets the user organizations status bridge table for user async.
        /// </summary>
        /// <param name="userID">The user ID.</param>
        public void GetUserOrganizationForUserAsync(Guid userID)
        {
            if (GetUserOrganizationForUserComplete != null)
            {
                GetUserOrganizationForUserComplete(this, new eNegEntityResultArgs<UserOrganization>(UserOrganizations.Where(s => s.UserID == userID)));
            }
        }

        /// <summary>
        /// Determines whether this user can leave this Organization or not
        /// </summary>
        /// <param name="userID">The user ID.</param>
        /// <param name="organizationID">The organization ID.</param>
        public void CanUserLeaveOrganization(Guid userID, Guid organizationID)
        {

        }

        /// <summary>
        /// Gets the organizations owners
        /// For sending mails for them.
        /// </summary>
        /// <param name="organizationIDs">The organization Ids.</param>
        public void GetOrganizationsOwners(Guid[] organizationIDs)
        {

        }

        /// <summary>
        /// Sets the alternative owner for organization.
        /// </summary>
        /// <param name="alternativeOwnerID">The alternative owner ID.</param>
        /// <param name="organizationID">The organization ID.</param>
        public void SetAlternativeOwnerForOrganization(Guid alternativeOwnerID, Guid organizationID)
        {

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
            #endregion

            Organizations.Add(organization);
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
            #endregion

            UserOrganizations.Add(userOrganization);
            return userOrganization;
        }

        /// <summary>
        /// Removes Organization from the current context
        /// </summary>
        /// <param name="organization">The organization.</param>
        public void RemoveOrganization(Organization organization)
        {
            if (Organizations.Contains(organization))
            {
                Organizations.Remove(organization);
            }
        }

        /// <summary>
        /// Removes the user organization.
        /// </summary>
        /// <param name="userOrganization">The user organization.</param>
        public void RemoveUserOrganization(UserOrganization userOrganization)
        {
            if (UserOrganizations.Contains(userOrganization))
            {
                UserOrganizations.Remove(userOrganization);
            }
        }

        /// <summary>
        /// Rejects the changes.
        /// </summary>
        public void RejectChanges()
        {

        }

      

        #endregion
        
        
    }
}
