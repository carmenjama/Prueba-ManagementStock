using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace ManagementProducts.Api.Controllers
{
    [Produces("application/json")]
    [ApiController]
    [Route("category")]
    public partial class CategoryController : ControllerBase
    {
        private string Token = string.Empty;
        private readonly IMapper Mapper;

        public CategoryController(IMapper mapper) => Mapper = mapper;
    }
}
