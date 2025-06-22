using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace ManagementTransaction.Api.Controllers
{
    [Produces("application/json")]
    [ApiController]
    [Route("type-transaction")]
    public partial class TypeTransactionController : ControllerBase
    {
        private string Token = string.Empty;
        private readonly IMapper Mapper;

        public TypeTransactionController(IMapper mapper) => Mapper = mapper;
    }
}
