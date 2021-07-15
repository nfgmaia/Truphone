using Truphone.Api.Models.Requests;
using Truphone.Api.Models.Responses;
using Truphone.Domain.Entities;

namespace Truphone.Api.Extensions
{
    static class ModelsExtensions
    {
        public static Device ToEntity(this DeviceRequest dto)
        {
            return new Device
            {
                Name = dto.Name,
                Brand = dto.Brand
            };
        }

        public static DeviceRequest ToRequest(this Device entity)
        {
            return new DeviceRequest
            {
                Name = entity.Name,
                Brand = entity.Brand
            };
        }

        public static DeviceResponse ToResponse(this Device entity)
        {
            return new DeviceResponse
            {
                Id = entity.Id,
                DateCreated = entity.DateCreated,
                Name = entity.Name,
                Brand = entity.Brand
            };
        }
    }
}
