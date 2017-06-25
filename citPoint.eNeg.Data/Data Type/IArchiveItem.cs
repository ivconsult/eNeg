
#region → Usings   .
using System.Collections.Generic;
using System.ComponentModel;

#endregion

#region → History  .

/* 
 * Date         User            Change
 * *********************************************
 * 3/26/2012 2:22:17 PM      mwahab         • creation
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

namespace citPOINT.eNeg.Data
{
    /// <summary>
    /// Interface for Archive Item.
    /// </summary>
    public interface IArchiveItem : INotifyPropertyChanged
    {
        #region → Properties     .

        /// <summary>
        /// Gets or sets the type of the archive item.
        /// </summary>
        /// <value>The type of the archive item.</value>
        ArchiveItemTypes ArchiveItemType { get; set; }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>The value.</value>
        object Value { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        string Name { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is loaded on demand.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is loaded on demand; otherwise, <c>false</c>.
        /// </value>
        bool IsLoadedOnDemand { get; set; }

        /// <summary>
        /// Gets or sets the parent.
        /// </summary>
        /// <value>The parent.</value>
        IArchiveItem Parent { get; set; }

        /// <summary>
        /// Gets or sets the children.
        /// </summary>
        /// <value>The children.</value>
        IList<IArchiveItem> Children { get; set; }

        /// <summary>
        /// Refreshes the children.
        /// </summary>
        void RefreshChildren();

        #endregion
    }
}
