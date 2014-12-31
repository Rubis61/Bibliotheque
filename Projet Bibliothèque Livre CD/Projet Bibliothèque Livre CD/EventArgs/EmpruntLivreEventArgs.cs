using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_Bibliothèque_Livre_CD
{
    public class EmpruntLivreEventArgs : EventArgs
    {
        public Livre livreEmprunté { get; set; }

        public EmpruntLivreEventArgs(Livre livre)
        {
            livreEmprunté = livre;
        }
    }
}
