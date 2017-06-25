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
    /// Interface for Negotiation
    /// </summary>
    public interface INegotiation
    {
        #region → Properties     .

        /// <summary>
        /// Gets or sets the negotiation ID.
        /// </summary>
        /// <value>The negotiation ID.</value>
        Guid NegotiationID { get; set; }

        /// <summary>
        /// Gets or sets the name of the negotiation.
        /// </summary>
        /// <value>The name of the negotiation.</value>
        string NegotiationName { get; set; }
        
        /// <summary>
        /// Gets a value indicating whether this instance is closed.
        /// </summary>
        /// <value><c>true</c> if this instance is closed; otherwise, <c>false</c>.</value>
        bool IsClosed { get;  }

        #endregion
    }
}
