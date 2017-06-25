
#region → Usings   .
using System.Windows;
using Telerik.Windows.Controls;
#endregion

#region → History  .
/* Date         User            Change
 * 
 * 19.01.11     Yousra Reda        creation
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
    /// Negotiator Combo Control inherits from RadComboBox and add new property 
    /// for negotiator name to act as text to can accept null value 
    /// </summary>
    public class eNegComboControl : RadComboBox
    {
        #region → Properties    .

        /// <summary>
        /// Gets or sets the name of the negotiator.
        /// </summary>
        /// <value>The name of the negotiator.</value>
        public string NegotiatorName
        {
            get { return (string)GetValue(NegotiatorNameProperty); }
            set
            {
                SetValue(NegotiatorNameProperty, value);

            }
        }

        /// <summary>
        ///  Using a DependencyProperty as the backing store for NegotiatorName.  This enables animation, styling, binding, etc...
        /// </summary>
        public static readonly DependencyProperty NegotiatorNameProperty =
            DependencyProperty.Register("NegotiatorName", typeof(string), typeof(eNegComboControl), new PropertyMetadata(new PropertyChangedCallback(OnChanging)));

        #endregion

        #region → Constructor   .

        /// <summary>
        /// Initializes a new instance of the <see cref="eNegComboControl"/> class.
        /// </summary>
        public eNegComboControl()
        {

        }
        #endregion

        #region → Event Handlers.

        /// <summary>
        /// Called when [changing].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.Windows.DependencyPropertyChangedEventArgs"/> instance containing the event data.</param>
        private static void OnChanging(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue != null)
                (sender as eNegComboControl).Text = e.NewValue.ToString();
        }
        #endregion


        #region → Methods       .

        #region → Protected     .

        /// <summary>
        /// Called when [text changed].
        /// </summary>
        /// <param name="oldValue">The old value.</param>
        /// <param name="newValue">The new value.</param>
        protected override void OnTextChanged(string oldValue, string newValue)
        {
            base.OnTextChanged(oldValue, newValue);
            NegotiatorName = newValue;
        }

        #endregion

        #endregion
    }
}
