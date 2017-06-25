
#region → Usings   .

#endregion

#region → History  .
/* Date         User        Change
 * 
 * 20.07.10    m.Wahab     • creation
 * 
 */
#endregion

#region → ToDos    .

/*
 * Date         set by User     Description
 * 
 * 
*/

#endregion

namespace citPOINT.eNeg.Infrastructure.Logging
{

    #region Enums

    /// <summary>
    ///   Identifies the Priority of Actions that has caused the trace.
    /// </summary>
    public enum Priority
    {
        /// <summary>
        /// Priority is Highest
        /// </summary>
        Highest,
        /// <summary>
        /// Priority is High
        /// </summary>
        High,
        /// <summary>
        /// Priority is Normal
        /// </summary>
        Normal,
        /// <summary>
        /// Priority is Low
        /// </summary>
        Low,
        /// <summary>
        /// Priority is Lowest
        /// </summary>
        Lowest

    }


    /// <summary>
    ///   Identifies the Category of Project layer ex.Infrastructure Layer Is The cause oF error.
    /// </summary>
    public enum Category
    {
        /// <summary>
        /// Mean  Infrastructure In Server side Project
        /// </summary>
        Infrastructure_Server,
        /// <summary>
        /// Mean  Infrastructure In Common Project
        /// </summary>
        Infrastructure_Common,
        /// <summary>
        /// Mean  Infrastructure In Client <B>[Silverlight]</B> Project
        /// </summary>
        Infrastructure_Client,
        /// <summary>
        /// Mean  Client Data <B>[Silverlight]</B> Project
        /// </summary>
        ClientData,
        /// <summary>
        /// Mean  Client <B>[Silverlight]</B> Project the Acual <B>[Xap] </B> File.
        /// </summary>
        Client,
        /// <summary>
        /// For Common Project
        /// </summary>
        Common,
        /// <summary>
        /// Mean  Server Data Ria Project.
        /// </summary>
        ServerData,
        /// <summary>
        /// Mean Server Project Or Hosted Project.
        /// </summary>
        Web,
        /// <summary>
        /// Project Carry the Model of MVVM
        /// </summary>
        Model,
        /// <summary>
        /// Project Carry the ViewModel of MVVM
        /// </summary>
        ViewModel
   }

    /// <summary>
    ///    Identifies the type of event that has caused the trace.
    /// </summary>
    public enum SeverityType
    {


        /// <summary>
        ///    Fatal error or application crash.
        /// </summary>
        Fatal = 1,

        /// <summary>
        ///     Recoverable error.
        /// </summary>
        Error = 2,

        /// <summary>
        ///     Noncritical problem.
        /// </summary>
        Warning = 4,


        /// <summary>
        /// Informational message.
        /// </summary>
        Information = 8,

    }
    #endregion

}
