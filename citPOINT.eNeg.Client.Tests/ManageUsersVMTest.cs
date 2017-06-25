
#region → Usings   .
using System;
using System.ComponentModel.Composition;
using System.Windows;
using citPOINT.eNeg.Common;
using citPOINT.eNeg.ViewModel;
using GalaSoft.MvvmLight.Messaging;
using Microsoft.Silverlight.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;

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
    /// This class is used to test all operation related to Manage Users
    /// </summary>
    [TestClass]
    public class ManageUsersVMTest : SilverlightTest
    {
        #region → Fields         .

        private ManageUsersViewModel ManageUsersVM;
        private string ErrorMessage;

        #endregion Fields

        #region → Properties     .
        /// <summary>
        /// View Model Object
        /// </summary>
        [Import(ViewModelTypes.ManageUsersViewModel)]
        public ManageUsersViewModel TheVM
        {
            get { return ManageUsersVM; }
            set { ManageUsersVM = value; }
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
            Assert.IsNotNull(ManageUsersVM, "Failed to retrieve the ViewModel Via MEF");
        }


        /// <summary>
        /// Finds the users.
        /// </summary>
        [Asynchronous]
        [TestMethod]
        public void FindUsers()
        {
            this.FindUsersAsync("Test_Test@gmail.com");
        }


        /// <summary>
        /// Finds the users async.
        /// </summary>
        /// <param name="KeyWord">The key word.</param>
        public void FindUsersAsync(string KeyWord)
        {
            EnqueueCallback(() => ManageUsersVM.FindUsersAsync(KeyWord));
            EnqueueCallback(() => Assert.IsTrue(string.IsNullOrEmpty(ErrorMessage), string.Concat("Error Message was recieved: ", ErrorMessage)));
            EnqueueCallback(() => Assert.IsTrue(ManageUsersVM.AllUsersSource.Count > 0, "Search Operation doesn't completed sucessfully"));
            EnqueueTestComplete();
        }

        /// <summary>
        /// Gets the user by alphabet.
        /// </summary>
        [Asynchronous]
        [TestMethod]
        public void GetUserByAlphabet()
        {
            GetUserByAlphabetAsync("T", "EmailAddress");
        }


        /// <summary>
        /// Gets the user by alphabet async.
        /// </summary>
        /// <param name="Alphabet">The alphabet.</param>
        /// <param name="ColumnName">Name of the column.</param>
        public void GetUserByAlphabetAsync(string Alphabet, string ColumnName)
        {
            EnqueueCallback(() => ManageUsersVM.GetUserByAlphabetAsync(Alphabet, ColumnName));
            EnqueueCallback(() => Assert.IsTrue(string.IsNullOrEmpty(ErrorMessage), string.Concat("Error Message was recieved: ", ErrorMessage)));
            EnqueueCallback(() => Assert.IsTrue(ManageUsersVM.AllUsersSource.Count > 0, "Filteration process doesn't completed sucessfully"));
            EnqueueTestComplete();
        }

        /// <summary>
        /// Gets the users by page number.
        /// </summary>
        [Asynchronous]
        [TestMethod]
        public void GetUsersByPageNumber()
        {
            GetUsersByPageNumberAsync(1);
        }

        /// <summary>
        /// Gets the users by page number async.
        /// </summary>
        /// <param name="PageNumber">The page number.</param>
        public void GetUsersByPageNumberAsync(int PageNumber)
        {
            EnqueueCallback(() => ManageUsersVM.GetUsersByPageNumberAsync(PageNumber));
            EnqueueCallback(() => Assert.IsTrue(string.IsNullOrEmpty(ErrorMessage), string.Concat("Error Message was recieved: ", ErrorMessage)));
            EnqueueCallback(() => Assert.IsTrue(ManageUsersVM.AllUsersSource.Count > 0, "Filteration process doesn't completed sucessfully"));
            EnqueueTestComplete();
        }
        #endregion Public

        #endregion Methods
    }
}
