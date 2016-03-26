namespace BoatRacingSimulatorTests
{
    using BoatRacingSimulator.Controllers;
    using BoatRacingSimulator.Database;
    using BoatRacingSimulator.Exceptions;
    using BoatRacingSimulator.Models;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class OpenRaceTests
    {
        [TestMethod]
        [ExpectedException(typeof(RaceAlreadyExistsException))]
        public void OpenRace_AlreadySetRace_ThrowsException()
        {
            // Arrange
            var race = new Race(100, 1, 2, true);
            var data = new BoatSimulatorDatabase();
            var controller = new BoatSimulatorController(data, race);

            // Act
            controller.OpenRace(100, 2, 1, true);
        }

        [TestMethod]
        public void OpenRace_NotSetRace_ValidRaceReturnsCorrectMesage()
        {
            // Arrange
            int distance = 500;
            int windSpeed = 11;
            int oceanSpeed = 9;
            var controller = new BoatSimulatorController();
            var expectedMeesage =
                string.Format(
                    "A new race with distance {0} meters, wind speed {1} m/s and ocean current speed {2} m/s has been set.", 
                    distance, 
                    windSpeed, 
                    oceanSpeed);

            // Act
            var actualMessage = controller.OpenRace(distance, windSpeed, oceanSpeed, true);

            // Assert
            Assert.AreEqual(expectedMeesage, actualMessage, "Method does not work properly");
        }
    }
}