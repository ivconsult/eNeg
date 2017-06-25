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
    /// This class is used to test all operation related to Manage Users
    /// </summary>
    [TestClass]
    public class ManageUsersViewModel_Test
    {
        #region → Fields         .

        private ManageUsersViewModel ManageUsersVM;
        private string ErrorMessage;

        #endregion Fields

        #region → Properties     .
        /// <summary>
        /// Gets or sets the VM.
        /// </summary>
        /// <value>The VM.</value>
        public ManageUsersViewModel TheVM
        {
            get { return ManageUsersVM; }
            set { ManageUsersVM = value; }
        }

        #endregion Properties

        #region → Constructor    .
        /// <summary>
        /// Test_s the intialization_ pass.
        /// </summary>
        [TestInitialize]
        public void Test_Intialization_Pass()
        {
            TheVM = new ManageUsersViewModel(new MockManageUsers());

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
            Assert.IsNotNull(ManageUsersVM, "Failed to retrieve the View Model");
        }
        
        /// <summary>
        /// Finds the users_ exist key word_ return collection.
        /// </summary>
        [TestMethod]
        public void FindUsers_ExistKeyWord_ReturnCollection()
        {
            TheVM.FindUsersAsync("Test_Test@gmail.com");
            Assert.IsTrue(string.IsNullOrEmpty(ErrorMessage), string.Concat("Error Message was recieved: ", ErrorMessage));
            Assert.IsTrue(TheVM.AllUsersSource.Count > 0, "The used keyword doesn't exist");
        }

        /// <summary>
        /// Finds the users_ not exist key word_ return empty collection.
        /// </summary>
        [TestMethod]
        public void FindUsers_NotExistKeyWord_ReturnEmptyCollection()
        {
            TheVM.FindUsersAsync("NotFound@gmail.com");
            Assert.IsTrue(string.IsNullOrEmpty(ErrorMessage), string.Concat("Error Message was recieved: ", ErrorMessage));
            Assert.IsTrue(TheVM.AllUsersSource.Count == 0, "The used keyword is exist");
        }

        /// <summary>
        /// Gets the user by alphabet_ exist alphabet_ return collection.
        /// </summary>
        [TestMethod]
        public void GetUserByAlphabet_ExistAlphabet_ReturnCollection()
        {
            TheVM.GetUserByAlphabetAsync("T", "EmailAddress");
            Assert.IsTrue(string.IsNullOrEmpty(ErrorMessage), string.Concat("Error Message was recieved: ", ErrorMessage));
            Assert.IsTrue(TheVM.AllUsersSource.Count > 0, "The used alphabet doesn't exist in email Address Column");
        }

        /// <summary>
        /// Gets the user by alphabet_ not exist alphabet_ return empty collection.
        /// </summary>
        [TestMethod]
        public void GetUserByAlphabet_NotExistAlphabet_ReturnEmptyCollection()
        {
            TheVM.GetUserByAlphabetAsync("→", "EmailAddress");
            Assert.IsTrue(string.IsNullOrEmpty(ErrorMessage), string.Concat("Error Message was recieved: ", ErrorMessage));
            Assert.IsTrue(TheVM.AllUsersSource.Count == 0, "The used alphabet is exist in email Address Column");
        }

        /// <summary>
        /// Gets the users by page number_ return collection.
        /// </summary>
        [TestMethod]
        public void GetUsersByPageNumber_ReturnCollection()
        {
            TheVM.GetUsersByPageNumberAsync(1);
            Assert.IsTrue(string.IsNullOrEmpty(ErrorMessage), string.Concat("Error Message was recieved: ", ErrorMessage));
            Assert.IsTrue(TheVM.AllUsersSource.Count > 0, "No users have been found");
        }

        [TestMethod]
        public void GetUsersByPageNumber_ReturnEmptyCollection()
        {
            TheVM.GetUsersByPageNumberAsync(10);
            Assert.IsTrue(string.IsNullOrEmpty(ErrorMessage), string.Concat("Error Message was recieved: ", ErrorMessage));
            Assert.IsTrue(TheVM.AllUsersSource.Count == 0, "There are users have been found");
        }
       
        #endregion

        #endregion
    }
}
