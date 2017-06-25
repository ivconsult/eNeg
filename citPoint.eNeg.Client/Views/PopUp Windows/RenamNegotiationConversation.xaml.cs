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
    public partial class RenamNegotiationConversation : UserControl, ICleanup
    {
        #region → Constructors   .

        /// <summary>
        /// Initializes a new instance of the <see cref="RenamNegotiationConversation"/> class.
        /// </summary>
        public RenamNegotiationConversation()
        {
            InitializeComponent();
        }

        #endregion

        #region → Event Handlers .

        /// <summary>
        /// For Make Save button as Accept Key
        /// </summary>
        /// <param name="sender">Value of Sender</param>
        /// <param name="e">Value of KeyEventArgs</param>
        private void UserControl_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                uxcmdSubmitNewOrganization.Focus();
                Dispatcher.BeginInvoke(() => { uxcmdSubmitNewOrganization.Command.Execute(uxcmdSubmitNewOrganization.CommandParameter); });
            }
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
