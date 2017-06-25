
namespace citPOINT.eNeg.CommonTest.Web
{
    using System.Data;
    using System.Linq;
    using System.ServiceModel.DomainServices.EntityFramework;
    using System.ServiceModel.DomainServices.Hosting;


    // Implements application logic using the eNegEntities context.
    // TODO: Add your application logic to these methods or in additional methods.
    // TODO: Wire up authentication (Windows/ASP.NET Forms) and uncomment the following to disable anonymous access
    // Also consider adding roles to restrict access as appropriate.
    // [RequiresAuthentication]
    [EnableClientAccess()]
    public class eNegService : LinqToEntitiesDomainService<eNegEntities>
    {

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'Channels' query.
        public IQueryable<Channel> GetChannels()
        {
            return this.ObjectContext.Channels;
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

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'ChannelTypes' query.
        public IQueryable<ChannelType> GetChannelTypes()
        {
            return this.ObjectContext.ChannelTypes;
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

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'Conversations' query.
        public IQueryable<Conversation> GetConversations()
        {
            return this.ObjectContext.Conversations;
        }

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

        public void UpdateConversation(Conversation currentConversation)
        {
            this.ObjectContext.Conversations.AttachAsModified(currentConversation, this.ChangeSet.GetOriginal(currentConversation));
        }

        public void DeleteConversation(Conversation conversation)
        {
            if ((conversation.EntityState == EntityState.Detached))
            {
                this.ObjectContext.Conversations.Attach(conversation);
            }
            this.ObjectContext.Conversations.DeleteObject(conversation);
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'Messages' query.
        public IQueryable<Message> GetMessages()
        {
            return this.ObjectContext.Messages;
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
    }
}


