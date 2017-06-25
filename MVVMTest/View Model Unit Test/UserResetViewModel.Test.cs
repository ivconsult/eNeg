
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
 * 17.07.11     Yousra Reda     creation & test all availble methods
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
    ///  Class to Test the User Reset View Model.
    /// </summary>
    [TestClass]
    public class UserResetViewModel_Test
    {
        #region → Fields         .
        private UserResetViewModel usrResetvm;
        private string ErrorMessage;
        #endregion

        #region → Properties     .
        /// <summary>
        /// Gets or sets the VM.
        /// </summary>
        /// <value>The VM.</value>
        public UserResetViewModel TheVM
        {
            get { return usrResetvm; }
            set { usrResetvm = value; }
        }
        #endregion

        #region → Constructors   .

        /// <summary>
        /// Test_s the intialization_ pass.
        /// </summary>
        [TestInitialize]
        public void Test_Intialization_Pass()
        {
            TheVM = new UserResetViewModel(new MockRegisterModel());

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
        /// Tests the basics.
        /// </summary>
        [TestMethod]
        public void TestVM_Existance_HaveInstance()
        {
            Assert.IsNotNull(TheVM, "Failed to retrieve the View Model");
        }

        /// <summary>
        /// Checks the user request reset_ right operation string_ succeeded.
        /// </summary>
        [TestMethod]
        public void CheckUserRequestReset_RightOperationString_Succeeded()
        {
            string operationString = HashHelper.ComputeSaltedHash("6E60D43F-5C70-48D9-8511-2564F58E483E") + HashHelper.ComputeSaltedHash("Yousra.Reda@gmail.com");
            TheVM.CheckUserRequestReset(operationString);
            Assert.IsTrue(string.IsNullOrEmpty(ErrorMessage), string.Concat("Error Message was recieved: ", ErrorMessage));
            Assert.IsTrue(!string.IsNullOrEmpty(TheVM.mOrignalEmailAddress), "Sorry the link you used is used before");
        }

        /// <summary>
        /// Checks the user request reset_ wrong operation string_ failed.
        /// </summary>
        [TestMethod]
        public void CheckUserRequestReset_WrongOperationString_Failed()
        {
            TheVM.CheckUserRequestReset(string.Empty);
            Assert.IsTrue(string.IsNullOrEmpty(ErrorMessage), string.Concat("Error Message was recieved: ", ErrorMessage));
            Assert.IsTrue(string.IsNullOrEmpty(TheVM.mOrignalEmailAddress), "Found a link for thst ");
        }

        /// <summary>
        /// Deletes the user operation by user I d_ right user I d_ succeeded.
        /// </summary>
        [TestMethod]
        public void DeleteUserOperationByUserID_RightUserID_Succeeded()
        {
            //Pass a Guid that I am sure it exist in the Mock used for testing this viewModel
            TheVM.DeleteUserOperationByUserID(new Guid("6E60D43F-5C70-48D9-8511-2564F58E483E"));
            Assert.IsTrue(string.IsNullOrEmpty(ErrorMessage), string.Concat("Error Message was recieved: ", ErrorMessage));
            Assert.IsTrue(TheVM.IsBusy, "Deleting User Operation failed as user id doesn't exist");
        }

        /// <summary>
        /// Deletes the user operation by user I d_ wrong user I d_ failed.
        /// </summary>
        [TestMethod]
        public void DeleteUserOperationByUserID_WrongUserID_Failed()
        {
            TheVM.DeleteUserOperationByUserID(Guid.NewGuid());
            Assert.IsTrue(string.IsNullOrEmpty(ErrorMessage), string.Concat("Error Message was recieved: ", ErrorMessage));
            Assert.IsTrue(!TheVM.IsBusy, "Deleting User Operation succeeded as user id was found");
        }


        #endregion

        #endregion
    }
}
