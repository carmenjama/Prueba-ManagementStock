using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace ManagementTrans.Api.Controllers
{
    [Produces("application/json")]
    [ApiController]
    [Route("transaction")]
    public partial class TransactionController : ControllerBase
    {
        private string Token = string.Empty;
        private readonly IMapper Mapper;

        public TransactionController(IMapper mapper) => Mapper = mapper;
    }
}
