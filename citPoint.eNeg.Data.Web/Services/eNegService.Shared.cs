#region → Usings   .
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
#endregion

#region → History  .

/* Date         User              Change
 * 
 * 02.08.10     M Wahab       Creation
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
    /// User Rules
    /// </summary>
    public static partial class UserRules
    {

        #region → Methods        .

        #region → Private        .

        /// <summary>
        /// Isvalids the email.
        /// </summary>
        /// <param name="EmailAddress">The email address.</param>
        /// <returns></returns>
        private static bool IsValidEmail(string EmailAddress)
        {
            return Regex.IsMatch(EmailAddress,
                 @"^(?("")("".+?""@)|(([0-9a-zA-Z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-zA-Z])@))" +
                 @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,6}))$");
        }

        #endregion Private

        #region → Public         .



        /// <summary>
        /// Determines whether [is valid email] [the specified email address].
        /// </summary>
        /// <param name="EmailAddress">The email address.</param>
        /// <param name="validationContext">The validation context.</param>
        /// <returns></returns>
        public static ValidationResult IsValidEmail(string EmailAddress, ValidationContext validationContext)
        {
            // user Email can be null
            if (string.IsNullOrEmpty(EmailAddress))
                return ValidationResult.Success;

            // Return true if strIn is in valid e-mail format.
            if (IsValidEmail(EmailAddress))
                return ValidationResult.Success;
            else
            {
                return new ValidationResult(ErrorResources.ValidationErrorInvalidEmail, new string[] { validationContext.DisplayName });
            }
        }

        /// <summary>
        /// Custom validation of whether   password and confirm password match
        /// </summary>
        /// <param name="PasswordConfirmation">Value Of PasswordConfirmation</param>
        /// <param name="validationContext">Value Of validationContext</param>
        /// <returns>success if Password matches confirm password</returns>
        public static ValidationResult CheckPasswordConfirmation(string PasswordConfirmation, ValidationContext validationContext)
        {
            if (!((validationContext.ObjectInstance as User) == null))
            {
                User currentUser = (User)validationContext.ObjectInstance;

                if (!string.IsNullOrEmpty(currentUser.Password) &&
                    !string.IsNullOrEmpty(PasswordConfirmation) &&
                     PasswordConfirmation.Length >= 6 &&
                    PasswordConfirmation.Length <= 50 &&
                    currentUser.Password.Length >= 6 &&
                    currentUser.Password.Length <= 50 &&
                    currentUser.Password != PasswordConfirmation)
                {
                    return new ValidationResult(ErrorResources.ValidationErrorPasswordConfirmationMismatch, new string[] { "PasswordConfirmation" });
                }
            }
            return ValidationResult.Success;
        }

        /// <summary>
        /// Custom validation of whether   password and confirm password match
        /// </summary>
        /// <param name="PasswordConfirmation">Value Of PasswordConfirmation</param>
        /// <param name="validationContext">Value Of validationContext</param>
        /// <returns>success if Password matches confirm password</returns>
        public static ValidationResult CheckPasswordWithConfirmation(string Password, ValidationContext validationContext)
        {
            if (!((validationContext.ObjectInstance as User) == null))
            {
                User currentUser = (User)validationContext.ObjectInstance;

                if (!string.IsNullOrEmpty(currentUser.PasswordConfirmation) &&
                    !string.IsNullOrEmpty(Password) &&

                    Password.Length >= 6 &&
                    Password.Length <= 50 &&
                    currentUser.PasswordConfirmation.Length >= 6 &&
                    currentUser.PasswordConfirmation.Length <= 50 &&

                    currentUser.PasswordConfirmation != Password)
                {
                    return new ValidationResult(ErrorResources.ValidationErrorPasswordConfirmationMismatch, new string[] { "PasswordConfirmation" });
                }

                else
                {
                    if (!string.IsNullOrEmpty(currentUser.PasswordConfirmation) &&
                        !string.IsNullOrEmpty(Password) &&
                        !string.IsNullOrEmpty(currentUser.Password) &&
                        Password.Length >= 6 &&
                        Password.Length <= 50 &&
                        currentUser.PasswordConfirmation.Length >= 6 &&
                        currentUser.PasswordConfirmation.Length <= 50 &&
                        currentUser.Password != Password)
                    {
                        currentUser.PasswordConfirmation = "";
                    }
                }
            }

            return ValidationResult.Success;
        }

        /// <summary>
        /// Custom validation of whether password is more Than 50 Charatcers
        /// </summary>
        /// <param name="Password">Value Of Password</param>
        /// <param name="validationContext">Value Of validationContext</param>
        /// <returns>success if Password Is Max 50</returns>
        public static ValidationResult CheckPasswordMaxLenght(string Password, ValidationContext validationContext)
        {
            if (!string.IsNullOrEmpty(Password) && Password.Length > 50)
                return new ValidationResult(ErrorResources.ValidationErrorBadMaxPasswordLength, new string[] { validationContext.DisplayName });

            return ValidationResult.Success;
        }

        /// <summary>
        /// Checks the password min lenght.
        /// </summary>
        /// <param name="Password">The password.</param>
        /// <param name="validationContext">The validation context.</param>
        /// <returns>Success if password is less than 6</returns>
        public static ValidationResult CheckPasswordMinLenght(string Password, ValidationContext validationContext)
        {
            if (!string.IsNullOrEmpty(Password) && Password.Length < 6)
                return new ValidationResult(ErrorResources.ValidationErrorBadPasswordLength, new string[] { validationContext.DisplayName });

            return ValidationResult.Success;
        }

        
        #region New password,New Email Area Check


        #region For New Password Checks

        /// <summary>
        /// Custom validation of whether password is not less than 6 Charatcers
        /// </summary>
        /// <param name="Password">Value Of Password</param>
        /// <param name="validationContext">Value Of validationContext</param>
        /// <returns>success if Password Is not less than 6  Charatcers</returns>
        public static ValidationResult CheckNewPasswordMinLenght(string Password, ValidationContext validationContext)
        {

            if (!((validationContext.ObjectInstance as User) == null))
            {
                User currentUser = (User)validationContext.ObjectInstance;

                if (!string.IsNullOrEmpty(Password) && Password.Length < 6 && currentUser.CheckForNewPassword)
                    return new ValidationResult(ErrorResources.ValidationErrorBadPasswordLength, new string[] { validationContext.DisplayName });

            }

            return ValidationResult.Success;
        }

        /// <summary>
        /// Determines whether [is new password field required] [the specified filed value].
        /// </summary>
        /// <param name="FiledValue">The filed value.</param>
        /// <param name="validationContext">The validation context.</param>
        /// <returns></returns>
        public static ValidationResult IsNewPasswordFieldRequired(string FiledValue, ValidationContext validationContext)
        {

            if (!((validationContext.ObjectInstance as User) == null))
            {
                User currentUser = (User)validationContext.ObjectInstance;

                if ((string.IsNullOrEmpty(FiledValue) || string.IsNullOrWhiteSpace(FiledValue)) && currentUser.CheckForNewPassword)
                    return new ValidationResult(ErrorResources.ValidationErrorRequiredField, new string[] { validationContext.DisplayName });
                
                    
            }


            return ValidationResult.Success;
        }

        /// <summary>
        /// Checks the new password confirmation.
        /// </summary>
        /// <param name="NewPasswordConfirmation">The new password confirmation.</param>
        /// <param name="validationContext">The validation context.</param>
        /// <returns></returns>
        public static ValidationResult CheckNewPasswordConfirmation(string NewPasswordConfirmation, ValidationContext validationContext)
        {
            if (!((validationContext.ObjectInstance as User) == null))
            {
                User currentUser = (User)validationContext.ObjectInstance;

                if (!string.IsNullOrEmpty(currentUser.NewPassword) &&
                    !string.IsNullOrEmpty(NewPasswordConfirmation) &&
                    NewPasswordConfirmation.Length >= 6 &&
                    NewPasswordConfirmation.Length <= 50 &&
                    currentUser.NewPassword.Length >= 6 &&
                    currentUser.NewPassword.Length <= 50 &&
                    currentUser.NewPassword != NewPasswordConfirmation)
                {
                    return new ValidationResult(ErrorResources.ValidationErrorPasswordConfirmationMismatch, new string[] { "NewPasswordConfirmation" });
                }
            }
            return ValidationResult.Success;
        }

        /// <summary>
        /// Checks the new password with new confirmation.
        /// </summary>
        /// <param name="NewPassword">The new password.</param>
        /// <param name="validationContext">The validation context.</param>
        /// <returns></returns>
        public static ValidationResult CheckNewPasswordWithNewConfirmation(string NewPassword, ValidationContext validationContext)
        {
            if (!((validationContext.ObjectInstance as User) == null))
            {
                User currentUser = (User)validationContext.ObjectInstance;

                if (!string.IsNullOrEmpty(currentUser.NewPasswordConfirmation) &&
                    !string.IsNullOrEmpty(NewPassword) &&
                    NewPassword.Length>=6  &&
                    NewPassword.Length <= 50 &&
                    currentUser.NewPasswordConfirmation.Length >= 6 &&
                    currentUser.NewPasswordConfirmation.Length <= 50 &&
                    currentUser.NewPasswordConfirmation != NewPassword)
                {
                    return new ValidationResult(ErrorResources.ValidationErrorPasswordConfirmationMismatch, new string[] { "NewPasswordConfirmation" });
                }

                else
                {
                    if (!string.IsNullOrEmpty(currentUser.NewPasswordConfirmation) &&
                        !string.IsNullOrEmpty(NewPassword) &&
                        !string.IsNullOrEmpty(currentUser.NewPassword) &&
                        currentUser.NewPassword != NewPassword)
                    {
                        currentUser.NewPasswordConfirmation = "";
                    }
                }
            }

            return ValidationResult.Success;
        }

        /// <summary>
        /// Checks the new Mail confirmation.
        /// </summary>
        /// <param name="NewMailConfirmation">The new Mail confirmation.</param>
        /// <param name="validationContext">The validation context.</param>
        /// <returns></returns>
        public static ValidationResult CheckNewMailConfirmation(string NewMailConfirmation, ValidationContext validationContext)
        {
            if (!((validationContext.ObjectInstance as User) == null))
            {
                User currentUser = (User)validationContext.ObjectInstance;

                if (!string.IsNullOrEmpty(currentUser.NewEmail) &&
                    !string.IsNullOrEmpty(NewMailConfirmation) &&
                    currentUser.NewEmail.ToLower() != NewMailConfirmation)
                {
                    return new ValidationResult(ErrorResources.ValidationErrorMailConfirmationMismatch, new string[] { "NewMailConfirmation" });
                }
            }
            return ValidationResult.Success;
        }

        #endregion  For New Password Checks

        #region For New Email Checks
        
        /// <summary>
        /// Determines whether [is new email field required] [the specified filed value].
        /// </summary>
        /// <param name="FiledValue">The filed value.</param>
        /// <param name="validationContext">The validation context.</param>
        /// <returns></returns>
        public static ValidationResult IsNewEmailFieldRequired(string FiledValue, ValidationContext validationContext)
        {

            if (!((validationContext.ObjectInstance as User) == null))
            {
                User currentUser = (User)validationContext.ObjectInstance;
                
                if (currentUser.CheckForNewEmailAddress)
                {
                    if ((string.IsNullOrEmpty(FiledValue) || string.IsNullOrWhiteSpace(FiledValue)))
                        return new ValidationResult(ErrorResources.ValidationErrorRequiredField, new string[] { validationContext.DisplayName });
                    else if (FiledValue.ToLower() == currentUser.EmailAddress.ToLower() && validationContext.DisplayName == "NewEmail")
                        return new ValidationResult(ErrorResources.ValidationErrorYourNewMailLikeYourMail, new string[] { validationContext.DisplayName });
                }

            }


            return ValidationResult.Success;
        }
        
        /// <summary>
        /// Custom validation of whether Email and confirm Email match
        /// </summary>
        /// <param name="NewEmailConfirmation">Value Of EmailConfirmation</param>
        /// <param name="validationContext">Value Of validationContext</param>
        /// <returns>success if Email matches confirm Email</returns>
        public static ValidationResult CheckNewEmailConfirmation(string NewEmailConfirmation, ValidationContext validationContext)
        {
            if (!((validationContext.ObjectInstance as User) == null))
            {
                User currentUser = (User)validationContext.ObjectInstance;

                if (!string.IsNullOrEmpty(currentUser.NewEmail) &&
                    !string.IsNullOrEmpty(NewEmailConfirmation) &&
                    IsValidEmail(NewEmailConfirmation) &&
                    IsValidEmail(currentUser.NewEmail) &&
                    currentUser.NewEmail.ToLower() != NewEmailConfirmation.ToLower())
                {
                    return new ValidationResult(ErrorResources.ValidationErrorMailConfirmationMismatch, new string[] { "NewEmailConfirmation" });
                }
            }
            return ValidationResult.Success;
        }

        /// <summary>
        /// Checks the new email with confirmation.
        /// </summary>
        /// <param name="NewEmail">The new email.</param>
        /// <param name="validationContext">The validation context.</param>
        /// <returns></returns>
        public static ValidationResult CheckNewEmailWithConfirmation(string NewEmail, ValidationContext validationContext)
        {
            if (!((validationContext.ObjectInstance as User) == null))
            {
                User currentUser = (User)validationContext.ObjectInstance;

                if (!string.IsNullOrEmpty(currentUser.NewEmailConfirmation) &&
                    !string.IsNullOrEmpty(NewEmail) &&
                    IsValidEmail(currentUser.NewEmailConfirmation) &&
                    IsValidEmail(NewEmail) &&
                    currentUser.NewEmailConfirmation.ToLower() != NewEmail.ToLower())
                {
                    return new ValidationResult(ErrorResources.ValidationErrorMailConfirmationMismatch, new string[] { "NewEmailConfirmation" });
                }

                else
                {
                    if (!string.IsNullOrEmpty(currentUser.NewEmailConfirmation) &&
                        !string.IsNullOrEmpty(NewEmail) &&
                        !string.IsNullOrEmpty(currentUser.NewEmail) &&
                        IsValidEmail(currentUser.NewEmailConfirmation) &&
                        IsValidEmail(NewEmail) &&
                        currentUser.NewEmail.ToLower() != NewEmail.ToLower())
                    {
                        currentUser.NewEmailConfirmation = "";
                    }
                }
            }

            return ValidationResult.Success;
        }

        
        #endregion For New Email Checks

        #endregion New password,New Email Area Check


        /// <summary>
        /// Checks the message lenght.
        /// </summary>
        /// <param name="messageContent">Content of the message.</param>
        /// <param name="validationContext">The validation context.</param>
        /// <returns></returns>
        public static ValidationResult CheckMessageLenght(string messageContent, ValidationContext validationContext)
        {
            if (!string.IsNullOrEmpty(messageContent) && messageContent.Length < 2)
                return new ValidationResult(ErrorResources.ValidationErrorBadMessageContent, new string[] { validationContext.DisplayName });

            return ValidationResult.Success;
        }
        #endregion
        
        #endregion
    }


}
