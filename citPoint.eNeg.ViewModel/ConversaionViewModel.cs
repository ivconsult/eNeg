
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
using GalaSoft.MvvmLight;

#endregion

#region → History  .

/* 
 * Date         User            Change
 * *********************************************
 * 04.04.12   Yousra Reda         • creation
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
    /// conversation view model to handle conversations
    /// </summary>
    public class ConversationViewModel : ViewModelBase
    {
        
        #region → Fields         .

        public INegotiationModel mNegotiationModel;
        private bool mIsMessagesSourceEmpty;
        private bool mIsBusy;
        private bool mIsForPublishedNegotiation;

        private Negotiation mcurrentNegotiation;

        private RelayCommand mExportConverationsAsPDFCommand = null;
        
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
        /// Gets or sets the current negotiation.
        /// </summary>
        /// <value>The current negotiation.</value>
        public Negotiation CurrentNegotiation
        {
            get { return mcurrentNegotiation; }
            set
            {
                //if (mcurrentNegotiation != value)
                {
                    mcurrentNegotiation = value;

                    this.RaisePropertyChanged("CurrentNegotiation");

                    if (mcurrentNegotiation != null)
                    {
                        RefreshConversationsSource();
                    }
                }

            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is conversations source empty.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is conversations source empty; otherwise, <c>false</c>.
        /// </value>
        public bool IsConversationsSourceEmpty
        {
            get
            {
                return mIsMessagesSourceEmpty;
            }
            set
            {
                mIsMessagesSourceEmpty = value;
                RaisePropertyChanged("IsConversationsSourceEmpty");
            }
        }

        /// <summary>
        /// Gets or sets the conversations source.
        /// </summary>
        /// <value>The conversations source.</value>
        public ObservableCollection<Conversation> ConversationsSource { get; private set; }

        #endregion Properties

        #region → Constractor    .

        /// <summary>
        /// Initializes a new instance of the <see cref="ConversationViewModel"/> class.
        /// </summary>
        public ConversationViewModel()
        {
            this.ConversationsSource = new ObservableCollection<Conversation>();
        }

        #endregion

        #region → Event Handlers .
       
        #endregion

        #region → Commands       .

        /// <summary>
        /// Gets the export conversations as PDF command.
        /// </summary>
        /// <value>The export conversations as PDF command.</value>
        public RelayCommand ExportConversationsAsPDFCommand
        {
            get
            {
                if (mExportConverationsAsPDFCommand == null)
                {
                    mExportConverationsAsPDFCommand = new RelayCommand(() =>
                    {
                        try
                        {
                            eNegMessanger.DoOperationMessage.Send(eNegMessanger.OperationType.ExportPDFConversations);
                            eNegMessanger.NewPopUp.Send("Save", eNegMessanger.PopUpType.SavePDF);
                        }
                        catch (Exception ex)
                        {
                            // notify user if there is any error
                            eNegMessanger.RaiseErrorMessage.Send(ex);
                        }
                    }
                    , () => !this.IsBusy && this.CurrentNegotiation != null &&
                        this.CurrentNegotiation.Conversations.Where(s => s.IsConvSelected).Count<Conversation>() > 0);
                }
                return mExportConverationsAsPDFCommand;
            }
        }

        #endregion

        #region → Methods        .

        #region → Public         .

        /// <summary>
        /// Refreshes the conversations source.
        /// </summary>
        public void RefreshConversationsSource()
        {
            if (CurrentNegotiation != null)
            {
                ConversationsSource = new ObservableCollection<Conversation>(
                                                           CurrentNegotiation
                                                             .Conversations
                                                             .OrderBy(s => s.ConversationName));

                IsConversationsSourceEmpty = (ConversationsSource.Count == 0);

                this.RaisePropertyChanged("ConversationsSource");
            }
        }

        /// <summary>
        /// Raises the can execute changed.
        /// </summary>
        public void RaiseCanExecuteChanged()
        {
            this.ExportConversationsAsPDFCommand.RaiseCanExecuteChanged();
        }

        #endregion  Public

        #endregion Methods
    }
}
