namespace UniveristyLearningSystemTests
{
    using System.Collections.Generic;
    using System.Text;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    using UniversityLearningSystem.Controllers;
    using UniversityLearningSystem.Data;
    using UniversityLearningSystem.Interfaces;
    using UniversityLearningSystem.Models;

    [TestClass]
    public class CoursesAllTests
    {
        [TestMethod]
        public void AnyCourses_RetursnRightMessage()
        {
            var expectedMessage = new StringBuilder();

            expectedMessage.AppendLine("OOP").AppendLine("AdvancedC#").AppendLine("HQ Code");
            var mockedCourseRepo = new Mock<IRepository<Course>>();
            mockedCourseRepo.Setup(r => r.GetAll()).Returns(new List<Course>());
            var database = new BangaloreUniversityData(new UsersRepository(), mockedCourseRepo.Object);
            var controller = new CoursesController(database, null);

            var view = controller.All();

            var actualMessage = view.Display();

            Assert.AreEqual(expectedMessage.ToString(), actualMessage);
        }

        [TestMethod]
        public void NoCOursses_RetursnRightMessage()
        {
            var expectedMessage = "No courses.";
            var mockedCourseRepo = new Mock<IRepository<Course>>();
            mockedCourseRepo.Setup(r => r.GetAll()).Returns(new List<Course>());
            var database = new BangaloreUniversityData(new UsersRepository(), mockedCourseRepo.Object);
            var controller = new CoursesController(database, null);

            var view = controller.All();

            var actualMessage = view.Display();

            Assert.AreEqual(expectedMessage, actualMessage);
        }
    }
}