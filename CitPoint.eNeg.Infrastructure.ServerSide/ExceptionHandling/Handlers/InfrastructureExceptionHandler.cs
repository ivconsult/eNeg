#region → Usings   .
using citPOINT.eNeg.Infrastructure.Common;

#endregion

#region → History  .
/*
    Date        User        Change
 *  28.07.2010  Dergham     • Creation
 */

#endregion

#region → ToDos    .
#endregion

namespace citPOINT.eNeg.Infrastructure.ExceptionHandling
{
    /// <summary>
    /// 
    /// </summary>
    public class InfrastructureExceptionHandler : BaseExceptionHandler<InfrastructureException>
    {
        #region → Fields         .



        #endregion

        #region → Properties     .



        #endregion

        #region → Constructors   .

        /// <summary>
        /// Constructor
        /// </summary>
        public InfrastructureExceptionHandler()
        {
            RegisterDefaultCommands();
        }


        #endregion

        #region → Methods        .

        #region → Public         . method

        /// <summary>
        /// Excecute the command 
        /// </summary>
        /// <param name="ex">Exception</param>
        /// <returns>ExceptionHandlingResult</returns>
        public override ExceptionHandlingResult Execute(InfrastructureException ex)
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

        /// <summary>
        /// Register Default Commands
        /// </summary>
        private void RegisterDefaultCommands()
        {
            RegisterCommand(new WrapExceptionCommand(typeof(CommonException), ExceptionsFriendlyMessages.CommonDataError));
        }

        #endregion

        #endregion
    }
}
