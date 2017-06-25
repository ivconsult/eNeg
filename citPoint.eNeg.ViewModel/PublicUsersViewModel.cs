
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
    /// This class is used to carry all operation related to public Users and inhert userbaseViewModel
    /// </summary>
    [Export(ViewModelTypes.PublicUsersViewModel)]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    #endregion
    public class PublicUsersViewModel : UserBaseViewModel
    {
        #region → Fields         .

        private bool mIsUserSourceEmpty = false;

        #endregion

        #region → properties     .

        /// <summary>
        /// Gets or sets a value indicating whether this instance is user source empty.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is user source empty; otherwise, <c>false</c>.
        /// </value>
        public bool IsUserSourceEmpty
        {
            get
            {
                return mIsUserSourceEmpty;

            }

            set
            {
                mIsUserSourceEmpty = value;
                this.RaisePropertyChanged("IsUserSourceEmpty");
            }
        }

        #endregion

        #region → Constructors   .

        /// <summary>
        /// Initializes a new instance of the <see cref="PublicUsersViewModel"/> class.
        /// </summary>
        /// <param name="mmanageUsersModel">The mmanage users model.</param>
        [ImportingConstructor]
        public PublicUsersViewModel(IUserBaseModel mmanageUsersModel)
            : base(mmanageUsersModel, true)
        {
            this.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(PublicUsersViewModel_PropertyChanged);
        }

        #endregion
        
        #region → Event Handlers .

        /// <summary>
        /// Handles the PropertyChanged event of the PublicUsersViewModel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.ComponentModel.PropertyChangedEventArgs"/> instance containing the event data.</param>
        private void PublicUsersViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "AllUsersSource")
            {
                this.IsUserSourceEmpty = this.AllUsersSource.Count <= 0;
            }
        }

        #endregion
    }
}
