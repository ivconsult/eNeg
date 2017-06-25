
#region → Usings   .
using System;
using System.ComponentModel;
using System.Linq;
using System.Collections.Generic;
using citPOINT.eNeg.Data.Web;

#endregion

#region → History  .

/* 
 * Date         User            Change
 * *********************************************
 * 8/14/2011 1:07:39 PM      mwahab         • creation
 * **********************************************
 */

# endregion

#region → ToDos    .

/*
 * Date         set by User     Description
 * 
 * 
*/

# endregion

namespace citPOINT.eNeg.Common.Helper
{

    /// <summary>
    /// Class for Repository 
    /// </summary>
    public static class Repository
    {

        #region → Fields         .

        private static eNegContext mNegContext;

        #endregion

        #region → Properties     .

        /// <summary>
        /// Protocted property with a getter only to used to update
        /// user login and logout data in eNeg Database
        /// </summary>
        /// <value>The context.</value>
        public static eNegContext Context
        {
            get
            {
                if (mNegContext == null)
                {
                    mNegContext = new eNegContext();
                }

                return mNegContext;
            }
            set
            {
                mNegContext = value;

            }
        }

        #endregion Properties

    }
}
