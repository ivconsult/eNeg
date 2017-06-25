
#region → Usings   .
using System.Windows;
using System.Windows.Controls;
using GalaSoft.MvvmLight.Command;
using System.Windows.Media;
#endregion

#region → History  .

/* Date         User
 * 
 * 02.11.10     Yousra Reda
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
    /// Custom User Control for alphabets used in filtering Users --> Manage Users 
    /// </summary>
    public partial class AlphabeticFilter : UserControl
    {
        #region → Fields         .
        
        /// <summary>
        /// Using a DependencyProperty as the backing store for SelectedAlphabet.  This enables animation, styling, binding, etc...
        /// </summary>
        public static readonly DependencyProperty SelectedAlphabetProperty =
            DependencyProperty.Register("SelectedAlphabet", typeof(string), typeof(AlphabeticFilter), null);

        /// <summary>
        /// Using a DependencyProperty as the backing store for FilterCommand.  
        /// This enables animation, styling, binding, etc...
        /// </summary>
        public static readonly DependencyProperty FilterCommandProperty =
            DependencyProperty.Register("FilterCommand", typeof(RelayCommand<string>), typeof(AlphabeticFilter), null);

        #endregion


        #region → Properties     .

        /// <summary>
        /// Gets or sets the selected alphabet.
        /// </summary>
        /// <value>The selected alphabet.</value>
        public string SelectedAlphabet
        {
            get { return (string)GetValue(SelectedAlphabetProperty); }
            set { SetValue(SelectedAlphabetProperty, value); }
        }
        
        /// <summary>
        /// Gets or sets the filter command.
        /// </summary>
        /// <value>The filter command.</value>
        public RelayCommand<string> FilterCommand
        {
            get { return (RelayCommand<string>)GetValue(FilterCommandProperty); }
            set { SetValue(FilterCommandProperty, value); }
        }              

        #endregion Properties
        
        #region → Constructors   .
        /// <summary>
        /// Initializes a new instance of the <see cref="AlphabeticFilter"/> class.
        /// </summary>
        public AlphabeticFilter()
        {
            InitializeComponent();

            GenerateStackPanelAlphabets();
        }
        #endregion Constructor
        
        #region → Event Handlers .

        /// <summary>
        /// Handles the Click event of the alphabet control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void alphabet_Click(object sender, RoutedEventArgs e)
        {
            if (FilterCommand != null)
            {
                FilterCommand.Execute((sender as HyperlinkButton).Content);
                SelectedAlphabet = (sender as HyperlinkButton).Content.ToString();
            }
            BuildAlphabiticFilter();
        }

        #endregion Event Handlers

        #region → Methods        .

        #region → Private        .

        /// <summary>
        /// Gets the stack panel alphabets.
        /// </summary>
        private void GenerateStackPanelAlphabets()
        {
            string strAlpha = "";

            for (int i = 65; i <= 90; i++) // Loop through the ASCII characters 65 to 90
            {
                strAlpha = ((char)i).ToString(); // Convert the int to a char to get the actual character behind the ASCII code

                HyperlinkButton alphabet = new HyperlinkButton();
                alphabet.Content = strAlpha;
                if(SelectedAlphabet == strAlpha)
                    alphabet.Foreground = new SolidColorBrush(Colors.Red);
                alphabet.Margin = new Thickness(5);
                alphabet.Click += new RoutedEventHandler(alphabet_Click);
                uxSPAlphabets.Children.Add(alphabet);
            }

        }

        #endregion Private

        #region → Public         .
        /// <summary>
        /// Builds the alphabitic fiter.
        /// </summary>
        public void BuildAlphabiticFilter()
        {
            uxSPAlphabets.Children.Clear();
            GenerateStackPanelAlphabets();
        }
        #endregion

        #endregion Methods
    }
}
