#region → Usings   .
using System.Windows.Controls;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
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
    /// Add New Conversation Popup
    /// </summary>
    public partial class AddConversation : UserControl, ICleanup
    {
        #region → Constructors   .

        /// <summary>
        /// Initializes a new instance of the <see cref="AddConversation"/> class.
        /// </summary>
        public AddConversation()
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
                uxcmdSubmitNewConversation.Focus();
                Dispatcher.BeginInvoke(() => { uxcmdSubmitNewConversation.Command.Execute(uxcmdSubmitNewConversation.CommandParameter); });
            }
        }

        #endregion

        #region → Methods        .

        #region → Public         .

        /// <summary>
        /// Cleanups this instance.
        /// </summary>
        public void Cleanup()
        {
            // call Cleanup on its ViewModel
            ((ICleanup)this.DataContext).Cleanup();

            // Cleanup itself
            Messenger.Default.Unregister(this);
        }

        #endregion
             
        #endregion
    }
}
