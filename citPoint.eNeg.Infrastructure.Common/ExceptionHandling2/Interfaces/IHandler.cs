
#region → Usings   .
using System;
using System.Collections.Generic;
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
    /// Interface Handler
    /// </summary>
    public interface IHandler
    {
        /// <summary>
        /// Gets or sets the new exception message.
        /// </summary>
        /// <value>The new exception message.</value>
        string NewExceptionMessage { get; set; }

        /// <summary>
        /// Excutes the specified exception.
        /// </summary>
        /// <param name="e">The exception.</param>
        /// <returns>Exception</returns>
        Exception Excute(Exception e);
    }
}
