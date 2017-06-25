#region → Usings   .
using citPOINT.eNeg.Common;
using citPOINT.eNeg.Data.Web;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.ServiceModel.DomainServices.Client;
using System.ServiceModel.DomainServices.Client.ApplicationServices;
using System.Windows.Messaging;
using System.Windows;
#endregion

#region → History  .

/* Date         User            Change
 * 
 * 03.08.10     Yousra Reda     creation
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

    #region  Using MEF to export MainPageViewModel
    /// <summary>
    /// This Class Is for MainPageViewModel
    /// </summary>
    [Export(ViewModelTypes.MainPageViewModel)]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    #endregion
    public class MainPageViewModel : ViewModelBase
    {
        #region → Fields         .
        /// <summary>
        /// LogInModel Instance
        /// </summary>
        public ILogInModel mLogInModel;
        private bool mIsBusy;
        private bool mIsLoggedOut;
        private bool mIsLoggedIn;
        private string mWelcomeText;
        private string mCurrentScreenText;
        private RelayCommand mLogoutCommand = null;
        private RelayCommand<string> mChangeScreenCommand = null;

        private bool mIsAdmin = false;
        private bool mIsUser = true;
        private string mUserName;
        #endregion Fields

        #region → Properties     .

        /// <summary>
        /// Gets a value indicating whether this instance is admin.
        /// </summary>
        /// <value><c>true</c> if this instance is admin; otherwise, <c>false</c>.</value>
        public bool IsAdmin
        {
            get
            {
                return mIsAdmin;
            }

            set
            {
                mIsAdmin = value;
                this.RaisePropertyChanged("IsAdmin");
            }
        }

        /// <summary>
        /// Gets a value indicating whether this instance is user.
        /// </summary>
        /// <value><c>true</c> if this instance is user; otherwise, <c>false</c>.</value>
        public bool IsUser
        {
            get
            {
                return mIsUser;
            }

            set
            {
                mIsUser = value;
                this.RaisePropertyChanged("IsUser");
            }
        }

        /// <summary>
        /// True if there is an operation (LoadUserAsync, 
        /// LoginAsync, or LogoutAsync) in progress; otherwise, false
        /// </summary>
        public bool IsBusy
        {
            get { return mIsBusy; }
            private set
            {
                if (value != mIsBusy)
                {
                    mIsBusy = value;
                    this.RaisePropertyChanged("IsBusy");
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is logged out.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is logged out; otherwise, <c>false</c>.
        /// </value>
        public bool IsLoggedOut
        {
            get { return mIsLoggedOut; }
            private set
            {
                if (value != mIsLoggedOut)
                {
                    mIsLoggedOut = value;
                    this.RaisePropertyChanged("IsLoggedOut");
                }
            }
        }

        /// <summary>
        /// true if there is someone Logged out; otherwise, false
        /// </summary>
        public bool IsLoggedIn
        {
            get { return mIsLoggedIn; }
            private set
            {
                if (value != mIsLoggedIn)
                {
                    mIsLoggedIn = value;
                    this.RaisePropertyChanged("IsLoggedIn");
                }
            }
        }

        /// <summary>
        /// carry the welcome text the will then be concatenated with Current LoggedIn UserName
        /// </summary>
        public string WelcomeText
        {
            get { return mWelcomeText; }
            private set
            {
                if (value != mWelcomeText)
                {
                    mWelcomeText = value;
                    this.RaisePropertyChanged("WelcomeText");
                }
            }
        }

        /// <summary>
        /// String carry the current screen we currently navigate
        /// </summary>
        public string CurrentScreenText
        {
            get { return mCurrentScreenText; }
            set
            {
                //if (value != mCurrentScreenText)
                //{
                mCurrentScreenText = value;
                this.RaisePropertyChanged("CurrentScreenText");
                //}
            }
        }

        /// <summary>
        /// Gets a value indicating whether this instance is organization owner.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is organization owner; otherwise, <c>false</c>.
        /// </value>
        public bool IsOrganizationOwner
        {
            get
            {
                return AppConfigurations.CurrentLoginUser != null ? AppConfigurations.CurrentLoginUser.IsOrganizationOwner : false;
            }
        }

        #endregion

        #region → Constructors   .


        /// <summary>
        /// Initializes a new instance of the <see cref="MainPageViewModel"/> class.
        /// </summary>
        /// <param name="loginModel">The login model.</param>
        [ImportingConstructor]
        public MainPageViewModel(ILogInModel loginModel)
        {

            #region Intilization
            mLogInModel = loginModel;
            // set the initial values
            this.IsBusy = mLogInModel.IsBusy;
            this.IsLoggedIn = mLogInModel.User.Identity.IsAuthenticated;
            this.IsLoggedOut = !(mLogInModel.User.Identity.IsAuthenticated);

            if (mLogInModel.User.Identity.IsAuthenticated)
            {
                LoginUser lu = mLogInModel.User as LoginUser;

                this.WelcomeText = "Welcome " + (string.IsNullOrEmpty(lu.FirstName) ? lu.EmailAddress : lu.FirstName.ToString() + " " + lu.LastName.ToString());
            }
            else
                this.WelcomeText = string.Empty;

            #endregion

            #region Set up event handling
            mLogInModel.AuthenticationChanged += new EventHandler<AuthenticationEventArgs>(_authenticationModel_AuthenticationChanged);
            mLogInModel.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(_authenticationModel_PropertyChanged);
            #endregion
        }

        #endregion Constructor

        #region → Event Handlers .
        
        /// <summary>
        /// Handles the AuthenticationChanged event of the _authenticationModel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.ServiceModel.DomainServices.Client.ApplicationServices.AuthenticationEventArgs"/> instance containing the event data.</param>
        private void _authenticationModel_AuthenticationChanged(object sender, AuthenticationEventArgs e)
        {
            if (e.User.Identity.IsAuthenticated)
            {
                LoginUser lu = e.User as LoginUser;

                if (lu.Locked || lu.Disabled)
                {
                    return;
                }
 
                this.WelcomeText = "Welcome " + (string.IsNullOrEmpty(lu.FirstName) ? lu.EmailAddress : lu.FirstName.ToString() + " " + lu.LastName.ToString());
                mUserName = mLogInModel.User.Identity.Name;


                if (e.User is LoginUser)
                {
                    IsAdmin = (e.User as LoginUser).eNegRoles.Contains("Admin");
                    IsUser = (e.User as LoginUser).eNegRoles.Contains("User");
                    
                    AppConfigurations.CurrentLoginUser.OrganizationOwnedID = lu.OrganizationOwnedID;

                    AppConfigurations.CurrentLoginUser.IsOrganizationOwner = lu.IsOrganizationOwner;

                    this.RaisePropertyChanged("IsOrganizationOwner");

                    AppConfigurations.CurrentLoginUser.PropertyChanged += new PropertyChangedEventHandler(CurrentLoginUser_PropertyChanged);
                }
            }
            else
                this.WelcomeText = string.Empty;

            this.IsLoggedIn = e.User.Identity.IsAuthenticated;
            this.IsLoggedOut = !(e.User.Identity.IsAuthenticated);
        }

        /// <summary>
        /// Handles the PropertyChanged event of the _authenticationModel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.ComponentModel.PropertyChangedEventArgs"/> instance containing the event data.</param>
        private void _authenticationModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "IsBusy":
                    this.IsBusy = mLogInModel.IsBusy;
                    break;
            }
        }

        /// <summary>
        /// Handles the PropertyChanged event of the CurrentLoginUser control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.ComponentModel.PropertyChangedEventArgs"/> instance containing the event data.</param>
        private void CurrentLoginUser_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "OrganizationOwnedID" || e.PropertyName == "IsOrganizationOwner")
            {
                this.RaisePropertyChanged("IsOrganizationOwner");
            }
        }

        #endregion Event Handlers

        #region → Commands       .

        /// <summary>
        /// Relay Command for logout process
        /// </summary>
        public RelayCommand LogoutCommand
        {
            get
            {
                if (mLogoutCommand == null)
                {
                    mLogoutCommand = new RelayCommand(() =>
                    {
                        try
                        {
                            if (!this.mLogInModel.IsLoggingOut)
                            {
                                this.IsAdmin = false;
                                this.IsUser = false;

                                eNegMessanger.ChangeScreenMessage.Send(ViewTypes.LoginView);
                                
                                this.mLogInModel.LogoutAsync();
                            }
                        }
                        catch (Exception ex)
                        {
                            // notify user if there is any error
                            eNegMessanger.RaiseErrorMessage.Send(ex);
                        }
                    });
                }
                return mLogoutCommand;
            }
        }

        /// <summary>
        /// Relay Command for Changing screen according to navigation
        /// </summary>
        public RelayCommand<string> ChangeScreenCommand
        {
            get
            {
                if (mChangeScreenCommand == null)
                {
                    mChangeScreenCommand = new RelayCommand<string>(g =>
                    {
                        try
                        {
                            if (this.CurrentScreenText != g || true)
                            {

                                this.CurrentScreenText = "";

                                switch (g)
                                {
                                    case ViewTypes.MainNegotiationView:
                                        eNegMessanger.ChangeScreenMessage.Send(ViewTypes.MainNegotiationView);
                                        this.CurrentScreenText = ViewTypes.MainNegotiationView;
                                        break;

                                    case ViewTypes.LoginView:
                                        eNegMessanger.ChangeScreenMessage.Send(ViewTypes.LoginView);
                                        this.CurrentScreenText = ViewTypes.LoginView;
                                        break;

                                    case ViewTypes.FullRegisterView:
                                        eNegMessanger.ChangeScreenMessage.Send(ViewTypes.FullRegisterView);
                                        this.CurrentScreenText = ViewTypes.FullRegisterView;
                                        break;

                                    case ViewTypes.ResetRequestView:
                                        eNegMessanger.ChangeScreenMessage.Send(ViewTypes.ResetRequestView);
                                        this.CurrentScreenText = ViewTypes.ResetRequestView;
                                        break;

                                    case ViewTypes.ResetView:
                                        eNegMessanger.ChangeScreenMessage.Send(ViewTypes.ResetView);
                                        this.CurrentScreenText = ViewTypes.ResetView;
                                        break;

                                    case ViewTypes.ConfirmMailView:
                                        eNegMessanger.ChangeScreenMessage.Send(ViewTypes.ConfirmMailView);
                                        this.CurrentScreenText = ViewTypes.ConfirmMailView;
                                        break;
                                     
                                    case ViewTypes.AcceptToBeOwnerView:
                                        eNegMessanger.ChangeScreenMessage.Send(ViewTypes.AcceptToBeOwnerView);
                                        this.CurrentScreenText = ViewTypes.AcceptToBeOwnerView;
                                        break;
                                    
                                    case ViewTypes.RefuseToBeOwnerView:
                                        eNegMessanger.ChangeScreenMessage.Send(ViewTypes.RefuseToBeOwnerView);
                                        this.CurrentScreenText = ViewTypes.RefuseToBeOwnerView;
                                        break;

                                    case ViewTypes.AcceptToBeOwner_RemoveOriginalView:
                                        eNegMessanger.ChangeScreenMessage.Send(ViewTypes.AcceptToBeOwner_RemoveOriginalView);
                                        this.CurrentScreenText = ViewTypes.AcceptToBeOwnerView;
                                        break;
                                    
                                    case ViewTypes.MyProfile:
                                        eNegMessanger.ChangeScreenMessage.Send(ViewTypes.MyProfile);
                                        this.CurrentScreenText = ViewTypes.MyProfile;
                                        break;

                                    case ViewTypes.UsersOverview:
                                        eNegMessanger.ChangeScreenMessage.Send(ViewTypes.UsersOverview);
                                        this.CurrentScreenText = ViewTypes.UsersOverview;
                                        break;

                                    case ViewTypes.PublishedNegotiations:
                                        eNegMessanger.ChangeScreenMessage.Send(ViewTypes.PublishedNegotiations);
                                        this.CurrentScreenText = ViewTypes.PublishedNegotiations;
                                        break;

                                    case ViewTypes.PublishedProfiles:
                                        eNegMessanger.ChangeScreenMessage.Send(ViewTypes.PublishedProfiles);
                                        this.CurrentScreenText = ViewTypes.PublishedProfiles;
                                        break;

                                    case ViewTypes.AddonView:
                                        eNegMessanger.ChangeScreenMessage.Send(ViewTypes.AddonView);
                                        this.CurrentScreenText = ViewTypes.AddonView;
                                        break;
                                    default:
                                        eNegMessanger.ChangeScreenMessage.Send(g);
                                        this.CurrentScreenText = g;
                                        break;
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            // notify user if there is any error
                            eNegMessanger.RaiseErrorMessage.Send(ex);
                        }
                    }, g => g != null);
                }
                return mChangeScreenCommand;
            }
        }

        #endregion Commands

        
    }
}
