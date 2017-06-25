
#region → Usings   .
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using citPOINT.eNeg.ViewModel;
#endregion

#region → History  .
/* Date         User            Change
 * 
 * 28.04.11     Yousra Reda        creation
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
    /// Control that act as Outlook or Hotmail (To: or cc:) 
    /// area to can recieve mutilple receivers and validate them
    /// </summary>
    public class NegotiatorPanel : WrapPanel
    {

        #region → Fields        .

        // Using a DependencyProperty as the backing store for IsMailChannel.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsMailChannelProperty =
            DependencyProperty.Register("IsMailChannel", typeof(bool), typeof(NegotiatorPanel), null);

        /// <summary>
        /// Using a DependencyProperty as the backing store for MultiNegotiatorSource.  This enables animation, styling, binding, etc...
        /// </summary>
        public static readonly DependencyProperty MultiNegotiatorSourceProperty =
            DependencyProperty.Register("MultiNegotiatorSource", typeof(List<NegotiationViewModel.MultiNegotiator>), typeof(NegotiatorPanel), new PropertyMetadata(new PropertyChangedCallback(OnSourceChanged)));
        #endregion

        #region → Properties    .

        /// <summary>
        /// Gets or sets a value indicating whether this instance is mail channel.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is mail channel; otherwise, <c>false</c>.
        /// </value>
        public bool IsMailChannel
        {
            get { return (bool)GetValue(IsMailChannelProperty); }
            set { SetValue(IsMailChannelProperty, value); }
        }


        /// <summary>
        /// Gets or sets the multi negotiator source.
        /// </summary>
        /// <value>The multi negotiator source.</value>
        public List<NegotiationViewModel.MultiNegotiator> MultiNegotiatorSource
        {
            get { return (List<NegotiationViewModel.MultiNegotiator>)GetValue(MultiNegotiatorSourceProperty); }
            set { SetValue(MultiNegotiatorSourceProperty, value); }
        }

        #endregion

        #region → Event Handlers.

        /// <summary>
        /// Handles the TextChanged event of the NewReceiver control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        void NewReceiver_TextChanged(object sender, RoutedEventArgs e)
        {
            if ((sender as NegotiatorItem).IsValidMail)
            {
                (((sender as NegotiatorItem).Tag) as NegotiationViewModel.MultiNegotiator).NewNegotiator = (sender as NegotiatorItem).Text;
            }
        }
        #endregion

        #region → Methods       .

        #region → Private       .

        /// <summary>
        /// Called when [source changed].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.Windows.DependencyPropertyChangedEventArgs"/> instance containing the event data.</param>
        private static void OnSourceChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue != null)
            {
                (sender as NegotiatorPanel).MultiNegotiatorSource = (List<NegotiationViewModel.MultiNegotiator>)e.NewValue;
                (sender as NegotiatorPanel).BuildControls();
            }
        }

        /// <summary>
        /// Builds the controls.
        /// </summary>
        private void BuildControls()
        {
            this.Children.Clear();

            foreach (var item in this.MultiNegotiatorSource)
            {
                NegotiatorItem NewReceiver = new NegotiatorItem()
                {
                    AlwaysInEditing = false,
                    ItemsSource = item.OldNegotiators,
                    NegotiatorName = item.NewNegotiator,
                    IsMailChannel = this.IsMailChannel
                };
                NewReceiver.Margin = new Thickness(2);
                NewReceiver.TextChanged += new RoutedEventHandler(NewReceiver_TextChanged);
                NewReceiver.Tag = item;
                this.Children.Add(NewReceiver);
            }
        }

        #endregion

        #endregion

    }
}
