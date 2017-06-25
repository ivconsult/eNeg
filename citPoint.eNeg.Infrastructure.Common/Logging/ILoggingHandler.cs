#region → Usings   .
using System.Collections.Generic;
#endregion

#region → History  .
/* Date         User        Change
 * 
 * 20.07.10    m.Wahab     • creation
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
    /// This Interface Used to generalize the logging Operation as a server side or a client side
    /// </summary>
    public interface ILoggingHandler
    {

        #region → Methods        .


        /// <summary>
        /// Used  to Log any data or exceptions that occured during running the program.
        /// </summary>
        /// <param name="Msg">the user friendly massage or any informations</param>
        /// <param name="category">Mean the project who have the problem or who want to write a message</param>
        /// <param name="priority">mean the important of the logging ex.High or Low</param>
        /// <param name="Type">the Type of the message ex.Error or info.</param>
        /// <param name="Properties">any addition informations</param>
        void Log(string Msg, Category category, Priority priority, SeverityType Type, IDictionary<string, object> Properties);

        #endregion

    }
}
