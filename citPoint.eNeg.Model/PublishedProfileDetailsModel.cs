
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
 * 17.08.11     M.Wahab       creation
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
    #region  Using MEF to export PublishedProfileDetailsViewModel
    /// <summary>
    /// Model Layer for Published Profile Details.
    /// </summary>
    [Export(typeof(IPublishedProfileDetailsModel))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    #endregion
    public class PublishedProfileDetailsModel : IPublishedProfileDetailsModel
    {
        #region → Fields         .
        private eNegContext mNegContext;
        private Boolean mHasChanges = false;
        private Boolean mIsBusy = false;
        #endregion

        #region → Properties     .

        /// <summary>
        /// Protocted property with a getter only to used to update user login and logout data in eNeg Database 
        /// </summary>
        protected eNegContext Context
        {
            get
            {
                if (mNegContext == null)
                {
                    mNegContext = Repository.Context;
                    mNegContext.PropertyChanged += new PropertyChangedEventHandler(_ctx_PropertyChanged);
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
        private void _ctx_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
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
        /// Event Handler For Method GetCountryAsync
        /// </summary>
        public event EventHandler<eNegEntityResultArgs<Country>> GetCountryComplete;

        /// <summary>
        /// Event Handler For Method PropertyChanged
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Event Handler For Method GetUserByID
        /// </summary>
        public event EventHandler<eNegEntityResultArgs<User>> GetUserByIDComplete;

        /// <summary>
        /// Call back of get user profile statisticals .
        /// </summary>
        public event EventHandler<eNegEntityResultArgs<UserProfileStatisticalsResult>> GetUserProfileStatisticalsComplete;

        /// <summary>
        /// Call back of can user see member profile.
        /// </summary>
        public event Action<InvokeOperation<bool>> CanUserSeeMemberProfileComplete;

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

        #region IPublishedProfileDetailsModel Interface implementation


        /// <summary>
        /// public method that call perform query to load countries
        /// </summary>
        public void GetCountryAsync()
        {
            PerformQuery<Country>(Context.GetCountryQuery(), GetCountryComplete);
        }

        /// <summary>
        /// public method that call perform query to load User by his GUID
        /// </summary>
        /// <param name="userID">The user ID.</param>
        public void GetUserByID(Guid userID)
        {

            PerformQuery<User>(Context.GetUserQuery().Where(u => u.UserID == userID), GetUserByIDComplete);
        }

        /// <summary>
        /// Gets the user profile statisticals.
        /// </summary>
        /// <param name="userID">The user ID.</param>
        public void GetUserProfileStatisticalsAsyc(Guid userID)
        {
            PerformQuery<UserProfileStatisticalsResult>(Context.GetUserProfileStatisticalsQuery(userID), GetUserProfileStatisticalsComplete);
        }


        /// <summary>
        /// Determines whether this instance can user see member profile or not.
        /// </summary>
        /// <param name="userID">The user ID.</param>
        public void CanUserSeeMemberProfileAsync(Guid userID)
        {
            this.Context.CanUserSeeMemberProfile(AppConfigurations.CurrentLoginUser.UserID, userID, CanUserSeeMemberProfileComplete, null);
        }



        #endregion

        #endregion

        #endregion Methods
    }
}