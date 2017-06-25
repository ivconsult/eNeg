#region → Usings   .
using citPOINT.eNeg.Common;
using citPOINT.eNeg.Data.Web;
using citPOINT.eNeg.ViewModel;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Telerik.Windows.Controls;
using ResizeMode = Telerik.Windows.Controls.ResizeMode;
using System.ComponentModel.Composition;
using System.Collections.ObjectModel;
#endregion

#region → History  .

/* Date         User            Change
 * 
 * 26.10.10     Yousra Reda     creation
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
    /// Negotiation Details View
    /// </summary>
    public partial class NegotiationDetails : UserControl, ICleanup
    {

        #region → Fields         .
        #endregion

        #region → Properties     .

        /// <summary>
        /// Gets or sets the Data context of view
        /// </summary>
        public ConversationViewModel ViewModel
        {
            get
            {
                return (DataContext as ConversationViewModel);
            }
            set
            {
                DataContext = value;
            }
        }


        #endregion

        #region → Constructors   .

        /// <summary>
        /// Initializes a new instance of the <see cref="NegotiationDetails"/> class.
        /// </summary>
        public NegotiationDetails()
        {
            InitializeComponent();

            RaiseCanExecuteCommands();

            #region → Registeration for needed messages in eNegMessenger

            eNegMessanger.SendCustomMessage.Register(this, OnUpdateMessage);
            eNegMessanger.DoOperationMessage.Register(this, OnDoOperationMessage);

            #endregion
        }

        #endregion

        #region → Event Handlers .

        #region Controls Event Handlers

        /// <summary>
        /// Handles the Checked event of the CheckBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            if (e.OriginalSource is CheckBox &&
                ((e.OriginalSource as CheckBox).DataContext != null) &&
                ((e.OriginalSource as CheckBox).DataContext is Conversation))
            {
                if ((e.OriginalSource as CheckBox).IsChecked.Value)
                {
                    ((e.OriginalSource as CheckBox).DataContext as Conversation).IsConvSelected = true;
                }
                else
                {
                    ((e.OriginalSource as CheckBox).DataContext as Conversation).IsConvSelected = false;
                }
            }

            ViewModel.RaiseCanExecuteChanged();
        }

        /// <summary>
        /// Handles the Checked event of the CheckAllBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void CheckAllBox_Checked(object sender, RoutedEventArgs e)
        {
            if (uxConversationsGridView.ItemsSource != null)
            {
                foreach (var conversation in (uxConversationsGridView.ItemsSource as ObservableCollection<Conversation>))
                {
                    if ((sender as CheckBox).IsChecked.Value)
                    {
                        conversation.IsConvSelected = true;
                    }
                    else
                    {
                        conversation.IsConvSelected = false;
                    }
                }
            }

            ViewModel.RaiseCanExecuteChanged();
        }

        #endregion

        #endregion

        #region → Methods        .

        #region → Private        .

        /// <summary>
        /// Called when [do operation message].
        /// </summary>
        /// <param name="Type">The type.</param>
        private void OnDoOperationMessage(eNegMessanger.OperationType Type)
        {
           if (Type == eNegMessanger.OperationType.ResetSelectAll)
            {
                (this.uxConversationsGridView.Columns["Select"].Header as CheckBox).IsChecked = false;
            }
        }

        /// <summary>
        /// Called when [update message].
        /// </summary>
        /// <param name="Message">The message.</param>
        private void OnUpdateMessage(eNegMessage Message)
        {
            if (!string.IsNullOrEmpty(Message.Message) &&
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
        /// Raises the can execute commands.
        /// </summary>
        public void RaiseCanExecuteCommands()
        {
            Dispatcher.BeginInvoke(() =>
            {
                if (ViewModel != null)
                {
                    ViewModel.RaiseCanExecuteChanged();

                }
            });
        }

        /// <summary>
        /// Cleanups this instance.
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
