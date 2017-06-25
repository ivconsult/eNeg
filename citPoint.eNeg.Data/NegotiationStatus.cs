
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
    /// NegotiationStatu class client-side extensions
    /// </summary>
    public partial class NegotiationStatu
    {

        #region → Methods        .

        #region → Public         .
/// <summary>
        /// Try validate for the NegotiationStatu class
        /// </summary>
        /// <returns>bool</returns>
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
        /// validates poperty according to criteria
        /// </summary>
        /// <param name="propertyName">propertyName</param>
        /// <returns>bool</returns>
        public bool TryValidateProperty(string propertyName)
        {
            if (string.IsNullOrEmpty(propertyName))
            {
                throw new ArgumentNullException("propertyName");
            }
            if (propertyName == "StatusID"
             || propertyName == "StatusDescription"
            )
            {

                ValidationContext context = new ValidationContext(this, null, null) { MemberName = propertyName };
                var validationResults = new Collection<ValidationResult>();
                if (propertyName == "StatusID")
                    return Validator.TryValidateProperty(this.StatusID, context, validationResults);
                if (propertyName == "StatusDescription")
                    return Validator.TryValidateProperty(this.StatusDescription, context, validationResults);
            }
            return false;
        }

        #endregion
        #endregion
        

    }

}
