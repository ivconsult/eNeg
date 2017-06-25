#region → Usings   .
using System;
#endregion

#region → History  .

/* 
 * Date         User            Change
 * *********************************************
 * 18.03.12    mwahab         • creation
 * **********************************************
 */

# endregion

#region → ToDos    .

/*
 * Date         set by User     Description
 * 
 * 
*/

# endregion

namespace citPOINT.eNeg.Apps.Common.Enums
{
    /// <summary>
    /// Views Types
    /// </summary>
    [Flags]
    public enum ViewsTypes
    {
        /// <summary>
        /// ReportView.
        /// </summary>
        Report = 0x01,

        /// <summary>
        /// App Settings.
        /// </summary>
        AppSettings = 0x02
    }
}
