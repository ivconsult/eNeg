#region → Usings   .
using citPOINT.eNeg.Common;
using citPOINT.eNeg.ViewModel;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using Telerik.Windows.Controls;
using ValidationResult = System.ComponentModel.DataAnnotations.ValidationResult;
using System.Collections.Generic;
using System.Windows.Media;
using citPOINT.eNeg.Data.Web;

#endregion

#region → History  .
/*
    Date            User                Change
 * 02.09.10         Dergham             Creation
 * 
 */

#endregion

#region → ToDos    .
#endregion

namespace citPOINT.eNeg.Client
{
    /// <summary>
    /// Message Header
    /// </summary>
    public partial class AddMessageView : UserControl, ICleanup
    {

        #region → Properties     .

        /// <summary>
        /// Gets the view model.
        /// </summary>
        /// <value>The view model.</value>
        public NegotiationViewModel ViewModel
        {
            get
            {
                return (DataContext as NegotiationViewModel);
            }
        }

        #endregion Properties

        #region → Constructors   .

        /// <summary>
        /// Initializes a new instance of the <see cref="MessageHeader"/> class.
        /// </summary>
        public AddMessageView(NegotiationViewModel viewModel)
        {
            InitializeComponent();

            this.DataContext = viewModel;

            eNegMessanger.FlippMessage.Register(this, OnFlippingByPageName);
            eNegMessanger.NewPopUp.Register(this, OnAddNewPopUp);

            eNegMessanger.FlippMessage.Register(this, OnFlipping);
            eNegMessanger.SendCustomMessage.Register(this, OnUpdateMessage);
        }

        #endregion Constructors

        #region → Event Handlers .

        /// <summary>
        /// For Make Register button as Accept Key
        /// </summary>
        /// <param name="sender">Value of Sender</param>
        /// <param name="e">Value of KeyEventArgs</param>
        private void UserControl_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter &&
                !uxTxtMessageContent.IsTabStop)
            {
                uxHlSave_Click(null, null);
            }
        }

        /// <summary>
        /// Handles the ParseDateTimeValue event of the uxDateTimeMessageDate control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="args">The <see cref="Telerik.Windows.Controls.ParseDateTimeEventArgs"/> instance containing the event data.</param>
        private void uxDateTimeMessageDate_ParseDateTimeValue(object sender, ParseDateTimeEventArgs args)
        {
            //if (args.IsParsingSuccessful)
            //{
            //    uxDateTimeMessageDate.IsDropDownOpen = false;
            //}
            //else
            //{
            //    uxDateTimeMessageDate.DateTimeText = uxDateTimeMessageDate.SelectedValue.ToString();
            //}
        }

        /// <summary>
        /// Handles the LostFocus event of the uxDateTimeMessageDate control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void uxDateTimeMessageDate_LostFocus(object sender, RoutedEventArgs e)
        {
            //RadDateTimePicker picker = (sender as RadDateTimePicker);

            //DateTime result;
            //if (DateTime.TryParse(picker.DateTimeText, out result))
            //{
            //    uxDateTimeMessageDate.DateTimeText = uxDateTimeMessageDate.SelectedValue.ToString();
            //}
        }

        /// <summary>
        /// Handles the Click event of the uxHlSave control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void uxHlSave_Click(object sender, RoutedEventArgs e)
        {
            #region → If Message Date is valid one, Set other contributor according to message flow and then Save Changes .

            DateTime EnteredDate;
            ViewModel.CurrentMessage.TryValidateObject();
            ViewModel.CurrentMessage.TryValidateProperty("MessageContent");
            ViewModel.CurrentMessage.TryValidateProperty("MessageSender");
            ViewModel.CurrentMessage.TryValidateProperty("MessageReceiver");

            if (string.IsNullOrEmpty(ViewModel.CurrentMessage.MessageSubject) ||
                string.IsNullOrWhiteSpace(ViewModel.CurrentMessage.MessageSubject))
            {
                ViewModel.CurrentMessage.MessageSubject =
                    string.Concat(ViewModel.CurrentMessage.MessageDate.ToString(), " ",
                    (uxCboChannels.SelectedItem as Channel).ChannelName);
            }

            if (DateTime.TryParse(uxDateTimeMessageDate.DateTimeText, out EnteredDate))
            {
                if (ViewModel.CurrentMessage.Received)
                {
                    if (Colors.Red.Equals(uxTxtSender.BorderBrush.GetValue(SolidColorBrush.ColorProperty)))
                    {
                        return;
                    }
                    ViewModel.CurrentMessage.MessageReceiver = AppConfigurations.CurrentLoginUser.EmailAddress;// " <" + AppConfigurations.CurrentLoginUser.EmailAddress + ">";
                }
                else
                {
                    if (Colors.Red.Equals(uxTxtReciever.BorderBrush.GetValue(SolidColorBrush.ColorProperty)))
                    {
                        return;
                    }

                    ViewModel.CurrentMessage.MessageSender = AppConfigurations.CurrentLoginUser.EmailAddress;// " <" + AppConfigurations.CurrentLoginUser.EmailAddress + ">";
                }

                (DataContext as NegotiationViewModel).SubmitMessageChangesCommand.Execute(null);
            }
            #endregion

            #region → Show tooltip as validation for Message Date when it is invalid                                      .
            else
            {
                if (!uxDateTimeMessageDate.IsTooltipOpen)
                {
                    uxDateTimeMessageDate.IsTooltipOpen = true;
                    uxDateTimeMessageDate.TooltipContent = "Enter a valid date and time!";
                }
            }
            #endregion
        }

        #endregion Event Handlers

        #region → Methods        .

        #region → Private        .

        /// <summary>
        /// Called when [flipping].
        /// </summary>
        /// <param name="pageName">Name of the page.</param>
        private void OnFlipping(string pageName)
        {
            if (pageName == ViewTypes.MessagesInAddon)
            {
                Dispatcher.BeginInvoke(() =>
                {
                    uxTxtMessageContent.Focus();
                    uxTxtMessageContent.SelectAll();
                });
            }
        }

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
                    uxtxtSuccess.MessageText = Message.Message;
                    uxtxtSuccess.Completed = Message.ShowMessageCompleted;
                    uxtxtSuccess.Show();
                });
            }
        }

        /// <summary>
        /// Called when [add new pop up].
        /// </summary>
        /// <param name="PopUpName">Name of the pop up.</param>
        private void OnAddNewPopUp(string PopUpName)
        {
            if (eNegMessanger.NewPopUp.PopUpType == eNegMessanger.PopUpType.AddMoreReceivers.ToString())
            {
                #region Show PopUp window to choose identify set of receivers and add it
                PopUpWindow AddMoreReceivers = new PopUpWindow(PopUpName);
                AddMoreReceivers.DataContext = this.DataContext;
                AddMoreReceivers.Content = new MultiNegotiatorsPopUp();
                AddMoreReceivers.CenterWindow();
                AddMoreReceivers.ShowDialog();
                #endregion
            }
        }

        /// <summary>
        /// Called when [flipping by page name].
        /// </summary>
        /// <param name="PageName">Name of the page.</param>
        private void OnFlippingByPageName(string PageName)
        {
            if (PageName.Contains("http"))
            {
                DataMatchingPopUp Custombrowser = new DataMatchingPopUp(PageName);
                Custombrowser.Show();
            }
        }

        #endregion

        #region → Public         .
        /// <summary>
        /// for Clean Up
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
