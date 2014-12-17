using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_Bibliothèque_Livre_CD
{
    class Bibliotheque
    {
        public List<CD> ListCD { get; set; }
        public List<Livre> ListLivres { get; set; }

        public Bibliotheque(List<CD> listCD, List<Livre> listLivres)
        {
            ListCD = listCD;
            ListLivres = listLivres;
        }

        public void ajouterCD(CD cd)
        {
            ListCD.Add(cd);
        }

        public void ajouterLivre(Livre livre)
        {
            ListLivres.Add(livre);
        }

        public CD rechercherCD(string titre)
        {
            return ListCD.Single(cd => cd.Titre == titre);
        }

        public Livre rechercherLivre(string titre)
        {
            return ListLivres.Single(livre => livre.Titre == titre);
        }
    }
}
