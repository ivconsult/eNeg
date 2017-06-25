
#region → Usings   .
using System;
using System.ServiceModel.DomainServices.Client.ApplicationServices;


#endregion

#region → History  .

/* Date         User
 * 
 * 05.08.10     Mohamed Abdulwahab
 * 26.09.2010   M.Wahab             Updating XML Comments
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
    /// Used for Returning the login Operation EventArgs
    /// </summary>
    public class LoginOperationEventArgs : eNegBaseResultArgs
    {

        #region → Fields         .

        private LoginOperation mLoginOp;

        #endregion Fields

        #region → Properties     .

        /// <summary>
        /// Get Value of Login Operation
        /// </summary>
        public LoginOperation LoginOp
        {
            get { return mLoginOp; }
        }

        #endregion Properties

        #region → Constructors   .

        /// <summary>
        /// Class Constructor
        /// </summary>
        /// <param name="ex">Value of Exception</param>
        public LoginOperationEventArgs(Exception ex)
            : base(ex)
        {
            mLoginOp = null;
        }

        /// <summary>
        /// Class Constructor
        /// </summary>
        /// <param name="loginOp">Value of Login Operation</param>
        public LoginOperationEventArgs(LoginOperation loginOp)
            : base(null)
        {
            mLoginOp = loginOp;
        }

        /// <summary>
        /// Class Constructor
        /// </summary>
        /// <param name="loginOp">Value of Login Operation</param>
        /// <param name="ex">Value of Exception</param>
        public LoginOperationEventArgs(LoginOperation loginOp, Exception ex)
            : base(ex)
        {
            mLoginOp = loginOp;
        }
        #endregion Constructors


    }
}
