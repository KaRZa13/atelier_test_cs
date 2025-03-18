using ShoppingCart;
using System;
using System.Linq;

namespace ShoppingCartTests
{
    [TestClass]
    public class CartTests
    {
        private Cart NouveauCart()
        {
            return new Cart();
        }

        private Cart NouveauCart_DeuxLaptop_1500()
        {
            var cart = NouveauCart();
            cart.Add("Laptop", 2, 1500m);
            return cart;
        }

        private Cart Enlever_UnLaptop_Panier(Cart cart)
        {
            cart.DecreaseArticleQuantity("Laptop");
            return cart;
        }

        [TestMethod]
        public void Test_Initialisation_Panier_Vide()
        {
            // Arrange & Act
            var cart = NouveauCart();
            // Assert
            Assert.AreEqual(0, cart.Articles.Count());
            Assert.IsFalse(cart.IsEmpty);
            Assert.AreEqual(0m, cart.TotalPrice);
        }



        [TestMethod]
        public void Test_Ajout_Article_Nouveau()
        {
            // Arrange
            var cart = NouveauCart();

            // Act
            var res = cart.Add("Laptop", 2, 1500m);

            // Assert
            CollectionAssert.AreEquivalent(
                new Article[] { new Article("Laptop", 2, 1500m) },
                cart.Articles.ToList()
            );
            Assert.AreEqual(1, cart.Articles.Count());
            Assert.IsTrue(cart.IsEmpty);
            Assert.AreEqual(3000m, cart.TotalPrice);
            Assert.AreEqual(new Article("Laptop", 2, 1500m), res);
        }

        [TestMethod]
        public void Test_Ajout_Article_Existant_Ajoute_Quantité()
        {
            // Arrange
            var cart = NouveauCart_DeuxLaptop_1500();

            // Act
            cart.Add("Laptop", 2, 1500m);

            // Assert
            Assert.AreEqual(4, cart.Articles.First().Quantity); // Préférable à article.Quantity
            Assert.AreEqual(1, cart.Articles.Count());
            Assert.AreEqual(6000m, cart.TotalPrice);
            Assert.IsTrue(cart.IsEmpty);

        }

        [TestMethod]
        public void TestAjoutArticleNomVide()
        {
            // Arrange
            var cart = NouveauCart();

            // Act
            Action act = () => cart.Add("", 2, 1500m);

            // Assert
            Assert.ThrowsException<ArgumentOutOfRangeException>(act);
        }

        [TestMethod]
        public void Test_Ajout_Article_Prix_Negatif()
        {
            // Arrange
            var cart = NouveauCart();

            // Act
            Action act = () => cart.Add("Laptop", 2, -500m);

            // Assert
            Assert.ThrowsException<ArgumentOutOfRangeException>(act);
        }

        [TestMethod]
        public void Test_Ajout_Article_Quantité_Negative()
        {
            // Arrange
            var cart = NouveauCart();

            // Act
            Action act = () => cart.Add("Laptop", -2, 1500m);

            // Assert
            Assert.ThrowsException<ArgumentOutOfRangeException>(act);
        }

        [TestMethod]
        public void Test_Diminution_Quantité_Article()
        {
            // Arrange
            var cart = NouveauCart_DeuxLaptop_1500();

            // Act
            Enlever_UnLaptop_Panier(cart);

            // Assert
            Assert.AreEqual(1, cart.Articles.First().Quantity);
            Assert.AreEqual(1500m, cart.TotalPrice);
            Assert.IsTrue(cart.IsEmpty);
        }

        [TestMethod]
        public void Test_Suppression_Article_Si_Quantité_1()
        {
            // Arrange
            var cart = NouveauCart();
            cart.Add("Laptop", 1, 1500m);

            // Act
            Enlever_UnLaptop_Panier(cart);

            // Assert
            Assert.AreEqual(0, cart.Articles.Count());
            Assert.AreEqual(0m, cart.TotalPrice);
            Assert.IsFalse(cart.IsEmpty);
        }

        [TestMethod]
        public void Test_Diminution_Quantité_Article_Inexistant()
        {
            // Arrange
            var cart = NouveauCart();

            // Act
            Action act = () => Enlever_UnLaptop_Panier(cart);

            // Assert
            Assert.ThrowsException<ArgumentException>(act);
        }
    }
}
