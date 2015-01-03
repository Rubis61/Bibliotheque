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
        public Emprunts Emprunts { get; set; }

        public event EventHandler<EmpruntLivreEventArgs> livreEmprunté;
        public event EventHandler<EmpruntCDEventArgs> cdEmprunté;

        private void OnLivreEmprunté(EmpruntLivreEventArgs e)
        {
            if (livreEmprunté != null)
            {
                livreEmprunté(this, e);
            }
        }

        private void OnCDEmprunté(EmpruntCDEventArgs e)
        {
            if(cdEmprunté != null)
            {
                cdEmprunté(this, e);
            }
        }

        public Bibliotheque()
        {
            ListCD = new List<CD>();
            ListLivres = new List<Livre>();
            Emprunts = new Emprunts();
        }

        public Bibliotheque(List<CD> listCD, List<Livre> listLivres, Emprunts emprunts)
        {
            ListCD = listCD;
            ListLivres = listLivres;
            Emprunts = emprunts;
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

        private void ajouterCD(CD cdAajouter)
        {
            if (ListCD.Where(cd => cd.Titre == cdAajouter.Titre).Count() >= 1)
            {
                ListCD.Single(cd => cd.Titre == cdAajouter.Titre).NombreEnStock++;
            }
            else
            {
                ListCD.Add(cdAajouter);
            }
        }

        //public string SupprimerUnCD(string titre)
        //{
        //    try
        //    {
        //        return ListLivres.SingleOrDefault(livre => titre == livre.Titre);
        //    }
        //    catch (InvalidOperationException)
        //    {
        //        return null;
        //    }
        //    catch
        //    {
        //        return null;
        //    }
        //}


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

        private void ajouterLivre(Livre livreAajouter)
        {
            if (ListLivres.Where(livre => livre.Titre == livreAajouter.Titre).Count() >= 1)
            {
                ListLivres.Single(livre => livre.Titre == livreAajouter.Titre).NombreEnStock++;
            }
            else
            {
                ListLivres.Add(livreAajouter);
            }
        }

        public CD rechercherCD(string titre)
        {
            try
            {
                return ListCD.SingleOrDefault(cd => cd.Titre == titre);
            }
            catch(InvalidOperationException)
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
                return ListLivres.SingleOrDefault(livre => livre.Titre == titre);
            }
            catch (InvalidOperationException)
            {
                return null;
            }
            catch
            {
                return null;
            }
        }

        public string emprunterCD(string titre)
        {
            CD cdAEmprunter = rechercherCD(titre);

            if (cdAEmprunter == null) return "null"; // Le CD n'a pas été trouvé

            if (cdAEmprunter.NombreEnStock >= 1)
            { // Si au moins un CD de disponible
                cdAEmprunter.NombreEnStock--; // Prise du CD en stock
                Emprunts.ajouterCD(cdAEmprunter); // Emprunt du CD
                return "true";
            }
            else // Si pas de CD disponible
            {
                return "false";
            }
        }

        public string emprunterLivre(string titre)
        {
            Livre livreAEmprunter = rechercherLivre(titre);

            if (livreAEmprunter == null) return "null"; // Le Livre n'a pas été trouvé

            if (livreAEmprunter.NombreEnStock >= 1)
            { // Si au moins un livre de disponible
                //Livre livreTrouvé = ListLivres.Single(livre => livre.Titre == livreAEmprunter.Titre);

                livreAEmprunter.NombreEnStock--; // Prise du Livre en stock
                Emprunts.ajouterLivre(livreAEmprunter); // Emprunt du Livre

                OnLivreEmprunté(new EmpruntLivreEventArgs(livreAEmprunter));
                return "true";
            }
            else // Si pas de livre disponible
            {
                return "false";
            }
        }

        public bool restituerCD(string titre)
        {
            CD cdARestituer = Emprunts.CDEmpruntés.Find(cd => cd.Titre == titre); // Recherche du CD dans la liste des emprunts

            if (cdARestituer == null) return false;

            ajouterCD(cdARestituer);
            Emprunts.CDEmpruntés.Remove(cdARestituer);

            return true;
        }

        public bool restituerLivre(string titre)
        {
            Livre livreARestituer = Emprunts.LivresEmpruntés.Find(livre => livre.Titre == titre); // Recherche du Livre dans la liste des emprunts

            if (livreARestituer == null) return false;

            ajouterLivre(livreARestituer);
            Emprunts.LivresEmpruntés.Remove(livreARestituer);

            return true;
        }
    }
}