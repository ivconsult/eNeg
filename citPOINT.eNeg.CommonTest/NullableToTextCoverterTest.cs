#region → Usings   .
using citPOINT.eNeg.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Globalization;
#endregion

#region → History  .

/* Date         User            Change
 * 
 * 29.02.11     Yousra Reda     creation
 */

# endregion

#region → ToDos    .

/*
 * Date         set by User     Description
 * 
 * 
*/

# endregion
namespace citPOINT.eNeg.CommonTest
{
    /// <summary>
    /// Test Nullable To Text Coverter
    /// </summary>
    [TestClass]
    public class NullableToTextCoverterTest
    {
        #region → Fields         .
        NullableToTextCoverter _target;
        #endregion

        #region → Constructors   .

        [TestInitialize]
        public void Initialize()
        {
            _target = new NullableToTextCoverter();
        }
        #endregion

        #region → Methods        .

        #region → Public         .
        [TestMethod]
        public void TestNullValue()
        {
            object result = _target.Convert(null, null, null, CultureInfo.CurrentCulture);
            Assert.IsNotNull(result, "Converter returned null.");
            Assert.AreEqual(string.Empty, result, "Converter returned invalid result.");
        }


        #endregion

        #endregion
    }
}
