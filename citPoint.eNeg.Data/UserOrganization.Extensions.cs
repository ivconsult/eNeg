
#region → Usings   .
using System;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ServiceModel.DomainServices.Client;
#endregion

#region → History  .

/* Date         User            Change
 * 
 *05.09.11     M.Wahab     creation
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
    /// UserOrganization class client-side extensions
    /// </summary>
    public partial class UserOrganization
    {        
        #region → Fields         .

        #endregion
        
        #region → Properties     .

        /// <summary>
        /// Gets or sets a value indicating whether this instance is owner.
        /// </summary>
        /// <value><c>true</c> if this instance is owner; otherwise, <c>false</c>.</value>
        public bool IsOwner
        {
            get
            {
                return (this.MemberStatus == 2  /*Pending Owner*/ || this.MemberStatus == 3 /*Owner*/);
            }
            set
            {
                if (value == true)
                {
                    this.MemberStatus = 2 /*Pending Owner*/;
                }
                else
                {
                    this.MemberStatus = 1 /*Member*/;
                }

                this.RaisePropertyChanged("IsMember");
                this.RaisePropertyChanged("IsOwner");
                this.RaisePropertyChanged("CanChangeMemberStatus");
                this.RaisePropertyChanged("RoleName");
                this.RaisePropertyChanged("UserOwnerForAnotherOrganization");
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is member.
        /// </summary>
        /// <value><c>true</c> if this instance is member; otherwise, <c>false</c>.</value>
        public bool IsMember
        {
            get
            {
                return this.MemberStatus != 0 /*Pending Member*/ ;
            }
            set
            {
                if (value == true)
                {
                    this.MemberStatus = 1 /* Member */;
                }
                else
                {
                    this.MemberStatus = 0 /*Pending Member*/;
                }

                this.RaisePropertyChanged("IsMember");
                this.RaisePropertyChanged("IsOwner");
                this.RaisePropertyChanged("CanChangeMemberStatus");
                this.RaisePropertyChanged("RoleName");
                this.RaisePropertyChanged("UserOwnerForAnotherOrganization");
            }
        }

        /// <summary>
        /// Gets a value indicating whether this instance can change member status.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance can change member status; otherwise, <c>false</c>.
        /// </value>
        public bool CanChangeMemberStatus
        {
            get
            {
                if (this.MemberStatus == 0 /* PendingMember */)
                {
                    return true;
                }

                else if (this.EntityState == EntityState.Modified)
                {
                    if ((this.GetOriginal() as UserOrganization).MemberStatus == 0 /*Pending member*/)
                    {
                        return true;
                    }
                }
                return false;
            }
        }

        /// <summary>
        /// Gets a value indicating whether [user has get status].
        /// </summary>
        /// <value><c>true</c> if [user has get status]; otherwise, <c>false</c>.</value>
        public bool UserHasGetStatus
        {
            get
            {
                return !CanChangeMemberStatus;
            }
        }

        /// <summary>
        /// Gets a value indicating whether [user owner for another organization].
        /// </summary>
        /// <value>
        /// 	<c>true</c> if [user owner for another organization]; otherwise, <c>false</c>.
        /// </value>
        public bool UserOwnerForAnotherOrganization
        {
            get
            {
                return !CanUserOwnOrganization;
            }
        }

        /// <summary>
        /// Gets a value indicating whether this instance can user own organization.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance can user own organization; otherwise, <c>false</c>.
        /// </value>
        public bool CanUserOwnOrganization
        {
            get
            {
                return !(this.User != null && this.User.UserOrganizations.Count > 1);
            }
        }
        
        /// <summary>
        /// Gets the name of the role.
        /// </summary>
        /// <value>The name of the role.</value>
        public string RoleName
        {
            get
            {
                switch (this.MemberStatus)
                {
                    case 0:
                        return "Pending Member";
                    case 1:
                        return "Member";
                    case 2:
                        return "Pending Owner";
                    case 3:
                        return "Owner";
                    default:
                        return "-----";
                }
            }
        }
        #endregion

        #region → Methods        .

        /// <summary>
        /// Try validate for the UserOrganization class
        /// </summary>
        /// <returns>True Or False </returns>
        public bool TryValidateObject()
        {
            ValidationContext context = new ValidationContext(this, null, null);
            var validationResults = new Collection<ValidationResult>();

            if (Validator.TryValidateObject(this, context, validationResults, false) == false)
            {
                foreach (ValidationResult error in validationResults)
                {
                    this.ValidationErrors.Add(error);
                }
                return false;
            }
            return true;
        }

        /// <summary>    
        /// Try Try Validate by Property name  
        /// </summary> 
        /// <returns>True Or False </returns> 
        public bool TryValidateProperty(string propertyName)
        {
            if (string.IsNullOrEmpty(propertyName))
            {
                throw new ArgumentNullException("propertyName");
            }
            if (propertyName == "UserOrganizationID"
             || propertyName == "UserID"
             || propertyName == "OrganizationID"
             || propertyName == "MemberStatus"
             || propertyName == "Deleted"
             || propertyName == "DeletedBy"
             || propertyName == "DeletedOn"
            )
            {
                ValidationContext context = new ValidationContext(this, null, null) { MemberName = propertyName };
                var validationResults = new Collection<ValidationResult>();
                if (propertyName == "UserOrganizationID")
                    return Validator.TryValidateProperty(this.UserOrganizationID, context, validationResults);
                if (propertyName == "UserID")
                    return Validator.TryValidateProperty(this.UserID, context, validationResults);
                if (propertyName == "OrganizationID")
                    return Validator.TryValidateProperty(this.OrganizationID, context, validationResults);
                if (propertyName == "MemberStatus")
                    return Validator.TryValidateProperty(this.MemberStatus, context, validationResults);
                if (propertyName == "Deleted")
                    return Validator.TryValidateProperty(this.Deleted, context, validationResults);
                if (propertyName == "DeletedBy")
                    return Validator.TryValidateProperty(this.DeletedBy, context, validationResults);
                if (propertyName == "DeletedOn")
                    return Validator.TryValidateProperty(this.DeletedOn, context, validationResults);
            }
            return false;
        }

        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns>return new Instance of UserOrganization</returns>
        public UserOrganization Clone()
        {
            UserOrganization mUserOrganization = new UserOrganization
                                        {
                                            UserOrganizationID = this.UserOrganizationID,
                                            UserID = this.UserID,
                                            OrganizationID = this.OrganizationID,
                                            MemberStatus = this.MemberStatus,
                                            Deleted = this.Deleted,
                                            DeletedBy = this.DeletedBy,
                                            DeletedOn = this.DeletedOn,


                                        };

            return mUserOrganization;
        }
        #endregion Methods
    }

}
