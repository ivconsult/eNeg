#region → Usings   .
using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.ServiceModel.DomainServices.Server;
using System.Data.Objects.DataClasses;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// User class exposes the following data members to the client:
    /// Name, FirstName, LastName, Email, Password, NewPassword,
    /// PasswordQuestion, PasswordAnswer, UserType, IsUserMaintenance
    /// and ProfileResetFlag
    /// </summary>
    public partial class User
    {
        /// <summary>
        /// class that represent Metadata
        /// </summary>
        public sealed class UserMetadata
        {
            #region → Properties     .

            /// <summary>
            /// Represent the Account Type of the user
            /// </summary>
            public AccountType AccountType { get; set; }

            /// <summary>
            /// Represent the Account Type ID of the user
            /// </summary>
            public Nullable<Guid> AccountTypeID { get; set; }

            /// <summary>
            /// Represent the Address of the user
            /// </summary>
            public string Address { get; set; }

            /// <summary>
            /// Represent the Answer Hash of the user
            /// </summary>
            public string AnswerHash { get; set; }

            /// <summary>
            /// Represent the Answer Salt of the user
            /// </summary>
            public string AnswerSalt { get; set; }

            /// <summary>
            /// Represent the City of the user
            /// </summary>
            public string City { get; set; }

            /// <summary>
            /// Represent the Company Name of the user
            /// </summary>
            public string CompanyName { get; set; }

            /// <summary>
            /// Represent the Country of the user
            /// </summary>
            public Country Country { get; set; }

            /// <summary>
            /// Represent the CountryID of the user
            /// </summary>
            public Nullable<Guid> CountryID { get; set; }

            /// <summary>
            /// Represent the Registeration Date of the user
            /// </summary>
            public DateTime CreateDate { get; set; }

            /// <summary>
            /// Represent whether the user is disabled or not
            /// </summary>
            public bool Disabled { get; set; }

            /// <summary>
            /// Represent the EmailAddress of the user
            /// </summary>
            [Required(ErrorMessageResourceName = "ValidationErrorRequiredField", ErrorMessageResourceType = typeof(ErrorResources))]
            [CustomValidation(typeof(UserRules), "IsValidEmail")]
            [Display(Name = "E-mail")]
            public string EmailAddress { get; set; }

            /// <summary>
            /// Represent the First Name of the user
            /// </summary>
                        [Display(Name = "First Name")]
            public string FirstName { get; set; }

            /// <summary>
            /// Represent the Gender of the user
            /// </summary>
            public Nullable<bool> Gender { get; set; }

            /// <summary>
            /// Represent the IPAddress of the user
            /// </summary>
            public string IPAddress { get; set; }

            /// <summary>
            /// Represent whether IsUserMiantenance or not
            /// </summary>
            public bool IsUserMaintenance { get; set; }

            /// <summary>
            /// Represent the Last Login Date of the user
            /// </summary>
            public Nullable<DateTime> LastLoginDate { get; set; }

            /// <summary>
            /// Represent the Las tName of the user
            /// </summary>
            [Display(Name = "Last Name")]
            public string LastName { get; set; }

            /// <summary>
            /// Represent the LCID of the user
            /// </summary>
            public Nullable<int> LCID { get; set; }

            /// <summary>
            /// Represent whether the user is Locked or not
            /// </summary>
            public bool Locked { get; set; }

            /// <summary>
            /// Represent the date that the user has been locked
            /// </summary>
            public Nullable<DateTime> LockedDate { get; set; }

            /// <summary>
            /// Represent the Mobile Number of the user
            /// </summary>
            public string Mobile { get; set; }

            /// <summary>
            /// Represent whether the user is online or not
            /// </summary>
            public bool Online { get; set; }


            /// <summary>
            /// Represent the Password Hash of the user
            /// </summary>
            [Exclude]
            public string PasswordHash { get; set; }

            /// <summary>
            /// Represent the Password Salt of the user
            /// </summary>
            [Exclude]
            public string PasswordSalt { get; set; }

            /// <summary>
            /// Represent the Phone Number of the user
            /// </summary>
            public string Phone { get; set; }

            /// <summary>
            /// Represent the user requested reset or not
            /// </summary>
            public bool Reset { get; set; }

            /// <summary>
            /// Represent the Prefered Language choosen with that user
            /// </summary>
            public PreferedLanguage PreferedLanguage { get; set; }

            /// <summary>
            /// Represent the Profile of the user
            /// </summary>
            public EntityCollection<Profile> Profile { get; set; }

            /// <summary>
            /// Represent the Security Question choosen with that user
            /// </summary>
            public SecurityQuestion SecurityQuestion { get; set; }

            /// <summary>
            /// Represent the Security Question ID choosen with that user
            /// </summary>
            public Nullable<Guid> SecurityQuestionID { get; set; }

            /// <summary>
            /// Represent the User ID of the user
            /// </summary>
            public Guid UserID { get; set; }



            /// <summary>
            /// Gets or sets the culture ID.
            /// </summary>
            /// <value>The culture ID.</value>
            public int? CultureID { get; set; }

            /// <summary>
            /// Gets or sets a value indicating whether this instance has public profile.
            /// </summary>
            /// <value>
            /// 	<c>true</c> if this instance has public profile; otherwise, <c>false</c>.
            /// </value>
            public bool HasPublicProfile { get; set; }


            /// <summary>
            /// Represent the UserRole of the user
            /// </summary>
            public EntityCollection<UserRole> UserRole { get; set; }
            #endregion


            #region → Constructors   .
            /// <summary>
            /// Default Constructor
            /// Metadata classes are not meant to be instantiated.
            /// </summary>
            private UserMetadata()
            {
            }

            #endregion
        }

        #region → Properties     .

        /// <summary>
        /// Represent the Password of the user
        /// </summary>
        [DataMember]
        [Required(ErrorMessageResourceName = "ValidationErrorRequiredField", ErrorMessageResourceType = typeof(ErrorResources))]
        [CustomValidation(typeof(UserRules), "CheckPasswordMinLenght")]
        [CustomValidation(typeof(UserRules), "CheckPasswordMaxLenght")]
        [CustomValidation(typeof(UserRules), "CheckPasswordWithConfirmation")]
        public string Password { get; set; }

        /// <summary>
        /// Represent the Password Confirmation of the user
        /// </summary>
        [DataMember]
        [Required(ErrorMessageResourceName = "ValidationErrorRequiredField", ErrorMessageResourceType = typeof(ErrorResources))]
        [CustomValidation(typeof(UserRules), "CheckPasswordMinLenght")]
        [CustomValidation(typeof(UserRules), "CheckPasswordMaxLenght")]
        [CustomValidation(typeof(UserRules), "CheckPasswordConfirmation")]
        [Display(Name = "Password Confirmation")]
        public string PasswordConfirmation { get; set; }

        /// <summary>
        /// Represent whether the user IsMale or Not
        /// </summary>
        [DataMember]
        public Nullable<global::System.Boolean> IsMale
        {
            get { return !this.Gender; }
            set
            {
                this.Gender = !value;
            }
        }

        /// <summary>
        /// Represent whether the user IsFemale or Not
        /// </summary>
        [DataMember]
        public Nullable<global::System.Boolean> IsFemale
        {
            get { return this.Gender; }
            set
            {
                this.Gender = value;
            }
        }

        /// <summary>
        /// Represent the Password Answer of the user
        /// </summary>
        [DataMember]
        public string PasswordAnswer { get; set; }

        /// <summary>
        /// Represent whether IsUserMiantenance or not
        /// </summary>
        [DataMember]
        public bool IsUserMaintenance { get; set; }

        /// <summary>
        /// Represent the ClientAddress of the user
        /// </summary>
        [DataMember]
        public string ClientAddress
        {
            get
            {
                try
                {
                    return System.Web.HttpContext.Current.Request.UserHostAddress;
                }
                catch (Exception)
                {

                }
                return "127.0.0.1";

            }
        }

        /// <summary>
        /// Represent eNegRights of the user
        /// </summary>
        [DataMember]
        public IEnumerable<string> eNegRights { get; set; }



        #region Reset Mail And Password

        /// <summary>
        /// Gets or sets a value indicating whether [check for new password].
        /// </summary>
        /// <value>
        /// 	<c>true</c> if [check for new password]; otherwise, <c>false</c>.
        /// </value>
        [DataMember]
        public bool CheckForNewPassword { get; set; }

        /// <summary>
        /// Represent the Password of the user
        /// </summary>
        [DataMember]
        [CustomValidation(typeof(UserRules), "IsNewPasswordFieldRequired")]
        [CustomValidation(typeof(UserRules), "CheckNewPasswordMinLenght")]
        [CustomValidation(typeof(UserRules), "CheckPasswordMaxLenght")]
        [CustomValidation(typeof(UserRules), "CheckNewPasswordWithNewConfirmation")]
        public string NewPassword { get; set; }

        /// <summary>
        /// Represent the Password Confirmation of the user
        /// </summary>
        [DataMember]
        [CustomValidation(typeof(UserRules), "IsNewPasswordFieldRequired")]
        [CustomValidation(typeof(UserRules), "CheckNewPasswordMinLenght")]
        [CustomValidation(typeof(UserRules), "CheckPasswordMaxLenght")]
        [CustomValidation(typeof(UserRules), "CheckNewPasswordConfirmation")]
        public string NewPasswordConfirmation { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [check for new password].
        /// </summary>
        /// <value>
        /// 	<c>true</c> if [check for new password]; otherwise, <c>false</c>.
        /// </value>
        [DataMember]
        public bool CheckForNewEmailAddress { get; set; }

        /// <summary>
        /// Gets or sets the new email.
        /// </summary>
        /// <value>The new email.</value>
        [DataMember]
        [CustomValidation(typeof(UserRules), "IsNewEmailFieldRequired")]
        [CustomValidation(typeof(UserRules), "IsValidEmail")]
        [CustomValidation(typeof(UserRules), "CheckNewEmailWithConfirmation")]
        public string NewEmail { get; set; }

        /// <summary>
        /// Gets or sets the new email confirmation.
        /// </summary>
        /// <value>The new email confirmation.</value>
        [DataMember]
        [CustomValidation(typeof(UserRules), "IsNewEmailFieldRequired")]
        [CustomValidation(typeof(UserRules), "IsValidEmail")]
        [CustomValidation(typeof(UserRules), "CheckNewEmailConfirmation")]
        public string NewEmailConfirmation { get; set; }

        #endregion Reset Mail And Password



        /// <summary>
        /// Gets or sets the e neg roles.
        /// </summary>
        /// <value>The e neg roles.</value>
        [DataMember]
        public IEnumerable<string> eNegRoles { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is selected.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is selected; otherwise, <c>false</c>.
        /// </value>
        [DataMember]
        public bool IsSelected { get; set; }
        #endregion

    }
}




