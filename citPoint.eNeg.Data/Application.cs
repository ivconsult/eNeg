
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
    /// Application class client-side extensions
    /// </summary>
    public partial class Application
    {
        #region → Properties     .

        /// <summary>
        /// Gets or sets a value indicating whether this instance has settings link.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance has settings link; otherwise, <c>false</c>.
        /// </value>
        public bool HasSettingsLink { get; set; }
        
        #endregion

        #region → Methods        .

        #region → Public         .
        /// <summary>
        /// Try validate for the Application class
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
            if (propertyName == "ApplicationID"
             || propertyName == "ApplicationTitle"
            )
            {

                ValidationContext context = new ValidationContext(this, null, null) { MemberName = propertyName };
                var validationResults = new Collection<ValidationResult>();
                if (propertyName == "ApplicationID")
                    return Validator.TryValidateProperty(this.ApplicationID, context, validationResults);
                if (propertyName == "ApplicationTitle")
                    return Validator.TryValidateProperty(this.ApplicationTitle, context, validationResults);
            }
            return false;
        }


        #endregion
        #endregion
       
    }

}
