#region → Usings   .
using System;
using citPOINT.eNeg.Apps.Common.Interfaces;
#endregion

#region → History  .

/* Date         User            change
 * 
 * 18.03.12     M.Wahab     creation
 */

# endregion

#region → ToDos    .

/*
 * Date         set by User     Description
 * 
*/

# endregion

namespace citPOINT.eNeg.Common.eNegApps
{
    /// <summary>
    /// Main Handler Exception .
    /// </summary>
    public class MainHandleException : IMainHandleException
    {
        #region → Fields         .

        private static MainHandleException mInstance;

        #endregion

        #region → Properties     .

        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <value>The instance.</value>
        public static MainHandleException Instance
        {
            get
            {
                if (mInstance == null)
                {
                    mInstance = new MainHandleException();
                }

                return mInstance;
            }
        }

        #endregion

        #region → Methods        .

        /// <summary>
        /// Handles the exception.
        /// </summary>
        /// <param name="exception">The exception.</param>
        /// <param name="applicationName">Name of the application.</param>
        public void HandleException(Exception exception, string applicationName)
        {
            //HACK:Review In Case of Application Name send

            eNegMessanger.RaiseErrorMessage.ApplicationName = applicationName;

            eNegMessanger.RaiseErrorMessage.Send(exception);

            eNegMessanger.RaiseErrorMessage.ApplicationName =string.Empty;
        }

        #endregion
    }
}
