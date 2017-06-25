
#region → Usings   .
using System;
using System.Collections.Generic;
using System.ServiceModel.DomainServices.Client;
using citPOINT.eNeg.Data.Web;
using Microsoft.VisualStudio.TestTools.UnitTesting;

#endregion

#region → History  .

/* Date         User            Change
 * 
 * 23.09.10     M.Wahab     creation
 */

# endregion

#region → ToDos    .

/*
 * Date         set by User     Description
 * 
*/

# endregion

namespace citPOINT.eNeg.Data.WebTest
{
    /// <summary>
    /// Class For Testing Insert Update Delete for All tables
    /// </summary>
    [TestClass]
    public class eNegServiceTest
    {

        #region → Fields         .

        eNegContext mContext;

        List<AccountType> mAccountTypes;
        List<Application> mApplications;
        List<Attachement> mAttachements;
        List<Channel> mChannels;
        List<ChannelType> mChannelTypes;
        List<Conversation> mConversations;
        List<Country> mCountrys;
        List<Message> mMessages;
        List<Negotiation> mNegotiations;
        List<NegotiationApplicationStatu> mNegotiationApplicationStatus;
        List<NegotiationStatu> mNegotiationStatus;
        List<PreferedLanguage> mPreferedLanguages;
        List<Profile> mProfiles;
        List<Right> mRights;
        List<Role> mRoles;
        List<RoleRight> mRoleRights;
        List<SecurityQuestion> mSecurityQuestions;
        List<User> mUsers;
        List<UserOperation> mUserOperations;
        List<UserRole> mUserRoles;

        private int CountOfAllEntries = 0;

        private Microsoft.VisualStudio.TestTools.UnitTesting.TestContext testContextInstance;

        #endregion Fields

        #region → Properties     .


        #region Object Count


        /// <summary>
        /// Get Count of AccountType
        /// </summary>
        public int AccountTypesCount
        {
            get
            {
                return this.AccountTypes.Count;
            }
        }

        /// <summary>
        /// Get Count of Application
        /// </summary>
        public int ApplicationsCount
        {
            get
            {
                return this.Applications.Count;
            }
        }

        /// <summary>
        /// Get Count of Attachement
        /// </summary>
        public int AttachementsCount
        {
            get
            {
                return this.Attachements.Count;
            }
        }

        /// <summary>
        /// Get Count of Channel
        /// </summary>
        public int ChannelsCount
        {
            get
            {
                return this.Channels.Count;
            }
        }

        /// <summary>
        /// Get Count of ChannelType
        /// </summary>
        public int ChannelTypesCount
        {
            get
            {
                return this.ChannelTypes.Count;
            }
        }

        /// <summary>
        /// Get Count of Conversation
        /// </summary>
        public int ConversationsCount
        {
            get
            {
                return this.Conversations.Count;
            }
        }

        /// <summary>
        /// Get Count of Country
        /// </summary>
        public int CountrysCount
        {
            get
            {
                return this.Countries.Count;
            }
        }

        /// <summary>
        /// Get Count of Message
        /// </summary>
        public int MessagesCount
        {
            get
            {
                return this.Messages.Count;
            }
        }

        /// <summary>
        /// Get Count of Negotiation
        /// </summary>
        public int NegotiationsCount
        {
            get
            {
                return this.Negotiations.Count;
            }
        }

        /// <summary>
        /// Get Count of NegotiationApplicationStatus
        /// </summary>
        public int NegotiationApplicationStatussCount
        {
            get
            {
                return this.NegotiationApplicationStatuss.Count;
            }
        }

        /// <summary>
        /// Get Count of NegotiationStatus
        /// </summary>
        public int NegotiationStatusCount
        {
            get
            {
                return this.NegotiationStatus.Count;
            }
        }

        /// <summary>
        /// Get Count of PreferedLanguage
        /// </summary>
        public int PreferedLanguagesCount
        {
            get
            {
                return this.PreferedLanguages.Count;
            }
        }

        /// <summary>
        /// Get Count of Profile
        /// </summary>
        public int ProfilesCount
        {
            get
            {
                return this.Profiles.Count;
            }
        }

        /// <summary>
        /// Get Count of Right
        /// </summary>
        public int RightsCount
        {
            get
            {
                return this.Rights.Count;
            }
        }

        /// <summary>
        /// Get Count of Role
        /// </summary>
        public int RolesCount
        {
            get
            {
                return this.Roles.Count;
            }
        }

        /// <summary>
        /// Get Count of RoleRight
        /// </summary>
        public int RoleRightsCount
        {
            get
            {
                return this.RoleRights.Count;
            }
        }

        /// <summary>
        /// Get Count of SecurityQuestion
        /// </summary>
        public int SecurityQuestionsCount
        {
            get
            {
                return this.SecurityQuestions.Count;
            }
        }

        /// <summary>
        /// Get Count of User
        /// </summary>
        public int UsersCount
        {
            get
            {
                return this.Users.Count;
            }
        }

        /// <summary>
        /// Get Count of UserOperation
        /// </summary>
        public int UserOperationsCount
        {
            get
            {
                return this.UserOperations.Count;
            }
        }

        /// <summary>
        /// Get Count of UserRole
        /// </summary>
        public int UserRolesCount
        {
            get
            {
                return this.UserRoles.Count;
            }
        }


        #endregion

        #region Mock Objects

        #region <2> AccountTypes

        /// <summary>
        /// List of Account Types
        /// </summary>
        public List<AccountType> AccountTypes
        {
            get
            {
                if (mAccountTypes == null)
                {
                    mAccountTypes = new List<AccountType>()
                       {
                           new AccountType ()
                            { 
                               AccountTypeID=Guid.NewGuid(),
                               TypeName="Free User",
                               TypeDescription="For Demo Users Only"
                            },

                            new AccountType ()
                            { 
                               AccountTypeID=Guid.NewGuid(),
                               TypeName="Premium",
                               TypeDescription="High level of User"
                            }
                       };
                }
                return mAccountTypes;
            }
        }

        #endregion AccountTypes

        #region <3> Applications

        /// <summary>
        /// Get a List of Applications
        /// </summary>
        public List<Application> Applications
        {
            get
            {
                if (mApplications == null)
                {
                    mApplications = new List<Application>()
                       {
                           new Application ()
                            { 
                               ApplicationID=Guid.NewGuid(),
                               ApplicationTitle="XN App 314",
                               ApplicationSettingLink="http://www.Monex314.com"

                            },

                            new Application ()
                            { 
                               ApplicationID=Guid.NewGuid(),
                               ApplicationTitle="Donapy App 4852",
                               ApplicationSettingLink="http://www.Donapy485.com"

                            },

                            new Application ()
                            { 
                               ApplicationID=Guid.NewGuid(),
                               ApplicationTitle="Chonxy App 47",
                               ApplicationSettingLink="http://www.Apps74.com"

                            }
                       };
                }
                return mApplications;
            }
        }

        #endregion Applications

        #region <2> Attachements

        /// <summary>
        /// Get a List of Attachements
        /// </summary>
        public List<Attachement> Attachements
        {
            get
            {
                if (mAttachements == null)
                {
                    mAttachements = new List<Attachement>()
                       {
                           new Attachement ()
                            { 
                                AttachementID=Guid.NewGuid(),
                                AttachementName="CarDescription.Doc",
                                AttachementContent=new byte[] {254,70,97,172,97,204,171,179,106,149,124,172,205,56,136,190,6,19,97,250,156,122,223,141,148,232,175,169,98,198,25,244,19,249,82,70,158,155,137,7,13,33,6,190,157,54,34,55,169,195,242,27,231,24,206,132,101,211,24,119,30,2,82,162,124,134,154,211,229,66,33,105,137,90,237,123,117,34,238,95,199,120,1,157,211,252,14,249,43,204,224,201,132,18,232,11,206,98,66,24,155,78,81,103,162,138,145,142,41,83,16,67,228,184,174,165,1,179,206,127,120,159,168,25,96,87,228,119,113,227,172,19,223,214,195,122,39,152,249,74,108,127,209,215,189,158,227,202,99,67,10,27,147,194,158,250,164,56,8,76,164,27,29,69,166,251,99,15,244,53,126,103,185,133,19,248,205,208,91,193,114,26,21,46,217,146,126,30,56,199,212,238,145,174,164,82,98,140,49,192,59,213,213,46,196,162,97,14,175,164,46,211,220,69,216,138,6,238,107,20,125,69,213,45,95,143,212,11,223,192,151,65,159,153,134,156,68,229,104,56,50,113,110,147,154,2,18,77,212,27,76,114,34,30,67,245,1,168,206,54,204,86,46,237,14,235,53,67,189,61,70,32,134,52,225,13,214,67,146,94,125,136,53,197,117,37,32,130,87,144,156,155,23,51,106,218,67,127,133,173},
                                MessageID=this.Messages[0].MessageID,
                                Message=this.Messages[0]
                                 

                            },

                            new Attachement ()
                            { 
                              
                                AttachementID=Guid.NewGuid(),
                                AttachementName="Aparment25.ppt",
                                AttachementContent=new byte[] {231,174,226,117,69,223,230,155,115,72,105,89,236,140,238,23,115,187,143,73,15,91,30,150,238,109,252,75,167,41,146,34,24,130,83,16,197,25,155,241,50,237,248,72,227,188,182,74,38,58,230,4,166,134,5,140,144,76,134,214,225,156,242,75,181,72,65,107,58,223,81,91,32,156,24,33,98,213,177,50,182,179,38,109,65,142,122,145,126,197,126,121,145,196,17,148,249,173,145,156,124,76,16,69,231,140,226,11,110,226,43,186,153,84,32,47,119,134,10,39,204,174,235,115,97,215,200,91,140,131,39,179,68,22,181,107,164,223,133,180,171,112,36,154,155,195,222,62,164,225,30,116,164,107,206,205,95,155,227,16,194,136,125,234,188,118,118,132,158,180,138,150,1,84,123,93,139,82,215,156,248,139,182,170,101,177,16,216,71,230,13,9,250,117,241,36,242,56,220,38},
                                MessageID=this.Messages[2].MessageID,
                                Message=this.Messages[2]
                                 
                                 
                                 

                            }
                       };
                }
                return mAttachements;
            }
        }

        #endregion Attachements

        #region <4> Channels

        /// <summary>
        /// Get a List of Channels
        /// </summary>
        public List<Channel> Channels
        {
            get
            {
                if (mChannels == null)
                {
                    mChannels = new List<Channel>()
                       {
                           new Channel ()
                            { 
                                ChannelID=Guid.NewGuid(),
                                ChannelName="Default Channel",
                                ChannelTypeID=this.ChannelTypes[0].ChannelTypeID,
                                ChannelType=this.ChannelTypes[0]
                            },

                            new Channel ()
                            { 
                                ChannelID=Guid.NewGuid(),
                                ChannelName="Outlook Channel",
                                ChannelTypeID=this.ChannelTypes[0].ChannelTypeID,
                                ChannelType=this.ChannelTypes[0]
                            },

                            new Channel ()
                            { 
                                ChannelID=Guid.NewGuid(),
                                ChannelName="Skype Channel",
                                ChannelTypeID=this.ChannelTypes[1].ChannelTypeID,
                                ChannelType=this.ChannelTypes[1]
                            },

                            new Channel ()
                            { 
                                ChannelID=Guid.NewGuid(),
                                ChannelName="Yahoo Channel",
                                ChannelTypeID=this.ChannelTypes[1].ChannelTypeID,
                                ChannelType=this.ChannelTypes[1]
                            }
                       };
                }
                return mChannels;
            }
        }

        #endregion Channels

        #region <2> ChannelTypes

        /// <summary>
        /// Get a List of Channel Types
        /// </summary>
        public List<ChannelType> ChannelTypes
        {
            get
            {
                if (mChannelTypes == null)
                {
                    mChannelTypes = new List<ChannelType>()
                       {
                           new ChannelType ()
                            { 
                               ChannelTypeID=Guid.NewGuid(),
                               TypeName="Mail Type"

                            },

                            new ChannelType ()
                            { 
                              ChannelTypeID=Guid.NewGuid(),
                               TypeName="Massenger Type"

                            }
                       };
                }
                return mChannelTypes;
            }
        }

        #endregion ChannelTypes

        #region <7> Conversations

        /// <summary>
        /// Get a List of Conversations
        /// </summary>
        public List<Conversation> Conversations
        {
            get
            {
                if (mConversations == null)
                {
                    mConversations = new List<Conversation>()
                       {

                            #region <3> Purchase a Car 
                           new Conversation ()
                            { 
                               ConversationID=Guid.NewGuid(),
                               ConversationName="BMW",
                               NegotiationID=this.Negotiations[0].NegotiationID,
                               Negotiation=this.Negotiations[0],
                               Deleted=false,
                               DeletedBy=this.Negotiations[0].UserID,
                               User=this.Users[0],
                               DeletedOn=DateTime.Now        
                            },

                            new Conversation ()
                            { 
                               ConversationID=Guid.NewGuid(),
                               ConversationName="Mercedes",
                               NegotiationID=this.Negotiations[0].NegotiationID,
                               Negotiation=this.Negotiations[0],
                               Deleted=false,
                               DeletedBy=this.Negotiations[0].UserID,
                               User=this.Users[0],
                               DeletedOn=DateTime.Now       
                            },

                            new Conversation ()
                            { 
                               ConversationID=Guid.NewGuid(),
                               ConversationName="Fiat",
                               NegotiationID=this.Negotiations[0].NegotiationID,
                               Negotiation=this.Negotiations[0],
                               Deleted=false,
                               DeletedBy=this.Negotiations[0].UserID,
                               User=this.Users[0],
                               DeletedOn=DateTime.Now       
                            },

#endregion Purchase a Car 

                            #region <2> Purchase an Aparment

                            new Conversation ()
                            { 
                               ConversationID=Guid.NewGuid(),
                               ConversationName="Cairo Apartment",
                               NegotiationID=this.Negotiations[1].NegotiationID,
                               Negotiation=this.Negotiations[1],
                               Deleted=false,
                               DeletedBy=this.Negotiations[1].UserID,
                               User=this.Users[0],
                               DeletedOn=DateTime.Now
                                  
                               
                            },


                            new Conversation ()
                            { 
                               ConversationID=Guid.NewGuid(),
                               ConversationName="Alex Apartment",
                               NegotiationID=this.Negotiations[1].NegotiationID,
                               Negotiation=this.Negotiations[1],
                               Deleted=false,
                               DeletedBy=this.Negotiations[1].UserID,
                               User=this.Users[0],
                               DeletedOn=DateTime.Now
                               
                            } ,

#endregion Purchase an Aparment

                            #region <1> Purchase a Computer

                            new Conversation ()
                            { 
                               ConversationID=Guid.NewGuid(),
                               ConversationName="Dell Computer",
                               NegotiationID=this.Negotiations[2].NegotiationID,
                               Negotiation=this.Negotiations[2],
                               Deleted=false,
                               DeletedBy=this.Negotiations[2].UserID,
                               User=this.Users[0],
                               DeletedOn=DateTime.Now

                            },

                             
#endregion Purchase a Computer

                            #region <1> Purchase a  Door

                            new Conversation ()
                            { 
                               ConversationID=Guid.NewGuid(),
                               ConversationName="Wood Door",
                               NegotiationID=this.Negotiations[3].NegotiationID,
                               Negotiation=this.Negotiations[3],
                               Deleted=false,
                               DeletedBy=this.Negotiations[3].UserID,
                               User=this.Users[1],
                               DeletedOn=DateTime.Now

                            },

                             
#endregion Purchase a Door

                       };
                }
                return mConversations;
            }
        }

        #endregion Conversations

        #region <3> Countrys

        /// <summary>
        /// Get a List of Countries
        /// </summary>
        public List<Country> Countries
        {
            get
            {
                if (mCountrys == null)
                {
                    mCountrys = new List<Country>()
                       {
                           new Country ()
                            { 
                               CountryID=Guid.NewGuid(),
                               CountryName="Egypt"
                            },

                            new Country ()
                            { 
                               CountryID=Guid.NewGuid(),
                               CountryName="Jordan"
                            }
                            ,

                            new Country ()
                            { 
                               CountryID=Guid.NewGuid(),
                               CountryName="Austria"
                            }
                       };
                }
                return mCountrys;
            }
        }

        #endregion Countrys

        #region <4> Messages

        /// <summary>
        /// Get a List of Messages
        /// </summary>
        public List<Message> Messages
        {
            get
            {
                if (mMessages == null)
                {
                    mMessages = new List<Message>()
                       {
                          
                           new Message ()
                            { 
                                MessageID = Guid.NewGuid(),
                                MessageDate = DateTime.Now,
                                MessageReceiver = "Car BMW Message Receiver 1" ,
                                MessageContent = "Car BMW Content 1" ,
                                MessageSender = "Car BMW Sender 1" ,
                                MessageSubject = "Car BMW Subject 1" ,
                                ChannelID = this.Channels[0].ChannelID,
                                Channel=this.Channels[0],
                                Deleted = false,
                                ConversationID = this.Conversations[0].ConversationID,
                                DeletedBy =  this.Conversations[0].User.UserID,
                                User=  this.Conversations[0].User,
                                DeletedOn = System.DateTime.Now,
                                IsSent = false,
                                IsAppsMessage=false
                           },


                           new Message ()
                            { 
                                MessageID = Guid.NewGuid(),
                                MessageDate = DateTime.Now,
                                MessageReceiver = "Car Fiat Message Receiver 2" ,
                                MessageContent = "Car Fiat Content 2" ,
                                MessageSender = "Car Fiat Sender 2" ,
                                MessageSubject = "Car Fiat Subject 2" ,
                                ChannelID = this.Channels[0].ChannelID,
                                Channel=this.Channels[0],
                                Deleted = false,
                                ConversationID = this.Conversations[1].ConversationID,
                                DeletedBy =  this.Conversations[1].User.UserID,
                                User=  this.Conversations[1].User,
                                DeletedOn = System.DateTime.Now,
                                IsSent = false,
                                IsAppsMessage=false
                      
                            },




                           new Message ()
                            { 
                                MessageID = Guid.NewGuid(),
                                MessageDate = DateTime.Now,
                                MessageReceiver = "Car Mercedes Message Receiver 3" ,
                                MessageContent = "Car Mercedes Content 3" ,
                                MessageSender = "Car Mercedes Sender 3" ,
                                MessageSubject = "Car Mercedes Subject 3" ,
                                ChannelID = this.Channels[1].ChannelID,
                                Channel=this.Channels[1],
                                Deleted = false,
                                ConversationID = this.Conversations[2].ConversationID,
                                DeletedBy =  this.Conversations[2].User.UserID,
                                User=  this.Conversations[2].User,
                                DeletedOn = System.DateTime.Now,
                                IsSent = false,
                                IsAppsMessage=false
                      
                            },


                            
                           new Message ()
                            { 
                                MessageID = Guid.NewGuid(),
                                MessageDate = DateTime.Now,
                                MessageReceiver = "Apartment Cario Message Receiver 4" ,
                                MessageContent = "Apartment Fiat Content 4" ,
                                MessageSender = "Apartment Fiat Sender 4" ,
                                MessageSubject = "Apartment Fiat Subject 4" ,
                                ChannelID = this.Channels[0].ChannelID,
                                Channel=this.Channels[0],
                                Deleted = false,
                                ConversationID = this.Conversations[3].ConversationID,
                                DeletedBy =  this.Conversations[3].User.UserID,
                                User=  this.Conversations[3].User,
                                DeletedOn = System.DateTime.Now,
                                IsSent = false,
                                IsAppsMessage=false
                           
                            }
                       };
                }
                return mMessages;
            }
        }

        #endregion Messages

        #region <4> Negotiations

        /// <summary>
        /// Get a List of Negotiations
        /// </summary>
        public List<Negotiation> Negotiations
        {
            get
            {
                if (mNegotiations == null)
                {
                    mNegotiations = new List<Negotiation>()
                       {
                           new Negotiation ()
                            { 
                                CreatedDate = DateTime.Now,
                                Deleted = false,
                                DeletedDate = DateTime.Now,
                                NegotiationID = Guid.NewGuid(),
                                NegotiationName = "Purchase a Car",
                                StatusID = this.NegotiationStatus[0].StatusID,
                                UserID = this.Users[0].UserID,
                                NegotiationStatu=this.NegotiationStatus[0],
                                IsNewNegotiation = true
                                 
                

                            },

                            new Negotiation ()
                            { 
                                CreatedDate = DateTime.Now,
                                Deleted = false,
                                DeletedDate = DateTime.Now,
                                NegotiationID = Guid.NewGuid(),
                                NegotiationName = "Purchase a Apartment",
                                StatusID = this.NegotiationStatus[0].StatusID,
                                UserID = this.Users[0].UserID,
                                NegotiationStatu=this.NegotiationStatus[0],
                                IsNewNegotiation = true
                
                            },

                            new Negotiation ()
                            { 
                                CreatedDate = DateTime.Now,
                                Deleted = false,
                                DeletedDate = DateTime.Now,
                                NegotiationID = Guid.NewGuid(),
                                NegotiationName = "Purchase a Computer",
                                StatusID = this.NegotiationStatus[0].StatusID,
                                UserID = this.Users[0].UserID,
                                NegotiationStatu=this.NegotiationStatus[0],
                                IsNewNegotiation = true
                

                            },


                            new Negotiation ()
                            { 
                                CreatedDate = DateTime.Now,
                                Deleted = false,
                                DeletedDate = DateTime.Now,
                                NegotiationID = Guid.NewGuid(),
                                NegotiationName = "Purchase a Door",
                                StatusID = this.NegotiationStatus[0].StatusID,
                                UserID = this.Users[1].UserID,
                                NegotiationStatu=this.NegotiationStatus[1],
                                IsNewNegotiation = true

                            } 

                          
                       };
                }
                return mNegotiations;
            }
        }

        #endregion Negotiations

        #region <n> NegotiationApplicationStatuss

        /// <summary>
        /// Get a List of Negotiation Application Status
        /// </summary>
        public List<NegotiationApplicationStatu> NegotiationApplicationStatuss
        {
            get
            {
                if (mNegotiationApplicationStatus == null)
                {
                    mNegotiationApplicationStatus = new List<NegotiationApplicationStatu>();


                    foreach (var neg in this.Negotiations)
                    {

                        foreach (var app in this.Applications)
                        {

                            mNegotiationApplicationStatus.Add(
                                        new NegotiationApplicationStatu()
                                        {
                                            NegotiationApplicationStatusID = Guid.NewGuid(),
                                            ApplicationID = app.ApplicationID,
                                            NegotiationID = neg.NegotiationID,
                                            UserID = neg.UserID,
                                            Active = false
                                        }
                                );

                        }
                    }
                }

                return mNegotiationApplicationStatus;
            }
        }

        #endregion NegotiationApplicationStatuss

        #region <2> NegotiationStatus

        /// <summary>
        /// Get a List of Negotiation Statuses
        /// </summary>
        public List<NegotiationStatu> NegotiationStatus
        {
            get
            {
                if (mNegotiationStatus == null)
                {
                    mNegotiationStatus = new List<NegotiationStatu>()
                       {
                           new NegotiationStatu ()
                            { 
                               StatusID=Guid.NewGuid(),
                               StatusDescription="On Going"
                            },

                            new NegotiationStatu ()
                            { 
                              StatusID=Guid.NewGuid(),
                               StatusDescription="Closed"

                            }
                       };
                }
                return mNegotiationStatus;
            }
        }

        #endregion NegotiationStatus

        #region <2> PreferedLanguages

        /// <summary>
        /// Get a List of Prefered Languages
        /// </summary>
        public List<PreferedLanguage> PreferedLanguages
        {
            get
            {
                if (mPreferedLanguages == null)
                {
                    mPreferedLanguages = new List<PreferedLanguage>()
                       {
                           new PreferedLanguage ()
                            { 
                               LCID=50000,
                               PreferedLanguage1="Spanish - Chile"
                            },

                            new PreferedLanguage ()
                            { 
                              LCID=60000,
                               PreferedLanguage1="English - Australia"
                            }
                       };
                }
                return mPreferedLanguages;
            }
        }

        #endregion PreferedLanguages

        #region <2> Profiles

        /// <summary>
        /// Get a List of Profile
        /// </summary>
        public List<Profile> Profiles
        {
            get
            {
                if (mProfiles == null)
                {
                    mProfiles = new List<Profile>()
                       {
                           new Profile ()
                            { 
                                ProfileID=Guid.NewGuid(),
                                CurrentTheme="Blue Theme",
                                UserID=this.Users[0].UserID,
                                User=this.Users[0]
                            },

                            new Profile ()
                            { 
                                ProfileID=Guid.NewGuid(),
                                CurrentTheme="Black Theme",
                                UserID=this.Users[1].UserID,
                                User=this.Users[1]

                            }
                       };
                }
                return mProfiles;
            }
        }

        #endregion Profiles

        #region <2> Rights

        /// <summary>
        /// Get a List of Rights
        /// </summary>
        public List<Right> Rights
        {
            get
            {
                if (mRights == null)
                {
                    mRights = new List<Right>()
                       {
                           new Right ()
                            { 
                               RightID=Guid.NewGuid(),
                               RightName="Open_Conversation",
                               RightDescription="if user can see the COnversation or not"
                            },

                        new Right ()
                            { 
                               RightID=Guid.NewGuid(),
                               RightName="Close_Conversation",
                               RightDescription="if user can Close Conversation or not"
                            }

                       };
                }
                return mRights;
            }
        }

        #endregion Rights

        #region <2> Roles

        /// <summary>
        /// Get a List of Roles
        /// </summary>
        public List<Role> Roles
        {
            get
            {
                if (mRoles == null)
                {
                    mRoles = new List<Role>()
                       {
                           new Role ()
                            { 
                               RoleID=Guid.NewGuid(),
                               RoleName="Admin",
                               RoleDescription="For Administrative users"

                            },

                            new Role ()
                            { 
                               RoleID=Guid.NewGuid(),
                               RoleName="User",
                               RoleDescription="For Normal users"

                            }
                       };
                }
                return mRoles;
            }
        }

        #endregion Roles

        #region <4> RoleRights

        /// <summary>
        /// Get a List of Role Rights
        /// </summary>
        public List<RoleRight> RoleRights
        {
            get
            {
                if (mRoleRights == null)
                {
                    mRoleRights = new List<RoleRight>()
                       {
                           new RoleRight ()
                            { 
                                RoleRightID=Guid.NewGuid(),
                                RightID=this.Rights[0].RightID,
                                Right=this.Rights[0],
                                RoleID=this.Roles[0].RoleID,
                                Role=this.Roles[0]

                            },

                            new RoleRight ()
                            { 
                                RoleRightID=Guid.NewGuid(),
                                RightID=this.Rights[1].RightID,
                                Right=this.Rights[1],
                                RoleID=this.Roles[0].RoleID,
                                Role=this.Roles[0]
                            },

                            new RoleRight ()
                            { 
                                RoleRightID=Guid.NewGuid(),
                                RightID=this.Rights[0].RightID,
                                Right=this.Rights[0],
                                RoleID=this.Roles[1].RoleID,
                                Role=this.Roles[1]

                            },

                            new RoleRight ()
                            { 
                                RoleRightID=Guid.NewGuid(),
                                RightID=this.Rights[1].RightID,
                                Right=this.Rights[1],
                                RoleID=this.Roles[1].RoleID,
                                Role=this.Roles[1]
                            }
                       };
                }
                return mRoleRights;
            }
        }

        #endregion RoleRights

        #region <6> SecurityQuestions

        /// <summary>
        /// Get a List of Security Questions
        /// </summary>
        public List<SecurityQuestion> SecurityQuestions
        {
            get
            {
                if (mSecurityQuestions == null)
                {
                    mSecurityQuestions = new List<SecurityQuestion>()
                       {
                           
                           new SecurityQuestion ()
                            { 
                               SecurityQuestionID=Guid.NewGuid(),
                               Question="What was your childhood nickname? "
                            },
                            new SecurityQuestion ()
                            { 
                               SecurityQuestionID=Guid.NewGuid(),
                               Question="What is your oldest cousin's first and last name?"
                            },

                            new SecurityQuestion ()
                            { 
                               SecurityQuestionID=Guid.NewGuid(),
                               Question="What is the name of your favorite childhood friend? "
                            },

                            new SecurityQuestion ()
                            { 
                               SecurityQuestionID=Guid.NewGuid(),
                               Question="What was the name of your first stuffed animal?"
                            },

                            new SecurityQuestion ()
                            { 
                               SecurityQuestionID=Guid.NewGuid(),
                               Question="Where were you when you first heard about 9/11?"
                            },

                            new SecurityQuestion ()
                            { 
                               SecurityQuestionID=Guid.NewGuid(),
                               Question="What is the license plate (registration) of your dad's first car?"
                            } 
                          
                       };
                }
                return mSecurityQuestions;
            }
        }

        #endregion SecurityQuestions

        #region <2> Users

        /// <summary>
        /// Get a List of Users
        /// </summary>
        public List<User> Users
        {
            get
            {
                if (mUsers == null)
                {
                    mUsers = new List<User>()
                       {
                           new User ()
                            { 
                                UserID = Guid.NewGuid(),
                                EmailAddress = "TestUser1@gmail.com",
                                Password = "123456A",
                                NewPassword = string.Empty,
                                PasswordConfirmation = "123456A",
                                Locked = true,
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

                            new User ()
                            { 
                              UserID = Guid.NewGuid(),
                                EmailAddress = "TestUser2@Hotmail.com",
                                Password = "123456B",
                                NewPassword = string.Empty,
                                PasswordConfirmation = "123456B",
                                Locked = false,
                                IPAddress = "11.11.11.11",
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
                                Reset = true    

                            }

                            ,

                            new User ()
                            { 
                              UserID = Guid.NewGuid(),
                                EmailAddress = "TestUser3@Hotmail.com",
                                Password = "123456B",
                                NewPassword = string.Empty,
                                PasswordConfirmation = "123456B",
                                Locked = false,
                                IPAddress = "11.11.11.11",
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
                                Reset = true    

                            }
                       };
                }
                return mUsers;
            }
        }

        #endregion Users

        #region <2> UserOperations

        /// <summary>
        /// Get a List of User Operations
        /// </summary>
        public List<UserOperation> UserOperations
        {
            get
            {
                if (mUserOperations == null)
                {
                    mUserOperations = new List<UserOperation>()
                       {
                           new UserOperation ()
                            { 
                                OperationID=Guid.NewGuid(),
                                UserID=this.Users[0].UserID,
                                User=this.Users[0],
                                OperationString="8hMXrtHCNNYbJMGiGCCO%2B3ouheYk4hO0GvjRBxMBimwjQnaSOOH4lmmFUs3lQRo47jgo82ea7nIDz5s0E15dis",
                                Type=eNeg.Common.eNegConstant.UserOperations.Confiramtion

                                 
                             },

                            new UserOperation ()
                            { 
                                OperationID=Guid.NewGuid(),
                                UserID=this.Users[1].UserID,
                                User=this.Users[1],
                                OperationString="11MXrtHCNNYbJMGiGCCO%2B3ouheYk4hO0GvjRBxMBimwjQnaSOOH4lmmFUs3lQRo47jgo82ea7nIDz5s0E15dis",
                                Type=eNeg.Common.eNegConstant.UserOperations.Confiramtion,
                                NewEmailAddress="NewEmailAddress@yahoo.com"
                             },

                            new UserOperation ()
                            { 
                                OperationID=Guid.NewGuid(),
                                UserID=this.Users[2].UserID,
                                User=this.Users[2],
                                OperationString="sgBDMXEqmdmz5nXMw%2BW3NV4QXMexDtm61s3XE4nikB8Hjjbqv0l14wC2jQWsCwUgnyClCFML/446e1RpRf6SZo",
                                Type=eNeg.Common.eNegConstant.UserOperations.ResetRequest
                            }
                       };
                }
                return mUserOperations;
            }
        }

        #endregion UserOperations

        #region <2> UserRoles

        /// <summary>
        /// Get a List of User Roles
        /// </summary>
        public List<UserRole> UserRoles
        {
            get
            {
                if (mUserRoles == null)
                {
                    mUserRoles = new List<UserRole>()
                       {
                           new UserRole ()
                            { 
                                UserRoleID=Guid.NewGuid(),
                              
                                UserID=this.Users[0].UserID,
                                User=this.Users[0],
                              
                                RoleID=this.Roles[0].RoleID,
                                Role=this.Roles[0]

                            },

                           new UserRole ()
                            { 
                                UserRoleID=Guid.NewGuid(),
                              
                                UserID=this.Users[1].UserID,
                                User=this.Users[1],
                                
                                RoleID=this.Roles[1].RoleID,
                                Role=this.Roles[1]

                            }
                           
                       };
                }
                return mUserRoles;
            }
        }

        #endregion UserRoles

        #endregion Mock Objects


        /// <summary>
        /// Context of Service eNeg
        /// </summary>
        private eNegContext Context
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
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public Microsoft.VisualStudio.TestTools.UnitTesting.TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }


        #endregion Properties

        #region → Constructors   .

        public eNegServiceTest()
        {
            CountOfAllEntries =
                              this.AccountTypesCount +
                              this.ApplicationsCount +
                              this.PreferedLanguagesCount +
                              this.ProfilesCount +
                              this.RightsCount +
                              this.RolesCount +
                              this.SecurityQuestionsCount +

                              this.ChannelsCount +
                              this.ChannelTypesCount +

                              this.CountrysCount +


                              this.NegotiationsCount +
                              this.ConversationsCount +
                              this.MessagesCount +
                              this.AttachementsCount +


                              this.NegotiationApplicationStatussCount +
                              this.NegotiationStatusCount +



                              this.RoleRightsCount +
                              this.UsersCount +
                              this.UserOperationsCount +
                              this.UserRolesCount;
             
        }
        #endregion

        #region → Methods        .


        #region Test Insert All Entries


        /// <summary>
        ///A test for Insert All Entries
        ///</summary>
        [TestMethod()]
        [Description(@"
Test Insert - Update - Delete 

For the following tables :-

    AccountType, Application, Attachement, Channel, ChannelType, Conversation, Country , Message 
    Negotiation, NegotiationStatus, PreferedLanguage, NegotiationApplicationStatus, Profile, Right,
    Role, RoleRight, SecurityQuestion, User, UserOperation, UserRole 

")]
        public void TestAllOperations()
        {
            try
            {





                foreach (var item in this.AccountTypes)
                {
                    this.Context.AccountTypes.Add(item);
                }

                foreach (var item in this.Applications)
                {
                    this.Context.Applications.Add(item);
                }

                foreach (var item in this.Attachements)
                {
                    this.Context.Attachements.Add(item);
                }

                foreach (var item in this.Channels)
                {
                    this.Context.Channels.Add(item);
                }

                foreach (var item in this.ChannelTypes)
                {
                    this.Context.ChannelTypes.Add(item);
                }

                foreach (var item in this.Conversations)
                {
                    this.Context.Conversations.Add(item);
                }

                foreach (var item in this.Countries)
                {
                    this.Context.Countries.Add(item);
                }

                foreach (var item in this.Messages)
                {
                    this.Context.Messages.Add(item);
                }

                foreach (var item in this.Negotiations)
                {
                    this.Context.Negotiations.Add(item);
                }

                foreach (var item in this.NegotiationApplicationStatuss)
                {
                    this.Context.NegotiationApplicationStatus.Add(item);
                }

                foreach (var item in this.NegotiationStatus)
                {
                    this.Context.NegotiationStatus.Add(item);
                }

                foreach (var item in this.PreferedLanguages)
                {
                    this.Context.PreferedLanguages.Add(item);
                }

                foreach (var item in this.Profiles)
                {
                    this.Context.Profiles.Add(item);
                }

                foreach (var item in this.Rights)
                {
                    this.Context.Rights.Add(item);
                }

                foreach (var item in this.Roles)
                {
                    this.Context.Roles.Add(item);
                }

                foreach (var item in this.RoleRights)
                {
                    this.Context.RoleRights.Add(item);
                }

                foreach (var item in this.SecurityQuestions)
                {
                    this.Context.SecurityQuestions.Add(item);
                }

                foreach (var item in this.Users)
                {
                    this.Context.Users.Add(item);
                }

                foreach (var item in this.UserOperations)
                {
                    this.Context.UserOperations.Add(item);
                }

                foreach (var item in this.UserRoles)
                {
                    this.Context.UserRoles.Add(item);
                }


                this.Context.SubmitChanges(new Action<SubmitOperation>(InsertAllEntriesComplete), null);
            }
            catch (Exception ex)
            {
                eNegMessageBox.ShowMessageBox(false, "InsertAllEntries", ex);
            }
        }


        void InsertAllEntriesComplete(SubmitOperation subOp)
        {
            if (!subOp.HasError)
            {

                if (subOp.ChangeSet.AddedEntities.Count != this.CountOfAllEntries)
                {
                    eNegMessageBox.ShowMessageBox(false, "InsertAllEntriesComplete", "Number of Records Inserted is not right.");
                }
                else
                {
                    UpdateAllEntries();
                }
            }
            else
            {
                eNegMessageBox.ShowMessageBox(false, "InsertAllEntriesComplete", subOp.Error);
            }
        }

        #endregion Test Insert All Entries

        #region Test Update All Entries


        /// <summary>
        ///A test for Update All Entries
        ///</summary>
        public void UpdateAllEntries()
        {
            try
            {
                this.Context.RejectChanges();
                foreach (var item in this.Context.AccountTypes)
                {
                    item.TypeDescription = item.TypeDescription + "_Update";
                }



                foreach (var item in this.Context.Applications)
                {
                    item.ApplicationTitle = item.ApplicationTitle + "_Update";
                }



                foreach (var item in this.Context.Attachements)
                {
                    item.AttachementName = item.AttachementName + "_Update";
                }



                foreach (var item in this.Context.Channels)
                {
                    item.ChannelName = item.ChannelName + "_Update";
                }



                foreach (var item in this.Context.ChannelTypes)
                {
                    item.TypeName = item.TypeName + "_Update";
                }



                foreach (var item in this.Context.Conversations)
                {
                    item.ConversationName = item.ConversationName + "_Update";
                }



                foreach (var item in this.Context.Countries)
                {
                    item.CountryName = item.CountryName + "_Update";
                }



                foreach (var item in this.Context.Messages)
                {
                    item.MessageSender = item.MessageSender + "_Update";
                }



                foreach (var item in this.Context.Negotiations)
                {
                    item.NegotiationName = item.NegotiationName + "_Update";
                }



                foreach (var item in this.Context.NegotiationApplicationStatus)
                {
                    item.Active = true;
                }



                foreach (var item in this.Context.NegotiationStatus)
                {
                    item.StatusDescription = item.StatusDescription + "_Update";
                }



                foreach (var item in this.Context.PreferedLanguages)
                {
                    item.PreferedLanguage1 = item.PreferedLanguage1 + "_Update";
                }



                foreach (var item in this.Context.Profiles)
                {
                    item.CurrentTheme = item.CurrentTheme + "_Update";
                }



                foreach (var item in this.Context.Rights)
                {
                    item.RightDescription = item.RightDescription + "_Update";
                }



                foreach (var item in this.Context.Roles)
                {
                    item.RoleName = item.RoleName + "_Update";
                }



                foreach (var item in this.Context.RoleRights)
                {
                    if (item.RightID == this.Rights[0].RightID)
                    {
                        item.RightID = this.Rights[1].RightID;
                    }
                    else
                    {
                        item.RightID = this.Rights[0].RightID;
                    }
                }



                foreach (var item in this.Context.SecurityQuestions)
                {
                    item.Question = item.Question + "_Update";
                }



                foreach (var item in this.Context.Users)
                {
                    item.CompanyName = item.CompanyName + "_Update";
                }



                foreach (var item in this.Context.UserOperations)
                {
                    item.OperationString = item.OperationString + "_Update";
                }



                foreach (var item in this.Context.UserRoles)
                {

                    if (item.UserID == this.Users[0].UserID)
                    {
                        item.UserID = this.Users[1].UserID;
                    }
                    else
                    {
                        item.UserID = this.Users[0].UserID;
                    }

                }


                this.Context.SubmitChanges(new Action<SubmitOperation>(UpdateAllEntriesComplete), null);
            }
            catch (Exception ex)
            {
                eNegMessageBox.ShowMessageBox(false, "UpdateAllEntries", ex);
            }
        }

        /// <summary>
        /// Event Complete of  Update All Entries
        /// </summary>
        /// <param name="subOp">Value of subOp</param>
        void UpdateAllEntriesComplete(SubmitOperation subOp)
        {
            if (!subOp.HasError)
            {

                if (subOp.ChangeSet.AddedEntities.Count == 0 &&
                    subOp.ChangeSet.RemovedEntities.Count == 0 &&
                    subOp.ChangeSet.ModifiedEntities.Count != this.CountOfAllEntries)
                {
                    eNegMessageBox.ShowMessageBox(false, "UpdateAllEntriesComplete", "Number of Records Inserted is not right.");
                }
                else
                {
                    DeleteAllEntries();
                }
            }
            else
            {
                eNegMessageBox.ShowMessageBox(false, "UpdateAllEntriesComplete", subOp.Error);
            }
        }

        #endregion Test Update All Entries

        #region Test Delete All Entries


        /// <summary>
        ///A test for Delete All Entries
        ///only for Delete Flag
        ///</summary>
        public void DeleteAllEntries()
        {
            try
            {
                this.Context.RejectChanges();

                this.CountOfAllEntries = this.MessagesCount +
                                         this.ConversationsCount +
                                         this.NegotiationsCount +
                                         this.UsersCount;


                while (this.Messages.Count > 0)
                {
                    this.Context.Messages.Remove(this.Messages[0]);
                    this.Messages.RemoveAt(0);
                }


                while (this.Conversations.Count > 0)
                {
                    this.Context.Conversations.Remove(this.Conversations[0]);
                    this.Conversations.RemoveAt(0);
                }


                while (this.Negotiations.Count > 0)
                {
                    this.Context.Negotiations.Remove(this.Negotiations[0]);
                    this.Negotiations.RemoveAt(0);
                }



                while (this.Users.Count > 0)
                {
                    this.Context.Users.Remove(this.Users[0]);
                    this.Users.RemoveAt(0);
                }


                this.Context.SubmitChanges(new Action<SubmitOperation>(DeleteAllEntriesComplete), null);
            }
            catch (Exception ex)
            {
                eNegMessageBox.ShowMessageBox(false, "DeleteAllEntries", ex);
            }
        }

        /// <summary>
        /// Event Complete of  Delete All Entries
        /// </summary>
        /// <param name="subOp">Value of subOp</param>
        void DeleteAllEntriesComplete(SubmitOperation subOp)
        {
            if (!subOp.HasError)
            {

                if (subOp.ChangeSet.AddedEntities.Count == 0 &&
                    subOp.ChangeSet.ModifiedEntities.Count == 0 &&
                    subOp.ChangeSet.RemovedEntities.Count != this.CountOfAllEntries )
                {
                    eNegMessageBox.ShowMessageBox(false, "DeleteAllEntriesComplete", "Number of Records Inserted is not right.");
                }
                else
                {
                    eNegMessageBox.ShowMessageBox(true, "Inset - Update - Delete All Entries ", DeleteString);
                }
            }
            else
            {
                eNegMessageBox.ShowMessageBox(false, "DeleteAllEntriesComplete" , subOp.Error);
            }
        }

        #endregion Test Delete All Entries


        /// <summary>
        /// get SQL Statement to Clear Database
        /// </summary>
        private string DeleteString
        {
            get
            {

                return @"
---------------------------------------------------
You must run these SQL commands Before retest again
---------------------------------------------------

DELETE [Attachement];
DELETE [Message];
DELETE [Conversation];
DELETE [Category];
DELETE [CategoryLog];
DELETE [History];
DELETE [Log];
DELETE [NegotiationApplicationStatus];
DELETE [UserOperation];
DELETE [UserRole];
DELETE [Negotiation];
DELETE [Profile];
DELETE [User];
DELETE [Channel];
DELETE [ChannelType];
DELETE [AccountType];
DELETE [ActionType];
DELETE [Application];
DELETE [SecurityQuestion];
DELETE [RoleRight];
DELETE [Right];
DELETE [Role];
DELETE [PreferedLanguage];
DELETE [Country];
DELETE [NegotiationStatus];";
            }
        }

        #endregion

    }
}
