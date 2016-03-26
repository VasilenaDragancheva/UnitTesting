namespace RoomsControllerTests
{
    using System.Collections.Generic;

    using HotelBookingSystem.Controllers;
    using HotelBookingSystem.Data;
    using HotelBookingSystem.Interfaces;
    using HotelBookingSystem.Models;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public class VenuesControllerTests
    {
        [TestMethod]
        public void All_NoVenues_ReturnsRightMessage()
        {
            var user = new User("username", "password", Roles.VenueAdmin);
            var mockedVenues = new Mock<IRepository<Venue>>();
            mockedVenues.Setup(r => r.GetAll()).Returns(new List<Venue>());
            var data = new HotelBookingSystemData { Venues = mockedVenues.Object };
            var expected = "There are currently no venues to show.";
            var venuesController = new VenuesController(data, user);
            var message = venuesController.All().Display();

            Assert.AreEqual(expected, message);
        }
    }
}