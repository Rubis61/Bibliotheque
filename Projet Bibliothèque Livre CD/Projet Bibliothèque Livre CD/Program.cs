using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_Bibliothèque_Livre_CD
{
    class Program
    {
        private static Bibliotheque bibliothèque = new Bibliotheque(new List<CD>(), new List<Livre>());

        static void Main(string[] args)
        {
            //*/ Ajoute ou enleve un '/' au début de cette ligne pour switch entre le code de test et le code final
            while (Application.RunApplication() == true)
            {
                Application.RunApplication();
            }
            /*/
            //////////////////////////////////////////////-Test 1-////////////////////////////////////////////////////////////////////
            // Test si lors de l'ajout d'un livre déja existant cela ajoute le nombre en stock de celui qui est dans la bibliothèque
            // Ajout de 4 fois le même livre
            //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            bibliothèque.ajouterLivre("123", "123-456-789", "moi", GenreDuLivre.Policier);
            bibliothèque.ajouterLivre("123", "123-456-789", "moi", GenreDuLivre.Policier);
            bibliothèque.ajouterLivre("123", "123-456-789", "moi", GenreDuLivre.Policier);
            bibliothèque.ajouterLivre("123", "123-456-789", "moi", GenreDuLivre.Policier);

            Livre livre = bibliothèque.rechercherLivre("123"); // récupération du livre en quadruple
            Console.WriteLine(livre.NombreEnStock); // Doit afficher 4

            ///////////////////////////////////////////////-Test 2-///////////////////////////////////////////////////////////////////
            // Test si lors de l'ajout d'un CD déja existant cela ajoute le nombre en stock de celui qui est dans la bibliothèque
            //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            List<Musique> musiques = new List<Musique>() // Liste de musiques pour créer un CD
            {
                new Musique("789", 0),
                new Musique("789", 1),
                new Musique("789", 2),
                new Musique("789", 3),
                new Musique("789", 4)
            };

            // Ajout de 4 fois le même CD
            bibliothèque.ajouterCD("456", "654", Style.Metal, musiques);
            bibliothèque.ajouterCD("456", "654", Style.Metal, musiques);
            bibliothèque.ajouterCD("456", "654", Style.Metal, musiques);

            CD cd = bibliothèque.rechercherCD("456"); // récupération du livre en quadruple
            Console.WriteLine(cd.NombreEnStock); // Doit afficher 3

            Console.ReadKey(); // Attend avant de fermer le programme
            //*/
        }
    }
}
