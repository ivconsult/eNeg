#region → Usings   .
using citPOINT.eNeg.Data.Web;
using System;
using System.ComponentModel;
using System.ServiceModel.DomainServices.Client;
using System.Collections.Generic;
#endregion

#region → History  .

/* Date         User            Change
 * 
 * 28.10.10     Yousra Reda     creation
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
    /// Interface for Class Managing Users
    /// </summary>
    public interface IUserBaseModel : INotifyPropertyChanged
    {

        #region → Properties     .

        /// <summary>
        /// Gets or sets a value indicating whether this instance is for public profile.
        /// this is the main key that sparate the manageUserViewmodel from PublicUserViewModel.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is for public profile; otherwise, <c>false</c>.
        /// </value>
        bool IsForPublicProfile { get; set; }

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

        /// <summary>
        /// Gets the context.
        /// </summary>
        /// <value>The context.</value>
        eNegContext Context { get; }

        /// <summary>
        /// Gets the user page.
        /// </summary>
        /// <value>The user page.</value>
        eNegPaging UserPager { get; }

        /// <summary>
        /// Gets a value indicating whether this instance is filtered by alphabet.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is filtered by alphabet; otherwise, <c>false</c>.
        /// </value>
        bool IsFilteredByAlphabet { get; set; }

        /// <summary>
        /// Gets or sets the filter letter.
        /// </summary>
        /// <value>The filter letter.</value>
        string FilterLetter { get; set; }

        /// <summary>
        /// Gets or sets the last selected column.
        /// </summary>
        /// <value>The last selected column.</value>
        string LastSelectedColumn { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is filtered by key.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is filtered by key; otherwise, <c>false</c>.
        /// </value>
        bool IsFilteredByKey { get; set; }

        /// <summary>
        /// Gets or sets the filter key word.
        /// </summary>
        /// <value>The filter key word.</value>
        string FilterKeyWord { get; set; }
        #endregion Properties

        #region → Methods        .

        /// <summary>
        /// Fins the users count async.
        /// </summary>
        /// <param name="KeyWord">The key word.</param>
        /// <param name="UserID">The user ID.</param>
        void FinUsersCountAsync(string KeyWord, Guid UserID);

        /// <summary>
        /// Finds the users async.
        /// </summary>
        /// <param name="KeyWord">The key word.</param>
        void FindUsersAsync(string KeyWord);

        /// <summary>
        /// Gets the user count async.
        /// </summary>
        void GetUserCountAsync();

        /// <summary>
        /// Gets the user by alphabet.
        /// </summary>
        /// <param name="Alphabet">The alphabet.</param>
        /// <param name="ColumnName">Name of the column.</param>
        void GetUserByAlphabetAsync(string Alphabet, string ColumnName);


        /// <summary>
        /// Gets the users by page number async.
        /// </summary>
        /// <param name="PageNumber">The page number.</param>
        void GetUsersByPageNumberAsync(int PageNumber);

        /// <summary>
        /// Gets the user organizations for owners users.
        /// </summary>
        /// <param name="usersIDs">The users Ids.</param>
        void GetUserOrganizationsForOwnersUsers(List<Guid> usersIDs);

        /// <summary>
        /// Rejects the changes.
        /// </summary>
        void RejectChanges();

        /// <summary>
        /// Removes the user.
        /// </summary>
        /// <param name="user">The user.</param>
        void RemoveUser(User user);

        /// <summary>
        /// Saves the changes async.
        /// </summary>
        void SaveChangesAsync();

        /// <summary>
        /// Gets the user count by alphabet async.
        /// </summary>
        /// <param name="Alphabet">The alphabet.</param>
        /// <param name="ColumnName">Name of the column.</param>
        /// <param name="UserID">The user ID.</param>
        void GetUserCountByAlphabetAsync(string Alphabet, string ColumnName, Guid UserID);

        /// <summary>
        /// Cleans up the Shared Context.
        /// </summary>
        void CleanUp();

        #endregion Methods

        #region → Events         .

        /// <summary>
        /// Occurs when [find user count complete].
        /// </summary>
        event Action<InvokeOperation<int?>> FindUserCountComplete;

        /// <summary>
        /// Occurs when [get users by alphabet count complete].
        /// </summary>
        event Action<InvokeOperation<int>> GetUsersCountByAlphabetComplete;

        /// <summary>
        /// Occurs when [get users count complete].
        /// </summary>
        event Action<InvokeOperation<int>> GetUsersCountComplete;

        /// <summary>
        /// Occurs when [find user complete].
        /// </summary>
        event EventHandler<eNegEntityResultArgs<User>> FindUsersComplete;

        /// <summary>
        /// Occurs when [get users by alphabet complete].
        /// </summary>
        event EventHandler<eNegEntityResultArgs<User>> GetUsersByAlphabetComplete;

        /// <summary>
        /// Occurs when [get users complete].
        /// </summary>
        event EventHandler<eNegEntityResultArgs<User>> GetUsersByPageNumberComplete;

        /// <summary>
        /// Call back of Geting user organizations for owners users.
        /// </summary>
        event EventHandler<eNegEntityResultArgs<UserOrganization>> GetUserOrganizationsForOwnersUsersComplete;

        /// <summary>
        /// Occurs when [save changes complete].
        /// </summary>
        event EventHandler<SubmitOperationEventArgs> SaveChangesComplete;

        #endregion Events
    }
}
