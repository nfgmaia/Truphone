using System.ComponentModel.DataAnnotations;

namespace Truphone.Api.Models.Requests
{
    public class DeviceSearchRequest
    {
        [Required]
        [StringLength(250)]
        public string Brand { get; set; }
    }
}
