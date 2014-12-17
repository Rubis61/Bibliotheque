using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_Bibliothèque_Livre_CD
{
    public enum GenreDuLivre {Drame, Policier, Culturel, Religieux};

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

        public static string[] getGenresDisponibles()
        {
            return Enum.GetNames(typeof(GenreDuLivre));
        }
    }
}