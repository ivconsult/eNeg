
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
 * 19.10.10     M.Wahab       creation
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

    #region  Using MEF to export RegisterationViewModel
    /// <summary>
    /// Model Layer for User Operation Register or Reset login.
    /// </summary>
    [Export(typeof(IUpdateUserProfileModel))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    #endregion
    public class UpdateUserProfileModel : IUpdateUserProfileModel
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
        private MailHelper MailHelper
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
        /// Occurs when [get culture complete].
        /// </summary>
        public event EventHandler<eNegEntityResultArgs<Culture>> GetCultureComplete;

        /// <summary>
        /// Event Handler For Method GetCountryAsync
        /// </summary>
        public event EventHandler<eNegEntityResultArgs<Country>> GetCountryComplete;

        /// <summary>
        /// Event Handler For Method GetPreferedLanguageAsync
        /// </summary>
        public event EventHandler<eNegEntityResultArgs<PreferedLanguage>> GetPreferedLanguageComplete;

        /// <summary>
        /// Event Handler For Method CheckIsEmailExistAsync
        /// Check Mail repeating
        /// </summary>
        public event EventHandler<eNegEntityResultArgs<User>> GetCheckIsEmailExistComplete;

        /// <summary>
        /// Event Handler For Method SendingMail
        /// </summary>
        public event Action<InvokeOperation> SendingMailCompleted;

        /// <summary>
        /// Event Handler For Method SaveChanges
        /// </summary>
        public event EventHandler<SubmitOperationEventArgs> SaveChangesComplete;

        /// <summary>
        /// Event Handler For Method PropertyChanged
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Event Handler For Method GetUserByID
        /// </summary>
        public event EventHandler<eNegEntityResultArgs<User>> GetUserByIDComplete;


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

        #region IRegisterModel Interface implementation

        /// <summary>
        /// Gets the culture async.
        /// </summary>
        public void GetCultureAsync()
        {
            PerformQuery<Culture>(Context.GetCulturesQuery().OrderBy(ss => ss.CultureName), GetCultureComplete);
        }

        /// <summary>
        /// public method that call perform query to load preferred languages
        /// </summary>
        public void GetPreferedLanguageAsync()
        {
            PerformQuery<PreferedLanguage>(Context.GetPreferedLanguageQuery(), GetPreferedLanguageComplete);
        }


        /// <summary>
        /// public method that call perform query to load countries
        /// </summary>
        public void GetCountryAsync()
        {
            PerformQuery<Country>(Context.GetCountryQuery(), GetCountryComplete);
        }

        /// <summary>
        /// public method that call perform query to check on email existance
        /// </summary>
        /// <param name="CurrentUser">CurrentUser</param>
        public void CheckIsEmailExist(User CurrentUser)
        {
            PerformQuery<User>(Context.GetUserQuery().Where(u => u.EmailAddress.ToLower() == CurrentUser.NewEmail.ToLower() && u.UserID != CurrentUser.UserID && u.Disabled == false), GetCheckIsEmailExistComplete);
        }

        /// <summary>
        /// public method that call perform query to load User by his GUID
        /// </summary>
        /// <param name="UserID">Value of UserID</param>
        public void GetUserByID(Guid? UserID)
        {
            PerformQuery<User>(Context.GetUserQuery().Where(u => u.UserID == UserID), GetUserByIDComplete);
        }

        /// <summary>
        /// to Send The Confirmation Mail to The User
        /// </summary>
        /// <param name="EmailAddress">User Mail Address</param>
        /// <param name="FirstName">The first name.</param>
        /// <param name="LastName">The last name.</param>
        /// <param name="OperationString">Hash of The UserID And MailAddress</param>
        public void SendConfiramtionMail(string EmailAddress, string FirstName, string LastName, string OperationString)
        {
            this.MailHelper.SendConfirmationMail(EmailAddress, FirstName, LastName, OperationString);
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

        /// <summary>
        /// Reject all changes happen on current Context
        /// </summary>
        public void RejectChanges()
        {
            this.Context.RejectChanges();
        }

        /// <summary>
        /// Cleans up.
        /// </summary>
        public void CleanUp()
        {
            Repository.Context = null;
        }

        #endregion

        #endregion

        #endregion Methods

    }


}