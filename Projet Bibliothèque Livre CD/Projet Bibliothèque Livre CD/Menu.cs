using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_Bibliothèque_Livre_CD
{
    public class Menu
    {
        public Bibliotheque bibliotheque = new Bibliotheque();
        public string saisiUtilisateur { get; set; }

        public void AfficherMenu()
        {

            Console.Clear();
            Console.WriteLine("Que voulez-vous faire?");
            Console.WriteLine();
            Console.WriteLine("1 - Lister la bibliothèque");
            Console.WriteLine("2 - Ajouter un livre");
            Console.WriteLine("3 - Emprunter un livre");
            Console.WriteLine("4 - Ramener un livre");
            Console.WriteLine("5 - Rechercher un livre par son titre");
            Console.WriteLine("6 - Ajouter un CD");
            Console.WriteLine("7 - Emprunter un CD");
            Console.WriteLine("8 - Ramener un CD");
            Console.WriteLine("9 - Rechercher un CD par son titre");
            Console.WriteLine();
        }

        public void ListerLaBibliotheque()
        {
            Console.WriteLine("Bibliothèque");
            Console.ReadLine();
        }

        public void AjouterUnLivre()
        {
            Console.WriteLine("Vous voulez ajouter un livre à la bibliothèque");
            Console.WriteLine();

            Console.WriteLine("Quel est le titre du livre?");
            string titre = Console.ReadLine();

            Console.WriteLine("Quel est l'auteur du livre?");
            string auteur = Console.ReadLine();

            Console.WriteLine("Quel est le numéro ISBN du livre?");
            string ISBN = Console.ReadLine();

            bibliotheque.ajouterLivre(titre, ISBN, auteur, AfficherEtSaisirGenreDuLivre());
            Console.WriteLine();
            Console.WriteLine("Le livre " + titre + " de l'auteur " + auteur + " a bien était ajouté avec ");
            Console.WriteLine("comme genre " + saisiUtilisateur + " avec comme numéro ISBN " + ISBN);
            Console.WriteLine("Appuyer sur une touche pour continuer...");
            Console.ReadLine();
        }
        public void EmprunterUnLivre()
        {
            Co
            bibliotheque.emprunterLivre()
        }
        public void RamenerUnLivre()
        {
            Console.WriteLine("Ramener un livre");
            Console.ReadLine();
        }
        public void RechercherLivreParTitre()
        {
            Console.WriteLine("Bibliothèque");
            Console.ReadLine();
        }
        public void AjouterUnCd()
        {
            Console.WriteLine("Vous voulez ajouter un CD à la bibliothèque");
            Console.WriteLine();

            Console.WriteLine("Quel est le titre de l'album?");
            string titre = Console.ReadLine();

            Console.WriteLine("Quel est l'artiste du CD?");
            string artiste = Console.ReadLine();
            List<Musique> maListe = CD.remplirListeDeMusique();
            bibliotheque.ajouterCD(titre, artiste, AfficherEtSaisirStyleDuCD(), maListe);
            Console.WriteLine();
            Console.WriteLine("Le CD " + titre + " de l'artiste " + artiste + " a bien était");
            Console.WriteLine(" ajouté avec comme style " + saisiUtilisateur + " et comme musiques : ");
            int i = 0;
            maListe.ForEach(delegate(Musique musique)
            {
                Console.WriteLine(i++ + " - " + musique.Titre);
            });
            Console.WriteLine("Appuyer sur une touche pour continuer...");
            Console.ReadLine();
        }
        public void EmprunterUnCD()
        {
            bool erreur;
            do
            {
                Console.WriteLine("Vous voulez emprunter un CD.");
                Console.WriteLine();
                Console.WriteLine("Quel est le titre du CD que vous voulez emprunter? (Tapez 'retour' pour annuler)");
                saisiUtilisateur = Console.ReadLine().ToString();
                erreur = bibliotheque.emprunterLivre(saisiUtilisateur);
                if(saisiUtilisateur == "retour" || saisiUtilisateur == "Retour")
                {
                    Application.VoulezVousContinuez();
                }
            }
            while (erreur == false);
            Console.WriteLine("Le CD " + saisiUtilisateur + " a bien était emprunter");
        }
        public void RamenerUnCd()
        {
            Console.WriteLine("Bibliothèque");
            Console.ReadLine();
        }
        public void RechercherCDParTitre()
        {
            Console.WriteLine("Bibliothèque");
            Console.ReadLine();
        }

        public GenreDuLivre AfficherEtSaisirGenreDuLivre()
        {

            Console.WriteLine("Quel est le genre du livre?");

            string[] genreDisponible = Livre.getGenresDisponibles();

            do
            {
              for (int i = 0; i < genreDisponible.Count(); i ++)
              {
                 Console.WriteLine(" - " + genreDisponible[i]);
              }
              saisiUtilisateur = Console.ReadLine().ToString();
            }
            while(!genreDisponible.Contains(saisiUtilisateur));

            return (GenreDuLivre)Enum.Parse(typeof(GenreDuLivre), saisiUtilisateur);
        }
        public Style AfficherEtSaisirStyleDuCD()
        {
            Console.WriteLine("Quel est le genre de l'album?");
            string[] styleDisponible = CD.getStylesDisponibles();
            do
            {
                
                for (int i = 0; i < styleDisponible.Count(); i++)
                {
                    Console.WriteLine(" - " + styleDisponible[i]);
                }
                saisiUtilisateur = Console.ReadLine().ToString();
            }
            while (!styleDisponible.Contains(saisiUtilisateur));

            return (Style)Enum.Parse(typeof(Style), saisiUtilisateur);
        }

    }
}
