#region → Usings   .
using citPOINT.eNeg.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Globalization;
using System.Windows;
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
    public class NullableToVisibilityConverterTest
    {
        #region → Fields         .
        NullableToVisibilityConverter _target;
        #endregion

        #region → Constructors   .

        [TestInitialize]
        public void Initialize()
        {
            _target = new NullableToVisibilityConverter();
        }
        #endregion

        #region → Methods        .

        #region → Public         .
        [TestMethod]
        public void TestNullValue()
        {
            object result = _target.Convert(null, null, null, CultureInfo.CurrentCulture);
            Assert.IsNotNull(result, "Converter returned null.");
            Assert.AreEqual(Visibility.Collapsed, result, "Converter returned invalid result.");
        }


        #endregion

        #endregion
    }
}
