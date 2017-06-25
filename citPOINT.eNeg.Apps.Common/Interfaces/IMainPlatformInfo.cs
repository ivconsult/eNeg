#region → Usings   .
using citPOINT.eNeg.Apps.Common.Enums;
using System.Collections.Generic;
using System.Windows.Shapes;
#endregion

#region → History  .

/* 
 * Date         User            Change
 * *********************************************
 * 18.03.12    mwahab         • creation
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

namespace citPOINT.eNeg.Apps.Common.Interfaces
{
    /// <summary>
    /// Interface for Main Platform Information like current user and negotiation
    /// </summary>
    public interface IMainPlatformInfo
    {
        #region → Properties     .

        /// <summary>
        /// Gets or sets the e neg application list.
        /// </summary>
        /// <value>The e neg application list.</value>
        List<IeNegApplication> eNegApplicationList { get; set; }

        /// <summary>
        /// Gets or sets the user info.
        /// </summary>
        /// <value>The user info.</value>
        IUserInfo UserInfo { get; set; }

        /// <summary>
        /// Gets or sets the current negotiation.
        /// </summary>
        /// <value>The current negotiation.</value>
        INegotiation CurrentNegotiation { get; set; }

        /// <summary>
        /// Gets or sets the current conversation.
        /// </summary>
        /// <value>The current conversation.</value>
        IConversation CurrentConversation { get; set; }

        /// <summary>
        /// Gets or sets the handle exception.
        /// </summary>
        /// <value>The handle exception.</value>
        IMainHandleException HandleException { get; }

        /// <summary>
        /// Gets or sets the current platform.
        /// </summary>
        /// <value>The current platform.</value>
        PlatformTypes CurrentPlatform { get; set; }

        /// <summary>
        /// Gets a value indicating whether this instance is running out of browser.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is running out of browser; otherwise, <c>false</c>.
        /// </value>
        bool IsRunningOutOfBrowser { get; set; }

        /// <summary>
        /// Gets the e neg host base address.
        /// </summary>
        /// <value>The e neg host base address.</value>
        string eNegHostBaseAddress { get; }

        /// <summary>
        /// Gets or sets the track changes.
        /// </summary>
        /// <value>The track changes.</value>
        ITrackChanges TrackChanges { get; set; }

        /// <summary>
        /// Gets or sets the host region size details.
        /// </summary>
        /// <value>The host region size details.</value>
        ApplicationHostBounds HostRegionSizeDetails { get; set; }

        #endregion

        #region → Methods        .

        #region → Public         .

        /// <summary>
        /// Applies the changes.
        /// </summary>
        void ApplyChanges();

        /// <summary>
        /// Gets the application info.
        /// </summary>
        /// <param name="appName">Name of the app.</param>
        /// <returns></returns>
        IeNegApplication GetApplicationInfo(string appName);

        #endregion

        #endregion
    }
}
