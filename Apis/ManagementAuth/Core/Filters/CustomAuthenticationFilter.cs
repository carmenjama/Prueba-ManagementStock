using ManagementAuth.Api.Core.Contexts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace ManagementAuth.Api.Core.Filters
{
    /// <summary>
    /// Adds AUTH token to the services that require authentication
    /// </summary>
    public class CustomAuthenticationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var apiDescription = context.ApiDescription;
            if (operation.Parameters == null) return;

            foreach (var parameter in operation.Parameters)
            {
                var description = apiDescription.ParameterDescriptions.First(p => p.Name == parameter.Name);
                if (parameter.Description == null)
                {
                    parameter.Description = description.ModelMetadata?.Description;
                }

                if (parameter.Schema.Default == null && description.DefaultValue != null)
                {
                    parameter.Schema.Default = new OpenApiString(description.DefaultValue.ToString());
                }

                parameter.Required |= description.IsRequired;
            }

            foreach (var parameter in operation.Parameters)
            {
                var description = apiDescription.ParameterDescriptions.First(p => p.Name == parameter.Name);
                if (parameter.Description == null)
                    parameter.Description = description.ModelMetadata?.Description;
                parameter.Required |= description.IsRequired;
            }

            if (operation.Security == null) operation.Security = new List<OpenApiSecurityRequirement>();

            var requiredScopes = context.MethodInfo
               .GetCustomAttributes(true)
               .OfType<AuthorizeAttribute>()
               .Select(attr => attr.Policy)
               .Distinct()
               .ToList();

            if (requiredScopes.Count.Equals(0)) return;

            if (!requiredScopes.Count.Equals(1))
                throw new NullReferenceException($"Scope configured [Authorize('{string.Join(",", requiredScopes)}')] not found!");

            var scopeSummary = "Scope";
            var scopeDetail = $@"[""{requiredScopes[0]}""]";

            operation.Description = string.IsNullOrEmpty(operation.Description)
              ? $"<b>{scopeSummary}</b>: {scopeDetail}"
              : $"{operation.Description} - <b>{scopeSummary}</b>: [{scopeDetail}]";
            var scheme = new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = ApplicationContext.BEARER_AUTHENTICATION
                }
            };

            operation.Security.Add(new OpenApiSecurityRequirement
            {
                [scheme] = new List<string>()
            });
        }
    }
}
