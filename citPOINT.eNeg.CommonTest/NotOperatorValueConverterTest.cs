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
    /// Test Not Operator Value Converter  
    /// if (True) → (false) && 
    /// if (false) → (True) and vice versa
    /// </summary>
    [TestClass]
    public class NotOperatorValueConverterTest
    {
        #region → Fields         .
        NotOperatorValueConverter _target;
        #endregion

        #region → Constructors   .

        [TestInitialize]
        public void Initialize()
        {
            _target = new NotOperatorValueConverter();
        }
        #endregion

        #region → Methods        .

        #region → Public         .
        [TestMethod]
        public void TestTrue()
        {
            object result = _target.Convert(true, typeof(bool), null, CultureInfo.CurrentCulture);
            Assert.IsNotNull(result, "Converter returned null.");
            Assert.AreEqual(false, result, "Converter returned invalid result.");
        }

        [TestMethod]
        public void TestFalse()
        {
            object result = _target.Convert(false, typeof(bool), null, CultureInfo.CurrentCulture);
            Assert.IsNotNull(result, "Converter returned null.");
            Assert.AreEqual(true, result, "Converter returned invalid result.");
        }

        #endregion

        #endregion
    }
}
