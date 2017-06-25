#region → Usings   .

using System;
using System.Linq;
using System.Collections.Generic;
using System.Windows;
using citPOINT.eNeg.Common;
using citPOINT.eNeg.Data.Web;
using System.ServiceModel.DomainServices.Client;

#endregion

#region → History  .

/* Date         User              Change
 * 
 * 17.07.11     Yousra Reda       Creation
 */

# endregion

#region → ToDos    .

/*
 * Date         set by User     Description
 * 
 * 
*/
# endregion

namespace citPOINT.eNeg.MVVM.UnitTest
{
    /// <summary>
    /// Mock for Manage Users Model
    /// </summary>
    //[Export(typeof(IManageUsersModel))]
    public class MockManageUsers : IUserBaseModel
    {
        #region → Fields         .
        private eNegContext mContext;
        private List<User> mUsers;
        private eNegPaging mUserPager;
        private bool mIsFilteredByAlphabet;
        private string mFilterLetter;
        private string mLastSelectedColumn;
        private bool mIsFilteredByKey;
        private string mFilterKeyWord;
        #endregion Fields

        #region → Properties     .

        /// <summary>
        /// Gets or sets a value indicating whether this instance is for public profile.
        /// this is the main key that sparate the manageUserViewmodel from PublicUserViewModel.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is for public profile; otherwise, <c>false</c>.
        /// </value>
        public bool IsForPublicProfile {get;set;}

        /// <summary>
        /// Gets the context.
        /// </summary>
        /// <value>The context.</value>
        public eNegContext Context
        {
            get
            {
                if (mContext == null)
                {
                    mContext = new eNegContext(new Uri("http://localhost:9000/citPOINT-eNeg-Data-Web-eNegService.svc", UriKind.Absolute));
                }
                return mContext;
            }
        }

        /// <summary>
        /// Gets a value indicating whether this instance has changes.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance has changes; otherwise, <c>false</c>.
        /// </value>
        public bool HasChanges
        {
            get { return false; }
        }

        /// <summary>
        /// Gets a value indicating whether this instance is busy.
        /// </summary>
        /// <value><c>true</c> if this instance is busy; otherwise, <c>false</c>.</value>
        public bool IsBusy
        {
            get { return false; }
        }

        /// <summary>
        /// Gets or sets the users.
        /// </summary>
        /// <value>The users.</value>
        public List<User> Users
        {
            get
            {
                if (mUsers == null)
                {
                    mUsers = new List<User>()
                      {
                            new User()
                                {
                                  UserID = Guid.NewGuid(),
                                  EmailAddress = "Test_Test@gmail.com",
                                  Password = "123456A",
                                  NewPassword = string.Empty,
                                  PasswordConfirmation = "123456A",
                                  Locked = false,
                                  IPAddress = "10.0.02.2",
                                  LastLoginDate = DateTime.Now,
                                   CreateDate = DateTime.Now,
                                  AnswerHash = string.Empty,
                                  AnswerSalt = string.Empty,
                                  Online = false,
                                  Disabled = false,
                                  FirstName = string.Empty,
                                  LastName = string.Empty,
                                  CompanyName = string.Empty,
                                  Address = string.Empty,
                                  City = string.Empty,
                                  Phone = string.Empty,
                                  Mobile = string.Empty,
                                  Gender = false,
                                  Reset = false
                            },
                            new User()
                            {
                                UserID = Guid.NewGuid(),
                                EmailAddress = "Test_Test2@gmail.com",
                                Password = "123456A",
                                NewPassword = string.Empty,
                                PasswordConfirmation = "123456A",
                                Locked = false,
                                IPAddress = "10.0.02.2",
                                LastLoginDate = DateTime.Now,
                                CreateDate = DateTime.Now,
                                AnswerHash = string.Empty,
                                AnswerSalt = string.Empty,
                                Online = false,
                                Disabled = false,
                                FirstName = string.Empty,
                                LastName = string.Empty,
                                CompanyName = string.Empty,
                                Address = string.Empty,
                                City = string.Empty,
                                Phone = string.Empty,
                                Mobile = string.Empty,
                                Gender = false,
                                Reset = false
                            },
                            new User()
                            {
                                UserID = Guid.NewGuid(),
                                EmailAddress = "Test_Test3@gmail.com",
                                Password = "123456A",
                                NewPassword = string.Empty,
                                PasswordConfirmation = "123456A",
                                Locked = false,
                                IPAddress = "10.0.02.2",
                                LastLoginDate = DateTime.Now,
                                CreateDate = DateTime.Now,
                                AnswerHash = string.Empty,
                                AnswerSalt = string.Empty,
                                Online = false,
                                Disabled = false,
                                FirstName = string.Empty,
                                LastName = string.Empty,
                                CompanyName = string.Empty,
                                Address = string.Empty,
                                City = string.Empty,
                                Phone = string.Empty,
                                Mobile = string.Empty,
                                Gender = false,
                                Reset = false
                            },
                            new User()
                            {
                                UserID = Guid.NewGuid(),
                                EmailAddress = "Test_Test4@gmail.com",
                                Password = "123456A",
                                NewPassword = string.Empty,
                                PasswordConfirmation = "123456A",
                                Locked = false,
                                IPAddress = "10.0.02.2",
                                LastLoginDate = DateTime.Now,
                                CreateDate = DateTime.Now,
                                AnswerHash = string.Empty,
                                AnswerSalt = string.Empty,
                                Online = false,
                                Disabled = false,
                                FirstName = string.Empty,
                                LastName = string.Empty,
                                CompanyName = string.Empty,
                                Address = string.Empty,
                                City = string.Empty,
                                Phone = string.Empty,
                                Mobile = string.Empty,
                                Gender = false,
                                Reset = false
                            }

                     };
                }

                return mUsers;
            }
        }

        /// <summary>
        /// Gets the user page.
        /// </summary>
        /// <value>The user page.</value>
        public eNegPaging UserPager
        {
            get
            {
                if (mUserPager == null)
                {
                    mUserPager = new eNegPaging();
                    mUserPager.CurrentPageNumber = 1;
                    mUserPager.ItemsCount = Users.Count;
                    mUserPager.ItemsPagesCount = 3;
                    //mUserPager.uxPagePanel = new System.Windows.Controls.StackPanel();
                }
                return mUserPager;
            }
        }

        /// <summary>
        /// Gets a value indicating whether this instance is filtered by alphabet.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is filtered by alphabet; otherwise, <c>false</c>.
        /// </value>
        public bool IsFilteredByAlphabet
        {
            get
            {
                return mIsFilteredByAlphabet;
            }
            set
            {
                mIsFilteredByAlphabet = value;
            }
        }

        /// <summary>
        /// Gets or sets the filter letter.
        /// </summary>
        /// <value>The filter letter.</value>
        public string FilterLetter
        {
            get
            {
                return mFilterLetter;
            }
            set
            {
                mFilterLetter = value;
            }
        }

        /// <summary>
        /// Gets or sets the last selected column.
        /// </summary>
        /// <value>The last selected column.</value>
        public string LastSelectedColumn
        {
            get
            {
                return mLastSelectedColumn;
            }
            set
            {
                mLastSelectedColumn = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is filtered by key.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is filtered by key; otherwise, <c>false</c>.
        /// </value>
        public bool IsFilteredByKey
        {
            get
            {
                return mIsFilteredByKey;
            }
            set
            {
                mIsFilteredByKey = value;
            }
        }

        /// <summary>
        /// Gets or sets the filter key word.
        /// </summary>
        /// <value>The filter key word.</value>
        public string FilterKeyWord
        {
            get
            {
                return mFilterKeyWord;
            }
            set
            {
                mFilterKeyWord = value;
            }
        }

        #endregion

        #region → Constructor    .

        /// <summary>
        /// Initializes a new instance of the <see cref="MockManageUsers"/> class.
        /// </summary>
        public MockManageUsers()
        {
            AppConfigurations.CurrentLoginUser = new LoginUser();
        }

        #endregion

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
        public event EventHandler<eNegEntityResultArgs<Data.Web.User>> FindUsersComplete;

        /// <summary>
        /// Occurs when [get users by alphabet complete].
        /// </summary>
        public event EventHandler<eNegEntityResultArgs<Data.Web.User>> GetUsersByAlphabetComplete;

        /// <summary>
        /// Occurs when [get users complete].
        /// </summary>
        public event EventHandler<eNegEntityResultArgs<Data.Web.User>> GetUsersByPageNumberComplete;

        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Occurs when [save changes complete].
        /// </summary>
        public event EventHandler<SubmitOperationEventArgs> SaveChangesComplete;

        /// <summary>
        /// Call back of Geting user organizations for owners users.
        /// </summary>
        public event EventHandler<eNegEntityResultArgs<UserOrganization>> GetUserOrganizationsForOwnersUsersComplete;

        #endregion Events

        #region → Methods        .

        #region → Public         .

        /// <summary>
        /// Fins the users count async.
        /// </summary>
        /// <param name="KeyWord">The key word.</param>
        /// <param name="UserID">The user ID.</param>
        public void FinUsersCountAsync(string KeyWord, Guid UserID)
        {
            if (FindUserCountComplete != null)
            {
                FindUserCountComplete(Context.FindUsersCount(KeyWord, UserID,IsForPublicProfile));
            }
        }

        /// <summary>
        /// Finds the users async.
        /// </summary>
        /// <param name="KeyWord">The key word.</param>
        public void FindUsersAsync(string KeyWord)
        {
            if (FindUsersComplete != null)
            {
                FindUsersComplete(this, new eNegEntityResultArgs<User>(this.Users.Where(s => s.EmailAddress.Contains(KeyWord))));
            }
        }

        /// <summary>
        /// Gets the user count async.
        /// </summary>
        public void GetUserCountAsync()
        {
            if (GetUsersCountComplete != null)
            {
                GetUsersCountComplete(Context.GetUsersCountExceptCurrentUser(AppConfigurations.CurrentLoginUser.UserID, IsForPublicProfile));
            }
        }

        /// <summary>
        /// Gets the user by alphabet.
        /// </summary>
        /// <param name="Alphabet">The alphabet.</param>
        /// <param name="ColumnName">Name of the column.</param>
        public void GetUserByAlphabetAsync(string Alphabet, string ColumnName)
        {
            if (GetUsersByAlphabetComplete != null)
            {
                bool AlphabetFound = false;
                List<User> MatchedUser = new List<User>();
                foreach (var user in Users)
                {
                    AlphabetFound = user.EmailAddress.Contains(Alphabet);
                    if (AlphabetFound)
                        MatchedUser.Add(user);
                }
                GetUsersByAlphabetComplete(this, new eNegEntityResultArgs<User>(MatchedUser));
            }
        }

        /// <summary>
        /// Gets the users by page number async.
        /// </summary>
        /// <param name="PageNumber">The page number.</param>
        public void GetUsersByPageNumberAsync(int PageNumber)
        {
            if (GetUsersByPageNumberComplete != null)
            {
                int NoOfPages = Users.Count / UserPager.ItemsPagesCount;
                if (Users.Count % UserPager.ItemsPagesCount > 0)
                {
                    NoOfPages++;
                }
                if (NoOfPages > PageNumber)
                {
                    GetUsersByPageNumberComplete(this, new eNegEntityResultArgs<User>(this.Users));
                }
                else
                {
                    GetUsersByPageNumberComplete(this, new eNegEntityResultArgs<User>(new List<User>()));
                }
            }
        }


        /// <summary>
        /// Rejects the changes.
        /// </summary>
        public void RejectChanges()
        {

        }

        /// <summary>
        /// Removes the user.
        /// </summary>
        /// <param name="user">The user.</param>
        public void RemoveUser(User user)
        {
            this.Users.Remove(user);
        }

        /// <summary>
        /// Save Complete
        /// </summary>
        public void SaveChangesAsync()
        {
            if (SaveChangesComplete != null)
            {
                SaveChangesComplete(this, new SubmitOperationEventArgs(null, null));
            }
        }

        /// <summary>
        /// Gets the user count by alphabet async.
        /// </summary>
        /// <param name="Alphabet">The alphabet.</param>
        /// <param name="ColumnName">Name of the column.</param>
        /// <param name="UserID">The user ID.</param>
        public void GetUserCountByAlphabetAsync(string Alphabet, string ColumnName, Guid UserID)
        {
            if (GetUsersCountByAlphabetComplete != null)
            {
                GetUsersCountByAlphabetComplete(Context.GetUsersCountByAlphabetExceptCurrentUser(Alphabet, ColumnName, UserID, IsForPublicProfile));
            }
        }

        /// <summary>
        /// Gets the user organizations for owners users.
        /// </summary>
        /// <param name="usersIDs">The users Ids.</param>
        public void GetUserOrganizationsForOwnersUsers(List<Guid> usersIDs)
        {
            
        }

        /// <summary>
        /// Cleans up the Shared Context.
        /// </summary>
        public void CleanUp()
        {
            
        }

        #endregion Methods

        #endregion Public

    }
}
