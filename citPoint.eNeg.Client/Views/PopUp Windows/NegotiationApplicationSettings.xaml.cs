#region → Usings   .
using System.Windows.Controls;
using GalaSoft.MvvmLight;
#endregion

#region → History  .
/* Date         User        Change
 * 
 * 16.08.11    m.Wahab     • creation
 * 
 */
#endregion

#region → ToDos    .

/*
 * Date         set by User     Description
 * 
 * 
*/

#endregion
namespace citPOINT.eNeg.Client
{
    /// <summary>
    /// Add New Organization Popup
    /// </summary>
    public partial class NegotiationApplicationSettings : UserControl, ICleanup
    {
        #region → Constructors   .

        /// <summary>
        /// Initializes a new instance of the <see cref="NegotiationApplicationSettings"/> class.
        /// </summary>
        public NegotiationApplicationSettings()
        {
            InitializeComponent();
        }

        #endregion

        #region → Methods        .

        /// <summary>
        /// Cleanups this instance.
        /// </summary>
        public void Cleanup()
        {
        }

        #endregion
    }
}
