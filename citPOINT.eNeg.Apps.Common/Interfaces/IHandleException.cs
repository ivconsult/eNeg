#region → Usings   .
using System;
#endregion

#region → History  .

/* 
 * Date         User            Change
 * *********************************************
 * 18.03.12    mwahab         • creation
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

namespace citPOINT.eNeg.Apps.Common.Interfaces
{
    /// <summary>
    /// Interface Handle Exception.
    /// </summary>
    public interface IMainHandleException
    {
        #region → Methods        .

        #region → Public         .

        /// <summary>
        /// Handles the exception.
        /// </summary>
        /// <param name="exception">The exception.</param>
        /// <param name="applicationName">Name of the application.</param>
        void HandleException(Exception exception,string applicationName);

        #endregion

        #endregion
    }
}
