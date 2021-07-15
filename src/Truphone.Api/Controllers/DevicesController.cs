using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Truphone.Api.Extensions;
using Truphone.Api.Models.Requests;
using Truphone.Api.Models.Responses;
using Truphone.Domain.Services;

namespace Truphone.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DevicesController : ControllerBase
    {
        private readonly IDeviceService service;

        public DevicesController(IDeviceService service)
        {
            this.service = service;
        }

        [HttpPost]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Post))]
        public async Task<ActionResult<DeviceResponse>> Add(DeviceRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var entity = await service.Add(request.ToEntity());
            return CreatedAtAction(nameof(Get), new { id = entity.Id }, entity.ToResponse());
        }

        [HttpGet("{id}")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        public async Task<ActionResult<DeviceResponse>> Get(long id)
        {
            var entity = await service.Get(id);
            if (entity == null)
            {
                return NotFound();
            }

            return Ok(entity.ToResponse());
        }

        [HttpGet]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<DeviceResponse>>> GetAll()
        {
            var entities = await service.GetAll();

            return Ok(entities.Select(e => e.ToResponse()));
        }

        [HttpPut("{id}")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Put))]
        public async Task<IActionResult> Update(long id, DeviceRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            if (!await service.Exists(id))
            {
                return NotFound();
            }

            await service.Update(id, request.ToEntity());
            return NoContent();
        }

        [HttpPatch("{id}")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Edit))]
        public async Task<IActionResult> UpdatePartial(long id, JsonPatchDocument<DeviceRequest> patchedRequest)
        {
            var entity = await service.Get(id);
            if (entity == null)
            {
                return NotFound();
            }

            var request = entity.ToRequest();
            patchedRequest.ApplyTo(request);

            if (!TryValidateModel(request))
            {
                return BadRequest();
            }

            await service.Update(id, request.ToEntity());
            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(long id)
        {
            if (!await service.Exists(id))
            {
                return NotFound();
            }

            await service.Delete(id);
            return NoContent();
        }

        [HttpGet("search")]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<DeviceResponse>>> Search([FromQuery] DeviceSearchRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var entities = await service.Search(request.Brand);
            return Ok(entities.Select(e => e.ToResponse()));
        }
    }
}
