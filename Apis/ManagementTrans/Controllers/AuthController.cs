using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ManagementProducts.Api.Controllers
{
    [Produces("application/json")]
    [ApiController]
    [Route("auth")]
    [AllowAnonymous]
    public partial class AuthController : ControllerBase
    {
        private readonly IMapper Mapper;

        public AuthController(IMapper mapper) => Mapper = mapper;
    }
}
