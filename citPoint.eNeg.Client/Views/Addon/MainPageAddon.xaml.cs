#region → Usings   .
using citPOINT.eNeg.Common;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.ComponentModel.Composition;
using System.Windows;
using System.Windows.Controls;
using citPOINT.eNeg.Infrastructure.ClientSide.ExceptionHandling;
using citPOINT.eNeg.Infrastructure.ExceptionHandling;
using Telerik.Windows.Controls;
using System.Windows.Media;

#endregion

#region → History  .

/* Date         User            Change
 * 
 * 09.08.10     Yousra Reda     creation
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
    /// Main Page Addon
    /// </summary>
    public partial class MainPageAddon : UserControl, ICleanup
    {
        #region → Fields         .

        private DialogMessage lastDialogMessage;

        #endregion

        #region → Properties     .

        #region Using MEF to import LoginFormViewModel

        /// <summary>
        /// Sets the Data context of this view
        /// </summary>
        [Import(ViewModelTypes.LoginFormViewModel)]
        public object ViewModel
        {
            set
            {
                DataContext = value;
            }
        }
        #endregion

        #endregion

        #region → Constructors   .

        /// <summary>
        /// Initializes a new instance of the <see cref="MainPageAddon"/> class.
        /// </summary>
        public MainPageAddon()
        {
            InitializeComponent();

            #region → Use MEF To load the View Model
            if (!GalaSoft.MvvmLight.ViewModelBase.IsInDesignModeStatic)
            {
                CompositionInitializer.SatisfyImports(this);
            }
            #endregion

            #region → Registeration for needed messages in eNegMessenger
            // register for ChangeScreenMessage
            eNegMessanger.ChangeScreenMessage.Register(this, OnChangeScreenMessage);
            // register for RaiseErrorMessage
            eNegMessanger.RaiseErrorMessage.Register(this, OnRaiseErrorMessage);
            // register for PleaseConfirmMessage
            eNegMessanger.ConfirmMessage.Register(this, OnConfirmMessage);

            #endregion

            #region → initialize the UserControl Width & Height

            //if (!AppConfigurations.IsRunningOutOfBrowser)
            //{
            //    ViewsAdjustSize ViewsAdjustSize = new ViewsAdjustSize(this);
            //    ViewsAdjustSize.BannerHeight = 0;
            //    ViewsAdjustSize.Margin = 0;
            //    ViewsAdjustSize.ReSize();
            //}

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

        #region → Events Handlers.

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
        /// <param name="changeScreen">Screen name</param>
        private void OnChangeScreenMessage(string changeScreen)
        {
            if (changeScreen.IndexOf("http") != -1)
            {
                HyperlinkButton h = new HyperlinkButton();
                h.NavigateUri = new Uri(changeScreen, UriKind.Absolute);
                eNegNavigation.NavigateToUrl(changeScreen, true);
                return;
            }

            #region → call Cleanup() on the current screen before switching

            if (changeScreen != ViewTypes.MainPlatformView)
            {
                ICleanup currentScreen = this.uxviewStaticPart.uxMainContent.Content as ICleanup;
                if (currentScreen != null)
                    currentScreen.Cleanup();
            }
            #endregion

            #region → Collapse controls that will not appear during logout
            uxviewStaticPart.uxspAfterloginLinks.Visibility = Visibility.Collapsed;

            #endregion

            #region → Switch on New Screen Name to Change View

            uxviewStaticPart.uxMainContent.Margin = new Thickness();

            switch (changeScreen)
            {
                case ViewTypes.LoadUser:
                    {
                        uxviewStaticPart.uxMainContent.Margin = new Thickness(50, 300, 50, 300);
                        uxviewStaticPart.uxMainContent.VerticalAlignment = VerticalAlignment.Center;
                        uxviewStaticPart.uxspAfterloginLinks.Visibility = Visibility.Collapsed;
                        this.uxviewStaticPart.uxMainContent.Content = new LoadUserView();
                        break;
                    }
                case ViewTypes.MainNegotiationView:
                    {
                        uxviewStaticPart.uxMainContent.VerticalAlignment = VerticalAlignment.Stretch;
                        uxviewStaticPart.uxMainContent.VerticalContentAlignment = VerticalAlignment.Stretch;


                        uxviewStaticPart.uxMainContent.HorizontalAlignment = HorizontalAlignment.Stretch;
                        uxviewStaticPart.uxMainContent.HorizontalContentAlignment = HorizontalAlignment.Stretch;

                        uxviewStaticPart.uxspAfterloginLinks.Visibility = Visibility.Visible;

                        this.uxviewStaticPart.uxMainContent.Content = new NegotiationsView();

                      //  (this.uxviewStaticPart.uxMainContent.Content as NegotiationsView).Height = Application.Current.Host.Content.ActualHeight - 125;

                        break;
                    }
                case ViewTypes.LoginView:
                    uxviewStaticPart.uxMainContent.VerticalAlignment = VerticalAlignment.Center;
                    this.uxviewStaticPart.uxMainContent.Content = new LoginView();

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
            ClientExceptionHandlerProvider.ShowMessageErrorWindow(exceptionHandlingResult.Message, ex);
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
            if ((ICleanup)this.DataContext != null)
                ((ICleanup)this.DataContext).Cleanup();

            // Cleanup itself
            Messenger.Default.Unregister(this);

            this.CloseConfirmationWindows -= new EventHandler<WindowClosedEventArgs>(MainPageAddon_CloseConfirmationWindows);

        }

        #endregion

        #endregion
    }
}
