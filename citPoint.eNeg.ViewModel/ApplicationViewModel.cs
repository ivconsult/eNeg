#region → Usings   .
using System;
using System.Linq;
using System.ComponentModel;
using System.ComponentModel.Composition;
using citPOINT.eNeg.Common;
using GalaSoft.MvvmLight;
using System.Collections.Generic;
using citPOINT.eNeg.Data.Web;
using citPOINT.eNeg.Apps.Common.Interfaces;
using citPOINT.eNeg.Common.eNegApps;
using System.Windows;

#endregion

#region → History  .

/* 
 * Date         User            Change
 * *********************************************
 * 18.03.12      mwahab         • creation
 * **********************************************
 */

# endregion

#region → ToDos    .

/*
 * Date         set by User     Description
 * 
 * 
*/

# endregion

namespace citPOINT.eNeg.ViewModel
{

    #region " Using MEF to export Application ViewModel  "
    /// <summary>
    /// Class for ApplicationViewModel 
    /// </summary>
    [Export(ViewModelTypes.ApplicationViewModel)]
    [PartCreationPolicy(CreationPolicy.Shared)]
    #endregion
    public class ApplicationViewModel : ViewModelBase
    {
        #region → Fields         .

        private IApplicationModel mApplicationModel;

        private bool mIsBusy;

        private bool mShowDownloadProgress = true;

        private List<IeNegApplication> meNegApplicationSource;

        private int numberofLoadedApps = 0;

        private IeNegApplication mMessagesApp;

        private IeNegApplication mAddMessagesApp;

        private const string MessagesArea = "Messages";

        private const string AddMessageArea = "Add Message";

        private List<string> LoadedApps = new List<string>();

        #endregion

        #region → Properties     .

        /// <summary>
        /// Gets or sets the instance.
        /// </summary>
        /// <value>The instance.</value>
        public static ApplicationViewModel Instance { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is user logged in.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is user logged in; otherwise, <c>false</c>.
        /// </value>
        public bool IsUserLoggedIn { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is aplications loaded.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is aplications loaded; otherwise, <c>false</c>.
        /// </value>
        public bool IsAplicationsLoaded { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is busy.
        /// </summary>
        /// <value><c>true</c> if this instance is busy; otherwise, <c>false</c>.</value>
        public bool IsBusy
        {
            get
            {
                return mIsBusy;
            }
            private set
            {
                if (value != mIsBusy)
                {
                    mIsBusy = value;
                    this.RaisePropertyChanged("IsBusy");
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [show download progress].
        /// </summary>
        /// <value>
        /// 	<c>true</c> if [show download progress]; otherwise, <c>false</c>.
        /// </value>
        public bool ShowDownloadProgress
        {
            get
            {
                return mShowDownloadProgress;
            }
            set
            {
                mShowDownloadProgress = value;

                this.RaisePropertyChanged("ShowDownloadProgress");
            }
        }

        /// <summary>
        /// Gets or sets the e neg application source.
        /// </summary>
        /// <value>The e neg application source.</value>
        public List<IeNegApplication> eNegApplicationSource
        {
            get
            {
                return meNegApplicationSource;
            }

            set
            {
                meNegApplicationSource = value;

                //Adding static apps.
                if (meNegApplicationSource.Count <= 0)
                {
                    meNegApplicationSource.Add(this.AddMessagesApp);

                    meNegApplicationSource.Add(this.MessagesApp);
                }

                this.RaisePropertyChanged("eNegApplicationSource");
            }
        }

        /// <summary>
        /// Gets the messages app.
        /// </summary>
        /// <value>The messages app.</value>
        private IeNegApplication MessagesApp
        {
            get
            {
                if (mMessagesApp == null)
                {
                    mMessagesApp = new eNegApplication()
                    {
                        ApplicationID = Guid.Empty,
                        ApplicationTitle = MessagesArea,
                        ApplicationRank = -1,
                        IsStaticApp = true,
                        DownloadingPercentage = 100,
                        IsActive = false,
                        RegionName = MessagesArea.Replace(" ", "") + "Region"
                    };
                }

                return mMessagesApp;
            }
        }

        /// <summary>
        /// Gets the add messages app.
        /// </summary>
        /// <value>The add messages app.</value>
        private IeNegApplication AddMessagesApp
        {
            get
            {
                if (mAddMessagesApp == null)
                {
                    mAddMessagesApp = new eNegApplication()
                    {
                        ApplicationID = Guid.Empty,
                        ApplicationTitle = AddMessageArea,
                        ApplicationRank = -2,
                        IsStaticApp = true,
                        DownloadingPercentage = 100,
                        IsActive = false,
                        RegionName = AddMessageArea.Replace(" ", "") + "Region"
                    };
                }

                return mAddMessagesApp;
            }
        }

        /// <summary>
        /// Gets the total number of apps.
        /// </summary>
        /// <value>The total number of apps.</value>
        private int TotalNumberOfApps
        {
            get
            {
                return this.eNegApplicationSource
                           .Where(s => !string.IsNullOrEmpty(s.ModuleName) &&
                                       !string.IsNullOrEmpty(s.AssemblyName) &&
                                       !string.IsNullOrEmpty(s.XapFilePath))
                           .Count();
            }
        }

        #endregion

        #region → Constractor    .

        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationViewModel"/> class.
        /// </summary>
        /// <param name="applicationModel">The application model.</param>
        [ImportingConstructor]
        public ApplicationViewModel(IApplicationModel applicationModel)
        {
            ApplicationViewModel.Instance = this;

            this.mApplicationModel = applicationModel;

            #region → Adding Event Handler .

            this.mApplicationModel.PropertyChanged += new PropertyChangedEventHandler(applicationModel_PropertyChanged);

            this.mApplicationModel.GetApplicationComplete += new EventHandler<eNegEntityResultArgs<Data.Web.Application>>(applicationModel_GetApplicationComplete);

            #endregion

            this.eNegApplicationSource = new List<IeNegApplication>();

            this.GetApplicationAsync();

            #region → Register Messenger   .

            eNegMessanger.ApplyChangesForAppsMessage.Register(this, OnApplicationChanged);

            #endregion

        }

        #endregion

        #region → Event Handlers .

        /// <summary>
        /// Handles the PropertyChanged event of the applicationModel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.ComponentModel.PropertyChangedEventArgs"/> instance containing the event data.</param>
        private void applicationModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            this.IsBusy = this.mApplicationModel.IsBusy;
        }

        /// <summary>
        /// call back of get application complete.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        private void applicationModel_GetApplicationComplete(object sender, eNegEntityResultArgs<Data.Web.Application> e)
        {
            if (!e.HasError)
            {
                //Wrapping Application
                foreach (var appItem in e.Results.OrderBy(o => o.ApplicationRank))
                {
                    this.eNegApplicationSource.Add(new eNegApplication(appItem));
                }

                //This is flag for login process
                this.IsAplicationsLoaded = true;

                //Obsrevable pattern
                MainPlatformInfo.Instance.eNegApplicationList = this.eNegApplicationSource;

                if (this.IsUserLoggedIn)
                {
                    eNegMessanger.ApplyChangesForAppsMessage.Send(new ApplicationChange()
                    {
                        IsForUserChanges = true,
                        CurrentUser = AppConfigurations.CurrentLoginUser
                    });

                    //Hack:Review Name Please.
                    eNegMessanger.ChangeScreenMessage.Send(ViewTypes.MainNegotiationView);
                }

                //In case if no apps to be loaded
                this.UpdateApplicationDownloaded(string.Empty);

            }
            else
            {
                eNegMessanger.RaiseErrorMessage.Send(e.Error);
            }

        }

        #endregion

        #region → Methods        .

        #region → Private        .

        /// <summary>
        /// Gets the application async.
        /// </summary>
        private void GetApplicationAsync()
        {
            this.mApplicationModel.GetApplicationAsync();
        }

        /// <summary>
        /// Called when [application changed].
        /// </summary>
        /// <param name="applicationChange">The application change.</param>
        private void OnApplicationChanged(ApplicationChange applicationChange)
        {
            if (applicationChange != null)
            {
                MainPlatformInfo.Instance.CurrentNegotiation = applicationChange.CurrentNegotiation;
                MainPlatformInfo.Instance.CurrentConversation = applicationChange.CurrentConversation;

                if (applicationChange.IsForUserChanges)
                {
                    MainPlatformInfo.Instance.UserInfo = applicationChange.CurrentUser;
                }

                //Mean selected node is Conversation
                if (MainPlatformInfo.Instance.CurrentConversation != null &&
                    MainPlatformInfo.Instance.CurrentNegotiation == null &&
                    applicationChange.CurrentConversation.Negotiation != null)
                {
                    MainPlatformInfo.Instance.CurrentNegotiation = applicationChange.CurrentConversation.Negotiation;
                }

                Negotiation neg = (MainPlatformInfo.Instance.CurrentNegotiation as Negotiation);

                if (neg != null &&
                    neg.NegotiationApplicationStatus != null &&
                    neg.NegotiationApplicationStatus.Count() > 0
                    &&
                    neg.StatusID == eNegConstant.NegotiationStatus.Ongoing)
                {
                    UpdateApplicationActivation(neg.NegotiationApplicationStatus);
                }
                else
                {
                    DeActivateAll();
                }

                this.AddMessagesApp.IsActive = MainPlatformInfo.Instance.CurrentConversation != null &&
                                               MainPlatformInfo.Instance.CurrentNegotiation != null &&
                                               !MainPlatformInfo.Instance.CurrentNegotiation.IsClosed &&
                                               MainPlatformInfo.Instance.CurrentPlatform == Apps.Common.Enums.PlatformTypes.AddOn;

                this.MessagesApp.IsActive = MainPlatformInfo.Instance.CurrentNegotiation != null ||
                                            MainPlatformInfo.Instance.CurrentConversation != null;

                //Some times u do need to apply changes e.g. in case of closed negotiation or published negotiations
                if (applicationChange.ApplyChanged)
                {
                    MainPlatformInfo.Instance.ApplyChanges();
                }
            }
        }

        /// <summary>
        /// Updates the application activation.
        /// </summary>
        /// <param name="applicationStatusList">The application status list.</param>
        private void UpdateApplicationActivation(IEnumerable<NegotiationApplicationStatu> applicationStatusList)
        {
            foreach (var AppStatusItem in applicationStatusList)
            {
                IeNegApplication enegApp = this.eNegApplicationSource
                                               .Where(s => s.ApplicationID == AppStatusItem.ApplicationID)
                                               .FirstOrDefault();

                if (enegApp != null)
                {
                    enegApp.IsActive = AppStatusItem.Active;
                }
            }
        }

        /// <summary>
        /// De-activate all applications.
        /// </summary>
        private void DeActivateAll()
        {
            foreach (var AppStatusItem in this.eNegApplicationSource)
            {
                AppStatusItem.IsActive = false;
            }
        }

        #endregion

        #region → Public         .

        /// <summary>
        /// Updates the downloading percentage.
        /// </summary>
        /// <param name="moduleName">Name of the module.</param>
        /// <param name="Percentage">The percentage.</param>
        public void UpdateDownloadingPercentage(string moduleName, int Percentage)
        {
            var myapp = this.eNegApplicationSource
                            .Where(s => s.ModuleName == moduleName)
                            .FirstOrDefault();

            if (myapp != null)
            {
                myapp.DownloadingPercentage = Percentage;

                if (Percentage == 100)
                {
                    UpdateApplicationDownloaded(moduleName);
                }
            }
        }

        /// <summary>
        /// Updates the application downloaded.
        /// </summary>
        /// <param name="moduleName">Name of the module.</param>
        public void UpdateApplicationDownloaded(string moduleName)
        {

            if (!string.IsNullOrEmpty(moduleName))
            {
                if (!LoadedApps.Contains(moduleName))
                {
                    LoadedApps.Add(moduleName);

                    numberofLoadedApps += 1;
                }

                //Some times not happen...e.g. last on is 99
                var myapp = this.eNegApplicationSource
                            .Where(s => s.ModuleName == moduleName)
                            .FirstOrDefault();

                if (myapp != null)
                {
                    myapp.DownloadingPercentage = 100;
                }
            }

            if (numberofLoadedApps >= this.TotalNumberOfApps)
            {
                this.ShowDownloadProgress = false;

                MainPlatformInfo.Instance.ApplyChanges();

                numberofLoadedApps = 0;
            }
        }

        /// <summary>
        /// Gets the index of the application.
        /// </summary>
        /// <param name="applicationID">The application ID.</param>
        /// <returns></returns>
        public int? GetApplicationIndex(Guid applicationID) {

            for (int i = 0; i < this.eNegApplicationSource.Count(); i++)
            {
                if (this.eNegApplicationSource.ToList()[i].ApplicationID == applicationID)
                {
                    return i+1;
                }
            }

            return null;
        }

        #endregion

        #endregion
    }
}
