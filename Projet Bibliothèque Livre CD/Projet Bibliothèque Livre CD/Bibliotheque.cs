using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_Bibliothèque_Livre_CD
{
    public class Bibliotheque
    {
        public List<CD> ListCD { get; set; }
        public List<Livre> ListLivres { get; set; }
        private Emprunts emprunts = new Emprunts();

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

        public void ajouterCD(string titre, string artiste, Style style, List<Musique> musiques)
        {
            int idNouveauCd = ListCD.Count;
            CD nouveauCD = new CD(idNouveauCd, titre, 1, artiste, style, musiques);

            if (ListCD.Where(cd => cd.Titre == nouveauCD.Titre).Count() >= 1)
            {
                ListCD.Single(cd => cd.Titre == nouveauCD.Titre).NombreEnStock++;
            }
            else
            {
                ListCD.Add(nouveauCD);
            }
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
            try
            {
                return ListCD.Single(cd => cd.Titre == titre);
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