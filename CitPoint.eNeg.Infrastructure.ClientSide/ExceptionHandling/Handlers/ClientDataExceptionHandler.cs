#region → Usings   .
using citPOINT.eNeg.Infrastructure.Logging;

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
    /// 
    /// </summary>
    public class ClientDataExceptionHandler : BaseExceptionHandler<ClientDataException>
    {
        #region → Fields         .



        #endregion

        #region → Properties     .



        #endregion

        #region → Constructors   .
        /// <summary>
        /// constructor
        /// </summary>
        public ClientDataExceptionHandler()
        {
            RegisterDefaultCommands();
        }

        #endregion

        #region → Methods        .

        #region → Public         .

        /// <summary>
        /// Excecute the command 
        /// </summary>
        /// <param name="ex">Exception</param>
        /// <returns>ExceptionHandlingResult</returns>

        public override ExceptionHandlingResult Execute(ClientDataException ex)
        {
            if (!ex.IsHandled)
            {           
                ExceptionHandlingResult result = new ExceptionHandlingResult();
                foreach (IExceptionHandlerCommand command in Commands.Values)
                {
                  ex.CustomMessage = citPOINT.eNeg.Infrastructure.Common.ExceptionsFriendlyMessages.ClientDataException;
                  result.Handled=  command.Execute(ex);
                  result.Exception = ex;
                  result.Message= command.Message;
                }

                return result;
            }

            else
            {
                ExceptionHandlingResult result = new ExceptionHandlingResult();
                result.Exception = ex;
                result.Handled = true;
                return result;
            }
        }

        #endregion

        #region → Private        . 

        /// <summary>
        /// Register Default Commands
        /// </summary>
        
        private void RegisterDefaultCommands()
        {
            RegisterCommand(new LogCommand<ClientLoggingHandler>()
            {
                Message = citPOINT.eNeg.Infrastructure.Common.ExceptionsFriendlyMessages.ClientDataException
            });
            RegisterCommand(new ShowMessageCommand()
            {
                Message = citPOINT.eNeg.Infrastructure.Common.ExceptionsFriendlyMessages.ClientDataException
            }
            );
        }

        #endregion

        #endregion
    }
}
