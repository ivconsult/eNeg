
#region → Usings   .
using System;
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
    /// Server Exception Handler Provider
    /// </summary>
    public class ServerExceptionHandlerProvider
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

            ExceptionManager.Instance.postHandlingAction = PostHandlingAction.NotifyRethrow;
            ExceptionManager.Instance.AddPolicy("Policy1");


            replaceHandler.ReplaceExceptionType = typeof(System.Exception);
            replaceHandler.NewExceptionMessage = "There is a problem so please contact your administrator or reload the page";

            LoggingHandler<ServerLoggingHandler> _loggingHandler = new LoggingHandler<ServerLoggingHandler>();
            _loggingHandler.Category = Category.ServerData;

            ExceptionManager.Instance.GetPolicy("Policy1").AddHandler(_loggingHandler);
            //ExceptionManager.Instance.GetPolicy("Policy1").AddHandler(replaceHandler);
        }

      

        #endregion

        #endregion
    }
}
