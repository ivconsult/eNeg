#region → Usings   .
using citPOINT.eNeg.Common;
using citPOINT.eNeg.ViewModel;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using System.ComponentModel.Composition;
using System.Windows.Controls;
using Telerik.Windows.Controls;
using System;
#endregion

#region → History  .
/* Date         User        Change
 * 
 * 05.08.10    m.Wahab     • creation
 * 
 */
#endregion

#region → ToDos    .

/*
 * Date         set by User     Description
 * 
 * 
*/

#endregion

namespace citPOINT.eNeg.Client
{
    /// <summary>
    /// Quick Register
    /// </summary>
    public partial class QuickRegister : UserControl, ICleanup
    {

        #region → Properties     .

        #region " Using MEF to import UserRegisterViewModel "
        /// <summary>
        /// Gets or sets the view model.
        /// </summary>
        /// <value>The view model.</value>
        [Import(ViewModelTypes.UserRegisterViewModel)]
        public UserRegisterViewModel ViewModel
        {
            get
            {
                return (this.DataContext as UserRegisterViewModel);
            }
            set
            {
                DataContext = value;
            }
        }


        /// <summary>
        /// Gets or sets the view model.
        /// </summary>
        /// <value>The view model.</value>
        [Import(ViewModelTypes.ManageUserOrganizationViewModel)]
        public ManageUserOrganizationViewModel ManageUserOrganizationViewModel
        {
            get
            {
                return this.ViewModel.ManageUserOrgViewModel;
            }
            set
            {
                this.ViewModel.ManageUserOrgViewModel = value;
            }
        }

        #endregion




        #endregion

        #region → Constructors   .
        /// <summary>
        /// Default Constructor
        /// </summary>
        public QuickRegister()
        {
            InitializeComponent();

            #region → Use MEF To load the View Model
            if (!GalaSoft.MvvmLight.ViewModelBase.IsInDesignModeStatic)
            {
                // Use MEF To load the View Model
                CompositionInitializer.SatisfyImports(this);
            }
            #endregion

            #region → Initialize Regiteration type
            if (!GalaSoft.MvvmLight.ViewModelBase.IsInDesignModeStatic)
            {
                this.ViewModel.IsQuickRegister = true;

            }
            #endregion

            #region → Registeration for needed messages in eNegMessenger
            eNegMessanger.SendCustomMessage.Register(this, OnUpdateMessage);
            eNegMessanger.BuildControl.Register(this, OnFocusControl);
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
                uxcmdRegister.Focus();
                Dispatcher.BeginInvoke(() => { uxcmdRegister.Command.Execute(uxcmdRegister.CommandParameter); });
            }
        }

        #endregion

        #region → Methods        .

        #region → Private        .

        /// <summary>
        /// Called when [focus control].
        /// </summary>
        /// <param name="controlName">Name of the control.</param>
        private void OnFocusControl(string controlName)
        {
            var control = this.FindName(controlName);
            if (control is RadButton)
            {
                Dispatcher.BeginInvoke(() =>
                    {
                        (control as RadButton).Focus();
                    });
            }
        }

        /// <summary>
        /// Called when [update message].
        /// </summary>
        /// <param name="Message">The message.</param>
        private void OnUpdateMessage(eNegMessage Message)
        {
            if (Message.ViewName == ViewTypes.QuickRegisterView && 
                Message.ReceiverApplicationID == Guid.Empty)
            {
                Dispatcher.BeginInvoke(() =>
                {
                    uxSPSucessMessage.MessageText = Message.Message;
                    uxSPSucessMessage.Completed = Message.ShowMessageCompleted;
                    uxSPSucessMessage.Show();
                });
                eNegMessanger.DoOperationMessage.Send(eNegMessanger.OperationType.ClearValidationMesssages);
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
