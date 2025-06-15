using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace ManagementProducts.Api.Controllers
{
    [Produces("application/json")]
    [ApiController]
    [Route("product")]
    public partial class ProductController : ControllerBase
    {
        private string Token = string.Empty;
        private readonly IMapper Mapper;

        public ProductController(IMapper mapper) => Mapper = mapper;
    }
}
