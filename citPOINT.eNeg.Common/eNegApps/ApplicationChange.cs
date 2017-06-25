
#region → Usings   .
using citPOINT.eNeg.Data.Web;
using citPOINT.eNeg.Apps.Common.Interfaces;
#endregion

#region → History  .

/* Date         User            change
 * 
 * 20.03.12     M.Wahab     creation
 */

# endregion

#region → ToDos    .

/*
 * Date         set by User     Description
 * 
*/

# endregion
namespace citPOINT.eNeg.Common.eNegApps
{   /// <summary>
    /// Class collecting all Apps Changes.
    /// </summary>
    public class ApplicationChange
    {
        #region → Properties     .

        /// <summary>
        /// Gets or sets a value indicating whether [apply changed].
        /// </summary>
        /// <value><c>true</c> if [apply changed]; otherwise, <c>false</c>.</value>
        public bool ApplyChanged { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is for user changes.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is for user changes; otherwise, <c>false</c>.
        /// </value>
        public bool IsForUserChanges { get; set; }

        /// <summary>
        /// Gets or sets the current user.
        /// </summary>
        /// <value>The current user.</value>
        public IUserInfo CurrentUser { get; set; }

        /// <summary>
        /// Gets or sets the current negotiation.
        /// </summary>
        /// <value>The current negotiation.</value>
        public Negotiation CurrentNegotiation { get; set; }

        /// <summary>
        /// Gets or sets the current conversation.
        /// </summary>
        /// <value>The current conversation.</value>
        public Conversation CurrentConversation { get; set; }

        #endregion
    }
}
