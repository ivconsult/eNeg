using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using citPOINT.eNeg.Infrastructure.Logging;

namespace TestPrj.Web
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {

            //LoggingProvider1<ServerLoggingHandler1>.Log(

            //ServerLoggingHandler1 x = new ServerLoggingHandler1();
            //x.Log("My Goolas", Category.ClientData);
            Exception ex=null;
            try
            {
                int x = 0;
                int y = 20/x;

            }
            catch (Exception m)
            {

                ex = m;
            }
           LoggingProvider<ServerLoggingHandler>.Log (ex.Message,  Category.Infrastructure_Client);
            

        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}