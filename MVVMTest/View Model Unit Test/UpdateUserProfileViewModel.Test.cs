

#region → Usings   .
using System;
using System.Linq;
using System.Windows;
using citPOINT.eNeg.Common;
using citPOINT.eNeg.ViewModel;
using GalaSoft.MvvmLight.Messaging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
#endregion

#region → History  .

/* Date         User            Change
 * 
 * 11.07.11    M.Wahab     creation & Test All methods
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
    /// Update User Profile View Model Test class
    /// </summary>
    [TestClass]
    public class UpdateUserProfileViewModel_Test
    {
        #region → Fields         .

        private UpdateUserProfileViewModel UpdateUserProfilevm;
        private string ErrorMessage;

        #endregion Fields

        #region → Properties     .

        /// <summary>
        /// View Model Object
        /// </summary>
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
        public void BuildUp()
        {
            TheVM = new UpdateUserProfileViewModel(new MockUpdateUserProfileModel());
            TheVM.ManageUserOrgViewModel = new ManageUserOrganizationViewModel(new MockManageUserOrganizationModel());

            TheVM.GetUserByID(AppConfigurations.ProfileUserID);

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
        [TestMethod]
        public void Test_Cancel_Changes_Command()
        {
            TheVM.GetUserByID(AppConfigurations.CurrentLoginUser.UserID);

            TheVM.CurrentUser.NewEmail = "Pops";

            //Run The Command Of Cancel Change Command
            TheVM.CancelChangeCommand.Execute(null);

            Assert.IsTrue(string.IsNullOrEmpty(ErrorMessage), string.Concat("Error Message was recieved: ", ErrorMessage));
            Assert.IsTrue(string.IsNullOrEmpty(TheVM.CurrentUser.NewEmail), "Cancel Changes Command faild");
        }

        #endregion Command

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
                    ErrorMessage = ex.Message;

                //MessageBox.Show(ErrorMessage, "Error", MessageBoxButton.OK);
            }
        }

        #endregion

        #region → Confirm Message      .

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

        #endregion

        #endregion

        #region → Public         .

        /// <summary>
        /// Tests the basics.
        /// </summary>
        [TestMethod]
        public void TestVM_Existance_HaveInstance()
        {
            Assert.IsNotNull(UpdateUserProfilevm, "Failed to retrieve the View Model");
        }

        /// <summary>
        /// Tests the get country.
        /// </summary>
        [TestMethod]
        public void GetCountries_WithoutCondition_ReturnCollection()
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
        /// Tests the get prefered language.
        /// </summary>
        [TestMethod]
        public void GetPreferedLanguages_WithoutCondition_ReturnCollection()
        {
            TheVM.GetPreferedLanguageAsync();
            Assert.IsTrue(string.IsNullOrEmpty(ErrorMessage), string.Concat("Error Message was recieved: ", ErrorMessage));
            Assert.IsTrue(TheVM.PreferedLanguageEntries.Count() > 0, "No Prefered Languages Found");

        }
        
        /// <summary>
        /// Gets the user by ID.
        /// </summary>
        [TestMethod]
        public void GetUserByID_PassingUserID_ReturnUser()
        {
            TheVM.CurrentUser = null;
            TheVM.GetUserByID(AppConfigurations.ProfileUserID);
            Assert.IsTrue(string.IsNullOrEmpty(ErrorMessage), string.Concat("An error message was received: ", ErrorMessage));
            Assert.IsTrue(TheVM.CurrentUser != null, "Could not set the current User");
        }

        /// <summary>
        /// Gets the user by ID.
        /// </summary>
        [TestMethod]
        public void GetUserByID_PassingNullUserID_ReturnNull()
        {
            TheVM.CurrentUser = null;
            TheVM.GetUserByID(Guid.Empty);
            Assert.IsTrue(string.IsNullOrEmpty(ErrorMessage), string.Concat("An error message was received: ", ErrorMessage));
            Assert.IsNull(TheVM.CurrentUser, "Failed to Get User By ID and Passing Null User ID");

        }

        /// <summary>
        /// Gets the user by ID.
        /// </summary>
        [TestMethod]
        public void GetUserByID_PassingNoneExistUserID_ReturnNull()
        {
            TheVM.CurrentUser = null;
            TheVM.GetUserByID(Guid.NewGuid());
            Assert.IsTrue(string.IsNullOrEmpty(ErrorMessage), string.Concat("An error message was received: ", ErrorMessage));
            Assert.IsNull(TheVM.CurrentUser, "Failed to Get User By ID and Passing None exist User ID");

        }
        
        /// <summary>
        /// Check mail repeated_ send_ none_ repeated_ result_ mail_ not_ repeated.
        /// </summary>
        [TestMethod]
        public void CheckMailRepeated_SendNoneRepeated_ResultMailNotRepeated()
        {
            TheVM.CheckIsEmailExist(TheVM.CurrentUser);

            Assert.IsTrue(TheVM.CurrentUser != null);
            Assert.IsTrue(string.IsNullOrEmpty(ErrorMessage), string.Concat("An error message was received: ", ErrorMessage));
            Assert.IsTrue(TheVM.CurrentUser.UserOperations.Count > 0, "Faild to Check Mail with sending None Repeated Mail");

        }
        
        /// <summary>
        /// Check the mail repeated send repeated mail result mail repeated.
        /// </summary>
        [TestMethod]
        public void CheckMailRepeated_SendRepeatedMail_ResultMailRepeated()
        {
            
            TheVM.CurrentUser.EmailAddress = "Existmail@gmail.com";
            TheVM.CheckIsEmailExist(TheVM.CurrentUser);

            Assert.IsTrue(TheVM.CurrentUser != null);
            Assert.IsTrue(string.IsNullOrEmpty(ErrorMessage), string.Concat("An error message was received: ", ErrorMessage));
            Assert.IsTrue(TheVM.CurrentUser.UserOperations.Count == 0, "Faild to Check Mail with sending Repeated Mail");

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
