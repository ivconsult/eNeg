#region → Usings   .



#endregion

#region → History  .
/*
        User            change              Date
 *      Dergham         Update strcuture    11.08.10
 
 */

#endregion

#region → ToDos    .
#endregion

namespace citPOINT.eNeg.Common
{
    /// <summary>
    /// Class that contain all available Views
    /// </summary>
    public sealed class ViewTypes
    {
        #region → Fields         .

        #region Constants

        /// <summary>
        /// Get value of Addon View
        /// </summary>
        public const string AddonView = "Add-on";

        /// <summary>
        /// Get Main Platform View.
        /// </summary>
        public const string MainPlatformView = "MainPlatformView";
        
        /// <summary>
        /// Get value of Login View
        /// </summary>
        public const string LoginView = "Login";

        /// <summary>
        /// Get value of Full Register View
        /// </summary>
        public const string FullRegisterView = "FullRegister";

        /// <summary>
        /// Get value of Quick Register View
        /// </summary>
        public const string QuickRegisterView = "QuickRegister";

        /// <summary>
        /// Get value of Forget Login View
        /// </summary>
        public const string ForgetLoginView = "ForgotLogin?";

        /// <summary>
        /// Get value of Reset Request View
        /// </summary>
        public const string ResetRequestView = "ResetRequest";

        /// <summary>
        /// Get value of Reset View
        /// </summary>
        public const string ResetView = "Reset";

        /// <summary>
        /// Get value of Confirm Mail View
        /// </summary>
        public const string ConfirmMailView = "ConfirmMail";

        /// <summary>
        /// Get value of Accept To Be Owner View
        /// </summary>
        public const string AcceptToBeOwnerView = "AcceptToBeOwner";

        /// <summary>
        /// Get value of Refuse To Be Owner View
        /// </summary>
        public const string RefuseToBeOwnerView = "RefuseToBeOwner";

        /// <summary>
        /// Get value of Accept To Be Owner Remove Original View
        /// </summary>
        public const string AcceptToBeOwner_RemoveOriginalView = "AcceptToBeOwner_RemoveOriginal";

        /// <summary>
        /// Get value of Main Negotiation View
        /// </summary>
        public const string MainNegotiationView = "MainNegotiationView";

        /// <summary>
        /// Get value of LoadUser View
        /// </summary>
        public const string LoadUser = "LoadUser";

        /// <summary>
        /// Get value of MyProfile View
        /// </summary>
        public const string MyProfile = "MyProfile";

        /// <summary>
        /// Get value of Conversation Details View
        /// </summary>
        public const string ConversationDetailsView = "ConversationDetailsView";
        
        /// <summary>
        /// Conversation Details For Published View.
        /// </summary>
        public const string ConversationDetailsForPublishedView = "ConversationDetailsForPublishedView";
        
        /// <summary>
        /// Get value of Negotiation view
        /// </summary>
        public const string NegotiationsView = "NegotiationView";

        /// <summary>
        /// Get Value of Users view
        /// </summary>
        public const string UsersOverview = "UsersOverview";

        /// <summary>
        ///  Get Value of User Details
        /// </summary>
        public const string UserDetails = "UserDetails";

        /// <summary>
        /// Get Value of Messages In Addon 
        /// </summary>
        public const string MessagesInAddon = "MessagesInAddon";

        /// <summary>
        /// Close Popup View
        /// </summary>
        public const string ClosePopupView = "ClosePopupView";

        /// <summary>
        /// Add another Receivers
        /// </summary>
        public const string AddMultiReceivers = "AddMultiReceivers";

        /// <summary>
        /// Remove a Receiver from Set of Receivers
        /// </summary>
        public const string RemoveMultiReceivers = "RemoveMultiReceivers";

        /// <summary>
        /// Profile Details
        /// </summary>
        public const string ProfileDetails = "ProfileDetails";


        /// <summary>
        /// Organization Management
        /// </summary>
        public const string OrganizationManagement = "OrganizationManagement";

        /// <summary>
        /// Published Profile
        /// </summary>
        public const string PublishedProfiles = "PublishedProfiles";

        /// <summary>
        /// Published Negotiations
        /// </summary>
        public const string PublishedNegotiations = "PublishedNegotiations";
        
        #region → Set of view names used in navigation in LoginViewModel

        /// <summary>
        /// Get value of navigate quick register view in case of Addon Out of Browser. 
        /// </summary>
        public const string NavigateQuickRegisterView = "NavigateQuickRegister";

        /// <summary>
        /// Get value of navigate forget login view in case of Addon Out of Browser. 
        /// </summary>
        public const string NavigateForgetLoginView = "NavigateForgetLogin";

        /// <summary>
        ///  Get value of navigate full register view in case of Addon Out of Browser. 
        /// </summary>
        public const string NavigateFullRegisterView = "NavigateFullRegister";

        #endregion
        
        #region → Popup for Negotiation and Conversation .

        /// <summary>
        /// Rename Negotiation and Conversation Popup
        /// </summary>
        public const string RenameNegotiationConversationPopup = "RenameNegotiationConversationPopup";
        
        /// <summary>
        /// Application Settings Popup.
        /// </summary>
        public const string ApplicationSettingsPopup = "ApplicationSettingsPopup";
        
        /// <summary>
        /// Negoitation Details View
        /// </summary>
        public const string NegoitationDetailsView = "NegoitationDetailsView";

        /// <summary>
        /// Negotiation Details For Published View
        /// </summary>
        public const string NegotiationDetailsForPublishedView = "NegotiationDetailsForPublishedView";

        /// <summary>
        /// Add New Negotiation View
        /// </summary>
        public const string AddNewNegotiationView = "AddNewNegotiationView";
        
        /// <summary>
        /// Add New Conversation View 
        /// </summary>
        public const string AddNewConversationView = "AddNewConversationView";
        #endregion


        /// <summary>
        /// Inner View Change User Password
        /// In User Profile.
        /// </summary>
        public const string InnerViewChangeUserPassword = "InnerViewChangeUserPassword";
        
        /// <summary>
        /// Inner View Change User Email.
        /// </summary>
        public const string InnerViewChangeUserEmail = "InnerViewChangeUserEmail";

        /// <summary>
        /// Inner View Change User Contacts Details
        /// </summary>
        public const string InnerViewChangeUserContactsDetails = "InnerViewChangeUserContactsDetails";

        #endregion

        #endregion
    }
}