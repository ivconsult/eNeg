
#region → Usings   .
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.DomainServices.EntityFramework;
using System.ServiceModel.DomainServices.Hosting;
using System.ServiceModel.DomainServices.Server;
using System.ServiceModel.DomainServices.Server.ApplicationServices;
using citPOINT.eNeg.Infrastructure.ExceptionHandling;
using citPOINT.eNeg.Infrastructure.Logging;
using citPOINT.eNeg.Common;

#endregion

#region → History  .

/* Date         User              Change
 * 
 * 02.08.10     Yousra Reda       Creation
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
    /// Service enable to me to Authentiacte user
    /// </summary>
    [EnableClientAccess()]
    public class LogInService : LinqToEntitiesDomainService<eNegEntities>, IAuthentication<LoginUser>
    {

        #region → Fields         .
        public static LoginUser mDefaultUser = new LoginUser()
        {
            EmailAddress = String.Empty,
            Password = String.Empty,
            Roles = new List<string>()
        };

        private static string mAuthenticatedUser;
        private static bool mIsPersistent;
        #endregion

        #region → Methods        .

        #region → Private        .

        /// <summary>
        /// Get user by user name
        /// </summary>
        /// <param name="userName">Value Of userName</param>
        /// <returns>Login User</returns>
        private LoginUser GetUserByName(string userName)
        {
            User foundUser = this.ObjectContext.User.FirstOrDefault(u => u.EmailAddress == userName);

            if (foundUser != null)
            {

                #region → Locked But Does not have activation mail .

                bool IsUserLocked = false;
                string OperationStringUrl = string.Empty;

                if (foundUser.Locked == false)
                {
                    var IsAnyActivationMailExist = this.ObjectContext.UserOperations
                                                                     .Where(s => s.UserID == foundUser.UserID &&
                                                                            s.Type == 0/*Activation Mail*/ &&
                                                                            string.IsNullOrEmpty(s.NewEmailAddress))
                                                                     .FirstOrDefault();
                    if (IsAnyActivationMailExist != null)
                    {
                        OperationStringUrl = IsAnyActivationMailExist.OperationString;
                        IsUserLocked = true;
                    }
                }

                #endregion

                #region → Load rights And Roles                    .

                IList<string> _rights = new List<string>();

                foreach (var item in this.ObjectContext.GetRightsByUserId(foundUser.UserID))
                {
                    _rights.Add(item.RightName);
                }

                IList<string> _roles = new List<string>();

                foreach (var item in this.ObjectContext.GetRolesByUserID(foundUser.UserID))
                {
                    _roles.Add(item.RoleName);
                }

                #endregion

                //in case if Current User Is owner for organization
                Guid organizationID = GetOrganizationOwnedForUser(foundUser.UserID);
 
                return new LoginUser()
                {
                    EmailAddress = foundUser.EmailAddress,
                    PasswordHash = foundUser.PasswordHash,
                    PasswordSalt = foundUser.PasswordSalt,
                    UserID = foundUser.UserID,
                    eNegRights = _rights,
                    Locked = foundUser.Locked || IsUserLocked, // Flag to re-send Activation mail.
                    Disabled = foundUser.Disabled,
                    FirstName = foundUser.FirstName,
                    LastName = foundUser.LastName,
                    eNegRoles = _roles,
                    IPAddress = foundUser.IPAddress,
                    OperationStringUrl = OperationStringUrl,
                    OrganizationOwnedID = organizationID,
                    IsOrganizationOwner = organizationID != Guid.Empty,



                    LockedDate = foundUser.LockedDate,
                    LastLoginDate = foundUser.LastLoginDate,
                    CreateDate = foundUser.CreateDate,
                    AccountTypeID = foundUser.AccountTypeID,
                    SecurityQuestionID = foundUser.SecurityQuestionID,
                    AnswerHash = foundUser.AnswerHash,
                    AnswerSalt = foundUser.AnswerSalt,
                    Online = foundUser.Online,
                    CompanyName = foundUser.CompanyName,
                    LCID = foundUser.LCID,
                    Address = foundUser.Address,
                    City = foundUser.City,
                    CountryID = foundUser.CountryID,
                    Phone = foundUser.Phone,
                    Mobile = foundUser.Mobile,
                    Gender = foundUser.Gender,
                    Reset = foundUser.Reset,
                    CultureID = foundUser.CultureID,
                    HasPublicProfile = foundUser.HasPublicProfile,
                    ProfileDescription = foundUser.ProfileDescription,

                };
            }
            else
                return null;
        }

        /// <summary>
        /// Gets the organization owned for user.
        /// </summary>
        /// <param name="userID">The user ID.</param>
        /// <returns></returns>
        [Invoke]
        private Guid GetOrganizationOwnedForUser(Guid userID)
        {

            var result = this.ObjectContext
                             .UserOrganizations
                             .Where(ss => ss.Deleted == false &&
                                          ss.UserID == userID &&
                                          ss.MemberStatus == eNegConstant.OrganizationMemberStatus.Owner);

            if (result != null && result.Count() > 0)
            {
                return result.First().OrganizationID;
            }
            return Guid.Empty;

        }
        #endregion


        #region → Public         .

        #region Used to Pass Username if it is available in Isolated Storage, else pass empty string
        public void AuthenticateUser(string Name)
        {
            mAuthenticatedUser = Name;
            if (!string.IsNullOrEmpty(Name))
                mIsPersistent = true;
            else
                mIsPersistent = false;
        }

        #endregion

        #region Validate Found User Password

        /// <summary>
        /// Validates the user.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        /// <param name="customData">The custom data.</param>
        /// <param name="userData">The user data.</param>
        /// <returns></returns>
        public bool ValidateUser(string username, string password, string customData, out string userData)
        {
            userData = null;

            LoginUser foundUser = this.GetUserByName(username);


            if (foundUser != null)
            {
                // generate password hash
                string passwordHash = HashHelper.ComputeSaltedHash(password, foundUser.PasswordSalt);

                if (string.Equals(passwordHash, foundUser.PasswordHash, StringComparison.Ordinal))
                {
                    return true;

                }
                else
                    return false;
            }
            else
                return false;
        }

        #endregion

        #region "IAuthentication<LoginUser> Interface implementation"

        /// <summary>
        /// Get the user information for the currently login user   
        /// </summary>
        /// <returns>login User</returns>
        [Query(IsComposable = false)]
        public LoginUser GetUser()
        {

            if (mIsPersistent)
            {
                LoginUser found = this.GetUserByName(mAuthenticatedUser);
                if (found != null)
                {
                    this.ObjectContext.MakeUserOnline(found.UserID, found.ClientAddress);
                    return found;
                }
                else
                    return LogInService.mDefaultUser;

            }

            return LogInService.mDefaultUser;
        }

        /// <summary>
        /// Not implemented.
        /// </summary>
        /// <param name="user">Value Of user</param>
        [Update]
        public void UpdateUser(LoginUser user)
        {

        }

        /// <summary>
        /// Validate and login
        /// </summary>
        /// <param name="userName">Value Of userName</param>
        /// <param name="password">Value Of password</param>
        /// <param name="isPersistent">Value Of isPersistent</param>
        /// <param name="customData">Value Of customData</param>
        /// <returns>returns the user how want to login if credentials a returns else returns default User</returns>
        public LoginUser Login(string userName, string password, bool isPersistent, string customData)
        {
            try
            {
                string userData;

                if (this.ValidateUser(userName, password, customData, out userData))
                {
                    return this.GetUserByName(userName);
                }
                return LogInService.mDefaultUser;
            }
            catch (Exception ex)
            {
                //ExceptionHandlingResult result = ExceptionHandlingProvider.Handle<ServerDataExceptionHandler, ServerLoggingHandler>(ex, false);
            }
            return LogInService.mDefaultUser;
        }

        /// <summary>
        /// Logout
        /// </summary>
        /// <returns>login User</returns>
        public LoginUser Logout()
        {
            return LogInService.mDefaultUser;
        }

        #endregion "IAuthentication<LoginUser> Interface implementation"

        #endregion

        #endregion
    }
}
