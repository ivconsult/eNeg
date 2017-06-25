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
    ///   Published Profile Details Model
    /// </summary>
    public interface IPublishedProfileDetailsModel : INotifyPropertyChanged
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
        /// Occurs when [get country complete].
        /// </summary>
        event EventHandler<eNegEntityResultArgs<Country>> GetCountryComplete;
        /// <summary>
        /// Occurs when [get user by ID complete].
        /// </summary>
        event EventHandler<eNegEntityResultArgs<User>> GetUserByIDComplete;

        /// <summary>
        /// Call back of get user profile statisticals .
        /// </summary>
        event EventHandler<eNegEntityResultArgs<UserProfileStatisticalsResult>> GetUserProfileStatisticalsComplete;

        /// <summary>
        /// Call back of can user see member profile.
        /// </summary>
        event Action<InvokeOperation<bool>> CanUserSeeMemberProfileComplete;

        #endregion Events

        #region → Methods        .

        /// <summary>
        /// Gets the country async.
        /// </summary>
        void GetCountryAsync();

        /// <summary>
        /// Gets the user by ID.
        /// </summary>
        /// <param name="userID">The user ID.</param>
        void GetUserByID(Guid userID);

        /// <summary>
        /// Gets the user profile statisticals.
        /// </summary>
        /// <param name="userID">The user ID.</param>
        void GetUserProfileStatisticalsAsyc(Guid userID);

        /// <summary>
        /// Determines whether this instance can user see member profile or not.
        /// </summary>
        /// <param name="userID">The user ID.</param>
        void CanUserSeeMemberProfileAsync(Guid userID);


        #endregion Methods



    }
}
