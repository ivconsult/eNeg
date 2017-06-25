#region → Usings   .
using System;
using System.ComponentModel;
using citPOINT.eNeg.Apps.Common.Interfaces;
using citPOINT.eNeg.Data.Web;
using citPOINT.eNeg.Apps.Common.Enums;
#endregion

#region → History  .

/* Date         User            change
 * 
 * 18.03.12     M.Wahab     creation
 */

# endregion

#region → ToDos    .

/*
 * Date         set by User     Description
 * 
*/

# endregion
namespace citPOINT.eNeg.Common.eNegApps
{
    /// <summary>
    /// eNeg Application.
    /// </summary>
    public class eNegApplication : IeNegApplication, INotifyPropertyChanged
    {
        #region → Fields         .

        private int mDownloadingPercentage;
        private string mRegionName;
        private bool mIsActive;
        bool mIsStaticApp = false;
        private Guid mApplicationID;
        private int mApplicationRank;
        private string mApplicationTitle;
        private string mApplicationBaseAddress;
        private string mApplicationMainServicePath;
        private string mModuleName;
        private string mXapFilePath;
        private string mAssemblyName;

        private ViewsTypes mDefaultView = ViewsTypes.Report;

        #endregion

        #region → Properties     .

        /// <summary>
        /// Gets or sets a value indicating whether this instance is static app.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is static app; otherwise, <c>false</c>.
        /// </value>
        public bool IsStaticApp
        {
            get
            {
                return mIsStaticApp;
            }
            set
            {
                mIsStaticApp = value;
                this.RaisePropertyChanged("IsStaticApp");
            }
        }

        /// <summary>
        /// Gets or sets the application ID.
        /// </summary>
        /// <value>The application ID.</value>
        public Guid ApplicationID
        {
            get
            {
                return this.mApplicationID;
            }
            set
            {
                this.mApplicationID = value;

                this.RaisePropertyChanged("ApplicationID");
            }
        }

        /// <summary>
        /// Gets or sets the name of the region.
        /// </summary>
        /// <value>The name of the region.</value>
        public string RegionName
        {
            get
            {
                return mRegionName;
            }

            set
            {
                if (value != mRegionName)
                {
                    mRegionName = value;
                    this.RaisePropertyChanged("RegionName");
                }
            }
        }

        /// <summary>
        /// Gets or sets the application title.
        /// </summary>
        /// <value>The application title.</value>
        public string ApplicationTitle
        {
            get
            {
                return mApplicationTitle;
            }

            set
            {
                if (value != mApplicationTitle)
                {
                    mApplicationTitle = value;
                    this.RaisePropertyChanged("ApplicationTitle");
                }
            }
        }

        /// <summary>
        /// Gets or sets the application rank.
        /// </summary>
        /// <value>The application rank.</value>
        public int ApplicationRank
        {
            get
            {
                return mApplicationRank;
            }
            set
            {

                mApplicationRank = value;
                this.RaisePropertyChanged("ApplicationRank");
            }
        }

        /// <summary>
        /// Gets or sets the application base address.
        /// </summary>
        /// <value>The application base address.</value>
        public string ApplicationBaseAddress
        {
            get
            {
                return mApplicationBaseAddress;
            }
            set
            {
                mApplicationBaseAddress = value;
                this.RaisePropertyChanged("mApplicationBaseAddress");
            }
        }

        /// <summary>
        /// Gets or sets the application main service path.
        /// </summary>
        /// <value>The application main service path.</value>
        public string ApplicationMainServicePath
        {
            get
            {
                return mApplicationMainServicePath;
            }
            set
            {
                mApplicationMainServicePath = value;
                this.RaisePropertyChanged("ApplicationMainServicePath");
            }
        }

        /// <summary>
        /// Gets or sets the name of the module.
        /// </summary>
        /// <value>The name of the module.</value>
        public string ModuleName
        {
            get
            {
                return mModuleName;
            }

            set
            {
                mModuleName = value;
                this.RaisePropertyChanged("ModuleName");
            }
        }

        /// <summary>
        /// Gets or sets the xap file path.
        /// </summary>
        /// <value>The xap file path.</value>
        public string XapFilePath
        {
            get
            {
                return mXapFilePath;
            }

            set
            {
                mXapFilePath = value;
                this.RaisePropertyChanged("XapFilePath");
            }
        }

        /// <summary>
        /// Gets or sets the name of the assembly.
        /// </summary>
        /// <value>The name of the assembly.</value>
        public string AssemblyName
        {
            get
            {
                return mAssemblyName;
            }

            set
            {
                mAssemblyName = value;
                this.RaisePropertyChanged("AssemblyName");
            }
        }

        /// <summary>
        /// Gets or sets the is active.
        /// </summary>
        /// <value>The is active.</value>
        public bool IsActive
        {
            get
            {
                return mIsActive;
            }

            set
            {
                if (value != mIsActive)
                {
                    mIsActive = value;
                    this.RaisePropertyChanged("IsActive");
                }
            }
        }
        /// <summary>
        /// Gets or sets the downloading percentage.
        /// </summary>
        /// <value>The downloading percentage.</value>
        public int DownloadingPercentage
        {
            get { return mDownloadingPercentage; }

            set
            {
                mDownloadingPercentage = value;
                this.RaisePropertyChanged("DownloadingPercentage");
                this.RaisePropertyChanged("ShowDownloadProgress");
            }
        }

        /// <summary>
        /// Gets a value indicating whether [show download progress].
        /// </summary>
        /// <value>
        /// 	<c>true</c> if [show download progress]; otherwise, <c>false</c>.
        /// </value>
        public bool ShowDownloadProgress
        {
            get
            {
                return this.DownloadingPercentage < 100;
            }
        }

        /// <summary>
        /// Gets a value indicating whether this instance is ready to download.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is ready to download; otherwise, <c>false</c>.
        /// </value>
        public bool IsReadyToDownload
        {
            get
            {
                return !string.IsNullOrEmpty(this.ModuleName) &&
                    !string.IsNullOrEmpty(this.AssemblyName) &&
                    !string.IsNullOrEmpty(this.XapFilePath) &&
                    !IsStaticApp;
            }
        }

        /// <summary>
        /// Gets or sets the default view.
        /// </summary>
        /// <value>The default view.</value>
        public ViewsTypes DefaultView
        {
            get
            {
                return mDefaultView;
            }

            set
            {
                mDefaultView = value;
                this.RaisePropertyChanged("DefaultView");
            }
        }

        #endregion

        #region → Contructor     .

        /// <summary>
        /// Initializes a new instance of the <see cref="eNegApplication"/> class.
        /// </summary>
        public eNegApplication()
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="eNegApplication"/> class.
        /// </summary>
        /// <param name="application">The application.</param>
        public eNegApplication(Application application)
            : this()
        {
            this.ApplicationID = application.ApplicationID;
            this.ApplicationRank = application.ApplicationRank;
            this.ApplicationBaseAddress = application.ApplicationBaseAddress;
            this.ApplicationMainServicePath = application.ApplicationMainServicePath;
            this.ApplicationTitle = application.ApplicationTitle;
            this.AssemblyName = application.AssemblyName;
            this.ModuleName = application.ModuleName;
            this.RegionName = application.ApplicationTitle.Replace(" ", "") + "Region";
            this.XapFilePath = application.XapFilePath;
            this.IsActive = true;

            if (this.IsReadyToDownload)
            {
                this.DownloadingPercentage = 0;
            }
            else
            {
                this.DownloadingPercentage = 100;
            }


            //Set default view like in case of prefapp setting view.
            if (!string.IsNullOrEmpty(AppConfigurations.ActionTypeParameter) && 
                AppConfigurations.ApplicationIDParameter.HasValue && 
                AppConfigurations.ApplicationIDParameter.Value == this.ApplicationID)
            {
                if (AppConfigurations.ActionTypeParameter.Equals(ViewsTypes.AppSettings.ToString(), StringComparison.InvariantCultureIgnoreCase))
                {
                    this.DefaultView = ViewsTypes.AppSettings;    
                }
            }

        }

        #endregion

        #region → Events         .

        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region → Methods        .

        #region → Private        .

        /// <summary>
        /// Raises the property changed.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        private void RaisePropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion

        #endregion

    }
}
