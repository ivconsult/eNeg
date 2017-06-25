#region → Usings   .
using System;
using citPOINT.eNeg.Common;
using citPOINT.eNeg.ViewModel;
using GalaSoft.MvvmLight.Messaging;
using Microsoft.VisualStudio.TestTools.UnitTesting;

#endregion

#region → History  .

/* Date         User            Change
 * 
 * 14.07.11    M.Wahab     creation
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
    /// Confirm Mail View Model Test class
    /// </summary>
    [TestClass]
    public class ConfirmMailViewModel_Test
    {
        #region → Fields         .

        private ConfirmMailViewModel ConfirmMailVM;
        private string ErrorMessage;
        private eNegMessage lasteNegMessage = null;

        #endregion Fields

        #region → Properties     .

        /// <summary>
        /// View Model Object
        /// </summary>
        public ConfirmMailViewModel TheVM
        {
            get { return ConfirmMailVM; }
            set { ConfirmMailVM = value; }
        }

        #endregion Properties

        #region → Constructors   .

        /// <summary>
        /// First Method Like Constructor
        /// </summary>
        [TestInitialize]
        public void Test_Intialization_Pass()
        {
            TheVM = new ConfirmMailViewModel(new MockConfirmModel());

            #region " Registeration for needed messages in eNegMessenger "

            // register for RaiseErrorMessage
            eNegMessanger.RaiseErrorMessage.Register(this, OnRaiseErrorMessage);

            eNegMessanger.SendCustomMessage.Register(this, OnUpdateMessage);

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
                    ErrorMessage = ex.Message;

                //MessageBox.Show(ErrorMessage, "Error", MessageBoxButton.OK);
            }
        }

        #endregion

        #region → Update Message       .

        /// <summary>
        /// Called when [update message].
        /// </summary>
        /// <param name="Message">The message.</param>
        private void OnUpdateMessage(eNegMessage Message)
        {
            lasteNegMessage = Message;
        }

        #endregion

        #endregion

        #region → Public         .

        /// <summary>
        /// Test the view model confirm mail Vm existance have instance.
        /// </summary>
        [TestMethod]
        public void TestVM_Existance_HaveInstance()
        {
            Assert.IsNotNull(ConfirmMailVM, "Failed to retrieve the View Model");
        }

        /// <summary>
        /// Test the confirmation by properties confirmation success.
        /// </summary>
        [TestMethod]
        public void TestConfirmation_ByProperties_ConfirmationSuccess()
        {
            lasteNegMessage = null;

            TheVM.OperationString = "1pM86JgdOLUAXihLaEaqRWazGmFOJ6Tum2JGhvTaPFIa/PvgrH/pPtukGaUZUNOvnZBLhJfR2sGNdQGnPR0u8s";

            Assert.IsTrue(lasteNegMessage != null &&
                          lasteNegMessage.MessageType == ImageType.Success,
                          "Failed to Confirm by properties");
        }

        /// <summary>
        /// Test_s the confirmation_ by_ method_ call_ succeed.
        /// </summary>
        [TestMethod]
        public void Test_Confirmation_By_Method_Call_Succeed()
        {
            lasteNegMessage = null;

            TheVM.UpdateUserByConfirmMail("1pM86JgdOLUAXihLaEaqRWazGmFOJ6Tum2JGhvTaPFIa/PvgrH/pPtukGaUZUNOvnZBLhJfR2sGNdQGnPR0u8s", eNegConstant.UserOperations.Confiramtion);

            Assert.IsTrue(lasteNegMessage != null &&
                          lasteNegMessage.MessageType == ImageType.Success,
                          "Failed to Confirm by Method call");
        }

        /// <summary>
        /// Test_s the confirmation_ by_ method_ call_ used_link_ failed.
        /// </summary>
        [TestMethod]
        public void Test_Confirmation_By_Method_Call_Used_link_Failed()
        {
            lasteNegMessage = null;

            TheVM.UpdateUserByConfirmMail("TaPFIa/PvgrH/pPtukGaUZUNOvnZBLhJfR2sGNdQGnPR0u8s1pM86JgdOLUAXihLaEaqRWazGmFOJ6Tum2JGhv", eNegConstant.UserOperations.Confiramtion);

            Assert.IsTrue(lasteNegMessage != null &&
                          lasteNegMessage.MessageType == ImageType.Error,
                          "Failed to Confirm by Method call");
        }



        /// <summary>
        /// Tests the refuse to be owner request_ succeed.
        /// </summary>
        [TestMethod]
        public void TestRefuseToBeOwnerRequest_Succeed()
        {
            lasteNegMessage = null;

            TheVM.UpdateUserByConfirmMail("1pM86JgdOLUAXihLaEaqRWazGmFOJ6Tum2JGhvTaPFIa/PvgrH/pPtukGaUZUNOvnZBLhJfR2sGNdQGnPR0u8s", eNegConstant.UserOperations.RefuseOwnerRequest);

            Assert.IsTrue(lasteNegMessage != null &&
                          lasteNegMessage.MessageType == ImageType.Success,
                          "Failed to refuse request of being owner in organization");
        }

        /// <summary>
        /// Tests the refuse to be owner request_ failed.
        /// </summary>
        [TestMethod]
        public void TestRefuseToBeOwnerRequest_Failed()
        {
            lasteNegMessage = null;

            TheVM.UpdateUserByConfirmMail("TaPFIa/PvgrH/pPtukGaUZUNOvnZBLhJfR2sGNdQGnPR0u8s1pM86JgdOLUAXihLaEaqRWazGmFOJ6Tum2JGhv", eNegConstant.UserOperations.RefuseOwnerRequest);

            Assert.IsTrue(lasteNegMessage != null &&
                          lasteNegMessage.MessageType == ImageType.Error,
                          "Failed to Not refuse to be Owner in organization");
        }

        /// <summary>
        /// Tests the accept owner request and delete original owner_ succeed.
        /// </summary>
        [TestMethod]
        public void TestAcceptOwnerRequestAndDeleteOriginalOwner_Succeed()
        {
            lasteNegMessage = null;

            TheVM.UpdateUserByConfirmMail("1pM86JgdOLUAXihLaEaqRWazGmFOJ6Tum2JGhvTaPFIa/PvgrH/pPtukGaUZUNOvnZBLhJfR2sGNdQGnPR0u8s", eNegConstant.UserOperations.Accept_DeleteOwnerRequest);

            Assert.IsTrue(lasteNegMessage != null &&
                          lasteNegMessage.MessageType == ImageType.Success,
                          "Failed to Confirm To be owner and delete original owner");
        }

        /// <summary>
        /// Tests the accept owner request and delete original owner_ failed.
        /// </summary>
        [TestMethod]
        public void TestAcceptOwnerRequestAndDeleteOriginalOwner_Failed()
        {
            lasteNegMessage = null;

            TheVM.UpdateUserByConfirmMail("TaPFIa/PvgrH/pPtukGaUZUNOvnZBLhJfR2sGNdQGnPR0u8s1pM86JgdOLUAXihLaEaqRWazGmFOJ6Tum2JGhv", eNegConstant.UserOperations.Accept_DeleteOwnerRequest);

            Assert.IsTrue(lasteNegMessage != null &&
                          lasteNegMessage.MessageType == ImageType.Error,
                          "Failed to Not Confirm To be Owner and delete original owner");
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
