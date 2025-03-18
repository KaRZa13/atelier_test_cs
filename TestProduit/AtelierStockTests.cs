using AtelierStock;

namespace AtelierStockTests
{
    [TestClass]
    public class ProduitTests
    {
        [TestMethod]
        public void Test_Initialisation_Des_Prix()
        {
            // Arrange
            var produit = new Produit("REF123", "Produit Test", 100m, 20m);

            // Act
            decimal prixVente = produit.PrixVente;

            // Assert
            Assert.AreEqual(120m, prixVente, "Le prix de vente doit être prixAchat * (1 + pourcentageMarge / 100).");
        }

        [TestMethod]
        public void Test_Initialisation_Stock_A_Zero()
        {
            // Arrange
            var produit = new Produit("REF123", "Produit Test", 50m, 10m);

            // Act & Assert
            Assert.AreEqual(0, produit.Stocks, "Le stock initial doit être 0.");
        }

        [TestMethod]
        public void Test_Ajouter_Produits()
        {
            // Arrange
            var produit = new Produit("REF123", "Produit Test", 50m, 10m);

            // Act
            produit.Rentrer(10);

            // Assert
            Assert.AreEqual(10, produit.Stocks, "Le stock après ajout doit être correct.");
        }

        [TestMethod]
        public void Test_Ajouter_0_Produits()
        {
            // Arrange
            var produit = new Produit("REF123", "Produit Test", 50m, 10m);

            // Act
            produit.Rentrer(0);

            // Assert
            Assert.AreEqual(0, produit.Stocks, "Ajouter 0 produit ne doit pas modifier le stock.");
        }

        [TestMethod]
        public void Test_Retirer_Produits()
        {
            // Arrange
            var produit = new Produit("REF123", "Produit Test", 50m, 10m);
            produit.Rentrer(10);

            // Act
            int retiré = produit.Sortir(5);

            // Assert
            Assert.AreEqual(5, retiré, "Le nombre de produits retirés doit être correct.");
            Assert.AreEqual(5, produit.Stocks, "Le stock après retrait doit être correct.");
        }

        [TestMethod]
        public void Test_Retirer_0_Produits()
        {
            // Arrange
            var produit = new Produit("REF123", "Produit Test", 50m, 10m);
            produit.Rentrer(10);

            // Act
            int retiré = produit.Sortir(0);

            // Assert
            Assert.AreEqual(0, retiré, "Retirer 0 produit ne doit rien changer.");
            Assert.AreEqual(10, produit.Stocks, "Le stock ne doit pas être modifié.");
        }

        [TestMethod]
        public void Test_Retirer_Plus_Que_Stock_Disponible()
        {
            // Arrange
            var produit = new Produit("REF123", "Produit Test", 50m, 10m);
            produit.Rentrer(5);

            // Act
            int retiré = produit.Sortir(10);

            // Assert
            Assert.AreEqual(5, retiré, "On ne doit retirer que le stock disponible.");
            Assert.AreEqual(0, produit.Stocks, "Le stock ne doit pas devenir négatif.");
        }

        [TestMethod]
        public void Test_Ajouter_Produits_Nombre_Négatif()
        {
            // Arrange
            var produit = new Produit("REF123", "Produit Test", 50m, 10m);

            // Act
            Action act = () => produit.Rentrer(-10);

            // Assert
            var ex = Assert.ThrowsException<ArgumentException>(act);
            Assert.AreEqual("La quantité à ajouter ne peut pas être négative.", ex.Message);
        }

        [TestMethod]
        public void Test_Retirer_Produits_Nombre_Négatif()
        {
            // Arrange
            var produit = new Produit("REF123", "Produit Test", 50m, 10m);

            // Act
            Action act = () => produit.Sortir(-10);

            // Assert
            var ex = Assert.ThrowsException<ArgumentException>(act);
            Assert.AreEqual("La quantité à retirer ne peut pas être négative.", ex.Message);
        }

        [TestMethod]
        public void Test_Rupture_De_Stock_Initial()
        {
            // Arrange
            var produit = new Produit("REF123", "Produit Test", 50m, 10m);

            // Act & Assert
            Assert.IsTrue(produit.EstEnRupture, "Un produit doit être en rupture de stock si son stock est 0.");
        }

        [TestMethod]
        public void Test_Pas_De_Rupture_Après_Ajout()
        {
            // Arrange
            var produit = new Produit("REF123", "Produit Test", 50m, 10m);
            produit.Rentrer(10);

            // Act & Assert
            Assert.IsFalse(produit.EstEnRupture, "Un produit avec du stock ne doit pas être en rupture.");
        }

        [TestMethod]
        public void Test_Rupture_Après_Retrait_Total()
        {
            // Arrange
            var produit = new Produit("REF123", "Produit Test", 50m, 10m);
            produit.Rentrer(5);

            // Act
            produit.Sortir(5);

            // Assert
            Assert.IsTrue(produit.EstEnRupture, "Un produit dont le stock est 0 doit être en rupture.");
        }
    }
}
