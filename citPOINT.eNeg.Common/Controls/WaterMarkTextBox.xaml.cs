
#region → Usings   .
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
#endregion

#region → History  .

/* Date         User            Change
 * 
 * 05.05.10     Yousra Reda     creation
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
    /// Custom control to act as a watermark TextBox
    /// </summary>
    public partial class WaterMarkTextBox : UserControl
    {

        #region → Fields         .

        private bool isFocused = false;

        /// <summary>
        /// Using a DependencyProperty as the backing store for Text.  This enables animation, styling, binding, etc...
        /// </summary>
        public static readonly DependencyProperty BindPropertyProperty =
            DependencyProperty.Register("BindProperty", typeof(string), typeof(WaterMarkTextBox), null);

        /// <summary>
        /// Using a DependencyProperty as the backing store for Text.  This enables animation, styling, binding, etc... 
        /// </summary>
        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(WaterMarkTextBox), new PropertyMetadata(new PropertyChangedCallback(OnTextChanging)));

        /// <summary>
        /// Using a DependencyProperty as the backing store for WaterMark.  This enables animation, styling, binding, etc... 
        /// </summary>
        public static readonly DependencyProperty WaterMarkProperty =
            DependencyProperty.Register("WaterMark", typeof(string), typeof(WaterMarkTextBox), new PropertyMetadata(new PropertyChangedCallback(OnWaterMarkChanging)));
        #endregion

        #region → Properties     .

        /// <summary>
        /// Gets or sets the bind property.
        /// </summary>
        /// <value>The bind property.</value>
        public string BindProperty
        {
            get { return (string)GetValue(BindPropertyProperty); }
            set { SetValue(BindPropertyProperty, value); }
        }

        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        /// <value>The text.</value>
        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        /// <summary>
        /// Gets or sets the water mark.
        /// </summary>
        /// <value>The water mark.</value>
        public string WaterMark
        {
            get { return (string)GetValue(WaterMarkProperty); }
            set { SetValue(WaterMarkProperty, value); }
        }

        #endregion

        #region → Constructors   .

        /// <summary>
        /// Initializes a new instance of the <see cref="WaterMarkTextBox"/> class.
        /// </summary>
        public WaterMarkTextBox()
        {
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(WaterMarkTextBox_Loaded);
        }
        #endregion

        #region → Event Handlers .

        /// <summary>
        /// Handles the Loaded event of the WaterMarkTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        void WaterMarkTextBox_Loaded(object sender, RoutedEventArgs e)
        {
            SetBindingToTextProperty();
        }

        /// <summary>
        /// Called when [text changing].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.Windows.DependencyPropertyChangedEventArgs"/> instance containing the event data.</param>
        private static void OnTextChanging(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            WaterMarkTextBox myControl = (sender as WaterMarkTextBox);

            myControl.uxTextBox.Text = e.NewValue.ToString();
        }

        /// <summary>
        /// Called when [water mark changing].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.Windows.DependencyPropertyChangedEventArgs"/> instance containing the event data.</param>
        private static void OnWaterMarkChanging(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            WaterMarkTextBox myControl = (sender as WaterMarkTextBox);

            myControl.uxTBWaterMark.Text = e.NewValue.ToString();
        }


        /// <summary>
        /// Handles the GotFocus event of the uxTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void uxTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            isFocused = true;
            HandleWaterMark();
        }

        /// <summary>
        /// Handles the LostFocus event of the uxTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void uxTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            isFocused = false;
            HandleWaterMark();
        }

        /// <summary>
        /// Handles the TextChanged event of the uxTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Controls.TextChangedEventArgs"/> instance containing the event data.</param>
        private void uxTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            HandleWaterMark();
        }

        /// <summary>
        /// Handles the MouseLeftButtonDown event of the uxTBWaterMark control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Input.MouseButtonEventArgs"/> instance containing the event data.</param>
        private void uxTBWaterMark_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var myControl = (((sender as TextBlock).Parent as Canvas).Parent as Grid).Children[0];

            if (myControl != null)
            {
                if (myControl is TextBox)
                {
                    Dispatcher.BeginInvoke(() =>
                        {
                            (myControl as TextBox).Focus();
                        });
                }
            }
        }

        #endregion

        #region → Methods        .

        #region → Private        .

        /// <summary>
        /// Handles the water mark.
        /// </summary>
        private void HandleWaterMark()
        {
            string textValue = this.uxTextBox.Text;
            this.uxWaterMark.Visibility = (string.IsNullOrEmpty(textValue) && !isFocused) ? Visibility.Visible : Visibility.Collapsed;
        }

        /// <summary>
        /// Sets the binding to text property.
        /// </summary>
        private void SetBindingToTextProperty()
        {
            Binding b = new Binding("");
            b.ValidatesOnDataErrors = true;
            b.ValidatesOnExceptions = true;
            b.ValidatesOnNotifyDataErrors = true;
            b.NotifyOnValidationError = true;
            b.Source = this.DataContext;


            b.Path = new PropertyPath(this.BindProperty);
            b.Mode = BindingMode.TwoWay;
            this.uxTextBox.SetBinding(TextBox.TextProperty, b);

            //Handle water mark visibility
            HandleWaterMark();
        }
        #endregion

        

        #endregion
    }
}
