
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
    /// 
    /// </summary>
    [TestClass]
    public class UserResetVMTest : SilverlightTest
    {

        #region → Fields         .

        private UserResetViewModel usrResetvm;

        private string ErrorMessage;

        #endregion

        #region → Properties     .

        /// <summary>
        /// Gets or sets the User Reset View Model.
        /// </summary>
        /// <value>The VM.</value>
        [Import(ViewModelTypes.UserResetViewModel)]
        public UserResetViewModel TheVM
        {
            get { return usrResetvm; }
            set { usrResetvm = value; }
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
            Assert.IsNotNull(usrResetvm, "Failed to retrieve the viewmodel via MEF");
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
        /// Checks the user request reset.
        /// </summary>
        /// <param name="Operation">The operation.</param>
        public void CheckUserRequestReset(string Operation)
        {
            EnqueueCallback(() => TheVM.CheckUserRequestReset(Operation));
            EnqueueCallback(() => Assert.IsTrue(string.IsNullOrEmpty(ErrorMessage), string.Concat("Error Message was recieved: ", ErrorMessage)));
            EnqueueTestComplete();
        }

        /// <summary>
        /// Tests the check user request reset.
        /// </summary>
        [Asynchronous]
        [TestMethod]
        public void TestCheckUserRequestReset()
        {
            string operation = "";
            CheckUserRequestReset(operation);
        }
        #endregion
        #endregion
    }
}
