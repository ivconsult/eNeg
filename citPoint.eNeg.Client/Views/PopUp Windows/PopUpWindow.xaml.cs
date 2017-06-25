#region → Usings   .
using citPOINT.eNeg.Common;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using System.Windows.Media;
using Telerik.Windows.Controls;
using System.Windows.Controls;
#endregion

#region → History  .

/* Date         User              Change
 * 
 * 16.01.11    Yousra.Mohammed       Creation
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
    /// Generic PopUp Window can get different Content and header for it
    /// , but in all cases it has similar properties like Resizing, Modal feature,...etc 
    /// </summary>
    public partial class PopUpWindow : RadWindow, ICleanup
    {
       
        #region → Fields         .

        private bool mIsExit = true; 
        private static int TopMostWindowID = 0;
        private int WindowID = 0;

        #endregion

        #region → Properties     .

        /// <summary>
        /// Gets a value indicating whether this instance is most top window.
        /// Solve the problem of 2 popup over other like my profile and open add new organization
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is most top window; otherwise, <c>false</c>.
        /// </value>        
        public bool IsMostTopWindow
        {
            get
            {
                return this.WindowID == TopMostWindowID;
            }
        }

        #endregion

        #region → Constructor    .

        
        /// <summary>
        /// Initializes a new instance of the <see cref="PopUpWindow"/> class.
        /// </summary>
        public PopUpWindow(string header)
        {
            TopMostWindowID += 1;

            this.WindowID = TopMostWindowID;

            InitializeComponent();

            CustomInitionlizeRadWindow(header);

            eNegMessanger.FlippMessage.Register(this, onFlippingByPageName);

        }
        #endregion

        #region → Event Handlers .

        /// <summary>
        /// Handles the Closed event of the RadWindow control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Windows.Controls.WindowClosedEventArgs"/> instance containing the event data.</param>
        private void RadWindow_Closed(object sender, WindowClosedEventArgs e)
        {
            if (IsMostTopWindow)
            {
                this.Cleanup();

                eNegMessanger.FlippMessage.Send(ViewTypes.ClosePopupView);    
            }
            
        }

        #endregion

        #region → Methods        .

        #region → Private        .

        /// <summary>
        /// Raises the <see cref="E:KeyDown"/> event.
        /// </summary>
        /// <param name="e">The <see cref="System.Windows.Input.KeyEventArgs"/> instance containing the event data.</param>
        protected override void OnKeyDown(System.Windows.Input.KeyEventArgs e)
        {
            base.OnKeyDown(e);
            if (e.Key == System.Windows.Input.Key.Escape)
            {
                mIsExit = true;
                this.Close();
            }
        }

        /// <summary>
        /// Ons the name of the flipping by page.
        /// </summary>
        /// <param name="PageName">Name of the page.</param>
        private void onFlippingByPageName(string PageName)
        {
            if (PageName == ViewTypes.ClosePopupView && IsMostTopWindow)
            {
                mIsExit = false;
                
                this.Close();

                this.Cleanup();

                TopMostWindowID -= 1;
            }
        }
        #endregion

        #region → Public         .
       
        /// <summary>
        /// Customs the initionlize RAD window.
        /// </summary>
        public void CustomInitionlizeRadWindow(string header)
        {
            this.ResizeMode = ResizeMode.NoResize;
            this.Header = header;
            this.ModalBackground = new SolidColorBrush(Colors.LightGray);
            this.ModalBackground.Opacity = 0.5;
            //done especially for multiple add receiver window.
            this.WindowStartupLocation = Telerik.Windows.Controls.WindowStartupLocation.Manual;
            this.Top = 40;
            this.Left = 720;
        }

        /// <summary>
        /// Centers the window.
        /// </summary>
        public void CenterWindow(double width, double height)
        {
            this.Width = width - 100;
            this.Height = height - 100;

            //this.VerticalContentAlignment = System.Windows.VerticalAlignment.Stretch;
            //this.HorizontalContentAlignment = System.Windows.HorizontalAlignment.Stretch;

            //this.VerticalAlignment = System.Windows.VerticalAlignment.Stretch;
            //this.HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch;

            //(this.Content as UserControl).Width = this.Width;
            //(this.Content as UserControl).MaxWidth = this.Width;
            //(this.Content as UserControl).MinWidth = this.Width;
            //(this.Content as UserControl).Height = this.Height;

            this.CenterWindow();
        }

        /// <summary>
        /// Centers the window.
        /// </summary>
        public void CenterWindow()
        {
            this.ModalBackground = new SolidColorBrush(Colors.LightGray);
            this.ModalBackground.Opacity = 0.5;
            this.WindowStartupLocation = Telerik.Windows.Controls.WindowStartupLocation.CenterScreen;
        }

        /// <summary>
        /// Cleanups this instance.
        /// </summary>
        public void Cleanup()
        {
            Messenger.Default.Unregister(this);
        }

        #endregion

        #endregion


    }
}
