using ManagementProducts.SharedDto.Dto;
using ManagementProducts.Use.Cases.Aurh.Interfaces;
using ManagementProducts.Use.Cases.Shared;
using ManagementProducts.Use.Cases.Transactions.Interfaces;
using ManagementTransaction.Api.Controllers.Payload;
using ManagementTransaction.Api.Core.Contexts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using System.ComponentModel.DataAnnotations;

namespace ManagementTransaction.Api.Controllers
{
    public partial class TransactionController
    {
        [HttpDelete]
        [Authorize]
        [Route("{id}")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        public IActionResult Delete(
            [FromServices] IDeleteTransaction useCase,
            [FromServices] IDecodeToken decodeToken,
            [FromRoute][Required] long id)
        {
            Token = Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", "");

            var validateToken = decodeToken
                .WithContext(ApplicationContext.JwtToken)
                .Execute(Token);

            if (validateToken.Failure() != null)
            {
                if (((UseCaseError)validateToken.Failure()).Reason == "Unauthorized") return Unauthorized();
                if (validateToken.Failure() is NoResult) return NotFound();
                if (validateToken.Failure() is UseCaseError) return BadRequest((UseCaseError)validateToken.Failure());
                return BadRequest();
            }

            var result = useCase
                .WithContext(ApplicationContext.SqlServerDbContext)
                .Execute(id, validateToken.Payload().User);

            if (result.Failure() != null)
            {
                if (result.Failure() is NoResult) return NotFound();
                if (result.Failure() is UseCaseError) return BadRequest((UseCaseError)result.Failure());
                return BadRequest();
            }

            return Ok(result.Payload());
        }
    }
}
