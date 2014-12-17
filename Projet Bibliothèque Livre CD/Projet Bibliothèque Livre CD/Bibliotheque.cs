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

        public Bibliotheque()
        {
            ListCD = new List<CD>();
            ListLivres = new List<Livre>();
        }

        public Bibliotheque(List<CD> listCD, List<Livre> listLivres)
        {
            ListCD = listCD;
            ListLivres = listLivres;
        }

        public void ajouterCD(CD cd)
        {
            ListCD.Add(cd);
        }

        public void ajouterLivre(string titre, string numeroISBN, string auteurDuLivre, GenreDuLivre genre)
        {
            int idNouveauLivre = ListLivres.Count;
            Livre nouveauLivre = new Livre(idNouveauLivre, titre, 1, numeroISBN,auteurDuLivre,genre);

            if( ListLivres.Where(livre => livre.Titre == nouveauLivre.Titre).Count() >= 1 )
            {
                ListLivres.Single(livre => livre.Titre == nouveauLivre.Titre).NombreEnStock++;
            }
            else
            {
                ListLivres.Add(nouveauLivre);
            }
        }

        public CD rechercherCD(string titre)
        {
            return ListCD.Single(cd => cd.Titre == titre);
        }

        public Livre rechercherLivre(string titre)
        {
            try
            {
                return ListLivres.Single(livre => livre.Titre == titre);
            }
            catch (InvalidOperationException e)
            {
                return null;
            }
            catch
            {
                return null;
            }
        }
    }
}
