
#region → Usings   .
using citPOINT.eNeg.Common;
using citPOINT.eNeg.ViewModel;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using System.ComponentModel.Composition;
using System.Windows.Controls;
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
    /// Reset View
    /// </summary>
    public partial class ResetView : UserControl, ICleanup
    {
        #region → Properties     .

        #region " Using MEF to import UserResetViewModel "

        /// <summary>
        /// Sets the view model.
        /// </summary>
        /// <value>The view model.</value>
        [Import(ViewModelTypes.UserResetViewModel)]
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
        /// Initializes a new instance of the <see cref="ResetView"/> class.
        /// </summary>
        /// <param name="OperationString">The operation string.</param>
        public ResetView(string OperationString)
        {
            InitializeComponent();

            #region → Use MEF To load the View Model                        .

            if (!ViewModelBase.IsInDesignModeStatic)
            {
                // Use MEF To load the View Model
                CompositionInitializer.SatisfyImports(this);
            }

            #endregion

            #region → Pass OperationString to the view Model                .

            ((UserResetViewModel)this.DataContext).OperationString = OperationString;

            #endregion

            #region → Registeration for needed messages in eNegMessenger    .

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
                uxcmdReset.Focus();
                Dispatcher.BeginInvoke(() => { uxcmdReset.Command.Execute(uxcmdReset.CommandParameter); });
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
                Dispatcher.BeginInvoke(() =>
                {
                    uxSPSucessMessage.MessageText = Message.Message;
                    uxSPSucessMessage.Completed = Message.ShowMessageCompleted;
                    uxSPSucessMessage.Show();

                    Thread.Sleep(new System.TimeSpan(5000));
                    eNegMessanger.FlippMessage.Send(ViewTypes.ClosePopupView);
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
