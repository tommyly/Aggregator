using log4net.Config;
using Topshelf;

namespace Proxy
{
    class Program
    {
        static void Main(string[] args)
        {
            XmlConfigurator.Configure();

            HostFactory.Run(x =>                                 
            {
                x.Service<ErrorProxyService>(s =>                        
                {
                    s.ConstructUsing(name => new ErrorProxyService());     
                    s.WhenStarted(proxy => proxy.Start());             
                    s.WhenStopped(proxy => proxy.Stop());              
                });

                x.RunAsLocalSystem();                            

                x.SetDescription("Apps Monitor Error Proxy Service");        
                x.SetDisplayName("AppsMonitor.ErrorProxy");                       
                x.SetServiceName("AppsMonitor.ErrorProxy");                        
            });
        }
    }
}
