#region → Usings   .
using System;
using System.ComponentModel.Composition;
using System.Linq;
using citPOINT.eNeg.Common;
using citPOINT.eNeg.Data.Web;
using citPOINT.eNeg.ViewModel;
using Microsoft.Silverlight.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.ServiceModel.DomainServices.Client;
using System.Windows;
using GalaSoft.MvvmLight.Messaging;
#endregion

#region → History  .

/* Date         User            Change
 * 
 * 24.08.10    M.Wahab     creation
 * 19.09.10    M.Wahab     Up grade to Include All functions
 * 20.09.10    M.Wahab     Up grade to Include All Relay Commands
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
    /// This class is used to test all operation related to Negotiation ,Conversation 
    /// And Messages,And Application Activation Dectivation
    /// </summary>
    [TestClass]
    public class NegotiationVMTest : SilverlightTest
    {
        #region → Fields         .

        private NegotiationViewModel negotiationvm;
        private string ErrorMessage;

        #endregion Fields

        #region → Properties     .
        /// <summary>
        /// View Model Object
        /// </summary>
        [Import(ViewModelTypes.NegotiationViewModel)]
        public NegotiationViewModel TheVM
        {
            get { return negotiationvm; }
            set { negotiationvm = value; }
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

        #region → Commands       .


        /// <summary>
        /// Adds the new negotiation command.
        /// </summary>
        [Asynchronous]
        [TestMethod]
        public void AddNewNegotiationCommand()
        {
            //Number Of records after adding New Record
            int NegotiationCountAfterAdd = TheVM.OnGoingNegotiationSource.Count + 1;

            //Run The Command Of Add new Command
            TheVM.AddNewNegotiationCommand.Execute(null);

            EnqueueCallback(() => Assert.IsTrue(string.IsNullOrEmpty(ErrorMessage), string.Concat("Error Message was recieved: ", ErrorMessage)));
            EnqueueCallback(() => Assert.IsTrue(TheVM.OnGoingNegotiationSource.Count() == NegotiationCountAfterAdd, "No AddNewNegotiationCommand Found"));

            EnqueueTestComplete();
        }

        [Asynchronous]
        [TestMethod]
        public void AddNewConversationCommand()
        {
            TheVM.CurrentNegotiation = this.TheVM.OnGoingNegotiationSource[0];

            //Number Of records after adding New Record
            int CounversationCountAfterAdd = this.TheVM.CurrentNegotiation.Conversations.Count() + 1;

            //Run The Command Of Add New Conversation Command
            TheVM.AddNewConversationCommand.Execute(null);

            //count After Add
            int ConversationCount = this.TheVM.OnGoingNegotiationSource[0].Conversations.Count();

            EnqueueCallback(() => Assert.IsTrue(string.IsNullOrEmpty(ErrorMessage), string.Concat("Error Message was recieved: ", ErrorMessage)));
            EnqueueCallback(() => Assert.IsTrue(ConversationCount == CounversationCountAfterAdd, "No AddNewConversationCommand Found"));

            EnqueueTestComplete();
        }



        /// <summary>
        /// Changes the application status command.
        /// </summary>
        [Asynchronous]
        [TestMethod]
        public void ChangeApplicationStatusCommand()
        {
            TheVM.CurrentNegotiation = this.TheVM.OnGoingNegotiationSource[0];

            Guid NegotiationApplicationStatusID = this.TheVM.OnGoingNegotiationSource[0].NegotiationApplicationStatus.FirstOrDefault().NegotiationApplicationStatusID;

            bool expectedValue = !this.TheVM.OnGoingNegotiationSource[0].NegotiationApplicationStatus.FirstOrDefault().Active;

            //Run The Command Of Change Application Status Command
            TheVM.ChangeApplicationStatusCommand.Execute(NegotiationApplicationStatusID);

            bool NewValue = this.TheVM.OnGoingNegotiationSource[0].NegotiationApplicationStatus.FirstOrDefault().Active;

            EnqueueCallback(() => Assert.IsTrue(string.IsNullOrEmpty(ErrorMessage), string.Concat("Error Message was recieved: ", ErrorMessage)));
            EnqueueCallback(() => Assert.IsTrue(expectedValue == NewValue, "No ChangeApplicationStatusCommand Found"));
            EnqueueTestComplete();
        }

        /// <summary>
        /// Deletes the item command.
        /// </summary>
        [Asynchronous]
        [TestMethod]
        public void DeleteItemCommand()
        {
            TheVM.CurrentNegotiation = this.TheVM.OnGoingNegotiationSource[this.TheVM.OnGoingNegotiationSource.Count - 1];

            //Number Of records after Delete Record
            int NegotiationCountAfterDelete = this.TheVM.OnGoingNegotiationSource.Count - 1;

            //Run The Command Of Delete Item Command
            TheVM.DeleteItemCommand.Execute(null);

            //count After Delete
            int NegotaionCount = this.TheVM.OnGoingNegotiationSource.Count();

            EnqueueCallback(() => Assert.IsTrue(string.IsNullOrEmpty(ErrorMessage), string.Concat("Error Message was recieved: ", ErrorMessage)));
            EnqueueCallback(() => Assert.IsTrue(NegotaionCount == NegotiationCountAfterDelete, "No DeleteItemCommand Found"));

            EnqueueTestComplete();
        }

        /// <summary>
        /// Deletes the selected messages command.
        /// </summary>
        [Asynchronous]
        [TestMethod]
        public void DeleteSelectedMessagesCommand()
        {
            TheVM.CurrentNegotiation = this.TheVM.OnGoingNegotiationSource[this.TheVM.OnGoingNegotiationSource.Count - 1];

            TheVM.CurrentConversation = TheVM.CurrentNegotiation.Conversations.First();
            foreach (var item in TheVM.CurrentConversation.Messages)
            {
                item.IsChecked = true;
            }

            TheVM.MessageVM.DeleteSelectedMessagesCommand.Execute(null);

            EnqueueCallback(() => Assert.IsTrue(string.IsNullOrEmpty(ErrorMessage), string.Concat("Error Message was recieved: ", ErrorMessage)));
            EnqueueCallback(() => Assert.IsTrue(TheVM.CurrentConversation.Messages.Count == 0, "No Messages Have been Deleed"));

            EnqueueTestComplete();
        }


        #endregion Command

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
            Assert.IsNotNull(negotiationvm, "Failed to retrieve the ViewModel Via MEF");
        }


        /// <summary>
        /// Gets the closed negotiation by page number async.
        /// </summary>
        [Asynchronous]
        [TestMethod]
        public void GetClosedNegotiationByPageNumberAsync()
        {

            EnqueueCallback(() => TheVM.GetClosedNegotiationByPageNumberAsync(1));
            EnqueueCallback(() => Assert.IsTrue(string.IsNullOrEmpty(ErrorMessage), string.Concat("Error Message was recieved: ", ErrorMessage)));
            EnqueueCallback(() => Assert.IsTrue(TheVM.ClosedNegotiationSource.Count() > 0, "No ClosedNegotiationSource Found"));
            EnqueueTestComplete();
        }


        /// <summary>
        /// Gets the closed negotiation count async.
        /// </summary>
        [Asynchronous]
        [TestMethod]
        public void GetClosedNegotiationCountAsync()
        {
            EnqueueCallback(() => TheVM.GetClosedNegotiationCountAsync());
            EnqueueCallback(() => Assert.IsTrue(string.IsNullOrEmpty(ErrorMessage), string.Concat("Error Message was recieved: ", ErrorMessage)));
            EnqueueCallback(() => Assert.IsTrue(TheVM.NegotiationModel.ClosedNegotiationPager.ItemsCount == 0, "No GetClosedNegotiationCountAsync Found"));
            EnqueueTestComplete();
        }


        /// <summary>
        /// Gets the on going negotiation count async.
        /// </summary>
        [Asynchronous]
        [TestMethod]
        public void GetOnGoingNegotiationCountAsync()
        {
            EnqueueCallback(() => TheVM.NegotiationModel.GetOnGoingNegotiationCountAsync());
            EnqueueCallback(() => Assert.IsTrue(string.IsNullOrEmpty(ErrorMessage), string.Concat("Error Message was recieved: ", ErrorMessage)));
            EnqueueCallback(() => Assert.IsTrue(TheVM.NegotiationModel.OnGoingNegotiationPager.ItemsCount == 0, "No GetOnGoingNegotiationCountAsync Found"));
            EnqueueTestComplete();
        }

        
        /// <summary>
        /// Gets the on going negotiation by page number async.
        /// </summary>
        [Asynchronous]
        [TestMethod]
        public void GetOnGoingNegotiationByPageNumberAsync()
        {
            EnqueueCallback(() => TheVM.GetOnGoingNegotiationByPageNumberAsync(1));
            EnqueueCallback(() => Assert.IsTrue(string.IsNullOrEmpty(ErrorMessage), string.Concat("Error Message was recieved: ", ErrorMessage)));
            EnqueueCallback(() => Assert.IsTrue(TheVM.OnGoingNegotiationSource.Count() > 0, "No OngiongNegotiationSource Found"));
            EnqueueTestComplete();
        }


        /// <summary>
        /// Gets the application async.
        /// </summary>
        [Asynchronous]
        [TestMethod]
        public void GetApplicationAsync()
        {
            EnqueueCallback(() => TheVM.NegotiationModel.GetApplicationAsync());
            EnqueueCallback(() => Assert.IsTrue(string.IsNullOrEmpty(ErrorMessage), string.Concat("Error Message was recieved: ", ErrorMessage)));
            EnqueueCallback(() => Assert.IsTrue(TheVM.ApplicationEntries.Count() > 0, "No GetApplicationAsync Found"));
            EnqueueTestComplete();
        }


        /// <summary>
        /// Gets the channel async.
        /// </summary>
        [Asynchronous]
        [TestMethod]
        public void GetChannelAsync()
        {
            EnqueueCallback(() => TheVM.GetChannelsAsync());
            EnqueueCallback(() => Assert.IsTrue(string.IsNullOrEmpty(ErrorMessage), string.Concat("Error Message was recieved: ", ErrorMessage)));
            EnqueueCallback(() => Assert.IsTrue(TheVM.Channels.Count() > 0, "No GetChannelsAsync Found"));
            EnqueueTestComplete();
        }


        /// <summary>
        /// Gets the negotiation application status async.
        /// </summary>
        [Asynchronous]
        [TestMethod]
        public void GetNegotiationApplicationStatusAsync()
        {
            #region Getting Negotiation

            EnqueueCallback(() => TheVM.GetOnGoingNegotiationByPageNumberAsync(1));
            List<Guid> NegIDs = new List<Guid>();

            foreach (var item in TheVM.OnGoingNegotiationSource)
            {
                NegIDs.Add(item.NegotiationID);
            }

            #endregion Getting Negotiation

            EnqueueCallback(() => TheVM.NegotiationModel.GetNegotiationApplicationStatusAsync(NegIDs.ToArray()));
            EnqueueCallback(() => Assert.IsTrue(string.IsNullOrEmpty(ErrorMessage), string.Concat("Error Message was recieved: ", ErrorMessage)));
            EnqueueCallback(() => Assert.IsTrue(TheVM.OnGoingNegotiationSource[0].NegotiationApplicationStatus.Count() > 0, "No GetNegotiationApplicationStatusAsync Found"));
            EnqueueTestComplete();
        }

        /// <summary>
        /// Gets the conversation by neg ID async.
        /// </summary>
        [Asynchronous]
        [TestMethod]
        public void GetConversationByNegIDAsync()
        {
            #region Getting Negotiation

            EnqueueCallback(() => TheVM.GetOnGoingNegotiationByPageNumberAsync(1));
            List<Guid> NegIDs = new List<Guid>();

            foreach (var item in TheVM.OnGoingNegotiationSource)
            {
                NegIDs.Add(item.NegotiationID);
            }

            #endregion Getting Negotiation

            EnqueueCallback(() => TheVM.NegotiationModel.GetConversationByNegIDAsync(NegIDs.ToArray()));
            EnqueueCallback(() => Assert.IsTrue(string.IsNullOrEmpty(ErrorMessage), string.Concat("Error Message was recieved: ", ErrorMessage)));
            EnqueueCallback(() => Assert.IsTrue(TheVM.OnGoingNegotiationSource[0].Conversations.Count() > 0, "No GetConversationByNegIDAsync Found"));
            EnqueueTestComplete();
        }


        /// <summary>
        /// Gets the message by negotiations ID async.
        /// </summary>
        [Asynchronous]
        [TestMethod]
        public void GetMessageByNegotiationsIDAsync()
        {
            #region Getting Negotiation

            EnqueueCallback(() => TheVM.GetOnGoingNegotiationByPageNumberAsync(1));
            List<Guid> NegIDs = new List<Guid>();

            foreach (var Negitem in TheVM.OnGoingNegotiationSource)
            {
                NegIDs.Add(Negitem.NegotiationID);
            }

            #endregion Getting Negotiation


            EnqueueCallback(() => TheVM.NegotiationModel.GetMessageByNegotiationsIDAsync(NegIDs.ToArray()));
            EnqueueCallback(() => Assert.IsTrue(string.IsNullOrEmpty(ErrorMessage), string.Concat("Error Message was recieved: ", ErrorMessage)));
            EnqueueCallback(() => Assert.IsTrue(TheVM.OnGoingNegotiationSource[0].Conversations.FirstOrDefault().Messages.Count() > 0, "No GetMessageByConvIDAsync Found"));
            EnqueueTestComplete();
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
