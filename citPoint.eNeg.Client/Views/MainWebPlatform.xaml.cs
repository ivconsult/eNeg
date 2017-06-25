
#region → Usings   .
using System;
using System.ComponentModel.Composition;
using System.Windows;
using System.Windows.Controls;
using citPOINT.eNeg.Common;
using citPOINT.eNeg.Infrastructure.ExceptionHandling;
using citPOINT.eNeg.ViewModel;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using Telerik.Windows.Controls;
using System.Windows.Media;
#endregion

#region → History  .

/* Date         User            Change
 * 
 * 21.03.12     Yousra Reda     creation
 */

# endregion

#region → ToDos    .

/*
 * Date         set by User     Description
 * 
 * 
*/

# endregion

namespace citPOINT.eNeg.Client
{
    /// <summary>
    /// User control used as the main page for web plateform
    /// </summary>
    public partial class MainWebPlatform : UserControl, ICleanup
    {
        #region → Fields         .
        string mPageName;
        string mOperationString;
        string mPageRequiredLogin;
        private DialogMessage lastDialogMessage;
        #endregion

        #region → Properties     .

        #region " Using MEF to import MainPageViewModel "

        /// <summary>
        /// Gets or sets the view model.
        /// </summary>
        /// <value>The view model.</value>
        [Import(ViewModelTypes.MainPageViewModel)]
        public MainPageViewModel ViewModel
        {
            set
            {
                DataContext = value;
            }

            get
            {
                return DataContext as MainPageViewModel;
            }
        }

        #endregion

        #endregion

        #region → Constructors   .

        /// <summary>
        /// Default Constuctor
        /// </summary>
        /// <param name="PageName">Name of the page.</param>
        /// <param name="OperationString">The operation string.</param>
        public MainWebPlatform(string PageName, string OperationString)
        {
            InitializeComponent();

            if (PageName == ViewTypes.MyProfile ||
                PageName == ViewTypes.ProfileDetails ||
                PageName == ViewTypes.OrganizationManagement)
            {
                mPageRequiredLogin = PageName;
                PageName = ViewTypes.LoadUser;
            }

            this.mPageName = PageName;

            this.mOperationString = OperationString;

            #region → Use MEF To load the View Model                     .
            if (!GalaSoft.MvvmLight.ViewModelBase.IsInDesignModeStatic)
            {
                CompositionInitializer.SatisfyImports(this);
            }
            #endregion

            #region → Registeration for needed messages in eNegMessenger .

            eNegMessanger.ChangeScreenMessage.Register(this, OnChangeScreenMessage);
            eNegMessanger.RaiseErrorMessage.Register(this, OnRaiseErrorMessage);
            eNegMessanger.ConfirmMessage.Register(this, OnConfirmMessage);

            #endregion

            #region → pen Reset page Or Loging page                      .

            if (PageName == ViewTypes.ResetView ||
                PageName == ViewTypes.ResetRequestView ||
                PageName == ViewTypes.FullRegisterView ||
                PageName == ViewTypes.QuickRegisterView ||
                PageName == ViewTypes.ConfirmMailView ||
                PageName == ViewTypes.AcceptToBeOwnerView ||
                PageName == ViewTypes.RefuseToBeOwnerView ||
                PageName == ViewTypes.AcceptToBeOwner_RemoveOriginalView ||
                PageName == ViewTypes.LoadUser
                )
            {
                eNegMessanger.ChangeScreenMessage.Send(PageName);
            }
            #endregion

            this.CloseConfirmationWindows += new EventHandler<WindowClosedEventArgs>(MainPageAddon_CloseConfirmationWindows);
        }

        #endregion

        #region → Events         .

        /// <summary>
        /// Occurs when [close confirmation windows].
        /// </summary>
        private event EventHandler<WindowClosedEventArgs> CloseConfirmationWindows;

        #endregion

        #region → Event Handlers .

        /// <summary>
        /// Handles the CloseConfirmationWindows event of the MainPageAddon control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Windows.Controls.WindowClosedEventArgs"/> instance containing the event data.</param>
        private void MainPageAddon_CloseConfirmationWindows(object sender, WindowClosedEventArgs e)
        {
            if (e.DialogResult.HasValue && e.DialogResult.Value == true)
            {
                lastDialogMessage.ProcessCallback(MessageBoxResult.OK);
            }
            else
            {
                lastDialogMessage.ProcessCallback(MessageBoxResult.Cancel);
            }
        }

        #endregion

        #region → Methods        .

        #region → Private        .

        /// <summary>
        /// change scrren according to the sent parameter
        /// </summary>
        /// <param name="changeScreen"></param>
        private void OnChangeScreenMessage(string changeScreen)
        {
            #region call Cleanup() on the current screen before switching

            //solve problem of closing send reset request.
            //if (changeScreen == ViewTypes.LoginView &&
            //    this.uxContentLoginPage.Content != null &&
            //    (this.uxContentLoginPage.Content is Login_QuickRegisterView))
            //{
            //    return;
            //}

            ICleanup currentScreen = this.uxMainContent.Content as ICleanup;
            if (currentScreen != null && !(currentScreen is NegotiationsConversationDetails))
                currentScreen.Cleanup();

            currentScreen = this.uxContentLoginPage.Content as ICleanup;
            if (currentScreen != null)
                currentScreen.Cleanup();

            #endregion

            #region Swich on Curent screen Name to change the view according to that
            switch (changeScreen)
            {
                case ViewTypes.LoadUser:
                    this.uxContentLoginPage.Content = new LoadUserView();
                    break;

                case ViewTypes.MainNegotiationView:
                    (this.ViewModel as MainPageViewModel).CurrentScreenText = ViewTypes.MainNegotiationView;
                    
                    uxMainContent.VerticalAlignment = VerticalAlignment.Stretch;
                    uxMainContent.VerticalContentAlignment = VerticalAlignment.Stretch;


                    uxMainContent.HorizontalAlignment = HorizontalAlignment.Stretch;
                    uxMainContent.HorizontalContentAlignment = HorizontalAlignment.Stretch;

                    this.uxMainContent.Content = new NegotiationsConversationDetails();


                    if (!string.IsNullOrEmpty(mPageRequiredLogin))
                    {
                        if (mPageRequiredLogin == ViewTypes.OrganizationManagement)
                        {
                            if (this.ViewModel.IsOrganizationOwner)
                            {
                                this.OnChangeScreenMessage(mPageRequiredLogin);
                                return;
                            }
                        }
                        else
                        {
                            this.OnChangeScreenMessage(mPageRequiredLogin);
                            return;
                        }
                    }

                    break;

                case ViewTypes.LoginView:
                    this.uxContentLoginPage.Content = new Login_QuickRegisterView();
                    (uxContentLoginPage.Content as Login_QuickRegisterView).uxLoginView.Cleanup();
                    break;

                case ViewTypes.QuickRegisterView:
                    this.uxContentLoginPage.Content = new Login_QuickRegisterView();
                    (uxContentLoginPage.Content as Login_QuickRegisterView).uxLoginView.Cleanup();
                    eNegMessanger.FlippMessage.Send();
                    break;

                case ViewTypes.FullRegisterView:
                    this.uxContentLoginPage.Content = new FullRegisterView();
                    break;

                case ViewTypes.ResetRequestView:
                    PopUpWindow RequestResetWindow = new PopUpWindow("Request Reset Login");
                    RequestResetWindow.Content = new ResetRequestView();
                    //Pass the original view width & height to can center the PopUp
                    RequestResetWindow.CenterWindow();
                    RequestResetWindow.ShowDialog();
                    break;

                case ViewTypes.ResetView:
                    PopUpWindow ResetWindow = new PopUpWindow("Request Reset Login");
                    ResetWindow.Content = new ResetView(this.mOperationString);
                    //Pass the original view width & height to can center the PopUp
                    ResetWindow.CenterWindow();
                    ResetWindow.ShowDialog();
                    break;

                case ViewTypes.ConfirmMailView:
                    this.uxContentLoginPage.Content = new ConfirmMailView(this.mOperationString, eNegConstant.UserOperations.Confiramtion);
                    break;

                case ViewTypes.AcceptToBeOwnerView:
                    this.uxContentLoginPage.Content = new ConfirmMailView(this.mOperationString, eNegConstant.UserOperations.AcceptOwnerRequest);
                    break;

                case ViewTypes.RefuseToBeOwnerView:
                    this.uxContentLoginPage.Content = new ConfirmMailView(this.mOperationString, eNegConstant.UserOperations.RefuseOwnerRequest);
                    break;

                case ViewTypes.AcceptToBeOwner_RemoveOriginalView:
                    this.uxContentLoginPage.Content = new ConfirmMailView(this.mOperationString, eNegConstant.UserOperations.Accept_DeleteOwnerRequest);
                    break;


                case ViewTypes.MyProfile:

                    AppConfigurations.ProfileUserID = AppConfigurations.CurrentLoginUser.UserID;
                    PopUpWindow pWindow = new PopUpWindow("My Profile");
                    pWindow.Content = new MasterMyProfileView();

                    //Pass the original view width & height to can center the PopUp
                    double height = App.Current.Host.Content.ActualHeight;

                    height = height < 680 ? 680 : height;

                    height = height > 800 ? 800 : height;

                    pWindow.CenterWindow(920, height);

                    pWindow.ShowDialog();

                    break;

                case ViewTypes.ProfileDetails:

                    PopUpWindow prfileWindow = new PopUpWindow("User Profile");

                    prfileWindow.Content = new PublishedProfileDetailsView(AppConfigurations.ProfileUserID);

                    //Pass the original view width & height to can center the PopUp
                    prfileWindow.CenterWindow(Content.RenderSize.Width, Content.RenderSize.Height);

                    prfileWindow.ShowDialog();

                    this.mPageRequiredLogin = "";

                    break;

                case ViewTypes.UsersOverview:
                    PopUpWindow ManageUsersWindow = new PopUpWindow("Manage Users");
                    ManageUsersWindow.Content = new ManageUsers();

                    //Pass the original view width & height to can center the PopUp
                    ManageUsersWindow.CenterWindow(Content.RenderSize.Width, Content.RenderSize.Height);

                    ManageUsersWindow.ShowDialog();
                    break;

                case ViewTypes.PublishedProfiles:
                    PopUpWindow PublicUsersWindow = new PopUpWindow("Public Users");
                    PublicUsersWindow.Content = new PublicUsers();
                    //Pass the original view width & height to can center the PopUp
                    PublicUsersWindow.CenterWindow(Content.RenderSize.Width, Content.RenderSize.Height);
                    PublicUsersWindow.ShowDialog();
                    break;

                case ViewTypes.OrganizationManagement:
                    PopUpWindow ManageOrganizationWindow = new PopUpWindow("Manage Organization");
                    ManageOrganizationWindow.Content = new ManageOrganizationView();
                    //Pass the original view width & height to can center the PopUp
                    ManageOrganizationWindow.CenterWindow(Content.RenderSize.Width, Content.RenderSize.Height);
                    ManageOrganizationWindow.ShowDialog();
                    this.ViewModel.CurrentScreenText = changeScreen;
                    this.mPageRequiredLogin = "";
                    break;



            }

            #endregion
        }

        /// <summary>
        /// Raise error message if there is any layer send RaiseErrorMessage
        /// </summary>
        /// <param name="ex"></param>
        private void OnRaiseErrorMessage(Exception ex)
        {
            ExceptionHandlingResult exceptionHandlingResult = ExceptionManager.Instance.HandleException(ex, "Policy1");

            ClientExceptionHandlerProvider.ShowMessageErrorWindow(exceptionHandlingResult.Message, ex, eNegMessanger.RaiseErrorMessage.ApplicationName);
        }

        /// <summary>
        /// Display Confirmation Message and resent back the result choosen 
        /// </summary>
        /// <param name="dialogMessage">dialogMessage</param>
        private void OnConfirmMessage(DialogMessage dialogMessage)
        {
            if (dialogMessage != null)
            {
                this.lastDialogMessage = dialogMessage;

                RadWindow.Confirm(new DialogParameters()
                {
                    ModalBackground = new SolidColorBrush(Color.FromArgb(100, 190, 190, 190)),// new LinearGradientBrush(x, 90),
                    Content = dialogMessage.Content,
                    Header = dialogMessage.Caption,

                    Closed = this.CloseConfirmationWindows,
                });

            }
        }

        #endregion

        #region → Public         .

        /// <summary>
        /// ICleanup interface implementation
        /// </summary>
        public void Cleanup()
        {
            // call Cleanup on its ViewModel
            ((ICleanup)this.DataContext).Cleanup();

            // Cleanup itself
            Messenger.Default.Unregister(this);
        }

        #endregion

        #endregion
    }

}
