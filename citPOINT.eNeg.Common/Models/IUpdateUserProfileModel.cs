#region → Usings   .
using citPOINT.eNeg.Data.Web;
using System;
using System.ComponentModel;
using System.ServiceModel.DomainServices.Client;

#endregion

#region → History  .

/* Date         User       Description
 * 
 * 19.10.10     M.wahab    ☼ Creation
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
    ///  Update User Profile Model
    /// </summary>
   public interface IUpdateUserProfileModel : INotifyPropertyChanged
    {

        #region → Properties     .
        
        /// <summary>
        /// Gets a value indicating whether this instance has changes.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance has changes; otherwise, <c>false</c>.
        /// </value>
        bool HasChanges { get; }
        
        /// <summary>
        /// Gets a value indicating whether this instance is busy.
        /// </summary>
        /// <value><c>true</c> if this instance is busy; otherwise, <c>false</c>.</value>
        bool IsBusy { get; }

        #endregion
       
        #region → Events         .

          /// <summary>
        /// Occurs when [get culture complete].
        /// </summary>
        event EventHandler<eNegEntityResultArgs<Culture>> GetCultureComplete; 

        /// <summary>
        /// Occurs when [get check is email exist complete].
        /// </summary>
        event EventHandler<eNegEntityResultArgs<User>> GetCheckIsEmailExistComplete;
        /// <summary>
        /// Occurs when [get country complete].
        /// </summary>
        event EventHandler<eNegEntityResultArgs<Country>> GetCountryComplete;
        /// <summary>
        /// Occurs when [get prefered language complete].
        /// </summary>
        event EventHandler<eNegEntityResultArgs<PreferedLanguage>> GetPreferedLanguageComplete;
        /// <summary>
        /// Occurs when [get user by ID complete].
        /// </summary>
        event EventHandler<eNegEntityResultArgs<User>> GetUserByIDComplete;
        /// <summary>
        /// Occurs when [save changes complete].
        /// </summary>
        event EventHandler<SubmitOperationEventArgs> SaveChangesComplete;
        /// <summary>
        /// Occurs when [sending mail completed].
        /// </summary>
        event Action<InvokeOperation> SendingMailCompleted;

        #endregion Events

        #region → Methods        .
        
        /// <summary>
        /// Checks the is email exist.
        /// </summary>
        /// <param name="CurrentUser">The current user.</param>
        void CheckIsEmailExist(User CurrentUser);
        
        /// <summary>
        /// Gets the culture async.
        /// </summary>
        void GetCultureAsync();
        
        /// <summary>
        /// Gets the country async.
        /// </summary>
        void GetCountryAsync();

        /// <summary>
        /// Gets the prefered language async.
        /// </summary>
        void GetPreferedLanguageAsync();

        /// <summary>
        /// Gets the user by ID.
        /// </summary>
        /// <param name="UserID">The user ID.</param>
        void GetUserByID(Guid? UserID);

        /// <summary>
        /// Rejects the changes.
        /// </summary>
        void RejectChanges();

        /// <summary>
        /// Saves the changes async.
        /// </summary>
        void SaveChangesAsync();

        /// <summary>
        /// Sends the confiramtion mail.
        /// </summary>
        /// <param name="EmailAddress">The email address.</param>
        /// <param name="FirstName">The first name.</param>
        /// <param name="LastName">The last name.</param>
        /// <param name="OperationString">The operation string.</param>
        void SendConfiramtionMail(string EmailAddress, string FirstName, string LastName, string OperationString);

        /// <summary>
        /// Cleans up.
        /// </summary>
        void CleanUp();

        #endregion Methods

      

    }
}
