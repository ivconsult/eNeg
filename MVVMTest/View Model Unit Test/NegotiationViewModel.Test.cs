
#region → Usings   .
using System;
using System.Collections.Generic;
//using System.ComponentModel.Composition;
using System.Linq;
using System.Windows;
using citPOINT.eNeg.Common;
using citPOINT.eNeg.Data.Web;
using citPOINT.eNeg.ViewModel;
using GalaSoft.MvvmLight.Messaging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using citPOINT.eNeg.Data;
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

namespace citPOINT.eNeg.MVVM.UnitTest
{

    /// <summary>
    /// This class is used to test all operation related to Negotiation ,Conversation
    /// And Messages,And Application Activation Dectivation
    /// </summary>
    [TestClass]
    public class NegotiationViewModel_Test
    {
        #region → Fields         .

        private NegotiationViewModel negotiationvm;

        private string ErrorMessage;

        private string LastFlipedMessage { get; set; }

        private string LastSendMessage { get; set; }

        #endregion

        #region → Properties     .

        /// <summary>
        /// View Model Object
        /// </summary>
        //[Import(ViewModelTypes.NegotiationViewModel)]
        public NegotiationViewModel TheVM
        {
            get { return negotiationvm; }
            set { negotiationvm = value; }
        }

        #endregion

        #region → Constructors   .

        /// <summary>
        /// First Method Like Constructor
        /// </summary>
        [TestInitialize]
        public void BuidUp()
        {
            TheVM = new NegotiationViewModel(new MockNegotiationModel());

            //CompositionInitializer.SatisfyImports(this);

            #region " Registeration for needed messages in eNegMessenger "
            // register for RaiseErrorMessage
            eNegMessanger.RaiseErrorMessage.Register(this, OnRaiseErrorMessage);
            // register for PleaseConfirmMessage
            eNegMessanger.ConfirmMessage.Register(this, OnConfirmMessage);

            eNegMessanger.FlippMessage.Register(this, OnFlippMessage);

            eNegMessanger.SendCustomMessage.Register(this, OnSendCustomMessage);


            #endregion

        }

        #endregion

        #region → Commands       .

        /// <summary>
        /// Navigate to conversation Details so flip the view.
        /// </summary>
        [TestMethod]
        public void NavigateToConversationDetails_Flip_TheViewNameSend()
        {
            TheVM.GetClosedNegotiationsArchiveAsync();

            IArchiveItem monthNode
                = TheVM.ClosedNegotiationArchiveSource[0]
                       .Children
                       .FirstOrDefault();

            TheVM.GetClosedNegotiationByArchiveAsync((int)monthNode.Parent.Value, (int)monthNode.Value);

            TheVM.CurrentNegotiation = (monthNode.Children.FirstOrDefault().Value as Negotiation);

            TheVM.SelectedItem = TheVM.CurrentNegotiation.Conversations.FirstOrDefault();

            //Run The Command Of Add new Command (Like Double Click)
            TheVM.NavigateToConversationDetailsCommand.Execute(TheVM.SelectedItem);

            Assert.IsTrue(string.IsNullOrEmpty(ErrorMessage), string.Concat("Error Message was recieved: ", ErrorMessage));

            Assert.IsTrue(((Conversation)(TheVM.SelectedItem)).Messages.Count > 0, "Navigate To Conversation Details Failed");
        }

        /// <summary>
        /// Adds the new negotiation command.
        /// </summary>
        [TestMethod]
        public void AddNegotiation_ExecuteCommand_NewNegotiationToCollection()
        {
            //Number Of records after adding New Record
            int NegotiationCountAfterAdd = TheVM.OnGoingNegotiationSource.Count + 1;

            //Run The Command Of Add new Command
            TheVM.AddNewNegotiationCommand.Execute(null);

            Assert.IsTrue(string.IsNullOrEmpty(ErrorMessage), string.Concat("Error Message was recieved: ", ErrorMessage));

            Assert.IsTrue(TheVM.OnGoingNegotiationSource.Count() == NegotiationCountAfterAdd, "No AddNewNegotiationCommand Found");
        }

        /// <summary>
        /// Add the conversation Excute Command Add conversation for Current Negotiation.
        /// </summary>
        [TestMethod]
        public void AddConversation_ExecuteCommand_AddConversationForCurrentNegotiation()
        {
            TheVM.CurrentNegotiation = this.TheVM.OnGoingNegotiationSource[0];

            //Number Of records after adding New Record
            int CounversationCountAfterAdd = this.TheVM.CurrentNegotiation.Conversations.Count() + 1;

            //Run The Command Of Add New Conversation Command
            TheVM.AddNewConversationCommand.Execute(null);

            //count After Add
            int ConversationCount = this.TheVM.OnGoingNegotiationSource[0].Conversations.Count();

            Assert.IsTrue(string.IsNullOrEmpty(ErrorMessage), string.Concat("Error Message was recieved: ", ErrorMessage));

            Assert.IsTrue(ConversationCount == CounversationCountAfterAdd, "No AddNewConversationCommand Found");
        }

        /// <summary>
        /// Changes the application status command.
        /// </summary>
        [TestMethod]
        public void ChangeApplicationStatus_ExecuteCommand_ApplicationActivated()
        {
            TheVM.CurrentNegotiation = this.TheVM.OnGoingNegotiationSource[0];

            Guid NegotiationApplicationStatusID = this.TheVM.OnGoingNegotiationSource[0].NegotiationApplicationStatus.FirstOrDefault().NegotiationApplicationStatusID;

            bool expectedValue = !this.TheVM.OnGoingNegotiationSource[0].NegotiationApplicationStatus.FirstOrDefault().Active;

            //Run The Command Of Change Application Status Command
            TheVM.ChangeApplicationStatusCommand.Execute(NegotiationApplicationStatusID);

            bool NewValue = this.TheVM.OnGoingNegotiationSource[0].NegotiationApplicationStatus.FirstOrDefault().Active;

            Assert.IsTrue(string.IsNullOrEmpty(ErrorMessage), string.Concat("Error Message was recieved: ", ErrorMessage));

            Assert.IsTrue(this.LastFlipedMessage == ViewTypes.ClosePopupView, "No ChangeApplicationStatusCommand Found");
        }

        /// <summary>
        /// Change the application status Excute Command Application Deactivated.
        /// </summary>
        [TestMethod]
        public void ChangeApplicationStatus_ExecuteCommand_ApplicationDeActivated()
        {
            TheVM.CurrentNegotiation = this.TheVM.OnGoingNegotiationSource[0];

            Guid NegotiationApplicationStatusID = this.TheVM.OnGoingNegotiationSource[0].NegotiationApplicationStatus.FirstOrDefault().NegotiationApplicationStatusID;

            this.TheVM.OnGoingNegotiationSource[0].NegotiationApplicationStatus.FirstOrDefault().Active = true;

            bool expectedValue = !this.TheVM.OnGoingNegotiationSource[0].NegotiationApplicationStatus.FirstOrDefault().Active;

            //Run The Command Of Change Application Status Command
            TheVM.ChangeApplicationStatusCommand.Execute(null);


            bool NewValue = this.TheVM.OnGoingNegotiationSource[0].NegotiationApplicationStatus.FirstOrDefault().Active;

            Assert.IsTrue(string.IsNullOrEmpty(ErrorMessage), string.Concat("Error Message was recieved: ", ErrorMessage));

            Assert.IsTrue(this.LastFlipedMessage == ViewTypes.ClosePopupView, "No ChangeApplicationStatusCommand Found");

        }

        /// <summary>
        /// Delete the on goning negotaition so negotiation deleted.
        /// </summary>
        [TestMethod]
        public void DeleteOnGoning_Negotaition_NegotiationDeleted()
        {
            TheVM.CurrentNegotiation = this.TheVM.OnGoingNegotiationSource[this.TheVM.OnGoingNegotiationSource.Count - 1];

            //Number Of records after Delete Record
            int NegotiationCountAfterDelete = this.TheVM.OnGoingNegotiationSource.Count - 1;

            //Run The Command Of Delete Item Command
            TheVM.DeleteItemCommand.Execute(null);

            //count After Delete
            int NegotaionCount = this.TheVM.OnGoingNegotiationSource.Count();

            Assert.IsTrue(string.IsNullOrEmpty(ErrorMessage), string.Concat("Error Message was recieved: ", ErrorMessage));
            Assert.IsTrue(NegotaionCount == NegotiationCountAfterDelete, "Delete OnGoning Negotaition Negotiation Deleted Faild");
        }

        /// <summary>
        /// Delete the closed Negotaition Negotiation Deleted.
        /// </summary>
        [TestMethod]
        public void DeleteClosed_Negotaition_NegotiationDeleted()
        {
            TheVM.GetClosedNegotiationsArchiveAsync();

            IArchiveItem monthNode
                = TheVM.ClosedNegotiationArchiveSource[0]
                       .Children
                       .FirstOrDefault();

            TheVM.GetClosedNegotiationByArchiveAsync((int)monthNode.Parent.Value, (int)monthNode.Value);

            TheVM.CurrentNegotiation = (monthNode.Children.FirstOrDefault().Value as Negotiation);

            //Number Of records after Delete Record
            int NegotiationCountAfterDelete = monthNode.Children.Count() - 1;

            //Run The Command Of Delete Item Command
            TheVM.DeleteItemCommand.Execute(null);

            //count After Delete
            int NegotaionCount = monthNode.Children.Count();

            Assert.IsTrue(string.IsNullOrEmpty(ErrorMessage), string.Concat("Error Message was recieved: ", ErrorMessage));

            Assert.IsTrue(NegotaionCount == NegotiationCountAfterDelete, "Delete Closed Negotaition Negotiation Deleted Faild");
        }

        /// <summary>
        /// Delete the on goning conversation Conversation Deleted.
        /// </summary>
        [TestMethod]
        public void DeleteOnGoning_Conversation_ConversationDeleted()
        {
            Negotiation negotiation = this.TheVM.OnGoingNegotiationSource[this.TheVM.OnGoingNegotiationSource.Count - 1];

            TheVM.SelectedItem = negotiation.Conversations.FirstOrDefault();

            //Number Of records after Delete Record
            int ConversationCountAfterDelete = negotiation.Conversations.Count - 1;

            //Run The Command Of Delete Item Command
            TheVM.DeleteItemCommand.Execute(null);

            //count After Delete
            int ConversationCount = negotiation.Conversations.Count();

            Assert.IsTrue(string.IsNullOrEmpty(ErrorMessage), string.Concat("Error Message was recieved: ", ErrorMessage));

            Assert.IsTrue(ConversationCount == ConversationCountAfterDelete, "Delete OnGoning Conversation Faild");
        }

        /// <summary>
        /// Delete the Closed conversation Conversation Deleted.
        /// </summary>
        [TestMethod]
        public void DeleteClosed_Conversation_ConversationDeleted()
        {

            TheVM.GetClosedNegotiationsArchiveAsync();

            IArchiveItem monthNode
                = TheVM.ClosedNegotiationArchiveSource[0]
                       .Children
                       .FirstOrDefault();

            TheVM.GetClosedNegotiationByArchiveAsync((int)monthNode.Parent.Value, (int)monthNode.Value);

            TheVM.CurrentNegotiation = (monthNode.Children.FirstOrDefault().Value as Negotiation);
            
            Negotiation negotiation = TheVM.CurrentNegotiation;

            TheVM.SelectedItem = negotiation.Conversations.FirstOrDefault();

            //Number Of records after Delete Record
            int ConversationCountAfterDelete = negotiation.Conversations.Count - 1;

            //Run The Command Of Delete Item Command
            TheVM.DeleteItemCommand.Execute(null);

            //count After Delete
            int ConversationCount = negotiation.Conversations.Count();

            Assert.IsTrue(string.IsNullOrEmpty(ErrorMessage), string.Concat("Error Message was recieved: ", ErrorMessage));

            Assert.IsTrue(ConversationCount == ConversationCountAfterDelete, "Delete Closed Conversation Faild");

        }

        /// <summary>
        /// Delete the Closed conversation Conversation Deleted.
        /// </summary>
        [TestMethod]
        public void DeleteSelected_ConversationAndNegotiationCheckedOne_CheckedDeleted()
        {

            #region → Delete Onging Negotiation .

            this.TheVM.OnGoingNegotiationSource[1].IsNegSelected = true;

            int tmpDeletedconversationCount = 0;

            for (int i = 0; i < this.TheVM.OnGoingNegotiationSource[0].Conversations.Count(); i = i + 2)
            {
                this.TheVM.OnGoingNegotiationSource[0].Conversations.ToList()[i].IsConvSelected = true;
                tmpDeletedconversationCount++;
            }

            //Number Of records after Delete Record
            int onGoingNegotiationCountAfterDeleted = this.TheVM.OnGoingNegotiationSource.Count() - 1;
            int onGoingConversationCountAfterDeleted = this.TheVM.OnGoingNegotiationSource[0].Conversations.Count - tmpDeletedconversationCount;

            #endregion

            #region → Delete Closed Negotiation .

            this.TheVM.ClosedNegotiationSource[1].IsNegSelected = true;

            tmpDeletedconversationCount = 0;

            for (int i = 0; i < this.TheVM.ClosedNegotiationSource[0].Conversations.Count(); i = i + 2)
            {
                this.TheVM.ClosedNegotiationSource[0].Conversations.ToList()[i].IsConvSelected = true;
                tmpDeletedconversationCount++;
            }

            //Number Of records after Delete Record
            int closedNegotiationCountAfterDeleted = this.TheVM.ClosedNegotiationSource.Count() - 1;
            int closedConversationCountAfterDeleted = this.TheVM.ClosedNegotiationSource[0].Conversations.Count - tmpDeletedconversationCount;

            #endregion


            //Run The Command Of Delete Item Command
            TheVM.DeleteSelectedItemsCommand.Execute(null);

            int ResultOnGoingNegotiationCountAfterDeleted = this.TheVM.OnGoingNegotiationSource.Count();
            int ResultOnGoingConversationCountAfterDeleted = this.TheVM.OnGoingNegotiationSource[0].Conversations.Count;

            int ResultClosedNegotiationCountAfterDeleted = this.TheVM.ClosedNegotiationSource.Count();
            int ResultclosedConversationCountAfterDeleted = this.TheVM.ClosedNegotiationSource[0].Conversations.Count;


            Assert.IsTrue(string.IsNullOrEmpty(ErrorMessage), string.Concat("Error Message was recieved: ", ErrorMessage));

            Assert.IsTrue((ResultClosedNegotiationCountAfterDeleted == closedNegotiationCountAfterDeleted) ||
                          (ResultclosedConversationCountAfterDeleted == closedConversationCountAfterDeleted) ||
                          (ResultOnGoingNegotiationCountAfterDeleted == onGoingNegotiationCountAfterDeleted) ||
                          (ResultOnGoingConversationCountAfterDeleted == onGoingConversationCountAfterDeleted), "Delete Closed Conversation Faild");

        }

        /// <summary>
        /// Deletes the selected messages command.
        /// </summary>
        [TestMethod]
        public void DeleteSelectedMessages_ByCheck_MessagesDeleted()
        {
            TheVM.CurrentNegotiation = this.TheVM.OnGoingNegotiationSource[this.TheVM.OnGoingNegotiationSource.Count - 1];

            TheVM.CurrentConversation = TheVM.CurrentNegotiation.Conversations.First();
            foreach (var item in TheVM.CurrentConversation.Messages)
            {
                item.IsChecked = true;
            }

            TheVM.MessageVM.DeleteSelectedMessagesCommand.Execute(null);

            Assert.IsTrue(string.IsNullOrEmpty(ErrorMessage), string.Concat("Error Message was recieved: ", ErrorMessage));
            Assert.IsTrue(TheVM.CurrentConversation.Messages.Count == 0, "No Messages Have been Deleed");
        }

        /// <summary>
        /// Publishes the negotiation user have not organization fail message.
        /// </summary>
        [TestMethod]
        public void PublishNegotiation_UserHaveNotOrganization_FailMessage()
        {
            #region → Arrange .

            this.LastSendMessage = string.Empty;

            TheVM.UserOrganizations = new List<Organization>();

            TheVM.AllNegotiations.FirstOrDefault().IsNegSelected = true;

            #endregion

            #region → Act     .

            TheVM.PublishNegotiationCommand.Execute(null);

            #endregion

            #region → Assert  .

            Assert.IsTrue(string.IsNullOrEmpty(ErrorMessage), string.Concat("Error Message was recieved: ", ErrorMessage));

            Assert.IsTrue(!string.IsNullOrEmpty(LastSendMessage) &&
                          LastSendMessage == ViewModel.Resources.UserHaveNotOrganization,
                          "Error in Publishing a Negotiation where the user have not organization");

            #endregion

        }

        /// <summary>
        /// Publishes the negotiation negotiation published before.
        /// </summary>
        [TestMethod]
        public void PublishNegotiation_NegotiationPublishedBefore_FailMessage()
        {
            #region → Arrange .
            //Try to publish this negotiation to all organization 
            this.LastSendMessage = string.Empty;

            TheVM.AllNegotiations.FirstOrDefault().IsNegSelected = true;

            TheVM.PublishNegotiationCommand.Execute(null);

            TheVM.FinishPublishNegotiationCommand.Execute(null);

            TheVM.AllNegotiations.FirstOrDefault().IsNegSelected = true;

            #endregion

            #region → Act     .

            TheVM.PublishNegotiationCommand.Execute(null);

            #endregion

            #region → Assert  .

            Assert.IsTrue(string.IsNullOrEmpty(ErrorMessage), string.Concat("Error Message was recieved: ", ErrorMessage));

            Assert.IsTrue(!string.IsNullOrEmpty(LastSendMessage) &&
                          LastSendMessage == ViewModel.Resources.NegotaionAlreadyPublished,
                          "Error in Publishing a Negotiation where the negotiation already published");

            #endregion
        }

        /// <summary>
        /// Publishes the negotiation publish.
        /// </summary>
        [TestMethod]
        public void PublishNegotiation_Publish_PublishSuccess()
        {
            #region → Arrange .
            //Try to publish this negotiation to all organization 
            this.LastSendMessage = string.Empty;

            Negotiation cNegotiation = TheVM.AllNegotiations.FirstOrDefault();

            cNegotiation.IsNegSelected = true;

            #endregion

            #region → Act     .

            TheVM.PublishNegotiationCommand.Execute(null);
            TheVM.FinishPublishNegotiationCommand.Execute(null);

            #endregion

            #region → Assert  .

            Assert.IsTrue(string.IsNullOrEmpty(ErrorMessage), string.Concat("Error Message was recieved: ", ErrorMessage));

            Assert.IsTrue(string.IsNullOrEmpty(LastSendMessage),
                          "Error in Publishing a Negotiation.");


            Assert.IsTrue(cNegotiation.NegotiationOrganizations.Count == TheVM.UserOrganizations.Count(),
                          "Error in Publishing a Negotiation.");

            #endregion

        }

        #endregion

        #region → Methods        .

        #region → Private        .

        #region → Raise Error Message   .

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

        #region → Confirm Message       .

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

        #endregion

        #region → Flipp Message         .

        /// <summary>
        /// Called when [flipp message].
        /// </summary>
        /// <param name="pageName">Name of the page.</param>
        private void OnFlippMessage(string pageName)
        {
            this.LastFlipedMessage = pageName;
        }

        #endregion

        #region → Send Custom Message   .

        /// <summary>
        /// Called when [send custom message].
        /// </summary>
        /// <param name="message">The message.</param>
        private void OnSendCustomMessage(eNegMessage message)
        {
            this.LastSendMessage = message.Message;
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
            Assert.IsNotNull(negotiationvm, "Failed to retrieve the View Model");
        }

        /// <summary>
        /// Gets the closed negotiation by page number async.
        /// </summary>
        [TestMethod]
        public void GetClosedNegotiation_ByPageNumber_ReturnCollection()
        {
            TheVM.GetClosedNegotiationsArchiveAsync();

            Assert.IsTrue(string.IsNullOrEmpty(ErrorMessage), string.Concat("Error Message was recieved: ", ErrorMessage));

            Assert.IsTrue(TheVM.ClosedNegotiationArchiveSource.Count() > 0, "No ClosedNegotiationSource Found");
        }


        /// <summary>
        /// Gets the on going negotiation by page number async.
        /// </summary>
        [TestMethod]
        public void GetOnGoingNegotiationByPageNumber_PageOne_ReturnCollection()
        {
            TheVM.GetOngingNegotiationsAsync();
            Assert.IsTrue(string.IsNullOrEmpty(ErrorMessage), string.Concat("Error Message was recieved: ", ErrorMessage));
            Assert.IsTrue(TheVM.OnGoingNegotiationSource.Count() > 0, "No OngiongNegotiationSource Found");
        }

        /// <summary>
        /// Get the application Without Condition Return Applications.
        /// </summary>
        [TestMethod]
        public void GetApplication_WithoutCondition_ReturnCollection()
        {
            TheVM.NegotiationModel.GetApplicationAsync();

            Assert.IsTrue(string.IsNullOrEmpty(ErrorMessage), string.Concat("Error Message was recieved: ", ErrorMessage));
            Assert.IsTrue(TheVM.ApplicationEntries.Count() > 0, "No GetApplicationAsync Found");
        }

        /// <summary>
        /// Get the Channel Return a Collection.
        /// </summary>
        [TestMethod]
        public void GetChannel_ReturnCollection()
        {
            TheVM.GetChannelsAsync();
            Assert.IsTrue(string.IsNullOrEmpty(ErrorMessage), string.Concat("Error Message was recieved: ", ErrorMessage));
            Assert.IsTrue(TheVM.Channels.Count() > 0, "No GetChannelsAsync Found");
        }

        /// <summary>
        /// Get the Negotiation Application status Return a Collection.
        /// </summary>
        [TestMethod]
        public void Get_NegotiationApplicationStatus_ReturnCollection()
        {
            #region Getting Negotiation

            TheVM.GetOngingNegotiationsAsync();

            List<Guid> NegIDs = new List<Guid>();

            foreach (var item in TheVM.OnGoingNegotiationSource)
            {
                NegIDs.Add(item.NegotiationID);
            }

            #endregion Getting Negotiation

            TheVM.NegotiationModel.GetNegotiationApplicationStatusAsync(NegIDs.ToArray());

            Assert.IsTrue(string.IsNullOrEmpty(ErrorMessage), string.Concat("Error Message was recieved: ", ErrorMessage));

            Assert.IsTrue(TheVM.OnGoingNegotiationSource[0].NegotiationApplicationStatus.Count() > 0, "No GetNegotiationAplicationStatusAsync Found");
        }

        /// <summary>
        /// Gets the conversation by neg ID async.
        /// </summary>
        [TestMethod]
        public void GetConversationByNegID_ExistIDs_ReturnCollection()
        {
            #region Getting Negotiation

            TheVM.GetOngingNegotiationsAsync();

            Guid[] NegIDs = TheVM.OnGoingNegotiationSource.Select(x => x.NegotiationID).ToArray();

            #endregion Getting Negotiation

            TheVM.NegotiationModel.GetConversationByNegotiationIDAsync(NegIDs.ToArray());

            Assert.IsTrue(string.IsNullOrEmpty(ErrorMessage), string.Concat("Error Message was recieved: ", ErrorMessage));
            Assert.IsTrue(TheVM.OnGoingNegotiationSource[0].Conversations.Count() > 0, "No GetConversationByNegIDAsync Found");

        }

        /// <summary>
        /// Gets the message by negotiations ID async.
        /// </summary>
        [TestMethod]
        public void GetMessageByNegotiationsID_ExistIDs_ReturncollectionMessages()
        {
            #region Getting Negotiation + Conversation

            TheVM.GetOngingNegotiationsAsync();

            Guid[] NegIDs = TheVM.OnGoingNegotiationSource.Select(x => x.NegotiationID).ToArray();

            TheVM.NegotiationModel.GetConversationByNegotiationIDAsync(NegIDs.ToArray());

            Conversation conv = TheVM.OnGoingNegotiationSource[0].Conversations.FirstOrDefault(); ;

            TheVM.SelectedItem = conv;

            #endregion

            Assert.IsTrue(string.IsNullOrEmpty(ErrorMessage), string.Concat("Error Message was recieved: ", ErrorMessage));

            Assert.IsTrue(TheVM.OnGoingNegotiationSource[0].Conversations.FirstOrDefault().Messages.Count() > 0, "No GetMessageByConvIDAsync Found");
        }


        /// <summary>
        /// Gets the organizations for current user.
        /// </summary>
        [TestMethod]
        public void GetOrganizations_ForCurrentUser_ReturncollectionOrganizations()
        {

            #region → Arrange .

            this.TheVM.UserOrganizations = new List<Organization>();

            #endregion

            #region → Act     .

            TheVM.GetOrganizationsForUserAsync();

            #endregion

            #region → Assert .

            Assert.IsTrue(string.IsNullOrEmpty(ErrorMessage), string.Concat("Error Message was recieved: ", ErrorMessage));

            Assert.IsTrue(this.TheVM.UserOrganizations.Count() > 0, "Error in getting the user organization");

            #endregion

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
