#region → Usings   .

using System.Windows.Controls;
using System.Windows;


#endregion

#region → History  .

/* Date         User              Change
 * 
 * 21.07.10     Yousra Reda        creation
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
    /// Custom control class with "LoggedIn" and "LoggedOut" states
    /// </summary>
    [TemplateVisualState(Name = "LoggedIn", GroupName = "CommonStates"),
    TemplateVisualState(Name = "LoggedOut", GroupName = "CommonStates")]
    public class MainPageControl : Control
    {
        #region → Fields         .

        #region Dependency Properties


        /// <summary>
        /// Using a DependencyProperty as the backing store for TitleContent.  This enables animation, styling, binding, etc...
        /// </summary>
        public static readonly DependencyProperty TitleContentProperty =
            DependencyProperty.Register("TitleContent", typeof(object), typeof(MainPageControl), null);

        /// <summary>
        /// Using a DependencyProperty as the backing store for LoggedInMenuContent.  This enables animation, styling, binding, etc...
        /// </summary>
        public static readonly DependencyProperty LoggedInMenuContentProperty =
            DependencyProperty.Register("LoggedInMenuContent", typeof(object), typeof(MainPageControl), null);


        /// <summary>
        /// Using a DependencyProperty as the backing store for LoggedOutMenuContent.  This enables animation, styling, binding, etc...
        /// </summary>
        public static readonly DependencyProperty LoggedOutMenuContentProperty =
            DependencyProperty.Register("LoggedOutMenuContent", typeof(object), typeof(MainPageControl), null);


        /// <summary>
        /// Using a DependencyProperty as the backing store for LoginPageContent.  This enables animation, styling, binding, etc...    
        /// </summary>
        public static readonly DependencyProperty LoginPageContentProperty =
            DependencyProperty.Register("LoginPageContent", typeof(object), typeof(MainPageControl), null);

        /// <summary>
        /// Using a DependencyProperty as the backing store for MainPageContent.  This enables animation, styling, binding, etc...
        /// </summary>
        public static readonly DependencyProperty MainPageContentProperty =
            DependencyProperty.Register("MainPageContent", typeof(object), typeof(MainPageControl), null);


        /// <summary>
        /// Using a DependencyProperty as the backing store for IsLoggedIn.  This enables animation, styling, binding, etc...
        /// </summary>
        public static readonly DependencyProperty IsLoggedInProperty =
            DependencyProperty.Register("IsLoggedIn", typeof(bool), typeof(MainPageControl),
            new PropertyMetadata(false, new PropertyChangedCallback(MainPageControl.OnIsLoggedInChanged)));

        /// <summary>
        /// Dependency property the can be binded in run time as an example
        /// </summary>
        public static readonly DependencyProperty CornerRadiusProperty =
            DependencyProperty.Register("CornerRadius",
                                        typeof(CornerRadius),
                                        typeof(MainPageControl), null);
        #endregion

        #endregion
        
        #region → Properties     .

        /// <summary>
        /// Represent the header (Title) part of the Main Page, It will appear whatever the state is LoggedIn or LoggedOut
        /// </summary>
        public object TitleContent
        {
            get { return GetValue(TitleContentProperty); }
            set { SetValue(TitleContentProperty, value); }
        }

        /// <summary>
        /// Represent the naviagtion part that will appear when the state is loggedIn
        /// </summary>
        public object LoggedInMenuContent
        {
            get { return GetValue(LoggedInMenuContentProperty); }
            set { SetValue(LoggedInMenuContentProperty, value); }
        }

        /// <summary>
        /// Represent the naviagtion part that will appear when the state is loggedOut
        /// </summary>
        public object LoggedOutMenuContent
        {
            get { return GetValue(LoggedOutMenuContentProperty); }
            set { SetValue(LoggedOutMenuContentProperty, value); }
        }
        /// <summary>
        /// Represent the Controls that will appear while state is loggedOut
        /// </summary>
        public object LoginPageContent
        {
            get { return GetValue(LoginPageContentProperty); }
            set { SetValue(LoginPageContentProperty, value); }
        }

        /// <summary>
        /// Represent the Controls that will appear while state is loggedIn, This part Change according to which screen loaded 
        /// </summary>
        public object MainPageContent
        {
            get { return GetValue(MainPageContentProperty); }
            set { SetValue(MainPageContentProperty, value); }
        }

        /// <summary>
        /// bool Property Indicate whether the state is LoggedIn or LoggedOut, state (LoggedIn) --> IsLoggedIn = true
        /// </summary>
        public bool IsLoggedIn
        {
            get { return (bool)GetValue(IsLoggedInProperty); }
            set { SetValue(IsLoggedInProperty, value); }
        }

        /// <summary>
        /// Property that descripes the characteristics of the rounded corner, such as that ca be appied to the border control
        /// </summary>
        public CornerRadius CornerRadius
        {
            get { return (CornerRadius)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }

        #endregion

        #region → Constructors   .

        /// <summary>
        /// Default Constructor with no arguments
        /// </summary>
        public MainPageControl()
        {
            DefaultStyleKey = typeof(MainPageControl);
        }

        #endregion
        
        #region → Methods        .

        #region → Private        .

        /// <summary>
        /// Property Changed Callback event handler that handle any change happen in the behaviour of the dependency property 
        /// </summary>
        /// <param name="sender">Value Of sender</param>
        /// <param name="e">Value Of e</param>
        private static void OnIsLoggedInChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            MainPageControl mainPageControl = sender as MainPageControl;

            if (mainPageControl != null)
            {
                mainPageControl.UpdateVisualState();
            }
        }

        /// <summary>
        /// It is a function that is called inside Property changed callback event handler to change the visible side of control
        /// </summary>
        private void UpdateVisualState()
        {
            if (this.IsLoggedIn)
            {
                VisualStateManager.GoToState(this, "LoggedIn", true);
            }
            else
            {
                VisualStateManager.GoToState(this, "LoggedOut", true);
            }
        }

        #endregion

        #region → Protected      .


        #endregion

        #region → Public         .

        /// <summary>
        /// Override OnApplyTemplate function to that control 
        /// </summary>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            UpdateVisualState();
        }

        #endregion


        #endregion


    }
}
