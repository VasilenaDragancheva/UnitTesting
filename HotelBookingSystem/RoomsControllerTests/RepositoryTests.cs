namespace RoomsControllerTests
{
    using HotelBookingSystem.Data;
    using HotelBookingSystem.Models;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class RepositoryTests
    {
        [TestMethod]
        public void Get_ExistingId_ReturnCOrrectValue()
        {
            // arrange
            var repository = new Repository<Room>();
            var room1 = new Room(1, 20);
            var room2 = new Room(2, 20);
            var room3 = new Room(3, 20);

            repository.Add(room1);
            repository.Add(room3);
            repository.Add(room2);

            // act
            var actualRoom = repository.Get(2);

            Assert.AreEqual(room3, actualRoom);
        }

        [TestMethod]
        public void Get_NotExistingId_ReturnDefault()
        {
            // arrange
            var repository = new Repository<Room>();
            var room1 = new Room(1, 20);
            var room2 = new Room(2, 20);
            var room3 = new Room(3, 20);

            repository.Add(room1);
            repository.Add(room3);

            // act
            var actualRoom = repository.Get(6);

            Assert.AreEqual(null, actualRoom);
        }
    }
}