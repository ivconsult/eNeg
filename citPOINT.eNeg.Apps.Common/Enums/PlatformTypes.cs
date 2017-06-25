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
    /// Platform Types
    /// </summary>
    [Flags]
    public enum PlatformTypes
    {
        /// <summary>
        /// Main Platform.
        /// </summary>
        MainPlatform = 0x01,

        /// <summary>
        /// Add On.
        /// </summary>
        AddOn = 0x02
    }
}
