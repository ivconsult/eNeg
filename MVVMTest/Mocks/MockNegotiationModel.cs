
#region → Usings   .
using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
//using System.ComponentModel.Composition;

using citPOINT.eNeg.Common;
using citPOINT.eNeg.Data.Web;
using System.ServiceModel.DomainServices.Client;
using System.Windows;
#endregion

#region → History  .

/* Date         User            Change
 * 
 * 24.08.10    M.Wahab     creation
 * 19.09.10    M.Wahab     Up grade to Include All functions
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

    public static class test
    {
        public static int ID(this DateTime t)
        {
            return t.Month + t.Year * 12;
        }
    }

    #region → Using  MEF to export INegotiationModel

    /// <summary>
    /// This class is used to carry all operation related to Negotiation ,Conversation
    /// And Messages,And Application Activation Dectivation
    /// </summary>
    //[Export(typeof(INegotiationModel))]
    #endregion
    public class MockNegotiationModel : INegotiationModel
    {
        #region → Fields         .

        private eNegContext mContext;
        private List<Negotiation> mOngiongNegotiations;
        private List<Negotiation> mClosedNegotiations;
        private List<User> mUsers;
        private List<Organization> mOrganizationSource;
        private List<Conversation> mConversations;
        private List<ChannelType> mChannelTypes;
        private List<Channel> mChannels;
        private List<Message> mMessages;
        private List<citPOINT.eNeg.Data.Web.Application> mApplications;
        private List<NegotiationApplicationStatu> mNegotiationApplicationStatuses;
        private List<NegotiationStatu> mNegotiationStatus;
        private List<Negotiation> mAllNegotiation;
        #endregion Fields

        #region → Properties     .

        /// <summary>
        /// property with a getter only to can use eNegService
        /// </summary>
        public eNegContext Context
        {
            get
            {
                if (mContext == null)
                {
                    mContext = new eNegContext(new Uri("http://localhost:9000/citPOINT-eNeg-Data-Web-eNegService.svc", UriKind.Absolute));
                }
                return mContext;
            }
        }

        /// <summary>
        /// Ongiong Negotiation
        /// </summary>
        private List<Negotiation> OngiongNegotiations
        {
            get
            {
                if (mOngiongNegotiations == null)
                {
                    mOngiongNegotiations = new List<Negotiation>()
                        {
                            AddNewNegotiation(true),
                            AddNewNegotiation(true),
                            AddNewNegotiation(true),
                            AddNewNegotiation(true),
                            AddNewNegotiation(true) 
                        };

                }

                return mOngiongNegotiations;
            }
        }

        /// <summary>
        /// Closed Negotiation
        /// </summary>
        private List<Negotiation> ClosedNegotiation
        {
            get
            {
                if (mClosedNegotiations == null)
                {
                    mClosedNegotiations = new List<Negotiation>()
                        {
                            AddNewNegotiation(true),
                            AddNewNegotiation(true),
                            AddNewNegotiation(true),
                            AddNewNegotiation(true),
                            AddNewNegotiation(true) 
                        };

                    foreach (var item in mClosedNegotiations)
                    {
                        item.StatusID = eNegConstant.NegotiationStatus.Closed;
                    }

                }

                return mClosedNegotiations;
            }
        }

        /// <summary>
        /// Gets all negotiation.
        /// </summary>
        /// <value>All negotiation.</value>
        public IEnumerable<Negotiation> AllNegotiation
        {
            get
            {
                if (mAllNegotiation == null)
                {
                    mAllNegotiation = new List<Negotiation>();
                    mAllNegotiation.AddRange(OngiongNegotiations);
                    mAllNegotiation.AddRange(ClosedNegotiation);
                }
                return mAllNegotiation;
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
                                  UserID = Guid.NewGuid(),
                                  EmailAddress = "Test_Test@gmail.com",
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
                                EmailAddress = "Test_Test2@gmail.com",
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
                    mOrganizationSource.Add(new Organization()
                    {
                        OrganizationID = Guid.Parse("a4c1082e-d90a-4521-b9a0-ce81f940f865"),
                        OrganizationName = @"Google Co.",
                        OrganizationTypeID = 4,
                        Deleted = false,
                        DeletedBy = Guid.Parse("c7b94075-ff76-4afa-adb9-c5dcaab7b044"),
                        DeletedOn = new DateTime(2011, 9, 8, 9, 32, 45),
                    });
                } return mOrganizationSource;
            }
        }

        /// <summary>
        /// get Channel Type
        /// </summary>
        private List<ChannelType> ChannelType
        {
            get
            {
                if (mChannelTypes == null)
                {
                    mChannelTypes = new List<ChannelType>()
                            {
                                new ChannelType ()
                                {    TypeName="Type 1",
                                     ChannelTypeID=Guid.NewGuid() 
                                    
                                },

                                new ChannelType ()
                                {    TypeName="Type 2",
                                     ChannelTypeID=Guid.NewGuid() 
                                    
                                },
                            };
                }

                return mChannelTypes;
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
                                     ChannelTypeID= ChannelType[0].ChannelTypeID,
                                     ChannelName="Default Channel 1",
                                     ChannelID =Guid.NewGuid(),
                                     ChannelType=mChannelTypes[0]
                                },

                                new Channel ()
                                {
                                     ChannelTypeID= ChannelType[0].ChannelTypeID,
                                     ChannelName="Default Channel 2",
                                     ChannelID =Guid.NewGuid(),
                                     ChannelType=mChannelTypes[0]
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
        /// Get List of Messages for Converations
        /// </summary>
        /// <param name="conversationID">The conversation ID.</param>
        /// <returns></returns>
        private List<Message> MessagesByConverationIDs(Guid conversationID)
        {
            return this.Messages.Where(s => s.ConversationID == conversationID).ToList();
        }

        /// <summary>
        /// List of Applications
        /// </summary>
        private List<citPOINT.eNeg.Data.Web.Application> Applications
        {
            get
            {
                if (this.mApplications == null)
                {
                    mApplications = new List<citPOINT.eNeg.Data.Web.Application>();


                    for (int appCounter = 1; appCounter <= 3; appCounter++)
                    {

                        citPOINT.eNeg.Data.Web.Application app = new citPOINT.eNeg.Data.Web.Application()
                        {
                            ApplicationID = Guid.NewGuid(),
                            ApplicationTitle = "citPOINT.eNeg.Data.Web.Application " + appCounter.ToString(),
                            ApplicationBaseAddress = "Www.app" + appCounter.ToString() + ".com"
                        };

                        this.mApplications.Add(app);
                    }
                }

                return mApplications;
            }
        }

        /// <summary>
        /// Get List of Negotiation citPOINT.eNeg.Data.Web.Application Status 
        /// </summary>
        private List<NegotiationApplicationStatu> NegotiationApplicationStatuses
        {
            get
            {
                if (mNegotiationApplicationStatuses == null)
                {
                    mNegotiationApplicationStatuses = new List<NegotiationApplicationStatu>();
                }
                return mNegotiationApplicationStatuses;
            }
        }

        /// <summary>
        /// Get List of Negotiation citPOINT.eNeg.Data.Web.Application Status
        /// </summary>
        /// <param name="NegotiationIDs">The negotiation Ids.</param>
        /// <returns></returns>
        private List<NegotiationApplicationStatu> NegotiationApplicationStatusByNegIDs(Guid[] NegotiationIDs)
        {

            List<NegotiationApplicationStatu> lstOfNegotiationApplicationStatus = new List<NegotiationApplicationStatu>();

            foreach (NegotiationApplicationStatu NegApp in this.NegotiationApplicationStatuses)
            {

                if (Array.IndexOf(NegotiationIDs, NegApp.NegotiationID) != -1)
                {
                    lstOfNegotiationApplicationStatus.Add(NegApp);
                }

            }

            return lstOfNegotiationApplicationStatus;

        }

        /// <summary>
        /// Get List of Negotiation Status as Open Or Closed
        /// </summary>
        /// <value>The negotiation status.</value>
        private List<NegotiationStatu> NegotiationStatus
        {
            get
            {
                if (mNegotiationStatus == null)
                {
                    mNegotiationStatus = new List<NegotiationStatu>(){ 
                                             new NegotiationStatu()
                                                {
                                                    StatusID = eNegConstant.NegotiationStatus.Ongoing,
                                                    StatusDescription = "open"
                                                },

                                            new NegotiationStatu()
                                                {
                                                    StatusID = eNegConstant.NegotiationStatus.Closed,
                                                    StatusDescription = "Closed"
                                                }};
                }

                return mNegotiationStatus;
            }
        }

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
        public MockNegotiationModel()
        {
            AppConfigurations.ProfileUserID = User[0].UserID;
            AppConfigurations.CurrentLoginUser = new LoginUser();

            AppConfigurations.CurrentLoginUser.UserID = AppConfigurations.ProfileUserID;

            this.OngiongNegotiations[0] = this.OngiongNegotiations[0];
            this.ClosedNegotiation[0] = this.ClosedNegotiation[0];
        }

        #endregion Constructor

        #region → Events         .

        /// <summary>
        /// Occurs when [get closed negotiation archive complete].
        /// </summary>
        public event EventHandler<eNegEntityResultArgs<NegotiationArchive>> GetClosedNegotiationArchiveComplete;

        /// <summary>
        /// Occurs when [get closed negotiation for archive complete].
        /// </summary>
        public event EventHandler<eNegEntityResultArgs<Negotiation>> GetClosedNegotiationForArchiveComplete;

        /// <summary>
        /// Occurs when [get conversation by neg ID complete].
        /// </summary>
        public event EventHandler<eNegEntityResultArgs<Conversation>> GetConversationByNegotiationIDComplete;

        /// <summary>
        /// Occurs when [get onging negotiation complete].
        /// </summary>
        public event EventHandler<eNegEntityResultArgs<Negotiation>> GetOngingNegotiationComplete;

        /// <summary>
        /// Occurs when [get messages by conversation Ids complete].
        /// </summary>
        public event EventHandler<eNegEntityResultArgs<Message>> GetMessagesByConversationIDsComplete;

        /// <summary>
        /// Occurs when [get negotiation orgaization complete].
        /// </summary>
        public event EventHandler<eNegEntityResultArgs<NegotiationOrganization>> GetNegotiationOrgaizationComplete;

        /// <summary>
        /// Occurs when [get organizations for user complete].
        /// </summary>
        public event EventHandler<eNegEntityResultArgs<Organization>> GetOrganizationsForUserComplete;

        /// <summary>
        /// Occurs when [get apps active for DM complete].
        /// </summary>
        public event EventHandler<eNegEntityResultArgs<UserApplicationStatu>> GetAppsActiveForDMComplete;

        /// <summary>
        /// Occurs when [check on negotiation repeat complete].
        /// </summary>
        public event Action<InvokeOperation<int?>> CheckOnNegotiationRepeatComplete;

        /// <summary>
        /// GetApplicationComplete
        /// </summary>
        public event EventHandler<eNegEntityResultArgs<Data.Web.Application>> GetApplicationComplete;

        /// <summary>
        /// GetAttachementComplete
        /// </summary>
        public event EventHandler<eNegEntityResultArgs<Attachement>> GetAttachementComplete;

        /// <summary>
        /// GetChannelComplete
        /// </summary>
        public event EventHandler<eNegEntityResultArgs<Channel>> GetChannelComplete;

        /// <summary>
        /// GetChannelTypeComplete
        /// </summary>
        public event EventHandler<eNegEntityResultArgs<ChannelType>> GetChannelTypeComplete;

        /// <summary>
        /// GetNegotiationApplicationStatusComplete
        /// </summary>
        public event EventHandler<eNegEntityResultArgs<NegotiationApplicationStatu>> GetNegotiationApplicationStatusComplete;

        /// <summary>
        /// GetNegotiationStatusComplete
        /// </summary>
        public event EventHandler<eNegEntityResultArgs<NegotiationStatu>> GetNegotiationStatusComplete;

        /// <summary>
        /// Occurs when [get negotiation page number complete].
        /// </summary>
        public event Action<InvokeOperation<int?>> GetNegotiationPageNumberComplete;

        /// <summary>
        /// SaveChangesComplete
        /// </summary>
        public event EventHandler<SubmitOperationEventArgs> SaveChangesComplete;

        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion  Events

        #region → Methods        .

        /// <summary>
        /// Get Is current User Is Authorized.
        /// </summary>
        /// <param name="RightName">Value of RightName</param>
        /// <returns>return is true Or false</returns>
        public bool IsAuthorized(RightsName RightName)
        {
            return true;
        }

        #region Negotiation Application Status

        /// <summary>
        /// Get Negotiation Status Async
        /// </summary>
        public void GetNegotiationStatusAsync()
        {
            if (GetNegotiationStatusComplete != null)
            {
                GetNegotiationStatusComplete(this, new eNegEntityResultArgs<NegotiationStatu>(this.NegotiationStatus));
            }
        }

        /// <summary>
        /// Get Negotiation Application Status Async
        /// </summary>
        public void GetNegotiationApplicationStatusAsync()
        {
            if (GetNegotiationApplicationStatusComplete != null)
            {
                GetNegotiationApplicationStatusComplete(this, new eNegEntityResultArgs<NegotiationApplicationStatu>(this.NegotiationApplicationStatuses));
            }
        }


        /// <summary>
        /// Get Negotiation Application Status Async  
        /// </summary>
        /// <param name="NegotiationIDs">NegotiationIDs</param>
        public void GetNegotiationApplicationStatusAsync(Guid[] NegotiationIDs)
        {
            if (GetNegotiationApplicationStatusComplete != null)
            {
                GetNegotiationApplicationStatusComplete(this, new eNegEntityResultArgs<NegotiationApplicationStatu>(this.NegotiationApplicationStatusByNegIDs(NegotiationIDs)));
            }
        }

        #endregion Negotiation Application Status

        /// <summary>
        /// Save Complete
        /// </summary>
        public void SaveChangesAsync()
        {
            if (SaveChangesComplete != null)
            {
                SaveChangesComplete(this, new SubmitOperationEventArgs(null, null));
            }
        }

        /// <summary>
        /// Reject Changes
        /// </summary>
        public void RejectChanges()
        {

        }

        /// <summary>
        /// Reject Message Changes
        /// </summary>
        public void RejectMessageChanges()
        {

        }

        #region Add-Remove-Negotiation-Conversation

        /// <summary>
        /// Remove Messages
        /// </summary>
        /// <param name="message">Messages</param>
        public void RemoveMessage(Message message)
        {
            this.Messages.Remove(message);
        }

        /// <summary>
        /// Adding Message and Return New Object
        /// </summary>
        /// <param name="SetInContext">set in current Context</param>
        /// <returns>return New Object of Message</returns>
        public Message AddNewMessage(bool SetInContext)
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
        /// Adding new Negotiation
        /// </summary>
        /// <param name="SetInContext">add to Current Context</param>
        /// <returns>New Object of Negotiation</returns>
        public Negotiation AddNewNegotiation(bool SetInContext)
        {
            mConversations = mConversations == null ? new List<Conversation>() : mConversations;


            string Negindex = (new Random()).Next(0, 1000).ToString();

            #region Adding Negotiation

            Negotiation Neg = new Negotiation()
            {
                CreatedDate = DateTime.Now,
                Deleted = false,
                DeletedDate = DateTime.Now,
                NegotiationID = Guid.NewGuid(),
                NegotiationName = "New Negotiation-" + Negindex,
                StatusID = eNegConstant.NegotiationStatus.Ongoing,
                UserID = AppConfigurations.CurrentLoginUser.UserID
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

            #region Adding Negotiation Application Status

            foreach (var app in this.Applications)
            {
                NegotiationApplicationStatu negAppStatus = new NegotiationApplicationStatu()
                {
                    NegotiationApplicationStatusID = Guid.NewGuid(),
                    Active = false,
                    Application = app,
                    ApplicationID = app.ApplicationID,
                    Negotiation = Neg,
                    NegotiationID = Neg.NegotiationID,
                    UserID = Neg.UserID
                };


                this.NegotiationApplicationStatuses.Add(negAppStatus);
                Neg.NegotiationApplicationStatus.Add(negAppStatus);
            }

            #endregion Adding Negotiation Application Status

            return Neg;
        }

        /// <summary>
        /// Adding New Conversation
        /// </summary>
        /// <param name="SetInContext">set in Current Context</param>
        /// <returns>return New Object of Conversations</returns>
        public Conversation AddNewConversation(bool SetInContext)
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
        /// Removing Negotiation
        /// </summary>
        /// <param name="Neg">Negotiation Item</param>
        public void RemoveNegotiation(Negotiation Neg)
        {
            this.OngiongNegotiations.Remove(Neg);
            this.ClosedNegotiation.Remove(Neg);
        }

        /// <summary>
        /// Removing Conversation
        /// </summary>
        /// <param name="Conv">Conversation Item</param>
        public void RemoveConversation(Conversation Conv)
        {
            this.Conversations.Remove(Conv);
        }

        /// <summary>
        /// Adds the negotiation organization.
        /// </summary>
        /// <param name="Neg">The neg.</param>
        /// <param name="orga">The orga.</param>
        /// <param name="SetInContext">if set to <c>true</c> [set in context].</param>
        /// <returns>Entity for Negotiation organization</returns>
        public NegotiationOrganization AddNegotiationOrganization(Negotiation Neg, Organization orga, bool SetInContext)
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

        #region → Applications     .

        /// <summary>
        /// Get citPOINT.eNeg.Data.Web.Application Asynchronize
        /// </summary>
        public void GetApplicationAsync()
        {
            if (GetApplicationComplete != null)
            {
                GetApplicationComplete(this, new eNegEntityResultArgs<citPOINT.eNeg.Data.Web.Application>(this.Applications));
            }
        }

        #endregion  Applications

        #region → Attachement      .

        public void GetAttachementAsync()
        {

        }

        #endregion Attachement

        #region → Channel          .

        /// <summary>
        /// Get Channel Async
        /// </summary>
        public void GetChannelAsync()
        {
            if (GetChannelComplete != null)
            {
                GetChannelComplete(this, new eNegEntityResultArgs<Channel>(Channels));
            }
        }

        #endregion Channel

        #region → ChannelType      .

        /// <summary>
        /// Get Channel Type Async
        /// </summary>
        public void GetChannelTypeAsync()
        {
            if (GetChannelTypeComplete != null)
            {
                GetChannelTypeComplete(this, new eNegEntityResultArgs<ChannelType>(ChannelType));
            }
        }

        #endregion ChannelType

        #region → Conversation     .

        /// <summary>
        /// Gets the conversation by negotiation ID async.
        /// </summary>
        /// <param name="negotiationIDs">The negotiation I ds.</param>
        public void GetConversationByNegotiationIDAsync(Guid[] negotiationIDs)
        {

            if (GetConversationByNegotiationIDComplete != null)
            {
                GetConversationByNegotiationIDComplete(this, new eNegEntityResultArgs<Conversation>(ConversationsByNegIDs(negotiationIDs)));
            }
        }



        #endregion Conversation

        #region → Messages         .

        /// <summary>
        /// Gets the message by conversation ID async.
        /// </summary>
        /// <param name="conversationID">The conversation ID.</param>
        public void GetMessageByConversationIDAsync(Guid conversationID)
        {
            GetMessagesByConversationIDsComplete(this, new eNegEntityResultArgs<Message>(this.MessagesByConverationIDs(conversationID)));
        }

        #endregion Messages

        #region → Negotiation      .

        /// <summary>
        /// Gets the closed negotiation archive async.
        /// </summary>
        public void GetClosedNegotiationsArchiveAsync()
        {
            List<NegotiationArchive> lstArchive = new List<NegotiationArchive>();

            foreach (var item in this.ClosedNegotiation)
            {
                if (lstArchive.Where(s => s.ArchiveID == item.CreatedDate.ID()).Count() == 0)
                {
                    lstArchive.Add(new NegotiationArchive() { ArchiveID = item.CreatedDate.ID(), ArchiveMonth = item.CreatedDate.Month, ArchiveYear = item.CreatedDate.Year });
                }
            }

            GetClosedNegotiationArchiveComplete(this, new eNegEntityResultArgs<NegotiationArchive>(lstArchive));
        }

        /// <summary>
        /// Gets the closed negotiation by archive async.
        /// </summary>
        /// <param name="archiveYear">The archive year.</param>
        /// <param name="archiveMonth">The archive month.</param>
        public void GetClosedNegotiationByArchiveAsync(int archiveYear, int archiveMonth)
        {
            if (GetClosedNegotiationForArchiveComplete != null)
            {
                GetClosedNegotiationForArchiveComplete(this, new eNegEntityResultArgs<Negotiation>(this.ClosedNegotiation.Where(s => s.CreatedDate.Year == archiveYear && s.CreatedDate.Month == archiveMonth)));
            }
        }

        /// <summary>
        /// Gets the onging negotiations async.
        /// </summary>
        public void GetOngingNegotiationsAsync()
        {
            if (GetOngingNegotiationComplete != null)
            {
                GetOngingNegotiationComplete(this, new eNegEntityResultArgs<Negotiation>(this.OngiongNegotiations));
            }
        }
         
        #endregion Negotiation

        /// <summary>
        /// Checks the on negotiation.
        /// </summary>
        /// <param name="negotiationID">The negotiation ID.</param>
        /// <param name="negotiationName">Name of the negotiation.</param>
        /// <param name="userID">The user ID.</param>
        public void CheckOnNegotiationRepeatAsync(Guid negotiationID, string negotiationName, Guid userID)
        {

        }

        /// <summary>
        /// Gets the active apps for DM.
        /// </summary>
        public void GetAppsActiveForDM()
        {

        }

        /// <summary>
        /// Gets the organizations for user async.
        /// </summary>
        public void GetOrganizationsForUserAsync()
        {

            GetOrganizationsForUserComplete(this, new eNegEntityResultArgs<Organization>(this.OrganizationSource));

        }

        /// <summary>
        /// Gets the negotiation organization async.
        /// </summary>
        /// <param name="NegotiationIDs">The negotiation I ds.</param>
        public void GetNegotiationOrganizationAsync(Guid[] NegotiationIDs)
        {

            GetNegotiationOrgaizationComplete(this, new eNegEntityResultArgs<NegotiationOrganization>(new List<NegotiationOrganization>()));

        }

        #endregion Methods

    }
}
