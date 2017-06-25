#region → Usings   .
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ServiceModel.DomainServices.Server.ApplicationServices;
using System.Runtime.Serialization;
using System.ComponentModel;
using System;
#endregion

#region → History  .

/* Date         User            Change
 * 
 * 02.08.10     M.Wahab     creation
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
    /// LoginUser class derives from User class and implements IUser interface,
    /// it only exposes the following three data members to the client:
    /// Name, Password, ProfileResetFlag, and Roles
    /// </summary>
    [DataContractAttribute(IsReference = true)]
    [MetadataTypeAttribute(typeof(User.UserMetadata))]
    public sealed class LoginUser : User, IUser
    {
        #region → Properties     .

        /// <summary>
        /// Represent the Roles assigned to current User
        /// </summary>
        [DataMember]
        public IEnumerable<string> Roles
        {
            get
            {
                return new List<string>();
            }
            set
            {
            }
        }

        /// <summary>
        /// Represent the name of the current User
        /// </summary>
        [Key]
        [DataMember]
        public string Name
        {
            get
            {
                return this.EmailAddress;
            }
            set
            {
                this.EmailAddress = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance has activation mail.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance has activation mail; otherwise, <c>false</c>.
        /// </value>
        [DataMember]
        [DefaultValue(true)]
        public bool HasActivationMail { get; set; }
        
        /// <summary>
        /// Gets or sets the operation string URL.
        /// </summary>
        /// <value>The operation string URL.</value>
        [DataMember]
        public string OperationStringUrl { get; set; }
                
        /// <summary>
        /// Gets or sets a value indicating whether this instance is organization owner.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is organization owner; otherwise, <c>false</c>.
        /// </value>
        [DataMember]
        [DefaultValue(false)]
        public bool IsOrganizationOwner { get; set; }

        /// <summary>
        /// Gets or sets the organization owned ID.
        /// </summary>
        /// <value>The organization owned ID.</value>
        [DataMember]
        public Guid OrganizationOwnedID { get; set; }
        

        #endregion
    }
}
