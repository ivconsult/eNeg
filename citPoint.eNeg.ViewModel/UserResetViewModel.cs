#region → Usings   .
using citPOINT.eNeg.Common;
using citPOINT.eNeg.Data.Web;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.ServiceModel.DomainServices.Client;
using System.Windows.Browser;
using HashHelper = citPOINT.eNeg.Common.HashHelper;
using System.IO.IsolatedStorage;

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
    #region → Using  MEF to export UserResetViewModel
    /// <summary>
    /// This class is to Reset User Login Data.
    /// </summary>
    [Export(ViewModelTypes.UserResetViewModel)]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    #endregion
    public class UserResetViewModel : ViewModelBase
    {

        #region → Fields         .

        private IUserRegisterModel mUserRegisterModel;
        private SessionContext mSessionContext;
        private User mCurrentUser;
        private string mOperationString;
        private string mNewOperationString;
        public string mOrignalEmailAddress = string.Empty;
        private bool mEnableTextBox;
        private bool mIsBusy;
        private RelayCommand mSubmitChangeCommand;
        private RelayCommand mCancelChangeCommand;

        #endregion Fields

        #region → Properties     .

        /// <summary>
        /// string From value Of UserID and User Mail
        /// </summary>
        public string OperationString
        {
            get
            {
                return mOperationString;
            }
            set
            {
                this.mOperationString = value;
                CheckUserRequestReset(value);
            }
        }

        /// <summary>
        /// Indicating That The Current User Is In Progress
        /// </summary>
        public bool EnableTextBox
        {
            get
            {
                return mEnableTextBox;
            }
            set
            {
                mEnableTextBox = value;
                this.RaisePropertyChanged("EnableTextBox");
            }
        }

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
                EnableTextBox = !mIsBusy;
                this.RaisePropertyChanged("IsBusy");
            }
        }

        /// <summary>
        /// Current User Data
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
                }
            }
        }

        #endregion

        #region → Constructors   .
        /// <summary>
        /// Initializes a new instance of the <see cref="UserResetViewModel"/> class.
        /// </summary>
        /// <param name="userRegisterModel">The user register model.</param>
        [ImportingConstructor]
        public UserResetViewModel(IUserRegisterModel userRegisterModel)
        {
            mUserRegisterModel = userRegisterModel;

            if (mUserRegisterModel.HasChanges)
                mUserRegisterModel.RejectChanges();

            try
            {
                if (!AppConfigurations.IsRunningOutOfBrowser)
                    mSessionContext = new SessionContext();
            }
            catch (Exception ex)
            {
            }

            // initiate a new User
            CurrentUser = mUserRegisterModel.AddNewUser(false);

            this.IsBusy = true;


            #region → Set up event handling           .

            mUserRegisterModel.SaveChangesComplete += new EventHandler<SubmitOperationEventArgs>(mUserRegisterModel_SaveChangesComplete);
            mUserRegisterModel.PropertyChanged += new PropertyChangedEventHandler(mUserRegisterModel_PropertyChanged);
            mUserRegisterModel.GetCheckIsEmailExistComplete += new EventHandler<eNegEntityResultArgs<User>>(muserRegisterModel_GetCheckIsEmailExistComplete);
            mUserRegisterModel.GetCheckUserRequestResetComplete += new EventHandler<eNegEntityResultArgs<User>>(muserRegisterModel_GetCheckUserRequestResetComplete);
            mUserRegisterModel.DeleteUserOperationByUserIDComplete += new EventHandler<eNegEntityResultArgs<User>>(mUserRegisterModel_DeleteUserOperationByUserIDComplete);
            mUserRegisterModel.SendingMailCompleted += new Action<System.ServiceModel.DomainServices.Client.InvokeOperation>(mUserRegisterModel_SendingMailCompleted);

            #endregion Set up event handling

            #region → Registering Messages            .

            // register for SubmitChangesMessage
            eNegMessanger.SubmitChangesMessage.Register(this, OnSubmitChangesMessage);
            // register for CancelChangesMessage
            eNegMessanger.CancelChangesMessage.Register(this, OnCancelChangesMessage);

            #endregion Registering Messages
        }

        #endregion Constructor

        #region → Event Handlers .

        /// <summary>
        /// if any data values changed Then this Property Fired 
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">e</param>
        private void mUserRegisterModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals("HasChanges"))
            {
                SubmitChangeCommand.RaiseCanExecuteChanged();
                CancelChangeCommand.RaiseCanExecuteChanged();
            }
        }

        /// <summary>
        /// Check If The Current User Have Request Mail or Not.
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        void muserRegisterModel_GetCheckUserRequestResetComplete(object sender, eNegEntityResultArgs<User> e)
        {
            if (!e.HasError)
            {
                User found = e.Results.FirstOrDefault();

                this.IsBusy = false;

                if (e.Results.Count() == 0)
                {
                    #region "Show Message That The User Must Request Reset First"

                    eNegMessage Message = new eNegMessage(Resources.RequestReset);
                    Message.ShowMessageCompleted += (s, args) =>
                                                {
                                                    eNegMessanger.ChangeScreenMessage.Send(ViewTypes.LoginView);
                                                };
                    eNegMessanger.SendCustomMessage.Send(Message);
                    return;
                    #endregion
                }
                else
                {
                    this.mOrignalEmailAddress = found.EmailAddress;
                    this.CurrentUser.UserID = found.UserID;
                    this.CurrentUser.EmailAddress = found.EmailAddress;
                }
            }
            else
            {
                // notify user if there is any error
                eNegMessanger.RaiseErrorMessage.Send(e.Error);
            }
        }

        /// <summary>
        /// Return Handler OF The method that Check is mail Exist or not
        /// Step 1 (Check Mail)
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        void muserRegisterModel_GetCheckIsEmailExistComplete(object sender, eNegEntityResultArgs<User> e)
        {
            if (!e.HasError)
            {
                if (e.Results.Count() != 0)
                {
                    this.CurrentUser.ValidationErrors.Add(new System.ComponentModel.DataAnnotations.ValidationResult(Resources.EmailRepeated, new string[] { "EmailAddress" }));
                    IsBusy = false;
                    return;
                }
                else
                {
                    //If Not Exist Then Get The Current User
                    DeleteUserOperationByUserID(this.CurrentUser.UserID);
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
        /// Step 2 (After Deleing User Operations And Get The Current User From Database)
        /// </summary>
        /// <param name="sender">Value Of Sender</param>
        /// <param name="e">Value Of e</param>
        void mUserRegisterModel_DeleteUserOperationByUserIDComplete(object sender, eNegEntityResultArgs<User> e)
        {
            if (!e.HasError)
            {
                User found = e.Results.FirstOrDefault();

                if (e.Results.Count() == 0)
                {
                    this.CurrentUser.ValidationErrors.Add(new System.ComponentModel.DataAnnotations.ValidationResult("This user ID is not exits", new string[] { "EmailAddress" }));
                    IsBusy = false;
                    return;
                }
                else
                {
                    found.EmailAddress = this.CurrentUser.EmailAddress;
                    found.Password = this.CurrentUser.Password;
                    found.PasswordConfirmation = this.CurrentUser.PasswordConfirmation;

                    //for to be hashed in case of update user
                    found.NewPassword = this.CurrentUser.Password;
                    found.NewPasswordConfirmation = this.CurrentUser.PasswordConfirmation;

                    found.Reset = false;

                    #region "Preparing For Confirmation Mail In Case Of Changing The User Mail"

                    if (!string.IsNullOrEmpty(this.mOrignalEmailAddress) &&
                        this.CurrentUser.EmailAddress.ToLower() != this.mOrignalEmailAddress.ToLower())
                    {
                        //Lock Unit He Activate with New Mail
                        found.Locked = true;

                        this.mNewOperationString = HashHelper.ComputeSaltedHash(this.CurrentUser.UserID.ToString()) + HashHelper.ComputeSaltedHash(this.CurrentUser.EmailAddress);

                        //To Solve Problem in Automation
                        this.mNewOperationString = this.mNewOperationString.Replace("+", "P");

                        //Add New Record In UserOperation
                        found.UserOperations.Add(new UserOperation() { OperationID = Guid.NewGuid(), UserID = this.CurrentUser.UserID, OperationString = this.mNewOperationString, Type = eNegConstant.UserOperations.Confiramtion });
                    }
                    else
                    {
                        //Already All User Operation Was Deleted From Step 1
                    }

                    #endregion

                    this.mUserRegisterModel.SaveChangesAsync();
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
        /// Event Fire as a callback For Function SaveChanes
        /// Step 3 (Saving Actualy In User Table)
        /// </summary>
        /// <param name="sender">Value Of Sender</param>
        /// <param name="e">Value Of e</param>
        private void mUserRegisterModel_SaveChangesComplete(object sender, SubmitOperationEventArgs e)
        {
            if (!e.HasError)
            {
                if ((e.SubmitOp.ChangeSet.ModifiedEntities.Count >= 1))
                {
                    ClearCash();

                    #region "Sending Mail To The User In Case He Change His Mail"
                    if (this.CurrentUser.EmailAddress.ToLower() != this.mOrignalEmailAddress.ToLower())
                    {
                        this.mUserRegisterModel.SendConfiramtionMail(this.CurrentUser.EmailAddress, this.CurrentUser.FirstName, this.CurrentUser.LastName, this.mNewOperationString);
                    }
                    #endregion

                    else
                    {
                        ShowMessage();
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
        void mUserRegisterModel_SendingMailCompleted(InvokeOperation e)
        {
            IsBusy = false;
            ShowMessage();
            if (e.HasError)
            {
                e.MarkErrorAsHandled();
                eNegMessanger.RaiseErrorMessage.Send(e.Error);
            }
        }

        #endregion Event Handlers

        #region → Commands       .

        /// <summary>
        /// User Save changes To Database via Calling SubmitChangesMessage so It call
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
                    , () => true);
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

                                // if confirmed, cancel this User
                                eNegMessanger.CancelChangesMessage.Send();
                                eNegMessanger.ChangeScreenMessage.Send(ViewTypes.LoginView);
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
                return mCancelChangeCommand;
            }
        }

        #endregion Commands

        #region → Methods        .

        #region → Private        .


        /// <summary>
        /// If One Send Message OF SumbitChanes then This Method Will Handle That Request
        /// and Validate the Current User
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

                if (this.mCurrentUser.TryValidateObject()
                   && this.mCurrentUser.TryValidateProperty("EmailAddress")
                   && this.mCurrentUser.TryValidateProperty("Password")
                   && this.mCurrentUser.TryValidateProperty("PasswordConfirmation"))
                {
                    IsBusy = true;
                    this.mUserRegisterModel.CheckIsEmailExist(this.mCurrentUser);
                }
            }
        }

        /// <summary>
        /// Show Messagebox that the Current Login Has been Reset
        /// </summary>
        private void ShowMessage()
        {
            #region "Show Message"
            string MessageString = Resources.ResetSuccess;

            //In case Of you change The Mail
            if (this.CurrentUser.EmailAddress.ToLower() != this.mOrignalEmailAddress.ToLower())
            {
                MessageString = Resources.NewUserCreatedText;
            }

            eNegMessage Message = new eNegMessage(MessageString);

            Message.ShowMessageCompleted += (s, args) =>
                {
                    HtmlPage.Window.Navigate(new Uri(AppConfigurations.HostBaseAddress + "Default.aspx", UriKind.Absolute));
                };

            eNegMessanger.SendCustomMessage.Send(Message);

            #endregion
        }

        /// <summary>
        /// User Cancelling changes via Calling CancelChangesMessage so It call
        /// OnCancelChangesMessage Method.
        /// </summary>
        private void OnCancelChangesMessage(Boolean ignore)
        {
            this.mUserRegisterModel.RejectChanges();
        }


        /// <summary>
        /// Clears the cash.
        /// </summary>
        private void ClearCash()
        {
            try
            {
                IsolatedStorageSettings appSettings = IsolatedStorageSettings.ApplicationSettings;
                appSettings.Remove("UserName");
                appSettings.Save();


                if (!AppConfigurations.IsRunningOutOfBrowser)
                {
                    mSessionContext.SetSessionValue("SessionUser", string.Empty);
                }
            }
            catch (Exception ex)
            {
                // notify user if there is any error
                eNegMessanger.RaiseErrorMessage.Send(ex);
            }
        }
        #endregion

        #region → Public         .

        /// <summary>
        /// For Deletign All Related Records In UserOperation Table
        /// And That in case if one Complete his Reset Login Operation
        /// and He Had not Change His EmailAddress.
        /// </summary>
        /// <param name="UserID">value of The UserID</param>
        public void DeleteUserOperationByUserID(Guid UserID)
        {
            mUserRegisterModel.DeleteUserOperationByUserID(UserID);
        }

        /// <summary>
        /// this function is To Check if the Current Lnk is valid or not
        /// </summary>
        /// <param name="OperationString">value of query string from your mail</param>
        public void CheckUserRequestReset(String OperationString)
        {
            mUserRegisterModel.CheckUserRequestReset(OperationString);
        }


        #region ICleanup interface implementation
        /// <summary>
        /// Cleanups this instance.
        /// </summary>
        public override void Cleanup()
        {
            // unregister all events
            mUserRegisterModel.SaveChangesComplete -= new EventHandler<SubmitOperationEventArgs>(mUserRegisterModel_SaveChangesComplete);
            mUserRegisterModel.PropertyChanged -= new PropertyChangedEventHandler(mUserRegisterModel_PropertyChanged);
            mUserRegisterModel.GetCheckIsEmailExistComplete -= new EventHandler<eNegEntityResultArgs<User>>(muserRegisterModel_GetCheckIsEmailExistComplete);
            mUserRegisterModel.GetCheckUserRequestResetComplete -= new EventHandler<eNegEntityResultArgs<User>>(muserRegisterModel_GetCheckUserRequestResetComplete);
            mUserRegisterModel.DeleteUserOperationByUserIDComplete -= new EventHandler<eNegEntityResultArgs<User>>(mUserRegisterModel_DeleteUserOperationByUserIDComplete);
            mUserRegisterModel.SendingMailCompleted -= new Action<System.ServiceModel.DomainServices.Client.InvokeOperation>(mUserRegisterModel_SendingMailCompleted);

            // unregister any messages for this ViewModel
            base.Cleanup();

            Messenger.Default.Unregister(this);
        }

        #endregion ICleanup interface implementation

        #endregion

        #endregion
    }
}