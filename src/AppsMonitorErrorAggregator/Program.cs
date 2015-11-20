using log4net.Config;
using Topshelf;

namespace Aggregator
{
    class Program
    {
        static void Main(string[] args)
        {
            XmlConfigurator.Configure();

            HostFactory.Run(x =>                                 
            {
                x.Service<ErrorAggregationService>(s =>                        
                {
                    s.ConstructUsing(name => new ErrorAggregationService());     
                    s.WhenStarted(aggregator => aggregator.Start());             
                    s.WhenStopped(aggregator => aggregator.Stop());              
                });

                x.RunAsLocalSystem();                            

                x.SetDescription("Apps Monitor Error Aggregation Service");        
                x.SetDisplayName("AppsMonitor.ErrorAggregator");                       
                x.SetServiceName("AppsMonitor.ErrorAggregator");                        
            });
        }
    }
}
