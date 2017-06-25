

#region → Usings   .
using System;
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
    /// Published Profile Details View Model Test
    /// </summary>
    [TestClass]
    public class ManageOrganizationViewModel_Test
    {
        #region → Fields         .

        private ManageOrganizationViewModel ManageOrganizationvm;
        private string ErrorMessage;
        private string mScreenName = "";

        #endregion Fields

        #region → Properties     .

        /// <summary>
        /// View Model Object
        /// </summary>
        public ManageOrganizationViewModel TheVM
        {
            get { return ManageOrganizationvm; }
            set { ManageOrganizationvm = value; }
        }

        /// <summary>
        /// Gets the user is owner for another organization.
        /// </summary>
        /// <value>The user is owner for another organization.</value>
        private Guid UserIsOwnerForAnotherOrganization
        {
            get
            {
                return Guid.Parse("b8021430-1baf-4262-bb7d-d472380aab1f");
            }
        }

        /// <summary>
        /// Gets the user is free.
        /// </summary>
        /// <value>The user is free.</value>
        private Guid UserIsFree
        {
            get
            {
                return Guid.Parse("8A81D072-61AA-4C46-8BDD-E9B019E47DEF");
            }
        }
        #endregion Properties

        #region → Constructors   .

        /// <summary>
        /// First Method Like Constructor
        /// </summary>
        [TestInitialize]
        public void BuildUp()
        {

            AppConfigurations.CurrentLoginUser = new LoginUser();
            AppConfigurations.CurrentLoginUser.UserID = Guid.Parse("f6287a1f-ecd3-4762-b970-4f3a4a491398");
            AppConfigurations.CurrentLoginUser.FirstName = "mohamed";
            AppConfigurations.CurrentLoginUser.LastName = "Enew";
            AppConfigurations.CurrentLoginUser.EmailAddress = "mohamedenew@hotmail.com";
            AppConfigurations.CurrentLoginUser.IsOrganizationOwner = true;
            AppConfigurations.CurrentLoginUser.OrganizationOwnedID = Guid.Parse("7ffd027f-a11f-4c98-80a7-291b7f4a2baf");

            TheVM = new ManageOrganizationViewModel(new MockManageOrganizationModel());

            TheVM.GetOrganization();
            TheVM.GetOrganizationMembersAsync();
            TheVM.GetOrganizationMembersStatusAsync();

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
        /// Removes the user test command remove success.
        /// </summary>
        [TestMethod]
        public void RemoveUser_TestCommand_RemoveSuccess()
        {
            int countAfterDelete = this.TheVM.OrganizationMembersStatus.Count - 1;


            //Run The Command Of Cancel Change Command
            TheVM.RemoveUserFromOrganizationCommand
                 .Execute(
                            this.TheVM.OrganizationMembersStatus
                                      .Where(ss => ss.UserID == this.UserIsOwnerForAnotherOrganization &&
                                                 ss.OrganizationID == AppConfigurations.CurrentLoginUser.OrganizationOwnedID)
                                      .FirstOrDefault()
                         );


            Assert.IsTrue(string.IsNullOrEmpty(ErrorMessage), string.Concat("Error Message was recieved: ", ErrorMessage));

            Assert.IsTrue(countAfterDelete == this.TheVM.OrganizationMembersStatus.Count(), "Removing member failed");

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
        /// Tests the VM existance have instance.
        /// </summary>
        [TestMethod]
        public void TestVM_Existance_HaveInstance()
        {
            Assert.IsNotNull(ManageOrganizationvm, "Failed to retrieve the View Model Via MEF");
        }

        /// <summary>
        /// Gets the organization by user Id return organization.
        /// </summary>
        [TestMethod]
        public void GetOrganization_ByUserID_ReturnOrganization()
        {
            TheVM.GetOrganization();
            Assert.IsTrue(string.IsNullOrEmpty(ErrorMessage), string.Concat("Error Message was recieved: ", ErrorMessage));
            Assert.IsTrue(TheVM.CurrentOrganization != null, "No Organization Found");
        }

        /// <summary>
        /// Tests the be owner user already owner failed.
        /// </summary>
        [TestMethod]
        public void TestBeOwner_UserAlreadyOwner_Failed()
        {
            UserOrganization OwnerInother = this.TheVM.OrganizationMembersStatus
                                      .Where(ss => ss.UserID == this.UserIsOwnerForAnotherOrganization &&
                                                 ss.OrganizationID == AppConfigurations.CurrentLoginUser.OrganizationOwnedID)
                                      .FirstOrDefault();

            Assert.IsTrue(string.IsNullOrEmpty(ErrorMessage), string.Concat("Error Message was recieved: ", ErrorMessage));

            Assert.IsNotNull(OwnerInother, "Can not find the user");

            Assert.IsTrue(!OwnerInother.CanUserOwnOrganization, "Removing member failed");
        }

        /// <summary>
        /// Tests the be owner user are free success.
        /// </summary>
        [TestMethod]
        public void TestBeOwner_UserAreFree_Success()
        {
            UserOrganization freeUser = this.TheVM.OrganizationMembersStatus
                                      .Where(ss => ss.UserID == this.UserIsFree &&
                                                 ss.OrganizationID == AppConfigurations.CurrentLoginUser.OrganizationOwnedID)
                                      .FirstOrDefault();

            Assert.IsTrue(string.IsNullOrEmpty(ErrorMessage), string.Concat("Error Message was recieved: ", ErrorMessage));

            Assert.IsNotNull(freeUser, "Can not find the user");

            Assert.IsTrue(freeUser.CanUserOwnOrganization, "Removing member failed");
        }

        /// <summary>
        /// Gets the organization members for current user return collection.
        /// </summary>
        [TestMethod]
        public void GetOrganizationMembers_ForCurrentUser_ReturnCollection()
        {
            this.TheVM.GetOrganizationMembersAsync();

            Assert.IsTrue(string.IsNullOrEmpty(ErrorMessage), string.Concat("Error Message was recieved: ", ErrorMessage));

            Assert.IsTrue(TheVM.OrganizationMembers.Count() > 0, "No Organization Members Found");
        }

        /// <summary>
        /// Gets the organization members status for current user return collection.
        /// </summary>
        [TestMethod]
        public void GetOrganizationMembersStatus_ForCurrentUser_ReturnCollection()
        {
            this.TheVM.GetOrganizationMembersStatusAsync();

            Assert.IsTrue(string.IsNullOrEmpty(ErrorMessage), string.Concat("Error Message was recieved: ", ErrorMessage));

            Assert.IsTrue(TheVM.OrganizationMembers.Count() > 0, "No Organization Members Status Found");
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
