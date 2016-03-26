namespace RoomsControllerTests
{
    using System;

    using HotelBookingSystem.Controllers;
    using HotelBookingSystem.CustomException;
    using HotelBookingSystem.Data;
    using HotelBookingSystem.Interfaces;
    using HotelBookingSystem.Models;
    using HotelBookingSystem.Views.Shared;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public class RoomsControllerTests
    {
        [TestMethod]
        [ExpectedException(typeof(AuthorizationFailedException))]
        public void Add_LoggedUserNotAdminRole_ThrowsException()
        {
            // Arrange
            var data = new HotelBookingSystemData();
            var user = new User("username", "password", Roles.User);

            var controller = new RoomsController(data, user);

            // act
            controller.Add(1, 2, 10m);
        }

        [TestMethod]
        public void Add_NotExistingVenue_ThrowsException()
        {
            // Arrange
            var data = new HotelBookingSystemData();
            var user = new User("username", "password", Roles.VenueAdmin);

            var controller = new RoomsController(data, user);

            // act
            var view = controller.Add(1, 2, 10m);
            var actual = view.Display();
            var expected = string.Format("The venue with ID {0} does not exist.", 1);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Add_NoUser_ThrowsException()
        {
            // Arrange
            var data = new HotelBookingSystemData();

            // act
            var controller = new RoomsController(data, null);
            controller.Add(1, 2, 10m);
        }

        [TestMethod]
        public void Add_RoomToExistingRepository_ReturnsRightMessage()
        {
            var data = new HotelBookingSystemData();
            var user = new User("username", "password", Roles.VenueAdmin);
            var controller = new RoomsController(data, user);
            data.Venues.Add(new Venue("Magnolia", "Sofia", "motel", user));

            // act
            var view = controller.Add(1, 2, 10m);
            var expected = view.Display();
            var actual = "The room with ID 1 has been created successfully.";

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [ExpectedException(typeof(AuthorizationFailedException))]
        public void AddPeiord_LoggedUserNotAdminRole_ThrowsException()
        {
            // Arrange
            var data = new HotelBookingSystemData();
            var user = new User("username", "password", Roles.User);

            var controller = new RoomsController(data, user);

            // act
            controller.AddPeriod(1, new DateTime(2016, 1, 1), new DateTime(2016, 2, 1));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void AddPeriod_NoUser_ThrowsException()
        {
            // Arrange
            var data = new HotelBookingSystemData();

            // act
            var controller = new RoomsController(data, null);
            controller.AddPeriod(1, new DateTime(2016, 1, 1), new DateTime(2016, 2, 1));
        }

        [TestMethod]
        public void AddPeriod_ToExistingRoom_ReturnsCorrectMessage()
        {
            // arrange
            var room = new Room(2, 20);
            room.Id = 2;
            var mockedRepo = new Mock<IRepository<Room>>();
            mockedRepo.Setup(r => r.Get(It.IsAny<int>())).Returns(room);
            var data = new HotelBookingSystemData(mockedRepo.Object);
            var user = new User("username", "password", Roles.VenueAdmin);
            var controller = new RoomsController(data, user);
            string expectedMessage = "The period has been added to room with ID 2.";

            // Act
            var view = controller.AddPeriod(2, new DateTime(2016, 1, 1), new DateTime(2016, 2, 1));
            string actualMessage = view.Display();

            // Assert
            Assert.AreEqual(expectedMessage, actualMessage);
        }

        [TestMethod]
        public void AddPeriod_ValidUserNotFoundROom_RetursRightMessage()
        {
            var user = new User("username", "password", Roles.VenueAdmin);
            var mockedRoom = new Mock<IRepository<Room>>();
            mockedRoom.Setup(r => r.Get(It.IsAny<int>())).Returns((Room)null);
            var data = new HotelBookingSystemData(mockedRoom.Object);
            var controller = new RoomsController(data, user);
            var error = new Error("The room with ID 1 does not exist.");

            // Act
            var view = controller.AddPeriod(1, new DateTime(2016, 1, 1), new DateTime(2016, 2, 1));

            // Assert
            Assert.AreEqual(error.Display(), view.Display());
        }

        [TestMethod]
        public void Book_NotExistingRoom_ReturnsErrorMessage()
        {
            // ararnage
            var mockedRepo = new Mock<IRepository<Room>>();
            mockedRepo.Setup(r => r.Get(It.IsAny<int>())).Returns((Room)null);
            var data = new HotelBookingSystemData(mockedRepo.Object);

            var controller = new RoomsController(data, new User("username", "password", Roles.VenueAdmin));

            var actualMessage = new Error("The room with ID 3 does not exist.").Display();

            // act
            var view = controller.Book(3, new DateTime(2016, 1, 1), new DateTime(2016, 2, 1), "null");

            // assert
            Assert.AreEqual(view.Display(), actualMessage);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Book_NotValidDates_ThrowsException()
        {
            // ararnage
            var room = new Room(2, 20);
            room.Id = 3;
            var mockedRepo = new Mock<IRepository<Room>>();
            mockedRepo.Setup(r => r.Get(It.IsAny<int>())).Returns(room);
            var data = new HotelBookingSystemData(mockedRepo.Object);

            var controller = new RoomsController(data, new User("username", "password", Roles.VenueAdmin));

            // act
            var view = controller.Book(3, new DateTime(2016, 3, 1), new DateTime(2016, 2, 1), "null");
        }
    }
}