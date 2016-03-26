namespace AirConditionerSystem.Tests
{
    using System.Text;

    using AirConditionerTestingSystem.Core;
    using AirConditionerTestingSystem.Models;
    using AirConditionerTestingSystem.Utilities;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class FindAllReportsByManufacturerTests
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
        public void Test_WhenNoReportsAdded_ShouldReturnValidResult()
        {
            // Act
            var result = this.controller.ExecuteCommand(this.validCommand);

            // Assert
            Assert.AreEqual(Constants.NoReports, result, "When no reports, does not work correctly");
        }

        [TestMethod]
        public void Test_WhenOneReportAdded_ShouldReturnValidFormat()
        {
            // Arrange
            var registration = new Command
                                   {
                                       Name = "RegisterStationaryAirConditioner", 
                                       Parameters = new[] { "Toshiba", "LXAA", "A", "999" }
                                   };
            var testing = new Command { Name = "TestAirConditioner", Parameters = new[] { "Toshiba", "LXAA" } };
            this.controller.ExecuteCommand(registration);
            this.controller.ExecuteCommand(testing);
            var expected = new StringBuilder();
            expected.AppendLine("Reports from Toshiba:");
            expected.AppendLine("Report")
                .AppendLine("====================")
                .AppendFormat("Manufacturer: {0}", "Toshiba")
                .AppendLine()
                .AppendFormat("Model: {0}", "LXAA")
                .AppendLine()
                .AppendFormat("Mark: {0}", "Passed")
                .AppendLine()
                .Append("====================");

            // Act
            var getReports = this.controller.ExecuteCommand(this.validCommand);

            // Assert
            Assert.AreEqual(expected.ToString(), getReports, "When no reports, does not work correctly");
        }

        private Command SetValidCommand()
        {
            var command = new Command { Name = "FindAllReportsByManufacturer", Parameters = new[] { "Toshiba" } };

            return command;
        }
    }
}