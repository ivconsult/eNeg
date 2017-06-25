#region → Usings   .
using citPOINT.eNeg.Common;
using citPOINT.eNeg.ViewModel;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using System.ComponentModel.Composition;
using System.Windows.Controls;
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
    /// Full Register View
    /// </summary>
    public partial class FullRegisterView : UserControl, ICleanup
    {
        #region → Properties     .

        #region " Using MEF to import LoginFormViewModel "
        /// <summary>
        /// Sets the view model.
        /// </summary>
        /// <value>The view model.</value>
        [Import(ViewModelTypes.UserRegisterViewModel)]
        public UserRegisterViewModel ViewModel
        {
            get
            {
                return (DataContext as UserRegisterViewModel);
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
                this.uxcntUserOrganizationManage.DataContext = value;
            }
        }

        #endregion

        #endregion

        #region → Constructors   .

        /// <summary>
        /// Initializes a new instance of the <see cref="FullRegisterView"/> class.
        /// </summary>
        public FullRegisterView()
        {
            InitializeComponent();


            #region " Use MEF To load the View Model "
            if (!ViewModelBase.IsInDesignModeStatic)
            {
                CompositionInitializer.SatisfyImports(this);
            }
            #endregion

            #region initialize the UserControl Width & Height

            ViewsAdjustSize viewsAdjustSize = new ViewsAdjustSize(this);

            #endregion initialize the UserControl Width & Height

            #region Initialize Registeration type
            if (!ViewModelBase.IsInDesignModeStatic)
            {
                ((UserRegisterViewModel)this.DataContext).IsQuickRegister = false;
            }
            #endregion

            #region Registeration for needed messages in eNegMessenger

            eNegMessanger.SendCustomMessage.Register(this, OnUpdateMessage);
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
