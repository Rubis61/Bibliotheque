using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_Bibliothèque_Livre_CD
{
    class EmpruntCDEventArgs : EventArgs
    {
        public CD cdEmprunté { get; set; }
        
        public EmpruntCDEventArgs(CD cd)
        {
            cdEmprunté = cd;
        }
    }
}