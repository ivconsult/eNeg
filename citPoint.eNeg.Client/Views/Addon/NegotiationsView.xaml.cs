
#region → Usings   .
using System.ComponentModel.Composition;
using System.Windows;
using System.Windows.Browser;
using System.Windows.Controls;
using citPOINT.eNeg.Common;
using citPOINT.eNeg.ViewModel;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using Telerik.Windows.Controls;
using citPOINT.eNeg.Common.eNegApps;
using citPOINT.eNeg.Apps.Common;
using System;
using citPOINT.eNeg.Client.Helper;

#endregion

#region → History  .
/* Date         User        Change
 * 
 * 22.08.10    m.Wahab     • creation
 * 
 */
#endregion

#region → ToDos    .

/*
 * Date         set by User     Description
 * 
 * 
*/

#endregion

namespace citPOINT.eNeg.Client
{
    /// <summary>
    /// Negotiations View
    /// </summary>
    public partial class NegotiationsView : UserControl, ICleanup
    {
        #region → Fields         .
        private IntializationAppIntegration IntializationAppIntegrationArea;
        bool mChangeColumnWidthForOngoing = false;
        bool mChangeColumnWidthForClosed = false;

        private ConversationDetails mConversationDetails;
        private NegotiationDetails mNegotiationDetails;
        private AddMessageView mAddMessageView;
        //ManageTreeDragDrop manageOngingTreeDragDrop;
        //ManageTreeDragDrop manageClosedTreeDragDrop;

        #endregion

        #region → Properties     .

        /// <summary>
        /// Gets or sets the Data context of this model
        /// </summary>
        [Import(ViewModelTypes.NegotiationViewModel)]
        public NegotiationViewModel ViewModel
        {
            get
            {
                return (DataContext as NegotiationViewModel);
            }
            set
            {
                DataContext = value;
            }
        }

        /// <summary>
        /// Gets the conversation details.
        /// </summary>
        /// <value>The conversation details.</value>
        public ConversationDetails ConversationDetails
        {
            get
            {
                if (mConversationDetails == null)
                {
                    mConversationDetails = new ConversationDetails();
                }

                if (mConversationDetails.DataContext == null && this.ViewModel != null)
                {
                    mConversationDetails.DataContext = this.ViewModel.MessageVM;
                }
                return mConversationDetails;
            }
        }

        /// <summary>
        /// Gets the negotiation details.
        /// </summary>
        /// <value>The negotiation details.</value>
        public NegotiationDetails NegotiationDetails
        {
            get
            {
                if (mNegotiationDetails == null)
                {
                    mNegotiationDetails = new NegotiationDetails();
                }

                if (mNegotiationDetails.DataContext == null && this.ViewModel != null)
                {
                    mNegotiationDetails.DataContext = this.ViewModel.ConversationVM;
                }
                return mNegotiationDetails;
            }
        }

        /// <summary>
        /// Gets the add message view.
        /// </summary>
        /// <value>The add message view.</value>
        public AddMessageView AddMessageView
        {
            get
            {
                if (mAddMessageView == null)
                {
                    mAddMessageView = new AddMessageView(this.ViewModel);
                }

                return mAddMessageView;
            }
        }
        #endregion

        #region → Constructors   .

        /// <summary>
        ///     Default Constructor
        /// </summary>
        public NegotiationsView()
        {
            InitializeComponent();

            #region " Use MEF To load the View Model "
            if (!GalaSoft.MvvmLight.ViewModelBase.IsInDesignModeStatic)
            {
                // Use MEF To load the View Model
                CompositionInitializer.SatisfyImports(this);

                //manageOngingTreeDragDrop = new ManageTreeDragDrop(uxCntClosedNegotiations.uxTreeClosed, (DataContext as NegotiationViewModel).CloseReOpenSelectedNegotiationCommand);
                //manageClosedTreeDragDrop = new ManageTreeDragDrop(uxCntOngoingNegotiations.uxTreeOngoing, (DataContext as NegotiationViewModel).CloseReOpenSelectedNegotiationCommand);

                AppConfigurations.ApplicationRegion = uxTabsArea;
            }

            #endregion

            #region → Load Application and Application Tab .

            //Hack: Important to be removed...
            IntializationAppIntegrationArea = new IntializationAppIntegration();

            IntializationAppIntegrationArea.Run();

            //Integrate the application in its region
            AppConfigurations.ApplicationRegion.Content = IntializationAppIntegrationArea.ShellView;

            this.Loaded += new System.Windows.RoutedEventHandler(NegotiationsConversationDetailsLoaded);

            #endregion

            #region → Initialize the UserControl Width & Height            .

            this.SizeChanged += new SizeChangedEventHandler(ViewsAdjustSizeAfterResize);
            this.ViewsAdjustSizeAfterResize(null, null);
            #endregion

            #region Registration for needed messages in eNegMessenger

            eNegMessanger.NewPopUp.Register(this, OnAddNewPopUp);
            eNegMessanger.FlippMessage.Register(this, OnFlipMessage);
            #endregion
        }



        #endregion

        #region → Event Handlers .


        /// <summary>
        /// Handles the Loaded event of the NegotiationsConversationDetails control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void NegotiationsConversationDetailsLoaded(object sender, RoutedEventArgs e)
        {
            Dispatcher.BeginInvoke(() =>
            {

                IntializationAppIntegrationArea.ShellView.UxDefaultTab.Content = new TextBlock()
                {
                    Text = "Please select a negotiation or conversation",
                    FontSize = 12,
                    TextWrapping = TextWrapping.Wrap,
                    Margin = new Thickness(10)
                };

                IntializationAppIntegrationArea.ShellView.UxMessagesTab.Content = ConversationDetails;

                IntializationAppIntegrationArea.ShellView.UxAddMessageTab.Content = new ScrollViewer()
                {
                    Content = AddMessageView,
                    VerticalScrollBarVisibility = ScrollBarVisibility.Auto
                };
                this.AddMessageView.HorizontalAlignment = HorizontalAlignment.Left;
                this.AddMessageView.VerticalAlignment = VerticalAlignment.Top;

                this.ViewsAdjustSizeAfterResize(null, null);
            });
        }

        /// <summary>
        /// For Resizing the Controls According to the New Size
        /// </summary>
        /// <param name="sender">Value of Sender</param>
        /// <param name="e">Value of EventArgs Size</param>
        private void ViewsAdjustSizeAfterResize(object sender, SizeChangedEventArgs e)
        {
            if (this.uxTabsArea.ActualHeight > 200 && this.uxTabsArea.ActualWidth > 200)
            {
                Dispatcher.BeginInvoke(() =>
               {
                   MainPlatformInfo.Instance.HostRegionSizeDetails
                       = new ApplicationHostBounds()
                       {
                           Height = uxTabsArea.ActualHeight - 45,
                           Left = uxNegotiationsSide.ActualWidth + 20,
                           Top = 115,
                           Width = uxTabsArea.ActualWidth - 25
                       };
               });
            }
        }

        #endregion

        #region → Methods        .

        #region → Private        .

        /// <summary>
        /// Opens the negotiation details view.
        /// </summary>
        private void OpenNegotiationDetailsView()
        {
            ViewModel.ConversationVM.RefreshConversationsSource();

            //NegotiationDetails.Height = this.ActualHeight;

            IntializationAppIntegrationArea.ShellView.UxMessagesTab.Content = this.NegotiationDetails;

            NegotiationDetails.RaiseCanExecuteCommands();
        }

        /// <summary>
        /// Opens the conversation details view.
        /// </summary>
        private void OpenConversationDetailsView()
        {
            //this.ViewModel.MessageVM.RefreshMessagesSource();

            // this.ConversationDetails.Height = this.ActualHeight;

            IntializationAppIntegrationArea.ShellView.UxMessagesTab.Content = this.ConversationDetails;

            //To expand certain message in case of coming from Add-On through double click on certain message
            eNegMessanger.LoadCompleted.Send(eNegMessanger.OperationType.SeeCertainMessage.ToString());

            ConversationDetails.RaiseCanExecuteCommands();
        }

        /// <summary>
        /// Called when [flip message].
        /// </summary>
        /// <param name="pageName">Name of the page.</param>
        private void OnFlipMessage(string pageName)
        {
            if (pageName == ViewTypes.ConversationDetailsView)
            {
                OpenConversationDetailsView();
            }
            else if (pageName == ViewTypes.NegoitationDetailsView)
            {
                OpenNegotiationDetailsView();
            }
            if (pageName == ViewTypes.RenameNegotiationConversationPopup)
            {
                var renamePopup = new PopUpWindow("Rename");
                var renamNegConv = new RenamNegotiationConversation { DataContext = this.ViewModel };
                renamePopup.Content = renamNegConv;
                renamePopup.CenterWindow();
                renamePopup.ShowDialog();
            }
            else if (pageName == ViewTypes.ApplicationSettingsPopup)
            {
                var renamePopup = new PopUpWindow("Application Settings");
                var applicationSettings = new NegotiationApplicationSettings { DataContext = this.ViewModel };
                renamePopup.Content = applicationSettings;
                renamePopup.CenterWindow();
                renamePopup.ShowDialog();
            }
        }


        /// <summary>
        /// Called when [add new pop up].
        /// </summary>
        /// <param name="popUpName">Name of the pop up.</param>
        private void OnAddNewPopUp(string popUpName)
        {
            if (eNegMessanger.NewPopUp.PopUpType == eNegMessanger.PopUpType.NewNegotiation.ToString())
            {
                #region Show PopUp window to add new negotiation
                var addNewNegotiation = new PopUpWindow(popUpName)
                {
                    DataContext = this.DataContext,
                    Content = new NewNegotiation()
                };
                addNewNegotiation.CenterWindow();
                addNewNegotiation.ShowDialog();
                #endregion
            }
            if (eNegMessanger.NewPopUp.PopUpType == eNegMessanger.PopUpType.NewConversation.ToString())
            {
                #region Show PopUp window to add new conversation
                var addNewconverastion = new PopUpWindow(popUpName)
                {
                    DataContext = this.DataContext,
                    Content = new AddConversation()
                };
                addNewconverastion.CenterWindow();
                addNewconverastion.ShowDialog();
                #endregion
            }
            if (eNegMessanger.NewPopUp.PopUpType == eNegMessanger.PopUpType.SavePDF.ToString())
            {
                #region Show PopUp window to Save just generated PDF for some messages or conversations
                var savePdf = new PopUpWindow(popUpName)
                {
                    DataContext = this.DataContext,
                    Content = new SavePDFView()
                };
                savePdf.CenterWindow();
                savePdf.ShowDialog();
                #endregion
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

            //manageOngingTreeDragDrop.Cleanup();
            //manageClosedTreeDragDrop.Cleanup();
        }

        #endregion

        #endregion

    }
}

