
#region → Usings   .

using System.Windows;
using System.Windows.Controls;


#endregion

#region → History  .

/* Date         User            change
 * 
 * 21.07.10     Yousra Reda     creation
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
    /// This is Custom Control named FlipControl that will be used in any dialog window
    /// </summary>
    /// 
    /// <summary>
    /// Custom control class with "Normal" and "Flipped" states
    /// </summary>
    [TemplateVisualState(Name = "Normal", GroupName = "CommonStates"),
    TemplateVisualState(Name = "Flipped", GroupName = "CommonStates")]
    public class FlipControl : Control
    {

        #region → Fields         .
        /// <summary>
        /// Field of type bool that indicate which side is visible, False --> FrontContent is visible
        /// </summary>
        private bool mIsFrontSideVisible;


        #region Dependency Properties

        /// <summary>
        /// It is a dependency property that can be binded in run time and also support animation and property changed notifications  
        /// </summary>
        public static readonly DependencyProperty IsFrontSideVisibleProperty =
          DependencyProperty.Register("IsFrontSideVisible", typeof(bool), typeof(FlipControl), new PropertyMetadata(false, IsFrontSideVisibleChanged));


        /// <summary>
        /// It is a dependency property that can be binded in run time and also support animation and property changed notifications  
        /// </summary>
        public static readonly DependencyProperty IsBackSideVisibleProperty =
          DependencyProperty.Register("IsBackSideVisible", typeof(bool), typeof(FlipControl), new PropertyMetadata(false, IsBackSideVisibleChanged));


        /// <summary>
        /// It is a dependency property that can be binded in run time and also support animation and property changed notifications  
        /// </summary>
        public static readonly DependencyProperty FrontContentProperty =
           DependencyProperty.Register("FrontContent", typeof(UIElement), typeof(FlipControl), new PropertyMetadata(null));

        /// <summary>
        /// It is a dependency property that can be binded in run time and also support animation and property changed notifications  
        /// </summary>
        public static readonly DependencyProperty BackContentProperty =
            DependencyProperty.Register("BackContent", typeof(UIElement), typeof(FlipControl), new PropertyMetadata(null));

        /// <summary>
        /// It is a dependency property that can be binded in run time and also support animation and property changed notifications  
        /// </summary>
        public static readonly DependencyProperty CornerRadiusProperty =
            DependencyProperty.Register("CornerRadius",
                                        typeof(CornerRadius),
                                        typeof(FlipControl), null);

        #endregion

        #endregion

        #region → Properties     .

        /// <summary>
        /// Property that indicates which side of the Flip Control is visible or not
        /// </summary>
        public bool IsFrontSideVisible
        {
            get { return (bool)GetValue(IsFrontSideVisibleProperty); }
            set { SetValue(IsFrontSideVisibleProperty, value); }
        }

        /// <summary>
        /// Property that give the availbility to bind the value of the appear side in runtime 
        /// </summary>
        public bool IsBackSideVisible
        {
            get { return (bool)GetValue(IsBackSideVisibleProperty); }
            set { SetValue(IsBackSideVisibleProperty, value); }
        }

        /// <summary>
        /// Property that give the availbility to put any content (controls) in the front content from xaml page 
        /// </summary>
        public UIElement FrontContent
        {
            get { return (UIElement)GetValue(FrontContentProperty); }
            set { SetValue(FrontContentProperty, value); }
        }

        /// <summary>
        /// Property that give the availbility to put any content (controls) in the front content from xaml page 
        /// </summary>
        public UIElement BackContent
        {
            get { return (UIElement)GetValue(BackContentProperty); }
            set { SetValue(BackContentProperty, value); }
        }

        /// <summary>
        /// property indicates the value of the rounded corner like that can be applied on border 
        /// </summary>
        public CornerRadius CornerRadius
        {
            get { return (CornerRadius)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }

        #endregion

        #region → Constructors   .

        /// <summary>
        /// Default Constructor with no argmunts
        /// </summary>
        public FlipControl()
        {
            this.DefaultStyleKey = typeof(FlipControl);
        }

        #endregion

        #region → Methods        .

        #region → Private        .

        /// <summary>
        /// Property Changed Callback event handler that handle any change happen in the behaviour of the dependency property 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void IsFrontSideVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            FlipControl flipControl = sender as FlipControl;
            if (flipControl != null)
            {
                flipControl.UpdateVisualState(true);
            }
        }

        /// <summary>
        /// Property Changed Callback event handler that handle any change happen in the behaviour of the dependency property 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void IsBackSideVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            FlipControl flipControl = sender as FlipControl;
            if (flipControl != null)
            {
                flipControl.UpdateVisualState(false);
            }
        }


        /// <summary>
        /// It is a function that is called inside Property changed callback event handler to change the visible side of control
        /// </summary>
        private void UpdateVisualState(bool useTransitions)
        {
            if (mIsFrontSideVisible)
                VisualStateManager.GoToState(this, "Flipped", useTransitions);
            else
                VisualStateManager.GoToState(this, "Normal", useTransitions);

            mIsFrontSideVisible = !mIsFrontSideVisible;
        }

        #endregion

        #region → Public         .
        /// <summary>
        /// Override for OnApplyTemplate Method
        /// </summary>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            UpdateVisualState(false);
        }
        #endregion

        #endregion

    }
}
