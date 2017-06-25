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
    #region → Using  MEF to export UserRequestResetViewModel
    /// <summary>
    /// this class is to Request Login data (user Data) reset.
    /// </summary>
    [Export(ViewModelTypes.UserRequestResetViewModel)]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    #endregion
    public class UserRequestResetViewModel : ViewModelBase
    {
        #region → Fields         .
        private IUserRegisterModel mUserRegisterModel;
        private User mCurrentUser;
        private string mOperationString;
        private bool mIsBusy;
        private RelayCommand mSubmitChangeCommand = null;
        private RelayCommand mCancelChangeCommand = null;
        #endregion

        #region → Properties     .

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
                }
            }
        }

        #endregion Properties

        #region → Constructors   .

        /// <summary>
        /// Initializes a new instance of the <see cref="UserRequestResetViewModel"/> class.
        /// </summary>
        /// <param name="userRegisterModel">The user register model.</param>
        [ImportingConstructor]
        public UserRequestResetViewModel(IUserRegisterModel userRegisterModel)
        {
            mUserRegisterModel = userRegisterModel;

            // initiate a new User
            CurrentUser = new User();

            #region → Set up event handling       .

            mUserRegisterModel.SaveChangesComplete += new EventHandler<SubmitOperationEventArgs>(mUserRegisterModel_SaveChangesComplete);
            mUserRegisterModel.PropertyChanged += new PropertyChangedEventHandler(mUserRegisterModel_PropertyChanged);
            mUserRegisterModel.GetCheckIsEmailExistComplete += new EventHandler<eNegEntityResultArgs<User>>(muserRegisterModel_GetCheckIsEmailExistComplete);
            mUserRegisterModel.GetResetRequestComplete += new EventHandler<eNegEntityResultArgs<User>>(muserRegisterModel_GetResetRequestComplete);
            mUserRegisterModel.SendingMailCompleted += new Action<InvokeOperation>(mUserRegisterModel_SendingMailCompleted);

            #endregion Set up event handling

            #region → Register Messages            .

            // register for SubmitChangesMessage
            eNegMessanger.SubmitChangesMessage.Register(this, OnSubmitChangesMessage);
            // register for CancelChangesMessage
            eNegMessanger.CancelChangesMessage.Register(this, OnCancelChangesMessage);

            #endregion Register Messages
        }

        #endregion Constructor

        #region → Event Handlers .

        /// <summary>
        /// return of Calling SavingChanges indicating that the save operation Completed.
        /// </summary>
        /// <param name="sender">Value of Sender</param>
        /// <param name="e">Value of e</param>
        private void mUserRegisterModel_SaveChangesComplete(object sender, SubmitOperationEventArgs e)
        {
            if (!e.HasError)
            {
                #region → Sending Mail To The User
                this.mUserRegisterModel.SendResetMail(this.CurrentUser.EmailAddress, this.CurrentUser.FirstName, this.CurrentUser.LastName, this.mOperationString);
                #endregion
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
            IsBusy = false;

            #region → Show Message              .

            eNegMessage Message = new eNegMessage(Resources.YouNewMail);
            Message.ShowMessageCompleted += (s, args) =>
            {
                eNegMessanger.ChangeScreenMessage.Send(ViewTypes.LoginView);
                eNegMessanger.FlippMessage.Send(ViewTypes.ClosePopupView);
            };
            eNegMessanger.SendCustomMessage.Send(Message);

            #endregion

            if (e.HasError)
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
        private void mUserRegisterModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals("HasChanges"))
            {
                SubmitChangeCommand.RaiseCanExecuteChanged();
                CancelChangeCommand.RaiseCanExecuteChanged();
            }
        }

        /// <summary>
        /// Step 2 In Save Chain
        /// Check if the Current Email Is 
        /// Already Exists or not and if this Account Is Active or not.
        /// </summary>
        /// <param name="sender">value of Sender</param>
        /// <param name="e">value of e</param>
        void muserRegisterModel_GetCheckIsEmailExistComplete(object sender, eNegEntityResultArgs<User> e)
        {
            if (!e.HasError)
            {
                User found = e.Results.FirstOrDefault<User>();

                if (e.Results.Count() == 0)
                {
                    this.CurrentUser.ValidationErrors.Add(new ValidationResult(Resources.EmailNotFound, new string[] { "EmailAddress" }));
                    IsBusy = false;
                    return;
                }
                else
                {
                    if (mUserRegisterModel.HasChanges)
                    {
                        mUserRegisterModel.RejectChanges();
                    }

                    #region → In case that The Current User Is Not Active   .
                    if (found.Locked)
                    {
                        this.CurrentUser.ValidationErrors.Add(new ValidationResult(Resources.EmailNotActive, new string[] { "EmailAddress" }));
                        IsBusy = false;
                        return;
                    }
                    #endregion

                    //Reset = true;
                    UpdateReset(found.UserID);
                }
            }
            else
            {
                // notify user if there is any error
                eNegMessanger.RaiseErrorMessage.Send(e.Error);
            }
        }

        /// <summary>
        /// Step 3 In Save Chain
        /// After Complete Setting Reset Flag
        /// </summary>
        /// <param name="sender">value of Sender</param>
        /// <param name="e">vale of e</param>
        void muserRegisterModel_GetResetRequestComplete(object sender, eNegEntityResultArgs<User> e)
        {
            if (!e.HasError)
            {
                if ((e.Results.Count() >= 1))
                {

                    this.mCurrentUser = e.Results.FirstOrDefault();

                    if (mUserRegisterModel.HasChanges)
                        mUserRegisterModel.RejectChanges();

                    #region → Preparing For Reset Mail   .

                    this.mOperationString = HashHelper.ComputeSaltedHash(this.CurrentUser.UserID.ToString()) + HashHelper.ComputeSaltedHash(this.CurrentUser.EmailAddress);

                    //To Solve Problem in Automation
                    this.mOperationString = this.mOperationString.Replace("+", "P");

                    //Add New Record In UserOperation
                    this.mCurrentUser.UserOperations.Add(new UserOperation() { OperationID = Guid.NewGuid(), UserID = this.CurrentUser.UserID, OperationString = this.mOperationString, Type = eNegConstant.UserOperations.ResetRequest });

                    #endregion

                    mUserRegisterModel.SaveChangesAsync();
                }
            }
            else
            {
                // notify user if there is any error
                eNegMessanger.RaiseErrorMessage.Send(e.Error);
            }
        }

        #endregion Event Handlers

        #region → Commands       .

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
                                eNegMessanger.CancelChangesMessage.Send();
                                eNegMessanger.ChangeScreenMessage.Send(ViewTypes.LoginView);
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
                return mCancelChangeCommand;
            }
        }

        #endregion Commands

        #region → Methods        .

        #region → Private        .

        /// <summary>
        /// Step 1 In Save Chain
        /// </summary>
        /// <param name="ignore">if set to <c>true</c> [ignore].</param>
        private void OnSubmitChangesMessage(eNegMessanger.OperationType ignore)
        {

            if (this.CurrentUser != null)
            {
                // this should trigger validation even if the Title is not changed and is null
                if (string.IsNullOrWhiteSpace(this.CurrentUser.EmailAddress))
                    this.CurrentUser.EmailAddress = string.Empty;


                if (this.mCurrentUser.TryValidateProperty("EmailAddress"))
                {
                    IsBusy = true;
                    this.mUserRegisterModel.CheckIsEmailExist(this.mCurrentUser);

                }
            }
        }

        /// <summary>
        /// User Cancelling changes via Calling CancelChangesMessage so It call
        /// OnCancelChangesMessage Method.
        /// </summary>
        private void OnCancelChangesMessage(Boolean ignore)
        {
            this.mUserRegisterModel.RejectChanges();
        }

        #endregion

        #region → Public         .

        /// <summary>
        /// Update User as Reset Request.
        /// Update Reset Flag By True.
        /// </summary>
        /// <param name="UserID">Vlaue Of Current User ID</param>
        public void UpdateReset(Guid? UserID)
        {
            mUserRegisterModel.UpdateReset(UserID);
        }

        #region ICleanup interface implementation

        /// <summary>
        /// Clean All Memory And unregister all events
        /// Dispose
        /// </summary>
        public override void Cleanup()
        {
            // unregister all events

            mUserRegisterModel.SaveChangesComplete -= new EventHandler<SubmitOperationEventArgs>(mUserRegisterModel_SaveChangesComplete);
            mUserRegisterModel.PropertyChanged -= new PropertyChangedEventHandler(mUserRegisterModel_PropertyChanged);
            mUserRegisterModel.GetCheckIsEmailExistComplete -= new EventHandler<eNegEntityResultArgs<User>>(muserRegisterModel_GetCheckIsEmailExistComplete);
            mUserRegisterModel.GetResetRequestComplete -= new EventHandler<eNegEntityResultArgs<User>>(muserRegisterModel_GetResetRequestComplete);
            mUserRegisterModel.SendingMailCompleted -= new Action<InvokeOperation>(mUserRegisterModel_SendingMailCompleted);

            // unregister any messages for this ViewModel
            base.Cleanup();
        }
        #endregion ICleanup interface implementation

        #endregion Public

        #endregion Methods
    }
}