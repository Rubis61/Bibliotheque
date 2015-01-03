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
        public LogApplication log = new LogApplication();
        public string saisieUtilisateur { get; set; }
        public static string pathOfLog { get; set; }

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
            Console.WriteLine("6 - Supprimer un livre de la bibliothèque");
            Console.WriteLine("7 - Ajouter un CD");
            Console.WriteLine("8 - Emprunter un CD");
            Console.WriteLine("9 - Ramener un CD");
            Console.WriteLine("10 - Rechercher un CD par son titre");
            Console.WriteLine("11 - Supprimer un CD de la bibliothèque");
            Console.WriteLine();
            Console.WriteLine("0 - Quitter");
            Console.WriteLine();
        }

        public void ListerLaBibliotheque()
        {
            log.WriteMessage(DateTime.Now.ToString() + " : " + "Listez la bibliothèque");

            Console.WriteLine("/////////////////////////////////////////////////////////////////////////");
            Console.WriteLine("//                            Bibliothèque                             //");
            Console.WriteLine("/////////////////////////////////////////////////////////////////////////");
            Console.WriteLine();
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

            Console.WriteLine();
            AppuyerSurUneTouchePourContinuer();
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
            Console.WriteLine("Le livre \"" + titre + "\" de l'auteur \"" + auteur + "\" a bien été ajouté avec ");
            Console.WriteLine("comme genre " + saisieUtilisateur.ToUpper() + " et comme numéro ISBN \"" + ISBN + "\"");

            Console.WriteLine();

            log.WriteMessage(DateTime.Now.ToString() + " : " + "Ajout du livre " + titre + " écrit par : " + auteur + " du genre : " + saisieUtilisateur.ToUpper() + " avec comme numéro ISBN : " + ISBN);

            AppuyerSurUneTouchePourContinuer();
        }
        public void EmprunterUnLivre()
        {
            bool erreur;
            do
            {
                Console.WriteLine("Vous voulez emprunter un livre.");
                Console.WriteLine();
                Console.WriteLine("Quel est le titre du livre que vous voulez emprunter ?");
                Console.WriteLine("Sinon tapez 'retour' pour annuler");
                saisieUtilisateur = Console.ReadLine().ToString();
                try
                {
                    erreur = Boolean.Parse(bibliotheque.emprunterLivre(saisieUtilisateur));

                    if(erreur == false)
                    {
                        Console.WriteLine("Le livre n'est plus disponible ! Revenez plus tard ;)");
                        Console.ReadKey();
                    }
                }
                catch(FormatException)
                {
                    erreur = false;
                    Console.WriteLine("Le livre n'existe pas ou n'a pas été trouvé dans la bibliothèque.");
                    Console.ReadKey();
                }
                if (saisieUtilisateur == "retour" || saisieUtilisateur == "Retour")
                {
                    return;
                }
            }
            while (erreur == false);

            Console.WriteLine("Le CD " + saisieUtilisateur + " a bien été emprunté");

            Console.WriteLine();

            log.WriteMessage(DateTime.Now.ToString() + " : " + "Emprunt du livre " + saisieUtilisateur);
            AppuyerSurUneTouchePourContinuer();
        }
        public void RamenerUnLivre()
        {
            bool livreRamené = false;
            Console.WriteLine("Vous voulez ramener un livre.");
            Console.WriteLine();

            do // Gestion de la saisie utilisateur
            {
                Console.WriteLine("Quel est le titre du livre que vous voulez rapporter ?");
                Console.WriteLine("Sinon tapez 'retour' pour annuler");
                saisieUtilisateur = Console.ReadLine().ToString();
                if (saisieUtilisateur.ToLower() == "retour")
                {
                    return;
                }
                livreRamené = bibliotheque.restituerLivre(saisieUtilisateur);
                if (livreRamené == false)
                {
                    Console.WriteLine("ERREUR : Le livre n'a pas été trouvé ! Veuillez recommencer !");
                    Console.WriteLine();
                }
                else Console.WriteLine("Le livre \"" + saisieUtilisateur + "\" a bien été rapporté");
            }
            while (livreRamené == false);

            log.WriteMessage(DateTime.Now.ToString() + " : " + "Emprunt du livre " + saisieUtilisateur);

            Console.WriteLine();
            AppuyerSurUneTouchePourContinuer();
        }
        public void RechercherLivreParTitre()
        {
            Livre livre;
            Console.WriteLine("Vous voulez rechercher un livre par son titre.");
            Console.WriteLine();

            do
            {
                Console.WriteLine("Quel est le titre du livre que vous voulez rechercher ?");
                Console.WriteLine("Sinon tapez 'retour' pour annuler");
                saisieUtilisateur = Console.ReadLine().ToString();

                if (saisieUtilisateur.ToLower() == "retour")
                {
                    return;
                }

                livre = bibliotheque.rechercherLivre(saisieUtilisateur);

                Console.WriteLine();

                if (livre == null)
                {
                    Console.WriteLine("ERREUR : Le livre n'a pas été trouvé ! Veuillez recommencer !");
                    Console.WriteLine();
                }
                else
                {
                    Console.WriteLine("Le livre a bien été trouvé : ");
                    Console.WriteLine("    " + livre.ToString());
                }
            }
            while (livre == null);

            Console.WriteLine();
            log.WriteMessage(DateTime.Now.ToString() + " : " + "Recherche du livre " + saisieUtilisateur);
            AppuyerSurUneTouchePourContinuer();
        }

        public void SupprimerUnLivre()
        {
            Livre livre;
            Console.WriteLine("Vous voulez supprimer un livre.");
            Console.WriteLine();

            do
            {
                Console.WriteLine("Quel est le titre du livre que vous voulez supprimer ?");
                Console.WriteLine("Sinon tapez 'retour' pour annuler");
                saisieUtilisateur = Console.ReadLine().ToString();

                if (saisieUtilisateur.ToLower() == "retour")
                {
                    return;
                }

                livre = bibliotheque.rechercherLivre(saisieUtilisateur);

                Console.WriteLine();

                if (livre == null)
                {
                    Console.WriteLine("ERREUR : Le livre n'a pas été trouvé ! Veuillez recommencer !");
                    Console.WriteLine();
                }
                else
                {
                    Console.WriteLine("Le livre a bien été supprimé : ");
                    livre.NombreEnStock--;
                }
            }
            while (livre == null);

            Console.WriteLine();
            AppuyerSurUneTouchePourContinuer();

        }

        public void RechercherCDParTitre()
        {
            CD cd;
            Console.WriteLine("Vous voulez rechercher un CD par son titre.");
            Console.WriteLine();

            do
            {
                Console.WriteLine("Quel est le titre du CD que vous voulez rechercher ?");
                Console.WriteLine("Sinon tapez 'retour' pour annuler");
                saisieUtilisateur = Console.ReadLine().ToString();

                if (saisieUtilisateur.ToLower() == "retour")
                {
                    return;
                }

                cd = bibliotheque.rechercherCD(saisieUtilisateur);

                Console.WriteLine();

                if (cd == null)
                {
                    Console.WriteLine("ERREUR : Le CD n'a pas été trouvé ! Veuillez recommencer !");
                    Console.WriteLine();
                }
                else
                {
                    Console.WriteLine("Le CD a bien été trouvé : ");
                    Console.WriteLine("    " + cd.ToString());
                }
            }
            while (cd == null);

            Console.WriteLine();
            AppuyerSurUneTouchePourContinuer();
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
            Console.WriteLine("Le CD \"" + titre + "\" de l'artiste \"" + artiste + "\" a bien été");
            Console.WriteLine(" ajouté avec comme style " + saisieUtilisateur.ToUpper() + " et comme musiques : ");
            
            maListe.ForEach(delegate(Musique musique)
            {
                Console.WriteLine(musique.Numero + " - " + musique.Titre);
            });

            Console.WriteLine();
            AppuyerSurUneTouchePourContinuer();
        }
        public void EmprunterUnCD()
        {
            bool erreur;
            do
            {
                Console.WriteLine("Vous voulez emprunter un CD.");
                Console.WriteLine();
                Console.WriteLine("Quel est le titre du CD que vous voulez emprunter?");
                Console.WriteLine("Tapez 'retour' pour annuler");
                saisieUtilisateur = Console.ReadLine().ToString();
                try
                {
                    erreur = Boolean.Parse(bibliotheque.emprunterCD(saisieUtilisateur));

                    if (erreur == false)
                    {
                        Console.WriteLine("Le CD n'est plus disponible ! Revenez plus tard ;)");
                        Console.ReadKey();
                    }
                }
                catch (FormatException)
                {
                    erreur = false;
                    Console.WriteLine("Le CD n'existe pas ou n'a pas été trouvé dans la bibliothèque.");
                    Console.ReadKey();
                }

                if(saisieUtilisateur.ToLower() == "retour")
                {
                    return;
                }
            }
            while (erreur == false);
            Console.WriteLine("Le CD \"" + saisieUtilisateur + "\" a bien été emprunté");
            Console.ReadLine();
        }
        public void RamenerUnCd()
        {
            bool cdRamené = false;
            Console.WriteLine("Vous voulez ramener un CD.");
            Console.WriteLine();

            do // Gestion de la saisie utilisateur
            {
                Console.WriteLine("Quel est le titre du CD que vous voulez rapporter?)");
                Console.WriteLine("Sinon tapez 'retour' pour annuler");
                saisieUtilisateur = Console.ReadLine().ToString();
                if (saisieUtilisateur.ToLower() == "retour")
                {
                    return;
                }
                cdRamené = bibliotheque.restituerCD(saisieUtilisateur);
                if (cdRamené == false)
                {
                    Console.WriteLine("ERREUR : Le CD n'a pas été trouvé ! Veuillez recommencer !");
                    Console.WriteLine();
                }
                else Console.WriteLine("Le CD \"" + saisieUtilisateur + "\" a bien été rapporté");
            }
            while (cdRamené == false);

            Console.WriteLine();
            AppuyerSurUneTouchePourContinuer();
        }
        public void SupprimerUnCD()
        {
            Console.WriteLine("Quel est le titre du livre ou CD que vous voulez enlever de la bibliothèque ?");
            Console.WriteLine("Sinon tapez 'retour' pour annuler");

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
              saisieUtilisateur = Console.ReadLine().ToString();
            }
            while(!genreDisponible.Contains(saisieUtilisateur));

            return (GenreDuLivre)Enum.Parse(typeof(GenreDuLivre), saisieUtilisateur);
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
                saisieUtilisateur = Console.ReadLine().ToString();
            }
            while (!styleDisponible.Contains(saisieUtilisateur));

            return (Style)Enum.Parse(typeof(Style), saisieUtilisateur);
        }

        private void AppuyerSurUneTouchePourContinuer()
        {
            Console.WriteLine("Appuyez sur une touche pour continuer...");
            Console.ReadKey();
        }
        public string EmplacementFichierLog()
        {
            Console.WriteLine("Où voulez vous placer le fichier de log de l'application?");
            Console.WriteLine(@"Exemple : C:\Users\(Username)\Desktop");
            string path = Console.ReadLine();
            Console.Clear();
            Console.WriteLine("Quel est le nom du fichier de log?");
            Console.WriteLine("Exemple : monfichierlog.txt");
            string name = Console.ReadLine();
            pathOfLog = path + @"\" + name;
            return pathOfLog;
        }
    }
}