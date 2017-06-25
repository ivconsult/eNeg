
#region → Usings   .

using System;
using Telerik.Windows.Controls;
#endregion

#region → History  .

/* Date         User              Change
 * 
 * 24.02.11    Yousra.Mohammed       Creation
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
    /// PopUp Window with a HTML placeholder Container to open DataMatching view in PopUp Window after adding New Message in Addon 
    /// </summary>
    public partial class DataMatchingPopUp : RadWindow
    {

        #region → Constructor    .

        /// <summary>
        /// Initializes a new instance of the <see cref="DataMatchingPopUp" /> class.
        /// </summary>
        /// <param name="URL">The URL.</param>
        public DataMatchingPopUp(string URL)
        {
            InitializeComponent();

            uxHtmlContainer.SourceUrl = new Uri(URL, UriKind.Absolute);
            
            CustomInitionlizeRadWindow();
        }
        #endregion

        #region → Methods        .

        #region → Public         .
        /// <summary>
        /// Customs the initionlize RAD window.
        /// </summary>
        public void CustomInitionlizeRadWindow()
        {
            this.WindowStartupLocation = Telerik.Windows.Controls.WindowStartupLocation.CenterScreen;
            this.ResizeMode = ResizeMode.CanResize;
            this.Header = "Data Matching";
            this.CanMove = true;
            this.CanClose = true;
        }

        #endregion

        #endregion
    }
}
