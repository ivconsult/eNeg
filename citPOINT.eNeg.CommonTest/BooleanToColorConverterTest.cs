#region → Usings   .
using citPOINT.eNeg.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Globalization;
using System.Windows;
using System.Windows.Media;
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
    /// Test Boolean to Color Converter 
    /// </summary>
    [TestClass]
    public class BooleanToColorConverterTest
    {

        #region → Fields         .
        BooleanToColorConverter _target;
        #endregion

        #region → Constructors   .

        [TestInitialize]
        public void Initialize()
        {
            _target = new BooleanToColorConverter();
        }
        #endregion

        #region → Methods        .

        #region → Public         .
        [TestMethod]
        public void TestTrue()
        {
            object result = _target.Convert(true, typeof(bool), null, CultureInfo.CurrentCulture);
            SolidColorBrush ExpectedResult = new SolidColorBrush(Colors.LightGray);
            Assert.IsNotNull(result, "Converter returned null.");
            Assert.AreEqual(result, result, "Converter returned invalid result.");
        }

        [TestMethod]
        public void TestFalse()
        {
            object result = _target.Convert(false, typeof(bool), null, CultureInfo.CurrentCulture);
            SolidColorBrush ExpectedResult = new SolidColorBrush(Colors.White);
            Assert.IsNotNull(result, "Converter returned null.");
            Assert.AreEqual(result, result, "Converter returned invalid result.");
        }

        #endregion

        #endregion
    }
}
