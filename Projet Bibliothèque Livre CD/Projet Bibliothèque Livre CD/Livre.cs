using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_Bibliothèque_Livre_CD
{
    class Livre
    {
        public string NumeroISBN { get; set; }
        public string AuteurDuLivre { get; set; }
        public enum GenreDuLivre {Drame, Policier, Culturel, Religieux};

        Livre(string numeroISBN, string auteurDuLivre)
        {
            NumeroISBN = numeroISBN;
            AuteurDuLivre = auteurDuLivre;
        }
    }
}