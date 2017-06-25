
#region → Usings   .
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Windows;
using citPOINT.eNeg.Common;
using citPOINT.eNeg.Data.Web;
using System.ServiceModel.DomainServices.Client;
#endregion

#region → History  .

/* Date         User            Change
 * 
 * 10.08.10     Yousra Reda     creation
 */

# endregion

#region → ToDos    .

/*
 * Date         set by User     Description
 * 
 * 
*/

# endregion


namespace citPOINT.eNeg.MVVM.Tests
{
    /// <summary>
    /// Mock of Register Model
    /// </summary>
    [Export(typeof(IUserRegisterModel))]
    public class MockRegisterModel : IUserRegisterModel
    {
        #region → Fields         .
        List<User> _users;
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
        /// Initializes a new instance of the <see cref="MockRegisterModel"/> class.
        /// </summary>
        public MockRegisterModel()
        {
            _users = new List<User>()
                            {
                               new User()
                                {
                                    UserID = Guid.NewGuid(),
                                    EmailAddress = "Yousra.Reda@gmail.com",
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
                                    EmailAddress = "TestUser@gmail.com",
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
        #endregion Constructor

        #region → Events         .

        /// <summary>
        /// Occurs when [get culture complete].
        /// </summary>
        public event EventHandler<eNegEntityResultArgs<Culture>> GetCultureComplete;
        /// <summary>
        /// Event Handler For Method GetAccountTypeAsync
        /// </summary>
        public event EventHandler<eNegEntityResultArgs<AccountType>> GetAccountTypeComplete;
        /// <summary>
        /// Event Handler For Method GetCountryAsync
        /// </summary>
        public event EventHandler<eNegEntityResultArgs<Country>> GetCountryComplete;
        /// <summary>
        /// Event Handler For Method GetPreferedLanguageAsync
        /// </summary>
        public event EventHandler<eNegEntityResultArgs<PreferedLanguage>> GetPreferedLanguageComplete;
        /// <summary>
        /// Event Handler For Method GetProfileAsync
        /// </summary>
        public event EventHandler<eNegEntityResultArgs<Profile>> GetProfileComplete;
        /// <summary>
        /// Event Handler For Method GetRightAsync
        /// </summary>
        public event EventHandler<eNegEntityResultArgs<Right>> GetRightComplete;
        /// <summary>
        /// Event Handler For Method GeetRoleAsync
        /// </summary>
        public event EventHandler<eNegEntityResultArgs<Role>> GetRoleComplete;
        /// <summary>
        /// Event Handler For Method GetRoleRightAsync
        /// </summary>
        public event EventHandler<eNegEntityResultArgs<RoleRight>> GetRoleRightComplete;
        /// <summary>
        /// Event Handler For Method GetSecurityQuestionAsync
        /// </summary>
        public event EventHandler<eNegEntityResultArgs<SecurityQuestion>> GetSecurityQuestionComplete;
        /// <summary>
        /// Event Handler For Method GetUserRoleAsync
        /// </summary>
        public event EventHandler<eNegEntityResultArgs<UserRole>> GetUserRoleComplete;


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
        /// Event Handler For Method CheckUserRequestReset
        /// this Method is To Check if the Current Link is valid or not
        /// </summary>
        public event EventHandler<eNegEntityResultArgs<User>> GetCheckUserRequestResetComplete;



        /// <summary>
        /// Event Handler For Method UpdateReset
        /// Update Reset Flag So This User Can Enter Direct
        /// to Reset Page In The Next Time Open The Project
        /// </summary>
        public event EventHandler<eNegEntityResultArgs<User>> GetResetRequestComplete;


        /// <summary>
        /// Event Handler For Method SaveChanges
        /// </summary>
        public event EventHandler<SubmitOperationEventArgs> SaveChangesComplete;

        /// <summary>
        /// Event Handler For Method PropertyChanged
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;


        /// <summary>
        /// Event Handler For Method  UpdateUserByConfirmMail
        /// Update Locked Flag By false
        /// </summary>
        public event EventHandler<eNegEntityResultArgs<User>> UpdateUserByConfirmMailComplete;

        /// <summary>
        /// Event Handler For Method DeleteUserOperationByUserID
        /// </summary>
        public event EventHandler<eNegEntityResultArgs<User>> DeleteUserOperationByUserIDComplete;
        /// <summary>
        /// Event Handler For Method SendingMail
        /// </summary>
        public event Action<InvokeOperation> SendingMailCompleted;


        /// <summary>
        /// Occurs when [get all application complete].
        /// </summary>
        public event EventHandler<eNegEntityResultArgs<Data.Web.Application>> GetAllApplicationComplete;

        #endregion Events

        #region → Methods        .

        /// <summary>
        /// Gets all culture lookup table.
        /// </summary>
        public void GetCultureAsync()
        {
            if (GetCountryComplete != null)
            {
                Deployment.Current.Dispatcher.BeginInvoke(() =>
                    {
                        GetCultureComplete(this, new eNegEntityResultArgs<Culture>(CultureSource));
                    });
            }
        }

        /// <summary>
        /// Loading All Records of AccountType
        /// </summary>
        public void GetAccountTypeAsync()
        {
            if (GetAccountTypeComplete != null)
            {
                Deployment.Current.Dispatcher.BeginInvoke(() =>
                    {
                        GetAccountTypeComplete(this, new eNegEntityResultArgs<AccountType>(
                            new List<AccountType>()
                            {
                                new AccountType()
                                {
                                    AccountTypeID = Guid.NewGuid(),
                                    TypeName = "Free"
                                },
                                new AccountType()
                                {
                                    AccountTypeID = Guid.NewGuid(),
                                    TypeName = "Premium"
                                }
                            }));
                    });
            }
        }

        /// <summary>
        /// Loading All Records of Country
        /// </summary>
        public void GetCountryAsync()
        {
            if (GetCountryComplete != null)
            {
                Deployment.Current.Dispatcher.BeginInvoke(() =>
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
                    });
            }
        }

        /// <summary>
        /// Loading All Records of PreferedLanguage
        /// </summary>
        public void GetPreferedLanguageAsync()
        {
            if (GetPreferedLanguageComplete != null)
            {
                Deployment.Current.Dispatcher.BeginInvoke(() =>
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
                });
            }
        }

        /// <summary>
        /// Loading All Records of Profile
        /// </summary>
        public void GetProfileAsync()
        {
            if (GetProfileComplete != null)
            {
                Deployment.Current.Dispatcher.BeginInvoke(() =>
                {
                    GetProfileComplete(this, new eNegEntityResultArgs<Profile>(
                        new List<Profile>()
                            {
                                new Profile()
                                {
                                    ProfileID = Guid.NewGuid(),
                                    CurrentTheme = "Silver",
                                    UserID = Guid.NewGuid()
                                },
                                new Profile()
                                {
                                    ProfileID = Guid.NewGuid(),
                                    CurrentTheme = "LightBlue",
                                    UserID = Guid.NewGuid()
                                }
                            }));
                });
            }
        }

        /// <summary>
        /// Loading All Records of Right
        /// </summary>
        public void GetRightAsync()
        {
            if (GetRightComplete != null)
            {
                Deployment.Current.Dispatcher.BeginInvoke(() =>
                {
                    GetRightComplete(this, new eNegEntityResultArgs<Right>(
                        new List<Right>()
                            {
                                new Right()
                                {
                                    RightID = Guid.NewGuid(),
                                    RightName = "FullControl"
                                },
                                new Right()
                                {
                                    RightID = Guid.NewGuid(),
                                    RightName = "Deny"
                                },
                                new Right()
                                {
                                    RightID = Guid.NewGuid(),
                                    RightName = "Read"
                                }
                            }));
                });
            }
        }

        /// <summary>
        /// Loading All Records of Role
        /// </summary>
        public void GetRoleAsync()
        {
            if (GetRoleComplete != null)
            {
                Deployment.Current.Dispatcher.BeginInvoke(() =>
                {
                    GetRoleComplete(this, new eNegEntityResultArgs<Role>(
                        new List<Role>()
                            {
                                new Role()
                                {
                                    RoleID = eNegConstant.Roles.Admin,
                                    RoleName = "Admin",
                                    RoleDescription = ""
                                },
                                new Role()
                                {
                                    RoleID = eNegConstant.Roles.User,
                                    RoleName = "User",
                                    RoleDescription = ""
                                }
                            }));
                });
            }
        }
        /// <summary>
        /// Loading All Records of RoleRight
        /// </summary>
        public void GetRoleRightAsync()
        {
            if (GetRoleRightComplete != null)
            {
                Deployment.Current.Dispatcher.BeginInvoke(() =>
                {
                    GetRoleRightComplete(this, new eNegEntityResultArgs<RoleRight>(
                        new List<RoleRight>()
                            {
                                new RoleRight()
                                {
                                    RoleRightID = Guid.NewGuid(),
                                    RoleID = eNegConstant.Roles.Admin,
                                    RightID = Guid.NewGuid()
                                },
                                new RoleRight()
                                {
                                    RoleRightID = Guid.NewGuid(),
                                    RoleID = eNegConstant.Roles.User,
                                    RightID = Guid.NewGuid()
                                }
                            }));
                });
            }
        }

        /// <summary>
        /// Loading All Records of SecurityQuestion
        /// </summary>
        public void GetSecurityQuestionAsync()
        {
            if (GetSecurityQuestionComplete != null)
            {
                Deployment.Current.Dispatcher.BeginInvoke(() =>
                {
                    GetSecurityQuestionComplete(this, new eNegEntityResultArgs<SecurityQuestion>(
                        new List<SecurityQuestion>()
                            {
                                new SecurityQuestion()
                                {
                                    SecurityQuestionID = Guid.NewGuid(),
                                    Question = "What is your Favourite color?"
                                },
                                new SecurityQuestion()
                                {
                                    SecurityQuestionID = Guid.NewGuid(),
                                    Question = "What is you first school?"
                                }
                            }));
                });
            }
        }


        /// <summary>
        /// Loading All Records of UserRole
        /// </summary>
        public void GetUserRoleAsync()
        {
            if (GetUserRoleComplete != null)
            {
                Deployment.Current.Dispatcher.BeginInvoke(() =>
                {
                    GetUserRoleComplete(this, new eNegEntityResultArgs<UserRole>(
                        new List<UserRole>()
                            {
                                new UserRole()
                                {
                                    UserRoleID = Guid.NewGuid(),
                                    RoleID = eNegConstant.Roles.Admin,
                                    UserID = Guid.NewGuid()
                                },
                                new UserRole()
                                {
                                    UserRoleID = Guid.NewGuid(),
                                    RoleID = eNegConstant.Roles.Admin,
                                    UserID = Guid.NewGuid()
                                }
                            }));
                });
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
                Deployment.Current.Dispatcher.BeginInvoke(() =>
                {
                    GetCheckIsEmailExistComplete(this, new eNegEntityResultArgs<User>(new List<User>()));
                });
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
                Deployment.Current.Dispatcher.BeginInvoke(() =>
                {
                    GetUserByIDComplete(this, new eNegEntityResultArgs<User>(_users));
                });
            }
        }

        /// <summary>
        /// Update Reset Flag So This User Can Enter Direct
        /// to Reset Page In The Next Time Open The Project
        /// </summary>
        /// <param name="UserID">The User Id ex.ABDBD-SDGH-SDFKS54-NSBNMA</param>
        public void UpdateReset(Guid? UserID)
        {
            if (GetResetRequestComplete != null)
            {
                Deployment.Current.Dispatcher.BeginInvoke(() =>
                {
                    GetResetRequestComplete(this, new eNegEntityResultArgs<User>(_users));
                });
            }
        }

        /// <summary>
        /// Adding New user to the Current Context
        /// </summary>
        /// <param name="SetInContext">Set In Current Context or as New Object only</param>
        /// <returns>New Istance on User</returns>
        public User AddNewUser(bool SetInContext)
        {
            var NewUser = new User()
            {
                UserID = Guid.NewGuid(),
                EmailAddress = "Yousra.Reda@gmail.com",
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
            };
            _users.Add(NewUser);
            return NewUser;
        }


        /// <summary>
        /// Remove the User From The Context
        /// </summary>
        /// <param name="user">the User Wanted to Remove</param>
        public void RemoveUser(User user)
        {
            _users.Remove(user);
        }

        /// <summary>
        /// Sends the mail.
        /// </summary>
        /// <param name="ToMail">To mail.</param>
        /// <param name="Subject">The subject.</param>
        /// <param name="Body">The body.</param>
        public void SendMail(string ToMail, string Subject, string Body)
        {

        }

        /// <summary>
        /// Apply All Changes in current Context to database.
        /// </summary>
        public void SaveChangesAsync()
        {
            if (SaveChangesComplete != null)
            {
                Deployment.Current.Dispatcher.BeginInvoke(() =>
                {
                    SaveChangesComplete(this, new SubmitOperationEventArgs(new Exception("")));
                });
            }
        }


        /// <summary>
        /// Reject all changes happen on current Context
        /// </summary>
        public void RejectChanges()
        {

        }


        /// <summary>
        /// this Method is To Check if the Current Link is valid or not
        /// </summary>
        /// <param name="OperationString">value of query string from your mail</param>
        public void CheckUserRequestReset(string OperationString)
        {
            if (GetCheckUserRequestResetComplete != null)
            {
                Deployment.Current.Dispatcher.BeginInvoke(() =>
                {
                    GetCheckUserRequestResetComplete(this, new eNegEntityResultArgs<User>(_users));
                });
            }
        }


        /// <summary>
        /// For Deletign All Related Records In UserOperation Table
        /// And That in case if one Complete his Reset Login Operation
        /// and He Had not Change His EmailAddress.
        /// </summary>
        /// <param name="UserID">value of The UserID</param>
        public void DeleteUserOperationByUserID(Guid UserID)
        {
            //throw new NotImplementedException();
        }

        /// <summary>
        /// public method that call perform query to Open The User To can be login.
        /// Update Locked Flag By false
        /// </summary>
        /// <param name="OperationString">value of string from userID + EmailAddress</param>
        public void UpdateUserByConfirmMail(string OperationString)
        {
            //throw new NotImplementedException();
        }


        /// <summary>
        /// Sending Reset login Mail to the user
        /// </summary>
        /// <param name="EmailAddress">value of user Email Address</param>
        /// <param name="FirstName">Value of FirstName</param>
        /// <param name="LastName">Value of LastName</param>
        /// <param name="OperationString">value of string from userID + EmailAddress</param>
        public void SendResetMail(string EmailAddress, string FirstName, string LastName, string OperationString)
        {
            //throw new NotImplementedException();
        }

        /// <summary>
        /// Sending Confirmation Message In case one Register for fisrt time or
        /// one reset his mail.
        /// </summary>
        /// <param name="EmailAddress">value of user Email Address</param>
        /// <param name="FirstName">Value of FirstName</param>
        /// <param name="LastName">Value of LastName</param>
        /// <param name="OperationString">value of string from userID + EmailAddress</param>
        public void SendConfiramtionMail(string EmailAddress, string FirstName, string LastName, string OperationString)
        {
            //throw new NotImplementedException();
        }
        /// <summary>
        /// Gets all applicaions.
        /// </summary>
        public void GetAllApplicaions()
        {
            //throw new NotImplementedException();
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
