#region → Usings   .
using System;
using System.ServiceModel.DomainServices.Client.ApplicationServices;
#endregion

#region → History  .

/* Date         User
 * 
 * 05.08.10     Mohamed Abdulwahab   Creation
 * 26.09.10     M.Wahab              Updating Xml Comments
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
    /// Used for Returning the logOut Operation EventArgs
    /// </summary>
    public class LogoutOperationEventArgs : eNegBaseResultArgs
    {
         
        #region → Fields         .

        private LogoutOperation mLogoutOp;

        #endregion Fields
        
        #region → Properties     .

        /// <summary>
        /// Get Logout Operation Values
        /// </summary>
        public LogoutOperation LogoutOp
        {
            get { return mLogoutOp; }
        }

        #endregion
        
        #region → Constructors   .
     
        /// <summary>
        /// the constructor of LogoutOperationEventArgs
        /// </summary>
        /// <param name="ex">Value Of Exception</param>
        public LogoutOperationEventArgs(Exception ex)
            : base(ex)
        {
            mLogoutOp = null;
        }

        /// <summary>
        /// the constructor of LogoutOperationEventArgs
        /// </summary>
        /// <param name="logoutOp">Value Of LogoutOperation</param>
        public LogoutOperationEventArgs(LogoutOperation logoutOp)
            : base(null)
        {
            mLogoutOp = logoutOp;
        }

        /// <summary>
        /// the constructor of LogoutOperationEventArgs
        /// </summary>
        /// <param name="logoutOp">Value Of LogoutOperation</param>
        /// <param name="ex">Value Of Exception</param>
        public LogoutOperationEventArgs(LogoutOperation logoutOp, Exception ex)
            : base(ex)
        {
            mLogoutOp = logoutOp;
        }

        #endregion Constructors

    }
}
