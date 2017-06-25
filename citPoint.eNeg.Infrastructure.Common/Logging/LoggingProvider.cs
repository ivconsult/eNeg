#region → Usings   .
using System.Collections.Generic;
#endregion

#region → History  .
/* Date         User        Change
 * 
 * 22.07.10    m.Wahab     • creation
 * 
 */
#endregion

#region → ToDos    .

/*
 * Date         set by User     Description
 * 
 * 
*/

#endregion

namespace citPOINT.eNeg.Infrastructure.Logging
{
    /// <summary>
    /// Used To Log On Server Or Clients
    /// </summary>
    public static class LoggingProvider<T> where T : ILoggingHandler
    {
        #region → Fields         .
        private static ILoggingHandler _logger;
        #endregion
        
        #region → Methods        .

        #region → Private        .
     
        /// <summary>
        /// Gets the logger.
        /// </summary>
        /// <value>The logger.</value>
        private static ILoggingHandler Logger
        {
            get
            {
                if (_logger == null)
                    _logger = InsatanceFactory.CreateInstant<T>();

                return _logger;
            }

        }

        #endregion
        
        #region → Public         .



        /// <summary>
        /// Used  to Log any data or exceptions that occured during running the program.
        /// </summary>
        /// <param name="Msg">the user friendly massage or any informations</param>
        /// <param name="category">Mean the project who have the problem or who want to write a message</param>
        public static void Log(string Msg, Category category)
        {
            Log(Msg, category, Priority.Normal, SeverityType.Information, null);
        }

        /// <summary>
        /// Used  to Log any data or exceptions that occured during running the program.
        /// </summary>
        /// <param name="Msg">the user friendly massage or any informations</param>
        /// <param name="category">Mean the project who have the problem or who want to write a message</param>
        /// <param name="priority">mean the important of the logging ex.High or Low</param>
        public static void Log(string Msg, Category category, Priority priority)
        {
            Log(Msg, category, priority, SeverityType.Information, null);

        }

        /// <summary>
        /// Used  to Log any data or exceptions that occured during running the program.
        /// </summary>
        /// <param name="Msg">the user friendly massage or any informations</param>
        /// <param name="category">Mean the project who have the problem or who want to write a message</param>
        /// <param name="priority">mean the important of the logging ex.High or Low</param>
        /// <param name="Type">the Type of the message ex.Error or info.</param>
        public static void Log(string Msg, Category category, Priority priority, SeverityType Type)
        {
            Log(Msg, category, priority, Type, null);
        }

        /// <summary>
        /// Used  to Log any data or exceptions that occured during running the program.
        /// </summary>
        /// <param name="Msg">the user friendly massage or any informations</param>
        /// <param name="category">Mean the project who have the problem or who want to write a message</param>
        /// <param name="Properties">any addition informations</param>
        public static void Log(string Msg, Category category, IDictionary<string, object> Properties)
        {
            Log(Msg, category, Priority.Normal, SeverityType.Information, Properties);

        }




        /// <summary>
        /// Used  to Log any data or exceptions that occured during running the program.
        /// </summary>
        /// <param name="Msg">the user friendly massage or any informations</param>
        /// <param name="category">Mean the project who have the problem or who want to write a message</param>
        /// <param name="priority">mean the important of the logging ex.High or Low</param>
        /// <param name="Type">the Type of the message ex.Error or info.</param>
        /// <param name="Properties">any addition informations</param>
        public static void Log(string Msg, Category category, Priority priority, SeverityType Type, IDictionary<string, object> Properties)
        {
            #region Filling Extra properties

            Logger.Log(Msg, category, priority, Type, Properties);
            #endregion
        }

        #endregion

        #endregion

    }
}

