
#region → Usings   .
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Controls;
using citPOINT.eNeg.Common;
using citPOINT.eNeg.Data.Web;
using GalaSoft.MvvmLight.Command;

#endregion

#region → History  .

/* Date         User            Change
 * 
 * 07.11.10     Yousra Reda     creation
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
    /// <summary>
    /// This class is used to carry all operation related to Managing Users 
    /// </summary>
    public partial class UserBaseViewModel
    {

        #region → Fields         .
        private RelayCommand<string> mGetUsersByPageNumberCommand = null;
        #endregion Fields

        #region → Properties     .

        /// <summary>
        /// Gets or sets the ux users page panel.
        /// </summary>
        /// <value>The ux users page panel.</value>
        public StackPanel uxUsersPagePanel
        {
            get { return mUserBaseModel.UserPager.uxPagePanel; }
            set
            {
                mUserBaseModel.UserPager.uxPagePanel = value;
                mUserBaseModel.UserPager.GetTableByPageNumber = GetUsersByPageNumberCommand;
            }
        }
        #endregion

        #region → Event Handlers .

        /// <summary>
        /// Ms the manage users model_ get users complete.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        private void mManageUsersModel_GetUsersByPageNumberComplete(object sender, eNegEntityResultArgs<User> e)
        {
            if (!e.HasError)
            {
                this.mUsers = e.Results.Where(s => s.UserID != AppConfigurations.CurrentLoginUser.UserID).AsEnumerable<User>().ToList();

                this.GetUserOrganizationsForOwnersUsers();

                RefreshDataSource();
            }
            else
            {
                eNegMessanger.RaiseErrorMessage.Send(e.Error);
            }
        }

        #endregion Event Handlers

        #region → Commands       .
        /// <summary>
        /// Gets the get users by page number.
        /// </summary>
        /// <value>The get users by page number.</value>
        public RelayCommand<string> GetUsersByPageNumberCommand
        {
            get
            {
                if (mGetUsersByPageNumberCommand == null)
                {
                    mGetUsersByPageNumberCommand = new RelayCommand<string>(
                      (s) => NavigateUsersByPageType(s),
                      (s) => true);
                }
                return mGetUsersByPageNumberCommand;
            }
        }

        /// <summary>
        /// Navigates the type of the users by page.
        /// </summary>
        /// <param name="param">The param.</param>
        private void NavigateUsersByPageType(string param)
        {

            switch (param)
            {
                #region in case of first page

                case "FIRST":
                    mUserBaseModel.UserPager.CurrentPageNumber = 1;
                    GetUsersByPageNumberAsync(mUserBaseModel.UserPager.CurrentPageNumber);
                    break;

                #endregion

                #region in case of last page

                case "LAST":
                    mUserBaseModel.UserPager.CurrentPageNumber = mUserBaseModel.UserPager.ItemsPagesCount == 0 ? 1 : mUserBaseModel.UserPager.ItemsPagesCount;
                    GetUsersByPageNumberAsync(mUserBaseModel.UserPager.CurrentPageNumber);
                    break;

                #endregion

                #region in case of prev page

                case "PREV":
                    if (mUserBaseModel.UserPager.CurrentPageNumber > 1)
                        GetUsersByPageNumberAsync(--mUserBaseModel.UserPager.CurrentPageNumber);
                    break;

                #endregion

                #region in case of first page

                case "NEXT":
                    if (mUserBaseModel.UserPager.CurrentPageNumber < mUserBaseModel.UserPager.ItemsPagesCount)
                        GetUsersByPageNumberAsync(++mUserBaseModel.UserPager.CurrentPageNumber);

                    break;

                #endregion

                #region in case of numeric page number

                default:
                    mUserBaseModel.UserPager.CurrentPageNumber = int.Parse(param);
                    GetUsersByPageNumberAsync(mUserBaseModel.UserPager.CurrentPageNumber);
                    break;

                #endregion
            }

        }

        #endregion Commands

        #region → Methods        .

        #region → Public         .
        /// <summary>
        /// Refreshes the data source.
        /// </summary>
        public void RefreshDataSource()
        {
            AllUsersSource = new ObservableCollection<User>(mUsers);
            RaisePropertyChanged("AllUsersSource");
        }
        #endregion
        #endregion
    }
}
