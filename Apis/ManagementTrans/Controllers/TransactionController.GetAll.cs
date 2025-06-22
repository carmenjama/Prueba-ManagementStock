using ManagementProducts.SharedDto.Dto;
using ManagementProducts.Use.Cases.Aurh.Interfaces;
using ManagementProducts.Use.Cases.Shared;
using ManagementProducts.Use.Cases.Transactions.Interfaces;
using ManagementTrans.Api.Controllers.Responses;
using ManagementTrans.Api.Core.Contexts;
using ManagementTrans.Api.Core.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using System.ComponentModel.DataAnnotations;

namespace ManagementTrans.Api.Controllers
{
    public partial class TransactionController
    {
        [HttpGet]
        [Authorize]
        [Route("{isPaginated}")]
        [ProducesResponseType(typeof(PaginatedDto<TransactionResponsse>), StatusCodes.Status200OK)]
        public IActionResult GetAll(
            [FromServices] ITransactionsGetAll useCase,
            [FromServices] IDecodeToken decodeToken,
            [FromRoute][Required] bool isPaginated,
            [FromQuery] long? id,
            [FromQuery] long? typeTransactionId,
            [FromQuery] string? productName,
            [FromQuery] string? status,
            [FromQuery] DateTime? filterMinDate,
            [FromQuery] DateTime? filterMaxDate,
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
                .Execute(new TransactionDto
                {
                    Id = id ?? 0,
                    TypeTransactionId = typeTransactionId ?? 0,
                    ProductName = productName ?? string.Empty,
                    Status = status ?? string.Empty,
                    FilterMinDate = filterMinDate,
                    FilterMaxDate = filterMaxDate,
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

            var items = Mapper.Map<IEnumerable<TransactionResponsse>>(result.Payload()?.Elements);
            if (!isPaginated)
                return Ok(items);

            return Ok(items.PagerObject(result.Payload()));
        }
    }
}
