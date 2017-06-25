#region → Usings   .
using System.Data;
using System.Linq;
using System.Collections.Generic;
using System.ServiceModel.DomainServices.EntityFramework;
using System.ServiceModel.DomainServices.Hosting;
using System.ServiceModel.DomainServices.Server;
using System.ServiceModel;
using System;
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
    [EnableClientAccess()]
    //[AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public partial class eNegService : LinqToEntitiesDomainService<eNegEntities>
    {

        
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'UserOperations' query.
        [Query(IsDefault = true)]
        public IQueryable<UserOperation> GetUserOperations()
        {
            if (ServiceAuthentication.IsValid())
            {
                return this.ObjectContext.UserOperations;
            }
            else
            {
                //// throw fault  
                throw new FaultException<InvalidOperationException>(new InvalidOperationException("Invalid credentials"), "Invalid credentials");
            }
        }

        public void InsertUserOperation(UserOperation userOperation)
        {
            if ((userOperation.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(userOperation, EntityState.Added);
            }
            else
            {
                this.ObjectContext.UserOperations.AddObject(userOperation);
            }
        }

        public void UpdateUserOperation(UserOperation currentUserOperation)
        {
            this.ObjectContext.UserOperations.AttachAsModified(currentUserOperation, this.ChangeSet.GetOriginal(currentUserOperation));
        }

        public void DeleteUserOperation(UserOperation userOperation)
        {
            if ((userOperation.EntityState == EntityState.Detached))
            {
                this.ObjectContext.UserOperations.Attach(userOperation);
            }
            this.ObjectContext.UserOperations.DeleteObject(userOperation);
        }


        
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'AccountType' query.
        [Query(IsDefault = true)]
        public IQueryable<AccountType> GetAccountType()
        {
            if (ServiceAuthentication.IsValid())
            {
                return this.ObjectContext.AccountType;
            }
            else
            {
                //// throw fault  
                throw new FaultException<InvalidOperationException>(new InvalidOperationException("Invalid credentials"), "Invalid credentials");
            }
        }

        public void InsertAccountType(AccountType accountType)
        {
            if ((accountType.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(accountType, EntityState.Added);
            }
            else
            {
                this.ObjectContext.AccountType.AddObject(accountType);
            }
        }

        public void UpdateAccountType(AccountType currentAccountType)
        {
            this.ObjectContext.AccountType.AttachAsModified(currentAccountType, this.ChangeSet.GetOriginal(currentAccountType));
        }

        public void DeleteAccountType(AccountType accountType)
        {
            if ((accountType.EntityState == EntityState.Detached))
            {
                this.ObjectContext.AccountType.Attach(accountType);
            }
            this.ObjectContext.AccountType.DeleteObject(accountType);
        }

        
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'ActionTypes' query.
        [Query(IsDefault = true)]
        public IQueryable<ActionType> GetActionTypes()
        {
            if (ServiceAuthentication.IsValid())
            {
                return this.ObjectContext.ActionTypes;
            }
            else
            {
                //// throw fault  
                throw new FaultException<InvalidOperationException>(new InvalidOperationException("Invalid credentials"), "Invalid credentials");
            }
        }

        public void InsertActionType(ActionType actionType)
        {
            if ((actionType.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(actionType, EntityState.Added);
            }
            else
            {
                this.ObjectContext.ActionTypes.AddObject(actionType);
            }
        }

        public void UpdateActionType(ActionType currentActionType)
        {
            this.ObjectContext.ActionTypes.AttachAsModified(currentActionType, this.ChangeSet.GetOriginal(currentActionType));
        }

        public void DeleteActionType(ActionType actionType)
        {
            if ((actionType.EntityState == EntityState.Detached))
            {
                this.ObjectContext.ActionTypes.Attach(actionType);
            }
            this.ObjectContext.ActionTypes.DeleteObject(actionType);
        }

        
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'Applications' query.
        [Query(IsDefault = true)]
        public IQueryable<Application> GetApplications()
        {
            if (ServiceAuthentication.IsValid())
            {
                return this.ObjectContext.Applications;
            }
            else
            {
                //// throw fault  
                throw new FaultException<InvalidOperationException>(new InvalidOperationException("Invalid credentials"), "Invalid credentials");
            }
        }

        public void InsertApplication(Application application)
        {
            if ((application.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(application, EntityState.Added);
            }
            else
            {
                this.ObjectContext.Applications.AddObject(application);
            }
        }

        public void UpdateApplication(Application currentApplication)
        {
            this.ObjectContext.Applications.AttachAsModified(currentApplication, this.ChangeSet.GetOriginal(currentApplication));
        }

        public void DeleteApplication(Application application)
        {
            if ((application.EntityState == EntityState.Detached))
            {
                this.ObjectContext.Applications.Attach(application);
            }
            this.ObjectContext.Applications.DeleteObject(application);
        }

        
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'Attachements' query.
        [Query(IsDefault = true)]
        public IQueryable<Attachement> GetAttachements()
        {
            if (ServiceAuthentication.IsValid())
            {
                return this.ObjectContext.Attachements;
            }
            else
            {
                //// throw fault  
                throw new FaultException<InvalidOperationException>(new InvalidOperationException("Invalid credentials"), "Invalid credentials");
            }
        }

        public void InsertAttachement(Attachement attachement)
        {
            if ((attachement.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(attachement, EntityState.Added);
            }
            else
            {
                this.ObjectContext.Attachements.AddObject(attachement);
            }
        }

        public void UpdateAttachement(Attachement currentAttachement)
        {
            this.ObjectContext.Attachements.AttachAsModified(currentAttachement, this.ChangeSet.GetOriginal(currentAttachement));
        }

        public void DeleteAttachement(Attachement attachement)
        {
            if ((attachement.EntityState == EntityState.Detached))
            {
                this.ObjectContext.Attachements.Attach(attachement);
            }
            this.ObjectContext.Attachements.DeleteObject(attachement);
        }

        
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'Categories' query.
        [Query(IsDefault = true)]
        public IQueryable<Category> GetCategories()
        {
            if (ServiceAuthentication.IsValid())
            {
                return this.ObjectContext.Categories;
            }
            else
            {
                //// throw fault  
                throw new FaultException<InvalidOperationException>(new InvalidOperationException("Invalid credentials"), "Invalid credentials");
            }
        }

        public void InsertCategory(Category category)
        {
            if ((category.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(category, EntityState.Added);
            }
            else
            {
                this.ObjectContext.Categories.AddObject(category);
            }
        }

        public void UpdateCategory(Category currentCategory)
        {
            this.ObjectContext.Categories.AttachAsModified(currentCategory, this.ChangeSet.GetOriginal(currentCategory));
        }

        public void DeleteCategory(Category category)
        {
            if ((category.EntityState == EntityState.Detached))
            {
                this.ObjectContext.Categories.Attach(category);
            }
            this.ObjectContext.Categories.DeleteObject(category);
        }

        
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'CategoryLogs' query.
        [Query(IsDefault = true)]
        public IQueryable<CategoryLog> GetCategoryLogs()
        {
            if (ServiceAuthentication.IsValid())
            {
                return this.ObjectContext.CategoryLogs;
            }
            else
            {
                //// throw fault  
                throw new FaultException<InvalidOperationException>(new InvalidOperationException("Invalid credentials"), "Invalid credentials");
            }
        }

        public void InsertCategoryLog(CategoryLog categoryLog)
        {
            if ((categoryLog.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(categoryLog, EntityState.Added);
            }
            else
            {
                this.ObjectContext.CategoryLogs.AddObject(categoryLog);
            }
        }

        public void UpdateCategoryLog(CategoryLog currentCategoryLog)
        {
            this.ObjectContext.CategoryLogs.AttachAsModified(currentCategoryLog, this.ChangeSet.GetOriginal(currentCategoryLog));
        }

        public void DeleteCategoryLog(CategoryLog categoryLog)
        {
            if ((categoryLog.EntityState == EntityState.Detached))
            {
                this.ObjectContext.CategoryLogs.Attach(categoryLog);
            }
            this.ObjectContext.CategoryLogs.DeleteObject(categoryLog);
        }

        
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'Channels' query.
        [Query(IsDefault = true)]
        public IQueryable<Channel> GetChannels()
        {
            if (ServiceAuthentication.IsValid())
            {
                return this.ObjectContext.Channels;
            }
            else
            {
                //// throw fault  
                throw new FaultException<InvalidOperationException>(new InvalidOperationException("Invalid credentials"), "Invalid credentials");
            }
        }

        public void InsertChannel(Channel channel)
        {
            if ((channel.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(channel, EntityState.Added);
            }
            else
            {
                this.ObjectContext.Channels.AddObject(channel);
            }
        }

        public void UpdateChannel(Channel currentChannel)
        {
            this.ObjectContext.Channels.AttachAsModified(currentChannel, this.ChangeSet.GetOriginal(currentChannel));
        }

        public void DeleteChannel(Channel channel)
        {
            if ((channel.EntityState == EntityState.Detached))
            {
                this.ObjectContext.Channels.Attach(channel);
            }
            this.ObjectContext.Channels.DeleteObject(channel);
        }

        
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'ChannelTypes' query.
        [Query(IsDefault = true)]
        public IQueryable<ChannelType> GetChannelTypes()
        {
            if (ServiceAuthentication.IsValid())
            {
                return this.ObjectContext.ChannelTypes;
            }
            else
            {
                //// throw fault  
                throw new FaultException<InvalidOperationException>(new InvalidOperationException("Invalid credentials"), "Invalid credentials");
            }
        }

        public void InsertChannelType(ChannelType channelType)
        {
            if ((channelType.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(channelType, EntityState.Added);
            }
            else
            {
                this.ObjectContext.ChannelTypes.AddObject(channelType);
            }
        }

        public void UpdateChannelType(ChannelType currentChannelType)
        {
            this.ObjectContext.ChannelTypes.AttachAsModified(currentChannelType, this.ChangeSet.GetOriginal(currentChannelType));
        }

        public void DeleteChannelType(ChannelType channelType)
        {
            if ((channelType.EntityState == EntityState.Detached))
            {
                this.ObjectContext.ChannelTypes.Attach(channelType);
            }
            this.ObjectContext.ChannelTypes.DeleteObject(channelType);
        }

        
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'Conversations' query.
        public void InsertConversation(Conversation conversation)
        {
            if ((conversation.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(conversation, EntityState.Added);
            }
            else
            {
                this.ObjectContext.Conversations.AddObject(conversation);
            }
        }

        public void DeleteConversation(Conversation conversation)
        {
            if ((conversation.EntityState == EntityState.Detached))
            {
                this.ObjectContext.Conversations.Attach(conversation);
            }
            this.ObjectContext.Conversations.DeleteObject(conversation);
        }

        
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'Country' query.
        [Query(IsDefault = true)]
        public IQueryable<Country> GetCountry()
        {
            if (ServiceAuthentication.IsValid())
            {
                return this.ObjectContext.Country;
            }
            else
            {
                //// throw fault  
                throw new FaultException<InvalidOperationException>(new InvalidOperationException("Invalid credentials"), "Invalid credentials");
            }
        }

        public void InsertCountry(Country country)
        {
            if ((country.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(country, EntityState.Added);
            }
            else
            {
                this.ObjectContext.Country.AddObject(country);
            }
        }

        public void UpdateCountry(Country currentCountry)
        {
            this.ObjectContext.Country.AttachAsModified(currentCountry, this.ChangeSet.GetOriginal(currentCountry));
        }

        public void DeleteCountry(Country country)
        {
            if ((country.EntityState == EntityState.Detached))
            {
                this.ObjectContext.Country.Attach(country);
            }
            this.ObjectContext.Country.DeleteObject(country);
        }

        
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'Histories' query.
        [Query(IsDefault = true)]
        public IQueryable<History> GetHistories()
        {
            if (ServiceAuthentication.IsValid())
            {
                return this.ObjectContext.Histories;
            }
            else
            {
                //// throw fault  
                throw new FaultException<InvalidOperationException>(new InvalidOperationException("Invalid credentials"), "Invalid credentials");
            }
        }

        public void InsertHistory(History history)
        {
            if ((history.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(history, EntityState.Added);
            }
            else
            {
                this.ObjectContext.Histories.AddObject(history);
            }
        }

        public void UpdateHistory(History currentHistory)
        {
            this.ObjectContext.Histories.AttachAsModified(currentHistory, this.ChangeSet.GetOriginal(currentHistory));
        }

        public void DeleteHistory(History history)
        {
            if ((history.EntityState == EntityState.Detached))
            {
                this.ObjectContext.Histories.Attach(history);
            }
            this.ObjectContext.Histories.DeleteObject(history);
        }

        
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'Logs' query.
        [Query(IsDefault = true)]
        public IQueryable<Log> GetLogs()
        {
            if (ServiceAuthentication.IsValid())
            {
                return this.ObjectContext.Logs;
            }
            else
            {
                //// throw fault  
                throw new FaultException<InvalidOperationException>(new InvalidOperationException("Invalid credentials"), "Invalid credentials");
            }
        }

        public void InsertLog(Log log)
        {
            if ((log.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(log, EntityState.Added);
            }
            else
            {
                this.ObjectContext.Logs.AddObject(log);
            }
        }

        public void UpdateLog(Log currentLog)
        {
            this.ObjectContext.Logs.AttachAsModified(currentLog, this.ChangeSet.GetOriginal(currentLog));
        }

        public void DeleteLog(Log log)
        {
            if ((log.EntityState == EntityState.Detached))
            {
                this.ObjectContext.Logs.Attach(log);
            }
            this.ObjectContext.Logs.DeleteObject(log);
        }

        
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'Messages' query.
        [Query(IsDefault = true)]
        public IQueryable<Message> GetMessages()
        {
            if (ServiceAuthentication.IsValid())
            {
                return this.ObjectContext.Messages;
            }
            else
            {
                //// throw fault  
                throw new FaultException<InvalidOperationException>(new InvalidOperationException("Invalid credentials"), "Invalid credentials");
            }
        }

        public void InsertMessage(Message message)
        {
            if ((message.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(message, EntityState.Added);
            }
            else
            {
                this.ObjectContext.Messages.AddObject(message);
            }
        }

        public void UpdateMessage(Message currentMessage)
        {
            this.ObjectContext.Messages.AttachAsModified(currentMessage, this.ChangeSet.GetOriginal(currentMessage));
        }

        public void DeleteMessage(Message message)
        {
            if ((message.EntityState == EntityState.Detached))
            {
                this.ObjectContext.Messages.Attach(message);
            }
            this.ObjectContext.Messages.DeleteObject(message);
        }

        
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'Negotiations' query.
        public void InsertNegotiation(Negotiation negotiation)
        {
            if ((negotiation.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(negotiation, EntityState.Added);
            }
            else
            {
                this.ObjectContext.Negotiations.AddObject(negotiation);
            }
        }

        public void UpdateNegotiation(Negotiation currentNegotiation)
        {
            this.ObjectContext.Negotiations.AttachAsModified(currentNegotiation, this.ChangeSet.GetOriginal(currentNegotiation));
        }

        public void DeleteNegotiation(Negotiation negotiation)
        {
            if ((negotiation.EntityState == EntityState.Detached))
            {
                this.ObjectContext.Negotiations.Attach(negotiation);
            }
            this.ObjectContext.Negotiations.DeleteObject(negotiation);
        }

        
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'NegotiationApplicationStatus' query.
        [Query(IsDefault = true)]
        public IQueryable<NegotiationApplicationStatu> GetNegotiationApplicationStatus()
        {
            if (ServiceAuthentication.IsValid())
            {
                return this.ObjectContext.NegotiationApplicationStatus;
            }
            else
            {
                //// throw fault  
                throw new FaultException<InvalidOperationException>(new InvalidOperationException("Invalid credentials"), "Invalid credentials");
            }

        }

        public void InsertNegotiationApplicationStatu(NegotiationApplicationStatu negotiationApplicationStatu)
        {

            if (negotiationApplicationStatu.Negotiation != null)
            {
                if ((negotiationApplicationStatu.EntityState != EntityState.Detached))
                {
                    this.ObjectContext.ObjectStateManager.ChangeObjectState(negotiationApplicationStatu, EntityState.Added);
                }
                else
                {
                    this.ObjectContext.NegotiationApplicationStatus.AddObject(negotiationApplicationStatu);
                }
            }
        }

        public void UpdateNegotiationApplicationStatu(NegotiationApplicationStatu currentNegotiationApplicationStatu)
        {
            this.ObjectContext.NegotiationApplicationStatus.AttachAsModified(currentNegotiationApplicationStatu, this.ChangeSet.GetOriginal(currentNegotiationApplicationStatu));
        }

        public void DeleteNegotiationApplicationStatu(NegotiationApplicationStatu negotiationApplicationStatu)
        {
            if ((negotiationApplicationStatu.EntityState == EntityState.Detached))
            {
                this.ObjectContext.NegotiationApplicationStatus.Attach(negotiationApplicationStatu);
            }
            this.ObjectContext.NegotiationApplicationStatus.DeleteObject(negotiationApplicationStatu);
        }

        
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'NegotiationStatus' query.
        [Query(IsDefault = true)]
        public IQueryable<NegotiationStatu> GetNegotiationStatus()
        {
            if (ServiceAuthentication.IsValid())
            {
                return this.ObjectContext.NegotiationStatus;
            }
            else
            {
                //// throw fault  
                throw new FaultException<InvalidOperationException>(new InvalidOperationException("Invalid credentials"), "Invalid credentials");
            }
        }

        public void InsertNegotiationStatu(NegotiationStatu negotiationStatu)
        {
            if ((negotiationStatu.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(negotiationStatu, EntityState.Added);
            }
            else
            {
                this.ObjectContext.NegotiationStatus.AddObject(negotiationStatu);
            }
        }

        public void UpdateNegotiationStatu(NegotiationStatu currentNegotiationStatu)
        {
            this.ObjectContext.NegotiationStatus.AttachAsModified(currentNegotiationStatu, this.ChangeSet.GetOriginal(currentNegotiationStatu));
        }

        public void DeleteNegotiationStatu(NegotiationStatu negotiationStatu)
        {
            if ((negotiationStatu.EntityState == EntityState.Detached))
            {
                this.ObjectContext.NegotiationStatus.Attach(negotiationStatu);
            }
            this.ObjectContext.NegotiationStatus.DeleteObject(negotiationStatu);
        }

        
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'PreferedLanguage' query.
        [Query(IsDefault = true)]
        public IQueryable<PreferedLanguage> GetPreferedLanguage()
        {
            if (ServiceAuthentication.IsValid())
            {
                return this.ObjectContext.PreferedLanguage;
            }
            else
            {
                //// throw fault  
                throw new FaultException<InvalidOperationException>(new InvalidOperationException("Invalid credentials"), "Invalid credentials");
            }
        }

        public void InsertPreferedLanguage(PreferedLanguage preferedLanguage)
        {
            if ((preferedLanguage.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(preferedLanguage, EntityState.Added);
            }
            else
            {
                this.ObjectContext.PreferedLanguage.AddObject(preferedLanguage);
            }
        }

        public void UpdatePreferedLanguage(PreferedLanguage currentPreferedLanguage)
        {
            this.ObjectContext.PreferedLanguage.AttachAsModified(currentPreferedLanguage, this.ChangeSet.GetOriginal(currentPreferedLanguage));
        }

        public void DeletePreferedLanguage(PreferedLanguage preferedLanguage)
        {
            if ((preferedLanguage.EntityState == EntityState.Detached))
            {
                this.ObjectContext.PreferedLanguage.Attach(preferedLanguage);
            }
            this.ObjectContext.PreferedLanguage.DeleteObject(preferedLanguage);
        }

        
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'Profile' query.
        [Query(IsDefault = true)]
        public IQueryable<Profile> GetProfile()
        {
            if (ServiceAuthentication.IsValid())
            {
                return this.ObjectContext.Profile;
            }
            else
            {
                //// throw fault  
                throw new FaultException<InvalidOperationException>(new InvalidOperationException("Invalid credentials"), "Invalid credentials");
            }
        }

        public void InsertProfile(Profile profile)
        {
            if ((profile.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(profile, EntityState.Added);
            }
            else
            {
                this.ObjectContext.Profile.AddObject(profile);
            }
        }

        public void UpdateProfile(Profile currentProfile)
        {
            this.ObjectContext.Profile.AttachAsModified(currentProfile, this.ChangeSet.GetOriginal(currentProfile));
        }

        public void DeleteProfile(Profile profile)
        {
            if ((profile.EntityState == EntityState.Detached))
            {
                this.ObjectContext.Profile.Attach(profile);
            }
            this.ObjectContext.Profile.DeleteObject(profile);
        }

        
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'Right' query.
        [Query(IsDefault = true)]
        public IQueryable<Right> GetRight()
        {
            if (ServiceAuthentication.IsValid())
            {
                return this.ObjectContext.Right;
            }
            else
            {
                //// throw fault  
                throw new FaultException<InvalidOperationException>(new InvalidOperationException("Invalid credentials"), "Invalid credentials");
            }
        }

        public void InsertRight(Right right)
        {
            if ((right.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(right, EntityState.Added);
            }
            else
            {
                this.ObjectContext.Right.AddObject(right);
            }
        }

        public void UpdateRight(Right currentRight)
        {
            this.ObjectContext.Right.AttachAsModified(currentRight, this.ChangeSet.GetOriginal(currentRight));
        }

        public void DeleteRight(Right right)
        {
            if ((right.EntityState == EntityState.Detached))
            {
                this.ObjectContext.Right.Attach(right);
            }
            this.ObjectContext.Right.DeleteObject(right);
        }

        
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'Role' query.
        [Query(IsDefault = true)]
        public IQueryable<Role> GetRole()
        {
            if (ServiceAuthentication.IsValid())
            {
                return this.ObjectContext.Role;
            }
            else
            {
                //// throw fault  
                throw new FaultException<InvalidOperationException>(new InvalidOperationException("Invalid credentials"), "Invalid credentials");
            }
        }

        public void InsertRole(Role role)
        {
            if ((role.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(role, EntityState.Added);
            }
            else
            {
                this.ObjectContext.Role.AddObject(role);
            }
        }

        public void UpdateRole(Role currentRole)
        {
            this.ObjectContext.Role.AttachAsModified(currentRole, this.ChangeSet.GetOriginal(currentRole));
        }

        public void DeleteRole(Role role)
        {
            if ((role.EntityState == EntityState.Detached))
            {
                this.ObjectContext.Role.Attach(role);
            }
            this.ObjectContext.Role.DeleteObject(role);
        }

        
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'RoleRight' query.
        [Query(IsDefault = true)]
        public IQueryable<RoleRight> GetRoleRight()
        {
            if (ServiceAuthentication.IsValid())
            {
                return this.ObjectContext.RoleRight;
            }
            else
            {
                //// throw fault  
                throw new FaultException<InvalidOperationException>(new InvalidOperationException("Invalid credentials"), "Invalid credentials");
            }
        }

        public void InsertRoleRight(RoleRight roleRight)
        {
            if ((roleRight.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(roleRight, EntityState.Added);
            }
            else
            {
                this.ObjectContext.RoleRight.AddObject(roleRight);
            }
        }

        public void UpdateRoleRight(RoleRight currentRoleRight)
        {
            this.ObjectContext.RoleRight.AttachAsModified(currentRoleRight, this.ChangeSet.GetOriginal(currentRoleRight));
        }

        public void DeleteRoleRight(RoleRight roleRight)
        {
            if ((roleRight.EntityState == EntityState.Detached))
            {
                this.ObjectContext.RoleRight.Attach(roleRight);
            }
            this.ObjectContext.RoleRight.DeleteObject(roleRight);
        }

        
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'SecurityQuestion' query.
        [Query(IsDefault = true)]
        public IQueryable<SecurityQuestion> GetSecurityQuestion()
        {
            if (ServiceAuthentication.IsValid())
            {
                return this.ObjectContext.SecurityQuestion;
            }
            else
            {
                //// throw fault  
                throw new FaultException<InvalidOperationException>(new InvalidOperationException("Invalid credentials"), "Invalid credentials");
            }
        }

        public void InsertSecurityQuestion(SecurityQuestion securityQuestion)
        {
            if ((securityQuestion.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(securityQuestion, EntityState.Added);
            }
            else
            {
                this.ObjectContext.SecurityQuestion.AddObject(securityQuestion);
            }
        }

        public void UpdateSecurityQuestion(SecurityQuestion currentSecurityQuestion)
        {
            this.ObjectContext.SecurityQuestion.AttachAsModified(currentSecurityQuestion, this.ChangeSet.GetOriginal(currentSecurityQuestion));
        }

        public void DeleteSecurityQuestion(SecurityQuestion securityQuestion)
        {
            if ((securityQuestion.EntityState == EntityState.Detached))
            {
                this.ObjectContext.SecurityQuestion.Attach(securityQuestion);
            }
            this.ObjectContext.SecurityQuestion.DeleteObject(securityQuestion);
        }

        
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'User' query.
        [Query(IsDefault = true)]
        public IQueryable<User> GetUser()
        {
            if (ServiceAuthentication.IsValid())
            {
                return this.ObjectContext.User;//.Include("UserOperations");
            }
            else
            {
                //// throw fault  
                throw new FaultException<InvalidOperationException>(new InvalidOperationException("Invalid credentials"), "Invalid credentials");
            }
        }

        public void DeleteUser(User user)
        {
            if ((user.EntityState == EntityState.Detached))
            {
                this.ObjectContext.User.Attach(user);
            }
            this.ObjectContext.User.DeleteObject(user);
        }

        
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'UserApplicationStatus' query.
        [Query(IsDefault = true)]
        public IQueryable<UserApplicationStatu> GetUserApplicationStatus()
        {
            if (ServiceAuthentication.IsValid())
            {
                return this.ObjectContext.UserApplicationStatus;
            }
            else
            {
                //// throw fault  
                throw new FaultException<InvalidOperationException>(new InvalidOperationException("Invalid credentials"), "Invalid credentials");
            }
        }

        public void InsertUserApplicationStatu(UserApplicationStatu userApplicationStatu)
        {
            if ((userApplicationStatu.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(userApplicationStatu, EntityState.Added);
            }
            else
            {
                this.ObjectContext.UserApplicationStatus.AddObject(userApplicationStatu);
            }
        }

        public void UpdateUserApplicationStatu(UserApplicationStatu currentUserApplicationStatu)
        {
            this.ObjectContext.UserApplicationStatus.AttachAsModified(currentUserApplicationStatu, this.ChangeSet.GetOriginal(currentUserApplicationStatu));
        }

        public void DeleteUserApplicationStatu(UserApplicationStatu userApplicationStatu)
        {
            if ((userApplicationStatu.EntityState == EntityState.Detached))
            {
                this.ObjectContext.UserApplicationStatus.Attach(userApplicationStatu);
            }
            this.ObjectContext.UserApplicationStatus.DeleteObject(userApplicationStatu);
        }

        
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'UserRole' query.
        [Query(IsDefault = true)]
        public IQueryable<UserRole> GetUserRole()
        {
            if (ServiceAuthentication.IsValid())
            {
                return this.ObjectContext.UserRole;
            }
            else
            {
                //// throw fault  
                throw new FaultException<InvalidOperationException>(new InvalidOperationException("Invalid credentials"), "Invalid credentials");
            }
        }

        public void InsertUserRole(UserRole userRole)
        {
            if ((userRole.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(userRole, EntityState.Added);
            }
            else
            {
                this.ObjectContext.UserRole.AddObject(userRole);
            }
        }

        public void UpdateUserRole(UserRole currentUserRole)
        {
            this.ObjectContext.UserRole.AttachAsModified(currentUserRole, this.ChangeSet.GetOriginal(currentUserRole));
        }

        public void DeleteUserRole(UserRole userRole)
        {
            if ((userRole.EntityState == EntityState.Detached))
            {
                this.ObjectContext.UserRole.Attach(userRole);
            }
            this.ObjectContext.UserRole.DeleteObject(userRole);
        }

        
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'Cultures' query.
       

        public void InsertCulture(Culture culture)
        {
            if ((culture.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(culture, EntityState.Added);
            }
            else
            {
                this.ObjectContext.Cultures.AddObject(culture);
            }
        }

        public void UpdateCulture(Culture currentCulture)
        {
            this.ObjectContext.Cultures.AttachAsModified(currentCulture, this.ChangeSet.GetOriginal(currentCulture));
        }

        public void DeleteCulture(Culture culture)
        {
            if ((culture.EntityState == EntityState.Detached))
            {
                this.ObjectContext.Cultures.Attach(culture);
            }
            this.ObjectContext.Cultures.DeleteObject(culture);
        }

        
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'NegotiationOrganizations' query.
      

        public void InsertNegotiationOrganization(NegotiationOrganization negotiationOrganization)
        {
            if ((negotiationOrganization.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(negotiationOrganization, EntityState.Added);
            }
            else
            {
                this.ObjectContext.NegotiationOrganizations.AddObject(negotiationOrganization);
            }
        }

        public void UpdateNegotiationOrganization(NegotiationOrganization currentNegotiationOrganization)
        {
            this.ObjectContext.NegotiationOrganizations.AttachAsModified(currentNegotiationOrganization, this.ChangeSet.GetOriginal(currentNegotiationOrganization));
        }

        public void DeleteNegotiationOrganization(NegotiationOrganization negotiationOrganization)
        {
            if ((negotiationOrganization.EntityState == EntityState.Detached))
            {
                this.ObjectContext.NegotiationOrganizations.Attach(negotiationOrganization);
            }
            this.ObjectContext.NegotiationOrganizations.DeleteObject(negotiationOrganization);
        }

        
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'Organizations' query.
        

        public void InsertOrganization(Organization organization)
        {
            if ((organization.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(organization, EntityState.Added);
            }
            else
            {
                this.ObjectContext.Organizations.AddObject(organization);
            }
        }

        public void UpdateOrganization(Organization currentOrganization)
        {
            this.ObjectContext.Organizations.AttachAsModified(currentOrganization, this.ChangeSet.GetOriginal(currentOrganization));
        }

        public void DeleteOrganization(Organization organization)
        {
            if ((organization.EntityState == EntityState.Detached))
            {
                this.ObjectContext.Organizations.Attach(organization);
            }
            this.ObjectContext.Organizations.DeleteObject(organization);
        }

        
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'OrganizationTypes' query.
        public IQueryable<OrganizationType> GetOrganizationTypes()
        {
            return this.ObjectContext.OrganizationTypes;
        }

        public void InsertOrganizationType(OrganizationType organizationType)
        {
            if ((organizationType.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(organizationType, EntityState.Added);
            }
            else
            {
                this.ObjectContext.OrganizationTypes.AddObject(organizationType);
            }
        }

        public void UpdateOrganizationType(OrganizationType currentOrganizationType)
        {
            this.ObjectContext.OrganizationTypes.AttachAsModified(currentOrganizationType, this.ChangeSet.GetOriginal(currentOrganizationType));
        }

        public void DeleteOrganizationType(OrganizationType organizationType)
        {
            if ((organizationType.EntityState == EntityState.Detached))
            {
                this.ObjectContext.OrganizationTypes.Attach(organizationType);
            }
            this.ObjectContext.OrganizationTypes.DeleteObject(organizationType);
        }
         
       

      

        public void InsertUserOrganization(UserOrganization userOrganization)
        {
            if ((userOrganization.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(userOrganization, EntityState.Added);
            }
            else
            {
                this.ObjectContext.UserOrganizations.AddObject(userOrganization);
            }
        }

        public void UpdateUserOrganization(UserOrganization currentUserOrganization)
        {
            this.ObjectContext.UserOrganizations.AttachAsModified(currentUserOrganization, this.ChangeSet.GetOriginal(currentUserOrganization));
        }

        public void DeleteUserOrganization(UserOrganization userOrganization)
        {
            if ((userOrganization.EntityState == EntityState.Detached))
            {
                this.ObjectContext.UserOrganizations.Attach(userOrganization);
            }
            this.ObjectContext.UserOrganizations.DeleteObject(userOrganization);
        }

    }
}


