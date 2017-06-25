
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
 * 02.08.10     M.Wahab       creation
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
    [Export(typeof(IUserRegisterModel))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    #endregion
    public class UserRegisterModel : IUserRegisterModel
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
        /// Occurs when [get all application complete].
        /// </summary>
        public event EventHandler<eNegEntityResultArgs<Application>> GetAllApplicationComplete;
        /// <summary>
        /// Occurs when [get culture complete].
        /// </summary>
        public event EventHandler<eNegEntityResultArgs<Culture>> GetCultureComplete;
        /// <summary>
        /// Event Handler For Method GetAccountTypeAsync
        /// </summary>
        public event EventHandler<eNegEntityResultArgs<AccountType>> GetAccountTypeComplete;
        /// <summary>
        /// Event Handler For Method GetCountryAsync
        /// </summary>
        public event EventHandler<eNegEntityResultArgs<Country>> GetCountryComplete;
        /// <summary>
        /// Event Handler For Method GetPreferedLanguageAsync
        /// </summary>
        public event EventHandler<eNegEntityResultArgs<PreferedLanguage>> GetPreferedLanguageComplete;
        /// <summary>
        /// Event Handler For Method GetProfileAsync
        /// </summary>
        public event EventHandler<eNegEntityResultArgs<Profile>> GetProfileComplete;
        /// <summary>
        /// Event Handler For Method GetRightAsync
        /// </summary>
        public event EventHandler<eNegEntityResultArgs<Right>> GetRightComplete;
        /// <summary>
        /// Event Handler For Method GeetRoleAsync
        /// </summary>
        public event EventHandler<eNegEntityResultArgs<Role>> GetRoleComplete;
        /// <summary>
        /// Event Handler For Method GetRoleRightAsync
        /// </summary>
        public event EventHandler<eNegEntityResultArgs<RoleRight>> GetRoleRightComplete;
        /// <summary>
        /// Event Handler For Method GetSecurityQuestionAsync
        /// </summary>
        public event EventHandler<eNegEntityResultArgs<SecurityQuestion>> GetSecurityQuestionComplete;
        /// <summary>
        /// Event Handler For Method GetUserRoleAsync
        /// </summary>
        public event EventHandler<eNegEntityResultArgs<UserRole>> GetUserRoleComplete;
        /// <summary>
        /// Event Handler For Method CheckIsEmailExistAsync
        /// Check Mail repeating
        /// </summary>
        public event EventHandler<eNegEntityResultArgs<User>> GetCheckIsEmailExistComplete;
        /// <summary>
        /// Event Handler For Method DeleteUserOperationByUserID
        /// </summary>
        public event EventHandler<eNegEntityResultArgs<User>> DeleteUserOperationByUserIDComplete;
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

        #region For User Reset
        /// <summary>
        /// Event Handler For Method GetUserByID
        /// </summary>
        public event EventHandler<eNegEntityResultArgs<User>> GetUserByIDComplete;
        /// <summary>
        /// Event Handler For Method CheckUserRequestReset
        /// this Method is To Check if the Current Link is valid or not
        /// </summary>
        public event EventHandler<eNegEntityResultArgs<User>> GetCheckUserRequestResetComplete;
        /// <summary>
        /// Event Handler For Method UpdateReset
        /// Update Reset Flag So This User Can Enter Direct 
        /// to Reset Page In The Next Time Open The Project
        /// </summary>
        public event EventHandler<eNegEntityResultArgs<User>> GetResetRequestComplete;

        #endregion Events
        
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
        /// public method that call perform query to load preferred languages
        /// </summary>
        public void GetPreferedLanguageAsync()
        {
            PerformQuery<PreferedLanguage>(Context.GetPreferedLanguageQuery(), GetPreferedLanguageComplete);
        }

        /// <summary>
        /// public method that call perform query to load account types
        /// </summary>
        public void GetAccountTypeAsync()
        {
            PerformQuery<AccountType>(Context.GetAccountTypeQuery(), GetAccountTypeComplete);
        }

        /// <summary>
        /// public method that call perform query to load countries
        /// </summary>
        public void GetCountryAsync()
        {
            PerformQuery<Country>(Context.GetCountryQuery(), GetCountryComplete);
        }

        /// <summary>
        /// public method that call perform query to load User Profile
        /// </summary>
        public void GetProfileAsync()
        {
            PerformQuery<Profile>(Context.GetProfileQuery(), GetProfileComplete);
        }

        /// <summary>
        /// public method that call perform query to load Rightes
        /// </summary>
        public void GetRightAsync()
        {
            PerformQuery<Right>(Context.GetRightQuery(), GetRightComplete);
        }

        /// <summary>
        /// public method that call perform query to load Roles
        /// </summary>
        public void GetRoleAsync()
        {
            PerformQuery<Role>(Context.GetRoleQuery(), GetRoleComplete);
        }

        /// <summary>
        /// public method that call perform query to load User Role Right
        /// </summary>
        public void GetRoleRightAsync()
        {
            PerformQuery<RoleRight>(Context.GetRoleRightQuery(), GetRoleRightComplete);
        }

        /// <summary>
        /// public method that call perform query to load Security Question
        /// </summary>
        public void GetSecurityQuestionAsync()
        {
            PerformQuery<SecurityQuestion>(Context.GetSecurityQuestionQuery(), GetSecurityQuestionComplete);
        }

        /// <summary>
        /// public method that call perform query to load User Roles
        /// </summary>
        public void GetUserRoleAsync()
        {
            PerformQuery<UserRole>(Context.GetUserRoleQuery(), GetUserRoleComplete);
        }

        /// <summary>
        /// Gets all applicaions.
        /// </summary>
        public void GetAllApplicaions()
        {
            PerformQuery<Application>(Context.GetApplicationsQuery(), GetAllApplicationComplete);
        }

        /// <summary>
        /// Gets the culture async.
        /// </summary>
        public void GetCultureAsync()
        {
            PerformQuery<Culture>(Context.GetCulturesQuery().OrderBy(ss => ss.CultureName), GetCultureComplete);
        }

        /// <summary>
        /// public method that call perform query to check on email existance
        /// </summary>
        /// <param name="CurrentUser">CurrentUser</param>
        public void CheckIsEmailExist(User CurrentUser)
        {
            PerformQuery<User>(Context.GetUserQuery().Where(u => u.EmailAddress.ToLower() == CurrentUser.EmailAddress.ToLower() && u.UserID != CurrentUser.UserID && u.Disabled == false), GetCheckIsEmailExistComplete);
        }

        /// <summary>
        /// public method that call perform query to update reset property in user table
        /// </summary>
        /// <param name="UserID">UserID</param>
        public void UpdateReset(Guid? UserID)
        {
            PerformQuery<User>(Context.UpdateResetQuery(UserID), GetResetRequestComplete);
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
        /// public method that call perform query to Check whether user already requests login or not
        /// </summary>
        ///   /// <param name="OperationString">Value of OperationString</param>
        public void CheckUserRequestReset(String OperationString)
        {
            PerformQuery<User>(Context.GetUserByOperationStringQuery(OperationString, eNegConstant.UserOperations.ResetRequest), GetCheckUserRequestResetComplete);
        }
        
        /// <summary>
        /// For Deletign All Related Records In UserOperation Table
        /// And That in case if one Complete his Reset Login Operation
        /// and He Had not Change His EmailAddress.
        /// </summary>
        /// <param name="UserID">value of The UserID</param>
        public void DeleteUserOperationByUserID(Guid UserID)
        {
            PerformQuery<User>(Context.DeleteUserOperationByUserIDQuery(UserID), DeleteUserOperationByUserIDComplete);
        }
        
        /// <summary>
        /// to Send The Reset Mail to The User
        /// </summary>
        /// <param name="EmailAddress">User Mail Address</param>
        /// <param name="FirstName">Value of FirstName</param>
        /// <param name="LastName">Value of LastName</param>
        /// <param name="OperationString">Hash of The UserID And MailAddress</param>
        public void SendResetMail(string EmailAddress, string FirstName, string LastName, string OperationString)
        {
            this.MailHelper.SendResetMail(EmailAddress, FirstName, LastName, OperationString);
        }

        /// <summary>
        /// to Send The Confirmation Mail to The User
        /// </summary>
        /// <param name="EmailAddress">User Mail Address</param>
        /// <param name="FirstName">Value of FirstName</param>
        /// <param name="LastName">Value of LastName</param>
        /// <param name="OperationString">Hash of The UserID And MailAddress</param>
        public void SendConfiramtionMail(string EmailAddress, string FirstName, string LastName, string OperationString)
        {
            this.MailHelper.SendConfirmationMail(EmailAddress, FirstName, LastName, OperationString);
        }

        /// <summary>
        /// public method that add user to the current context
        /// </summary>
        /// <param name="SetInContext">Add in Current Context</param>
        /// <returns>New Instance of User</returns>
        public User AddNewUser(bool SetInContext)
        {
            #region → Add New User                              .
            User NewUser = new User()
            {
                UserID = Guid.NewGuid(),
                CheckForNewEmailAddress = false,
                CheckForNewPassword = false,
                Locked = false,// this is a new algorithm to detected if one have activation male or not.
                IPAddress = "10.0.02.2",
                LastLoginDate = DateTime.Now,
                CreateDate = DateTime.Now,
                AnswerHash = string.Empty,
                AnswerSalt = string.Empty,
                Online = false,
                Disabled = false,
                FirstName = string.Empty,
                LastName = string.Empty,
                CompanyName = string.Empty,
                Address = string.Empty,
                City = string.Empty,
                Phone = string.Empty,
                Mobile = string.Empty,
                Gender = false,
                Reset = false,
                IsFemale = false,
                IsMale = true,
                PasswordAnswer = "",
                HasPublicProfile = false
            };

            if (SetInContext)
                this.Context.Users.Add(NewUser);
            #endregion

            #region → Build User Application Status for this User

            if (SetInContext)
            {
                foreach (var App in this.Context.Applications)
                {
                    UserApplicationStatu UserAppStatus = new UserApplicationStatu()
                    {
                        UserAppStatusID = Guid.NewGuid(),
                        ApplicationID = App.ApplicationID,
                        Application = App,
                        UserID = NewUser.UserID,
                        User = NewUser,
                        IsDMActive = false
                    };

                    NewUser.UserApplicationStatus.Add(UserAppStatus);
                    this.Context.UserApplicationStatus.Add(UserAppStatus);
                }
            }
            #endregion

            return NewUser;
        }

        /// <summary>
        /// public method that remove user from the current context
        /// </summary>
        /// <param name="user">Value of user Wanted to Delete</param>
        public void RemoveUser(User user)
        {
            if (this.Context.Users.Contains(user))
                this.Context.Users.Remove(user);
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