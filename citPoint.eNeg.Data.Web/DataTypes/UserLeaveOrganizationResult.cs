

#region → Usings   .
using System;
using System.ComponentModel.DataAnnotations;



#endregion

#region → History  .

/* 
 * Date         User            Change
 * *********************************************
 * 8/14/2011 9:34:16 AM      mwahab         • creation
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

    // The MetadataTypeAttribute identifies LeaveOrganizationResultMetadata as the class
    // that carries additional metadata for the LeaveOrganizationResult class.
    [MetadataTypeAttribute(typeof(UserLeaveOrganizationResult.UserLeaveOrganizationResultMetadata))]
    public partial class UserLeaveOrganizationResult
    {

        // This class allows you to attach custom attributes to properties
        // of the LeaveOrganizationResult class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class UserLeaveOrganizationResultMetadata
        {

            // Metadata classes are not meant to be instantiated.
            private UserLeaveOrganizationResultMetadata()
            {

            }

            #region → Properties     .

            [Key]
            public bool CanLeave { get; set; }

            public Guid? AlternativeOwnerID { get; set; }

            public int? OwnersCount { get; set; }

            public string EmailAddress { get; set; }

            public string FirstName { get; set; }

            public string LastName { get; set; }
            
            #endregion Properties

        }
    }

}
