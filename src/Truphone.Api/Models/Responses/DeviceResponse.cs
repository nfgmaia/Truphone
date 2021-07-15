using System;

namespace Truphone.Api.Models.Responses
{
    public class DeviceResponse
    {
        public long Id { get; set; }
        public DateTime DateCreated { get; set; }
        public string Name { get; set; }
        public string Brand { get; set; }
    }
}
