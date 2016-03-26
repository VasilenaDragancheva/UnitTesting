namespace AirConditionerSystem.Tests
{
    using System;

    using AirConditionerTestingSystem.Core;
    using AirConditionerTestingSystem.CustomExceptions;
    using AirConditionerTestingSystem.Models;
    using AirConditionerTestingSystem.Utilities;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class RegistrationTests
    {
        private Controller controller;

        private Command validCommand;

        [TestInitialize]
        public void CreateInstances()
        {
            this.controller = new Controller();
            this.validCommand = this.SetValidCommand();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Register_NegativePowerUsage_ShouldThrowException()
        {
            // Arrange
            this.validCommand.Parameters[3] = "-2";

            // Act
            this.controller.ExecuteCommand(this.validCommand);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Register_ShorterManiFacuture_ShouldThrowException()
        {
            // Arrange
            this.validCommand.Parameters[0] = "LG";

            // Act
            this.controller.ExecuteCommand(this.validCommand);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Register_ShorterModel_ShouldThrowException()
        {
            // Arrange
            this.validCommand.Parameters[1] = "G";

            // Act
            this.controller.ExecuteCommand(this.validCommand);
        }

        [TestMethod]
        [ExpectedException(typeof(DuplicateEntryException))]
        public void Register_ValidButUniqueParameters_ThrowsException()
        {
            // Arrange
            this.validCommand = this.SetValidCommand();
            this.controller.ExecuteCommand(this.validCommand);
            var nextCommand = new Command
                                  {
                                      Name = "RegisterStationaryAirConditioner", 
                                      Parameters = new[] { "Siemens", "LXAA", "C", "1600" }
                                  };

            // Act
            this.controller.ExecuteCommand(nextCommand);
        }

        [TestMethod]
        public void Register_ValidUniqueParameters_ShouldSuccess()
        {
            // Arrange
            this.validCommand = this.SetValidCommand();
            var expected = string.Format(Constants.RegisterConditioner, "LXAA", "Siemens");

            // Act
            var actual = this.controller.ExecuteCommand(this.validCommand);
            Assert.AreEqual(expected, actual, "Registration is not successful");
        }

        private Command SetValidCommand()
        {
            var command = new Command
                              {
                                  Name = "RegisterStationaryAirConditioner", 
                                  Parameters = new[] { "Siemens", "LXAA", "A", "1200" }
                              };

            return command;
        }
    }
}