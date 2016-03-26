namespace BuhtigIssueTrackerTests
{
    using System;
    using System.Collections.Generic;

    using BuhtigIssueTracker.Contracts;
    using BuhtigIssueTracker.Core;
    using BuhtigIssueTracker.Enum;
    using BuhtigIssueTracker.Models;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public class IssueTrackerControllerTests
    {
        [TestMethod]
        public void CreateIssue_CorrectInput_RightMessage()
        {
            var loggedUser = new User("pesho", "gosho");
            var data = new BuhtigIssueTrackerData();
            data.LoggedUser = loggedUser;
            var controller = new IssueTrackerController(data);
            var expected = "Issue 1 created successfully";

            var actual = controller.CreateIssue("problem", "great problem", IssuePriority.High, new[] { "trouble" });

            Assert.AreEqual(actual, expected);
        }

        [TestMethod]
        public void CreateIssue_NotLoggedUser_ReturnsRighMessage()
        {
            var mockedData = new Mock<IBuhtigIssueTrackerData>();
            mockedData.Setup(d => d.LoggedUser).Returns((User)null);
            var controller = new IssueTrackerController(mockedData.Object);
            var expected = "There is no currently logged in user";

            var actual = controller.CreateIssue("pro", "trouble", IssuePriority.High, new[] { "trouble", "sos" });

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CreateIssue_ShortDescription_ThrowsException()
        {
            var loggedUser = new User("pesho", "gosho");
            var mockedData = new Mock<IBuhtigIssueTrackerData>();
            mockedData.Setup(d => d.LoggedUser).Returns(loggedUser);
            var controller = new IssueTrackerController(mockedData.Object);
            controller.CreateIssue("problem", "grea", IssuePriority.High, new[] { "pr" });
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CreateIssue_ShortName_ThrowsException()
        {
            var loggedUser = new User("pesho", "gosho");
            var mockedData = new Mock<IBuhtigIssueTrackerData>();
            mockedData.Setup(d => d.LoggedUser).Returns(loggedUser);
            var controller = new IssueTrackerController(mockedData.Object);
            controller.CreateIssue("pr", "great problem", IssuePriority.High, new[] { "pr" });
        }

        [TestMethod]
        public void GetMyIssues_NoIssues_RightMessage()
        {
            var controller = new IssueTrackerController();
            controller.Data.LoggedUser = new User("pesho", "gosho");
            var actual = controller.GetMyIssues();
            var expected = "No issues";

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetMyIssues_NoLoggedUser_RightMessage()
        {
            var controller = new IssueTrackerController();
            controller.Data.LoggedUser = null;

            var actual = controller.GetMyIssues();
            var expected = "There is no currently logged in user";

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetMyIssues_TwoIssuesUser_RightMessage()
        {
            var issue1 = new Issue("title1", "descripiton1", IssuePriority.High, new List<string> { "drundrun" });
            var loggedUser = new User("pesho", "gosho");

            var data = new BuhtigIssueTrackerData();
            data.LoggedUser = loggedUser;
            data.AddIssue(issue1);
            var controller = new IssueTrackerController(data);
            var expetected = issue1.ToString();

            var actual = controller.GetMyIssues();

            Assert.AreEqual(expetected, actual);
        }

        [TestMethod]
        public void RegisterUser_AlreadyLoggedUser_ReturnsRightMessage()
        {
            var loggedUser = new User("pesho", "gosho");
            var mockedData = new Mock<IBuhtigIssueTrackerData>();
            mockedData.Setup(d => d.LoggedUser).Returns(loggedUser);
            var controller = new IssueTrackerController(mockedData.Object);
            var expected = "There is already a logged in user";
            var actualMessage = controller.RegisterUser("ivan", "Dragan", "Dragan");

            Assert.AreEqual(expected, actualMessage);
        }

        [TestMethod]
        public void RegisterUser_CorrectInput_ReturnsRighMessage()
        {
            var mockedData = new Mock<IBuhtigIssueTrackerData>();
            mockedData.Setup(d => d.LoggedUser).Returns((User)null);
            var controller = new IssueTrackerController(mockedData.Object);
            mockedData.Setup(d => d.UsersByUsername.ContainsKey(It.IsAny<string>())).Returns(false);

            var expectedMessage = "User Mimi registered successfully";
            var actual = controller.RegisterUser("Mimi", "pp", "pp");

            Assert.AreEqual(expectedMessage, actual);
        }

        [TestMethod]
        public void RegisterUser_ExistingUsername_ReturnsRighMessage()
        {
            var mockedData = new Mock<IBuhtigIssueTrackerData>();
            mockedData.Setup(d => d.LoggedUser).Returns((User)null);
            var controller = new IssueTrackerController(mockedData.Object);
            mockedData.Setup(d => d.UsersByUsername.ContainsKey(It.IsAny<string>())).Returns(true);

            var expectedMessage = "A user with username Mimi already exists";
            var actual = controller.RegisterUser("Mimi", "pp", "pp");

            Assert.AreEqual(expectedMessage, actual);
        }

        [TestMethod]
        public void RegisterUser_NotMachingPassowords_ReturnsRighMessage()
        {
            var mockedData = new Mock<IBuhtigIssueTrackerData>();
            mockedData.Setup(d => d.LoggedUser).Returns((User)null);
            var controller = new IssueTrackerController(mockedData.Object);
            mockedData.Setup(d => d.UsersByUsername.ContainsKey(It.IsAny<string>())).Returns(false);

            var expectedMessage = "The provided passwords do not match";
            var actual = controller.RegisterUser("Mimi", "p", "pp");

            Assert.AreEqual(expectedMessage, actual);
        }

        [TestMethod]
        public void SearchIssue_NotTag_RetursnRughtMessage()
        {
            // TODO 
        }
    }
}