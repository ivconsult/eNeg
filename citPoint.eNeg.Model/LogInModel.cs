#region → Usings   .
using citPOINT.eNeg.Common;
using citPOINT.eNeg.Data.Web;
using System;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.IO.IsolatedStorage;
using System.Security.Principal;
using System.ServiceModel.DomainServices.Client;
using System.ServiceModel.DomainServices.Client.ApplicationServices;
#endregion

#region → History  .

/* Date         User            Change
 * 
 * 02.08.10     Yousra Reda     creation
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
    /// LoginModel that deal with login service 
    /// </summary>

    #region  Using MEF to export LoginViewModel
    [Export(typeof(ILogInModel))]
    [PartCreationPolicy(CreationPolicy.Shared)]

    #endregion

    public class LogInModel : ILogInModel
    {

        #region → Fields         .
        private AuthenticationService mService;
        private LogInContext mLoginSevice;
        private eNegContext mNegContext;

        private Boolean mIsBusy = false;
        private Boolean mHasChanges = false;
        SessionContext mSessionContext = new SessionContext();
        IsolatedStorageSettings appSettings = IsolatedStorageSettings.ApplicationSettings;
        #endregion

        #region → Properties     .

        /// <summary>
        /// Protocted property with a getter only to can use our custom login Service
        /// </summary>
        protected LogInContext LoginSevice
        {
            get
            {
                if (mLoginSevice == null)
                {
                    mLoginSevice = new LogInContext();
                }
                return mLoginSevice;
            }
        }

        /// <summary>
        /// Protocted property with a getter only to can wrap silverlight Authentication Service 
        /// and register for loggedIn and LoggedOut Events
        /// </summary>
        protected AuthenticationService AuthService
        {
            get
            {
                if (mService == null)
                {

                    if (((WebAuthenticationService)WebContext.Current.Authentication).DomainContext == null)
                        // This will enable WebContext to dsicover Authenticaion Context's from a CLass Library 
                        ((WebAuthenticationService)WebContext.Current.Authentication).DomainContext = new LogInContext();

                    mService = WebContext.Current.Authentication;

                    ((INotifyPropertyChanged)mService).PropertyChanged += new PropertyChangedEventHandler(_service_PropertyChanged);
                    mService.LoggedIn += new EventHandler<AuthenticationEventArgs>(_service_LoggedIn);
                    mService.LoggedOut += new EventHandler<AuthenticationEventArgs>(_service_LoggedOut);
                }

                return mService;
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
                    mNegContext = new eNegContext();
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
        public Boolean _ctxIsBusy
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


        /// <summary>
        /// A principal object represents the security context of the user
        /// on whose behalf the code is running, including that user's
        /// identity (IIdentity) and any roles to which they belong.
        /// </summary>
        public IPrincipal User
        {
            get { return this.AuthService.User; }
        }

        /// <summary>
        /// True if there is an operation (LoadUserAsync, 
        /// LoginAsync, or LogoutAsync) in progress; otherwise, false
        /// </summary>
        public Boolean IsBusy
        {
            get { return this.AuthService.IsBusy; }
        }

        /// <summary>
        /// true if there is an operation in progress; otherwise, false
        /// </summary>
        public bool IsLoadingUser
        {
            get { return this.AuthService.IsLoadingUser; }
        }

        /// <summary>
        /// true if there is an operation in progress; otherwise, false
        /// </summary>
        public bool IsLoggingIn
        {
            get { return this.AuthService.IsLoggingIn; }
        }

        /// <summary>
        /// true if there is an operation in progress; otherwise, false
        /// </summary>
        public bool IsLoggingOut
        {
            get { return this.AuthService.IsLoggingOut; }
        }

        /// <summary>
        /// true if there is an operation in progress; otherwise, false
        /// </summary>
        public bool IsSavingUser
        {
            get { return this.AuthService.IsSavingUser; }
        }
        #endregion

        #region → Event Handlers .
        /// <summary>
        /// Private Event Handler that called after any change in HasChanges, IsLoading, IsSubmitting properties
        /// </summary>
        /// <param name="sender">Value Of sender</param>
        /// <param name="e">Value Of e</param>
        void _ctx_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "HasChanges":
                    this.HasChanges = mNegContext.HasChanges;
                    break;
                case "IsLoading":
                    this._ctxIsBusy = mNegContext.IsLoading;
                    break;
                case "IsSubmitting":
                    this._ctxIsBusy = mNegContext.IsSubmitting;
                    break;
            }
        }

        /// <summary>
        /// Private Event Handler that called after any change in IsBusy, IsLoadingUser, IsLoggedIn, IsLogggedOut, IsSavingUser, User properties
        /// </summary>
        /// <param name="sender">Value Of sender</param>
        /// <param name="e">Value Of e</param>
        void _service_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "IsBusy":
                    this.OnPropertyChanged("IsBusy");
                    break;
                case "IsLoadingUser":
                    this.OnPropertyChanged("IsLoadingUser");
                    break;
                case "IsLoggingIn":
                    this.OnPropertyChanged("IsLoggingIn");
                    break;
                case "IsLoggingOut":
                    this.OnPropertyChanged("IsLoggingOut");
                    break;
                case "IsSavingUser":
                    this.OnPropertyChanged("IsSavingUser");
                    break;
                case "User":
                    this.OnPropertyChanged("User");
                    break;
            }
        }

        /// <summary>
        /// Private Event handler for the process of logging in
        /// </summary>
        /// <param name="sender">Value Of sender</param>
        /// <param name="e">Value Of e</param>
        private void _service_LoggedIn(object sender, AuthenticationEventArgs e)
        {
            if (this.AuthenticationChanged != null)
                this.AuthenticationChanged(this, e);
        }

        /// <summary>
        /// Private Event handler for the process of logging out
        /// </summary>
        /// <param name="sender">Value Of sender</param>
        /// <param name="e">Value Of e</param>
        private void _service_LoggedOut(object sender, AuthenticationEventArgs e)
        {
            if (this.AuthenticationChanged != null)
                this.AuthenticationChanged(this, e);
        }

        /// <summary>
        /// Private Event Handler that fired automatically after finishing loading Current User to the System
        /// </summary>
        /// <param name="loadUserOperation">Value Of loadUserOperation</param>
        private void LoadUserOperation_Completed(LoadUserOperation loadUserOperation)
        {
            if (loadUserOperation.HasError)
            {
                if (this.LoadUserComplete != null)
                    this.LoadUserComplete(this, new LoadUserOperationEventArgs(loadUserOperation, loadUserOperation.Error));
                loadUserOperation.MarkErrorAsHandled();
            }
            else
            {
                if (this.LoadUserComplete != null)
                {
                    if (!string.IsNullOrEmpty((loadUserOperation.User as LoginUser).EmailAddress))
                    {
                        AppConfigurations.CurrentLoginUser = (loadUserOperation.User as LoginUser);
                    }
                    this.LoadUserComplete(this, new LoadUserOperationEventArgs(loadUserOperation));
                }
            }
        }

        /// <summary>
        /// Private Event Handler that fired automatically after finishing the process of logging in
        /// </summary>
        /// <param name="loginOperation">Value Of loginOperation</param>
        private void LoginOperation_Completed(LoginOperation loginOperation)
        {
            if (loginOperation.LoginSuccess)
            {
                if (this.LoginComplete != null)
                    this.LoginComplete(this, new LoginOperationEventArgs(loginOperation));
            }
            else
            {
                if (loginOperation.HasError)
                {
                    if (this.LoginComplete != null)
                        this.LoginComplete(this, new LoginOperationEventArgs(loginOperation, loginOperation.Error));
                    loginOperation.MarkErrorAsHandled();
                }
                else if (!loginOperation.IsCanceled)
                {
                    if (this.LoginComplete != null)
                    {
                        Exception ex = new Exception("Wrong UserName or Password");
                        this.LoginComplete(this, new LoginOperationEventArgs(ex));
                    }
                }
                else
                {
                    if (this.LoginComplete != null)
                        this.LoginComplete(this, new LoginOperationEventArgs(loginOperation));
                }
            }
        }

        /// <summary>
        /// Private Event Handler that fired automatically after finishing the process of logging out
        /// </summary>
        /// <param name="logoutOperation">The logout operation.</param>
        private void LogoutOperation_Completed(LogoutOperation logoutOperation)
        {
            if (logoutOperation.HasError)
            {
                if (this.LogoutComplete != null)
                    this.LogoutComplete(this, new LogoutOperationEventArgs(logoutOperation, logoutOperation.Error));
                logoutOperation.MarkErrorAsHandled();
            }
            else
            {
                if (this.LogoutComplete != null)
                    this.LogoutComplete(this, new LogoutOperationEventArgs(logoutOperation));
            }
        }

        /// <summary>
        /// This will automatically authenticate a user after reading appSettings to get UserName
        /// </summary>
        /// <param name="invOp">Value of Invoke Operaton</param>
        public void AuthenticateUseromplete(InvokeOperation invOp)
        {
            if (!invOp.HasError)
            {
                this.AuthService.LoadUser(this.LoadUserOperation_Completed, null);
            }
            else
            {
                eNegMessanger.RaiseErrorMessage.Send(invOp.Error);
            }
        }

        /// <summary>
        /// This will automatically get session from Server Side this session will conatin UserName (EmailAddress) if exist
        /// </summary>
        /// <param name="invOp">Value of Invoke Operaton</param>
        public void GetSessionComplete(InvokeOperation<string> invOp)
        {
            if (!invOp.HasError)
            {
                if (appSettings.Contains("UserName"))
                {
                    LoginSevice.AuthenticateUser(appSettings["UserName"].ToString(), new Action<InvokeOperation>(AuthenticateUseromplete), null);
                }
                else if (appSettings.Contains("TempUserName"))
                {
                    LoginSevice.AuthenticateUser(appSettings["TempUserName"].ToString(), new Action<InvokeOperation>(AuthenticateUseromplete), null);
                }
                else if (!string.IsNullOrEmpty(invOp.Value))
                {
                    LoginSevice.AuthenticateUser(invOp.Value, new Action<InvokeOperation>(AuthenticateUseromplete), null);
                }
                else
                {
                    LoginSevice.AuthenticateUser("", new Action<InvokeOperation>(AuthenticateUseromplete), null);
                }
                //appSettings.Save();
            }
            else
            {
                eNegMessanger.RaiseErrorMessage.Send(invOp.Error);
            }
        }


        #endregion

        #region → Events         .

        /// <summary>
        /// Event that represent the callback of of the process of Loading user of was presistent in the last login operation
        /// </summary>
        public event EventHandler<LoadUserOperationEventArgs> LoadUserComplete;

        /// <summary>
        /// Event that represent the callback of of the process of login
        /// </summary>
        public event EventHandler<LoginOperationEventArgs> LoginComplete;

        /// <summary>
        /// Event that represent the callback of of the process of logout
        /// </summary>
        public event EventHandler<LogoutOperationEventArgs> LogoutComplete;

        /// <summary>
        /// Event that represent the callback of of the process of Selecting User By Name
        /// </summary>
        public event EventHandler<eNegEntityResultArgs<User>> GetUserByUserNameComplete;

        /// <summary>
        /// Event that represent the callback of of the process of saving any pending changes in the context
        /// </summary>
        public event EventHandler<SubmitOperationEventArgs> SaveChangesComplete;

        /// <summary>
        /// Event that represent the callback of of the process of MakeUserOffline (Update his data in DB)
        /// </summary>
        public event EventHandler<eNegEntityResultArgs<User>> MakeUserOfflineComplete;

        /// <summary>
        /// Event that represent the callback of of the process of ChangingAuthntication of current user
        /// </summary>
        public event EventHandler<AuthenticationEventArgs> AuthenticationChanged;

        /// <summary>
        /// Event that represent the callback of of the process of ChangingAuthntication of current user
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region → Methods        .

        #region → Private        .
        /// <summary>
        /// Private Method used to perform query on certain entity of eNeg Entities
        /// </summary>
        /// <typeparam name="T"></typeparam>
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
        #endregion

        #region → Protected      .

        #region "INotifyPropertyChanged Interface implementation"


        /// <summary>
        /// Called when [property changed].
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion "INotifyPropertyChanged Interface implementation"

        #endregion

        #region → Public         .

        #region "IAuthenticationModel Interface implementation"

        /// <summary>
        /// public method that call perform query to update user data when he logout
        /// </summary>
        /// <param name="UserID">Value Of UserID</param>
        public void MakeUserOffline(Guid? UserID)
        {
            PerformQuery<User>(Context.MakeUserOfflineQuery(UserID), MakeUserOfflineComplete);
        }

        /// <summary>
        /// public method call perform query to select user by his UserName
        /// </summary>
        /// <param name="UserName">Value Of UserName</param>
        public void GetUserByUserName(string UserName)
        {
            PerformQuery<User>(Context.GetUserQuery().Where(u => u.EmailAddress == UserName), GetUserByUserNameComplete);
        }

        /// <summary>
        /// public method to save any change occured in the object created from eNegcontext
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
        /// This will read appSettings to detect 
        /// whether the user choose "Keep me signed in" on a previous login attempt or not
        /// </summary>
        public void LoadUserAsync()
        {
            try
            {
                if (!AppConfigurations.IsRunningOutOfBrowser)
                {
                    mSessionContext.GetSessionValue("SessionUser", new Action<InvokeOperation<string>>(GetSessionComplete), null);
                }
                else
                {
                    if (appSettings.Contains("UserName"))
                    {
                        LoginSevice.AuthenticateUser(appSettings["UserName"].ToString(), new Action<InvokeOperation>(AuthenticateUseromplete), null);
                    }
                    else
                    {
                        LoginSevice.AuthenticateUser("", new Action<InvokeOperation>(AuthenticateUseromplete), null);
                    }
                    appSettings.Save();
                }
            }
            catch (Exception ex)
            {
                eNegMessanger.RaiseErrorMessage.Send(ex);
            }
        }

        /// <summary>
        /// Authenticate a user with user name and password
        /// </summary>
        /// <param name="loginParameters">Value Of loginParameters</param>
        public void LoginAsync(LoginParameters loginParameters)
        {
            this.AuthService.Login(loginParameters, this.LoginOperation_Completed, null);
        }

        /// <summary>
        /// Logout User Asynchronously
        /// </summary>
        public void LogoutAsync()
        {
            this.AuthService.Logout(this.LogoutOperation_Completed, null);
        }


        #endregion


        #endregion

        #endregion
    }
}
