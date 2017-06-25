

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
 * 21.08.11    M.Wahab     creation & Test All methods
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
    public class PublishedProfileDetailsViewModel_Test
    {
        #region → Fields         .

        private PublishedProfileDetailsViewModel PublishedProfileDetailsvm;
        private string ErrorMessage;

        private string mScreenName = "";

        #endregion Fields

        #region → Properties     .

        /// <summary>
        /// View Model Object
        /// </summary>
        public PublishedProfileDetailsViewModel TheVM
        {
            get { return PublishedProfileDetailsvm; }
            set { PublishedProfileDetailsvm = value; }
        }

        #endregion Properties

        #region → Constructors   .

        /// <summary>
        /// First Method Like Constructor
        /// </summary>
        [TestInitialize]
        public void BuildUp()
        {
            TheVM = new PublishedProfileDetailsViewModel(new MockPublishedProfileDetailsModel());

            TheVM.GetUserByID(AppConfigurations.ProfileUserID);

            #region " Registeration for needed messages in eNegMessenger "
            // register for RaiseErrorMessage
            eNegMessanger.RaiseErrorMessage.Register(this, OnRaiseErrorMessage);
           
            // register for PleaseConfirmMessage
            eNegMessanger.ConfirmMessage.Register(this, OnConfirmMessage);


            eNegMessanger.ChangeScreenMessage.Register(this, OnChangeScreen);

            eNegMessanger.FlippMessage.Register(this, OnChangeScreen);

            #endregion

        }

        #endregion Constructor

        #region → Commands       .

        /// <summary>
        /// Tests the link for organization command.
        /// </summary>
        [TestMethod]
        public void TestLink_ForOrganization_Command()
        {
            TheVM.IsForManageOrganization=true;
                       
            //Run The Command Of Cancel Change Command
            TheVM.LinkClickedCommand.Execute(null);

            Assert.IsTrue(string.IsNullOrEmpty(ErrorMessage), string.Concat("Error Message was recieved: ", ErrorMessage));

            Assert.IsTrue(!string.IsNullOrEmpty(mScreenName) && mScreenName==ViewTypes.OrganizationManagement, "Navigate for Organization management failed");

        }

        /// <summary>
        /// Tests the link for public profile command.
        /// </summary>
        [TestMethod]
        public void TestLink_ForPublicProfile_Command()
        {
            TheVM.IsForManageOrganization = false;

            //Run The Command Of Cancel Change Command
            TheVM.LinkClickedCommand.Execute(null);

            Assert.IsTrue(string.IsNullOrEmpty(ErrorMessage), string.Concat("Error Message was recieved: ", ErrorMessage));

            Assert.IsTrue(!string.IsNullOrEmpty(mScreenName) && mScreenName == ViewTypes.PublishedProfiles, "Navigate for Public Profile failed");

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

        /// <summary>
        /// Called when [change screen].
        /// </summary>
        /// <param name="screenName">Name of the screen.</param>
        private void OnChangeScreen(string screenName)
        {
            mScreenName = screenName;
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
            Assert.IsNotNull(PublishedProfileDetailsvm, "Failed to retrieve the View Model");
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
