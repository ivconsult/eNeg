#region → Usings   .
using citPOINT.eNeg.Data.Web;
using System;
using System.ComponentModel;
using System.ServiceModel.DomainServices.Client;

#endregion

#region → History  .

/* Date         User
 * 
 * 02.08.10     Mohamed Abdulwahab
 */

# endregion

#region → ToDos    .

/*
 * Date         set by User     Description
 * 
 * 

*/

# endregion

namespace citPOINT.eNeg.Common
{
    /// <summary>
    /// Used as A public Inteface For Registeration Model
    /// </summary>
    public interface IUserRegisterModel
    {

        #region → Properties     .
        /// <summary>
        /// Any Data in current Cintext has changed 
        /// e.g Added,Modified,or Deleted.
        /// </summary>
        bool HasChanges { get; }
        /// <summary>
        /// is Current Context is Busy in Loading Data or Submitting.
        /// </summary>
        bool IsBusy { get; }

        #endregion

        #region → Events         .

        /// <summary>
        /// Occurs when [get all application complete].
        /// </summary>
        event EventHandler<eNegEntityResultArgs<Application>> GetAllApplicationComplete;

        /// <summary>
        /// Occurs when [get culture complete].
        /// </summary>
        event EventHandler<eNegEntityResultArgs<Culture>> GetCultureComplete;

        /// <summary>
        /// Event Handler For Method GetAccountTypeAsync
        /// </summary>
        event EventHandler<eNegEntityResultArgs<AccountType>> GetAccountTypeComplete;
        /// <summary>
        /// Event Handler For Method GetCountryAsync
        /// </summary>
        event EventHandler<eNegEntityResultArgs<Country>> GetCountryComplete;
        /// <summary>
        /// Event Handler For Method GetPreferedLanguageAsync
        /// </summary>
        event EventHandler<eNegEntityResultArgs<PreferedLanguage>> GetPreferedLanguageComplete;
        /// <summary>
        /// Event Handler For Method GetProfileAsync
        /// </summary>
        event EventHandler<eNegEntityResultArgs<Profile>> GetProfileComplete;
        /// <summary>
        /// Event Handler For Method GetRightAsync
        /// </summary>
        event EventHandler<eNegEntityResultArgs<Right>> GetRightComplete;
        /// <summary>
        /// Event Handler For Method GeetRoleAsync
        /// </summary>
        event EventHandler<eNegEntityResultArgs<Role>> GetRoleComplete;
        /// <summary>
        /// Event Handler For Method GetRoleRightAsync
        /// </summary>
        event EventHandler<eNegEntityResultArgs<RoleRight>> GetRoleRightComplete;
        /// <summary>
        /// Event Handler For Method GetSecurityQuestionAsync
        /// </summary>
        event EventHandler<eNegEntityResultArgs<SecurityQuestion>> GetSecurityQuestionComplete;
        /// <summary>
        /// Event Handler For Method GetUserRoleAsync
        /// </summary>
        event EventHandler<eNegEntityResultArgs<UserRole>> GetUserRoleComplete;
        /// <summary>
        /// Event Handler For Method CheckIsEmailExistAsync
        /// Check Mail repeating
        /// </summary>
        event EventHandler<eNegEntityResultArgs<User>> GetCheckIsEmailExistComplete;
        /// <summary>
        /// Event Handler For Method DeleteUserOperationByUserID
        /// </summary>
        event EventHandler<eNegEntityResultArgs<User>> DeleteUserOperationByUserIDComplete;
        /// <summary>
        /// Event Handler For Method SendingMail
        /// </summary>
        event Action<InvokeOperation> SendingMailCompleted;
        /// <summary>
        /// Event Handler For Method SaveChanges
        /// </summary>
        event EventHandler<SubmitOperationEventArgs> SaveChangesComplete;
        /// <summary>
        /// Event Handler For Method PropertyChanged
        /// </summary>
        event PropertyChangedEventHandler PropertyChanged;

        #region For User Reset
        /// <summary>
        /// Event Handler For Method GetUserByID
        /// </summary>
        event EventHandler<eNegEntityResultArgs<User>> GetUserByIDComplete;
        /// <summary>
        /// Event Handler For Method CheckUserRequestReset
        /// this Method is To Check if the Current Link is valid or not
        /// </summary>
        event EventHandler<eNegEntityResultArgs<User>> GetCheckUserRequestResetComplete;
        /// <summary>
        /// Event Handler For Method UpdateReset
        /// Update Reset Flag So This User Can Enter Direct 
        /// to Reset Page In The Next Time Open The Project
        /// </summary>
        event EventHandler<eNegEntityResultArgs<User>> GetResetRequestComplete;
        #endregion



        #endregion

        #region → Methods        .
        #region → Public         .

        /// <summary>
        /// Loading All Records of AccountType
        /// </summary>
        void GetAccountTypeAsync();
        /// <summary>
        /// Loading All Records of Country
        /// </summary>
        void GetCountryAsync();
        /// <summary>
        /// Loading All Records of PreferedLanguage
        /// </summary>
        void GetPreferedLanguageAsync();
        /// <summary>
        /// Loading All Records of Profile
        /// </summary>
        void GetProfileAsync();
        /// <summary>
        /// Loading All Records of Right
        /// </summary>
        void GetRightAsync();
        /// <summary>
        /// Loading All Records of Role
        /// </summary>
        void GetRoleAsync();
        /// <summary>
        /// Loading All Records of RoleRight
        /// </summary>
        void GetRoleRightAsync();
        /// <summary>
        /// Loading All Records of SecurityQuestion
        /// </summary>
        void GetSecurityQuestionAsync();
        /// <summary>
        /// Loading All Records of UserRole
        /// </summary>
        void GetUserRoleAsync();
        
        /// <summary>
        /// Gets all applicaions.
        /// </summary>
        void GetAllApplicaions();

        /// <summary>
        /// Gets all culture lookup table.
        /// </summary>
        void GetCultureAsync();

        /// <summary>
        /// metjod to Check is Email Exist or Not.
        /// </summary>
        void CheckIsEmailExist(User CurrentUser);

        #region For User Reset

        /// <summary>
        /// Get All User Data By his ID.
        /// </summary>
        /// <param name="UserID">UserID (Guid)</param>
        void GetUserByID(Guid? UserID);

        /// <summary>
        /// this Method is To Check if the Current Link is valid or not
        /// </summary>
        /// <param name="OperationString">value of query string from your mail</param>
        void CheckUserRequestReset(String OperationString);

        /// <summary>
        /// For Deletign All Related Records In UserOperation Table
        /// And That in case if one Complete his Reset Login Operation
        /// and He Had not Change His EmailAddress.
        /// </summary>
        /// <param name="UserID">value of The UserID</param>
        void DeleteUserOperationByUserID(Guid UserID);

        /// <summary>
        /// Update Reset Flag So This User Can Enter Direct 
        /// to Reset Page In The Next Time Open The Project
        /// </summary>
        /// <param name="UserID">The User Id ex.ABDBD-SDGH-SDFKS54-NSBNMA</param>
        void UpdateReset(Guid? UserID);
        #endregion

        /// <summary>
        /// Adding New user to the Current Context
        /// </summary>
        /// <param name="SetInContext">Set In Current Context or as New Object only</param>
        /// <returns>New Istance on User</returns>
        User AddNewUser(bool SetInContext);
        /// <summary>
        /// Remove the User From The Context
        /// </summary>
        /// <param name="user">the User Wanted to Remove</param>
        void RemoveUser(User user);

        /// <summary>
        /// Sending Reset login Mail to the user
        /// </summary>
        /// <param name="EmailAddress">value of user Email Address</param>
        /// <param name="FirstName">Value of FirstName</param>
        /// <param name="LastName">Value of LastName</param>
        /// <param name="OperationString">value of string from userID + EmailAddress</param>
        void SendResetMail(string EmailAddress, string FirstName, string LastName, string OperationString);

        /// <summary>
        /// Sending Confirmation Message In case one Register for fisrt time or
        /// one reset his mail.
        /// </summary>
        /// <param name="EmailAddress">value of user Email Address</param>
        /// <param name="FirstName">Value of FirstName</param>
        /// <param name="LastName">Value of LastName</param>
        /// <param name="OperationString">value of string from userID + EmailAddress</param>
        void SendConfiramtionMail(string EmailAddress, string FirstName, string LastName, string OperationString);

        /// <summary>
        /// Apply All Changes in current Context to database.
        /// </summary>
        void SaveChangesAsync();

        /// <summary>
        /// Reject all changes happen on current Context
        /// </summary>
        void RejectChanges();

        /// <summary>
        /// Cleans up.
        /// </summary>
        void CleanUp();

        #endregion Public
        #endregion Methods
    }
}
