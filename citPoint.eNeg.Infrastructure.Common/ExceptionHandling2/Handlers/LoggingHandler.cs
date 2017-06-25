
#region → Usings   .
using System;
using System.Collections.Generic;
using citPOINT.eNeg.Infrastructure.Logging;
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
    /// Loging Handler For Server Side and Client Side
    /// </summary>
    /// <typeparam name="T">T is Type of Logging Handler</typeparam>
    public class LoggingHandler<T> : IHandler where T : ILoggingHandler
    {
        #region → Fields         .

        private string mExceptionMessage;
        private Category mCategory;

        #endregion

        #region → Properties     .

        /// <summary>
        /// Gets or sets the new exception message.
        /// </summary>
        /// <value>The new exception message.</value>
        public string NewExceptionMessage
        {
            set { mExceptionMessage = value; }
            get { return mExceptionMessage; }
        }

        /// <summary>
        /// Gets or sets the category.
        /// </summary>
        /// <value>The category.</value>
        public Category Category
        {
            set { mCategory = value; }
            get { return mCategory; }
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
            string expMessage = string.IsNullOrEmpty(NewExceptionMessage) ? e.Message : NewExceptionMessage;

            LoggingProvider<T>.Log(expMessage, Category);
            return e;
        }

        #endregion  Public


        #endregion Methods

        
    }
}
