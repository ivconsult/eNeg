
namespace citPOINT.eNeg.Data.Web
{
    using System;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Data.Objects.DataClasses;
    using System.Runtime.Serialization;
    using System.ServiceModel.DomainServices.Server;






    // The MetadataTypeAttribute identifies CultureMetadata as the class
    // that carries additional metadata for the Culture class.
    [MetadataTypeAttribute(typeof(Culture.CultureMetadata))]
    public partial class Culture
    {

        // This class allows you to attach custom attributes to properties
        // of the Culture class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class CultureMetadata
        {

            // Metadata classes are not meant to be instantiated.
            private CultureMetadata()
            {
            }

            public int CultureID { get; set; }

            public string CultureName { get; set; }

            public EntityCollection<User> Users { get; set; }
        }
    }

    // The MetadataTypeAttribute identifies NegotiationOrganizationMetadata as the class
    // that carries additional metadata for the NegotiationOrganization class.
    [MetadataTypeAttribute(typeof(NegotiationOrganization.NegotiationOrganizationMetadata))]
    public partial class NegotiationOrganization
    {

        // This class allows you to attach custom attributes to properties
        // of the NegotiationOrganization class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class NegotiationOrganizationMetadata
        {

            // Metadata classes are not meant to be instantiated.
            private NegotiationOrganizationMetadata()
            {
            }

            public bool Deleted { get; set; }

            public Nullable<Guid> DeletedBy { get; set; }

            public Nullable<DateTime> DeletedOn { get; set; }

            public Negotiation Negotiation { get; set; }

            public Guid NegotiationID { get; set; }

            public Guid NegotiationOrganizationID { get; set; }

            public Organization Organization { get; set; }

            public Guid OrganizationID { get; set; }
        }
    }

    // The MetadataTypeAttribute identifies OrganizationMetadata as the class
    // that carries additional metadata for the Organization class.
    [MetadataTypeAttribute(typeof(Organization.OrganizationMetadata))]
    public partial class Organization
    {

        // This class allows you to attach custom attributes to properties
        // of the Organization class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class OrganizationMetadata
        {

            // Metadata classes are not meant to be instantiated.
            private OrganizationMetadata()
            {
            }

            public bool Deleted { get; set; }

            public Nullable<Guid> DeletedBy { get; set; }

            public Nullable<DateTime> DeletedOn { get; set; }

            public EntityCollection<NegotiationOrganization> NegotiationOrganizations { get; set; }

            public Guid OrganizationID { get; set; }
            
            [Display(Name = "Name")]
            public string OrganizationName { get; set; }
            
            [Display(Name = "Type")]
            public OrganizationType OrganizationType { get; set; }
            
            [Display(Name = "Type")]
            public int OrganizationTypeID { get; set; }

            public EntityCollection<UserOrganization> UserOrganizations { get; set; }
        }
    }

    // The MetadataTypeAttribute identifies OrganizationTypeMetadata as the class
    // that carries additional metadata for the OrganizationType class.
    [MetadataTypeAttribute(typeof(OrganizationType.OrganizationTypeMetadata))]
    public partial class OrganizationType
    {

        // This class allows you to attach custom attributes to properties
        // of the OrganizationType class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class OrganizationTypeMetadata
        {

            // Metadata classes are not meant to be instantiated.
            private OrganizationTypeMetadata()
            {
            }

            public string OrganizationTypeName { get; set; }

            public EntityCollection<Organization> Organizations { get; set; }

            public int OrganizationTypeID { get; set; }
        }
    }

    // The MetadataTypeAttribute identifies UserOrganizationMetadata as the class
    // that carries additional metadata for the UserOrganization class.
    [MetadataTypeAttribute(typeof(UserOrganization.UserOrganizationMetadata))]
    public partial class UserOrganization
    {

        // This class allows you to attach custom attributes to properties
        // of the UserOrganization class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class UserOrganizationMetadata
        {

            // Metadata classes are not meant to be instantiated.
            private UserOrganizationMetadata()
            {
            }

            public bool Deleted { get; set; }

            public Nullable<Guid> DeletedBy { get; set; }

            public Nullable<DateTime> DeletedOn { get; set; }

            public byte MemberStatus { get; set; }

            public Organization Organization { get; set; }

            public Guid OrganizationID { get; set; }

            public User User { get; set; }

            public Guid UserID { get; set; }

            public Guid UserOrganizationID { get; set; }
        }
    }

    // The MetadataTypeAttribute identifies UserOperationMetadata as the class
    // that carries additional metadata for the UserOperation class.
    [MetadataTypeAttribute(typeof(UserOperation.UserOperationMetadata))]
    public partial class UserOperation
    {

        // This class allows you to attach custom attributes to properties
        // of the UserOperation class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class UserOperationMetadata
        {

            // Metadata classes are not meant to be instantiated.
            private UserOperationMetadata()
            {
            }

            public Guid OperationID { get; set; }

            public string OperationString { get; set; }

            public Nullable<byte> Type { get; set; }

            public Nullable<Guid> UserID { get; set; }

            public User User { get; set; }

            public string NewEmailAddress { get; set; }

            public Nullable<Guid> RequestUserID { get; set; }

            public Nullable<Guid> OrganizationID { get; set; }
        }
    }

    // The MetadataTypeAttribute identifies AccountTypeMetadata as the class
    // that carries additional metadata for the AccountType class.
    [MetadataTypeAttribute(typeof(AccountType.AccountTypeMetadata))]
    public partial class AccountType
    {

        // This class allows you to attach custom attributes to properties
        // of the AccountType class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class AccountTypeMetadata
        {

            // Metadata classes are not meant to be instantiated.
            private AccountTypeMetadata()
            {
            }

            public Guid AccountTypeID { get; set; }

            public string TypeDescription { get; set; }

            public string TypeName { get; set; }

            public EntityCollection<User> User { get; set; }
        }
    }

    // The MetadataTypeAttribute identifies ActionTypeMetadata as the class
    // that carries additional metadata for the ActionType class.
    [MetadataTypeAttribute(typeof(ActionType.ActionTypeMetadata))]
    public partial class ActionType
    {

        // This class allows you to attach custom attributes to properties
        // of the ActionType class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class ActionTypeMetadata
        {

            // Metadata classes are not meant to be instantiated.
            private ActionTypeMetadata()
            {
            }

            public string ActionDescription { get; set; }

            public Guid ActionTypeID { get; set; }

            public EntityCollection<History> Histories { get; set; }
        }
    }

    // The MetadataTypeAttribute identifies ApplicationMetadata as the class
    // that carries additional metadata for the Application class.
    [MetadataTypeAttribute(typeof(Application.ApplicationMetadata))]
    public partial class Application
    {

        // This class allows you to attach custom attributes to properties
        // of the Application class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class ApplicationMetadata
        {

            // Metadata classes are not meant to be instantiated.
            private ApplicationMetadata()
            {
            }

            public Guid ApplicationID { get; set; }

            public string ApplicationTitle { get; set; }

            public string AssemblyName { get; set; }

            public string ModuleName { get; set; }

            public string XapFilePath { get; set; }

            public int ApplicationRank { get; set; }

            public string ApplicationMainServicePath { get; set; }

            public string ApplicationBaseAddress { get; set; }

            public EntityCollection<NegotiationApplicationStatu> NegotiationApplicationStatus { get; set; }
        }
    }

    // The MetadataTypeAttribute identifies AttachementMetadata as the class
    // that carries additional metadata for the Attachement class.
    [MetadataTypeAttribute(typeof(Attachement.AttachementMetadata))]
    public partial class Attachement
    {

        // This class allows you to attach custom attributes to properties
        // of the Attachement class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class AttachementMetadata
        {

            // Metadata classes are not meant to be instantiated.
            private AttachementMetadata()
            {
            }

            public byte[] AttachementContent { get; set; }

            public Guid AttachementID { get; set; }

            public string AttachementName { get; set; }

            public Message Message { get; set; }

            public Nullable<Guid> MessageID { get; set; }
        }
    }

    // The MetadataTypeAttribute identifies CategoryMetadata as the class
    // that carries additional metadata for the Category class.
    [MetadataTypeAttribute(typeof(Category.CategoryMetadata))]
    public partial class Category
    {

        // This class allows you to attach custom attributes to properties
        // of the Category class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class CategoryMetadata
        {

            // Metadata classes are not meant to be instantiated.
            private CategoryMetadata()
            {
            }

            public int CategoryID { get; set; }

            public EntityCollection<CategoryLog> CategoryLogs { get; set; }

            public string CategoryName { get; set; }
        }
    }

    // The MetadataTypeAttribute identifies CategoryLogMetadata as the class
    // that carries additional metadata for the CategoryLog class.
    [MetadataTypeAttribute(typeof(CategoryLog.CategoryLogMetadata))]
    public partial class CategoryLog
    {

        // This class allows you to attach custom attributes to properties
        // of the CategoryLog class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class CategoryLogMetadata
        {

            // Metadata classes are not meant to be instantiated.
            private CategoryLogMetadata()
            {
            }

            public Category Category { get; set; }

            public int CategoryID { get; set; }

            public int CategoryLogID { get; set; }

            public Log Log { get; set; }

            public int LogID { get; set; }
        }
    }

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

        [DataMember]
        [DefaultValue(false)]
        public bool IsConvSelected { get; set; }


        [DataMember]
        [DefaultValue(false)]
        public bool IsNewConversation { get; set; }




    }

    // The MetadataTypeAttribute identifies CountryMetadata as the class
    // that carries additional metadata for the Country class.
    [MetadataTypeAttribute(typeof(Country.CountryMetadata))]
    public partial class Country
    {

        // This class allows you to attach custom attributes to properties
        // of the Country class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class CountryMetadata
        {

            // Metadata classes are not meant to be instantiated.
            private CountryMetadata()
            {
            }

            public Guid CountryID { get; set; }

            public string CountryName { get; set; }

            public EntityCollection<User> User { get; set; }
        }
    }

    // The MetadataTypeAttribute identifies HistoryMetadata as the class
    // that carries additional metadata for the History class.
    [MetadataTypeAttribute(typeof(History.HistoryMetadata))]
    public partial class History
    {

        // This class allows you to attach custom attributes to properties
        // of the History class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class HistoryMetadata
        {

            // Metadata classes are not meant to be instantiated.
            private HistoryMetadata()
            {
            }

            public DateTime ActionDate { get; set; }

            public ActionType ActionType { get; set; }

            public Guid ActionTypeID { get; set; }

            public Guid HistoryID { get; set; }

            public string NewValue { get; set; }

            public string OldValue { get; set; }

            public string TableName { get; set; }

            public User User { get; set; }

            public Guid UserID { get; set; }
        }
    }

    // The MetadataTypeAttribute identifies LogMetadata as the class
    // that carries additional metadata for the Log class.
    [MetadataTypeAttribute(typeof(Log.LogMetadata))]
    public partial class Log
    {

        // This class allows you to attach custom attributes to properties
        // of the Log class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class LogMetadata
        {

            // Metadata classes are not meant to be instantiated.
            private LogMetadata()
            {
            }

            public string AppDomainName { get; set; }

            public EntityCollection<CategoryLog> CategoryLogs { get; set; }

            public Nullable<int> EventID { get; set; }

            public string FormattedMessage { get; set; }

            public int LogID { get; set; }

            public string MachineName { get; set; }

            public string Message { get; set; }

            public int Priority { get; set; }

            public string ProcessID { get; set; }

            public string ProcessName { get; set; }

            public string Severity { get; set; }

            public string ThreadName { get; set; }

            public DateTime Timestamp { get; set; }

            public string Title { get; set; }

            public string Win32ThreadId { get; set; }
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

            [Required(ErrorMessageResourceName = "MessageContentRequired", ErrorMessageResourceType = typeof(ErrorResources))]
            [CustomValidation(typeof(UserRules), "CheckMessageLenght")]
            public string MessageContent { get; set; }

            public Nullable<DateTime> MessageDate { get; set; }

            public Guid MessageID { get; set; }


            [Required(ErrorMessageResourceName = "MessageReceiverRequired", ErrorMessageResourceType = typeof(ErrorResources))]
            public string MessageReceiver { get; set; }

            [Required(ErrorMessageResourceName = "MessageSenderRequired", ErrorMessageResourceType = typeof(ErrorResources))]
            public string MessageSender { get; set; }

            public string MessageSubject { get; set; }

            [DefaultValue(false)]
            public bool IsSent { get; set; }

            public User User { get; set; }


            /// <summary>
            /// Gets or sets a value indicating whether this instance is apps message.
            /// </summary>
            /// <value>
            /// 	<c>true</c> if this instance is apps message; otherwise, <c>false</c>.
            /// </value>
            [DefaultValue(false)]
            public bool IsAppsMessage { get; set; }

        }



        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="Message"/> is recieved.
        /// </summary>
        /// <value><c>true</c> if recieved; otherwise, <c>false</c>.</value>
        [DataMember]
        public bool Received
        {
            get { return !this.IsSent; }
            set
            {
                this.IsSent = !value;
            }
        }


        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="Message"/> is sent.
        /// </summary>
        /// <value><c>true</c> if sent; otherwise, <c>false</c>.</value>
        [DataMember]
        public bool Sent
        {
            get { return this.IsSent; }
            set
            {
                this.IsSent = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is selected.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is selected; otherwise, <c>false</c>.
        /// </value>
        [DataMember]
        [DefaultValue(false)]
        public bool IsSelected { get; set; }

        /// <summary>
        /// Gets the negotiator.
        /// </summary>
        /// <value>The negotiator.</value>
        [DataMember]
        public string Negotiator
        {
            get
            {
                //if (IsSent)
                //{
                //    return MessageReceiver;
                //}
                return MessageSender;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is checked.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is checked; otherwise, <c>false</c>.
        /// </value>
        [DataMember]
        [DefaultValue(false)]
        public bool IsChecked { get; set; }
    }

    // The MetadataTypeAttribute identifies NegotiationMetadata as the class
    // that carries additional metadata for the Negotiation class.
    [MetadataTypeAttribute(typeof(Negotiation.NegotiationMetadata))]
    public partial class Negotiation
    {

        // This class allows you to attach custom attributes to properties
        // of the Negotiation class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class NegotiationMetadata
        {

            // Metadata classes are not meant to be instantiated.
            private NegotiationMetadata()
            {
            }
            public EntityCollection<Conversation> Conversations { get; set; }

            public DateTime CreatedDate { get; set; }

            public Nullable<bool> Deleted { get; set; }

            public Nullable<DateTime> DeletedDate { get; set; }

            public EntityCollection<NegotiationApplicationStatu> NegotiationApplicationStatus { get; set; }

            public Guid NegotiationID { get; set; }

            public string NegotiationName { get; set; }

            //[Include]
            public NegotiationStatu NegotiationStatu { get; set; }

            public Nullable<Guid> StatusID { get; set; }

            public Guid UserID { get; set; }
        }

        [DataMember]
        [DefaultValue(false)]
        public bool IsNegSelected { get; set; }


        [DataMember]
        [DefaultValue(false)]
        public bool IsNewNegotiation { get; set; }



    }


    // The MetadataTypeAttribute identifies NegotiationApplicationStatuMetadata as the class
    // that carries additional metadata for the NegotiationApplicationStatu class.
    [MetadataTypeAttribute(typeof(NegotiationApplicationStatu.NegotiationApplicationStatuMetadata))]
    public partial class NegotiationApplicationStatu
    {

        // This class allows you to attach custom attributes to properties
        // of the NegotiationApplicationStatu class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class NegotiationApplicationStatuMetadata
        {

            // Metadata classes are not meant to be instantiated.
            private NegotiationApplicationStatuMetadata()
            {
            }

            public bool Active { get; set; }

            [Include]
            public Application Application { get; set; }

            public Guid ApplicationID { get; set; }

            public Negotiation Negotiation { get; set; }

            public Guid NegotiationApplicationStatusID { get; set; }

            public Guid NegotiationID { get; set; }

            public User User { get; set; }

            public Guid UserID { get; set; }
        }


    }

    // The MetadataTypeAttribute identifies NegotiationStatuMetadata as the class
    // that carries additional metadata for the NegotiationStatu class.
    [MetadataTypeAttribute(typeof(NegotiationStatu.NegotiationStatuMetadata))]
    public partial class NegotiationStatu
    {

        // This class allows you to attach custom attributes to properties
        // of the NegotiationStatu class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class NegotiationStatuMetadata
        {

            // Metadata classes are not meant to be instantiated.
            private NegotiationStatuMetadata()
            {
            }

            public EntityCollection<Negotiation> Negotiations { get; set; }

            public string StatusDescription { get; set; }

            public Guid StatusID { get; set; }
        }
    }

    // The MetadataTypeAttribute identifies PreferedLanguageMetadata as the class
    // that carries additional metadata for the PreferedLanguage class.
    [MetadataTypeAttribute(typeof(PreferedLanguage.PreferedLanguageMetadata))]
    public partial class PreferedLanguage
    {

        // This class allows you to attach custom attributes to properties
        // of the PreferedLanguage class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class PreferedLanguageMetadata
        {

            // Metadata classes are not meant to be instantiated.
            private PreferedLanguageMetadata()
            {
            }

            public int LCID { get; set; }

            public string PreferedLanguage1 { get; set; }

            public EntityCollection<User> User { get; set; }
        }
    }

    // The MetadataTypeAttribute identifies ProfileMetadata as the class
    // that carries additional metadata for the Profile class.
    [MetadataTypeAttribute(typeof(Profile.ProfileMetadata))]
    public partial class Profile
    {

        // This class allows you to attach custom attributes to properties
        // of the Profile class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class ProfileMetadata
        {

            // Metadata classes are not meant to be instantiated.
            private ProfileMetadata()
            {
            }

            public string CurrentTheme { get; set; }

            public Guid ProfileID { get; set; }

            public User User { get; set; }

            public Nullable<Guid> UserID { get; set; }
        }
    }

    // The MetadataTypeAttribute identifies RightMetadata as the class
    // that carries additional metadata for the Right class.
    [MetadataTypeAttribute(typeof(Right.RightMetadata))]
    public partial class Right
    {

        // This class allows you to attach custom attributes to properties
        // of the Right class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class RightMetadata
        {

            // Metadata classes are not meant to be instantiated.
            private RightMetadata()
            {
            }

            public string RightDescription { get; set; }

            public Guid RightID { get; set; }

            public string RightName { get; set; }

            public EntityCollection<RoleRight> RoleRight { get; set; }
        }
    }

    // The MetadataTypeAttribute identifies RoleMetadata as the class
    // that carries additional metadata for the Role class.
    [MetadataTypeAttribute(typeof(Role.RoleMetadata))]
    public partial class Role
    {

        // This class allows you to attach custom attributes to properties
        // of the Role class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class RoleMetadata
        {

            // Metadata classes are not meant to be instantiated.
            private RoleMetadata()
            {
            }

            public string RoleDescription { get; set; }

            public Guid RoleID { get; set; }

            public string RoleName { get; set; }

            public EntityCollection<RoleRight> RoleRight { get; set; }

            public EntityCollection<UserRole> UserRole { get; set; }
        }
    }

    // The MetadataTypeAttribute identifies RoleRightMetadata as the class
    // that carries additional metadata for the RoleRight class.
    [MetadataTypeAttribute(typeof(RoleRight.RoleRightMetadata))]
    public partial class RoleRight
    {

        // This class allows you to attach custom attributes to properties
        // of the RoleRight class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class RoleRightMetadata
        {

            // Metadata classes are not meant to be instantiated.
            private RoleRightMetadata()
            {
            }

            public Right Right { get; set; }

            public Nullable<Guid> RightID { get; set; }

            public Role Role { get; set; }

            public Nullable<Guid> RoleID { get; set; }

            public Guid RoleRightID { get; set; }
        }
    }

    // The MetadataTypeAttribute identifies SecurityQuestionMetadata as the class
    // that carries additional metadata for the SecurityQuestion class.
    [MetadataTypeAttribute(typeof(SecurityQuestion.SecurityQuestionMetadata))]
    public partial class SecurityQuestion
    {

        // This class allows you to attach custom attributes to properties
        // of the SecurityQuestion class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class SecurityQuestionMetadata
        {

            // Metadata classes are not meant to be instantiated.
            private SecurityQuestionMetadata()
            {
            }

            public string Question { get; set; }

            public Guid SecurityQuestionID { get; set; }

            public EntityCollection<User> User { get; set; }
        }
    }

    // The MetadataTypeAttribute identifies UserMetadata as the class
    // that carries additional metadata for the User class.
    [MetadataTypeAttribute(typeof(User.UserMetadata))]
    public partial class User
    {



    }

    // The MetadataTypeAttribute identifies UserApplicationStatuMetadata as the class
    // that carries additional metadata for the UserApplicationStatu class.
    [MetadataTypeAttribute(typeof(UserApplicationStatu.UserApplicationStatuMetadata))]
    public partial class UserApplicationStatu
    {

        // This class allows you to attach custom attributes to properties
        // of the UserApplicationStatu class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class UserApplicationStatuMetadata
        {

            // Metadata classes are not meant to be instantiated.
            private UserApplicationStatuMetadata()
            {
            }

            public Application Application { get; set; }

            public Guid ApplicationID { get; set; }

            public Nullable<bool> IsDMActive { get; set; }

            public User User { get; set; }

            public Guid UserAppStatusID { get; set; }

            public Guid UserID { get; set; }
        }
    }

    // The MetadataTypeAttribute identifies UserRoleMetadata as the class
    // that carries additional metadata for the UserRole class.
    [MetadataTypeAttribute(typeof(UserRole.UserRoleMetadata))]
    public partial class UserRole
    {

        // This class allows you to attach custom attributes to properties
        // of the UserRole class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class UserRoleMetadata
        {

            // Metadata classes are not meant to be instantiated.
            private UserRoleMetadata()
            {
            }

            public Role Role { get; set; }

            public Nullable<Guid> RoleID { get; set; }

            public User User { get; set; }

            public Nullable<Guid> UserID { get; set; }

            public Guid UserRoleID { get; set; }
        }
    }
}
