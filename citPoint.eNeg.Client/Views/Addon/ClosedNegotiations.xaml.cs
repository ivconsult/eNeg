
#region → Usings   .

using System.Windows.Controls;
using citPOINT.eNeg.Common;
using citPOINT.eNeg.Data.Web;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using Telerik.Windows.Controls;
using citPOINT.eNeg.Data;
using citPOINT.eNeg.ViewModel;

#endregion

#region → History  .
/* Date         User            Change
 * 
 * 09.08.10     Yousra Reda     creation
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
    /// Closed Negotiations
    /// </summary>
    public partial class ClosedNegotiations : UserControl, ICleanup
    {
        #region → Fields         .

        private RadTreeViewItem currentItem;

        #endregion

        #region → Properties     .

        /// <summary>
        /// Gets or sets the view model.
        /// </summary>
        /// <value>The view model.</value>
        public NegotiationViewModel ViewModel
        {
            get
            {
                return (DataContext as NegotiationViewModel);
            }
        }

        /// <summary>
        /// Gets or sets the last instance.
        /// </summary>
        /// <value>The last instance.</value>
        public static PublishedNegotiationsView LastInstance { get; set; }

        #endregion

        #region → Constructors   .

        /// <summary>
        /// Default constructor
        /// </summary>
        public ClosedNegotiations()
        {
            InitializeComponent();

            if (!GalaSoft.MvvmLight.ViewModelBase.IsInDesignModeStatic)
            {
                this.uxTreeClosed.LoadOnDemand += new System.EventHandler<Telerik.Windows.RadRoutedEventArgs>(uxTreeClosed_LoadOnDemand);

                this.uxTreeClosed.ItemPrepared += new System.EventHandler<RadTreeViewItemPreparedEventArgs>(uxTreeClosed_ItemPrepared);

                eNegMessanger.DoOperationMessage.Register(this, OnDoOperationChanged);
            }
        }

        #endregion

        #region → Event Handlers .

        #region Control Event Handler

        /// <summary>
        /// Handles the ItemPrepared event of the uxTreeClosed control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Windows.Controls.RadTreeViewItemPreparedEventArgs"/> instance containing the event data.</param>
        private void uxTreeClosed_ItemPrepared(object sender, RadTreeViewItemPreparedEventArgs e)
        {
            RadTreeViewItem item = e.PreparedItem;

            if (item.Item is IArchiveItem &&
                (item.Item as IArchiveItem).ArchiveItemType == ArchiveItemTypes.Conversation)
            {
                item.IsLoadOnDemandEnabled = false;
            }
        }

        /// <summary>
        /// Handles the LoadOnDemand event of the uxTreeClosed control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Windows.RadRoutedEventArgs"/> instance containing the event data.</param>
        private void uxTreeClosed_LoadOnDemand(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {
            RadTreeViewItem item = e.OriginalSource as RadTreeViewItem;

            currentItem = item;

            currentItem.IsSelected = true;

            this.ViewModel.SelectedItem = item.Item;
        }

        /// <summary>
        /// Event handeler fired when the context menu opened
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">RoutedEventArgs</param>
        private void contextMenu_Opened(object sender, System.Windows.RoutedEventArgs e)
        {
            RadTreeViewItem mSelectedItem;

            RadTreeViewItem treeViewItem = (sender as RadContextMenu).GetClickedElement<RadTreeViewItem>();
            mSelectedItem = treeViewItem;

            if (treeViewItem == null || mSelectedItem == null)
            {
                (sender as RadContextMenu).IsOpen = false;
            }
            else
            {
                ArchiveItem archiveItem = (mSelectedItem.Header as citPOINT.eNeg.Data.ArchiveItem);

                if (archiveItem != null && (
                    archiveItem.ArchiveItemType == ArchiveItemTypes.Year ||
                    archiveItem.ArchiveItemType == ArchiveItemTypes.Month))
                {
                    (sender as RadContextMenu).IsOpen = false;
                }
            }

            if (mSelectedItem != null)
            {
                mSelectedItem.Focus();
                mSelectedItem.IsSelected = true;
            }
        }

        #endregion

        #endregion

        #region → Methods        .

        #region → Private        .

        /// <summary>
        /// Called when [do operation changed].
        /// </summary>
        /// <param name="opType">Type of the op.</param>
        private void OnDoOperationChanged(eNegMessanger.OperationType opType)
        {
            if (opType == eNegMessanger.OperationType.ClosedNegotiationArchiveLoaded &&
                currentItem != null)
            {
                currentItem.IsExpanded = true;

                currentItem.IsLoadOnDemandEnabled = false;
            }
        }

        #endregion

        #region → Public         .

        /// <summary>
        /// ICleanup interface implementation
        /// </summary>
        public void Cleanup()
        {
            this.uxTreeClosed.LoadOnDemand -= new System.EventHandler<Telerik.Windows.RadRoutedEventArgs>(uxTreeClosed_LoadOnDemand);

            this.uxTreeClosed.ItemPrepared -= new System.EventHandler<RadTreeViewItemPreparedEventArgs>(uxTreeClosed_ItemPrepared);

            // Cleanup itself
            Messenger.Default.Unregister(this);
        }

        #endregion

        #endregion
    }
}
