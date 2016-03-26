namespace UniveristyLearningSystemTests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using UniversityLearningSystem;
    using UniversityLearningSystem.Controllers;
    using UniversityLearningSystem.Data;

    [TestClass]
    public class DisplayTestMethodTests
    {
        [TestMethod]
        public void LogoutDisplay_SHoudlRerturnCorrectMessage()
        {
            // arrange
            var user = new User("Dragan", "Dragan", Role.Lecturer);
            var controller = new UsersController(new BangaloreUniversityData(), user);
            var expected = string.Format("User {0} logged out successfully.", user.Username);

            // act
            var view = controller.Logout();
            string actual = view.Display();

            // Assert
            Assert.AreEqual(expected, actual);
        }
    }
}