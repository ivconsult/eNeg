#region → Usings   .
using citPOINT.eNeg.Data.Web;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Windows.Controls;
using Telerik.Windows.Controls;
using citPOINT.eNeg.Common.eNegApps;
#endregion

#region → History  .

/* Date         User             Change
 * 
 * 25.07.10     Yousra Reda      creation
 */

# endregion

#region → ToDos    .

/*
 * Date         set by User     Description
 * 
 * 
*/

# endregion

namespace citPOINT.eNeg.Common
{
    /// <summary>
    /// Class Used to send and register messaging
    /// </summary>
    public static class eNegMessanger
    {
        #region Enums

        /// <summary>
        /// Enumerator represent the set of PopUp types that exist in that system
        /// </summary>
        public enum PopUpType
        {
            /// <summary>
            /// Add more receivers
            /// </summary>
            AddMoreReceivers,

            /// <summary>
            /// Add New Organization
            /// </summary>
            AddNewOrganization,

            /// <summary>
            /// Select Organization To Publish
            /// </summary>
            SelectOrganizationToPublish,

            /// <summary>
            /// Save PDF
            /// </summary>
            SavePDF,

            /// <summary>
            /// New Negotiation
            /// </summary>
            NewNegotiation,

            /// <summary>
            /// New Conversation
            /// </summary>
            NewConversation

        }

        /// <summary>
        /// Enumerator represent the set of messages that can be sent
        /// </summary>
        enum MessageTypes
        {
            ChangeScreen,
            SubmitChanges,
            CancelChanges,
            RaiseError,
            Confirm,
            StatusUpdate,
            EditNegotiation,
            EditConversation,
            DoOperation,
            LoadUser,
            FocusTreeNode,
            CustomMessage,
            AddNewPopUpMessage,
            LoadComplete,
            EditMessage,
            BuildControl,
            EditUserOrganization,
            Appchanges,
            LoadingQueue,
            /// <summary>
            /// Activate Application.
            /// </summary>
            ActivateApplication
        }

        /// <summary>
        /// Define thetypes of operation that can be accomplished
        /// </summary>
        public enum OperationType
        {
            /// <summary>
            /// For indicating That DataLoad Completed.
            /// </summary>
            DataLoadCompleted,

            /// <summary>
            /// Negotiation Submit
            /// </summary>
            NegotiationSubmit,

            /// <summary>
            /// Negotiation Archive Loaded.
            /// </summary>
            NegotiationArchiveLoaded,

            /// <summary>
            /// Closed Negotiation Archive Loaded.
            /// </summary>
            ClosedNegotiationArchiveLoaded,

            /// <summary>
            /// Conversation Submit
            /// </summary>
            ConversationSubmit,

            /// <summary>
            /// Message Submit
            /// </summary>
            MessageSubmit,

            /// <summary>
            /// Submit All
            /// </summary>
            SubmitAll,

            /// <summary>
            /// See Certain Message in Conversation Details
            /// </summary>
            SeeCertainMessage,

            /// <summary>
            /// Negotiation Application Status.
            /// </summary>
            NegotiationApplicationStatus,

            /// <summary>
            /// call Export messages pdf service
            /// </summary>
            ExportPDFMessages,

            /// <summary>
            /// call Export conversations pdf service
            /// </summary>
            ExportPDFConversations,

            /// <summary>
            /// Hide Delete Column in messages view
            /// </summary>
            HideDeleteColumn,

            /// <summary>
            /// Show Delete column in messages view
            /// </summary>
            ShowDeleteColumn,

            /// <summary>
            /// UnCheck Select All check box
            /// </summary>
            ResetSelectAll,

            /// <summary>
            /// Clear the validations message on current user
            /// </summary>
            ClearValidationMesssages,
            
        }

        #endregion

        #region static Classes
        
        #region → Negotiation                    .
        /// <summary>
        /// Class used to set negotation in edit mode
        /// </summary>
        public static class EditNegotiationMessage
        {
            /// <summary>
            /// Send message with a negotation
            /// </summary>
            /// <param name="CurrentNegotiation">Negotiation</param>
            public static void Send(Negotiation CurrentNegotiation)
            {
                Messenger.Default.Send<Negotiation>(CurrentNegotiation, MessageTypes.EditNegotiation);
            }

            /// <summary>
            /// Register to recieve that type of message
            /// </summary>
            /// <param name="recipient">recipient</param>
            /// <param name="action">method that will be called when message is recieved</param>
            public static void Register(object recipient, Action<Negotiation> action)
            {
                Messenger.Default.Register<Negotiation>(recipient, MessageTypes.EditNegotiation, action);
            }
        }
        #endregion

        #region → Apply Changes For Apps Messager.
        /// <summary>
        /// Class used to set negotation in edit mode
        /// </summary>
        public static class ApplyChangesForAppsMessage
        {
            /// <summary>
            /// Sends the specified current changes.
            /// </summary>
            /// <param name="currentChanges">The current changes.</param>
            public static void Send(ApplicationChange currentChanges)
            {
                Messenger.Default.Send<ApplicationChange>(currentChanges, MessageTypes.Appchanges);
            }

            /// <summary>
            /// Register to recieve that type of message
            /// </summary>
            /// <param name="recipient">recipient</param>
            /// <param name="action">method that will be called when message is recieved</param>
            public static void Register(object recipient, Action<ApplicationChange> action)
            {
                Messenger.Default.Register<ApplicationChange>(recipient, MessageTypes.Appchanges, action);
            }
        }

        #endregion

        #region → Conversation                   .

        /// <summary>
        /// Class used to set conversation in edit mode
        /// </summary>
        public static class EditConversationMessage
        {
            /// <summary>
            /// send message with a recipient
            /// </summary>
            /// <param name="CurrentConversation">Conversation</param>
            public static void Send(Conversation CurrentConversation)
            {
                Messenger.Default.Send<Conversation>(CurrentConversation, MessageTypes.EditConversation);
            }

            /// <summary>
            /// Register message with a recipient
            /// </summary>
            /// <param name="recipient">recipient</param>
            /// <param name="action">method that will be called when message is recieved</param>
            public static void Register(object recipient, Action<Conversation> action)
            {
                Messenger.Default.Register<Conversation>(recipient, MessageTypes.EditConversation, action);
            }
        }
        #endregion

        #region → Message                        .
        /// <summary>
        /// Class used to set Message in View Model
        /// </summary>
        public static class EditCertainMessage
        {
            /// <summary>
            /// Sends the specified current message.
            /// </summary>
            /// <param name="CurrentMessage">The current message.</param>
            public static void Send(Message CurrentMessage)
            {
                Messenger.Default.Send<Message>(CurrentMessage, MessageTypes.EditMessage);
            }

            /// <summary>
            /// Registers the specified recipient.
            /// </summary>
            /// <param name="recipient">The recipient.</param>
            /// <param name="action">The action.</param>
            public static void Register(object recipient, Action<Message> action)
            {
                Messenger.Default.Register<Message>(recipient, MessageTypes.EditMessage, action);
            }
        }
        #endregion

        #region → Negotiation Application Status .
        /// <summary>
        /// Class used to set negotation in edit mode
        /// </summary>
        public static class ChangeCurrentNegotionAppsStatus
        {
            /// <summary>
            /// Send message with a IEnumerable
            /// </summary>
            /// <param name="NegotiationAppStatus">IEnumerable</param>
            public static void Send(IEnumerable<NegotiationApplicationStatu> NegotiationAppStatus)
            {
                Messenger.Default.Send<IEnumerable<NegotiationApplicationStatu>>(NegotiationAppStatus, MessageTypes.EditNegotiation);
            }

            /// <summary>
            /// Register to recieve that type of message
            /// </summary>
            /// <param name="recipient">The recipient that has register for this type of message</param>
            /// <param name="action">Method that will be called when message is recieved</param>
            public static void Register(object recipient, Action<IEnumerable<NegotiationApplicationStatu>> action)
            {
                Messenger.Default.Register<IEnumerable<NegotiationApplicationStatu>>(recipient, MessageTypes.EditNegotiation, action);
            }
        }
        #endregion

        #region → User Organization              .
        /// <summary>
        /// Class used to set User Organization in edit mode
        /// </summary>
        public static class EditUserOrganizationMessage
        {
            /// <summary>
            /// Send message with a User Organization
            /// </summary>
            /// <param name="CurrentUserOrganization">User Organization</param>
            public static void Send(UserOrganization CurrentUserOrganization)
            {
                Messenger.Default.Send<UserOrganization>(CurrentUserOrganization, MessageTypes.EditUserOrganization);
            }

            /// <summary>
            /// Register to recieve that type of message
            /// </summary>
            /// <param name="recipient">recipient</param>
            /// <param name="action">method that will be called when message is recieved</param>
            public static void Register(object recipient, Action<UserOrganization> action)
            {
                Messenger.Default.Register<UserOrganization>(recipient, MessageTypes.EditUserOrganization, action);
            }
        }
        #endregion

        #region → Change Screen Message          .

        /// <summary>
        /// Class to changes the current screen loaded
        /// </summary>
        public static class ChangeScreenMessage
        {
            /// <summary>
            /// Send this type of message to any recipient who want to register that type of messages
            /// </summary>
            public static void Send(string screenName)
            {
                Messenger.Default.Send<string>(screenName, MessageTypes.ChangeScreen);
            }

            /// <summary>
            /// Register to recieve that type of message
            /// </summary>
            /// <param name="recipient">The recipient that has register for this type of message</param>
            /// <param name="action">Method that will be called when message is recieved</param>
            public static void Register(object recipient, Action<string> action)
            {
                Messenger.Default.Register<string>(recipient, MessageTypes.ChangeScreen, action);
            }
        }
        #endregion

        #region → Do Operation Message           .


        /// <summary>
        /// Used To Inform the View That The view Model Have Complete Any Task
        /// </summary>
        public static class DoOperationMessage
        {
            /// <summary>
            /// Send this type of message to any recipient who want to register that type of messages
            /// </summary>
            public static void Send(OperationType Types)
            {
                Messenger.Default.Send<OperationType>(Types, MessageTypes.DoOperation);
            }

            /// <summary>
            /// Register to recieve that type of message
            /// </summary>
            /// <param name="recipient">The recipient that has register for this type of message</param>
            /// <param name="action">Method that will be called when message is recieved</param>
            public static void Register(object recipient, Action<OperationType> action)
            {
                Messenger.Default.Register<OperationType>(recipient, MessageTypes.DoOperation, action);
            }
        }

        #endregion

        #region → Flip Message                   .
        /// <summary>
        /// Class to flip any flip control
        /// </summary>
        public static class FlippMessage
        {
            /// <summary>
            /// Send this type of message to any recipient who want to register that type of messages
            /// </summary>
            public static void Send()
            {
                Messenger.Default.Send<bool>(true);
            }

            /// <summary>
            /// Send this type of message to any recipient who want to register that type of messages
            /// </summary>
            public static void Send(bool isFlip)
            {
                Messenger.Default.Send<bool>(isFlip);
            }

            /// <summary>
            /// Send this type of message to any recipient who want to register that type of messages
            /// </summary>
            public static void Send(string ViewName)
            {

                Messenger.Default.Send<string>(ViewName);
            }

            /// <summary>
            /// Register to recieve that type of message
            /// </summary>
            /// <param name="recipient">The recipient that has register for this type of message</param>
            /// <param name="action">Method that will be called when message is recieved</param>
            public static void Register(object recipient, Action<bool> action)
            {
                Messenger.Default.Register<bool>(recipient, action);
            }

            /// <summary>
            /// Register to recieve that type of message
            /// </summary>
            /// <param name="recipient">The recipient that has register for this type of message</param>
            /// <param name="action">Method that will be called when message is recieved</param>
            public static void Register(object recipient, Action<string> action)
            {
                Messenger.Default.Register<string>(recipient, action);
            }
        }
        #endregion

        #region → Ativate Application Message    .

        /// <summary>
        /// Ativate Application Message
        /// that will be for open activate tab.
        /// </summary>
        public static class AtivateApplicationMessage
        {
            /// <summary>
            /// Send this type of message to any recipient who want to register that type of messages
            /// </summary>
            public static void Send(Guid applicationID)
            {
                Messenger.Default.Send<Guid>(applicationID, MessageTypes.ActivateApplication);
            }

            /// <summary>
            /// Register to recieve that type of message
            /// </summary>
            /// <param name="recipient">The recipient that has register for this type of message</param>
            /// <param name="action">Method that will be called when message is recieved</param>
            public static void Register(object recipient, Action<Guid> action)
            {
                Messenger.Default.Register<Guid>(recipient, MessageTypes.ActivateApplication, action);
            }
        }

        #endregion

        #region → Status Update Message          .
        /// <summary>
        /// Class to update status
        /// </summary>
        public static class StatusUpdateMessage
        {
            /// <summary>
            /// Send this type of message to any recipient who want to register that type of messages
            /// </summary>
            public static void Send(DialogMessage dialogMessage)
            {
                Messenger.Default.Send<DialogMessage>(dialogMessage, MessageTypes.StatusUpdate);
            }

            /// <summary>
            /// Register to recieve that type of message
            /// </summary>
            /// <param name="recipient">The recipient that has register for this type of message</param>
            /// <param name="action">Method that will be handle the excption send and appear friendly message</param>
            public static void Register(object recipient, Action<DialogMessage> action)
            {
                Messenger.Default.Register<DialogMessage>(recipient, MessageTypes.StatusUpdate, action);
            }
        }
        #endregion

        #region → Status Update Message          .

        /// <summary>
        /// Class to update status
        /// </summary>
        public static class SendCustomMessage
        {

            /// <summary>
            /// Send this type of message to any recipient who want to register that type of messages
            /// </summary>
            public static void Send(eNegMessage dialogMessage)
            {
                Messenger.Default.Send<eNegMessage>(dialogMessage, MessageTypes.CustomMessage);
            }

            /// <summary>
            /// Register to recieve that type of message
            /// </summary>
            /// <param name="recipient">The recipient that has register for this type of message</param>
            /// <param name="action">Method that will be handle the excption send and appear friendly message</param>
            public static void Register(object recipient, Action<eNegMessage> action)
            {
                Messenger.Default.Register<eNegMessage>(recipient, MessageTypes.CustomMessage, action);
            }
        }
        #endregion

        #region → Submit Changes Message         .

        /// <summary>
        /// Class to submit any pending changes
        /// </summary>
        public static class SubmitChangesMessage
        {
            /// <summary>
            /// Send this type of message to any recipient who want to register that type of messages
            /// </summary>
            /// <param name="OpType">Type of the op.</param>
            public static void Send(OperationType OpType = OperationType.SubmitAll)
            {
                Messenger.Default.Send<OperationType>(OpType, MessageTypes.SubmitChanges);
            }

            /// <summary>
            /// Register to recieve that type of message
            /// </summary>
            /// <param name="recipient">The recipient that has register for this type of message</param>
            /// <param name="action">Method that will be called when message is recieved</param>
            public static void Register(object recipient, Action<OperationType> action)
            {
                Messenger.Default.Register<OperationType>(recipient, MessageTypes.SubmitChanges, action);
            }
        }
        #endregion

        #region → Cancel Changes Message         .

        /// <summary>
        /// Class to reject any pending changes
        /// </summary>
        public static class CancelChangesMessage
        {
            /// <summary>
            /// Send this type of message to any recipient who want to register that type of messages
            /// </summary>
            public static void Send()
            {
                Messenger.Default.Send<Boolean>(true, MessageTypes.CancelChanges);
            }

            /// <summary>
            /// Register to recieve that type of message
            /// </summary>
            /// <param name="recipient">The recipient that has register for this type of message</param>
            /// <param name="action">Method that will be called when message is recieved</param>
            public static void Register(object recipient, Action<Boolean> action)
            {
                Messenger.Default.Register<Boolean>(recipient, MessageTypes.CancelChanges, action);
            }
        }

        #endregion

        #region → Raise Error Message            .

        /// <summary>
        /// Class to handle any raised exception
        /// </summary>
        public static class RaiseErrorMessage
        {
            /// <summary>
            /// Gets or sets the name of the application.
            /// </summary>
            /// <value>The name of the application.</value>
            public static string ApplicationName { get; set; }

            /// <summary>
            /// Send this type of message to any recipient who want to register that type of messages
            /// </summary>
            public static void Send(Exception ex)
            {
                Messenger.Default.Send<Exception>(ex, MessageTypes.RaiseError);
            }

            /// <summary>
            /// Register to recieve that type of message
            /// </summary>
            /// <param name="recipient">The recipient that has register for this type of message</param>
            /// <param name="action">Method that will be handle the excption send and appear friendly message</param>
            public static void Register(object recipient, Action<Exception> action)
            {
                Messenger.Default.Register<Exception>(recipient, MessageTypes.RaiseError, action);
            }
        }
        #endregion

        #region → Confirm Message                .

        /// <summary>
        /// Class to Wait user confirm
        /// </summary>
        public static class ConfirmMessage
        {
            /// <summary>
            /// Send this type of message to any recipient who want to register that type of messages
            /// </summary>
            public static void Send(DialogMessage dialogMessage)
            {
                Messenger.Default.Send<DialogMessage>(dialogMessage, MessageTypes.Confirm);
            }

            /// <summary>
            /// Register to recieve that type of message
            /// </summary>
            /// <param name="recipient">The recipient that has register for this type of message</param>
            /// <param name="action">Dialog Messge that will wait for user response</param>
            public static void Register(object recipient, Action<DialogMessage> action)
            {
                Messenger.Default.Register<DialogMessage>(recipient, MessageTypes.Confirm, action);
            }
        }

        #endregion

        #region → Load User Message              .
        /// <summary>
        /// Class to submit any pending changes
        /// </summary>
        public static class LoadUserMessage
        {
            /// <summary>
            /// Send this type of message to any recipient who want to register that type of messages
            /// </summary>
            public static void Send(bool WasPresistent)
            {
                Messenger.Default.Send<Boolean>(true, MessageTypes.LoadUser);
            }

            /// <summary>
            /// Register to recieve that type of message
            /// </summary>
            /// <param name="recipient">The recipient that has register for this type of message</param>
            /// <param name="action">Method that will be called when message is recieved</param>
            public static void Register(object recipient, Action<Boolean> action)
            {
                Messenger.Default.Register<Boolean>(recipient, MessageTypes.LoadUser, action);
            }
        }
        #endregion

        #region → Show PopUp Message             .

        /// <summary>
        /// Opening a new PopUp
        /// </summary>
        public static class NewPopUp
        {
            #region → Properties  .

            /// <summary>
            /// Gets or sets the type of the pop up.
            /// </summary>
            /// <value>The type of the pop up.</value>
            public static string PopUpType { get; set; }

            #endregion

            /// <summary>
            /// Sends the specified issue.
            /// </summary>
            /// <param name="value">The issue.</param>
            /// <param name="type">The type.</param>
            public static void Send(string value, PopUpType type)
            {
                PopUpType = type.ToString();
                Messenger.Default.Send<string>(value, MessageTypes.AddNewPopUpMessage);
            }

            /// <summary>
            /// Registers the specified recipient.
            /// </summary>
            /// <param name="recipient">The recipient.</param>
            /// <param name="action">The action.</param>
            public static void Register(object recipient, Action<string> action)
            {
                Messenger.Default.Register<string>(recipient, MessageTypes.AddNewPopUpMessage, action);
            }

        }

        #endregion

        #region → LoadOperationComplete          .

        /// <summary>
        /// Class to send recieve messages when certain operation complete
        /// </summary>
        public static class LoadCompleted
        {
            /// <summary>
            /// Send this type of message to any recipient who want to register that type of messages
            /// </summary>
            /// <param name="OperationName">Name of the operation.</param>
            public static void Send(string OperationName)
            {
                Messenger.Default.Send<string>(OperationName, MessageTypes.LoadComplete);
            }

            /// <summary>
            /// Register to recieve that type of message
            /// </summary>
            /// <param name="recipient">The recipient that has register for this type of message</param>
            /// <param name="action">Method that will be called when message is recieved</param>
            public static void Register(object recipient, Action<string> action)
            {
                Messenger.Default.Register<string>(recipient, MessageTypes.LoadComplete, action);
            }
        }
        #endregion

        #region → Build Control                  .

        /// <summary>
        /// Class to Build certain control
        /// </summary>
        public static class BuildControl
        {
            /// <summary>
            /// Sends the specified control type.
            /// </summary>
            /// <param name="ControlType">Type of the control.</param>
            public static void Send(string ControlType)
            {
                Messenger.Default.Send<string>(ControlType, MessageTypes.BuildControl);
            }

            /// <summary>
            /// Registers the specified recipient.
            /// </summary>
            /// <param name="recipient">The recipient.</param>
            /// <param name="action">The action.</param>
            public static void Register(object recipient, Action<string> action)
            {
                Messenger.Default.Register<string>(recipient, MessageTypes.BuildControl, action);
            }
        }

        #endregion

        #region → Load Queue for Negotiations    .

        /// <summary>
        /// Class to Load Queue for Negotiations
        /// </summary>
        public static class LoadingQueueMessage
        {
            /// <summary>
            /// Send this type of message to any recipient who want to register that type of messages
            /// </summary>
            /// <param name="loadQueueItem">The load queue item.</param>
            public static void Send(LoadingQueue loadQueueItem)
            {
                Messenger.Default.Send<LoadingQueue>(loadQueueItem, MessageTypes.LoadingQueue);
            }

            /// <summary>
            /// Register to recieve that type of message
            /// </summary>
            /// <param name="recipient">The recipient that has register for this type of message</param>
            /// <param name="action">Method that will be called when message is recieved</param>
            public static void Register(object recipient, Action<LoadingQueue> action)
            {
                Messenger.Default.Register<LoadingQueue>(recipient, MessageTypes.LoadingQueue, action);
            }
        }
        #endregion

        #endregion
    }
}
