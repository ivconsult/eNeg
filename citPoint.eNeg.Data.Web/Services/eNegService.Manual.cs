#region → Usings   .
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.ServiceModel.DomainServices.EntityFramework;
using System.ServiceModel.DomainServices.Server;
using System.ServiceModel.Channels;
using System.ServiceModel;
using citPOINT.eNeg.Common;
using System.Collections.ObjectModel;
using iTextSharp.text;
using System.IO;
using iTextSharp.text.pdf;
using System.Web;
#endregion

#region → History  .

/* Date         User              Change
 * 
 * 02.08.10     M Wahab       Creation
 */

# endregion

#region → ToDos    .

/*
 * Date         set by User     Description
 * 
 * 
*/

# endregion
namespace citPOINT.eNeg.Data.Web
{
    // Implements application logic using the eNegEntities context.
    // Also consider adding roles to restrict access as appropriate.
    // [RequiresAuthentication]
    public partial class eNegService
    {

        #region → Fields         .
        LogInService mLoginService = new LogInService();

        #region → ExportPDF      .

        Rectangle r;
        Document exportedDocument;
        private PdfContentByte _pcb;
        MemoryStream memoryStream;
        PdfWriter writer;
        iTextSharp.text.Image imageHeader;
        PdfReader readerCover;
        PdfTemplate background;
        string path = HttpContext.Current.Request.PhysicalApplicationPath;

        //private readonly BaseFont CenteredTitle = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, false);
        Font CenteredTitle = new Font(Font.FontFamily.TIMES_ROMAN, 20f, Font.BOLD);
        Font SubTitle = new Font(Font.FontFamily.TIMES_ROMAN, 10f, Font.BOLD);
        Font SubTitleValue = new Font(Font.FontFamily.TIMES_ROMAN, 10f, Font.NORMAL);
        Font Normal = new Font(Font.FontFamily.TIMES_ROMAN, 9f, Font.NORMAL);

        Chunk NewLine = new Chunk(Environment.NewLine);
        #endregion

        #endregion

        #region → Methods        .

        #region → Public         .

        #region → GeneratePDF    .

        /// <summary>
        /// Creates the specified conversations.
        /// </summary>
        /// <param name="conversationIDs">The conversation I ds.</param>
        /// <returns>
        /// Stream of bytes containing the generated PDF
        /// </returns>
        [Invoke]
        public byte[] GeneratePDFConversations(Guid[] conversationIDs)
        {
            try
            {
                var conversations = this.ObjectContext.Conversations.Where(s => conversationIDs.Contains(s.ConversationID))
                    .Where(s => s.Deleted == false);

                //Set the document width & height, prepare write and output stream
                PreparePDFDocument();

                //Add page header to pdf document using eNegpdfPageEventHandler
                SetPageHeader();

                //Open the pdf document to start writing inside
                exportedDocument.Open();

                //Add cover to the pdf document as a start page
                SetPageCover();

                //create a new page
                exportedDocument.NewPage();

                //print the negotiation name in the pdf centered
                PrintTextCentered(conversations.First().Negotiation.NegotiationName);

                foreach (var conv in conversations)
                {
                    //print the conversation name in the pdf centered
                    PrintTextCentered(conv.ConversationName);

                    var Msgs = conv.Messages;

                    PrintMessages(Msgs);
                }

                exportedDocument.Close();

                return memoryStream.GetBuffer();
            }
            catch (DocumentException dex)
            {
                throw (dex);
            }
            catch (IOException ioex)
            {
                throw (ioex);
            }
            finally
            {
                exportedDocument.Close();
                writer.Flush();
            }
        }

        /// <summary>
        /// Generates the PDF messages.
        /// </summary>
        /// <param name="messagesIDs">The messages I ds.</param>
        /// <returns></returns>
        [Invoke]
        public byte[] GeneratePDFMessages(Guid[] messagesIDs)
        {
            try
            {
                var messages = this.ObjectContext.Messages.Where(s => messagesIDs.Contains(s.MessageID))
                    .Where(s => s.Deleted == false);

                //Set the document width & height, prepare write and output stream
                PreparePDFDocument();

                //Add page header to pdf document using eNegpdfPageEventHandler
                SetPageHeader();

                //Open the pdf document to start writing inside
                exportedDocument.Open();

                //Add cover to the pdf document as a start page
                SetPageCover();

                //create a new page
                exportedDocument.NewPage();

                //print the negotiation name in the pdf centered
                PrintTextCentered(messages.First().Conversation.Negotiation.NegotiationName);

                //print the conversation name in the pdf centered
                PrintTextCentered(messages.First().Conversation.ConversationName);

                PrintMessages(messages);

                exportedDocument.Close();

                return memoryStream.GetBuffer();
            }
            catch (DocumentException dex)
            {
                throw (dex);
            }
            catch (IOException ioex)
            {
                throw (ioex);
            }
            finally
            {
                exportedDocument.Close();
                writer.Flush();
            }
        }

        #endregion

        #region → To Do in coming implementation                           .

        /*                       ▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀
                                ▀               ToDo:              ▀
                               ▀ Add the following snippet of code  ▀
                              ▀    to check on authentication in any ▀ 
                             ▀       published service method         ▀
                         ▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀
                         ▀                                                  ▀
                         ▀    if (ServiceAuthentication.IsValid())          ▀
                         ▀   {                                              ▀
                         ▀                                                  ▀
                         ▀   }                                              ▀
                         ▀   else                                           ▀
                         ▀   {                                              ▀
                         ▀       // throw fault exception to indicate       ▀
                         ▀       // the caller that the service need        ▀
                         ▀       // valid credentials                       ▀
                         ▀       throw new                                  ▀
                         ▀         FaultException<InvalidOperationException>▀
                         ▀          (new InvalidOperationException          ▀
                         ▀          ("Invalid credentials"),                ▀
                         ▀            "Invalid credentials");               ▀
                         ▀   }                                              ▀
                         ▀                                                  ▀
                         ▀                                                  ▀
                         ▀ ▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀*/

        #endregion

        #region → Services Created especially for Mobile Add-on            .

        /// <summary>
        /// Add a message to conversation from outside eNeg from a different Add-on.
        /// </summary>
        /// <param name="AppName">Name of the app.</param>
        /// <param name="UserID">The user ID.</param>
        /// <param name="conversationID">The conversation ID.</param>
        /// <param name="messageContent">Content of the message.</param>
        /// <param name="messageSubject">The message subject.</param>
        /// <param name="messageSender">The message sender.</param>
        /// <param name="messageReceiver">The message receiver.</param>
        /// <param name="isSent">if set to <c>true</c> [is sent].</param>
        /// <returns>
        /// a string of New Message ID if Added Sucessfully and return Null if add process fail
        /// </returns>
        public string AddMessageToConversation(string AppName, Guid UserID, Guid conversationID, string messageContent, string messageSubject, string messageSender, string messageReceiver, bool isSent)
        {
            if (ServiceAuthentication.IsValid())
            {
                #region → Get Perfect Channel by App Name .

                var channel = this.ObjectContext.Channels.FirstOrDefault(s => s.ChannelName.ToLower() == AppName.ToLower());

                if (channel == null)
                {
                    channel = this.ObjectContext.Channels.FirstOrDefault();
                }

                if (channel == null)
                {
                    return null;
                }

                #endregion

                Guid messageID = Guid.NewGuid();

                this.ObjectContext.AddToMessages(
                                                new Message()
                                                {
                                                    ConversationID = conversationID,
                                                    DeletedBy = UserID,
                                                    ChannelID = channel.ChannelID,
                                                    MessageContent = messageContent,
                                                    MessageSubject = messageSubject,
                                                    MessageSender = string.Format("<{0}>", messageSender),
                                                    MessageReceiver = string.Format("<{0}>", messageReceiver),
                                                    IsSent = isSent,
                                                    Deleted = false,
                                                    DeletedOn = DateTime.Now,
                                                    MessageDate = DateTime.Now,
                                                    MessageID = messageID,
                                                    IsAppsMessage = false
                                                });

                if (this.ObjectContext.SaveChanges() > 0)
                {
                    return messageID.ToString();
                }
                else
                {
                    return null;
                }
            }
            else
            {
                //// throw fault  
                throw new FaultException<InvalidOperationException>(new InvalidOperationException("Invalid credentials"), "Invalid credentials");
            }
        }

        /// <summary>
        /// Gets list of negotiations related to specific user using User ID.
        /// </summary>
        /// <param name="UserID">The user ID.</param>
        /// <returns>List of Negotiations</returns>
        public IQueryable<Negotiation> GetNegotiationsByUserID(Guid UserID)
        {
            if (ServiceAuthentication.IsValid())
            {
                var Qry = this.ObjectContext.Negotiations.Where(s => s.UserID == UserID && s.Deleted == false);
                return Qry;
            }
            else
            {
                //// throw fault  
                throw new FaultException<InvalidOperationException>(new InvalidOperationException("Invalid credentials"), "Invalid credentials");
            }
        }

        /// <summary>
        /// Gets the active support applications for conversation.
        /// </summary>
        /// <param name="conversationID">The conversation ID.</param>
        /// <returns></returns>
        public IQueryable<Application> GetActiveSupportApplicationsForConversation(Guid conversationID)
        {
            if (ServiceAuthentication.IsValid())
            {
                List<Application> Apps = new List<Application>();

                Conversation Conv = this.ObjectContext.Conversations.Where(s => s.ConversationID == conversationID && s.Deleted == false).FirstOrDefault();

                if (Conv != null)
                {
                    Negotiation Neg = this.ObjectContext.Negotiations.Where(s => s.NegotiationID == Conv.NegotiationID).FirstOrDefault();

                    if (Neg != null)
                    {
                        List<NegotiationApplicationStatu> NegAppStatus = this.ObjectContext.NegotiationApplicationStatus.
                            Where(s => s.NegotiationID == Neg.NegotiationID && s.Active == true).ToList();

                        List<Guid> tmpGuid = new List<Guid>();

                        foreach (var item in NegAppStatus)
                        {
                            tmpGuid.Add(item.ApplicationID);
                        }

                        if (NegAppStatus != null)
                        {
                            return this.ObjectContext.Applications.Where(s => tmpGuid.Contains(s.ApplicationID));
                        }
                    }
                }

                return null;
            }
            else
            {
                //// throw fault  
                throw new FaultException<InvalidOperationException>(new InvalidOperationException("Invalid credentials"), "Invalid credentials");
            }
        }

        #endregion

        #region → Services can be invoked thought WebService from PrefApp  .

        /// <summary>
        /// Used to check whether user can login on App or not
        /// </summary>
        /// <param name="UserName">Value of UserName</param>
        /// <param name="Password">Value of Password</param>
        /// <returns>IQueryable of user</returns>
        public IQueryable<User> UserCanLogin(string UserName, string Password)
        {
            string UserData = null;

            IQueryable<User> foundUser = this.ObjectContext.UserCanLogin(UserName).AsQueryable<User>();
            if (foundUser.FirstOrDefault() != null)
            {
                if (mLoginService.ValidateUser(UserName, Password, "", out UserData))
                {
                    return this.ObjectContext.UserCanLogin(UserName).AsQueryable<User>();
                }
                else
                {
                    List<User> DefaultUser = new List<User>()
                    {
                        new User()
                        {
                            EmailAddress = string.Empty,
                            Locked = true,
                            Disabled = true,
                            Online = false,
                            UserID = Guid.NewGuid()
                        }
                    };
                    return DefaultUser.AsQueryable<User>();
                }
            }
            else
            {
                return this.ObjectContext.UserCanLogin(UserName).AsQueryable<User>();
            }
        }

        /// <summary>
        /// Gets the user by ID.
        /// </summary>
        /// <param name="UserID">The user ID.</param>
        /// <returns></returns>
        public IQueryable<User> GetUserByID(Guid UserID)
        {
            if (ServiceAuthentication.IsValid())
            {
                return this.ObjectContext.GetUserByID(UserID).AsQueryable<User>();
            }
            else
            {
                //// throw fault  
                throw new FaultException<InvalidOperationException>(new InvalidOperationException("Invalid credentials"), "Invalid credentials");
            }
        }

        /// <summary>
        /// Update User Make him Online
        /// </summary>
        /// <param name="UserID">Value Of UserID</param>
        /// <param name="IPAddress">The IP address.</param>
        /// <returns>IQueyrable of User</returns>
        public IQueryable<User> MakeUserOnline(Guid? UserID, string IPAddress)
        {
            if (ServiceAuthentication.IsValid())
            {
                return this.ObjectContext.MakeUserOnline(UserID, IPAddress).AsQueryable<User>();
            }
            else
            {
                //// throw fault  
                throw new FaultException<InvalidOperationException>(new InvalidOperationException("Invalid credentials"), "Invalid credentials");
            }
        }

        /// <summary>
        /// Update User Make him Offline
        /// </summary>
        /// <param name="UserID">Value Of UserID</param>
        /// <returns>IQueyrable of User</returns>
        public IQueryable<User> MakeUserOffline(Guid? UserID)
        {
            if (ServiceAuthentication.IsValid())
            {
                return this.ObjectContext.MakeUserOffline(UserID).AsQueryable<User>();
            }
            else
            {
                //// throw fault  
                throw new FaultException<InvalidOperationException>(new InvalidOperationException("Invalid credentials"), "Invalid credentials");
            }
        }

        /// <summary>
        /// Gets the active apps for DM.
        /// </summary>
        /// <param name="UserID">The user ID.</param>
        /// <returns>Active Apps for Data Matching in Addon</returns>
        public IQueryable<UserApplicationStatu> GetAppsActiveForDM(Guid UserID)
        {
            if (ServiceAuthentication.IsValid())
            {
                return this.ObjectContext.UserApplicationStatus.Where(s => s.UserID == UserID);
            }
            else
            {
                //// throw fault  
                throw new FaultException<InvalidOperationException>(new InvalidOperationException("Invalid credentials"), "Invalid credentials");
            }
        }

        /// <summary>
        /// Updates the data matching in addon.
        /// </summary>
        /// <param name="AppName">Name of the app.</param>
        /// <param name="UserID">The user ID.</param>
        /// <param name="IsActive">if set to <c>true</c> [is active].</param>
        /// <returns></returns>
        public IQueryable<UserApplicationStatu> UpdateDataMatchingStatusInAddon(string AppName, Guid UserID, bool IsActive)
        {
            if (ServiceAuthentication.IsValid())
            {
                return this.ObjectContext.DataMatchingInAddonUpdate(AppName, UserID, IsActive).AsQueryable<UserApplicationStatu>();
            }
            else
            {
                //// throw fault  
                throw new FaultException<InvalidOperationException>(new InvalidOperationException("Invalid credentials"), "Invalid credentials");
            }
        }

        /// <summary>
        /// Retrieves the application DM status.
        /// </summary>
        /// <param name="AppName">Name of the app.</param>
        /// <param name="UserID">The user ID.</param>
        /// <returns></returns>
        public IQueryable<UserApplicationStatu> RetrieveApplicationDMStatus(string AppName, Guid UserID)
        {
            if (ServiceAuthentication.IsValid())
            {
                return this.ObjectContext.RetrieveApplicationDMStatus(AppName, UserID).AsQueryable<UserApplicationStatu>();
            }
            else
            {
                //// throw fault  
                throw new FaultException<InvalidOperationException>(new InvalidOperationException("Invalid credentials"), "Invalid credentials");
            }
        }

        /// <summary>
        /// Gets the available negotiations to analysis.
        /// </summary>
        /// <param name="UserID">The user ID.</param>
        /// <param name="AppName">Name of the app.</param>
        /// <returns>Negotiations</returns>
        public IQueryable<Negotiation> GetAvailableNegotiationsToAnalysis(Guid UserID, string AppName)
        {
            if (ServiceAuthentication.IsValid())
            {
                var Qry = this.ObjectContext.Negotiations.Where(s => s.UserID == UserID &&
                                     s.StatusID == new Guid("e3b0b130-133e-4c1d-957c-14c67869446c") && s.Deleted == false)
                                    .Where(s => s.NegotiationApplicationStatus.Where(g => g.Application.ApplicationTitle == AppName && g.Active).FirstOrDefault() != null).OrderBy(s => s.NegotiationName);
                return Qry;
            }
            else
            {
                //// throw fault  
                throw new FaultException<InvalidOperationException>(new InvalidOperationException("Invalid credentials"), "Invalid credentials");
            }
        }

        /// <summary>
        /// Gets the negotiations by list of certain IDs.
        /// </summary>
        /// <param name="NegIDs">List of certain ID</param>
        /// <returns>Negotiations</returns>
        [Query(HasSideEffects = true)]
        public IQueryable<Negotiation> GetNegotiationsByListOfIDs(Guid[] NegIDs)
        {
            if (ServiceAuthentication.IsValid())
            {
                var Qry = this.ObjectContext.Negotiations.Where(s => NegIDs.Contains(s.NegotiationID)).Where(s => s.Deleted == false).OrderBy(s => s.NegotiationName);
                return Qry;
            }
            else
            {
                throw new FaultException<InvalidOperationException>(new InvalidOperationException("Invalid credentials"), "Invalid credentials");
            }
        }

        /// <summary>
        /// Get Conversations By Negotiation ID
        /// </summary>
        /// <param name="NegIDs">array of neg IDs</param>
        /// <returns>Conversations</returns>
        [Query(HasSideEffects = true)]
        public IQueryable<Conversation> GetConversationsByNegotiationID(Guid[] NegIDs)
        {
            if (ServiceAuthentication.IsValid())
            {
                var Qry = this.ObjectContext.Conversations.Where(s => NegIDs.Contains(s.NegotiationID) && s.Deleted == false);//.OrderBy(s => s.ConversationName);
                return Qry;
            }
            else
            {
                //// throw fault  
                throw new FaultException<InvalidOperationException>(new InvalidOperationException("Invalid credentials"), "Invalid credentials");
            }
        }

        /// <summary>
        /// Get Messages By Negotiation IDs
        /// </summary>
        /// <param name="NegotiationIDS">The negotiation IDS.</param>
        /// <returns>Messages</returns>
        [Query(HasSideEffects = true)]
        public IQueryable<Message> GetMessagesByNegotiationID(Guid?[] NegotiationIDS)
        {
            if (ServiceAuthentication.IsValid())
            {
                var Qry = this.ObjectContext.Messages.Where(s => NegotiationIDS.Contains(s.Conversation.NegotiationID)).Where(g => g.Deleted == false).OrderByDescending(s => s.MessageDate);
                return Qry;
            }
            else
            {
                //// throw fault  
                throw new FaultException<InvalidOperationException>(new InvalidOperationException("Invalid credentials"), "Invalid credentials");
            }
        }

        /// <summary>
        /// Gets the messages by negotiation ID for apps.
        /// </summary>
        /// <param name="NegotiationIDS">The negotiation IDS.</param>
        /// <returns>Messages</returns>
        [Query(HasSideEffects = true)]
        public IQueryable<Message> GetMessagesByNegotiationIDForApps(Guid?[] NegotiationIDS)
        {
            if (ServiceAuthentication.IsValid())
            {
                var Qry = this.ObjectContext.Messages
                                                        .Where(s => NegotiationIDS.Contains(s.Conversation.NegotiationID) &&
                                                                    s.Deleted == false && s.IsAppsMessage == false)
                                                        .OrderByDescending(s => s.MessageDate);
                return Qry;
            }
            else
            {
                //// throw fault  
                throw new FaultException<InvalidOperationException>(new InvalidOperationException("Invalid credentials"), "Invalid credentials");
            }
        }

        /// <summary>
        /// Updates the apps statisticals by messages.
        /// </summary>
        /// <param name="AppName">Name of the app.</param>
        /// <param name="UserID">The user ID.</param>
        /// <param name="conversationID">The conversation ID.</param>
        /// <param name="messageContent">Content of the message.</param>
        /// <param name="messageSubject">The message subject.</param>
        /// <param name="messageSender">The message sender.</param>
        /// <param name="messageReceiver">The message receiver.</param>
        /// <returns>bool value indicate add new message success or failed</returns>
        public bool UpdateAppsStatisticalsByMessages(string AppName, Guid UserID, Guid conversationID, string messageContent, string messageSubject, string messageSender, string messageReceiver)
        {
            if (ServiceAuthentication.IsValid())
            {
                #region → Get Perfect Channel by App Name .

                var channel = this.ObjectContext.Channels.FirstOrDefault(s => s.ChannelName.ToLower() == AppName.ToLower());

                if (channel == null)
                {
                    channel = this.ObjectContext.Channels.FirstOrDefault();
                }

                if (channel == null)
                {
                    return false;
                }

                #endregion

                this.ObjectContext.AddToMessages(
                                                new Message()
                                                {
                                                    ConversationID = conversationID,
                                                    DeletedBy = UserID,
                                                    ChannelID = channel.ChannelID,
                                                    MessageContent = messageContent,
                                                    MessageSubject = messageSubject,
                                                    MessageSender = string.Format("<{0}>", messageSender),
                                                    MessageReceiver = string.Format("<{0}>", messageReceiver),
                                                    IsSent = false,
                                                    Deleted = false,
                                                    DeletedOn = DateTime.Now,
                                                    MessageDate = DateTime.Now,
                                                    MessageID = Guid.NewGuid(),
                                                    IsAppsMessage = true
                                                });

                return this.ObjectContext.SaveChanges() > 0;
            }
            else
            {
                //// throw fault  
                throw new FaultException<InvalidOperationException>(new InvalidOperationException("Invalid credentials"), "Invalid credentials");
            }
        }


        /// <summary>
        /// Gets the partner mail for conversation.
        /// Used in Culture App to Know the culture of partner.
        /// </summary>
        /// <param name="conversationID">The conversation ID.</param>
        /// <returns></returns>
        public string GetPartnerMailForConversation(Guid conversationID)
        {
            if (ServiceAuthentication.IsValid())
            {
                var msg = this.ObjectContext
                             .Messages
                             .Where(ss => ss.ConversationID == conversationID &&
                                          ss.IsAppsMessage == false &&
                                          ss.Deleted == false)
                             .OrderByDescending(oo => oo.MessageDate)
                             .Take(1);

                if (msg == null || msg.Count() == 0)
                {
                    return null;
                }
                else if (msg.First().IsSent)
                {
                    return msg.First().MessageReceiver;
                }
                else
                {
                    return msg.First().MessageSender;
                }
            }
            else
            {
                //// throw fault  
                throw new FaultException<InvalidOperationException>(new InvalidOperationException("Invalid credentials"), "Invalid credentials");
            }

        }

        #endregion

        #region → Services can be invoked through eSource App              .

        /// <summary>
        /// Updates the user frome source.
        /// </summary>
        /// <param name="userID">The user ID.</param>
        /// <param name="fName">Name of the f.</param>
        /// <param name="lName">Name of the l.</param>
        /// <param name="gender">if set to <c>true</c> [gender].</param>
        /// <param name="companyName">Name of the company.</param>
        /// <returns></returns>
        public bool UpdateUserFromeSource(Guid userID, string fName, string lName, bool gender, string companyName)
        {
            if (ServiceAuthentication.IsValid())
            {
                User FoundUser = UpdateReset(userID).FirstOrDefault();
                if (FoundUser != null)
                {
                    FoundUser.FirstName = fName;
                    FoundUser.LastName = lName;
                    FoundUser.Gender = gender;
                    FoundUser.CompanyName = companyName;

                    this.ObjectContext.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                //// throw fault  
                throw new FaultException<InvalidOperationException>(new InvalidOperationException("Invalid credentials"), "Invalid credentials");
            }
        }

        #endregion

        #region → User                                                     .

        /// <summary>
        /// Update Reset Flag to Be True
        /// </summary>
        /// <param name="UserID">Value Of UserID</param>
        /// <returns>IQueyrable of User</returns>
        public IQueryable<User> UpdateReset(Guid? UserID)
        {
            if (ServiceAuthentication.IsValid())
            {
                return this.ObjectContext.UserResetByUserID(UserID).AsQueryable<User>();
            }
            else
            {
                //// throw fault  
                throw new FaultException<InvalidOperationException>(new InvalidOperationException("Invalid credentials"), "Invalid credentials");
            }
        }

        /// <summary>
        /// To Get User In case of User Confirmation Mail
        /// Or Incase Of User ResetRequest
        /// </summary>
        /// <param name="OperationString">Value Of OperationString</param>
        /// <param name="OperationType">Value Of OperationType</param>
        /// <returns>IQueyrable of User</returns>
        public IQueryable<User> GetUserByOperationString(string OperationString, byte OperationType)
        {
            if (ServiceAuthentication.IsValid())
            {
                return this.ObjectContext.GetUserByOperationString(OperationString, OperationType).AsQueryable<User>();
            }
            else
            {
                //// throw fault  
                throw new FaultException<InvalidOperationException>(new InvalidOperationException("Invalid credentials"), "Invalid credentials");
            }
        }

        /// <summary>
        /// To Update User In case of User Confirmation Mail
        /// Or Incase Of User ResetRequest
        /// So It Update Lock And Delete Any Operations
        /// </summary>
        /// <param name="operationString">Value Of OperationString</param>
        /// <param name="operationStringType">Type of the operation string.</param>
        /// <returns>IQueyrable of User</returns>
        public IQueryable<User> UpdateUserByConfirmMail(string operationString, byte operationStringType)
        {
            if (ServiceAuthentication.IsValid())
            {
                return this.ObjectContext.UpdateUserByConfirmMail(operationString, operationStringType).AsQueryable<User>();
            }
            else
            {
                //// throw fault  
                throw new FaultException<InvalidOperationException>(new InvalidOperationException("Invalid credentials"), "Invalid credentials");
            }
        }

        /// <summary>
        /// For Deletign All Related Records In UserOperation Table
        /// And That in case if one Complete his Reset Login Operation
        /// and He Had not Change His EmailAddress.
        /// </summary>
        /// <param name="UserID">value of The UserID</param>
        /// <returns>The User Object</returns>
        public IQueryable<User> DeleteUserOperationByUserID(Guid UserID)
        {
            if (ServiceAuthentication.IsValid())
            {
                return this.ObjectContext.DeleteUserOperationByUserID(UserID).AsQueryable<User>();
            }
            else
            {
                //// throw fault  
                throw new FaultException<InvalidOperationException>(new InvalidOperationException("Invalid credentials"), "Invalid credentials");
            }
        }

        /// <summary>
        /// Insert user to object context 
        /// </summary>
        /// <param name="user">Value of User</param>
        public void InsertUser(User user)
        {
            // Re-generate password hash and password salt
            user.PasswordSalt = HashHelper.CreateRandomSalt();
            user.PasswordHash = HashHelper.ComputeSaltedHash(user.Password, user.PasswordSalt);

            if (!string.IsNullOrEmpty(user.PasswordAnswer))
            {
                // set a valid PasswordQuestion
                // Re-generate password hash and password salt
                user.AnswerSalt = HashHelper.CreateRandomSalt();
                user.AnswerHash = HashHelper.ComputeSaltedHash(user.PasswordAnswer, user.AnswerSalt);
            }

            user.IPAddress = user.ClientAddress;

            if ((user.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(user, EntityState.Added);
            }
            else
            {
                this.ObjectContext.User.AddObject(user);
            }
        }

        /// <summary>
        /// Update user in ObjectContext
        /// </summary>
        /// <param name="currentUser">Value of new User</param>
        public void UpdateUser(User currentUser)
        {


            if (!string.IsNullOrEmpty(currentUser.NewPassword) &&
                !string.IsNullOrEmpty(currentUser.NewPasswordConfirmation) &&
                currentUser.NewPassword == currentUser.NewPasswordConfirmation)
            {
                // Re-generate password hash and password salt
                currentUser.PasswordSalt = HashHelper.CreateRandomSalt();
                currentUser.PasswordHash = HashHelper.ComputeSaltedHash(currentUser.NewPassword, currentUser.PasswordSalt);
            }
            else
            {

                currentUser.PasswordSalt = "=-=-=-"; //Flag Mean Never change password
                currentUser.PasswordHash = "=-=-=-"; //Flag Mean Never change password
            }



            if (!string.IsNullOrEmpty(currentUser.PasswordAnswer))
            {
                // set a valid PasswordQuestion
                // Re-generate password hash and password salt
                currentUser.AnswerSalt = HashHelper.CreateRandomSalt();
                currentUser.AnswerHash = HashHelper.ComputeSaltedHash(currentUser.PasswordAnswer, currentUser.AnswerSalt);
            }


            currentUser.IPAddress = currentUser.ClientAddress;


            this.ObjectContext.User.AttachAsModified(currentUser, this.ChangeSet.GetOriginal(currentUser));
        }

        /// <summary>
        /// Gets the users count accept current user.
        /// </summary>
        /// <param name="UserID">The user ID.</param>
        /// <param name="IsForPublicProfile">if set to <c>true</c> [is for public profile].</param>
        /// <returns>Count of records exist</returns>
        [Invoke]
        public int GetUsersCountExceptCurrentUser(Guid UserID, bool IsForPublicProfile)
        {
            if (ServiceAuthentication.IsValid())
            {
                return ObjectContext.User
                                    .Where(s => s.UserID != UserID &&
                                                s.Disabled == false &&
                                                (IsForPublicProfile == false || s.HasPublicProfile == true))
                                    .Count();
            }
            else
            {
                //// throw fault  
                throw new FaultException<InvalidOperationException>(new InvalidOperationException("Invalid credentials"), "Invalid credentials");
            }
        }

        /// <summary>
        /// Gets the users count by alphabet except current user.
        /// </summary>
        /// <param name="Alphabet">The alphabet.</param>
        /// <param name="ColumnName">Name of the column.</param>
        /// <param name="UserID">The user ID.</param>
        /// <param name="IsForPublicProfile">if set to <c>true</c> [is for public profile].</param>
        /// <returns>Count of records exist</returns>
        [Invoke]
        public int GetUsersCountByAlphabetExceptCurrentUser(string Alphabet, string ColumnName, Guid UserID, bool IsForPublicProfile)
        {
            if (ServiceAuthentication.IsValid())
            {
                var Type = typeof(User);
                var Parameter = Expression.Parameter(Type, "Alphabet");

                Expression expr = Expression.Call(
                      Expression.Property(Parameter, Parameter.Type.GetProperty(ColumnName)),
                      typeof(string).GetMethod("StartsWith", new Type[] { typeof(string) }),
                      new Expression[] { Expression.Constant(Alphabet, typeof(string)) });


                return ObjectContext.User
                                    .Where(Expression.Lambda<Func<User, bool>>(expr, new ParameterExpression[] { Parameter }))
                                    .Where(s => s.UserID != UserID &&
                                                    s.Disabled == false &&
                                                    (IsForPublicProfile == false || s.HasPublicProfile == true))
                                    .Count();
            }
            else
            {
                //// throw fault  
                throw new FaultException<InvalidOperationException>(new InvalidOperationException("Invalid credentials"), "Invalid credentials");
            }
        }

        /// <summary>
        /// Finds the user.
        /// </summary>
        /// <param name="KeyWord">The key word.</param>
        /// <param name="UserID">The user ID.</param>
        /// <param name="IsForPublicProfile">if set to <c>true</c> [is for public profile].</param>
        /// <returns>IQueryable of user</returns>
        public IQueryable<User> FindUser(string KeyWord, Guid UserID, bool IsForPublicProfile)
        {
            if (ServiceAuthentication.IsValid())
            {
                return ObjectContext.FindUser(KeyWord, UserID, IsForPublicProfile).AsQueryable<User>();
            }
            else
            {
                //// throw fault  
                throw new FaultException<InvalidOperationException>(new InvalidOperationException("Invalid credentials"), "Invalid credentials");
            }
        }

        /// <summary>
        /// Finds the users count.
        /// </summary>
        /// <param name="KeyWord">The key word.</param>
        /// <param name="UserID">The user ID.</param>
        /// <param name="IsForPublicProfile">if set to <c>true</c> [is for public profile].</param>
        /// <returns>Count of Users in current Search</returns>
        public int? FindUsersCount(string KeyWord, Guid UserID, bool IsForPublicProfile)
        {
            if (ServiceAuthentication.IsValid())
            {
                return ObjectContext.FindUsersCount(KeyWord, UserID, IsForPublicProfile).SingleOrDefault();
            }
            else
            {
                //// throw fault  
                throw new FaultException<InvalidOperationException>(new InvalidOperationException("Invalid credentials"), "Invalid credentials");
            }
        }

        /// <summary>
        /// Gets the user profile statisticals.
        /// e.g. Negotiation Count 152
        ///      Conversation Count 144
        /// </summary>
        /// <param name="UserID">The user ID.</param>
        /// <returns></returns>
        public IQueryable<UserProfileStatisticalsResult> GetUserProfileStatisticals(Guid UserID)
        {
            return this.ObjectContext.GetUserProfileStatisticals(UserID).AsQueryable();
        }

        /// <summary>
        /// Determines whether this instance [can user see member profile] the specified current user ID.
        /// </summary>
        /// <param name="currentUserID">The current user ID.</param>
        /// <param name="memberUserID">The member user ID.</param>
        /// <returns>
        /// 	<c>true</c> if this instance [can user see member profile] the specified current user ID; otherwise, <c>false</c>.
        /// </returns>
        [Invoke]
        public bool CanUserSeeMemberProfile(Guid currentUserID, Guid memberUserID)
        {
            return this.ObjectContext.CanUserSeeMemberProfile(currentUserID, memberUserID).FirstOrDefault().Value;
        }

        #endregion

        #region → All services related to Negotiation  (Count-Pager)       .

        /// <summary>
        /// Used To Count the Number of Open Negotiation
        /// </summary>
        /// <param name="StatusType">Open Or Closed Or ... GUID</param>
        /// <param name="UserID">The user ID.</param>
        /// <returns>count of negotiation with certain status</returns>
        [Invoke]
        public int GetNegotiationCountByStatus(Guid StatusType, Guid UserID)
        {
            if (ServiceAuthentication.IsValid())
            {
                return this.ObjectContext.Negotiations.Where(s => s.StatusID == StatusType && s.UserID == UserID && s.Deleted == false).Count();
            }
            else
            {
                //// throw fault  
                throw new FaultException<InvalidOperationException>(new InvalidOperationException("Invalid credentials"), "Invalid credentials");
            }
        }

        /// <summary>
        /// Update Conversation
        /// </summary>
        /// <param name="currentConversation">New Conversation</param>
        public void UpdateConversation(Conversation currentConversation)
        {
            this.ObjectContext.Conversations.AttachAsModified(currentConversation, this.ChangeSet.GetOriginal(currentConversation));
        }

        /// <summary>
        /// Get Negotiations
        /// </summary>
        /// <returns>Negotiations</returns>
        public IQueryable<Negotiation> GetNegotiations()
        {
            if (ServiceAuthentication.IsValid())
            {
                return this.ObjectContext.Negotiations;
            }
            else
            {
                //// throw fault  
                throw new FaultException<InvalidOperationException>(new InvalidOperationException("Invalid credentials"), "Invalid credentials");
            }
        }

        /// <summary>
        /// Get Conversations
        /// </summary>
        /// <returns>Conversations</returns>
        [Query(IsDefault = true)]
        public IQueryable<Conversation> GetConversations()
        {
            if (ServiceAuthentication.IsValid())
            {
                return this.ObjectContext.Conversations;
            }
            else
            {
                //// throw fault  
                throw new FaultException<InvalidOperationException>(new InvalidOperationException("Invalid credentials"), "Invalid credentials");
            }
        }

        /// <summary>
        /// Get ApplicationStatus By Negotiation ID
        /// </summary>
        /// <param name="NegIDs">Array of negotiation IDs</param>
        /// <returns>NegotiationApplicationStatus</returns>
        public IQueryable<NegotiationApplicationStatu> GetNegotiationApplicationStatusByNegotiationID(Guid[] NegIDs)
        {
            if (ServiceAuthentication.IsValid())
            {
                var Qry = this.ObjectContext.NegotiationApplicationStatus.Where(s => NegIDs.Contains(s.NegotiationID));
                return Qry;
            }
            else
            {
                //// throw fault  
                throw new FaultException<InvalidOperationException>(new InvalidOperationException("Invalid credentials"), "Invalid credentials");
            }
        }

        /// <summary>
        /// Checks the on negotiation repeat.
        /// </summary>
        /// <param name="negotiationID">The negotiation ID.</param>
        /// <param name="negotiationName">Name of the negotiation.</param>
        /// <param name="userID">The user ID.</param>
        /// <returns>
        /// Number of Negotiation exist with the same name for that user but not deleted
        /// </returns>
        public int? CheckOnNegotiationRepeat(Guid negotiationID, string negotiationName, Guid userID)
        {
            if (ServiceAuthentication.IsValid())
            {
                return this.ObjectContext.CheckOnNegotiationRepeat(negotiationID, negotiationName, userID).SingleOrDefault();
            }
            else
            {
                //// throw fault  
                throw new FaultException<InvalidOperationException>(new InvalidOperationException("Invalid credentials"), "Invalid credentials");
            }
        }

        /// <summary>
        /// Gets the negotiation organizations [Used to Publish the Negotiation].
        /// </summary>
        /// <param name="negotiationIDList">The negotiation ID list.</param>
        /// <returns>a Collection of Negotiation Organization</returns>
        public IQueryable<NegotiationOrganization> GetNegotiationOrganizations(Guid[] negotiationIDList)
        {
            if (ServiceAuthentication.IsValid())
            {
                return this.ObjectContext
                      .NegotiationOrganizations
                      .Where(s => s.Deleted == false &&
                               negotiationIDList.Contains(s.NegotiationID));
            }
            else
            {
                //// throw fault  
                throw new FaultException<InvalidOperationException>(new InvalidOperationException("Invalid credentials"), "Invalid credentials");
            }
        }

        /// <summary>
        /// Gets the published negoiations.
        /// </summary>
        /// <param name="NegStatus">The neg status.</param>
        /// <param name="NegOwner">The neg owner.</param>
        /// <param name="UserID">The user ID.</param>
        /// <param name="archiveYear">The archive year.</param>
        /// <param name="archiveMonth">The archive month.</param>
        /// <returns></returns>
        public IQueryable<Negotiation> GetPublishedNegoiations(byte NegStatus, byte NegOwner, Guid UserID, int archiveYear, int archiveMonth)
        {
            if (ServiceAuthentication.IsValid())
            {
                return this.ObjectContext.GetPublishedNegotiations(NegOwner, NegStatus, archiveYear, archiveMonth, UserID).AsQueryable<Negotiation>();
            }
            else
            {
                //// throw fault  
                throw new FaultException<InvalidOperationException>(new InvalidOperationException("Invalid credentials"), "Invalid credentials");
            }
        }

        /// <summary>
        /// Gets the published negotiation archive.
        /// </summary>
        /// <param name="NegStatus">The neg status.</param>
        /// <param name="NegOwner">The neg owner.</param>
        /// <param name="UserID">The user ID.</param>
        /// <returns></returns>
        public IQueryable<NegotiationArchive> GetPublishedNegotiationArchive(byte NegStatus, byte NegOwner, Guid UserID)
        {
            if (ServiceAuthentication.IsValid())
            {
                return this.ObjectContext.GetPublishedNegotiationArchive(NegOwner, NegStatus, UserID).AsQueryable();
            }
            else
            {
                //// throw fault  
                throw new FaultException<InvalidOperationException>(new InvalidOperationException("Invalid credentials"), "Invalid credentials");
            }
        }

        /// <summary>
        /// Gets the closed negotiation archive.
        /// </summary>
        /// <param name="UserID">The user ID.</param>
        /// <returns></returns>
        public IQueryable<NegotiationArchive> GetClosedNegotiationArchive(Guid UserID)
        {
            if (ServiceAuthentication.IsValid())
            {
                return this.ObjectContext.GetClosedNegotiationArchive(UserID).AsQueryable();
            }
            else
            {
                //// throw fault  
                throw new FaultException<InvalidOperationException>(new InvalidOperationException("Invalid credentials"), "Invalid credentials");
            }
        }

        /// <summary>
        /// Gets the closed negoiations for archive.
        /// </summary>
        /// <param name="UserID">The user ID.</param>
        /// <param name="archiveYear">The archive year.</param>
        /// <param name="archiveMonth">The archive month.</param>
        /// <returns></returns>
        public IQueryable<Negotiation> GetClosedNegoiationsForArchive(Guid UserID, int archiveYear, int archiveMonth)
        {
            if (ServiceAuthentication.IsValid())
            {
                return this.ObjectContext.GetClosedNegotiationByArchive(archiveYear, archiveMonth, UserID).AsQueryable<Negotiation>();
            }
            else
            {
                //// throw fault  
                throw new FaultException<InvalidOperationException>(new InvalidOperationException("Invalid credentials"), "Invalid credentials");
            }
        }


        #endregion

        #region → Organization   (Organization Members )                   .

        /// <summary>
        /// Gets the organizations.
        /// </summary>
        /// <returns></returns>
        public IQueryable<Organization> GetOrganizations()
        {
            return this.ObjectContext
                       .Organizations
                       .Where(s => s.Deleted == false)
                       .OrderBy(o => o.OrganizationName);
        }

        /// <summary>
        /// Gets the organization by ID.
        /// </summary>
        /// <param name="organizationID">The organization ID.</param>
        /// <returns></returns>
        [Query(HasSideEffects = true)]
        public IQueryable<Organization> GetOrganizationByID(Guid organizationID)
        {
            if (ServiceAuthentication.IsValid())
            {
                return this.ObjectContext.Organizations.Where(ss => ss.Deleted == false
                    && ss.OrganizationID == organizationID);
            }
            else
            {
                //// throw fault  
                throw new FaultException<InvalidOperationException>(new InvalidOperationException("Invalid credentials"), "Invalid credentials");
            }
        }

        /// <summary>
        /// Gets the organization by its owner ID.
        /// </summary>
        /// <param name="OwnerID">The owner ID.</param>
        /// <returns>the Organization owned by the User</returns>
        [Query(HasSideEffects = true)]
        public IQueryable<Organization> GetOrganizationByItsOwnerID(Guid OwnerID)
        {
            if (ServiceAuthentication.IsValid())
            {

                return this.ObjectContext.Organizations
                                        .Where(ss => ss.UserOrganizations
                                                       .Where(dd => dd.UserID == OwnerID &&
                                                                    dd.Deleted == false &&
                                                                    (dd.MemberStatus == eNegConstant.OrganizationMemberStatus.Owner))
                                                       .Count() > 0 &&
                                                       ss.Deleted == false)
                                        .AsQueryable<Organization>();

            }
            else
            {
                //// throw fault  
                throw new FaultException<InvalidOperationException>(new InvalidOperationException("Invalid credentials"), "Invalid credentials");
            }
        }

        /// <summary>
        /// Gets the user organizations for user.
        /// </summary>
        /// <param name="userID">The user ID.</param>
        /// <returns></returns>
        [Query(HasSideEffects = true)]
        public IQueryable<UserOrganization> GetUserOrganizationsForUser(Guid userID)
        {
            if (ServiceAuthentication.IsValid())
            {
                return this.ObjectContext.UserOrganizations.Where(ss => ss.Deleted == false && ss.UserID == userID);
            }
            else
            {
                //// throw fault  
                throw new FaultException<InvalidOperationException>(new InvalidOperationException("Invalid credentials"), "Invalid credentials");
            }
        }

        /// <summary>
        /// Gets the user organizations for owners users.
        /// </summary>
        /// <param name="usersIDs">The users I ds.</param>
        /// <returns>Return Owners Only</returns>
        public IQueryable<UserOrganization> GetUserOrganizationsForOwnersUsers(List<Guid> usersIDs)
        {
            if (ServiceAuthentication.IsValid())
            {
                return this.ObjectContext
                           .UserOrganizations
                           .Where(ss => ss.Deleted == false &&
                                        usersIDs.Contains(ss.UserID) &&
                                        ss.MemberStatus == eNegConstant.OrganizationMemberStatus.Owner);
            }
            else
            {
                //// throw fault  
                throw new FaultException<InvalidOperationException>(new InvalidOperationException("Invalid credentials"), "Invalid credentials");
            }
        }

        /// <summary>
        /// Gets the user organizations.
        /// Used in case of need to know which organization that user is member in.
        /// </summary>
        /// <param name="userID">The user ID.</param>
        /// <returns></returns>
        [Query(HasSideEffects = true)]
        public IQueryable<Organization> GetOrganizationsForUser(Guid userID)
        {
            if (ServiceAuthentication.IsValid())
            {

                return this.ObjectContext.Organizations
                                         .Where(ss => ss.UserOrganizations
                                                        .Where(dd => dd.UserID == userID &&
                                                                     dd.Deleted == false &&
                                                                     (dd.MemberStatus != eNegConstant.OrganizationMemberStatus.PendingMember))
                                                        .Count() > 0 &&
                                                        ss.Deleted == false)
                                         .AsQueryable<Organization>();
            }
            else
            {
                //// throw fault  
                throw new FaultException<InvalidOperationException>(new InvalidOperationException("Invalid credentials"), "Invalid credentials");

            }
        }

        /// <summary>
        /// Gets the organizations owners'.
        /// Used in case of register user to send Notification Message for owners
        /// </summary>
        /// <param name="organizationIDs">The organization Ids.</param>
        /// <returns>a list of users</returns>
        [Query(HasSideEffects = true)]
        public IQueryable<User> GetOrganizationsOwners(Guid[] organizationIDs)
        {
            if (ServiceAuthentication.IsValid())
            {

                return this.ObjectContext.User
                                         .Where(ss => ss.UserOrganizations
                                                        .Where(dd => organizationIDs.Contains(dd.OrganizationID) &&
                                                                     dd.Deleted == false &&
                                                                     dd.MemberStatus == eNegConstant.OrganizationMemberStatus.Owner)
                                                        .Count() > 0 &&
                                                        ss.Disabled == false)
                                         .AsQueryable<User>();
            }
            else
            {
                //// throw fault  
                throw new FaultException<InvalidOperationException>(new InvalidOperationException("Invalid credentials"), "Invalid credentials");

            }
        }

        /// <summary>
        /// Gets the organization members.
        /// </summary>
        /// <param name="organizationID">The organization ID.</param>
        /// <returns>all users memebers</returns>
        [Query(HasSideEffects = true)]
        public IQueryable<User> GetOrganizationMembers(Guid organizationID)
        {
            if (ServiceAuthentication.IsValid())
            {
                return this.ObjectContext.User
                                         .Where(ss => ss.UserOrganizations
                                                        .Where(dd => dd.OrganizationID == organizationID &&
                                                                     dd.Deleted == false)
                                                        .Count() > 0 &&
                                                        ss.Disabled == false)
                                         .AsQueryable<User>();
            }
            else
            {
                //// throw fault  
                throw new FaultException<InvalidOperationException>(new InvalidOperationException("Invalid credentials"), "Invalid credentials");

            }
        }

        /// <summary>
        /// Gets the organization members status.
        /// </summary>
        /// <param name="organizationID">The organization ID.</param>
        /// <returns>all users memebers</returns>
        [Query(HasSideEffects = true)]
        public IQueryable<UserOrganization> GetOrganizationMembersStatus(Guid organizationID)
        {
            if (ServiceAuthentication.IsValid())
            {
                var UserOrgs = this.ObjectContext.UserOrganizations
                                         .Where(dd => dd.OrganizationID == organizationID &&
                                                      dd.Deleted == false).AsQueryable<UserOrganization>();

                List<UserOrganization> result = UserOrgs.ToList();

                foreach (var item in UserOrgs)
                {
                    var NewUserOrg = this.ObjectContext.UserOrganizations
                                         .Where(s => s.UserID == item.UserID &&
                                                     s.MemberStatus == 3 &&
                                                     s.Deleted == false)
                                         .AsQueryable<UserOrganization>();

                    foreach (var NewItem in NewUserOrg)
                    {
                        if (UserOrgs.Where(s => s.OrganizationID == NewItem.OrganizationID).FirstOrDefault() == null)
                        {
                            result.Add(NewItem);
                        }
                    }
                }
                return result.AsQueryable<UserOrganization>();
            }
            else
            {
                //// throw fault  
                throw new FaultException<InvalidOperationException>(new InvalidOperationException("Invalid credentials"), "Invalid credentials");
            }
        }

        /// <summary>
        /// Determines whether this instance [can user leave organization] the specified user ID.
        /// </summary>
        /// <param name="userID">The user ID.</param>
        /// <param name="organizationID">The organization ID.</param>
        /// <returns>Return Single row with result indicating if one can leave or not</returns>
        public IQueryable<UserLeaveOrganizationResult> CanUserLeaveOrganization(Guid userID, Guid organizationID)
        {
            if (ServiceAuthentication.IsValid())
            {
                return this.ObjectContext.CanUserLeaveOrganization(userID, organizationID).AsQueryable<UserLeaveOrganizationResult>();
            }
            else
            {
                //// throw fault  
                throw new FaultException<InvalidOperationException>(new InvalidOperationException("Invalid credentials"), "Invalid credentials");
            }
        }

        #endregion

        #region → Related To Cultures                                      .

        /// <summary>
        /// Updates the user culture.
        /// </summary>
        /// <param name="userID">The user ID.</param>
        /// <param name="cultureID">The culture ID.</param>
        /// <returns>bool true if success</returns>
        public bool UpdateUserCulture(Guid userID, int cultureID)
        {
            if (ServiceAuthentication.IsValid())
            {
                User objUser = this.ObjectContext.User.Where(ss => ss.UserID == userID).FirstOrDefault();

                if (objUser != null)
                {
                    objUser.CultureID = cultureID;
                }

                return this.ObjectContext.SaveChanges() > 0;
            }
            else
            {
                //// throw fault  
                throw new FaultException<InvalidOperationException>(new InvalidOperationException("Invalid credentials"), "Invalid credentials");
            }
        }

        /// <summary>
        /// Gets the cultures.
        /// </summary>
        /// <returns>List of Cultures</returns>
        [Query(HasSideEffects = true)]
        public IQueryable<Culture> GetCultures()
        {
            if (ServiceAuthentication.IsValid())
            {
                return this.ObjectContext.Cultures.OrderBy(o => o.CultureName);
            }
            else
            {
                //// throw fault  
                throw new FaultException<InvalidOperationException>(new InvalidOperationException("Invalid credentials"), "Invalid credentials");
            }
        }

        #endregion

        #endregion

        #region → Private        .

        /// <summary>
        /// Removes the brackets.
        /// </summary>
        /// <param name="word">The word.</param>
        /// <returns></returns>
        private string RemoveBrackets(string word)
        {
            //karli <Karli@yahoo.com>
            // <sunder@yahoo.com>

            string TempParticipant = word.Substring(0, word.IndexOf('<'));
            if (string.IsNullOrWhiteSpace(TempParticipant))
            {
                TempParticipant = word.Substring(word.IndexOf('<') + 1, word.IndexOf('>') - word.IndexOf('<') - 1);
            }
            else
            {
                TempParticipant = word;
            }

            return TempParticipant;

        }


        /// <summary>
        /// Prints the text centered.
        /// </summary>
        /// <param name="text">The text.</param>
        private void PrintTextCentered(string text)
        {

            Paragraph TitleParagraph = new Paragraph();
            TitleParagraph.Add(NewLine);
            TitleParagraph.Add(NewLine);
            TitleParagraph.Add(NewLine);
            TitleParagraph.Add(NewLine);
            TitleParagraph.Add(NewLine);
            TitleParagraph.Add(NewLine);
            TitleParagraph.Add(NewLine);
            TitleParagraph.Add(NewLine);
            TitleParagraph.Add(NewLine);
            TitleParagraph.Add(NewLine);

            PdfPTable table = new PdfPTable(1);
            Rectangle page = exportedDocument.PageSize;
            table.TotalWidth = page.Width;
            Phrase phrase = new Phrase(text, CenteredTitle);

            PdfPCell c = new PdfPCell(phrase);
            c.Border = Rectangle.NO_BORDER;
            c.VerticalAlignment = Element.ALIGN_CENTER;
            c.HorizontalAlignment = Element.ALIGN_CENTER;
            table.AddCell(c);
            TitleParagraph.Add(table);
            exportedDocument.Add(TitleParagraph);

            // Get the top layer and write some text
            //_pcb = writer.DirectContent;
            //_pcb.BeginText();

            //_pcb.SetFontAndSize(CenteredTitle, 20);
            //_pcb.ShowTextAligned(PdfContentByte.ALIGN_CENTER, text, 200, 320, 0);

            //_pcb.EndText();

            exportedDocument.NewPage();
        }

        /// <summary>
        /// Sets the page cover.
        /// </summary>
        private void SetPageCover()
        {
            readerCover = new PdfReader(path + "Images/bicycle.pdf");
            background = writer.GetImportedPage(readerCover, 1);
            _pcb = writer.DirectContentUnder;
            _pcb.AddTemplate(background, 0, 0);
        }

        /// <summary>
        /// Sets the page header.
        /// </summary>
        private void SetPageHeader()
        {

            // the image we're using for the page header      
            imageHeader = iTextSharp.text.Image.GetInstance(path + "Images/Logo.png");

            // instantiate the custom PdfPageEventHelper
            eNegpdfPageEventHandler eEvent = new eNegpdfPageEventHandler()
            {
                ImageHeader = imageHeader
            };

            // and add it to the PdfWriter
            writer.PageEvent = eEvent;
        }

        /// <summary>
        /// Prepares the PDF document.
        /// </summary>
        private void PreparePDFDocument()
        {
            r = new Rectangle(400, 600);
            exportedDocument = new Document(r);
            memoryStream = new MemoryStream();
            writer = PdfWriter.GetInstance(exportedDocument, memoryStream);

            exportedDocument.AddAuthor("eNeg System");
            exportedDocument.AddCreationDate();
            exportedDocument.AddCreator("eNeg System");
            
        }

        /// <summary>
        /// Prints the messages.
        /// </summary>
        /// <param name="messages">The messages.</param>
        private void PrintMessages(IEnumerable<Message> messages)
        {
            foreach (var msg in messages)
            {
                Chunk Date = new Chunk("Date: ", SubTitle);
                Chunk DateValue = new Chunk(msg.MessageDate.Value.ToString(), SubTitleValue);

                Phrase DatePhrase = new Phrase();
                DatePhrase.Add(Date);
                DatePhrase.Add(DateValue);
                DatePhrase.Add(NewLine);

                Chunk Subject = new Chunk("Subject: ", SubTitle);
                Chunk SubjectValue = new Chunk(msg.MessageSubject, SubTitleValue);

                Phrase SubjectPhrase = new Phrase();
                SubjectPhrase.Add(Subject);
                SubjectPhrase.Add(SubjectValue);
                SubjectPhrase.Add(NewLine);

                Chunk From = new Chunk("From: ", SubTitle);
                Chunk FromValue = new Chunk((msg.MessageSender), SubTitleValue);

                Phrase FromPhrase = new Phrase();
                FromPhrase.Add(From);
                FromPhrase.Add(FromValue);
                FromPhrase.Add(NewLine);

                Chunk To = new Chunk("To:", SubTitle);
                Chunk ToValue = new Chunk((msg.MessageReceiver), SubTitleValue);

                Phrase ToPhrase = new Phrase();
                ToPhrase.Add(To);
                ToPhrase.Add(ToValue);
                ToPhrase.Add(NewLine);

                Chunk Channel = new Chunk("Channel: ", SubTitle);
                Chunk ChannelValue = new Chunk(msg.Channel.ChannelName, SubTitleValue);

                Phrase ChannelPhrase = new Phrase();
                ChannelPhrase.Add(Channel);
                ChannelPhrase.Add(ChannelValue);
                ChannelPhrase.Add(NewLine);

                Chunk Content = new Chunk(msg.MessageContent, Normal);

                Phrase ContentPhrase = new Phrase();
                ContentPhrase.Add(Content);
                ContentPhrase.Add(NewLine);

                Paragraph messageParagraph = new Paragraph();
                messageParagraph.Add(DatePhrase);
                messageParagraph.Add(SubjectPhrase);
                messageParagraph.Add(FromPhrase);
                messageParagraph.Add(ToPhrase);
                messageParagraph.Add(ChannelPhrase);
                messageParagraph.Add(ContentPhrase);

                exportedDocument.Add(messageParagraph);

                exportedDocument.NewPage();


            }

        }
        #endregion

        #endregion

    }
}


