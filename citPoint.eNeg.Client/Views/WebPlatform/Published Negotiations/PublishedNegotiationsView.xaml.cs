#region → Usings   .
using citPOINT.eNeg.Common;
using citPOINT.eNeg.Data.Web;
using citPOINT.eNeg.ViewModel;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.ComponentModel.Composition;
using Telerik.Windows.Controls;
using citPOINT.eNeg.Data;
using System.Windows.Threading;
#endregion

#region → History  .
/* Date         User            Change
 * 
 * 23.08.10     Dergham        creation
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
    /// Main Ongoing Negotiation
    /// </summary>
    public partial class PublishedNegotiationsView : UserControl, ICleanup
    {

        #region → Fields         .

        private RadTreeViewItem currentItem;

        #endregion

        #region → Properties     .

        /// <summary>
        /// Gets or sets the view model.
        /// </summary>
        /// <value>The view model.</value>
        [Import(ViewModelTypes.PublishedNegotiationViewModel)]
        public PublishedNegotiationViewModel ViewModel
        {
            get
            {
                return (DataContext as PublishedNegotiationViewModel);
            }

            set
            {
                DataContext = value;
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
        /// Default Constructor 
        /// </summary>
        public PublishedNegotiationsView()
        {
            PublishedNegotiationsView.LastInstance = this;

            InitializeComponent();

            #region " Use MEF To load the View Model "

            if (!GalaSoft.MvvmLight.ViewModelBase.IsInDesignModeStatic)
            {
                // Use MEF To load the View Model
                CompositionInitializer.SatisfyImports(this);

                eNegMessanger.DoOperationMessage.Register(this, OnDoOperationChanged);

                this.uxTreePublishedNegs.LoadOnDemand += new EventHandler<Telerik.Windows.RadRoutedEventArgs>(treeView_LoadOnDemand);

                this.uxTreePublishedNegs.ItemPrepared += new EventHandler<RadTreeViewItemPreparedEventArgs>(treeView_ItemPrepared);
            }

            #endregion

        }

        #endregion

        #region → Event Handlers .

        /// <summary>
        /// Handles the ItemPrepared event of the treeView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Windows.Controls.RadTreeViewItemPreparedEventArgs"/> instance containing the event data.</param>
        private void treeView_ItemPrepared(object sender, RadTreeViewItemPreparedEventArgs e)
        {
            RadTreeViewItem item = e.PreparedItem;

            if (item.Item is IArchiveItem &&
                (item.Item as IArchiveItem).ArchiveItemType == ArchiveItemTypes.Conversation)
            {
                item.IsLoadOnDemandEnabled = false;
            }
        }

        /// <summary>
        /// Handles the LoadOnDemand event of the treeView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Windows.RadRoutedEventArgs"/> instance containing the event data.</param>
        private void treeView_LoadOnDemand(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {
            RadTreeViewItem item = e.OriginalSource as RadTreeViewItem;

            currentItem = item;

            this.ViewModel.SelectedItem = item.Item;

        }

        #endregion

        #region → Methods        .

        #region → Private        .

        /// <summary>
        /// Called when [do operation changed].
        /// </summary>
        /// <param name="opType">Type of the op.</param>
        private void OnDoOperationChanged(eNegMessanger.OperationType opType)
        {
            if (opType == eNegMessanger.OperationType.NegotiationArchiveLoaded && currentItem != null)
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
            // call Cleanup on its ViewModel
            if ((ICleanup)this.DataContext != null)
                ((ICleanup)this.DataContext).Cleanup();

            // Cleanup itself
            Messenger.Default.Unregister(this);
        }

        #endregion


        #endregion

    }
}




