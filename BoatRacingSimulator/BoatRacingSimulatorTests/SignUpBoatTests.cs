namespace BoatRacingSimulatorTests
{
    using System;

    using BoatRacingSimulator.Controllers;
    using BoatRacingSimulator.Exceptions;
    using BoatRacingSimulator.Interfaces;
    using BoatRacingSimulator.Models;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public class SignUpBoatTests
    {
        [TestMethod]
        [ExpectedException(typeof(NoSetRaceException))]
        public void SignBoat_ToNotSetRace_ThrowsException()
        {
            var boat = new SailBoat("Model", 300, 72);
            var mockedData = new Mock<IBoatSimulatorDatabase>();
            mockedData.Setup(d => d.Boats.GetItem(It.IsAny<string>())).Returns(boat);

            var controller = new BoatSimulatorController(mockedData.Object, null);

            // act
            controller.SignUpBoat("Model");
        }

        [TestMethod]
        public void SignMotorBoat_ToSetRace_ALlowdCorrectMessage()
        {
            // act
            var race = new Race(1000, 10, 15, true);
            var firstEngne = new JetEngine("engine1", 500, 30);
            var secondEngine = new SterndriveEngine("engine2", 500, 67);
            var boat = new PowerBoat("PowerBoat", 1000, firstEngne, secondEngine);
            var mockedData = new Mock<IBoatSimulatorDatabase>();
            mockedData.Setup(d => d.Boats.GetItem(It.IsAny<string>())).Returns(boat);
            var controller = new BoatSimulatorController(mockedData.Object, race);
            var expected = "Boat with model PowerBoat has signed up for the current Race.";

            // act
            var actual = controller.SignUpBoat("PowerBoat");

            // assert
            Assert.AreEqual(expected, actual, "Method does not return right message");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void SignMotorBoat_ToSetUpRace_NotAllowedThrowsException()
        {
            string model = "CoolYacht";
            var engine = new JetEngine("engine", 400, 20);
            var race = new Race(1000, 10, 15, false);
            var motorBoat = new Yacht(model, 700, engine, 200);
            var mockedData = new Mock<IBoatSimulatorDatabase>();
            mockedData.Setup(d => d.Boats.GetItem(It.IsAny<string>())).Returns(motorBoat);

            var controller = new BoatSimulatorController(mockedData.Object, race);

            // act
            controller.SignUpBoat(model);
        }
    }
}