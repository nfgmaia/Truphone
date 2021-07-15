using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Truphone.Application.Services;
using Truphone.Domain.Adapters;
using Truphone.Domain.Entities;

namespace Truphone.UnitTests.Application
{
    [TestClass]
    public class DeviceServiceTests
    {
        private Mock<IUnitOfWork> Uow;
        private Mock<IDeviceRepository> DeviceRepository;

        [TestInitialize]
        public void Initialize()
        {
            Uow = new Mock<IUnitOfWork>();
            DeviceRepository = new Mock<IDeviceRepository>();
            Uow.Setup(e => e.DeviceRepository).Returns(DeviceRepository.Object);
        }

        [TestMethod]
        public async Task AddShouldReturnTheInsertedEntity()
        {
            // Arrange
            var input = new Device { Name = "Name", Brand = "Brand" };
            long insertedId = 1;
            DeviceRepository.Setup(e => e.Add(It.IsAny<Device>())).ReturnsAsync(insertedId);
            DeviceRepository.Setup(e => e.Get(It.IsAny<long>())).ReturnsAsync(
                new Device { Id = insertedId, DateCreated = DateTime.Now, DateUpdated = DateTime.Now, Name = input.Name, Brand = input.Brand });

            // Act
            var entity = await new DeviceService(Uow.Object).Add(input);

            // Assert
            entity.Should().NotBeNull();
            entity.Id.Should().Be(insertedId);
            entity.Name.Should().Be(input.Name);
            entity.Brand.Should().Be(input.Brand);
            entity.DateCreated.Should().NotBe(default(DateTime));
            entity.DateUpdated.Should().NotBe(default(DateTime));
        }

        [TestMethod]
        public async Task GetShouldReturnAnEntityWhenFound()
        {
            // Arrange
            long id = 1;
            DeviceRepository.Setup(e => e.Get(It.IsAny<long>())).ReturnsAsync(
                new Device { Id = id, DateCreated = DateTime.Now, DateUpdated = DateTime.Now });

            // Act
            var entity = await new DeviceService(Uow.Object).Get(id);

            // Assert
            entity.Should().NotBeNull();
            entity.Id.Should().Be(id);
            entity.DateCreated.Should().NotBe(default(DateTime));
            entity.DateUpdated.Should().NotBe(default(DateTime));
        }

        [TestMethod]
        public async Task GetShouldReturnNullWhenEntityNotFound()
        {
            // Arrange
            int id = 1;
            DeviceRepository.Setup(e => e.Get(id)).ReturnsAsync((Device)null);
            DeviceRepository.Setup(e => e.Get(It.IsAny<long>())).ReturnsAsync(id == 1 ? null : new Device());

            // Act
            var entity = await new DeviceService(Uow.Object).Get(id);

            // Assert
            entity.Should().BeNull();
        }

        [TestMethod]
        public async Task GetAllShouldReturnEmptyListWhenNoEntitiesFound()
        {
            // Arrange
            DeviceRepository.Setup(e => e.GetAll()).ReturnsAsync((IReadOnlyCollection<Device>)null);

            // Act
            var list = await new DeviceService(Uow.Object).GetAll();

            // Assert
            list.Should().NotBeNull().And.BeEmpty();
        }

        [TestMethod]
        public async Task SearchShouldReturnEmptyListWhenNoEntitiesFound()
        {
            // Arrange
            DeviceRepository.Setup(e => e.Query(It.IsAny<string>())).ReturnsAsync((IReadOnlyCollection<Device>)null);

            // Act
            var list = await new DeviceService(Uow.Object).Search("myBrand");

            // Assert
            list.Should().NotBeNull().And.BeEmpty();
        }

        [TestMethod]
        public async Task UpdateShouldPreserveDateCreated()
        {
            // Arrange
            int id = 1;
            var dateCreated = DateTime.Now;
            DeviceRepository.Setup(e => e.Get(It.IsAny<long>())).ReturnsAsync(
                new Device { Id = id, Name = "Name", Brand = "Brand", DateCreated = dateCreated, DateUpdated = dateCreated });

            DeviceRepository.Setup(e => e.Update(It.IsAny<Device>())).ReturnsAsync(true);

            // Act
            var input = new Device { Name = "NewName", Brand = "NewBrand" };
            var entity = await new DeviceService(Uow.Object).Update(id, input);

            //// Assert
            entity.Should().NotBeNull();
            entity.DateCreated.Should().Be(dateCreated);
        }
    }
}
