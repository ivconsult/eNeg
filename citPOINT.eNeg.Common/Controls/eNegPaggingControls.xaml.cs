

#region → Usings   .

using System.Windows;
using System.Windows.Controls;
using GalaSoft.MvvmLight.Command;

#endregion

#region → History  .

/* Date         User
 * 
 * 26.10.10     Mohamed Abdulwahab
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
    /// eNeg Pagging Controls
    /// </summary>
    public partial class eNegPaggingControls : UserControl
    {

        #region → Fields         .
        #endregion

        #region → Properties     .

        /// <summary>
        /// Gets or sets the navigate command.
        /// </summary>
        /// <value>The navigate command.</value>
        public RelayCommand<string> NavigateCommand
        {
            get { return (RelayCommand<string>)GetValue(NavigateCommandProperty); }
            set { SetValue(NavigateCommandProperty, value); }
        }


        /// <summary>
        ///  Using a DependencyProperty as the backing store for NavigateCommand.
        ///  This enables animation, styling, binding, etc...
        /// </summary>
        public static readonly DependencyProperty NavigateCommandProperty =
            DependencyProperty.Register("NavigateCommand", typeof(RelayCommand<string>), typeof(eNegPaggingControls), null);


        /// <summary>
        /// Gets or sets the page numbers panel.
        /// </summary>
        /// <value>The page numbers panel.</value>
        public StackPanel PageNumbersPanel
        {
            get { return this.uxPnlPagesNumbers; }
            set { uxPnlPagesNumbers = value; }
        }


        #endregion  Properties

        #region → Constructors   .

        /// <summary>
        /// Initializes a new instance of the <see cref="eNegPaggingControls"/> class.
        /// </summary>
        public eNegPaggingControls()
        {
            InitializeComponent();
        }

        #endregion Constructor

        #region → Event Handlers .

        /// <summary>
        /// Handles the Click event of the uxcmd control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void uxcmd_Click(object sender, RoutedEventArgs e)
        {
            if (NavigateCommand != null)
            {
                NavigateCommand.Execute((sender as Button).CommandParameter);
            }
        }

        #endregion Event Handlers


    }
}
