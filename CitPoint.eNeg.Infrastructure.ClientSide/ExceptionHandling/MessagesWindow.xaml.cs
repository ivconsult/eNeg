

#region → Usings   .
using System.Windows;
using System.Windows.Controls;
using System;
using System.Text;
using citPOINT.eNeg.Infrastructure.ExceptionHandling;
using System.Reflection;
#endregion

#region → History  .

/* 
 * Date         User            Change
 * *********************************************
 * 4/3/2011    A.Dergham        • creation
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

namespace citPOINT.eNeg.Infrastructure.ClientSide.ExceptionHandling
{
    /// <summary>
    /// Class that represent the Message Window as a Child Window to appear any handled exction in a friendly form
    /// </summary>
    public partial class MessagesWindow : ChildWindow
    {
        #region → Fields         .

        private Exception mException;

        string appName = string.Empty;

        #endregion

        #region → Constractor    .

        /// <summary>
        /// Constructor with two arguments which is
        /// 1. The message error that will appear in case of raising an exception
        /// 2. The inner exception that occured
        /// </summary>
        /// <param name="header">Header of the message window</param>
        /// <param name="exception">The exception.</param>
        public MessagesWindow(string header, Exception exception)
        {
            InitializeComponent();

            mException = exception;

            txtBlockMesssageHeader.Text = header;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MessagesWindow"/> class.
        /// </summary>
        /// <param name="header">The header.</param>
        /// <param name="exception">The exception.</param>
        /// <param name="appName">Name of the app.</param>
        public MessagesWindow(string header, Exception exception, string appName)
            : this(header, exception)
        {
            InitializeComponent();

            mException = exception;

            txtBlockMesssageHeader.Text = header;

            this.appName = appName;
        }

        #endregion

        #region → Event Handlers .

        /// <summary>
        /// Handles the Click event of the OKButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        /// <summary>
        /// Handles the Click event of the CancelButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        /// <summary>
        /// Handles the KeyDown event of the ChildWindow control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Input.KeyEventArgs"/> instance containing the event data.</param>
        private void ChildWindow_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Ctrl)
            {
                uxcmdCopyToClibBoard.Visibility = System.Windows.Visibility.Visible;
            }
        }

        /// <summary>
        /// Handles the Closing event of the ChildWindow control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.ComponentModel.CancelEventArgs"/> instance containing the event data.</param>
        private void ChildWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            ClientExceptionHandlerProvider.LastShownMessage = null;
        }

        /// <summary>
        /// Handles the Click event of the uxcmdCopyToClibBoard control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void uxcmdCopyToClibBoard_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Clipboard.SetText(GetExceptionDetails());
            }
            catch { }
        }

        #endregion

        #region → Methods        .

        #region → Public         .

        /// <summary>
        /// Gets the exception details.
        /// </summary>
        /// <returns>string</returns>
        private string GetExceptionDetails()
        {
            this.appName = !string.IsNullOrEmpty(this.appName) ? string.Format("[{0}] ", this.appName) : string.Empty;

            StringBuilder sb = new StringBuilder();

            //Adding Date Time
            sb.Append(string.Format("Date/Time:{0} / {1}.{2}", DateTime.Now.ToShortDateString(), DateTime.Now.ToLongDateString(), Environment.NewLine));
            sb.AppendLine(string.Format("Exception Type:{0}", mException.GetType().ToString()));

            if (!string.IsNullOrEmpty(this.appName))
            {
                sb.AppendLine(string.Format("Application Name: {0} ", this.appName));
            }

            sb.AppendLine("Exception Friendly Message:-");

            sb.AppendLine(this.txtBlockMesssageHeader.Text);
            
            sb.AppendLine("Exception Message:-");
            sb.AppendLine(mException.Message);
            sb.Append(Environment.NewLine);


            sb.AppendLine("Exception Stack Trace:-");
            sb.AppendLine(mException.StackTrace);

            if (mException.InnerException != null)
            {

                sb.AppendLine(string.Format("Inner Exception Type:{0}", mException.InnerException.GetType().ToString()));

                sb.AppendLine("Inner Exception Message:-");
                sb.AppendLine(mException.InnerException.Message);

                if (mException.InnerException.GetType().Equals(typeof(ReflectionTypeLoadException)))
                {
                    foreach (var exceptionItem in (mException.InnerException as ReflectionTypeLoadException).LoaderExceptions)
                    {
                        sb.Append(Environment.NewLine);
                        sb.AppendLine(exceptionItem.Message);
                    }
                }
            }

            return sb.ToString();


        }

        #endregion

        #endregion

    }
}

