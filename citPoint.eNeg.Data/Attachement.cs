
#region → Usings   .
using System;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
#endregion

#region → History  .

/* Date         User            Change
 * 
 *16.08.10     M.Wahab     creation
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
    /// Attachement class client-side extensions
    /// </summary>
    public partial class Attachement
    {
        #region → Methods        .

        #region → Public         .

    /// <summary>
        /// Try validate for the Attachement class
        /// </summary>
        /// <returns>true if object valid</returns>
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
        /// Tries the validate property.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        /// <returns></returns>
        public bool TryValidateProperty(string propertyName)
        {
            if (string.IsNullOrEmpty(propertyName))
            {
                throw new ArgumentNullException("propertyName");
            }
            if (propertyName == "AttachementID"
             || propertyName == "AttachementName"
             || propertyName == "AttachementContent"
             || propertyName == "MessageID"
            )
            {

                ValidationContext context = new ValidationContext(this, null, null) { MemberName = propertyName };
                var validationResults = new Collection<ValidationResult>();
                if (propertyName == "AttachementID")
                    return Validator.TryValidateProperty(this.AttachementID, context, validationResults);
                if (propertyName == "AttachementName")
                    return Validator.TryValidateProperty(this.AttachementName, context, validationResults);
                if (propertyName == "AttachementContent")
                    return Validator.TryValidateProperty(this.AttachementContent, context, validationResults);
                if (propertyName == "MessageID")
                    return Validator.TryValidateProperty(this.MessageID, context, validationResults);
            }
            return false;
        }
        #endregion
        #endregion

        

    }

}
