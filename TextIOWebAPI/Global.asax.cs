using EllieMae.Encompass.Client;
using System.Web.Http;

namespace EncompWebAPI
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            ApplicationLog.DebugEnabled = true;

            new EllieMae.Encompass.Runtime.RuntimeServices().Initialize();

            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }
}
