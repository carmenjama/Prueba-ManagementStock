using ManagementTrans.Api.Core.Contexts;

namespace ManagementTrans.Api
{
    internal static class Service
    {
        private static IWebHost api;

        private static void Main(string[] args)
        {
            int minWorker = ApplicationContext.ThreadsSettings.Workers;
            int minIOC = ApplicationContext.ThreadsSettings.CompletionPorts;
            if (ThreadPool.SetMinThreads(minWorker, minIOC))
            {
                ThreadPool.GetMinThreads(out minWorker, out minIOC);
                Console.WriteLine($"OK, min thread workers set to {minWorker} worker threads and {minIOC} completion port threads!");
            }
            else
            {
                ThreadPool.GetMinThreads(out minWorker, out minIOC);
                Console.WriteLine("Warning, the minimum number of threads was not changed!!!");
                Console.WriteLine($"The threads are kept by default, min thread workers set to {minWorker} worker threads and {minIOC} completion port threads!");
            }

            Console.WriteLine($"{ApplicationContext.ApplicationName}..!");

            Init();
            Start();
        }

        private static void Init()
        {
            api = new WebHostBuilder()
              .UseKestrel()
              .ConfigureLogging((hostContext, loggingBuilder) =>
              {
                  loggingBuilder.ClearProviders();
                  loggingBuilder.AddConsole(options => options.IncludeScopes = true);
                  if (hostContext.HostingEnvironment.IsDevelopment())
                      loggingBuilder.SetMinimumLevel(LogLevel.Debug);
                  else
                      loggingBuilder.SetMinimumLevel(LogLevel.Error);
              })
              .UseStartup<Startup>()
              .Build();
        }

        private static void Start()
        {
            Console.WriteLine($"Starting Service {ApplicationContext.ApplicationName}..!");
            api.RunAsync().ContinueWith((state) => Stop()).Wait();
        }

        private static void Stop()
        {
            Console.WriteLine($"Stopped Service {ApplicationContext.ApplicationName}..!");
        }
    }
}
