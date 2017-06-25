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
using Telerik.Windows;
using Telerik.Windows.Controls;
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
    /// Ongoing Negotiations
    /// </summary>
    public partial class OngoingNegotiations : UserControl, ICleanup
    {
        #region → Fields         .

        RadTreeViewItem mSelectedItem;
        List<string> mExpanedNegotiations = new List<string>();
        bool mIsEventsHandler = false;
        private NegotiationsView mNegotiationsView;

        #endregion fields

        #region → Properties     .

        /// <summary>
        /// Gets the view model.
        /// </summary>
        /// <value>The view model.</value>
        private NegotiationViewModel ViewModel
        {
            get { return ((NegotiationViewModel)(this.DataContext)); }
        }

        /// <summary>
        /// Gets or sets the negotiations view.
        /// </summary>
        /// <value>The negotiations view.</value>
        public NegotiationsView NegotiationsView
        {
            get { return mNegotiationsView; }
            set { mNegotiationsView = value; }
        }

        #endregion

        #region → Constructors   .

        /// <summary>
        /// Default Constructor
        /// </summary>
        public OngoingNegotiations()
        {
            InitializeComponent();

            #region " Registeration for needed messages in eNegMessenger "

            eNegMessanger.EditNegotiationMessage.Register(this, OnChangeTreeNode);

            eNegMessanger.EditConversationMessage.Register(this, OnChangeConversationTreeNode);

            eNegMessanger.DoOperationMessage.Register(this, OnLoadCompleted);

            #endregion
        }

        #endregion

        #region → Event Handlers .

        #region Control Events

        /// <summary>
        /// Event handeler fired when the context menu opened
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">RoutedEventArgs</param>
        private void contextMenu_Opened(object sender, System.Windows.RoutedEventArgs e)
        {

            RadTreeViewItem treeViewItem = (sender as RadContextMenu).GetClickedElement<RadTreeViewItem>();
            mSelectedItem = treeViewItem;

            if (treeViewItem == null || mSelectedItem.Header is Message)
            {
                (sender as RadContextMenu).IsOpen = false;
                return;
            }

            if (mSelectedItem != null)
            {
                RadTreeViewItem radTreeViewItem = mSelectedItem;
                uxTreeOngoing.SelectedItem = radTreeViewItem.Header;
                mSelectedItem = radTreeViewItem;
            }

        }

        /// <summary>
        /// Execute the choosen command from the context menu
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">RadRoutedEventArgs</param>
        private void contextMenu_Clicked(object sender, RadRoutedEventArgs e)
        {
            RadMenuItem item = e.OriginalSource as RadMenuItem;
            if ((item == null))
            {
                return;
            }

            if (mSelectedItem != null && item.Header.ToString() == "Rename")
            {

            }
        }

        #endregion

        /// <summary>
        /// Called when [negotiations_ on deleting].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        void OngoingNegotiations_OnDeleting(object sender, EventArgs e)
        {
            TakeCaptureFromTree();
        }

        /// <summary>
        /// Called when [negotiations_ data saved].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        void OngoingNegotiations_DataSaved(object sender, EventArgs e)
        {
            ApplyCaptureToTree();
        }

        #endregion

        #region → Methods        .

        #region → Private        .

        /// <summary>
        /// Open Tree node in edit moe if we choose new negotiation
        /// </summary>
        /// <param name="NegNode">Neg Node</param>
        private void OnChangeTreeNode(Negotiation NegNode)
        {
            if (NegNode != null && this.IsActiveTab(this))
            {
                mSelectedItem = uxTreeOngoing.GetItemByPath("Negotiation : " + NegNode.NegotiationID.ToString());

                // Becuase we run the Code in thread so the Actual Value Changed Before The Begin of Second Thread
                bool IsNewNegotiation = NegNode.IsNewNegotiation;

                if (mSelectedItem != null && (NegNode.IsNewNegotiation || NegNode.IsNegSelected))
                {
                    Dispatcher.BeginInvoke(() =>
                    {

                        mSelectedItem.Focus();
                        mSelectedItem.IsSelected = true;
                        mSelectedItem.IsExpanded = true;
                        if (IsNewNegotiation)
                        {
                            mSelectedItem.BeginEdit();
                        }
                    });

                }
            }
        }

        /// <summary>
        /// Open Tree node in edit moe if we choose new Conversation
        /// </summary>
        /// <param name="conversationNode">The conversation node.</param>
        private void OnChangeConversationTreeNode(Conversation conversationNode)
        {
            if (conversationNode != null && this.IsActiveTab(this))
            {

                string NodePath = string.Format("Negotiation : {0}\\Conversation : {1}",
                                               conversationNode.NegotiationID.ToString(),
                                               conversationNode.ConversationID.ToString());

                bool isNewConversation = conversationNode.IsNewConversation;

                mSelectedItem = uxTreeOngoing.GetItemByPath(NodePath);

                if (mSelectedItem != null && (conversationNode.IsNewConversation || conversationNode.IsConvSelected))
                {

                    Dispatcher.BeginInvoke(() =>
                    {
                        mSelectedItem.Focus();
                        mSelectedItem.IsSelected = true;

                        //if (isNewConversation)
                        //    mSelectedItem.BeginEdit();

                    });

                }
            }
        }

        /// <summary>
        /// Open Tree node in edit moe if we choose new negotiation
        /// </summary>
        /// <param name="Types">The types.</param>
        private void OnLoadCompleted(eNegMessanger.OperationType Types)
        {
            if (this.DataContext != null && !mIsEventsHandler)
            {
                this.ViewModel.DataSaved += new EventHandler(OngoingNegotiations_DataSaved);
                this.ViewModel.OnDeleting += new EventHandler(OngoingNegotiations_OnDeleting);
                mIsEventsHandler = true;
            }
        }

        /// <summary>
        /// Takes the capture from tree.
        /// as node opened and Closed
        /// </summary>
        private void TakeCaptureFromTree()
        {
            mExpanedNegotiations.Clear();
            foreach (var item in uxTreeOngoing.Items)
            {

                string NodePath = string.Format("Negotiation : {0}", (item as Negotiation).NegotiationID.ToString());

                RadTreeViewItem TreeItem = uxTreeOngoing.GetItemByPath(NodePath);

                if (TreeItem.IsExpanded)
                    mExpanedNegotiations.Add(TreeItem.FullPath);
            }
        }

        /// <summary>
        /// Applay the capture to tree.
        /// </summary>
        private void ApplyCaptureToTree()
        {
            foreach (var item in mExpanedNegotiations)
            {
                RadTreeViewItem TreeItem = uxTreeOngoing.GetItemByPath(item);

                if (TreeItem != null)
                    TreeItem.ExpandAll();
            }

            mExpanedNegotiations.Clear();
        }
        
        /// <summary>
        /// Determines whether [is active tab] [the specified control].
        /// </summary>
        /// <param name="control">The control.</param>
        /// <returns>
        /// 	<c>true</c> if [is active tab] [the specified control]; otherwise, <c>false</c>.
        /// </returns>
        private bool IsActiveTab(FrameworkElement control)
        {
            if (control != null)
            {
                if (control is RadPanelBarItem)
                {
                    return (control as RadPanelBarItem).IsExpanded;
                }
                else if (control.Parent != null && (control.Parent as FrameworkElement) != null)
                {
                    return IsActiveTab((control.Parent as FrameworkElement));
                }
            }

            return true;
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
