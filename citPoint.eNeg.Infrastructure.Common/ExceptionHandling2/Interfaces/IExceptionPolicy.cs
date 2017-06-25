
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
    public interface IExceptionPolicy
    {
        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>The name.</value>
        string Name { get; }

        /// <summary>
        /// Excutes the specified e.
        /// </summary>
        /// <param name="e">The Exception.</param>
        /// <returns></returns>
        ExceptionHandlingResult Excute(Exception e);

        /// <summary>
        /// Adds the handler.
        /// </summary>
        /// <param name="h">The Handler.</param>
        /// <returns></returns>
        bool AddHandler(IHandler h);

        /// <summary>
        /// Removes the handler.
        /// </summary>
        /// <param name="h">The Handler.</param>
        /// <returns></returns>
        bool RemoveHandler(IHandler h);
    }
}
