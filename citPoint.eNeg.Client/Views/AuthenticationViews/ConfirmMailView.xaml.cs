
#region → Usings   .
using System.ComponentModel.Composition;
using System.Windows.Controls;
using citPOINT.eNeg.Common;
using citPOINT.eNeg.ViewModel;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using System;
#endregion

#region → History  .
/* Date         User        Change
 * 
 * 01.09.10    m.Wahab     • creation
 * 
 */
#endregion

#region → ToDos    .

/*
 * Date         set by User     Description
 * 
*/

#endregion

namespace citPOINT.eNeg.Client
{
    /// <summary>
    /// Confirm Mail View
    /// </summary>
    public partial class ConfirmMailView : UserControl
    {
        #region → Properties     .

        #region " Using MEF to import ConfirmMailViewModel "
        /// <summary>
        /// Sets the view model.
        /// </summary>
        /// <value>The view model.</value>
        [Import(ViewModelTypes.ConfirmMailViewModel)]
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
        /// Default Constructor
        /// </summary>
        /// <param name="operationString">Hash string</param>
        /// <param name="operationStringType">Type of the operation string.</param>
        public ConfirmMailView(string operationString, byte operationStringType)
        {
            InitializeComponent();

            #region → Use MEF To load the View Model
            if (!ViewModelBase.IsInDesignModeStatic)
            {
                // Use MEF To load the View Model
                CompositionInitializer.SatisfyImports(this);
            }
            #endregion

            #region → Pass OperationString to the view Model
            //should be the first step to can use it when setting value of the opersation string
            ((ConfirmMailViewModel)this.DataContext).OperationStringType = operationStringType;
            ((ConfirmMailViewModel)this.DataContext).OperationString = operationString;            
            #endregion

            #region → Registeration for needed messages in eNegMessenger

            eNegMessanger.SendCustomMessage.Register(this, OnUpdateMessage);
            #endregion
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
                    uxBsyProgress.IsBusy = false;
                    uxSPSucessMessage.MessageType = Message.MessageType;
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
