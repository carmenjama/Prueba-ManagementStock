using Azure.Core;
using ManagementProducts.Api.Controllers.Responses;
using ManagementProducts.Api.Core.Contexts;
using ManagementProducts.Api.Core.Shared;
using ManagementProducts.SharedDto.Dto;
using ManagementProducts.Use.Cases.Aurh.Interfaces;
using ManagementProducts.Use.Cases.Category.Interfaces;
using ManagementProducts.Use.Cases.Shared;
using ManagementProducts.Use.Cases.TypeTransactions.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using System.ComponentModel.DataAnnotations;

namespace ManagementProducts.Api.Controllers
{
    public partial class TypeTransactionController
    {
        [HttpGet]
        [Authorize]
        [Route("{isPaginated}")]
        [ProducesResponseType(typeof(PaginatedDto<TypeTransactionResponsse>), StatusCodes.Status200OK)]
        public IActionResult GetAll(
            [FromServices] ITypeTransactionsGetAll useCase,
            [FromServices] IDecodeToken decodeToken,
            [FromRoute][Required] bool isPaginated,
            [FromQuery] int page = 0,
            [FromQuery] int limit = 0)
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
                .Execute(new TypeTransactionDto
                {
                    Status = "ACTIVO",
                    IsPaginated = isPaginated,
                    Page = page,
                    Limit = limit
                });

            if (result.Failure() != null)
            {
                if (result.Failure() is NoResult) return NotFound();
                if (result.Failure() is UseCaseError) return BadRequest((UseCaseError)result.Failure());
                return BadRequest();
            }

            var items = Mapper.Map<IEnumerable<TypeTransactionResponsse>>(result.Payload()?.Elements);
            if (!isPaginated)
                return Ok(items);

            return Ok(items.PagerObject(result.Payload()));
        }
    }
}
