#region → Usings   .
using System;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
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
    /// User class client-side extensions
    /// </summary>
    public partial class User : IUserInfo
    {

        #region → Properties     .

        /// <summary>
        /// Returns each user in the format of "user (FirstName LastName)"
        /// </summary>
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

        /// <summary>
        /// Gets a value indicating whether this instance is not owner.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is not owner; otherwise, <c>false</c>.
        /// </value>
        public bool IsNotOwner
        {
            get
            {
                return this.UserOrganizations.Where(s => s.MemberStatus == 3/*Owner*/).Count() == 0;
            }
        }


        /// <summary>
        /// Gets a value indicating whether this instance is owner.
        /// </summary>
        /// <value><c>true</c> if this instance is owner; otherwise, <c>false</c>.</value>
        public bool IsOwner
        {
            get
            {
                return !this.IsNotOwner;
            }
        }
        #endregion

        #region → Methods        .

        #region → Public         .
        /// <summary>
        /// Try validate for the Issue class
        /// </summary>
        /// <returns>true if property valid</returns>
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
        /// Try validate for the Issue class
        /// </summary>
        /// <returns>true if property valid</returns>
        public bool TryValidateEmailRepeating()
        {

            return true;
        }


        /// <summary>
        /// Try validate the specified property for User class
        /// </summary>
        /// <param name="propertyName">propertyName</param>
        /// <returns>true if property valid</returns>
        public bool TryValidateProperty(string propertyName)
        {
            if (string.IsNullOrEmpty(propertyName))
            {
                throw new ArgumentNullException("propertyName");
            }
            if (propertyName == "UserID"
             || propertyName == "EmailAddress"
             || propertyName == "Password"
             || propertyName == "PasswordConfirmation"
             || propertyName == "NewPassword"
             || propertyName == "NewPasswordConfirmation"
             || propertyName == "NewEmail"
             || propertyName == "NewEmailConfirmation"
             || propertyName == "Locked"
             || propertyName == "LockedDate"
             || propertyName == "LastLoginDate"
             || propertyName == "CreateDate"
             || propertyName == "AccountTypeID"
             || propertyName == "IPAddress"
             || propertyName == "SecurityQuestionID"
             || propertyName == "AnswerHash"
             || propertyName == "AnswerSalt"
             || propertyName == "Online"
             || propertyName == "Disabled"
             || propertyName == "FirstName"
             || propertyName == "LastName"
             || propertyName == "CompanyName"
             || propertyName == "LCID"
             || propertyName == "Address"
             || propertyName == "City"
             || propertyName == "CountryID"
             || propertyName == "Phone"
             || propertyName == "Mobile"
             || propertyName == "Gender"
            )
            {

                ValidationContext context = new ValidationContext(this, null, null) { MemberName = propertyName };
                var validationResults = new Collection<ValidationResult>();




                if (propertyName == "UserID")
                    return Validator.TryValidateProperty(this.UserID, context, validationResults);
                if (propertyName == "EmailAddress")
                    return Validator.TryValidateProperty(this.EmailAddress, context, validationResults);
                if (propertyName == "Password")
                    return Validator.TryValidateProperty(this.Password, context, validationResults);
                if (propertyName == "PasswordConfirmation")
                    return Validator.TryValidateProperty(this.PasswordConfirmation, context, validationResults);
                if (propertyName == "NewPassword")
                    return Validator.TryValidateProperty(this.NewPassword, context, validationResults);
                if (propertyName == "NewPasswordConfirmation")
                    return Validator.TryValidateProperty(this.NewPasswordConfirmation, context, validationResults);
                if (propertyName == "NewEmail")
                    return Validator.TryValidateProperty(this.NewEmail, context, validationResults);
                if (propertyName == "NewEmailConfirmation")
                    return Validator.TryValidateProperty(this.NewEmailConfirmation, context, validationResults);
                if (propertyName == "Locked")
                    return Validator.TryValidateProperty(this.Locked, context, validationResults);
                if (propertyName == "LockedDate")
                    return Validator.TryValidateProperty(this.LockedDate, context, validationResults);
                if (propertyName == "LastLoginDate")
                    return Validator.TryValidateProperty(this.LastLoginDate, context, validationResults);
                if (propertyName == "CreateDate")
                    return Validator.TryValidateProperty(this.CreateDate, context, validationResults);
                if (propertyName == "AccountTypeID")
                    return Validator.TryValidateProperty(this.AccountTypeID, context, validationResults);
                if (propertyName == "IPAddress")
                    return Validator.TryValidateProperty(this.IPAddress, context, validationResults);
                if (propertyName == "SecurityQuestionID")
                    return Validator.TryValidateProperty(this.SecurityQuestionID, context, validationResults);
                if (propertyName == "AnswerHash")
                    return Validator.TryValidateProperty(this.AnswerHash, context, validationResults);
                if (propertyName == "AnswerSalt")
                    return Validator.TryValidateProperty(this.AnswerSalt, context, validationResults);
                if (propertyName == "Online")
                    return Validator.TryValidateProperty(this.Online, context, validationResults);
                if (propertyName == "Disabled")
                    return Validator.TryValidateProperty(this.Disabled, context, validationResults);
                if (propertyName == "FirstName")
                    return Validator.TryValidateProperty(this.FirstName, context, validationResults);
                if (propertyName == "LastName")
                    return Validator.TryValidateProperty(this.LastName, context, validationResults);
                if (propertyName == "CompanyName")
                    return Validator.TryValidateProperty(this.CompanyName, context, validationResults);
                if (propertyName == "LCID")
                    return Validator.TryValidateProperty(this.LCID, context, validationResults);
                if (propertyName == "Address")
                    return Validator.TryValidateProperty(this.Address, context, validationResults);
                if (propertyName == "City")
                    return Validator.TryValidateProperty(this.City, context, validationResults);
                if (propertyName == "CountryID")
                    return Validator.TryValidateProperty(this.CountryID, context, validationResults);
                if (propertyName == "Phone")
                    return Validator.TryValidateProperty(this.Phone, context, validationResults);
                if (propertyName == "Mobile")
                    return Validator.TryValidateProperty(this.Mobile, context, validationResults);
                if (propertyName == "Gender")
                    return Validator.TryValidateProperty(this.Gender, context, validationResults);
            }



            return false;
        }

        #endregion

        #endregion

                
    }
}