
#region → Usings   .
using System;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
#endregion

#region → History  .

/* Date         User            Change
 * 
 *15.08.11     M.Wahab     creation
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
    /// Organization class client-side extensions
    /// </summary>
    public partial class Organization
    {

        #region → Properties     .

        /// <summary>
        /// Gets or sets a value indicating whether this instance can item publish.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance can item publish; otherwise, <c>false</c>.
        /// </value>
        public bool CanItemPublish { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is selected.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is selected; otherwise, <c>false</c>.
        /// </value>
        public bool IsSelected { get; set; }


        #endregion

        #region → Methods        .

        /// <summary>
        /// Try validate for the Organization class
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
            if (propertyName == "OrganizationID"
             || propertyName == "OrganizationName"
             || propertyName == "OrganizationTypeID"
             || propertyName == "Deleted"
             || propertyName == "DeletedBy"
             || propertyName == "DeletedOn"
            )
            {

                ValidationContext context = new ValidationContext(this, null, null) { MemberName = propertyName };
                var validationResults = new Collection<ValidationResult>();
                if (propertyName == "OrganizationID")
                    return Validator.TryValidateProperty(this.OrganizationID, context, validationResults);
                if (propertyName == "OrganizationName")
                    return Validator.TryValidateProperty(this.OrganizationName, context, validationResults);
                if (propertyName == "OrganizationTypeID")
                    return Validator.TryValidateProperty(this.OrganizationTypeID, context, validationResults);
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
        /// <returns>return new Instance of Organization</returns>
        public Organization Clone()
        {
            Organization mOrganization = new Organization
                                        {
                                            OrganizationID = this.OrganizationID,
                                            OrganizationName = this.OrganizationName,
                                            OrganizationTypeID = this.OrganizationTypeID,
                                            Deleted = this.Deleted,
                                            DeletedBy = this.DeletedBy,
                                            DeletedOn = this.DeletedOn,


                                        };

            return mOrganization;
        }
        
        #endregion Methods

    }

}
