#region → Usings   .
using System;
using System.ComponentModel;
using citPOINT.eNeg.Data.Web;
#endregion

#region → History  .

/* Date         User         Change
 * 
 * 18.03.12     M.Wahab      •creation
 */

# endregion

#region → ToDos    .

/*
 * Date         set by User     Description
 * 
 * 
*/

# endregion

namespace citPOINT.eNeg.Common
{
    /// <summary>
    /// Interface for Application Model that will help in integration process.
    /// </summary>
    public interface IApplicationModel
    {
        #region → Properties     .
               
        /// <summary>
        /// Gets a value indicating whether this instance is busy.
        /// </summary>
        /// <value><c>true</c> if this instance is busy; otherwise, <c>false</c>.</value>
        bool IsBusy { get; }

        #endregion

        #region → Events         .

        /// <summary>
        /// Occurs when [get application complete].
        /// </summary>
        event EventHandler<eNegEntityResultArgs<Application>> GetApplicationComplete;

        /// <summary>
        /// Occurs when [property changed].
        /// </summary>
        event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region → Methods        .

        /// <summary>
        /// Gets application asynchronously
        /// </summary>
        void GetApplicationAsync();

        #endregion        
    }
}
