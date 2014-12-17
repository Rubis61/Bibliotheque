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

        public void ajouterLivre(Livre livre)
        {
            LivresEmpruntés.Add(livre);
        }

        public void ajouterCD(CD cd)
        {
            CDEmpruntés.Add(cd);
        }

        public void enleverCD(CD cd)
        {
            CDEmpruntés.Remove(cd);
        }

        public void enleverLivre(Livre livre)
        {
            LivresEmpruntés.Remove(livre);
        }
    }
}
