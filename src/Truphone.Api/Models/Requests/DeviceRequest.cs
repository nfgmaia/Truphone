using System.ComponentModel.DataAnnotations;

namespace Truphone.Api.Models.Requests
{
    public class DeviceRequest
    {
        [Required]
        [StringLength(250)]
        public string Name { get; set; }

        [StringLength(250)]
        public string Brand { get; set; }
    }
}
