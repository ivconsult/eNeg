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
    /// Conversation Details View
    /// </summary>
    public partial class ConversationDetails : UserControl, ICleanup
    {

        #region → Fields         .
        #endregion

        #region → Properties     .

        /// <summary>
        /// Gets the view model.
        /// </summary>
        /// <value>The view model.</value>
        private MessageViewModel ViewModel
        {
            get
            {
                return (this.DataContext as MessageViewModel);
            }
        }

        #endregion

        #region → Constructors   .

        /// <summary>
        /// Initializes a new instance of the <see cref="ConversationDetails"/> class.
        /// </summary>
        /// <param name="showFunctionColumn">if set to <c>true</c> [show function column].</param>
        public ConversationDetails(bool showFunctionColumn = true)
        {
            InitializeComponent();

            RaiseCanExecuteCommands();

            #region → Registeration for needed messages in eNegMessenger

            #region → Expand Message By ID if user coming to this view through double click on message in Add-on

            eNegMessanger.LoadCompleted.Register(this, OnLoadOperationCompleted);

            #endregion

            eNegMessanger.DoOperationMessage.Register(this, OnDoOperationMessage);

            eNegMessanger.SendCustomMessage.Register(this, OnUpdateMessage);


            if (AppConfigurations.IsAddon || !showFunctionColumn) //No channnels or fuction box in case of Addon
            {
                this.uxMessagesGridView.Columns["Functions"].IsVisible = false;
                this.uxMessagesGridView.Columns["Channel"].Width = new GridViewLength(100, GridViewLengthUnitType.Star);
            }

            #endregion
        }

        #endregion

        #region → Event Handlers .

        #region Controls Event Handlers

        /// <summary>
        /// Handles the Click event of the uxcmdDeleteMessage control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void uxcmdDeleteMessage_Click(object sender, RoutedEventArgs e)
        {
            this.ViewModel.DeleteMessageCommand.Execute((sender as RadButton).Tag);
        }

        /// <summary>
        /// Handles the Checked event of the CheckBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            if (e.OriginalSource is CheckBox &&
                ((e.OriginalSource as CheckBox).DataContext != null) &&
                ((e.OriginalSource as CheckBox).DataContext is Message))
            {
                if ((e.OriginalSource as CheckBox).IsChecked.Value)
                {
                    ((e.OriginalSource as CheckBox).DataContext as Message).IsChecked = true;
                }
                else
                {
                    ((e.OriginalSource as CheckBox).DataContext as Message).IsChecked = false;
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
            if (uxMessagesGridView.ItemsSource != null)
            {
                foreach (var message in (uxMessagesGridView.ItemsSource as ObservableCollection<Message>))
                {
                    if ((sender as CheckBox).IsChecked.Value)
                    {
                        message.IsChecked = true;
                    }
                    else
                    {
                        message.IsChecked = false;
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
            if (Type == eNegMessanger.OperationType.HideDeleteColumn)
            {
                this.uxMessagesGridView.Columns["Functions"].IsVisible = false;
                this.uxMessagesGridView.Columns["Channel"].Width = new GridViewLength(100, GridViewLengthUnitType.Star);
            }
            else if (Type == eNegMessanger.OperationType.ShowDeleteColumn)
            {
                this.uxMessagesGridView.Columns["Functions"].IsVisible = true;
                this.uxMessagesGridView.Columns["Channel"].Width = new GridViewLength(100, GridViewLengthUnitType.Auto);
            }
            else if (Type == eNegMessanger.OperationType.ResetSelectAll)
            {
                (this.uxMessagesGridView.Columns["Select"].Header as CheckBox).IsChecked = false;
            }
        }

        /// <summary>
        /// Called when [load operation completed].
        /// </summary>
        /// <param name="OperationName">Name of the operation.</param>
        private void OnLoadOperationCompleted(string OperationName)
        {
            if (OperationName == eNegMessanger.OperationType.SeeCertainMessage.ToString())
            {
                if (AppConfigurations.MessageIDParameter.HasValue)
                {
                    Dispatcher.BeginInvoke(() =>
                    {
                        //uxeNegAccordionMessages.ExpandAccordionItem(AppConfigurations.MessageIDParameter.Value);
                        AppConfigurations.MessageIDParameter = null;
                    });
                }
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
