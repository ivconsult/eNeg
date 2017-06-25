#region → Usings   .
using citPOINT.eNeg.Common;
using citPOINT.eNeg.Data.Web;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.ServiceModel.DomainServices.Client;
using HashHelper = citPOINT.eNeg.Common.HashHelper;
using citPOINT.eNeg.Common.eNegApps;

#endregion

#region → History  .

/* Date         User            Change
 * 
 * 19.10.10     M.Wahab     • creation
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
    #region  Using MEF to export Update User Profile ViewModel

    /// <summary>
    /// This Class Do all Operations Related To Update My Profile or Update any user Profile.
    /// And Sending Mail to the User in case he change his mail.
    /// </summary>
    [Export(ViewModelTypes.UpdateUserProfileViewModel)]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    #endregion
    public class UpdateUserProfileViewModel : ViewModelBase
    {

        #region → Fields         .
        private IUpdateUserProfileModel mUpdateUserProfileModel;
        private ManageUserOrganizationViewModel mManageUserOrgViewModel;

        private User mCurrentUser;

        private IEnumerable<Culture> mCultureEntries;
        private IEnumerable<PreferedLanguage> mPreferedLanguageEntries;
        private IEnumerable<Country> mCountryEntries;

        private string mOperationString;
        private bool mIsBusy;
        private bool isMailChanged = false;
        private bool mShowBusyIndicator;

        private RelayCommand mSubmitChangeCommand = null;
        private RelayCommand<string> mCancelChangeCommand = null;
        private RelayCommand mChangeUserMailCommand = null;
        private RelayCommand mChangeUserPasswordCommand;
        private RelayCommand mBackToUsersOverViewCommand;

        #endregion

        #region → Properties     .

        /// <summary>
        /// Gets or sets a value indicating whether [show busy indicator].
        /// </summary>
        /// <value><c>true</c> if [show busy indicator]; otherwise, <c>false</c>.</value>
        public bool ShowBusyIndicator
        {
            get
            {
                return mShowBusyIndicator;
            }
            set
            {
                mShowBusyIndicator = value;
                this.RaisePropertyChanged("ShowBusyIndicator");
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
        /// The Value Of the Current User
        /// (New User info.)
        /// </summary>
        public User CurrentUser
        {
            get { return mCurrentUser; }
            set
            {
                if (mCurrentUser != value)
                {
                    mCurrentUser = value;

                    if (this.ManageUserOrgViewModel != null && value != null)
                    {
                        this.ManageUserOrgViewModel.CurrentUser = value;
                    }

                    this.RaisePropertyChanged("CurrentUser");
                }
            }
        }

        /// <summary>
        /// Gets or sets the manage user org view model.
        /// </summary>
        /// <value>The manage user org view model.</value>
        public ManageUserOrganizationViewModel ManageUserOrgViewModel
        {
            get
            {
                return mManageUserOrgViewModel;
            }
            set
            {
                mManageUserOrgViewModel = value;

                if (this.CurrentUser != null && this.CurrentUser.UserID != Guid.Empty && value != null)
                {
                    this.mManageUserOrgViewModel.CurrentUser = this.CurrentUser;
                }

                this.RaisePropertyChanged("ManageUserOrgViewModel");
            }
        }

        /// <summary>
        /// Gets a value indicating whether this instance is for manage users.
        /// Hide change the User mail in case of calling this View
        /// By User Management to admin user can't change the mail of the user.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is for manage users; otherwise, <c>false</c>.
        /// </value>
        public bool IsForManageUsers
        {
            get
            {
                return AppConfigurations.ProfileUserID != AppConfigurations.CurrentLoginUser.UserID;
            }
        }

        /// <summary>
        /// Gets a value indicating whether this instance is for my profile.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is for my profile; otherwise, <c>false</c>.
        /// </value>
        public bool IsForMyProfile
        {
            get
            {
                return !IsForManageUsers;
            }
        }

        #region → User New Mail    .

        /// <summary>
        /// Gets a value indicating whether this instance can change user mail.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance can change user mail; otherwise, <c>false</c>.
        /// </value>
        public bool CanChangeUserMail
        {
            get
            {
                return this.CurrentUser.CheckForNewEmailAddress;
            }
        }

        /// <summary>
        /// Gets or sets the change user mail text.
        /// </summary>
        /// <value>The change user mail text.</value>
        public string ChangeUserMailText
        {
            get
            {
                return (this.CurrentUser.CheckForNewEmailAddress ? "- " : "+ ") + "Change Your Mail";
            }
        }

        #endregion

        #region → User New Password.

        /// <summary>
        /// Gets a value indicating whether this instance can change user password.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance can change user password; otherwise, <c>false</c>.
        /// </value>
        public bool CanChangeUserPassword
        {
            get
            {
                return this.CurrentUser.CheckForNewPassword;
            }
        }

        /// <summary>
        /// Gets the change user password text.
        /// </summary>
        /// <value>The change user password text.</value>
        public string ChangeUserPasswordText
        {
            get
            {
                return (this.CurrentUser.CheckForNewPassword ? "-" : "+") + " Change Password";
            }
        }

        #endregion

        #region → Lockup tables    .

        /// <summary>
        /// Value Of Lockup Table (Culture)
        /// </summary>
        public IEnumerable<Culture> CultureEntries
        {
            get { return mCultureEntries; }
            private set
            {
                if (value != mCultureEntries)
                {
                    mCultureEntries = value;
                    this.RaisePropertyChanged("CultureEntries");

                }
            }
        }

        /// <summary>
        /// Value Of Lockup Table (PreferedLanguage)
        /// </summary>
        public IEnumerable<PreferedLanguage> PreferedLanguageEntries
        {
            get { return mPreferedLanguageEntries; }
            private set
            {
                if (value != mPreferedLanguageEntries)
                {
                    mPreferedLanguageEntries = value;
                    this.RaisePropertyChanged("PreferedLanguageEntries");
                }
            }
        }

        /// <summary>
        /// Value Of Lockup Table (Country)
        /// </summary>
        public IEnumerable<Country> CountryEntries
        {
            get { return mCountryEntries; }
            private set
            {
                if (value != mCountryEntries)
                {
                    mCountryEntries = value;
                    this.RaisePropertyChanged("CountryEntries");
                }
            }
        }

        #endregion

        #endregion Properties

        #region → Constructors   .

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateUserProfileViewModel"/> class.
        /// </summary>
        /// <param name="updateUserProfileModel">The user register model.</param>
        [ImportingConstructor]
        public UpdateUserProfileViewModel(IUpdateUserProfileModel updateUserProfileModel)
        {
            mUpdateUserProfileModel = updateUserProfileModel;
            if (mUpdateUserProfileModel.HasChanges)
                mUpdateUserProfileModel.RejectChanges();

            #region Set up event handling

            mUpdateUserProfileModel.SaveChangesComplete += new EventHandler<SubmitOperationEventArgs>(mUpdateUserProfileModel_SaveChangesComplete);
            mUpdateUserProfileModel.PropertyChanged += new PropertyChangedEventHandler(mUpdateUserProfileModel_PropertyChanged);
            mUpdateUserProfileModel.GetCountryComplete += new EventHandler<eNegEntityResultArgs<Country>>(mUpdateUserProfileModel_GetCountryComplete);
            mUpdateUserProfileModel.GetCultureComplete += new EventHandler<eNegEntityResultArgs<Culture>>(mUpdateUserProfileModel_GetCultureComplete);
            mUpdateUserProfileModel.GetPreferedLanguageComplete += new EventHandler<eNegEntityResultArgs<PreferedLanguage>>(mUpdateUserProfileModel_GetPreferedLanguageComplete);
            mUpdateUserProfileModel.GetCheckIsEmailExistComplete += new EventHandler<eNegEntityResultArgs<User>>(mUpdateUserProfileModel_GetCheckIsEmailExistComplete);
            mUpdateUserProfileModel.SendingMailCompleted += new Action<InvokeOperation>(mUpdateUserProfileModel_SendingMailCompleted);
            mUpdateUserProfileModel.GetUserByIDComplete += new EventHandler<eNegEntityResultArgs<User>>(mUpdateUserProfileModel_GetUserByIDComplete);


            #endregion

            #region Loading Related Tables

            GetCountryAsync();
            GetPreferedLanguageAsync();
            GetCultureAsync();
            GetUserByID(AppConfigurations.ProfileUserID);

            #endregion

            // initiate a new User
            CurrentUser = new User();

        }

        #endregion "Constructor"

        #region → Event Handlers .

        #region →  Loading Event Handlers .

        /// <summary>
        /// Call back of geting Cultures. 
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        void mUpdateUserProfileModel_GetCultureComplete(object sender, eNegEntityResultArgs<Culture> e)
        {
            if (!e.HasError)
            {
                CultureEntries = e.Results.OrderBy(g => g.CultureName);
            }
            else
            {
                // notify user if there is any error
                eNegMessanger.RaiseErrorMessage.Send(e.Error);
            }
        }

        /// <summary>
        /// Complete events of Get Prefered Language
        /// that return A value of all records of table (PreferedLanguage).
        /// </summary>
        /// <param name="sender">Value Of Sender</param>
        /// <param name="e">Value Of e</param>
        void mUpdateUserProfileModel_GetPreferedLanguageComplete(object sender, eNegEntityResultArgs<PreferedLanguage> e)
        {
            if (!e.HasError)
            {
                PreferedLanguageEntries = e.Results.OrderBy(g => g.PreferedLanguage1);
            }
            else
            {
                // notify user if there is any error
                eNegMessanger.RaiseErrorMessage.Send(e.Error);
            }
        }

        /// <summary>
        /// Complete events of GetCountry
        /// that return A value of all records of table (Country).
        /// </summary>
        /// <param name="sender">Value Of Sender</param>
        /// <param name="e">Value Of e</param>
        void mUpdateUserProfileModel_GetCountryComplete(object sender, eNegEntityResultArgs<Country> e)
        {

            if (!e.HasError)
            {
                CountryEntries = e.Results.OrderBy(g => g.CountryName);
            }
            else
            {
                // notify user if there is any error
                eNegMessanger.RaiseErrorMessage.Send(e.Error);
            }
        }

        /// <summary>
        /// Complete handler of Get User By ID.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        void mUpdateUserProfileModel_GetUserByIDComplete(object sender, eNegEntityResultArgs<User> e)
        {
            if (!e.HasError)
            {
                CurrentUser = e.Results.FirstOrDefault();
            }
            else
            {
                // notify user if there is any error
                eNegMessanger.RaiseErrorMessage.Send(e.Error);
            }
        }
        #endregion Loading Events

        /// <summary>
        /// Indicating That thier is any changes in the Current Data.
        /// </summary>
        /// <param name="sender">Value Of Sender</param>
        /// <param name="e">Value Of e</param>
        private void mUpdateUserProfileModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals("HasChanges") || e.PropertyName.Equals("IsBusy"))
            {
                this.ShowBusyIndicator = this.mUpdateUserProfileModel.IsBusy;

                //this.IsBusy = this.mUpdateUserProfileModel.IsBusy;
                SubmitChangeCommand.RaiseCanExecuteChanged();
                CancelChangeCommand.RaiseCanExecuteChanged();
                ChangeUserMailCommand.RaiseCanExecuteChanged();
            }

            if (e.PropertyName.Equals("IsBusy"))
            {
                this.ShowBusyIndicator = this.mUpdateUserProfileModel.IsBusy;
            }

        }

        /// <summary>
        /// In Case of that the User Email Is not Exist Before 
        /// So this method Will Continue in Saving Operation
        /// </summary>
        /// <param name="sender">value of Sender</param>
        /// <param name="e">Value Of e</param>
        private void mUpdateUserProfileModel_GetCheckIsEmailExistComplete(object sender, eNegEntityResultArgs<User> e)
        {
            if (!e.HasError)
            {
                if (e.Results.Count() != 0)
                {
                    this.CurrentUser.ValidationErrors.Add(new System.ComponentModel.DataAnnotations.ValidationResult(Resources.EmailRepeated, new string[] { "NewEmail" }));
                    IsBusy = false;
                    return;
                }

                else
                {
                    #region Preparing For Confirmation Mail

                    this.mOperationString = HashHelper.ComputeSaltedHash(this.CurrentUser.UserID.ToString()) + HashHelper.ComputeSaltedHash(this.CurrentUser.EmailAddress);

                    //To Solve Problem in Automation
                    this.mOperationString = this.mOperationString.Replace("+", "P");

                    //Add New Record In UserOperation
                    this.mCurrentUser.UserOperations.Add(new UserOperation() { OperationID = Guid.NewGuid(), UserID = this.CurrentUser.UserID, OperationString = this.mOperationString, Type = eNegConstant.UserOperations.Confiramtion, NewEmailAddress = this.CurrentUser.NewEmail });

                    #endregion Preparing For Confirmation Mail

                    isMailChanged = true;

                    this.CurrentUser.CheckForNewEmailAddress = false;
                    this.CurrentUser.CheckForNewPassword = false;

                    mUpdateUserProfileModel.SaveChangesAsync();
                }
            }
            else
            {
                // notify user if there is any error
                eNegMessanger.RaiseErrorMessage.Send(e.Error);
                IsBusy = false;
            }
        }

        /// <summary>
        /// return of Calling SavingChanges indicating that the save operation Completed.
        /// </summary>
        /// <param name="sender">Value of Sender</param>
        /// <param name="e">Value of e</param>
        private void mUpdateUserProfileModel_SaveChangesComplete(object sender, SubmitOperationEventArgs e)
        {
            if (!e.HasError)
            {
                if ((e.SubmitOp.ChangeSet.ModifiedEntities.Count >= 1) &&
                    (e.SubmitOp.ChangeSet.ModifiedEntities.FirstOrDefault() is User))
                {
                    #region → Sending Mails for Organization Owners .

                    List<Entity> lstAddedUserOrganization = e.SubmitOp.ChangeSet.AddedEntities.Where(s => s.GetType().Equals(typeof(UserOrganization))).ToList();

                    if (lstAddedUserOrganization.Count > 0)
                    {
                        eNegMessanger.EditUserOrganizationMessage.Send(lstAddedUserOrganization.First() as UserOrganization);
                    }
                    List<Entity> lstLeavedUserOrganizations = e.SubmitOp.ChangeSet.RemovedEntities.Where(s => s.GetType().Equals(typeof(UserOrganization))).ToList();

                    if (lstLeavedUserOrganizations.Count > 0)
                    {
                        eNegMessanger.EditUserOrganizationMessage.Send(lstLeavedUserOrganizations.First() as UserOrganization);
                    }

                    this.ManageUserOrgViewModel.PrepairQueueMails(lstAddedUserOrganization);

                    if (MainPlatformInfo.Instance.UserInfo.UserID == this.CurrentUser.UserID)
                    {
                        MainPlatformInfo.Instance.UserInfo = this.CurrentUser;
                    }

                    #endregion

                    #region "Sending Mail To The User"
                    if (this.isMailChanged)
                    {
                        isMailChanged = false;

                        this.mUpdateUserProfileModel.SendConfiramtionMail(this.CurrentUser.NewEmail, this.CurrentUser.FirstName, this.CurrentUser.LastName, this.mOperationString);
                    }
                    #endregion "Sending Mail To The User"
                    else
                    {
                        ShowMessage();
                    }

                    ManageUserMailStatus();
                    ManageUserPasswordStatus();
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
        private void mUpdateUserProfileModel_SendingMailCompleted(InvokeOperation e)
        {
            IsBusy = false;

            if (e.HasError)
            {
                e.MarkErrorAsHandled();

                eNegMessanger.RaiseErrorMessage.Send(e.Error);
            }
            else
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
                            if (!mUpdateUserProfileModel.IsBusy)
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
                    , () => !mUpdateUserProfileModel.IsBusy && mUpdateUserProfileModel.HasChanges);
                }
                return mSubmitChangeCommand;
            }
        }

        /// <summary>
        /// User Cancelling changes via Calling Cancel Changes Message so It call
        /// OnCancelChangesMessage Method.
        /// </summary>
        public RelayCommand<string> CancelChangeCommand
        {
            get
            {
                if (mCancelChangeCommand == null)
                {
                    mCancelChangeCommand = new RelayCommand<string>((viewName) =>
                    {
                        try
                        {
                            if (!mUpdateUserProfileModel.IsBusy)
                            {
                                OnCancelChangesMessage();

                                if (!string.IsNullOrEmpty(viewName) && viewName.Equals("ClosePopupView", StringComparison.InvariantCultureIgnoreCase))
                                {
                                    eNegMessanger.FlippMessage.Send(ViewTypes.ClosePopupView);
                                }
                                else
                                {
                                    eNegMessanger.FlippMessage.Send(ViewTypes.InnerViewChangeUserContactsDetails);
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            // notify user if there is any error
                            eNegMessanger.RaiseErrorMessage.Send(ex);
                        }
                    }
                    , (viewName) => !mUpdateUserProfileModel.IsBusy);
                }
                return mCancelChangeCommand;
            }
        }

        /// <summary>
        /// Open or closed the options of changing user mail command.
        /// </summary>
        /// <value>The change user mail command.</value>
        public RelayCommand ChangeUserMailCommand
        {
            get
            {
                if (mChangeUserMailCommand == null)
                {
                    mChangeUserMailCommand = new RelayCommand(() =>
                    {
                        try
                        {
                            if (!mUpdateUserProfileModel.IsBusy)
                            {
                                this.OnCancelChangesMessage();

                                this.CurrentUser.CheckForNewEmailAddress = true;

                                this.ManageUserMailStatus();

                                //Reset values in case of undo Add new E-mail

                                this.CurrentUser.NewEmail = null;

                                this.CurrentUser.NewEmailConfirmation = null;

                                this.CurrentUser.ValidationErrors.Clear();

                                eNegMessanger.FlippMessage.Send(ViewTypes.InnerViewChangeUserEmail);

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
                return mChangeUserMailCommand;
            }
        }

        /// <summary>
        /// Gets the change user password command.
        /// </summary>
        /// <value>The change user password command.</value>
        public RelayCommand ChangeUserPasswordCommand
        {
            get
            {
                if (mChangeUserPasswordCommand == null)
                {
                    mChangeUserPasswordCommand = new RelayCommand(() =>
                    {
                        try
                        {
                            if (!mUpdateUserProfileModel.IsBusy)
                            {
                                this.OnCancelChangesMessage();

                                this.CurrentUser.CheckForNewPassword = true;

                                this.ManageUserPasswordStatus();

                                this.CurrentUser.NewPassword = null;

                                this.CurrentUser.NewPasswordConfirmation = null;

                                //RemoveValidation("NewPassword");
                                //RemoveValidation("NewPasswordConfirmation");

                                this.CurrentUser.ValidationErrors.Clear();

                                eNegMessanger.FlippMessage.Send(ViewTypes.InnerViewChangeUserPassword);
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
                return mChangeUserPasswordCommand;
            }
        }

        /// <summary>
        /// Gets the back to users over view command.
        /// </summary>
        /// <value>The back to users over view command.</value>
        public RelayCommand BackToUsersOverViewCommand
        {
            get
            {
                if (mBackToUsersOverViewCommand == null)
                {
                    mBackToUsersOverViewCommand = new RelayCommand(() =>
                    {
                        try
                        {
                            eNegMessanger.FlippMessage.Send(ViewTypes.UsersOverview);
                        }
                        catch (Exception ex)
                        {
                            // notify user if there is any error
                            eNegMessanger.RaiseErrorMessage.Send(ex);
                        }
                    }
                    , () => true);
                }
                return mBackToUsersOverViewCommand;
            }
        }



        #endregion

        #region → Methods        .

        #region → Private        .

        /// <summary>
        /// Shows the message After save or After Sending The Mail.
        /// </summary>
        private void ShowMessage()
        {
            #region ► Show Message ◄

            eNegMessage Message = new eNegMessage(this.CurrentUser.CheckForNewEmailAddress ? Resources.UpdateMyProfileAndUserEmail : Resources.UpdateMyProfileSuccess);

            eNegMessanger.SendCustomMessage.Send(Message);

            eNegMessanger.FlippMessage.Send(ViewTypes.InnerViewChangeUserContactsDetails);

            #endregion

            //Reload the Current User Profile with The Updated One.
            //GetUserByID(AppConfigurations.ProfileUserID);
        }

        /// <summary>
        /// Executed when SubmitChangesMessage is received
        /// </summary>
        private void OnSubmitChangesMessage()
        {
            if (this.CurrentUser != null)
            {


                #region ►    Email Validation      ◄

                // this should trigger validation even if the Title is not changed and is null
                if (string.IsNullOrWhiteSpace(this.CurrentUser.EmailAddress))
                    this.CurrentUser.EmailAddress = string.Empty;

                if (this.CurrentUser.CheckForNewEmailAddress)
                {
                    if (string.IsNullOrWhiteSpace(this.CurrentUser.NewEmail) || string.IsNullOrEmpty(this.CurrentUser.NewEmail))
                        this.CurrentUser.NewEmail = string.Empty;

                    if (string.IsNullOrWhiteSpace(this.CurrentUser.NewEmailConfirmation) || string.IsNullOrEmpty(this.CurrentUser.NewEmailConfirmation))
                        this.CurrentUser.NewEmailConfirmation = string.Empty;
                }

                #endregion ► Email Validation ◄

                #region ►    Password Validation   ◄

                if (this.CurrentUser.CheckForNewPassword)
                {

                    //---------------------------------------------------------------------------------
                    if (this.CurrentUser.NewPassword == null)
                        this.CurrentUser.NewPassword = string.Empty;
                    else if (string.IsNullOrWhiteSpace(this.CurrentUser.NewPassword))
                        this.CurrentUser.NewPassword = null;
                    //---------------------------------------------------------------------------------
                    if (this.CurrentUser.NewPasswordConfirmation == null)
                        this.CurrentUser.NewPasswordConfirmation = string.Empty;
                    else if (string.IsNullOrWhiteSpace(this.CurrentUser.NewPasswordConfirmation))
                        this.CurrentUser.NewPasswordConfirmation = null;
                    //---------------------------------------------------------------------------------


                    this.CurrentUser.Password = this.CurrentUser.NewPassword;
                    this.CurrentUser.PasswordConfirmation = this.CurrentUser.NewPasswordConfirmation;
                }
                else
                {
                    //Flag to pass validation
                    this.CurrentUser.Password = "Password";
                    this.CurrentUser.PasswordConfirmation = "Password";

                    this.CurrentUser.NewPassword = string.Empty;
                    this.CurrentUser.NewPasswordConfirmation = string.Empty;
                }

                #endregion ► Password Validation ◄

                #region ►   FName,LName Validation ◄

                //Mean the current page is Contact details not change passowrd or change email.
                if (!this.CurrentUser.CheckForNewEmailAddress && !this.CurrentUser.CheckForNewPassword)
                {
                    if (string.IsNullOrWhiteSpace(this.CurrentUser.FirstName))
                        this.CurrentUser.ValidationErrors.Add(new ValidationResult(Resources.ValidationErrorRequiredField, new string[] { "FirstName" }));
                    else
                    {
                        if (this.CurrentUser.FirstName.Length <= 1)
                            this.CurrentUser.ValidationErrors.Add(new ValidationResult(Resources.ValidateErrorYourNameLenght, new string[] { "FirstName" }));
                    }


                    if (string.IsNullOrWhiteSpace(this.CurrentUser.LastName))
                        this.CurrentUser.ValidationErrors.Add(new ValidationResult(Resources.ValidationErrorRequiredField, new string[] { "LastName" }));

                    else
                    {
                        if (this.CurrentUser.LastName.Length <= 1)
                            this.CurrentUser.ValidationErrors.Add(new ValidationResult(Resources.ValidateErrorYourNameLenght, new string[] { "LastName" }));
                    }

                    if (this.CurrentUser.ValidationErrors.Count() > 0)
                        return;
                }

                #endregion FName,LName Validation

                #region ►    Saving process        ◄

                if (this.mCurrentUser.TryValidateObject()
                    && this.mCurrentUser.TryValidateProperty("EmailAddress")
                    && this.mCurrentUser.TryValidateProperty("NewPassword")
                    && this.mCurrentUser.TryValidateProperty("NewPasswordConfirmation")
                    && this.mCurrentUser.TryValidateProperty("NewEmail")
                    && this.mCurrentUser.TryValidateProperty("NewEmailConfirmation")
                    )
                {

                    if (this.CurrentUser.CheckForNewEmailAddress)
                    {
                        IsBusy = true;
                        CheckIsEmailExist(this.mCurrentUser);
                    }

                    else
                    {
                        this.CurrentUser.CheckForNewPassword = false;

                        mUpdateUserProfileModel.SaveChangesAsync();
                    }


                }
                #endregion ►    Saving process        ◄
            }
        }

        /// <summary>
        ///Executed when CancelChangesMessage is recieved
        /// </summary>
        private void OnCancelChangesMessage()
        {
            this.mUpdateUserProfileModel.RejectChanges();

            this.ManageUserOrgViewModel.RejectChanges();

            ManageUserMailStatus();

            ManageUserPasswordStatus();
        }

        /// <summary>
        /// Manages the state of the user mail.
        /// </summary>
        private void ManageUserMailStatus()
        {
            //Open the option for one to change his mailif one can change his mail
            this.RaisePropertyChanged("CanChangeUserMail");

            //Change the label of toggle button
            this.RaisePropertyChanged("ChangeUserMailText");
        }

        /// <summary>
        /// Manages the state of the user password.
        /// </summary>
        private void ManageUserPasswordStatus()
        {
            //Open the option for one to change his passwrod if one can change his mail
            this.RaisePropertyChanged("CanChangeUserPassword");

            //Change the label of toggle button
            this.RaisePropertyChanged("ChangeUserPasswordText");

        }

        /// <summary>
        /// Removes the validation for any fileds.
        /// e.g. Password and New Password Confirmation,New Email and New Email Confirmation.
        /// </summary>
        /// <param name="FieldName">Name of the field.</param>
        private void RemoveValidation(string FieldName)
        {
            ValidationResult validationResult = null;

            do
            {
                validationResult = this.CurrentUser.ValidationErrors
                                                   .Where(ss => ss.MemberNames.Contains(FieldName))
                                                   .FirstOrDefault();

                if (validationResult != null)
                    this.CurrentUser.ValidationErrors.Remove(validationResult);

            } while (validationResult != null);

        }

        #endregion

        #region → Public         .

        /// <summary>
        /// Gets the user by ID.
        /// </summary>
        /// <param name="UserID">The user ID.</param>
        public void GetUserByID(Guid UserID)
        {
            mUpdateUserProfileModel.GetUserByID(UserID);
        }

        /// <summary>
        /// Loading table (Country).
        /// </summary>
        public void GetCountryAsync()
        {
            mUpdateUserProfileModel.GetCountryAsync();
        }


        /// <summary>
        /// Loading table (Culture).
        /// </summary>
        public void GetCultureAsync()
        {
            mUpdateUserProfileModel.GetCultureAsync();
        }

        /// <summary>
        /// Loading table (PreferedLanguage).
        /// </summary>
        public void GetPreferedLanguageAsync()
        {
            mUpdateUserProfileModel.GetPreferedLanguageAsync();
        }

        /// <summary>
        /// Check In the Current User Email is already registered before Or Not.
        /// </summary>
        /// <param name="user">Vlaue Of The Current User</param>
        public void CheckIsEmailExist(User user)
        {
            mUpdateUserProfileModel.CheckIsEmailExist(user);
        }

        #region "ICleanup interface implementation"
        /// <summary>
        /// Cleanups this instance.
        /// </summary>
        public override void Cleanup()
        {
            // Unregister all events
            mUpdateUserProfileModel.SaveChangesComplete -= new EventHandler<SubmitOperationEventArgs>(mUpdateUserProfileModel_SaveChangesComplete);
            mUpdateUserProfileModel.PropertyChanged -= new PropertyChangedEventHandler(mUpdateUserProfileModel_PropertyChanged);
            mUpdateUserProfileModel.GetCountryComplete -= new EventHandler<eNegEntityResultArgs<Country>>(mUpdateUserProfileModel_GetCountryComplete);
            mUpdateUserProfileModel.GetPreferedLanguageComplete -= new EventHandler<eNegEntityResultArgs<PreferedLanguage>>(mUpdateUserProfileModel_GetPreferedLanguageComplete);
            mUpdateUserProfileModel.GetCheckIsEmailExistComplete -= new EventHandler<eNegEntityResultArgs<User>>(mUpdateUserProfileModel_GetCheckIsEmailExistComplete);
            mUpdateUserProfileModel.SendingMailCompleted -= new Action<InvokeOperation>(mUpdateUserProfileModel_SendingMailCompleted);
            mUpdateUserProfileModel.GetUserByIDComplete -= new EventHandler<eNegEntityResultArgs<User>>(mUpdateUserProfileModel_GetUserByIDComplete);

            // unregister any messages for this ViewModel
            base.Cleanup();

            // unregister any messages for this ViewModel
            // Cleanup itself
            Messenger.Default.Unregister(this);

            this.ManageUserOrgViewModel.Cleanup();

            if (this.IsForMyProfile)
            {
                //to Clear Context
                this.mUpdateUserProfileModel.CleanUp();
            }

        }
        #endregion "ICleanup interface implementation"

        #endregion

        #endregion
    }
}