
#region → Usings   .
using System;
using GalaSoft.MvvmLight.Messaging;
#endregion

#region → History  .

/* Date         User           Change
 * 
 * 25.07.10     Yousra Reda    creation
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
    public sealed class eNegNotifications
    {
        #region Enums

        /// <summary>
        /// Is there is new Negotiation
        /// </summary>
        public const string NewNegotiation = "StartNewNegotiation";
       
        #endregion


        #region static Classes

        /// <summary>
        /// Class to confirm any notify
        /// </summary>
        public static class ConfirmNotify
        {
            /// <summary>
            /// Send this type of message to any recipient who want to register that type of messages
            /// </summary>
            /// <param name="action">value of action</param>
            public static void Send(Action<Boolean> action)
            {
                Messenger.Default.Send<NotificationMessageAction<Boolean>>(
                    new NotificationMessageAction<Boolean>(NewNegotiation, action));
            }

            /// <summary>
            /// Register to recieve that type of message
            /// </summary>
            /// <param name="recipient">The recipient that has register for this type of message</param>
            /// <param name="action">Method that will be called when message is recieved</param>
            public static void Register(object recipient, Action<NotificationMessageAction<Boolean>> action)
            {
                Messenger.Default.Register<NotificationMessageAction<Boolean>>(recipient, action);
            }
        }

        #endregion
    }
}
