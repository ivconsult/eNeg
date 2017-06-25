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
 * 17.07.11    Yousra Reda      creation & Tset Change Screen Relay Command
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
    /// This is a test class for MainPage ViewModel
    /// </summary>
    [TestClass]
    public class MainPageViewModel_Test
    {
        #region → Fields         .

        private MainPageViewModel mainPagevm;
        private string ErrorMessage;

        #endregion

        #region → Properties     .

        /// <summary>
        /// Gets or sets the VM.
        /// </summary>
        /// <value>The VM.</value>
        public MainPageViewModel TheVM
        {
            get { return mainPagevm; }
            set { mainPagevm = value; }
        }

        #endregion Properties

        #region → Constructor    .
        /// <summary>
        /// Test_s the intialization_ pass.
        /// </summary>
        [TestInitialize]
        public void Test_Intialization_Pass()
        {
            TheVM = new MainPageViewModel(new MockLoginModel());

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
            Assert.IsNotNull(TheVM, "Failed to retrieve the View Model");
        }
        
        /// <summary>
        /// Changes the screen command_ screen name parameter_ succeeded.
        /// </summary>
        [TestMethod]
        public void ChangeScreenCommand_ScreenNameParameter_Succeeded()
        {
            string OriginalScreenName = TheVM.CurrentScreenText;
            TheVM.ChangeScreenCommand.Execute(ViewTypes.ConversationDetailsView);
            string ModifiedScreenName = TheVM.CurrentScreenText;
            Assert.IsTrue(string.IsNullOrEmpty(ErrorMessage), string.Concat("Error Message was recieved: ", ErrorMessage));
            Assert.IsTrue(OriginalScreenName != ModifiedScreenName, "Change Screen Command execution Failed");
        }

        /// <summary>
        /// Changes the screen command_ same screen name parameter_ failed.
        /// </summary>
        [TestMethod]
        public void ChangeScreenCommand_SameScreenNameParameter_Failed()
        {
            string OriginalScreenName = TheVM.CurrentScreenText;
            TheVM.ChangeScreenCommand.Execute(OriginalScreenName);
            string ModifiedScreenName = TheVM.CurrentScreenText;
            Assert.IsTrue(string.IsNullOrEmpty(ErrorMessage), string.Concat("Error Message was recieved: ", ErrorMessage));
            Assert.IsTrue(OriginalScreenName == ModifiedScreenName || ModifiedScreenName == string.Empty, "Change Screen Command execution done successfully");
        }

        #endregion  Public

        #endregion Methods
    }
}
