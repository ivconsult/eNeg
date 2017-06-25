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
 * 17.07.11    Yousra Reda      creation & Test All methods
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
    /// This is a test class for LoginModel and ViewModel
    /// </summary>
    [TestClass]
    public class LoginViewModel_Test
    {

        #region → Fields         .

        private LogInViewModel loginvm;
        private string ErrorMessage;

        #endregion

        #region → Properties     .

        /// <summary>
        /// Gets or sets the VM.
        /// </summary>
        /// <value>The VM.</value>
        public LogInViewModel TheVM
        {
            get { return loginvm; }
            set { loginvm = value; }
        }

        #endregion Properties

        #region → Constructor    .
        /// <summary>
        /// Test_s the intialization_ pass.
        /// </summary>
        [TestInitialize]
        public void Test_Intialization_Pass()
        {
            TheVM = new LogInViewModel(new MockLoginModel());

            //Initialize CurentUser That is supposed to be trying to login using Login page.
            TheVM.CurrentUser = new LoginUser()
                {
                    UserID = Guid.NewGuid(),
                    EmailAddress = "Yousra.reda@gmail.com",
                    Password = "P@ssword1234",
                    NewPassword = "P@ssword1234",
                    Locked = false,
                    IPAddress = "172.0.0.1",
                    LastLoginDate = DateTime.Now,
                    CreateDate = DateTime.Now,
                    Online = false,
                    Disabled = false,
                    FirstName = "Yousra",
                    LastName = "Reda",
                };

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

        #endregion

        #region → Public         .

        /// <summary>
        /// Test the view model existance have instance.
        /// </summary>
        [TestMethod]
        public void TestVM_Existance_HaveInstance()
        {
            Assert.IsNotNull(TheVM, "Failed to retrieve the View Model");
        }

        /// <summary>
        /// Login_s the succeeded.
        /// </summary>
        [TestMethod]
        public void Login_Succeeded()
        {
            TheVM.LoginAsync();
        }

        /// <summary>
        /// Logout_s the succeeded.
        /// </summary>
        [TestMethod]
        public void Logout_Succeeded()
        {
            TheVM.LogoutAsync();
            Assert.IsTrue(string.IsNullOrEmpty(ErrorMessage), string.Concat("Error Message was recieved: ", ErrorMessage));
            Assert.IsTrue(!TheVM.IsLoginDone, "Logout operation doesn't completed");
        }
        #endregion  Public

        #endregion Methods
    }
}
