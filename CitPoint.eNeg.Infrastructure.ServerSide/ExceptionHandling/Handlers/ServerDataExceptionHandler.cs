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
    /// Class that handles server's data exceptions
    /// </summary>
    public class ServerDataExceptionHandler : BaseExceptionHandler<ServerDataException>
    {
        #region → Fields         .

        #endregion

        #region → Properties     .



        #endregion

        #region → Constructors   .

        /// <summary>
        /// Constructor
        /// </summary>
         
        public ServerDataExceptionHandler()
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
        
        public override ExceptionHandlingResult Execute(ServerDataException ex)
        {
            if (!ex.IsHandled)
            {
                return DefaultHandle(ex);
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

        private void RegisterDefaultCommands()
        {
            RegisterCommand(new LogCommand<ServerLoggingHandler>()
            {
                Message = citPOINT.eNeg.Infrastructure.Common.ExceptionsFriendlyMessages.ServerDataError
            });
            RegisterCommand(new WrapExceptionCommand(typeof(ServerDataException),citPOINT.eNeg.Infrastructure.Common.ExceptionsFriendlyMessages.ServerDataError));
        }

        #endregion

        #region → Protected      .


        #endregion

        #endregion
    }
}
