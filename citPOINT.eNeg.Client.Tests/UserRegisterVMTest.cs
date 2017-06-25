
#region → Usings   .
using System;
using System.ComponentModel.Composition;
using System.Linq;
using citPOINT.eNeg.Common;
using citPOINT.eNeg.ViewModel;
using Microsoft.Silverlight.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using citPOINT.eNeg.Data.Web;
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
    /// Class to test the User Registeration Process.
    /// </summary>
    [TestClass]
    public class UserRegisterVMTest : SilverlightTest
    {

        #region → Fields         .

        private UserRegisterViewModel usrRegistervm;
        private string ErrorMessage;

        #endregion Fields
        
        #region → Properties     .

        /// <summary>
        /// Gets or sets the User rigistration view model VM.
        /// </summary>
        /// <value>The VM.</value>
        [Import(ViewModelTypes.UserRegisterViewModel)]
        public UserRegisterViewModel TheVM
        {
            get { return usrRegistervm; }
            set { usrRegistervm = value; }
        }

        #endregion properties
        
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
            Assert.IsNotNull(usrRegistervm, "Failed to retrieve the viewmodel via MEF");
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
        /// Tests the type of the get account.
        /// </summary>
        [Asynchronous]
        [TestMethod]
        public void TestGetAccountType()
        {
            EnqueueCallback(() => TheVM.GetAccountTypeAsync());
            EnqueueCallback(() => Assert.IsTrue(string.IsNullOrEmpty(ErrorMessage), string.Concat("Error Message was recieved: ", ErrorMessage)));
            EnqueueCallback(() => Assert.IsTrue(TheVM.AccountTypeEntries.Count() > 0, "No Account Types Found"));
            EnqueueTestComplete();
        }

        /// <summary>
        /// Tests the get country.
        /// </summary>
        [Asynchronous]
        [TestMethod]
        public void TestGetCountry()
        {
            EnqueueCallback(() => TheVM.GetCountryAsync());
            EnqueueCallback(() => Assert.IsTrue(string.IsNullOrEmpty(ErrorMessage), string.Concat("Error Message was recieved: ", ErrorMessage)));
            EnqueueCallback(() => Assert.IsTrue(TheVM.CountryEntries.Count() > 0, "No Countries Found"));
            EnqueueTestComplete();
        }



        /// <summary>
        /// Tests the get Cultures.
        /// </summary>
        [Asynchronous]
        [TestMethod]
        public void GetCultures()
        {
            EnqueueCallback(() => TheVM.GetCultureAsync());
            EnqueueCallback(() => Assert.IsTrue(string.IsNullOrEmpty(ErrorMessage), string.Concat("Error Message was recieved: ", ErrorMessage)));
            EnqueueCallback(() => Assert.IsTrue(TheVM.CultureEntries.Count() > 0, "No Cultures Found"));
            EnqueueTestComplete();
        }
        /// <summary>
        /// Tests the get prefered language.
        /// </summary>
        [Asynchronous]
        [TestMethod]
        public void TestGetPreferedLanguage()
        {
            EnqueueCallback(() => TheVM.GetPreferedLanguageAsync());
            EnqueueCallback(() => Assert.IsTrue(string.IsNullOrEmpty(ErrorMessage), string.Concat("Error Message was recieved: ", ErrorMessage)));
            EnqueueCallback(() => Assert.IsTrue(TheVM.PreferedLanguageEntries.Count() > 0, "No Prefered Languages Found"));
            EnqueueTestComplete();
        }

        /// <summary>
        /// Testts the get security question.
        /// </summary>
        [Asynchronous]
        [TestMethod]
        public void TesttGetSecurityQuestion()
        {
            EnqueueCallback(() => TheVM.GetSecurityQuestionAsync());
            EnqueueCallback(() => Assert.IsTrue(string.IsNullOrEmpty(ErrorMessage), string.Concat("Error Message was recieved: ", ErrorMessage)));
            EnqueueCallback(() => Assert.IsTrue(TheVM.SecurityQuestionEntries.Count() > 0, "No Secuirity Question Found"));
            EnqueueTestComplete();
        }

         
        #endregion Public

        #endregion Methods
    }
}
