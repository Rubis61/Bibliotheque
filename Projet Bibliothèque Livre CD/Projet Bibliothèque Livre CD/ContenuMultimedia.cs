using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_Bibliothèque_Livre_CD
{
    class ContenuMultimedia
    {
        public int IdentifiantUnique { get; set; }
        public string TitreAlbum { get; set; }
        public int NombreEnStock { get; set; }

        public ContenuMultimedia(int identifiantUnique, string titreAlbum, int nombreEnStock)
        {
            IdentifiantUnique = identifiantUnique;
            TitreAlbum = titreAlbum;
            NombreEnStock = nombreEnStock;
        }
    }
}
