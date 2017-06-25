#region → Usings   .
using System.Collections.Generic;

#endregion

#region → History  .

/* Date         User        Change
 * 
 * 28.07.10     M.Wahab     Creation
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
    /// the Base class for all models
    /// </summary>
    public abstract class ModelBase : IModelBase
    {
        #region → Fields         .

        private static IList<string> mrights;

        #endregion

        #region → Properties     .

        /// <summary>
        /// The All Rights of The Current User
        /// </summary>
        public static IList<string> Rights
        {
            get { return mrights; }
            set { mrights = value; }
        }

        #endregion Properties
        
        #region → Methods        .

        
        /// <summary>
        /// Mean This Person Is Authorized to Do Somting or Not Authorized
        /// </summary>
        /// <param name="RightName">The Name OF The Rights ex.Messages_Add</param>
        /// <returns>true if user is authorized</returns>
        public bool IsAuthorized(RightsName RightName)
        {

            if (Rights == null)
                return false;
            //If The rights Is Exists
            if (Rights.Contains(RightName.ToString()))
                return true;

            return false;
        }
         

        #endregion Methods


    }
}
