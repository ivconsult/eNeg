#region → Usings   .
using citPOINT.eNeg.Common;
using citPOINT.eNeg.ViewModel;
using GalaSoft.MvvmLight;
using System.ComponentModel.Composition;
using System.Windows.Controls;
#endregion

#region → History  .

/* Date         User              Change
 * 
 * 17.02.11     M.Wahab         → Creation
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
    /// LogOut Automation Test
    /// </summary>
    public partial class LogOutAutomationTest : Page
    {

        #region → Properties     .

        #region " Using MEF to import LoginFormViewModel "

        /// <summary>
        /// Sets the view model.
        /// </summary>
        /// <value>The view model.</value>
        [Import(ViewModelTypes.LoginFormViewModel)]
        public LogInViewModel ViewModel
        {
            get
            {
                return (DataContext as LogInViewModel);
            }
            set
            {
                DataContext = value;
            }
        }
        #endregion

        #endregion

        #region → Constructors   .

        /// <summary>
        /// Initializes a new instance of the <see cref="LogOutAutomationTest"/> class.
        /// </summary>
        public LogOutAutomationTest()
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
        
        #region → Methods        .

        #region → Public         .

        /// <summary>
        /// Clears the cash.
        /// </summary>
        public void ClearCash()
        {
            ViewModel.LogoutCommand.Execute(null);
        }

        #endregion

        #endregion

    }
}
