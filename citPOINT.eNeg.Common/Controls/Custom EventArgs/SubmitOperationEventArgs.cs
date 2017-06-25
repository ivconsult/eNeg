
#region → Usings   .

using System;
using System.ServiceModel.DomainServices.Client;

#endregion

#region → History  .

/* Date         User
 * 
 * 28.07.10     Mohamed Abdulwahab  Creation
 * 26.09.10     M.Wahab             Updating XML Comments
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
    /// Class used as EventArgs for Save changes function
    /// </summary>
    public class SubmitOperationEventArgs : eNegBaseResultArgs
    {
        
        #region → Fields         .

        private SubmitOperation mSubmitOp;

        #endregion Fields
        
        #region → Properties     .

        /// <summary>
        /// Get Value of Submit Operation
        /// </summary>
        public SubmitOperation SubmitOp
        {
            get { return mSubmitOp; }
        }

        #endregion Properties
        
        #region → Constructors   .

        /// <summary>
        /// the constructor of SubmitOperationEventArgs
        /// </summary>
        /// <param name="ex">Value Of Exception</param>
        public SubmitOperationEventArgs(Exception ex)
            : base(ex)
        {
            mSubmitOp = null;
        }

        /// <summary>
        /// the constructor of SubmitOperationEventArgs
        /// </summary>
        /// <param name="submitOp">Value Of Submit Operation</param>
        public SubmitOperationEventArgs(SubmitOperation submitOp)
            : base(null)
        {
            mSubmitOp = submitOp;
        }

        /// <summary>
        /// the constructor of SubmitOperationEventArgs
        /// </summary>
        /// <param name="submitOp">Value Of Submit Operation</param>
        /// <param name="ex">Value Of Exception</param>
        public SubmitOperationEventArgs(SubmitOperation submitOp, Exception ex)
            : base(ex)
        {
            mSubmitOp = submitOp;
        }

        #endregion Constructors
        
    }
}