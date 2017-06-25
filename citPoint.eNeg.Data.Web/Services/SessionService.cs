
#region → Usings   .
using System.ServiceModel.DomainServices.Hosting;
using System.ServiceModel.DomainServices.Server;
#endregion

#region → History  .

/* Date         User              Change
 * 
 * 04.10.10     Yousra Reda       Creation
 */

# endregion

#region → ToDos    .

/*
 * Date         set by User     Description
 * 04.10.10      Yousra Reda       Creation
 * 
*/

# endregion
namespace citPOINT.eNeg.Data.Web
{
    /// <summary>
    /// Service that gets and sets a Session to or from server side
    /// </summary>
    [EnableClientAccess()]
    public class SessionService : DomainService
    {

        #region → Events         .

        #endregion

        #region → Methods        .

        #region → Public         .

        /// <summary>
        /// Get the session of a given key and returns the value casted to string
        /// </summary>
        /// <param name="sessionKey">Key of session needed</param>
        /// <returns>Value of session in string format</returns>
        public string GetSessionValue(string sessionKey)
        {
            
            return System.Web.HttpContext.Current.Session[sessionKey] != null ?
               System.Web.HttpContext.Current.Session[sessionKey].ToString() : string.Empty;
        }

        /// <summary>
        /// Sets the session of a given key with a given value
        /// </summary>
        /// <param name="sessionKey">Key of session</param>
        /// <param name="sessionValue">Value of Session</param>
        public void SetSessionValue(string sessionKey, string sessionValue)
        {
            System.Web.HttpContext.Current.Session[sessionKey] = sessionValue;
        }

        #endregion

        #endregion


    }
}
