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

            Console.WriteLine("///////////////////////////////DISPONIBLES///////////////////////////////");
            Console.WriteLine("CDs : ");
            foreach (var cd in bibliotheque.ListCD)
            {
                Console.WriteLine(cd.ToString());
            }
            Console.WriteLine();
            Console.WriteLine("Livres : ");
            foreach (var livre in bibliotheque.ListLivres)
            {
                Console.WriteLine(livre.ToString());
            }

            Console.WriteLine();
            Console.WriteLine("///////////////////////////////EMPRUNTS//////////////////////////////////");
            Console.WriteLine("CDs : ");
            foreach (var cd in bibliotheque.Emprunts.CDEmpruntés)
            {
                Console.WriteLine(cd.ToString());
            }
            Console.WriteLine();
            Console.WriteLine("Livres : ");
            foreach (var livre in bibliotheque.Emprunts.LivresEmpruntés)
            {
                Console.WriteLine(livre.ToString());
            }

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
            bool erreur;
            do
            {
                Console.WriteLine("Vous voulez emprunter un livre.");
                Console.WriteLine();
                Console.WriteLine("Quel est le titre du livre que vous voulez emprunter? (Tapez 'retour' pour annuler)");
                saisiUtilisateur = Console.ReadLine().ToString();
                erreur = bibliotheque.emprunterLivre(saisiUtilisateur);
                if (saisiUtilisateur == "retour" || saisiUtilisateur == "Retour")
                {
                    break;
                }
            }
            while (erreur == false);
            Console.WriteLine("Le CD " + saisiUtilisateur + " a bien était emprunter");
        }
        public void RamenerUnLivre()
        {
            bool livreRamené = false;
            Console.WriteLine("Vous voulez ramener un livre.");
            Console.WriteLine();

            do // Gestion de la saisie utilisateur
            {
                Console.WriteLine("Quel est le titre du livre que vous voulez rapporter? (Tapez 'retour' pour annuler)");
                saisiUtilisateur = Console.ReadLine().ToString();
                if (saisiUtilisateur.ToLower() == "retour")
                {
                    break;
                }
                livreRamené = bibliotheque.restituerLivre(saisiUtilisateur);
                if (livreRamené == false)
                {
                    Console.WriteLine("ERREUR : Le livre n'a pas été trouvé ! Veuillez recommencer !");
                    Console.WriteLine();
                }
            }
            while (livreRamené == false);
            
            Console.WriteLine();
            if (livreRamené == true) Console.WriteLine("Le livre \"" + saisiUtilisateur + "\" a bien été rapporté");

            if(saisiUtilisateur.ToLower() != "retour") Console.ReadLine();
        }
        public void RechercherLivreParTitre()
        {
            Livre livre;
            do
            {
                Console.WriteLine("Vous voulez rechercher un livre par son titre.");
                Console.WriteLine();
                Console.WriteLine("Quel est le titre du livre que vous voulez rechercher (Tapez 'retour' pour annuler)");
                saisiUtilisateur = Console.ReadLine().ToString();
                livre = bibliotheque.rechercherLivre(saisiUtilisateur); ;

                if (saisiUtilisateur == "retour" || saisiUtilisateur == "Retour")
                {
                    break;
                }
            }
            while (livre == null);
            Console.WriteLine("Le livre " + saisiUtilisateur + " a bien était trouvé");
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
                erreur = bibliotheque.emprunterCD(saisiUtilisateur);

                if(saisiUtilisateur == "retour" || saisiUtilisateur == "Retour")
                {
                    break;
                }
            }
            while (erreur == false);
            Console.WriteLine("Le CD " + saisiUtilisateur + " a bien était emprunter");
            Console.ReadLine();
        }
        public void RamenerUnCd()
        {
            bool cdRamené = false;
            Console.WriteLine("Vous voulez ramener un CD.");
            Console.WriteLine();

            do // Gestion de la saisie utilisateur
            {
                Console.WriteLine("Quel est le titre du CD que vous voulez rapporter? (Tapez 'retour' pour annuler)");
                saisiUtilisateur = Console.ReadLine().ToString();
                if (saisiUtilisateur.ToLower() == "retour")
                {
                    break;
                }
                cdRamené = bibliotheque.restituerCD(saisiUtilisateur);
                if (cdRamené == false)
                {
                    Console.WriteLine("ERREUR : Le CD n'a pas été trouvé ! Veuillez recommencer !");
                    Console.WriteLine();
                }
            }
            while (cdRamené == false);

            Console.WriteLine();
            if (cdRamené == true) Console.WriteLine("Le CD \"" + saisiUtilisateur + "\" a bien été rapporté");


            if (saisiUtilisateur.ToLower() != "retour") Console.ReadLine();
        }
        public void RechercherCDParTitre()
        {
            CD cd;
            do
            {
                Console.WriteLine("Vous voulez rechercher un CD par son titre.");
                Console.WriteLine();
                Console.WriteLine("Quel est le titre du CD que vous voulez rechercher (Tapez 'retour' pour annuler)");
                saisiUtilisateur = Console.ReadLine().ToString();
                cd = bibliotheque.rechercherCD(saisiUtilisateur);

                if (saisiUtilisateur == "retour" || saisiUtilisateur == "Retour")
                {
                    break;
                }
            }
            while (cd == null);
            Console.WriteLine("Le CD " + saisiUtilisateur + " a bien était trouvé");
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
