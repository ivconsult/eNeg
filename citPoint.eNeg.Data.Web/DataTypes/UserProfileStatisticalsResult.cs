

#region → Usings   .
using System;
using System.ComponentModel.DataAnnotations;



#endregion

#region → History  .

/* 
 * Date         User            Change
 * *********************************************
 * 18.08.11     mwahab         • creation
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
     [MetadataTypeAttribute(typeof(UserProfileStatisticalsResult.UserProfileStatisticalsResultMetadata))]
    public partial class UserProfileStatisticalsResult
    {

        
        internal sealed class UserProfileStatisticalsResultMetadata
        {

            // Metadata classes are not meant to be instantiated.
            private UserProfileStatisticalsResultMetadata()
            {

            }

            #region → Properties     .

            /// <summary>
            /// Gets or sets the name of the statistical.
            /// </summary>
            /// <value>The name of the statistical.</value>
            [Key]
            public string StatisticalName { get; set; }

            /// <summary>
            /// Gets or sets the statistical value.
            /// </summary>
            /// <value>The statistical value.</value>
            public decimal StatisticalValue { get; set; }
            
            #endregion Properties

        }
    }

}
