
#region → Usings   .



#endregion

#region → History  .

/* Date         User
 * 
 * 28.07.10     Mohamed Abdulwahab
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
    /// Interface as Model Base for all Models
    /// </summary>
    public interface IModelBase
    {
        #region → Methods        .

        /// <summary>
        /// Determines if the user is authorized or not for a right
        /// </summary>
        /// <param name="RightName">right name</param>
        /// <returns>is One is authorized to do functionality or not(Bool)</returns>
        bool IsAuthorized(RightsName RightName);

        #endregion

    }
}
