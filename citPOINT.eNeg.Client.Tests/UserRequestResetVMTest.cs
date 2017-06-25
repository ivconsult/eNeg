#region → Usings   .
using System;
using System.ComponentModel.Composition;
using citPOINT.eNeg.Common;
using citPOINT.eNeg.ViewModel;
using Microsoft.Silverlight.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
#endregion

#region → History  .

/* Date         User            Change
 * 
 * 10.08.10     Yousra Reda     creation
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
    /// Class to Test the User Request Reset View Model.
    /// </summary>
    [TestClass]
    public class UserRequestResetVMTest : SilverlightTest
    {


        #region → Fields         .


        private UserRequestResetViewModel usrRequestResetvm;

        private string ErrorMessage;

        #endregion

        #region → Properties     .

        /// <summary>
        /// Gets or sets the User Request Reset View Model.
        /// </summary>
        /// <value>The VM.</value>
        [Import(ViewModelTypes.UserRequestResetViewModel)]
        public UserRequestResetViewModel TheVM
        {
            get { return usrRequestResetvm; }
            set { usrRequestResetvm = value; }
        }

        #endregion
        
        #region → Methods        .

        #region → Public         .


        /// <summary>
        /// Buids up.
        /// </summary>
        [TestInitialize]
        public void BuidUp()
        {
            CompositionInitializer.SatisfyImports(this);
        }

        /// <summary>
        /// Tests the basics.
        /// </summary>
        [TestMethod]
        public void TestBasics()
        {
            Assert.IsNotNull(usrRequestResetvm, "Failed to retrieve the viewmodel via MEF");
            eNegMessanger.RaiseErrorMessage.Register(this, OnRaiseErrorMessage);
        }

        /// <summary>
        /// Called when [raise error message].
        /// </summary>
        /// <param name="ex">The ex.</param>
        public void OnRaiseErrorMessage(Exception ex)
        {
            ErrorMessage = ex.Message;
        }
        
        /// <summary>
        /// Updates the reset.
        /// </summary>
        /// <param name="UserID">The user ID.</param>
        public void UpdateReset(Guid? UserID)
        {
            EnqueueCallback(() => TheVM.UpdateReset(UserID));
            EnqueueCallback(() => Assert.IsTrue(string.IsNullOrEmpty(ErrorMessage), string.Concat("Error Message was recieved: ", ErrorMessage)));
            EnqueueTestComplete();
        }

        /// <summary>
        /// Tests the update reset.
        /// </summary>
        [Asynchronous]
        [TestMethod]
        public void TestUpdateReset()
        {
            Guid UserID = Guid.NewGuid();
            UpdateReset(UserID);
        }

        #endregion

        #endregion

    }
}
