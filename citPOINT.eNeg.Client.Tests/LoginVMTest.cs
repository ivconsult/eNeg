
#region → Usings   .
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Silverlight.Testing;
using citPOINT.eNeg.ViewModel;
using System.ComponentModel.Composition;
using citPOINT.eNeg.Common;
using System;
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

namespace citPOINT.eNeg.MVVM.Test
{
    /// <summary>
    ///This is a test class for LoginModel and ViewModel
    ///</summary>
    [TestClass]
    public class LoginVMTest : SilverlightTest
    {

        #region → Fields         .

        private LogInViewModel mLogInViewModel;
        private string ErrorMessage;

        #endregion Fields
        
        #region → Properties     .


        #region Use MEF To load the View Model

        /// <summary>
        /// Gets or sets the log in view model.
        /// </summary>
        /// <value>The log in view model.</value>
        [Import(ViewModelTypes.LoginFormViewModel)]
        public LogInViewModel LogInViewModel
        {
            get { return mLogInViewModel; }
            set { mLogInViewModel = value; }
        }


        #endregion 

        #endregion
        
        #region → Methods        .

        #region → Public         .

        /// <summary>
        /// Buids up.
        /// </summary>
        [TestInitialize]
        public void BuidUp()
        {
            CompositionInitializer.SatisfyImports(this);
        }

        /// <summary>
        /// Tests the basics.
        /// </summary>
        [TestMethod]
        public void TestBasics()
        {
            Assert.IsNotNull(mLogInViewModel, "Failed to retrieve the viewmodel via MEF");
            eNegMessanger.RaiseErrorMessage.Register(this, OnRaiseErrorMessage);
        }

        /// <summary>
        /// Called when [raise error message].
        /// </summary>
        /// <param name="ex">The ex.</param>
        public void OnRaiseErrorMessage(Exception ex)
        {
            ErrorMessage = ex.Message;
        }

        /// <summary>
        /// Logins this instance.
        /// </summary>
        [Asynchronous]
        [TestMethod]
        [Ignore]
        public void Login()
        {
            EnqueueCallback(() => LogInViewModel.LoginAsync());
            EnqueueCallback(() => Assert.IsTrue(string.IsNullOrEmpty(ErrorMessage), string.Concat("Error Message was recieved: ", ErrorMessage)));
            EnqueueCallback(() => Assert.IsTrue(LogInViewModel.IsLoginDone, "User Cannot Login"));
            EnqueueTestComplete();
        }



        /// <summary>
        /// Logouts this instance.
        /// </summary>
        [Asynchronous]
        [TestMethod]
        public void Logout()
        {
            EnqueueCallback(() => LogInViewModel.LogoutAsync());
            EnqueueCallback(() => Assert.IsTrue(string.IsNullOrEmpty(ErrorMessage), string.Concat("Error Message was recieved: ", ErrorMessage)));
            EnqueueCallback(() => Assert.IsTrue(!LogInViewModel.IsLoginDone, "User Cannot Logout"));
            EnqueueTestComplete();
        }

       
        #endregion Public

        #endregion Methods
    }
}
