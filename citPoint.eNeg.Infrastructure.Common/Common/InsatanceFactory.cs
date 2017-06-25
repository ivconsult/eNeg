#region → Usings   .
using System;
#endregion

#region → History  .
/*
    Date        User        Change
 *  27.07.2010  Dergham     • Creation
 */

#endregion

#region → ToDos    .
#endregion
namespace citPOINT.eNeg.Infrastructure
{
    /// <summary>
    /// Class to instantial new object for any class just by its type T.
    /// </summary>
    public static class InsatanceFactory
    {
        #region → Methods        .

        /// <summary>
        /// Create new Insatnce of Type T
        /// </summary>
        /// <typeparam name="T">Type of the instance</typeparam>
        /// <returns>instance of Type T</returns>
        public static T CreateInstant<T>()
        {
            var handler = Activator.CreateInstance<T>();
            return handler;
        }

        #endregion Methods
    }
}
