#region → Usings   .
using citPOINT.eNeg.Common;
using citPOINT.eNeg.ViewModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
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
    /// Negotiator Combo Control inherits from auto complete box and add new property 
    /// for negotiator name to act as text to can accept null value 
    /// </summary>
    public class NegotiatorItem : AutoCompleteBox
    {

        #region → Fields        .

        // Using a DependencyProperty as the backing store for IsMailChannel.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsMailChannelProperty =
            DependencyProperty.Register("IsMailChannel", typeof(bool), typeof(NegotiatorItem), null);

        /// <summary>
        /// Using a DependencyProperty as the backing store for .  This enables animation, styling, binding, etc...
        /// </summary>
        public static readonly DependencyProperty AlwaysInEditingProperty =
            DependencyProperty.Register("AlwaysInEditing", typeof(bool), typeof(NegotiatorItem), null);

        /// <summary>
        ///  Using a DependencyProperty as the backing store for NegotiatorName.  This enables animation, styling, binding, etc...
        /// </summary>
        public static readonly DependencyProperty NegotiatorNameProperty =
            DependencyProperty.Register("NegotiatorName", typeof(string), typeof(NegotiatorItem), new PropertyMetadata(new PropertyChangedCallback(OnChanging)));

        private bool mIsInEditingMode = false;

        private StringBuilder CellTemp;

        private string ReformatedEMail;

        #endregion

        #region → Properties    .

        /// <summary>
        /// Gets or sets a value indicating whether this instance is tab pressed.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is tab pressed; otherwise, <c>false</c>.
        /// </value>
        public static bool IsTabPressed { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is mail channel.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is mail channel; otherwise, <c>false</c>.
        /// </value>
        public bool IsMailChannel
        {
            get { return (bool)GetValue(IsMailChannelProperty); }
            set
            {
                IsMailChannelStatic = value;
                SetValue(IsMailChannelProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is mail channel static.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is mail channel static; otherwise, <c>false</c>.
        /// </value>
        public static bool IsMailChannelStatic { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is in editing mode static.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is in editing mode static; otherwise, <c>false</c>.
        /// </value>
        public static bool IsInEditingModeStatic { get; set; }

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
        /// Gets or sets a value indicating whether [always in editing].
        /// </summary>
        /// <value><c>true</c> if [always in editing]; otherwise, <c>false</c>.</value>
        public bool AlwaysInEditing
        {
            get { return (bool)GetValue(AlwaysInEditingProperty); }
            set { SetValue(AlwaysInEditingProperty, value); }
        }

        /// <summary>
        /// Gets a value indicating whether this instance is valid mail.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is valid mail; otherwise, <c>false</c>.
        /// </value>
        public bool IsValidMail
        {
            get
            {
                return ValidateMail();//&& !CheckOnMailExistance();
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is in editing mode.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is in editing mode; otherwise, <c>false</c>.
        /// </value>
        public bool IsInEditingMode
        {
            get
            {
                return mIsInEditingMode;

            }
            set
            {
                IsInEditingModeStatic = value;
                mIsInEditingMode = value;
            }
        }

        #endregion

        #region → Constructor   .

        /// <summary>
        /// Initializes a new instance of the <see cref="NegotiatorItem"/> class.
        /// </summary>
        public NegotiatorItem()
        {
        }

        #endregion

        #region → Event Handlers.

        /// <summary>
        /// Handles the MouseLeftButtonDown event of the NegotiatorItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Input.MouseButtonEventArgs"/> instance containing the event data.</param>
        void NegotiatorItem_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            string CurrentReceiverEmail = (((sender as Image).Parent as StackPanel).Children[0] as TextBlock).Text;

            eNegMessanger.FlippMessage.Send(ViewTypes.RemoveMultiReceivers + ":" + CurrentReceiverEmail);
        }

        /// <summary>
        /// Called when [changing].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.Windows.DependencyPropertyChangedEventArgs"/> instance containing the event data.</param>
        private static void OnChanging(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue != null)
            {
                (sender as NegotiatorItem).Text = e.NewValue.ToString();
                if ((sender as NegotiatorItem).IsValidMail)
                {
                    //if (IsMailChannelStatic &&
                    //    (!string.IsNullOrEmpty(e.NewValue.ToString()) ||
                    //    !string.IsNullOrWhiteSpace(e.NewValue.ToString())))
                    //if(!IsInEditingModeStatic)
                    if (!string.IsNullOrEmpty(e.NewValue.ToString()) ||
                        !string.IsNullOrWhiteSpace(e.NewValue.ToString()))
                    {
                        if (IsMailChannelStatic)
                            (sender as NegotiatorItem).SetReadOnlyState();
                        else if (IsTabPressed)
                            (sender as NegotiatorItem).SetReadOnlyState();
                    }
                }
                else
                {
                    Deployment.Current.Dispatcher.BeginInvoke(() =>
                    {
                        (sender as NegotiatorItem).Focus();
                    });
                }
            }
        }
        #endregion

        #region → Methods       .

        #region → Private       .

        /// <summary>
        /// Checks the on mail existance.
        /// </summary>
        /// <returns></returns>
        private bool CheckOnMailExistance()
        {
            bool RedundancyExist = false;
            if (DataContext != null && this.DataContext is NegotiationViewModel)
            {
                RedundancyExist = (this.DataContext as NegotiationViewModel).MultiNegotiatorSource.Where(s => s.NewNegotiator == this.Text).Count() > 1;
            }
            return RedundancyExist;
        }

        /// <summary>
        /// Validates the mail.
        /// </summary>
        /// <returns>Result of validation</returns>
        private bool ValidateMail()
        {
            string EmailPattern = @"^(?("")("".+?""@)|(([0-9a-zA-Z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-zA-Z])@))" +
                     @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,6}))$";

            string NegotiatorEmail = string.Empty;
            string NickName = string.Empty;

            NegotiatorName = this.Text;

            NegotiatorEmail = NegotiatorName;

            if (NegotiatorEmail != null)
            {
                if (!IsMailChannel)
                {
                    ReformatedEMail = NegotiatorEmail;
                    return true;
                }

                if (NegotiatorEmail.Contains("@"))
                {
                    #region Case: User entered valid format containing '-' e.g, ks-Karls.Sant@yahoo.com
                    //if (NegotiatorEmail.Contains("-"))
                    //{
                    //    NickName = NegotiatorEmail.Substring(0, NegotiatorEmail.IndexOf('-'));
                    //    NegotiatorEmail = NegotiatorEmail.Substring(NegotiatorEmail.IndexOf('-') + 1);
                    //    if (Regex.IsMatch(NegotiatorEmail, EmailPattern))
                    //    {
                    //        NegotiatorEmail = " <" + NegotiatorEmail;
                    //        NegotiatorEmail = string.Concat(NegotiatorEmail, ">");
                    //        ReformatedEMail = string.Concat(NickName, NegotiatorEmail);
                    //        return true;
                    //    }
                    //}
                    #endregion

                    #region Case: User entered valid Email format e.g, Karls.Sant@yahoo.com
                    if (Regex.IsMatch(NegotiatorEmail, EmailPattern))
                    {
                        //NegotiatorEmail = " <" + NegotiatorEmail;
                        ReformatedEMail = NegotiatorEmail;// string.Concat(NegotiatorEmail, ">");
                        return true;
                    }
                    #endregion
                }

                #region Case: User entered valid Email format but from DDL so it contain '<' & '>' Symbols e.g, Ks <Karls.Sant@yahoo.com>
                //if (NegotiatorEmail.Contains("<") && NegotiatorEmail.Contains(">"))
                //{
                //    string Temp = string.Empty;
                //    if (NegotiatorEmail.IndexOf('>') - NegotiatorEmail.IndexOf('<') > 0)
                //    {
                //        Temp = NegotiatorEmail.Substring(NegotiatorEmail.IndexOf('<') + 1, NegotiatorEmail.IndexOf('>') - NegotiatorEmail.IndexOf('<') - 1);
                //        if (Regex.IsMatch(Temp, EmailPattern))
                //        {
                //            ReformatedEMail = NegotiatorEmail;
                //            return true;
                //        }
                //    }
                //}
                #endregion

            }
            return false;
        }

        /// <summary>
        /// Sets the state of the read only.
        /// </summary>
        private void SetReadOnlyState()
        {
            if (!IsInEditingMode && !AlwaysInEditing)
            {
                if (CellTemp == null)
                {
                    FormatTemplate();
                }
                this.Template = (ControlTemplate)XamlReader.Load(CellTemp.ToString());

                if (this.Parent != null)
                {
                    eNegMessanger.FlippMessage.Send(ViewTypes.AddMultiReceivers);
                }
            }
        }

        /// <summary>
        /// Formats the template.
        /// </summary>
        private void FormatTemplate()
        {
            #region Build Template in Run time
            CellTemp = new StringBuilder();

            CellTemp.Append("<ControlTemplate ");
            CellTemp.Append("xmlns='http://schemas.microsoft.com/winfx/");
            CellTemp.Append("2006/xaml/presentation' ");
            CellTemp.Append("xmlns:x='http://schemas.microsoft.com/winfx/2006/xaml' ");

            //Be sure to replace "YourNamespace" and "YourAssembly" with your app's 
            //actual namespace and assembly here
            CellTemp.Append("xmlns:local='clr-namespace:citPOINT.eNeg.Client");
            CellTemp.Append(";assembly=citPOINT.eNeg.Client'>");

            CellTemp.Append("<Border BorderBrush='Black' BorderThickness='1' CornerRadius='3' Margin='3'>");
            CellTemp.Append("<StackPanel ");
            CellTemp.Append("Orientation='Horizontal' Background='OldLace'>");
            CellTemp.Append("<TextBlock ");
            CellTemp.Append("Text='" + this.NegotiatorName.Replace("<", "&lt;").Replace(">", "&gt;") + "' Width='Auto' />");

            CellTemp.Append("<Image Name='uxImageRemove' Source='/citPOINT.eNeg.Client;component/Images/Delete-icon.png' Width='8' Height='8' ");
            CellTemp.Append("Stretch='UniformToFill' />");
            CellTemp.Append("</StackPanel>");
            CellTemp.Append("</Border>");
            CellTemp.Append("</ControlTemplate>");
            #endregion
        }


        #endregion

        #region → Protected     .

        /// <summary>
        /// Called when [apply template].
        /// </summary>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            if (GetTemplateChild("uxImageRemove") != null)
                (GetTemplateChild("uxImageRemove") as Image).MouseLeftButtonDown += new MouseButtonEventHandler(NegotiatorItem_MouseLeftButtonDown);
        }

        /// <summary>
        /// Raises the <see cref="E:TextChanged"/> event.
        /// </summary>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        protected override void OnTextChanged(RoutedEventArgs e)
        {
            base.OnTextChanged(e);
            NegotiatorName = this.Text;
            IsInEditingMode = true;

            if (IsValidMail)
            {
                this.BorderBrush = new SolidColorBrush(Colors.Black);
                ToolTipService.SetToolTip(this, null);
            }
            else
            {
                this.BorderBrush = new SolidColorBrush(Colors.Red);
                ToolTip myToolTip = new ToolTip();
                myToolTip.Background = new SolidColorBrush(Colors.Red);
                myToolTip.Foreground = new SolidColorBrush(Colors.White);
                myToolTip.Content = eNeg.ViewModel.Resources.InvalidNegotiatorName;

                ToolTipService.SetToolTip(this, myToolTip);
            }
        }

        /// <summary>
        /// Raises the <see cref="E:LostFocus"/> event.
        /// </summary>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        protected override void OnLostFocus(RoutedEventArgs e)
        {
            base.OnLostFocus(e);

            if (!IsValidMail)
            {
                //this.BorderBrush = new SolidColorBrush(Colors.Red);
                //ToolTip myToolTip = new ToolTip();
                //myToolTip.Background = new SolidColorBrush(Colors.Red);
                //myToolTip.Foreground = new SolidColorBrush(Colors.White);
                //myToolTip.Content = eNeg.ViewModel.Resources.InvalidNegotiatorName;

                //ToolTipService.SetToolTip(this, myToolTip);

            }
            //else if (CheckOnMailExistance())
            //{
            //    this.BorderBrush = new SolidColorBrush(Colors.Red);
            //    ToolTip myToolTip = new ToolTip();
            //    myToolTip.Background = new SolidColorBrush(Colors.Red);
            //    myToolTip.Foreground = new SolidColorBrush(Colors.White);
            //    myToolTip.Content = eNeg.ViewModel.Resources.ReceiverExist;

            //    ToolTipService.SetToolTip(this, myToolTip);                
            //}
            else
            {
                if (this.IsDropDownOpen)
                {
                    return;
                }
                this.Text = this.NegotiatorName;
                this.BorderBrush = new SolidColorBrush(Colors.Black);
                ToolTipService.SetToolTip(this, null);

                this.NegotiatorName = ReformatedEMail;

                if (IsMailChannel || IsTabPressed)
                {
                    IsInEditingMode = false;

                    SetReadOnlyState();
                }
            }
        }

        /// <summary>
        /// Called before the <see cref="E:System.Windows.UIElement.KeyDown"/> event occurs.
        /// </summary>
        /// <param name="e">The data for the event.</param>
        protected override void OnKeyDown(System.Windows.Input.KeyEventArgs e)
        {
            base.OnKeyDown(e);

            if (e.Key == System.Windows.Input.Key.Space ||
                //e.Key == System.Windows.Input.Key.Tab ||
                e.Key == System.Windows.Input.Key.Enter ||
                e.PlatformKeyCode == 186 /*;*/ ||
                e.PlatformKeyCode == 188 /*,*/ )
            {
                this.NegotiatorName = ReformatedEMail;
                if (this.Tag != null)
                {
                    ((this.Tag) as NegotiationViewModel.MultiNegotiator).NewNegotiator = this.NegotiatorName;
                }
                if (!this.IsMailChannel)
                {
                    IsTabPressed = true;
                    //IsInEditingMode = false;
                    //SetReadOnlyState();
                    eNegMessanger.FlippMessage.Send(ViewTypes.AddMultiReceivers);
                }
                else if (this.IsValidMail)
                {
                    //IsInEditingMode = false;
                    //SetReadOnlyState();
                    eNegMessanger.FlippMessage.Send(ViewTypes.AddMultiReceivers);
                }
                e.Handled = true;
            }
            else
            {
                if (!IsMailChannel)
                {
                    IsTabPressed = false;
                }
            }
        }

        /// <summary>
        /// Raises the <see cref="E:SelectionChanged"/> event.
        /// </summary>
        /// <param name="e">The <see cref="System.Windows.Controls.SelectionChangedEventArgs"/> instance containing the event data.</param>
        protected override void OnSelectionChanged(System.Windows.Controls.SelectionChangedEventArgs e)
        {
            base.OnSelectionChanged(e);
            this.NegotiatorName = ReformatedEMail;

            if (this.Tag != null)
            {
                ((this.Tag) as NegotiationViewModel.MultiNegotiator).NewNegotiator = this.NegotiatorName;
            }

            if (this.IsValidMail)
            {

            }
            //this.Focus();
            //IsInEditingMode = true;
        }
        #endregion

        #endregion       
    }
}
