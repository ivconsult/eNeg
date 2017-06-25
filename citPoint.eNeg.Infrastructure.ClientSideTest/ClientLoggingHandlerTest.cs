#region → Usings   .

using System;
using System.Collections.Generic;
using citPOINT.eNeg.Infrastructure.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;

#endregion

#region → History  .
/* Date         User        Change
 * 
 * 26.07.10    m.Wahab     • creation
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


namespace citPOINT.eNeg.Infrastructure.ClientSideTest
{


    /// <summary>
    ///This is a test class for ClientLoggingHandlerTest and is intended
    ///to contain all ClientLoggingHandlerTest Unit Tests
    ///</summary>
    [TestClass()]
    public class ClientLoggingHandlerTest
    {

        #region → Fields         .
        private TestContext testContextInstance;
        #endregion

        #region → Prperties      .

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #endregion
        
        #region → Methods        .
        /// <summary>
        /// A test for Log
        /// </summary>
        [TestMethod()]
        public void LogToClientTest()
        {
            //Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Writer.Write("Hello");

            #region Loading Variables

            #region Exception Intialization
            Exception ex = null;
            try
            {
                int x = 0;
                int y = 1 / x;
            }
            catch (Exception mex)
            {

                ex = mex;
            }
            #endregion

            //ClientLoggingHandler target = new ClientLoggingHandler(); // TODO: Initialize to an appropriate value
            string Msg = "Hello Logging Client Side ";

            Category category = Category.ServerData;
            Priority priority = Priority.Highest;
            SeverityType Type = SeverityType.Information;
            IDictionary<string, object> Properties = new Dictionary<string, object>();
            Properties.Add("TestKey", "test value");
            #endregion

            #region Acual Test


            LoggingProvider<ClientLoggingHandler>.Log(Msg, category);
            LoggingProvider<ClientLoggingHandler>.Log(Msg, category, priority);
            LoggingProvider<ClientLoggingHandler>.Log(Msg, category, priority, Type);
            LoggingProvider<ClientLoggingHandler>.Log(Msg, category, Properties);
            LoggingProvider<ClientLoggingHandler>.Log(Msg, category, priority, Type, Properties);

            #endregion

        }
        #endregion

    }
}
