
#region → Usings   .
using System;
using citPOINT.eNeg.Infrastructure.ClientSide.ExceptionHandling;
using citPOINT.eNeg.Infrastructure.Logging;

#endregion

#region → History  .

/* 
 * Date         User            Change
 * *********************************************
 * 5/3/2011      Team         • creation
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

namespace citPOINT.eNeg.Infrastructure.ExceptionHandling
{
    /// <summary>
    /// Client Exception Handler Provider
    /// </summary>
    public class ClientExceptionHandlerProvider
    {
        #region → Fields         .

        internal static string LastShownMessage = null;

        #endregion

        #region → Methods        .

        #region → Public         .

        /// <summary>
        /// Repaires the exception policies.
        /// </summary>
        public static void RepaireExceptionPolicies()
        {
            ReplaceHandler replaceHandler = new ReplaceHandler();

            ExceptionManager.Instance.postHandlingAction = PostHandlingAction.None;
            ExceptionManager.Instance.AddPolicy("Policy1");


            replaceHandler.ReplaceExceptionType = typeof(System.Exception);
            replaceHandler.NewExceptionMessage = "There is a problem so please contact your administrator or reload the page";

            LoggingHandler<ClientLoggingHandler> _loggingHandler = new LoggingHandler<ClientLoggingHandler>();
            _loggingHandler.Category = Category.Client;

            ExceptionManager.Instance.GetPolicy("Policy1").AddHandler(_loggingHandler);
            ExceptionManager.Instance.GetPolicy("Policy1").AddHandler(replaceHandler);
        }

        /// <summary>
        /// Shows the message error window.
        /// </summary>
        /// <param name="header">The header.</param>
        /// <param name="exception">The exception.</param>
        public static void ShowMessageErrorWindow(string header, Exception exception)
        {
            ShowMessageErrorWindow(header, exception, String.Empty);
        }
        
        /// <summary>
        /// Shows the message error window.
        /// </summary>
        /// <param name="header">The header.</param>
        /// <param name="exception">The exception.</param>
        /// <param name="applicationName">Name of the application.</param>
        public static void ShowMessageErrorWindow(string header, Exception exception, string applicationName)
        {
            if (ClientExceptionHandlerProvider.LastShownMessage == null || ClientExceptionHandlerProvider.LastShownMessage != header)
            {
                ClientExceptionHandlerProvider.LastShownMessage = header;

                MessagesWindow win = new MessagesWindow(header, exception, applicationName);

                win.Show();
            }
        }

        #endregion

        #endregion
    }
}
