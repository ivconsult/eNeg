
#region → Usings   .
using citPOINT.eNeg.Data.Web;
using Microsoft.VisualStudio.TestTools.UnitTesting;

#endregion

#region → History  .

/* Date         User              Change
 * 
 * 19.09.10     Yousra Reda       Creation
 */

# endregion

#region → ToDos    .

/*
 * Date         set by User     Description
 * 
 * 
*/
# endregion
namespace citPOINT.eNeg.Data.WebTest
{
    interface ILoginServiceTest
    {

        #region → Properties     .
        LogInContext LoginContext { get; }
        TestContext TestContext { get; set; }
        LoginUser UserObj();
        #endregion Properties


        #region → Methods        .
        void GetUser();
        void Logout();
        void Login();
        #endregion Methods
    }
}
