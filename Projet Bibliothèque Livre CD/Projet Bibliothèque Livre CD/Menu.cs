using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_Bibliothèque_Livre_CD
{
    public static class Menu
    {
        public static string choixUtilisateur;
        public static string finDuProgramme;
        public static Bibliotheque bibliotheque = new Bibliotheque();
        public static bool AfficherMenu()
        {
            do
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

                choixUtilisateur = Console.ReadLine().ToString();
                
            }
            while (choixUtilisateur != "1" && choixUtilisateur != "2" && choixUtilisateur != "3" && choixUtilisateur != "4" && choixUtilisateur != "5" && choixUtilisateur != "6" && choixUtilisateur != "7");

            Console.Clear();

            switch(choixUtilisateur)
            {
                case "1": ListerLaBibliotheque();       break;
                case "2": AjouterUnLivre();             break;
                case "3": RamenerUnLivre();             break;
                case "4": RechercherLivreParTitre();    break;
                case "5": AjouterUnCd();                break;
                case "6": RamenerUnCd();                break;
                case "7": RechercherCDParTitre();       break;
            }

            return VoulezVousContinuez();
        }

        public static void ListerLaBibliotheque()
        {
            Console.WriteLine("Bibliothèque");
            Console.ReadLine();
        }

        public static void AjouterUnLivre()
        {
            Console.WriteLine("Vous voulez ajouter un livre à la bibliothèque");
            Console.WriteLine();

            Console.WriteLine("Quel est le titre du livre?");
            string titre = Console.ReadLine();

            Console.WriteLine("Quel est l'auteur du livre?");
            string auteur = Console.ReadLine();

            Console.WriteLine("Quel est le genre du livre?");

            string genre = Console.ReadLine();

            Console.WriteLine("Quel est le numéro ISBN du livre?");
            string ISBN = Console.ReadLine();

            //string[] genreDisponible 
            //bibliotheque.ajouterLivre(titre, ISBN, auteur, (GenreDuLivre)Enum.Parse(typeof(GenreDuLivre), genre));
            
            Console.WriteLine("Le livre " + titre + " de l'auteur " + auteur + " a bien était ajouté avec comme genre " + genre + " avec comme numéro ISBN " + ISBN);
            Console.WriteLine("Appuyer sur une touche pour continuer...");
            Console.ReadLine();
        }
        public static void RamenerUnLivre()
        {
            Console.WriteLine("Ramener un livre");
            Console.ReadLine();
        }
        public static void RechercherLivreParTitre()
        {
            Console.WriteLine("Bibliothèque");
            Console.ReadLine();
        }
        public static void AjouterUnCd()
        {
            Console.WriteLine("Bibliothèque");
            Console.ReadLine();
        }
        public static void RamenerUnCd()
        {
            Console.WriteLine("Bibliothèque");
            Console.ReadLine();
        }
        public static void RechercherCDParTitre()
        {
            Console.WriteLine("Bibliothèque");
            Console.ReadLine();
        }
        public static bool VoulezVousContinuez()
        {
            do
            {
                Console.Clear();
                Console.WriteLine("Voulez vous arrêter ? o/n");
                Console.WriteLine();
                finDuProgramme = Console.ReadLine().ToString();
            }while (finDuProgramme != "o" && finDuProgramme != "O" && finDuProgramme != "n" && finDuProgramme != "N");

            if (finDuProgramme == "o" || finDuProgramme == "O")
            {
                return false;
            }
            else return true;
        }
    }
}
