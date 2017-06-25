
#region → Usings   .
using System.ComponentModel.Composition;
using System.Windows.Controls;
using citPOINT.eNeg.Common;
using citPOINT.eNeg.ViewModel;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using System.Windows.Browser;
using System;
using citPOINT.eNeg.Client.Helper;
#endregion

#region → History  .

/* Date         User            Change
 * 
 * 26.10.10     Yousra Reda     creation
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
    /// Main Container for Negotiations and its details (Messages)
    /// </summary>
    public partial class NegotiationsConversationDetails : UserControl, ICleanup
    {

        #region → Fields         .
        private ManageTreeDragDrop manageOngingTreeDragDrop;
        private ManageTreeDragDrop manageClosedTreeDragDrop;

        private MainNegotiationView mNegotiationsView;
        private ConversationDetails mConversationDetails;
        private ConversationDetails mConversationDetailsForPublished;

        private IntializationAppIntegration IntializationAppIntegrationArea;
        private NegotiationDetails mNegotiationDetails;
        private NegotiationDetails mNegotiationDetailsForPublished;

        #endregion

        #region → Properties     .
        /// <summary>
        /// Gets or sets the Data context of view
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
        /// Gets the negotiation details for published.
        /// </summary>
        /// <value>The negotiation details for published.</value>
        public NegotiationDetails NegotiationDetailsForPublished
        {
            get
            {
                if (mNegotiationDetailsForPublished == null)
                {
                    mNegotiationDetailsForPublished = new NegotiationDetails();
                }

                if (mNegotiationDetailsForPublished.DataContext == null && PublishedNegotiationsView.LastInstance.ViewModel != null)
                {
                    mNegotiationDetailsForPublished.DataContext = PublishedNegotiationsView.LastInstance.ViewModel.ConversationVM;
                }
                return mNegotiationDetailsForPublished;
            }
        }

        /// <summary>
        /// Gets the conversation details for published.
        /// </summary>
        /// <value>The conversation details for published.</value>
        public ConversationDetails ConversationDetailsForPublished
        {
            get
            {
                if (mConversationDetailsForPublished == null)
                {
                    mConversationDetailsForPublished = new ConversationDetails(false);
                }

                if (mConversationDetailsForPublished.DataContext == null && PublishedNegotiationsView.LastInstance.ViewModel != null)
                {
                    mConversationDetailsForPublished.DataContext = PublishedNegotiationsView.LastInstance.ViewModel.MessageVM;
                }
                return mConversationDetailsForPublished;
            }
        }

        /// <summary>
        /// Gets the naegotiations view.
        /// </summary>
        /// <value>The naegotiations view.</value>
        public MainNegotiationView NegotiationsView
        {
            get
            {
                if (mNegotiationsView == null)
                {
                    mNegotiationsView = new MainNegotiationView();
                }
                return mNegotiationsView;
            }
        }
        #endregion

        #region → Constructors   .

        /// <summary>
        /// Initializes a new instance of the <see cref="NegotiationsConversationDetails"/> class.
        /// </summary>
        public NegotiationsConversationDetails()
        {
            InitializeComponent();

            #region → Use MEF To load the View Model and initialize the content of the view  .

            if (!ViewModelBase.IsInDesignModeStatic)
            {
                // Use MEF To load the View Model
                CompositionInitializer.SatisfyImports(this);

                uxNegotiationsContent.Content = NegotiationsView;

                //if ((this.uxNegotiationsContent.Content as MainNegotiationView) != null)
                //{
                //    (DataContext as NegotiationViewModel).uxClosedPagePanel =
                //        (this.uxNegotiationsContent.Content as MainNegotiationView).uxCntClosedNegotiations.uxctlNavigation.PageNumbersPanel;
                //    (DataContext as NegotiationViewModel).uxOnGoingPagePanel =
                //        (this.uxNegotiationsContent.Content as MainNegotiationView).uxCntOngoingNegotiations.uxctlNavigation.PageNumbersPanel;

                //    manageOngingTreeDragDrop =
                //        new ManageTreeDragDrop((this.uxNegotiationsContent.Content as MainNegotiationView).uxCntClosedNegotiations.uxTreeClosed,
                //            (DataContext as NegotiationViewModel).CloseReOpenSelectedNegotiationCommand);
                //    manageClosedTreeDragDrop
                //        = new ManageTreeDragDrop((this.uxNegotiationsContent.Content as MainNegotiationView).uxCntOngoingNegotiations.uxTreeOngoing,
                //            (DataContext as NegotiationViewModel).CloseReOpenSelectedNegotiationCommand);
                //}
            }

            #region → Load Application and Application Tab .

            //Hack: Important to be removed...
            IntializationAppIntegrationArea = new IntializationAppIntegration();

            IntializationAppIntegrationArea.Run();

            //Integrate the application in its region
            AppConfigurations.ApplicationRegion.Content = IntializationAppIntegrationArea.ShellView;

            this.Loaded += new System.Windows.RoutedEventHandler(NegotiationsConversationDetails_Loaded);

            #endregion

            #endregion

            eNegMessanger.FlippMessage.Register(this, OnFlipMessage);


        }


        #endregion

        #region → Event Handlers .

        /// <summary>
        /// Handles the Loaded event of the NegotiationsConversationDetails control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void NegotiationsConversationDetails_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            Dispatcher.BeginInvoke(() =>
            {

                IntializationAppIntegrationArea.ShellView.UxDefaultTab.Content = new TextBlock()
                {
                    Text = "Please Select a negotiation or conversation",
                    FontSize = 12,
                    TextWrapping = System.Windows.TextWrapping.Wrap,
                    Margin = new System.Windows.Thickness(10)
                };

                IntializationAppIntegrationArea.ShellView.UxMessagesTab.Content = this.ConversationDetails;

                IntializationAppIntegrationArea.ShellView.UxAddMessageTab.Content = new TextBlock()
                {
                    Text = " Add New Message Area",
                    FontSize = 12,
                    TextWrapping = System.Windows.TextWrapping.Wrap
                };


            });
        }
        #endregion

        #region → Methods        .

        #region → private        .

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
            else if (pageName == ViewTypes.ConversationDetailsForPublishedView)
            {
                OpenConversationDetailsForPublishedNegView();
            }
            else if (pageName == ViewTypes.NegotiationDetailsForPublishedView)
            {
                OpenNegotiaitonDetailsForPublishedNegView();
            }
            else if (pageName == ViewTypes.RenameNegotiationConversationPopup)
            {
                var renamePopup = new PopUpWindow("Rename");
                var renamNegConv = new RenamNegotiationConversation();
                renamNegConv.DataContext = this.ViewModel;
                renamePopup.Content = renamNegConv;
                renamePopup.CenterWindow();
                renamePopup.ShowDialog();
            }
            else if (pageName == ViewTypes.ApplicationSettingsPopup)
            {
                var renamePopup = new PopUpWindow("Application Settings");
                var applicationSettings = new NegotiationApplicationSettings();
                applicationSettings.DataContext = this.ViewModel;
                renamePopup.Content = applicationSettings;
                renamePopup.CenterWindow();
                renamePopup.ShowDialog();
            }
        }

        /// <summary>
        /// Opens the conversation details view.
        /// </summary>
        private void OpenConversationDetailsView()
        {
            if (IntializationAppIntegrationArea.ShellView.UxMessagesTab!=null)
            {
                IntializationAppIntegrationArea.ShellView.UxMessagesTab.Content = this.ConversationDetails;

                //To expand certain message in case of coming from Add-On through double click on certain message
                eNegMessanger.LoadCompleted.Send(eNegMessanger.OperationType.SeeCertainMessage.ToString());

                ConversationDetails.RaiseCanExecuteCommands();    
            }            
        }

        /// <summary>
        /// Opens the negotiation details view.
        /// </summary>
        private void OpenNegotiationDetailsView()
        {
            this.ViewModel.ConversationVM.RefreshConversationsSource();

            IntializationAppIntegrationArea.ShellView.UxMessagesTab.Content = this.NegotiationDetails;

            NegotiationDetails.RaiseCanExecuteCommands();
        }


        /// <summary>
        /// Opens the conversation details for published neg view.
        /// </summary>
        private void OpenConversationDetailsForPublishedNegView()
        {
            PublishedNegotiationsView.LastInstance.ViewModel.MessageVM.RefreshMessagesSource();

           IntializationAppIntegrationArea.ShellView.UxMessagesTab.Content = this.ConversationDetailsForPublished;

            //To expand certain message in case of coming from Add-On through double click on certain message
            eNegMessanger.LoadCompleted.Send(eNegMessanger.OperationType.SeeCertainMessage.ToString());

            ConversationDetailsForPublished.RaiseCanExecuteCommands();
        }

        /// <summary>
        /// Opens the negotiaiton details for published neg view.
        /// </summary>
        private void OpenNegotiaitonDetailsForPublishedNegView()
        {
            PublishedNegotiationsView.LastInstance.ViewModel.ConversationVM.RefreshConversationsSource();

            IntializationAppIntegrationArea.ShellView.UxMessagesTab.Content = this.NegotiationDetailsForPublished;

            NegotiationDetailsForPublished.RaiseCanExecuteCommands();
        }

        #endregion

        #region → public         .

        /// <summary>
        /// ICleanup interface implementation
        /// </summary>
        public void Cleanup()
        {
            // call Cleanup on its ViewModel
            ((ICleanup)this.DataContext).Cleanup();

            // Cleanup itself
            Messenger.Default.Unregister(this);

            if (manageOngingTreeDragDrop != null &&
                manageClosedTreeDragDrop != null)
            {
                manageOngingTreeDragDrop.Cleanup();
                manageClosedTreeDragDrop.Cleanup();
            }

            ConversationDetails.Cleanup();

            this.NegotiationsView.Cleanup();

            this.Loaded -= new System.Windows.RoutedEventHandler(NegotiationsConversationDetails_Loaded);

        }

        #endregion

        #endregion
    }
}
