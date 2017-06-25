#region → Usings   .
using citPOINT.eNeg.Common;
using citPOINT.eNeg.Data.Web;
using citPOINT.eNeg.ViewModel;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.ComponentModel.Composition;
using System.Windows.Controls;
using Telerik.Windows;
using Telerik.Windows.Controls.GridView;
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

namespace citPOINT.eNeg.Client
{

    /// <summary>
    /// UsersOverview View
    /// </summary>
    public partial class UsersOverview : UserControl, ICleanup
    {

        #region → Properties     .
        /// <summary>
        /// Gets or sets the Data context of view
        /// </summary>
        [Import(ViewModelTypes.ManageUsersViewModel)]
        public ManageUsersViewModel ViewModel
        {
            get
            {
                return (DataContext as ManageUsersViewModel);
            }
            set
            {
                DataContext = value;
            }
        }
        #endregion Properties

        #region → Constructors   .
        /// <summary>
        /// Initializes a new instance of the <see cref="UsersOverview"/> class.
        /// </summary>
        public UsersOverview()
        {
            InitializeComponent();

            #region " Use MEF To load the View Model "

            if (!ViewModelBase.IsInDesignModeStatic)
            {
                // Use MEF To load the View Model
                CompositionInitializer.SatisfyImports(this);
                (DataContext as ManageUsersViewModel).uxUsersPagePanel = this.uxctlNavigation.PageNumbersPanel;
            }
            #endregion " Use MEF To load the View Model "

            #region Add handler for cell double click event

            uxUsersGridView.AddHandler(GridViewCell.CellDoubleClickEvent, new EventHandler<RadRoutedEventArgs>(OnDoubleClick));

            #endregion Add handler for cell double click event

            #region → Register for needed eNeg Messanger messages.
            
            eNegMessanger.BuildControl.Register(this, onBuildControl);

            #endregion
        }
        #endregion Constructor

        #region → Event Handlers .

        #region Control Event Handlers

        /// <summary>
        /// Ons the double click.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="Telerik.Windows.RadRoutedEventArgs"/> instance containing the event data.</param>
        private void OnDoubleClick(object sender, RadRoutedEventArgs e)
        {
            if (e.OriginalSource is GridViewCell)
            {
                if ((e.Source as GridViewCell).Column.CellTemplate != null && ((e.Source as GridViewCell).Column.CellTemplate.LoadContent() is CheckBox))
                {
                    return;
                }
                AppConfigurations.ProfileUserID = ((e.OriginalSource as GridViewCell).ParentRow.Item as User).UserID;
                eNegMessanger.FlippMessage.Send(ViewTypes.UserDetails);
            }
        }

        /// <summary>
        /// Handles the CheckChanged event of the CheckBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void CheckBox_CheckChanged(object sender, System.Windows.RoutedEventArgs e)
        {
            if (e.OriginalSource is CheckBox &&
                ((e.OriginalSource as CheckBox).DataContext != null) &&
                ((e.OriginalSource as CheckBox).DataContext is User))
            {
                if ((e.OriginalSource as CheckBox).IsChecked.Value)
                {
                    ((e.OriginalSource as CheckBox).DataContext as User).Disabled = true;
                    ((e.OriginalSource as CheckBox).DataContext as User).IsSelected = true;
                }
                else
                {
                    ((e.OriginalSource as CheckBox).DataContext as User).Disabled = false;
                    ((e.OriginalSource as CheckBox).DataContext as User).IsSelected = false;
                }
            }

            ViewModel.RaiseCanExecuteChanged();
        }

        #endregion Control Event Handlers

        #endregion Event Handlers

        #region → Methods        .

        #region → Private        .

        /// <summary>
        /// Ons the build control.
        /// </summary>
        /// <param name="controlName">Name of the control.</param>
        private void onBuildControl(string controlName)
        {
            switch (controlName)
            {
                case ControlTypes.AlphabeticControl:
                    uxAlphaFilter.BuildAlphabiticFilter();
                    break;
            }
        }
        #endregion

        #region → Public         .

        /// <summary>
        /// ICleanup interface implementation
        /// </summary>
        public void Cleanup()
        {
            // call Cleanup on its ViewModel
            ((ICleanup)this.DataContext).Cleanup();

            // Cleanup itself
            Messenger.Default.Unregister(this);
        }

        #endregion

        #endregion

    }
}
