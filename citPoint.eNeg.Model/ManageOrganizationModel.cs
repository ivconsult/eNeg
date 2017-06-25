
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
    /// Model Layer for Managing Organization .
    /// </summary>
    #region  Using MEF to export ManageOrganizationModel
    [Export(typeof(IManageOrganizationModel))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    #endregion
    public class ManageOrganizationModel : IManageOrganizationModel
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
                    if (mNegContext == null)
                    {
                        mNegContext = new eNegContext();
                    }
                    mNegContext.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(ctx_PropertyChanged);
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
        /// Occurs when [get organization by ID complete].
        /// </summary>
        public event EventHandler<eNegEntityResultArgs<Organization>> GetOrganizationByIDComplete;

        /// <summary>
        /// Occurs when [get organization members complete].
        /// </summary>
        public event EventHandler<eNegEntityResultArgs<User>> GetOrganizationMembersComplete;

        /// <summary>
        /// Occurs when [get organization members status complete].
        /// </summary>
        public event EventHandler<eNegEntityResultArgs<UserOrganization>> GetOrganizationMembersStatusComplete;

        /// <summary>
        /// Event Handler For Method SendingMail
        /// </summary>
        public event Action<InvokeOperation> SendingMailCompleted;

        /// <summary>
        /// Occurs when [save changes complete].
        /// </summary>
        public event EventHandler<SubmitOperationEventArgs> SaveChangesComplete;

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

        #region IManageOrganizationModel Interface implementation

        /// <summary>
        /// Gets the organization by ID.
        /// </summary>
        /// <param name="organizationID">The organization ID.</param>
        public void GetOrganizationByID()
        {
            PerformQuery<Organization>(Context.GetOrganizationByIDQuery(AppConfigurations.CurrentLoginUser.OrganizationOwnedID), GetOrganizationByIDComplete);
        }

        /// <summary>
        /// Gets the organization members async.
        /// </summary>
        /// <param name="organizationID">The organization ID.</param>
        public void GetOrganizationMembersAsync()
        {
            PerformQuery<User>(Context.GetOrganizationMembersQuery(AppConfigurations.CurrentLoginUser.OrganizationOwnedID), GetOrganizationMembersComplete);
        }

        /// <summary>
        /// Gets the organization members status async.
        /// </summary>
        public void GetOrganizationMembersStatusAsync()
        {
            PerformQuery<UserOrganization>(Context.GetOrganizationMembersStatusQuery(AppConfigurations.CurrentLoginUser.OrganizationOwnedID), GetOrganizationMembersStatusComplete);
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
        /// Reject all changes happen on current Context
        /// </summary>
        public void RejectChanges()
        {
            this.Context.RejectChanges();
        }

        /// <summary>
        /// Apply All Changes in current Context to database.
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
        #endregion

        #endregion

        #endregion

    }
}
