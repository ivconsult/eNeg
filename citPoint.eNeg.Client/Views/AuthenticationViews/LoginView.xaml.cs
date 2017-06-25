
#region → Usings   .
using System.Windows.Controls;
using GalaSoft.MvvmLight;
using System.ComponentModel.Composition;
using citPOINT.eNeg.Common;
using GalaSoft.MvvmLight.Messaging;
using System.Windows.Browser;
using System;
using citPOINT.eNeg.ViewModel;
#endregion

#region → History  .

/* Date         User              Change
 * 
 * 05.08.10     Yousra Reda       Creation
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
    /// Login View
    /// </summary>
    public partial class LoginView : UserControl, ICleanup
    {

        #region → Properties     .

        #region " Using MEF to import LoginFormViewModel "

        /// <summary>
        /// Sets the view model.
        /// </summary>
        /// <value>The view model.</value>
        [Import(ViewModelTypes.LoginFormViewModel)]
        public LogInViewModel ViewModel
        {
            get
            {
                return DataContext as LogInViewModel;
            }
            set
            {
                DataContext = value;
            }
        }
        #endregion

        #endregion

        #region → Constructors   .

        /// <summary>
        /// Initializes a new instance of the <see cref="LoginView"/> class.
        /// </summary>
        public LoginView()
        {
            InitializeComponent();

            #region " Use MEF To load the View Model "
            if (!ViewModelBase.IsInDesignModeStatic)
            {
                CompositionInitializer.SatisfyImports(this);
            }
            #endregion

            #region Registeration for needed messages in eNegMessenger

            eNegMessanger.SendCustomMessage.Register(this, OnUpdateMessage);
            eNegMessanger.FlippMessage.Register(this, OnFlipMessage);
            #endregion
        }

        #endregion

        #region → Event Handlers .

        /// <summary>
        /// For Make Register button as Accept Key
        /// </summary>
        /// <param name="sender">Value of Sender</param>
        /// <param name="e">Value of KeyEventArgs</param>
        private void UserControl_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                uxcmdLogin.Focus();
                Dispatcher.BeginInvoke(() => { uxcmdLogin.Command.Execute(uxcmdLogin.CommandParameter); });
            }
        }

        #endregion

        #region → Methods        .

        #region → Private        .
        
        /// <summary>
        /// Called when [update message].
        /// </summary>
        /// <param name="Message">The message.</param>
        private void OnUpdateMessage(eNegMessage Message)
        {
            if (Message.ViewName == ViewTypes.LoginView && Message.ReceiverApplicationID == Guid.Empty)
            {
                Dispatcher.BeginInvoke(() =>
                {
                    uxSPSucessMessage.MessageText = Message.Message;
                    uxSPSucessMessage.Completed = Message.ShowMessageCompleted;
                    uxSPSucessMessage.Show();
                });    
            }
            
        }

        /// <summary>
        /// Called when [flip message].
        /// </summary>
        /// <param name="PageName">Name of the page.</param>
        private void OnFlipMessage(string PageName)
        {
            switch (PageName)
            {
                case ViewTypes.NavigateQuickRegisterView:
                    //uxcmdQuickRegister.NavigateUri = new Uri(AppConfigurations.HostBaseAddress + "Default.aspx?PageName=QuickRegister", UriKind.Absolute);
                    break;
                case ViewTypes.NavigateForgetLoginView:
                    uxcmdForgetPassword.NavigateUri = new Uri(AppConfigurations.HostBaseAddress + "Default.aspx?PageName=ResetRequest", UriKind.Absolute);
                    break;
                case ViewTypes.NavigateFullRegisterView:
                    //uxcmdFullRegister.NavigateUri = new Uri(AppConfigurations.HostBaseAddress + "Default.aspx?PageName=FullRegister", UriKind.Absolute);
                    break;
            }
        }

        #endregion

        #region → Public         .

        /// <summary>
        /// ICleanup interface implementation
        /// </summary>
        public void Cleanup()
        {
            ViewModel.CurrentUser = new Data.Web.LoginUser();

            // call Cleanup on its ViewModel
            ((ICleanup)this.DataContext).Cleanup();


            // Cleanup itself
            Messenger.Default.Unregister(this);
        }


        #endregion            

        #endregion
    }
}
