using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_Bibliothèque_Livre_CD
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Musique> musiques = new List<Musique>
            {
                new Musique("Heroes", 1),
                new Musique("titre2", 2),
                new Musique("titre3", 3),
                new Musique("titre4", 4),
            };
            CD cd = new CD(0, "Heroes", 55, "Sabaton", Style.Metal, musiques);
            CD cd2 = new CD(0, "123", 222, "Mozart", Style.Classique, musiques);
            CD cd3 = new CD(0, "456", 100, "Sabaton", Style.Metal, musiques);
            Livre livre = new Livre(0, "Bible", 122, "12345-456", "Dieu", GenreDuLivre.Religieux);
            Livre livre2 = new Livre(0, "123", 1, "12345-456", "Dieu", GenreDuLivre.Culturel);
            Livre livre3 = new Livre(0, "456", 1, "12345-456", "Dieu", GenreDuLivre.Drame);

            Bibliotheque bibliothèque = new Bibliotheque(new List<CD>(), new List<Livre>());
            bibliothèque.ajouterCD(cd);
            bibliothèque.ajouterCD(cd2);
            bibliothèque.ajouterCD(cd3);
            bibliothèque.ajouterLivre(livre);
            bibliothèque.ajouterLivre(livre2);
            bibliothèque.ajouterLivre(livre3);

            CD cdTrouvé = bibliothèque.rechercherCD("Heroes");
            Console.WriteLine(cdTrouvé.Titre + " Stock : " + cdTrouvé.NombreEnStock);
            Console.WriteLine();

            Livre livreTrouvé = bibliothèque.rechercherLivre("Bible");
            Console.WriteLine(livreTrouvé.Titre + " Stock : " + livreTrouvé.NombreEnStock);
            Console.WriteLine();

            Console.ReadKey();
        }
    }
}
