#region → Usings   .
using System;
using System.ComponentModel.Composition;
using System.Linq;
using System.Windows;
using citPOINT.eNeg.Common;
using citPOINT.eNeg.Data.Web;
using citPOINT.eNeg.ViewModel;
using GalaSoft.MvvmLight.Messaging;
using Microsoft.Silverlight.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
#endregion

#region → History  .

/* Date         User            Change
 * 
 * 20.10.10    M.Wahab     creation
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
    /// This class is used to test all operation related to UpdateUserProfile ,Conversation 
    /// And Messages,And Application Activation Dectivation
    /// </summary>
    [TestClass]
    public class UpdateUserProfileVMTest : SilverlightTest
    {
        #region → Fields         .

        private UpdateUserProfileViewModel UpdateUserProfilevm;
        private string ErrorMessage;

        #endregion Fields

        #region → Properties     .
        /// <summary>
        /// View Model Object
        /// </summary>
        [Import(ViewModelTypes.UpdateUserProfileViewModel)]
        public UpdateUserProfileViewModel TheVM
        {
            get { return UpdateUserProfilevm; }
            set { UpdateUserProfilevm = value; }
        }

        #endregion Properties

        #region → Constructors   .

        /// <summary>
        /// First Method Like Constructor
        /// </summary>
        [TestInitialize]
        public void BuidUp()
        {
            CompositionInitializer.SatisfyImports(this);

            #region " Registeration for needed messages in eNegMessenger "
            // register for RaiseErrorMessage
            eNegMessanger.RaiseErrorMessage.Register(this, OnRaiseErrorMessage);
            // register for PleaseConfirmMessage
            eNegMessanger.ConfirmMessage.Register(this, OnConfirmMessage);
            #endregion

        }



        #endregion Constructor

        #region → Commands       .


        /// <summary>
        /// Cancels the change command.
        /// </summary>
        [Asynchronous]
        [TestMethod]
        public void CancelChangeCommand()
        {
            EnqueueCallback(() => TheVM.GetUserByID( AppConfigurations.CurrentLoginUser.UserID));

            TheVM.CurrentUser.NewEmail = "Pops";

            //Run The Command Of Cancel Change Command
            TheVM.CancelChangeCommand.Execute(null);


            EnqueueCallback(() => Assert.IsTrue(string.IsNullOrEmpty(ErrorMessage), string.Concat("Error Message was recieved: ", ErrorMessage)));
            EnqueueCallback(() => Assert.IsTrue(string.IsNullOrEmpty(TheVM.CurrentUser.NewEmail), "No AddNewUpdateUserProfileCommand Found"));

            EnqueueTestComplete();
        }

        /// <summary>
        /// Submits the change command.
        /// </summary>
        [Asynchronous]
        [TestMethod]
        public void SubmitChangeCommand()
        {
            //Run The Command Of Add New Conversation Command
            TheVM.CurrentUser.CheckForNewEmailAddress = true;
            TheVM.CurrentUser.NewEmailConfirmation = "eNeg2010@yahoo.com";
            TheVM.CurrentUser.NewEmail = "eNeg2010@yahoo.com";

        // TheVM.SubmitChangeCommand.Execute(null);

            EnqueueTestComplete();
        }



        #endregion Command

        #region → Methods        .

        #region → Private        .


        #region " RaiseErrorMessage "

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
                    ErrorMessage = ex.Message;

                //MessageBox.Show(ErrorMessage, "Error", MessageBoxButton.OK);
            }
        }

        #endregion "RaiseErrorMessage"


        #region " ConfirmMessage "

        /// <summary>
        /// Display Confirmation Message and resent back the result choosen 
        /// </summary>
        /// <param name="dialogMessage">dialogMessage</param>
        private void OnConfirmMessage(DialogMessage dialogMessage)
        {
            if (dialogMessage != null)
            {
               dialogMessage.Callback(MessageBoxResult.OK);
           }
        }

        #endregion "PleaseConfirmMessage"

        #endregion Private

        #region → Public         .

        /// <summary>
        /// Tests the basics.
        /// </summary>
        [TestMethod]
        public void TestBasics()
        {
            Assert.IsNotNull(UpdateUserProfilevm, "Failed to retrieve the ViewModel Via MEF");
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
        /// Checks the is email exist.
        /// </summary>
        /// <param name="_user">The _user.</param>
        public void CheckIsEmailExist(User _user)
        {
            EnqueueCallback(() => TheVM.CheckIsEmailExist(_user));
            EnqueueConditional(() => TheVM.CurrentUser != null);
            EnqueueCallback(() => Assert.IsTrue(string.IsNullOrEmpty(ErrorMessage), string.Concat("An error message was received: ", ErrorMessage)));
            EnqueueCallback(() => Assert.IsTrue(TheVM.CurrentUser != null, "Could not set the current User"));
            EnqueueTestComplete();
        }


        /// <summary>
        /// Gets the user by ID.
        /// </summary>
        [Asynchronous]
        [TestMethod]
        public void GetUserByID()
        {
            TheVM.CurrentUser = null;
            EnqueueCallback(() => TheVM.GetUserByID(AppConfigurations.ProfileUserID));
            EnqueueCallback(() => Assert.IsTrue(string.IsNullOrEmpty(ErrorMessage), string.Concat("An error message was received: ", ErrorMessage)));
            EnqueueCallback(() => Assert.IsTrue(TheVM.CurrentUser != null, "Could not set the current User"));
            EnqueueTestComplete();
        }


        /// <summary>
        /// Tests the check is email exist.
        /// </summary>
        [Asynchronous]
        [TestMethod]
        public void TestCheckIsEmailExist()
        {
            User newUser = new User()
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

            CheckIsEmailExist(newUser);
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