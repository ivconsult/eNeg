
#region → Usings   .
using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using citPOINT.eNeg.Common;
using System.ComponentModel.Composition;
using citPOINT.eNeg.Data.Web;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System.Linq;
#endregion

#region → History  .

/* Date         User            Change
 * 
 * 22.09.11     Yousra Reda     creation
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
    #region Using MEF to export ManageUsersViewModel
    /// <summary>
    /// This class is used to carry all operation related to Managing Users and inhert userbaseViewModel
    /// </summary>
    [Export(ViewModelTypes.ManageUsersViewModel)]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    #endregion
    public class ManageUsersViewModel : UserBaseViewModel
    {
        #region → Fields         .
        private RelayCommand mDeleteSelectedUsersCommand = null;
        #endregion

        #region → Constructors   .

        /// <summary>
        /// Initializes a new instance of the <see cref="ManageUsersViewModel"/> class.
        /// </summary>
        /// <param name="mmanageUsersModel">The mmanage users model.</param>
        [ImportingConstructor]
        public ManageUsersViewModel(IUserBaseModel mmanageUsersModel)
            : base(mmanageUsersModel, false)
        {
            mmanageUsersModel.GetUserOrganizationsForOwnersUsersComplete += new EventHandler<eNegEntityResultArgs<UserOrganization>>(mManageUsersModel_GetUserOrganizationsForOwnersUsersComplete);

            #region Register needed messages in eNegMessenger
            eNegMessanger.SubmitChangesMessage.Register(this, OnSubmitChangesMessage);
            #endregion Register needed messages in eNegMessenge
        }

        #endregion

        #region → Event Handlers .

        /// <summary>
        /// Ms the manage users model_ get user organizations for owners users complete.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        private void mManageUsersModel_GetUserOrganizationsForOwnersUsersComplete(object sender, eNegEntityResultArgs<UserOrganization> e)
        {
            if (!e.HasError)
            {
                RefreshDataSource();
            }
            else
            {
                eNegMessanger.RaiseErrorMessage.Send(e.Error);
            }
        }
        #endregion

        #region → Commands       .

        /// <summary>
        /// Gets the delete selected users.
        /// </summary>
        /// <value>The delete selected users.</value>
        public RelayCommand DeleteSelectedUsersCommand
        {
            get
            {
                if (mDeleteSelectedUsersCommand == null)
                {
                    mDeleteSelectedUsersCommand = new RelayCommand(() =>
                    {
                        try
                        {


                            if (!mUserBaseModel.IsBusy)
                            {
                                #region Confirmation Message

                                Action<MessageBoxResult> callBackResult = null;

                                //Firsly ask user to confirm editing that item
                                DialogMessage DeleteDialogMsg = new DialogMessage(
                                    this,
                                    Resources.DeleteCurrentItemMessageBoxText, 
                                    result => callBackResult(result))
                                {
                                    Button = MessageBoxButton.OKCancel,
                                    Caption = Resources.ConfirmMessageBoxCaption
                                };

                                eNegMessanger.ConfirmMessage.Send(DeleteDialogMsg);

                                #endregion "Confirmation Message"

                                callBackResult = (result) =>
                                {
                                    if (result == MessageBoxResult.Cancel)
                                        return;

                                    while (mUsers.Where(s => s.IsSelected).Count() > 0)
                                    {
                                        UserBaseModel.RemoveUser(mUsers.Where(s => s.IsSelected).First());
                                      
                                        mUsers.Remove(mUsers.Where(s => s.IsSelected).First());
                                    }

                                    RefreshDataSource();

                                    RaiseCanExecuteChanged();
                                    
                                    eNegMessanger.SubmitChangesMessage.Send();
                                };
                            }
                        }
                        catch (Exception ex)
                        {
                            eNegMessanger.RaiseErrorMessage.Send(ex);
                        }
                    }, () => !mUserBaseModel.IsBusy && AllUsersSource != null && AllUsersSource.Where(a => a.IsSelected == true).Count() > 0);
                }
                return mDeleteSelectedUsersCommand;
            }
        }
        #endregion

        #region → Methods        .

        #region → Private        .

        /// <summary>
        /// Called when [submit changes message].
        /// </summary>
        /// <param name="ignore">if set to <c>true</c> [ignore].</param>
        private void OnSubmitChangesMessage(eNegMessanger.OperationType ignore)
        {
            if ((mUserBaseModel.HasChanges))
            {
                mUserBaseModel.SaveChangesAsync();
            }
        }
        #endregion

        #region → Public         .

        /// <summary>
        /// Raises the can execute changed.
        /// </summary>
        public override void RaiseCanExecuteChanged()
        {
            DeleteSelectedUsersCommand.RaiseCanExecuteChanged();
        }
        #endregion
        
        #endregion

    }
}
