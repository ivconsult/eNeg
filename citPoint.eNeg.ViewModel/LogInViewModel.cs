#region → Usings   .
using citPOINT.eNeg.Common;
using citPOINT.eNeg.Data.Web;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.DataAnnotations;
using System.IO.IsolatedStorage;
using System.Linq;
using System.ServiceModel.DomainServices.Client.ApplicationServices;
using System.Windows.Browser;
using System.ServiceModel.DomainServices.Client;
using System.Windows.Input;
using System.Windows;
using System.Net;
using System.Threading;
using System.Diagnostics;

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

namespace citPOINT.eNeg.ViewModel
{

    #region " Using MEF to export LoginFormViewModel "
    /// <summary>
    /// for LogIn out 
    /// </summary>
    [Export(ViewModelTypes.LoginFormViewModel)]
    [PartCreationPolicy(CreationPolicy.Shared)]
    #endregion
    public class LogInViewModel : ViewModelBase
    {

        #region → Fields         .
        private ILogInModel mLogInModel;
        private string mLoginScreenErrorMessage;
        private LoginUser mCurrentUser;
        private bool mIsLoginDone;

        private RelayCommand mlogoutCommand = null;
        private RelayCommand<LoginUser> mloginCommand = null;
        private string mWelcome;
        private SessionContext mSessionContext;
        private RelayCommand<string> mLoginViewNavigatationCommand = null;
        private WebClient InvokePageService;
        private bool mIsAddonLinkVisible;
        #endregion Fields

        #region → Properties     .

        /// <summary>
        /// To indicate whether login operation done or not yet
        /// </summary>
        public bool IsLoginDone
        {
            get { return mIsLoginDone; }
            set { mIsLoginDone = value; }
        }

        /// <summary>
        /// Gets or sets the main page view model.
        /// </summary>
        /// <value>The main page view model.</value>
        public MainPageViewModel MainPageViewModel { get; set; }

        /// <summary>
        /// Property with setter  getter to contain the current User after login process
        /// </summary>
        public LoginUser CurrentUser
        {
            get
            {
                return mCurrentUser;
            }
            set
            {
                if (value != mCurrentUser)
                {
                    mCurrentUser = value;
                    this.RaisePropertyChanged("CurrentUser");
                }
            }
        }

        /// <summary>
        /// String that carry the any error happen in logging In process 
        /// </summary>
        public string LoginScreenErrorMessage
        {
            get
            {
                return mLoginScreenErrorMessage;
            }
            private set
            {
                if (value != mLoginScreenErrorMessage)
                {
                    mLoginScreenErrorMessage = value;
                    RaisePropertyChanged("LoginScreenErrorMessage");
                }
            }
        }

        /// <summary>
        /// carry the welcome text the will then be concatenated with Current LoggedIn UserName
        /// </summary>
        public string Welcome
        {
            get { return mWelcome; }
            private set
            {
                if (value != mWelcome)
                {
                    mWelcome = value;
                    this.RaisePropertyChanged("Welcome");
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is addon.
        /// </summary>
        /// <value><c>true</c> if this instance is addon; otherwise, <c>false</c>.</value>
        public bool IsAddonLinkVisible
        {
            get { return mIsAddonLinkVisible; }
            set
            {
                mIsAddonLinkVisible = value;
                RaisePropertyChanged("IsAddonLinkVisible");
            }
        }


        /// <summary>
        /// Gets a value indicating whether this instance is for certain page.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is for certain page; otherwise, <c>false</c>.
        /// </value>
        private bool IsForCertainPage
        {
            get
            {
                if (!HtmlPage.Document.QueryString.ContainsKey("PageName") || string.IsNullOrEmpty(HtmlPage.Document.QueryString.ContainsKey("PageName").ToString()))
                {
                    return false;
                }

                string pageName = HtmlPage.Document.QueryString["PageName"].ToString();

                if (pageName == ViewTypes.ProfileDetails ||
                    pageName == ViewTypes.MyProfile ||
                    pageName == ViewTypes.OrganizationManagement)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }

        /// <summary>
        /// Gets or sets the application VM.
        /// </summary>
        /// <value>The application VM.</value>
        [Import(ViewModelTypes.ApplicationViewModel, AllowRecomposition = false)]
        public ApplicationViewModel ApplicationVM { get; set; }

        #endregion

        #region → Constructors   .

        /// <summary>
        /// constructor that take a parameter IlogInModel and take its values to mLoginModel and regitser required events 
        /// </summary>
        /// <param name="LogInModel">Interface for loginModel</param>
        [ImportingConstructor]
        public LogInViewModel(ILogInModel LogInModel)
        {
            #region Intilization
            mLogInModel = LogInModel;
            IsAddonLinkVisible = !AppConfigurations.IsAddon;

            try
            {
                if (!AppConfigurations.IsRunningOutOfBrowser)
                    mSessionContext = new SessionContext();
            }
            catch { }

            // Clear any previous error message
            this.LoginScreenErrorMessage = null;

            // Clear the user name and password
            this.CurrentUser = new LoginUser();
            #endregion

            #region Set up event handling
            mLogInModel.AuthenticationChanged += new EventHandler<AuthenticationEventArgs>(mLogInModel_AuthenticationChanged);
            mLogInModel.LoginComplete += new EventHandler<LoginOperationEventArgs>(mLogInModel_LoginComplete);
            mLogInModel.LogoutComplete += new EventHandler<LogoutOperationEventArgs>(mLogInModel_LogoutComplete);
            mLogInModel.LoadUserComplete += new EventHandler<LoadUserOperationEventArgs>(mLogInModel_LoadUserComplete);
            mLogInModel.GetUserByUserNameComplete += new EventHandler<eNegEntityResultArgs<User>>(mLogInModel_GetUserByUserNameComplete);
            #endregion

            #region User Welcome Message

            if (mLogInModel.User.Identity.IsAuthenticated)
            {
                LoginUser lu = mLogInModel.User as LoginUser;


                this.Welcome = "Welcome " + (string.IsNullOrEmpty(lu.FirstName) ? lu.EmailAddress : lu.FirstName.ToString() + " " + lu.LastName.ToString());

            }
            else
                this.Welcome = string.Empty;
            #endregion

            try
            {
                if ((AppConfigurations.IsAddon && AppConfigurations.IsRunningOutOfBrowser)
                    || !IsForCertainPage)
                {
                    mLogInModel.LoadUserAsync();
                }
            }
            catch { }
        }


        #endregion Constructor

        #region → Event Handlers .

        /// <summary>
        /// remove appSettings in case of running Add-On
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">LogoutOperationEventArgs</param>
        private void mLogInModel_LogoutComplete(object sender, LogoutOperationEventArgs e)
        {
            if (!e.HasError || (e.HasError && e.Error.Message == "FromUnitTest"))
            {
                if (!AppConfigurations.IsRunningOutOfBrowser)
                {
                    mSessionContext.SetSessionValue("SessionUser", string.Empty);
                }

                if (!e.HasError || (e.HasError && e.Error.Message != "FromUnitTest"))
                {
                    IsolatedStorageSettings appSettings = IsolatedStorageSettings.ApplicationSettings;
                    appSettings.Remove("UserName");
                    appSettings.Save();
                }

                IsLoginDone = false;

                if (AppConfigurations.CurrentLoginUser != null)
                    mLogInModel.MakeUserOffline(AppConfigurations.CurrentLoginUser.UserID);

                eNegMessanger.ChangeScreenMessage.Send(ViewTypes.LoginView);

            }
            else
            {
                eNegMessanger.RaiseErrorMessage.Send(e.Error);
            }
        }

        /// <summary>
        /// Navigate User automatically to MainNegotiationView if the user is authenticated
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">LoadUserOperationEventArgs</param>
        private void mLogInModel_LoadUserComplete(object sender, LoadUserOperationEventArgs e)
        {
            IsolatedStorageSettings appSettings = IsolatedStorageSettings.ApplicationSettings;

            if (appSettings.Contains("TempUserName"))
            {
                appSettings.Remove("TempUserName");
                appSettings.Save();
            }
            if (!e.HasError)
            {

                LoginUser lu = (e.LoginOp.User as LoginUser);

                if (string.IsNullOrEmpty(lu.EmailAddress) || lu.Locked || lu.Disabled)
                {
                    eNegMessanger.ChangeScreenMessage.Send(ViewTypes.LoginView);
                    return;
                }
                else
                {
                    this.IntializeAppIntegration();
                }
            }
            else
            {
                eNegMessanger.RaiseErrorMessage.Send(e.Error);
            }
        }


        /// <summary>
        /// Save appSettings for UserName according to the user choice of "Keep Me Signed In" option
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">LoginOperationEventArgs</param>
        private void mLogInModel_LoginComplete(object sender, LoginOperationEventArgs e)
        {
            if (e.LoginOp == null)
                return;
            if (e.LoginOp.LoginSuccess)
            {
                IsLoginDone = true;
                AppConfigurations.CurrentLoginUser = (e.LoginOp.User as LoginUser);


                if (AppConfigurations.CurrentLoginUser.Locked == true)
                {
                    ResendMailProcess();
                    this.CurrentUser.ValidationErrors.Add(new ValidationResult(Resources.AccountLocked, new string[] { "EmailAddress" }));
                    return;
                }

                ModelBase.Rights = (IList<string>)(e.LoginOp.User as LoginUser).eNegRights;

                ManageRememberMe();
                MakeUserOnline();

            }
            else if (e.HasError)
            {
                IsLoginDone = false;
                LoginScreenErrorMessage = e.LoginOp.Error.Message;
                GetTempUser();
                this.CurrentUser.ValidationErrors.Add(new ValidationResult(Resources.WrongPassword, new string[] { "Password" }));
                return;
            }
        }

        /// <summary>
        /// Update in User data (online, LastLoginDate) in Database after login process complete
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">eNegEntityResultArgs for User</param>
        private void mLogInModel_GetUserByUserNameComplete(object sender, eNegEntityResultArgs<User> e)
        {
            User found = e.Results.FirstOrDefault();

            if (found != null)
            {
                if (found.Disabled)
                {
                    GetTempUser();
                    this.CurrentUser.ValidationErrors.Add(new ValidationResult(Resources.AccountDisabled, new string[] { "EmailAddress" }));
                    return;
                }
                if (found.Locked)
                {
                    GetTempUser();
                    this.CurrentUser.ValidationErrors.Add(new ValidationResult(Resources.AccountLocked, new string[] { "EmailAddress" }));
                    return;
                }

                //If the user who want to login is not locked or not disabled, Call login service to authenticate him
                LoginAsync();
            }
            else
            {
                GetTempUser();
                this.CurrentUser.ValidationErrors.Add(new ValidationResult(Resources.WrongCredentials, new string[] { "EmailAddress" }));
                return;
            }
        }

        /// <summary>
        /// For handling Authentication Changed
        /// </summary>
        /// <param name="sender">Value of Sender</param>
        /// <param name="e">Value of AuthenticationEventArgs</param>
        void mLogInModel_AuthenticationChanged(object sender, AuthenticationEventArgs e)
        {
            if (e.User.Identity.IsAuthenticated)
            {
                LoginUser lu = e.User as LoginUser;

                if (lu.Locked || lu.Disabled)
                {
                    return;
                }

                if (!AppConfigurations.IsRunningOutOfBrowser)
                {
                    //Set session value for email address if user is authenticated.
                    mSessionContext.SetSessionValue("SessionUser", lu.EmailAddress);
                }

                IsolatedStorageSettings appSettings = IsolatedStorageSettings.ApplicationSettings;

                #region → in case that user was last sign in was persistent     .
                if (appSettings.Contains("UserName"))
                {
                    //This mean that this load is coming from eNeg 
                    //to addon to start new negotiation with "Remember Me"
                    AppConfigurations.CurrentLoginUser.IsPersistent = true;
                }
                #endregion

                #region → in case that user was last sign in was not persistent .
                else
                {
                    //This mean that this load is coming from eNeg 
                    //to addon to start new negotiation without "Remember Me"
                    AppConfigurations.CurrentLoginUser.IsPersistent = false;
                }

                #endregion


                this.Welcome = "Welcome " + (string.IsNullOrEmpty(lu.FirstName) ? lu.EmailAddress : lu.FirstName.ToString() + " " + lu.LastName.ToString());

                if (AppConfigurations.IsRunningOutOfBrowser)
                {
                    this.IntializeAppIntegration();
                }
            }
            else
            {
                this.Welcome = string.Empty;
            }
        }

        /// <summary>
        /// Handles the DownloadStringCompleted event of the InvokePageService control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Net.DownloadStringCompletedEventArgs"/> instance containing the event data.</param>
        void InvokePageService_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                // To Do:
                // any action need to be done after invoking page of different app 
                // to save cookies in case of running out of browser
            }
            else
            {
                eNegMessanger.RaiseErrorMessage.Send(e.Error);
            }
        }

        /// <summary>
        /// Sets the session complete.
        /// </summary>
        /// <param name="invOp">The inv op.</param>
        public void SetSessionComplete(InvokeOperation invOp)
        {
            if (!invOp.HasError)
            {
                if (IsLoginDone)
                {
                    this.IntializeAppIntegration();
                }
            }
            else
            {
                eNegMessanger.RaiseErrorMessage.Send(invOp.Error);
            }
        }

        #endregion

        #region → Commands       .

        /// <summary>
        /// Gets the login view navigatation command.
        /// </summary>
        /// <value>The login view navigatation command.</value>
        public RelayCommand<string> LoginViewNavigatationCommand
        {
            get
            {
                if (mLoginViewNavigatationCommand == null)
                {
                    mLoginViewNavigatationCommand = new RelayCommand<string>((s) =>
                    {
                        try
                        {
                            string PageName = s.Replace(" ", string.Empty);

                            #region → If Application is Add-on       .
                            if (AppConfigurations.IsAddon)
                            {
                                #region → If the Application is Add-on & OOB  .
                                if (AppConfigurations.IsRunningOutOfBrowser)
                                {
                                    switch (PageName)
                                    {
                                        case ViewTypes.QuickRegisterView:
                                            eNegMessanger.FlippMessage.Send(ViewTypes.NavigateQuickRegisterView);
                                            break;
                                        case ViewTypes.FullRegisterView:
                                            eNegMessanger.FlippMessage.Send(ViewTypes.NavigateFullRegisterView);
                                            break;
                                        case ViewTypes.ForgetLoginView:
                                            eNegMessanger.FlippMessage.Send(ViewTypes.NavigateForgetLoginView);
                                            break;
                                        case ViewTypes.MyProfile:
                                            eNegNavigation.NavigateToUrl(AppConfigurations.HostBaseAddress +
                                                                         "Default.aspx?PageName=MyProfile");
                                            break;
                                        case ViewTypes.MainPlatformView:
                                            eNegNavigation.NavigateToUrl(AppConfigurations.HostBaseAddress + "Default.aspx");
                                            break;
                                    }
                                }
                                #endregion

                                #region → If Application is Addon & In Browser.
                                else
                                {
                                    switch (PageName)
                                    {
                                        case ViewTypes.QuickRegisterView:
                                            HtmlPage.Window.Navigate(new Uri(AppConfigurations.HostBaseAddress + "Default.aspx?PageName=QuickRegister", UriKind.Absolute));
                                            break;
                                        case ViewTypes.FullRegisterView:
                                            HtmlPage.Window.Navigate(new Uri(AppConfigurations.HostBaseAddress + "Default.aspx?PageName=FullRegister", UriKind.Absolute));
                                            break;
                                        case ViewTypes.ForgetLoginView:
                                            HtmlPage.Window.Navigate(new Uri(AppConfigurations.HostBaseAddress + "Default.aspx?PageName=ResetRequest", UriKind.Absolute));
                                            break;


                                        case ViewTypes.MyProfile:
                                            eNegNavigation.NavigateToUrl(AppConfigurations.HostBaseAddress +
                                                                         "Default.aspx?PageName=MyProfile");
                                             break;

                                        case ViewTypes.MainPlatformView:
                                             eNegMessanger.ChangeScreenMessage.Send(ViewTypes.MainPlatformView);
                                            break;
                                    }
                                }
                                #endregion
                            }
                            #endregion

                            #region → If Application is web platform .
                            else
                            {
                                switch (PageName)
                                {
                                    case ViewTypes.QuickRegisterView:
                                        eNegMessanger.FlippMessage.Send(ViewTypes.QuickRegisterView);
                                        break;
                                    case ViewTypes.FullRegisterView:
                                        eNegMessanger.ChangeScreenMessage.Send(ViewTypes.FullRegisterView);
                                        break;
                                    case ViewTypes.ForgetLoginView:
                                        eNegMessanger.ChangeScreenMessage.Send(ViewTypes.ResetRequestView);
                                        break;
                                    case ViewTypes.AddonView:
                                        HtmlPage.Window.Navigate(new Uri(AppConfigurations.HostBaseAddress + "Addon.aspx", UriKind.Absolute));
                                        break;
                                }
                            }
                            #endregion
                        }
                        catch (Exception ex)
                        {
                            // notify user if there is any error
                            eNegMessanger.RaiseErrorMessage.Send(ex);
                        }
                    });
                }
                return mLoginViewNavigatationCommand;
            }
        }

        /// <summary>
        /// Relay Command for logout process
        /// </summary>
        public RelayCommand LogoutCommand
        {
            get
            {
                if (mlogoutCommand == null)
                {
                    mlogoutCommand = new RelayCommand(() =>
                    {
                        try
                        {
                            if (!this.mLogInModel.IsLoggingOut)
                            {
                                this.CurrentUser = new LoginUser();

                                LogoutAsync();
                            }
                        }
                        catch (Exception ex)
                        {
                            // notify user if there is any error
                            eNegMessanger.RaiseErrorMessage.Send(ex);
                        }
                    });
                }
                return mlogoutCommand;
            }
        }

        /// <summary>
        /// Relay Command for login process
        /// </summary>
        public RelayCommand<LoginUser> LoginCommand
        {
            get
            {
                if (mloginCommand == null)
                {
                    mloginCommand = new RelayCommand<LoginUser>(g =>
                    {
                        if (!this.mLogInModel.IsLoggingIn)
                        {
                            if (this.CurrentUser.EmailAddress == null)
                                this.CurrentUser.EmailAddress = string.Empty;

                            if (this.CurrentUser.Password == null)
                                this.CurrentUser.Password = string.Empty;


                            // clear any previous error message
                            this.LoginScreenErrorMessage = null;
                            // only call loginAsyc when all input are validated
                            if (g.TryValidateProperty("EmailAddress") && g.TryValidateProperty("Password"))
                            {
                                mLogInModel.GetUserByUserName(CurrentUser.EmailAddress);
                            }
                        }
                    }, g => g != null);
                }
                return mloginCommand;
            }
        }

        #endregion

        #region → Methods        .

        #region → Private        .

        /// <summary>
        /// Gets the temp user.
        /// </summary>
        private void GetTempUser()
        {
            string temp = CurrentUser.EmailAddress;
            CurrentUser = new LoginUser();
            CurrentUser.EmailAddress = temp;
        }

        /// <summary>
        /// Makes the user online.
        /// </summary>
        private void MakeUserOnline()
        {
            //Update Login Data 
            var found = mLogInModel.Context.Users.Where(s => s.UserID == AppConfigurations.CurrentLoginUser.UserID).First();
            found.LastLoginDate = DateTime.Now;
            found.Online = true;
            found.Password = CurrentUser.Password;
            found.PasswordConfirmation = CurrentUser.Password;

            if (found.TryValidateObject()
                && found.TryValidateProperty("EmailAddress")
                && found.TryValidateProperty("Password")
                && found.TryValidateProperty("PasswordConfirmation"))
            {
                mLogInModel.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Check On whether we are working on addon and if so save or remove 
        /// "keep me signed in" configurations from or in to Isolated Storage
        /// </summary>
        private void ManageRememberMe()
        {
            if (!AppConfigurations.IsRunningOutOfBrowser)
                mSessionContext.SetSessionValue("SessionUser", CurrentUser.EmailAddress, new Action<InvokeOperation>(SetSessionComplete), null);

            IsolatedStorageSettings appSettings = IsolatedStorageSettings.ApplicationSettings;
            
            if (CurrentUser.IsPersistent)
            {
                if (!appSettings.Contains("UserName"))
                {
                    appSettings.Add("UserName", CurrentUser.EmailAddress);
                }
                else
                {
                    appSettings["UserName"] = CurrentUser.EmailAddress;
                }

                appSettings.Save();
            }
        }


        /// <summary>
        /// Resends the mail process.
        /// </summary>
        private void ResendMailProcess()
        {
            MailHelper MailHelper = new MailHelper();
            MailHelper.MailSendComplete += (InvoOp) =>
            {
                if (!InvoOp.HasError)
                {
                    var found = mLogInModel.Context.Users.Where(s => s.UserID == AppConfigurations.CurrentLoginUser.UserID).First();
                    found.Locked = true;
                    found.Password = CurrentUser.Password;
                    found.PasswordConfirmation = CurrentUser.Password;

                    mLogInModel.SaveChangesAsync();

                    eNegMessage Message = new eNegMessage(Resources.NewUserCreatedText);

                    //this is flag to indicate which one will recieve this sent
                    Message.ViewName = ViewTypes.LoginView;

                    eNegMessanger.SendCustomMessage.Send(Message);
                }
                else
                {
                    eNegMessanger.RaiseErrorMessage.Send(InvoOp.Error);
                }
            };

            MailHelper.SendConfirmationMail(AppConfigurations.CurrentLoginUser.EmailAddress, AppConfigurations.CurrentLoginUser.FirstName, AppConfigurations.CurrentLoginUser.LastName, AppConfigurations.CurrentLoginUser.OperationStringUrl);
        }

        /// <summary>
        /// Intializes the app integration.
        /// </summary>
        private void IntializeAppIntegration()
        {
            ApplicationVM.IsUserLoggedIn = true;

            if (ApplicationVM.IsAplicationsLoaded)
            {
                eNegMessanger.ApplyChangesForAppsMessage.Send(new Common.eNegApps.ApplicationChange()
                {
                    IsForUserChanges = true,
                    ApplyChanged = true,
                    CurrentUser = AppConfigurations.CurrentLoginUser
                });


                eNegMessanger.ChangeScreenMessage.Send(ViewTypes.MainNegotiationView);
            }
        }

        #endregion Private

        #region → Public         .


        /// <summary>
        ///  Used to can Logout using eNeg Login Service
        /// </summary>
        public void LogoutAsync()
        {
            this.mLogInModel.LogoutAsync();
        }

        /// <summary>
        /// Used to can LoginUser using eNeg Login Service
        /// </summary>
        public void LoginAsync()
        {
            this.mLogInModel.LoginAsync(new LoginParameters
                    (CurrentUser.EmailAddress, CurrentUser.Password, CurrentUser.IsPersistent, null));
        }

        #endregion public

        #endregion Methods
    }
}
