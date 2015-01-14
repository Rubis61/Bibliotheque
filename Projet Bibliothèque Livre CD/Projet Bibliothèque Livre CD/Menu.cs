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
        public BDD bdd = new BDD();
        public LogApplication log = new LogApplication();
        public string saisieUtilisateur { get; set; }
        public static string pathOfLog { get; set; }
        public bool IsLocal { get; set; }

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

            List<Livre> listLivres = null;
            List<CD> listCD = null;
            List<Livre> listLivresEmpruntés = null;
            List<CD> listCDsEmpruntés = null;

            if( IsLocal )
            {
                listLivres = bibliotheque.ListLivres;
                listCD = bibliotheque.ListCD;
                listLivresEmpruntés = bibliotheque.Emprunts.LivresEmpruntés;
                listCDsEmpruntés = bibliotheque.Emprunts.CDEmpruntés;
            }
            else
            {
                listLivres = bdd.getLivres().ToList();
                listCD = bdd.getCDs().ToList();
                listLivresEmpruntés = bdd.getLivresEmpruntés().ToList();
                listCDsEmpruntés = bdd.getCDsEmpruntés().ToList();
            }

            Console.WriteLine("/////////////////////////////////////////////////////////////////////////");
            Console.WriteLine("//                            Bibliothèque                             //");
            Console.WriteLine("/////////////////////////////////////////////////////////////////////////");
            Console.WriteLine();
            Console.WriteLine("///////////////////////////////DISPONIBLES///////////////////////////////");
            Console.WriteLine("CDs : ");
            foreach (var cd in listCD)
            {
                Console.WriteLine(cd.ToString());
            }
            Console.WriteLine();
            Console.WriteLine("Livres : ");
            foreach (var livre in listLivres)
            {
                Console.WriteLine(livre.ToString());
            }

            Console.WriteLine();
            Console.WriteLine("///////////////////////////////EMPRUNTS//////////////////////////////////");
            Console.WriteLine("CDs : ");
            foreach (var cd in listCDsEmpruntés)
            {
                Console.WriteLine(cd.ToString());
            }
            Console.WriteLine();
            Console.WriteLine("Livres : ");
            foreach (var livre in listLivresEmpruntés)
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

            GenreDuLivre genre = AfficherEtSaisirGenreDuLivre();
            if (IsLocal)
            {
                bibliotheque.ajouterLivre(titre, ISBN, auteur, genre);
            }
            else
            {
                Livre livre = new Livre(0, titre, 1, ISBN, auteur, genre);
                bdd.AjouterLivre(livre);
            }

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
                Console.Clear();
                Console.WriteLine("Vous voulez emprunter un livre.");
                Console.WriteLine();
                Console.WriteLine("Quel est le titre du livre que vous voulez emprunter ?");
                Console.WriteLine("Sinon tapez 'retour' pour annuler");
                saisieUtilisateur = Console.ReadLine().ToString();
                try
                {
                    if (IsLocal)
                    {
                        erreur = Boolean.Parse(bibliotheque.emprunterLivre(saisieUtilisateur));
                    }
                    else
                    {
                        erreur = bdd.EmprunterUnLivre(saisieUtilisateur);
                    }
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

            Console.WriteLine("Le livre " + saisieUtilisateur + " a bien été emprunté");

            Console.WriteLine();

            log.WriteMessage(DateTime.Now.ToString() + " : " + "Emprunt du livre " + saisieUtilisateur);
            AppuyerSurUneTouchePourContinuer();
        }

        public void RamenerUnLivre()
        {
            bool livreRamené;
            Console.WriteLine("Vous voulez ramener un livre.");
            Console.WriteLine();

            do // Gestion de la saisie utilisateur
            {
                Console.Clear();
                Console.WriteLine("Quel est le titre du livre que vous voulez rapporter ?");
                Console.WriteLine("Sinon tapez 'retour' pour annuler");
                saisieUtilisateur = Console.ReadLine().ToString();
                if (saisieUtilisateur.ToLower() == "retour")
                {
                    return;
                }
                if (IsLocal)
                {
                    livreRamené = bibliotheque.restituerLivre(saisieUtilisateur);
                    if (livreRamené == false)
                    {
                        Console.WriteLine("ERREUR : Le livre n'a pas été trouvé ! Veuillez recommencer !");
                        Console.WriteLine();
                    }
                    else  Console.WriteLine("Le livre \"" + saisieUtilisateur + "\" a bien été rapporté");
                }
                else
                {
                    bdd.rechercherCD(saisieUtilisateur);
                    livreRamené = bdd.RamenerUnLivre(saisieUtilisateur);
                    if (livreRamené == false)
                    {
                        Console.WriteLine("ERREUR : Le livre n'a pas été trouvé ! Veuillez recommencer !");
                        Console.WriteLine();
                    }
                    else Console.WriteLine("Le livre \"" + saisieUtilisateur + "\" a bien été rapporté");
                }

            } while (livreRamené == false);

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

                if (IsLocal)
                {
                    livre = bibliotheque.rechercherLivre(saisieUtilisateur);
                }
                else
                {
                    livre = bdd.RechercherLivre(saisieUtilisateur);
                }
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
            bool result = false;
            do
            {
                Console.WriteLine("Quel est le titre du livre que vous voulez enlever de la bibliothèque ?");
                Console.WriteLine("Sinon tapez 'retour' pour annuler");
                saisieUtilisateur = Console.ReadLine().ToString();

                if (saisieUtilisateur.ToLower() == "retour")
                {
                    return;
                }
                if (IsLocal)
                {
                    result = bibliotheque.SupprimerUnLivre(saisieUtilisateur);
                }
                else result = bdd.SupprimerUnLivre(saisieUtilisateur);

                if (result)
                {
                    Console.WriteLine("Le livre a bien était supprimé");
                    Console.WriteLine();
                    log.WriteMessage(DateTime.Now.ToString() + " : " + "Supression du livre : " + saisieUtilisateur);
                    AppuyerSurUneTouchePourContinuer();  
                    return;
                }
                else
                {
                    Console.WriteLine("Le livre n'existe pas.");
                }
            }
            while (result == false);
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

                if (IsLocal) cd = bibliotheque.rechercherCD(saisieUtilisateur);
                else cd = bdd.rechercherCD(saisieUtilisateur);
                
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
                    log.WriteMessage(DateTime.Now.ToString() + " : " + "Recherche du CD : " + saisieUtilisateur);
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

            Console.WriteLine("Combien de cd disponibles ?");
            int nombre = 1;
            while( int.TryParse(Console.ReadLine(), out nombre) ) ;

            List<Musique> listMusiques = CD.remplirListeDeMusique();

            Style style = AfficherEtSaisirStyleDuCD();

            if (IsLocal) bibliotheque.ajouterCD(titre, artiste, style, listMusiques);
            else bdd.ajouterCD(new CD(0, titre, nombre, artiste, style, listMusiques )); 

            Console.WriteLine();
            Console.WriteLine("Le CD \"" + titre + "\" de l'artiste \"" + artiste + "\" a bien été");
            Console.WriteLine(" ajouté avec comme style " + saisieUtilisateur.ToUpper() + " et comme musiques : ");
            
            foreach (var musique in listMusiques)
            {
                Console.WriteLine(musique.Numero + " - " + musique.Titre);                
            }
            log.WriteMessage(DateTime.Now.ToString() + " : " + "Ajout du CD " + titre + " dans la bibliothèque avec comme artiste " + artiste + " et comme genre " + saisieUtilisateur.ToUpper());
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
                    if (IsLocal) erreur = Boolean.Parse(bibliotheque.emprunterCD(saisieUtilisateur));
                    else erreur = bdd.EmprunterUnCD(saisieUtilisateur);

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
            log.WriteMessage(DateTime.Now.ToString() + " : " + "Le Cd " + saisieUtilisateur + " a été emprunter.");
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

                if (IsLocal) cdRamené = bibliotheque.restituerCD(saisieUtilisateur);
                else cdRamené = bdd.ramenerUnCd(saisieUtilisateur);

                if (cdRamené == false)
                {
                    Console.WriteLine("ERREUR : Le CD n'a pas été trouvé ! Veuillez recommencer !");
                    Console.WriteLine();
                }
                else Console.WriteLine("Le CD \"" + saisieUtilisateur + "\" a bien été rapporté");
            }
            while (cdRamené == false);
            log.WriteMessage(DateTime.Now.ToString() + " : " + "Le Cd " + saisieUtilisateur + " a était rapporté.");
            Console.WriteLine();
            AppuyerSurUneTouchePourContinuer();
        }

        public void SupprimerUnCD()
        {
            bool result = false;
            do
            {
                Console.WriteLine("Quel est le titre du CD que vous voulez enlever de la bibliothèque ?");
                Console.WriteLine("Sinon tapez 'retour' pour annuler");
                saisieUtilisateur = Console.ReadLine().ToString();

                if (saisieUtilisateur.ToLower() == "retour")
                {
                    return;
                }
                
                if (IsLocal) result = bibliotheque.SupprimerUnCD(saisieUtilisateur);
                else result = bdd.SupprimerUnCD(saisieUtilisateur);

                if (result)
                {
                    bibliotheque.SupprimerUnCD(saisieUtilisateur);
                    Console.WriteLine("Le CD a bien été supprimé");
                    Console.WriteLine();
                    log.WriteMessage(DateTime.Now.ToString() + " : " + "Suppresion du CD " + saisieUtilisateur);
                    AppuyerSurUneTouchePourContinuer();
                    return;
                }
                else 
                {
                    Console.WriteLine("Le CD n'existe pas.");
                }
            }
            while (result == false);
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
            if( path == "" )
            {
                pathOfLog = name;
            }
            else pathOfLog = path + @"\" + name;

            return pathOfLog;
        }
        public void ChoixLocalOuBDD()
        {
            do
            {
                Console.Clear();
                Console.WriteLine("Voulez vous travailler en local où en BDD?");
                Console.WriteLine("Tapez 'Local' ou 'BDD' pour choisir sur quel support travailler");
                saisieUtilisateur = Console.ReadLine().ToString().ToUpper();
            } while (saisieUtilisateur != "LOCAL" && saisieUtilisateur != "BDD");
            if (saisieUtilisateur == "LOCAL")
            {
                IsLocal = true;
            }
            else IsLocal = false;
        }
    }
}