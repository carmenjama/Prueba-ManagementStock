using ManagementTrans.Api.Core.Filters;
using ManagementTrans.Api.Core.Contexts;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace ManagementTrans.Api.Core.Handlers
{
    public class ConfigureSwaggerOptionsHandler : IConfigureNamedOptions<SwaggerGenOptions>
    {
        public void Configure(SwaggerGenOptions options)
        {
        }

        public void Configure(string name, SwaggerGenOptions options)
        {
            options.SwaggerDoc(ApplicationContext.AssemblyVersion, new OpenApiInfo
            {
                Version = $"Version: {ApplicationContext.AssemblyVersion} (.NET Core: {Environment.Version})",
                Title = ApplicationContext.ApplicationName,
            });

            //Adds secured by defining one or more security schemes
            options.AddSecurityDefinition(ApplicationContext.BEARER_AUTHENTICATION, new OpenApiSecurityScheme
            {
                Name = ApplicationContext.AUTHORIZATION_HEADER,
                Scheme = ApplicationContext.BEARER_AUTHENTICATION,
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
            });

            //Adds AUTH token to the services that require authentication
            options.OperationFilter<CustomAuthenticationFilter>();
        }
    }
}
