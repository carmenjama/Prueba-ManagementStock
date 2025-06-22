using System.Text.Json.Serialization;
using System.Text;
using ManagementAuth.Api.Core.Formatters;
using ManagementAuth.Api.Core.Contexts;
using AspNetCoreRateLimit;
using ManagementAuth.Api.Core.Handlers;
using ManagementAuth.Api.Core.Settings;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json.Converters;
using TanvirArjel.Extensions.Microsoft.DependencyInjection;

namespace ManagementAuth.Api
{
    public class Startup
    {
        //Origins identifier
        private const string _specificOrigins = "_SpecificOrigins";

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services">Interface Service Collection</param>
        public void ConfigureServices(IServiceCollection services)
        {
            //Adds service configs
            services
              //Adds services required for using options
              .AddOptions()
              //Adds caching implementation
              .AddMemoryCache()
              //Adds cross-origin resources
              .AddCors(options =>
              {
                  options.AddPolicy(
                  name: _specificOrigins,
                  configurePolicy: builder =>
                      {
                          builder
                    .AllowCredentials()
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .SetIsOriginAllowed(origin => true);
                      });
              })
            //Adds JsonBody request controllers entry
            .AddControllersWithViews(options => { options.InputFormatters.Insert(0, new RawJsonBodyInputFormatter()); });
            //Adds Json formatters specific features input and output configured

            // Inside the ConfigureServices method
            services
                .AddControllersWithViews(options => { options.InputFormatters.Insert(0, new RawJsonBodyInputFormatter()); })
                .AddNewtonsoftJson(options => // Fix: Ensure AddNewtonsoftJson is chained to AddControllersWithViews
                {
                    options.UseCamelCasing(false);
                    options.UseMemberCasing();
                    options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                    options.SerializerSettings.Converters.Add(new StringEnumConverter());
                });
            // Adds services for using Problem Details format
            services.AddProblemDetails();

            services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
            });
            //Loads caching configuration
            services.Configure<IpRateLimitOptions>(ApplicationContext.IpRateLimitOptions);
            //Injects counter and rules stores
            services.AddSingleton<IIpPolicyStore, MemoryCacheIpPolicyStore>();
            services.AddSingleton<IRateLimitCounterStore, MemoryCacheRateLimitCounterStore>();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            // Registers required services for health checks
            services.AddHealthChecks();
            //Set swagger options configuration
            services.ConfigureOptions<ConfigureSwaggerOptionsHandler>();
            //Adds AutoMapper dependency inyection 
            services
            .AddAutoMapper(typeof(AutoMapperSettings).Assembly)
            .AddSwaggerGen(config =>
            {
                config.UseInlineDefinitionsForEnums();
            });

            //Validates development enviroment to authorization control
            if (ApplicationContext.IsProduction)
            {
                //Adds all services anonymous authorization is allowed
                services.AddSingleton<IAuthorizationHandler, HasAnonymousScopeHanddler>();
            }
            else
            {
                //Adds services able to make a decision if authorization is allowed
                services.AddSingleton<IAuthorizationHandler, HasScopeHandler>();
            }

            //Adds HttpContext access inside services
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            //Adds IpRateLimit configuration (resolvers, counter key builders)
            services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
            //Adds rate limit in memory
            services.AddInMemoryRateLimiting();

            //Adds singleton, trasient or scoped types implemented
            services.AddServicesOfAllTypes();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
             .AddJwtBearer(options =>
             {
                 options.TokenValidationParameters = new TokenValidationParameters
                 {
                     ValidateIssuer = false,
                     ValidateAudience = true,
                     ValidateLifetime = true,
                     ValidateIssuerSigningKey = true,
                     ValidIssuer = ApplicationContext.JwtToken.IsUserToken,
                     ValidAudience = ApplicationContext.JwtToken.IsUserToken,
                     IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(ApplicationContext.JwtToken.Key))
                 };
             });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //Adds IpRateLimit middleware for the set multiple limits for different scenarios
            app.UseIpRateLimiting();
            //Adds middleware for redirectiong HTTP requests to HTTPS
            app.UseHttpsRedirection();
            //Adds middleware for using header strinct transport security (HSTS)
            if (env.IsDevelopment()) app.UseDeveloperExceptionPage(); else app.UseHsts();

            if (!ApplicationContext.IsProduction)
            {
                app.UseSwagger(options => { options.RouteTemplate = "docs/{documentName}/docs.json"; });
                app.UseSwaggerUI(options =>
                {
                    //Disable swagger schemas at bottom
                    options.DefaultModelsExpandDepth(-1);
                    options.SwaggerEndpoint($"/docs/{ApplicationContext.AssemblyVersion}/docs.json", ApplicationContext.ApplicationName);
                    options.RoutePrefix = "docs";
                    options.DocumentTitle = ApplicationContext.ApplicationName;
                    options.DisplayRequestDuration();
                });
            }

            //Enables static files serving for the current request path
            app.UseStaticFiles();
            //Adds Rounting middleware
            app.UseRouting();
            //Adds CORS (Cross-Origin Request) middleware to allow cross domain requests
            app.UseCors(policyName: _specificOrigins);
            //Adds Authentication middleware
            app.UseAuthentication();
            //Adds Authorization middleware
            app.UseAuthorization();

            // Converts unhandled exceptions into Problem Details responses
            app.UseExceptionHandler();
            // Returns the Problem Details response for (empty) non-successful responses
            app.UseStatusCodePages();

            //Endpoints bridge for controllers action without specifying any routes
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHealthChecks("/health-checks");
                endpoints.MapControllers();
            });
        }
    }
}
