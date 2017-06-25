
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
    /// Interface Exception Manager
    /// </summary>
    public interface IExceptionManager
    {
        /// <summary>
        /// Handles the exception.
        /// </summary>
        /// <param name="e">The e.</param>
        /// <param name="policy">The policy.</param>
        /// <returns></returns>
        ExceptionHandlingResult HandleException(Exception e, string policy);
    }
}
