using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_Bibliothèque_Livre_CD
{
    class Program
    {
        //private static Bibliotheque bibliothèque = new Bibliotheque(new List<CD>(), new List<Livre>(), new Emprunts());
        static void Main(string[] args)
        {
            // Activer cette ligne que pour générer la BDD, par exemple si modification
            /*/ générerBDD(); //*/

            //TESTS();

            Application.menu.bibliotheque.livreEmprunté += bibliotheque_LivreEmprunté;
                /*(sender, livre) => 
                    Console.WriteLine(livre.livreEmprunté.ToString());
                */
            Application.menu.bibliotheque.cdEmprunté += bibliotheque_CDEmprunté;

            DémarrerApplication();

            Menu menu = new Menu();
            bool erreur;
            LogApplication log = new LogApplication();
            do
            {
                erreur = log.CréerUnFichierTexte(menu.EmplacementFichierLog());
            } while (erreur == false);
            menu.ChoixLocalOuBDD();

            //*/ Ajoute ou enleve un '/' au début de cette ligne pour switch entre le code de test et le code final
            Application.générerDonnées(); // Rempli les données de la bibliothèque pour les tests, plus tard celà sera fait par la base de données
            
            while (Application.RunApplication()) ;

            ArrêterApplication();

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

        private static void ArrêterApplication()
        {
            Console.Write("Arrêt de l'application : ");

            for (int i = 0; i < 20; i++)
            {
                Console.Write("█");
                System.Threading.Thread.Sleep(50);
            }

            Console.Clear();
        }

        private static void DémarrerApplication()
        {
            Console.Write("Démarrage de l'application : ");

            for (int i = 0; i < 20; i++)
            {
                Console.Write("█");
                System.Threading.Thread.Sleep(50);
            }

            Console.Clear();
        }

        private static void bibliotheque_LivreEmprunté(Object sender, EmpruntLivreEventArgs e)
        {
            Console.WriteLine("Un livre a été emprunté : ");
            Console.WriteLine("    " + e.livreEmprunté.ToString());
        }

        private static void bibliotheque_CDEmprunté(Object sender, EmpruntCDEventArgs e)
        {
            Console.WriteLine("Un cd a été emprunté : ");
            Console.WriteLine("    " + e.cdEmprunté.ToString());
        }

        private static void TESTS()
        {
            BDD bdd = new BDD();
            /*
            bool ok = bdd.ajouterLivre(new Livre(-1, "OK", 5, "456-654-789", "Moi", GenreDuLivre.Drame));
            ok = bdd.setNombreEnStock_Livre("OK", 10);

            List<Livre> livres = bdd.getLivres().ToList();
            livres.ForEach((livre) => Console.WriteLine(livre.ToString())); // Test méthode BDD: GetLivres()*/
            List<CD> cds = bdd.getCDs().ToList();
            foreach (var cd in cds)
            {
                Console.WriteLine(cd.Titre);
            }

            List<Musique> musiques = new List<Musique>()
            {
                new Musique("Mus1", 0),
                new Musique("Mus2", 1)
            };
            CD cdTest = new CD(0, "Essai", 1, "Moi", Style.Metal, musiques);
            bdd.ajouterCD(cdTest);
        }

        private static void générerBDD()
        {
            //Création du fichier de stockage de la base de données SQLite
            //A ne faire qu'une fois.
            //SQLiteConnection.CreateFile("Bibliotheque.sqlite");
            // Connexion à la base de données
            SQLiteConnection dbConnection = new SQLiteConnection("Data Source=Bibliotheque.sqlite;Version=3;");
            dbConnection.Open(); // Ouverture de la connexion
            string sqlCreateTable_Livre = // Création Table Livre
              @"CREATE TABLE Livre (
                Id         INTEGER       PRIMARY KEY AUTOINCREMENT,
                Titre      NVARCHAR (50) NULL,
                Nombre     INT           NULL,
                Auteur     NVARCHAR (50) NULL,
                NumeroISBN NVARCHAR (50) NULL,
                Genre      NVARCHAR (50) NULL );";
            string sqlCreateTable_CD = // Création Table CD
              @"CREATE TABLE CD (
                Id      INTEGER       PRIMARY KEY AUTOINCREMENT,
                Titre   NVARCHAR (50) NULL,
                Nombre  INT           DEFAULT ((1)) NULL,
                Artiste NVARCHAR (50) NULL,
                Style   NVARCHAR (50) NULL );";
            string sqlCreateTable_Musique = // Création Table Musique
              @"CREATE TABLE Musique (
                Id    INTEGER       PRIMARY KEY AUTOINCREMENT,
                FK_CD INT           NOT NULL,
                Titre NVARCHAR (50) NULL,
                PRIMARY KEY (Id ASC) );";

            // Exécution des requêtes pour créer les 3 tables
            SQLiteCommand command = new SQLiteCommand(sqlCreateTable_Livre, dbConnection);
            /*/command.ExecuteNonQuery(); //*/

            command = new SQLiteCommand(sqlCreateTable_CD, dbConnection);
            /**/command.ExecuteNonQuery(); //*/

           // command = new SQLiteCommand(sqlCreateTable_Musique, dbConnection);
            /*/command.ExecuteNonQuery(); //*/
            /*
            // Insertion de 3 livres : Bible par DIEU de genre Religieux
            string sqlInsert_Livre = "INSERT INTO Livre(Titre, Nombre, Auteur, NumeroISBN, Genre) VALUES('Bible', 3, 'Dieu', '123-456-789', 'Religieux');";
            command = new SQLiteCommand(sqlInsert_Livre, dbConnection);*/
            /*/ command.ExecuteNonQuery(); //*/
            /*
            string sqlSelect_Livre = @"SELECT * FROM Livre WHERE Titre='Bible';";
            command = new SQLiteCommand(sqlSelect_Livre, dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();

            while(reader.Read())
            {
                Console.WriteLine(reader["Id"] + " " + reader["Titre"] + " " + reader["Auteur"] + " " + reader["NumeroISBN"] + " " + reader["Genre"]);
            }*/
        }
    }
}
