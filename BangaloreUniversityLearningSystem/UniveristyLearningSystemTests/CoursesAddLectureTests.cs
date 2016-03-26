namespace UniveristyLearningSystemTests
{
    using System;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    using UniversityLearningSystem;
    using UniversityLearningSystem.Controllers;
    using UniversityLearningSystem.Data;
    using UniversityLearningSystem.Interfaces;
    using UniversityLearningSystem.Models;

    [TestClass]
    public class CoursesAddLectureTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void NotLecturer_ThrowsException()
        {
            User user = new User("Pesho", "Pesho", Role.Student);
            var controller = new CoursesController(new BangaloreUniversityData(), user);

            controller.AddLecture(1, "Pesho");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void NotLoggedInUser_ThrowsException()
        {
            User user = null;
            var controller = new CoursesController(new BangaloreUniversityData(), user);

            controller.AddLecture(1, "Pesho");
        }

        [TestMethod]
        public void ValidUser_AddCourse()
        {
            var course = new Course("OOP");
            var user = new User("Pesho", "Pesho", Role.Lecturer);
            var mockedCourses = new Mock<IRepository<Course>>();
            mockedCourses.Setup(c => c.Get(It.IsAny<int>())).Returns(course);
            var data = new BangaloreUniversityData(new UsersRepository(), mockedCourses.Object);

            var controller = new CoursesController(data, user);
            var view = controller.AddLecture(1, "Design Patterns");
            string actual = view.Display();

            string expected = "Lecture successfully added to course .";

            Assert.AreEqual(expected, actual);
        }
    }
}