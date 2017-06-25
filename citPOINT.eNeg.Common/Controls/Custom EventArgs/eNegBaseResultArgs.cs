

#region → Usings   .

using System;

#endregion

#region → History  .

/* Date         User
 * 
 * 27.07.10     Mohamed Abdulwahab
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
    /// Used As the base ResultArgs
    /// </summary>
    public class eNegBaseResultArgs : EventArgs
    {

        #region → Fields         .
        private Exception mError;
        
        #endregion
        
        #region → Properties     .
        /// <summary>
        /// Exception Happen While The invocation of the any function
        /// </summary>
        /// 
        public Exception Error
        {
            get { return mError; }
        }

        /// <summary>
        /// return if the calling of any function have errors or not.
        /// </summary>
        public bool HasError
        {
            get { return this.mError != null; }
        }

        #endregion
        
        #region → Constructors   .

        /// <summary>
        /// Constructor of the class take the exception if thier is.
        /// </summary>
        /// <param name="ex">The acual exception</param>
        public eNegBaseResultArgs(Exception ex)
        {
            mError = ex;
        }

        #endregion

    }
}
