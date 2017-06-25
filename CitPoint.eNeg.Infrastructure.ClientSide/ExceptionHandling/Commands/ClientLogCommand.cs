#region → Usings   .
using System;
#endregion

#region → History  .
/*
    Date        User        Change
 *  27.07.2010  Dergham     • Creation
 */

#endregion

#region → ToDos    .
#endregion

namespace citPOINT.eNeg.Infrastructure.ExceptionHandling
{
    /// <summary>
    /// Client Log Command
    /// </summary>
    public class ClientLogCommand:BaseExceptionHandlerCommand
    {
        /// <summary>
        /// Execute the command
        /// </summary>
        /// <param name="ex">Exception</param>
        /// <returns>Boolean</returns>
        public override bool Execute(Exception ex)
        {
            Logging.LoggingProvider<Logging.ClientLoggingHandler>.Log(PrepareMessage(ex), Logging.Category.ClientData);
            return true;
        }
    }
}
