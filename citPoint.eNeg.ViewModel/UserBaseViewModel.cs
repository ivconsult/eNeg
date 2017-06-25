
#region → Usings   .
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using Telerik.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;
using System.Windows.Controls;
#endregion

#region → History  .

/* Date         User            Change
 * 
 * 01.11.10     Yousra Reda     creation
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
    /// This class treated as a parent class that handel all operation related to users entities
    /// </summary>
    public partial class UserBaseViewModel : GalaSoft.MvvmLight.ViewModelBase
    {
        #region → Fields         .

        protected IUserBaseModel mUserBaseModel;
        protected IList<User> mUsers;
        private User mCurrentUser;

        private RelayCommand mGetAllUsersCommand = null;
        private RelayCommand<string> mFilterUsersByAlphabetCommand = null;
        private RelayCommand<KeyEventArgs> mFindUserByEnterKeyDownCommand = null;
        private RelayCommand mFindUserCommand = null;
        private RelayCommand<GridViewSortedEventArgs> mSortUsersByCertainPropertyCommand = null;

        #endregion Fields

        #region → Properties     .

        /// <summary>
        /// Gets or sets the filter key word.
        /// </summary>
        /// <value>The filter key word.</value>
        public string FilterKeyWord
        {
            get { return mUserBaseModel.FilterKeyWord; }
            set
            {
                mUserBaseModel.FilterKeyWord = value;
                RaisePropertyChanged("FilterKeyWord");
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is filtered by alphabet.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is filtered by alphabet; otherwise, <c>false</c>.
        /// </value>
        public bool IsFilteredByAlphabet
        {
            get { return mUserBaseModel.IsFilteredByAlphabet; }
            set
            {
                mUserBaseModel.IsFilteredByAlphabet = value;
                RaisePropertyChanged("IsFilteredByAlphabet");
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is filtered by key.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is filtered by key; otherwise, <c>false</c>.
        /// </value>
        public bool IsFilteredByKey
        {
            get { return mUserBaseModel.IsFilteredByKey; }
            set
            {
                mUserBaseModel.IsFilteredByKey = value;
                RaisePropertyChanged("IsFilteredByKey");
            }
        }

        /// <summary>
        /// Gets or sets the filter letter.
        /// </summary>
        /// <value>The filter letter.</value>
        public string FilterLetter
        {
            get { return mUserBaseModel.FilterLetter; }
            set
            {
                mUserBaseModel.FilterLetter = value;
                RaisePropertyChanged("FilterLetter");
            }
        }

        /// <summary>
        /// Gets or sets the user base model.
        /// </summary>
        /// <value>The user base model.</value>
        public IUserBaseModel UserBaseModel
        {
            get { return mUserBaseModel; }
            set { mUserBaseModel = value; }
        }

        /// <summary>
        /// Gets or sets the current user.
        /// </summary>
        /// <value>The current user.</value>
        public User CurrentUser
        {
            get { return mCurrentUser; }
            set
            {
                if (mCurrentUser != value)
                {
                    mCurrentUser = value;
                    RaisePropertyChanged("CurrentUser");
                }
            }
        }

        /// <summary>
        /// Gets or sets all users source.
        /// </summary>
        /// <value>All users source.</value>
        public ObservableCollection<User> AllUsersSource { get; private set; }


        #endregion Properties

        #region → Constructors   .

        /// <summary>
        /// Initializes a new instance of the <see cref="ManageUsersViewModel"/> class.
        /// </summary>
        public UserBaseViewModel(IUserBaseModel mmanageUsersModel, bool mIsForPublicProfile)
        {
            UserBaseModel = mmanageUsersModel;
            UserBaseModel.IsForPublicProfile = mIsForPublicProfile;

            mUserBaseModel.LastSelectedColumn = "EmailAddress";

            if (mUserBaseModel.HasChanges)
                mUserBaseModel.RejectChanges();

            #region Intializing Data Source

            this.mUsers = new List<User>();
            RefreshDataSource();

            #endregion Intializing Data Source

            #region Set up event handling
            mUserBaseModel.GetUsersCountComplete += new Action<InvokeOperation<int>>(mManageUsersModel_GetUsersCountComplete);
            mUserBaseModel.GetUsersByPageNumberComplete += new EventHandler<eNegEntityResultArgs<User>>(mManageUsersModel_GetUsersByPageNumberComplete);
            mUserBaseModel.GetUsersByAlphabetComplete += new EventHandler<eNegEntityResultArgs<User>>(mManageUsersModel_GetUsersByAlphabetComplete);
            mUserBaseModel.GetUsersCountByAlphabetComplete += new Action<InvokeOperation<int>>(mManageUsersModel_GetUsersCountByAlphabetComplete);
            mUserBaseModel.FindUsersComplete += new EventHandler<eNegEntityResultArgs<User>>(mManageUsersModel_FindUsersComplete);
            mUserBaseModel.FindUserCountComplete += new Action<InvokeOperation<int?>>(mManageUsersModel_FindUserCountComplete);
            mmanageUsersModel.PropertyChanged += new PropertyChangedEventHandler(mmanageUsersModel_PropertyChanged);
            #endregion Set up event handling

            mUserBaseModel.UserPager.ITEMSPAGESIZE = 20;
            GetUsersByPageNumberAsync(1);

        }

        #endregion Constructors

        #region → Event Handlers .

        /// <summary>
        /// Handles the PropertyChanged event of the mmanageUsersModel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.ComponentModel.PropertyChangedEventArgs"/> instance containing the event data.</param>
        private void mmanageUsersModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals("HasChanges") || e.PropertyName.Equals("IsBusy"))
            {

            }
        }

        /// <summary>
        /// Ms the manage users model_ get users count by alphabet complete.
        /// </summary>
        /// <param name="obj">The obj.</param>
        private void mManageUsersModel_GetUsersCountByAlphabetComplete(InvokeOperation<int> obj)
        {
            if (!obj.HasError)
            {
                if (obj.Value == 0)
                {
                    mUsers.Clear();
                    RefreshDataSource();
                }
                mUserBaseModel.UserPager.ItemsCount = obj.Value;
                mUserBaseModel.UserPager.BuildPaging();
            }
            else
            {
                eNegMessanger.RaiseErrorMessage.Send(obj.Error);
            }
        }

        /// <summary>
        /// Ms the manage users model_ get users count complete.
        /// </summary>
        /// <param name="obj">The obj.</param>
        private void mManageUsersModel_GetUsersCountComplete(InvokeOperation<int> obj)
        {
            if (!obj.HasError)
            {
                if (obj.Value == 0)
                {
                    mUsers.Clear();
                    RefreshDataSource();
                }
                mUserBaseModel.UserPager.ItemsCount = obj.Value;
                mUserBaseModel.UserPager.BuildPaging();
            }
            else
            {
                eNegMessanger.RaiseErrorMessage.Send(obj.Error);
            }
        }

        /// <summary>
        /// Ms the manage users model_ get users by alphabet complete.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        private void mManageUsersModel_GetUsersByAlphabetComplete(object sender, eNegEntityResultArgs<User> e)
        {
            if (!e.HasError)
            {
                mUsers = e.Results.AsEnumerable<User>().ToList();

                this.GetUserOrganizationsForOwnersUsers();

                RefreshDataSource();
            }
            else
            {
                eNegMessanger.RaiseErrorMessage.Send(e.Error);
            }
        }

        /// <summary>
        /// Ms the manage users model_ find user count complete.
        /// </summary>
        /// <param name="obj">The obj.</param>
        private void mManageUsersModel_FindUserCountComplete(InvokeOperation<int?> obj)
        {
            if (!obj.HasError)
            {
                if (obj.Value == 0)
                {
                    mUsers.Clear();

                    RefreshDataSource();
                }


                mUserBaseModel.UserPager.ItemsCount = obj.Value == 0 ? 0 : (int)obj.Value;
                mUserBaseModel.UserPager.BuildPaging();
            }
            else
            {
                eNegMessanger.RaiseErrorMessage.Send(obj.Error);
            }
        }

        /// <summary>
        /// Ms the manage users model_ find users complete.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        private void mManageUsersModel_FindUsersComplete(object sender, eNegEntityResultArgs<User> e)
        {
            if (!e.HasError)
            {
                mUsers = e.Results.AsEnumerable<User>().ToList();

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
        /// Gets the sort users by certain property command.
        /// </summary>
        /// <value>The sort users by certain property command.</value>
        public RelayCommand<GridViewSortedEventArgs> SortUsersByCertainPropertyCommand
        {
            get
            {
                if (mSortUsersByCertainPropertyCommand == null)
                {
                    mSortUsersByCertainPropertyCommand = new RelayCommand<GridViewSortedEventArgs>((e) =>
                    {
                        try
                        {
                            UserBaseModel.LastSelectedColumn = e.Column.UniqueName;
                            RaiseCanExecuteChanged();
                        }
                        catch (Exception ex)
                        {
                            eNegMessanger.RaiseErrorMessage.Send(ex);
                        }
                    }, (e) => mUserBaseModel != null && !mUserBaseModel.IsBusy);
                }
                return mSortUsersByCertainPropertyCommand;
            }
        }

        /// <summary>
        /// Gets the find user by enter key down command.
        /// </summary>
        /// <value>The find user by enter key down command.</value>
        public RelayCommand<KeyEventArgs> FindUserByEnterKeyDownCommand
        {
            get
            {
                if (mFindUserByEnterKeyDownCommand == null)
                {
                    mFindUserByEnterKeyDownCommand = new RelayCommand<KeyEventArgs>((args) =>
                    {
                        try
                        {
                            if (args.Key == Key.Enter)
                            {

                                if (args.OriginalSource != null &&
                                    args.OriginalSource is TextBox)
                                {
                                    FilterKeyWord = (args.OriginalSource as TextBox).Text;
                                }
                                FindUserCommand.Execute(null);
                            }
                        }
                        catch (Exception ex)
                        {
                            eNegMessanger.RaiseErrorMessage.Send(ex);
                        }
                    });
                }
                return mFindUserByEnterKeyDownCommand;
            }
        }

        /// <summary>
        /// Gets the find user.
        /// </summary>
        /// <value>The find user.</value>
        public RelayCommand FindUserCommand
        {
            get
            {
                if (mFindUserCommand == null)
                {
                    mFindUserCommand = new RelayCommand(() =>
                    {
                        try
                        {
                            #region ReInitialize other cases of Getting Users
                            IsFilteredByAlphabet = false;
                            FilterLetter = string.Empty;
                            IsFilteredByKey = true;
                            #endregion

                            mUserBaseModel.UserPager.CurrentPageNumber = 1;
                            FindUsersAsync(FilterKeyWord);

                            eNegMessanger.BuildControl.Send(ControlTypes.AlphabeticControl);
                            RaiseCanExecuteChanged();
                        }
                        catch (Exception ex)
                        {
                            eNegMessanger.RaiseErrorMessage.Send(ex);
                        }
                    }, () => mUserBaseModel != null && !mUserBaseModel.IsBusy && !string.IsNullOrEmpty(FilterKeyWord));
                }
                return mFindUserCommand;
            }
        }

        /// <summary>
        /// Gets the get all users.
        /// </summary>
        /// <value>The get all users.</value>
        public RelayCommand GetAllUsersCommand
        {
            get
            {
                if (mGetAllUsersCommand == null)
                {
                    mGetAllUsersCommand = new RelayCommand(() =>
                    {
                        try
                        {

                            #region ReInitialize other cases of Getting Users
                            IsFilteredByAlphabet = false;
                            FilterLetter = string.Empty;
                            IsFilteredByKey = false;
                            FilterKeyWord = string.Empty;
                            #endregion

                            RaiseCanExecuteChanged();
                            eNegMessanger.BuildControl.Send(ControlTypes.AlphabeticControl);

                            GetUsersByPageNumberAsync(1);
                        }
                        catch (Exception ex)
                        {
                            eNegMessanger.RaiseErrorMessage.Send(ex);
                        }
                    }, () => mUserBaseModel != null && !mUserBaseModel.IsBusy);
                }
                return mGetAllUsersCommand;
            }
        }

        /// <summary>
        /// Gets the filter users by alphabet.
        /// </summary>
        /// <value>The filter users by alphabet.</value>
        public RelayCommand<string> FilterUsersByAlphabetCommand
        {
            get
            {
                if (mFilterUsersByAlphabetCommand == null)
                {
                    mFilterUsersByAlphabetCommand = new RelayCommand<string>((s) =>
                    {
                        try
                        {
                            #region ReInitialize other cases of Getting Users
                            IsFilteredByKey = false;
                            FilterKeyWord = string.Empty;
                            IsFilteredByAlphabet = true;
                            FilterLetter = s;
                            #endregion

                            mUserBaseModel.UserPager.CurrentPageNumber = 1;
                            GetUserByAlphabetAsync(s, mUserBaseModel.LastSelectedColumn);
                        }
                        catch (Exception ex)
                        {
                            eNegMessanger.RaiseErrorMessage.Send(ex);
                        }
                    }, (s) => mUserBaseModel != null && !mUserBaseModel.IsBusy);
                }
                return mFilterUsersByAlphabetCommand;
            }

        }

        #endregion Commands

        #region → Methods        .

        #region → Public         .

        /// <summary>
        /// Finds the users.
        /// </summary>
        /// <param name="KeyWord">The key word.</param>
        public void FindUsersAsync(string KeyWord)
        {
            mUserBaseModel.FindUsersAsync(KeyWord);
        }

        /// <summary>
        /// Gets the user by alphabet async.
        /// </summary>
        /// <param name="Alphabet">The alphabet.</param>
        /// <param name="ColumnName">Name of the column.</param>
        public void GetUserByAlphabetAsync(string Alphabet, string ColumnName)
        {
            mUserBaseModel.GetUserByAlphabetAsync(Alphabet, ColumnName);
        }

        /// <summary>
        /// Gets the users by page number async.
        /// </summary>
        /// <param name="PageNumber">The page number.</param>
        public void GetUsersByPageNumberAsync(int PageNumber)
        {
            mUserBaseModel.GetUsersByPageNumberAsync(PageNumber);
        }

        /// <summary>
        /// Gets the user organizations for owners users.
        /// </summary>
        public void GetUserOrganizationsForOwnersUsers()
        {
            if (!mUserBaseModel.IsForPublicProfile)
            {
                List<Guid> UsersIds = new List<Guid>();

                foreach (var userItem in this.mUsers)
                {
                    UsersIds.Add(userItem.UserID);
                }

                if (UsersIds.Count() > 0)
                {
                    this.mUserBaseModel.GetUserOrganizationsForOwnersUsers(UsersIds);
                }
            }
        }

        /// <summary>
        /// Raises the can execute changed.
        /// </summary>
        public virtual void RaiseCanExecuteChanged()
        {

        }

        #region "ICleanup interface implementation"
        /// <summary>
        /// Cleanups this instance.
        /// </summary>
        public override void Cleanup()
        {
            // unregister all events
            mUserBaseModel.GetUsersCountComplete -= new Action<InvokeOperation<int>>(mManageUsersModel_GetUsersCountComplete);
            mUserBaseModel.GetUsersByPageNumberComplete -= new EventHandler<eNegEntityResultArgs<User>>(mManageUsersModel_GetUsersByPageNumberComplete);
            mUserBaseModel.GetUsersByAlphabetComplete -= new EventHandler<eNegEntityResultArgs<User>>(mManageUsersModel_GetUsersByAlphabetComplete);
            mUserBaseModel.GetUsersCountByAlphabetComplete -= new Action<InvokeOperation<int>>(mManageUsersModel_GetUsersCountByAlphabetComplete);
            mUserBaseModel.FindUsersComplete -= new EventHandler<eNegEntityResultArgs<User>>(mManageUsersModel_FindUsersComplete);
            mUserBaseModel.FindUserCountComplete -= new Action<InvokeOperation<int?>>(mManageUsersModel_FindUserCountComplete);
            mUserBaseModel.PropertyChanged -= new PropertyChangedEventHandler(mmanageUsersModel_PropertyChanged);

            // unregister any messages for this ViewModel
            base.Cleanup();

            // unregister any messages for this ViewModel
            // Cleanup itself
            Messenger.Default.Unregister(this);

            //to Clear Context
            this.mUserBaseModel.CleanUp();
        }
        #endregion "ICleanup interface implementation"


        #endregion Public

        #endregion Methods
    }
}
