
#region → Usings   .
using System;
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
    /// Exception Handling Final result
    /// </summary>
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name = "ExceptionHandlingResult", Namespace = "http://schemas.datacontract.org/2004/07/citPOINT.Infrastructure.ExceptionHandling")]
    public class ExceptionHandlingResult
    {
        #region → Fields         .

        private Exception mE;
        private bool mIsHandled;
        private string mMessage;

        #endregion

        #region → Properties     .

        /// <summary>
        /// Gets the e.
        /// </summary>
        /// <value>The e.</value>
        public Exception e { get { return mE; } }

        /// <summary>
        /// Gets a value indicating whether this instance is handled.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is handled; otherwise, <c>false</c>.
        /// </value>
        public bool IsHandled { get { return mIsHandled; } }

        /// <summary>
        /// Gets the message.
        /// </summary>
        /// <value>The message.</value>
        public string Message { get { return mMessage; } }

        #endregion Properties

        #region → Constractor    .

        /// <summary>
        /// Initializes a new instance of the <see cref="ExceptionHandlingResult"/> class.
        /// </summary>
        public ExceptionHandlingResult()
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ExceptionHandlingResult"/> class.
        /// </summary>
        /// <param name="e">The exception.</param>
        /// <param name="ishandled">if set to <c>true</c> [ishandled].</param>
        /// <param name="message">The message.</param>
        public ExceptionHandlingResult(Exception e, bool ishandled, string message)
        {
            mE = e;
            mIsHandled = ishandled;
            mMessage = message;
        }
        #endregion
         
    }
}
