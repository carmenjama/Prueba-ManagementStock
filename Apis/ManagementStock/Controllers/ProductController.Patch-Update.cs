using ManagementProducts.Api.Core.Contexts;
using ManagementProducts.SharedDto.Dto;
using ManagementProducts.Use.Cases.Aurh.Interfaces;
using ManagementProducts.Use.Cases.Product.Interfaces;
using ManagementProducts.Use.Cases.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using System.ComponentModel.DataAnnotations;

namespace ManagementProducts.Api.Controllers
{
    public partial class ProductController
    {
        [HttpPatch]
        [Authorize]
        [Route("{id}")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        public IActionResult Update(
            [FromServices] IUpdateProduct useCaseUpdate,
            [FromServices] IGetProductById useCaseGetById,
            [FromServices] IDecodeToken decodeToken,
            [FromRoute][Required] long id,
            [FromBody][Required] JsonPatchDocument<ProductDto> payload)
        {
            if (payload is null) return BadRequest();
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

            var product = useCaseGetById
                .WithContext(ApplicationContext.SqlServerDbContext)
                .Execute(id);
            if (product.Failure() != null)
            {
                if (product.Failure() is NoResult) return NotFound();
                if (product.Failure() is UseCaseError) return BadRequest((UseCaseError)product.Failure());
                return BadRequest();
            }

            payload.ApplyTo(product.Payload());
            var result = useCaseUpdate
                .WithContext(ApplicationContext.SqlServerDbContext)
                .Execute(product.Payload(), validateToken.Payload().User);

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
