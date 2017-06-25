#region → Usings   .
#endregion

#region → History  .

/* 
 * Date         User            Change
 * *********************************************
 * 17.03.12    mwahab         • creation
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
    /// Interface for Observer App.
    /// </summary>
    public interface IObserverApp
    {
        #region → Properties     .

        /// <summary>
        /// Gets the name of the app.
        /// </summary>
        /// <value>The name of the app.</value>
        string AppName { get; }

        #endregion

        #region → Methods        .

        #region → Public         .

        /// <summary>
        /// Applies the changes.
        /// </summary>
        /// <param name="isActive">if set to <c>true</c> [is active].</param>
        void ApplyChanges(bool isActive);

        #endregion

        #endregion
    }
}

