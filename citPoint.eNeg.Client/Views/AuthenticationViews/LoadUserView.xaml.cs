
#region → Usings   .
using System.ComponentModel.Composition;
using System.Windows.Controls;
using citPOINT.eNeg.Common;
using GalaSoft.MvvmLight;
#endregion

#region → History  .

/* Date         User            Change
 * 
 * 03.08.10     Yousra Reda     creation
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
    /// View used a s waiting indicator during authenticatio Process
    /// </summary>
    public partial class LoadUserView : UserControl
    {
        #region → Properties     .

        #region " Using MEF to import LoginFormViewModel "

        /// <summary>
        /// Sets the Data context of this view
        /// </summary>
        /// <value>The view model.</value>
        [Import(ViewModelTypes.LoginFormViewModel)]
        public object ViewModel
        {
            set
            {
                DataContext = value;
            }
        }
        #endregion

        #endregion
               
        #region → Constructors   .

        /// <summary>
        /// Initializes a new instance of the <see cref="LoadUserView"/> class.
        /// </summary>
        public LoadUserView()
        {
            InitializeComponent();

            #region " Use MEF To load the View Model "
            if (!ViewModelBase.IsInDesignModeStatic)
            {
                CompositionInitializer.SatisfyImports(this);
            }
            #endregion

        }
        #endregion
    }
}
