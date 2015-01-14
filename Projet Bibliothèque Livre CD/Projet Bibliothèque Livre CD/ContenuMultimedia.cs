using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_Bibliothèque_Livre_CD
{
    public class ContenuMultimedia
    {
        public int IdentifiantUnique { get; set; }
        public string Titre { get; set; }
        public int NombreEnStock { get; set; }

        public ContenuMultimedia(int identifiantUnique, string titre, int nombreEnStock)
        {
            IdentifiantUnique = identifiantUnique;
            Titre = titre;
            NombreEnStock = nombreEnStock;
        }
    }
}