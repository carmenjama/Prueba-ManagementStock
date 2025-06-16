using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ManagementProducts.Api.Controllers.Payload
{
    public class UserPayload
    {
        [Required, DefaultValue("admin")]
        public string User { get; set; } = string.Empty;
        [Required, DefaultValue("16073a5bbae5f899b3f55b4e533e156a")]
        public string Pass { get; set; } = string.Empty;
    }
}
