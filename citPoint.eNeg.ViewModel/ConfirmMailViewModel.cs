#region → Usings   .
using citPOINT.eNeg.Common;
using citPOINT.eNeg.Data.Web;
using GalaSoft.MvvmLight;
using System;
using System.ComponentModel.Composition;
using System.Linq;
using System.Windows.Browser;
using System.IO.IsolatedStorage;

#endregion

#region → History  .

/* Date         User            Change
 * 
 * 03.08.10     M.Wahab     creation
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

    #region → Using  MEF to export ConfirmMailViewModel
    /// <summary>
    /// This class is used to activate the user account.
    /// </summary>
    [Export(ViewModelTypes.ConfirmMailViewModel)]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    #endregion
    public class ConfirmMailViewModel : ViewModelBase
    {
        #region → Fields         .
        private IConfirmMailModel mConfirmMailModel;
        private string mOperationString;
        private byte mOperationStringType;
        private eNegMessage Message;
        private bool NeedOrganization = false;
        private User NewOwner;
        private User OriginalOwner;
        private MailHelper mMailHelper;
        #endregion

        #region → Properties     .

        /// <summary>
        /// Gets or sets the mail helper.
        /// </summary>
        /// <value>The mail helper.</value>
        public MailHelper MailHelper
        {
            get
            {
                if (mMailHelper == null)
                {
                    mMailHelper = new MailHelper();
                }
                return mMailHelper;
            }
            set
            {
                mMailHelper = value;
            }
        }

        /// <summary>
        /// Gets or sets the type of the operation string.
        /// </summary>
        /// <value>The type of the operation string.</value>
        public byte OperationStringType
        {
            get
            {
                return mOperationStringType;
            }
            set
            {
                this.mOperationStringType = value;
            }
        }

        /// <summary>
        /// string From value Of UserID and User Mail
        /// </summary>
        /// <value>The operation string.</value>
        public string OperationString
        {
            get
            {
                return mOperationString;
            }
            set
            {
                this.mOperationString = value;

                if (!mConfirmMailModel.IsBusy)
                {
                    #region "Activate the User Account"
                    UpdateUserByConfirmMail(value, OperationStringType);
                    #endregion
                }
            }
        }

        #endregion

        #region → Constructors   .

        /// <summary>
        /// Initializes a new instance of the <see cref="ConfirmMailViewModel"/> class.
        /// </summary>
        /// <param name="ConfirmMailModel">The confirm mail model.</param>
        [ImportingConstructor]
        public ConfirmMailViewModel(IConfirmMailModel ConfirmMailModel)
        {
            mConfirmMailModel = ConfirmMailModel;

            #region → Set up event handling
            mConfirmMailModel.UpdateUserByConfirmMailComplete += new EventHandler<eNegEntityResultArgs<User>>(mUserRegisterModel_UpdateUserByConfirmMailComplete);
            mConfirmMailModel.GetOrganizationByUserIDComplete += new EventHandler<eNegEntityResultArgs<Organization>>(mConfirmMailModel_GetOrganizationByUserIDComplete);
            #endregion
        }


        #endregion Constructor

        #region → Event Handlers .

        /// <summary>
        /// Now the User is ready and activated to be used For Login
        /// </summary>
        /// <param name="sender">Value of Sender</param>
        /// <param name="e">Value of e</param>
        private void mUserRegisterModel_UpdateUserByConfirmMailComplete(object sender, eNegEntityResultArgs<User> e)
        {
            if (!e.HasError)
            {
                ImageType MessageType = (e.Results.Count() == 0 ? ImageType.Error : ImageType.Success);
                Message = new eNegMessage(e.Results.Count() == 0 ? Resources.InvalidConfirmationMail : Resources.ActivationMailSuccess, MessageType);

                Message.ShowMessageCompleted += (s, args) =>
                  {
                      HtmlPage.Window.Navigate(new Uri(AppConfigurations.HostBaseAddress + "Default.aspx", UriKind.Absolute));
                  };

                if (e.Results.Count() > 1)
                {
                    NeedOrganization = true;

                    OriginalOwner = e.Results.First();
                    NewOwner = e.Results.ElementAt(1);

                    Guid UserID = (OperationStringType == eNegConstant.UserOperations.RefuseOwnerRequest) ? e.Results.First().UserID : e.Results.ElementAt(1).UserID;

                    this.mConfirmMailModel.GetOrganizationByItsOwnerID(UserID);
                }
                else
                {
                    //to not login any more it will be login automatically.
                    if (e.Results.Count() > 0 && this.OperationStringType == 0)
                    {
                        ManageRememberMe(e.Results.First().EmailAddress);
                    }

                    eNegMessanger.SendCustomMessage.Send(Message);
                }
            }
            else
            {
                eNegMessanger.RaiseErrorMessage.Send(e.Error);
            }
        }

        /// <summary>
        /// Ms the confirm mail model_ get organization by user ID complete.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        void mConfirmMailModel_GetOrganizationByUserIDComplete(object sender, eNegEntityResultArgs<Organization> e)
        {
            if (!e.HasError)
            {
                NeedOrganization = false;

                var org = e.Results.FirstOrDefault();
                if (org != null)
                {
                    if (OperationStringType == eNegConstant.UserOperations.AcceptOwnerRequest ||
                               OperationStringType == eNegConstant.UserOperations.Accept_DeleteOwnerRequest)
                    {
                        MailHelper.SendMailMemberAcceptedToBeOwner(OriginalOwner, NewOwner, org);
                        Message.Message = Resources.ActivateOrgOwnerSucess;
                    }
                    else if (OperationStringType == eNegConstant.UserOperations.RefuseOwnerRequest)
                    {
                        MailHelper.SendMailMemberRefusedToBeOwner(OriginalOwner, NewOwner, org);
                        Message.Message = Resources.RefuseOrgOwnerSucess;
                    }

                    eNegMessanger.SendCustomMessage.Send(Message);
                }
            }
            else
            {
                eNegMessanger.RaiseErrorMessage.Send(e.Error);
            }
        }

        #endregion

        #region → Methods        .
        #region → Private        .

        /// <summary>
        /// Check On whether we are working on addon and if so save or remove 
        /// "keep me signed in" configurations from or in to Isolated Storage
        /// </summary>
        private void ManageRememberMe(string emailAddress)
        {
            IsolatedStorageSettings appSettings = IsolatedStorageSettings.ApplicationSettings;

            if (!appSettings.Contains("UserName"))
            {
                appSettings.Add("UserName", emailAddress);
            }
            else
            {
                appSettings["UserName"] = emailAddress;
            }

            appSettings.Save();
        }

        #endregion

        #region → Public         .

        /// <summary>
        /// public method that call perform query to Open The User To can be login.
        /// Update Locked Flag By false
        /// </summary>
        /// <param name="operationString">string in your mail</param>
        /// <param name="operationStringType">Type of the operation string.</param>
        public void UpdateUserByConfirmMail(String operationString, byte operationStringType)
        {
            mConfirmMailModel.UpdateUserByConfirmMail(operationString, operationStringType);
        }

        #region "ICleanup interface implementation"
        /// <summary>
        /// Cleanups this instance.
        /// </summary>
        public override void Cleanup()
        {
            // unregister all events
            mConfirmMailModel.UpdateUserByConfirmMailComplete -= new EventHandler<eNegEntityResultArgs<User>>(mUserRegisterModel_UpdateUserByConfirmMailComplete);

            // unregister any messages for this ViewModel
            base.Cleanup();
        }

        #endregion "ICleanup interface implementation"

        #endregion

        #endregion
    }
}