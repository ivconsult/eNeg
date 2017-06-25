
#region → Usings   .
using System;
using System.ComponentModel;
using System.Linq;
using System.ServiceModel.DomainServices.Client;
using citPOINT.eNeg.Common;
using citPOINT.eNeg.Data.Web;
using System.ComponentModel.Composition;
using System.Linq.Expressions;
using System.Collections.Generic;
using citPOINT.eNeg.Common.Helper;
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
    /// Model Layer for the process of Managing Users throught Admin account   
    /// </summary>
    #region  Using MEF to export to MangeUsersViewModel

    [Export(typeof(IUserBaseModel))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    #endregion
    public class UserBaseModel : IUserBaseModel
    {

        #region → Fields         .

        private eNegContext mNegContext;
        private eNegPaging mUsersPager;

        private bool mIsBusy = false;
        private bool mHasChanges = false;
        private bool mIsFilteredByAlphabet = false;
        private string mFilterLetter;
        private string mLastSelectedColumn;
        private bool mIsFilteredByKey = false;
        private string mFilterKeyWord;
        #endregion Fields

        #region → Properties     .

        /// <summary>
        /// Gets or sets a value indicating whether this instance is for public profile.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is for public profile; otherwise, <c>false</c>.
        /// </value>
        public bool IsForPublicProfile { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is filtered by key.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is filtered by key; otherwise, <c>false</c>.
        /// </value>
        public bool IsFilteredByKey
        {
            get { return mIsFilteredByKey; }
            set { mIsFilteredByKey = value; }
        }

        /// <summary>
        /// Gets or sets the filter key word.
        /// </summary>
        /// <value>The filter key word.</value>
        public string FilterKeyWord
        {
            get { return mFilterKeyWord; }
            set
            {
                mFilterKeyWord = value;
            }
        }

        /// <summary>
        /// Gets or sets the last name of the selected column.
        /// </summary>
        /// <value>The last name of the selected column.</value>
        public string LastSelectedColumn
        {
            get { return mLastSelectedColumn; }
            set { mLastSelectedColumn = value; }
        }

        /// <summary>
        /// Gets or sets the filter letter.
        /// </summary>
        /// <value>The filter letter.</value>
        public string FilterLetter
        {
            get { return mFilterLetter; }
            set { mFilterLetter = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is filtered by alphabet.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is filtered by alphabet; otherwise, <c>false</c>.
        /// </value>
        public bool IsFilteredByAlphabet
        {
            get { return mIsFilteredByAlphabet; }
            set { mIsFilteredByAlphabet = value; }
        }

        /// <summary>
        /// Gets the user page.
        /// </summary>
        /// <value>The user page.</value>
        public eNegPaging UserPager
        {
            get
            {
                if (mUsersPager == null)
                    mUsersPager = new eNegPaging();
                return mUsersPager;
            }
        }

        /// <summary>
        /// Gets the context.
        /// </summary>
        /// <value>The context.</value>
        public eNegContext Context
        {
            get
            {
                if (mNegContext == null)
                {
                    mNegContext = Repository.Context;
                    mNegContext.PropertyChanged += new PropertyChangedEventHandler(mNegContext_PropertyChanged);
                }
                return mNegContext;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is busy.
        /// </summary>
        /// <value><c>true</c> if this instance is busy; otherwise, <c>false</c>.</value>
        public bool IsBusy
        {
            get
            {
                return mIsBusy;
            }
            private set
            {
                if (mIsBusy != value)
                {
                    mIsBusy = value;
                    OnPropertyChnaged("IsBusy");
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance has changes.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance has changes; otherwise, <c>false</c>.
        /// </value>
        public bool HasChanges
        {
            get
            {
                return mHasChanges;
            }
            private set
            {
                if (mHasChanges != value)
                {
                    mHasChanges = value;
                    OnPropertyChnaged("HasChanges");
                }
            }
        }


        #endregion Properties

        #region → Event Handlers .

        /// <summary>
        /// Handles the PropertyChanged event of the mNegContext control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.ComponentModel.PropertyChangedEventArgs"/> instance containing the event data.</param>
        private void mNegContext_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "HasChanges":
                    this.HasChanges = mNegContext.HasChanges;
                    break;
                case "IsBusy":
                    this.IsBusy = mNegContext.IsLoading;
                    break;
                case "IsSubmitting":
                    this.IsBusy = mNegContext.IsSubmitting;
                    break;
            }
        }

        #endregion Event Handlers

        #region → Events         .

        /// <summary>
        /// Occurs when [find user count complete].
        /// </summary>
        public event Action<InvokeOperation<int?>> FindUserCountComplete;

        /// <summary>
        /// Occurs when [get users by alphabet count complete].
        /// </summary>
        public event Action<InvokeOperation<int>> GetUsersCountByAlphabetComplete;

        /// <summary>
        /// Occurs when [get users count complete].
        /// </summary>
        public event Action<InvokeOperation<int>> GetUsersCountComplete;

        /// <summary>
        /// Occurs when [find user complete].
        /// </summary>
        public event EventHandler<eNegEntityResultArgs<User>> FindUsersComplete;

        /// <summary>
        /// Occurs when [get users by alphabet complete].
        /// </summary>
        public event EventHandler<eNegEntityResultArgs<User>> GetUsersByAlphabetComplete;

        /// <summary>
        /// Occurs when [get users complete].
        /// </summary>
        public event EventHandler<eNegEntityResultArgs<User>> GetUsersByPageNumberComplete;

        /// <summary>
        /// Call back of Geting user organizations for owners users.
        /// </summary>
        public event EventHandler<eNegEntityResultArgs<UserOrganization>> GetUserOrganizationsForOwnersUsersComplete;

        /// <summary>
        /// Occurs when [save changes complete].
        /// </summary>
        public event EventHandler<SubmitOperationEventArgs> SaveChangesComplete;

        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion Events

        #region → Methods        .

        #region → Private        .

        /// <summary>
        /// Private Method used to perform query on certain entity of eNeg Entities
        /// </summary>
        /// <typeparam name="T">Value Of T</typeparam>
        /// <param name="qry">Value Of qry</param>
        /// <param name="evt">Value Of evt</param>
        private void PerformQuery<T>(EntityQuery<T> qry, EventHandler<eNegEntityResultArgs<T>> evt) where T : Entity
        {

            Context.Load<T>(qry, LoadBehavior.RefreshCurrent, r =>
            {
                if (evt != null)
                {
                    try
                    {
                        if (r.HasError)
                        {
                            evt(this, new eNegEntityResultArgs<T>(r.Error));
                            r.MarkErrorAsHandled();
                        }
                        else
                        {
                            evt(this, new eNegEntityResultArgs<T>(r.Entities));
                        }
                    }
                    catch (Exception ex)
                    {
                        evt(this, new eNegEntityResultArgs<T>(ex));
                    }
                }
            }, null);
        }

        /// <summary>
        /// Performs the users query.
        /// </summary>
        /// <typeparam name="T">The Entity.</typeparam>
        /// <param name="qry">The qry.</param>
        /// <param name="evt">The evt.</param>
        private void PerformUsersQuery<T>(EntityQuery<T> qry, EventHandler<eNegEntityResultArgs<T>> evt) where T : Entity
        {
            Context.Load<T>(qry, LoadBehavior.RefreshCurrent, r =>
            {
                if (evt != null)
                {
                    try
                    {
                        if (r.HasError)
                        {
                            evt(this, new eNegEntityResultArgs<T>(r.Error));
                            r.MarkErrorAsHandled();
                        }
                        else if (r.Entities.Count() > 0)
                        {
                            evt(this, new eNegEntityResultArgs<T>(r.Entities));
                        }
                        else if (r.Entities.Count() == 0 && UserPager.CurrentPageNumber > 1)
                        {
                            GetPrevUsersAsync();
                        }
                    }
                    catch (Exception ex)
                    {
                        evt(this, new eNegEntityResultArgs<T>(ex));
                    }
                }
            }, null);
        }
        

        /// <summary>
        /// Performs the users query.
        /// </summary>
        private void PerformUsersQuery(string CaseName)
        {
            switch (CaseName)
            {
                case "FilterByAlphabet":
                    GetUserByAlphabetAsync(FilterLetter, LastSelectedColumn);
                    break;
                case "FilterByKeyWord":
                    FindUsersAsync(FilterKeyWord);
                    break;
                case "NoFilter":
                    GetAllUsers();
                    break;
            }

            // Execute the Get Users. query

        }

        /// <summary>
        /// Gets all users.
        /// </summary>
        private void GetAllUsers()
        {
            int LocalPageNumber = UserPager.CurrentPageNumber - 1;

            // Generate the query with paging
            var qry = Context.GetUserQuery()
                        .Where(s => s.UserID != AppConfigurations.CurrentLoginUser.UserID &&
                                    s.Disabled == false &&
                                    (IsForPublicProfile == false || s.HasPublicProfile == true))
                        .OrderBy(s => s.EmailAddress)
                        .Skip(LocalPageNumber * UserPager.ITEMSPAGESIZE)
                        .Take(UserPager.ITEMSPAGESIZE);

            GetUserCountAsync();

            PerformUsersQuery(qry, GetUsersByPageNumberComplete);
        }

        /// <summary>
        /// Checks the on case required.
        /// </summary>
        private void CheckOnCaseRequired()
        {
            if (IsFilteredByAlphabet)
            {
                PerformUsersQuery("FilterByAlphabet");
            }
            else if (IsFilteredByKey)
            {
                PerformUsersQuery("FilterByKeyWord");
            }
            else
            {
                PerformUsersQuery("NoFilter");
            }
        }

        #endregion Private
        
        #region → Protected      .

        /// <summary>
        /// Called when [property chnaged].
        /// </summary>
        /// <param name="PropertyName">Name of the property.</param>
        protected virtual void OnPropertyChnaged(string PropertyName)
        {
            if (PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(PropertyName));
            }
        }

        #endregion Protected
        
        #region → Public         .

        #region IManageUsersModel Interface implementation

        /// <summary>
        /// Fins the users count async.
        /// </summary>
        /// <param name="KeyWord">The key word.</param>
        /// <param name="UserID">The user ID.</param>
        public void FinUsersCountAsync(string KeyWord, Guid UserID)
        {
            Context.FindUsersCount(KeyWord, UserID, IsForPublicProfile, FindUserCountComplete, null);
        }

        /// <summary>
        /// Finds the users async.
        /// </summary>
        /// <param name="KeyWord">The key word.</param>
        public void FindUsersAsync(string KeyWord)
        {
            string TempKeyWord = KeyWord;

            int LocalPageNumber = UserPager.CurrentPageNumber - 1;

            TempKeyWord = SurroundWithEscapeChar(TempKeyWord);

            var qry = Context.FindUserQuery(TempKeyWord, AppConfigurations.CurrentLoginUser.UserID, IsForPublicProfile)
                        .Skip(LocalPageNumber * UserPager.ITEMSPAGESIZE)
                        .Take(UserPager.ITEMSPAGESIZE);

            PerformUsersQuery(qry, FindUsersComplete);

            FinUsersCountAsync(TempKeyWord, AppConfigurations.CurrentLoginUser.UserID);
        }

        /// <summary>
        /// Surrounds the with escape char.
        /// </summary>
        /// <param name="TempKeyWord">The temp key word.</param>
        /// <returns>The string after formatting</returns>
        private string SurroundWithEscapeChar(string TempKeyWord)
        {
            string[] temp = new string[TempKeyWord.Length];
            for (int i = 0; i < TempKeyWord.Length; i++)
            {
                temp[i] = "[" + TempKeyWord[i];
            }
            for (int i = 0; i < temp.Length; i++)
            {
                temp[i] += "]";
            }
            TempKeyWord = string.Join("", temp);
            return TempKeyWord;
        }

        /// <summary>
        /// Gets the user count by alphabet async.
        /// </summary>
        /// <param name="Alphabet">The alphabet.</param>
        /// <param name="ColumnName">Name of the column.</param>
        /// <param name="UserID">The user ID.</param>
        public void GetUserCountByAlphabetAsync(string Alphabet, string ColumnName, Guid UserID)
        {
            Context.GetUsersCountByAlphabetExceptCurrentUser(Alphabet, ColumnName, UserID, IsForPublicProfile, GetUsersCountByAlphabetComplete, null);
        }

        /// <summary>
        /// Gets the user count async.
        /// </summary>
        public void GetUserCountAsync()
        {
            Context.GetUsersCountExceptCurrentUser(AppConfigurations.CurrentLoginUser.UserID, IsForPublicProfile, GetUsersCountComplete, null);
        }

        /// <summary>
        /// Gets the user by alphabet.
        /// </summary>
        /// <param name="Alphabet">The alphabet.</param>
        /// <param name="ColumnName">Name of the column.</param>
        public void GetUserByAlphabetAsync(string Alphabet, string ColumnName)
        {
            int LocalPageNumber = UserPager.CurrentPageNumber - 1;

            var Type = typeof(User);
            var Parameter = Expression.Parameter(Type, "Alphabet");

            Expression expr = Expression.Call(
                  Expression.Property(Parameter, Parameter.Type.GetProperty(ColumnName)),
                  typeof(string).GetMethod("StartsWith", new Type[] { typeof(string) }),
                  new Expression[] { Expression.Constant(Alphabet, typeof(string)) });

            var qry = Context.GetUserQuery()
                             .Where(Expression.Lambda<Func<User, bool>>(expr, new ParameterExpression[] { Parameter }))
                             .Where(s => s.UserID != AppConfigurations.CurrentLoginUser.UserID &&
                                         s.Disabled == false &&
                                         (IsForPublicProfile == false || s.HasPublicProfile == true))
                             .OrderBy(s => s.EmailAddress)
                             .Skip(LocalPageNumber * UserPager.ITEMSPAGESIZE)
                             .Take(UserPager.ITEMSPAGESIZE);

            GetUserCountByAlphabetAsync(Alphabet, LastSelectedColumn, AppConfigurations.CurrentLoginUser.UserID);

            PerformUsersQuery(qry, GetUsersByAlphabetComplete);
        }

        /// <summary>
        /// Gets the user organizations for owners users.
        /// </summary>
        /// <param name="usersIDs">The users Ids.</param>
        public void GetUserOrganizationsForOwnersUsers(List<Guid> usersIDs)
        {
            this.Context.UserOrganizations.Clear();
            PerformQuery(this.Context.GetUserOrganizationsForOwnersUsersQuery(usersIDs), GetUserOrganizationsForOwnersUsersComplete);
        }

        /// <summary>
        /// Gets the prev users async.
        /// </summary>
        public void GetPrevUsersAsync()
        {
            if (UserPager.CurrentPageNumber > 1)
            {
                UserPager.CurrentPageNumber--;
                CheckOnCaseRequired();
            }
        }

        /// <summary>
        /// Gets the users by page number async.
        /// </summary>
        /// <param name="PageNumber">The page number.</param>
        public void GetUsersByPageNumberAsync(int PageNumber)
        {
            UserPager.CurrentPageNumber = PageNumber;
            CheckOnCaseRequired();
        }

        /// <summary>
        /// Removes the user.
        /// </summary>
        /// <param name="user">The user.</param>
        public void RemoveUser(User user)
        {
            if (Context.Users.Contains(user))
            {
                Context.Users.Remove(user);
            }
        }

        /// <summary>
        /// Saves the changes async.
        /// </summary>
        public void SaveChangesAsync()
        {
            this.Context.SubmitChanges(s =>
            {
                if (SaveChangesComplete != null)
                {
                    try
                    {
                        if (s.HasError)
                        {
                            SaveChangesComplete(this, new SubmitOperationEventArgs(s, s.Error));
                            s.MarkErrorAsHandled();
                        }
                        else
                        {
                            SaveChangesComplete(this, new SubmitOperationEventArgs(s));
                        }
                    }
                    catch (Exception ex)
                    {
                        SaveChangesComplete(this, new SubmitOperationEventArgs(ex));
                    }
                }
            }, null);
        }

        /// <summary>
        /// Rejects the changes.
        /// </summary>
        public void RejectChanges()
        {
            this.Context.RejectChanges();
        }

        /// <summary>
        /// Cleans up.
        /// </summary>
        public void CleanUp()
        {
            Repository.Context = null;
        }

        #endregion IManageUsersModel Interface implementation

        #endregion Public

        #endregion Methods        
    }
}
