
#region → Usings   .
using System.Windows.Controls;
using citPOINT.eNeg.Common;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using System;
#endregion

#region → History  .

/* Date         User              Change
 * 
 * 05.08.10     Yousra Reda       Creation
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
    /// Flip of Login Quick Register View
    /// </summary>
    public partial class Login_QuickRegisterView : UserControl, ICleanup
    {

        #region → Constructors   .

        /// <summary>
        /// Initializes a new instance of the <see cref="Login_QuickRegisterView"/> class.
        /// </summary>
        public Login_QuickRegisterView()
        {
            InitializeComponent();

            ViewsAdjustSize ViewsAdjustSize = new ViewsAdjustSize(this);

            #region " Registeration for needed messages in eNegMessenger "
            eNegMessanger.SendCustomMessage.Register(this, OnUpdateMessage);
            eNegMessanger.FlippMessage.Register(this, OnFlippedchanged);
            eNegMessanger.FlippMessage.Register(this, OnFlippedByName);
            #endregion
        }

        #endregion

        #region → Event Handlers .

        /// <summary>
        /// Called when [update message].
        /// </summary>
        /// <param name="Message">The message.</param>
        private void OnUpdateMessage(eNegMessage Message)
        {
            if (Message.ViewName == ViewTypes.QuickRegisterView &&
                Message.ReceiverApplicationID == Guid.Empty)
            {
                this.uxLoginView.ViewModel.CurrentUser.ValidationErrors.Clear();
            }
        }

        #region  ChangeScreenMessage
        /// <summary>
        /// Fliip Control When any layer send OnFlip Message
        /// </summary>
        /// <param name="IsFlipped">if set to <c>true</c> [is flipped].</param>
        private void OnFlippedchanged(bool IsFlipped)
        {
            //uxFlipControl.IsFrontSideVisible = !uxFlipControl.IsFrontSideVisible;
        }

        /// <summary>
        /// Called when [flipped by name].
        /// </summary>
        /// <param name="FrontContent">Content of the front.</param>
        private void OnFlippedByName(string FrontContent)
        {
            switch (FrontContent)
            {
                case ViewTypes.LoginView:
                    //uxFlipControl.IsFrontSideVisible = false;
                    break;

                case ViewTypes.QuickRegisterView:
                    //uxFlipControl.IsFrontSideVisible = true;
                    break;
            }
        }

        #endregion "ChangeScreenMessage"

        #endregion

        #region → Methods        .


        #region → Public         .

        /// <summary>
        /// ICleanup interface implementation
        /// </summary>
        public void Cleanup()
        {
            // call Cleanup on its ViewModel
            if ((ICleanup)this.DataContext != null)
                ((ICleanup)this.DataContext).Cleanup();

            // call Cleanup on its ViewModel
            if ((ICleanup)uxLoginView.DataContext != null)
                ((ICleanup)uxLoginView.DataContext).Cleanup();

            // call Cleanup on its ViewModel
            if ((ICleanup)uxQuickRegister != null)
                ((ICleanup)uxQuickRegister).Cleanup();


            // Cleanup itself
            Messenger.Default.Unregister(this);
        }


        #endregion

        #endregion
    }
}
