
#region → Usings   .
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Security.Principal;
using System.ServiceModel.DomainServices.Client.ApplicationServices;
using System.Windows;
using citPOINT.eNeg.Common;
using citPOINT.eNeg.Data.Web;

#endregion

#region → History  .

/* Date         User              Change
 * 
 * 19.09.10     Yousra Reda       Creation
 */

# endregion

#region → ToDos    .

/*
 * Date         set by User     Description
 * 
 * 
*/
# endregion


namespace citPOINT.eNeg.Client.Tests
{
    /// <summary>
    /// Mock of Login Model
    /// </summary>
    [Export(typeof(ILogInModel))]
    public class MockLoginModel : ILogInModel
    {
        #region → Fields         .
        private List<User> _users;
        private List<citPOINT.eNeg.Data.Web.Application> mApplications;
        private eNegContext mContext;
        #endregion

        #region → Properties     .

        /// <summary>
        /// Gets if data context is busy
        /// </summary>
        /// <value></value>
        public bool IsBusy
        {
            get
            {
                return false;
            }
        }

        /// <summary>
        /// Gets if IsLoadingUser is in progress
        /// </summary>
        /// <value></value>
        public bool IsLoadingUser
        {
            get
            {
                return false;
            }
        }

        /// <summary>
        /// Gets if IsLoggingIn is in progress
        /// </summary>
        /// <value></value>
        public bool IsLoggingIn
        {
            get
            {
                return false;
            }
        }

        /// <summary>
        /// Gets if IsLoggingOut is in progress
        /// </summary>
        /// <value></value>
        public bool IsLoggingOut
        {
            get
            {
                return false;
            }
        }

        /// <summary>
        /// Indicates whether the process of saving user is in progress or not
        /// </summary>
        /// <value></value>
        public bool IsSavingUser
        {
            get
            {
                return false;
            }
        }

        /// <summary>
        /// Represent the authenticated user
        /// </summary>
        /// <value></value>
        public IPrincipal User
        {
            get { return new LoginUser(); }
        }

        /// <summary>
        /// Gets the context.
        /// </summary>
        /// <value>The context.</value>
        public eNegContext Context
        {
            get
            {
                if (mContext == null)
                {
                    mContext = new eNegContext(new Uri("http://localhost:9000/citPOINT-eNeg-Data-Web-eNegService.svc", UriKind.Absolute));
                }
                return mContext;
            }
        }

        /// <summary>
        /// List of Applications
        /// </summary>
        private List<citPOINT.eNeg.Data.Web.Application> Applications
        {
            get
            {
                if (this.mApplications == null)
                {
                    mApplications = new List<citPOINT.eNeg.Data.Web.Application>();


                    for (int appCounter = 1; appCounter <= 3; appCounter++)
                    {

                        citPOINT.eNeg.Data.Web.Application app = new citPOINT.eNeg.Data.Web.Application()
                        {
                            ApplicationID = Guid.NewGuid(),
                            ApplicationTitle = "Application " + appCounter.ToString(),
                            ApplicationSettingLink = "Www.app" + appCounter.ToString() + ".com"
                        };

                        this.mApplications.Add(app);
                    }
                }

                return mApplications;
            }
        }

        #endregion

        #region → Constructors   .

        /// <summary>
        /// Initializes a new instance of the <see cref="MockLoginModel"/> class.
        /// </summary>
        public MockLoginModel()
        {
            _users = new List<User>()
            {
                new User()
                {
                    UserID = Guid.NewGuid(),
                    EmailAddress = "Yousra.reda@gmail.com",
                    Password = "P@ssword1234",
                    NewPassword = "P@ssword1234",
                    Locked = false,
                    IPAddress = "172.0.0.1",
                    LastLoginDate = DateTime.Now,
                    CreateDate = DateTime.Now,
                    Online = false,
                    Disabled = false,
                    FirstName = "Yousra",
                    LastName = "Reda",
                },
                new User()
                {
                    UserID = Guid.NewGuid(),
                    EmailAddress = "Test@gmail.com",
                    Password = "P@ssword1234",
                    NewPassword = "P@ssword1234",
                    Locked = false,
                    IPAddress = "172.0.0.1",
                    LastLoginDate = DateTime.Now,
                    CreateDate = DateTime.Now,
                    Online = false,
                    Disabled = false,
                    FirstName = "Test",
                    LastName = "Test",
                }

            };


 
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
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// GetApplicationComplete
        /// </summary>
        public event EventHandler<eNegEntityResultArgs<Data.Web.Application>> GetApplicationComplete;
              
        #endregion

        #region → Methods        .

        #region → Public         .

        /// <summary>
        /// This will read appSettings to detect
        /// whether the user choose "Keep me signed in" on a previous login attempt or not
        /// </summary>
        public void LoadUserAsync()
        {

        }

        /// <summary>
        /// Authenticate a user with user name and password
        /// </summary>
        /// <param name="loginParameters">Value Of loginParameters</param>
        public void LoginAsync(LoginParameters loginParameters)
        {
            if (LoginComplete != null)
            {
                Deployment.Current.Dispatcher.BeginInvoke(() =>
                {
                    LoginComplete(this, new LoginOperationEventArgs(new Exception("")));
                });
            }
        }

        /// <summary>
        /// Logout User Asynchronously
        /// </summary>
        public void LogoutAsync()
        {
            if (LogoutComplete != null)
            {
                Deployment.Current.Dispatcher.BeginInvoke(() =>
                {
                    AppConfigurations.IsRunningOutOfBrowser = true;
                    
                    LogoutComplete(this, new LogoutOperationEventArgs(new Exception("FromUnitTest") ));
                    AppConfigurations.IsRunningOutOfBrowser = false;
                });
            }
        }

        /// <summary>
        /// public method call perform query to select user by his UserName
        /// </summary>
        /// <param name="UserName">Value Of UserName</param>
        public void GetUserByUserName(string UserName)
        {
            if (GetUserByUserNameComplete != null)
            {
                Deployment.Current.Dispatcher.BeginInvoke(() =>
                {
                    GetUserByUserNameComplete(this, new eNegEntityResultArgs<User>(_users));
                });
            }
        }

        /// <summary>
        /// public method to save any change occured in the object created from eNegcontext
        /// </summary>
        public void SaveChangesAsync()
        {

        }

        /// <summary>
        /// public method that call perform query to update user data when he logout
        /// </summary>
        /// <param name="UserID">Value Of UserID</param>
        public void MakeUserOffine(Guid? UserID)
        {
            if (MakeUserOfflineComplete != null)
            {
                Deployment.Current.Dispatcher.BeginInvoke(() =>
                {
                    MakeUserOfflineComplete(this, new eNegEntityResultArgs<User>(_users));
                });
            }
        }

        /// <summary>
        /// Gets application asynchronously
        /// </summary>
        public void GetApplicationAsync()
        {
            if (GetApplicationComplete != null)
            {
                Deployment.Current.Dispatcher.BeginInvoke(() =>
                {
                    GetApplicationComplete(this, new eNegEntityResultArgs<Data.Web.Application>(this.Applications));
                });
            }
        }

        /// <summary>
        /// public method that call perform query to update user data when he logout
        /// </summary>
        /// <param name="UserID">Value Of UserID</param>
        public void MakeUserOffline(Guid? UserID)
        {
            if (MakeUserOfflineComplete != null)
            {
                Deployment.Current.Dispatcher.BeginInvoke(() =>
                {
                    MakeUserOfflineComplete(this, new eNegEntityResultArgs<User>(_users));
                });
            }
        }

        #endregion
        
        #endregion        
    }
}
