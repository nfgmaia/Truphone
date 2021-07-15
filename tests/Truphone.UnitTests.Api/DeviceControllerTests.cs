using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Net;
using System.Threading.Tasks;
using Truphone.Api.Controllers;
using Truphone.Api.Models.Requests;
using Truphone.Domain.Adapters;
using Truphone.Domain.Entities;
using Truphone.Domain.Services;

namespace Truphone.UnitTests.Api
{
    [TestClass]
    public class DeviceControllerTests
    {
        private Mock<IDeviceService> DeviceService;
        private Mock<IUnitOfWork> Uow;

        [TestInitialize]
        public void Initialize()
        {
            DeviceService = new Mock<IDeviceService>();
            Uow = new Mock<IUnitOfWork>();
        }

        [TestMethod]
        public async Task AddShouldReturnBadRequestWithInvalidModelState()
        {
            // Arrange
            DeviceService.Setup(e => e.Add(It.IsAny<Device>())).ReturnsAsync(new Device());
            var controller = new DevicesController(DeviceService.Object);
            controller.ModelState.AddModelError("Name", "Required");
            
            // Act
            var request = new DeviceRequest();
            var result = await controller.Add(request);

            // Assert
            result.Result.Should().BeOfType(typeof(BadRequestResult));
        }

        [TestMethod]
        public async Task GetShouldReturnNotFoundWhenEntityNotExists()
        {
            // Arrange
            int id = 1;
            DeviceService.Setup(e => e.Get(It.IsAny<long>())).ReturnsAsync(id == 1 ? null : new Device());

            // Act
            var request = new DeviceRequest();
            var result = await new DevicesController(DeviceService.Object).Get(id);

            // Assert
            result.Result.Should().BeOfType(typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task UpdateShouldReturnNoContentWhenSuccessful()
        {
            // Arrange
            int id = 1;
            DeviceService.Setup(e => e.Exists(It.IsAny<long>())).ReturnsAsync(true);

            // Act
            var request = new DeviceRequest();
            var result = await new DevicesController(DeviceService.Object).Update(id, request);

            // Assert
            result.Should().BeOfType(typeof(NoContentResult));
        }

        [TestMethod]
        public async Task DeleteShouldReturnNotFoundWhenEntityNotExists()
        {
            // Arrange
            int id = 1;
            DeviceService.Setup(e => e.Exists(It.IsAny<long>())).ReturnsAsync(false);

            // Act
            var request = new DeviceRequest();
            var result = await new DevicesController(DeviceService.Object).Delete(id);

            // Assert
            result.Should().BeOfType(typeof(NotFoundResult));
        }
    }
}
