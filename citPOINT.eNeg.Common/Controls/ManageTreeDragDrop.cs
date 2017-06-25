#region → Usings   .
using citPOINT.eNeg.Data.Web;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.DragDrop;

#endregion

#region → History  .

/* Date         User            Change
 * 
 * 29.09.10     M.Wahab        creation
 */

# endregion History

#region → ToDos    .

/*
 * Date         set by User     Description
 * 
 * 
*/

# endregion ToDos

namespace citPOINT.eNeg.Common
{

    /// <summary>
    /// Class For Manage Drag and Drop Of 
    /// Negotiation from Onging to Closed and Vice Versa
    /// </summary>
    public class ManageTreeDragDrop : ICleanup
    {
        #region → Fields         .

        RadTreeView mUxTree;
        RelayCommand mCloseReOpenNegotiationCommand;

        #endregion Fields

        #region → Properties     .

        /// <summary>
        /// Get Or Set RelayCommand To CloseReOpenNegotiationCommand
        /// </summary>
        public RelayCommand CloseReOpenNegotiationCommand
        {
            get { return mCloseReOpenNegotiationCommand; }
            set { mCloseReOpenNegotiationCommand = value; }
        }

        /// <summary>
        /// Get Or Set RadTreeView
        /// </summary>
        public RadTreeView UxTree
        {
            get { return mUxTree; }
            set { mUxTree = value; }
        }

        #endregion Properties

        #region → Constructors   .
        /// <summary>
        /// Default Contructor
        /// </summary>
        /// <param name="RadTreeView">RadTreeView</param>
        /// <param name="CloseReOpenNegotiationCommand">RelayCommand To CloseReOpenNegotiationCommand</param>
        public ManageTreeDragDrop(RadTreeView RadTreeView, RelayCommand CloseReOpenNegotiationCommand)
        {
            this.UxTree = RadTreeView;
            this.CloseReOpenNegotiationCommand = CloseReOpenNegotiationCommand;
            this.UxTree.PreviewDragEnded += new RadTreeViewDragEndedEventHandler(UxTree_PreviewDragEnded);
            this.mUxTree.PreviewDragStarted += new RadTreeViewDragEventHandler(mUxTree_PreviewDragStarted);
            RadDragAndDropManager.AddDragInfoHandler(this.UxTree, new EventHandler<DragDropEventArgs>(uxTree_onDragInfo));


        }




        #endregion Constructor

        #region → Event Handlers .


        /// <summary>
        /// Handle the the drag operation into the ongoing Negotiations TreeView 
        /// </summary>
        /// <param name="sender">value of sender</param>
        /// <param name="e">value of drag drop event args</param>
        public void uxTree_onDragInfo(object sender, DragDropEventArgs e)
        {
            if (e.Options.Status == DragStatus.DragComplete)
            {
                if ((e.Options.Destination is RadTreeView) || (e.Options.Destination is RadTreeViewItem))
                {

                    RadTreeView Tree = DestinationTree(e.Options.Destination);

                    if ((e.Options.Payload as Collection<object>).First() is Negotiation &&
                         Tree.Name != UxTree.Name)
                    {
                        CloseReOpenNegotiationCommand.Execute(null);

                    }
                }
            }
        }


        /// <summary>
        /// Handles the PreviewDragStarted event of the mUxTree control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Windows.Controls.RadTreeViewDragEventArgs"/> instance containing the event data.</param>
        public void mUxTree_PreviewDragStarted(object sender, RadTreeViewDragEventArgs e)
        {
            try
            {
                if (e.DraggedItems.Count == 0 ||
                    e.DraggedItems[0] is Conversation ||
                    this.UxTree.SelectedItem == null)
                {
                    e.Handled = true;
                    return;
                }

                if (e.DraggedItems[0] is Negotiation &&
                    (this.UxTree.SelectedItem as Negotiation).NegotiationID != (e.DraggedItems[0] as Negotiation).NegotiationID)
                {

                    e.Handled = true;
                    return;
                }
            }
            catch (Exception ex)
            {
                eNegMessanger.RaiseErrorMessage.Send(ex);
            }
        }

        /// <summary>
        /// Event handler to disable dragging of conversations
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">RadTreeViewDragEndedEventArgs</param>
        private void UxTree_PreviewDragEnded(object sender, RadTreeViewDragEndedEventArgs e)
        {
            try
            {
                if (e.DraggedItems[0] is Conversation)
                {
                    e.Handled = true;
                    return;
                }

                if (e.TargetDropItem == null)
                {
                    e.Handled = true;
                    return;
                }
                else
                {
                    if (e.TargetDropItem.Header is Conversation)
                    {
                        e.Handled = true;
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                eNegMessanger.RaiseErrorMessage.Send(ex);
            }
        }


        #endregion Event Handlers

        #region → Methods        .

        #region → Private        .

        /// <summary>
        /// Get The Destination Tree
        /// </summary>
        /// <param name="Destination">Value of Destination</param>
        /// <returns>RadTreeView</returns>
        private RadTreeView DestinationTree(FrameworkElement Destination)
        {
            if (Destination is RadTreeView)
            {
                return (Destination as RadTreeView);
            }

            if (Destination is RadTreeViewItem)
            {
                return (Destination as RadTreeViewItem).ParentTreeView;
            }

            return null;
        }

        #endregion Private

        #region → Public         .

        /// <summary>
        /// To Clean all Resources
        /// </summary>
        public void Cleanup()
        {
            this.UxTree.PreviewDragEnded -= new RadTreeViewDragEndedEventHandler(UxTree_PreviewDragEnded);
            RadDragAndDropManager.RemoveDragInfoHandler(this.UxTree, new EventHandler<DragDropEventArgs>(uxTree_onDragInfo));
        }

        #endregion Public

        #endregion Methods

    }
}
