#region → Usings   .
using citPOINT.eNeg.Common;
using citPOINT.eNeg.Data.Web;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.ServiceModel.DomainServices.Client;
using HashHelper = citPOINT.eNeg.Common.HashHelper;
using citPOINT.eNeg.Common;
using System.Collections.Generic;
using System.Windows;
using GalaSoft.MvvmLight.Messaging;

#endregion

#region → History  .

/* Date         User            Change
 * 
 * 16.08.11     Y.Mohammed     • creation
 */

# endregion

#region → ToDos    .

/*
 * Date         set by User     Description
 * 
 * 
*/

# endregion

namespace citPOINT.eNeg.ViewModel
{
    #region → Using  MEF to export ManageUserOrganizationViewModel
    /// <summary>
    /// this class is to Manage User Organization View Model.
    /// </summary>
    [Export(ViewModelTypes.ManageUserOrganizationViewModel)]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    #endregion
    public class ManageUserOrganizationViewModel : ViewModelBase
    {
        #region → Fields         .

        private List<QueueMail> mQueueMails;

        private IManageUserOrganizationModel mManageUserOrganizationModel;
        private User mCurrentUser;
        private Organization mSelectedOrganization;
        private bool mIsBusy;
        private UserOrganization mLastLeavedUserOrganiztion;
        private Organization mNewOrganization;

        private IEnumerable<OrganizationType> mOrganizationTypeEntries;
        private List<Organization> mOrganizationEntries;
        private List<UserOrganization> mUserOrganizationSource;
        private IEnumerable<Organization> mFilterdOrganizationSource;

        private RelayCommand mSubmitChangeCommand = null;
        private RelayCommand mCancelChangeCommand = null;
        private RelayCommand mJoinOrganizationCommand;
        private RelayCommand mAddNewOrganizationCommand;
        private RelayCommand<UserOrganization> mLeaveOrganizationCommand;
        private RelayCommand mSubmitAddingNewOrganization;
        private RelayCommand mCancelAddingNewOrganizationCommand;


        private int mQueueMailsCount;
        private bool mIsUserJoinedToOrganization;

        #endregion

        #region → Properties     .

        /// <summary>
        /// Gets or sets a value indicating whether this instance is for register page.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is for register page; otherwise, <c>false</c>.
        /// </value>
        public bool IsForRegisterPage { get; set; }

        /// <summary>
        /// Indicating That The Current User Is In Progress
        /// </summary>
        public bool IsBusy
        {
            get
            {
                return mIsBusy;
            }
            set
            {
                mIsBusy = value;
                this.RaisePropertyChanged("IsBusy");
            }
        }

        /// <summary>
        /// The Value Of the Current User
        /// </summary>
        public User CurrentUser
        {
            get { return mCurrentUser; }
            set
            {
                if (mCurrentUser != value)
                {
                    mCurrentUser = value;
                    this.RaisePropertyChanged("CurrentUser");

                    GetUserOrganizationForUserAsync();
                }
            }
        }

        /// <summary>
        /// Gets or sets the queue mails count.
        /// </summary>
        /// <value>The queue mails count.</value>
        public int QueueMailsCount
        {
            get { return mQueueMailsCount; }
            set { mQueueMailsCount = value; }
        }

        /// <summary>
        /// Gets or sets the selected organization [in combobox of Organizations].
        /// </summary>
        /// <value>The selected organization.</value>
        public Organization SelectedOrganization
        {
            get
            {
                return mSelectedOrganization;
            }
            set
            {
                mSelectedOrganization = value;
                this.RaisePropertyChanged("SelectedOrganization");
                this.RaiseCanExecuteChanged();
            }
        }

        /// <summary>
        /// Gets or sets the new organization.
        /// </summary>
        /// <value>The new organization.</value>
        public Organization NewOrganization
        {
            get
            {
                return mNewOrganization;
            }
            set
            {
                mNewOrganization = value;
                this.RaisePropertyChanged("NewOrganization");
            }
        }

        /// <summary>
        /// Gets or sets the organization type entries.
        /// </summary>
        /// <value>The organization type entries.</value>
        public IEnumerable<OrganizationType> OrganizationTypeEntries
        {
            get
            {
                return mOrganizationTypeEntries;
            }
            set
            {
                mOrganizationTypeEntries = value;
                this.RaisePropertyChanged("OrganizationTypeEntries");
            }
        }

        /// <summary>
        /// Gets or sets the organization entries.
        /// </summary>
        /// <value>The organization entries.</value>
        public List<Organization> OrganizationEntries
        {
            get
            {
                return mOrganizationEntries;
            }
            set
            {
                mOrganizationEntries = value;
                this.RaisePropertyChanged("OrganizationEntries");
                this.FilterOrganization();
            }
        }

        /// <summary>
        /// Gets or sets the filterd organization source.
        /// [exclude what selected before]
        /// </summary>
        /// <value>The filterd organization source.</value>
        public IEnumerable<Organization> FilterdOrganizationSource
        {
            get
            {
                return mFilterdOrganizationSource;
            }
            set
            {
                mFilterdOrganizationSource = value;
                this.RaisePropertyChanged("FilterdOrganizationSource");
            }
        }

        /// <summary>
        /// Gets or sets the user organization source.
        /// </summary>
        /// <value>The user organization source.</value>
        public List<UserOrganization> UserOrganizationSource
        {
            get
            {
                return mUserOrganizationSource;
            }
            set
            {
                mUserOrganizationSource = value;
                this.RaisePropertyChanged("UserOrganizationSource");
                if (mUserOrganizationSource.Count() > 0)
                {
                    IsUserJoinedToOrganization = true;
                }
                else
                {
                    IsUserJoinedToOrganization = false;
                }
                this.FilterOrganization();
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is user joined to organization.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is user joined to organization; otherwise, <c>false</c>.
        /// </value>
        public bool IsUserJoinedToOrganization
        {
            get
            {
                return mIsUserJoinedToOrganization;
            }
            set
            {
                if (mIsUserJoinedToOrganization != value)
                {
                    mIsUserJoinedToOrganization = value;
                    RaisePropertyChanged("IsUserJoinedToOrganization");
                }
            }
        }

        /// <summary>
        /// Gets or sets the queue mails.
        /// </summary>
        /// <value>The queue mails.</value>
        private List<QueueMail> QueueMails
        {
            get
            {
                return mQueueMails;
            }
            set
            {
                mQueueMails = value;
            }
        }

        #endregion Properties

        #region → Constructors   .

        /// <summary>
        /// Initializes a new instance of the <see cref="ManageUserOrganizationViewModel"/> class.
        /// </summary>
        /// <param name="ManageUserOrganizationModel">The manage user organization model.</param>
        [ImportingConstructor]
        public ManageUserOrganizationViewModel(IManageUserOrganizationModel ManageUserOrganizationModel)
        {
            mManageUserOrganizationModel = ManageUserOrganizationModel;

            #region → Set up event handling       .

            mManageUserOrganizationModel.PropertyChanged += new PropertyChangedEventHandler(mManageUserOrganizationModel_PropertyChanged);
            mManageUserOrganizationModel.SendingMailCompleted += new Action<InvokeOperation>(mManageUserOrganizationModel_SendingMailCompleted);

            mManageUserOrganizationModel.GetOrganizationTypeComplete += new EventHandler<eNegEntityResultArgs<OrganizationType>>(mManageUserOrganizationModel_GetOrganizationTypeComplete);
            mManageUserOrganizationModel.GetOrganizationComplete += new EventHandler<eNegEntityResultArgs<Organization>>(mManageUserOrganizationModel_GetOrganizationComplete);
            mManageUserOrganizationModel.GetUserOrganizationForUserComplete += new EventHandler<eNegEntityResultArgs<UserOrganization>>(mManageUserOrganizationModel_GetUserOrganizationForUserComplete);
            mManageUserOrganizationModel.CanUserLeaveOrganizationComplete += new EventHandler<eNegEntityResultArgs<UserLeaveOrganizationResult>>(mManageUserOrganizationModel_CanUserLeaveOrganizationComplete);

            mManageUserOrganizationModel.GetOrganizationsOwnersComplete += new EventHandler<eNegEntityResultArgs<User>>(mManageUserOrganizationModel_GetOrganizationsOwnersComplete);

            #endregion Set up event handling

            #region → Load Lookup tables          .

            this.OrganizationEntries = new List<Organization>();
            this.OrganizationTypeEntries = new List<OrganizationType>();
            this.UserOrganizationSource = new List<UserOrganization>();

            this.QueueMails = new List<QueueMail>();

            this.GetOrganizationTypeAsync();
            this.GetOrganizationAsync();

            #endregion

        }

        #endregion Constructor

        #region → Event Handlers .

        /// <summary>
        /// Ms the manage user organization model_ get user organization for user complete.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        private void mManageUserOrganizationModel_GetUserOrganizationForUserComplete(object sender, eNegEntityResultArgs<UserOrganization> e)
        {
            if (!e.HasError)
            {
                this.UserOrganizationSource = e.Results.ToList();
            }
            else
            {
                // notify user if there is any error
                eNegMessanger.RaiseErrorMessage.Send(e.Error);
            }
        }

        /// <summary>
        /// Ms the manage user organization model_ get organization type complete.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        private void mManageUserOrganizationModel_GetOrganizationTypeComplete(object sender, eNegEntityResultArgs<OrganizationType> e)
        {
            if (!e.HasError)
            {
                this.OrganizationTypeEntries = e.Results;
            }
            else
            {
                // notify user if there is any error
                eNegMessanger.RaiseErrorMessage.Send(e.Error);
            }
        }

        /// <summary>
        /// Ms the manage user organization model_ get organization complete.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        private void mManageUserOrganizationModel_GetOrganizationComplete(object sender, eNegEntityResultArgs<Organization> e)
        {
            if (!e.HasError)
            {
                this.OrganizationEntries = e.Results.ToList();
            }
            else
            {
                // notify user if there is any error
                eNegMessanger.RaiseErrorMessage.Send(e.Error);
            }
        }

        /// <summary>
        /// Can user leave organization complete.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        private void mManageUserOrganizationModel_CanUserLeaveOrganizationComplete(object sender, eNegEntityResultArgs<UserLeaveOrganizationResult> e)
        {
            if (!e.HasError)
            {
                if (e.Results.FirstOrDefault() != null)
                {
                    UserLeaveOrganizationResult returnResult = e.Results.FirstOrDefault();
                   
                    if (returnResult.CanLeave == false)
                    {
                        //send message for go to manage organization
                        #region Leave Message

                        Action<MessageBoxResult> callBackResult = null;

                        //Firsly ask user to confirm editing that item
                        DialogMessage leaveDialogMsg = new DialogMessage(this,
                            Resources.YouWantToLeaveOrg,
                            result => callBackResult(result))
                        {
                            Button = MessageBoxButton.OKCancel,
                            Caption = Resources.ConfirmMessageBoxCaption
                        };

                        eNegMessanger.ConfirmMessage.Send(leaveDialogMsg);

                        #endregion "Confirmation Message"

                        callBackResult = (result) =>
                        {
                            if (result == MessageBoxResult.Cancel)
                                return;

                            AppConfigurations.RemoveOriginalOwner = true;
                            eNegMessanger.ChangeScreenMessage.Send(ViewTypes.OrganizationManagement);
                        };
                    }
                    // thier are an other owners or this is normal member
                    else if (returnResult.CanLeave.Value && !returnResult.AlternativeOwnerID.HasValue)
                    {
                        this.RemoveUserOrganization(this.mLastLeavedUserOrganiztion, false);
                    }
                    //he is the only one in this organization so delete also this organization
                    else if (returnResult.CanLeave.Value && returnResult.AlternativeOwnerID.HasValue && returnResult.AlternativeOwnerID.Value == this.CurrentUser.UserID)
                    {
                        this.RemoveUserOrganization(this.mLastLeavedUserOrganiztion, true);
                    }
                    //thier are only one other member so he will be the Owner and send mail for him after save
                    else if (returnResult.CanLeave.Value && returnResult.AlternativeOwnerID.HasValue && returnResult.AlternativeOwnerID.Value != this.CurrentUser.UserID)
                    {
                        this.RemoveUserOrganization(this.mLastLeavedUserOrganiztion, false);

                        this.SetAlternativeOwnerForOrganization(returnResult.AlternativeOwnerID.Value, this.mLastLeavedUserOrganiztion.OrganizationID);

                        //Add to Queue mails list this mail
                        this.QueueMails.Add(
                                new QueueMail(this.mManageUserOrganizationModel.MailHelper,
                                              QueueMailType.AlternativeOwner,
                                              this.mLastLeavedUserOrganiztion.Organization,
                                              new User()
                                                   {
                                                       EmailAddress = returnResult.EmailAddress,
                                                       FirstName = returnResult.FirstName,
                                                       LastName = returnResult.LastName
                                                   },
                                              null));
                    }
                }
            }
            else
            {
                eNegMessanger.RaiseErrorMessage.Send(e.Error);
            }
        }

        /// <summary>
        /// Get organizations owners complete.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        private void mManageUserOrganizationModel_GetOrganizationsOwnersComplete(object sender, eNegEntityResultArgs<User> e)
        {
            if (!e.HasError)
            {
                foreach (var organizationOwnerItem in e.Results)
                {
                    this.QueueMails.Add(new QueueMail(this.mManageUserOrganizationModel.MailHelper, QueueMailType.RequestFromOwner, null, organizationOwnerItem, this.CurrentUser));
                }
            }
            else
            {
                eNegMessanger.RaiseErrorMessage.Send(e.Error);
            }

            this.SendQueuedMails();
        }

        /// <summary>
        /// After Sending the Confirmation Mail
        /// </summary>
        /// <param name="e">Value e</param>
        private void mManageUserOrganizationModel_SendingMailCompleted(InvokeOperation e)
        {
            if (!e.HasError)
            {
                IsBusy = false;

                QueueMailsCount--;

                #region → Show Message              .

                if (QueueMailsCount <= 0)
                {
                    eNegMessage Message = new eNegMessage(Resources.OrganizationMailSuccess);

                    Message.ShowMessageCompleted += (s, args) =>
                    {
                        if (this.IsForRegisterPage)
                        {
                            eNegMessanger.ChangeScreenMessage.Send(ViewTypes.LoginView);
                        }
                    };

                    eNegMessanger.SendCustomMessage.Send(Message);
                }
                #endregion
            }
            else
            {
                e.MarkErrorAsHandled();
                eNegMessanger.RaiseErrorMessage.Send(e.Error);
            }
        }

        /// <summary>
        /// Indicating That thier is any changes in the Current Data.
        /// </summary>
        /// <param name="sender">Value Of Sender</param>
        /// <param name="e">Value Of e</param>
        private void mManageUserOrganizationModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (!this.mManageUserOrganizationModel.IsBusy)
            {
                FilterOrganization();
            }

            RaiseCanExecuteChanged();
        }

        #endregion Event Handlers

        #region → Commands       .

        /// <summary>
        /// Gets the join organization command.
        /// </summary>
        /// <value>The join organization command.</value>
        public RelayCommand JoinOrganizationCommand
        {
            get
            {
                if (mJoinOrganizationCommand == null)
                {
                    mJoinOrganizationCommand = new RelayCommand(() =>
                    {
                        try
                        {
                            if (!mManageUserOrganizationModel.IsBusy)
                            {
                                UserOrganization userOrganization = AddNewUserOrganization(this.CurrentUser, this.SelectedOrganization, eNegConstant.OrganizationMemberStatus.PendingMember, true);

                                List<UserOrganization> tmp = this.UserOrganizationSource.ToList();

                                tmp.Add(userOrganization);

                                this.UserOrganizationSource = tmp;

                                eNegMessanger.EditUserOrganizationMessage.Send(userOrganization);
                            }
                        }
                        catch (Exception ex)
                        {
                            // notify user if there is any error
                            eNegMessanger.RaiseErrorMessage.Send(ex);
                        }
                    }
                    , () => !this.IsBusy && this.SelectedOrganization != null);
                }
                return mJoinOrganizationCommand;
            }
        }

        /// <summary>
        /// Gets the add new organization command.
        /// </summary>
        /// <value>The add new organization command.</value>
        public RelayCommand AddNewOrganizationCommand
        {
            get
            {
                if (mAddNewOrganizationCommand == null)
                {
                    mAddNewOrganizationCommand = new RelayCommand(() =>
                    {
                        try
                        {
                            if (!mManageUserOrganizationModel.IsBusy)
                            {
                                this.NewOrganization = new Organization();
                                eNegMessanger.NewPopUp.Send("New Organization", eNegMessanger.PopUpType.AddNewOrganization);
                            }
                        }
                        catch (Exception ex)
                        {
                            // notify user if there is any error
                            eNegMessanger.RaiseErrorMessage.Send(ex);
                        }
                    }
                    , () => !this.IsBusy);
                }
                return mAddNewOrganizationCommand;
            }
        }

        /// <summary>
        /// Gets the submit adding new organization.
        /// </summary>
        /// <value>The submit adding new organization.</value>
        public RelayCommand SubmitAddingNewOrganization
        {
            get
            {
                if (mSubmitAddingNewOrganization == null)
                {
                    mSubmitAddingNewOrganization = new RelayCommand(() =>
                    {
                        try
                        {
                            if (!mManageUserOrganizationModel.IsBusy && this.NewOrganization != null)
                            {
                                #region → Check if User is Owner for another Organization .

                                //check Role Admin for any organization
                                if (this.UserOrganizationSource.Where(ss => ss.MemberStatus == eNegConstant.OrganizationMemberStatus.Owner && ss.UserID == this.CurrentUser.UserID).Count() > 0)
                                {
                                    Organization org = this.UserOrganizationSource.Where(ss => ss.MemberStatus == eNegConstant.OrganizationMemberStatus.Owner && ss.UserID == this.CurrentUser.UserID).FirstOrDefault().Organization;

                                    this.NewOrganization.ValidationErrors.Add(new ValidationResult(string.Format(Resources.OnlyOneOrganizationOwner, org.OrganizationName), new string[] { "OrganizationName" }));

                                    return;
                                }

                                #endregion

                                this.NewOrganization.TryValidateObject();

                                #region → Check  Name repeating                           .

                                //Check repeat in name.
                                if (!string.IsNullOrEmpty(this.NewOrganization.OrganizationName) &&
                                     this.OrganizationEntries.Where(ss => ss.OrganizationName.ToLower().Trim() == this.NewOrganization.OrganizationName.ToLower().Trim()).Count() > 0)
                                {
                                    this.NewOrganization.ValidationErrors.Add(new ValidationResult(Resources.OrganizationRepeat, new string[] { "OrganizationName" }));
                                }
                                #endregion

                                #region → Organization Type ID                            .

                                // Organization Type ID
                                if (this.NewOrganization.OrganizationTypeID == 0)
                                {
                                    this.NewOrganization.ValidationErrors.Add(new ValidationResult(Resources.ValidationErrorRequiredField, new string[] { "OrganizationTypeID" }));
                                }

                                #endregion


                                if (this.NewOrganization.ValidationErrors.Count() <= 0)
                                {

                                    #region → Cloning Virtual Object into Actual Context      .

                                    Organization organization = this.AddNewOrganization(this.CurrentUser, true);
                                    organization.OrganizationName = this.NewOrganization.OrganizationName;
                                    organization.OrganizationType = this.NewOrganization.OrganizationType;
                                    organization.OrganizationTypeID = this.NewOrganization.OrganizationTypeID;

                                    this.OrganizationEntries.Add(organization);

                                    this.UserOrganizationSource = new List<UserOrganization>(this.CurrentUser.UserOrganizations);

                                    this.NewOrganization = null;

                                    #endregion

                                    eNegMessanger.FlippMessage.Send(ViewTypes.ClosePopupView);
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            // notify user if there is any error
                            eNegMessanger.RaiseErrorMessage.Send(ex);
                        }
                    }
                    , () => !this.IsBusy && this.NewOrganization != null);
                }
                return mSubmitAddingNewOrganization;
            }
        }

        /// <summary>
        /// Gets the cancel adding new organization command.
        /// </summary>
        /// <value>The cancel adding new organization command.</value>
        public RelayCommand CancelAddingNewOrganizationCommand
        {
            get
            {
                if (mCancelAddingNewOrganizationCommand == null)
                {
                    mCancelAddingNewOrganizationCommand = new RelayCommand(() =>
                    {
                        try
                        {
                            if (!mManageUserOrganizationModel.IsBusy)
                            {
                                this.NewOrganization = null;
                                eNegMessanger.FlippMessage.Send(ViewTypes.ClosePopupView);
                            }
                        }
                        catch (Exception ex)
                        {
                            // notify user if there is any error
                            eNegMessanger.RaiseErrorMessage.Send(ex);
                        }
                    }
                    , () => true);
                }
                return mCancelAddingNewOrganizationCommand;
            }
        }

        /// <summary>
        /// Gets the leave organization command.
        /// </summary>
        /// <value>The leave organization command.</value>
        public RelayCommand<UserOrganization> LeaveOrganizationCommand
        {
            get
            {
                if (mLeaveOrganizationCommand == null)
                {
                    mLeaveOrganizationCommand = new RelayCommand<UserOrganization>((userOrganization) =>
                    {
                        try
                        {
                            if (!mManageUserOrganizationModel.IsBusy && userOrganization != null && userOrganization.Organization != null)
                            {
                                // New record just added
                                if (userOrganization.EntityState == EntityState.New)
                                {
                                    this.RemoveUserOrganization(userOrganization);
                                }
                                else
                                {
                                    this.mLastLeavedUserOrganiztion = userOrganization;
                                    this.CanUserLeaveOrganization(userOrganization.UserID, userOrganization.OrganizationID);
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            // notify user if there is any error
                            eNegMessanger.RaiseErrorMessage.Send(ex);
                        }
                    }
                    , (userOrganization) => !this.IsBusy && userOrganization != null && userOrganization.Organization != null);
                }
                return mLeaveOrganizationCommand;
            }
        }

        #endregion Commands

        #region → Methods        .

        #region → Private        .

        /// <summary>
        /// To change the status Enable property for buttons
        /// </summary>
        private void RaiseCanExecuteChanged()
        {
            this.IsBusy = this.mManageUserOrganizationModel.IsBusy;


            JoinOrganizationCommand.RaiseCanExecuteChanged();
            AddNewOrganizationCommand.RaiseCanExecuteChanged();
        }

        /// <summary>
        /// Filters the organization.
        /// [Exlude joined org.]
        /// </summary>
        private void FilterOrganization()
        {
            if (this.OrganizationEntries != null)
            {
                if (this.IsForRegisterPage)
                {
                    this.FilterdOrganizationSource = this.OrganizationEntries
                                                         .Where(ss => ss.UserOrganizations.Count() == 0);
                }
                else if (this.CurrentUser != null)
                {
                    this.FilterdOrganizationSource = this.OrganizationEntries
                                                         .Where(ss => ss.UserOrganizations.Where(dd => dd.UserID == this.CurrentUser.UserID).Count() == 0);
                }

            }
        }

        #endregion

        #region → Public         .

        /// <summary>
        /// Gets the organization type async.
        /// </summary>
        public void GetOrganizationTypeAsync()
        {
            this.mManageUserOrganizationModel.GetOrganizationTypeAsync();
        }

        /// <summary>
        /// Gets the organization async.
        /// </summary>
        void GetOrganizationAsync()
        {
            this.mManageUserOrganizationModel.GetOrganizationAsync();
        }

        /// <summary>
        /// Gets the user organizations status bridge table for user async.
        /// </summary>
        public void GetUserOrganizationForUserAsync()
        {
            if (this.CurrentUser != null && this.CurrentUser.EntityState != EntityState.New)
            {
                this.mManageUserOrganizationModel.GetUserOrganizationForUserAsync(this.CurrentUser.UserID);
            }
        }

        /// <summary>
        /// Determines whether this user can leave this Organization or not
        /// </summary>
        /// <param name="userID">The user ID.</param>
        /// <param name="organizationID">The organization ID.</param>
        public void CanUserLeaveOrganization(Guid userID, Guid organizationID)
        {
            this.mManageUserOrganizationModel.CanUserLeaveOrganization(userID, organizationID);
        }

        /// <summary>
        /// Gets the organizations owners
        /// For sending mails for them.
        /// </summary>
        /// <param name="organizationIDs">The organization Ids.</param>
        public void GetOrganizationsOwners(Guid[] organizationIDs)
        {
            this.mManageUserOrganizationModel.GetOrganizationsOwners(organizationIDs);
        }

        /// <summary>
        /// Sets the alternative owner for organization.
        /// </summary>
        /// <param name="alternativeOwnerID">The alternative owner ID.</param>
        /// <param name="organizationID">The organization ID.</param>
        public void SetAlternativeOwnerForOrganization(Guid alternativeOwnerID, Guid organizationID)
        {
            this.mManageUserOrganizationModel.SetAlternativeOwnerForOrganization(alternativeOwnerID, organizationID);
        }


        /// <summary>
        /// User Cancelling changes via Calling CancelChangesMessage so It call
        /// OnCancelChangesMessage Method.
        /// </summary>
        public void RejectChanges()
        {
            this.mManageUserOrganizationModel.RejectChanges();
            if (this.mManageUserOrganizationModel.Context != null)
            {
                this.OrganizationEntries = this.mManageUserOrganizationModel.Context.Organizations.ToList();
                this.FilterdOrganizationSource = this.mManageUserOrganizationModel.Context.Organizations.ToList();
                this.UserOrganizationSource = this.mManageUserOrganizationModel.Context.UserOrganizations.ToList();
            }


            this.FilterOrganization();
        }

        #region → Preparing a List of mails and send them   .

        /// <summary>
        /// Prepaires the queue mails.
        /// </summary>
        /// <param name="AddedUserOrganization">The added user organization.</param>
        public void PrepairQueueMails(List<Entity> AddedUserOrganization)
        {
            List<Guid> lstOrgs = new List<Guid>();

            #region → Collecting a List of Organization whcich is added New .

            if (AddedUserOrganization != null)
            {
                foreach (var item in AddedUserOrganization)
                {
                    UserOrganization userOrg = (item as UserOrganization);

                    if (userOrg != null &&
                        userOrg.UserID == this.CurrentUser.UserID &&
                        userOrg.MemberStatus == eNegConstant.OrganizationMemberStatus.PendingMember)
                    {
                        lstOrgs.Add(userOrg.OrganizationID);
                    }
                }
            }
            #endregion

            //in case he join new organization
            if (lstOrgs.Count() > 0)
            {
                this.GetOrganizationsOwners(lstOrgs.ToArray());
            }
            else//in case of alternative mails
            {
                SendQueuedMails();
            }

            #region → My profile and current user may be be owner or not (so visibilty of menu).

            if (AppConfigurations.CurrentLoginUser != null &&
                AppConfigurations.CurrentLoginUser.UserID == this.CurrentUser.UserID)
            {
                AppConfigurations.CurrentLoginUser.IsOrganizationOwner = this.UserOrganizationSource.Where(ss => ss.MemberStatus == eNegConstant.OrganizationMemberStatus.Owner).Count() > 0;
                if (AppConfigurations.CurrentLoginUser.IsOrganizationOwner)
                {
                    AppConfigurations.CurrentLoginUser.OrganizationOwnedID = this.UserOrganizationSource.Where(ss => ss.MemberStatus == eNegConstant.OrganizationMemberStatus.Owner).First().OrganizationID;
                }
            }

            #endregion


        }

        /// <summary>
        /// Sends the queued mails.
        /// </summary>
        private void SendQueuedMails()
        {
            QueueMailsCount = QueueMails.Count;
            foreach (var queueMailsItem in this.QueueMails)
            {
                queueMailsItem.Send();
            }
            QueueMails.Clear();
        }

        #endregion

        /// <summary>
        /// public method that add Organization to the current context
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="SetInContext">Add in Current Context</param>
        /// <returns>New Instance of Organization</returns>
        public Organization AddNewOrganization(User user, bool SetInContext)
        {
            return this.mManageUserOrganizationModel.AddNewOrganization(this.CurrentUser, true);
        }

        /// <summary>
        /// Adds the new user organization.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="organziation">The organziation.</param>
        /// <param name="memberStatus">The member status.[Member,Owner]</param>
        /// <param name="SetInContext">if set to <c>true</c> [set in context].</param>
        /// <returns>new Instance of User Organization</returns>
        public UserOrganization AddNewUserOrganization(User user, Organization organziation, byte memberStatus, bool SetInContext)
        {
            return this.mManageUserOrganizationModel.AddNewUserOrganization(user, organziation, memberStatus, true);
        }

        /// <summary>
        /// Removes the user organization.
        /// </summary>
        /// <param name="userOrganization">The user organization.</param>
        /// <param name="alsoRemoveOrganization">if set to <c>true</c> [also remove organization].</param>
        public void RemoveUserOrganization(UserOrganization userOrganization, bool alsoRemoveOrganization = false)
        {
            #region → Remove Organization if it is New One .

            if (userOrganization.Organization.EntityState == EntityState.New || alsoRemoveOrganization)
            {
                Organization tmpOrganization = userOrganization.Organization;

                this.OrganizationEntries.Remove(tmpOrganization);
                this.RaisePropertyChanged("OrganizationEntries");

                this.mManageUserOrganizationModel.RemoveOrganization(tmpOrganization);
            }

            #endregion

            this.mManageUserOrganizationModel.RemoveUserOrganization(userOrganization);
            this.UserOrganizationSource.Remove(userOrganization);
            this.UserOrganizationSource = new List<UserOrganization>(this.UserOrganizationSource);
        }

        #region

        /// <summary>
        /// Clean All Memory And unregister all events
        /// Dispose
        /// </summary>
        public override void Cleanup()
        {
            // unregister all events

            mManageUserOrganizationModel.PropertyChanged -= new PropertyChangedEventHandler(mManageUserOrganizationModel_PropertyChanged);
            mManageUserOrganizationModel.SendingMailCompleted -= new Action<InvokeOperation>(mManageUserOrganizationModel_SendingMailCompleted);

            ////Cleanup

            // unregister any messages for this ViewModel
            base.Cleanup();
        }
        #endregion ICleanup interface implementation

        #endregion Public

        #endregion Methods
    }
}