#region → Usings   .
using citPOINT.eNeg.Infrastructure.Logging;
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
    /// Server Log Command
    /// </summary>
    public class ServerLogCommand:BaseExceptionHandlerCommand
    {
        /// <summary>
        /// Execute the command
        /// </summary>
        /// <param name="ex">Execute</param>
        /// <returns>Boolean</returns>
        public override bool Execute(Exception ex)
        {
            Logging.LoggingProvider<ServerLoggingHandler>.Log(base.PrepareMessage(ex),Category.ServerData);
            return true;
        }
     
    }
}
