﻿#region → Usings   .
using DanielVaughan.Logging;
using System.Collections.Generic;
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
    /// <summary>
    /// Used To Log On Server
    /// </summary>
    public class ClientLoggingHandler : ILoggingHandler
    {
        ILog logs;


        #region ILoggingHandler Members
        /// <summary>
        /// Used  to Log any data or exceptions that occured during running the program.
        /// </summary>
        /// <param name="Msg">the user friendly massage or any informations</param>
        /// <param name="category">Mean the project who have the problem or who want to write a message</param>
        /// <param name="priority">mean the important of the logging ex.High or Low</param>
        /// <param name="Type">the Type of the message ex.Error or info.</param>
        /// <param name="Properties">any addition informations</param>
        public void Log(string Msg, Category category, Priority priority, SeverityType Type, IDictionary<string, object> Properties)
        {
            #region Filling Extra properties

            if (Properties == null)
            {
                Properties = new Dictionary<string, object>();
            }

            if (!Properties.ContainsKey("Priority"))
                Properties.Add("Priority", priority.ToString());
            else
                Properties["Priority"] = priority.ToString();

            //-----------------------------------------------------
            if (!Properties.ContainsKey("Category"))
                Properties.Add("Category", category.ToString());
            else
                Properties["Category"] = category.ToString();

            //-----------------------------------------------------
            if (!Properties.ContainsKey("LogLayer"))
                Properties.Add("LogLayer", Type.ToString());
            else
                Properties["LogLayer"] = Type.ToString();

            #endregion

            #region Log Now
           logs = LogManager.GetLog("Page");

            switch (Type)
            {
                case SeverityType.Error:
                    {
                        logs.Error(Msg, null, Properties);
                        break;
                    }
                case SeverityType.Fatal:
                    {
                        logs.Fatal(Msg, null, Properties);
                        break;
                    }

                case SeverityType.Information:
                    {
                        logs.Info(Msg, null, Properties);
                        break;
                    }

                case SeverityType.Warning:
                    {
                        logs.Warn(Msg, null, Properties);
                        break;
                    }

                default:
                    {
                        logs.Info(Msg, null, Properties);
                        break;
                    }
            }

            #endregion

        }


        #endregion
    }
}

