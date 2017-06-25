#region → Usings   .

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


namespace citPOINT.eNeg.Infrastructure.ServerSideTest
{


    /// <summary>
    ///This is a test class for ServerLoggingHandlerTest and is intended
    ///to contain all ServerLoggingHandlerTest Unit Tests
    ///</summary>
    [TestClass()]
    public class ServerLoggingHandlerTest
    {


        #region → Fileds         .

        private TestContext mtestContextInstance;

        #endregion Fileds

        #region → Properties     .

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return mtestContextInstance;
            }
            set
            {
                mtestContextInstance = value;
            }
        }

        #endregion Properties

        #region → Constructors   .

        /// <summary>
        /// Default  constructor
        /// </summary>
        public ServerLoggingHandlerTest()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        #endregion Constructor

        #region → Methods        .


        #region → Public         .

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion


        /// <summary>
        ///A test for Log
        ///</summary>
        [TestMethod()]
                public void LogToServerTest()
        {
            Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Writer.Write("Hello");

            #region Loading Variables

            //ServerLoggingHandler target = new ServerLoggingHandler(); // TODO: Initialize to an appropriate value
            string Msg = "Hello Logging Server Side ";

            Category category = Category.ServerData;
            Priority priority = Priority.Highest;
            SeverityType Type = SeverityType.Information;
            IDictionary<string, object> Properties = new Dictionary<string, object>();
            Properties.Add("TestKey", "test value");
            #endregion

            #region Acual Test


            LoggingProvider<ServerLoggingHandler>.Log(Msg, category);
            LoggingProvider<ServerLoggingHandler>.Log(Msg, category, priority);
            LoggingProvider<ServerLoggingHandler>.Log(Msg, category, priority, Type);
            LoggingProvider<ServerLoggingHandler>.Log(Msg, category, Properties);
            LoggingProvider<ServerLoggingHandler>.Log(Msg, category, priority, Type, Properties);

            #endregion

        }

        #endregion Public


        #endregion Methods


    }
}
