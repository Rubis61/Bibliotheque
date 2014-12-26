﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_Bibliothèque_Livre_CD
{
    class Program
    {
        private static Bibliotheque bibliothèque = new Bibliotheque(new List<CD>(), new List<Livre>(), new Emprunts());

        static void Main(string[] args)
        {
            //*/ Ajoute ou enleve un '/' au début de cette ligne pour switch entre le code de test et le code final
            Application.générerDonnées(); // Rempli les données de la bibliothèque pour les tests, plus tard celà sera fait par la base de données
            
            while (Application.RunApplication()) ;

            Console.Write("Sortie de l'application : ");
            
            for( int i=0 ; i<20 ; i++ )
            {
                Console.Write("█");
                System.Threading.Thread.Sleep(100);
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

            bool OK = false;

            OK = bibliothèque.emprunterCD("456"); // Doit être OK
            Console.WriteLine("Emprunt CD 456 : " + (OK == true).ToString());

            OK = bibliothèque.restituerCD("125"); // N'existe pas
            Console.WriteLine("Restitution CD 125 : " + (OK==false).ToString());

            OK = bibliothèque.restituerCD("456"); // Existe
            Console.WriteLine("Restitution CD 456 : " + (OK == true).ToString());

            Console.ReadKey(); // Attend avant de fermer le programme
            //*/
        }
    }
}
