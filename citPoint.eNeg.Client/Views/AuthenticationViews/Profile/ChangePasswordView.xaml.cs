using System.Windows.Controls;

namespace citPOINT.eNeg.Client
{
    /// <summary>
    /// Change Password View.
    /// </summary>
    public partial class ChangePasswordView : UserControl
    {
        #region → Constractors   .

        /// <summary>
        /// Initializes a new instance of the <see cref="ChangePasswordView"/> class.
        /// </summary>
        public ChangePasswordView()
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
                uxcmdSubmitChangePassword.Focus();
                Dispatcher.BeginInvoke(() => { uxcmdSubmitChangePassword.Command.Execute(uxcmdSubmitChangePassword.CommandParameter); });
            }
        }

        #endregion
    }
}
