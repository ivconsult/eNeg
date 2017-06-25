
#region → Usings   .
using System;
using System.ComponentModel;
using citPOINT.eNeg.Data.Web;

#endregion

#region → History  .

/* Date         User            Change
 * 
 * 04.09.11     yousra reda     •creation
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
    /// Interface for Confimermation model
    /// </summary>
    public interface IConfirmMailModel
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
        /// Occurs when [update user by confirm mail complete].
        /// </summary>
        event EventHandler<eNegEntityResultArgs<User>> UpdateUserByConfirmMailComplete;

        /// <summary>
        /// Occurs when [get organization by user ID complete].
        /// </summary>
        event EventHandler<eNegEntityResultArgs<Organization>> GetOrganizationByUserIDComplete;

        /// <summary>
        /// Occurs when [property changed].
        /// </summary>
        event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region → Methods        .
        /// <summary>
        /// Rejects the changes.
        /// </summary>
        void RejectChanges();
        /// <summary>
        /// Updates the user by confirm mail.
        /// </summary>
        /// <param name="OperationString">The operation string.</param>
        /// <param name="OperationStringType">Type of the operation string.</param>
        void UpdateUserByConfirmMail(string OperationString,byte OperationStringType);

        /// <summary>
        /// Gets the organization by its owner ID.
        /// </summary>
        /// <param name="OwnerID">The owner ID.</param>
        void GetOrganizationByItsOwnerID(Guid OwnerID);

        #endregion
    }
}
