#region → Usings   .
using citPOINT.eNeg.Common;
using citPOINT.eNeg.ViewModel;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.IO.IsolatedStorage;
using System.Windows;
using System.Windows.Controls;
using citPOINT.eNeg.Common.eNegApps;
using citPOINT.eNeg.Apps.Common;
#endregion

#region → History  .
/* Date         User            Change
 * 
 * 23.08.10     Dergham        creation
 * 24.08.10    Yousra Reda       MEF/Load Data  
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
    /// Main Negotiation View
    /// </summary>
    public partial class MainNegotiationView : UserControl, ICleanup
    {
        #region → Constructors   .

        /// <summary>
        /// Default constructor
        /// </summary>
        public MainNegotiationView()
        {
            InitializeComponent();

            #region → Initialize the UserControl Width & Height            .

            var viewsAdjustSize = new ViewsAdjustSize(new UserControl());
            viewsAdjustSize.AfterResize += new EventHandler<EventArgsSize>(ViewsAdjustSizeAfterResize);
            viewsAdjustSize.ReSize();

            #endregion initialize the UserControl Width & Height

            #region → Registeration for needed messages in eNegMessenger   .

            eNegMessanger.NewPopUp.Register(this, OpenPopUp);

            AppConfigurations.ApplicationRegion = uxTabsArea;

            #endregion

            this.Loaded += new RoutedEventHandler(MainNegotiationViewLoaded);
        }

        #endregion Constructor

        #region → Event Handlers .

        /// <summary>
        /// Handles the Loaded event of the MainNegotiationView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void MainNegotiationViewLoaded(object sender, RoutedEventArgs e)
        {
            ViewsAdjustSizeAfterResize(sender, null);
        }

        /// <summary>
        /// For Resizing the Controls According to the New Size
        /// </summary>
        /// <param name="sender">Value of Sender</param>
        /// <param name="e">Value of EventArgs Size</param>
        private void ViewsAdjustSizeAfterResize(object sender, EventArgsSize e)
        {
            if (this.uxTabsArea.ActualHeight > 100 && this.uxNegotiationsSide.ActualWidth > 100)
            {
                Dispatcher.BeginInvoke(() =>
                {
                    MainPlatformInfo.Instance.HostRegionSizeDetails
                        = new ApplicationHostBounds()
                        {
                            Height = this.uxTabsArea.ActualHeight - 27,
                            Left = this.uxNegotiationsSide.ActualWidth + 20,
                            Top = 115,
                            Width = this.uxTabsArea.ActualWidth - 2
                        };
                });

            }
        }

        #endregion Event Handlers

        #region → Methods        .


        #region → Private        .

        /// <summary>
        /// Opens the pop up.
        /// </summary>
        /// <param name="windowName">Name of the window.</param>
        private void OpenPopUp(string windowName)
        {

            if (eNegMessanger.NewPopUp.PopUpType == eNegMessanger.PopUpType.SelectOrganizationToPublish.ToString())
            {
                #region Show PopUp window to choose identify select set of organizations want to publish for them
                PopUpWindow SelectOrgnaizatioConfirm = new PopUpWindow(windowName);
                SelectOrgnaizatioConfirm.DataContext = this.DataContext;
                SelectOrgnaizatioConfirm.Content = new PublishNegotiationPopUp();
                SelectOrgnaizatioConfirm.CenterWindow();
                SelectOrgnaizatioConfirm.ShowDialog();
                #endregion
            }
            if (eNegMessanger.NewPopUp.PopUpType == eNegMessanger.PopUpType.SavePDF.ToString())
            {
                #region Show PopUp window to Save just generated PDF for some messages or conversations
                PopUpWindow SavePDF = new PopUpWindow(windowName);
                SavePDF.DataContext = this.DataContext;
                SavePDF.Content = new SavePDFView();
                SavePDF.CenterWindow();
                SavePDF.ShowDialog();
                #endregion
            }
            if (eNegMessanger.NewPopUp.PopUpType == eNegMessanger.PopUpType.NewConversation.ToString())
            {
                #region Show PopUp window to add new conversation
                PopUpWindow addNewconverastion = new PopUpWindow(windowName);
                addNewconverastion.DataContext = this.DataContext;
                addNewconverastion.Content = new AddConversation();
                addNewconverastion.CenterWindow();
                addNewconverastion.ShowDialog();
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
            if (DataContext != null)
                ((ICleanup)this.DataContext).Cleanup();

            // Cleanup itself
            Messenger.Default.Unregister(this);
        }
        #endregion

        #endregion Methods
    }
}
