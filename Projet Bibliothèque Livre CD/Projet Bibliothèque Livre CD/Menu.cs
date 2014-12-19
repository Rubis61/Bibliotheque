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
        public void AfficherMenu()
        {

            Console.Clear();
            Console.WriteLine("Que voulez-vous faire?");
            Console.WriteLine("1 - Lister la bibliothèque");
            Console.WriteLine("2 - Ajouter un livre");
            Console.WriteLine("3 - Ramener un livre");
            Console.WriteLine("4 - Rechercher un livre par son titre");
            Console.WriteLine("5 - Ajouter un CD");
            Console.WriteLine("6 - Ramener un CD");
            Console.WriteLine("7 - Rechercher un CD par son titre");
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

            Console.WriteLine("Le livre " + titre + " de l'auteur " + auteur + " a bien était ajouté avec comme genre " + " avec comme numéro ISBN " + ISBN);
            Console.WriteLine("Appuyer sur une touche pour continuer...");
            Console.ReadLine();
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
            Console.WriteLine("Bibliothèque");
            Console.ReadLine();
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
            Console.WriteLine();
            Console.WriteLine("Tapez le genre de votre livre");
            Console.WriteLine();

            string saisieUtilisateur;
            string[] genreDisponible = Livre.getGenresDisponibles();

            do
            {
              Console.Clear();
              for (int i = 0; i < genreDisponible.Count(); i ++)
              {
                 Console.WriteLine(genreDisponible[i]);
              }
              saisieUtilisateur = Console.ReadLine().ToString();
            }
            while(!genreDisponible.Contains(saisieUtilisateur));

            return (GenreDuLivre)Enum.Parse(typeof(GenreDuLivre), saisieUtilisateur);
        }
    }
}
