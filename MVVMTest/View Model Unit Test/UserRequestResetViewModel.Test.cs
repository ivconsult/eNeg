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
    /// Class to Test the User Request Reset View Model.
    /// </summary>
    [TestClass]
    public class UserRequestResetViewModel_Test
    {
        #region → Fields         .
        private UserRequestResetViewModel usrRequestResetvm;
        private string ErrorMessage;
        #endregion

        #region → Properties     .
        /// <summary>
        /// Gets or sets the VM.
        /// </summary>
        /// <value>The VM.</value>
        public UserRequestResetViewModel TheVM
        {
            get { return usrRequestResetvm; }
            set { usrRequestResetvm = value; }
        }
        #endregion

        #region → Constructors   .

        /// <summary>
        /// Test_s the intialization_ pass.
        /// </summary>
        [TestInitialize]
        public void Test_Intialization_Pass()
        {
            TheVM = new UserRequestResetViewModel(new MockRegisterModel());

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
        /// Updates the reset request_ right user I d_ succeeded.
        /// </summary>
        [TestMethod]
        public void UpdateResetRequest_RightUserID_Succeeded()
        {
            //Pass a Guid that I am sure it exist in the Mock used for testing this viewModel
            TheVM.UpdateReset(new Guid("6E60D43F-5C70-48D9-8511-2564F58E483E"));
            Assert.IsTrue(string.IsNullOrEmpty(ErrorMessage), string.Concat("Error Message was recieved: ", ErrorMessage));
            Assert.IsNotNull(TheVM.CurrentUser.UserOperations.Where(s => s.Type == eNegConstant.UserOperations.ResetRequest), "Reset request doesn't registered in Database");
        }

        /// <summary>
        /// Updates the reset request_ wrong user I d_ failed.
        /// </summary>
        [TestMethod]
        public void UpdateResetRequest_WrongUserID_Failed()
        {
            //Pass a Guid that I am sure it exist in the Mock used for testing this viewModel
            TheVM.UpdateReset(Guid.NewGuid());
            Assert.IsTrue(string.IsNullOrEmpty(ErrorMessage), string.Concat("Error Message was recieved: ", ErrorMessage));
            Assert.IsTrue(TheVM.CurrentUser.UserOperations.Where(s => s.Type == eNegConstant.UserOperations.ResetRequest).Count() == 0, "Reset request has been registered in Database");
        }

        #endregion

        #endregion
    }
}
