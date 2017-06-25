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
    /// Interface for Conversation.
    /// </summary>
    public interface IConversation
    {
        #region → Properties     .

        /// <summary>
        /// Gets or sets the conversation ID.
        /// </summary>
        /// <value>The conversation ID.</value>
        Guid ConversationID { get; set; }

        /// <summary>
        /// Gets or sets the name of the conversation.
        /// </summary>
        /// <value>The name of the conversation.</value>
        string ConversationName { get; set; }

        #endregion
    }
}
