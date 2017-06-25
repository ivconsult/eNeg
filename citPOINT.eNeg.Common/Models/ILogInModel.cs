#region → Usings   .
using System;
using System.ServiceModel.DomainServices.Client.ApplicationServices;
using System.ComponentModel;
using System.Security.Principal;
using citPOINT.eNeg.Data.Web;
using System.ServiceModel.DomainServices.Client;
#endregion

#region → History  .

/* Date         User            Change
 * 
 * 02.08.10     Yousra Reda     creation
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
    /// Interface for Class LogInModel
    /// </summary>
    public interface ILogInModel : INotifyPropertyChanged
    {
        #region → Properties     .

        /// <summary>
        /// Gets if data context is busy
        /// </summary>
        bool IsBusy { get; }

        /// <summary>
        /// Gets if IsLoadingUser is in progress 
        /// </summary>
        bool IsLoadingUser { get; }

        /// <summary>
        /// Gets if IsLoggingIn is in progress 
        /// </summary>
        bool IsLoggingIn { get; }

        /// <summary>
        /// Gets if IsLoggingOut is in progress 
        /// </summary>
        bool IsLoggingOut { get; }

        /// <summary>
        /// Indicates whether the process of saving user is in progress or not
        /// </summary>
        bool IsSavingUser { get; }


        /// <summary>
        /// Represent the authenticated user 
        /// </summary>
        IPrincipal User { get; }

        /// <summary>
        /// Gets the context.
        /// </summary>
        /// <value>The context.</value>
        eNegContext Context { get; }
        #endregion

        #region → Events         .

        /// <summary>
        /// Event that represent the callback of of the process of Loading user of was presistent in the last login operation
        /// </summary>
        event EventHandler<LoadUserOperationEventArgs> LoadUserComplete;

        /// <summary>
        /// Event that represent the callback of of the process of login
        /// </summary>
        event EventHandler<LoginOperationEventArgs> LoginComplete;

        /// <summary>
        /// Event that represent the callback of of the process of logout
        /// </summary>
        event EventHandler<LogoutOperationEventArgs> LogoutComplete;

        /// <summary>
        /// Event that represent the callback of of the process of Selecting User By Name
        /// </summary>
        event EventHandler<eNegEntityResultArgs<User>> GetUserByUserNameComplete;

        /// <summary>
        /// Event that represent the callback of of the process of saving any pending changes in the context
        /// </summary>
        event EventHandler<SubmitOperationEventArgs> SaveChangesComplete;

        /// <summary>
        /// Event that represent the callback of of the process of MakeUserOffline (Update his data in DB)
        /// </summary>
        event EventHandler<eNegEntityResultArgs<User>> MakeUserOfflineComplete;

        /// <summary>
        /// Event that represent the callback of of the process of ChangingAuthntication of current user
        /// </summary>
        event EventHandler<AuthenticationEventArgs> AuthenticationChanged;

        #endregion

        #region → Methods        .

        /// <summary>
        /// This will read appSettings to detect 
        /// whether the user choose "Keep me signed in" on a previous login attempt or not
        /// </summary>
        void LoadUserAsync();

        /// <summary>
        /// Authenticate a user with user name and password
        /// </summary>
        /// <param name="loginParameters">Value Of loginParameters</param>
        void LoginAsync(LoginParameters loginParameters);

        /// <summary>
        /// Logout User Asynchronously
        /// </summary>
        void LogoutAsync();

        /// <summary>
        /// public method to save any change occured in the object created from eNegcontext
        /// </summary>
        void SaveChangesAsync();

        /// <summary>
        /// public method call perform query to select user by his UserName
        /// </summary>
        /// <param name="UserName">Value Of UserName</param>
        void GetUserByUserName(string UserName);

        /// <summary>
        /// public method that call perform query to update user data when he logout
        /// </summary>
        /// <param name="UserID">Value Of UserID</param>
        void MakeUserOffline(Guid? UserID);

        #endregion

    }
}
