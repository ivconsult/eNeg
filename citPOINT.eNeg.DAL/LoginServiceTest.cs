


#region → Usings   .
using System;
using System.ServiceModel.DomainServices.Client;
using citPOINT.eNeg.Data.Web;
using Microsoft.VisualStudio.TestTools.UnitTesting;
#endregion

#region → History  .

/* Date         User              Change
 * 
 * 19.09.10     Yousra Reda       Creation
 */

# endregion

#region → ToDos    .

/*
 * Date         set by User     Description
 * 
 * 
*/
# endregion

namespace citPOINT.eNeg.Data.WebTest
{
    [TestClass()]
    public class LoginServiceTest :ILoginServiceTest
    {

        #region → Fields         .
        private TestContext testContextInstance;
        private LogInContext mLoginContext;
        private LoginUser mUser;
        #endregion Fields


        #region → Properties     .

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        /// <summary>
        /// instance of LoginContext which wrap inside loginService of eNeg to can use available services
        /// </summary>
        public LogInContext LoginContext
        {
            get
            {
                if (mLoginContext == null)
                {
                    mLoginContext = new LogInContext(new Uri("http://localhost:9000/citPOINT-eNeg-Data-Web-LogInService.svc", UriKind.Absolute));
                }
                return mLoginContext;
            }

        }
        #endregion Properties      


        #region → Constructors   .
        public LoginServiceTest()
        {
            UserObj();
        }
        #endregion Constructor


        #region → Methods        .

        #region Generate Mock

        /// <summary>
        /// Initialize mUser with new login user
        /// </summary>
        /// <returns>New Login User</returns>
        public LoginUser UserObj()
        {
            if (mUser == null)
            {
                mUser = new LoginUser()
                {
                    UserID = Guid.NewGuid(),
                    EmailAddress = "yousra.reda@gmail.com",
                    Password = "123456",
                    Locked = false,
                    IPAddress = "10.0.02.2",
                    LastLoginDate = DateTime.Now,
                    Online = false,
                    Disabled = false,
                };
            }
            return mUser;
        }
        #endregion Generate Mock

        /// <summary>
        /// Test Get user operation which is responsible for Authenicate user in case of choosing "Remember Me" option
        /// </summary>
        [TestMethod]
        public void GetUser()
        {
            LoginContext.Load<LoginUser>(LoginContext.GetUserQuery(), LoadBehavior.RefreshCurrent, s =>
            {
                if (s.HasError)
                {
                    Assert.Fail("Fail to Authenticate User\r\n" + s.Error.Message);
                }
            }, true);
        }

        /// <summary>
        /// Test Logout operaton used in login service
        /// </summary>
        [TestMethod]
        public void Logout()
        {
            LoginContext.Load<LoginUser>(LoginContext.LogoutQuery(), LoadBehavior.RefreshCurrent, s =>
            {
                if (s.HasError)
                {
                    Assert.Fail("Fail to logout\r\n" + s.Error.Message);
                }
            }, true);
        }

        /// <summary>
        /// Test Login operation used in LoginService
        /// </summary>
        [TestMethod]
        public void Login()
        {
            LoginContext.Load<LoginUser>(LoginContext.LoginQuery("yousra.reda@gmail.com", "123456", false, ""), LoadBehavior.RefreshCurrent, s =>
                {
                    if (s.HasError)
                    {
                        Assert.Fail("Fail to login\r\n" + s.Error.Message);
                    }
                }, true);
        }

        #endregion Methods
    }
}
