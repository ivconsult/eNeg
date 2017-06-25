
#region → Usings   .
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
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
    /// Custom control to act as a watermark PasswordBox 
    /// </summary>
    public partial class WaterMarkPasswordBox : UserControl
    {
        #region → Fields         .

        private bool isFocused = false;


        /// <summary>
        /// Using a DependencyProperty as the backing store for Text.  This enables animation, styling, binding, etc...
        /// </summary>
        public static readonly DependencyProperty BindPropertyProperty =
            DependencyProperty.Register("BindProperty", typeof(string), typeof(WaterMarkPasswordBox), null);

        /// <summary>
        /// Using a DependencyProperty as the backing store for WaterMark.  This enables animation, styling, binding, etc...
        /// </summary>
        public static readonly DependencyProperty WaterMarkProperty =
            DependencyProperty.Register("WaterMark", typeof(string), typeof(WaterMarkPasswordBox), new PropertyMetadata(new PropertyChangedCallback(OnWaterMarkChanging)));

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
        /// Initializes a new instance of the <see cref="WaterMarkPasswordBox"/> class.
        /// </summary>
        public WaterMarkPasswordBox()
        {
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(WaterMarkPasswordBox_Loaded);
        }
        #endregion

        #region → Event Handlers .

        /// <summary>
        /// Handles the Loaded event of the WaterMarkPasswordBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        void WaterMarkPasswordBox_Loaded(object sender, RoutedEventArgs e)
        {
            this.uxPasswordBox.Name = this.Name + "_Password";
            
            SetBindingToPwsProperty();
        }

        /// <summary>
        /// Handles the LostFocus event of the uxPasswordBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void uxPasswordBox_LostFocus(object sender, RoutedEventArgs e)
        {
            isFocused = false;
            HandleWaterMark();
        }

        /// <summary>
        /// Handles the GotFocus event of the uxPasswordBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void uxPasswordBox_GotFocus(object sender, RoutedEventArgs e)
        {
            isFocused = true;
            HandleWaterMark();
        }

        /// <summary>
        /// Handles the MouseLeftButtonDown event of the uxTBWaterMark control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Input.MouseButtonEventArgs"/> instance containing the event data.</param>
        private void uxTBWaterMark_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var myControl = (((sender as TextBlock).Parent as Canvas).Parent as Grid).Children[0];

            if (myControl != null)
            {
                if (myControl is PasswordBox)
                {
                    Dispatcher.BeginInvoke(() =>
                    {
                        (myControl as PasswordBox).Focus();
                    });
                }
            }
        }

        /// <summary>
        /// Called when [water mark changing].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.Windows.DependencyPropertyChangedEventArgs"/> instance containing the event data.</param>
        private static void OnWaterMarkChanging(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            WaterMarkPasswordBox myControl = (sender as WaterMarkPasswordBox);

            myControl.uxTBWaterMark.Text = e.NewValue.ToString();
        }

        /// <summary>
        /// Handles the PasswordChanged event of the uxPasswordBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void uxPasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            //Handle water mark visibility
            HandleWaterMark();
        }

        #endregion

        #region → Methods        .

        #region → Private        .


        /// <summary>
        /// Handles the water mark.
        /// </summary>
        private void HandleWaterMark()
        {
            string textValue = this.uxPasswordBox.Password;
            this.uxWaterMark.Visibility = (string.IsNullOrEmpty(textValue) && !isFocused) ? Visibility.Visible : Visibility.Collapsed;
        }

        /// <summary>
        /// Sets the binding to PWS property.
        /// </summary>
        private void SetBindingToPwsProperty()
        {
            Binding b = new Binding("");

             //ValidatesOnExceptions=True, NotifyOnValidationError=True

            //b.ValidatesOnDataErrors = true;
            b.ValidatesOnExceptions = true;
           // b.ValidatesOnNotifyDataErrors = true;
            b.NotifyOnValidationError = true;
            b.Source = this.DataContext;


            b.Path = new PropertyPath(this.BindProperty);
            b.Mode = BindingMode.TwoWay;
            this.uxPasswordBox.SetBinding(PasswordBox.PasswordProperty, b);

            //Handle water mark visibility
            HandleWaterMark();
        }

        #endregion

       

        #endregion

    }
}
