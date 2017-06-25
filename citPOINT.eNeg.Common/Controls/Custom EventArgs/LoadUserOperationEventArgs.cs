

#region → Usings   .
using System;
using System.ServiceModel.DomainServices.Client.ApplicationServices;


#endregion

#region → History  .

/* Date         User        Change
 * 
 * 05.08.10     M.Wahab     Creation
 * 26.09.10     M.Wahab     Updating XML Comments
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
    ///  Used for Loading User Operation EventArgs
    ///  So Return the Current User
    /// </summary>
    public class LoadUserOperationEventArgs : eNegBaseResultArgs
    {

        #region → Fields         .

        private LoadUserOperation mLoadUserOp;

        #endregion Fields
        
        #region → Properties     .

        /// <summary>
        /// Get Value of LoadUserOperation
        /// </summary>
        public LoadUserOperation LoginOp
        {
            get { return mLoadUserOp; }
        }

        #endregion Properties

        #region → Constructors   .

        /// <summary>
        /// Class Constructor
        /// </summary>
        /// <param name="ex">Value of Exception</param>
        public LoadUserOperationEventArgs(Exception ex)
            : base(ex)
        {
            mLoadUserOp = null;
        }

        /// <summary>
        /// Class Constructor
        /// </summary>
        /// <param name="loadUserOp">Value of LoadUserOperation </param>
        public LoadUserOperationEventArgs(LoadUserOperation loadUserOp)
            : base(null)
        {
            mLoadUserOp = loadUserOp;
        }

        /// <summary>
        /// Class Constructor
        /// </summary>
        /// <param name="loadUserOp">Value of LoadUserOperation </param>
        /// <param name="ex">Value of Exception</param>
        public LoadUserOperationEventArgs(LoadUserOperation loadUserOp, Exception ex)
            : base(ex)
        {
            mLoadUserOp = loadUserOp;
        }

        #endregion Constructors

    }
}
