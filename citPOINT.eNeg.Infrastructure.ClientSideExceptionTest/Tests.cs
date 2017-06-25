using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Silverlight.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using citPOINT.eNeg.Infrastructure.ExceptionHandling;
using citPOINT.eNeg.Infrastructure.Logging;
using System.IO;

namespace citPOINT.eNeg.Infrastructure.ClientSideTest
{
    [TestClass]
    public class ClientExceptionHandling
    {

        /// <summary>
        ///A test for exception handling
        ///</summary>              
        [TestMethod()]
        public void ClientExceptionTest()
        {
            LoggingProvider<ClientLoggingHandler>.Log("Test", Category.Common);

            IOException e = new IOException("File Not Exist");


            try
            {
                ClientExceptionHandlerProvider.RepaireExceptionPolicies();

                ExceptionHandlingResult exceptionHandlingResult = ExceptionManager.Instance.HandleException(e, "Policy1");

                Assert.AreEqual(exceptionHandlingResult.Message, "The process cannot access the file because it is being used by another process.");

            }
            catch (Exception ex)
            {

            }


        }


    }
}