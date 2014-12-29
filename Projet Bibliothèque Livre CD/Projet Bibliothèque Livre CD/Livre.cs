using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_Bibliothèque_Livre_CD
{
    public enum GenreDuLivre {NonDéfini, Drame, Policier, Culturel, Religieux};

    public class Livre : ContenuMultimedia
    {
        public string NumeroISBN { get; set; }
        public string AuteurDuLivre { get; set; }

        public GenreDuLivre Genre { get; set; }

        public Livre(int id, string titre, int nombreEnStock, string numeroISBN, string auteurDuLivre, GenreDuLivre genre)
            : base(id, titre, nombreEnStock)
        {
            NumeroISBN = numeroISBN;
            AuteurDuLivre = auteurDuLivre;
            Genre = genre;
        }

        /// <summary>
        /// Constructeur de copie
        /// </summary>
        /// <param name="livre"></param>
        public Livre(Livre livre)
            : base(livre.IdentifiantUnique, livre.Titre, livre.NombreEnStock)
        {
            NumeroISBN = livre.NumeroISBN;
            AuteurDuLivre = livre.AuteurDuLivre;
            Genre = livre.Genre;
        }

        public override string ToString()
        {
            return "Titre : " + Titre + ", Nombre : " + NombreEnStock + ", Auteur : " + AuteurDuLivre +
                   ", Genre : " + Genre.ToString();
        }

        public static string[] getGenresDisponibles()
        {
            return Enum.GetNames(typeof(GenreDuLivre));
        }
    }
}