
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
    public class WrapHandler : IHandler
    {
        #region → Fields         .

        private Type mWrapperExceptionType;
        private string mExceptionMessage;
        private Exception mWrapperException;

        #endregion

        #region → Properties     .

        /// <summary>
        /// Gets or sets the type of the wrapper exception.
        /// </summary>
        /// <value>The type of the wrapper exception.</value>
        public Type WrapperExceptionType
        {
            set { mWrapperExceptionType = value; }
            get { return mWrapperExceptionType; }
        }

        /// <summary>
        /// Gets or sets the new exception message.
        /// </summary>
        /// <value>The new exception message.</value>
        public string NewExceptionMessage
        {
            set { mExceptionMessage = value; }
            get { return mExceptionMessage; }
        }

        #endregion

        #region → Methods        .


        #region → Public

        /// <summary>
        /// Excutes the specified exception.
        /// </summary>
        /// <param name="e">The exception.</param>
        /// <returns>Exception</returns>
        public Exception Excute(Exception e)
        {
            mWrapperException = (Exception)Activator.CreateInstance(mWrapperExceptionType, new object[] { mExceptionMessage, e });
            return mWrapperException;
        }

        #endregion

        #endregion
    }
}
