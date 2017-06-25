#region → Usings   .

using System;
using System.Linq;
using System.ComponentModel.Composition;
using System.Windows.Controls;
using citPOINT.eNeg.ViewModel;
using Microsoft.Practices.Prism.Modularity;
using citPOINT.eNeg.Common.Controls;
using citPOINT.eNeg.Common;

#endregion

#region → History  .
/*
 * Date            User              Change
 * 20.03.20        M.Wahab            Creation
 * 
 */

#endregion

#region → ToDos    .
#endregion

namespace citPOINT.eNeg.Client
{
    /// <summary>
    /// class for region Area Application Itegration Shell.
    /// </summary>
    [Export]
    public partial class ApplicationItegrationShell : UserControl, IPartImportsSatisfiedNotification
    {
        #region → Fields         .

        private bool isFirstTime = true;

        #endregion

        #region → Properties     .

        /// <summary>
        /// Gets or sets the module manager.
        /// </summary>
        /// <value>The module manager.</value>
        [Import(AllowRecomposition = false)]
        public IModuleManager ModuleManager { get; set; }

        /// <summary>
        /// Gets the application VM.
        /// </summary>
        /// <value>The application VM.</value>
        public ApplicationViewModel ViewModel
        {
            get
            {
                return ApplicationViewModel.Instance;
            }
        }

        /// <summary>
        /// Gets the ux messager control.
        /// </summary>
        /// <value>The ux messager control.</value>
        public eNegTabItem UxMessagesTab
        {
            get
            {
                if (uxtabApplication.Items.Count() > 1)
                {
                    return (eNegTabItem)uxtabApplication.Items[2];
                }

                return null;
            }
        }

        /// <summary>
        /// Gets the ux add message control.
        /// </summary>
        /// <value>The ux add message control.</value>
        public eNegTabItem UxAddMessageTab
        {
            get
            {
                if (uxtabApplication.Items.Count() > 2)
                {
                    return (eNegTabItem)uxtabApplication.Items[1];
                }

                return null;
            }
        }

        /// <summary>
        /// Gets the ux default tab.
        /// </summary>
        /// <value>The ux default tab.</value>
        public eNegTabItem UxDefaultTab
        {
            get
            {
                if (uxtabApplication.Items.Count() > 0)
                {
                    return (eNegTabItem)uxtabApplication.Items[0];
                }

                return null;
            }
        }

        #endregion

        #region → Constructors   .

        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationItegrationShell"/> class.
        /// </summary>
        public ApplicationItegrationShell()
        {
            this.DataContext = ViewModel;

            InitializeComponent();

            eNegMessanger.AtivateApplicationMessage.Register(this, OnActivateApplication);
        }

        #endregion

        #region → Event Handlers .

        /// <summary>
        /// Handles the ModuleDownloadProgressChanged event of the ModuleManager control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Microsoft.Practices.Prism.Modularity.ModuleDownloadProgressChangedEventArgs"/> instance containing the event data.</param>
        private void ModuleManager_ModuleDownloadProgressChanged(object sender, ModuleDownloadProgressChangedEventArgs e)
        {
            ViewModel.UpdateDownloadingPercentage(e.ModuleInfo.ModuleName, e.ProgressPercentage);
        }

        /// <summary>
        /// Handles the LoadModuleCompleted event of the ModuleManager control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Microsoft.Practices.Prism.Modularity.LoadModuleCompletedEventArgs"/> instance containing the event data.</param>
        private void ModuleManager_LoadModuleCompleted(object sender, LoadModuleCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                Dispatcher.BeginInvoke(() =>
                {
                    ViewModel.UpdateApplicationDownloaded(e.ModuleInfo.ModuleName);
                });
            }
            else
            {
                eNegMessanger.RaiseErrorMessage.ApplicationName = e.ModuleInfo.ModuleName;
                eNegMessanger.RaiseErrorMessage.Send(e.Error);
                eNegMessanger.RaiseErrorMessage.ApplicationName = string.Empty;
                e.IsErrorHandled = true;
            }
        }

        #endregion

        #region → Methods        .

        #region → Private        .

        /// <summary>
        /// Called when [activate application].
        /// </summary>
        /// <param name="applicationID">The application ID.</param>
        private void OnActivateApplication(Guid applicationID)
        {
            Dispatcher.BeginInvoke(() =>
            {
                int? index = this.ViewModel.GetApplicationIndex(applicationID);

                if (index.HasValue)
                {
                    this.uxtabApplication.SelectedIndex = index.Value;
                }
            });
        }

        #endregion

        #region → Public         .

        /// <summary>
        /// Called when a part's imports have been satisfied and it is safe to use.
        /// </summary>
        public void OnImportsSatisfied()
        {
            if (this.isFirstTime)
            {
                isFirstTime = false;

                ModuleManager.LoadModuleCompleted += new EventHandler<LoadModuleCompletedEventArgs>(ModuleManager_LoadModuleCompleted);

                ModuleManager.ModuleDownloadProgressChanged += new EventHandler<ModuleDownloadProgressChangedEventArgs>(ModuleManager_ModuleDownloadProgressChanged);
            }
        }

        #endregion

        #endregion

    }
}

