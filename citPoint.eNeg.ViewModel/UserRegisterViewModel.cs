
/*Good Comments
 * 
 Discouse the Two Pathes of adding users
 
*In case the Mail services Success
    1-Step one the User Added by lock=False
    2-Send Email (Success)
    3-Lock=True
    4-Send Success Message.

*In case the Mail Services Failed
    1-Step one the User Added by lock=False
    2-Send Email (Failed)
    3-Send Success Message.
    4-When user will login Activation mail will be send Again.
 
 * */

#region → Usings   .
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.ServiceModel.DomainServices.Client;
using citPOINT.eNeg.Common;
using citPOINT.eNeg.Data.Web;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using HashHelper = citPOINT.eNeg.Common.HashHelper;
using System.Windows.Input;
using System.Windows.Controls;
using System.Windows;



#endregion

#region → History  .

/* Date         User            Change
 * 
 * 03.08.10     M.Wahab     • creation
 * 06.09.10     M.Wahab     • Update XML Documentation And Region Accourding To New Code Convention
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
    #region  Using MEF to export UserRegisterViewModel
    /// <summary>
    /// This Class Do all Operations Related To Add New User And Sending Confirmation
    /// Mail to the user (Full Regisetr And Quick Register).
    /// </summary>
    [Export(ViewModelTypes.UserRegisterViewModel)]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    #endregion
    public class UserRegisterViewModel : ViewModelBase
    {

        #region → Fields         .

        private IUserRegisterModel mUserRegisterModel;
        private ManageUserOrganizationViewModel mManageUserOrgViewModel;
        private bool mIsQuickRegister;
        private User mCurrentUser;


        private IEnumerable<Culture> mCultureEntries;
        private IEnumerable<SecurityQuestion> mSecurityQuestionEntries;
        private IEnumerable<PreferedLanguage> mPreferedLanguageEntries;
        private IEnumerable<Country> mCountryEntries;
        private IEnumerable<AccountType> mAccountTypeEntries;

        private string mOperationString;
        private bool mIsBusy;

        private RelayCommand mSubmitChangeCommand = null;
        private RelayCommand mCancelChangeCommand = null;
        private RelayCommand<KeyEventArgs> mFocusControlAndExecuteCommand;

        #endregion

        #region → Properties     .

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
        /// Indicating If The Current View Is Full Register Or Quick Register
        /// this Is to detect the Required Fields
        /// </summary>
        public bool IsQuickRegister
        {
            get
            {
                return mIsQuickRegister;
            }
            set
            {
                mIsQuickRegister = value;
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

                if (this.CurrentUser != null && value != null)
                {
                    this.mManageUserOrgViewModel.CurrentUser = this.CurrentUser;
                }

                if (value != null)
                {
                    // This let the View model navigate to Login page
                    ManageUserOrgViewModel.IsForRegisterPage = true;
                }

                this.RaisePropertyChanged("ManageUserOrgViewModel");
            }
        }

        #region →  Lookup Tables         .

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
        /// Value Of Lockup Table (SecurityQuestion)
        /// </summary>
        public IEnumerable<SecurityQuestion> SecurityQuestionEntries
        {
            get { return mSecurityQuestionEntries; }
            private set
            {
                if (value != mSecurityQuestionEntries)
                {
                    mSecurityQuestionEntries = value;
                    this.RaisePropertyChanged("SecurityQuestionEntries");

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

        /// <summary>
        /// Value Of Lockup Table (AccountType)
        /// </summary>
        public IEnumerable<AccountType> AccountTypeEntries
        {
            get { return mAccountTypeEntries; }
            private set
            {
                if (value != mAccountTypeEntries)
                {
                    mAccountTypeEntries = value;
                    this.RaisePropertyChanged("AccountTypeEntries");
                }
            }
        }

        #endregion

        #endregion Properties

        #region → Constructors   .

        /// <summary>
        /// Initializes a new instance of the <see cref="UserRegisterViewModel"/> class.
        /// </summary>
        /// <param name="userRegisterModel">The user register model.</param>
        [ImportingConstructor]
        public UserRegisterViewModel(IUserRegisterModel userRegisterModel)
        {

            mUserRegisterModel = userRegisterModel;
            if (mUserRegisterModel.HasChanges)
                mUserRegisterModel.RejectChanges();

            #region → Set up event handling       .
            mUserRegisterModel.SaveChangesComplete += new EventHandler<SubmitOperationEventArgs>(mUserRegisterModel_SaveChangesComplete);
            mUserRegisterModel.PropertyChanged += new PropertyChangedEventHandler(mUserRegisterModel_PropertyChanged);
            mUserRegisterModel.GetAccountTypeComplete += new EventHandler<eNegEntityResultArgs<AccountType>>(_userRegisterModel_GetAccountTypeComplete);
            mUserRegisterModel.GetCountryComplete += new EventHandler<eNegEntityResultArgs<Country>>(_userRegisterModel_GetCountryComplete);
            mUserRegisterModel.GetPreferedLanguageComplete += new EventHandler<eNegEntityResultArgs<PreferedLanguage>>(_userRegisterModel_GetPreferedLanguageComplete);
            mUserRegisterModel.GetSecurityQuestionComplete += new EventHandler<eNegEntityResultArgs<SecurityQuestion>>(_userRegisterModel_GetSecurityQuestionComplete);
            mUserRegisterModel.GetCheckIsEmailExistComplete += new EventHandler<eNegEntityResultArgs<User>>(muserRegisterModel_GetCheckIsEmailExistComplete);
            mUserRegisterModel.SendingMailCompleted += new Action<InvokeOperation>(mUserRegisterModel_SendingMailCompleted);
            mUserRegisterModel.GetAllApplicationComplete += new EventHandler<eNegEntityResultArgs<Data.Web.Application>>(mUserRegisterModel_GetAllApplicationComplete);
            mUserRegisterModel.GetCultureComplete += new EventHandler<eNegEntityResultArgs<Culture>>(mUserRegisterModel_GetCultureComplete);

            #endregion

            #region → Loading Related Tables      .

            GetAccountTypeAsync();
            GetCountryAsync();
            GetPreferedLanguageAsync();
            GetSecurityQuestionAsync();
            GetCultureAsync();

            //In the callback of this GetQuery() Create new user on fly
            //to ensure that in case of register new user all application will be loaded
            //to can create UserApplicationStatus for each App for this new user
            GetAllApplicaions();

            #endregion

            #region → Register Messages           .

            // register for SubmitChangesMessage
            eNegMessanger.SubmitChangesMessage.Register(this, OnSubmitChangesMessage);
            // register for CancelChangesMessage
            eNegMessanger.CancelChangesMessage.Register(this, OnCancelChangesMessage);

            #endregion Register Messages
        }



        #endregion

        #region → Event Handlers .

        #region → Loading Lookup Tables   .

        /// <summary>
        /// Call back of geting Cultures. 
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        void mUserRegisterModel_GetCultureComplete(object sender, eNegEntityResultArgs<Culture> e)
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
        /// Ms the user register model_ get all application complete.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        private void mUserRegisterModel_GetAllApplicationComplete(object sender, eNegEntityResultArgs<Data.Web.Application> e)
        {
            if (!e.HasError)
            {
                // initiate a new User
                CurrentUser = AddNewUser(true);

                this.RaiseCanButtonExcute();

                // raise property changed for CurrentUser to reflect changes.
                this.RaisePropertyChanged("CurrentUser");
            }
            else
            {
                // notify user if there is any error
                eNegMessanger.RaiseErrorMessage.Send(e.Error);
            }
        }

        /// <summary>
        /// Complete events of GetSecurityQuestion
        /// that return A value of all records of table (SecurityQuestion).
        /// </summary>
        /// <param name="sender">Value Of Sender</param>
        /// <param name="e">Value Of e</param>
        private void _userRegisterModel_GetSecurityQuestionComplete(object sender, eNegEntityResultArgs<SecurityQuestion> e)
        {
            if (!e.HasError)
            {
                SecurityQuestionEntries = e.Results.OrderBy(g => g.Question);
            }
            else
            {
                // notify user if there is any error
                eNegMessanger.RaiseErrorMessage.Send(e.Error);
            }
        }

        /// <summary>
        /// Complete events of GetPreferedLanguage
        /// that return A value of all records of table (PreferedLanguage).
        /// </summary>
        /// <param name="sender">Value Of Sender</param>
        /// <param name="e">Value Of e</param>
        private void _userRegisterModel_GetPreferedLanguageComplete(object sender, eNegEntityResultArgs<PreferedLanguage> e)
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
        private void _userRegisterModel_GetCountryComplete(object sender, eNegEntityResultArgs<Country> e)
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
        /// Complete events of GetAccountType
        /// that return A value of all records of table (AccountType).
        /// </summary>
        /// <param name="sender">Value Of Sender</param>
        /// <param name="e">Value Of e</param>
        private void _userRegisterModel_GetAccountTypeComplete(object sender, eNegEntityResultArgs<AccountType> e)
        {
            if (!e.HasError)
            {
                AccountTypeEntries = e.Results.OrderBy(g => g.TypeName);
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
        private void mUserRegisterModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals("HasChanges") || e.PropertyName.Equals("IsBusy"))
            {
                RaiseCanButtonExcute();
            }
        }

        private void RaiseCanButtonExcute()
        {
            SubmitChangeCommand.RaiseCanExecuteChanged();
            CancelChangeCommand.RaiseCanExecuteChanged();
        }

        /// <summary>
        /// In Case of that the User Email Is not Exist Before 
        /// So this method Will Continue in Saving Operation
        /// </summary>
        /// <param name="sender">value of Sender</param>
        /// <param name="e">Value Of e</param>
        private void muserRegisterModel_GetCheckIsEmailExistComplete(object sender, eNegEntityResultArgs<User> e)
        {
            if (!e.HasError)
            {
                if (e.Results.Count() != 0)
                {
                    this.CurrentUser.ValidationErrors.Add(new ValidationResult(Resources.EmailRepeated, new string[] { "EmailAddress" }));
                    IsBusy = false;
                    return;
                }
                else
                {
                    #region → Preparing For Confirmation Mail   .

                    this.mOperationString = HashHelper.ComputeSaltedHash(this.CurrentUser.UserID.ToString()) + HashHelper.ComputeSaltedHash(this.CurrentUser.EmailAddress);

                    //To Solve Problem in Automation
                    this.mOperationString = this.mOperationString.Replace("+", "P");

                    //Add New Record In UserOperation
                    this.mCurrentUser.UserOperations.Add(new UserOperation() { OperationID = Guid.NewGuid(), UserID = this.CurrentUser.UserID, OperationString = this.mOperationString, Type = eNegConstant.UserOperations.Confiramtion });

                    #endregion Preparing For Confirmation Mail

                    #region → Add Role For The User             .
                    this.mCurrentUser.UserRole.Add(new UserRole() { RoleID = eNegConstant.Roles.User, UserID = this.CurrentUser.UserID, UserRoleID = Guid.NewGuid() });
                    #endregion

                    mUserRegisterModel.SaveChangesAsync();
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
        private void mUserRegisterModel_SaveChangesComplete(object sender, SubmitOperationEventArgs e)
        {
            if (!e.HasError)
            {
                if ((e.SubmitOp.ChangeSet.AddedEntities.Count >= 1) &&
                    (e.SubmitOp.ChangeSet.AddedEntities.Where(s => s.GetType().Equals(typeof(User))).FirstOrDefault() is User))
                {
                    #region → Sending Mail To The User              .

                    this.mUserRegisterModel.SendConfiramtionMail(this.CurrentUser.EmailAddress, this.CurrentUser.FirstName, this.CurrentUser.LastName, this.mOperationString);

                    #endregion

                    #region → Sending Mails for Organization Owners .

                    List<Entity> lstAddedUserOrganization = e.SubmitOp.ChangeSet.AddedEntities.Where(s => s.GetType().Equals(typeof(UserOrganization))).ToList();
                    this.ManageUserOrgViewModel.PrepairQueueMails(lstAddedUserOrganization);

                    #endregion
                }

                // In case of Updating Lock=true
                else if ((e.SubmitOp.ChangeSet.ModifiedEntities.Count >= 1) &&
                    (e.SubmitOp.ChangeSet.ModifiedEntities.Where(s => s.GetType().Equals(typeof(User))).FirstOrDefault() is User))
                {
                    ShowSuccessMessage();
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
        private void mUserRegisterModel_SendingMailCompleted(InvokeOperation e)
        {
            if (!e.HasError)
            {
                // Lock the User again
                this.CurrentUser.Locked = true;

                // Save new Lock
                mUserRegisterModel.SaveChangesAsync();
            }

            else
            {
                IsBusy = false;

                ShowSuccessMessage();

                e.MarkErrorAsHandled();
                eNegMessanger.RaiseErrorMessage.Send(e.Error);
            }

        }

        #endregion

        #region → Commands       .

        /// <summary>
        /// Gets the focus control and execute command.
        /// </summary>
        /// <value>The focus control and execute command.</value>
        public RelayCommand<KeyEventArgs> FocusControlAndExecuteCommand
        {
            get
            {
                if (mFocusControlAndExecuteCommand == null)
                {
                    mFocusControlAndExecuteCommand = new RelayCommand<KeyEventArgs>((e) =>
                        {
                            try
                            {
                                if (e.Key == System.Windows.Input.Key.Enter)
                                {
                                    Grid container = null;
                                    //if you press enter when was focus on TextBox
                                    if (e.OriginalSource is TextBox)
                                    {
                                        container = (((e.OriginalSource as System.Windows.Controls.TextBox).Parent as Grid).Parent as WaterMarkTextBox).Parent as Grid;
                                    }
                                    //if you press enter when was focus on PasswordBox
                                    else
                                    {
                                        container = (((e.OriginalSource as System.Windows.Controls.PasswordBox).Parent as Grid).Parent as WaterMarkPasswordBox).Parent as Grid;
                                    }

                                    foreach (var element in container.Children)
                                    {
                                        if (element is Panel)
                                        {
                                            foreach (var subelement in (element as Panel).Children)
                                            {
                                                if (subelement is Button)
                                                {
                                                    eNegMessanger.BuildControl.Send((subelement as Button).Name);
                                                    break;
                                                }
                                            }
                                        }
                                        else if (element is Button)
                                        {
                                            eNegMessanger.BuildControl.Send((element as Button).Name);
                                            break;
                                        }
                                    }

                                    Deployment.Current.Dispatcher.BeginInvoke(() =>
                                        {
                                            SubmitChangeCommand.Execute(null);
                                        });
                                }
                            }
                            catch (Exception ex)
                            {
                                eNegMessanger.RaiseErrorMessage.Send(ex);
                            }
                        }, (e) => !mUserRegisterModel.IsBusy);
                }
                return mFocusControlAndExecuteCommand;
            }
        }

        /// <summary>
        /// User Save changes via Calling SubmitChangesMessage so It call
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
                            if (!mUserRegisterModel.IsBusy)
                            {
                                eNegMessanger.SubmitChangesMessage.Send();
                            }
                        }
                        catch (Exception ex)
                        {
                            // notify user if there is any error
                            eNegMessanger.RaiseErrorMessage.Send(ex);
                        }
                    }
                    , () => this.mUserRegisterModel.HasChanges && !mUserRegisterModel.IsBusy);
                }
                return mSubmitChangeCommand;
            }
        }

        /// <summary>
        /// User Cancelling changes via Calling CancelChangesMessage so It call
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

                            if (!mUserRegisterModel.IsBusy)
                            {
                                #region  "In case of Quick Register Then Return To Login"

                                if (this.IsQuickRegister)
                                {

                                    this.CurrentUser.EmailAddress = null;
                                    this.CurrentUser.Password = null;
                                    this.CurrentUser.PasswordConfirmation = null;
                                    this.CurrentUser.ValidationErrors.Clear();

                                    eNegMessanger.FlippMessage.Send();
                                    return;
                                }
                                #endregion

                                // if confirmed, cancel this User
                                eNegMessanger.CancelChangesMessage.Send();
                                eNegMessanger.ChangeScreenMessage.Send(ViewTypes.LoginView);
                                this.ManageUserOrgViewModel.RejectChanges();


                            }
                        }
                        catch (Exception ex)
                        {
                            // notify user if there is any error
                            eNegMessanger.RaiseErrorMessage.Send(ex);
                        }
                    }
                    , () => this.mUserRegisterModel.HasChanges && !mUserRegisterModel.IsBusy);
                }
                return mCancelChangeCommand;
            }
        }

        #endregion

        #region → Methods        .

        #region → Private        .


        /// <summary>
        /// Shows the success message.
        /// </summary>
        private void ShowSuccessMessage()
        {
            IsBusy = false;

            eNegMessage Message = new eNegMessage(Resources.NewUserCreatedText);
            
            //this is flag to indicate which one will recieve this sent
            Message.ViewName = ViewTypes.QuickRegisterView;

            Message.ShowMessageCompleted += (s, args) =>
            {
                #region  "In case of Quick Register Then Return To Login By Flipping"

                if (this.IsQuickRegister)
                {
                    eNegMessanger.FlippMessage.Send(ViewTypes.LoginView);
                    this.CurrentUser = this.mUserRegisterModel.AddNewUser(true);
                    return;
                }

                #endregion

                //Check if thier any mails will be send by the Organization
                if (this.ManageUserOrgViewModel.QueueMailsCount <= 0)
                {
                    eNegMessanger.ChangeScreenMessage.Send(ViewTypes.LoginView);
                }
            };

            eNegMessanger.SendCustomMessage.Send(Message);
        }

        /// <summary>
        /// Executed when SubmitChangesMessage is recieved
        /// </summary>
        /// <param name="ignore">ignore</param>
        private void OnSubmitChangesMessage(eNegMessanger.OperationType ignore)
        {
            if (this.CurrentUser != null)
            {

                // this should trigger validation even if the Title is not changed and is null
                if (string.IsNullOrWhiteSpace(this.CurrentUser.EmailAddress))
                    this.CurrentUser.EmailAddress = string.Empty;

                if (string.IsNullOrWhiteSpace(this.CurrentUser.Password))
                    this.CurrentUser.Password = string.Empty;

                if (string.IsNullOrWhiteSpace(this.CurrentUser.PasswordConfirmation))
                    this.CurrentUser.PasswordConfirmation = string.Empty;

                #region "Validation on FName,LName For Full REgister Only"

                if (!IsQuickRegister)
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
                #endregion "Validation on FName,LName For Full REgister Only"

                if (this.mCurrentUser.TryValidateObject()
                    && this.mCurrentUser.TryValidateProperty("EmailAddress")
                    && this.mCurrentUser.TryValidateProperty("Password")
                    && this.mCurrentUser.TryValidateProperty("PasswordConfirmation"))
                {
                    IsBusy = true;
                    CheckIsEmailExist(this.mCurrentUser);

                }
            }
        }

        /// <summary>
        ///Executed when CancelChangesMessage is recieved
        /// </summary>
        /// <param name="ignore">ignore</param>
        private void OnCancelChangesMessage(Boolean ignore)
        {
            this.mUserRegisterModel.RejectChanges();
        }

        #endregion

        #region → Public         .

        #region → Loading Lookup Tables   .


        /// <summary>
        /// Loading table (Culture).
        /// </summary>
        public void GetCultureAsync()
        {
            mUserRegisterModel.GetCultureAsync();
        }


        /// <summary>
        /// Loading table (AccountType).
        /// </summary>
        public void GetAccountTypeAsync()
        {
            mUserRegisterModel.GetAccountTypeAsync();
        }

        /// <summary>
        /// Loading table (Country).
        /// </summary>
        public void GetCountryAsync()
        {
            mUserRegisterModel.GetCountryAsync();
        }

        /// <summary>
        /// Loading table (PreferedLanguage).
        /// </summary>
        public void GetPreferedLanguageAsync()
        {
            mUserRegisterModel.GetPreferedLanguageAsync();
        }

        /// <summary>
        /// Loading table (SecurityQuestion).
        /// </summary>
        public void GetSecurityQuestionAsync()
        {
            mUserRegisterModel.GetSecurityQuestionAsync();
        }

        /// <summary>
        /// Gets all Applicaions.
        /// </summary>
        public void GetAllApplicaions()
        {
            mUserRegisterModel.GetAllApplicaions();
        }

        #endregion

        /// <summary>
        /// Check In the Current User Email is already registered before Or Not.
        /// </summary>
        /// <param name="user">Vlaue Of The Current User</param>
        public void CheckIsEmailExist(User user)
        {
            mUserRegisterModel.CheckIsEmailExist(user);
        }

        /// <summary>
        /// Adding New Record (User) in the Current Context.
        /// </summary>
        public User AddNewUser(bool SetInContext)
        {
            return mUserRegisterModel.AddNewUser(SetInContext);
        }

        #region "ICleanup interface implementation"
        /// <summary>
        /// Cleanups this instance.
        /// </summary>
        public override void Cleanup()
        {
            // unregister all events
            mUserRegisterModel.SaveChangesComplete -= new EventHandler<SubmitOperationEventArgs>(mUserRegisterModel_SaveChangesComplete);
            mUserRegisterModel.PropertyChanged -= new PropertyChangedEventHandler(mUserRegisterModel_PropertyChanged);
            mUserRegisterModel.GetAccountTypeComplete -= new EventHandler<eNegEntityResultArgs<AccountType>>(_userRegisterModel_GetAccountTypeComplete);
            mUserRegisterModel.GetCountryComplete -= new EventHandler<eNegEntityResultArgs<Country>>(_userRegisterModel_GetCountryComplete);
            mUserRegisterModel.GetPreferedLanguageComplete -= new EventHandler<eNegEntityResultArgs<PreferedLanguage>>(_userRegisterModel_GetPreferedLanguageComplete);
            mUserRegisterModel.GetSecurityQuestionComplete -= new EventHandler<eNegEntityResultArgs<SecurityQuestion>>(_userRegisterModel_GetSecurityQuestionComplete);
            mUserRegisterModel.GetCheckIsEmailExistComplete -= new EventHandler<eNegEntityResultArgs<User>>(muserRegisterModel_GetCheckIsEmailExistComplete);
            mUserRegisterModel.GetAllApplicationComplete -= new EventHandler<eNegEntityResultArgs<citPOINT.eNeg.Data.Web.Application>>(mUserRegisterModel_GetAllApplicationComplete);

            // unregister any messages for this ViewModel
            base.Cleanup();

            // unregister any messages for this ViewModel
            // Cleanup itself
            Messenger.Default.Unregister(this);

            this.ManageUserOrgViewModel.Cleanup();

            //to Clear Context
            this.mUserRegisterModel.CleanUp();
        }
        #endregion "ICleanup interface implementation"

        #endregion

        #endregion
    }
}