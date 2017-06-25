
#region → Usings   .

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
    /// Post Handling Action
    /// </summary>
    public enum PostHandlingAction
    {
        /// <summary>
        /// Notify Rethrow
        /// </summary>
        NotifyRethrow,

        /// <summary>
        /// Throw New Exception
        /// </summary>
        ThrowNewException,

        /// <summary>
        /// None
        /// </summary>
        None
    }
}
