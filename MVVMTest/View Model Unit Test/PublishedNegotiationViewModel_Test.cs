

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


/* Important Comments about the mock
 
                            ╔════════════╗
◘ Organization      ╔═══════║  CitPOINT  ║═══════╗ 
                    ║       ╚════════════╝       ║
                    ↓                            ↓
               ╔════════════╗             ╔════════════╗           
◘ Users        ║  @ User 1  ║             ║  @ User 2  ║ 
               ╚════════════╝             ╚════════════╝
           ╔═════════════════════╗    ╔═════════════════════╗
◘ Negs     ║ Onging.Negotation 3 ║    ║ Onging.Negotation 6 ║     ==>> 9 Onging
           ╚═════════════════════╝    ╚═════════════════════╝ 
           ╔═════════════════════╗    ╔═════════════════════╗
           ║ Closed.Negotation 2 ║    ║ Closed.Negotation 4 ║     ==>> 6 Closed
           ╚═════════════════════╝    ╚═════════════════════╝ 

◘ User 1 Is my user.
 
◘ My Negotaions     ==>> 5;
 
◘ Others Negotaions ==>> 10;
 
*/

namespace citPOINT.eNeg.MVVM.UnitTest
{
    /// <summary>
    /// Published Negotiation view Model Test
    /// </summary>
    [TestClass]
    public class PublishedNegotiationViewModel_Test
    {
        #region → Fields         .

        private PublishedNegotiationViewModel PublishedNegotiationvm;
        private string ErrorMessage;
        private string mScreenName = "";

        #endregion Fields

        #region → Properties     .

        /// <summary>
        /// View Model Object
        /// </summary>
        public PublishedNegotiationViewModel TheVM
        {
            get { return PublishedNegotiationvm; }
            set { PublishedNegotiationvm = value; }
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
            AppConfigurations.CurrentLoginUser.FirstName = "User";
            AppConfigurations.CurrentLoginUser.LastName = "name";
            AppConfigurations.CurrentLoginUser.EmailAddress = "User1@Domain.com";

            TheVM = new PublishedNegotiationViewModel(new MockPublishedNegotiationModel());


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
        /// Gets the All negotiation without Filter (Status=>ALL  Owner=>All).
        /// </summary>
        [TestMethod]
        public void GetNegotiation_All_All_ReturnAcollection()
        {
            #region → Arrange .

            #endregion

            #region → Act     .

            TheVM.GetPublishedNegotiationArchiveAsync();

            #endregion

            #region → Assert  .

            Assert.IsTrue(string.IsNullOrEmpty(ErrorMessage), string.Concat("Error Message was recieved: ", ErrorMessage));

            Assert.IsTrue(this.TheVM.NegotiationArchiveSource.Count() == 15, "Failed to get all negotiation without filter");

            #endregion

        }

        /// <summary>
        /// Gets the negotiation (Status=>ALL  Owner=>MyNegotaion).
        /// </summary>
        [TestMethod]
        public void GetNegotiation_All_MyNegotaion_ReturnAcollection()
        {
            #region → Arrange .

            this.TheVM.NegotiationStatusFilterID = eNegConstant.NegotiationStatusFilter.All;
            this.TheVM.NegotiationOwnerFilterID = eNegConstant.NegotiationOwnerFilter.MyNegotiations;

            #endregion

            #region → Act     .

            TheVM.GetPublishedNegotiationArchiveAsync();

            #endregion

            #region → Assert  .

            Assert.IsTrue(string.IsNullOrEmpty(ErrorMessage), string.Concat("Error Message was recieved: ", ErrorMessage));

            Assert.IsTrue(this.TheVM.NegotiationArchiveSource.Count() == 5, "Failed to get all My negotiation");

            #endregion
        }

        /// <summary>
        /// Gets the negotiation (Status=>ALL  Owner=>Others negotiations).
        /// </summary>
        [TestMethod]
        public void GetNegotiation_ALL_OtherNegotaion_ReturnAcollection()
        {
            #region → Arrange .

            this.TheVM.NegotiationStatusFilterID = eNegConstant.NegotiationStatusFilter.All;
            this.TheVM.NegotiationOwnerFilterID = eNegConstant.NegotiationOwnerFilter.OthersNegotiatons;

            #endregion

            #region → Act     .

            TheVM.GetPublishedNegotiationArchiveAsync();

            #endregion

            #region → Assert  .

            Assert.IsTrue(string.IsNullOrEmpty(ErrorMessage), string.Concat("Error Message was recieved: ", ErrorMessage));

            Assert.IsTrue(this.TheVM.NegotiationArchiveSource.Count() == 10, "Failed to get all others negotiations'");

            #endregion
        }

        /// <summary>
        /// Gets the negotiation (Status=>Onging  Owner=>All).
        /// </summary>
        [TestMethod]
        public void GetNegotiation_Onging_All_Owners_ReturnAcollection()
        {

            #region → Arrange .

            this.TheVM.NegotiationStatusFilterID = eNegConstant.NegotiationStatusFilter.Ongoing;

            #endregion

            #region → Act     .

            TheVM.GetPublishedNegotiationArchiveAsync();

            #endregion

            #region → Assert  .

            Assert.IsTrue(string.IsNullOrEmpty(ErrorMessage), string.Concat("Error Message was recieved: ", ErrorMessage));

            Assert.IsTrue(this.TheVM.NegotiationArchiveSource.Count() == 9, "Failed to get onging negotaion for all users");

            #endregion
        }

        /// <summary>
        /// Gets the negotiation (Status=>Onging  Owner=>MyNegotaion).
        /// </summary>
        [TestMethod]
        public void GetNegotiation_Onging_MyNegotaion_ReturnAcollection()
        {

            #region → Arrange .

            this.TheVM.NegotiationStatusFilterID = eNegConstant.NegotiationStatusFilter.Ongoing;
            this.TheVM.NegotiationOwnerFilterID = eNegConstant.NegotiationOwnerFilter.MyNegotiations;

            #endregion

            #region → Act     .

            TheVM.GetPublishedNegotiationArchiveAsync();

            #endregion

            #region → Assert  .

            Assert.IsTrue(string.IsNullOrEmpty(ErrorMessage), string.Concat("Error Message was recieved: ", ErrorMessage));

            Assert.IsTrue(this.TheVM.NegotiationArchiveSource.Count() == 3, "Failed to get onging negotaion for Me only");

            #endregion
        }

        /// <summary>
        /// Gets the negotiation (Status=>Onging  Owner=>Others Negotaions).
        /// </summary>
        [TestMethod]
        public void GetNegotiation_Onging_OthersNegotaions_ReturnAcollection()
        {

            #region → Arrange .

            this.TheVM.NegotiationStatusFilterID = eNegConstant.NegotiationStatusFilter.Ongoing;
            this.TheVM.NegotiationOwnerFilterID = eNegConstant.NegotiationOwnerFilter.OthersNegotiatons;

            #endregion

            #region → Act     .

            TheVM.GetPublishedNegotiationArchiveAsync();

            #endregion

            #region → Assert  .

            Assert.IsTrue(string.IsNullOrEmpty(ErrorMessage), string.Concat("Error Message was recieved: ", ErrorMessage));

            Assert.IsTrue(this.TheVM.NegotiationArchiveSource.Count() == 6, "Failed to get onging negotaion that published by other users");

            #endregion

        }

        /// <summary>
        /// Gets the negotiation (Status=>Closed  Owner=>All).
        /// </summary>
        [TestMethod]
        public void GetNegotiation_Closed_All_Owners_ReturnAcollection()
        {
            #region → Arrange .

            this.TheVM.NegotiationStatusFilterID = eNegConstant.NegotiationStatusFilter.Closed;

            #endregion

            #region → Act     .

            TheVM.GetPublishedNegotiationArchiveAsync();

            #endregion

            #region → Assert  .

            Assert.IsTrue(string.IsNullOrEmpty(ErrorMessage), string.Concat("Error Message was recieved: ", ErrorMessage));

            Assert.IsTrue(this.TheVM.NegotiationArchiveSource.Count() == 6, "Failed to get Closed negotaion for all users");

            #endregion
        }

        /// <summary>
        /// Gets the negotiation (Status=>Closed  Owner=>MyNegotiation).
        /// </summary>
        [TestMethod]
        public void GetNegotiation_Closed_MyNegotaion_ReturnAcollection()
        {

            #region → Arrange .

            this.TheVM.NegotiationStatusFilterID = eNegConstant.NegotiationStatusFilter.Closed;
            this.TheVM.NegotiationOwnerFilterID = eNegConstant.NegotiationOwnerFilter.MyNegotiations;

            #endregion

            #region → Act     .

            TheVM.GetPublishedNegotiationArchiveAsync();

            #endregion

            #region → Assert  .

            Assert.IsTrue(string.IsNullOrEmpty(ErrorMessage), string.Concat("Error Message was recieved: ", ErrorMessage));

            Assert.IsTrue(this.TheVM.NegotiationArchiveSource.Count() == 2, "Failed to get Closed negotaion that published by me.");

            #endregion

        }

        /// <summary>
        /// Gets the negotiation (Status=>Closed  Owner=>Others Negotiation).
        /// </summary>
        [TestMethod]
        public void GetNegotiation_Closed_OthersNegotaions_ReturnAcollection()
        {

            #region → Arrange .

            this.TheVM.NegotiationStatusFilterID = eNegConstant.NegotiationStatusFilter.Closed;
            this.TheVM.NegotiationOwnerFilterID = eNegConstant.NegotiationOwnerFilter.OthersNegotiatons;

            #endregion

            #region → Act     .

            TheVM.GetPublishedNegotiationArchiveAsync();

            #endregion

            #region → Assert  .

            Assert.IsTrue(string.IsNullOrEmpty(ErrorMessage), string.Concat("Error Message was recieved: ", ErrorMessage));

            Assert.IsTrue(this.TheVM.NegotiationArchiveSource.Count() == 4, "Failed to get Closed negotaion that published by other users.");

            #endregion

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
            Assert.IsNotNull(PublishedNegotiationvm, "Failed to retrieve the View Model Via MEF");
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


