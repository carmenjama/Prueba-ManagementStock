using ManagementProducts.Api.Controllers.Responses;
using ManagementProducts.Api.Core.Contexts;
using ManagementProducts.Api.Core.Shared;
using ManagementProducts.SharedDto.Dto;
using ManagementProducts.Use.Cases.Aurh.Interfaces;
using ManagementProducts.Use.Cases.Product.Interfaces;
using ManagementProducts.Use.Cases.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using System.ComponentModel.DataAnnotations;

namespace ManagementProducts.Api.Controllers
{
    public partial class ProductController
    {
        [HttpGet]
        [Authorize]
        [Route("{isPaginated}")]
        [ProducesResponseType(typeof(PaginatedDto<ProductResponse>), StatusCodes.Status200OK)]
        public IActionResult GetAll(
            [FromServices] IGetAllProduct useCase,
            [FromServices] IDecodeToken decodeToken,
            [FromRoute][Required] bool isPaginated,
            [FromQuery] long? id,
            [FromQuery] long? categoryId,
            [FromQuery] string? code,
            [FromQuery] string? name,
            [FromQuery] decimal? price,
            [FromQuery] string? unit,
            [FromQuery] decimal? stock,
            [FromQuery] decimal? filterPriceMin,
            [FromQuery] decimal? filterPriceMax,
            [FromQuery] decimal? filterStockMin,
            [FromQuery] decimal? filterStockMax,
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
                .Execute(new ProductDto
                {
                    Id = id,
                    CategoryId = categoryId ?? 0,
                    Code = code ?? string.Empty,
                    Name = name ?? string.Empty,
                    Price = price ?? 0,
                    Unit = unit ?? string.Empty,
                    Stock = stock ?? 0,
                    FilterPriceMin = filterPriceMin,
                    FilterPriceMax = filterPriceMax,
                    FilterStockMin = filterStockMin,
                    FilterStockMax = filterStockMax,
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

            var items = Mapper.Map<IEnumerable<ProductResponse>>(result.Payload()?.Elements);
            if (!isPaginated)
                return Ok(items);

            return Ok(items.PagerObject(result.Payload()));
        }
    }
}
