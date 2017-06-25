#region → Usings   .
using System;
using System.ComponentModel;
using citPOINT.eNeg.Apps.Common.Enums;
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
    /// Interface for eNeg Application.
    /// </summary>
    public interface IeNegApplication
    {
        #region → Properties     .

        /// <summary>
        /// Gets or sets the application ID.
        /// </summary>
        /// <value>The application ID.</value>
        Guid ApplicationID { get; set; }
        
        /// <summary>
        /// Gets or sets the application rank.
        /// </summary>
        /// <value>The application rank.</value>
        int ApplicationRank { get; set; }

        /// <summary>
        /// Gets or sets the application base address.
        /// </summary>
        /// <value>The application base address.</value>
        string ApplicationBaseAddress { get; set; }

        /// <summary>
        /// Gets or sets the application main service path.
        /// </summary>
        /// <value>The application main service path.</value>
        string ApplicationMainServicePath { get; set; }

        /// <summary>
        /// Gets or sets the application title.
        /// </summary>
        /// <value>The application title.</value>
        string ApplicationTitle { get; set; }

        /// <summary>
        /// Gets or sets the name of the assemby.
        /// </summary>
        /// <value>The name of the assemby.</value>
        string AssemblyName { get; set; }

        /// <summary>
        /// Gets or sets the name of the module.
        /// </summary>
        /// <value>The name of the module.</value>
        string ModuleName { get; set; }

        /// <summary>
        /// Gets or sets the xap file path.
        /// </summary>
        /// <value>The xap file path.</value>
        string XapFilePath { get; set; }

        /// <summary>
        /// Gets or sets the name of the region.
        /// </summary>
        /// <value>The name of the region.</value>
        string RegionName { get; set; }

        /// <summary>
        /// Gets or sets the is active.
        /// </summary>
        /// <value>The is active.</value>
        bool IsActive { get; set; }

        /// <summary>
        /// Gets or sets the downloading percentage.
        /// </summary>
        /// <value>The downloading percentage.</value>
        int DownloadingPercentage { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is static app.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is static app; otherwise, <c>false</c>.
        /// </value>
        bool IsStaticApp { get; set; }

        /// <summary>
        /// Gets a value indicating whether [show download progress].
        /// </summary>
        /// <value>
        /// 	<c>true</c> if [show download progress]; otherwise, <c>false</c>.
        /// </value>
        bool ShowDownloadProgress { get; }

        /// <summary>
        /// Gets or sets the default view.
        /// </summary>
        /// <value>The default view.</value>
        ViewsTypes DefaultView { get; set; }

        #endregion

        #region → Events         .

        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        event PropertyChangedEventHandler PropertyChanged;

        #endregion
    }
}
