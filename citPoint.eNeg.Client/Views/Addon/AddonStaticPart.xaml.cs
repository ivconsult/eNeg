
#region → Usings   .
using System;
using System.Windows;
using System.Windows.Controls;
using citPOINT.eNeg.Common;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;

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
    /// Addon Static Part
    /// </summary>
    public partial class AddonStaticPart : UserControl,ICleanup
    {
 
        #region → Constructors   .

        /// <summary>
        /// Initializes a new instance of the <see cref="AddonStaticPart"/> class.
        /// </summary>
        public AddonStaticPart()
        {
            InitializeComponent();


            #region OOB install Links Handling

            Loaded += new RoutedEventHandler(MainPageAddon_Loaded);
            Application.Current.InstallStateChanged += new EventHandler(Current_InstallStateChanged);
            

            if (App.Current.InstallState == InstallState.Installed)
            {
                uxcmdInstallOOB.Visibility = Visibility.Collapsed;
            }
            #endregion

            
            #region initialize the UserControl Width & Height
            //if (!AppConfigurations.IsRunningOutOfBrowser)
            //{
            //    ViewsAdjustSize ViewsAdjustSize = new ViewsAdjustSize(this);
            //    ViewsAdjustSize.BannerHeight = 0;
            //    ViewsAdjustSize.Margin = 0;
            //    ViewsAdjustSize.ReSize();
            //}
            #endregion 
        }

        #endregion
        
        #region → Event Handlers .

        /// <summary>
        /// Event Handler that will be fired automatically according to any change in the OOB installation
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">EventArgs</param>
        private void Current_InstallStateChanged(object sender, EventArgs e)
        {
            switch (Application.Current.InstallState)
            {
                case InstallState.InstallFailed:
                    break;
                case InstallState.Installed:
                    ToggleInstallLink(true);
                    break;
                case InstallState.Installing:
                    break;
                case InstallState.NotInstalled:
                    ToggleInstallLink(false);
                    break;
            }
        }

        /// <summary>
        /// detect the state of the running application whether running in or out ofbrowser to toggle install link 
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">RoutedEventArgs</param>
        private void MainPageAddon_Loaded(object sender, RoutedEventArgs e)
        {
            if (App.Current.IsRunningOutOfBrowser)
            {
                ToggleInstallLink(true);
                
                #region Download any new updates to out of browser Version 
                App.Current.CheckAndDownloadUpdateCompleted += new CheckAndDownloadUpdateCompletedEventHandler(Current_CheckAndDownloadUpdateCompleted);
                App.Current.CheckAndDownloadUpdateAsync();
                #endregion
            }
            else
            {
                if (App.Current.InstallState == InstallState.Installed)
                {
                    ToggleInstallLink(true);
                }
            }
        }

        /// <summary>
        /// Handles the CheckAndDownloadUpdateCompleted event of the Current control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.CheckAndDownloadUpdateCompletedEventArgs"/> instance containing the event data.</param>
        private void Current_CheckAndDownloadUpdateCompleted(object sender, CheckAndDownloadUpdateCompletedEventArgs e)
        {
            if (e.Error == null && e.UpdateAvailable)
            {
                MessageBox.Show("Application updated, please restart to apply changes.");
            }
        }

        /// <summary>
        /// Install application OOB
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">RoutedEventArgs</param>
        private void uxcmdInstallOOB_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Application.Current.Install(); 
            }
            catch (Exception EX)
            {
                if (EX.Message != "Error HRESULT E_FAIL has been returned from a call to a COM component.")
                {
                    throw EX;
                }
            }
        }

        #endregion
        
        #region → Methods        .

        #region → Public         .

        #region Toggle the visiblity of install OOB Link according to the state of installation

        /// <summary>
        /// ToggleInstallLink
        /// </summary>
        /// <param name="hide">hide</param>
        public void ToggleInstallLink(bool hide)
        {
            if (hide)
            {
                uxcmdInstallOOB.Visibility = Visibility.Collapsed;
                uxPipeSeparator.Visibility = Visibility.Collapsed;
            }
            else
            {
                uxcmdInstallOOB.Visibility = Visibility.Visible;
                uxPipeSeparator.Visibility = Visibility.Visible;
            }
        }
        #endregion

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
