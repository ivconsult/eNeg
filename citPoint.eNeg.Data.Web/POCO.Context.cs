//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Data.Objects;
using System.Data.EntityClient;

namespace citPOINT.eNeg.Data.Web
{
    public partial class eNegEntities : ObjectContext
    {
        public const string ConnectionString = "name=eNegEntities";
        public const string ContainerName = "eNegEntities";
    
        #region Constructors
    
        public eNegEntities()
            : base(ConnectionString, ContainerName)
        {
            this.ContextOptions.LazyLoadingEnabled = true;
        }
    
        public eNegEntities(string connectionString)
            : base(connectionString, ContainerName)
        {
            this.ContextOptions.LazyLoadingEnabled = true;
        }
    
        public eNegEntities(EntityConnection connection)
            : base(connection, ContainerName)
        {
            this.ContextOptions.LazyLoadingEnabled = true;
        }
    
        #endregion
    
        #region ObjectSet Properties
    
        public ObjectSet<Country> Country
        {
            get { return _country  ?? (_country = CreateObjectSet<Country>("Country")); }
        }
        private ObjectSet<Country> _country;
    
        public ObjectSet<PreferedLanguage> PreferedLanguage
        {
            get { return _preferedLanguage  ?? (_preferedLanguage = CreateObjectSet<PreferedLanguage>("PreferedLanguage")); }
        }
        private ObjectSet<PreferedLanguage> _preferedLanguage;
    
        public ObjectSet<Profile> Profile
        {
            get { return _profile  ?? (_profile = CreateObjectSet<Profile>("Profile")); }
        }
        private ObjectSet<Profile> _profile;
    
        public ObjectSet<Right> Right
        {
            get { return _right  ?? (_right = CreateObjectSet<Right>("Right")); }
        }
        private ObjectSet<Right> _right;
    
        public ObjectSet<Role> Role
        {
            get { return _role  ?? (_role = CreateObjectSet<Role>("Role")); }
        }
        private ObjectSet<Role> _role;
    
        public ObjectSet<RoleRight> RoleRight
        {
            get { return _roleRight  ?? (_roleRight = CreateObjectSet<RoleRight>("RoleRight")); }
        }
        private ObjectSet<RoleRight> _roleRight;
    
        public ObjectSet<SecurityQuestion> SecurityQuestion
        {
            get { return _securityQuestion  ?? (_securityQuestion = CreateObjectSet<SecurityQuestion>("SecurityQuestion")); }
        }
        private ObjectSet<SecurityQuestion> _securityQuestion;
    
        public ObjectSet<User> User
        {
            get { return _user  ?? (_user = CreateObjectSet<User>("User")); }
        }
        private ObjectSet<User> _user;
    
        public ObjectSet<UserRole> UserRole
        {
            get { return _userRole  ?? (_userRole = CreateObjectSet<UserRole>("UserRole")); }
        }
        private ObjectSet<UserRole> _userRole;
    
        public ObjectSet<AccountType> AccountType
        {
            get { return _accountType  ?? (_accountType = CreateObjectSet<AccountType>("AccountType")); }
        }
        private ObjectSet<AccountType> _accountType;
    
        public ObjectSet<Application> Applications
        {
            get { return _applications  ?? (_applications = CreateObjectSet<Application>("Applications")); }
        }
        private ObjectSet<Application> _applications;
    
        public ObjectSet<Attachement> Attachements
        {
            get { return _attachements  ?? (_attachements = CreateObjectSet<Attachement>("Attachements")); }
        }
        private ObjectSet<Attachement> _attachements;
    
        public ObjectSet<Channel> Channels
        {
            get { return _channels  ?? (_channels = CreateObjectSet<Channel>("Channels")); }
        }
        private ObjectSet<Channel> _channels;
    
        public ObjectSet<ChannelType> ChannelTypes
        {
            get { return _channelTypes  ?? (_channelTypes = CreateObjectSet<ChannelType>("ChannelTypes")); }
        }
        private ObjectSet<ChannelType> _channelTypes;
    
        public ObjectSet<Conversation> Conversations
        {
            get { return _conversations  ?? (_conversations = CreateObjectSet<Conversation>("Conversations")); }
        }
        private ObjectSet<Conversation> _conversations;
    
        public ObjectSet<Negotiation> Negotiations
        {
            get { return _negotiations  ?? (_negotiations = CreateObjectSet<Negotiation>("Negotiations")); }
        }
        private ObjectSet<Negotiation> _negotiations;
    
        public ObjectSet<NegotiationApplicationStatu> NegotiationApplicationStatus
        {
            get { return _negotiationApplicationStatus  ?? (_negotiationApplicationStatus = CreateObjectSet<NegotiationApplicationStatu>("NegotiationApplicationStatus")); }
        }
        private ObjectSet<NegotiationApplicationStatu> _negotiationApplicationStatus;
    
        public ObjectSet<NegotiationStatu> NegotiationStatus
        {
            get { return _negotiationStatus  ?? (_negotiationStatus = CreateObjectSet<NegotiationStatu>("NegotiationStatus")); }
        }
        private ObjectSet<NegotiationStatu> _negotiationStatus;
    
        public ObjectSet<ActionType> ActionTypes
        {
            get { return _actionTypes  ?? (_actionTypes = CreateObjectSet<ActionType>("ActionTypes")); }
        }
        private ObjectSet<ActionType> _actionTypes;
    
        public ObjectSet<Category> Categories
        {
            get { return _categories  ?? (_categories = CreateObjectSet<Category>("Categories")); }
        }
        private ObjectSet<Category> _categories;
    
        public ObjectSet<CategoryLog> CategoryLogs
        {
            get { return _categoryLogs  ?? (_categoryLogs = CreateObjectSet<CategoryLog>("CategoryLogs")); }
        }
        private ObjectSet<CategoryLog> _categoryLogs;
    
        public ObjectSet<History> Histories
        {
            get { return _histories  ?? (_histories = CreateObjectSet<History>("Histories")); }
        }
        private ObjectSet<History> _histories;
    
        public ObjectSet<Log> Logs
        {
            get { return _logs  ?? (_logs = CreateObjectSet<Log>("Logs")); }
        }
        private ObjectSet<Log> _logs;
    
        public ObjectSet<Message> Messages
        {
            get { return _messages  ?? (_messages = CreateObjectSet<Message>("Messages")); }
        }
        private ObjectSet<Message> _messages;
    
        public ObjectSet<UserApplicationStatu> UserApplicationStatus
        {
            get { return _userApplicationStatus  ?? (_userApplicationStatus = CreateObjectSet<UserApplicationStatu>("UserApplicationStatus")); }
        }
        private ObjectSet<UserApplicationStatu> _userApplicationStatus;
    
        public ObjectSet<Culture> Cultures
        {
            get { return _cultures  ?? (_cultures = CreateObjectSet<Culture>("Cultures")); }
        }
        private ObjectSet<Culture> _cultures;
    
        public ObjectSet<NegotiationOrganization> NegotiationOrganizations
        {
            get { return _negotiationOrganizations  ?? (_negotiationOrganizations = CreateObjectSet<NegotiationOrganization>("NegotiationOrganizations")); }
        }
        private ObjectSet<NegotiationOrganization> _negotiationOrganizations;
    
        public ObjectSet<Organization> Organizations
        {
            get { return _organizations  ?? (_organizations = CreateObjectSet<Organization>("Organizations")); }
        }
        private ObjectSet<Organization> _organizations;
    
        public ObjectSet<OrganizationType> OrganizationTypes
        {
            get { return _organizationTypes  ?? (_organizationTypes = CreateObjectSet<OrganizationType>("OrganizationTypes")); }
        }
        private ObjectSet<OrganizationType> _organizationTypes;
    
        public ObjectSet<UserOrganization> UserOrganizations
        {
            get { return _userOrganizations  ?? (_userOrganizations = CreateObjectSet<UserOrganization>("UserOrganizations")); }
        }
        private ObjectSet<UserOrganization> _userOrganizations;
    
        public ObjectSet<UserOperation> UserOperations
        {
            get { return _userOperations  ?? (_userOperations = CreateObjectSet<UserOperation>("UserOperations")); }
        }
        private ObjectSet<UserOperation> _userOperations;

        #endregion
        #region Function Imports
        public ObjectResult<User> UserResetByUserID(Nullable<System.Guid> userID)
        {
    
            ObjectParameter userIDParameter;
    
            if (userID.HasValue)
            {
                userIDParameter = new ObjectParameter("UserID", userID);
            }
            else
            {
                userIDParameter = new ObjectParameter("UserID", typeof(System.Guid));
            }
            return base.ExecuteFunction<User>("UserResetByUserID", userIDParameter);
        }
        public ObjectResult<GetRightsByUserId_Result> GetRightsByUserId(Nullable<System.Guid> userID)
        {
    
            ObjectParameter userIDParameter;
    
            if (userID.HasValue)
            {
                userIDParameter = new ObjectParameter("UserID", userID);
            }
            else
            {
                userIDParameter = new ObjectParameter("UserID", typeof(System.Guid));
            }
            return base.ExecuteFunction<GetRightsByUserId_Result>("GetRightsByUserId", userIDParameter);
        }
        public ObjectResult<User> MakeUserOffline(Nullable<System.Guid> userID)
        {
    
            ObjectParameter userIDParameter;
    
            if (userID.HasValue)
            {
                userIDParameter = new ObjectParameter("UserID", userID);
            }
            else
            {
                userIDParameter = new ObjectParameter("UserID", typeof(System.Guid));
            }
            return base.ExecuteFunction<User>("MakeUserOffline", userIDParameter);
        }
        public ObjectResult<User> MakeUserOnline(Nullable<System.Guid> userID, string iPAddress)
        {
    
            ObjectParameter userIDParameter;
    
            if (userID.HasValue)
            {
                userIDParameter = new ObjectParameter("UserID", userID);
            }
            else
            {
                userIDParameter = new ObjectParameter("UserID", typeof(System.Guid));
            }
    
            ObjectParameter iPAddressParameter;
    
            if (iPAddress != null)
            {
                iPAddressParameter = new ObjectParameter("IPAddress", iPAddress);
            }
            else
            {
                iPAddressParameter = new ObjectParameter("IPAddress", typeof(string));
            }
            return base.ExecuteFunction<User>("MakeUserOnline", userIDParameter, iPAddressParameter);
        }
        public ObjectResult<User> GetUserByOperationString(string operationString, Nullable<byte> operationType)
        {
    
            ObjectParameter operationStringParameter;
    
            if (operationString != null)
            {
                operationStringParameter = new ObjectParameter("OperationString", operationString);
            }
            else
            {
                operationStringParameter = new ObjectParameter("OperationString", typeof(string));
            }
    
            ObjectParameter operationTypeParameter;
    
            if (operationType.HasValue)
            {
                operationTypeParameter = new ObjectParameter("OperationType", operationType);
            }
            else
            {
                operationTypeParameter = new ObjectParameter("OperationType", typeof(byte));
            }
            return base.ExecuteFunction<User>("GetUserByOperationString", operationStringParameter, operationTypeParameter);
        }
        public ObjectResult<User> UpdateUserByConfirmMail(string operationString, Nullable<byte> type)
        {
    
            ObjectParameter operationStringParameter;
    
            if (operationString != null)
            {
                operationStringParameter = new ObjectParameter("OperationString", operationString);
            }
            else
            {
                operationStringParameter = new ObjectParameter("OperationString", typeof(string));
            }
    
            ObjectParameter typeParameter;
    
            if (type.HasValue)
            {
                typeParameter = new ObjectParameter("Type", type);
            }
            else
            {
                typeParameter = new ObjectParameter("Type", typeof(byte));
            }
            return base.ExecuteFunction<User>("UpdateUserByConfirmMail", operationStringParameter, typeParameter);
        }
        public ObjectResult<User> DeleteUserOperationByUserID(Nullable<System.Guid> userID)
        {
    
            ObjectParameter userIDParameter;
    
            if (userID.HasValue)
            {
                userIDParameter = new ObjectParameter("UserID", userID);
            }
            else
            {
                userIDParameter = new ObjectParameter("UserID", typeof(System.Guid));
            }
            return base.ExecuteFunction<User>("DeleteUserOperationByUserID", userIDParameter);
        }
        public ObjectResult<User> UserCanLogin(string emailAddress)
        {
    
            ObjectParameter emailAddressParameter;
    
            if (emailAddress != null)
            {
                emailAddressParameter = new ObjectParameter("EmailAddress", emailAddress);
            }
            else
            {
                emailAddressParameter = new ObjectParameter("EmailAddress", typeof(string));
            }
            return base.ExecuteFunction<User>("UserCanLogin", emailAddressParameter);
        }
        public ObjectResult<Role> GetRolesByUserID(Nullable<System.Guid> userID)
        {
    
            ObjectParameter userIDParameter;
    
            if (userID.HasValue)
            {
                userIDParameter = new ObjectParameter("UserID", userID);
            }
            else
            {
                userIDParameter = new ObjectParameter("UserID", typeof(System.Guid));
            }
            return base.ExecuteFunction<Role>("GetRolesByUserID", userIDParameter);
        }
        public ObjectResult<User> FindUser(string key, Nullable<System.Guid> userID, Nullable<bool> isForPublicProfile)
        {
    
            ObjectParameter keyParameter;
    
            if (key != null)
            {
                keyParameter = new ObjectParameter("key", key);
            }
            else
            {
                keyParameter = new ObjectParameter("key", typeof(string));
            }
    
            ObjectParameter userIDParameter;
    
            if (userID.HasValue)
            {
                userIDParameter = new ObjectParameter("UserID", userID);
            }
            else
            {
                userIDParameter = new ObjectParameter("UserID", typeof(System.Guid));
            }
    
            ObjectParameter isForPublicProfileParameter;
    
            if (isForPublicProfile.HasValue)
            {
                isForPublicProfileParameter = new ObjectParameter("IsForPublicProfile", isForPublicProfile);
            }
            else
            {
                isForPublicProfileParameter = new ObjectParameter("IsForPublicProfile", typeof(bool));
            }
            return base.ExecuteFunction<User>("FindUser", keyParameter, userIDParameter, isForPublicProfileParameter);
        }
        public ObjectResult<Nullable<int>> FindUsersCount(string key, Nullable<System.Guid> userID, Nullable<bool> isForPublicProfile)
        {
    
            ObjectParameter keyParameter;
    
            if (key != null)
            {
                keyParameter = new ObjectParameter("key", key);
            }
            else
            {
                keyParameter = new ObjectParameter("key", typeof(string));
            }
    
            ObjectParameter userIDParameter;
    
            if (userID.HasValue)
            {
                userIDParameter = new ObjectParameter("UserID", userID);
            }
            else
            {
                userIDParameter = new ObjectParameter("UserID", typeof(System.Guid));
            }
    
            ObjectParameter isForPublicProfileParameter;
    
            if (isForPublicProfile.HasValue)
            {
                isForPublicProfileParameter = new ObjectParameter("IsForPublicProfile", isForPublicProfile);
            }
            else
            {
                isForPublicProfileParameter = new ObjectParameter("IsForPublicProfile", typeof(bool));
            }
            return base.ExecuteFunction<Nullable<int>>("FindUsersCount", keyParameter, userIDParameter, isForPublicProfileParameter);
        }
        public ObjectResult<Nullable<int>> CheckOnNegotiationRepeat(Nullable<System.Guid> negotiationID, string negotiationName, Nullable<System.Guid> userID)
        {
    
            ObjectParameter negotiationIDParameter;
    
            if (negotiationID.HasValue)
            {
                negotiationIDParameter = new ObjectParameter("NegotiationID", negotiationID);
            }
            else
            {
                negotiationIDParameter = new ObjectParameter("NegotiationID", typeof(System.Guid));
            }
    
            ObjectParameter negotiationNameParameter;
    
            if (negotiationName != null)
            {
                negotiationNameParameter = new ObjectParameter("NegotiationName", negotiationName);
            }
            else
            {
                negotiationNameParameter = new ObjectParameter("NegotiationName", typeof(string));
            }
    
            ObjectParameter userIDParameter;
    
            if (userID.HasValue)
            {
                userIDParameter = new ObjectParameter("UserID", userID);
            }
            else
            {
                userIDParameter = new ObjectParameter("UserID", typeof(System.Guid));
            }
            return base.ExecuteFunction<Nullable<int>>("CheckOnNegotiationRepeat", negotiationIDParameter, negotiationNameParameter, userIDParameter);
        }
        public ObjectResult<UserApplicationStatu> DataMatchingInAddonUpdate(string applicationTitle, Nullable<System.Guid> userID, Nullable<bool> isDMActive)
        {
    
            ObjectParameter applicationTitleParameter;
    
            if (applicationTitle != null)
            {
                applicationTitleParameter = new ObjectParameter("ApplicationTitle", applicationTitle);
            }
            else
            {
                applicationTitleParameter = new ObjectParameter("ApplicationTitle", typeof(string));
            }
    
            ObjectParameter userIDParameter;
    
            if (userID.HasValue)
            {
                userIDParameter = new ObjectParameter("UserID", userID);
            }
            else
            {
                userIDParameter = new ObjectParameter("UserID", typeof(System.Guid));
            }
    
            ObjectParameter isDMActiveParameter;
    
            if (isDMActive.HasValue)
            {
                isDMActiveParameter = new ObjectParameter("IsDMActive", isDMActive);
            }
            else
            {
                isDMActiveParameter = new ObjectParameter("IsDMActive", typeof(bool));
            }
            return base.ExecuteFunction<UserApplicationStatu>("DataMatchingInAddonUpdate", applicationTitleParameter, userIDParameter, isDMActiveParameter);
        }
        public ObjectResult<UserApplicationStatu> RetrieveApplicationDMStatus(string applicationTitle, Nullable<System.Guid> userID)
        {
    
            ObjectParameter applicationTitleParameter;
    
            if (applicationTitle != null)
            {
                applicationTitleParameter = new ObjectParameter("ApplicationTitle", applicationTitle);
            }
            else
            {
                applicationTitleParameter = new ObjectParameter("ApplicationTitle", typeof(string));
            }
    
            ObjectParameter userIDParameter;
    
            if (userID.HasValue)
            {
                userIDParameter = new ObjectParameter("UserID", userID);
            }
            else
            {
                userIDParameter = new ObjectParameter("UserID", typeof(System.Guid));
            }
            return base.ExecuteFunction<UserApplicationStatu>("RetrieveApplicationDMStatus", applicationTitleParameter, userIDParameter);
        }
        public ObjectResult<UserLeaveOrganizationResult> CanUserLeaveOrganization(Nullable<System.Guid> userID, Nullable<System.Guid> organizationID)
        {
    
            ObjectParameter userIDParameter;
    
            if (userID.HasValue)
            {
                userIDParameter = new ObjectParameter("UserID", userID);
            }
            else
            {
                userIDParameter = new ObjectParameter("UserID", typeof(System.Guid));
            }
    
            ObjectParameter organizationIDParameter;
    
            if (organizationID.HasValue)
            {
                organizationIDParameter = new ObjectParameter("OrganizationID", organizationID);
            }
            else
            {
                organizationIDParameter = new ObjectParameter("OrganizationID", typeof(System.Guid));
            }
            return base.ExecuteFunction<UserLeaveOrganizationResult>("CanUserLeaveOrganization", userIDParameter, organizationIDParameter);
        }
        public ObjectResult<User> GetUserByID(Nullable<System.Guid> userID)
        {
    
            ObjectParameter userIDParameter;
    
            if (userID.HasValue)
            {
                userIDParameter = new ObjectParameter("UserID", userID);
            }
            else
            {
                userIDParameter = new ObjectParameter("UserID", typeof(System.Guid));
            }
            return base.ExecuteFunction<User>("GetUserByID", userIDParameter);
        }
        public ObjectResult<UserProfileStatisticalsResult> GetUserProfileStatisticals(Nullable<System.Guid> userID)
        {
    
            ObjectParameter userIDParameter;
    
            if (userID.HasValue)
            {
                userIDParameter = new ObjectParameter("UserID", userID);
            }
            else
            {
                userIDParameter = new ObjectParameter("UserID", typeof(System.Guid));
            }
            return base.ExecuteFunction<UserProfileStatisticalsResult>("GetUserProfileStatisticals", userIDParameter);
        }
        public ObjectResult<Nullable<bool>> CanUserSeeMemberProfile(Nullable<System.Guid> currentUserID, Nullable<System.Guid> memberUserID)
        {
    
            ObjectParameter currentUserIDParameter;
    
            if (currentUserID.HasValue)
            {
                currentUserIDParameter = new ObjectParameter("CurrentUserID", currentUserID);
            }
            else
            {
                currentUserIDParameter = new ObjectParameter("CurrentUserID", typeof(System.Guid));
            }
    
            ObjectParameter memberUserIDParameter;
    
            if (memberUserID.HasValue)
            {
                memberUserIDParameter = new ObjectParameter("MemberUserID", memberUserID);
            }
            else
            {
                memberUserIDParameter = new ObjectParameter("MemberUserID", typeof(System.Guid));
            }
            return base.ExecuteFunction<Nullable<bool>>("CanUserSeeMemberProfile", currentUserIDParameter, memberUserIDParameter);
        }
        public ObjectResult<Negotiation> GetPublishedNegotiations(Nullable<byte> negOwner, Nullable<byte> negStatus, Nullable<int> archiveYear, Nullable<int> archiveMonth, Nullable<System.Guid> userID)
        {
    
            ObjectParameter negOwnerParameter;
    
            if (negOwner.HasValue)
            {
                negOwnerParameter = new ObjectParameter("NegOwner", negOwner);
            }
            else
            {
                negOwnerParameter = new ObjectParameter("NegOwner", typeof(byte));
            }
    
            ObjectParameter negStatusParameter;
    
            if (negStatus.HasValue)
            {
                negStatusParameter = new ObjectParameter("NegStatus", negStatus);
            }
            else
            {
                negStatusParameter = new ObjectParameter("NegStatus", typeof(byte));
            }
    
            ObjectParameter archiveYearParameter;
    
            if (archiveYear.HasValue)
            {
                archiveYearParameter = new ObjectParameter("ArchiveYear", archiveYear);
            }
            else
            {
                archiveYearParameter = new ObjectParameter("ArchiveYear", typeof(int));
            }
    
            ObjectParameter archiveMonthParameter;
    
            if (archiveMonth.HasValue)
            {
                archiveMonthParameter = new ObjectParameter("ArchiveMonth", archiveMonth);
            }
            else
            {
                archiveMonthParameter = new ObjectParameter("ArchiveMonth", typeof(int));
            }
    
            ObjectParameter userIDParameter;
    
            if (userID.HasValue)
            {
                userIDParameter = new ObjectParameter("UserID", userID);
            }
            else
            {
                userIDParameter = new ObjectParameter("UserID", typeof(System.Guid));
            }
            return base.ExecuteFunction<Negotiation>("GetPublishedNegotiations", negOwnerParameter, negStatusParameter, archiveYearParameter, archiveMonthParameter, userIDParameter);
        }
        public ObjectResult<NegotiationArchive> GetPublishedNegotiationArchive(Nullable<byte> negOwner, Nullable<byte> negStatus, Nullable<System.Guid> userID)
        {
    
            ObjectParameter negOwnerParameter;
    
            if (negOwner.HasValue)
            {
                negOwnerParameter = new ObjectParameter("NegOwner", negOwner);
            }
            else
            {
                negOwnerParameter = new ObjectParameter("NegOwner", typeof(byte));
            }
    
            ObjectParameter negStatusParameter;
    
            if (negStatus.HasValue)
            {
                negStatusParameter = new ObjectParameter("NegStatus", negStatus);
            }
            else
            {
                negStatusParameter = new ObjectParameter("NegStatus", typeof(byte));
            }
    
            ObjectParameter userIDParameter;
    
            if (userID.HasValue)
            {
                userIDParameter = new ObjectParameter("UserID", userID);
            }
            else
            {
                userIDParameter = new ObjectParameter("UserID", typeof(System.Guid));
            }
            return base.ExecuteFunction<NegotiationArchive>("GetPublishedNegotiationArchive", negOwnerParameter, negStatusParameter, userIDParameter);
        }
        public ObjectResult<NegotiationArchive> GetClosedNegotiationArchive(Nullable<System.Guid> userID)
        {
    
            ObjectParameter userIDParameter;
    
            if (userID.HasValue)
            {
                userIDParameter = new ObjectParameter("UserID", userID);
            }
            else
            {
                userIDParameter = new ObjectParameter("UserID", typeof(System.Guid));
            }
            return base.ExecuteFunction<NegotiationArchive>("GetClosedNegotiationArchive", userIDParameter);
        }
        public ObjectResult<Negotiation> GetClosedNegotiationByArchive(Nullable<int> archiveYear, Nullable<int> archiveMonth, Nullable<System.Guid> userID)
        {
    
            ObjectParameter archiveYearParameter;
    
            if (archiveYear.HasValue)
            {
                archiveYearParameter = new ObjectParameter("ArchiveYear", archiveYear);
            }
            else
            {
                archiveYearParameter = new ObjectParameter("ArchiveYear", typeof(int));
            }
    
            ObjectParameter archiveMonthParameter;
    
            if (archiveMonth.HasValue)
            {
                archiveMonthParameter = new ObjectParameter("ArchiveMonth", archiveMonth);
            }
            else
            {
                archiveMonthParameter = new ObjectParameter("ArchiveMonth", typeof(int));
            }
    
            ObjectParameter userIDParameter;
    
            if (userID.HasValue)
            {
                userIDParameter = new ObjectParameter("UserID", userID);
            }
            else
            {
                userIDParameter = new ObjectParameter("UserID", typeof(System.Guid));
            }
            return base.ExecuteFunction<Negotiation>("GetClosedNegotiationByArchive", archiveYearParameter, archiveMonthParameter, userIDParameter);
        }

        #endregion
    }
}