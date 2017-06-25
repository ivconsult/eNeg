
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
    /// Mock of Published Profile Details Model.
    /// </summary>
    public class MockPublishedProfileDetailsModel : IPublishedProfileDetailsModel
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
        /// Initializes a new instance of the <see cref="MockPublishedProfileDetailsModel"/> class.
        /// </summary>
        public MockPublishedProfileDetailsModel()
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
        /// Event Handler For Method GetCountryAsync
        /// </summary>
        public event EventHandler<eNegEntityResultArgs<Country>> GetCountryComplete;

        /// <summary>
        /// Event Handler For Method GetUserByID
        /// </summary>
        public event EventHandler<eNegEntityResultArgs<User>> GetUserByIDComplete;

        /// <summary>
        /// Call back of get user profile statisticals .
        /// </summary>
        public event EventHandler<eNegEntityResultArgs<UserProfileStatisticalsResult>> GetUserProfileStatisticalsComplete;

        /// <summary>
        /// Call back of can user see member profile.
        /// </summary>
        public event Action<InvokeOperation<bool>> CanUserSeeMemberProfileComplete;

        /// <summary>
        /// Event Handler For Method PropertyChanged
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion Events

        #region → Methods        .


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
        /// Gets the user by ID.
        /// </summary>
        /// <param name="userID">The user ID.</param>
        public void GetUserByID(Guid userID)
        {
            if (GetUserByIDComplete != null)
            {
                List<User> tmpUsers = new List<User>();
                foreach (var userItem in _users)
                {
                    if (userItem.UserID == userID)
                    {
                        tmpUsers.Add(userItem);
                        break;
                    }
                }

                GetUserByIDComplete(this, new eNegEntityResultArgs<User>(tmpUsers));
            }
        }

        /// <summary>
        /// Gets the user profile statisticals.
        /// </summary>
        /// <param name="userID">The user ID.</param>
        public void GetUserProfileStatisticalsAsyc(Guid userID)
        {
            List<UserProfileStatisticalsResult> userStatistical = new List<UserProfileStatisticalsResult>();

            userStatistical.Add(new UserProfileStatisticalsResult()
                     {
                         StatisticalName = eNegConstant.UserProfileStatisticals.NegotiationCount,
                         StatisticalValue = 52
                     });


            userStatistical.Add(new UserProfileStatisticalsResult()
            {
                StatisticalName = eNegConstant.UserProfileStatisticals.ConversationCount,
                StatisticalValue = 144
            });

            userStatistical.Add(new UserProfileStatisticalsResult()
            {
                StatisticalName = eNegConstant.UserProfileStatisticals.Application_Avg_Count,
                StatisticalValue = 5
            });


            GetUserProfileStatisticalsComplete(this, new eNegEntityResultArgs<UserProfileStatisticalsResult>(userStatistical));
        }

        /// <summary>
        /// Determines whether this instance can user see member profile or not.
        /// </summary>
        /// <param name="userID">The user ID.</param>
        public void CanUserSeeMemberProfileAsync(Guid userID)
        {
            
        }

        /// <summary>
        /// Rejects the changes.
        /// </summary>
        public void RejectChanges()
        {
            _users[0].NewEmail = string.Empty;
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
