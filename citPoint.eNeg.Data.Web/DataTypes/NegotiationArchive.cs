

#region → Usings   .
using System;
using System.ComponentModel.DataAnnotations;



#endregion

#region → History  .

/* 
 * Date         User            Change
 * *********************************************
 * 25.03.12     mwahab         • creation
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


namespace citPOINT.eNeg.Data.Web
{

    /// <summary>
    /// User Profile Statisticals Result
    /// </summary>
    [MetadataTypeAttribute(typeof(NegotiationArchive.NegotiationArchiveMetadata))]
    public partial class NegotiationArchive
    {
        internal sealed class NegotiationArchiveMetadata
        {

            // Metadata classes are not meant to be instantiated.
            private NegotiationArchiveMetadata()
            {

            }

            #region → Properties     .


            /// <summary>
            /// Gets or sets the archive ID.
            /// </summary>
            /// <value>The archive ID.</value>
            [Key]
            public int ArchiveID { get; set; }

            /// <summary>
            /// Gets or sets the archive month.
            /// </summary>
            /// <value>The archive month.</value>
            public int? ArchiveMonth { get; set; }

            public int? ArchiveYear { get; set; }

            #endregion Properties

        }
    }

}
