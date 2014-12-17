using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_Bibliothèque_Livre_CD
{
    class Musique
    {
        public int Numero { get; set; }
        public string Titre { get; set; }

        public Musique(string titre, int numéro)
        {
            Numero = numéro;
            Titre = titre;
        }
    }
}
