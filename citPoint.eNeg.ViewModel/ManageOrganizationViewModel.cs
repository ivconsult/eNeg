#region → Usings   .
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.ServiceModel.DomainServices.Client;
using System.Windows;
using citPOINT.eNeg.Common;
using citPOINT.eNeg.Data.Web;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
#endregion

#region → History  .

/* Date         User         Change
 * 
 * 05.09.11     M.Wahab     • creation
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
    #region  Using MEF to export Manage Organization View Model
    /// <summary>
    /// This Class Do all Operations Related To Manage Organization View Model.
    /// And Sending Mail to the Users.
    /// </summary>
    [Export(ViewModelTypes.ManageOrganizationViewModel)]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    #endregion
    public class ManageOrganizationViewModel : ViewModelBase
    {

        #region → Fields         .

        private IManageOrganizationModel mManageOrganizationModel;

        private List<User> mOrganizationMembers;
        private List<UserOrganization> mOrganizationMembersStatus;
        private List<User> mListOfRemovedUsers;

        private bool mIsBusy;
        private int noOfSentMails = 0;
        private int queueMailsCount = 0;

        private bool mIsUsersSourceEmpty = false;

        private List<QueueMail> mQueueMails;
        private User mOrganizationOwner;

        private Organization mCurrentOrganization;

        private RelayCommand mSubmitChangeCommand = null;
        private RelayCommand mCancelChangeCommand = null;
        private RelayCommand<UserOrganization> mRemoveUserFromOrganizationCommand;

        #endregion

        #region → Properties     .

        /// <summary>
        /// Gets the organization ID.
        /// </summary>
        /// <value>The organization ID.</value>
        private Guid OrganizationID
        {
            get
            {
                return AppConfigurations.CurrentLoginUser.OrganizationOwnedID;
            }
        }

        /// <summary>
        /// Gets or sets the list of removed users.
        /// </summary>
        /// <value>The list of removed users.</value>
        private List<User> ListOfRemovedUsers
        {
            get { return mListOfRemovedUsers; }
            set { mListOfRemovedUsers = value; }
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

        /// <summary>
        /// Gets a value indicating whether this instance is for choose another owner.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is for choose another owner; otherwise, <c>false</c>.
        /// </value>
        public bool IsForChooseAnotherOwner
        {
            get
            {
                return AppConfigurations.RemoveOriginalOwner;
            }
        }

        /// <summary>
        /// Indicating That The Current User Is In Progress
        /// Saving Data.
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
        /// Gets or sets a value indicating whether this instance is user source empty.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is user source empty; otherwise, <c>false</c>.
        /// </value>
        public bool IsUsersSourceEmpty
        {
            get
            {
                return mIsUsersSourceEmpty;

            }
            set
            {
                mIsUsersSourceEmpty = value;
                this.RaisePropertyChanged("IsUsersSourceEmpty");
            }
        }


        #region → Lockup tables    .

        /// <summary>
        /// Gets or sets the organization members status.
        /// </summary>
        /// <value>The organization members status.</value>
        public List<UserOrganization> OrganizationMembersStatus
        {
            get
            {
                return mOrganizationMembersStatus;
            }
            set
            {
                mOrganizationMembersStatus = value;
                this.RaisePropertyChanged("OrganizationMembersStatus");
            }
        }

        /// <summary>
        /// Gets or sets the organization members.
        /// </summary>
        /// <value>The organization members.</value>
        public List<User> OrganizationMembers
        {
            get
            {
                return mOrganizationMembers;
            }
            set
            {
                mOrganizationMembers = value;
                this.RaisePropertyChanged("OrganizationMembers");
            }
        }

        /// <summary>
        /// Gets the organization owner.
        /// </summary>
        /// <value>The organization owner.</value>
        private User OrganizationOwner
        {
            get
            {
                if (mOrganizationOwner == null)
                {
                    mOrganizationOwner = new User()
                    {
                        FirstName = AppConfigurations.CurrentLoginUser.FirstName,
                        LastName = AppConfigurations.CurrentLoginUser.LastName,
                        EmailAddress = AppConfigurations.CurrentLoginUser.EmailAddress,
                        UserID = AppConfigurations.CurrentLoginUser.UserID,
                    };
                }
                return mOrganizationOwner;
            }
        }

        /// <summary>
        /// Gets or sets the current organization.
        /// </summary>
        /// <value>The current organization.</value>
        public Organization CurrentOrganization
        {
            get
            {
                return mCurrentOrganization;
            }
            set
            {
                mCurrentOrganization = value;
                this.RaisePropertyChanged("CurrentOrganization");
            }
        }

        #endregion

        #endregion

        #region → Constructors   .

        /// <summary>
        /// Initializes a new instance of the <see cref="ManageOrganizationViewModel"/> class.
        /// </summary>
        /// <param name="ManageOrganizationModel">The manage organization model.</param>
        [ImportingConstructor]
        public ManageOrganizationViewModel(IManageOrganizationModel ManageOrganizationModel)
        {
            this.mManageOrganizationModel = ManageOrganizationModel;

            if (this.mManageOrganizationModel.HasChanges)
                this.mManageOrganizationModel.RejectChanges();

            this.ListOfRemovedUsers = new List<User>();
            this.QueueMails = new List<QueueMail>();

            #region → Setup event handling   .

            this.mManageOrganizationModel.SaveChangesComplete += new EventHandler<SubmitOperationEventArgs>(mManageOrganizationModel_SaveChangesComplete);
            this.mManageOrganizationModel.PropertyChanged += new PropertyChangedEventHandler(mManageOrganizationModel_PropertyChanged);
            this.mManageOrganizationModel.GetOrganizationMembersComplete += new EventHandler<eNegEntityResultArgs<User>>(mManageOrganizationModel_GetOrganizationMembersComplete);
            this.mManageOrganizationModel.GetOrganizationMembersStatusComplete += new EventHandler<eNegEntityResultArgs<UserOrganization>>(mManageOrganizationModel_GetOrganizationMembersStatusComplete);
            this.mManageOrganizationModel.SendingMailCompleted += new Action<InvokeOperation>(mManageOrganizationModel_SendingMailCompleted);
            this.mManageOrganizationModel.GetOrganizationByIDComplete += new EventHandler<eNegEntityResultArgs<Organization>>(mManageOrganizationModel_GetOrganizationByIDComplete);

            #endregion

            #region → Loading Related Tables .
            GetOrganization();
            GetOrganizationMembersAsync();
            GetOrganizationMembersStatusAsync();
            #endregion

        }

        #endregion

        #region → Event Handlers .

        #region →  Loading Event Handlers .

        /// <summary>
        /// Call back og get organization members status.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The args.</param>
        private void mManageOrganizationModel_GetOrganizationMembersStatusComplete(object sender, eNegEntityResultArgs<UserOrganization> e)
        {
            if (!e.HasError)
            {
                this.OrganizationMembersStatus = e.Results
                                                  .Where(s => s.OrganizationID == this.OrganizationID && s.UserID != this.OrganizationOwner.UserID)
                                                  .ToList();

                this.IsUsersSourceEmpty = this.OrganizationMembersStatus == null || this.OrganizationMembersStatus.Count == 0;
            }
            else
            {
                // notify user if there is any error
                eNegMessanger.RaiseErrorMessage.Send(e.Error);
            }
        }

        /// <summary>
        /// Call back og get organization members.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The args.</param>
        private void mManageOrganizationModel_GetOrganizationMembersComplete(object sender, eNegEntityResultArgs<User> e)
        {
            if (!e.HasError)
            {
                this.OrganizationMembers = e.Results
                                            .OrderBy(g => g.FirstName)
                                            .ToList();

                //In case this callback return after binding OrgaizationMembersGrid
                if (OrganizationMembersStatus != null)
                    OrganizationMembersStatus = new List<UserOrganization>(OrganizationMembersStatus);

            }
            else
            {
                // notify user if there is any error
                eNegMessanger.RaiseErrorMessage.Send(e.Error);
            }
        }

        /// <summary>
        /// Call back of get organization by ID.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        private void mManageOrganizationModel_GetOrganizationByIDComplete(object sender, eNegEntityResultArgs<Organization> e)
        {
            if (!e.HasError)
            {
                this.CurrentOrganization = e.Results.FirstOrDefault();
            }
            else
            {
                // notify user if there is any error
                eNegMessanger.RaiseErrorMessage.Send(e.Error);
            }
        }

        #endregion

        /// <summary>
        /// Indicating That thier is any changes in the Current Data.
        /// </summary>
        /// <param name="sender">Value Of Sender</param>
        /// <param name="e">Value Of e</param>
        private void mManageOrganizationModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals("HasChanges") || e.PropertyName.Equals("IsBusy"))
            {
                SubmitChangeCommand.RaiseCanExecuteChanged();
                CancelChangeCommand.RaiseCanExecuteChanged();
                RemoveUserFromOrganizationCommand.RaiseCanExecuteChanged();

                //Selecting Row in grid
                Deployment.Current.Dispatcher.BeginInvoke(() => ActivateOrganizationMember());

            }
        }

        /// <summary>
        /// return of Calling SavingChanges indicating that the save operation Completed.
        /// </summary>
        /// <param name="sender">Value of Sender</param>
        /// <param name="e">Value of e</param>
        private void mManageOrganizationModel_SaveChangesComplete(object sender, SubmitOperationEventArgs e)
        {
            if (!e.HasError)
            {
                if (e.SubmitOp.ChangeSet.ModifiedEntities.Count > 0 || e.SubmitOp.ChangeSet.RemovedEntities.Count > 0)
                {
                    //To make rebind to hide check boxes (IS Member) PM->M or PM->O
                    this.OrganizationMembersStatus = new List<UserOrganization>(this.OrganizationMembersStatus);

                    this.IsUsersSourceEmpty = this.OrganizationMembersStatus == null || this.OrganizationMembersStatus.Count == 0;

                    if (this.QueueMails.Count() > 0)
                    {
                        this.SendQueuedMails();
                    }
                    else
                    {
                        this.ShowMessage();
                    }
                }
            }
            else
            {
                // notify user if there is any error
                eNegMessanger.RaiseErrorMessage.Send(e.Error);
            }
        }

        /// <summary>
        /// After Sending the Confirmation Mail
        /// </summary>
        /// <param name="e">Value e</param>
        private void mManageOrganizationModel_SendingMailCompleted(InvokeOperation e)
        {
            this.noOfSentMails++;

            if (e.HasError)
            {
                e.MarkErrorAsHandled();
                eNegMessanger.RaiseErrorMessage.Send(e.Error);
            }

            if (this.noOfSentMails == this.queueMailsCount)
            {
                ShowMessage();
            }

        }

        #endregion

        #region → Commands       .

        /// <summary>
        /// User Save changes via Calling Submit Changes Message so It call
        /// OnSubmitChangesMessage Method.
        /// </summary>
        public RelayCommand SubmitChangeCommand
        {
            get
            {
                if (mSubmitChangeCommand == null)
                {
                    mSubmitChangeCommand = new RelayCommand(() =>
                    {
                        try
                        {
                            if (!mManageOrganizationModel.IsBusy)
                            {

                                OnSubmitChangesMessage();
                            }
                        }
                        catch (Exception ex)
                        {
                            // notify user if there is any error
                            eNegMessanger.RaiseErrorMessage.Send(ex);
                        }
                    }
                    , () => !mManageOrganizationModel.IsBusy && mManageOrganizationModel.HasChanges);
                }
                return mSubmitChangeCommand;
            }
        }

        /// <summary>
        /// User Cancelling changes via Calling Cancel Changes Message so It call
        /// OnCancelChangesMessage Method.
        /// </summary>
        public RelayCommand CancelChangeCommand
        {
            get
            {
                if (mCancelChangeCommand == null)
                {
                    mCancelChangeCommand = new RelayCommand(() =>
                    {
                        try
                        {
                            if (!mManageOrganizationModel.IsBusy)
                            {
                                OnCancelChangesMessage();
                            }

                        }
                        catch (Exception ex)
                        {
                            // notify user if there is any error
                            eNegMessanger.RaiseErrorMessage.Send(ex);
                        }
                    }
                    , () => !mManageOrganizationModel.IsBusy && mManageOrganizationModel.HasChanges);
                }
                return mCancelChangeCommand;
            }
        }

        /// <summary>
        /// Gets the remove user from organization command.
        /// </summary>
        /// <value>The remove user from organization command.</value>
        public RelayCommand<UserOrganization> RemoveUserFromOrganizationCommand
        {
            get
            {
                if (mRemoveUserFromOrganizationCommand == null)
                {
                    mRemoveUserFromOrganizationCommand = new RelayCommand<UserOrganization>((userOrganization) =>
                    {
                        try
                        {
                            if (!mManageOrganizationModel.IsBusy && userOrganization != null)
                            {
                                #region → Confirmation Message .

                                Action<MessageBoxResult> callBackResult = null;

                                // ask to confirm canceling this new issue first
                                DialogMessage dialogMessage = new DialogMessage(
                                    this,
                                    Resources.RemoveCurrentUserMessageBoxText,
                                      result => callBackResult(result))
                                {
                                    Button = MessageBoxButton.OKCancel,
                                    Caption = Resources.ConfirmMessageBoxCaption
                                };

                                eNegMessanger.ConfirmMessage.Send(dialogMessage);

                                #endregion "Confirmation Message"

                                callBackResult = (result) =>
                                {
                                    if (result == MessageBoxResult.Cancel)
                                        return;

                                    this.ListOfRemovedUsers.Add(userOrganization.User);

                                    this.OrganizationMembersStatus.Remove(userOrganization);

                                    this.RemoveUserOrganization(userOrganization);

                                    this.OrganizationMembersStatus = new List<UserOrganization>(this.OrganizationMembersStatus);

                                    this.RaisePropertyChanged("OrganizationMembersStatus");
                                };
                            }
                        }
                        catch (Exception ex)
                        {
                            // notify user if there is any error
                            eNegMessanger.RaiseErrorMessage.Send(ex);
                        }
                    }
                    , (userOrganization) => !mManageOrganizationModel.IsBusy && userOrganization != null);
                }
                return mRemoveUserFromOrganizationCommand;
            }
        }

        #endregion

        #region → Methods        .

        #region → Private        .

        #region → Preparing a List of mails and send them   .

        /// <summary>
        /// Prepaires the queue mails.
        /// </summary>
        private void PrepairQueueMails()
        {
            foreach (var userItem in this.ListOfRemovedUsers)
            {
                this.QueueMails.Add(new QueueMail(this.mManageOrganizationModel.MailHelper, QueueMailType.MemeberRemoved, this.CurrentOrganization, this.OrganizationOwner, userItem));
            }

            foreach (var userOrgItem in this.OrganizationMembersStatus.Where(ss => ss.EntityState == EntityState.Modified))
            {

                UserOrganization orignalRecord = userOrgItem.GetOriginal() as UserOrganization;

                if (orignalRecord != null && orignalRecord.MemberStatus != userOrgItem.MemberStatus)
                {
                    #region → Be Pending Owner .

                    if (userOrgItem.MemberStatus == eNegConstant.OrganizationMemberStatus.PendingOwner)
                    {

                        string refusedOperationstring = "";
                        string acceptOperationstring = "";

                        //Refused operation string
                        refusedOperationstring = AddUserOperation(userOrgItem.User, eNegConstant.UserOperations.RefuseOwnerRequest);

                        //in Case one want to leave the organization and put deputy owner.
                        if (AppConfigurations.RemoveOriginalOwner)
                        {
                            acceptOperationstring = AddUserOperation(userOrgItem.User, eNegConstant.UserOperations.Accept_DeleteOwnerRequest);
                        }
                        else
                        {
                            acceptOperationstring = AddUserOperation(userOrgItem.User, eNegConstant.UserOperations.AcceptOwnerRequest);
                        }

                        this.QueueMails.Add(new QueueMail(this.mManageOrganizationModel.MailHelper, QueueMailType.AskToBeOwner, this.CurrentOrganization, userOrgItem.User, null, acceptOperationstring, refusedOperationstring));
                    }

                    #endregion

                    // Be Member was pending member (PM → M) .
                    else if (userOrgItem.MemberStatus == eNegConstant.OrganizationMemberStatus.Member && orignalRecord.MemberStatus == eNegConstant.OrganizationMemberStatus.PendingMember)
                    {
                        this.QueueMails.Add(new QueueMail(this.mManageOrganizationModel.MailHelper, QueueMailType.MemeberActivated, this.CurrentOrganization, userOrgItem.User, userOrgItem.User));
                    }
                    // Be Member was pending Owner or Owner (PM → PO-O) .
                    else if (userOrgItem.MemberStatus == eNegConstant.OrganizationMemberStatus.Member && (orignalRecord.MemberStatus == eNegConstant.OrganizationMemberStatus.Owner || orignalRecord.MemberStatus == eNegConstant.OrganizationMemberStatus.PendingOwner))
                    {
                        this.QueueMails.Add(new QueueMail(this.mManageOrganizationModel.MailHelper, QueueMailType.NoLongerIsOwner, this.CurrentOrganization, userOrgItem.User, userOrgItem.User));
                    }
                }

            }
        }

        /// <summary>
        /// Sends the queued mails.
        /// </summary>
        private void SendQueuedMails()
        {
            this.queueMailsCount = this.QueueMails.Count;
            this.noOfSentMails = 0;

            foreach (var queueMailsItem in this.QueueMails)
            {
                queueMailsItem.Send();
            }

            this.QueueMails.Clear();
        }

        /// <summary>
        /// Adds the user operation.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="type">The type.</param>
        /// <returns></returns>
        private string AddUserOperation(User user, byte type)
        {
            string mOperationString = HashHelper.ComputeSaltedHash(user.UserID.ToString()) + HashHelper.ComputeSaltedHash(user.EmailAddress);

            //To Solve Problem in Automation
            mOperationString = mOperationString.Replace("+", "P");

            //Add New Record In UserOperation
            user.UserOperations.Add(new UserOperation() { OperationID = Guid.NewGuid(), UserID = user.UserID, OperationString = mOperationString, Type = type, RequestUserID = this.OrganizationOwner.UserID, OrganizationID = this.OrganizationID });

            return mOperationString;
        }

        #endregion

        /// <summary>
        /// Shows the message After save or After Sending The Mail.
        /// </summary>
        private void ShowMessage()
        {
            #region ► Show Message ◄

            IsBusy = false;

            eNegMessage Message = new eNegMessage(Resources.UpdateManageOrganizationSuccess);
            eNegMessanger.SendCustomMessage.Send(Message);

            #endregion
        }

        /// <summary>
        /// Executed when SubmitChangesMessage is received
        /// </summary>
        private void OnSubmitChangesMessage()
        {
            this.IsBusy = true;

            this.PrepairQueueMails();

            this.mManageOrganizationModel.SaveChangesAsync();
        }

        /// <summary>
        ///Executed when CancelChangesMessage is recieved
        /// </summary>
        private void OnCancelChangesMessage()
        {
            this.mManageOrganizationModel.RejectChanges();
            this.ListOfRemovedUsers.Clear();
        }

        /// <summary>
        /// Activates the organization member.
        /// </summary>
        private void ActivateOrganizationMember()
        {
            if (this.mManageOrganizationModel.IsBusy == false &&
                AppConfigurations.ProfileUserID != null &&
                !this.mManageOrganizationModel.HasChanges &&
                this.OrganizationMembersStatus != null &&
                this.OrganizationMembersStatus.Count() > 0)
            {
                UserOrganization tmp = this.OrganizationMembersStatus
                                           .Where(ss => ss.UserID == AppConfigurations.ProfileUserID &&
                                                        ss.OrganizationID == this.OrganizationID)
                                            .FirstOrDefault();

                if (tmp != null)
                {
                    eNegMessanger.EditUserOrganizationMessage.Send(tmp);
                }
            }
        }

        #endregion

        #region → Public         .

        /// <summary>
        /// Gets the organization members async.
        /// </summary>
        public void GetOrganizationMembersAsync()
        {
            this.mManageOrganizationModel.GetOrganizationMembersAsync();
        }

        /// <summary>
        /// Gets the organization members status async.
        /// </summary>
        public void GetOrganizationMembersStatusAsync()
        {
            this.mManageOrganizationModel.GetOrganizationMembersStatusAsync();
        }

        /// <summary>
        /// Gets the organization.
        /// </summary>
        public void GetOrganization()
        {
            this.mManageOrganizationModel.GetOrganizationByID();
        }

        /// <summary>
        /// Removes the user organization.
        /// </summary>
        /// <param name="userOrganization">The user organization.</param>
        public void RemoveUserOrganization(UserOrganization userOrganization)
        {
            this.mManageOrganizationModel.RemoveUserOrganization(userOrganization);
        }

        #region → ICleanup interface implementation .

        /// <summary>
        /// Cleanups this instance.
        /// </summary>
        public override void Cleanup()
        {
            // Unregister all events

            this.mManageOrganizationModel.SaveChangesComplete -= new EventHandler<SubmitOperationEventArgs>(mManageOrganizationModel_SaveChangesComplete);
            this.mManageOrganizationModel.PropertyChanged -= new PropertyChangedEventHandler(mManageOrganizationModel_PropertyChanged);
            this.mManageOrganizationModel.GetOrganizationMembersComplete -= new EventHandler<eNegEntityResultArgs<User>>(mManageOrganizationModel_GetOrganizationMembersComplete);
            this.mManageOrganizationModel.GetOrganizationMembersStatusComplete -= new EventHandler<eNegEntityResultArgs<UserOrganization>>(mManageOrganizationModel_GetOrganizationMembersStatusComplete);
            this.mManageOrganizationModel.SendingMailCompleted -= new Action<InvokeOperation>(mManageOrganizationModel_SendingMailCompleted);

            // unregister any messages for this ViewModel
            base.Cleanup();

            // unregister any messages for this ViewModel
            // Cleanup itself
            Messenger.Default.Unregister(this);

            AppConfigurations.RemoveOriginalOwner = false;
        }

        #endregion

        #endregion

        #endregion
    }
}