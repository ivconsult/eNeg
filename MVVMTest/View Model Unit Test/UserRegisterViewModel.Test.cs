#region → Usings   .
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using citPOINT.eNeg.Common;
using citPOINT.eNeg.ViewModel;
using GalaSoft.MvvmLight.Messaging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using citPOINT.eNeg.Data.Web;
#endregion

#region → History  .

/* Date         User            Change
 * 
 * 14.07.11    Yousra Reda      creation & Test All methods
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
    /// Class to test the User Registeration Process.
    /// </summary>
    [TestClass]
    public class UserRegisterViewModel_Test
    {
        #region → Fields         .

        private UserRegisterViewModel UserRegistervm;
        private string ErrorMessage;

        #region → ListOfUsersCanBeUsedHere  .
        public List<User> UsersList = new List<User>() 
        {
            new User()
            {
                UserID = new Guid("6E60D43F-5C70-48D9-8511-2564F58E483E"),
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
                EmailAddress = "M.Wahab@gmail.com",
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
        #endregion

        #endregion Fields

        #region → Properties     .

        /// <summary>
        /// Gets or sets the VM.
        /// </summary>
        /// <value>The VM.</value>
        public UserRegisterViewModel TheVM
        {
            get { return UserRegistervm; }
            set { UserRegistervm = value; }
        }

        #endregion properties

        #region → Constructors   .

        /// <summary>
        /// First Method Like Constructor
        /// </summary>
        [TestInitialize]
        public void Test_Intialization_Pass()
        {
            TheVM = new UserRegisterViewModel(new MockRegisterModel());
            TheVM.ManageUserOrgViewModel = new ManageUserOrganizationViewModel(new MockManageUserOrganizationModel());

            //Initialize Current User
            TheVM.CurrentUser = UsersList[0];

            #region → Registeration for needed messages in eNegMessenger
            // register for RaiseErrorMessage
            eNegMessanger.RaiseErrorMessage.Register(this, OnRaiseErrorMessage);
            #endregion
        }
        #endregion

        #region → Methods        .

        #region → Private        .

        #region → Raise Error Message  .

        /// <summary>
        /// Raise error message if there is any layer send RaiseErrorMessage
        /// </summary>
        /// <param name="ex">exception to raise</param>
        private void OnRaiseErrorMessage(Exception ex)
        {
            if (ex != null)
            {
                if (ex.InnerException != null)
                {
                    ErrorMessage = ex.Message + "\r\n" + ex.InnerException.Message;
                }
                else
                {
                    ErrorMessage = ex.Message;
                }
            }
        }

        #endregion

        /// <summary>
        /// Checks the email exist and found.
        /// </summary>
        /// <param name="_user">The _user.</param>
        private void CheckEmailExistAndFound(User _user)
        {
            TheVM.CheckIsEmailExist(_user);
            Assert.IsTrue(string.IsNullOrEmpty(ErrorMessage), string.Concat("An error message was received: ", ErrorMessage));
            Assert.IsTrue(TheVM.CurrentUser.UserRole.Count == 0, "Email address doesn't used before");
        }

        /// <summary>
        /// Checks the email exist but not found.
        /// </summary>
        /// <param name="_user">The _user.</param>
        private void CheckEmailExistButNotFound(User _user)
        {
            TheVM.CheckIsEmailExist(_user);
            Assert.IsTrue(string.IsNullOrEmpty(ErrorMessage), string.Concat("An error message was received: ", ErrorMessage));
            Assert.IsTrue(TheVM.CurrentUser.UserRole.Count > 0, "Email address has been found");
        }

        #endregion

        #region → Public         .

        /// <summary>
        /// Get_s the account type_ return collection.
        /// </summary>
        [TestMethod]
        public void Get_AccountType_ReturnCollection()
        {
            TheVM.GetAccountTypeAsync();
            Assert.IsTrue(string.IsNullOrEmpty(ErrorMessage), string.Concat("Error Message was recieved: ", ErrorMessage));
            Assert.IsTrue(TheVM.AccountTypeEntries.Count() > 0, "No Account Types Found");
        }

        /// <summary>
        /// Get_s the country_ return collection.
        /// </summary>
        [TestMethod]
        public void Get_Country_ReturnCollection()
        {
            TheVM.GetCountryAsync();
            Assert.IsTrue(string.IsNullOrEmpty(ErrorMessage), string.Concat("Error Message was recieved: ", ErrorMessage));
            Assert.IsTrue(TheVM.CountryEntries.Count() > 0, "No Countries Found");
        }

        /// <summary>
        /// Tests the get Cultures.
        /// </summary>
        [TestMethod]
        public void GetCultures_WithoutCondition_ReturnCollection()
        {
            TheVM.GetCultureAsync();
            Assert.IsTrue(string.IsNullOrEmpty(ErrorMessage), string.Concat("Error Message was recieved: ", ErrorMessage));
            Assert.IsTrue(TheVM.CultureEntries.Count() > 0, "No Cultures Found");
        }

        /// <summary>
        /// Get_s the prefered language_ return collection.
        /// </summary>
        [TestMethod]
        public void Get_PreferedLanguage_ReturnCollection()
        {
            TheVM.GetPreferedLanguageAsync();
            Assert.IsTrue(string.IsNullOrEmpty(ErrorMessage), string.Concat("Error Message was recieved: ", ErrorMessage));
            Assert.IsTrue(TheVM.PreferedLanguageEntries.Count() > 0, "No PreferedLanguage Found");
        }

        /// <summary>
        /// Get_s the security question_ return collection.
        /// </summary>
        [TestMethod]
        public void Get_SecurityQuestion_ReturnCollection()
        {
            TheVM.GetSecurityQuestionAsync();
            Assert.IsTrue(string.IsNullOrEmpty(ErrorMessage), string.Concat("Error Message was recieved: ", ErrorMessage));
            Assert.IsTrue(TheVM.SecurityQuestionEntries.Count() > 0, "No SecurityQuestion Found");
        }

        /// <summary>
        /// Check_s the is mail exist_ already exist.
        /// </summary>
        [TestMethod]
        public void Check_IsMailExist_AlreadyExist()
        {
            CheckEmailExistAndFound(UsersList[0]);
        }

        /// <summary>
        /// Check_s the is mail exist_ not exist.
        /// </summary>
        [TestMethod]
        public void Check_IsMailExist_NotExist()
        {
            CheckEmailExistButNotFound(UsersList[1]);
        }

        /// <summary>
        /// Used To Clean All Resources
        /// </summary>
        [TestCleanup]
        public void CleanUp()
        {
            // call Cleanup on its ViewModel
            TheVM.Cleanup();

            // Cleanup itself
            Messenger.Default.Unregister(this);
        }

        #endregion Public

        #endregion Methods
    }
}
