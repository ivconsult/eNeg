
#region → Usings   .
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.ServiceModel.DomainServices.Client;
using citPOINT.eNeg.Common;
using citPOINT.eNeg.Data.Web;

#endregion

#region → History  .

/* Date         User            Change
 * 
 * 25.09.11    M.Wahab     creation
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

    #region → Using  MEF to export IPublishedNegotiationModel

    /// <summary>
    /// This class is used to carry all operation related to Published Negotiation
    /// </summary>
    //[Export(typeof(IPublishedNegotiationModel))]
    #endregion
    public class MockPublishedNegotiationModel : IPublishedNegotiationModel
    {
        #region → Fields         .

        private List<Negotiation> mPublishedNegotiations;
        private List<User> mUsers;
        private List<Conversation> mConversations;
        private List<Channel> mChannels;
        private List<Message> mMessages;
        private List<Organization> mOrganizationSource;
        private List<Negotiation> mAllNegotiation;
        #endregion Fields

        #region → Properties     .
        
        #region → Private        .

        /// <summary>
        /// Gets the Organization source.
        /// </summary>
        /// <value>The Organization source.</value>
        private List<Organization> OrganizationSource
        {
            get
            {
                if (mOrganizationSource == null)
                {
                    mOrganizationSource = new List<Organization>();
                    mOrganizationSource.Add(new Organization()
                    {
                        OrganizationID = Guid.Parse("5a65dd1a-5f20-46bf-b4a5-29c7db8578a6"),
                        OrganizationName = @"citPOINT",
                        OrganizationTypeID = 3,
                        Deleted = false,
                        DeletedBy = Guid.Parse("b283aa75-631d-4111-bad3-82487e396a02"),
                        DeletedOn = new DateTime(2011, 9, 8, 9, 17, 51),
                    });

                } return mOrganizationSource;
            }
        }

        /// <summary>
        /// Ongiong Negotiation
        /// </summary>
        private List<Negotiation> PublishedNegotiations
        {
            get
            {
                if (mPublishedNegotiations == null)
                {
                    mPublishedNegotiations = new List<Negotiation>()
                        {
                            AddNewNegotiation(true,eNegConstant.NegotiationStatus.Ongoing,this.User[0].UserID,"Neg.User1.Onging.1",new DateTime(2000,10,1)),
                            AddNewNegotiation(true,eNegConstant.NegotiationStatus.Ongoing,this.User[0].UserID,"Neg.User1.Onging.2",new DateTime(2001,10,1)),
                            AddNewNegotiation(true,eNegConstant.NegotiationStatus.Ongoing,this.User[0].UserID,"Neg.User1.Onging.3",new DateTime(2002,12,1)),

                            AddNewNegotiation(true,eNegConstant.NegotiationStatus.Closed,this.User[0].UserID,"Neg.User1.Closed.4",new DateTime(2003,03,1)),
                            AddNewNegotiation(true,eNegConstant.NegotiationStatus.Closed,this.User[0].UserID,"Neg.User1.Closed.5",new DateTime(2004,04,1)),



                            AddNewNegotiation(true,eNegConstant.NegotiationStatus.Ongoing,this.User[1].UserID,"Neg.User2.Onging.1",new DateTime(2005,10,1)),
                            AddNewNegotiation(true,eNegConstant.NegotiationStatus.Ongoing,this.User[1].UserID,"Neg.User2.Onging.2",new DateTime(2006,10,1)),
                            AddNewNegotiation(true,eNegConstant.NegotiationStatus.Ongoing,this.User[1].UserID,"Neg.User2.Onging.3",new DateTime(2007,10,1)),
                            AddNewNegotiation(true,eNegConstant.NegotiationStatus.Ongoing,this.User[1].UserID,"Neg.User2.Onging.4",new DateTime(2008,5,1)),
                            AddNewNegotiation(true,eNegConstant.NegotiationStatus.Ongoing,this.User[1].UserID,"Neg.User2.Onging.5",new DateTime(2009,10,1)),
                            AddNewNegotiation(true,eNegConstant.NegotiationStatus.Ongoing,this.User[1].UserID,"Neg.User2.Onging.6",new DateTime(2010,10,1)),

                            AddNewNegotiation(true,eNegConstant.NegotiationStatus.Closed,this.User[1].UserID,"Neg.User2.Closed.7",new DateTime(2011,5,1)),
                            AddNewNegotiation(true,eNegConstant.NegotiationStatus.Closed,this.User[1].UserID,"Neg.User2.Closed.8",new DateTime(2012,6,1)),
                            AddNewNegotiation(true,eNegConstant.NegotiationStatus.Closed,this.User[1].UserID,"Neg.User2.Closed.9",new DateTime(2013,8,1)),
                            AddNewNegotiation(true,eNegConstant.NegotiationStatus.Closed,this.User[1].UserID,"Neg.User2.Closed.10",new DateTime(2014,6,1))
    };

                }

                return mPublishedNegotiations;
            }
        }

        /// <summary>
        /// Get Users
        /// </summary>
        private List<User> User
        {
            get
            {
                if (mUsers == null)
                {
                    mUsers = new List<User>()
                      {
                            new User()
                                {
                                  UserID = Guid.Parse("f6287a1f-ecd3-4762-b970-4f3a4a491398"),
                                  EmailAddress = "User1@Domain.com",
                                  Password = "123456A",
                                  NewPassword = string.Empty,
                                  PasswordConfirmation = "123456A",
                                  Locked = false,
                                  IPAddress = "10.0.02.2",
                                  LastLoginDate = DateTime.Now,
                                   CreateDate = DateTime.Now,
                                  AnswerHash = string.Empty,
                                  AnswerSalt = string.Empty,
                                  Online = false,
                                  Disabled = false,
                                  FirstName = string.Empty,
                                  LastName = string.Empty,
                                  CompanyName = string.Empty,
                                  Address = string.Empty,
                                  City = string.Empty,
                                  Phone = string.Empty,
                                  Mobile = string.Empty,
                                  Gender = false,
                                  Reset = false
                            },

                        new User()
                            {
                                UserID = Guid.NewGuid(),
                                EmailAddress = "User2@Domain.com",
                                Password = "123456A",
                                NewPassword = string.Empty,
                                PasswordConfirmation = "123456A",
                                Locked = false,
                                IPAddress = "10.0.02.2",
                                LastLoginDate = DateTime.Now,
                                CreateDate = DateTime.Now,
                                AnswerHash = string.Empty,
                                AnswerSalt = string.Empty,
                                Online = false,
                                Disabled = false,
                                FirstName = string.Empty,
                                LastName = string.Empty,
                                CompanyName = string.Empty,
                                Address = string.Empty,
                                City = string.Empty,
                                Phone = string.Empty,
                                Mobile = string.Empty,
                                Gender = false,
                                Reset = false
                            }

                     };
                }
                return mUsers;
            }
        }

        /// <summary>
        /// get Channels
        /// </summary>
        private List<Channel> Channels
        {
            get
            {
                if (mChannels == null)
                {

                    mChannels = new List<Channel>()
                            {
                                new Channel ()
                                {
                                     ChannelName="Default Channel 1",
                                     ChannelID =Guid.NewGuid(),
                                },

                                new Channel ()
                                {
                                     ChannelName="Default Channel 2",
                                     ChannelID =Guid.NewGuid(),
                                }
                            };
                }

                return mChannels;
            }
        }

        /// <summary>
        /// get Conversation
        /// </summary>
        /// <value>The conversations.</value>
        private List<Conversation> Conversations
        {
            get
            {
                if (mConversations == null)
                {
                    mConversations = new List<Conversation>();
                }
                return mConversations;
            }
        }

        /// <summary>
        /// get Conversation by Negotiation ID
        /// </summary>
        /// <param name="NegIDs">The neg I ds.</param>
        /// <returns></returns>
        private List<Conversation> ConversationsByNegIDs(Guid[] NegIDs)
        {
            List<Conversation> lstOfConv = new List<Conversation>();
            foreach (Conversation Conv in Conversations)
            {

                if (Array.IndexOf(NegIDs, Conv.NegotiationID) != -1)
                {
                    lstOfConv.Add(Conv);
                }

            }

            return lstOfConv;

        }

        /// <summary>
        /// Get List of Messages
        /// </summary>
        private List<Message> Messages
        {
            get
            {
                if (mMessages == null)
                {
                    mMessages = new List<Message>();
                }
                return mMessages;
            }
        }

        /// <summary>
        /// Messageses the by conversation I ds.
        /// </summary>
        /// <param name="conversationID">The conversation ID.</param>
        /// <returns></returns>
        private List<Message> MessagesByConversationIDs(Guid conversationID)
        {
            List<Message> lstOfMessages = new List<Message>();

            foreach (Message msg in this.Messages)
            {

                if (conversationID == msg.ConversationID)
                {
                    lstOfMessages.Add(msg);
                }

            }
            return lstOfMessages;
        }

        #endregion

        /// <summary>
        /// get if any changes happen in the Current Data
        /// </summary>
        /// <value></value>
        public bool HasChanges
        {
            get { return false; }
        }

        /// <summary>
        /// get if the Current Context is Busy
        /// </summary>
        /// <value></value>
        public bool IsBusy
        {
            get { return false; }
        }

        #endregion

        #region → Constructors   .

        /// <summary>
        /// Initializes a new instance of the <see cref="MockNegotiationModel"/> class.
        /// </summary>
        public MockPublishedNegotiationModel()
        {
            AppConfigurations.ProfileUserID = User[0].UserID;

            AppConfigurations.CurrentLoginUser = new LoginUser();

            AppConfigurations.CurrentLoginUser.UserID = AppConfigurations.ProfileUserID;

            this.PublishedNegotiations[0] = this.PublishedNegotiations[0];
        }

        #endregion Constructor

        #region → Events         .

        /// <summary>
        /// Occurs when [get published negotiation archive complete].
        /// </summary>
        public event EventHandler<eNegEntityResultArgs<NegotiationArchive>> GetPublishedNegotiationArchiveComplete;

        /// <summary>
        /// Occurs when [get messages by conversation Ids complete].
        /// </summary>
        public event EventHandler<eNegEntityResultArgs<Message>> GetMessagesByConversationIDsComplete;

        /// <summary>
        /// Occurs when [get conversation by neg ID complete].
        /// </summary>
        public event EventHandler<eNegEntityResultArgs<Conversation>> GetConversationByNegIDComplete;

        /// <summary>
        /// Occurs when [get published negotiation complete].
        /// </summary>
        public event EventHandler<eNegEntityResultArgs<Negotiation>> GetPublishedNegotiationComplete;

        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Get Channel Complete
        /// </summary>
        public event EventHandler<eNegEntityResultArgs<Channel>> GetChannelComplete;

        #endregion  Events

        #region → Methods        .

        #region → Private        .

        #region Add-Remove-Negotiation-Conversation

        /// <summary>
        /// Adding new Negotiation
        /// </summary>
        /// <param name="SetInContext">add to Current Context</param>
        /// <returns>New Object of Negotiation</returns>
        private Negotiation AddNewNegotiation(bool SetInContext, Guid StatusID, Guid UserID, string Name, DateTime createdDate)
        {
            mConversations = mConversations == null ? new List<Conversation>() : mConversations;


            #region Adding Negotiation

            Negotiation Neg = new Negotiation()
            {
                CreatedDate = createdDate,
                Deleted = false,
                DeletedDate = DateTime.Now,
                NegotiationID = Guid.NewGuid(),
                NegotiationName = Name,
                StatusID = StatusID,
                UserID = UserID
            };

            #endregion Adding Negotiation

            #region Adding Conversation / Messgaes

            for (int mConConter = 0; mConConter < 10; mConConter++)
            {

                //Adding Conversation to Negotiation
                Conversation Conv = AddNewConversation(true);
                Conv.NegotiationID = Neg.NegotiationID;
                Neg.Conversations.Add(Conv);
                mConversations.Add(Conv);

            }

            #endregion Adding Conversation / Messgaes

            // Publish
            Neg.NegotiationOrganizations.Add(AddNegotiationOrganization(Neg, this.OrganizationSource[0], true));
            return Neg;
        }

        /// <summary>
        /// Adding New Conversation
        /// </summary>
        /// <param name="SetInContext">set in Current Context</param>
        /// <returns>return New Object of Conversations</returns>
        private Conversation AddNewConversation(bool SetInContext)
        {

            #region Adding Conversation

            Conversation conv = new Conversation()
            {
                ConversationID = Guid.NewGuid(),
                Deleted = false,
                DeletedBy = AppConfigurations.CurrentLoginUser.UserID,
                DeletedOn = DateTime.Now,
                ConversationName = "New Conversation"
            };

            #endregion Adding Conversation

            #region Adding Messages

            for (int mMessagesCounter = 0; mMessagesCounter < 25; mMessagesCounter++)
            {
                Message message = AddNewMessage(true);
                message.ConversationID = conv.ConversationID;
                this.Messages.Add(message);
                conv.Messages.Add(message);
            }

            #endregion Adding Messages

            return conv;
        }

        /// <summary>
        /// Adding Message and Return New Object
        /// </summary>
        /// <param name="SetInContext">set in current Context</param>
        /// <returns>return New Object of Message</returns>
        private Message AddNewMessage(bool SetInContext)
        {
            return new Message()
            {
                ChannelID = this.Channels[0].ChannelID,
                Channel = this.Channels[0],
                Deleted = false,
                ConversationID = Guid.NewGuid(),
                DeletedOn = DateTime.Now,
                DeletedBy = AppConfigurations.CurrentLoginUser.UserID,
                MessageReceiver = " <MessageReceiver@gmail.com> ",
                MessageSender = " <MessageSender@yahoo.Com> ",
                MessageDate = DateTime.Now,
                MessageID = Guid.NewGuid(),
                MessageContent = "MessageContent ",
                MessageSubject = " MessageSubject " + this.Messages.Count.ToString(),
                IsAppsMessage = false
            };

        }

        /// <summary>
        /// Adds the negotiation organization.
        /// </summary>
        /// <param name="Neg">The neg.</param>
        /// <param name="orga">The orga.</param>
        /// <param name="SetInContext">if set to <c>true</c> [set in context].</param>
        /// <returns>Entity for Negotiation organization</returns>
        private NegotiationOrganization AddNegotiationOrganization(Negotiation Neg, Organization orga, bool SetInContext)
        {
            NegotiationOrganization NegOrga = new NegotiationOrganization()
            {
                NegotiationOrganizationID = Guid.NewGuid(),
                Negotiation = Neg,
                Organization = orga,
                NegotiationID = Neg.NegotiationID,
                OrganizationID = orga.OrganizationID,
                Deleted = false,
                DeletedOn = DateTime.Now,
                DeletedBy = AppConfigurations.CurrentLoginUser.UserID
            };

            return NegOrga;

        }

        #endregion Add-Remove-Negotiation-Conversation

        #endregion

        /// <summary>
        /// Gets channels asynchronously
        /// </summary>
        public void GetChannelAsync()
        {
            if (GetChannelComplete != null)
            {
                GetChannelComplete(this, new eNegEntityResultArgs<Channel>(this.Channels));
            }
        }

        /// <summary>
        /// Gets the conversation by neg ID async.
        /// </summary>
        /// <param name="NegotiationIDs">The negotiation Ids.</param>
        public void GetConversationByNegIDAsync(Guid[] NegotiationIDs)
        {
            if (GetConversationByNegIDComplete != null)
            {
                GetConversationByNegIDComplete(this, new eNegEntityResultArgs<Conversation>(ConversationsByNegIDs(NegotiationIDs)));
            }
        }

        /// <summary>
        /// Gets the published negotiation by archive.
        /// </summary>
        /// <param name="archiveYear">The archive year.</param>
        /// <param name="archiveMonth">The archive month.</param>
        /// <param name="NegStatusFilter">The neg status filter.</param>
        /// <param name="NegOwnerFilter">The neg owner filter.</param>
        public void GetPublishedNegotiationByArchiveAsync(int archiveYear, int archiveMonth, eNegConstant.NegotiationStatusFilter NegStatusFilter, eNegConstant.NegotiationOwnerFilter NegOwnerFilter)
        {
            if (GetPublishedNegotiationComplete != null)
            {
                if (archiveMonth == 2012)
                {
                    IEnumerable<Negotiation> negotaionList = this.PublishedNegotiations.OrderBy(o => o.NegotiationName);

                    negotaionList = negotaionList.Where(s =>

                                             ((NegStatusFilter == eNegConstant.NegotiationStatusFilter.Closed && s.StatusID == eNegConstant.NegotiationStatus.Closed) ||
                                             (NegStatusFilter == eNegConstant.NegotiationStatusFilter.Ongoing && s.StatusID == eNegConstant.NegotiationStatus.Ongoing) ||
                                             (NegStatusFilter == eNegConstant.NegotiationStatusFilter.All)) &&

                                             ((NegOwnerFilter == eNegConstant.NegotiationOwnerFilter.OthersNegotiatons && s.UserID != AppConfigurations.CurrentLoginUser.UserID) ||
                                             (NegOwnerFilter == eNegConstant.NegotiationOwnerFilter.MyNegotiations && s.UserID == AppConfigurations.CurrentLoginUser.UserID) ||
                                             (NegOwnerFilter == eNegConstant.NegotiationOwnerFilter.All))
                                         );


                    GetPublishedNegotiationComplete(this, new eNegEntityResultArgs<Negotiation>(negotaionList));
                }
                else
                {
                    List<Negotiation> c = new List<Negotiation>();

                    GetPublishedNegotiationComplete(this, new eNegEntityResultArgs<Negotiation>(c));
                }
            }
        }

        /// <summary>
        /// Gets the published negotiation archive async.
        /// </summary>
        /// <param name="negStatusFilter">The neg status filter.</param>
        /// <param name="negOwnerFilter">The neg owner filter.</param>
        public void GetPublishedNegotiationArchiveAsync(eNegConstant.NegotiationStatusFilter negStatusFilter, eNegConstant.NegotiationOwnerFilter negOwnerFilter)
        {
            if (this.GetPublishedNegotiationArchiveComplete != null)
            {

                List<NegotiationArchive> negArchv = new List<NegotiationArchive>();

                IEnumerable<Negotiation> negotaionList = this.PublishedNegotiations.OrderBy(o => o.NegotiationName);

                negotaionList = negotaionList.Where(s =>

                                         ((negStatusFilter == eNegConstant.NegotiationStatusFilter.Closed && s.StatusID == eNegConstant.NegotiationStatus.Closed) ||
                                         (negStatusFilter == eNegConstant.NegotiationStatusFilter.Ongoing && s.StatusID == eNegConstant.NegotiationStatus.Ongoing) ||
                                         (negStatusFilter == eNegConstant.NegotiationStatusFilter.All)) &&

                                         ((negOwnerFilter == eNegConstant.NegotiationOwnerFilter.OthersNegotiatons && s.UserID != AppConfigurations.CurrentLoginUser.UserID) ||
                                         (negOwnerFilter == eNegConstant.NegotiationOwnerFilter.MyNegotiations && s.UserID == AppConfigurations.CurrentLoginUser.UserID) ||
                                         (negOwnerFilter == eNegConstant.NegotiationOwnerFilter.All))
                                     );

                foreach (var item in  negotaionList)
                {
                    if (negArchv.Where(s => s.ArchiveYear == item.CreatedDate.Year && s.ArchiveMonth == item.CreatedDate.Month).FirstOrDefault() == null)
                    {
                        negArchv.Add(new NegotiationArchive()
                            {
                                ArchiveID = (item.CreatedDate.Month + item.CreatedDate.Year * 12),
                                ArchiveMonth = item.CreatedDate.Month,
                                ArchiveYear = item.CreatedDate.Year,
                            });
                    }
                }

                this.GetPublishedNegotiationArchiveComplete(this,new eNegEntityResultArgs<NegotiationArchive>(negArchv));
            }
        }

        /// <summary>
        /// Gets the message by conversation ID async.
        /// </summary>
        /// <param name="conversationID">The conversation ID.</param>
        public void GetMessageByConversationIDAsync(Guid conversationID)
        {
            if (GetMessagesByConversationIDsComplete != null)
            {
                GetMessagesByConversationIDsComplete(this, new eNegEntityResultArgs<Message>(this.MessagesByConversationIDs(conversationID)));
            }
        }

        #endregion Methods

    }
}
