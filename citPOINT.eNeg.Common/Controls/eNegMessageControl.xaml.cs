

#region → Usings   .
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
#endregion

#region → History  .

/* Date         User            Change
 * 
 * 29.11.10     Yousra Reda     creation
 */

# endregion

#region → ToDos    .

/*
 * Date         set by User     Description
 * 
 * 
*/

# endregion



namespace citPOINT.eNeg.Common
{

    /// <summary>
    /// Enumerator that represent types for images used for that eNeg message
    /// </summary>
    public enum ImageType
    {
        /// <summary>
        /// For Success
        /// </summary>
        Success = 0,
        /// <summary>
        /// For Info
        /// </summary>
        Info = 1,
        /// <summary>
        /// For Error
        /// </summary>
        Error = 2,
        /// <summary>
        /// For Warning
        /// </summary>
        Warning = 3
    }
    /// <summary>
    /// eNeg Message Control as  MessageBox
    /// </summary>
    public partial class eNegMessageControl : UserControl
    {
        #region → Fields         .

        /// <summary>
        /// Using a DependencyProperty as the backing store for MessageText.  This enables animation, styling, binding, etc...
        /// </summary>
        public static readonly DependencyProperty MessageTextProperty =
            DependencyProperty.Register("MessageText", typeof(string), typeof(eNegMessageControl), null);

        private int mTimerCounter;
        private DispatcherTimer mSucessMSGTimer = new DispatcherTimer();



        /// <summary>
        /// Gets or sets the type of the message.
        /// </summary>
        /// <value>The type of the message.</value>
        public ImageType MessageType
        {
            get { return (ImageType)GetValue(MessageTypeProperty); }
            set
            {
                SetValue(MessageTypeProperty, value);
                uxImgSuccess.Visibility = System.Windows.Visibility.Collapsed;
                uxImgInfo.Visibility = System.Windows.Visibility.Collapsed;
                uxImgErrors.Visibility = System.Windows.Visibility.Collapsed;
                uxImgWarning.Visibility = System.Windows.Visibility.Collapsed;


                switch (value)
                {
                    case ImageType.Success:
                        uxImgSuccess.Visibility = System.Windows.Visibility.Visible;
                        break;
                    case ImageType.Info:
                        uxImgInfo.Visibility = System.Windows.Visibility.Visible;
                        break;
                    case ImageType.Error:
                        uxImgErrors.Visibility = System.Windows.Visibility.Visible;
                        break;
                    case ImageType.Warning:
                        uxImgWarning.Visibility = System.Windows.Visibility.Visible;
                        break;
                    default:
                        uxImgSuccess.Visibility = System.Windows.Visibility.Visible;
                        break;
                }



            }
        }

        /// <summary>
        /// Using a DependencyProperty as the backing store for MessageType.  This enables animation, styling, binding, etc...
        /// </summary>
        public static readonly DependencyProperty MessageTypeProperty =
            DependencyProperty.Register("MessageType", typeof(ImageType), typeof(eNegMessageControl), null);


        #endregion

        #region → Properties     .
        /// <summary>
        /// Gets or sets the message text.
        /// </summary>
        /// <value>The message text.</value>
        public string MessageText
        {
            get { return (string)GetValue(MessageTextProperty); }
            set
            {
                SetValue(MessageTextProperty, value);
                this.uxlblUpdateMessage.Text = value;
            }
        }
        #endregion

        #region → Constructors   .

        /// <summary>
        /// Initializes a new instance of the <see cref="eNegMessageControl" /> class.
        /// </summary>
        public eNegMessageControl()
        {
            InitializeComponent();
            mSucessMSGTimer.Interval = new TimeSpan(0, 0, 0, 0, 100); // 100 Milliseconds 
            mSucessMSGTimer.Tick += new EventHandler(mSucessMSGTimer_Tick);
        }
        #endregion

        #region → Events         .

        /// <summary>
        /// Occured when the Timer Complted and the Control be Unvisible.
        /// </summary>
        public EventHandler Completed;

        #endregion

        #region → Event Handlers .

        /// <summary>
        /// Handles the Tick event of the mSucessMSGTimer control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
        void mSucessMSGTimer_Tick(object sender, EventArgs e)
        {
            mTimerCounter++;
            this.Opacity -= 0.02;
            if (mTimerCounter == 50)
            {

                mTimerCounter = 0;
                mSucessMSGTimer.Stop();
                this.Visibility = System.Windows.Visibility.Collapsed;

                if (Completed != null)
                    Completed(this, new EventArgs());

            }
        }

        #endregion

        #region → Methods        .

        #region → Public         .

        /// <summary>
        /// Hides this instance.
        /// </summary>
        public void Hide()
        {
            mSucessMSGTimer.Stop();
            mTimerCounter = 0;
            this.Visibility = System.Windows.Visibility.Collapsed;
        }

        /// <summary>
        /// Shows this instance.
        /// </summary>
        public void Show()
        {
            this.Visibility = System.Windows.Visibility.Visible;
            this.Opacity = 1;

            if (!mSucessMSGTimer.IsEnabled)
            {
                mTimerCounter = 0;
                mSucessMSGTimer.Start();
            }
        }
        #endregion
        #endregion
    }
}
