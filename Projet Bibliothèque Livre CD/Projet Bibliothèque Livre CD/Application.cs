using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_Bibliothèque_Livre_CD
{

    public static class Application
    {
        public static string choixUtilisateur;
        public static string finDuProgramme;
        static Menu menu = new Menu();
        public static bool RunApplication()
        {
            do
            {

                menu.AfficherMenu();
                choixUtilisateur = Console.ReadLine().ToString();
                
            }
            while (choixUtilisateur != "1" && choixUtilisateur != "2" && choixUtilisateur != "3" && choixUtilisateur != "4" && choixUtilisateur != "5" && choixUtilisateur != "6" && choixUtilisateur != "7");

            Console.Clear();

            switch(choixUtilisateur)
            {
                case "1": menu.ListerLaBibliotheque();       break;
                case "2": menu.AjouterUnLivre();             break;
                case "3": menu.RamenerUnLivre();             break;
                case "4": menu.RechercherLivreParTitre();    break;
                case "5": menu.AjouterUnCd();                break;
                case "6": menu.RamenerUnCd();                break;
                case "7": menu.RechercherCDParTitre();       break;
            }

            return VoulezVousContinuez();
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
}
