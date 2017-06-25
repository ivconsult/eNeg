
#region → Usings   .
using System;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using citPOINT.eNeg.Apps.Common.Interfaces;
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
    /// LoginUser class client-side extensions
    /// </summary>
    public partial class LoginUser : IUserInfo
    {
        #region → Fields         .

        #endregion

        #region → Properties     .

        /// <summary>
        /// Store the the value of "Keep Me Signed in"
        /// </summary>
        public bool IsPersistent { get; set; }

        /// <summary>
        /// Gets the display name.
        /// </summary>
        /// <value>The display name.</value>
        public string DisplayName
        {
            get
            {
                if (this._emailAddress == null)
                    return " ";
                else
                    return this._emailAddress + " (" + this._firstName + " " + this._lastName + ")";
            }
        }

        /// <summary>
        /// Gets or sets the full name.
        /// </summary>
        /// <value>The full name.</value>
        public string FullName
        {
            get
            {
                if (string.IsNullOrEmpty(this.FirstName) || string.IsNullOrEmpty(this.LastName))
                {
                    return this.EmailAddress;
                }
                else
                {
                    return this.FirstName + " " + this.LastName;
                }
            }
        }
        #endregion

        #region → Methods        .

        #region → Public         .

        /// <summary>
        /// Try validate for the Issue class
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
        /// Try validate the specified property for LoginUser class
        /// </summary>
        /// <param name="propertyName">propertyName</param>
        /// <returns>true if property valid</returns>
        public bool TryValidateProperty(string propertyName)
        {
            if (string.IsNullOrEmpty(propertyName))
            {
                throw new ArgumentNullException("propertyName");
            }

            if (propertyName == "EmailAddress" || propertyName == "Password"
                || propertyName == "IsPersistent" || propertyName == "Roles" || propertyName == "PasswordConfirmation")
            {
                ValidationContext context = new ValidationContext(this, null, null) { MemberName = propertyName };

                var validationResults = new Collection<ValidationResult>();

                if (propertyName == "EmailAddress")
                    return Validator.TryValidateProperty(this.EmailAddress, context, validationResults);
                else if (propertyName == "Password")
                    return Validator.TryValidateProperty(this.Password, context, validationResults);
                else if (propertyName == "PasswordConfirmation")
                    return Validator.TryValidateProperty(this.Password, context, validationResults);
                else if (propertyName == "IsPersistent")
                    return Validator.TryValidateProperty(this.IsPersistent, context, validationResults);
            }
            return false;
        }

        #endregion

        #endregion


        
    }
}
