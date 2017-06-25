#region → Usings   .
using System;
using System.ComponentModel;
using System.Linq;
using System.Collections.Generic;

#endregion

#region → History  .

/* 
 * Date         User            Change
 * *********************************************
 * 3/17/2012 10:30:25 AM      mwahab         • creation
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
    /// Class for ITrackChanges 
    /// </summary>
    public interface ITrackChanges 
    {
        #region → Methods        .

        #region → Public         .

        /// <summary>
        /// Adds the observer app.
        /// </summary>
        /// <param name="observerApp">The observer app.</param>
        void AddObserverApp(IObserverApp observerApp);
        
        /// <summary>
        /// Removes the observer app.
        /// </summary>
        /// <param name="observerApp">The observer app.</param>
        void RemoveObserverApp(IObserverApp observerApp);

        /// <summary>
        /// Notifies all registered Apps.
        /// </summary>
        void Notify();
        
        #endregion 
        
        #endregion

    }
}
