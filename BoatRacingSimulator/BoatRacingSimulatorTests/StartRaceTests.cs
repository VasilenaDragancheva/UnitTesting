namespace BoatRacingSimulatorTests
{
    using System.Text;

    using BoatRacingSimulator.Controllers;
    using BoatRacingSimulator.Database;
    using BoatRacingSimulator.Exceptions;
    using BoatRacingSimulator.Interfaces;
    using BoatRacingSimulator.Models;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public class StartRaceTests
    {
        [TestMethod]
        public void EnoughParticipants_ShouldReturnPorperWinners()
        {
            // Arrange
            var race = new Race(1500, 150, 10, false);
            var boat1 = new RowBoat("MasterRower10", 200, 4);
            var boat2 = new RowBoat("MasterRower11", 200, 4);
            var boat3 = new RowBoat("MasterRower12", 200, 4);
            race.AddParticipant(boat2);
            race.AddParticipant(boat1);
            race.AddParticipant(boat3);
            var firstOutput = this.FormatOuput(boat2, race);
            var secondOutput = this.FormatOuput(boat1, race);
            var thirdOutput = this.FormatOuput(boat3, race);
            var expectedMessage = new StringBuilder();
            expectedMessage.AppendLine(string.Format("First place: {0}", firstOutput))
                .AppendLine(string.Format("Second place: {0}", secondOutput))
                .Append(string.Format("Third place: {0}", thirdOutput));
            var controller = new BoatSimulatorController(new BoatSimulatorDatabase(), race);

            // Act
            var actualMessage = controller.StartRace();

            // Assert
            Assert.AreEqual(expectedMessage.ToString(), actualMessage, "Does not work properly");
        }

        [TestMethod]
        [ExpectedException(typeof(InsufficientContestantsException))]
        public void NotEnoughParticipant_ThrowsProperException()
        {
            // Arrange
            var mockedRace = new Mock<IRace>();
            mockedRace.Setup(r => r.GetParticipants().Count).Returns(2);
            var controller = new BoatSimulatorController(new BoatSimulatorDatabase(), mockedRace.Object);

            // Act
            controller.StartRace();
        }

        [TestMethod]
        [ExpectedException(typeof(NoSetRaceException))]
        public void NotSetupRace_ThrowsException()
        {
            // arrange
            var controller = new BoatSimulatorController();

            // act
            controller.StartRace();
        }

        private object FormatOuput(IBoat boat, IRace race)
        {
            double timeCalculted = race.Distance / boat.CalculateRaceSpeed(race);
            var timeToString = timeCalculted <= 0 ? "Did not finish!" : timeCalculted.ToString("0.00") + " sec";
            string result = string.Format("{0} Model: {1} Time: {2}", boat.GetType().Name, boat.Model, timeToString);
            return result;
        }
    }
}