using System.Windows.Controls;

namespace citPOINT.eNeg.Client
{
    /// <summary>
    /// Change Email Address View
    /// </summary>
    public partial class ChangeEmailAddressView : UserControl
    {
        #region → Constructors   .

        /// <summary>
        /// Initializes a new instance of the <see cref="ChangeEmailAddressView"/> class.
        /// </summary>
        public ChangeEmailAddressView()
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
                uxcmdSubmitChangeEmailAddress.Focus();

                Dispatcher.BeginInvoke(() => uxcmdSubmitChangeEmailAddress.Command.Execute(uxcmdSubmitChangeEmailAddress.CommandParameter));
            }
        }

        #endregion
    }
}
