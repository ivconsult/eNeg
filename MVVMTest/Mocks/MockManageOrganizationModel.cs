
#region → Usings   .
using System;
using System.ComponentModel;
//using System.ComponentModel.Composition;
using System.Linq;
using System.ServiceModel.DomainServices.Client;
using citPOINT.eNeg.Common;
using citPOINT.eNeg.Data.Web;
//using citPOINT.eNeg.Model.Helper;
using System.Collections.Generic;

#endregion

#region → History  .

/* Date         User            Change
 * 
 * 04.09.11     yousra reda     •creation
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
    /// <summary>
    /// Model Layer for Managing Organization .
    /// </summary>
    #region  Using MEF to export ManageOrganizationModel
    //[Export(typeof(IManageOrganizationModel))]
    //[PartCreationPolicy(CreationPolicy.NonShared)]
    #endregion
    public class MockManageOrganizationModel : IManageOrganizationModel
    {

        #region → Fields         .
        private eNegContext mNegContext;
        private MailHelper mMailHelper;
        private Boolean mHasChanges = false;
        private Boolean mIsBusy = false;


        private List<OrganizationType> mOrganizationTypeSource;
        private List<Organization> mOrganizationSource;
        private List<User> mUserSource;
        private List<UserOperation> mUserOperationSource;
        private List<UserOrganization> mUserOrganizationSource;
        #endregion

        #region → Properties     .

        #region → Sample Mock   .

        /// <summary>
        /// Gets the OrganizationType source.
        /// </summary>
        /// <value>The OrganizationType source.</value>
        public List<OrganizationType> OrganizationTypeSource
        {
            get
            {
                if (mOrganizationTypeSource == null)
                {
                    mOrganizationTypeSource = new List<OrganizationType>();
                    mOrganizationTypeSource.Add(new OrganizationType()
                    {
                        OrganizationTypeID = 1,
                        OrganizationTypeName = @"Sole Proprietorship - EU",
                    });
                    mOrganizationTypeSource.Add(new OrganizationType()
                    {
                        OrganizationTypeID = 2,
                        OrganizationTypeName = @"Partnership - OG",
                    });
                    mOrganizationTypeSource.Add(new OrganizationType()
                    {
                        OrganizationTypeID = 3,
                        OrganizationTypeName = @"LP - KG",
                    });
                    mOrganizationTypeSource.Add(new OrganizationType()
                    {
                        OrganizationTypeID = 4,
                        OrganizationTypeName = @"LLC/LTD – GmbH",
                    });
                    mOrganizationTypeSource.Add(new OrganizationType()
                    {
                        OrganizationTypeID = 5,
                        OrganizationTypeName = @"PLC – AG",
                    });
                    mOrganizationTypeSource.Add(new OrganizationType()
                    {
                        OrganizationTypeID = 6,
                        OrganizationTypeName = @"NGO - NGO",
                    });
                } return mOrganizationTypeSource;
            }
        }

        /// <summary>
        /// Gets the Organization source.
        /// </summary>
        /// <value>The Organization source.</value>
        public List<Organization> OrganizationSource
        {
            get
            {
                if (mOrganizationSource == null)
                {
                    mOrganizationSource = new List<Organization>();
                    mOrganizationSource.Add(new Organization()
                    {
                        OrganizationID = Guid.Parse("7ffd027f-a11f-4c98-80a7-291b7f4a2baf"),
                        OrganizationName = @"EST Co.",
                        OrganizationTypeID = 3,
                        Deleted = false,
                        DeletedBy = Guid.Parse("f6287a1f-ecd3-4762-b970-4f3a4a491398"),
                        DeletedOn = new DateTime(2011, 9, 5, 15, 4, 46),
                    });
                    mOrganizationSource.Add(new Organization()
                    {
                        OrganizationID = Guid.Parse("6a1f0aa1-d9f1-430c-842b-f07fa932b8ad"),
                        OrganizationName = @"Google Co.",
                        OrganizationTypeID = 2,
                        Deleted = false,
                        DeletedBy = Guid.Parse("b8021430-1baf-4262-bb7d-d472380aab1f"),
                        DeletedOn = new DateTime(2011, 9, 6, 15, 11, 39),
                    });
                } return mOrganizationSource;
            }
        }

        /// <summary>
        /// Gets the User source.
        /// </summary>
        /// <value>The User source.</value>
        public List<User> UserSource
        {
            get
            {
                if (mUserSource == null)
                {
                    mUserSource = new List<User>();
                    mUserSource.Add(new User()
                    {
                        UserID = Guid.Parse("f6287a1f-ecd3-4762-b970-4f3a4a491398"),
                        EmailAddress = @"mohamedenew@hotmail.com",
                        Locked = false,
                        LockedDate = null,
                        LastLoginDate = new DateTime(2011, 9, 6, 15, 10, 47),
                        CreateDate = new DateTime(2011, 9, 5, 15, 4, 7),
                        AccountTypeID = null,
                        IPAddress = @"127.0.0.1",
                        SecurityQuestionID = null,
                        AnswerHash = @"",
                        AnswerSalt = @"",
                        Online = false,
                        Disabled = false,
                        FirstName = @"mohamed",
                        LastName = @"Enew",
                        CompanyName = @"",
                        LCID = null,
                        Address = @"",
                        City = @"",
                        CountryID = null,
                        Phone = @"",
                        Mobile = @"",
                        Gender = false,
                        Reset = false,
                        CultureID = 1,
                        HasPublicProfile = true,
                        ProfileDescription = null,
                    });
                    mUserSource.Add(new User()
                    {
                        UserID = Guid.Parse("f7cca750-9f3d-4095-9a94-926e7da2b658"),
                        EmailAddress = @"test@test.com",
                        Locked = false,
                        LockedDate = null,
                        LastLoginDate = new DateTime(2011, 9, 5, 15, 3, 29),
                        CreateDate = new DateTime(2011, 9, 5, 15, 3, 29),
                        AccountTypeID = null,
                        IPAddress = @"127.0.0.1",
                        SecurityQuestionID = null,
                        AnswerHash = @"",
                        AnswerSalt = @"",
                        Online = false,
                        Disabled = false,
                        FirstName = @"",
                        LastName = @"",
                        CompanyName = @"",
                        LCID = null,
                        Address = @"",
                        City = @"",
                        CountryID = null,
                        Phone = @"",
                        Mobile = @"",
                        Gender = false,
                        Reset = false,
                        CultureID = null,
                        HasPublicProfile = false,
                        ProfileDescription = null,
                    });
                    mUserSource.Add(new User()
                    {
                        UserID = Guid.Parse("b8021430-1baf-4262-bb7d-d472380aab1f"),
                        EmailAddress = @"mohamedenew@yahoo.com",
                        Locked = false,
                        LockedDate = null,
                        LastLoginDate = new DateTime(2011, 9, 6, 15, 11, 18),
                        CreateDate = new DateTime(2011, 9, 5, 15, 5, 55),
                        AccountTypeID = null,
                        IPAddress = @"127.0.0.1",
                        SecurityQuestionID = null,
                        AnswerHash = @"",
                        AnswerSalt = @"",
                        Online = false,
                        Disabled = false,
                        FirstName = @"Mohamed",
                        LastName = @"Yahoo",
                        CompanyName = @"",
                        LCID = null,
                        Address = @"",
                        City = @"",
                        CountryID = null,
                        Phone = @"",
                        Mobile = @"",
                        Gender = false,
                        Reset = false,
                        CultureID = null,
                        HasPublicProfile = false,
                        ProfileDescription = null,
                    });
                    mUserSource.Add(new User()
                    {
                        UserID = Guid.Parse("8a81d072-61aa-4c46-8bdd-e9b019e47def"),
                        EmailAddress = @"mohammedenew@gmail.com",
                        Locked = false,
                        LockedDate = null,
                        LastLoginDate = new DateTime(2011, 9, 6, 15, 12, 27),
                        CreateDate = new DateTime(2011, 9, 5, 15, 5, 15),
                        AccountTypeID = null,
                        IPAddress = @"127.0.0.1",
                        SecurityQuestionID = null,
                        AnswerHash = @"",
                        AnswerSalt = @"",
                        Online = true,
                        Disabled = false,
                        FirstName = @"Mohamed",
                        LastName = @"Ali",
                        CompanyName = @"",
                        LCID = null,
                        Address = @"",
                        City = @"",
                        CountryID = null,
                        Phone = @"",
                        Mobile = @"",
                        Gender = false,
                        Reset = false,
                        CultureID = null,
                        HasPublicProfile = false,
                        ProfileDescription = null,
                    });
                } return mUserSource;
            }
        }

        /// <summary>
        /// Gets the UserOperation source.
        /// </summary>
        /// <value>The UserOperation source.</value>
        public List<UserOperation> UserOperationSource
        {
            get
            {
                if (mUserOperationSource == null)
                {
                    mUserOperationSource = new List<UserOperation>();
                } return mUserOperationSource;
            }
        }

        /// <summary>
        /// Gets the UserOrganization source.
        /// </summary>
        /// <value>The UserOrganization source.</value>
        public List<UserOrganization> UserOrganizationSource
        {
            get
            {
                if (mUserOrganizationSource == null)
                {
                    mUserOrganizationSource = new List<UserOrganization>();
                    //mUserOrganizationSource.Add(new UserOrganization()
                    //{
                    //    UserOrganizationID = Guid.Parse("4cbb01f3-ed86-4ad3-8250-a480004a114b"),
                    //    UserID = Guid.Parse("8a81d072-61aa-4c46-8bdd-e9b019e47def"),
                    //    OrganizationID = Guid.Parse("6a1f0aa1-d9f1-430c-842b-f07fa932b8ad"),
                    //    MemberStatus = 0,
                    //    Deleted = false,
                    //    DeletedBy = Guid.Parse("8a81d072-61aa-4c46-8bdd-e9b019e47def"),
                    //    DeletedOn = new DateTime(2011, 9, 6, 15, 12, 38),
                    //});
                    mUserOrganizationSource.Add(new UserOrganization()
                    {
                        UserOrganizationID = Guid.Parse("139c4f93-2483-4e06-9933-c4847e8d4af6"),
                        UserID = Guid.Parse("8a81d072-61aa-4c46-8bdd-e9b019e47def"),
                        OrganizationID = Guid.Parse("7ffd027f-a11f-4c98-80a7-291b7f4a2baf"),
                        MemberStatus = 0,
                        Deleted = false,
                        DeletedBy = Guid.Parse("8a81d072-61aa-4c46-8bdd-e9b019e47def"),
                        DeletedOn = new DateTime(2011, 9, 5, 15, 5, 40),
                    });
                    mUserOrganizationSource.Add(new UserOrganization()
                    {
                        UserOrganizationID = Guid.Parse("7af056f3-227d-400a-b15a-d0cd7f03ec39"),
                        UserID = Guid.Parse("f6287a1f-ecd3-4762-b970-4f3a4a491398"),
                        OrganizationID = Guid.Parse("7ffd027f-a11f-4c98-80a7-291b7f4a2baf"),
                        MemberStatus = 3,
                        Deleted = false,
                        DeletedBy = Guid.Parse("f6287a1f-ecd3-4762-b970-4f3a4a491398"),
                        DeletedOn = new DateTime(2011, 9, 5, 15, 4, 46),
                    });
                    mUserOrganizationSource.Add(new UserOrganization()
                    {
                        UserOrganizationID = Guid.Parse("6dbd791c-7fb7-4571-88ad-d9e679c670ba"),
                        UserID = Guid.Parse("b8021430-1baf-4262-bb7d-d472380aab1f"),
                        OrganizationID = Guid.Parse("7ffd027f-a11f-4c98-80a7-291b7f4a2baf"),
                        MemberStatus = 0,
                        Deleted = false,
                        DeletedBy = Guid.Parse("b8021430-1baf-4262-bb7d-d472380aab1f"),
                        DeletedOn = new DateTime(2011, 9, 5, 15, 6, 18),
                    });
                    mUserOrganizationSource.Add(new UserOrganization()
                    {
                        UserOrganizationID = Guid.Parse("c3794df7-b572-4ee9-afdc-eef1eaf13f63"),
                        UserID = Guid.Parse("b8021430-1baf-4262-bb7d-d472380aab1f"),
                        OrganizationID = Guid.Parse("6a1f0aa1-d9f1-430c-842b-f07fa932b8ad"),
                        MemberStatus = 3,
                        Deleted = false,
                        DeletedBy = Guid.Parse("b8021430-1baf-4262-bb7d-d472380aab1f"),
                        DeletedOn = new DateTime(2011, 9, 6, 15, 11, 39),
                    });
                } return mUserOrganizationSource;
            }
        }

        #endregion

        /// <summary>
        /// Property with getter only Used to send mail to user when he request login
        /// </summary>
        public MailHelper MailHelper
        {
            get
            {
                if (mMailHelper == null)
                {

                    mMailHelper = new MailHelper();
                    mMailHelper.MailSendComplete += new Action<InvokeOperation>(SendingMailCompleted);
                }

                return mMailHelper;
            }
        }

        /// <summary>
        /// Protocted property with a getter only to used to update user login and logout data in eNeg Database 
        /// </summary>
        public eNegContext Context
        {
            get
            {
                if (mNegContext == null)
                {
                    if (mNegContext == null)
                    {
                        if (mNegContext == null)
                        {
                            mNegContext = new eNegContext(new Uri("http://localhost:9000/citPOINT-eNeg-Data-Web-eNegService.svc", UriKind.Absolute));
                        }
                    }
                    mNegContext.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(ctx_PropertyChanged);
                }

                return mNegContext;
            }
        }

        /// <summary>
        /// True if _ctx.HasChanges is true; otherwise, false
        /// </summary>
        public Boolean HasChanges
        {
            get
            {
                return this.mHasChanges;
            }

            private set
            {
                if (this.mHasChanges != value)
                {
                    this.mHasChanges = value;
                    this.OnPropertyChanged("HasChanges");
                }
            }
        }

        /// <summary>
        /// True if either "IsLoading" or "IsSubmitting" is
        /// in progress; otherwise, false
        /// </summary>
        public Boolean IsBusy
        {
            get
            {
                return this.mIsBusy;
            }

            private set
            {
                if (this.mIsBusy != value)
                {
                    this.mIsBusy = value;
                    this.OnPropertyChanged("IsBusy");
                }
            }
        }

        #endregion

        #region → Constructor    .

        public MockManageOrganizationModel()
        {
            this.ManageTablesRelations();
        }

        #endregion

        #region → Event Handlers .

        /// <summary>
        /// Private Event Handler that called after any change in 
        /// HasChanges, IsLoading, IsSubmitting properties
        /// </summary>
        /// <param name="sender">Value of Sender</param>
        /// <param name="e">Value of e</param>
        private void ctx_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "HasChanges":
                    this.HasChanges = mNegContext.HasChanges;
                    break;
                case "IsLoading":
                    this.IsBusy = mNegContext.IsLoading;
                    break;
                case "IsSubmitting":
                    this.IsBusy = mNegContext.IsSubmitting;
                    break;
            }
        }
        #endregion

        #region → Events         .
        /// <summary>
        /// Occurs when [get organization by ID complete].
        /// </summary>
        public event EventHandler<eNegEntityResultArgs<Organization>> GetOrganizationByIDComplete;

        /// <summary>
        /// Occurs when [get organization members complete].
        /// </summary>
        public event EventHandler<eNegEntityResultArgs<User>> GetOrganizationMembersComplete;

        /// <summary>
        /// Occurs when [get organization members status complete].
        /// </summary>
        public event EventHandler<eNegEntityResultArgs<UserOrganization>> GetOrganizationMembersStatusComplete;

        /// <summary>
        /// Event Handler For Method SendingMail
        /// </summary>
        public event Action<InvokeOperation> SendingMailCompleted;

        /// <summary>
        /// Occurs when [save changes complete].
        /// </summary>
        public event EventHandler<SubmitOperationEventArgs> SaveChangesComplete;

        /// <summary>
        /// Event Handler For Method PropertyChanged
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region → Methods        .

        #region → Private        .

        /// <summary>
        /// Private Method used to perform query on certain entity of eNeg Entities
        /// </summary>
        /// <typeparam name="T">Value Of T</typeparam>
        /// <param name="qry">Value Of qry</param>
        /// <param name="evt">Value Of evt</param>
        private void PerformQuery<T>(EntityQuery<T> qry, EventHandler<eNegEntityResultArgs<T>> evt) where T : Entity
        {
            Context.Load<T>(qry, LoadBehavior.RefreshCurrent, r =>
            {
                if (evt != null)
                {
                    try
                    {
                        if (r.HasError)
                        {
                            evt(this, new eNegEntityResultArgs<T>(r.Error));
                            r.MarkErrorAsHandled();
                        }
                        else
                        {
                            evt(this, new eNegEntityResultArgs<T>(r.Entities));
                        }
                    }
                    catch (Exception ex)
                    {
                        evt(this, new eNegEntityResultArgs<T>(ex));
                    }
                }
            }, null);
        }

        /// <summary>
        /// Manages the tables relations.
        /// </summary>
        private void ManageTablesRelations()
        {

            #region → OrganizationType                                  .

            foreach (var OrganizationTypeItem in this.OrganizationTypeSource)
            {
                //Organization
                foreach (var OrganizationItem in this.OrganizationSource.Where(ss => ss.OrganizationTypeID == OrganizationTypeItem.OrganizationTypeID))
                {
                    OrganizationTypeItem.Organizations.Add(OrganizationItem);
                    OrganizationItem.OrganizationType = OrganizationTypeItem;
                }
            }

            #endregion

            #region → Organization                                      .

            foreach (var OrganizationItem in this.OrganizationSource)
            {
                //UserOrganization
                foreach (var UserOrganizationItem in this.UserOrganizationSource.Where(ss => ss.OrganizationID == OrganizationItem.OrganizationID))
                {
                    OrganizationItem.UserOrganizations.Add(UserOrganizationItem);
                    UserOrganizationItem.Organization = OrganizationItem;
                }


            }

            #endregion

            #region → User                                              .

            foreach (var UserItem in this.UserSource)
            {



                //UserOperation
                foreach (var UserOperationItem in this.UserOperationSource.Where(ss => ss.UserID == UserItem.UserID))
                {
                    UserItem.UserOperations.Add(UserOperationItem);
                    UserOperationItem.User = UserItem;
                }



                //UserOrganization
                foreach (var UserOrganizationItem in this.UserOrganizationSource.Where(ss => ss.UserID == UserItem.UserID))
                {
                    UserItem.UserOrganizations.Add(UserOrganizationItem);
                    UserOrganizationItem.User = UserItem;
                }



            }

            #endregion



        }

        #endregion Private

        #region → Protected      .

        #region INotifyPropertyChanged Interface implementation
        /// <summary>
        /// Handle changes in any Property
        /// </summary>
        /// <param name="propertyName">Property Name</param>
        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion INotifyPropertyChanged Interface implementation

        #endregion Protected

        #region → Public         .

        #region IManageOrganizationModel Interface implementation

        /// <summary>
        /// Gets the organization by ID.
        /// </summary>
        /// <param name="organizationID">The organization ID.</param>
        public void GetOrganizationByID()
        {
            if (GetOrganizationByIDComplete != null)
            {
                GetOrganizationByIDComplete(this, new eNegEntityResultArgs<Organization>(this.OrganizationSource));
            }
        }

        /// <summary>
        /// Gets the organization members async.
        /// </summary>
        /// <param name="organizationID">The organization ID.</param>
        public void GetOrganizationMembersAsync()
        {
            if (GetOrganizationMembersComplete != null)
            {
                GetOrganizationMembersComplete(this, new eNegEntityResultArgs<User>(this.UserSource));
            }
        }

        /// <summary>
        /// Gets the organization members status async.
        /// </summary>
        public void GetOrganizationMembersStatusAsync()
        {
            if (GetOrganizationMembersStatusComplete != null)
            {

                List<UserOrganization> result = this.UserOrganizationSource
                                                .Where(ss => ss.OrganizationID == AppConfigurations.CurrentLoginUser.OrganizationOwnedID ||
                                                             ss.MemberStatus == eNegConstant.OrganizationMemberStatus.PendingMember ||
                                                             ss.MemberStatus == eNegConstant.OrganizationMemberStatus.Owner).ToList();

                this.GetOrganizationMembersStatusComplete(this, new eNegEntityResultArgs<UserOrganization>(result));
            }
        }

        /// <summary>
        /// Removes the user organization.
        /// </summary>
        /// <param name="userOrganization">The user organization.</param>
        public void RemoveUserOrganization(UserOrganization userOrganization)
        {
            this.UserOrganizationSource.Remove(userOrganization);
        }

        /// <summary>
        /// Reject all changes happen on current Context
        /// </summary>
        public void RejectChanges()
        {
            // this.Context.RejectChanges();
        }

        /// <summary>
        /// Apply All Changes in current Context to database.
        /// </summary>
        public void SaveChangesAsync()
        {
            if (SaveChangesComplete != null)
            {
                SaveChangesComplete(this, new SubmitOperationEventArgs(null, null));
            }
        }
        #endregion

        #endregion

        #endregion

    }
}
