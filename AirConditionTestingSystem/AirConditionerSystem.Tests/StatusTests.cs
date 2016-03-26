namespace AirConditionerSystem.Tests
{
    using AirConditionerTestingSystem.Core;
    using AirConditionerTestingSystem.Models;
    using AirConditionerTestingSystem.Utilities;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class StatusTests
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
        public void Status_WithNoTests_ReturnsNoWorkDone()
        {
            // Arrange
            int count = 5;
            this.RegisterAirConditioners(5);
            var expected = string.Format(Constants.Status, 0);

            // Act
            var actual = this.controller.ExecuteCommand(this.validCommand);

            // Assert
            Assert.AreEqual(expected, actual, "Status with no testing does not work");
        }

        public void Status_WithSeveralTests_ReturnsCOrrectWorkDone()
        {
            // Arrange
            int count = 5;
            this.RegisterAirConditioners(5);
            this.controller.ExecuteCommand(
                new Command { Name = "TestAirConditioner", Parameters = new[] { "Toshiba", "LXAA1" } });
            this.controller.ExecuteCommand(
                new Command { Name = "TestAirConditioner", Parameters = new[] { "Toshiba", "LXAA2" } });

            var expected = string.Format(Constants.Status, 40);

            // Act
            var actual = this.controller.ExecuteCommand(this.validCommand);

            // Assert
            Assert.AreEqual(expected, actual, "Status  does not work");
        }

        private void RegisterAirConditioners(int count)
        {
            for (int i = 0; i < count; i++)
            {
                this.controller.ExecuteCommand(
                    new Command
                        {
                            Name = "RegisterStationaryAirConditioner", 
                            Parameters = new[] { "Siemens", "LXAA" + i, "A", "1200" }
                        });
            }
        }

        private Command SetValidCommand()
        {
            return new Command { Name = "Status", Parameters = new string[0] };
        }
    }
}