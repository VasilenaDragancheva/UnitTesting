namespace RoomsControllerTests
{
    using HotelBookingSystem.Controllers;
    using HotelBookingSystem.Data;
    using HotelBookingSystem.Models;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class UsersControllerTests
    {
        [TestMethod]
        public void Logout_ValidUser_ReturnsCorrectMessage()
        {
            var data = new HotelBookingSystemData();
            var validUser = new User("username", "password", Roles.User);
            var expectedMessage = "The user username has logged out.";
            var controller = new UsersController(data, validUser);

            // act
            var view = controller.Logout();
            var actualMessage = view.Display();

            Assert.AreEqual(expectedMessage, actualMessage);
        }

        [TestMethod]
        public void Logout_ValidUser_SetCurrentUserToNull()
        {
            var data = new HotelBookingSystemData();
            var validUser = new User("username", "password", Roles.User);

            var controller = new UsersController(data, validUser);

            // act
            controller.Logout();

            Assert.IsNull(controller.CurrentUser);
        }
    }
}