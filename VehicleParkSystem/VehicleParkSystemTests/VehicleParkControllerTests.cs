namespace VehicleParkSystemTests
{
    using System;
    using System.Linq;
    using System.Net;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    using VehicleParkSystem;
    using VehicleParkSystem.Contracts;
    using VehicleParkSystem.Core;
    using VehicleParkSystem.Models;

    [TestClass]
    public class VehicleParkControllerTests
    {
        [TestMethod]
        public void InserCar_NotValidPlace_ReturnsCorrectMessage()
        {
            // arrange
            int sectors = 10;
            int places = 10;
            var controller = new VehicleParkController(sectors, places);
            var car = new Car("AA1111A", "Vaskata", 2);
            var expected = string.Format("There is no place {0} in sector {1}", 14, 3);

            // act
            string actualResult = controller.InsertVehicle(car, 3, 14, DateTime.Now);

            // assert
            Assert.AreEqual(expected, actualResult);
        }

        [TestMethod]
        public void InserCar_NotValidSector_ReturnsCorrectMessage()
        {
            // arrange
            int sectors = 10;
            int places = 10;
            var controller = new VehicleParkController(sectors, places);
            var car = new Car("AA1111A", "Vaskata", 2);
            var expected = string.Format("There is no sector {0} in the park", 11);

            // act
            string actualResult = controller.InsertVehicle(car, 11, 4, DateTime.Now);

            // assert
            Assert.AreEqual(expected, actualResult);
        }

        [TestMethod]
        public void InserCar_OccupiedPlace_ReturnsCorrectMessage()
        {
            // arrange
            int sectors = 10;
            int places = 10;
            var layout = new Layout(sectors, places);
            var car = new Car("AA1111A", "Vaskata", 2);
            var mockedData = new Mock<IVehicleParkData>();
            mockedData.Setup(d => d.VehiclesByPlace.ContainsKey(It.IsAny<string>())).Returns(true);

            var controller = new VehicleParkController(layout, mockedData.Object);

            var expected = string.Format("The place ({0},{1}) is occupied", 1, 1);

            // act
            var actual = controller.InsertVehicle(car, 1, 1, DateTime.Now);

            // assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void InserCar_ParkedVehicle_ReturnsCorrectMessage()
        {
            // arrange
            int sectors = 10;
            int places = 10;
            var layout = new Layout(sectors, places);
            var car = new Car("AA1111A", "Vaskata", 2);
            var mockedData = new Mock<IVehicleParkData>();
            mockedData.Setup(d => d.VehiclesByPlace.ContainsKey(It.IsAny<string>())).Returns(false);
            mockedData.Setup(d => d.VehiclesByLincensePlate.ContainsKey(It.IsAny<string>())).Returns(true);

            var controller = new VehicleParkController(layout, mockedData.Object);

            var expected = string.Format(
                "There is already a vehicle with license plate {0} in the park",
                car.LicensePlate);

            // act
            var actual = controller.InsertVehicle(car, 1, 1, DateTime.Now);

            // assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void InserCar_FreePlace_ReturnsCorrectMessage()
        {
            // arrange
            int sectors = 10;
            int places = 10;
            var layout = new Layout(sectors, places);
            var car = new Car("AA1111A", "Vaskata", 2);
            var mockedData = new Mock<IVehicleParkData>();
            mockedData.Setup(d => d.VehiclesByPlace.ContainsKey(It.IsAny<string>())).Returns(false);
            mockedData.Setup(d => d.VehiclesByLincensePlate.ContainsKey(It.IsAny<string>())).Returns(false);

            var controller = new VehicleParkController(layout, mockedData.Object);

            var expected = "Car parked successfully at place (1,1)";


            // act
            var actual = controller.InsertVehicle(car, 1, 1, DateTime.Now);

            // assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ExitVehicle_NonExistingVehicle_RightMessage()
        {
            var mockedData = new Mock<IVehicleParkData>();
            mockedData.Setup(d => d.VehiclesByLincensePlate.ContainsKey(It.IsAny<string>())).Returns(false);
            var controller = new VehicleParkController(new Layout(10, 10), mockedData.Object);
            var truck = new Truck("AA3333A", "Mee", 3);
            var expected = string.Format("There is no vehicle with license plate {0} in the park", truck.LicensePlate);
            // act
            var actual = controller.ExitVehicle("AA3333A", DateTime.Now, 190);
            // assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ExitVehicle_ExistingVehicle_RightMessage()
        {
            var mockedData = new Mock<IVehicleParkData>();
            mockedData.Setup(d => d.VehiclesByLincensePlate.ContainsKey(It.IsAny<string>())).Returns(true);
            var controller = new VehicleParkController(new Layout(10, 10), mockedData.Object);
            var truck = new Truck("AA3333A", "Mee", 3);
            mockedData.Setup(d => d.VehiclesByLincensePlate[It.IsAny<string>()]).Returns(truck);

        }

        [TestMethod]
        public void GetStatus_NoVehciles_RetursRghtMessge()
        {

            var mockedData = new Mock<IVehicleParkData>();
            int[] places = new int[10];
            mockedData.Setup(d => d.OccupiedPlacesBySector).Returns(places);
            var controller = new VehicleParkController(new Layout(10, 10), mockedData.Object);
            var formated = places.Select(
                    (s, i) =>
                    string.Format(
                        "Sector {0}: {1} / {2} ({3}% full)",
                        i + 1,
                        s,
                        10,
                       Math.Ceiling((decimal)s / 10 * 100)));

            var expected = string.Join(Environment.NewLine, formated);
            string actual = controller.GetStatus();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetStatus_SeveralVehciles_RetursRghtMessge()
        {
            int sectors = 10;
            var mockedData = new Mock<IVehicleParkData>();
            int[] places = { 1, 3, 4, 5, 5, 5, 10, 9, 0, 0 };
            mockedData.Setup(d => d.OccupiedPlacesBySector).Returns(places);
            var controller = new VehicleParkController(new Layout(sectors, 10), mockedData.Object);
            var formated = places.Select(
                    (s, i) =>
                    string.Format(
                        "Sector {0}: {1} / {2} ({3}% full)",
                        i + 1,
                        s,
                        10,
                       Math.Ceiling((decimal)s / 10 * 100)));

            var expected = string.Join(Environment.NewLine, formated);
            string actual = controller.GetStatus();

            Assert.AreEqual(expected, actual);
        }
    }
}