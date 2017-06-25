
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
    /// Channel class client-side extensions
    /// </summary>
    public partial class Channel
    {
        #region → Methods        .

        #region → Public         .

        /// <summary>
        /// Try validate for the Channel class
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
        /// Validates property according to criteria
        /// </summary>
        /// <param name="propertyName">propertyName</param>
        /// <returns>bool</returns>
        public bool TryValidateProperty(string propertyName)
        {
            if (string.IsNullOrEmpty(propertyName))
            {
                throw new ArgumentNullException("propertyName");
            }
            if (propertyName == "ChannelID"
             || propertyName == "ChannelName"
             || propertyName == "ChannelTypeID"
            )
            {

                ValidationContext context = new ValidationContext(this, null, null) { MemberName = propertyName };
                var validationResults = new Collection<ValidationResult>();
                if (propertyName == "ChannelID")
                    return Validator.TryValidateProperty(this.ChannelID, context, validationResults);
                if (propertyName == "ChannelName")
                    return Validator.TryValidateProperty(this.ChannelName, context, validationResults);
                if (propertyName == "ChannelTypeID")
                    return Validator.TryValidateProperty(this.ChannelTypeID, context, validationResults);
            }
            return false;
        }
        #endregion
        #endregion

       

    }

}
