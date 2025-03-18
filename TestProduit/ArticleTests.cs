using ShoppingCart;
using System;

namespace ShoppingCartTests
{
    [TestClass]
    public class ArticleTests
    {
        [TestMethod]
        public void Test_Initialisation_Article_Valide()
        {
            // Arrange & Act
            var article = new Article("Laptop", 2, 1500m);

            // Assert
            Assert.AreEqual("Laptop", article.ProductName);
            Assert.AreEqual(2, article.Quantity);
            Assert.AreEqual(1500m, article.Price);
            Assert.AreEqual(2 * 1500m, article.TotalPrice);
        }

        [TestMethod]
        public void Test_Initialisation_Prix_Negatif()
        {
            // Arrange & Act
            Action act = () => new Article("Laptop", 2, -1500m);

            // Assert
            Assert.ThrowsException<ArgumentOutOfRangeException>(act);
        }

        [TestMethod]
        public void Test_Initialisation_Prix_Zero()
        {
            // Arrange & Act
            Action act = () => new Article("Laptop", 2, 0m);

            // Assert
            Assert.ThrowsException<ArgumentOutOfRangeException>(act);
        }

        [TestMethod]
        public void Test_Initialisation_Quantite_Negative()
        {
            // Arrange & Act
            Action act = () => new Article("Laptop", -1, 1500m);

            // Assert
            Assert.ThrowsException<ArgumentOutOfRangeException>(act);
        }

        [TestMethod]
        public void Test_Initialisation_Quantite_Zero()
        {
            // Arrange & Act
            Action act = () => new Article("Laptop", 0, 1500m);

            // Assert
            Assert.ThrowsException<ArgumentOutOfRangeException>(act);
        }

        [TestMethod]
        public void Test_Modification_Quantite_Valide()
        {
            // Arrange
            var article = new Article("Laptop", 2, 1500m);

            // Act
            article.Quantity = 5;

            // Assert
            Assert.AreEqual(5, article.Quantity);
            Assert.AreEqual(5 * 1500m, article.TotalPrice);
        }

        [TestMethod]
        public void Test_Modification_Quantite_Negative()
        {
            // Arrange
            var article = new Article("Laptop", 2, 1500m);

            // Act
            Action act = () => article.Quantity = -3;

            // Assert
            Assert.ThrowsException<ArgumentOutOfRangeException>(act);
        }

        [TestMethod]
        public void Test_Modification_Quantite_Zero()
        {
            // Arrange
            var article = new Article("Laptop", 2, 1500m);

            // Act
            Action act = () => article.Quantity = 0;

            // Assert
            Assert.ThrowsException<ArgumentOutOfRangeException>(act);
        }

        [TestMethod]
        public void Test_Articles_Egaux()
        {
            // Arrange
            var article1 = new Article("Laptop", 2, 1500m);
            var article2 = new Article("Laptop", 2, 1500m);

            // Act & Assert
            Assert.AreEqual(article1, article2);
        }

        [TestMethod]
        public void Test_Articles_Differents()
        {
            // Arrange
            var article1 = new Article("Laptop", 2, 1500m);
            var article2 = new Article("Mouse", 2, 50m);

            // Act & Assert
            Assert.AreNotEqual(article1, article2);
        }
    }
}