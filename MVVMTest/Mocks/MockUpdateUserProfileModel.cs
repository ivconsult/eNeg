
#region → Usings   .
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ServiceModel.DomainServices.Client;
using citPOINT.eNeg.Common;
using citPOINT.eNeg.Data.Web;
#endregion

#region → History  .

/* Date         User            Change
 * 
 * 20.10.10     M.Wahab         ► creation
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
    /// Mock Of Update User Profile Model
    /// </summary>
    public class MockUpdateUserProfileModel : IUpdateUserProfileModel
    {
        #region → Fields         .

        private List<User> _users;
        private List<Culture> mCultureSource;

        #endregion Fields

        #region → Properties     .

        /// <summary>
        /// Any Data in current Context has changed
        /// e.g Added,Modified,or Deleted.
        /// </summary>
        /// <value></value>
        public bool HasChanges
        {
            get { return false; }
        }

        /// <summary>
        /// is Current Context is Busy in Loading Data or Submitting.
        /// </summary>
        /// <value></value>
        public bool IsBusy
        {
            get { return false; }
        }

        /// <summary>
        /// Gets the Culture source.
        /// </summary>
        /// <value>The Culture source.</value>
        public List<Culture> CultureSource
        {
            get
            {
                if (mCultureSource == null)
                {
                    mCultureSource = new List<Culture>();
                    mCultureSource.Add(new Culture()
                    {
                        CultureID = 1,
                        CultureName = @"Arab World",
                    });
                    mCultureSource.Add(new Culture()
                    {
                        CultureID = 2,
                        CultureName = @"Argentina",
                    });
                    mCultureSource.Add(new Culture()
                    {
                        CultureID = 3,
                        CultureName = @"Australia",
                    });
                    mCultureSource.Add(new Culture()
                    {
                        CultureID = 5,
                        CultureName = @"Austria",
                    });
                    mCultureSource.Add(new Culture()
                    {
                        CultureID = 6,
                        CultureName = @"Bangladesh",
                    });
                    mCultureSource.Add(new Culture()
                    {
                        CultureID = 7,
                        CultureName = @"Belgium",
                    });
                    mCultureSource.Add(new Culture()
                    {
                        CultureID = 8,
                        CultureName = @"Brazil",
                    });
                    mCultureSource.Add(new Culture()
                    {
                        CultureID = 9,
                        CultureName = @"Bulgaria",
                    });
                } return mCultureSource;
            }
        }

        #endregion Properties

        #region → Constructors   .

        /// <summary>
        /// Initializes a new instance of the <see cref="MockUpdateUserProfileModel"/> class.
        /// </summary>
        public MockUpdateUserProfileModel()
        {

            AppConfigurations.ProfileUserID = Guid.NewGuid();
            AppConfigurations.CurrentLoginUser = new LoginUser();

            AppConfigurations.CurrentLoginUser.UserID = AppConfigurations.ProfileUserID;


            _users = new List<User>()
                            {
                               new User()
                                {
                                    UserID =    AppConfigurations.CurrentLoginUser.UserID,
                                    EmailAddress = "Mohammedenew@gmail.com",
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
                                    FirstName = "First Name",
                                    LastName = "Last Name",
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
                                    UserID =    Guid.NewGuid(),
                                    EmailAddress = "Existmail@gmail.com",
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
                                    FirstName = "First Name",
                                    LastName = "Last Name",
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

        #endregion Constructor

        #region → Events         .

        /// <summary>
        /// Occurs when [get culture complete].
        /// </summary>
        public event EventHandler<eNegEntityResultArgs<Culture>> GetCultureComplete;

        /// <summary>
        /// Event Handler For Method GetCountryAsync
        /// </summary>
        public event EventHandler<eNegEntityResultArgs<Country>> GetCountryComplete;

        /// <summary>
        /// Event Handler For Method GetPreferedLanguageAsync
        /// </summary>
        public event EventHandler<eNegEntityResultArgs<PreferedLanguage>> GetPreferedLanguageComplete;

        /// <summary>
        /// Event Handler For Method CheckIsEmailExistAsync
        /// Check Mail repeating
        /// </summary>
        public event EventHandler<eNegEntityResultArgs<User>> GetCheckIsEmailExistComplete;

        /// <summary>
        /// Event Handler For Method GetUserByID
        /// </summary>
        public event EventHandler<eNegEntityResultArgs<User>> GetUserByIDComplete;

        /// <summary>
        /// Event Handler For Method SaveChanges
        /// </summary>
        public event EventHandler<SubmitOperationEventArgs> SaveChangesComplete;

        /// <summary>
        /// Event Handler For Method PropertyChanged
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Event Handler For Method SendingMail
        /// </summary>
        public event Action<InvokeOperation> SendingMailCompleted;

        #endregion Events

        #region → Methods        .
        
        /// <summary>
        /// Gets all culture lookup table.
        /// </summary>
        public void GetCultureAsync()
        {
            if (GetCultureComplete != null)
            {
                GetCultureComplete(this, new eNegEntityResultArgs<Culture>(CultureSource));
            }
        }

        /// <summary>
        /// Loading All Records of Country
        /// </summary>
        public void GetCountryAsync()
        {
            if (GetCountryComplete != null)
            {

                GetCountryComplete(this, new eNegEntityResultArgs<Country>(
                    new List<Country>()
                            {
                                new Country()
                                {
                                    CountryID = Guid.NewGuid(),
                                    CountryName = "Egypt"
                                },
                                new Country()
                                {
                                    CountryID = Guid.NewGuid(),
                                    CountryName = "India"
                                }
                            }));

            }
        }

        /// <summary>
        /// Loading All Records of PreferedLanguage
        /// </summary>
        public void GetPreferedLanguageAsync()
        {
            if (GetPreferedLanguageComplete != null)
            {
                GetPreferedLanguageComplete(this, new eNegEntityResultArgs<PreferedLanguage>(
                    new List<PreferedLanguage>()
                            {
                                new PreferedLanguage()
                                {
                                    LCID = 1,
                                    PreferedLanguage1 = "English"
                                },
                                new PreferedLanguage()
                                {
                                    LCID = 1,
                                    PreferedLanguage1 = "Germany"
                                }
                            }));
            }
        }

        /// <summary>
        /// metjod to Check is Email Exist or Not.
        /// </summary>
        /// <param name="CurrentUser">The Current User</param>
        public void CheckIsEmailExist(User CurrentUser)
        {
            if (GetCheckIsEmailExistComplete != null)
            {
                List<User> tmpUsers = new List<User>();
                foreach (var userItem in _users)
                {
                    if (userItem.UserID != CurrentUser.UserID && userItem.EmailAddress == CurrentUser.EmailAddress)
                    {
                        tmpUsers.Add(userItem);
                        break;
                    }
                }

                GetCheckIsEmailExistComplete(this, new eNegEntityResultArgs<User>(tmpUsers));

            }
        }

        /// <summary>
        /// Get All User Data By his ID.
        /// </summary>
        /// <param name="UserID">UserID (Guid)</param>
        public void GetUserByID(Guid? UserID)
        {
            if (GetUserByIDComplete != null)
            {
                List<User> tmpUsers = new List<User>();
                foreach (var userItem in _users)
                {
                    if (userItem.UserID == UserID.Value)
                    {
                        tmpUsers.Add(userItem);
                        break;
                    }
                }

                GetUserByIDComplete(this, new eNegEntityResultArgs<User>(tmpUsers));
            }
        }

        /// <summary>
        /// Apply All Changes in current Context to database.
        /// </summary>
        public void SaveChangesAsync()
        {
            if (SaveChangesComplete != null)
            {
                SaveChangesComplete(this, new SubmitOperationEventArgs(new Exception("")));

            }
        }

        /// <summary>
        /// Reject all changes happen on current Context
        /// </summary>
        public void RejectChanges()
        {
            _users[0].NewEmail = string.Empty;
        }

        /// <summary>
        /// Sending Confirmation Message In case one UpdateUserProfile for fisrt time or
        /// one reset his mail.
        /// </summary>
        /// <param name="EmailAddress">value of user Email Address</param>
        /// <param name="FirstName">Value of FirstName</param>
        /// <param name="LastName">Value of LastName</param>
        /// <param name="OperationString">value of string from userID + EmailAddress</param>
        public void SendConfiramtionMail(string EmailAddress, string FirstName, string LastName, string OperationString)
        {

        }
        
        /// <summary>
        /// Cleans up.
        /// </summary>
        public void CleanUp()
        {

        }
        #endregion Methods
                
    }
}
