namespace UniveristyLearningSystemTests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using UniversityLearningSystem.Data;

    [TestClass]
    public class UserRepositoryTests
    {
        [TestMethod]
        public void NotValidId_ReturnDefault()
        {
            // Arrange
            var repositoryInt = new Repository<int>();
            repositoryInt.Add(1);
            repositoryInt.Add(2);
            repositoryInt.Add(3);

            // Act
            int actualValue = repositoryInt.Get(5);

            // Assert
            Assert.AreEqual(0, actualValue);
        }

        [TestMethod]
        public void NotValidIdReferenceType_ReturnNull()
        {
            // Arrange
            var repositoryString = new Repository<string>();
            repositoryString.Add("one");
            repositoryString.Add("two");
            repositoryString.Add("three");

            // Act
            string actualValue = repositoryString.Get(5);

            // Assert
            Assert.IsNull(actualValue);
        }

        [TestMethod]
        public void ValidId_ReturnCorrectValue()
        {
            // Arrange
            var repositoryInt = new Repository<int>();
            repositoryInt.Add(1);
            repositoryInt.Add(2);
            repositoryInt.Add(3);

            // Act
            int actualValue = repositoryInt.Get(2);

            // Assert
            Assert.AreEqual(2, actualValue);
        }

        [TestMethod]
        public void ValidIdReferenceType_ReturnCorrectValue()
        {
            // Arrange
            var repositoryInt = new Repository<string>();
            repositoryInt.Add("one");
            repositoryInt.Add("two");
            repositoryInt.Add("three");

            // Act
            string actualValue = repositoryInt.Get(2);

            // Assert
            Assert.AreEqual("two", actualValue);
        }
    }
}