
namespace citPOINT.eNeg.CommonTest.Web
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Data.Objects.DataClasses;


    // The MetadataTypeAttribute identifies ChannelMetadata as the class
    // that carries additional metadata for the Channel class.
    [MetadataTypeAttribute(typeof(Channel.ChannelMetadata))]
    public partial class Channel
    {

        // This class allows you to attach custom attributes to properties
        // of the Channel class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class ChannelMetadata
        {

            // Metadata classes are not meant to be instantiated.
            private ChannelMetadata()
            {
            }

            public Guid ChannelID { get; set; }

            public string ChannelName { get; set; }

            public ChannelType ChannelType { get; set; }

            public Nullable<Guid> ChannelTypeID { get; set; }

            public EntityCollection<Message> Messages { get; set; }
        }
    }

    // The MetadataTypeAttribute identifies ChannelTypeMetadata as the class
    // that carries additional metadata for the ChannelType class.
    [MetadataTypeAttribute(typeof(ChannelType.ChannelTypeMetadata))]
    public partial class ChannelType
    {

        // This class allows you to attach custom attributes to properties
        // of the ChannelType class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class ChannelTypeMetadata
        {

            // Metadata classes are not meant to be instantiated.
            private ChannelTypeMetadata()
            {
            }

            public EntityCollection<Channel> Channels { get; set; }

            public Guid ChannelTypeID { get; set; }

            public string TypeName { get; set; }
        }
    }

    // The MetadataTypeAttribute identifies ConversationMetadata as the class
    // that carries additional metadata for the Conversation class.
    [MetadataTypeAttribute(typeof(Conversation.ConversationMetadata))]
    public partial class Conversation
    {

        // This class allows you to attach custom attributes to properties
        // of the Conversation class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class ConversationMetadata
        {

            // Metadata classes are not meant to be instantiated.
            private ConversationMetadata()
            {
            }

            public Guid ConversationID { get; set; }

            public string ConversationName { get; set; }

            public Nullable<bool> Deleted { get; set; }

            public Nullable<Guid> DeletedBy { get; set; }

            public Nullable<DateTime> DeletedOn { get; set; }

            public EntityCollection<Message> Messages { get; set; }

            public Negotiation Negotiation { get; set; }

            public Guid NegotiationID { get; set; }

            public User User { get; set; }
        }
    }

    // The MetadataTypeAttribute identifies MessageMetadata as the class
    // that carries additional metadata for the Message class.
    [MetadataTypeAttribute(typeof(Message.MessageMetadata))]
    public partial class Message
    {

        // This class allows you to attach custom attributes to properties
        // of the Message class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class MessageMetadata
        {

            // Metadata classes are not meant to be instantiated.
            private MessageMetadata()
            {
            }

            public EntityCollection<Attachement> Attachements { get; set; }

            public Channel Channel { get; set; }

            public Guid ChannelID { get; set; }

            public Conversation Conversation { get; set; }

            public Nullable<Guid> ConversationID { get; set; }

            public Nullable<bool> Deleted { get; set; }

            public Nullable<Guid> DeletedBy { get; set; }

            public Nullable<DateTime> DeletedOn { get; set; }

            public string MessageContent { get; set; }

            public Nullable<DateTime> MessageDate { get; set; }

            public Guid MessageID { get; set; }

            public string MessageReceiver { get; set; }

            public string MessageSender { get; set; }

            public string MessageSubject { get; set; }

            public User User { get; set; }
        }
    }
}
