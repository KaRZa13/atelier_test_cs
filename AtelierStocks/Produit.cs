namespace AtelierStock
{
    public class Produit
    {
        private decimal prixAchat, pourcentageMarge;
        private int stocks;
        private string reference, libelle;

        public Produit(string reference, string libelle, decimal prixAchat, decimal pourcentageMarge)
        {
            this.reference = reference;
            this.libelle = libelle;
            this.prixAchat = prixAchat;
            this.pourcentageMarge = pourcentageMarge;
            stocks = 0;
        }
        #region Accesseurs
        public string Reference => reference;
        public string Libelle => libelle;
        public int Stocks => stocks; // { get { return stocks; } }
        public decimal PrixVente => prixAchat * (1 + pourcentageMarge / 100);
        public decimal PrixAchat => prixAchat;
        #endregion

        #region Stocks

        /// <summary>
        /// Sort la quantité spécifiée des stocks pour le produit concerné. Prend en compte la rupture de stock.
        /// </summary>
        /// <param name="quantite">Quantité à retirer</param>
        /// <returns>Valeur réellement retirée inférieure (rupture) ou égale à la quantité</returns>
        public int Sortir(int quantite)
        {
            if (quantite < 0)
                throw new ArgumentException("La quantité à retirer ne peut pas être négative.");

            int retiré = Math.Min(quantite, stocks);
            stocks -= retiré;
            return retiré;
        }

        public void Rentrer(int quantite)
        {
            if (quantite < 0)
                throw new ArgumentException("La quantité à ajouter ne peut pas être négative.");

            stocks += quantite;
        }

        public bool EstEnRupture => stocks == 0;

        #endregion

    }
}
