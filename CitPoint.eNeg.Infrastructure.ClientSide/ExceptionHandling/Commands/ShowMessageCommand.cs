#region → Usings   .
using citPOINT.eNeg.Infrastructure.ClientSide.ExceptionHandling;
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
    /// Show Message Command
    /// </summary>
    public class ShowMessageCommand:BaseExceptionHandlerCommand
    {
        #region → Fields         .



        #endregion

        #region → Properties     .



        #endregion

        #region → Constructors   .



        #endregion

        #region → Methods        .

        /// <summary>
        /// Execute
        /// </summary>
        /// <param name="ex">Exception</param>
        /// <returns>Boolean</returns>
        public override bool Execute(Exception ex)
        {


          // Message= citPOINT.eNeg.Infrastructure.Common.ExceptionsFriendlyMessages.CommonException;
           MessagesWindow win = new MessagesWindow(Message,ex.StackTrace);
           win.Show();
            return true;
        }

        #endregion
    }
}
