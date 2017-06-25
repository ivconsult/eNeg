
#region → Usings   .
#endregion

#region → History  .

/* Date         User           Change
 * 
 * 05.08.10     M Wahab   creation
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

    #region Enums
    /// <summary>
    /// Rights for Authorization
    /// </summary>
    public enum RightsName
    {
        /// <summary>
        /// Delete an Existing Conversation
        /// </summary>
        Conversations_Delete,
        /// <summary>
        /// View Apps
        /// </summary>
        Manage_Apps_See,
        /// <summary>
        /// Add New Negotiation
        /// </summary>
        Negotiations_Start,
        /// <summary>
        /// View or browse the Conversation
        /// </summary>
        Conversations_See,
        /// <summary>
        /// Delete an Existing Negotiation
        /// </summary>
        Negotiations_Delete,
        /// <summary>
        /// View or browse the Negotiation
        /// </summary>
        Negotiations_See,
        /// <summary>
        /// Configure The Apps
        /// </summary>
        Manage_Apps_Configure,
        /// <summary>
        /// Close Re-Open an Existing Negotiation
        /// </summary>
        Negotiations_Close_ReOpen,
        /// <summary>
        /// Add New Conversation
        /// </summary>
        Conversations_Start,
        /// <summary>
        /// Activate De-Active Apps
        /// </summary>
        Manage_Apps_Activate_Deactivate,
        /// <summary>
        /// Rename an Existing Conversation
        /// </summary>
        Conversations_Rename,
        /// <summary>
        /// Rename an Existing Negotiation
        /// </summary>
        Negotiations_Rename
    }

    #endregion
}
