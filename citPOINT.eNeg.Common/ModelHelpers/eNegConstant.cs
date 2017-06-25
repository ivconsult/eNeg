
#region → Usings   .

using System;

#endregion

#region → History  .

/* Date         User
 * 
 * 16.08.10     Mohamed Abdulwahab
 */

# endregion

#region → ToDos    .

/*
 * Date         set by User     Description
 * 
 * 

*/

# endregion

namespace citPOINT.eNeg.Common
{
    /// <summary>
    /// Constant for All Tables (Lockup Tables)
    /// </summary>
    public static class eNegConstant
    {
        #region → Roles Constant Admin-User        .
        /// <summary>
        /// For User Roles
        /// </summary>
        public class Roles
        {
            #region → Fields         .

            private static Guid mAdmin;
            private static Guid mUser;

            #endregion  Fields

            #region → Properties     .

            /// <summary>
            /// Admin User (GUID FOR Admin Role)
            /// </summary>
            public static Guid Admin
            {
                get
                {
                    if (mAdmin == Guid.Empty)
                        mAdmin = new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa");

                    return mAdmin;
                }

            }

            /// <summary>
            /// User Role Type (GUID User Role)
            /// </summary>
            public static Guid User
            {
                get
                {
                    if (mUser == Guid.Empty)
                        mUser = new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaab");

                    return mUser;
                }

            }

            #endregion  Properties

        }

        #endregion Roles Constant Admin-User

        #region  → Negotiation Status Open-Closed   .

        /// <summary>
        /// For Negotiation Status
        /// </summary>
        public class NegotiationStatus
        {
            #region → Fields         .
            private static Guid mOpen;
            private static Guid mClosed;
            #endregion  Fields

            #region → Properties     .
            /// <summary>
            /// for Ongoing Negotiation Status Type
            /// </summary>
            public static Guid Ongoing
            {
                get
                {
                    if (mOpen == Guid.Empty)
                        mOpen = new Guid("e3b0b130-133e-4c1d-957c-14c67869446c");

                    return mOpen;
                }

            }

            /// <summary>
            /// for Closed Negotiation Status Type
            /// </summary>
            public static Guid Closed
            {
                get
                {
                    if (mClosed == Guid.Empty)
                        mClosed = new Guid("dfcea50d-18a2-4e41-9be8-1673e88101c4");

                    return mClosed;
                }

            }

            #endregion  Properties
        }

        #endregion Negotiation Status Open-Closed

        #region  → UserOperations Constant          .
        /// <summary>
        /// For UserOperations
        /// </summary>
        public class UserOperations
        {
            #region → Fields         .

            private static byte mConfiramtion = 0;
            private static byte mResetRequest = 1;
            private static byte mAcceptOwnerRequest = 2;
            private static byte mRefuseOwnerRequest = 3;
            private static byte mAccept_DeleteOwnerRequest = 4;

            #endregion  Fields

            #region → Properties     .

            /// <summary>
            /// Mean The Type  Of Message Is Confirmation Message
            /// </summary>
            public static byte Confiramtion
            {
                get
                {
                    return mConfiramtion;
                }
            }

            /// <summary>
            /// Mean The Type OF Message Is Reset Request
            /// </summary>
            public static byte ResetRequest
            {
                get
                {
                    return mResetRequest;
                }
            }

            /// <summary>
            /// Organization Member Accept to be Owner.
            /// </summary>
            /// <value>The accept owner request.</value>
            public static byte AcceptOwnerRequest
            {
                get
                {
                    return mAcceptOwnerRequest;
                }
            }

            /// <summary>
            /// Organization member Refused to be owner .
            /// </summary>
            /// <value>The refuse owner request.</value>
            public static byte RefuseOwnerRequest
            {
                get
                {
                    return mRefuseOwnerRequest;
                }
            }

            /// <summary>
            /// Organization Member Accept to be Owner and 
            /// the actual owner will be deleted form the organization.
            /// </summary>
            /// <value>The accept_ delete owner request.</value>
            public static byte Accept_DeleteOwnerRequest
            {
                get
                {
                    return mAccept_DeleteOwnerRequest;
                }
            }

            #endregion Properties
        }

        #endregion UserOperations Constant

        #region  → Channel Constant                 .

        /// <summary>
        /// For Channels
        /// </summary>
        public class Channel
        {
            #region → Fields         .

            private static Guid mEmail;

            #endregion Fields

            #region → Properties     .

            /// <summary>
            /// Email Channel Type
            /// </summary>
            public static Guid Email
            {
                get
                {

                    if (mEmail == Guid.Empty)
                        mEmail = new Guid("e3f62f4f-0050-4dd2-a92b-ddb6fbecc041");

                    return mEmail;
                }
            }

            #endregion Properties

        }

        #endregion Channel Constant

        #region → Channel Type Constant            .

        /// <summary>
        /// Channel Type
        /// </summary>
        public class ChannelType
        {
            #region → Fields         .

            private static Guid mAppsChannels;

            #endregion Fields

            #region → Properties     .

            /// <summary>
            /// Gets the apps channels.
            /// </summary>
            /// <value>The apps channels.</value>
            public static Guid AppsChannels
            {
                get
                {

                    if (mAppsChannels == Guid.Empty)
                        mAppsChannels = new Guid("01862435-6783-43D3-B454-2DF645EB2875");

                    return mAppsChannels;
                }
            }

            #endregion Properties

        }
        #endregion Channel Type Constant

        #region → Action Parameter Type            .

        /// <summary>
        /// Action Type Parameter Values.
        /// </summary>
        public static class ActionParameterType
        {
            /// <summary>
            /// Display mean open the node in selected mode only
            /// </summary>
            public static string Display = "Display";
            /// <summary>
            /// Edit mean open the node in Editable mode.
            /// </summary>
            public static string Edit = "Edit";
            /// <summary>
            /// Conversation Details mean open the Conversation Messages in web platform.
            /// </summary>
            public static string ConversationDetails = "ConversationDetails";
            /// <summary>
            /// AddNewMessage mean Add new Message in add-on.
            /// </summary>
            public static string AddNewMessage = "AddNewMessage";

            /// <summary>
            /// Data Match a Message in third-Party App
            /// </summary>
            public static string DataMatching = "DataMatching";

            /// <summary>
            /// App. Settings
            /// </summary>
            public static string AppSettings = "AppSettings";

            /// <summary>
            /// Report Views.
            /// </summary>
            public static string Report = "Report";

            
        }
        #endregion

        #region  → Organization Member Status       .

        /// <summary>
        /// Organization Member Status
        /// </summary>
        public class OrganizationMemberStatus
        {
            #region → Properties     .

            /// <summary>
            /// Gets the pending member.
            /// </summary>
            /// <value>The pending member.</value>
            public const int PendingMember = 0;

            /// <summary>
            /// Gets the member.
            /// </summary>
            /// <value>The member.</value>
            public const int Member = 1;

            /// <summary>
            /// Gets the pending owner.
            /// </summary>
            /// <value>The pending owner.</value>
            public const int PendingOwner = 2;

            /// <summary>
            /// Gets the owner.
            /// </summary>
            /// <value>The owner.</value>
            public const int Owner = 3;

            #endregion  Properties
        }

        #endregion Organization Member Status  .

        #region  → User Profile Statisticals        .

        /// <summary>
        /// Profile Statisticals
        /// </summary>
        public class UserProfileStatisticals
        {
            #region → Properties     .

            /// <summary>
            /// Get Negotiation Count.
            /// </summary>
            public const string NegotiationCount = "Negotiation";

            /// <summary>
            /// Get Conversation Count.
            /// </summary>
            public const string ConversationCount = "Conversation";

            /// <summary>
            /// Get Application Avg. Count.
            /// </summary>
            public const string Application_Avg_Count = "Apps.Avg";

            #endregion  Properties
        }

        #endregion User Profile Statisticals        .

        #region → PublishedNegotiationFilters      .

        /// <summary>
        /// Negotiation Status Filter types
        /// </summary>
        public enum NegotiationStatusFilter
        {
            /// <summary>
            /// For both Ongoing & Closed Negotiations
            /// </summary>
            All = 0,
            /// <summary>
            /// For Ongoing Negotiations
            /// </summary>
            Ongoing = 1,
            /// <summary>
            /// For Closed Negotiations
            /// </summary>
            Closed = 2
        }

        /// <summary>
        /// Negotiation Owner Filter
        /// </summary>
        public enum NegotiationOwnerFilter
        {
            /// <summary>
            /// For both My & Others Negotiations 
            /// </summary>
            All = 0,
            /// <summary>
            /// For My Negotiation
            /// </summary>
            MyNegotiations = 1,
            /// <summary>
            /// For Others Negotiation
            /// </summary>
            OthersNegotiatons = 2
        }
        #endregion Enum
    }
}
