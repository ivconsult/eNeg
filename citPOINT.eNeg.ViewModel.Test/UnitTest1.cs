using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.ComponentModel.Composition;
using citPOINT.eNeg.Common;
using citPOINT.eNeg.ViewModel;

namespace citPOINT.eNeg.MVVM.TestNormal
{
    [TestClass]
    public class UnitTest1
    {
        private UpdateUserProfileViewModel UpdateUserProfilevm;


        /// <summary>
        /// View Model Object
        /// </summary>
        [Import(ViewModelTypes.UpdateUserProfileViewModel)]
        public UpdateUserProfileViewModel TheVM
        {
            get { return UpdateUserProfilevm; }
            set { UpdateUserProfilevm = value; }
        }






        /// <summary>
        /// First Method Like Constructor
        /// </summary>
        [TestInitialize]
        public void BuidUp()
        {
            CompositionInitializer.SatisfyImports(this);

            #region " Registeration for needed messages in eNegMessenger "
            //// register for RaiseErrorMessage
            //eNegMessanger.RaiseErrorMessage.Register(this, OnRaiseErrorMessage);
            //// register for PleaseConfirmMessage
            //eNegMessanger.ConfirmMessage.Register(this, OnConfirmMessage);
            #endregion

        }

        [TestMethod]
        public void TestMethod1()
        {


        }
    }
}
