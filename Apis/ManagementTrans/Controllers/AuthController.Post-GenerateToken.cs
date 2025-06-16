using ManagementProducts.Api.Controllers.Payload;
using ManagementProducts.Api.Core.Contexts;
using ManagementProducts.Use.Cases.Aurh.Interfaces;
using ManagementProducts.Use.Cases.Shared;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace ManagementProducts.Api.Controllers
{
    public partial class AuthController
    {
        [HttpPost]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        public IActionResult GenerateToken(
            [FromServices] IGenerateToken useCase,
            [FromBody][Required] UserPayload payload)
        {
            var result = useCase
                .WithContext(ApplicationContext.JwtToken)
                .Execute(payload.User, payload.Pass, ApplicationContext.ApplicationCode);

            if (result.Failure() != null)
            {
                if (((UseCaseError)result.Failure()).Reason == "Unauthorized") return Unauthorized();
                if (result.Failure() is NoResult) return NotFound();
                if (result.Failure() is UseCaseError) return BadRequest((UseCaseError)result.Failure());
                return BadRequest();
            }

            return Ok(result.Payload());
        }
    }
}
