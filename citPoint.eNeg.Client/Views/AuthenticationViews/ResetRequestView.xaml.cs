
#region → Usings   .
using System.ComponentModel.Composition;
using System.Windows.Controls;
using citPOINT.eNeg.Common;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using System.Threading;
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
    /// Reset Request View
    /// </summary>
    public partial class ResetRequestView : UserControl, ICleanup
    {
        #region → Properties     .

        #region " Using MEF to import UserRequestResetViewModel "
        /// <summary>
        /// Sets the view model.
        /// </summary>
        /// <value>The view model.</value>
        [Import(ViewModelTypes.UserRequestResetViewModel)]
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
        /// Initializes a new instance of the <see cref="ResetRequestView"/> class.
        /// </summary>
        public ResetRequestView()
        {
            InitializeComponent();

            #region → Use MEF To load the View Model                      .

            if (!ViewModelBase.IsInDesignModeStatic)
            {
                // Use MEF To load the View Model
                CompositionInitializer.SatisfyImports(this);
            }

            #endregion

            #region → Registeration for needed messages in eNegMessenger  .

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
                uxcmdSendMyLogin.Focus();
                Dispatcher.BeginInvoke(() => { uxcmdSendMyLogin.Command.Execute(uxcmdSendMyLogin.CommandParameter); });
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
            if (Message.ReceiverApplicationID == Guid.Empty)
            {
                uxSPSucessMessage.MessageText = Message.Message;
                uxSPSucessMessage.Completed = Message.ShowMessageCompleted;
                uxSPSucessMessage.Show();
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
