using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ManagementProducts.Api.Controllers.Payload
{
    public class UserPayload
    {
        [Required]
        public string User { get; set; } = string.Empty;
        [Required]
        public string Pass { get; set; } = string.Empty;
    }
}
