using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_Bibliothèque_Livre_CD
{
    class TRAVAILAFAIRE
    {
        /// ici on mettra les choses à faire, pour pouvoir s'y retrouver ^^
        /// 
        /// Bon travail pour l'architecture du code avec l'Application RUN :D
        ///      - Merci
        ///      
        ///emprunter un livre : t'as du copier coller l'emprunter un CD, y'a des choses qui sont pas bonnes (class : Bibliothèque / méthode : emprunterUnLivre)
        ///      - Corrigé
        ///      
        //deux fois la méthode ajouterLivre normal? (class : Bibliotheque / méthode : ajouterLivre) 
        ///      - !! OUI C'EST NORMAL !! y'en a une qui me sert en privée dans la classe et l'autre publique pour les autres classes
        ///      
        ///quand t'emprunte un CD ou un livre le stock est pas géré, tu peux emprunter 40 fois le truk
        ///      - FAIT!
        ///      
        ///tester si le livre / CD existe avant de le restituer? (class Bibliotheque / méthode restituer CD et restituer Livre)
        ///     - OK
        ///     
        /// Emprunt d'un Livre/CD par évênement, cf : Consigne
        ///      - FAIT!
        ///      
        /// Cohérence nom de méthode/contenu (ex: VoulezVousContinuez? et Voulez vous arrêter?)
        ///     - OK
        ///     
        /// Gérer le retour de "emprunterLivre" et "emprunterCD", est ce que l'on gère 3 états ? genre true = emprunté, false = nombre pas disponible et null = livre non trouvé ?
        ///     - OK, remplacé le booléen par un string que je convert en booleen, si ça plante, c'est que c'est null donc livre non trouvé, sinon c'est true où false
        ///     - + ajout d'un message pour savoir pourquoi ça plante ( l'utilisateur sait que le livre n'existe pas / ou qu'il n'est plus en stock)
        ///     - ?bool
        ///     
        /// Parfois le programme ne s'arrete pas lorsque je fais "o" dans "VoulezVousContinuez" --> je l'ai corrigé ! cf : boucle principale dans Program
        ///     - OK
        ///     
        /// Choix 0 dans le menu pour arrêter le programme => Fait
        ///     - OK
        /// 
        /// Pas con le ToLower() j'y avais pas pensé, c'plus propre, je l'ai modifié sur ceux qui restés
        /// 
        /// Fun le petit launcher, j'ai baissé un peu le sleep, un peu long
        /// 
        /// Système de LOG ?!?!
        ///     - OK
        /// 
        /// 3 avertissements "variable e" jamais utilisé
        ///     - Corrigé, enlevé les arguments inutiles
        ///     
        ///SUPPRESSION D'UN ELEMENT !!
        ///     - OK
        ///     
        /// ForEach de parcours des listes
        ///     - OK
        ///     
        /// Comment gérer les emprunts dans la BDD ??!!   
        ///     - OK
        /// ============= APRES TOUT CA ============
        /// 
        /// Gestion de la sauvegarde/restauration par fichier ou par BDD -> BDD EN COURS
        /// 
        /// Fautes d'orthographes => En cours 
        /// 
        /// Conventions de nommages (PascalCase - camelCase)
        /// 
        /// attributs privées    
    }
}
