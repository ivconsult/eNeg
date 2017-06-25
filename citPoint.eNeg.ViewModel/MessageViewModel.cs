
#region → Usings   .
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using citPOINT.eNeg.Common;
using citPOINT.eNeg.Data.Web;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System.ServiceModel.DomainServices.Client;
using System.Collections.Generic;
using System.Windows.Controls;
using System.IO;

#endregion

#region → History  .

/* 
 * Date         User            Change
 * *********************************************
 * 20-09-2011    mwahab         • creation
 * **********************************************
 */

# endregion

#region → ToDos    .

/*
 * Date         set by User     Description
 * 
 * 
*/

# endregion

namespace citPOINT.eNeg.ViewModel
{
    /// <summary>
    /// Message View Model To handling messages.
    /// </summary>
    public class MessageViewModel : GalaSoft.MvvmLight.ViewModelBase
    {

        #region → Fields         .

        public INegotiationModel mNegotiationModel;
        private bool mCanEditInMessages;
        private bool mIsMessagesSourceEmpty;
        private bool mIsAddonMessagesChecked = true;
        private bool mIsAppMessagesChecked = true;
        private bool mIsBusy;
        private bool mIsForPublishedNegotiation;

        private Conversation mcurrentConversation;

        private RelayCommand mExportMessagesAsPDFCommand = null;
        private RelayCommand mDeleteSelectedMessagesCommand = null;
        private RelayCommand mFilterMessagesCommand = null;
        private RelayCommand<Guid> mDeleteMessageCommand;

        #endregion

        #region → Properties     .

        /// <summary>
        /// Gets or sets a value indicating whether this instance is for published negotiation.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is for published negotiation; otherwise, <c>false</c>.
        /// </value>
        public bool IsForPublishedNegotiation
        {
            get
            {
                return mIsForPublishedNegotiation;
            }
            set
            {
                mIsForPublishedNegotiation = value;
                this.RaisePropertyChanged("CanEditInMessages");
                this.RaisePropertyChanged("IsForPublishedNegotiation");
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is busy.
        /// </summary>
        /// <value><c>true</c> if this instance is busy; otherwise, <c>false</c>.</value>
        public bool IsBusy
        {
            get
            {
                return mIsBusy;
            }
            set
            {
                mIsBusy = value;
                this.RaiseCanExecuteChanged();
                this.RaisePropertyChanged("IsBusy");
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is current negotiation closed.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is current negotiation closed; otherwise, <c>false</c>.
        /// </value>
        public bool CanEditInMessages
        {
            get
            {
                return mCanEditInMessages && !this.IsForPublishedNegotiation;
            }
            set
            {
                mCanEditInMessages = value;
                RaisePropertyChanged("CanEditInMessages");
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is addon messages checked.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is addon messages checked; otherwise, <c>false</c>.
        /// </value>
        public bool IsAddonMessagesChecked
        {
            get
            {
                return mIsAddonMessagesChecked;
            }
            set
            {
                mIsAddonMessagesChecked = value;
                RaisePropertyChanged("IsAddonMessagesChecked");
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is app messages checked.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is app messages checked; otherwise, <c>false</c>.
        /// </value>
        public bool IsAppMessagesChecked
        {
            get
            {
                return mIsAppMessagesChecked;
            }
            set
            {
                mIsAppMessagesChecked = value;
                RaisePropertyChanged("IsAppMessagesChecked");
            }
        }

        /// <summary>
        /// Gets or sets the current conversation.
        /// </summary>
        /// <value>The current conversation.</value>
        public Conversation CurrentConversation
        {
            get { return mcurrentConversation; }
            set
            {
                if (mcurrentConversation != value)
                {
                    mcurrentConversation = value;

                    this.IsAddonMessagesChecked = true;

                    this.IsAppMessagesChecked = true;

                    this.RaisePropertyChanged("CurrentConversation");

                    if (mcurrentConversation != null)
                    {
                        CanEditInMessages = (mcurrentConversation.Negotiation != null && mcurrentConversation.Negotiation.StatusID == eNegConstant.NegotiationStatus.Ongoing);

                        if (CanEditInMessages)
                        {
                            eNegMessanger.DoOperationMessage.Send(eNegMessanger.OperationType.ShowDeleteColumn);
                        }
                        else
                        {
                            eNegMessanger.DoOperationMessage.Send(eNegMessanger.OperationType.HideDeleteColumn);
                        }
                        RefreshMessagesSource();
                    }
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is there any messages.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is there any messages; otherwise, <c>false</c>.
        /// </value>
        public bool IsMessagesSourceEmpty
        {
            get
            {
                return mIsMessagesSourceEmpty;
            }
            set
            {
                mIsMessagesSourceEmpty = value;
                RaisePropertyChanged("IsMessagesSourceEmpty");
            }
        }

        /// <summary>
        /// Gets or sets the messages source.
        /// </summary>
        /// <value>The messages source.</value>
        public ObservableCollection<Message> MessagesSource { get; private set; }

        #endregion Properties

        #region → Constractor    .
        /// <summary>
        /// Initializes a new instance of the <see cref="MessageViewModel"/> class.
        /// </summary>
        public MessageViewModel()
        {

            if (AppConfigurations.IsAddon)
            {
                this.IsForPublishedNegotiation = true;
            }

            this.MessagesSource = new ObservableCollection<Message>();

        }

        #endregion

        #region → Event Handlers .

        #endregion

        #region → Commands       .

        /// <summary>
        /// Gets the export as PDF command.
        /// </summary>
        /// <value>The export as PDF command.</value>
        public RelayCommand ExportMessagesAsPDFCommand
        {
            get
            {
                if (mExportMessagesAsPDFCommand == null)
                {
                    mExportMessagesAsPDFCommand = new RelayCommand(() =>
                    {
                        try
                        {
                            eNegMessanger.DoOperationMessage.Send(eNegMessanger.OperationType.ExportPDFMessages);
                            eNegMessanger.NewPopUp.Send("Save", eNegMessanger.PopUpType.SavePDF);
                        }
                        catch (Exception ex)
                        {
                            // notify user if there is any error
                            eNegMessanger.RaiseErrorMessage.Send(ex);
                        }
                    }
                    , () => !this.IsBusy && this.CurrentConversation != null &&
                        this.CurrentConversation.Messages.Where(s => s.IsChecked).Count<Message>() > 0);
                }
                return mExportMessagesAsPDFCommand;
            }
        }

        /// <summary>
        /// Gets the filter messages command.
        /// </summary>
        /// <value>The filter messages command.</value>
        public RelayCommand FilterMessagesCommand
        {
            get
            {
                if (mFilterMessagesCommand == null)
                {
                    mFilterMessagesCommand = new RelayCommand(() =>
                    {
                        try
                        {
                            RefreshMessagesSource();

                            this.RaiseCanExecuteChanged();
                        }
                        catch (Exception ex)
                        {
                            // notify user if there is any error
                            eNegMessanger.RaiseErrorMessage.Send(ex);
                        }
                    }
                    , () => !this.IsBusy);
                }
                return mFilterMessagesCommand;
            }
        }

        /// <summary>
        /// Gets the delete selected messages command.
        /// </summary>
        /// <value>The delete selected messages command.</value>
        public RelayCommand DeleteSelectedMessagesCommand
        {
            get
            {
                if (mDeleteSelectedMessagesCommand == null)
                {
                    mDeleteSelectedMessagesCommand = new RelayCommand(() =>
                    {
                        try
                        {
                            #region Confirmation Message

                            Action<MessageBoxResult> callBackResult = null;

                            if (!this.IsBusy)
                            {
                                //Firsly ask user to confirm editing that item
                                DialogMessage DeleteDialogMsg = new DialogMessage(this,
                                    Resources.DeleteCurrentItemMessageBoxText, result => callBackResult(result))
                                {
                                    Button = MessageBoxButton.OKCancel,
                                    Caption = Resources.ConfirmMessageBoxCaption
                                };
                                eNegMessanger.ConfirmMessage.Send(DeleteDialogMsg);


                            }

                            #endregion Confirmation Message

                            callBackResult = (result) =>
                            {
                                if (result == MessageBoxResult.Cancel)
                                    return;

                                foreach (var item in CurrentConversation.Messages.Where(s => s.IsChecked))
                                {
                                    CurrentConversation.Messages.Remove(item);
                                }

                                RefreshMessagesSource();

                                eNegMessanger.SubmitChangesMessage.Send();
                            };
                        }
                        catch (Exception ex)
                        {
                            eNegMessanger.RaiseErrorMessage.Send(ex);
                        }
                    }
                    , () => this.CurrentConversation != null &&
                        this.CurrentConversation.Messages.Where(s => s.IsChecked).Count<Message>() > 0);
                }
                return mDeleteSelectedMessagesCommand;
            }
        }

        /// <summary>
        /// Gets the delete message command.
        /// </summary>
        /// <value>The delete message command.</value>
        public RelayCommand<Guid> DeleteMessageCommand
        {
            get
            {
                if (mDeleteMessageCommand == null)
                {
                    mDeleteMessageCommand = new RelayCommand<Guid>((MessageID) =>
                    {
                        try
                        {
                            if (!this.IsBusy)
                            {

                                #region Confirmation Message
                                
                                Action<MessageBoxResult> callBackResult = null;
                               
                                if (!this.IsBusy)
                                {
                                    //Firsly ask user to confirm editing that item
                                    DialogMessage DeleteDialogMsg = new DialogMessage(this,
                                        Resources.DeleteCurrentItemMessageBoxText, result => callBackResult(result))
                                    {
                                        Button = MessageBoxButton.OKCancel,
                                        Caption = Resources.ConfirmMessageBoxCaption
                                    };

                                    eNegMessanger.ConfirmMessage.Send(DeleteDialogMsg);
                                }

                                #endregion Confirmation Message

                                callBackResult = (result) =>
                                {
                                    if (result == MessageBoxResult.Cancel)
                                        return;

                                    this.CurrentConversation.Messages.Remove
                                        (CurrentConversation.Messages.Where(s => s.MessageID == MessageID).First());

                                    this.RefreshMessagesSource();

                                    eNegMessanger.SubmitChangesMessage.Send();
                                };
                            }
                        }
                        catch (Exception ex)
                        {
                            eNegMessanger.RaiseErrorMessage.Send(ex);
                        }
                    }
                    , (MessageID) => this.CurrentConversation != null &&
                        this.CurrentConversation.Messages.Where(s => s.IsChecked).Count<Message>() > 0);
                }
                return mDeleteMessageCommand;
            }
        }

        #endregion

        #region → Methods        .

        #region → Public         .

        /// <summary>
        /// Refreshes the messages source.
        /// </summary>
        public void RefreshMessagesSource()
        {
            if (CurrentConversation != null)
            {
                if (MessagesSource != null)
                {
                    while (this.MessagesSource.Where(ss => ss.IsSelected == true || ss.IsChecked == true).Count() > 0)
                    {
                        Message msg = this.MessagesSource.Where(ss => ss.IsSelected == true || ss.IsChecked == true).First();
                        msg.IsSelected = false;
                        msg.IsChecked = false;
                    }
                }

                MessagesSource = new ObservableCollection<Message>(
                                                           CurrentConversation
                                                             .Messages
                                                             .Where(s => (s.IsAppsMessage == !IsAddonMessagesChecked || s.IsAppsMessage == IsAppMessagesChecked) &&
                                                                       (IsAddonMessagesChecked || IsAppMessagesChecked))
                                                             .OrderByDescending(s => s.MessageDate));

                if (!this.CurrentConversation.IsLoadedOnDemand)
                {
                    IsMessagesSourceEmpty = (MessagesSource.Count == 0);
                }
                else
                {
                    IsMessagesSourceEmpty = false;
                }
                this.RaisePropertyChanged("MessagesSource");
            }
        }

        /// <summary>
        /// Raises the can execute changed.
        /// </summary>
        public void RaiseCanExecuteChanged()
        {
            this.DeleteSelectedMessagesCommand.RaiseCanExecuteChanged();
            this.ExportMessagesAsPDFCommand.RaiseCanExecuteChanged();
            this.FilterMessagesCommand.RaiseCanExecuteChanged();
        }

        #endregion  Public

        #endregion Methods
    }
}
