
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
    /// Replace Handler
    /// </summary>
    public class ReplaceHandler : IHandler
    {
        #region → Fields         .

        private Type mNewExceptionType;
        private Exception mReplaceException;
        private string mNewExceptionMessage;

        #endregion

        #region → Properties     .

        /// <summary>
        /// Gets or sets the type of the replace exception.
        /// </summary>
        /// <value>The type of the replace exception.</value>
        public Type ReplaceExceptionType
        {
            set { mNewExceptionType = value; }
            get { return mNewExceptionType; }
        }

        /// <summary>
        /// Gets or sets the new exception message.
        /// </summary>
        /// <value>The new exception message.</value>
        public string NewExceptionMessage
        {
            set { mNewExceptionMessage = value; }
            get { return mNewExceptionMessage; }
        }

        #endregion Properties

        #region → Methods        .

        #region → Public         .

        /// <summary>
        /// Excutes the specified exception.
        /// </summary>
        /// <param name="e">The exception.</param>
        /// <returns>Exception</returns>
        public Exception Excute(Exception e)
        {
            
            string temp = ExceptionResource.ResourceManager.GetString(e.GetType().ToString().Replace(".", "_"));
            NewExceptionMessage = string.IsNullOrEmpty(temp) ? NewExceptionMessage : temp;

            NewExceptionMessage = string.IsNullOrEmpty(NewExceptionMessage) ? e.Message : NewExceptionMessage;

            if (e.Message.Contains("Invalid credentials"))
            {
                NewExceptionMessage = ExceptionResource.ServiceInvalidCredentials;
            }
            mReplaceException = (Exception)Activator.CreateInstance(mNewExceptionType, new object[] { NewExceptionMessage });

            return mReplaceException;
        }

        #endregion  Public


        #endregion Methods
    }
}
