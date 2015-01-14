using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_Bibliothèque_Livre_CD
{
    public class Emprunts
    {
        public List<Livre> LivresEmpruntés { get; set; }
        public List<CD> CDEmpruntés { get; set; }

        public Emprunts()
        {
            LivresEmpruntés = new List<Livre>();
            CDEmpruntés = new List<CD>();
        }

        public void AjouterLivre(Livre livreAajouter)
        {
            Livre nouveauLivre = new Livre(livreAajouter);
            nouveauLivre.NombreEnStock = 1;

            if (LivresEmpruntés.Where(livre => livre.Titre == nouveauLivre.Titre).Count() >= 1) // Si le livre est déja dans la liste de livres empruntés
            {
                LivresEmpruntés.Single(livre => livre.Titre == nouveauLivre.Titre).NombreEnStock++; // Ajout d'un en stock
            }
            else // Le livre n'existe pas déja
            {
                LivresEmpruntés.Add(nouveauLivre); // Ajout du nouveau livre
            }
        }

        public void AjouterCD(CD cdAajouter)
        {
            CD nouveauCD = new CD(cdAajouter);
            nouveauCD.NombreEnStock = 1;

            if (CDEmpruntés.Where(cd => cd.Titre == nouveauCD.Titre).Count() >= 1) // Si le cd est déja dans la liste de cds empruntés
            {
                CDEmpruntés.Single(cd => cd.Titre == nouveauCD.Titre).NombreEnStock++; // Ajout d'un en stock
            }
            else // Le cd n'existe pas déja
            {
                CDEmpruntés.Add(nouveauCD); // Ajout du nouveau cd
            }
        }

        public void EnleverCD(CD cd)
        {
            CDEmpruntés.Remove(cd);
        }

        public void EnleverLivre(Livre livre)
        {
            LivresEmpruntés.Remove(livre);
        }
    }
}
