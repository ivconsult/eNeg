#region → Usings   .
using System.ComponentModel.Composition;
using citPOINT.eNeg.Apps.Common.Enums;
using citPOINT.eNeg.Apps.Common.Interfaces;
using System.Windows.Threading;
using System.Windows;
using System.Linq;
using System.Collections.Generic;
using System.Windows.Shapes;
using citPOINT.eNeg.Apps.Common;
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
    /// Concrete class for Main Platform Info.
    /// </summary>
    public class MainPlatformInfo : IMainPlatformInfo
    {
        #region → Fields         .

        private static MainPlatformInfo mInstance;

        private ITrackChanges mTrackChanges;

        private List<IeNegApplication> meNegApplicationList;
        
        private ApplicationHostBounds mHostRegionSizeDetails;

        #endregion

        #region → Properties     .

        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <value>The instance.</value>
        public static MainPlatformInfo Instance
        {
            get
            {
                if (mInstance == null)
                {

                    mInstance = new MainPlatformInfo();

                    mInstance.HostRegionSizeDetails = new ApplicationHostBounds() { Height = 200, Width = 200, Left = 0, Top = 0 };
                }

                return mInstance;
            }
        }

        /// <summary>
        /// Gets or sets the e neg application list.
        /// </summary>
        /// <value>The e neg application list.</value>
        public List<IeNegApplication> eNegApplicationList
        {
            get
            {
                return meNegApplicationList;
            }
            set
            {
                meNegApplicationList = value;

                this.TrackChanges = new TrackChanges(meNegApplicationList);
            }
        }

        /// <summary>
        /// Gets or sets the user info.
        /// </summary>
        /// <value>The user info.</value>
        public IUserInfo UserInfo { get; set; }

        /// <summary>
        /// Gets or sets the current negotiation.
        /// </summary>
        /// <value>The current negotiation.</value>
        public INegotiation CurrentNegotiation { get; set; }

        /// <summary>
        /// Gets or sets the current conversation.
        /// </summary>
        /// <value>The current conversation.</value>
        public IConversation CurrentConversation { get; set; }

        /// <summary>
        /// Gets or sets the handle exception.
        /// </summary>
        /// <value>The handle exception.</value>
        public IMainHandleException HandleException
        {
            get
            {
                return MainHandleException.Instance;
            }
        }

        /// <summary>
        /// Gets or sets the current platform.
        /// </summary>
        /// <value>The current platform.</value>
        public PlatformTypes CurrentPlatform { get; set; }

        /// <summary>
        /// Gets or sets the track changes.
        /// </summary>
        /// <value>The track changes.</value>
        public ITrackChanges TrackChanges { get; set; }

        /// <summary>
        /// Gets a value indicating whether this instance is running out of browser.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is running out of browser; otherwise, <c>false</c>.
        /// </value>
        public bool IsRunningOutOfBrowser { get; set; }

        /// <summary>
        /// Gets the e neg host base address.
        /// </summary>
        /// <value>The e neg host base address.</value>
        public string eNegHostBaseAddress
        {
            get
            {
                return AppConfigurations.HostBaseAddress;
            }
        }

        /// <summary>
        /// Gets or sets the host region size details.
        /// </summary>
        /// <value>The host region size details.</value>
        public ApplicationHostBounds HostRegionSizeDetails
        {
            get
            {
                return mHostRegionSizeDetails;
            }
            set
            {
                mHostRegionSizeDetails = value;
            }
        }

        #endregion

        #region → Constractor    .

        /// <summary>
        /// Initializes a new instance of the <see cref="MainPlatformInfo"/> class.
        /// </summary>
        private MainPlatformInfo()
        {

        }

        #endregion

        #region → Methods        .

        #region → Public         .

        /// <summary>
        /// Applies the changes.
        /// </summary>
        public void ApplyChanges()
        {
            if (this.TrackChanges != null)
            {
                Deployment.Current.Dispatcher.BeginInvoke(() =>
                                       {
                                           this.TrackChanges.Notify();
                                       });
            }
        }

        /// <summary>
        /// Gets the application info.
        /// </summary>
        /// <param name="appName">Name of the app.</param>
        /// <returns></returns>
        public IeNegApplication GetApplicationInfo(string appName)
        {
            if (this.eNegApplicationList != null)
            {
                var app = this.eNegApplicationList
                              .Where(s => s.ApplicationTitle.Equals(appName, System.StringComparison.InvariantCultureIgnoreCase))
                              .FirstOrDefault();

                if (app != null)
                {
                    return app;
                }
            }

            return null;
        }

        #endregion

        #endregion

    }
}
