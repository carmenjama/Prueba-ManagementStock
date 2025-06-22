using ManagementTrans.Api.Core.Settings;
using System.Diagnostics;
using System.Net.Sockets;
using System.Net;
using ManagementProducts.SharedDto.DbContext;

namespace ManagementTrans.Api.Core.Contexts
{
    public class ApplicationContext
    {
        internal const string FOLDER_NAME = @"Config";
        private static string ConfigurationFileName { get; set; } = string.Empty;
        public static string ApplicationCode => AppSettings.GetValue<string>("Application:Code");
        public static string ApplicationName => AppSettings.GetValue<string>("Application:Name");
        public static string AssemblyVersion => typeof(ApplicationContext).Assembly.GetName().Version.ToString();
        public static string AssemblyName => typeof(ApplicationContext).Assembly.GetName().Name;
        internal const string AUTHORIZATION_HEADER = "Authorization";
        internal const string BEARER_AUTHENTICATION = "Bearer";
        internal static string ConfigurationPath { get => Path.Combine(AppContext.BaseDirectory, FOLDER_NAME); }
        
        public static string Version { get; set; } = AppSettings?.GetSection("ApiSettings:Application:Version")?.Get<string>() ?? string.Empty;
        public static bool IsProduction => AppSettings.GetValue<bool>("Application:IsProduction");
        public static ThreadsSettings ThreadsSettings => AppSettings.GetSection("ApiSettings:ThreadsSettings").Get<ThreadsSettings>();
        public static SqlServerDbContext SqlServerDbContext => AppSettings.GetSection("SqlServerContext").Get<SqlServerDbContext>();
        public static JwtTokenContext JwtToken => AppSettings.GetSection("JwtToken").Get<JwtTokenContext>();
        public static IConfigurationSection IpRateLimitOptions => AppSettings.GetSection(":AspNetCoreRateLimit:IpRateLimiting");

        private static IConfiguration AppSettings
        {
            get
            {
                var env = new ConfigurationBuilder()
                  .AddEnvironmentVariables()
                  .Build()
                  .GetSection("PLATFORM").Value;

                ConfigurationFileName = string.IsNullOrEmpty(env)
                  ? $"appsetting.json"
                  : $"appsetting.{env}.json";

                ApplyDevelopmentEnvironment();

                return new ConfigurationBuilder()
                  .SetBasePath(ConfigurationPath)
                  .AddJsonFile(ConfigurationFileName)
                  .Build();
            }
        }

        public static string LocalIPAddress
        {
            get
            {
                try
                {
                    var host = Dns.GetHostEntry(Dns.GetHostName());
                    foreach (var ip in host.AddressList)
                    {
                        if (ip.AddressFamily == AddressFamily.InterNetwork) return ip.ToString();
                    }
                    return "127.0.0.1";
                }
                catch (Exception)
                {
                    return "127.0.0.1";
                }

            }
        }

        [Conditional("DEBUG")]
        private static void ApplyDevelopmentEnvironment()
        {
            ConfigurationFileName = $"appsetting.development.json";
        }
    }
}
