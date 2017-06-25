
namespace citPOINT.eNeg.Infrastructure.ClientSideTest.Web
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.ServiceModel.DomainServices.Hosting;
    using System.ServiceModel.DomainServices.Server;
    using citPOINT.eNeg.Infrastructure.Logging;
    using citPOINT.eNeg.Infrastructure.ExceptionHandling;

    // TODO: Create methods containing your application logic.
    [EnableClientAccess()]
    public class ServerSimulationService : DomainService
    {
        [Invoke]
        public void HandleAtServer()
        {
            try
            {
                LoggingProvider<ClientLoggingHandler>.Log("خطأ عند الخادم", Category.ServerData);
                throw new Exception("Server");
            }
            catch (Exception ex)
            {
              //  ExceptionHandlingProvider.Handle<ServerDataExceptionHandler, ServerLoggingHandler>(new ServerDataException(ex.Message, ex), false);
            }
        }
    }
}


